using System;
using KSerialization;

// Token: 0x02000882 RID: 2178
public class RoverChoreMonitor : GameStateMachine<RoverChoreMonitor, RoverChoreMonitor.Instance, IStateMachineTarget, RoverChoreMonitor.Def>
{
	// Token: 0x06003BE6 RID: 15334 RVA: 0x0014C150 File Offset: 0x0014A350
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.loop;
		this.loop.ToggleBehaviour(GameTags.Creatures.Tunnel, (RoverChoreMonitor.Instance smi) => true, null).ToggleBehaviour(GameTags.Creatures.Builder, (RoverChoreMonitor.Instance smi) => true, null);
	}

	// Token: 0x040024BD RID: 9405
	public GameStateMachine<RoverChoreMonitor, RoverChoreMonitor.Instance, IStateMachineTarget, RoverChoreMonitor.Def>.State loop;

	// Token: 0x0200183F RID: 6207
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001840 RID: 6208
	public new class Instance : GameStateMachine<RoverChoreMonitor, RoverChoreMonitor.Instance, IStateMachineTarget, RoverChoreMonitor.Def>.GameInstance
	{
		// Token: 0x06009BF8 RID: 39928 RVA: 0x0038FAEF File Offset: 0x0038DCEF
		public Instance(IStateMachineTarget master, RoverChoreMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06009BF9 RID: 39929 RVA: 0x0038FB00 File Offset: 0x0038DD00
		protected override void OnCleanUp()
		{
			base.OnCleanUp();
		}

		// Token: 0x04007856 RID: 30806
		[Serialize]
		public int lastDigCell = -1;

		// Token: 0x04007857 RID: 30807
		private Action<object> OnDestinationReachedDelegate;
	}
}
