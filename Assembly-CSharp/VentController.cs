using System;
using UnityEngine;

// Token: 0x0200005C RID: 92
public class VentController : GameStateMachine<VentController, VentController.Instance>
{
	// Token: 0x060001B2 RID: 434 RVA: 0x0000C158 File Offset: 0x0000A358
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		this.root.EventHandler(GameHashes.VentAnimatingChanged, new GameStateMachine<VentController, VentController.Instance, IStateMachineTarget, object>.GameEvent.Callback(VentController.UpdateMeterColor)).EventTransition(GameHashes.VentClosed, this.closed, (VentController.Instance smi) => smi.GetComponent<Vent>().Closed()).EventTransition(GameHashes.VentOpen, this.off, (VentController.Instance smi) => !smi.GetComponent<Vent>().Closed());
		this.off.PlayAnim("off").EventTransition(GameHashes.VentAnimatingChanged, this.working_pre, new StateMachine<VentController, VentController.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(VentController.IsAnimating));
		this.working_pre.PlayAnim("working_pre").OnAnimQueueComplete(this.working_loop);
		this.working_loop.PlayAnim("working_loop", KAnim.PlayMode.Loop).Enter(new StateMachine<VentController, VentController.Instance, IStateMachineTarget, object>.State.Callback(VentController.PlayOutputMeterAnim)).EventTransition(GameHashes.VentAnimatingChanged, this.working_pst, GameStateMachine<VentController, VentController.Instance, IStateMachineTarget, object>.Not(new StateMachine<VentController, VentController.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(VentController.IsAnimating)));
		this.working_pst.PlayAnim("working_pst").OnAnimQueueComplete(this.off);
		this.closed.PlayAnim("closed").EventTransition(GameHashes.VentAnimatingChanged, this.working_pre, new StateMachine<VentController, VentController.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(VentController.IsAnimating));
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x0000C2BE File Offset: 0x0000A4BE
	public static void PlayOutputMeterAnim(VentController.Instance smi)
	{
		smi.PlayMeterAnim();
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x0000C2C6 File Offset: 0x0000A4C6
	public static bool IsAnimating(VentController.Instance smi)
	{
		return smi.exhaust.IsAnimating();
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x0000C2D4 File Offset: 0x0000A4D4
	public static void UpdateMeterColor(VentController.Instance smi, object data)
	{
		if (data != null)
		{
			Color32 color = (Color32)data;
			smi.SetMeterOutputColor(color);
		}
	}

	// Token: 0x04000117 RID: 279
	public GameStateMachine<VentController, VentController.Instance, IStateMachineTarget, object>.State off;

	// Token: 0x04000118 RID: 280
	public GameStateMachine<VentController, VentController.Instance, IStateMachineTarget, object>.State working_pre;

	// Token: 0x04000119 RID: 281
	public GameStateMachine<VentController, VentController.Instance, IStateMachineTarget, object>.State working_loop;

	// Token: 0x0400011A RID: 282
	public GameStateMachine<VentController, VentController.Instance, IStateMachineTarget, object>.State working_pst;

	// Token: 0x0400011B RID: 283
	public GameStateMachine<VentController, VentController.Instance, IStateMachineTarget, object>.State closed;

	// Token: 0x0400011C RID: 284
	public StateMachine<VentController, VentController.Instance, IStateMachineTarget, object>.BoolParameter isAnimating;

	// Token: 0x02001051 RID: 4177
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006042 RID: 24642
		public bool usingDynamicColor;

		// Token: 0x04006043 RID: 24643
		public string outputSubstanceAnimName;
	}

	// Token: 0x02001052 RID: 4178
	public new class Instance : GameStateMachine<VentController, VentController.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06007F99 RID: 32665 RVA: 0x0032C2EC File Offset: 0x0032A4EC
		public Instance(IStateMachineTarget master, VentController.Def def)
			: base(master, def)
		{
			if (def.usingDynamicColor)
			{
				this.outputSubstanceMeter = new MeterController(this.anim, "meter_target", def.outputSubstanceAnimName, Meter.Offset.NoChange, Grid.SceneLayer.Building, Array.Empty<string>());
			}
		}

		// Token: 0x06007F9A RID: 32666 RVA: 0x0032C322 File Offset: 0x0032A522
		public void PlayMeterAnim()
		{
			if (this.outputSubstanceMeter != null)
			{
				this.outputSubstanceMeter.meterController.Play(this.outputSubstanceMeter.meterController.initialAnim, KAnim.PlayMode.Loop, 1f, 0f);
			}
		}

		// Token: 0x06007F9B RID: 32667 RVA: 0x0032C35C File Offset: 0x0032A55C
		public void SetMeterOutputColor(Color32 color)
		{
			if (this.outputSubstanceMeter != null)
			{
				this.outputSubstanceMeter.meterController.TintColour = color;
			}
		}

		// Token: 0x04006044 RID: 24644
		[MyCmpGet]
		private KBatchedAnimController anim;

		// Token: 0x04006045 RID: 24645
		[MyCmpGet]
		public Exhaust exhaust;

		// Token: 0x04006046 RID: 24646
		private MeterController outputSubstanceMeter;
	}
}
