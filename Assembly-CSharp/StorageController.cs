using System;

// Token: 0x0200005B RID: 91
public class StorageController : GameStateMachine<StorageController, StorageController.Instance>
{
	// Token: 0x060001B0 RID: 432 RVA: 0x0000C084 File Offset: 0x0000A284
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		this.root.EventTransition(GameHashes.OnStorageInteracted, this.working, null);
		this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.on, (StorageController.Instance smi) => smi.GetComponent<Operational>().IsOperational);
		this.on.PlayAnim("on").EventTransition(GameHashes.OperationalChanged, this.off, (StorageController.Instance smi) => !smi.GetComponent<Operational>().IsOperational);
		this.working.PlayAnim("working").OnAnimQueueComplete(this.off);
	}

	// Token: 0x04000114 RID: 276
	public GameStateMachine<StorageController, StorageController.Instance, IStateMachineTarget, object>.State off;

	// Token: 0x04000115 RID: 277
	public GameStateMachine<StorageController, StorageController.Instance, IStateMachineTarget, object>.State on;

	// Token: 0x04000116 RID: 278
	public GameStateMachine<StorageController, StorageController.Instance, IStateMachineTarget, object>.State working;

	// Token: 0x0200104E RID: 4174
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x0200104F RID: 4175
	public new class Instance : GameStateMachine<StorageController, StorageController.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06007F93 RID: 32659 RVA: 0x0032C2AA File Offset: 0x0032A4AA
		public Instance(IStateMachineTarget master, StorageController.Def def)
			: base(master)
		{
		}
	}
}
