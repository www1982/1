using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Database;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200061E RID: 1566
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/StoredMinionIdentity")]
public class StoredMinionIdentity : KMonoBehaviour, ISaveLoadable, IAssignableIdentity, IListableOption, IPersonalPriorityManager
{
	// Token: 0x170001B0 RID: 432
	// (get) Token: 0x060025D4 RID: 9684 RVA: 0x000D85E7 File Offset: 0x000D67E7
	// (set) Token: 0x060025D5 RID: 9685 RVA: 0x000D85EF File Offset: 0x000D67EF
	[Serialize]
	public string genderStringKey { get; set; }

	// Token: 0x170001B1 RID: 433
	// (get) Token: 0x060025D6 RID: 9686 RVA: 0x000D85F8 File Offset: 0x000D67F8
	// (set) Token: 0x060025D7 RID: 9687 RVA: 0x000D8600 File Offset: 0x000D6800
	[Serialize]
	public string nameStringKey { get; set; }

	// Token: 0x170001B2 RID: 434
	// (get) Token: 0x060025D8 RID: 9688 RVA: 0x000D8609 File Offset: 0x000D6809
	// (set) Token: 0x060025D9 RID: 9689 RVA: 0x000D8611 File Offset: 0x000D6811
	[Serialize]
	public HashedString personalityResourceId { get; set; }

	// Token: 0x060025DA RID: 9690 RVA: 0x000D861C File Offset: 0x000D681C
	[OnDeserialized]
	[Obsolete]
	private void OnDeserializedMethod()
	{
		if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 7))
		{
			int num = 0;
			foreach (KeyValuePair<string, bool> keyValuePair in this.MasteryByRoleID)
			{
				if (keyValuePair.Value && keyValuePair.Key != "NoRole")
				{
					num++;
				}
			}
			this.TotalExperienceGained = MinionResume.CalculatePreviousExperienceBar(num);
			foreach (KeyValuePair<HashedString, float> keyValuePair2 in this.AptitudeByRoleGroup)
			{
				this.AptitudeBySkillGroup[keyValuePair2.Key] = keyValuePair2.Value;
			}
		}
		if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 29))
		{
			this.forbiddenTagSet = new HashSet<Tag>(this.forbiddenTags);
			this.forbiddenTags = null;
		}
		if (!this.model.IsValid)
		{
			this.model = MinionConfig.MODEL;
		}
		if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 30))
		{
			this.bodyData = Accessorizer.UpdateAccessorySlots(this.nameStringKey, ref this.accessories);
		}
		if (this.clothingItems.Count > 0)
		{
			this.customClothingItems[ClothingOutfitUtility.OutfitType.Clothing] = new List<ResourceRef<ClothingItemResource>>(this.clothingItems);
			this.clothingItems.Clear();
		}
		List<ResourceRef<Accessory>> list = this.accessories.FindAll((ResourceRef<Accessory> acc) => acc.Get() == null);
		if (list.Count > 0)
		{
			List<ClothingItemResource> list2 = new List<ClothingItemResource>();
			foreach (ResourceRef<Accessory> resourceRef in list)
			{
				ClothingItemResource clothingItemResource = Db.Get().Permits.ClothingItems.TryResolveAccessoryResource(resourceRef.Guid);
				if (clothingItemResource != null && !list2.Contains(clothingItemResource))
				{
					list2.Add(clothingItemResource);
					this.customClothingItems[ClothingOutfitUtility.OutfitType.Clothing].Add(new ResourceRef<ClothingItemResource>(clothingItemResource));
				}
			}
			this.bodyData = Accessorizer.UpdateAccessorySlots(this.nameStringKey, ref this.accessories);
		}
		this.OnDeserializeModifiers();
	}

	// Token: 0x060025DB RID: 9691 RVA: 0x000D888C File Offset: 0x000D6A8C
	public bool HasPerk(SkillPerk perk)
	{
		foreach (KeyValuePair<string, bool> keyValuePair in this.MasteryBySkillID)
		{
			if (keyValuePair.Value && Db.Get().Skills.Get(keyValuePair.Key).perks.Contains(perk))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060025DC RID: 9692 RVA: 0x000D890C File Offset: 0x000D6B0C
	public bool HasMasteredSkill(string skillId)
	{
		return this.MasteryBySkillID.ContainsKey(skillId) && this.MasteryBySkillID[skillId];
	}

	// Token: 0x060025DD RID: 9693 RVA: 0x000D892A File Offset: 0x000D6B2A
	protected override void OnPrefabInit()
	{
		this.assignableProxy = new Ref<MinionAssignablesProxy>();
		this.minionModifiers = base.GetComponent<MinionModifiers>();
		this.savedAttributeValues = new Dictionary<string, float>();
	}

	// Token: 0x060025DE RID: 9694 RVA: 0x000D8950 File Offset: 0x000D6B50
	[OnSerializing]
	private void OnSerialize()
	{
		this.savedAttributeValues.Clear();
		foreach (AttributeInstance attributeInstance in this.minionModifiers.attributes)
		{
			this.savedAttributeValues.Add(attributeInstance.Attribute.Id, attributeInstance.GetTotalValue());
		}
	}

	// Token: 0x060025DF RID: 9695 RVA: 0x000D89C4 File Offset: 0x000D6BC4
	protected override void OnSpawn()
	{
		string[] array = MinionConfig.GetAttributes();
		string[] array2 = MinionConfig.GetAmounts();
		AttributeModifier[] array3 = MinionConfig.GetTraits();
		if (this.model == BionicMinionConfig.MODEL)
		{
			array = BionicMinionConfig.GetAttributes();
			array2 = BionicMinionConfig.GetAmounts();
			array3 = BionicMinionConfig.GetTraits();
		}
		BaseMinionConfig.AddMinionAttributes(this.minionModifiers, array);
		BaseMinionConfig.AddMinionAmounts(this.minionModifiers, array2);
		BaseMinionConfig.AddMinionTraits(BaseMinionConfig.GetMinionNameForModel(this.model), BaseMinionConfig.GetMinionBaseTraitIDForModel(this.model), this.minionModifiers, array3);
		this.ValidateProxy();
		this.CleanupLimboMinions();
	}

	// Token: 0x060025E0 RID: 9696 RVA: 0x000D8A4D File Offset: 0x000D6C4D
	public void OnHardDelete()
	{
		if (this.assignableProxy.Get() != null)
		{
			Util.KDestroyGameObject(this.assignableProxy.Get().gameObject);
		}
		ScheduleManager.Instance.OnStoredDupeDestroyed(this);
		Components.StoredMinionIdentities.Remove(this);
	}

	// Token: 0x060025E1 RID: 9697 RVA: 0x000D8A90 File Offset: 0x000D6C90
	private void OnDeserializeModifiers()
	{
		foreach (KeyValuePair<string, float> keyValuePair in this.savedAttributeValues)
		{
			Klei.AI.Attribute attribute = Db.Get().Attributes.TryGet(keyValuePair.Key);
			if (attribute == null)
			{
				attribute = Db.Get().BuildingAttributes.TryGet(keyValuePair.Key);
			}
			if (attribute != null)
			{
				if (this.minionModifiers.attributes.Get(attribute.Id) != null)
				{
					this.minionModifiers.attributes.Get(attribute.Id).Modifiers.Clear();
					this.minionModifiers.attributes.Get(attribute.Id).ClearModifiers();
				}
				else
				{
					this.minionModifiers.attributes.Add(attribute);
				}
				this.minionModifiers.attributes.Add(new AttributeModifier(attribute.Id, keyValuePair.Value, () => DUPLICANTS.ATTRIBUTES.STORED_VALUE, false, false));
			}
		}
	}

	// Token: 0x060025E2 RID: 9698 RVA: 0x000D8BC4 File Offset: 0x000D6DC4
	public void ValidateProxy()
	{
		this.assignableProxy = MinionAssignablesProxy.InitAssignableProxy(this.assignableProxy, this);
	}

	// Token: 0x060025E3 RID: 9699 RVA: 0x000D8BD8 File Offset: 0x000D6DD8
	public string[] GetClothingItemIds(ClothingOutfitUtility.OutfitType outfitType)
	{
		if (this.customClothingItems.ContainsKey(outfitType))
		{
			string[] array = new string[this.customClothingItems[outfitType].Count];
			for (int i = 0; i < this.customClothingItems[outfitType].Count; i++)
			{
				array[i] = this.customClothingItems[outfitType][i].Get().Id;
			}
			return array;
		}
		return null;
	}

	// Token: 0x060025E4 RID: 9700 RVA: 0x000D8C48 File Offset: 0x000D6E48
	private void CleanupLimboMinions()
	{
		KPrefabID component = base.GetComponent<KPrefabID>();
		bool flag = false;
		if (component.InstanceID == -1)
		{
			DebugUtil.LogWarningArgs(new object[] { "Stored minion with an invalid kpid! Attempting to recover...", this.storedName });
			flag = true;
			if (KPrefabIDTracker.Get().GetInstance(component.InstanceID) != null)
			{
				KPrefabIDTracker.Get().Unregister(component);
			}
			component.InstanceID = KPrefabID.GetUniqueID();
			KPrefabIDTracker.Get().Register(component);
			DebugUtil.LogWarningArgs(new object[] { "Restored as:", component.InstanceID });
		}
		if (component.conflicted)
		{
			DebugUtil.LogWarningArgs(new object[] { "Minion with a conflicted kpid! Attempting to recover... ", component.InstanceID, this.storedName });
			if (KPrefabIDTracker.Get().GetInstance(component.InstanceID) != null)
			{
				KPrefabIDTracker.Get().Unregister(component);
			}
			component.InstanceID = KPrefabID.GetUniqueID();
			KPrefabIDTracker.Get().Register(component);
			DebugUtil.LogWarningArgs(new object[] { "Restored as:", component.InstanceID });
		}
		this.assignableProxy.Get().SetTarget(this, base.gameObject);
		bool flag2 = false;
		foreach (MinionStorage minionStorage in Components.MinionStorages.Items)
		{
			List<MinionStorage.Info> storedMinionInfo = minionStorage.GetStoredMinionInfo();
			for (int i = 0; i < storedMinionInfo.Count; i++)
			{
				MinionStorage.Info info = storedMinionInfo[i];
				if (flag && info.serializedMinion != null && info.serializedMinion.GetId() == -1 && info.name == this.storedName)
				{
					DebugUtil.LogWarningArgs(new object[]
					{
						"Found a minion storage with an invalid ref, rebinding.",
						component.InstanceID,
						this.storedName,
						minionStorage.gameObject.name
					});
					info = new MinionStorage.Info(this.storedName, new Ref<KPrefabID>(component));
					storedMinionInfo[i] = info;
					minionStorage.GetComponent<Assignable>().Assign(this);
					flag2 = true;
					break;
				}
				if (info.serializedMinion != null && info.serializedMinion.Get() == component)
				{
					flag2 = true;
					break;
				}
			}
			if (flag2)
			{
				break;
			}
		}
		if (!flag2)
		{
			DebugUtil.LogWarningArgs(new object[] { "Found a stored minion that wasn't in any minion storage. Respawning them at the portal.", component.InstanceID, this.storedName });
			GameObject activeTelepad = GameUtil.GetActiveTelepad();
			if (activeTelepad != null)
			{
				MinionStorage.DeserializeMinion(component.gameObject, activeTelepad.transform.GetPosition());
			}
		}
	}

	// Token: 0x060025E5 RID: 9701 RVA: 0x000D8F28 File Offset: 0x000D7128
	public string GetProperName()
	{
		return this.storedName;
	}

	// Token: 0x060025E6 RID: 9702 RVA: 0x000D8F30 File Offset: 0x000D7130
	public List<Ownables> GetOwners()
	{
		return this.assignableProxy.Get().ownables;
	}

	// Token: 0x060025E7 RID: 9703 RVA: 0x000D8F42 File Offset: 0x000D7142
	public Ownables GetSoleOwner()
	{
		return this.assignableProxy.Get().GetComponent<Ownables>();
	}

	// Token: 0x060025E8 RID: 9704 RVA: 0x000D8F54 File Offset: 0x000D7154
	public bool HasOwner(Assignables owner)
	{
		return this.GetOwners().Contains(owner as Ownables);
	}

	// Token: 0x060025E9 RID: 9705 RVA: 0x000D8F67 File Offset: 0x000D7167
	public int NumOwners()
	{
		return this.GetOwners().Count;
	}

	// Token: 0x060025EA RID: 9706 RVA: 0x000D8F74 File Offset: 0x000D7174
	public Accessory GetAccessory(AccessorySlot slot)
	{
		for (int i = 0; i < this.accessories.Count; i++)
		{
			if (this.accessories[i].Get() != null && this.accessories[i].Get().slot == slot)
			{
				return this.accessories[i].Get();
			}
		}
		return null;
	}

	// Token: 0x060025EB RID: 9707 RVA: 0x000D8FD6 File Offset: 0x000D71D6
	public bool IsNull()
	{
		return this == null;
	}

	// Token: 0x060025EC RID: 9708 RVA: 0x000D8FE0 File Offset: 0x000D71E0
	public string GetStorageReason()
	{
		KPrefabID component = base.GetComponent<KPrefabID>();
		foreach (MinionStorage minionStorage in Components.MinionStorages.Items)
		{
			using (List<MinionStorage.Info>.Enumerator enumerator2 = minionStorage.GetStoredMinionInfo().GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.serializedMinion.Get() == component)
					{
						return minionStorage.GetProperName();
					}
				}
			}
		}
		return "";
	}

	// Token: 0x060025ED RID: 9709 RVA: 0x000D9098 File Offset: 0x000D7298
	public bool IsPermittedToConsume(string consumable)
	{
		return !this.forbiddenTagSet.Contains(consumable);
	}

	// Token: 0x060025EE RID: 9710 RVA: 0x000D90B0 File Offset: 0x000D72B0
	public bool IsChoreGroupDisabled(ChoreGroup chore_group)
	{
		foreach (string text in this.traitIDs)
		{
			if (Db.Get().traits.Exists(text))
			{
				Trait trait = Db.Get().traits.Get(text);
				if (trait.disabledChoreGroups != null)
				{
					ChoreGroup[] disabledChoreGroups = trait.disabledChoreGroups;
					for (int i = 0; i < disabledChoreGroups.Length; i++)
					{
						if (disabledChoreGroups[i].IdHash == chore_group.IdHash)
						{
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x060025EF RID: 9711 RVA: 0x000D9160 File Offset: 0x000D7360
	public int GetPersonalPriority(ChoreGroup chore_group)
	{
		ChoreConsumer.PriorityInfo priorityInfo;
		if (this.choreGroupPriorities.TryGetValue(chore_group.IdHash, out priorityInfo))
		{
			return priorityInfo.priority;
		}
		return 0;
	}

	// Token: 0x060025F0 RID: 9712 RVA: 0x000D918A File Offset: 0x000D738A
	public int GetAssociatedSkillLevel(ChoreGroup group)
	{
		return 0;
	}

	// Token: 0x060025F1 RID: 9713 RVA: 0x000D918D File Offset: 0x000D738D
	public void SetPersonalPriority(ChoreGroup group, int value)
	{
	}

	// Token: 0x060025F2 RID: 9714 RVA: 0x000D918F File Offset: 0x000D738F
	public void ResetPersonalPriorities()
	{
	}

	// Token: 0x04001630 RID: 5680
	[Serialize]
	public string storedName;

	// Token: 0x04001631 RID: 5681
	[Serialize]
	public Tag model;

	// Token: 0x04001632 RID: 5682
	[Serialize]
	public string gender;

	// Token: 0x04001636 RID: 5686
	[Serialize]
	[ReadOnly]
	public float arrivalTime;

	// Token: 0x04001637 RID: 5687
	[Serialize]
	public int voiceIdx;

	// Token: 0x04001638 RID: 5688
	[Serialize]
	public KCompBuilder.BodyData bodyData;

	// Token: 0x04001639 RID: 5689
	[Serialize]
	public List<Ref<KPrefabID>> assignedItems;

	// Token: 0x0400163A RID: 5690
	[Serialize]
	public List<Ref<KPrefabID>> equippedItems;

	// Token: 0x0400163B RID: 5691
	[Serialize]
	public List<string> traitIDs;

	// Token: 0x0400163C RID: 5692
	[Serialize]
	public List<ResourceRef<Accessory>> accessories;

	// Token: 0x0400163D RID: 5693
	[Obsolete("Deprecated, use customClothingItems")]
	[Serialize]
	public List<ResourceRef<ClothingItemResource>> clothingItems = new List<ResourceRef<ClothingItemResource>>();

	// Token: 0x0400163E RID: 5694
	[Serialize]
	public Dictionary<ClothingOutfitUtility.OutfitType, List<ResourceRef<ClothingItemResource>>> customClothingItems = new Dictionary<ClothingOutfitUtility.OutfitType, List<ResourceRef<ClothingItemResource>>>();

	// Token: 0x0400163F RID: 5695
	[Serialize]
	public Dictionary<WearableAccessorizer.WearableType, WearableAccessorizer.Wearable> wearables = new Dictionary<WearableAccessorizer.WearableType, WearableAccessorizer.Wearable>();

	// Token: 0x04001640 RID: 5696
	[Obsolete("Deprecated, use forbiddenTagSet")]
	[Serialize]
	public List<Tag> forbiddenTags;

	// Token: 0x04001641 RID: 5697
	[Serialize]
	public HashSet<Tag> forbiddenTagSet;

	// Token: 0x04001642 RID: 5698
	[Serialize]
	public Ref<MinionAssignablesProxy> assignableProxy;

	// Token: 0x04001643 RID: 5699
	[Serialize]
	public List<Effects.SaveLoadEffect> saveLoadEffects;

	// Token: 0x04001644 RID: 5700
	[Serialize]
	public List<Effects.SaveLoadImmunities> saveLoadImmunities;

	// Token: 0x04001645 RID: 5701
	[Serialize]
	public Dictionary<string, bool> MasteryByRoleID = new Dictionary<string, bool>();

	// Token: 0x04001646 RID: 5702
	[Serialize]
	public Dictionary<string, bool> MasteryBySkillID = new Dictionary<string, bool>();

	// Token: 0x04001647 RID: 5703
	[Serialize]
	public List<string> grantedSkillIDs = new List<string>();

	// Token: 0x04001648 RID: 5704
	[Serialize]
	public Dictionary<HashedString, float> AptitudeByRoleGroup = new Dictionary<HashedString, float>();

	// Token: 0x04001649 RID: 5705
	[Serialize]
	public Dictionary<HashedString, float> AptitudeBySkillGroup = new Dictionary<HashedString, float>();

	// Token: 0x0400164A RID: 5706
	[Serialize]
	public float TotalExperienceGained;

	// Token: 0x0400164B RID: 5707
	[Serialize]
	public string currentHat;

	// Token: 0x0400164C RID: 5708
	[Serialize]
	public string targetHat;

	// Token: 0x0400164D RID: 5709
	[Serialize]
	public Dictionary<HashedString, ChoreConsumer.PriorityInfo> choreGroupPriorities = new Dictionary<HashedString, ChoreConsumer.PriorityInfo>();

	// Token: 0x0400164E RID: 5710
	[Serialize]
	public List<AttributeLevels.LevelSaveLoad> attributeLevels;

	// Token: 0x0400164F RID: 5711
	[Serialize]
	public Dictionary<string, float> savedAttributeValues;

	// Token: 0x04001650 RID: 5712
	public MinionModifiers minionModifiers;

	// Token: 0x020014B8 RID: 5304
	public interface IStoredMinionExtension
	{
		// Token: 0x06008EB2 RID: 36530
		void PushTo(StoredMinionIdentity destination);

		// Token: 0x06008EB3 RID: 36531
		void PullFrom(StoredMinionIdentity source);

		// Token: 0x06008EB4 RID: 36532
		void AddStoredMinionGameObjectRequirements(GameObject storedMinionGameObject);
	}
}
