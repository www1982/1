using System;
using STRINGS;

// Token: 0x02000734 RID: 1844
public class Gantry : Switch
{
	// Token: 0x06002E78 RID: 11896 RVA: 0x0010A81C File Offset: 0x00108A1C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (Gantry.infoStatusItem == null)
		{
			Gantry.infoStatusItem = new StatusItem("GantryAutomationInfo", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			Gantry.infoStatusItem.resolveStringCallback = new Func<string, object, string>(Gantry.ResolveInfoStatusItemString);
		}
		base.GetComponent<KAnimControllerBase>().PlaySpeedMultiplier = 0.5f;
		this.smi = new Gantry.Instance(this, base.IsSwitchedOn);
		this.smi.StartSM();
		base.GetComponent<KSelectable>().ToggleStatusItem(Gantry.infoStatusItem, true, this.smi);
	}

	// Token: 0x06002E79 RID: 11897 RVA: 0x0010A8B9 File Offset: 0x00108AB9
	protected override void OnCleanUp()
	{
		if (this.smi != null)
		{
			this.smi.StopSM("cleanup");
		}
		base.OnCleanUp();
	}

	// Token: 0x06002E7A RID: 11898 RVA: 0x0010A8D9 File Offset: 0x00108AD9
	public void SetWalkable(bool active)
	{
		this.fakeFloorAdder.SetFloor(active);
	}

	// Token: 0x06002E7B RID: 11899 RVA: 0x0010A8E7 File Offset: 0x00108AE7
	protected override void Toggle()
	{
		base.Toggle();
		this.smi.SetSwitchState(this.switchedOn);
	}

	// Token: 0x06002E7C RID: 11900 RVA: 0x0010A900 File Offset: 0x00108B00
	protected override void OnRefreshUserMenu(object data)
	{
		if (!this.smi.IsAutomated())
		{
			base.OnRefreshUserMenu(data);
		}
	}

	// Token: 0x06002E7D RID: 11901 RVA: 0x0010A916 File Offset: 0x00108B16
	protected override void UpdateSwitchStatus()
	{
	}

	// Token: 0x06002E7E RID: 11902 RVA: 0x0010A918 File Offset: 0x00108B18
	private static string ResolveInfoStatusItemString(string format_str, object data)
	{
		Gantry.Instance instance = (Gantry.Instance)data;
		string text = (instance.IsAutomated() ? BUILDING.STATUSITEMS.GANTRY.AUTOMATION_CONTROL : BUILDING.STATUSITEMS.GANTRY.MANUAL_CONTROL);
		string text2 = (instance.IsExtended() ? BUILDING.STATUSITEMS.GANTRY.EXTENDED : BUILDING.STATUSITEMS.GANTRY.RETRACTED);
		return string.Format(text, text2);
	}

	// Token: 0x04001B7E RID: 7038
	public static readonly HashedString PORT_ID = "Gantry";

	// Token: 0x04001B7F RID: 7039
	[MyCmpReq]
	private Building building;

	// Token: 0x04001B80 RID: 7040
	[MyCmpReq]
	private FakeFloorAdder fakeFloorAdder;

	// Token: 0x04001B81 RID: 7041
	private Gantry.Instance smi;

	// Token: 0x04001B82 RID: 7042
	private static StatusItem infoStatusItem;

	// Token: 0x020015E1 RID: 5601
	public class States : GameStateMachine<Gantry.States, Gantry.Instance, Gantry>
	{
		// Token: 0x0600931C RID: 37660 RVA: 0x00369520 File Offset: 0x00367720
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.extended;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.retracted_pre.Enter(delegate(Gantry.Instance smi)
			{
				smi.SetActive(true);
			}).Exit(delegate(Gantry.Instance smi)
			{
				smi.SetActive(false);
			}).PlayAnim("off_pre")
				.OnAnimQueueComplete(this.retracted);
			this.retracted.PlayAnim("off").ParamTransition<bool>(this.should_extend, this.extended_pre, GameStateMachine<Gantry.States, Gantry.Instance, Gantry, object>.IsTrue);
			this.extended_pre.Enter(delegate(Gantry.Instance smi)
			{
				smi.SetActive(true);
			}).Exit(delegate(Gantry.Instance smi)
			{
				smi.SetActive(false);
			}).PlayAnim("on_pre")
				.OnAnimQueueComplete(this.extended);
			this.extended.Enter(delegate(Gantry.Instance smi)
			{
				smi.master.SetWalkable(true);
			}).Exit(delegate(Gantry.Instance smi)
			{
				smi.master.SetWalkable(false);
			}).PlayAnim("on")
				.ParamTransition<bool>(this.should_extend, this.retracted_pre, GameStateMachine<Gantry.States, Gantry.Instance, Gantry, object>.IsFalse)
				.ToggleTag(GameTags.GantryExtended);
		}

		// Token: 0x0400712F RID: 28975
		public GameStateMachine<Gantry.States, Gantry.Instance, Gantry, object>.State retracted_pre;

		// Token: 0x04007130 RID: 28976
		public GameStateMachine<Gantry.States, Gantry.Instance, Gantry, object>.State retracted;

		// Token: 0x04007131 RID: 28977
		public GameStateMachine<Gantry.States, Gantry.Instance, Gantry, object>.State extended_pre;

		// Token: 0x04007132 RID: 28978
		public GameStateMachine<Gantry.States, Gantry.Instance, Gantry, object>.State extended;

		// Token: 0x04007133 RID: 28979
		public StateMachine<Gantry.States, Gantry.Instance, Gantry, object>.BoolParameter should_extend;
	}

	// Token: 0x020015E2 RID: 5602
	public class Instance : GameStateMachine<Gantry.States, Gantry.Instance, Gantry, object>.GameInstance
	{
		// Token: 0x0600931E RID: 37662 RVA: 0x003696AC File Offset: 0x003678AC
		public Instance(Gantry master, bool manual_start_state)
			: base(master)
		{
			this.manual_on = manual_start_state;
			this.operational = base.GetComponent<Operational>();
			this.logic = base.GetComponent<LogicPorts>();
			base.Subscribe(-592767678, new Action<object>(this.OnOperationalChanged));
			base.Subscribe(-801688580, new Action<object>(this.OnLogicValueChanged));
			base.smi.sm.should_extend.Set(true, base.smi, false);
		}

		// Token: 0x0600931F RID: 37663 RVA: 0x00369732 File Offset: 0x00367932
		public bool IsAutomated()
		{
			return this.logic.IsPortConnected(Gantry.PORT_ID);
		}

		// Token: 0x06009320 RID: 37664 RVA: 0x00369744 File Offset: 0x00367944
		public bool IsExtended()
		{
			if (!this.IsAutomated())
			{
				return this.manual_on;
			}
			return this.logic_on;
		}

		// Token: 0x06009321 RID: 37665 RVA: 0x0036975B File Offset: 0x0036795B
		public void SetSwitchState(bool on)
		{
			this.manual_on = on;
			this.UpdateShouldExtend();
		}

		// Token: 0x06009322 RID: 37666 RVA: 0x0036976A File Offset: 0x0036796A
		public void SetActive(bool active)
		{
			this.operational.SetActive(this.operational.IsOperational && active, false);
		}

		// Token: 0x06009323 RID: 37667 RVA: 0x00369785 File Offset: 0x00367985
		private void OnOperationalChanged(object data)
		{
			this.UpdateShouldExtend();
		}

		// Token: 0x06009324 RID: 37668 RVA: 0x00369790 File Offset: 0x00367990
		private void OnLogicValueChanged(object data)
		{
			LogicValueChanged logicValueChanged = (LogicValueChanged)data;
			if (logicValueChanged.portID != Gantry.PORT_ID)
			{
				return;
			}
			this.logic_on = LogicCircuitNetwork.IsBitActive(0, logicValueChanged.newValue);
			this.UpdateShouldExtend();
		}

		// Token: 0x06009325 RID: 37669 RVA: 0x003697D0 File Offset: 0x003679D0
		private void UpdateShouldExtend()
		{
			if (!this.operational.IsOperational)
			{
				return;
			}
			if (this.IsAutomated())
			{
				base.smi.sm.should_extend.Set(this.logic_on, base.smi, false);
				return;
			}
			base.smi.sm.should_extend.Set(this.manual_on, base.smi, false);
		}

		// Token: 0x04007134 RID: 28980
		private Operational operational;

		// Token: 0x04007135 RID: 28981
		public LogicPorts logic;

		// Token: 0x04007136 RID: 28982
		public bool logic_on = true;

		// Token: 0x04007137 RID: 28983
		private bool manual_on;
	}
}
