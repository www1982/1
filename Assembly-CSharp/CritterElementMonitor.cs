using System;

// Token: 0x0200085C RID: 2140
public class CritterElementMonitor : GameStateMachine<CritterElementMonitor, CritterElementMonitor.Instance, IStateMachineTarget>
{
	// Token: 0x06003AC1 RID: 15041 RVA: 0x0014697A File Offset: 0x00144B7A
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.Update("UpdateInElement", delegate(CritterElementMonitor.Instance smi, float dt)
		{
			smi.UpdateCurrentElement(dt);
		}, UpdateRate.SIM_1000ms, false);
	}

	// Token: 0x020017E7 RID: 6119
	public new class Instance : GameStateMachine<CritterElementMonitor, CritterElementMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06009AC0 RID: 39616 RVA: 0x0038B208 File Offset: 0x00389408
		// (remove) Token: 0x06009AC1 RID: 39617 RVA: 0x0038B240 File Offset: 0x00389440
		public event Action<float> OnUpdateEggChances;

		// Token: 0x06009AC2 RID: 39618 RVA: 0x0038B275 File Offset: 0x00389475
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x06009AC3 RID: 39619 RVA: 0x0038B27E File Offset: 0x0038947E
		public void UpdateCurrentElement(float dt)
		{
			this.OnUpdateEggChances(dt);
		}
	}
}
