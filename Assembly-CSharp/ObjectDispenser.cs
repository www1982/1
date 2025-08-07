using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000798 RID: 1944
public class ObjectDispenser : Switch, IUserControlledCapacity
{
	// Token: 0x17000331 RID: 817
	// (get) Token: 0x0600335C RID: 13148 RVA: 0x0012141D File Offset: 0x0011F61D
	// (set) Token: 0x0600335D RID: 13149 RVA: 0x00121435 File Offset: 0x0011F635
	public virtual float UserMaxCapacity
	{
		get
		{
			return Mathf.Min(this.userMaxCapacity, base.GetComponent<Storage>().capacityKg);
		}
		set
		{
			this.userMaxCapacity = value;
			this.filteredStorage.FilterChanged();
		}
	}

	// Token: 0x17000332 RID: 818
	// (get) Token: 0x0600335E RID: 13150 RVA: 0x00121449 File Offset: 0x0011F649
	public float AmountStored
	{
		get
		{
			return base.GetComponent<Storage>().MassStored();
		}
	}

	// Token: 0x17000333 RID: 819
	// (get) Token: 0x0600335F RID: 13151 RVA: 0x00121456 File Offset: 0x0011F656
	public float MinCapacity
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x17000334 RID: 820
	// (get) Token: 0x06003360 RID: 13152 RVA: 0x0012145D File Offset: 0x0011F65D
	public float MaxCapacity
	{
		get
		{
			return base.GetComponent<Storage>().capacityKg;
		}
	}

	// Token: 0x17000335 RID: 821
	// (get) Token: 0x06003361 RID: 13153 RVA: 0x0012146A File Offset: 0x0011F66A
	public bool WholeValues
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000336 RID: 822
	// (get) Token: 0x06003362 RID: 13154 RVA: 0x0012146D File Offset: 0x0011F66D
	public LocString CapacityUnits
	{
		get
		{
			return GameUtil.GetCurrentMassUnit(false);
		}
	}

	// Token: 0x06003363 RID: 13155 RVA: 0x00121475 File Offset: 0x0011F675
	protected override void OnPrefabInit()
	{
		this.Initialize();
	}

	// Token: 0x06003364 RID: 13156 RVA: 0x00121480 File Offset: 0x0011F680
	protected void Initialize()
	{
		base.OnPrefabInit();
		this.log = new LoggerFS("ObjectDispenser", 35);
		this.filteredStorage = new FilteredStorage(this, null, this, false, Db.Get().ChoreTypes.StorageFetch);
		base.Subscribe<ObjectDispenser>(-905833192, ObjectDispenser.OnCopySettingsDelegate);
	}

	// Token: 0x06003365 RID: 13157 RVA: 0x001214D4 File Offset: 0x0011F6D4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.smi = new ObjectDispenser.Instance(this, base.IsSwitchedOn);
		this.smi.StartSM();
		if (ObjectDispenser.infoStatusItem == null)
		{
			ObjectDispenser.infoStatusItem = new StatusItem("ObjectDispenserAutomationInfo", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			ObjectDispenser.infoStatusItem.resolveStringCallback = new Func<string, object, string>(ObjectDispenser.ResolveInfoStatusItemString);
		}
		this.filteredStorage.FilterChanged();
		base.GetComponent<KSelectable>().ToggleStatusItem(ObjectDispenser.infoStatusItem, true, this.smi);
	}

	// Token: 0x06003366 RID: 13158 RVA: 0x0012156C File Offset: 0x0011F76C
	protected override void OnCleanUp()
	{
		this.filteredStorage.CleanUp();
		base.OnCleanUp();
	}

	// Token: 0x06003367 RID: 13159 RVA: 0x00121580 File Offset: 0x0011F780
	private void OnCopySettings(object data)
	{
		GameObject gameObject = (GameObject)data;
		if (gameObject == null)
		{
			return;
		}
		ObjectDispenser component = gameObject.GetComponent<ObjectDispenser>();
		if (component == null)
		{
			return;
		}
		this.UserMaxCapacity = component.UserMaxCapacity;
	}

	// Token: 0x06003368 RID: 13160 RVA: 0x001215BC File Offset: 0x0011F7BC
	public void DropHeldItems()
	{
		while (this.storage.Count > 0)
		{
			GameObject gameObject = this.storage.Drop(this.storage.items[0], true);
			if (this.rotatable != null)
			{
				gameObject.transform.SetPosition(base.transform.GetPosition() + this.rotatable.GetRotatedCellOffset(this.dropOffset).ToVector3());
			}
			else
			{
				gameObject.transform.SetPosition(base.transform.GetPosition() + this.dropOffset.ToVector3());
			}
		}
		this.smi.GetMaster().GetComponent<Storage>().DropAll(false, false, default(Vector3), true, null);
	}

	// Token: 0x06003369 RID: 13161 RVA: 0x0012168B File Offset: 0x0011F88B
	protected override void Toggle()
	{
		base.Toggle();
	}

	// Token: 0x0600336A RID: 13162 RVA: 0x00121693 File Offset: 0x0011F893
	protected override void OnRefreshUserMenu(object data)
	{
		if (!this.smi.IsAutomated())
		{
			base.OnRefreshUserMenu(data);
		}
	}

	// Token: 0x0600336B RID: 13163 RVA: 0x001216AC File Offset: 0x0011F8AC
	private static string ResolveInfoStatusItemString(string format_str, object data)
	{
		ObjectDispenser.Instance instance = (ObjectDispenser.Instance)data;
		string text = (instance.IsAutomated() ? BUILDING.STATUSITEMS.OBJECTDISPENSER.AUTOMATION_CONTROL : BUILDING.STATUSITEMS.OBJECTDISPENSER.MANUAL_CONTROL);
		string text2 = (instance.IsOpened ? BUILDING.STATUSITEMS.OBJECTDISPENSER.OPENED : BUILDING.STATUSITEMS.OBJECTDISPENSER.CLOSED);
		return string.Format(text, text2);
	}

	// Token: 0x04001EDB RID: 7899
	public static readonly HashedString PORT_ID = "ObjectDispenser";

	// Token: 0x04001EDC RID: 7900
	private LoggerFS log;

	// Token: 0x04001EDD RID: 7901
	public CellOffset dropOffset;

	// Token: 0x04001EDE RID: 7902
	[MyCmpReq]
	private Building building;

	// Token: 0x04001EDF RID: 7903
	[MyCmpReq]
	private Storage storage;

	// Token: 0x04001EE0 RID: 7904
	[MyCmpGet]
	private Rotatable rotatable;

	// Token: 0x04001EE1 RID: 7905
	private ObjectDispenser.Instance smi;

	// Token: 0x04001EE2 RID: 7906
	private static StatusItem infoStatusItem;

	// Token: 0x04001EE3 RID: 7907
	[Serialize]
	private float userMaxCapacity = float.PositiveInfinity;

	// Token: 0x04001EE4 RID: 7908
	protected FilteredStorage filteredStorage;

	// Token: 0x04001EE5 RID: 7909
	private static readonly EventSystem.IntraObjectHandler<ObjectDispenser> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<ObjectDispenser>(delegate(ObjectDispenser component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x0200169F RID: 5791
	public class States : GameStateMachine<ObjectDispenser.States, ObjectDispenser.Instance, ObjectDispenser>
	{
		// Token: 0x06009602 RID: 38402 RVA: 0x00376938 File Offset: 0x00374B38
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.idle.PlayAnim("on").EventHandler(GameHashes.OnStorageChange, delegate(ObjectDispenser.Instance smi)
			{
				smi.UpdateState();
			}).ParamTransition<bool>(this.should_open, this.drop_item, (ObjectDispenser.Instance smi, bool p) => p && !smi.master.GetComponent<Storage>().IsEmpty());
			this.load_item.PlayAnim("working_load").OnAnimQueueComplete(this.load_item_pst);
			this.load_item_pst.ParamTransition<bool>(this.should_open, this.idle, (ObjectDispenser.Instance smi, bool p) => !p).ParamTransition<bool>(this.should_open, this.drop_item, (ObjectDispenser.Instance smi, bool p) => p);
			this.drop_item.PlayAnim("working_dispense").OnAnimQueueComplete(this.idle).Exit(delegate(ObjectDispenser.Instance smi)
			{
				smi.master.DropHeldItems();
			});
		}

		// Token: 0x0400736E RID: 29550
		public GameStateMachine<ObjectDispenser.States, ObjectDispenser.Instance, ObjectDispenser, object>.State load_item;

		// Token: 0x0400736F RID: 29551
		public GameStateMachine<ObjectDispenser.States, ObjectDispenser.Instance, ObjectDispenser, object>.State load_item_pst;

		// Token: 0x04007370 RID: 29552
		public GameStateMachine<ObjectDispenser.States, ObjectDispenser.Instance, ObjectDispenser, object>.State drop_item;

		// Token: 0x04007371 RID: 29553
		public GameStateMachine<ObjectDispenser.States, ObjectDispenser.Instance, ObjectDispenser, object>.State idle;

		// Token: 0x04007372 RID: 29554
		public StateMachine<ObjectDispenser.States, ObjectDispenser.Instance, ObjectDispenser, object>.BoolParameter should_open;
	}

	// Token: 0x020016A0 RID: 5792
	public class Instance : GameStateMachine<ObjectDispenser.States, ObjectDispenser.Instance, ObjectDispenser, object>.GameInstance
	{
		// Token: 0x06009604 RID: 38404 RVA: 0x00376A8C File Offset: 0x00374C8C
		public Instance(ObjectDispenser master, bool manual_start_state)
			: base(master)
		{
			this.manual_on = manual_start_state;
			this.operational = base.GetComponent<Operational>();
			this.logic = base.GetComponent<LogicPorts>();
			base.Subscribe(-592767678, new Action<object>(this.OnOperationalChanged));
			base.Subscribe(-801688580, new Action<object>(this.OnLogicValueChanged));
			base.smi.sm.should_open.Set(true, base.smi, false);
		}

		// Token: 0x06009605 RID: 38405 RVA: 0x00376B12 File Offset: 0x00374D12
		public void UpdateState()
		{
			base.smi.GoTo(base.sm.load_item);
		}

		// Token: 0x06009606 RID: 38406 RVA: 0x00376B2A File Offset: 0x00374D2A
		public bool IsAutomated()
		{
			return this.logic.IsPortConnected(ObjectDispenser.PORT_ID);
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06009607 RID: 38407 RVA: 0x00376B3C File Offset: 0x00374D3C
		public bool IsOpened
		{
			get
			{
				if (!this.IsAutomated())
				{
					return this.manual_on;
				}
				return this.logic_on;
			}
		}

		// Token: 0x06009608 RID: 38408 RVA: 0x00376B53 File Offset: 0x00374D53
		public void SetSwitchState(bool on)
		{
			this.manual_on = on;
			this.UpdateShouldOpen();
		}

		// Token: 0x06009609 RID: 38409 RVA: 0x00376B62 File Offset: 0x00374D62
		public void SetActive(bool active)
		{
			this.operational.SetActive(active, false);
		}

		// Token: 0x0600960A RID: 38410 RVA: 0x00376B71 File Offset: 0x00374D71
		private void OnOperationalChanged(object data)
		{
			this.UpdateShouldOpen();
		}

		// Token: 0x0600960B RID: 38411 RVA: 0x00376B7C File Offset: 0x00374D7C
		private void OnLogicValueChanged(object data)
		{
			LogicValueChanged logicValueChanged = (LogicValueChanged)data;
			if (logicValueChanged.portID != ObjectDispenser.PORT_ID)
			{
				return;
			}
			this.logic_on = LogicCircuitNetwork.IsBitActive(0, logicValueChanged.newValue);
			this.UpdateShouldOpen();
		}

		// Token: 0x0600960C RID: 38412 RVA: 0x00376BBC File Offset: 0x00374DBC
		private void UpdateShouldOpen()
		{
			this.SetActive(this.operational.IsOperational);
			if (!this.operational.IsOperational)
			{
				return;
			}
			if (this.IsAutomated())
			{
				base.smi.sm.should_open.Set(this.logic_on, base.smi, false);
				return;
			}
			base.smi.sm.should_open.Set(this.manual_on, base.smi, false);
		}

		// Token: 0x04007373 RID: 29555
		private Operational operational;

		// Token: 0x04007374 RID: 29556
		public LogicPorts logic;

		// Token: 0x04007375 RID: 29557
		public bool logic_on = true;

		// Token: 0x04007376 RID: 29558
		private bool manual_on;
	}
}
