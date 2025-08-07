using System;
using System.Collections.Generic;
using KSerialization;
using TUNING;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x0200100D RID: 4109
	[SerializationConfig(MemberSerialization.OptIn)]
	[AddComponentMenu("KMonoBehaviour/scripts/Traits")]
	public class Traits : KMonoBehaviour, ISaveLoadable
	{
		// Token: 0x06007EB1 RID: 32433 RVA: 0x00329F1E File Offset: 0x0032811E
		public List<string> GetTraitIds()
		{
			return this.TraitIds;
		}

		// Token: 0x06007EB2 RID: 32434 RVA: 0x00329F26 File Offset: 0x00328126
		public void SetTraitIds(List<string> traits)
		{
			this.TraitIds = traits;
		}

		// Token: 0x06007EB3 RID: 32435 RVA: 0x00329F30 File Offset: 0x00328130
		protected override void OnSpawn()
		{
			foreach (string text in this.TraitIds)
			{
				if (Db.Get().traits.Exists(text))
				{
					Trait trait = Db.Get().traits.Get(text);
					if (Game.IsCorrectDlcActiveForCurrentSave(trait))
					{
						this.AddInternal(trait);
					}
				}
			}
			if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 15))
			{
				List<DUPLICANTSTATS.TraitVal> joytraits = DUPLICANTSTATS.JOYTRAITS;
				if (base.GetComponent<MinionIdentity>())
				{
					bool flag = true;
					foreach (DUPLICANTSTATS.TraitVal traitVal in joytraits)
					{
						if (this.HasTrait(traitVal.id))
						{
							flag = false;
						}
					}
					if (flag)
					{
						DUPLICANTSTATS.TraitVal random = joytraits.GetRandom<DUPLICANTSTATS.TraitVal>();
						Trait trait2 = Db.Get().traits.Get(random.id);
						this.Add(trait2);
					}
				}
			}
		}

		// Token: 0x06007EB4 RID: 32436 RVA: 0x0032A058 File Offset: 0x00328258
		private void AddInternal(Trait trait)
		{
			if (!this.HasTrait(trait))
			{
				this.TraitList.Add(trait);
				trait.AddTo(this.GetAttributes());
				if (trait.OnAddTrait != null)
				{
					trait.OnAddTrait(base.gameObject);
				}
			}
		}

		// Token: 0x06007EB5 RID: 32437 RVA: 0x0032A094 File Offset: 0x00328294
		public void Add(Trait trait)
		{
			DebugUtil.Assert(base.IsInitialized() || base.GetComponent<Modifiers>().IsInitialized(), "Tried adding a trait on a prefab, use Modifiers.initialTraits instead!", trait.Name, base.gameObject.name);
			if (trait.ShouldSave)
			{
				this.TraitIds.Add(trait.Id);
			}
			this.AddInternal(trait);
		}

		// Token: 0x06007EB6 RID: 32438 RVA: 0x0032A0F4 File Offset: 0x003282F4
		public bool HasTrait(string trait_id)
		{
			bool flag = false;
			using (List<Trait>.Enumerator enumerator = this.TraitList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Id == trait_id)
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06007EB7 RID: 32439 RVA: 0x0032A154 File Offset: 0x00328354
		public bool HasTrait(Trait trait)
		{
			using (List<Trait>.Enumerator enumerator = this.TraitList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == trait)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06007EB8 RID: 32440 RVA: 0x0032A1AC File Offset: 0x003283AC
		public void Clear()
		{
			while (this.TraitList.Count > 0)
			{
				this.Remove(this.TraitList[0]);
			}
		}

		// Token: 0x06007EB9 RID: 32441 RVA: 0x0032A1D0 File Offset: 0x003283D0
		public void Remove(Trait trait)
		{
			for (int i = 0; i < this.TraitList.Count; i++)
			{
				if (this.TraitList[i] == trait)
				{
					this.TraitList.RemoveAt(i);
					this.TraitIds.Remove(trait.Id);
					trait.RemoveFrom(this.GetAttributes());
					return;
				}
			}
		}

		// Token: 0x06007EBA RID: 32442 RVA: 0x0032A230 File Offset: 0x00328430
		public bool IsEffectIgnored(Effect effect)
		{
			foreach (Trait trait in this.TraitList)
			{
				if (trait.ignoredEffects != null && Array.IndexOf<string>(trait.ignoredEffects, effect.Id) != -1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007EBB RID: 32443 RVA: 0x0032A2A0 File Offset: 0x003284A0
		public bool IsChoreGroupDisabled(ChoreGroup choreGroup)
		{
			Trait trait;
			return this.IsChoreGroupDisabled(choreGroup, out trait);
		}

		// Token: 0x06007EBC RID: 32444 RVA: 0x0032A2B6 File Offset: 0x003284B6
		public bool IsChoreGroupDisabled(ChoreGroup choreGroup, out Trait disablingTrait)
		{
			return this.IsChoreGroupDisabled(choreGroup.IdHash, out disablingTrait);
		}

		// Token: 0x06007EBD RID: 32445 RVA: 0x0032A2C8 File Offset: 0x003284C8
		public bool IsChoreGroupDisabled(HashedString choreGroupId)
		{
			Trait trait;
			return this.IsChoreGroupDisabled(choreGroupId, out trait);
		}

		// Token: 0x06007EBE RID: 32446 RVA: 0x0032A2E0 File Offset: 0x003284E0
		public bool IsChoreGroupDisabled(HashedString choreGroupId, out Trait disablingTrait)
		{
			foreach (Trait trait in this.TraitList)
			{
				if (trait.disabledChoreGroups != null)
				{
					ChoreGroup[] disabledChoreGroups = trait.disabledChoreGroups;
					for (int i = 0; i < disabledChoreGroups.Length; i++)
					{
						if (disabledChoreGroups[i].IdHash == choreGroupId)
						{
							disablingTrait = trait;
							return true;
						}
					}
				}
			}
			disablingTrait = null;
			return false;
		}

		// Token: 0x04005F79 RID: 24441
		public List<Trait> TraitList = new List<Trait>();

		// Token: 0x04005F7A RID: 24442
		[Serialize]
		private List<string> TraitIds = new List<string>();
	}
}
