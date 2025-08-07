using System;
using UnityEngine;

// Token: 0x02000340 RID: 832
public class MorbRoverMakerKeepsake : GameStateMachine<MorbRoverMakerKeepsake, MorbRoverMakerKeepsake.Instance, IStateMachineTarget, MorbRoverMakerKeepsake.Def>
{
	// Token: 0x06001138 RID: 4408 RVA: 0x00064E88 File Offset: 0x00063088
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.silent;
		this.silent.PlayAnim("silent").Enter(new StateMachine<MorbRoverMakerKeepsake, MorbRoverMakerKeepsake.Instance, IStateMachineTarget, MorbRoverMakerKeepsake.Def>.State.Callback(MorbRoverMakerKeepsake.CalculateNextActivationTime)).Update(new Action<MorbRoverMakerKeepsake.Instance, float>(MorbRoverMakerKeepsake.TimerUpdate), UpdateRate.SIM_200ms, false);
		this.talking.PlayAnim("idle").OnAnimQueueComplete(this.silent);
	}

	// Token: 0x06001139 RID: 4409 RVA: 0x00064EF5 File Offset: 0x000630F5
	public static void CalculateNextActivationTime(MorbRoverMakerKeepsake.Instance smi)
	{
		smi.CalculateNextActivationTime();
	}

	// Token: 0x0600113A RID: 4410 RVA: 0x00064EFD File Offset: 0x000630FD
	public static void TimerUpdate(MorbRoverMakerKeepsake.Instance smi, float dt)
	{
		if (GameClock.Instance.GetTime() > smi.NextActivationTime)
		{
			smi.GoTo(smi.sm.talking);
		}
	}

	// Token: 0x04000AE0 RID: 2784
	public const string SILENT_ANIMATION_NAME = "silent";

	// Token: 0x04000AE1 RID: 2785
	public const string TALKING_ANIMATION_NAME = "idle";

	// Token: 0x04000AE2 RID: 2786
	public GameStateMachine<MorbRoverMakerKeepsake, MorbRoverMakerKeepsake.Instance, IStateMachineTarget, MorbRoverMakerKeepsake.Def>.State silent;

	// Token: 0x04000AE3 RID: 2787
	public GameStateMachine<MorbRoverMakerKeepsake, MorbRoverMakerKeepsake.Instance, IStateMachineTarget, MorbRoverMakerKeepsake.Def>.State talking;

	// Token: 0x020011EC RID: 4588
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040064AE RID: 25774
		public Vector2 OperationalRandomnessRange = new Vector2(120f, 600f);
	}

	// Token: 0x020011ED RID: 4589
	public new class Instance : GameStateMachine<MorbRoverMakerKeepsake, MorbRoverMakerKeepsake.Instance, IStateMachineTarget, MorbRoverMakerKeepsake.Def>.GameInstance
	{
		// Token: 0x0600846C RID: 33900 RVA: 0x00335AD9 File Offset: 0x00333CD9
		public Instance(IStateMachineTarget master, MorbRoverMakerKeepsake.Def def)
			: base(master, def)
		{
		}

		// Token: 0x0600846D RID: 33901 RVA: 0x00335AF0 File Offset: 0x00333CF0
		public void CalculateNextActivationTime()
		{
			float time = GameClock.Instance.GetTime();
			float num = time + base.def.OperationalRandomnessRange.x;
			float num2 = time + base.def.OperationalRandomnessRange.y;
			this.NextActivationTime = global::UnityEngine.Random.Range(num, num2);
		}

		// Token: 0x040064AF RID: 25775
		public float NextActivationTime = -1f;
	}
}
