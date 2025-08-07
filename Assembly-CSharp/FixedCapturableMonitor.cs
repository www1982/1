using System;

// Token: 0x02000596 RID: 1430
public class FixedCapturableMonitor : GameStateMachine<FixedCapturableMonitor, FixedCapturableMonitor.Instance, IStateMachineTarget, FixedCapturableMonitor.Def>
{
	// Token: 0x060020AF RID: 8367 RVA: 0x000BCDBC File Offset: 0x000BAFBC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleBehaviour(GameTags.Creatures.WantsToGetCaptured, (FixedCapturableMonitor.Instance smi) => smi.ShouldGoGetCaptured(), null).Enter(delegate(FixedCapturableMonitor.Instance smi)
		{
			Components.FixedCapturableMonitors.Add(smi);
		}).Exit(delegate(FixedCapturableMonitor.Instance smi)
		{
			Components.FixedCapturableMonitors.Remove(smi);
		});
	}

	// Token: 0x02001405 RID: 5125
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001406 RID: 5126
	public new class Instance : GameStateMachine<FixedCapturableMonitor, FixedCapturableMonitor.Instance, IStateMachineTarget, FixedCapturableMonitor.Def>.GameInstance
	{
		// Token: 0x06008C35 RID: 35893 RVA: 0x00355390 File Offset: 0x00353590
		public Instance(IStateMachineTarget master, FixedCapturableMonitor.Def def)
			: base(master, def)
		{
			this.ChoreConsumer = base.GetComponent<ChoreConsumer>();
			this.Navigator = base.GetComponent<Navigator>();
			this.PrefabTag = base.GetComponent<KPrefabID>().PrefabTag;
			BabyMonitor.Def def2 = master.gameObject.GetDef<BabyMonitor.Def>();
			this.isBaby = def2 != null;
		}

		// Token: 0x06008C36 RID: 35894 RVA: 0x003553E4 File Offset: 0x003535E4
		public bool ShouldGoGetCaptured()
		{
			return this.targetCapturePoint != null && this.targetCapturePoint.IsRunning() && this.targetCapturePoint.shouldCreatureGoGetCaptured && (!this.isBaby || this.targetCapturePoint.def.allowBabies);
		}

		// Token: 0x04006B64 RID: 27492
		public FixedCapturePoint.Instance targetCapturePoint;

		// Token: 0x04006B65 RID: 27493
		public ChoreConsumer ChoreConsumer;

		// Token: 0x04006B66 RID: 27494
		public Navigator Navigator;

		// Token: 0x04006B67 RID: 27495
		public Tag PrefabTag;

		// Token: 0x04006B68 RID: 27496
		public bool isBaby;
	}
}
