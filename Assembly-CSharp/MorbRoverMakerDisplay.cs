using System;

// Token: 0x0200033F RID: 831
public class MorbRoverMakerDisplay : GameStateMachine<MorbRoverMakerDisplay, MorbRoverMakerDisplay.Instance, IStateMachineTarget, MorbRoverMakerDisplay.Def>
{
	// Token: 0x06001132 RID: 4402 RVA: 0x00064C30 File Offset: 0x00062E30
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.Never;
		default_state = this.off.idle;
		this.root.Target(this.monitor);
		this.off.DefaultState(this.off.idle);
		this.off.entering.PlayAnim("display_off").OnAnimQueueComplete(this.off.idle);
		this.off.idle.Target(this.masterTarget).EventTransition(GameHashes.TagsChanged, this.off.exiting, new StateMachine<MorbRoverMakerDisplay, MorbRoverMakerDisplay.Instance, IStateMachineTarget, MorbRoverMakerDisplay.Def>.Transition.ConditionCallback(MorbRoverMakerDisplay.ShouldBeOn)).Target(this.monitor)
			.PlayAnim("display_off_idle", KAnim.PlayMode.Loop);
		this.off.exiting.PlayAnim("display_on").OnAnimQueueComplete(this.on);
		this.on.Target(this.masterTarget).TagTransition(GameTags.Operational, this.off.entering, true).Target(this.monitor)
			.DefaultState(this.on.idle);
		this.on.idle.Transition(this.on.germ, new StateMachine<MorbRoverMakerDisplay, MorbRoverMakerDisplay.Instance, IStateMachineTarget, MorbRoverMakerDisplay.Def>.Transition.ConditionCallback(MorbRoverMakerDisplay.HasGermsAddedAndGermsAreNeeded), UpdateRate.SIM_200ms).Transition(this.on.noGerm, new StateMachine<MorbRoverMakerDisplay, MorbRoverMakerDisplay.Instance, IStateMachineTarget, MorbRoverMakerDisplay.Def>.Transition.ConditionCallback(MorbRoverMakerDisplay.NoGermsAddedAndGermsAreNeeded), UpdateRate.SIM_200ms).PlayAnim("display_idle", KAnim.PlayMode.Loop);
		this.on.noGerm.Transition(this.on.idle, new StateMachine<MorbRoverMakerDisplay, MorbRoverMakerDisplay.Instance, IStateMachineTarget, MorbRoverMakerDisplay.Def>.Transition.ConditionCallback(MorbRoverMakerDisplay.GermsNoLongerNeeded), UpdateRate.SIM_200ms).Transition(this.on.germ, new StateMachine<MorbRoverMakerDisplay, MorbRoverMakerDisplay.Instance, IStateMachineTarget, MorbRoverMakerDisplay.Def>.Transition.ConditionCallback(MorbRoverMakerDisplay.HasGermsAddedAndGermsAreNeeded), UpdateRate.SIM_200ms).PlayAnim("display_no_germ", KAnim.PlayMode.Loop);
		this.on.germ.Transition(this.on.idle, new StateMachine<MorbRoverMakerDisplay, MorbRoverMakerDisplay.Instance, IStateMachineTarget, MorbRoverMakerDisplay.Def>.Transition.ConditionCallback(MorbRoverMakerDisplay.GermsNoLongerNeeded), UpdateRate.SIM_200ms).Transition(this.on.noGerm, new StateMachine<MorbRoverMakerDisplay, MorbRoverMakerDisplay.Instance, IStateMachineTarget, MorbRoverMakerDisplay.Def>.Transition.ConditionCallback(MorbRoverMakerDisplay.NoGermsAddedAndGermsAreNeeded), UpdateRate.SIM_200ms).PlayAnim("display_germ", KAnim.PlayMode.Loop);
	}

	// Token: 0x06001133 RID: 4403 RVA: 0x00064E45 File Offset: 0x00063045
	public static bool NoGermsAddedAndGermsAreNeeded(MorbRoverMakerDisplay.Instance smi)
	{
		return smi.GermsAreNeeded && !smi.HasRecentlyConsumedGerms;
	}

	// Token: 0x06001134 RID: 4404 RVA: 0x00064E5A File Offset: 0x0006305A
	public static bool HasGermsAddedAndGermsAreNeeded(MorbRoverMakerDisplay.Instance smi)
	{
		return smi.GermsAreNeeded && smi.HasRecentlyConsumedGerms;
	}

	// Token: 0x06001135 RID: 4405 RVA: 0x00064E6C File Offset: 0x0006306C
	public static bool ShouldBeOn(MorbRoverMakerDisplay.Instance smi)
	{
		return smi.ShouldBeOn();
	}

	// Token: 0x06001136 RID: 4406 RVA: 0x00064E74 File Offset: 0x00063074
	public static bool GermsNoLongerNeeded(MorbRoverMakerDisplay.Instance smi)
	{
		return !smi.GermsAreNeeded;
	}

	// Token: 0x04000AD6 RID: 2774
	public const string METER_TARGET_NAME = "meter_display_target";

	// Token: 0x04000AD7 RID: 2775
	public const string OFF_IDLE_ANIM_NAME = "display_off_idle";

	// Token: 0x04000AD8 RID: 2776
	public const string OFF_ENTERING_ANIM_NAME = "display_off";

	// Token: 0x04000AD9 RID: 2777
	public const string OFF_EXITING_ANIM_NAME = "display_on";

	// Token: 0x04000ADA RID: 2778
	public const string GERM_ICON_ANIM_NAME = "display_germ";

	// Token: 0x04000ADB RID: 2779
	public const string NO_GERM_ANIM_NAME = "display_no_germ";

	// Token: 0x04000ADC RID: 2780
	public const string ON_IDLE_ANIM_NAME = "display_idle";

	// Token: 0x04000ADD RID: 2781
	public StateMachine<MorbRoverMakerDisplay, MorbRoverMakerDisplay.Instance, IStateMachineTarget, MorbRoverMakerDisplay.Def>.TargetParameter monitor;

	// Token: 0x04000ADE RID: 2782
	public MorbRoverMakerDisplay.OffStates off;

	// Token: 0x04000ADF RID: 2783
	public MorbRoverMakerDisplay.OnStates on;

	// Token: 0x020011E8 RID: 4584
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040064A1 RID: 25761
		public float Timeout = 1f;
	}

	// Token: 0x020011E9 RID: 4585
	public class OffStates : GameStateMachine<MorbRoverMakerDisplay, MorbRoverMakerDisplay.Instance, IStateMachineTarget, MorbRoverMakerDisplay.Def>.State
	{
		// Token: 0x040064A2 RID: 25762
		public GameStateMachine<MorbRoverMakerDisplay, MorbRoverMakerDisplay.Instance, IStateMachineTarget, MorbRoverMakerDisplay.Def>.State entering;

		// Token: 0x040064A3 RID: 25763
		public GameStateMachine<MorbRoverMakerDisplay, MorbRoverMakerDisplay.Instance, IStateMachineTarget, MorbRoverMakerDisplay.Def>.State idle;

		// Token: 0x040064A4 RID: 25764
		public GameStateMachine<MorbRoverMakerDisplay, MorbRoverMakerDisplay.Instance, IStateMachineTarget, MorbRoverMakerDisplay.Def>.State exiting;
	}

	// Token: 0x020011EA RID: 4586
	public class OnStates : GameStateMachine<MorbRoverMakerDisplay, MorbRoverMakerDisplay.Instance, IStateMachineTarget, MorbRoverMakerDisplay.Def>.State
	{
		// Token: 0x040064A5 RID: 25765
		public GameStateMachine<MorbRoverMakerDisplay, MorbRoverMakerDisplay.Instance, IStateMachineTarget, MorbRoverMakerDisplay.Def>.State idle;

		// Token: 0x040064A6 RID: 25766
		public GameStateMachine<MorbRoverMakerDisplay, MorbRoverMakerDisplay.Instance, IStateMachineTarget, MorbRoverMakerDisplay.Def>.State shake;

		// Token: 0x040064A7 RID: 25767
		public GameStateMachine<MorbRoverMakerDisplay, MorbRoverMakerDisplay.Instance, IStateMachineTarget, MorbRoverMakerDisplay.Def>.State noGerm;

		// Token: 0x040064A8 RID: 25768
		public GameStateMachine<MorbRoverMakerDisplay, MorbRoverMakerDisplay.Instance, IStateMachineTarget, MorbRoverMakerDisplay.Def>.State germ;

		// Token: 0x040064A9 RID: 25769
		public GameStateMachine<MorbRoverMakerDisplay, MorbRoverMakerDisplay.Instance, IStateMachineTarget, MorbRoverMakerDisplay.Def>.State checkmark;
	}

	// Token: 0x020011EB RID: 4587
	public new class Instance : GameStateMachine<MorbRoverMakerDisplay, MorbRoverMakerDisplay.Instance, IStateMachineTarget, MorbRoverMakerDisplay.Def>.GameInstance
	{
		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06008464 RID: 33892 RVA: 0x0033594D File Offset: 0x00333B4D
		public bool HasRecentlyConsumedGerms
		{
			get
			{
				return GameClock.Instance.GetTime() - this.lastTimeGermsConsumed < base.def.Timeout;
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06008465 RID: 33893 RVA: 0x0033596D File Offset: 0x00333B6D
		public bool GermsAreNeeded
		{
			get
			{
				return this.morbRoverMaker.MorbDevelopment_Progress < 1f;
			}
		}

		// Token: 0x06008466 RID: 33894 RVA: 0x00335984 File Offset: 0x00333B84
		public Instance(IStateMachineTarget master, MorbRoverMakerDisplay.Def def)
			: base(master, def)
		{
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			this.meter = new MeterController(component, "meter_display_target", "display_off_idle", Meter.Offset.UserSpecified, Grid.SceneLayer.BuildingFront, Array.Empty<string>());
			base.sm.monitor.Set(this.meter.gameObject, base.smi, false);
		}

		// Token: 0x06008467 RID: 33895 RVA: 0x003359EC File Offset: 0x00333BEC
		public override void StartSM()
		{
			this.morbRoverMaker = base.gameObject.GetSMI<MorbRoverMaker.Instance>();
			MorbRoverMaker.Instance instance = this.morbRoverMaker;
			instance.GermsAdded = (Action<long>)Delegate.Combine(instance.GermsAdded, new Action<long>(this.OnGermsAdded));
			MorbRoverMaker.Instance instance2 = this.morbRoverMaker;
			instance2.OnUncovered = (global::System.Action)Delegate.Combine(instance2.OnUncovered, new global::System.Action(this.OnUncovered));
			base.StartSM();
		}

		// Token: 0x06008468 RID: 33896 RVA: 0x00335A5E File Offset: 0x00333C5E
		private void OnGermsAdded(long amount)
		{
			this.lastTimeGermsConsumed = GameClock.Instance.GetTime();
		}

		// Token: 0x06008469 RID: 33897 RVA: 0x00335A70 File Offset: 0x00333C70
		public bool ShouldBeOn()
		{
			return this.morbRoverMaker.HasBeenRevealed && this.operational.IsOperational;
		}

		// Token: 0x0600846A RID: 33898 RVA: 0x00335A8C File Offset: 0x00333C8C
		private void OnUncovered()
		{
			if (base.IsInsideState(base.sm.off.idle))
			{
				this.GoTo(base.sm.off.exiting);
			}
		}

		// Token: 0x040064AA RID: 25770
		private float lastTimeGermsConsumed = -1f;

		// Token: 0x040064AB RID: 25771
		[MyCmpReq]
		private Operational operational;

		// Token: 0x040064AC RID: 25772
		private MorbRoverMaker.Instance morbRoverMaker;

		// Token: 0x040064AD RID: 25773
		private MeterController meter;
	}
}
