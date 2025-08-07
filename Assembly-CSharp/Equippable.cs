using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x020008FB RID: 2299
[SerializationConfig(MemberSerialization.OptIn)]
public class Equippable : Assignable, ISaveLoadable, IGameObjectEffectDescriptor, IQuality
{
	// Token: 0x0600401A RID: 16410 RVA: 0x00167E5D File Offset: 0x0016605D
	public global::QualityLevel GetQuality()
	{
		return this.quality;
	}

	// Token: 0x0600401B RID: 16411 RVA: 0x00167E65 File Offset: 0x00166065
	public void SetQuality(global::QualityLevel level)
	{
		this.quality = level;
	}

	// Token: 0x17000499 RID: 1177
	// (get) Token: 0x0600401C RID: 16412 RVA: 0x00167E6E File Offset: 0x0016606E
	// (set) Token: 0x0600401D RID: 16413 RVA: 0x00167E7B File Offset: 0x0016607B
	public EquipmentDef def
	{
		get
		{
			return this.defHandle.Get<EquipmentDef>();
		}
		set
		{
			this.defHandle.Set<EquipmentDef>(value);
		}
	}

	// Token: 0x0600401E RID: 16414 RVA: 0x00167E8C File Offset: 0x0016608C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (this.def.AdditionalTags != null)
		{
			foreach (Tag tag in this.def.AdditionalTags)
			{
				base.GetComponent<KPrefabID>().AddTag(tag, false);
			}
		}
	}

	// Token: 0x0600401F RID: 16415 RVA: 0x00167EDC File Offset: 0x001660DC
	protected override void OnSpawn()
	{
		Components.AssignableItems.Add(this);
		if (this.isEquipped)
		{
			if (this.assignee != null && this.assignee is MinionIdentity)
			{
				this.assignee = (this.assignee as MinionIdentity).assignableProxy.Get();
				this.assignee_identityRef.Set(this.assignee as KMonoBehaviour);
			}
			if (this.assignee == null && this.assignee_identityRef.Get() != null)
			{
				this.assignee = this.assignee_identityRef.Get().GetComponent<IAssignableIdentity>();
			}
			if (this.assignee != null)
			{
				Equipment component = this.assignee.GetSoleOwner().GetComponent<Equipment>();
				bool flag = true;
				global::UnityEngine.Object component2 = component.GetComponent<MinionAssignablesProxy>();
				GameObject gameObject = null;
				if (component2 != null)
				{
					gameObject = component.GetComponent<MinionAssignablesProxy>().GetTargetGameObject();
					if (gameObject != null)
					{
						flag = gameObject.GetComponent<KPrefabID>().isSpawned;
					}
				}
				if (flag)
				{
					this.EquipToAssignable();
				}
				else
				{
					gameObject.Subscribe(1589886948, new Action<object>(this.OnAsigneeSpawnedAndReadyForEquip));
				}
			}
			else
			{
				global::Debug.LogWarning("Equippable trying to be equipped to missing prefab");
				this.isEquipped = false;
			}
		}
		base.Subscribe<Equippable>(1969584890, Equippable.SetDestroyedTrueDelegate);
	}

	// Token: 0x06004020 RID: 16416 RVA: 0x00168009 File Offset: 0x00166209
	private void EquipToAssignable()
	{
		if (this.assignee != null)
		{
			this.assignee.GetSoleOwner().GetComponent<Equipment>().Equip(this);
		}
	}

	// Token: 0x06004021 RID: 16417 RVA: 0x00168029 File Offset: 0x00166229
	private void OnAsigneeSpawnedAndReadyForEquip(object o)
	{
		GameObject gameObject = (GameObject)o;
		this.EquipToAssignable();
		gameObject.Unsubscribe(1589886948, new Action<object>(this.OnAsigneeSpawnedAndReadyForEquip));
	}

	// Token: 0x06004022 RID: 16418 RVA: 0x00168050 File Offset: 0x00166250
	public KAnimFile GetBuildOverride()
	{
		EquippableFacade component = base.GetComponent<EquippableFacade>();
		if (component == null || component.BuildOverride == null)
		{
			return this.def.BuildOverride;
		}
		return Assets.GetAnim(component.BuildOverride);
	}

	// Token: 0x06004023 RID: 16419 RVA: 0x00168094 File Offset: 0x00166294
	public override void Assign(IAssignableIdentity new_assignee)
	{
		if (new_assignee == this.assignee)
		{
			return;
		}
		if (base.slot != null && new_assignee is MinionIdentity)
		{
			new_assignee = (new_assignee as MinionIdentity).assignableProxy.Get();
		}
		if (base.slot != null && new_assignee is StoredMinionIdentity)
		{
			new_assignee = (new_assignee as StoredMinionIdentity).assignableProxy.Get();
		}
		if (new_assignee is MinionAssignablesProxy)
		{
			AssignableSlotInstance slot = new_assignee.GetSoleOwner().GetComponent<Equipment>().GetSlot(base.slot);
			if (slot != null)
			{
				Assignable assignable = slot.assignable;
				if (assignable != null)
				{
					assignable.Unassign();
				}
			}
		}
		base.Assign(new_assignee);
	}

	// Token: 0x06004024 RID: 16420 RVA: 0x00168130 File Offset: 0x00166330
	public override void Unassign()
	{
		if (this.isEquipped)
		{
			((this.assignee is MinionIdentity) ? ((MinionIdentity)this.assignee).assignableProxy.Get().GetComponent<Equipment>() : ((KMonoBehaviour)this.assignee).GetComponent<Equipment>()).Unequip(this);
			this.OnUnequip();
		}
		base.Unassign();
	}

	// Token: 0x06004025 RID: 16421 RVA: 0x00168190 File Offset: 0x00166390
	public void OnEquip(AssignableSlotInstance slot)
	{
		this.isEquipped = true;
		if (SelectTool.Instance.selected == this.selectable)
		{
			SelectTool.Instance.Select(null, false);
		}
		base.GetComponent<KBatchedAnimController>().enabled = false;
		base.GetComponent<KSelectable>().IsSelectable = false;
		string name = base.GetComponent<KPrefabID>().PrefabTag.Name;
		GameObject targetGameObject = slot.gameObject.GetComponent<MinionAssignablesProxy>().GetTargetGameObject();
		Effects component = targetGameObject.GetComponent<Effects>();
		if (component != null)
		{
			foreach (Effect effect in this.def.EffectImmunites)
			{
				component.AddImmunity(effect, name, true);
			}
		}
		if (this.def.OnEquipCallBack != null)
		{
			this.def.OnEquipCallBack(this);
		}
		base.GetComponent<KPrefabID>().AddTag(GameTags.Equipped, false);
		targetGameObject.Trigger(-210173199, this);
	}

	// Token: 0x06004026 RID: 16422 RVA: 0x0016829C File Offset: 0x0016649C
	public void OnUnequip()
	{
		this.isEquipped = false;
		if (this.destroyed)
		{
			return;
		}
		base.GetComponent<KPrefabID>().RemoveTag(GameTags.Equipped);
		base.GetComponent<KBatchedAnimController>().enabled = true;
		base.GetComponent<KSelectable>().IsSelectable = true;
		string name = base.GetComponent<KPrefabID>().PrefabTag.Name;
		if (this.assignee != null)
		{
			Ownables soleOwner = this.assignee.GetSoleOwner();
			if (soleOwner)
			{
				GameObject targetGameObject = soleOwner.GetComponent<MinionAssignablesProxy>().GetTargetGameObject();
				if (targetGameObject)
				{
					Effects component = targetGameObject.GetComponent<Effects>();
					if (component != null)
					{
						foreach (Effect effect in this.def.EffectImmunites)
						{
							component.RemoveImmunity(effect, name);
						}
					}
				}
			}
		}
		if (this.def.OnUnequipCallBack != null)
		{
			this.def.OnUnequipCallBack(this);
		}
		if (this.assignee != null)
		{
			Ownables soleOwner2 = this.assignee.GetSoleOwner();
			if (soleOwner2)
			{
				GameObject targetGameObject2 = soleOwner2.GetComponent<MinionAssignablesProxy>().GetTargetGameObject();
				if (targetGameObject2)
				{
					targetGameObject2.Trigger(-1841406856, this);
				}
			}
		}
	}

	// Token: 0x06004027 RID: 16423 RVA: 0x001683E4 File Offset: 0x001665E4
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		if (this.def != null)
		{
			List<Descriptor> equipmentEffects = GameUtil.GetEquipmentEffects(this.def);
			if (this.def.additionalDescriptors != null)
			{
				foreach (Descriptor descriptor in this.def.additionalDescriptors)
				{
					equipmentEffects.Add(descriptor);
				}
			}
			return equipmentEffects;
		}
		return new List<Descriptor>();
	}

	// Token: 0x040027CF RID: 10191
	private global::QualityLevel quality;

	// Token: 0x040027D0 RID: 10192
	[MyCmpAdd]
	private EquippableWorkable equippableWorkable;

	// Token: 0x040027D1 RID: 10193
	[MyCmpAdd]
	private EquippableFacade facade;

	// Token: 0x040027D2 RID: 10194
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x040027D3 RID: 10195
	public DefHandle defHandle;

	// Token: 0x040027D4 RID: 10196
	[Serialize]
	public bool isEquipped;

	// Token: 0x040027D5 RID: 10197
	private bool destroyed;

	// Token: 0x040027D6 RID: 10198
	[Serialize]
	public bool unequippable = true;

	// Token: 0x040027D7 RID: 10199
	[Serialize]
	public bool hideInCodex;

	// Token: 0x040027D8 RID: 10200
	private static readonly EventSystem.IntraObjectHandler<Equippable> SetDestroyedTrueDelegate = new EventSystem.IntraObjectHandler<Equippable>(delegate(Equippable component, object data)
	{
		component.destroyed = true;
	});
}
