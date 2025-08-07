using System;

// Token: 0x02000A08 RID: 2568
public class SafeCellMonitor : GameStateMachine<SafeCellMonitor, SafeCellMonitor.Instance, IStateMachineTarget, SafeCellMonitor.Def>
{
	// Token: 0x06004ACE RID: 19150 RVA: 0x001B19F8 File Offset: 0x001AFBF8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.safe;
		this.root.ToggleUrge(Db.Get().Urges.MoveToSafety);
		this.safe.EventTransition(GameHashes.SafeCellDetected, this.danger, (SafeCellMonitor.Instance smi) => smi.IsAreaUnsafe());
		this.danger.EventTransition(GameHashes.SafeCellLost, this.safe, (SafeCellMonitor.Instance smi) => !smi.IsAreaUnsafe()).ToggleChore((SafeCellMonitor.Instance smi) => new MoveToSafetyChore(smi.master), this.safe);
	}

	// Token: 0x0400317C RID: 12668
	public GameStateMachine<SafeCellMonitor, SafeCellMonitor.Instance, IStateMachineTarget, SafeCellMonitor.Def>.State safe;

	// Token: 0x0400317D RID: 12669
	public GameStateMachine<SafeCellMonitor, SafeCellMonitor.Instance, IStateMachineTarget, SafeCellMonitor.Def>.State danger;

	// Token: 0x02001A92 RID: 6802
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001A93 RID: 6803
	public new class Instance : GameStateMachine<SafeCellMonitor, SafeCellMonitor.Instance, IStateMachineTarget, SafeCellMonitor.Def>.GameInstance
	{
		// Token: 0x0600A415 RID: 42005 RVA: 0x003A5585 File Offset: 0x003A3785
		public Instance(IStateMachineTarget master, SafeCellMonitor.Def def)
			: base(master, def)
		{
			this.safeCellSensor = base.GetComponent<Sensors>().GetSensor<SafeCellSensor>();
		}

		// Token: 0x0600A416 RID: 42006 RVA: 0x003A55A0 File Offset: 0x003A37A0
		public bool IsAreaUnsafe()
		{
			return this.safeCellSensor.HasSafeCell();
		}

		// Token: 0x0400801F RID: 32799
		private SafeCellSensor safeCellSensor;
	}
}
