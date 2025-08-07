using System;

// Token: 0x02000BB7 RID: 2999
[SkipSaveFileSerialization]
public class Thriver : StateMachineComponent<Thriver.StatesInstance>
{
	// Token: 0x060059B5 RID: 22965 RVA: 0x0020696B File Offset: 0x00204B6B
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x02001CE8 RID: 7400
	public class StatesInstance : GameStateMachine<Thriver.States, Thriver.StatesInstance, Thriver, object>.GameInstance
	{
		// Token: 0x0600AC85 RID: 44165 RVA: 0x003C242C File Offset: 0x003C062C
		public StatesInstance(Thriver master)
			: base(master)
		{
		}

		// Token: 0x0600AC86 RID: 44166 RVA: 0x003C2438 File Offset: 0x003C0638
		public bool IsStressed()
		{
			StressMonitor.Instance smi = base.master.GetSMI<StressMonitor.Instance>();
			return smi != null && smi.IsStressed();
		}
	}

	// Token: 0x02001CE9 RID: 7401
	public class States : GameStateMachine<Thriver.States, Thriver.StatesInstance, Thriver>
	{
		// Token: 0x0600AC87 RID: 44167 RVA: 0x003C245C File Offset: 0x003C065C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.root.EventTransition(GameHashes.NotStressed, this.idle, null).EventTransition(GameHashes.Stressed, this.stressed, null).EventTransition(GameHashes.StressedHadEnough, this.stressed, null)
				.Enter(delegate(Thriver.StatesInstance smi)
				{
					StressMonitor.Instance smi2 = smi.master.GetSMI<StressMonitor.Instance>();
					if (smi2 != null && smi2.IsStressed())
					{
						smi.GoTo(this.stressed);
					}
				});
			this.idle.DoNothing();
			this.stressed.ToggleEffect("Thriver");
			this.toostressed.DoNothing();
		}

		// Token: 0x040087A4 RID: 34724
		public GameStateMachine<Thriver.States, Thriver.StatesInstance, Thriver, object>.State idle;

		// Token: 0x040087A5 RID: 34725
		public GameStateMachine<Thriver.States, Thriver.StatesInstance, Thriver, object>.State stressed;

		// Token: 0x040087A6 RID: 34726
		public GameStateMachine<Thriver.States, Thriver.StatesInstance, Thriver, object>.State toostressed;
	}
}
