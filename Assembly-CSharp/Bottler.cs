using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x020006E1 RID: 1761
[AddComponentMenu("KMonoBehaviour/Workable/Bottler")]
public class Bottler : Workable, IUserControlledCapacity
{
	// Token: 0x17000234 RID: 564
	// (get) Token: 0x06002BA8 RID: 11176 RVA: 0x000FBB57 File Offset: 0x000F9D57
	// (set) Token: 0x06002BA9 RID: 11177 RVA: 0x000FBB83 File Offset: 0x000F9D83
	public float UserMaxCapacity
	{
		get
		{
			if (this.consumer != null)
			{
				return Mathf.Min(this.userMaxCapacity, this.storage.capacityKg);
			}
			return 0f;
		}
		set
		{
			this.userMaxCapacity = value;
			this.SetConsumerCapacity(value);
		}
	}

	// Token: 0x17000235 RID: 565
	// (get) Token: 0x06002BAA RID: 11178 RVA: 0x000FBB93 File Offset: 0x000F9D93
	public float AmountStored
	{
		get
		{
			return this.storage.MassStored();
		}
	}

	// Token: 0x17000236 RID: 566
	// (get) Token: 0x06002BAB RID: 11179 RVA: 0x000FBBA0 File Offset: 0x000F9DA0
	public float MinCapacity
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x17000237 RID: 567
	// (get) Token: 0x06002BAC RID: 11180 RVA: 0x000FBBA7 File Offset: 0x000F9DA7
	public float MaxCapacity
	{
		get
		{
			return this.storage.capacityKg;
		}
	}

	// Token: 0x17000238 RID: 568
	// (get) Token: 0x06002BAD RID: 11181 RVA: 0x000FBBB4 File Offset: 0x000F9DB4
	public bool WholeValues
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000239 RID: 569
	// (get) Token: 0x06002BAE RID: 11182 RVA: 0x000FBBB7 File Offset: 0x000F9DB7
	public LocString CapacityUnits
	{
		get
		{
			return GameUtil.GetCurrentMassUnit(false);
		}
	}

	// Token: 0x1700023A RID: 570
	// (get) Token: 0x06002BAF RID: 11183 RVA: 0x000FBBBF File Offset: 0x000F9DBF
	private Tag SourceTag
	{
		get
		{
			if (this.smi.master.consumer.conduitType != ConduitType.Gas)
			{
				return GameTags.LiquidSource;
			}
			return GameTags.GasSource;
		}
	}

	// Token: 0x1700023B RID: 571
	// (get) Token: 0x06002BB0 RID: 11184 RVA: 0x000FBBE4 File Offset: 0x000F9DE4
	private Tag ElementTag
	{
		get
		{
			if (this.smi.master.consumer.conduitType != ConduitType.Gas)
			{
				return GameTags.Liquid;
			}
			return GameTags.Gas;
		}
	}

	// Token: 0x06002BB1 RID: 11185 RVA: 0x000FBC0C File Offset: 0x000F9E0C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_bottler_kanim") };
		this.workAnims = new HashedString[] { "pick_up" };
		this.workingPstComplete = null;
		this.workingPstFailed = null;
		this.synchronizeAnims = true;
		base.SetOffsets(new CellOffset[] { this.workCellOffset });
		base.SetWorkTime(this.overrideAnims[0].GetData().GetAnim("pick_up").totalTime);
		this.resetProgressOnStop = true;
		this.showProgressBar = false;
	}

	// Token: 0x06002BB2 RID: 11186 RVA: 0x000FBCB8 File Offset: 0x000F9EB8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.smi = new Bottler.Controller.Instance(this);
		this.smi.StartSM();
		base.Subscribe<Bottler>(-905833192, Bottler.OnCopySettingsDelegate);
		this.UpdateStoredItemState();
		this.SetConsumerCapacity(this.userMaxCapacity);
	}

	// Token: 0x06002BB3 RID: 11187 RVA: 0x000FBD08 File Offset: 0x000F9F08
	protected override void OnForcedCleanUp()
	{
		if (base.worker != null)
		{
			ChoreDriver component = base.worker.GetComponent<ChoreDriver>();
			if (component != null)
			{
				component.StopChore();
			}
			else
			{
				base.worker.StopWork();
			}
		}
		if (this.workerMeter != null)
		{
			this.CleanupBottleProxyObject();
		}
		base.OnForcedCleanUp();
	}

	// Token: 0x06002BB4 RID: 11188 RVA: 0x000FBD5F File Offset: 0x000F9F5F
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		this.CreateBottleProxyObject(worker);
	}

	// Token: 0x06002BB5 RID: 11189 RVA: 0x000FBD70 File Offset: 0x000F9F70
	private void CreateBottleProxyObject(WorkerBase worker)
	{
		if (this.workerMeter != null)
		{
			this.CleanupBottleProxyObject();
		}
		PrimaryElement firstPrimaryElement = this.smi.master.GetFirstPrimaryElement();
		if (firstPrimaryElement == null)
		{
			return;
		}
		this.workerMeter = new MeterController(worker.GetComponent<KBatchedAnimController>(), "snapto_chest", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "snapto_chest" });
		this.workerMeter.meterController.SwapAnims(firstPrimaryElement.Element.substance.anims);
		this.workerMeter.meterController.Play("empty", KAnim.PlayMode.Paused, 1f, 0f);
		Color32 colour = firstPrimaryElement.Element.substance.colour;
		colour.a = byte.MaxValue;
		this.workerMeter.SetSymbolTint(new KAnimHashedString("meter_fill"), colour);
		this.workerMeter.SetSymbolTint(new KAnimHashedString("water1"), colour);
		this.workerMeter.SetSymbolTint(new KAnimHashedString("substance_tinter"), colour);
		this.workerMeter.SetSymbolTint(new KAnimHashedString("substance_tinter_cap"), colour);
	}

	// Token: 0x06002BB6 RID: 11190 RVA: 0x000FBE8C File Offset: 0x000FA08C
	private void CleanupBottleProxyObject()
	{
		if (this.workerMeter != null && !this.workerMeter.gameObject.IsNullOrDestroyed())
		{
			this.workerMeter.Unlink();
			this.workerMeter.gameObject.DeleteObject();
		}
		else
		{
			string text = "Bottler finished work but could not clean up the proxy bottle object. workerMeter=";
			MeterController meterController = this.workerMeter;
			DebugUtil.DevLogError(text + ((meterController != null) ? meterController.ToString() : null));
			KCrashReporter.ReportDevNotification("Bottle emptier could not clean up proxy object", Environment.StackTrace, "", false, null);
		}
		this.workerMeter = null;
	}

	// Token: 0x06002BB7 RID: 11191 RVA: 0x000FBF0E File Offset: 0x000FA10E
	protected override void OnStopWork(WorkerBase worker)
	{
		base.OnStopWork(worker);
		this.CleanupBottleProxyObject();
	}

	// Token: 0x06002BB8 RID: 11192 RVA: 0x000FBF1D File Offset: 0x000FA11D
	protected override void OnAbortWork(WorkerBase worker)
	{
		base.OnAbortWork(worker);
		this.GetAnimController().Play("ready", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x06002BB9 RID: 11193 RVA: 0x000FBF48 File Offset: 0x000FA148
	protected override void OnCompleteWork(WorkerBase worker)
	{
		Storage component = worker.GetComponent<Storage>();
		Pickupable.PickupableStartWorkInfo pickupableStartWorkInfo = (Pickupable.PickupableStartWorkInfo)worker.GetStartWorkInfo();
		if (pickupableStartWorkInfo.amount > 0f)
		{
			this.storage.TransferMass(component, pickupableStartWorkInfo.originalPickupable.KPrefabID.PrefabID(), pickupableStartWorkInfo.amount, false, false, false);
		}
		GameObject gameObject = component.FindFirst(pickupableStartWorkInfo.originalPickupable.KPrefabID.PrefabID());
		if (gameObject != null)
		{
			Pickupable component2 = gameObject.GetComponent<Pickupable>();
			component2.targetWorkable = component2;
			component2.RemoveTag(this.SourceTag);
			FetchableMonitor.Instance instance = component2.GetSMI<FetchableMonitor.Instance>();
			if (instance != null)
			{
				instance.SetForceUnfetchable(false);
			}
			pickupableStartWorkInfo.setResultCb(gameObject);
		}
		else
		{
			pickupableStartWorkInfo.setResultCb(null);
		}
		base.OnCompleteWork(worker);
	}

	// Token: 0x06002BBA RID: 11194 RVA: 0x000FC008 File Offset: 0x000FA208
	private void OnReservationsChanged(Pickupable _ignore, bool _ignore2, Pickupable.Reservation _ignore3)
	{
		bool flag = false;
		using (List<GameObject>.Enumerator enumerator = this.storage.items.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.GetComponent<Pickupable>().ReservedAmount > 0f)
				{
					flag = true;
					break;
				}
			}
		}
		foreach (GameObject gameObject in this.storage.items)
		{
			FetchableMonitor.Instance instance = gameObject.GetSMI<FetchableMonitor.Instance>();
			if (instance != null)
			{
				instance.SetForceUnfetchable(flag);
			}
		}
	}

	// Token: 0x06002BBB RID: 11195 RVA: 0x000FC0C0 File Offset: 0x000FA2C0
	private void SetConsumerCapacity(float value)
	{
		if (this.consumer != null)
		{
			this.consumer.capacityKG = value;
			float num = this.storage.MassStored() - this.userMaxCapacity;
			if (num > 0f)
			{
				this.storage.DropSome(this.storage.FindFirstWithMass(this.smi.master.ElementTag, 0f).ElementID.CreateTag(), num, false, false, new Vector3(0.8f, 0f, 0f), true, false);
			}
		}
	}

	// Token: 0x06002BBC RID: 11196 RVA: 0x000FC151 File Offset: 0x000FA351
	protected override void OnCleanUp()
	{
		if (this.smi != null)
		{
			this.smi.StopSM("OnCleanUp");
		}
		base.OnCleanUp();
	}

	// Token: 0x06002BBD RID: 11197 RVA: 0x000FC174 File Offset: 0x000FA374
	private PrimaryElement GetFirstPrimaryElement()
	{
		for (int i = 0; i < this.storage.Count; i++)
		{
			GameObject gameObject = this.storage[i];
			if (!(gameObject == null))
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (!(component == null))
				{
					return component;
				}
			}
		}
		return null;
	}

	// Token: 0x06002BBE RID: 11198 RVA: 0x000FC1C0 File Offset: 0x000FA3C0
	private void UpdateStoredItemState()
	{
		this.storage.allowItemRemoval = this.smi != null && this.smi.GetCurrentState() == this.smi.sm.operational.ready;
		foreach (GameObject gameObject in this.storage.items)
		{
			if (gameObject != null)
			{
				gameObject.Trigger(-778359855, this.storage);
			}
		}
	}

	// Token: 0x06002BBF RID: 11199 RVA: 0x000FC264 File Offset: 0x000FA464
	private void OnCopySettings(object data)
	{
		Bottler component = ((GameObject)data).GetComponent<Bottler>();
		this.UserMaxCapacity = component.UserMaxCapacity;
	}

	// Token: 0x040019D3 RID: 6611
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x040019D4 RID: 6612
	public Storage storage;

	// Token: 0x040019D5 RID: 6613
	public ConduitConsumer consumer;

	// Token: 0x040019D6 RID: 6614
	public CellOffset workCellOffset = new CellOffset(0, 0);

	// Token: 0x040019D7 RID: 6615
	[Serialize]
	public float userMaxCapacity = float.PositiveInfinity;

	// Token: 0x040019D8 RID: 6616
	private Bottler.Controller.Instance smi;

	// Token: 0x040019D9 RID: 6617
	private int storageHandle;

	// Token: 0x040019DA RID: 6618
	private MeterController workerMeter;

	// Token: 0x040019DB RID: 6619
	private static readonly EventSystem.IntraObjectHandler<Bottler> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<Bottler>(delegate(Bottler component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x0200156B RID: 5483
	private class Controller : GameStateMachine<Bottler.Controller, Bottler.Controller.Instance, Bottler>
	{
		// Token: 0x0600912F RID: 37167 RVA: 0x00362BA4 File Offset: 0x00360DA4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.nonoperational;
			this.root.Enter(delegate(Bottler.Controller.Instance smi)
			{
				smi.master.storage.allowItemRemoval = false;
			});
			this.nonoperational.PlayAnim("off").TagTransition(GameTags.Operational, this.operational, false);
			this.operational.EnterTransition(this.operational.ready, new StateMachine<Bottler.Controller, Bottler.Controller.Instance, Bottler, object>.Transition.ConditionCallback(Bottler.Controller.IsFull)).DefaultState(this.operational.empty).TagTransition(GameTags.Operational, this.nonoperational, true);
			this.operational.empty.PlayAnim("off").EventHandlerTransition(GameHashes.OnStorageChange, this.operational.filling, (Bottler.Controller.Instance smi, object o) => Bottler.Controller.IsFull(smi));
			this.operational.filling.PlayAnim("working").Enter(delegate(Bottler.Controller.Instance smi)
			{
				smi.UpdateMeter();
			}).OnAnimQueueComplete(this.operational.ready);
			this.operational.ready.EventTransition(GameHashes.OnStorageChange, this.operational.empty, GameStateMachine<Bottler.Controller, Bottler.Controller.Instance, Bottler, object>.Not(new StateMachine<Bottler.Controller, Bottler.Controller.Instance, Bottler, object>.Transition.ConditionCallback(Bottler.Controller.IsFull))).PlayAnim("ready").Enter(delegate(Bottler.Controller.Instance smi)
			{
				smi.master.storage.allowItemRemoval = true;
			})
				.Exit(delegate(Bottler.Controller.Instance smi)
				{
					smi.master.storage.allowItemRemoval = false;
				})
				.Enter(delegate(Bottler.Controller.Instance smi)
				{
					smi.master.storage.allowItemRemoval = true;
					smi.UpdateMeter();
					foreach (GameObject gameObject in smi.master.storage.items)
					{
						Pickupable component = gameObject.GetComponent<Pickupable>();
						component.targetWorkable = smi.master;
						component.SetOffsets(new CellOffset[] { smi.master.workCellOffset });
						Pickupable pickupable = component;
						pickupable.OnReservationsChanged = (Action<Pickupable, bool, Pickupable.Reservation>)Delegate.Combine(pickupable.OnReservationsChanged, new Action<Pickupable, bool, Pickupable.Reservation>(smi.master.OnReservationsChanged));
						component.KPrefabID.AddTag(smi.master.SourceTag, false);
						gameObject.Trigger(-778359855, smi.master.storage);
					}
				})
				.Exit(delegate(Bottler.Controller.Instance smi)
				{
					smi.master.storage.allowItemRemoval = false;
					foreach (GameObject gameObject2 in smi.master.storage.items)
					{
						Pickupable component2 = gameObject2.GetComponent<Pickupable>();
						component2.targetWorkable = component2;
						component2.SetOffsetTable(OffsetGroups.InvertedStandardTable);
						component2.OnReservationsChanged = (Action<Pickupable, bool, Pickupable.Reservation>)Delegate.Remove(component2.OnReservationsChanged, new Action<Pickupable, bool, Pickupable.Reservation>(smi.master.OnReservationsChanged));
						component2.KPrefabID.RemoveTag(smi.master.SourceTag);
						FetchableMonitor.Instance smi2 = component2.GetSMI<FetchableMonitor.Instance>();
						if (smi2 != null)
						{
							smi2.SetForceUnfetchable(false);
						}
						gameObject2.Trigger(-778359855, smi.master.storage);
					}
				});
		}

		// Token: 0x06009130 RID: 37168 RVA: 0x00362DAA File Offset: 0x00360FAA
		public static bool IsFull(Bottler.Controller.Instance smi)
		{
			return smi.master.storage.MassStored() >= smi.master.userMaxCapacity && smi.master.userMaxCapacity > 0f;
		}

		// Token: 0x04006FA1 RID: 28577
		public GameStateMachine<Bottler.Controller, Bottler.Controller.Instance, Bottler, object>.State nonoperational;

		// Token: 0x04006FA2 RID: 28578
		public Bottler.Controller.OperationalStates operational;

		// Token: 0x02002764 RID: 10084
		public class OperationalStates : GameStateMachine<Bottler.Controller, Bottler.Controller.Instance, Bottler, object>.State
		{
			// Token: 0x0400ADF3 RID: 44531
			public GameStateMachine<Bottler.Controller, Bottler.Controller.Instance, Bottler, object>.State empty;

			// Token: 0x0400ADF4 RID: 44532
			public GameStateMachine<Bottler.Controller, Bottler.Controller.Instance, Bottler, object>.State filling;

			// Token: 0x0400ADF5 RID: 44533
			public GameStateMachine<Bottler.Controller, Bottler.Controller.Instance, Bottler, object>.State ready;
		}

		// Token: 0x02002765 RID: 10085
		public new class Instance : GameStateMachine<Bottler.Controller, Bottler.Controller.Instance, Bottler, object>.GameInstance
		{
			// Token: 0x17000CD6 RID: 3286
			// (get) Token: 0x0600C6E2 RID: 50914 RVA: 0x00412E6B File Offset: 0x0041106B
			// (set) Token: 0x0600C6E3 RID: 50915 RVA: 0x00412E73 File Offset: 0x00411073
			public MeterController meter { get; private set; }

			// Token: 0x0600C6E4 RID: 50916 RVA: 0x00412E7C File Offset: 0x0041107C
			public Instance(Bottler master)
				: base(master)
			{
				this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "bottle", "off", Meter.Offset.UserSpecified, Grid.SceneLayer.BuildingFront, new string[] { "bottle", "substance_tinter", "substance_tinter_cap" });
			}

			// Token: 0x0600C6E5 RID: 50917 RVA: 0x00412ECC File Offset: 0x004110CC
			public void UpdateMeter()
			{
				PrimaryElement firstPrimaryElement = base.smi.master.GetFirstPrimaryElement();
				if (firstPrimaryElement == null)
				{
					return;
				}
				this.meter.meterController.SwapAnims(firstPrimaryElement.Element.substance.anims);
				this.meter.meterController.Play(OreSizeVisualizerComponents.GetAnimForMass(firstPrimaryElement.Mass), KAnim.PlayMode.Paused, 1f, 0f);
				Color32 colour = firstPrimaryElement.Element.substance.colour;
				colour.a = byte.MaxValue;
				this.meter.SetSymbolTint(new KAnimHashedString("meter_fill"), colour);
				this.meter.SetSymbolTint(new KAnimHashedString("water1"), colour);
				this.meter.SetSymbolTint(new KAnimHashedString("substance_tinter"), colour);
				this.meter.SetSymbolTint(new KAnimHashedString("substance_tinter_cap"), colour);
			}
		}
	}
}
