using System;
using System.Collections.Generic;
using Klei;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000BB9 RID: 3001
[AddComponentMenu("KMonoBehaviour/Workable/TinkerStation")]
public class TinkerStation : Workable, IGameObjectEffectDescriptor, ISim1000ms
{
	// Token: 0x17000686 RID: 1670
	// (set) Token: 0x060059BC RID: 22972 RVA: 0x00206A47 File Offset: 0x00204C47
	public AttributeConverter AttributeConverter
	{
		set
		{
			this.attributeConverter = value;
		}
	}

	// Token: 0x17000687 RID: 1671
	// (set) Token: 0x060059BD RID: 22973 RVA: 0x00206A50 File Offset: 0x00204C50
	public float AttributeExperienceMultiplier
	{
		set
		{
			this.attributeExperienceMultiplier = value;
		}
	}

	// Token: 0x17000688 RID: 1672
	// (set) Token: 0x060059BE RID: 22974 RVA: 0x00206A59 File Offset: 0x00204C59
	public string SkillExperienceSkillGroup
	{
		set
		{
			this.skillExperienceSkillGroup = value;
		}
	}

	// Token: 0x17000689 RID: 1673
	// (set) Token: 0x060059BF RID: 22975 RVA: 0x00206A62 File Offset: 0x00204C62
	public float SkillExperienceMultiplier
	{
		set
		{
			this.skillExperienceMultiplier = value;
		}
	}

	// Token: 0x060059C0 RID: 22976 RVA: 0x00206A6C File Offset: 0x00204C6C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.attributeConverter = Db.Get().AttributeConverters.MachinerySpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.MOST_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Technicals.Id;
		this.skillExperienceMultiplier = SKILLS.MOST_DAY_EXPERIENCE;
		if (this.useFilteredStorage)
		{
			ChoreType byHash = Db.Get().ChoreTypes.GetByHash(this.fetchChoreType);
			this.filteredStorage = new FilteredStorage(this, null, null, false, byHash);
		}
		base.Subscribe<TinkerStation>(-592767678, TinkerStation.OnOperationalChangedDelegate);
	}

	// Token: 0x060059C1 RID: 22977 RVA: 0x00206B03 File Offset: 0x00204D03
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.useFilteredStorage && this.filteredStorage != null)
		{
			this.filteredStorage.FilterChanged();
		}
	}

	// Token: 0x060059C2 RID: 22978 RVA: 0x00206B26 File Offset: 0x00204D26
	protected override void OnCleanUp()
	{
		if (this.filteredStorage != null)
		{
			this.filteredStorage.CleanUp();
		}
		base.OnCleanUp();
	}

	// Token: 0x060059C3 RID: 22979 RVA: 0x00206B44 File Offset: 0x00204D44
	private bool CorrectRolePrecondition(MinionIdentity worker)
	{
		MinionResume component = worker.GetComponent<MinionResume>();
		return component != null && component.HasPerk(this.requiredSkillPerk);
	}

	// Token: 0x060059C4 RID: 22980 RVA: 0x00206B74 File Offset: 0x00204D74
	private void OnOperationalChanged(object data)
	{
		RoomTracker component = base.GetComponent<RoomTracker>();
		if (component != null && component.room != null)
		{
			component.room.RetriggerBuildings();
		}
	}

	// Token: 0x060059C5 RID: 22981 RVA: 0x00206BA4 File Offset: 0x00204DA4
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		if (!this.operational.IsOperational)
		{
			return;
		}
		base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.ComplexFabricatorProducing, this);
		this.operational.SetActive(true, false);
	}

	// Token: 0x060059C6 RID: 22982 RVA: 0x00206BE4 File Offset: 0x00204DE4
	protected override void OnStopWork(WorkerBase worker)
	{
		base.OnStopWork(worker);
		base.ShowProgressBar(false);
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.ComplexFabricatorProducing, this);
		this.operational.SetActive(false, false);
	}

	// Token: 0x060059C7 RID: 22983 RVA: 0x00206C24 File Offset: 0x00204E24
	protected override void OnCompleteWork(WorkerBase worker)
	{
		base.OnCompleteWork(worker);
		PrimaryElement primaryElement = this.storage.FindFirstWithMass(this.inputMaterial, this.massPerTinker);
		if (primaryElement != null)
		{
			SimHashes elementID = primaryElement.ElementID;
			float num = 1f;
			float num2;
			SimUtil.DiseaseInfo diseaseInfo;
			this.storage.ConsumeAndGetDisease(elementID.CreateTag(), this.massPerTinker, out num2, out diseaseInfo, out num);
			GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab(this.outputPrefab), base.transform.GetPosition() + Vector3.up, Grid.SceneLayer.Ore, null, 0);
			PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
			component.SetElement(elementID, true);
			component.Temperature = num;
			gameObject.SetActive(true);
		}
		this.chore = null;
	}

	// Token: 0x060059C8 RID: 22984 RVA: 0x00206CCD File Offset: 0x00204ECD
	public void Sim1000ms(float dt)
	{
		this.UpdateChore();
	}

	// Token: 0x060059C9 RID: 22985 RVA: 0x00206CD8 File Offset: 0x00204ED8
	private void UpdateChore()
	{
		if (this.operational.IsOperational && (this.ToolsRequested() || this.alwaysTinker) && this.HasMaterial())
		{
			if (this.chore == null)
			{
				this.chore = new WorkChore<TinkerStation>(Db.Get().ChoreTypes.GetByHash(this.choreType), this, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
				this.chore.AddPrecondition(ChorePreconditions.instance.HasSkillPerk, this.requiredSkillPerk);
				base.SetWorkTime(this.toolProductionTime);
				return;
			}
		}
		else if (this.chore != null)
		{
			this.chore.Cancel("Can't tinker");
			this.chore = null;
		}
	}

	// Token: 0x060059CA RID: 22986 RVA: 0x00206D8B File Offset: 0x00204F8B
	private bool HasMaterial()
	{
		return this.storage.FindFirstWithMass(this.inputMaterial, this.massPerTinker) != null;
	}

	// Token: 0x060059CB RID: 22987 RVA: 0x00206DAC File Offset: 0x00204FAC
	private bool ToolsRequested()
	{
		return MaterialNeeds.GetAmount(this.outputPrefab, base.gameObject.GetMyWorldId(), false) > 0f && this.GetMyWorld().worldInventory.GetAmount(this.outputPrefab, true) <= 0f;
	}

	// Token: 0x060059CC RID: 22988 RVA: 0x00206DFC File Offset: 0x00204FFC
	public override List<Descriptor> GetDescriptors(GameObject go)
	{
		string text = this.inputMaterial.ProperName();
		List<Descriptor> descriptors = base.GetDescriptors(go);
		descriptors.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTCONSUMEDPERUSE, text, GameUtil.GetFormattedMass(this.massPerTinker, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTCONSUMEDPERUSE, text, GameUtil.GetFormattedMass(this.massPerTinker, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), Descriptor.DescriptorType.Requirement, false));
		descriptors.AddRange(GameUtil.GetAllDescriptors(Assets.GetPrefab(this.outputPrefab), false));
		List<Tinkerable> list = new List<Tinkerable>();
		foreach (GameObject gameObject in Assets.GetPrefabsWithComponent<Tinkerable>())
		{
			Tinkerable component = gameObject.GetComponent<Tinkerable>();
			if (component.tinkerMaterialTag == this.outputPrefab)
			{
				list.Add(component);
			}
		}
		if (list.Count > 0)
		{
			Effect effect = Db.Get().effects.Get(list[0].addedEffect);
			descriptors.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.ADDED_EFFECT, effect.Name), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ADDED_EFFECT, effect.Name, Effect.CreateTooltip(effect, true, "\n    • ", true)), Descriptor.DescriptorType.Effect, false));
			descriptors.Add(new Descriptor(this.EffectTitle, this.EffectTooltip, Descriptor.DescriptorType.Effect, false));
			foreach (Tinkerable tinkerable in list)
			{
				Descriptor descriptor = new Descriptor(string.Format(this.EffectItemString, tinkerable.GetProperName()), string.Format(this.EffectItemTooltip, tinkerable.GetProperName()), Descriptor.DescriptorType.Effect, false);
				descriptor.IncreaseIndent();
				descriptors.Add(descriptor);
			}
		}
		return descriptors;
	}

	// Token: 0x060059CD RID: 22989 RVA: 0x00206FEC File Offset: 0x002051EC
	public static TinkerStation AddTinkerStation(GameObject go, string required_room_type)
	{
		TinkerStation tinkerStation = go.AddOrGet<TinkerStation>();
		go.AddOrGet<RoomTracker>().requiredRoomType = required_room_type;
		return tinkerStation;
	}

	// Token: 0x04003B90 RID: 15248
	public HashedString choreType;

	// Token: 0x04003B91 RID: 15249
	public HashedString fetchChoreType;

	// Token: 0x04003B92 RID: 15250
	private Chore chore;

	// Token: 0x04003B93 RID: 15251
	[MyCmpAdd]
	private Operational operational;

	// Token: 0x04003B94 RID: 15252
	[MyCmpAdd]
	private Storage storage;

	// Token: 0x04003B95 RID: 15253
	public bool useFilteredStorage;

	// Token: 0x04003B96 RID: 15254
	protected FilteredStorage filteredStorage;

	// Token: 0x04003B97 RID: 15255
	public float toolProductionTime = 160f;

	// Token: 0x04003B98 RID: 15256
	public bool alwaysTinker;

	// Token: 0x04003B99 RID: 15257
	public float massPerTinker;

	// Token: 0x04003B9A RID: 15258
	public Tag inputMaterial;

	// Token: 0x04003B9B RID: 15259
	public Tag outputPrefab;

	// Token: 0x04003B9C RID: 15260
	public float outputTemperature;

	// Token: 0x04003B9D RID: 15261
	public string EffectTitle = UI.BUILDINGEFFECTS.IMPROVED_BUILDINGS;

	// Token: 0x04003B9E RID: 15262
	public string EffectTooltip = UI.BUILDINGEFFECTS.TOOLTIPS.IMPROVED_BUILDINGS;

	// Token: 0x04003B9F RID: 15263
	public string EffectItemString = UI.BUILDINGEFFECTS.IMPROVED_BUILDINGS_ITEM;

	// Token: 0x04003BA0 RID: 15264
	public string EffectItemTooltip = UI.BUILDINGEFFECTS.TOOLTIPS.IMPROVED_BUILDINGS_ITEM;

	// Token: 0x04003BA1 RID: 15265
	private static readonly EventSystem.IntraObjectHandler<TinkerStation> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<TinkerStation>(delegate(TinkerStation component, object data)
	{
		component.OnOperationalChanged(data);
	});
}
