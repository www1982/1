using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000780 RID: 1920
public class MaskStation : StateMachineComponent<MaskStation.SMInstance>, IBasicBuilding
{
	// Token: 0x17000320 RID: 800
	// (get) Token: 0x06003291 RID: 12945 RVA: 0x0011CF72 File Offset: 0x0011B172
	// (set) Token: 0x06003292 RID: 12946 RVA: 0x0011CF7F File Offset: 0x0011B17F
	private bool isRotated
	{
		get
		{
			return (this.gridFlags & Grid.SuitMarker.Flags.Rotated) > (Grid.SuitMarker.Flags)0;
		}
		set
		{
			this.UpdateGridFlag(Grid.SuitMarker.Flags.Rotated, value);
		}
	}

	// Token: 0x17000321 RID: 801
	// (get) Token: 0x06003293 RID: 12947 RVA: 0x0011CF89 File Offset: 0x0011B189
	// (set) Token: 0x06003294 RID: 12948 RVA: 0x0011CF96 File Offset: 0x0011B196
	private bool isOperational
	{
		get
		{
			return (this.gridFlags & Grid.SuitMarker.Flags.Operational) > (Grid.SuitMarker.Flags)0;
		}
		set
		{
			this.UpdateGridFlag(Grid.SuitMarker.Flags.Operational, value);
		}
	}

	// Token: 0x06003295 RID: 12949 RVA: 0x0011CFA0 File Offset: 0x0011B1A0
	public void UpdateOperational()
	{
		bool flag = this.GetTotalOxygenAmount() >= this.oxygenConsumedPerMask * (float)this.maxUses;
		this.shouldPump = this.IsPumpable();
		if (this.operational.IsOperational && this.shouldPump && !flag)
		{
			this.operational.SetActive(true, false);
		}
		else
		{
			this.operational.SetActive(false, false);
		}
		this.noElementStatusGuid = this.selectable.ToggleStatusItem(Db.Get().BuildingStatusItems.InvalidMaskStationConsumptionState, this.noElementStatusGuid, !this.shouldPump, null);
	}

	// Token: 0x06003296 RID: 12950 RVA: 0x0011D038 File Offset: 0x0011B238
	private bool IsPumpable()
	{
		ElementConsumer[] components = base.GetComponents<ElementConsumer>();
		int num = Grid.PosToCell(base.transform.GetPosition());
		bool flag = false;
		foreach (ElementConsumer elementConsumer in components)
		{
			for (int j = 0; j < (int)elementConsumer.consumptionRadius; j++)
			{
				for (int k = 0; k < (int)elementConsumer.consumptionRadius; k++)
				{
					int num2 = num + k + Grid.WidthInCells * j;
					bool flag2 = Grid.Element[num2].IsState(Element.State.Gas);
					bool flag3 = Grid.Element[num2].id == elementConsumer.elementToConsume;
					if (flag2 && flag3)
					{
						flag = true;
					}
				}
			}
		}
		return flag;
	}

	// Token: 0x06003297 RID: 12951 RVA: 0x0011D0DC File Offset: 0x0011B2DC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		ChoreType choreType = Db.Get().ChoreTypes.Get(this.choreTypeID);
		this.filteredStorage = new FilteredStorage(this, null, null, false, choreType);
	}

	// Token: 0x06003298 RID: 12952 RVA: 0x0011D118 File Offset: 0x0011B318
	private List<GameObject> GetPossibleMaterials()
	{
		List<GameObject> list = new List<GameObject>();
		this.materialStorage.Find(this.materialTag, list);
		return list;
	}

	// Token: 0x06003299 RID: 12953 RVA: 0x0011D13F File Offset: 0x0011B33F
	private float GetTotalMaterialAmount()
	{
		return this.materialStorage.GetMassAvailable(this.materialTag);
	}

	// Token: 0x0600329A RID: 12954 RVA: 0x0011D152 File Offset: 0x0011B352
	private float GetTotalOxygenAmount()
	{
		return this.oxygenStorage.GetMassAvailable(this.oxygenTag);
	}

	// Token: 0x0600329B RID: 12955 RVA: 0x0011D168 File Offset: 0x0011B368
	private void RefreshMeters()
	{
		float num = this.GetTotalMaterialAmount();
		num = Mathf.Clamp01(num / ((float)this.maxUses * this.materialConsumedPerMask));
		float num2 = this.GetTotalOxygenAmount();
		num2 = Mathf.Clamp01(num2 / ((float)this.maxUses * this.oxygenConsumedPerMask));
		this.materialsMeter.SetPositionPercent(num);
		this.oxygenMeter.SetPositionPercent(num2);
	}

	// Token: 0x0600329C RID: 12956 RVA: 0x0011D1C8 File Offset: 0x0011B3C8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		this.CreateNewReactable();
		this.cell = Grid.PosToCell(this);
		Grid.RegisterSuitMarker(this.cell);
		this.isOperational = base.GetComponent<Operational>().IsOperational;
		base.Subscribe<MaskStation>(-592767678, MaskStation.OnOperationalChangedDelegate);
		this.isRotated = base.GetComponent<Rotatable>().IsRotated;
		base.Subscribe<MaskStation>(-1643076535, MaskStation.OnRotatedDelegate);
		this.materialsMeter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_resources_target", "meter_resources", this.materialsMeterOffset, Grid.SceneLayer.BuildingBack, new string[] { "meter_resources_target" });
		this.oxygenMeter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_oxygen_target", "meter_oxygen", this.oxygenMeterOffset, Grid.SceneLayer.BuildingFront, new string[] { "meter_oxygen_target" });
		if (this.filteredStorage != null)
		{
			this.filteredStorage.FilterChanged();
		}
		base.Subscribe<MaskStation>(-1697596308, MaskStation.OnStorageChangeDelegate);
		this.RefreshMeters();
	}

	// Token: 0x0600329D RID: 12957 RVA: 0x0011D2D4 File Offset: 0x0011B4D4
	private void Update()
	{
		float num = this.GetTotalMaterialAmount() / this.materialConsumedPerMask;
		float num2 = this.GetTotalOxygenAmount() / this.oxygenConsumedPerMask;
		int num3 = (int)Mathf.Min(num, num2);
		int num4 = 0;
		Grid.UpdateSuitMarker(this.cell, num3, num4, this.gridFlags, this.PathFlag);
	}

	// Token: 0x0600329E RID: 12958 RVA: 0x0011D320 File Offset: 0x0011B520
	protected override void OnCleanUp()
	{
		if (this.filteredStorage != null)
		{
			this.filteredStorage.CleanUp();
		}
		if (base.isSpawned)
		{
			Grid.UnregisterSuitMarker(this.cell);
		}
		if (this.reactable != null)
		{
			this.reactable.Cleanup();
		}
		base.OnCleanUp();
	}

	// Token: 0x0600329F RID: 12959 RVA: 0x0011D36C File Offset: 0x0011B56C
	private void OnOperationalChanged(bool isOperational)
	{
		this.isOperational = isOperational;
	}

	// Token: 0x060032A0 RID: 12960 RVA: 0x0011D375 File Offset: 0x0011B575
	private void OnStorageChange(object data)
	{
		this.RefreshMeters();
	}

	// Token: 0x060032A1 RID: 12961 RVA: 0x0011D37D File Offset: 0x0011B57D
	private void UpdateGridFlag(Grid.SuitMarker.Flags flag, bool state)
	{
		if (state)
		{
			this.gridFlags |= flag;
			return;
		}
		this.gridFlags &= ~flag;
	}

	// Token: 0x060032A2 RID: 12962 RVA: 0x0011D3A1 File Offset: 0x0011B5A1
	private void CreateNewReactable()
	{
		this.reactable = new MaskStation.OxygenMaskReactable(this);
	}

	// Token: 0x04001E49 RID: 7753
	private static readonly EventSystem.IntraObjectHandler<MaskStation> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<MaskStation>(delegate(MaskStation component, object data)
	{
		component.OnStorageChange(data);
	});

	// Token: 0x04001E4A RID: 7754
	private static readonly EventSystem.IntraObjectHandler<MaskStation> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<MaskStation>(delegate(MaskStation component, object data)
	{
		component.OnOperationalChanged((bool)data);
	});

	// Token: 0x04001E4B RID: 7755
	private static readonly EventSystem.IntraObjectHandler<MaskStation> OnRotatedDelegate = new EventSystem.IntraObjectHandler<MaskStation>(delegate(MaskStation component, object data)
	{
		component.isRotated = ((Rotatable)data).IsRotated;
	});

	// Token: 0x04001E4C RID: 7756
	public float materialConsumedPerMask = 1f;

	// Token: 0x04001E4D RID: 7757
	public float oxygenConsumedPerMask = 1f;

	// Token: 0x04001E4E RID: 7758
	public Tag materialTag = GameTags.Metal;

	// Token: 0x04001E4F RID: 7759
	public Tag oxygenTag = GameTags.Breathable;

	// Token: 0x04001E50 RID: 7760
	public int maxUses = 10;

	// Token: 0x04001E51 RID: 7761
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001E52 RID: 7762
	[MyCmpGet]
	private KSelectable selectable;

	// Token: 0x04001E53 RID: 7763
	public Storage materialStorage;

	// Token: 0x04001E54 RID: 7764
	public Storage oxygenStorage;

	// Token: 0x04001E55 RID: 7765
	private bool shouldPump;

	// Token: 0x04001E56 RID: 7766
	private MaskStation.OxygenMaskReactable reactable;

	// Token: 0x04001E57 RID: 7767
	private MeterController materialsMeter;

	// Token: 0x04001E58 RID: 7768
	private MeterController oxygenMeter;

	// Token: 0x04001E59 RID: 7769
	public Meter.Offset materialsMeterOffset = Meter.Offset.Behind;

	// Token: 0x04001E5A RID: 7770
	public Meter.Offset oxygenMeterOffset;

	// Token: 0x04001E5B RID: 7771
	public string choreTypeID;

	// Token: 0x04001E5C RID: 7772
	protected FilteredStorage filteredStorage;

	// Token: 0x04001E5D RID: 7773
	public KAnimFile interactAnim = Assets.GetAnim("anim_equip_clothing_kanim");

	// Token: 0x04001E5E RID: 7774
	private int cell;

	// Token: 0x04001E5F RID: 7775
	public PathFinder.PotentialPath.Flags PathFlag;

	// Token: 0x04001E60 RID: 7776
	private Guid noElementStatusGuid;

	// Token: 0x04001E61 RID: 7777
	private Grid.SuitMarker.Flags gridFlags;

	// Token: 0x02001669 RID: 5737
	private class OxygenMaskReactable : Reactable
	{
		// Token: 0x060094F7 RID: 38135 RVA: 0x00371A30 File Offset: 0x0036FC30
		public OxygenMaskReactable(MaskStation mask_station)
			: base(mask_station.gameObject, "OxygenMask", Db.Get().ChoreTypes.SuitMarker, 1, 1, false, 0f, 0f, float.PositiveInfinity, 0f, ObjectLayer.NumLayers)
		{
			this.maskStation = mask_station;
		}

		// Token: 0x060094F8 RID: 38136 RVA: 0x00371A84 File Offset: 0x0036FC84
		public override bool InternalCanBegin(GameObject new_reactor, Navigator.ActiveTransition transition)
		{
			if (this.reactor != null)
			{
				return false;
			}
			if (this.maskStation == null)
			{
				base.Cleanup();
				return false;
			}
			bool flag = !new_reactor.GetComponent<MinionIdentity>().GetEquipment().IsSlotOccupied(Db.Get().AssignableSlots.Suit);
			int x = transition.navGridTransition.x;
			if (x == 0)
			{
				return false;
			}
			if (!flag)
			{
				return (x >= 0 || !this.maskStation.isRotated) && (x <= 0 || this.maskStation.isRotated);
			}
			return this.maskStation.smi.IsReady() && (x <= 0 || !this.maskStation.isRotated) && (x >= 0 || this.maskStation.isRotated);
		}

		// Token: 0x060094F9 RID: 38137 RVA: 0x00371B54 File Offset: 0x0036FD54
		protected override void InternalBegin()
		{
			this.startTime = Time.time;
			KBatchedAnimController component = this.reactor.GetComponent<KBatchedAnimController>();
			component.AddAnimOverrides(this.maskStation.interactAnim, 1f);
			component.Play("working_pre", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue("working_loop", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue("working_pst", KAnim.PlayMode.Once, 1f, 0f);
			this.maskStation.CreateNewReactable();
		}

		// Token: 0x060094FA RID: 38138 RVA: 0x00371BE8 File Offset: 0x0036FDE8
		public override void Update(float dt)
		{
			Facing facing = (this.reactor ? this.reactor.GetComponent<Facing>() : null);
			if (facing && this.maskStation)
			{
				facing.SetFacing(this.maskStation.GetComponent<Rotatable>().GetOrientation() == Orientation.FlipH);
			}
			if (Time.time - this.startTime > 2.8f)
			{
				this.Run();
				base.Cleanup();
			}
		}

		// Token: 0x060094FB RID: 38139 RVA: 0x00371C60 File Offset: 0x0036FE60
		private void Run()
		{
			GameObject reactor = this.reactor;
			Equipment equipment = reactor.GetComponent<MinionIdentity>().GetEquipment();
			bool flag = !equipment.IsSlotOccupied(Db.Get().AssignableSlots.Suit);
			Navigator component = reactor.GetComponent<Navigator>();
			bool flag2 = component != null && (component.flags & this.maskStation.PathFlag) > PathFinder.PotentialPath.Flags.None;
			if (flag)
			{
				if (!this.maskStation.smi.IsReady())
				{
					return;
				}
				GameObject gameObject = Util.KInstantiate(Assets.GetPrefab("Oxygen_Mask".ToTag()), null, null);
				gameObject.SetActive(true);
				SimHashes elementID = this.maskStation.GetPossibleMaterials()[0].GetComponent<PrimaryElement>().ElementID;
				gameObject.GetComponent<PrimaryElement>().SetElement(elementID, false);
				SuitTank component2 = gameObject.GetComponent<SuitTank>();
				this.maskStation.materialStorage.ConsumeIgnoringDisease(this.maskStation.materialTag, this.maskStation.materialConsumedPerMask);
				this.maskStation.oxygenStorage.Transfer(component2.storage, component2.elementTag, this.maskStation.oxygenConsumedPerMask, false, true);
				Equippable component3 = gameObject.GetComponent<Equippable>();
				component3.Assign(equipment.GetComponent<IAssignableIdentity>());
				component3.isEquipped = true;
			}
			if (!flag)
			{
				Assignable assignable = equipment.GetAssignable(Db.Get().AssignableSlots.Suit);
				assignable.Unassign();
				if (!flag2)
				{
					Notification notification = new Notification(MISC.NOTIFICATIONS.SUIT_DROPPED.NAME, NotificationType.BadMinor, (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.SUIT_DROPPED.TOOLTIP, null, true, 0f, null, null, null, true, false, false);
					assignable.GetComponent<Notifier>().Add(notification, "");
				}
			}
		}

		// Token: 0x060094FC RID: 38140 RVA: 0x00371E07 File Offset: 0x00370007
		protected override void InternalEnd()
		{
			if (this.reactor != null)
			{
				this.reactor.GetComponent<KBatchedAnimController>().RemoveAnimOverrides(this.maskStation.interactAnim);
			}
		}

		// Token: 0x060094FD RID: 38141 RVA: 0x00371E32 File Offset: 0x00370032
		protected override void InternalCleanup()
		{
		}

		// Token: 0x0400729A RID: 29338
		private MaskStation maskStation;

		// Token: 0x0400729B RID: 29339
		private float startTime;
	}

	// Token: 0x0200166A RID: 5738
	public class SMInstance : GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.GameInstance
	{
		// Token: 0x060094FE RID: 38142 RVA: 0x00371E34 File Offset: 0x00370034
		public SMInstance(MaskStation master)
			: base(master)
		{
		}

		// Token: 0x060094FF RID: 38143 RVA: 0x00371E3D File Offset: 0x0037003D
		private bool HasSufficientMaterials()
		{
			return base.master.GetTotalMaterialAmount() >= base.master.materialConsumedPerMask;
		}

		// Token: 0x06009500 RID: 38144 RVA: 0x00371E5A File Offset: 0x0037005A
		private bool HasSufficientOxygen()
		{
			return base.master.GetTotalOxygenAmount() >= base.master.oxygenConsumedPerMask;
		}

		// Token: 0x06009501 RID: 38145 RVA: 0x00371E77 File Offset: 0x00370077
		public bool OxygenIsFull()
		{
			return base.master.GetTotalOxygenAmount() >= base.master.oxygenConsumedPerMask * (float)base.master.maxUses;
		}

		// Token: 0x06009502 RID: 38146 RVA: 0x00371EA1 File Offset: 0x003700A1
		public bool IsReady()
		{
			return this.HasSufficientMaterials() && this.HasSufficientOxygen();
		}
	}

	// Token: 0x0200166B RID: 5739
	public class States : GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation>
	{
		// Token: 0x06009503 RID: 38147 RVA: 0x00371EB8 File Offset: 0x003700B8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.notOperational;
			this.notOperational.PlayAnim("off").TagTransition(GameTags.Operational, this.charging, false);
			this.charging.TagTransition(GameTags.Operational, this.notOperational, true).EventTransition(GameHashes.OnStorageChange, this.notCharging, (MaskStation.SMInstance smi) => smi.OxygenIsFull() || !smi.master.shouldPump).Update(delegate(MaskStation.SMInstance smi, float dt)
			{
				smi.master.UpdateOperational();
			}, UpdateRate.SIM_1000ms, false)
				.Enter(delegate(MaskStation.SMInstance smi)
				{
					if (smi.OxygenIsFull() || !smi.master.shouldPump)
					{
						smi.GoTo(this.notCharging);
						return;
					}
					if (smi.IsReady())
					{
						smi.GoTo(this.charging.openChargingPre);
						return;
					}
					smi.GoTo(this.charging.closedChargingPre);
				});
			this.charging.opening.QueueAnim("opening_charging", false, null).OnAnimQueueComplete(this.charging.open);
			this.charging.open.PlayAnim("open_charging_loop", KAnim.PlayMode.Loop).EventTransition(GameHashes.OnStorageChange, this.charging.closing, (MaskStation.SMInstance smi) => !smi.IsReady());
			this.charging.closing.QueueAnim("closing_charging", false, null).OnAnimQueueComplete(this.charging.closed);
			this.charging.closed.PlayAnim("closed_charging_loop", KAnim.PlayMode.Loop).EventTransition(GameHashes.OnStorageChange, this.charging.opening, (MaskStation.SMInstance smi) => smi.IsReady());
			this.charging.openChargingPre.PlayAnim("open_charging_pre").OnAnimQueueComplete(this.charging.open);
			this.charging.closedChargingPre.PlayAnim("closed_charging_pre").OnAnimQueueComplete(this.charging.closed);
			this.notCharging.TagTransition(GameTags.Operational, this.notOperational, true).EventTransition(GameHashes.OnStorageChange, this.charging, (MaskStation.SMInstance smi) => !smi.OxygenIsFull() && smi.master.shouldPump).Update(delegate(MaskStation.SMInstance smi, float dt)
			{
				smi.master.UpdateOperational();
			}, UpdateRate.SIM_1000ms, false)
				.Enter(delegate(MaskStation.SMInstance smi)
				{
					if (!smi.OxygenIsFull() && smi.master.shouldPump)
					{
						smi.GoTo(this.charging);
						return;
					}
					if (smi.IsReady())
					{
						smi.GoTo(this.notCharging.openChargingPst);
						return;
					}
					smi.GoTo(this.notCharging.closedChargingPst);
				});
			this.notCharging.opening.PlayAnim("opening_not_charging").OnAnimQueueComplete(this.notCharging.open);
			this.notCharging.open.PlayAnim("open_not_charging_loop").EventTransition(GameHashes.OnStorageChange, this.notCharging.closing, (MaskStation.SMInstance smi) => !smi.IsReady());
			this.notCharging.closing.PlayAnim("closing_not_charging").OnAnimQueueComplete(this.notCharging.closed);
			this.notCharging.closed.PlayAnim("closed_not_charging_loop").EventTransition(GameHashes.OnStorageChange, this.notCharging.opening, (MaskStation.SMInstance smi) => smi.IsReady());
			this.notCharging.openChargingPst.PlayAnim("open_charging_pst").OnAnimQueueComplete(this.notCharging.open);
			this.notCharging.closedChargingPst.PlayAnim("closed_charging_pst").OnAnimQueueComplete(this.notCharging.closed);
		}

		// Token: 0x0400729C RID: 29340
		public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State notOperational;

		// Token: 0x0400729D RID: 29341
		public MaskStation.States.ChargingStates charging;

		// Token: 0x0400729E RID: 29342
		public MaskStation.States.NotChargingStates notCharging;

		// Token: 0x020027AB RID: 10155
		public class ChargingStates : GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State
		{
			// Token: 0x0400AFB4 RID: 44980
			public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State opening;

			// Token: 0x0400AFB5 RID: 44981
			public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State open;

			// Token: 0x0400AFB6 RID: 44982
			public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State closing;

			// Token: 0x0400AFB7 RID: 44983
			public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State closed;

			// Token: 0x0400AFB8 RID: 44984
			public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State openChargingPre;

			// Token: 0x0400AFB9 RID: 44985
			public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State closedChargingPre;
		}

		// Token: 0x020027AC RID: 10156
		public class NotChargingStates : GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State
		{
			// Token: 0x0400AFBA RID: 44986
			public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State opening;

			// Token: 0x0400AFBB RID: 44987
			public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State open;

			// Token: 0x0400AFBC RID: 44988
			public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State closing;

			// Token: 0x0400AFBD RID: 44989
			public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State closed;

			// Token: 0x0400AFBE RID: 44990
			public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State openChargingPst;

			// Token: 0x0400AFBF RID: 44991
			public GameStateMachine<MaskStation.States, MaskStation.SMInstance, MaskStation, object>.State closedChargingPst;
		}
	}
}
