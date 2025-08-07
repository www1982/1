using System;
using Klei.AI;
using UnityEngine;

// Token: 0x02000A11 RID: 2577
public class SteppedInMonitor : GameStateMachine<SteppedInMonitor, SteppedInMonitor.Instance>
{
	// Token: 0x06004AFF RID: 19199 RVA: 0x001B2E64 File Offset: 0x001B1064
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.satisfied.Transition(this.carpetedFloor, new StateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(SteppedInMonitor.IsOnCarpet), UpdateRate.SIM_200ms).Transition(this.wetFloor, new StateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(SteppedInMonitor.IsFloorWet), UpdateRate.SIM_200ms).Transition(this.wetBody, new StateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(SteppedInMonitor.IsSubmerged), UpdateRate.SIM_200ms);
		this.carpetedFloor.Enter(new StateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.State.Callback(SteppedInMonitor.GetCarpetFeet)).ToggleExpression(Db.Get().Expressions.Tickled, null).Update(new Action<SteppedInMonitor.Instance, float>(SteppedInMonitor.GetCarpetFeet), UpdateRate.SIM_1000ms, false)
			.Transition(this.satisfied, GameStateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.Not(new StateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(SteppedInMonitor.IsOnCarpet)), UpdateRate.SIM_200ms)
			.Transition(this.wetFloor, new StateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(SteppedInMonitor.IsFloorWet), UpdateRate.SIM_200ms)
			.Transition(this.wetBody, new StateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(SteppedInMonitor.IsSubmerged), UpdateRate.SIM_200ms);
		this.wetFloor.Enter(new StateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.State.Callback(SteppedInMonitor.GetWetFeet)).Update(new Action<SteppedInMonitor.Instance, float>(SteppedInMonitor.GetWetFeet), UpdateRate.SIM_1000ms, false).Transition(this.satisfied, GameStateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.Not(new StateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(SteppedInMonitor.IsFloorWet)), UpdateRate.SIM_200ms)
			.Transition(this.wetBody, new StateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(SteppedInMonitor.IsSubmerged), UpdateRate.SIM_200ms);
		this.wetBody.Enter(new StateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.State.Callback(SteppedInMonitor.GetSoaked)).Update(new Action<SteppedInMonitor.Instance, float>(SteppedInMonitor.GetSoaked), UpdateRate.SIM_1000ms, false).Transition(this.wetFloor, GameStateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.Not(new StateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(SteppedInMonitor.IsSubmerged)), UpdateRate.SIM_200ms);
	}

	// Token: 0x06004B00 RID: 19200 RVA: 0x001B2FFD File Offset: 0x001B11FD
	private static void GetCarpetFeet(SteppedInMonitor.Instance smi, float dt)
	{
		SteppedInMonitor.GetCarpetFeet(smi);
	}

	// Token: 0x06004B01 RID: 19201 RVA: 0x001B3008 File Offset: 0x001B1208
	private static void GetCarpetFeet(SteppedInMonitor.Instance smi)
	{
		if (!smi.effects.HasEffect("SoakingWet") && !smi.effects.HasEffect("WetFeet") && smi.IsEffectAllowed("CarpetFeet"))
		{
			smi.effects.Add("CarpetFeet", true);
		}
	}

	// Token: 0x06004B02 RID: 19202 RVA: 0x001B3058 File Offset: 0x001B1258
	private static void GetWetFeet(SteppedInMonitor.Instance smi, float dt)
	{
		SteppedInMonitor.GetWetFeet(smi);
	}

	// Token: 0x06004B03 RID: 19203 RVA: 0x001B3060 File Offset: 0x001B1260
	private static void GetWetFeet(SteppedInMonitor.Instance smi)
	{
		if (!smi.effects.HasEffect("SoakingWet") && smi.IsEffectAllowed("WetFeet"))
		{
			smi.effects.Add("WetFeet", true);
		}
	}

	// Token: 0x06004B04 RID: 19204 RVA: 0x001B3093 File Offset: 0x001B1293
	private static void GetSoaked(SteppedInMonitor.Instance smi, float dt)
	{
		SteppedInMonitor.GetSoaked(smi);
	}

	// Token: 0x06004B05 RID: 19205 RVA: 0x001B309C File Offset: 0x001B129C
	private static void GetSoaked(SteppedInMonitor.Instance smi)
	{
		if (smi.effects.HasEffect("WetFeet"))
		{
			smi.effects.Remove("WetFeet");
		}
		if (smi.IsEffectAllowed("SoakingWet"))
		{
			smi.effects.Add("SoakingWet", true);
		}
	}

	// Token: 0x06004B06 RID: 19206 RVA: 0x001B30EC File Offset: 0x001B12EC
	private static bool IsOnCarpet(SteppedInMonitor.Instance smi)
	{
		int num = Grid.CellBelow(Grid.PosToCell(smi));
		if (!Grid.IsValidCell(num))
		{
			return false;
		}
		GameObject gameObject = Grid.Objects[num, 9];
		return Grid.IsValidCell(num) && gameObject != null && gameObject.HasTag(GameTags.Carpeted);
	}

	// Token: 0x06004B07 RID: 19207 RVA: 0x001B313C File Offset: 0x001B133C
	private static bool IsFloorWet(SteppedInMonitor.Instance smi)
	{
		int num = Grid.PosToCell(smi);
		return Grid.IsValidCell(num) && Grid.Element[num].IsLiquid;
	}

	// Token: 0x06004B08 RID: 19208 RVA: 0x001B3168 File Offset: 0x001B1368
	private static bool IsSubmerged(SteppedInMonitor.Instance smi)
	{
		int num = Grid.CellAbove(Grid.PosToCell(smi));
		return Grid.IsValidCell(num) && Grid.Element[num].IsLiquid;
	}

	// Token: 0x040031AF RID: 12719
	public const string CARPET_EFFECT_NAME = "CarpetFeet";

	// Token: 0x040031B0 RID: 12720
	public const string WET_FEET_EFFECT_NAME = "WetFeet";

	// Token: 0x040031B1 RID: 12721
	public const string SOAK_EFFECT_NAME = "SoakingWet";

	// Token: 0x040031B2 RID: 12722
	public GameStateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.State satisfied;

	// Token: 0x040031B3 RID: 12723
	public GameStateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.State carpetedFloor;

	// Token: 0x040031B4 RID: 12724
	public GameStateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.State wetFloor;

	// Token: 0x040031B5 RID: 12725
	public GameStateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.State wetBody;

	// Token: 0x02001AAA RID: 6826
	public new class Instance : GameStateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x0600A47F RID: 42111 RVA: 0x003A677B File Offset: 0x003A497B
		// (set) Token: 0x0600A47E RID: 42110 RVA: 0x003A6772 File Offset: 0x003A4972
		public string[] effectsAllowed { get; private set; }

		// Token: 0x0600A480 RID: 42112 RVA: 0x003A6783 File Offset: 0x003A4983
		public Instance(IStateMachineTarget master)
			: this(master, new string[] { "CarpetFeet", "WetFeet", "SoakingWet" })
		{
		}

		// Token: 0x0600A481 RID: 42113 RVA: 0x003A67AA File Offset: 0x003A49AA
		public Instance(IStateMachineTarget master, string[] effectsAllowed)
			: base(master)
		{
			this.effects = base.GetComponent<Effects>();
			this.effectsAllowed = effectsAllowed;
		}

		// Token: 0x0600A482 RID: 42114 RVA: 0x003A67C8 File Offset: 0x003A49C8
		public bool IsEffectAllowed(string effectName)
		{
			if (this.effectsAllowed == null || this.effectsAllowed.Length == 0)
			{
				return false;
			}
			for (int i = 0; i < this.effectsAllowed.Length; i++)
			{
				if (this.effectsAllowed[i] == effectName)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400806C RID: 32876
		public Effects effects;
	}
}
