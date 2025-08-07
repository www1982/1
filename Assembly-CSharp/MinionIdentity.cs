using System;
using System.Collections.Generic;
using Klei.AI;
using Klei.CustomSettings;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020005E1 RID: 1505
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/MinionIdentity")]
public class MinionIdentity : KMonoBehaviour, ISaveLoadable, IAssignableIdentity, IListableOption, ISim1000ms
{
	// Token: 0x1700016B RID: 363
	// (get) Token: 0x060022D6 RID: 8918 RVA: 0x000C7F94 File Offset: 0x000C6194
	// (set) Token: 0x060022D7 RID: 8919 RVA: 0x000C7F9C File Offset: 0x000C619C
	[Serialize]
	public string genderStringKey { get; set; }

	// Token: 0x1700016C RID: 364
	// (get) Token: 0x060022D8 RID: 8920 RVA: 0x000C7FA5 File Offset: 0x000C61A5
	// (set) Token: 0x060022D9 RID: 8921 RVA: 0x000C7FAD File Offset: 0x000C61AD
	[Serialize]
	public string nameStringKey { get; set; }

	// Token: 0x1700016D RID: 365
	// (get) Token: 0x060022DA RID: 8922 RVA: 0x000C7FB6 File Offset: 0x000C61B6
	// (set) Token: 0x060022DB RID: 8923 RVA: 0x000C7FBE File Offset: 0x000C61BE
	[Serialize]
	public HashedString personalityResourceId { get; set; }

	// Token: 0x060022DC RID: 8924 RVA: 0x000C7FC7 File Offset: 0x000C61C7
	public static void DestroyStatics()
	{
		MinionIdentity.maleNameList = null;
		MinionIdentity.femaleNameList = null;
	}

	// Token: 0x060022DD RID: 8925 RVA: 0x000C7FD8 File Offset: 0x000C61D8
	protected override void OnPrefabInit()
	{
		if (this.name == null)
		{
			this.name = MinionIdentity.ChooseRandomName();
		}
		if (GameClock.Instance != null)
		{
			this.arrivalTime = (float)GameClock.Instance.GetCycle();
		}
		KAnimControllerBase component = base.GetComponent<KAnimControllerBase>();
		if (component != null)
		{
			KAnimControllerBase kanimControllerBase = component;
			kanimControllerBase.OnUpdateBounds = (Action<Bounds>)Delegate.Combine(kanimControllerBase.OnUpdateBounds, new Action<Bounds>(this.OnUpdateBounds));
		}
		GameUtil.SubscribeToTags<MinionIdentity>(this, MinionIdentity.OnDeadTagAddedDelegate, true);
		base.Subscribe<MinionIdentity>(1502190696, MinionIdentity.OnQueueDestroyObjectDelegate);
	}

	// Token: 0x060022DE RID: 8926 RVA: 0x000C8068 File Offset: 0x000C6268
	protected override void OnSpawn()
	{
		if (this.addToIdentityList)
		{
			this.ValidateProxy();
			this.CleanupLimboMinions();
		}
		PathProber component = base.GetComponent<PathProber>();
		if (component != null)
		{
			component.SetGroupProber(MinionGroupProber.Get());
		}
		this.SetName(this.name);
		if (this.nameStringKey == null)
		{
			this.nameStringKey = this.name;
		}
		this.SetGender(this.gender);
		if (this.genderStringKey == null)
		{
			this.genderStringKey = "NB";
		}
		if (this.personalityResourceId == HashedString.Invalid)
		{
			Personality personalityFromNameStringKey = Db.Get().Personalities.GetPersonalityFromNameStringKey(this.nameStringKey);
			if (personalityFromNameStringKey != null)
			{
				this.personalityResourceId = personalityFromNameStringKey.Id;
			}
		}
		if (!this.model.IsValid)
		{
			Personality personalityFromNameStringKey2 = Db.Get().Personalities.GetPersonalityFromNameStringKey(this.nameStringKey);
			if (personalityFromNameStringKey2 != null)
			{
				this.model = personalityFromNameStringKey2.model;
			}
		}
		if (this.addToIdentityList)
		{
			Components.MinionIdentities.Add(this);
			if (!Components.MinionIdentitiesByModel.ContainsKey(this.model))
			{
				Components.MinionIdentitiesByModel[this.model] = new Components.Cmps<MinionIdentity>();
			}
			Components.MinionIdentitiesByModel[this.model].Add(this);
			if (!base.gameObject.HasTag(GameTags.Dead))
			{
				Components.LiveMinionIdentities.Add(this);
				if (!Components.LiveMinionIdentitiesByModel.ContainsKey(this.model))
				{
					Components.LiveMinionIdentitiesByModel[this.model] = new Components.Cmps<MinionIdentity>();
				}
				Components.LiveMinionIdentitiesByModel[this.model].Add(this);
				Game.Instance.Trigger(2144209314, this);
			}
		}
		SymbolOverrideController component2 = base.GetComponent<SymbolOverrideController>();
		if (component2 != null)
		{
			Accessorizer component3 = base.gameObject.GetComponent<Accessorizer>();
			if (component3 != null)
			{
				string text = HashCache.Get().Get(component3.GetAccessory(Db.Get().AccessorySlots.Mouth).symbol.hash).Replace("mouth", "cheek");
				component2.AddSymbolOverride("snapto_cheek", Assets.GetAnim("head_swap_kanim").GetData().build.GetSymbol(text), 1);
				component2.AddSymbolOverride("snapto_hair_always", component3.GetAccessory(Db.Get().AccessorySlots.Hair).symbol, 1);
				component2.AddSymbolOverride(Db.Get().AccessorySlots.HatHair.targetSymbolId, Db.Get().AccessorySlots.HatHair.Lookup("hat_" + HashCache.Get().Get(component3.GetAccessory(Db.Get().AccessorySlots.Hair).symbol.hash)).symbol, 1);
			}
		}
		this.voiceId = (this.voiceIdx + 1).ToString("D2");
		Prioritizable component4 = base.GetComponent<Prioritizable>();
		if (component4 != null)
		{
			component4.showIcon = false;
		}
		Pickupable component5 = base.GetComponent<Pickupable>();
		if (component5 != null)
		{
			component5.carryAnimOverride = Assets.GetAnim("anim_incapacitated_carrier_kanim");
		}
		this.ApplyCustomGameSettings();
	}

	// Token: 0x060022DF RID: 8927 RVA: 0x000C83AC File Offset: 0x000C65AC
	public void ValidateProxy()
	{
		this.assignableProxy = MinionAssignablesProxy.InitAssignableProxy(this.assignableProxy, this);
	}

	// Token: 0x060022E0 RID: 8928 RVA: 0x000C83C0 File Offset: 0x000C65C0
	private void CleanupLimboMinions()
	{
		KPrefabID component = base.GetComponent<KPrefabID>();
		if (component.InstanceID == -1)
		{
			DebugUtil.LogWarningArgs(new object[] { "Minion with an invalid kpid! Attempting to recover...", this.name });
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
			DebugUtil.LogWarningArgs(new object[] { "Minion with a conflicted kpid! Attempting to recover... ", component.InstanceID, this.name });
			if (KPrefabIDTracker.Get().GetInstance(component.InstanceID) != null)
			{
				KPrefabIDTracker.Get().Unregister(component);
			}
			component.InstanceID = KPrefabID.GetUniqueID();
			KPrefabIDTracker.Get().Register(component);
			DebugUtil.LogWarningArgs(new object[] { "Restored as:", component.InstanceID });
		}
		this.assignableProxy.Get().SetTarget(this, base.gameObject);
	}

	// Token: 0x060022E1 RID: 8929 RVA: 0x000C84F9 File Offset: 0x000C66F9
	public string GetProperName()
	{
		return base.gameObject.GetProperName();
	}

	// Token: 0x060022E2 RID: 8930 RVA: 0x000C8506 File Offset: 0x000C6706
	public string GetVoiceId()
	{
		return this.voiceId;
	}

	// Token: 0x060022E3 RID: 8931 RVA: 0x000C850E File Offset: 0x000C670E
	public void SetName(string name)
	{
		this.name = name;
		if (this.selectable != null)
		{
			this.selectable.SetName(name);
		}
		base.gameObject.name = name;
		NameDisplayScreen.Instance.UpdateName(base.gameObject);
	}

	// Token: 0x060022E4 RID: 8932 RVA: 0x000C854D File Offset: 0x000C674D
	public void SetStickerType(string stickerType)
	{
		this.stickerType = stickerType;
	}

	// Token: 0x060022E5 RID: 8933 RVA: 0x000C8556 File Offset: 0x000C6756
	public bool IsNull()
	{
		return this == null;
	}

	// Token: 0x060022E6 RID: 8934 RVA: 0x000C855F File Offset: 0x000C675F
	public void SetGender(string gender)
	{
		this.gender = gender;
		this.selectable.SetGender(gender);
	}

	// Token: 0x060022E7 RID: 8935 RVA: 0x000C8574 File Offset: 0x000C6774
	public static string ChooseRandomName()
	{
		if (MinionIdentity.femaleNameList == null)
		{
			MinionIdentity.maleNameList = new MinionIdentity.NameList(Game.Instance.maleNamesFile);
			MinionIdentity.femaleNameList = new MinionIdentity.NameList(Game.Instance.femaleNamesFile);
		}
		if (global::UnityEngine.Random.value > 0.5f)
		{
			return MinionIdentity.maleNameList.Next();
		}
		return MinionIdentity.femaleNameList.Next();
	}

	// Token: 0x060022E8 RID: 8936 RVA: 0x000C85D1 File Offset: 0x000C67D1
	private void OnQueueDestroyObject()
	{
		this.RemoveFromComponentsLists();
	}

	// Token: 0x060022E9 RID: 8937 RVA: 0x000C85DC File Offset: 0x000C67DC
	private void RemoveFromComponentsLists()
	{
		Components.MinionIdentities.Remove(this);
		if (Components.MinionIdentitiesByModel.ContainsKey(this.model))
		{
			Components.MinionIdentitiesByModel[this.model].Remove(this);
		}
		Components.LiveMinionIdentities.Remove(this);
		if (Components.LiveMinionIdentitiesByModel.ContainsKey(this.model))
		{
			Components.LiveMinionIdentitiesByModel[this.model].Remove(this);
		}
	}

	// Token: 0x060022EA RID: 8938 RVA: 0x000C8650 File Offset: 0x000C6850
	protected override void OnCleanUp()
	{
		if (this.assignableProxy != null)
		{
			MinionAssignablesProxy minionAssignablesProxy = this.assignableProxy.Get();
			if (minionAssignablesProxy && minionAssignablesProxy.target == this)
			{
				Util.KDestroyGameObject(minionAssignablesProxy.gameObject);
			}
		}
		this.RemoveFromComponentsLists();
		Game.Instance.Trigger(2144209314, this);
	}

	// Token: 0x060022EB RID: 8939 RVA: 0x000C86A3 File Offset: 0x000C68A3
	private void OnUpdateBounds(Bounds bounds)
	{
		KBoxCollider2D component = base.GetComponent<KBoxCollider2D>();
		component.offset = bounds.center;
		component.size = bounds.extents;
	}

	// Token: 0x060022EC RID: 8940 RVA: 0x000C86D0 File Offset: 0x000C68D0
	private void OnDied(object data)
	{
		this.GetSoleOwner().UnassignAll();
		this.GetEquipment().UnequipAll();
		Components.LiveMinionIdentities.Remove(this);
		if (Components.LiveMinionIdentitiesByModel.ContainsKey(this.model))
		{
			Components.LiveMinionIdentitiesByModel[this.model].Remove(this);
		}
		Game.Instance.Trigger(-1523247426, this);
		Game.Instance.Trigger(2144209314, this);
	}

	// Token: 0x060022ED RID: 8941 RVA: 0x000C8746 File Offset: 0x000C6946
	public List<Ownables> GetOwners()
	{
		return this.assignableProxy.Get().ownables;
	}

	// Token: 0x060022EE RID: 8942 RVA: 0x000C8758 File Offset: 0x000C6958
	public Ownables GetSoleOwner()
	{
		return this.assignableProxy.Get().GetComponent<Ownables>();
	}

	// Token: 0x060022EF RID: 8943 RVA: 0x000C876A File Offset: 0x000C696A
	public bool HasOwner(Assignables owner)
	{
		return this.GetOwners().Contains(owner as Ownables);
	}

	// Token: 0x060022F0 RID: 8944 RVA: 0x000C877D File Offset: 0x000C697D
	public int NumOwners()
	{
		return this.GetOwners().Count;
	}

	// Token: 0x060022F1 RID: 8945 RVA: 0x000C878A File Offset: 0x000C698A
	public Equipment GetEquipment()
	{
		return this.assignableProxy.Get().GetComponent<Equipment>();
	}

	// Token: 0x060022F2 RID: 8946 RVA: 0x000C879C File Offset: 0x000C699C
	public void Sim1000ms(float dt)
	{
		if (this == null)
		{
			return;
		}
		if (this.navigator == null)
		{
			this.navigator = base.GetComponent<Navigator>();
		}
		if (this.navigator != null && !this.navigator.IsMoving())
		{
			return;
		}
		if (this.choreDriver == null)
		{
			this.choreDriver = base.GetComponent<ChoreDriver>();
		}
		if (this.choreDriver != null)
		{
			Chore currentChore = this.choreDriver.GetCurrentChore();
			if (currentChore != null && currentChore is FetchAreaChore)
			{
				MinionResume component = base.GetComponent<MinionResume>();
				if (component != null)
				{
					component.AddExperienceWithAptitude(Db.Get().SkillGroups.Hauling.Id, dt, SKILLS.ALL_DAY_EXPERIENCE);
				}
			}
		}
	}

	// Token: 0x060022F3 RID: 8947 RVA: 0x000C8858 File Offset: 0x000C6A58
	private void ApplyCustomGameSettings()
	{
		SettingLevel currentQualitySetting = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.ImmuneSystem);
		if (currentQualitySetting.id == "Compromised")
		{
			Db.Get().Attributes.DiseaseCureSpeed.Lookup(this).Add(new AttributeModifier(Db.Get().Attributes.DiseaseCureSpeed.Id, -0.3333f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.IMMUNESYSTEM.LEVELS.COMPROMISED.ATTRIBUTE_MODIFIER_NAME, false, false, true));
			Db.Get().Attributes.GermResistance.Lookup(this).Add(new AttributeModifier(Db.Get().Attributes.GermResistance.Id, -2f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.IMMUNESYSTEM.LEVELS.COMPROMISED.ATTRIBUTE_MODIFIER_NAME, false, false, true));
		}
		else if (currentQualitySetting.id == "Weak")
		{
			Db.Get().Attributes.GermResistance.Lookup(this).Add(new AttributeModifier(Db.Get().Attributes.GermResistance.Id, -1f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.IMMUNESYSTEM.LEVELS.WEAK.ATTRIBUTE_MODIFIER_NAME, false, false, true));
		}
		else if (currentQualitySetting.id == "Strong")
		{
			Db.Get().Attributes.DiseaseCureSpeed.Lookup(this).Add(new AttributeModifier(Db.Get().Attributes.DiseaseCureSpeed.Id, 2f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.IMMUNESYSTEM.LEVELS.STRONG.ATTRIBUTE_MODIFIER_NAME, false, false, true));
			Db.Get().Attributes.GermResistance.Lookup(this).Add(new AttributeModifier(Db.Get().Attributes.GermResistance.Id, 2f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.IMMUNESYSTEM.LEVELS.STRONG.ATTRIBUTE_MODIFIER_NAME, false, false, true));
		}
		else if (currentQualitySetting.id == "Invincible")
		{
			Db.Get().Attributes.DiseaseCureSpeed.Lookup(this).Add(new AttributeModifier(Db.Get().Attributes.DiseaseCureSpeed.Id, 100000000f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.IMMUNESYSTEM.LEVELS.INVINCIBLE.ATTRIBUTE_MODIFIER_NAME, false, false, true));
			Db.Get().Attributes.GermResistance.Lookup(this).Add(new AttributeModifier(Db.Get().Attributes.GermResistance.Id, 200f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.IMMUNESYSTEM.LEVELS.INVINCIBLE.ATTRIBUTE_MODIFIER_NAME, false, false, true));
		}
		SettingLevel currentQualitySetting2 = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.Stress);
		if (currentQualitySetting2.id == "Doomed")
		{
			Db.Get().Amounts.Stress.deltaAttribute.Lookup(this).Add(new AttributeModifier(Db.Get().Amounts.Stress.deltaAttribute.Id, 0.033333335f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.STRESS.LEVELS.DOOMED.ATTRIBUTE_MODIFIER_NAME, false, false, true));
		}
		else if (currentQualitySetting2.id == "Pessimistic")
		{
			Db.Get().Amounts.Stress.deltaAttribute.Lookup(this).Add(new AttributeModifier(Db.Get().Amounts.Stress.deltaAttribute.Id, 0.016666668f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.STRESS.LEVELS.PESSIMISTIC.ATTRIBUTE_MODIFIER_NAME, false, false, true));
		}
		else if (currentQualitySetting2.id == "Optimistic")
		{
			Db.Get().Amounts.Stress.deltaAttribute.Lookup(this).Add(new AttributeModifier(Db.Get().Amounts.Stress.deltaAttribute.Id, -0.016666668f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.STRESS.LEVELS.OPTIMISTIC.ATTRIBUTE_MODIFIER_NAME, false, false, true));
		}
		else if (currentQualitySetting2.id == "Indomitable")
		{
			Db.Get().Amounts.Stress.deltaAttribute.Lookup(this).Add(new AttributeModifier(Db.Get().Amounts.Stress.deltaAttribute.Id, float.NegativeInfinity, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.STRESS.LEVELS.INDOMITABLE.ATTRIBUTE_MODIFIER_NAME, false, false, true));
		}
		SettingLevel currentQualitySetting3 = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.CalorieBurn);
		if (currentQualitySetting3.id == "VeryHard")
		{
			Db.Get().Amounts.Calories.deltaAttribute.Lookup(this).Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, DUPLICANTSTATS.STANDARD.BaseStats.CALORIES_BURNED_PER_SECOND * 1f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.CALORIE_BURN.LEVELS.VERYHARD.ATTRIBUTE_MODIFIER_NAME, false, false, true));
			return;
		}
		if (currentQualitySetting3.id == "Hard")
		{
			Db.Get().Amounts.Calories.deltaAttribute.Lookup(this).Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, DUPLICANTSTATS.STANDARD.BaseStats.CALORIES_BURNED_PER_SECOND * 0.5f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.CALORIE_BURN.LEVELS.HARD.ATTRIBUTE_MODIFIER_NAME, false, false, true));
			return;
		}
		if (currentQualitySetting3.id == "Easy")
		{
			Db.Get().Amounts.Calories.deltaAttribute.Lookup(this).Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, DUPLICANTSTATS.STANDARD.BaseStats.CALORIES_BURNED_PER_SECOND * -0.5f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.CALORIE_BURN.LEVELS.EASY.ATTRIBUTE_MODIFIER_NAME, false, false, true));
			return;
		}
		if (currentQualitySetting3.id == "Disabled")
		{
			Db.Get().Amounts.Calories.deltaAttribute.Lookup(this).Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, float.PositiveInfinity, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.CALORIE_BURN.LEVELS.DISABLED.ATTRIBUTE_MODIFIER_NAME, false, false, true));
		}
	}

	// Token: 0x060022F4 RID: 8948 RVA: 0x000C8E20 File Offset: 0x000C7020
	public static float GetCalorieBurnMultiplier()
	{
		float num = 1f;
		SettingLevel currentQualitySetting = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.CalorieBurn);
		if (currentQualitySetting.id == "VeryHard")
		{
			num = 2f;
		}
		else if (currentQualitySetting.id == "Hard")
		{
			num = 1.5f;
		}
		else if (currentQualitySetting.id == "Easy")
		{
			num = 0.5f;
		}
		else if (currentQualitySetting.id == "Disabled")
		{
			num = 0f;
		}
		return num;
	}

	// Token: 0x04001437 RID: 5175
	public const string HairAlwaysSymbol = "snapto_hair_always";

	// Token: 0x04001438 RID: 5176
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x04001439 RID: 5177
	[MyCmpReq]
	public Modifiers modifiers;

	// Token: 0x0400143A RID: 5178
	public int femaleVoiceCount;

	// Token: 0x0400143B RID: 5179
	public int maleVoiceCount;

	// Token: 0x0400143C RID: 5180
	[Serialize]
	public Tag model;

	// Token: 0x0400143D RID: 5181
	[Serialize]
	private new string name;

	// Token: 0x0400143E RID: 5182
	[Serialize]
	public string gender;

	// Token: 0x04001442 RID: 5186
	[Serialize]
	public string stickerType;

	// Token: 0x04001443 RID: 5187
	[Serialize]
	[ReadOnly]
	public float arrivalTime;

	// Token: 0x04001444 RID: 5188
	[Serialize]
	public int voiceIdx;

	// Token: 0x04001445 RID: 5189
	[Serialize]
	public Ref<MinionAssignablesProxy> assignableProxy;

	// Token: 0x04001446 RID: 5190
	private Navigator navigator;

	// Token: 0x04001447 RID: 5191
	private ChoreDriver choreDriver;

	// Token: 0x04001448 RID: 5192
	public float timeLastSpoke;

	// Token: 0x04001449 RID: 5193
	private string voiceId;

	// Token: 0x0400144A RID: 5194
	private KAnimHashedString overrideExpression;

	// Token: 0x0400144B RID: 5195
	private KAnimHashedString expression;

	// Token: 0x0400144C RID: 5196
	public bool addToIdentityList = true;

	// Token: 0x0400144D RID: 5197
	private static MinionIdentity.NameList maleNameList;

	// Token: 0x0400144E RID: 5198
	private static MinionIdentity.NameList femaleNameList;

	// Token: 0x0400144F RID: 5199
	private static readonly EventSystem.IntraObjectHandler<MinionIdentity> OnDeadTagAddedDelegate = GameUtil.CreateHasTagHandler<MinionIdentity>(GameTags.Dead, delegate(MinionIdentity component, object data)
	{
		component.OnDied(data);
	});

	// Token: 0x04001450 RID: 5200
	private static readonly EventSystem.IntraObjectHandler<MinionIdentity> OnQueueDestroyObjectDelegate = new EventSystem.IntraObjectHandler<MinionIdentity>(delegate(MinionIdentity component, object data)
	{
		component.OnQueueDestroyObject();
	});

	// Token: 0x0200146F RID: 5231
	private class NameList
	{
		// Token: 0x06008DBA RID: 36282 RVA: 0x0035963C File Offset: 0x0035783C
		public NameList(TextAsset file)
		{
			string[] array = file.text.Replace("  ", " ").Replace("\r\n", "\n").Split('\n', StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(' ', StringSplitOptions.None);
				if (array2[array2.Length - 1] != "" && array2[array2.Length - 1] != null)
				{
					this.names.Add(array2[array2.Length - 1]);
				}
			}
			this.names.Shuffle<string>();
		}

		// Token: 0x06008DBB RID: 36283 RVA: 0x003596DC File Offset: 0x003578DC
		public string Next()
		{
			List<string> list = this.names;
			int num = this.idx;
			this.idx = num + 1;
			return list[num % this.names.Count];
		}

		// Token: 0x04006C9B RID: 27803
		private List<string> names = new List<string>();

		// Token: 0x04006C9C RID: 27804
		private int idx;
	}
}
