using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000FF1 RID: 4081
	[SerializationConfig(MemberSerialization.OptIn)]
	[AddComponentMenu("KMonoBehaviour/scripts/Effects")]
	public class Effects : KMonoBehaviour, ISaveLoadable, ISim1000ms
	{
		// Token: 0x06007DFA RID: 32250 RVA: 0x00326FA7 File Offset: 0x003251A7
		protected override void OnPrefabInit()
		{
			this.autoRegisterSimRender = false;
		}

		// Token: 0x06007DFB RID: 32251 RVA: 0x00326FB0 File Offset: 0x003251B0
		protected override void OnSpawn()
		{
			if (this.saveLoadImmunities != null)
			{
				foreach (Effects.SaveLoadImmunities saveLoadImmunities in this.saveLoadImmunities)
				{
					if (Db.Get().effects.Exists(saveLoadImmunities.effectID))
					{
						Effect effect = Db.Get().effects.Get(saveLoadImmunities.effectID);
						this.AddImmunity(effect, saveLoadImmunities.giverID, true);
					}
				}
			}
			if (this.saveLoadEffects != null)
			{
				foreach (Effects.SaveLoadEffect saveLoadEffect in this.saveLoadEffects)
				{
					if (Db.Get().effects.Exists(saveLoadEffect.id))
					{
						Effect effect2 = Db.Get().effects.Get(saveLoadEffect.id);
						EffectInstance effectInstance = this.Add(effect2, true);
						if (effectInstance != null)
						{
							effectInstance.timeRemaining = saveLoadEffect.timeRemaining;
						}
					}
				}
			}
			if (this.effectsThatExpire.Count > 0)
			{
				SimAndRenderScheduler.instance.Add(this, this.simRenderLoadBalance);
			}
		}

		// Token: 0x06007DFC RID: 32252 RVA: 0x003270B4 File Offset: 0x003252B4
		public EffectInstance Get(string effect_id)
		{
			foreach (EffectInstance effectInstance in this.effects)
			{
				if (effectInstance.effect.Id == effect_id)
				{
					return effectInstance;
				}
			}
			return null;
		}

		// Token: 0x06007DFD RID: 32253 RVA: 0x0032711C File Offset: 0x0032531C
		public EffectInstance Get(HashedString effect_id)
		{
			foreach (EffectInstance effectInstance in this.effects)
			{
				if (effectInstance.effect.IdHash == effect_id)
				{
					return effectInstance;
				}
			}
			return null;
		}

		// Token: 0x06007DFE RID: 32254 RVA: 0x00327184 File Offset: 0x00325384
		public EffectInstance Get(Effect effect)
		{
			foreach (EffectInstance effectInstance in this.effects)
			{
				if (effectInstance.effect == effect)
				{
					return effectInstance;
				}
			}
			return null;
		}

		// Token: 0x06007DFF RID: 32255 RVA: 0x003271E0 File Offset: 0x003253E0
		public bool HasImmunityTo(Effect effect)
		{
			using (List<Effects.EffectImmunity>.Enumerator enumerator = this.effectImmunites.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.effect == effect)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06007E00 RID: 32256 RVA: 0x0032723C File Offset: 0x0032543C
		public EffectInstance Add(string effect_id, bool should_save)
		{
			Effect effect = Db.Get().effects.Get(effect_id);
			return this.Add(effect, should_save);
		}

		// Token: 0x06007E01 RID: 32257 RVA: 0x00327264 File Offset: 0x00325464
		public EffectInstance Add(HashedString effect_id, bool should_save)
		{
			Effect effect = Db.Get().effects.Get(effect_id);
			return this.Add(effect, should_save);
		}

		// Token: 0x06007E02 RID: 32258 RVA: 0x0032728C File Offset: 0x0032548C
		public EffectInstance Add(Effect newEffect, bool should_save)
		{
			if (this.HasImmunityTo(newEffect))
			{
				return null;
			}
			Traits component = base.GetComponent<Traits>();
			if (component != null && component.IsEffectIgnored(newEffect))
			{
				return null;
			}
			Attributes attributes = this.GetAttributes();
			EffectInstance effectInstance = this.Get(newEffect);
			if (!string.IsNullOrEmpty(newEffect.stompGroup))
			{
				for (int i = this.effects.Count - 1; i >= 0; i--)
				{
					if (this.effects[i] != effectInstance && !(this.effects[i].effect.stompGroup != newEffect.stompGroup) && this.effects[i].effect.stompPriority > newEffect.stompPriority)
					{
						return null;
					}
				}
				for (int j = this.effects.Count - 1; j >= 0; j--)
				{
					if (this.effects[j] != effectInstance && !(this.effects[j].effect.stompGroup != newEffect.stompGroup) && this.effects[j].effect.stompPriority <= newEffect.stompPriority)
					{
						this.Remove(this.effects[j].effect);
					}
				}
			}
			if (effectInstance == null)
			{
				effectInstance = new EffectInstance(base.gameObject, newEffect, should_save);
				newEffect.AddTo(attributes);
				this.effects.Add(effectInstance);
				if (newEffect.duration > 0f)
				{
					this.effectsThatExpire.Add(effectInstance);
					if (this.effectsThatExpire.Count == 1)
					{
						SimAndRenderScheduler.instance.Add(this, this.simRenderLoadBalance);
					}
				}
				if (newEffect.tag != null)
				{
					base.GetComponent<KPrefabID>().AddTag(newEffect.tag.Value, false);
				}
				base.Trigger(-1901442097, newEffect);
			}
			effectInstance.timeRemaining = newEffect.duration;
			return effectInstance;
		}

		// Token: 0x06007E03 RID: 32259 RVA: 0x0032746B File Offset: 0x0032566B
		public void Remove(Effect effect)
		{
			this.Remove(effect.IdHash);
		}

		// Token: 0x06007E04 RID: 32260 RVA: 0x0032747C File Offset: 0x0032567C
		public void Remove(HashedString effect_id)
		{
			int i = 0;
			while (i < this.effectsThatExpire.Count)
			{
				if (this.effectsThatExpire[i].effect.IdHash == effect_id)
				{
					int num = this.effectsThatExpire.Count - 1;
					this.effectsThatExpire[i] = this.effectsThatExpire[num];
					this.effectsThatExpire.RemoveAt(num);
					if (this.effectsThatExpire.Count == 0)
					{
						SimAndRenderScheduler.instance.Remove(this);
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			for (int j = 0; j < this.effects.Count; j++)
			{
				if (this.effects[j].effect.IdHash == effect_id)
				{
					Attributes attributes = this.GetAttributes();
					EffectInstance effectInstance = this.effects[j];
					effectInstance.OnCleanUp();
					Effect effect = effectInstance.effect;
					effect.RemoveFrom(attributes);
					int num2 = this.effects.Count - 1;
					this.effects[j] = this.effects[num2];
					this.effects.RemoveAt(num2);
					if (effect.tag != null)
					{
						base.GetComponent<KPrefabID>().RemoveTag(effect.tag.Value);
					}
					base.Trigger(-1157678353, effect);
					return;
				}
			}
		}

		// Token: 0x06007E05 RID: 32261 RVA: 0x003275D8 File Offset: 0x003257D8
		public void Remove(string effect_id)
		{
			int i = 0;
			while (i < this.effectsThatExpire.Count)
			{
				if (this.effectsThatExpire[i].effect.Id == effect_id)
				{
					int num = this.effectsThatExpire.Count - 1;
					this.effectsThatExpire[i] = this.effectsThatExpire[num];
					this.effectsThatExpire.RemoveAt(num);
					if (this.effectsThatExpire.Count == 0)
					{
						SimAndRenderScheduler.instance.Remove(this);
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			for (int j = 0; j < this.effects.Count; j++)
			{
				if (this.effects[j].effect.Id == effect_id)
				{
					Attributes attributes = this.GetAttributes();
					EffectInstance effectInstance = this.effects[j];
					effectInstance.OnCleanUp();
					Effect effect = effectInstance.effect;
					effect.RemoveFrom(attributes);
					int num2 = this.effects.Count - 1;
					this.effects[j] = this.effects[num2];
					this.effects.RemoveAt(num2);
					base.Trigger(-1157678353, effect);
					return;
				}
			}
		}

		// Token: 0x06007E06 RID: 32262 RVA: 0x0032770C File Offset: 0x0032590C
		public bool HasEffect(HashedString effect_id)
		{
			using (List<EffectInstance>.Enumerator enumerator = this.effects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.effect.IdHash == effect_id)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06007E07 RID: 32263 RVA: 0x00327770 File Offset: 0x00325970
		public bool HasEffect(string effect_id)
		{
			using (List<EffectInstance>.Enumerator enumerator = this.effects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.effect.Id == effect_id)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06007E08 RID: 32264 RVA: 0x003277D4 File Offset: 0x003259D4
		public bool HasEffect(Effect effect)
		{
			using (List<EffectInstance>.Enumerator enumerator = this.effects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.effect == effect)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06007E09 RID: 32265 RVA: 0x00327830 File Offset: 0x00325A30
		public void Sim1000ms(float dt)
		{
			for (int i = 0; i < this.effectsThatExpire.Count; i++)
			{
				EffectInstance effectInstance = this.effectsThatExpire[i];
				if (effectInstance.IsExpired())
				{
					this.Remove(effectInstance.effect);
				}
				effectInstance.timeRemaining -= dt;
			}
		}

		// Token: 0x06007E0A RID: 32266 RVA: 0x00327884 File Offset: 0x00325A84
		public void AddImmunity(Effect effect, string giverID, bool shouldSave = true)
		{
			if (giverID != null)
			{
				foreach (Effects.EffectImmunity effectImmunity in this.effectImmunites)
				{
					if (effectImmunity.giverID == giverID && effectImmunity.effect == effect)
					{
						return;
					}
				}
			}
			Effects.EffectImmunity effectImmunity2 = new Effects.EffectImmunity(effect, giverID, shouldSave);
			this.effectImmunites.Add(effectImmunity2);
			base.Trigger(1152870979, effectImmunity2);
		}

		// Token: 0x06007E0B RID: 32267 RVA: 0x00327914 File Offset: 0x00325B14
		public void RemoveImmunity(Effect effect, string ID)
		{
			Effects.EffectImmunity effectImmunity = default(Effects.EffectImmunity);
			bool flag = false;
			foreach (Effects.EffectImmunity effectImmunity2 in this.effectImmunites)
			{
				if (effectImmunity2.effect == effect && (ID == null || ID == effectImmunity2.giverID))
				{
					effectImmunity = effectImmunity2;
					flag = true;
				}
			}
			if (flag)
			{
				this.effectImmunites.Remove(effectImmunity);
				base.Trigger(964452195, effectImmunity);
			}
		}

		// Token: 0x06007E0C RID: 32268 RVA: 0x003279AC File Offset: 0x00325BAC
		[OnSerializing]
		internal void OnSerializing()
		{
			List<Effects.SaveLoadEffect> list = new List<Effects.SaveLoadEffect>();
			foreach (EffectInstance effectInstance in this.effects)
			{
				if (effectInstance.shouldSave)
				{
					Effects.SaveLoadEffect saveLoadEffect = new Effects.SaveLoadEffect
					{
						id = effectInstance.effect.Id,
						timeRemaining = effectInstance.timeRemaining,
						saved = true
					};
					list.Add(saveLoadEffect);
				}
			}
			this.saveLoadEffects = list.ToArray();
			List<Effects.SaveLoadImmunities> list2 = new List<Effects.SaveLoadImmunities>();
			foreach (Effects.EffectImmunity effectImmunity in this.effectImmunites)
			{
				if (effectImmunity.shouldSave)
				{
					Effect effect = effectImmunity.effect;
					Effects.SaveLoadImmunities saveLoadImmunities = new Effects.SaveLoadImmunities
					{
						effectID = effect.Id,
						giverID = effectImmunity.giverID,
						saved = true
					};
					list2.Add(saveLoadImmunities);
				}
			}
			this.saveLoadImmunities = list2.ToArray();
		}

		// Token: 0x06007E0D RID: 32269 RVA: 0x00327AE8 File Offset: 0x00325CE8
		public List<Effects.SaveLoadImmunities> GetAllImmunitiesForSerialization()
		{
			List<Effects.SaveLoadImmunities> list = new List<Effects.SaveLoadImmunities>();
			foreach (Effects.EffectImmunity effectImmunity in this.effectImmunites)
			{
				Effect effect = effectImmunity.effect;
				Effects.SaveLoadImmunities saveLoadImmunities = new Effects.SaveLoadImmunities
				{
					effectID = effect.Id,
					giverID = effectImmunity.giverID,
					saved = effectImmunity.shouldSave
				};
				list.Add(saveLoadImmunities);
			}
			return list;
		}

		// Token: 0x06007E0E RID: 32270 RVA: 0x00327B80 File Offset: 0x00325D80
		public List<Effects.SaveLoadEffect> GetAllEffectsForSerialization()
		{
			List<Effects.SaveLoadEffect> list = new List<Effects.SaveLoadEffect>();
			foreach (EffectInstance effectInstance in this.effects)
			{
				Effects.SaveLoadEffect saveLoadEffect = new Effects.SaveLoadEffect
				{
					id = effectInstance.effect.Id,
					timeRemaining = effectInstance.timeRemaining,
					saved = effectInstance.shouldSave
				};
				list.Add(saveLoadEffect);
			}
			return list;
		}

		// Token: 0x06007E0F RID: 32271 RVA: 0x00327C14 File Offset: 0x00325E14
		public List<EffectInstance> GetTimeLimitedEffects()
		{
			return this.effectsThatExpire;
		}

		// Token: 0x06007E10 RID: 32272 RVA: 0x00327C1C File Offset: 0x00325E1C
		public void CopyEffects(Effects source)
		{
			foreach (EffectInstance effectInstance in source.effects)
			{
				this.Add(effectInstance.effect, effectInstance.shouldSave).timeRemaining = effectInstance.timeRemaining;
			}
			foreach (EffectInstance effectInstance2 in source.effectsThatExpire)
			{
				this.Add(effectInstance2.effect, effectInstance2.shouldSave).timeRemaining = effectInstance2.timeRemaining;
			}
		}

		// Token: 0x04005F0A RID: 24330
		[Serialize]
		private Effects.SaveLoadEffect[] saveLoadEffects;

		// Token: 0x04005F0B RID: 24331
		[Serialize]
		private Effects.SaveLoadImmunities[] saveLoadImmunities;

		// Token: 0x04005F0C RID: 24332
		private List<EffectInstance> effects = new List<EffectInstance>();

		// Token: 0x04005F0D RID: 24333
		private List<EffectInstance> effectsThatExpire = new List<EffectInstance>();

		// Token: 0x04005F0E RID: 24334
		private List<Effects.EffectImmunity> effectImmunites = new List<Effects.EffectImmunity>();

		// Token: 0x020025CC RID: 9676
		[Serializable]
		public struct EffectImmunity
		{
			// Token: 0x0600C197 RID: 49559 RVA: 0x00406E47 File Offset: 0x00405047
			public EffectImmunity(Effect e, string id, bool save = true)
			{
				this.giverID = id;
				this.effect = e;
				this.shouldSave = save;
			}

			// Token: 0x0400A8CA RID: 43210
			public string giverID;

			// Token: 0x0400A8CB RID: 43211
			public Effect effect;

			// Token: 0x0400A8CC RID: 43212
			public bool shouldSave;
		}

		// Token: 0x020025CD RID: 9677
		[Serializable]
		public struct SaveLoadImmunities
		{
			// Token: 0x0400A8CD RID: 43213
			public string giverID;

			// Token: 0x0400A8CE RID: 43214
			public string effectID;

			// Token: 0x0400A8CF RID: 43215
			public bool saved;
		}

		// Token: 0x020025CE RID: 9678
		[Serializable]
		public struct SaveLoadEffect
		{
			// Token: 0x0400A8D0 RID: 43216
			public string id;

			// Token: 0x0400A8D1 RID: 43217
			public float timeRemaining;

			// Token: 0x0400A8D2 RID: 43218
			public bool saved;
		}
	}
}
