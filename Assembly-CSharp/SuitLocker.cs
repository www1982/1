using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020007D5 RID: 2005
public class SuitLocker : StateMachineComponent<SuitLocker.StatesInstance>
{
	// Token: 0x17000387 RID: 903
	// (get) Token: 0x060035E4 RID: 13796 RVA: 0x0012CBE8 File Offset: 0x0012ADE8
	public float OxygenAvailable
	{
		get
		{
			KPrefabID storedOutfit = this.GetStoredOutfit();
			if (storedOutfit == null)
			{
				return 0f;
			}
			return storedOutfit.GetComponent<SuitTank>().PercentFull();
		}
	}

	// Token: 0x17000388 RID: 904
	// (get) Token: 0x060035E5 RID: 13797 RVA: 0x0012CC18 File Offset: 0x0012AE18
	public float BatteryAvailable
	{
		get
		{
			KPrefabID storedOutfit = this.GetStoredOutfit();
			if (storedOutfit == null)
			{
				return 0f;
			}
			return storedOutfit.GetComponent<LeadSuitTank>().batteryCharge;
		}
	}

	// Token: 0x060035E6 RID: 13798 RVA: 0x0012CC48 File Offset: 0x0012AE48
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_arrow", "meter_scale" });
		DebugUtil.DevAssert(this.OutfitTags.Length == 1, "Suit Locker " + base.name + " requesting more than one suit type, this will break the fetch chore", null);
		if (this.OutfitTags.Length == 1)
		{
			GameObject prefab = Assets.GetPrefab(this.OutfitTags[0]);
			if (prefab != null)
			{
				PrimaryElement component = prefab.GetComponent<PrimaryElement>();
				this.OutfitMass = component.MassPerUnit;
			}
		}
		else
		{
			this.OutfitMass = (float)global::TUNING.EQUIPMENT.SUITS.ATMOSUIT_MASS;
		}
		SuitLocker.UpdateSuitMarkerStates(Grid.PosToCell(base.transform.position), base.gameObject);
		base.smi.StartSM();
		Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Suits, true);
	}

	// Token: 0x060035E7 RID: 13799 RVA: 0x0012CD38 File Offset: 0x0012AF38
	public KPrefabID GetStoredOutfit()
	{
		foreach (GameObject gameObject in base.GetComponent<Storage>().items)
		{
			if (!(gameObject == null))
			{
				KPrefabID component = gameObject.GetComponent<KPrefabID>();
				if (!(component == null) && component.IsAnyPrefabID(this.OutfitTags))
				{
					return component;
				}
			}
		}
		return null;
	}

	// Token: 0x060035E8 RID: 13800 RVA: 0x0012CDB8 File Offset: 0x0012AFB8
	public float GetSuitScore()
	{
		float num = -1f;
		KPrefabID partiallyChargedOutfit = this.GetPartiallyChargedOutfit();
		if (partiallyChargedOutfit)
		{
			num = partiallyChargedOutfit.GetComponent<SuitTank>().PercentFull();
			JetSuitTank component = partiallyChargedOutfit.GetComponent<JetSuitTank>();
			if (component && component.PercentFull() < num)
			{
				num = component.PercentFull();
			}
		}
		return num;
	}

	// Token: 0x060035E9 RID: 13801 RVA: 0x0012CE08 File Offset: 0x0012B008
	public KPrefabID GetPartiallyChargedOutfit()
	{
		KPrefabID storedOutfit = this.GetStoredOutfit();
		if (!storedOutfit)
		{
			return null;
		}
		if (storedOutfit.GetComponent<SuitTank>().PercentFull() < global::TUNING.EQUIPMENT.SUITS.MINIMUM_USABLE_SUIT_CHARGE)
		{
			return null;
		}
		JetSuitTank component = storedOutfit.GetComponent<JetSuitTank>();
		if (component && component.PercentFull() < global::TUNING.EQUIPMENT.SUITS.MINIMUM_USABLE_SUIT_CHARGE)
		{
			return null;
		}
		return storedOutfit;
	}

	// Token: 0x060035EA RID: 13802 RVA: 0x0012CE5C File Offset: 0x0012B05C
	public KPrefabID GetFullyChargedOutfit()
	{
		KPrefabID storedOutfit = this.GetStoredOutfit();
		if (!storedOutfit)
		{
			return null;
		}
		if (!storedOutfit.GetComponent<SuitTank>().IsFull())
		{
			return null;
		}
		JetSuitTank component = storedOutfit.GetComponent<JetSuitTank>();
		if (component && !component.IsFull())
		{
			return null;
		}
		return storedOutfit;
	}

	// Token: 0x060035EB RID: 13803 RVA: 0x0012CEA4 File Offset: 0x0012B0A4
	private void CreateFetchChore()
	{
		this.fetchChore = new FetchChore(Db.Get().ChoreTypes.EquipmentFetch, base.GetComponent<Storage>(), this.OutfitMass, new HashSet<Tag>(this.OutfitTags), FetchChore.MatchCriteria.MatchID, Tag.Invalid, new Tag[] { GameTags.Assigned }, null, true, null, null, null, Operational.State.None, 0);
		this.fetchChore.allowMultifetch = false;
	}

	// Token: 0x060035EC RID: 13804 RVA: 0x0012CF0D File Offset: 0x0012B10D
	private void CancelFetchChore()
	{
		if (this.fetchChore != null)
		{
			this.fetchChore.Cancel("SuitLocker.CancelFetchChore");
			this.fetchChore = null;
		}
	}

	// Token: 0x060035ED RID: 13805 RVA: 0x0012CF30 File Offset: 0x0012B130
	public bool HasOxygen()
	{
		GameObject oxygen = this.GetOxygen();
		return oxygen != null && oxygen.GetComponent<PrimaryElement>().Mass > 0f;
	}

	// Token: 0x060035EE RID: 13806 RVA: 0x0012CF64 File Offset: 0x0012B164
	private void RefreshMeter()
	{
		GameObject oxygen = this.GetOxygen();
		float num = 0f;
		if (oxygen != null)
		{
			num = oxygen.GetComponent<PrimaryElement>().Mass / base.GetComponent<ConduitConsumer>().capacityKG;
			num = Math.Min(num, 1f);
		}
		this.meter.SetPositionPercent(num);
	}

	// Token: 0x060035EF RID: 13807 RVA: 0x0012CFB8 File Offset: 0x0012B1B8
	public bool IsSuitFullyCharged()
	{
		KPrefabID storedOutfit = this.GetStoredOutfit();
		if (!(storedOutfit != null))
		{
			return false;
		}
		SuitTank component = storedOutfit.GetComponent<SuitTank>();
		if (component != null && component.PercentFull() < 1f)
		{
			return false;
		}
		JetSuitTank component2 = storedOutfit.GetComponent<JetSuitTank>();
		if (component2 != null && component2.PercentFull() < 1f)
		{
			return false;
		}
		LeadSuitTank leadSuitTank = ((storedOutfit != null) ? storedOutfit.GetComponent<LeadSuitTank>() : null);
		return !(leadSuitTank != null) || leadSuitTank.PercentFull() >= 1f;
	}

	// Token: 0x060035F0 RID: 13808 RVA: 0x0012D044 File Offset: 0x0012B244
	public bool IsOxygenTankFull()
	{
		KPrefabID storedOutfit = this.GetStoredOutfit();
		if (storedOutfit != null)
		{
			SuitTank component = storedOutfit.GetComponent<SuitTank>();
			return component == null || component.PercentFull() >= 1f;
		}
		return false;
	}

	// Token: 0x060035F1 RID: 13809 RVA: 0x0012D085 File Offset: 0x0012B285
	private void OnRequestOutfit()
	{
		base.smi.sm.isWaitingForSuit.Set(true, base.smi, false);
	}

	// Token: 0x060035F2 RID: 13810 RVA: 0x0012D0A5 File Offset: 0x0012B2A5
	private void OnCancelRequest()
	{
		base.smi.sm.isWaitingForSuit.Set(false, base.smi, false);
	}

	// Token: 0x060035F3 RID: 13811 RVA: 0x0012D0C8 File Offset: 0x0012B2C8
	public void DropSuit()
	{
		KPrefabID storedOutfit = this.GetStoredOutfit();
		if (storedOutfit == null)
		{
			return;
		}
		base.GetComponent<Storage>().Drop(storedOutfit.gameObject, true);
	}

	// Token: 0x060035F4 RID: 13812 RVA: 0x0012D0FC File Offset: 0x0012B2FC
	public void EquipTo(Equipment equipment)
	{
		KPrefabID storedOutfit = this.GetStoredOutfit();
		if (storedOutfit == null)
		{
			return;
		}
		base.GetComponent<Storage>().Drop(storedOutfit.gameObject, true);
		Prioritizable component = storedOutfit.GetComponent<Prioritizable>();
		PrioritySetting masterPriority = component.GetMasterPriority();
		PrioritySetting prioritySetting = new PrioritySetting(PriorityScreen.PriorityClass.basic, 5);
		if (component != null && component.GetMasterPriority().priority_class == PriorityScreen.PriorityClass.topPriority)
		{
			component.SetMasterPriority(prioritySetting);
		}
		storedOutfit.GetComponent<Equippable>().Assign(equipment.GetComponent<IAssignableIdentity>());
		storedOutfit.GetComponent<EquippableWorkable>().CancelChore("Manual equip");
		if (component != null && component.GetMasterPriority() != masterPriority)
		{
			component.SetMasterPriority(masterPriority);
		}
		equipment.Equip(storedOutfit.GetComponent<Equippable>());
		this.returnSuitWorkable.CreateChore();
	}

	// Token: 0x060035F5 RID: 13813 RVA: 0x0012D1B8 File Offset: 0x0012B3B8
	public void UnequipFrom(Equipment equipment)
	{
		Assignable assignable = equipment.GetAssignable(Db.Get().AssignableSlots.Suit);
		assignable.Unassign();
		Durability component = assignable.GetComponent<Durability>();
		if (component != null && component.IsWornOut())
		{
			this.ConfigRequestSuit();
			return;
		}
		base.GetComponent<Storage>().Store(assignable.gameObject, false, false, true, false);
	}

	// Token: 0x060035F6 RID: 13814 RVA: 0x0012D216 File Offset: 0x0012B416
	public void ConfigRequestSuit()
	{
		base.smi.sm.isConfigured.Set(true, base.smi, false);
		base.smi.sm.isWaitingForSuit.Set(true, base.smi, false);
	}

	// Token: 0x060035F7 RID: 13815 RVA: 0x0012D254 File Offset: 0x0012B454
	public void ConfigNoSuit()
	{
		base.smi.sm.isConfigured.Set(true, base.smi, false);
		base.smi.sm.isWaitingForSuit.Set(false, base.smi, false);
	}

	// Token: 0x060035F8 RID: 13816 RVA: 0x0012D294 File Offset: 0x0012B494
	public bool CanDropOffSuit()
	{
		return base.smi.sm.isConfigured.Get(base.smi) && !base.smi.sm.isWaitingForSuit.Get(base.smi) && this.GetStoredOutfit() == null;
	}

	// Token: 0x060035F9 RID: 13817 RVA: 0x0012D2E9 File Offset: 0x0012B4E9
	private GameObject GetOxygen()
	{
		return base.GetComponent<Storage>().FindFirst(GameTags.Oxygen);
	}

	// Token: 0x060035FA RID: 13818 RVA: 0x0012D2FC File Offset: 0x0012B4FC
	private void ChargeSuit(float dt)
	{
		KPrefabID storedOutfit = this.GetStoredOutfit();
		if (storedOutfit == null)
		{
			return;
		}
		GameObject oxygen = this.GetOxygen();
		if (oxygen == null)
		{
			return;
		}
		SuitTank component = storedOutfit.GetComponent<SuitTank>();
		float num = component.capacity * 15f * dt / 600f;
		num = Mathf.Min(num, component.capacity - component.GetTankAmount());
		num = Mathf.Min(oxygen.GetComponent<PrimaryElement>().Mass, num);
		if (num > 0f)
		{
			base.GetComponent<Storage>().Transfer(component.storage, component.elementTag, num, false, true);
		}
	}

	// Token: 0x060035FB RID: 13819 RVA: 0x0012D390 File Offset: 0x0012B590
	public void SetSuitMarker(SuitMarker suit_marker)
	{
		SuitLocker.SuitMarkerState suitMarkerState = SuitLocker.SuitMarkerState.HasMarker;
		if (suit_marker == null)
		{
			suitMarkerState = SuitLocker.SuitMarkerState.NoMarker;
		}
		else if (suit_marker.transform.GetPosition().x > base.transform.GetPosition().x && suit_marker.GetComponent<Rotatable>().IsRotated)
		{
			suitMarkerState = SuitLocker.SuitMarkerState.WrongSide;
		}
		else if (suit_marker.transform.GetPosition().x < base.transform.GetPosition().x && !suit_marker.GetComponent<Rotatable>().IsRotated)
		{
			suitMarkerState = SuitLocker.SuitMarkerState.WrongSide;
		}
		else if (!suit_marker.GetComponent<Operational>().IsOperational)
		{
			suitMarkerState = SuitLocker.SuitMarkerState.NotOperational;
		}
		if (suitMarkerState != this.suitMarkerState)
		{
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.NoSuitMarker, false);
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.SuitMarkerWrongSide, false);
			switch (suitMarkerState)
			{
			case SuitLocker.SuitMarkerState.NoMarker:
				base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.NoSuitMarker, null);
				break;
			case SuitLocker.SuitMarkerState.WrongSide:
				base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.SuitMarkerWrongSide, null);
				break;
			}
			this.suitMarkerState = suitMarkerState;
		}
	}

	// Token: 0x060035FC RID: 13820 RVA: 0x0012D4BA File Offset: 0x0012B6BA
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		SuitLocker.UpdateSuitMarkerStates(Grid.PosToCell(base.transform.position), null);
	}

	// Token: 0x060035FD RID: 13821 RVA: 0x0012D4D8 File Offset: 0x0012B6D8
	private static void GatherSuitBuildings(int cell, int dir, List<SuitLocker.SuitLockerEntry> suit_lockers, List<SuitLocker.SuitMarkerEntry> suit_markers)
	{
		int num = dir;
		for (;;)
		{
			int num2 = Grid.OffsetCell(cell, num, 0);
			if (Grid.IsValidCell(num2) && !SuitLocker.GatherSuitBuildingsOnCell(num2, suit_lockers, suit_markers))
			{
				break;
			}
			num += dir;
		}
	}

	// Token: 0x060035FE RID: 13822 RVA: 0x0012D508 File Offset: 0x0012B708
	private static bool GatherSuitBuildingsOnCell(int cell, List<SuitLocker.SuitLockerEntry> suit_lockers, List<SuitLocker.SuitMarkerEntry> suit_markers)
	{
		GameObject gameObject = Grid.Objects[cell, 1];
		if (gameObject == null)
		{
			return false;
		}
		SuitMarker component = gameObject.GetComponent<SuitMarker>();
		if (component != null)
		{
			suit_markers.Add(new SuitLocker.SuitMarkerEntry
			{
				suitMarker = component,
				cell = cell
			});
			return true;
		}
		SuitLocker component2 = gameObject.GetComponent<SuitLocker>();
		if (component2 != null)
		{
			suit_lockers.Add(new SuitLocker.SuitLockerEntry
			{
				suitLocker = component2,
				cell = cell
			});
			return true;
		}
		return false;
	}

	// Token: 0x060035FF RID: 13823 RVA: 0x0012D594 File Offset: 0x0012B794
	private static SuitMarker FindSuitMarker(int cell, List<SuitLocker.SuitMarkerEntry> suit_markers)
	{
		if (!Grid.IsValidCell(cell))
		{
			return null;
		}
		foreach (SuitLocker.SuitMarkerEntry suitMarkerEntry in suit_markers)
		{
			if (suitMarkerEntry.cell == cell)
			{
				return suitMarkerEntry.suitMarker;
			}
		}
		return null;
	}

	// Token: 0x06003600 RID: 13824 RVA: 0x0012D5FC File Offset: 0x0012B7FC
	public static void UpdateSuitMarkerStates(int cell, GameObject self)
	{
		ListPool<SuitLocker.SuitLockerEntry, SuitLocker>.PooledList pooledList = ListPool<SuitLocker.SuitLockerEntry, SuitLocker>.Allocate();
		ListPool<SuitLocker.SuitMarkerEntry, SuitLocker>.PooledList pooledList2 = ListPool<SuitLocker.SuitMarkerEntry, SuitLocker>.Allocate();
		if (self != null)
		{
			SuitLocker component = self.GetComponent<SuitLocker>();
			if (component != null)
			{
				pooledList.Add(new SuitLocker.SuitLockerEntry
				{
					suitLocker = component,
					cell = cell
				});
			}
			SuitMarker component2 = self.GetComponent<SuitMarker>();
			if (component2 != null)
			{
				pooledList2.Add(new SuitLocker.SuitMarkerEntry
				{
					suitMarker = component2,
					cell = cell
				});
			}
		}
		SuitLocker.GatherSuitBuildings(cell, 1, pooledList, pooledList2);
		SuitLocker.GatherSuitBuildings(cell, -1, pooledList, pooledList2);
		pooledList.Sort(SuitLocker.SuitLockerEntry.comparer);
		for (int i = 0; i < pooledList.Count; i++)
		{
			SuitLocker.SuitLockerEntry suitLockerEntry = pooledList[i];
			SuitLocker.SuitLockerEntry suitLockerEntry2 = suitLockerEntry;
			ListPool<SuitLocker.SuitLockerEntry, SuitLocker>.PooledList pooledList3 = ListPool<SuitLocker.SuitLockerEntry, SuitLocker>.Allocate();
			pooledList3.Add(suitLockerEntry);
			for (int j = i + 1; j < pooledList.Count; j++)
			{
				SuitLocker.SuitLockerEntry suitLockerEntry3 = pooledList[j];
				if (Grid.CellRight(suitLockerEntry2.cell) != suitLockerEntry3.cell)
				{
					break;
				}
				i++;
				suitLockerEntry2 = suitLockerEntry3;
				pooledList3.Add(suitLockerEntry3);
			}
			int num = Grid.CellLeft(suitLockerEntry.cell);
			int num2 = Grid.CellRight(suitLockerEntry2.cell);
			SuitMarker suitMarker = SuitLocker.FindSuitMarker(num, pooledList2);
			if (suitMarker == null)
			{
				suitMarker = SuitLocker.FindSuitMarker(num2, pooledList2);
			}
			foreach (SuitLocker.SuitLockerEntry suitLockerEntry4 in pooledList3)
			{
				suitLockerEntry4.suitLocker.SetSuitMarker(suitMarker);
			}
			pooledList3.Recycle();
		}
		pooledList.Recycle();
		pooledList2.Recycle();
	}

	// Token: 0x040020A3 RID: 8355
	[MyCmpGet]
	private Building building;

	// Token: 0x040020A4 RID: 8356
	public Tag[] OutfitTags;

	// Token: 0x040020A5 RID: 8357
	private float OutfitMass;

	// Token: 0x040020A6 RID: 8358
	private FetchChore fetchChore;

	// Token: 0x040020A7 RID: 8359
	[MyCmpAdd]
	public SuitLocker.ReturnSuitWorkable returnSuitWorkable;

	// Token: 0x040020A8 RID: 8360
	private MeterController meter;

	// Token: 0x040020A9 RID: 8361
	private SuitLocker.SuitMarkerState suitMarkerState;

	// Token: 0x02001718 RID: 5912
	[AddComponentMenu("KMonoBehaviour/Workable/ReturnSuitWorkable")]
	public class ReturnSuitWorkable : Workable
	{
		// Token: 0x060097B0 RID: 38832 RVA: 0x0037D8F0 File Offset: 0x0037BAF0
		protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
			this.resetProgressOnStop = true;
			this.workTime = 0.25f;
			this.synchronizeAnims = false;
		}

		// Token: 0x060097B1 RID: 38833 RVA: 0x0037D914 File Offset: 0x0037BB14
		public void CreateChore()
		{
			if (this.urgentChore == null)
			{
				SuitLocker component = base.GetComponent<SuitLocker>();
				this.urgentChore = new WorkChore<SuitLocker.ReturnSuitWorkable>(Db.Get().ChoreTypes.ReturnSuitUrgent, this, null, true, null, null, null, true, null, false, false, null, false, true, false, PriorityScreen.PriorityClass.personalNeeds, 5, false, false);
				this.urgentChore.AddPrecondition(SuitLocker.ReturnSuitWorkable.DoesSuitNeedRechargingUrgent, null);
				this.urgentChore.AddPrecondition(this.HasSuitMarker, component);
				this.urgentChore.AddPrecondition(this.SuitTypeMatchesLocker, component);
				this.idleChore = new WorkChore<SuitLocker.ReturnSuitWorkable>(Db.Get().ChoreTypes.ReturnSuitIdle, this, null, true, null, null, null, true, null, false, false, null, false, true, false, PriorityScreen.PriorityClass.idle, 5, false, false);
				this.idleChore.AddPrecondition(SuitLocker.ReturnSuitWorkable.DoesSuitNeedRechargingIdle, null);
				this.idleChore.AddPrecondition(this.HasSuitMarker, component);
				this.idleChore.AddPrecondition(this.SuitTypeMatchesLocker, component);
			}
		}

		// Token: 0x060097B2 RID: 38834 RVA: 0x0037D9F5 File Offset: 0x0037BBF5
		public void CancelChore()
		{
			if (this.urgentChore != null)
			{
				this.urgentChore.Cancel("ReturnSuitWorkable.CancelChore");
				this.urgentChore = null;
			}
			if (this.idleChore != null)
			{
				this.idleChore.Cancel("ReturnSuitWorkable.CancelChore");
				this.idleChore = null;
			}
		}

		// Token: 0x060097B3 RID: 38835 RVA: 0x0037DA35 File Offset: 0x0037BC35
		protected override void OnStartWork(WorkerBase worker)
		{
			base.ShowProgressBar(false);
		}

		// Token: 0x060097B4 RID: 38836 RVA: 0x0037DA3E File Offset: 0x0037BC3E
		protected override bool OnWorkTick(WorkerBase worker, float dt)
		{
			return true;
		}

		// Token: 0x060097B5 RID: 38837 RVA: 0x0037DA44 File Offset: 0x0037BC44
		protected override void OnCompleteWork(WorkerBase worker)
		{
			Equipment equipment = worker.GetComponent<MinionIdentity>().GetEquipment();
			if (equipment.IsSlotOccupied(Db.Get().AssignableSlots.Suit))
			{
				if (base.GetComponent<SuitLocker>().CanDropOffSuit())
				{
					base.GetComponent<SuitLocker>().UnequipFrom(equipment);
				}
				else
				{
					equipment.GetAssignable(Db.Get().AssignableSlots.Suit).Unassign();
				}
			}
			if (this.urgentChore != null)
			{
				this.CancelChore();
				this.CreateChore();
			}
		}

		// Token: 0x060097B6 RID: 38838 RVA: 0x0037DABD File Offset: 0x0037BCBD
		public override HashedString[] GetWorkAnims(WorkerBase worker)
		{
			return new HashedString[]
			{
				new HashedString("none")
			};
		}

		// Token: 0x060097B7 RID: 38839 RVA: 0x0037DAD8 File Offset: 0x0037BCD8
		public ReturnSuitWorkable()
		{
			Chore.Precondition precondition = default(Chore.Precondition);
			precondition.id = "IsValid";
			precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.HAS_SUIT_MARKER;
			precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
			{
				return ((SuitLocker)data).suitMarkerState == SuitLocker.SuitMarkerState.HasMarker;
			};
			this.HasSuitMarker = precondition;
			precondition = default(Chore.Precondition);
			precondition.id = "IsValid";
			precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.HAS_SUIT_MARKER;
			precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
			{
				SuitLocker suitLocker = (SuitLocker)data;
				Equipment equipment = context.consumerState.equipment;
				if (equipment == null)
				{
					return false;
				}
				AssignableSlotInstance slot = equipment.GetSlot(Db.Get().AssignableSlots.Suit);
				return !(slot.assignable == null) && slot.assignable.GetComponent<KPrefabID>().IsAnyPrefabID(suitLocker.OutfitTags);
			};
			this.SuitTypeMatchesLocker = precondition;
			base..ctor();
		}

		// Token: 0x040074A2 RID: 29858
		public static readonly Chore.Precondition DoesSuitNeedRechargingUrgent = new Chore.Precondition
		{
			id = "DoesSuitNeedRechargingUrgent",
			description = DUPLICANTS.CHORES.PRECONDITIONS.DOES_SUIT_NEED_RECHARGING_URGENT,
			fn = delegate(ref Chore.Precondition.Context context, object data)
			{
				Equipment equipment = context.consumerState.equipment;
				if (equipment == null)
				{
					return false;
				}
				AssignableSlotInstance slot = equipment.GetSlot(Db.Get().AssignableSlots.Suit);
				if (slot.assignable == null)
				{
					return false;
				}
				Equippable component = slot.assignable.GetComponent<Equippable>();
				if (component == null || !component.isEquipped)
				{
					return false;
				}
				SuitTank component2 = slot.assignable.GetComponent<SuitTank>();
				if (component2 != null && component2.NeedsRecharging())
				{
					return true;
				}
				JetSuitTank component3 = slot.assignable.GetComponent<JetSuitTank>();
				if (component3 != null && component3.NeedsRecharging())
				{
					return true;
				}
				LeadSuitTank component4 = slot.assignable.GetComponent<LeadSuitTank>();
				return component4 != null && component4.NeedsRecharging();
			}
		};

		// Token: 0x040074A3 RID: 29859
		public static readonly Chore.Precondition DoesSuitNeedRechargingIdle = new Chore.Precondition
		{
			id = "DoesSuitNeedRechargingIdle",
			description = DUPLICANTS.CHORES.PRECONDITIONS.DOES_SUIT_NEED_RECHARGING_IDLE,
			fn = delegate(ref Chore.Precondition.Context context, object data)
			{
				Equipment equipment2 = context.consumerState.equipment;
				if (equipment2 == null)
				{
					return false;
				}
				AssignableSlotInstance slot2 = equipment2.GetSlot(Db.Get().AssignableSlots.Suit);
				if (slot2.assignable == null)
				{
					return false;
				}
				Equippable component5 = slot2.assignable.GetComponent<Equippable>();
				return !(component5 == null) && component5.isEquipped && (slot2.assignable.GetComponent<SuitTank>() != null || slot2.assignable.GetComponent<JetSuitTank>() != null || slot2.assignable.GetComponent<LeadSuitTank>() != null);
			}
		};

		// Token: 0x040074A4 RID: 29860
		public Chore.Precondition HasSuitMarker;

		// Token: 0x040074A5 RID: 29861
		public Chore.Precondition SuitTypeMatchesLocker;

		// Token: 0x040074A6 RID: 29862
		private WorkChore<SuitLocker.ReturnSuitWorkable> urgentChore;

		// Token: 0x040074A7 RID: 29863
		private WorkChore<SuitLocker.ReturnSuitWorkable> idleChore;
	}

	// Token: 0x02001719 RID: 5913
	public class StatesInstance : GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.GameInstance
	{
		// Token: 0x060097B9 RID: 38841 RVA: 0x0037DC21 File Offset: 0x0037BE21
		public StatesInstance(SuitLocker suit_locker)
			: base(suit_locker)
		{
		}
	}

	// Token: 0x0200171A RID: 5914
	public class States : GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker>
	{
		// Token: 0x060097BA RID: 38842 RVA: 0x0037DC2C File Offset: 0x0037BE2C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.empty;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.root.Update("RefreshMeter", delegate(SuitLocker.StatesInstance smi, float dt)
			{
				smi.master.RefreshMeter();
			}, UpdateRate.RENDER_200ms, false);
			this.empty.DefaultState(this.empty.notconfigured).EventTransition(GameHashes.OnStorageChange, this.charging, (SuitLocker.StatesInstance smi) => smi.master.GetStoredOutfit() != null).ParamTransition<bool>(this.isWaitingForSuit, this.waitingforsuit, GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.IsTrue)
				.Enter("CreateReturnSuitChore", delegate(SuitLocker.StatesInstance smi)
				{
					smi.master.returnSuitWorkable.CreateChore();
				})
				.RefreshUserMenuOnEnter()
				.Exit("CancelReturnSuitChore", delegate(SuitLocker.StatesInstance smi)
				{
					smi.master.returnSuitWorkable.CancelChore();
				})
				.PlayAnim("no_suit_pre")
				.QueueAnim("no_suit", false, null);
			GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State state = this.empty.notconfigured.ParamTransition<bool>(this.isConfigured, this.empty.configured, GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.IsTrue);
			string text = BUILDING.STATUSITEMS.SUIT_LOCKER_NEEDS_CONFIGURATION.NAME;
			string text2 = BUILDING.STATUSITEMS.SUIT_LOCKER_NEEDS_CONFIGURATION.TOOLTIP;
			string text3 = "status_item_no_filter_set";
			StatusItem.IconType iconType = StatusItem.IconType.Custom;
			NotificationType notificationType = NotificationType.BadMinor;
			bool flag = false;
			StatusItemCategory statusItemCategory = Db.Get().StatusItemCategories.Main;
			state.ToggleStatusItem(text, text2, text3, iconType, notificationType, flag, default(HashedString), 129022, null, null, statusItemCategory);
			GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State state2 = this.empty.configured.RefreshUserMenuOnEnter();
			string text4 = BUILDING.STATUSITEMS.SUIT_LOCKER.READY.NAME;
			string text5 = BUILDING.STATUSITEMS.SUIT_LOCKER.READY.TOOLTIP;
			string text6 = "";
			StatusItem.IconType iconType2 = StatusItem.IconType.Info;
			NotificationType notificationType2 = NotificationType.Neutral;
			bool flag2 = false;
			statusItemCategory = Db.Get().StatusItemCategories.Main;
			state2.ToggleStatusItem(text4, text5, text6, iconType2, notificationType2, flag2, default(HashedString), 129022, null, null, statusItemCategory);
			GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State state3 = this.waitingforsuit.EventTransition(GameHashes.OnStorageChange, this.charging, (SuitLocker.StatesInstance smi) => smi.master.GetStoredOutfit() != null).Enter("CreateFetchChore", delegate(SuitLocker.StatesInstance smi)
			{
				smi.master.CreateFetchChore();
			}).ParamTransition<bool>(this.isWaitingForSuit, this.empty, GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.IsFalse)
				.RefreshUserMenuOnEnter()
				.PlayAnim("no_suit_pst")
				.QueueAnim("awaiting_suit", false, null)
				.Exit("ClearIsWaitingForSuit", delegate(SuitLocker.StatesInstance smi)
				{
					this.isWaitingForSuit.Set(false, smi, false);
				})
				.Exit("CancelFetchChore", delegate(SuitLocker.StatesInstance smi)
				{
					smi.master.CancelFetchChore();
				});
			string text7 = BUILDING.STATUSITEMS.SUIT_LOCKER.SUIT_REQUESTED.NAME;
			string text8 = BUILDING.STATUSITEMS.SUIT_LOCKER.SUIT_REQUESTED.TOOLTIP;
			string text9 = "";
			StatusItem.IconType iconType3 = StatusItem.IconType.Info;
			NotificationType notificationType3 = NotificationType.Neutral;
			bool flag3 = false;
			statusItemCategory = Db.Get().StatusItemCategories.Main;
			state3.ToggleStatusItem(text7, text8, text9, iconType3, notificationType3, flag3, default(HashedString), 129022, null, null, statusItemCategory);
			this.charging.DefaultState(this.charging.pre).RefreshUserMenuOnEnter().EventTransition(GameHashes.OnStorageChange, this.empty, (SuitLocker.StatesInstance smi) => smi.master.GetStoredOutfit() == null)
				.ToggleStatusItem(Db.Get().MiscStatusItems.StoredItemDurability, (SuitLocker.StatesInstance smi) => smi.master.GetStoredOutfit().gameObject)
				.Enter(delegate(SuitLocker.StatesInstance smi)
				{
					KAnim.Build.Symbol symbol = smi.master.GetStoredOutfit().GetComponent<KBatchedAnimController>().AnimFiles[0].GetData().build.GetSymbol("suit");
					SymbolOverrideController component = smi.GetComponent<SymbolOverrideController>();
					component.TryRemoveSymbolOverride("suit_swap", 0);
					if (symbol != null)
					{
						component.AddSymbolOverride("suit_swap", symbol, 0);
					}
				});
			this.charging.pre.Enter(delegate(SuitLocker.StatesInstance smi)
			{
				if (smi.master.IsSuitFullyCharged())
				{
					smi.GoTo(this.suitfullycharged);
					return;
				}
				smi.GetComponent<KBatchedAnimController>().Play("no_suit_pst", KAnim.PlayMode.Once, 1f, 0f);
				smi.GetComponent<KBatchedAnimController>().Queue("charging_pre", KAnim.PlayMode.Once, 1f, 0f);
			}).OnAnimQueueComplete(this.charging.operational);
			GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State state4 = this.charging.operational.TagTransition(GameTags.Operational, this.charging.notoperational, true).Transition(this.charging.nooxygen, (SuitLocker.StatesInstance smi) => !smi.master.HasOxygen(), UpdateRate.SIM_200ms).PlayAnim("charging_loop", KAnim.PlayMode.Loop)
				.Enter("SetActive", delegate(SuitLocker.StatesInstance smi)
				{
					smi.master.GetComponent<Operational>().SetActive(true, false);
				})
				.Transition(this.charging.pst, (SuitLocker.StatesInstance smi) => smi.master.IsSuitFullyCharged(), UpdateRate.SIM_200ms)
				.Update("ChargeSuit", delegate(SuitLocker.StatesInstance smi, float dt)
				{
					smi.master.ChargeSuit(dt);
				}, UpdateRate.SIM_200ms, false)
				.Exit("ClearActive", delegate(SuitLocker.StatesInstance smi)
				{
					smi.master.GetComponent<Operational>().SetActive(false, false);
				});
			string text10 = BUILDING.STATUSITEMS.SUIT_LOCKER.CHARGING.NAME;
			string text11 = BUILDING.STATUSITEMS.SUIT_LOCKER.CHARGING.TOOLTIP;
			string text12 = "";
			StatusItem.IconType iconType4 = StatusItem.IconType.Info;
			NotificationType notificationType4 = NotificationType.Neutral;
			bool flag4 = false;
			statusItemCategory = Db.Get().StatusItemCategories.Main;
			state4.ToggleStatusItem(text10, text11, text12, iconType4, notificationType4, flag4, default(HashedString), 129022, null, null, statusItemCategory);
			GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State state5 = this.charging.nooxygen.TagTransition(GameTags.Operational, this.charging.notoperational, true).Transition(this.charging.operational, (SuitLocker.StatesInstance smi) => smi.master.HasOxygen(), UpdateRate.SIM_200ms).Transition(this.charging.pst, (SuitLocker.StatesInstance smi) => smi.master.IsSuitFullyCharged(), UpdateRate.SIM_200ms)
				.PlayAnim("no_o2_loop", KAnim.PlayMode.Loop);
			string text13 = BUILDING.STATUSITEMS.SUIT_LOCKER.NO_OXYGEN.NAME;
			string text14 = BUILDING.STATUSITEMS.SUIT_LOCKER.NO_OXYGEN.TOOLTIP;
			string text15 = "status_item_suit_locker_no_oxygen";
			StatusItem.IconType iconType5 = StatusItem.IconType.Custom;
			NotificationType notificationType5 = NotificationType.BadMinor;
			bool flag5 = false;
			statusItemCategory = Db.Get().StatusItemCategories.Main;
			state5.ToggleStatusItem(text13, text14, text15, iconType5, notificationType5, flag5, default(HashedString), 129022, null, null, statusItemCategory);
			GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State state6 = this.charging.notoperational.TagTransition(GameTags.Operational, this.charging.operational, false).PlayAnim("not_charging_loop", KAnim.PlayMode.Loop).Transition(this.charging.pst, (SuitLocker.StatesInstance smi) => smi.master.IsSuitFullyCharged(), UpdateRate.SIM_200ms);
			string text16 = BUILDING.STATUSITEMS.SUIT_LOCKER.NOT_OPERATIONAL.NAME;
			string text17 = BUILDING.STATUSITEMS.SUIT_LOCKER.NOT_OPERATIONAL.TOOLTIP;
			string text18 = "";
			StatusItem.IconType iconType6 = StatusItem.IconType.Info;
			NotificationType notificationType6 = NotificationType.Neutral;
			bool flag6 = false;
			statusItemCategory = Db.Get().StatusItemCategories.Main;
			state6.ToggleStatusItem(text16, text17, text18, iconType6, notificationType6, flag6, default(HashedString), 129022, null, null, statusItemCategory);
			this.charging.pst.PlayAnim("charging_pst").OnAnimQueueComplete(this.suitfullycharged);
			GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State state7 = this.suitfullycharged.EventTransition(GameHashes.OnStorageChange, this.empty, (SuitLocker.StatesInstance smi) => smi.master.GetStoredOutfit() == null).PlayAnim("has_suit").RefreshUserMenuOnEnter()
				.ToggleStatusItem(Db.Get().MiscStatusItems.StoredItemDurability, (SuitLocker.StatesInstance smi) => smi.master.GetStoredOutfit().gameObject);
			string text19 = BUILDING.STATUSITEMS.SUIT_LOCKER.FULLY_CHARGED.NAME;
			string text20 = BUILDING.STATUSITEMS.SUIT_LOCKER.FULLY_CHARGED.TOOLTIP;
			string text21 = "";
			StatusItem.IconType iconType7 = StatusItem.IconType.Info;
			NotificationType notificationType7 = NotificationType.Neutral;
			bool flag7 = false;
			statusItemCategory = Db.Get().StatusItemCategories.Main;
			state7.ToggleStatusItem(text19, text20, text21, iconType7, notificationType7, flag7, default(HashedString), 129022, null, null, statusItemCategory);
		}

		// Token: 0x040074A8 RID: 29864
		public SuitLocker.States.EmptyStates empty;

		// Token: 0x040074A9 RID: 29865
		public SuitLocker.States.ChargingStates charging;

		// Token: 0x040074AA RID: 29866
		public GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State waitingforsuit;

		// Token: 0x040074AB RID: 29867
		public GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State suitfullycharged;

		// Token: 0x040074AC RID: 29868
		public StateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.BoolParameter isWaitingForSuit;

		// Token: 0x040074AD RID: 29869
		public StateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.BoolParameter isConfigured;

		// Token: 0x040074AE RID: 29870
		public StateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.BoolParameter hasSuitMarker;

		// Token: 0x020027E5 RID: 10213
		public class ChargingStates : GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State
		{
			// Token: 0x0400B0E9 RID: 45289
			public GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State pre;

			// Token: 0x0400B0EA RID: 45290
			public GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State pst;

			// Token: 0x0400B0EB RID: 45291
			public GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State operational;

			// Token: 0x0400B0EC RID: 45292
			public GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State nooxygen;

			// Token: 0x0400B0ED RID: 45293
			public GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State notoperational;
		}

		// Token: 0x020027E6 RID: 10214
		public class EmptyStates : GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State
		{
			// Token: 0x0400B0EE RID: 45294
			public GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State configured;

			// Token: 0x0400B0EF RID: 45295
			public GameStateMachine<SuitLocker.States, SuitLocker.StatesInstance, SuitLocker, object>.State notconfigured;
		}
	}

	// Token: 0x0200171B RID: 5915
	private enum SuitMarkerState
	{
		// Token: 0x040074B0 RID: 29872
		HasMarker,
		// Token: 0x040074B1 RID: 29873
		NoMarker,
		// Token: 0x040074B2 RID: 29874
		WrongSide,
		// Token: 0x040074B3 RID: 29875
		NotOperational
	}

	// Token: 0x0200171C RID: 5916
	private struct SuitLockerEntry
	{
		// Token: 0x040074B4 RID: 29876
		public SuitLocker suitLocker;

		// Token: 0x040074B5 RID: 29877
		public int cell;

		// Token: 0x040074B6 RID: 29878
		public static SuitLocker.SuitLockerEntry.Comparer comparer = new SuitLocker.SuitLockerEntry.Comparer();

		// Token: 0x020027E8 RID: 10216
		public class Comparer : IComparer<SuitLocker.SuitLockerEntry>
		{
			// Token: 0x0600C9D4 RID: 51668 RVA: 0x004169EC File Offset: 0x00414BEC
			public int Compare(SuitLocker.SuitLockerEntry a, SuitLocker.SuitLockerEntry b)
			{
				return a.cell - b.cell;
			}
		}
	}

	// Token: 0x0200171D RID: 5917
	private struct SuitMarkerEntry
	{
		// Token: 0x040074B7 RID: 29879
		public SuitMarker suitMarker;

		// Token: 0x040074B8 RID: 29880
		public int cell;
	}
}
