using System;

// Token: 0x0200005A RID: 90
public class PoweredController : GameStateMachine<PoweredController, PoweredController.Instance>
{
	// Token: 0x060001AE RID: 430 RVA: 0x0000BFE4 File Offset: 0x0000A1E4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.on, (PoweredController.Instance smi) => smi.GetComponent<Operational>().IsOperational);
		this.on.PlayAnim("on").EventTransition(GameHashes.OperationalChanged, this.off, (PoweredController.Instance smi) => !smi.GetComponent<Operational>().IsOperational);
	}

	// Token: 0x04000112 RID: 274
	public GameStateMachine<PoweredController, PoweredController.Instance, IStateMachineTarget, object>.State off;

	// Token: 0x04000113 RID: 275
	public GameStateMachine<PoweredController, PoweredController.Instance, IStateMachineTarget, object>.State on;

	// Token: 0x0200104B RID: 4171
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x0200104C RID: 4172
	public new class Instance : GameStateMachine<PoweredController, PoweredController.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06007F8D RID: 32653 RVA: 0x0032C267 File Offset: 0x0032A467
		public Instance(IStateMachineTarget master, PoweredController.Def def)
			: base(master, def)
		{
		}

		// Token: 0x0400603B RID: 24635
		public bool ShowWorkingStatus;
	}
}
