using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000BBA RID: 3002
[AddComponentMenu("KMonoBehaviour/Workable/Tinkerable")]
public class Tinkerable : Workable
{
	// Token: 0x060059D0 RID: 22992 RVA: 0x0020707C File Offset: 0x0020527C
	public static Tinkerable MakePowerTinkerable(GameObject prefab)
	{
		RoomTracker roomTracker = prefab.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.PowerPlant.Id;
		roomTracker.requirement = RoomTracker.Requirement.TrackingOnly;
		Tinkerable tinkerable = prefab.AddOrGet<Tinkerable>();
		tinkerable.tinkerMaterialTag = PowerControlStationConfig.TINKER_TOOLS;
		tinkerable.tinkerMaterialAmount = 1f;
		tinkerable.tinkerMass = 5f;
		tinkerable.requiredSkillPerk = PowerControlStationConfig.ROLE_PERK;
		tinkerable.onCompleteSFX = "Generator_Microchip_installed";
		tinkerable.boostSymbolNames = new string[] { "booster", "blue_light_bloom" };
		tinkerable.SetWorkTime(30f);
		tinkerable.workerStatusItem = Db.Get().DuplicantStatusItems.Tinkering;
		tinkerable.attributeConverter = Db.Get().AttributeConverters.MachinerySpeed;
		tinkerable.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		tinkerable.choreTypeTinker = Db.Get().ChoreTypes.PowerTinker.IdHash;
		tinkerable.choreTypeFetch = Db.Get().ChoreTypes.PowerFetch.IdHash;
		tinkerable.addedEffect = "PowerTinker";
		tinkerable.effectAttributeId = Db.Get().Attributes.Machinery.Id;
		tinkerable.effectMultiplier = 0.025f;
		tinkerable.multitoolContext = "powertinker";
		tinkerable.multitoolHitEffectTag = "fx_powertinker_splash";
		tinkerable.shouldShowSkillPerkStatusItem = false;
		prefab.AddOrGet<Storage>();
		prefab.AddOrGet<Effects>();
		prefab.GetComponent<KPrefabID>().prefabInitFn += delegate(GameObject inst)
		{
			inst.GetComponent<Tinkerable>().SetOffsetTable(OffsetGroups.InvertedStandardTable);
		};
		return tinkerable;
	}

	// Token: 0x060059D1 RID: 22993 RVA: 0x00207210 File Offset: 0x00205410
	public static Tinkerable MakeFarmTinkerable(GameObject prefab)
	{
		RoomTracker roomTracker = prefab.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.Farm.Id;
		roomTracker.requirement = RoomTracker.Requirement.TrackingOnly;
		Tinkerable tinkerable = prefab.AddOrGet<Tinkerable>();
		tinkerable.tinkerMaterialTag = FarmStationConfig.TINKER_TOOLS;
		tinkerable.tinkerMaterialAmount = 1f;
		tinkerable.tinkerMass = 5f;
		tinkerable.requiredSkillPerk = Db.Get().SkillPerks.CanFarmTinker.Id;
		tinkerable.workerStatusItem = Db.Get().DuplicantStatusItems.Tinkering;
		tinkerable.addedEffect = "FarmTinker";
		tinkerable.effectAttributeId = Db.Get().Attributes.Botanist.Id;
		tinkerable.effectMultiplier = 0.1f;
		tinkerable.SetWorkTime(15f);
		tinkerable.attributeConverter = Db.Get().AttributeConverters.PlantTendSpeed;
		tinkerable.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		tinkerable.choreTypeTinker = Db.Get().ChoreTypes.CropTend.IdHash;
		tinkerable.choreTypeFetch = Db.Get().ChoreTypes.FarmFetch.IdHash;
		tinkerable.multitoolContext = "tend";
		tinkerable.multitoolHitEffectTag = "fx_tend_splash";
		tinkerable.shouldShowSkillPerkStatusItem = false;
		prefab.AddOrGet<Storage>();
		prefab.AddOrGet<Effects>();
		prefab.GetComponent<KPrefabID>().prefabInitFn += delegate(GameObject inst)
		{
			inst.GetComponent<Tinkerable>().SetOffsetTable(OffsetGroups.InvertedStandardTable);
		};
		return tinkerable;
	}

	// Token: 0x060059D2 RID: 22994 RVA: 0x00207388 File Offset: 0x00205588
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_use_machine_kanim") };
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Tinkering;
		this.attributeConverter = Db.Get().AttributeConverters.MachinerySpeed;
		this.faceTargetWhenWorking = true;
		this.synchronizeAnims = false;
		base.Subscribe<Tinkerable>(-1157678353, Tinkerable.OnEffectRemovedDelegate);
		base.Subscribe<Tinkerable>(-1697596308, Tinkerable.OnStorageChangeDelegate);
		base.Subscribe<Tinkerable>(144050788, Tinkerable.OnUpdateRoomDelegate);
		base.Subscribe<Tinkerable>(-592767678, Tinkerable.OnOperationalChangedDelegate);
	}

	// Token: 0x060059D3 RID: 22995 RVA: 0x00207435 File Offset: 0x00205635
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Prioritizable.AddRef(base.gameObject);
		this.prioritizableAdded = true;
		base.Subscribe<Tinkerable>(493375141, Tinkerable.OnRefreshUserMenuDelegate);
		this.UpdateVisual();
	}

	// Token: 0x060059D4 RID: 22996 RVA: 0x00207466 File Offset: 0x00205666
	protected override void OnCleanUp()
	{
		this.UpdateMaterialReservation(false);
		if (this.updateHandle.IsValid)
		{
			this.updateHandle.ClearScheduler();
		}
		if (this.prioritizableAdded)
		{
			Prioritizable.RemoveRef(base.gameObject);
		}
		base.OnCleanUp();
	}

	// Token: 0x060059D5 RID: 22997 RVA: 0x002074A0 File Offset: 0x002056A0
	private void OnOperationalChanged(object data)
	{
		this.QueueUpdateChore();
	}

	// Token: 0x060059D6 RID: 22998 RVA: 0x002074A8 File Offset: 0x002056A8
	private void OnEffectRemoved(object data)
	{
		this.QueueUpdateChore();
	}

	// Token: 0x060059D7 RID: 22999 RVA: 0x002074B0 File Offset: 0x002056B0
	private void OnUpdateRoom(object data)
	{
		this.QueueUpdateChore();
	}

	// Token: 0x060059D8 RID: 23000 RVA: 0x002074B8 File Offset: 0x002056B8
	private void OnStorageChange(object data)
	{
		if (((GameObject)data).IsPrefabID(this.tinkerMaterialTag))
		{
			this.QueueUpdateChore();
		}
	}

	// Token: 0x060059D9 RID: 23001 RVA: 0x002074D4 File Offset: 0x002056D4
	private void QueueUpdateChore()
	{
		if (this.updateHandle.IsValid)
		{
			this.updateHandle.ClearScheduler();
		}
		this.updateHandle = GameScheduler.Instance.Schedule("UpdateTinkerChore", 1.2f, new Action<object>(this.UpdateChoreCallback), null, null);
	}

	// Token: 0x060059DA RID: 23002 RVA: 0x00207521 File Offset: 0x00205721
	private void UpdateChoreCallback(object obj)
	{
		this.UpdateChore();
	}

	// Token: 0x060059DB RID: 23003 RVA: 0x0020752C File Offset: 0x0020572C
	private void UpdateChore()
	{
		Operational component = base.GetComponent<Operational>();
		bool flag = component == null || component.IsFunctional;
		bool flag2 = this.HasEffect();
		bool flag3 = this.HasCorrectRoom();
		bool flag4 = !flag2 && flag && flag3 && this.userMenuAllowed;
		bool flag5 = flag2 || !flag3 || !this.userMenuAllowed;
		if (this.chore == null && flag4)
		{
			this.UpdateMaterialReservation(true);
			if (this.HasMaterial())
			{
				this.chore = new WorkChore<Tinkerable>(Db.Get().ChoreTypes.GetByHash(this.choreTypeTinker), this, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
				if (component != null)
				{
					this.chore.AddPrecondition(ChorePreconditions.instance.IsFunctional, component);
				}
			}
			else
			{
				this.chore = new FetchChore(Db.Get().ChoreTypes.GetByHash(this.choreTypeFetch), this.storage, this.tinkerMaterialAmount * this.tinkerMass, new HashSet<Tag> { this.tinkerMaterialTag }, FetchChore.MatchCriteria.MatchID, Tag.Invalid, null, null, true, new Action<Chore>(this.OnFetchComplete), null, null, Operational.State.Functional, 0);
			}
			this.chore.AddPrecondition(ChorePreconditions.instance.HasSkillPerk, this.requiredSkillPerk);
			if (!string.IsNullOrEmpty(base.GetComponent<RoomTracker>().requiredRoomType))
			{
				this.chore.AddPrecondition(ChorePreconditions.instance.IsInMyRoom, Grid.PosToCell(base.transform.GetPosition()));
				return;
			}
		}
		else if (this.chore != null && flag5)
		{
			this.UpdateMaterialReservation(false);
			this.chore.Cancel("No longer needed");
			this.chore = null;
		}
	}

	// Token: 0x060059DC RID: 23004 RVA: 0x002076DB File Offset: 0x002058DB
	private bool HasCorrectRoom()
	{
		return this.roomTracker.IsInCorrectRoom();
	}

	// Token: 0x060059DD RID: 23005 RVA: 0x002076E8 File Offset: 0x002058E8
	private bool RoomHasTinkerstation()
	{
		if (!this.roomTracker.IsInCorrectRoom())
		{
			return false;
		}
		if (this.roomTracker.room == null)
		{
			return false;
		}
		foreach (KPrefabID kprefabID in this.roomTracker.room.buildings)
		{
			if (!(kprefabID == null))
			{
				TinkerStation component = kprefabID.GetComponent<TinkerStation>();
				if (component != null && component.outputPrefab == this.tinkerMaterialTag)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060059DE RID: 23006 RVA: 0x00207790 File Offset: 0x00205990
	private void UpdateMaterialReservation(bool shouldReserve)
	{
		if (shouldReserve && !this.hasReservedMaterial)
		{
			MaterialNeeds.UpdateNeed(this.tinkerMaterialTag, this.tinkerMaterialAmount, base.gameObject.GetMyWorldId());
			this.hasReservedMaterial = shouldReserve;
			return;
		}
		if (!shouldReserve && this.hasReservedMaterial)
		{
			MaterialNeeds.UpdateNeed(this.tinkerMaterialTag, -this.tinkerMaterialAmount, base.gameObject.GetMyWorldId());
			this.hasReservedMaterial = shouldReserve;
		}
	}

	// Token: 0x060059DF RID: 23007 RVA: 0x002077FB File Offset: 0x002059FB
	private void OnFetchComplete(Chore data)
	{
		this.UpdateMaterialReservation(false);
		this.chore = null;
		this.UpdateChore();
	}

	// Token: 0x060059E0 RID: 23008 RVA: 0x00207814 File Offset: 0x00205A14
	protected override void OnCompleteWork(WorkerBase worker)
	{
		base.OnCompleteWork(worker);
		this.storage.ConsumeIgnoringDisease(this.tinkerMaterialTag, this.tinkerMaterialAmount);
		float totalValue = worker.GetAttributes().Get(Db.Get().Attributes.Get(this.effectAttributeId)).GetTotalValue();
		this.effects.Add(this.addedEffect, true).timeRemaining *= 1f + totalValue * this.effectMultiplier;
		this.UpdateVisual();
		this.UpdateMaterialReservation(false);
		this.chore = null;
		this.UpdateChore();
		string sound = GlobalAssets.GetSound(this.onCompleteSFX, false);
		if (sound != null)
		{
			SoundEvent.EndOneShot(SoundEvent.BeginOneShot(sound, base.transform.position, 1f, false));
		}
	}

	// Token: 0x060059E1 RID: 23009 RVA: 0x002078D8 File Offset: 0x00205AD8
	private void UpdateVisual()
	{
		if (this.boostSymbolNames == null)
		{
			return;
		}
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		bool flag = this.effects.HasEffect(this.addedEffect);
		foreach (string text in this.boostSymbolNames)
		{
			component.SetSymbolVisiblity(text, flag);
		}
	}

	// Token: 0x060059E2 RID: 23010 RVA: 0x0020792F File Offset: 0x00205B2F
	private bool HasMaterial()
	{
		return this.storage.GetAmountAvailable(this.tinkerMaterialTag) >= this.tinkerMaterialAmount;
	}

	// Token: 0x060059E3 RID: 23011 RVA: 0x0020794D File Offset: 0x00205B4D
	private bool HasEffect()
	{
		return this.effects.HasEffect(this.addedEffect);
	}

	// Token: 0x060059E4 RID: 23012 RVA: 0x00207960 File Offset: 0x00205B60
	private void OnRefreshUserMenu(object data)
	{
		if (this.roomTracker.IsInCorrectRoom())
		{
			string name = Db.Get().effects.Get(this.addedEffect).Name;
			string properName = this.GetProperName();
			KIconButtonMenu.ButtonInfo buttonInfo = (this.userMenuAllowed ? new KIconButtonMenu.ButtonInfo("action_switch_toggle", UI.USERMENUACTIONS.TINKER.DISALLOW, new global::System.Action(this.OnClickToggleTinker), global::Action.NumActions, null, null, null, string.Format(UI.USERMENUACTIONS.TINKER.TOOLTIP_DISALLOW, name, properName), true) : new KIconButtonMenu.ButtonInfo("action_switch_toggle", UI.USERMENUACTIONS.TINKER.ALLOW, new global::System.Action(this.OnClickToggleTinker), global::Action.NumActions, null, null, null, string.Format(UI.USERMENUACTIONS.TINKER.TOOLTIP_ALLOW, name, properName), true));
			Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
		}
	}

	// Token: 0x060059E5 RID: 23013 RVA: 0x00207A39 File Offset: 0x00205C39
	private void OnClickToggleTinker()
	{
		this.userMenuAllowed = !this.userMenuAllowed;
		this.UpdateChore();
	}

	// Token: 0x04003BA2 RID: 15266
	private Chore chore;

	// Token: 0x04003BA3 RID: 15267
	[MyCmpGet]
	private Storage storage;

	// Token: 0x04003BA4 RID: 15268
	[MyCmpGet]
	private Effects effects;

	// Token: 0x04003BA5 RID: 15269
	[MyCmpGet]
	private RoomTracker roomTracker;

	// Token: 0x04003BA6 RID: 15270
	public Tag tinkerMaterialTag;

	// Token: 0x04003BA7 RID: 15271
	public float tinkerMaterialAmount;

	// Token: 0x04003BA8 RID: 15272
	public float tinkerMass;

	// Token: 0x04003BA9 RID: 15273
	public string addedEffect;

	// Token: 0x04003BAA RID: 15274
	public string effectAttributeId;

	// Token: 0x04003BAB RID: 15275
	public float effectMultiplier;

	// Token: 0x04003BAC RID: 15276
	public string[] boostSymbolNames;

	// Token: 0x04003BAD RID: 15277
	public string onCompleteSFX;

	// Token: 0x04003BAE RID: 15278
	public HashedString choreTypeTinker = Db.Get().ChoreTypes.PowerTinker.IdHash;

	// Token: 0x04003BAF RID: 15279
	public HashedString choreTypeFetch = Db.Get().ChoreTypes.PowerFetch.IdHash;

	// Token: 0x04003BB0 RID: 15280
	[Serialize]
	private bool userMenuAllowed = true;

	// Token: 0x04003BB1 RID: 15281
	private static readonly EventSystem.IntraObjectHandler<Tinkerable> OnEffectRemovedDelegate = new EventSystem.IntraObjectHandler<Tinkerable>(delegate(Tinkerable component, object data)
	{
		component.OnEffectRemoved(data);
	});

	// Token: 0x04003BB2 RID: 15282
	private static readonly EventSystem.IntraObjectHandler<Tinkerable> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<Tinkerable>(delegate(Tinkerable component, object data)
	{
		component.OnStorageChange(data);
	});

	// Token: 0x04003BB3 RID: 15283
	private static readonly EventSystem.IntraObjectHandler<Tinkerable> OnUpdateRoomDelegate = new EventSystem.IntraObjectHandler<Tinkerable>(delegate(Tinkerable component, object data)
	{
		component.OnUpdateRoom(data);
	});

	// Token: 0x04003BB4 RID: 15284
	private static readonly EventSystem.IntraObjectHandler<Tinkerable> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<Tinkerable>(delegate(Tinkerable component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x04003BB5 RID: 15285
	private static readonly EventSystem.IntraObjectHandler<Tinkerable> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<Tinkerable>(delegate(Tinkerable component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x04003BB6 RID: 15286
	private bool prioritizableAdded;

	// Token: 0x04003BB7 RID: 15287
	private SchedulerHandle updateHandle;

	// Token: 0x04003BB8 RID: 15288
	private bool hasReservedMaterial;
}
