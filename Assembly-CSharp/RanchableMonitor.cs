using System;

// Token: 0x020005A0 RID: 1440
public class RanchableMonitor : GameStateMachine<RanchableMonitor, RanchableMonitor.Instance, IStateMachineTarget, RanchableMonitor.Def>
{
	// Token: 0x060020E1 RID: 8417 RVA: 0x000BDAE5 File Offset: 0x000BBCE5
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleBehaviour(GameTags.Creatures.WantsToGetRanched, (RanchableMonitor.Instance smi) => smi.ShouldGoGetRanched(), null);
	}

	// Token: 0x02001423 RID: 5155
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001424 RID: 5156
	public new class Instance : GameStateMachine<RanchableMonitor, RanchableMonitor.Instance, IStateMachineTarget, RanchableMonitor.Def>.GameInstance
	{
		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06008CA2 RID: 36002 RVA: 0x003567CC File Offset: 0x003549CC
		// (set) Token: 0x06008CA3 RID: 36003 RVA: 0x003567D4 File Offset: 0x003549D4
		public ChoreConsumer ChoreConsumer { get; private set; }

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06008CA4 RID: 36004 RVA: 0x003567DD File Offset: 0x003549DD
		public Navigator NavComponent
		{
			get
			{
				return this.navComponent;
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06008CA5 RID: 36005 RVA: 0x003567E5 File Offset: 0x003549E5
		public RanchedStates.Instance States
		{
			get
			{
				if (this.states == null)
				{
					this.states = this.controller.GetSMI<RanchedStates.Instance>();
				}
				return this.states;
			}
		}

		// Token: 0x06008CA6 RID: 36006 RVA: 0x00356806 File Offset: 0x00354A06
		public Instance(IStateMachineTarget master, RanchableMonitor.Def def)
			: base(master, def)
		{
			this.ChoreConsumer = base.GetComponent<ChoreConsumer>();
			this.navComponent = base.GetComponent<Navigator>();
		}

		// Token: 0x06008CA7 RID: 36007 RVA: 0x00356828 File Offset: 0x00354A28
		public bool ShouldGoGetRanched()
		{
			return this.TargetRanchStation != null && this.TargetRanchStation.IsRunning() && this.TargetRanchStation.IsRancherReady;
		}

		// Token: 0x04006BC4 RID: 27588
		public RanchStation.Instance TargetRanchStation;

		// Token: 0x04006BC5 RID: 27589
		private Navigator navComponent;

		// Token: 0x04006BC6 RID: 27590
		private RanchedStates.Instance states;
	}
}
