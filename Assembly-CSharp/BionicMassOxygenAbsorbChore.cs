using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000475 RID: 1141
public class BionicMassOxygenAbsorbChore : Chore<BionicMassOxygenAbsorbChore.Instance>
{
	// Token: 0x060017FD RID: 6141 RVA: 0x00084CC0 File Offset: 0x00082EC0
	public BionicMassOxygenAbsorbChore(IStateMachineTarget target, bool critical)
		: base(critical ? Db.Get().ChoreTypes.BionicAbsorbOxygen_Critical : Db.Get().ChoreTypes.BionicAbsorbOxygen, target, target.GetComponent<ChoreProvider>(), false, null, null, null, critical ? PriorityScreen.PriorityClass.compulsory : PriorityScreen.PriorityClass.personalNeeds, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new BionicMassOxygenAbsorbChore.Instance(this, target.gameObject);
		Func<int> func = new Func<int>(base.smi.UpdateTargetCell);
		this.AddPrecondition(ChorePreconditions.instance.IsNotRedAlert, null);
		this.AddPrecondition(ChorePreconditions.instance.CanMoveToDynamicCellUntilBegun, func);
	}

	// Token: 0x060017FE RID: 6142 RVA: 0x00084D58 File Offset: 0x00082F58
	public override string ResolveString(string str)
	{
		float num = ((base.smi == null) ? 0f : base.smi.GetAverageMassConsumedPerSecond());
		return string.Format(base.ResolveString(str), GameUtil.GetFormattedMass(num, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
	}

	// Token: 0x060017FF RID: 6143 RVA: 0x00084D9C File Offset: 0x00082F9C
	public override void Begin(Chore.Precondition.Context context)
	{
		if (context.consumerState.consumer == null)
		{
			global::Debug.LogError("BionicMassAbsorbOxygenChore null context.consumer");
			return;
		}
		if (context.consumerState.consumer.GetSMI<BionicOxygenTankMonitor.Instance>() == null)
		{
			global::Debug.LogError("BionicMassAbsorbOxygenChore null BionicOxygenTankMonitor.Instance");
			return;
		}
		base.smi.ResetMassTrackHistory();
		base.smi.sm.dupe.Set(context.consumerState.consumer, base.smi);
		base.Begin(context);
	}

	// Token: 0x06001800 RID: 6144 RVA: 0x00084E1C File Offset: 0x0008301C
	public static bool IsNotAllowedByScheduleAndChoreIsNotCritical(BionicMassOxygenAbsorbChore.Instance smi)
	{
		return !BionicMassOxygenAbsorbChore.IsCriticalChore(smi) && !BionicMassOxygenAbsorbChore.IsAllowedBySchedule(smi);
	}

	// Token: 0x06001801 RID: 6145 RVA: 0x00084E31 File Offset: 0x00083031
	public static bool IsAllowedBySchedule(BionicMassOxygenAbsorbChore.Instance smi)
	{
		return BionicOxygenTankMonitor.IsAllowedToSeekOxygenBySchedule(smi.oxygenTankMonitor);
	}

	// Token: 0x06001802 RID: 6146 RVA: 0x00084E3E File Offset: 0x0008303E
	public static bool IsCriticalChore(BionicMassOxygenAbsorbChore.Instance smi)
	{
		return smi.master.choreType == Db.Get().ChoreTypes.BionicAbsorbOxygen_Critical;
	}

	// Token: 0x06001803 RID: 6147 RVA: 0x00084E5C File Offset: 0x0008305C
	public static void ResetOxygenTimer(BionicMassOxygenAbsorbChore.Instance smi)
	{
		smi.sm.SecondsPassedWithoutOxygen.Set(0f, smi, false);
	}

	// Token: 0x06001804 RID: 6148 RVA: 0x00084E76 File Offset: 0x00083076
	public static void RefreshTargetSafeCell(BionicMassOxygenAbsorbChore.Instance smi)
	{
		smi.UpdateTargetCell();
	}

	// Token: 0x06001805 RID: 6149 RVA: 0x00084E7F File Offset: 0x0008307F
	public static void UpdateTargetSafeCell(BionicMassOxygenAbsorbChore.Instance smi, float dt)
	{
		BionicMassOxygenAbsorbChore.RefreshTargetSafeCell(smi);
	}

	// Token: 0x06001806 RID: 6150 RVA: 0x00084E87 File Offset: 0x00083087
	public static bool HasSpaceInOxygenTank(BionicMassOxygenAbsorbChore.Instance smi)
	{
		return smi.oxygenTankMonitor.SpaceAvailableInTank > 0f;
	}

	// Token: 0x06001807 RID: 6151 RVA: 0x00084E9B File Offset: 0x0008309B
	public static bool ChoreIsCriticalModeAndGiveUpOxygenLevelReached(BionicMassOxygenAbsorbChore.Instance smi)
	{
		return BionicMassOxygenAbsorbChore.IsCriticalChore(smi) && smi.oxygenTankMonitor.OxygenPercentage >= 0.25f;
	}

	// Token: 0x06001808 RID: 6152 RVA: 0x00084EBC File Offset: 0x000830BC
	public static bool BreathIsFull(BionicMassOxygenAbsorbChore.Instance smi)
	{
		AmountInstance amountInstance = smi.gameObject.GetAmounts().Get(Db.Get().Amounts.Breath);
		return amountInstance.value >= amountInstance.GetMax();
	}

	// Token: 0x06001809 RID: 6153 RVA: 0x00084EFA File Offset: 0x000830FA
	public static void UpdateTargetSafeCellOnlyInCriticalMode(BionicMassOxygenAbsorbChore.Instance smi, float dt)
	{
		if (BionicMassOxygenAbsorbChore.IsCriticalChore(smi))
		{
			BionicMassOxygenAbsorbChore.RefreshTargetSafeCell(smi);
		}
	}

	// Token: 0x0600180A RID: 6154 RVA: 0x00084F0C File Offset: 0x0008310C
	public static void AbsorbUpdate(BionicMassOxygenAbsorbChore.Instance smi, float dt)
	{
		float num = Mathf.Min(dt * BionicMassOxygenAbsorbChore.ABSORB_RATE, smi.oxygenTankMonitor.SpaceAvailableInTank);
		BionicMassOxygenAbsorbChore.AbsorbUpdateData absorbUpdateData = new BionicMassOxygenAbsorbChore.AbsorbUpdateData(smi, dt);
		int num2;
		SimHashes nearBreathableElement = BionicMassOxygenAbsorbChore.GetNearBreathableElement(num2 = Grid.PosToCell(smi.sm.dupe.Get(smi)), BionicMassOxygenAbsorbChore.ABSORB_RANGE, out num2);
		HandleVector<Game.ComplexCallbackInfo<Sim.MassConsumedCallback>>.Handle handle = Game.Instance.massConsumedCallbackManager.Add(new Action<Sim.MassConsumedCallback, object>(BionicMassOxygenAbsorbChore.OnSimConsumeCallback), absorbUpdateData, "BionicMassOxygenAbsorbChore");
		SimMessages.ConsumeMass(num2, nearBreathableElement, num, 6, handle.index);
	}

	// Token: 0x0600180B RID: 6155 RVA: 0x00084F98 File Offset: 0x00083198
	private static void OnSimConsumeCallback(Sim.MassConsumedCallback mass_cb_info, object data)
	{
		BionicMassOxygenAbsorbChore.AbsorbUpdateData absorbUpdateData = (BionicMassOxygenAbsorbChore.AbsorbUpdateData)data;
		absorbUpdateData.smi.OnSimConsume(mass_cb_info, absorbUpdateData.dt);
	}

	// Token: 0x0600180C RID: 6156 RVA: 0x00084FBE File Offset: 0x000831BE
	private static void ShowOxygenBar(BionicMassOxygenAbsorbChore.Instance smi)
	{
		if (NameDisplayScreen.Instance != null)
		{
			NameDisplayScreen.Instance.SetBionicOxygenTankDisplay(smi.gameObject, new Func<float>(smi.GetOxygen), true);
		}
	}

	// Token: 0x0600180D RID: 6157 RVA: 0x00084FEA File Offset: 0x000831EA
	private static void HideOxygenBar(BionicMassOxygenAbsorbChore.Instance smi)
	{
		if (NameDisplayScreen.Instance != null)
		{
			NameDisplayScreen.Instance.SetBionicOxygenTankDisplay(smi.gameObject, null, false);
		}
	}

	// Token: 0x0600180E RID: 6158 RVA: 0x0008500C File Offset: 0x0008320C
	public static SimHashes GetNearBreathableElement(int centralCell, CellOffset[] range, out int elementCell)
	{
		float num = 0f;
		int num2 = centralCell;
		SimHashes simHashes = SimHashes.Vacuum;
		foreach (CellOffset cellOffset in range)
		{
			int num3 = Grid.OffsetCell(centralCell, cellOffset);
			SimHashes simHashes2 = SimHashes.Vacuum;
			float breathableMassInCell = BionicMassOxygenAbsorbChore.GetBreathableMassInCell(num3, out simHashes2);
			if (breathableMassInCell > Mathf.Epsilon && (simHashes == SimHashes.Vacuum || breathableMassInCell > num))
			{
				simHashes = simHashes2;
				num = breathableMassInCell;
				num2 = num3;
			}
		}
		elementCell = num2;
		return simHashes;
	}

	// Token: 0x0600180F RID: 6159 RVA: 0x00085084 File Offset: 0x00083284
	private static float GetBreathableMassInCell(int cell, out SimHashes elementID)
	{
		if (Grid.IsValidCell(cell))
		{
			Element element = Grid.Element[cell];
			if (element.HasTag(GameTags.Breathable))
			{
				elementID = element.id;
				return Grid.Mass[cell];
			}
		}
		elementID = SimHashes.Vacuum;
		return 0f;
	}

	// Token: 0x04000DD6 RID: 3542
	public static CellOffset[] ABSORB_RANGE = new CellOffset[]
	{
		new CellOffset(0, 0),
		new CellOffset(0, 1),
		new CellOffset(1, 1),
		new CellOffset(-1, 1),
		new CellOffset(1, 0),
		new CellOffset(-1, 0)
	};

	// Token: 0x04000DD7 RID: 3543
	public const float ABSORB_RATE_IDEAL_CHORE_DURATION = 30f;

	// Token: 0x04000DD8 RID: 3544
	public static readonly float ABSORB_RATE = BionicOxygenTankMonitor.OXYGEN_TANK_CAPACITY_KG / 30f;

	// Token: 0x04000DD9 RID: 3545
	public const int HISTORY_ROW_COUNT = 15;

	// Token: 0x04000DDA RID: 3546
	public const float LOW_OXYGEN_TRESHOLD = 2f;

	// Token: 0x04000DDB RID: 3547
	public const float GIVE_UP_DURATION_CRTICIAL_MODE = 2f;

	// Token: 0x04000DDC RID: 3548
	public const float GIVE_UP_DURATION_LOW_OXYGEN_MODE = 4f;

	// Token: 0x04000DDD RID: 3549
	public const float CRITICAL_CHORE_GIVE_UP_OXYGEN_LEVEL_TRESHOLD = 0.25f;

	// Token: 0x04000DDE RID: 3550
	public const string ABSORB_ANIM_FILE = "anim_bionic_absorb_kanim";

	// Token: 0x04000DDF RID: 3551
	public const string ABSORB_PRE_ANIM_NAME = "absorb_pre";

	// Token: 0x04000DE0 RID: 3552
	public const string ABSORB_LOOP_ANIM_NAME = "absorb_loop";

	// Token: 0x04000DE1 RID: 3553
	public const string ABSORB_PST_ANIM_NAME = "absorb_pst";

	// Token: 0x04000DE2 RID: 3554
	public static CellOffset MouthCellOffset = new CellOffset(0, 1);

	// Token: 0x02001269 RID: 4713
	public class States : GameStateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore>
	{
		// Token: 0x06008630 RID: 34352 RVA: 0x0033C030 File Offset: 0x0033A230
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.move;
			base.Target(this.dupe);
			this.root.Exit(delegate(BionicMassOxygenAbsorbChore.Instance smi)
			{
				smi.ChangeCellReservation(Grid.InvalidCell);
			});
			this.move.DefaultState(this.move.onGoing).ScheduleChange(this.fail, new StateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.Transition.ConditionCallback(BionicMassOxygenAbsorbChore.IsNotAllowedByScheduleAndChoreIsNotCritical));
			this.move.onGoing.Enter(new StateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.State.Callback(BionicMassOxygenAbsorbChore.RefreshTargetSafeCell)).Update(new Action<BionicMassOxygenAbsorbChore.Instance, float>(BionicMassOxygenAbsorbChore.UpdateTargetSafeCellOnlyInCriticalMode), UpdateRate.RENDER_1000ms, false).MoveTo((BionicMassOxygenAbsorbChore.Instance smi) => smi.targetCell, this.absorb, this.move.fail, true);
			this.move.fail.ReturnFailure();
			this.absorb.ScheduleChange(this.fail, new StateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.Transition.ConditionCallback(BionicMassOxygenAbsorbChore.IsNotAllowedByScheduleAndChoreIsNotCritical)).ToggleTag(GameTags.RecoveringBreath).ToggleAnims("anim_bionic_absorb_kanim", 0f)
				.Enter(new StateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.State.Callback(BionicMassOxygenAbsorbChore.ShowOxygenBar))
				.Exit(new StateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.State.Callback(BionicMassOxygenAbsorbChore.HideOxygenBar))
				.DefaultState(this.absorb.pre);
			this.absorb.pre.PlayAnim("absorb_pre", KAnim.PlayMode.Once).OnAnimQueueComplete(this.absorb.loop).ScheduleGoTo(3f, this.absorb.loop)
				.Exit(new StateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.State.Callback(BionicMassOxygenAbsorbChore.ResetOxygenTimer));
			this.absorb.loop.Enter(new StateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.State.Callback(BionicMassOxygenAbsorbChore.ResetOxygenTimer)).ParamTransition<float>(this.SecondsPassedWithoutOxygen, this.absorb.pst, (BionicMassOxygenAbsorbChore.Instance smi, float secondsPassed) => secondsPassed > smi.GetGiveupTimerTimeout()).OnSignal(this.TankFilledSignal, this.absorb.pst)
				.PlayAnim("absorb_loop", KAnim.PlayMode.Loop)
				.Update(new Action<BionicMassOxygenAbsorbChore.Instance, float>(BionicMassOxygenAbsorbChore.AbsorbUpdate), UpdateRate.SIM_200ms, false)
				.Transition(this.absorb.pst, new StateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.Transition.ConditionCallback(BionicMassOxygenAbsorbChore.ChoreIsCriticalModeAndGiveUpOxygenLevelReached), UpdateRate.SIM_200ms);
			this.absorb.pst.Transition(this.absorb.criticalRecoverBreath.pre, new StateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.Transition.ConditionCallback(BionicMassOxygenAbsorbChore.IsCriticalChore), UpdateRate.SIM_200ms).PlayAnim("absorb_pst", KAnim.PlayMode.Once).OnAnimQueueComplete(this.complete)
				.ScheduleGoTo(3f, this.complete);
			this.absorb.criticalRecoverBreath.ToggleAnims("anim_emotes_default_kanim", 0f).DefaultState(this.absorb.criticalRecoverBreath.pre);
			this.absorb.criticalRecoverBreath.pre.PlayAnim("breathe_pre").QueueAnim("breathe_loop", false, null).OnAnimQueueComplete(this.absorb.criticalRecoverBreath.loop);
			this.absorb.criticalRecoverBreath.loop.PlayAnim("breathe_loop", KAnim.PlayMode.Loop).ToggleAttributeModifier("Recovering Breath", (BionicMassOxygenAbsorbChore.Instance smi) => smi.recoveringbreath, null).Transition(this.absorb.criticalRecoverBreath.pst, new StateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.Transition.ConditionCallback(BionicMassOxygenAbsorbChore.BreathIsFull), UpdateRate.SIM_200ms)
				.Transition(this.absorb.criticalRecoverBreath.pst, (BionicMassOxygenAbsorbChore.Instance smi) => smi.UpdateTargetCell() == Grid.InvalidCell, UpdateRate.SIM_200ms);
			this.absorb.criticalRecoverBreath.pst.PlayAnim("breathe_pst", KAnim.PlayMode.Once).OnAnimQueueComplete(this.complete).ScheduleGoTo(3f, this.complete);
			this.fail.ReturnFailure();
			this.complete.ReturnSuccess();
		}

		// Token: 0x04006600 RID: 26112
		public BionicMassOxygenAbsorbChore.States.MoveStates move;

		// Token: 0x04006601 RID: 26113
		public BionicMassOxygenAbsorbChore.States.MassAbsorbStates absorb;

		// Token: 0x04006602 RID: 26114
		public GameStateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.State fail;

		// Token: 0x04006603 RID: 26115
		public GameStateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.State complete;

		// Token: 0x04006604 RID: 26116
		public StateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.FloatParameter SecondsPassedWithoutOxygen;

		// Token: 0x04006605 RID: 26117
		public StateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.TargetParameter dupe;

		// Token: 0x04006606 RID: 26118
		public StateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.Signal TankFilledSignal;

		// Token: 0x0200263C RID: 9788
		public class MoveStates : GameStateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.State
		{
			// Token: 0x0400AA05 RID: 43525
			public GameStateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.State onGoing;

			// Token: 0x0400AA06 RID: 43526
			public GameStateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.State fail;
		}

		// Token: 0x0200263D RID: 9789
		public class MassAbsorbStates : GameStateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.State
		{
			// Token: 0x0400AA07 RID: 43527
			public GameStateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.State pre;

			// Token: 0x0400AA08 RID: 43528
			public GameStateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.State loop;

			// Token: 0x0400AA09 RID: 43529
			public GameStateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.State pst;

			// Token: 0x0400AA0A RID: 43530
			public BionicMassOxygenAbsorbChore.States.MassAbsorbStates.CriticalRecover criticalRecoverBreath;

			// Token: 0x02003878 RID: 14456
			public class CriticalRecover : GameStateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.State
			{
				// Token: 0x0400E457 RID: 58455
				public GameStateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.State pre;

				// Token: 0x0400E458 RID: 58456
				public GameStateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.State loop;

				// Token: 0x0400E459 RID: 58457
				public GameStateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.State pst;
			}
		}
	}

	// Token: 0x0200126A RID: 4714
	public struct AbsorbUpdateData
	{
		// Token: 0x06008632 RID: 34354 RVA: 0x0033C42E File Offset: 0x0033A62E
		public AbsorbUpdateData(BionicMassOxygenAbsorbChore.Instance smi, float dt)
		{
			this.smi = smi;
			this.dt = dt;
		}

		// Token: 0x04006607 RID: 26119
		public BionicMassOxygenAbsorbChore.Instance smi;

		// Token: 0x04006608 RID: 26120
		public float dt;
	}

	// Token: 0x0200126B RID: 4715
	public class Instance : GameStateMachine<BionicMassOxygenAbsorbChore.States, BionicMassOxygenAbsorbChore.Instance, BionicMassOxygenAbsorbChore, object>.GameInstance, BionicOxygenTankMonitor.IChore
	{
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06008633 RID: 34355 RVA: 0x0033C43E File Offset: 0x0033A63E
		public float CRITICAL_OXYGEN_MASS_GIVE_UP_TRESHOLD
		{
			get
			{
				return this.oxygenBreather.ConsumptionRate * 8f;
			}
		}

		// Token: 0x06008634 RID: 34356 RVA: 0x0033C451 File Offset: 0x0033A651
		public float GetGiveupTimerTimeout()
		{
			if (this.oxygenTankMonitor == null)
			{
				return 2f;
			}
			if (!BionicOxygenTankMonitor.AreOxygenLevelsCritical(this.oxygenTankMonitor))
			{
				return 4f;
			}
			return 2f;
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06008636 RID: 34358 RVA: 0x0033C482 File Offset: 0x0033A682
		// (set) Token: 0x06008635 RID: 34357 RVA: 0x0033C479 File Offset: 0x0033A679
		public OxygenBreather oxygenBreather { get; private set; }

		// Token: 0x06008637 RID: 34359 RVA: 0x0033C48C File Offset: 0x0033A68C
		public Instance(BionicMassOxygenAbsorbChore master, GameObject duplicant)
			: base(master)
		{
			base.sm.dupe.Set(duplicant, base.smi, false);
			this.oxygenTankMonitor = duplicant.GetSMI<BionicOxygenTankMonitor.Instance>();
			this.oxygenBreather = duplicant.GetComponent<OxygenBreather>();
			Klei.AI.Attribute deltaAttribute = Db.Get().Amounts.Breath.deltaAttribute;
			float recover_BREATH_DELTA = DUPLICANTSTATS.STANDARD.BaseStats.RECOVER_BREATH_DELTA;
			this.recoveringbreath = new AttributeModifier(deltaAttribute.Id, recover_BREATH_DELTA, DUPLICANTS.MODIFIERS.RECOVERINGBREATH.NAME, false, false, true);
		}

		// Token: 0x06008638 RID: 34360 RVA: 0x0033C52B File Offset: 0x0033A72B
		public bool IsConsumingOxygen()
		{
			return !base.IsInsideState(base.sm.move);
		}

		// Token: 0x06008639 RID: 34361 RVA: 0x0033C544 File Offset: 0x0033A744
		public void ChangeCellReservation(int newCell)
		{
			if (this.targetCell != Grid.InvalidCell && Grid.Reserved[this.targetCell])
			{
				Grid.Reserved[this.targetCell] = false;
			}
			if (newCell != Grid.InvalidCell && !Grid.Reserved[newCell])
			{
				Grid.Reserved[newCell] = true;
			}
		}

		// Token: 0x0600863A RID: 34362 RVA: 0x0033C5A2 File Offset: 0x0033A7A2
		public override void StopSM(string reason)
		{
			this.ChangeCellReservation(Grid.InvalidCell);
			base.StopSM(reason);
		}

		// Token: 0x0600863B RID: 34363 RVA: 0x0033C5B8 File Offset: 0x0033A7B8
		public int UpdateTargetCell()
		{
			this.oxygenTankMonitor.UpdatePotentialCellToAbsorbOxygen(this.targetCell);
			int absorbOxygenCell = this.oxygenTankMonitor.AbsorbOxygenCell;
			this.ChangeCellReservation(absorbOxygenCell);
			this.targetCell = absorbOxygenCell;
			return absorbOxygenCell;
		}

		// Token: 0x0600863C RID: 34364 RVA: 0x0033C5F4 File Offset: 0x0033A7F4
		public void ResetMassTrackHistory()
		{
			this.massAbsorbedHistory.Clear();
			for (int i = 0; i < 15; i++)
			{
				this.massAbsorbedHistory.Enqueue(0f);
			}
		}

		// Token: 0x0600863D RID: 34365 RVA: 0x0033C629 File Offset: 0x0033A829
		public void AddMassToHistory(float mass_rate_this_tick)
		{
			if (this.massAbsorbedHistory.Count == 15)
			{
				this.massAbsorbedHistory.Dequeue();
			}
			this.massAbsorbedHistory.Enqueue(mass_rate_this_tick);
		}

		// Token: 0x0600863E RID: 34366 RVA: 0x0033C654 File Offset: 0x0033A854
		public float GetAverageMassConsumedPerSecond()
		{
			float num = 0f;
			int num2 = 0;
			foreach (float num3 in this.massAbsorbedHistory)
			{
				num += num3;
				num2++;
			}
			if (num2 <= 0)
			{
				return 0f;
			}
			num /= (float)num2;
			return num;
		}

		// Token: 0x0600863F RID: 34367 RVA: 0x0033C6C0 File Offset: 0x0033A8C0
		public void OnSimConsume(Sim.MassConsumedCallback mass_cb_info, float dt)
		{
			if (this.oxygenBreather == null || this.oxygenTankMonitor == null || this.oxygenBreather.prefabID.HasTag(GameTags.Dead))
			{
				return;
			}
			this.AddMassToHistory(mass_cb_info.mass / dt);
			GameObject gameObject = this.oxygenBreather.gameObject;
			bool flag = BionicOxygenTankMonitor.AreOxygenLevelsCritical(this.oxygenTankMonitor);
			float num = (flag ? this.CRITICAL_OXYGEN_MASS_GIVE_UP_TRESHOLD : 2f);
			if (this.GetAverageMassConsumedPerSecond() <= num)
			{
				base.sm.SecondsPassedWithoutOxygen.Set(base.sm.SecondsPassedWithoutOxygen.Get(base.smi) + dt, base.smi, false);
			}
			else
			{
				BionicMassOxygenAbsorbChore.ResetOxygenTimer(base.smi);
			}
			if (flag)
			{
				float num2 = DUPLICANTSTATS.STANDARD.Breath.BREATH_RATE * DUPLICANTSTATS.STANDARD.BaseStats.OXYGEN_USED_PER_SECOND;
				if (mass_cb_info.mass == 0f)
				{
					mass_cb_info.temperature = DUPLICANTSTATS.BIONICS.Temperature.Internal.IDEAL;
				}
				mass_cb_info.mass += DUPLICANTSTATS.STANDARD.BaseStats.RECOVER_BREATH_DELTA * num2 * dt + DUPLICANTSTATS.STANDARD.BaseStats.OXYGEN_USED_PER_SECOND * dt;
			}
			float num3 = this.oxygenTankMonitor.AddGas(mass_cb_info);
			if (num3 > Mathf.Epsilon)
			{
				SimMessages.EmitMass(Grid.PosToCell(gameObject), mass_cb_info.elemIdx, num3, mass_cb_info.temperature, byte.MaxValue, 0, -1);
			}
			if (!BionicMassOxygenAbsorbChore.HasSpaceInOxygenTank(this))
			{
				base.sm.TankFilledSignal.Trigger(this);
			}
		}

		// Token: 0x06008640 RID: 34368 RVA: 0x0033C843 File Offset: 0x0033AA43
		public float GetOxygen()
		{
			if (this.oxygenTankMonitor != null)
			{
				return this.oxygenTankMonitor.OxygenPercentage;
			}
			return 0f;
		}

		// Token: 0x04006609 RID: 26121
		public AttributeModifier recoveringbreath;

		// Token: 0x0400660B RID: 26123
		public Queue<float> massAbsorbedHistory = new Queue<float>();

		// Token: 0x0400660C RID: 26124
		public int targetCell = Grid.InvalidCell;

		// Token: 0x0400660D RID: 26125
		public BionicOxygenTankMonitor.Instance oxygenTankMonitor;
	}
}
