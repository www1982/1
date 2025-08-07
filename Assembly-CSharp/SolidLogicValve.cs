using System;
using KSerialization;

// Token: 0x020007C9 RID: 1993
[SerializationConfig(MemberSerialization.OptIn)]
public class SolidLogicValve : StateMachineComponent<SolidLogicValve.StatesInstance>
{
	// Token: 0x0600354D RID: 13645 RVA: 0x0012A4EC File Offset: 0x001286EC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x0600354E RID: 13646 RVA: 0x0012A4F4 File Offset: 0x001286F4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x0600354F RID: 13647 RVA: 0x0012A507 File Offset: 0x00128707
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x04002028 RID: 8232
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04002029 RID: 8233
	[MyCmpReq]
	private SolidConduitBridge bridge;

	// Token: 0x020016FF RID: 5887
	public class States : GameStateMachine<SolidLogicValve.States, SolidLogicValve.StatesInstance, SolidLogicValve>
	{
		// Token: 0x06009758 RID: 38744 RVA: 0x0037BC1C File Offset: 0x00379E1C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.off;
			this.root.DoNothing();
			this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.on, (SolidLogicValve.StatesInstance smi) => smi.GetComponent<Operational>().IsOperational).Enter(delegate(SolidLogicValve.StatesInstance smi)
			{
				smi.GetComponent<Operational>().SetActive(false, false);
			});
			this.on.DefaultState(this.on.idle).EventTransition(GameHashes.OperationalChanged, this.off, (SolidLogicValve.StatesInstance smi) => !smi.GetComponent<Operational>().IsOperational).Enter(delegate(SolidLogicValve.StatesInstance smi)
			{
				smi.GetComponent<Operational>().SetActive(true, false);
			});
			this.on.idle.PlayAnim("on").Transition(this.on.working, (SolidLogicValve.StatesInstance smi) => smi.IsDispensing(), UpdateRate.SIM_200ms);
			this.on.working.PlayAnim("on_flow", KAnim.PlayMode.Loop).Transition(this.on.idle, (SolidLogicValve.StatesInstance smi) => !smi.IsDispensing(), UpdateRate.SIM_200ms);
		}

		// Token: 0x04007459 RID: 29785
		public GameStateMachine<SolidLogicValve.States, SolidLogicValve.StatesInstance, SolidLogicValve, object>.State off;

		// Token: 0x0400745A RID: 29786
		public SolidLogicValve.States.ReadyStates on;

		// Token: 0x020027DA RID: 10202
		public class ReadyStates : GameStateMachine<SolidLogicValve.States, SolidLogicValve.StatesInstance, SolidLogicValve, object>.State
		{
			// Token: 0x0400B0B0 RID: 45232
			public GameStateMachine<SolidLogicValve.States, SolidLogicValve.StatesInstance, SolidLogicValve, object>.State idle;

			// Token: 0x0400B0B1 RID: 45233
			public GameStateMachine<SolidLogicValve.States, SolidLogicValve.StatesInstance, SolidLogicValve, object>.State working;
		}
	}

	// Token: 0x02001700 RID: 5888
	public class StatesInstance : GameStateMachine<SolidLogicValve.States, SolidLogicValve.StatesInstance, SolidLogicValve, object>.GameInstance
	{
		// Token: 0x0600975A RID: 38746 RVA: 0x0037BDA0 File Offset: 0x00379FA0
		public StatesInstance(SolidLogicValve master)
			: base(master)
		{
		}

		// Token: 0x0600975B RID: 38747 RVA: 0x0037BDA9 File Offset: 0x00379FA9
		public bool IsDispensing()
		{
			return base.master.bridge.IsDispensing;
		}
	}
}
