using System;
using UnityEngine;

// Token: 0x0200099A RID: 2458
public class JetSuitMonitor : GameStateMachine<JetSuitMonitor, JetSuitMonitor.Instance>
{
	// Token: 0x06004749 RID: 18249 RVA: 0x0019B014 File Offset: 0x00199214
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		base.Target(this.owner);
		this.off.EventTransition(GameHashes.PathAdvanced, this.flying, new StateMachine<JetSuitMonitor, JetSuitMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(JetSuitMonitor.ShouldStartFlying));
		this.flying.Enter(new StateMachine<JetSuitMonitor, JetSuitMonitor.Instance, IStateMachineTarget, object>.State.Callback(JetSuitMonitor.StartFlying)).Exit(new StateMachine<JetSuitMonitor, JetSuitMonitor.Instance, IStateMachineTarget, object>.State.Callback(JetSuitMonitor.StopFlying)).EventTransition(GameHashes.PathAdvanced, this.off, new StateMachine<JetSuitMonitor, JetSuitMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(JetSuitMonitor.ShouldStopFlying))
			.Update(new Action<JetSuitMonitor.Instance, float>(JetSuitMonitor.Emit), UpdateRate.SIM_200ms, false);
	}

	// Token: 0x0600474A RID: 18250 RVA: 0x0019B0B0 File Offset: 0x001992B0
	public static bool ShouldStartFlying(JetSuitMonitor.Instance smi)
	{
		return smi.navigator && smi.navigator.CurrentNavType == NavType.Hover;
	}

	// Token: 0x0600474B RID: 18251 RVA: 0x0019B0CF File Offset: 0x001992CF
	public static bool ShouldStopFlying(JetSuitMonitor.Instance smi)
	{
		return !smi.navigator || smi.navigator.CurrentNavType != NavType.Hover;
	}

	// Token: 0x0600474C RID: 18252 RVA: 0x0019B0F1 File Offset: 0x001992F1
	public static void StartFlying(JetSuitMonitor.Instance smi)
	{
	}

	// Token: 0x0600474D RID: 18253 RVA: 0x0019B0F3 File Offset: 0x001992F3
	public static void StopFlying(JetSuitMonitor.Instance smi)
	{
	}

	// Token: 0x0600474E RID: 18254 RVA: 0x0019B0F8 File Offset: 0x001992F8
	public static void Emit(JetSuitMonitor.Instance smi, float dt)
	{
		if (!smi.navigator)
		{
			return;
		}
		GameObject gameObject = smi.sm.owner.Get(smi);
		if (!gameObject)
		{
			return;
		}
		int num = Grid.PosToCell(gameObject.transform.GetPosition());
		float num2 = 0.1f * dt;
		num2 = Mathf.Min(num2, smi.jet_suit_tank.amount);
		smi.jet_suit_tank.amount -= num2;
		float num3 = num2 * 3f;
		if (num3 > 1E-45f)
		{
			SimMessages.AddRemoveSubstance(num, SimHashes.CarbonDioxide, CellEventLogger.Instance.ElementConsumerSimUpdate, num3, 473.15f, byte.MaxValue, 0, true, -1);
		}
		if (smi.jet_suit_tank.amount == 0f)
		{
			smi.navigator.AddTag(GameTags.JetSuitOutOfFuel);
			smi.navigator.SetCurrentNavType(NavType.Floor);
		}
	}

	// Token: 0x04002F22 RID: 12066
	public GameStateMachine<JetSuitMonitor, JetSuitMonitor.Instance, IStateMachineTarget, object>.State off;

	// Token: 0x04002F23 RID: 12067
	public GameStateMachine<JetSuitMonitor, JetSuitMonitor.Instance, IStateMachineTarget, object>.State flying;

	// Token: 0x04002F24 RID: 12068
	public StateMachine<JetSuitMonitor, JetSuitMonitor.Instance, IStateMachineTarget, object>.TargetParameter owner;

	// Token: 0x0200199D RID: 6557
	public new class Instance : GameStateMachine<JetSuitMonitor, JetSuitMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600A000 RID: 40960 RVA: 0x0039A9EE File Offset: 0x00398BEE
		public Instance(IStateMachineTarget master, GameObject owner)
			: base(master)
		{
			base.sm.owner.Set(owner, base.smi, false);
			this.navigator = owner.GetComponent<Navigator>();
			this.jet_suit_tank = master.GetComponent<JetSuitTank>();
		}

		// Token: 0x04007CFB RID: 31995
		public Navigator navigator;

		// Token: 0x04007CFC RID: 31996
		public JetSuitTank jet_suit_tank;
	}
}
