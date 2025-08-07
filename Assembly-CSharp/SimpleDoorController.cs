using System;

// Token: 0x02000B68 RID: 2920
public class SimpleDoorController : GameStateMachine<SimpleDoorController, SimpleDoorController.StatesInstance, IStateMachineTarget, SimpleDoorController.Def>
{
	// Token: 0x06005709 RID: 22281 RVA: 0x001F8730 File Offset: 0x001F6930
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.inactive;
		this.inactive.TagTransition(GameTags.RocketOnGround, this.active, false);
		this.active.DefaultState(this.active.closed).TagTransition(GameTags.RocketOnGround, this.inactive, true).Enter(delegate(SimpleDoorController.StatesInstance smi)
		{
			smi.Register();
		})
			.Exit(delegate(SimpleDoorController.StatesInstance smi)
			{
				smi.Unregister();
			});
		this.active.closed.PlayAnim((SimpleDoorController.StatesInstance smi) => smi.GetDefaultAnim(), KAnim.PlayMode.Loop).ParamTransition<int>(this.numOpens, this.active.opening, (SimpleDoorController.StatesInstance smi, int p) => p > 0);
		this.active.opening.PlayAnim("enter_pre", KAnim.PlayMode.Once).OnAnimQueueComplete(this.active.open);
		this.active.open.PlayAnim("enter_loop", KAnim.PlayMode.Loop).ParamTransition<int>(this.numOpens, this.active.closedelay, (SimpleDoorController.StatesInstance smi, int p) => p == 0);
		this.active.closedelay.ParamTransition<int>(this.numOpens, this.active.open, (SimpleDoorController.StatesInstance smi, int p) => p > 0).ScheduleGoTo(0.5f, this.active.closing);
		this.active.closing.PlayAnim("enter_pst", KAnim.PlayMode.Once).OnAnimQueueComplete(this.active.closed);
	}

	// Token: 0x04003A3A RID: 14906
	public GameStateMachine<SimpleDoorController, SimpleDoorController.StatesInstance, IStateMachineTarget, SimpleDoorController.Def>.State inactive;

	// Token: 0x04003A3B RID: 14907
	public SimpleDoorController.ActiveStates active;

	// Token: 0x04003A3C RID: 14908
	public StateMachine<SimpleDoorController, SimpleDoorController.StatesInstance, IStateMachineTarget, SimpleDoorController.Def>.IntParameter numOpens;

	// Token: 0x02001C93 RID: 7315
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001C94 RID: 7316
	public class ActiveStates : GameStateMachine<SimpleDoorController, SimpleDoorController.StatesInstance, IStateMachineTarget, SimpleDoorController.Def>.State
	{
		// Token: 0x040086B9 RID: 34489
		public GameStateMachine<SimpleDoorController, SimpleDoorController.StatesInstance, IStateMachineTarget, SimpleDoorController.Def>.State closed;

		// Token: 0x040086BA RID: 34490
		public GameStateMachine<SimpleDoorController, SimpleDoorController.StatesInstance, IStateMachineTarget, SimpleDoorController.Def>.State opening;

		// Token: 0x040086BB RID: 34491
		public GameStateMachine<SimpleDoorController, SimpleDoorController.StatesInstance, IStateMachineTarget, SimpleDoorController.Def>.State open;

		// Token: 0x040086BC RID: 34492
		public GameStateMachine<SimpleDoorController, SimpleDoorController.StatesInstance, IStateMachineTarget, SimpleDoorController.Def>.State closedelay;

		// Token: 0x040086BD RID: 34493
		public GameStateMachine<SimpleDoorController, SimpleDoorController.StatesInstance, IStateMachineTarget, SimpleDoorController.Def>.State closing;
	}

	// Token: 0x02001C95 RID: 7317
	public class StatesInstance : GameStateMachine<SimpleDoorController, SimpleDoorController.StatesInstance, IStateMachineTarget, SimpleDoorController.Def>.GameInstance, INavDoor
	{
		// Token: 0x0600ABB1 RID: 43953 RVA: 0x003C04A0 File Offset: 0x003BE6A0
		public StatesInstance(IStateMachineTarget master, SimpleDoorController.Def def)
			: base(master, def)
		{
		}

		// Token: 0x0600ABB2 RID: 43954 RVA: 0x003C04AC File Offset: 0x003BE6AC
		public string GetDefaultAnim()
		{
			KBatchedAnimController component = base.master.GetComponent<KBatchedAnimController>();
			if (component != null)
			{
				return component.initialAnim;
			}
			return "idle_loop";
		}

		// Token: 0x0600ABB3 RID: 43955 RVA: 0x003C04DC File Offset: 0x003BE6DC
		public void Register()
		{
			int num = Grid.PosToCell(base.gameObject.transform.GetPosition());
			Grid.HasDoor[num] = true;
		}

		// Token: 0x0600ABB4 RID: 43956 RVA: 0x003C050C File Offset: 0x003BE70C
		public void Unregister()
		{
			int num = Grid.PosToCell(base.gameObject.transform.GetPosition());
			Grid.HasDoor[num] = false;
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x0600ABB5 RID: 43957 RVA: 0x003C053B File Offset: 0x003BE73B
		public bool isSpawned
		{
			get
			{
				return base.master.gameObject.GetComponent<KMonoBehaviour>().isSpawned;
			}
		}

		// Token: 0x0600ABB6 RID: 43958 RVA: 0x003C0552 File Offset: 0x003BE752
		public void Close()
		{
			base.sm.numOpens.Delta(-1, base.smi);
		}

		// Token: 0x0600ABB7 RID: 43959 RVA: 0x003C056C File Offset: 0x003BE76C
		public bool IsOpen()
		{
			return base.IsInsideState(base.sm.active.open) || base.IsInsideState(base.sm.active.closedelay);
		}

		// Token: 0x0600ABB8 RID: 43960 RVA: 0x003C059E File Offset: 0x003BE79E
		public void Open()
		{
			base.sm.numOpens.Delta(1, base.smi);
		}
	}
}
