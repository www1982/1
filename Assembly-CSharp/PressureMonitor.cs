using System;
using Klei.AI;

// Token: 0x02000A00 RID: 2560
public class PressureMonitor : GameStateMachine<PressureMonitor, PressureMonitor.Instance, IStateMachineTarget, PressureMonitor.Def>
{
	// Token: 0x06004AA8 RID: 19112 RVA: 0x001B058C File Offset: 0x001AE78C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.safe;
		this.safe.Transition(this.inPressure, new StateMachine<PressureMonitor, PressureMonitor.Instance, IStateMachineTarget, PressureMonitor.Def>.Transition.ConditionCallback(PressureMonitor.IsInPressureGas), UpdateRate.SIM_200ms);
		this.inPressure.Transition(this.safe, GameStateMachine<PressureMonitor, PressureMonitor.Instance, IStateMachineTarget, PressureMonitor.Def>.Not(new StateMachine<PressureMonitor, PressureMonitor.Instance, IStateMachineTarget, PressureMonitor.Def>.Transition.ConditionCallback(PressureMonitor.IsInPressureGas)), UpdateRate.SIM_200ms).DefaultState(this.inPressure.idle);
		this.inPressure.idle.EventTransition(GameHashes.EffectImmunityAdded, this.inPressure.immune, new StateMachine<PressureMonitor, PressureMonitor.Instance, IStateMachineTarget, PressureMonitor.Def>.Transition.ConditionCallback(PressureMonitor.IsImmuneToPressure)).Update(new Action<PressureMonitor.Instance, float>(PressureMonitor.HighPressureUpdate), UpdateRate.SIM_200ms, false);
		this.inPressure.immune.EventTransition(GameHashes.EffectImmunityRemoved, this.inPressure.idle, GameStateMachine<PressureMonitor, PressureMonitor.Instance, IStateMachineTarget, PressureMonitor.Def>.Not(new StateMachine<PressureMonitor, PressureMonitor.Instance, IStateMachineTarget, PressureMonitor.Def>.Transition.ConditionCallback(PressureMonitor.IsImmuneToPressure)));
	}

	// Token: 0x06004AA9 RID: 19113 RVA: 0x001B066D File Offset: 0x001AE86D
	public static bool IsInPressureGas(PressureMonitor.Instance smi)
	{
		return smi.IsInHighPressure();
	}

	// Token: 0x06004AAA RID: 19114 RVA: 0x001B0675 File Offset: 0x001AE875
	public static bool IsImmuneToPressure(PressureMonitor.Instance smi)
	{
		return smi.IsImmuneToHighPressure();
	}

	// Token: 0x06004AAB RID: 19115 RVA: 0x001B067D File Offset: 0x001AE87D
	public static void RemoveOverpressureEffect(PressureMonitor.Instance smi)
	{
		smi.RemoveEffect();
	}

	// Token: 0x06004AAC RID: 19116 RVA: 0x001B0685 File Offset: 0x001AE885
	public static void HighPressureUpdate(PressureMonitor.Instance smi, float dt)
	{
		if (smi.timeinstate > 3f)
		{
			smi.AddEffect();
		}
	}

	// Token: 0x0400314B RID: 12619
	public const string OVER_PRESSURE_EFFECT_NAME = "PoppedEarDrums";

	// Token: 0x0400314C RID: 12620
	public const float TIME_IN_PRESSURE_BEFORE_EAR_POPS = 3f;

	// Token: 0x0400314D RID: 12621
	private static CellOffset[] PRESSURE_TEST_OFFSET = new CellOffset[]
	{
		new CellOffset(0, 0),
		new CellOffset(0, 1)
	};

	// Token: 0x0400314E RID: 12622
	public GameStateMachine<PressureMonitor, PressureMonitor.Instance, IStateMachineTarget, PressureMonitor.Def>.State safe;

	// Token: 0x0400314F RID: 12623
	public PressureMonitor.PressureStates inPressure;

	// Token: 0x02001A7C RID: 6780
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001A7D RID: 6781
	public class PressureStates : GameStateMachine<PressureMonitor, PressureMonitor.Instance, IStateMachineTarget, PressureMonitor.Def>.State
	{
		// Token: 0x04007FDC RID: 32732
		public GameStateMachine<PressureMonitor, PressureMonitor.Instance, IStateMachineTarget, PressureMonitor.Def>.State idle;

		// Token: 0x04007FDD RID: 32733
		public GameStateMachine<PressureMonitor, PressureMonitor.Instance, IStateMachineTarget, PressureMonitor.Def>.State immune;
	}

	// Token: 0x02001A7E RID: 6782
	public new class Instance : GameStateMachine<PressureMonitor, PressureMonitor.Instance, IStateMachineTarget, PressureMonitor.Def>.GameInstance
	{
		// Token: 0x0600A3B9 RID: 41913 RVA: 0x003A486F File Offset: 0x003A2A6F
		public Instance(IStateMachineTarget master, PressureMonitor.Def def)
			: base(master, def)
		{
			this.effects = base.GetComponent<Effects>();
		}

		// Token: 0x0600A3BA RID: 41914 RVA: 0x003A4885 File Offset: 0x003A2A85
		public bool IsImmuneToHighPressure()
		{
			return this.effects.HasImmunityTo(Db.Get().effects.Get("PoppedEarDrums"));
		}

		// Token: 0x0600A3BB RID: 41915 RVA: 0x003A48A8 File Offset: 0x003A2AA8
		public bool IsInHighPressure()
		{
			int num = Grid.PosToCell(base.gameObject);
			for (int i = 0; i < PressureMonitor.PRESSURE_TEST_OFFSET.Length; i++)
			{
				int num2 = Grid.OffsetCell(num, PressureMonitor.PRESSURE_TEST_OFFSET[i]);
				if (Grid.IsValidCell(num2) && Grid.Element[num2].IsGas && Grid.Mass[num2] > 4f)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600A3BC RID: 41916 RVA: 0x003A4910 File Offset: 0x003A2B10
		public void RemoveEffect()
		{
			this.effects.Remove("PoppedEarDrums");
		}

		// Token: 0x0600A3BD RID: 41917 RVA: 0x003A4922 File Offset: 0x003A2B22
		public void AddEffect()
		{
			this.effects.Add("PoppedEarDrums", true);
		}

		// Token: 0x04007FDE RID: 32734
		private Effects effects;
	}
}
