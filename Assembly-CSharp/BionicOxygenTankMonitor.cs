using System;
using Klei;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x020009D4 RID: 2516
public class BionicOxygenTankMonitor : GameStateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>
{
	// Token: 0x060049AE RID: 18862 RVA: 0x001AAB8C File Offset: 0x001A8D8C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.fistSpawn;
		this.fistSpawn.ParamTransition<bool>(this.HasSpawnedBefore, this.safe, GameStateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.IsTrue).Enter(new StateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.State.Callback(BionicOxygenTankMonitor.StartWithFullTank));
		this.safe.Transition(this.low, GameStateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.Not(new StateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.Transition.ConditionCallback(BionicOxygenTankMonitor.AreOxygenLevelsSafe)), UpdateRate.SIM_200ms);
		this.low.DefaultState(this.low.idle);
		this.low.idle.Transition(this.critical, new StateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.Transition.ConditionCallback(BionicOxygenTankMonitor.AreOxygenLevelsCritical), UpdateRate.SIM_200ms).Transition(this.safe, new StateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.Transition.ConditionCallback(BionicOxygenTankMonitor.AreOxygenLevelsSafe), UpdateRate.SIM_200ms).ScheduleChange(this.low.schedule, new StateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.Transition.ConditionCallback(BionicOxygenTankMonitor.IsAllowedToSeekOxygenBySchedule));
		this.low.schedule.ToggleUrge(Db.Get().Urges.FindOxygenRefill).DefaultState(this.low.schedule.enableSensors).Transition(this.critical, new StateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.Transition.ConditionCallback(BionicOxygenTankMonitor.AreOxygenLevelsCriticalAndNotConsumingOxygen), UpdateRate.SIM_200ms)
			.Exit(new StateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.State.Callback(BionicOxygenTankMonitor.DisableOxygenSourceSensors));
		this.low.schedule.enableSensors.Enter(new StateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.State.Callback(BionicOxygenTankMonitor.EnableOxygenSourceSensors)).GoTo(this.low.schedule.oxygenCanisterMode);
		this.low.schedule.oxygenCanisterMode.DefaultState(this.low.schedule.oxygenCanisterMode.running);
		this.low.schedule.oxygenCanisterMode.running.ScheduleChange(this.low.idle, new StateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.Transition.ConditionCallback(BionicOxygenTankMonitor.IsNotAllowedToSeekOxygenSourceItemsByScheduleAndSeekChoreHasNotBegun)).OnSignal(this.OxygenSourceItemLostSignal, this.low.schedule.environmentAbsorbMode, new Func<BionicOxygenTankMonitor.Instance, bool>(BionicOxygenTankMonitor.NoOxygenSourceAvailableButAbsorbCellAvailable)).OnSignal(this.AbsorbCellChangedSignal, this.low.schedule.environmentAbsorbMode, (BionicOxygenTankMonitor.Instance smi) => !BionicOxygenTankMonitor.FindOxygenSourceChoreIsRunning(smi) && BionicOxygenTankMonitor.NoOxygenSourceAvailableButAbsorbCellAvailable(smi))
			.Update(new Action<BionicOxygenTankMonitor.Instance, float>(BionicOxygenTankMonitor.UpdateAbsorbCellIfNoOxygenSourceAvailable), UpdateRate.SIM_200ms, false)
			.ToggleChore((BionicOxygenTankMonitor.Instance smi) => new FindAndConsumeOxygenSourceChore(smi.master, false), this.low.schedule.oxygenCanisterMode.ends, this.low.schedule.oxygenCanisterMode.ends);
		this.low.schedule.oxygenCanisterMode.ends.EnterTransition(this.safe, new StateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.Transition.ConditionCallback(BionicOxygenTankMonitor.AreOxygenLevelsSafe)).GoTo(this.low.idle);
		this.low.schedule.environmentAbsorbMode.DefaultState(this.low.schedule.environmentAbsorbMode.running);
		this.low.schedule.environmentAbsorbMode.running.ScheduleChange(this.low.idle, new StateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.Transition.ConditionCallback(BionicOxygenTankMonitor.IsNotAllowedToSeekOxygenSourceItemsByScheduleAndAbsorbChoreHasNotBegun)).OnSignal(this.ClosestOxygenSourceChanged, this.low.schedule.oxygenCanisterMode, new Func<BionicOxygenTankMonitor.Instance, bool>(BionicOxygenTankMonitor.OxygenSourceItemAvailableAndAbsorbChoreNotStarted)).ToggleChore((BionicOxygenTankMonitor.Instance smi) => new BionicMassOxygenAbsorbChore(smi.master, false), this.low.schedule.environmentAbsorbMode.ends, this.low.schedule.environmentAbsorbMode.ends);
		this.low.schedule.environmentAbsorbMode.ends.EnterTransition(this.safe, new StateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.Transition.ConditionCallback(BionicOxygenTankMonitor.AreOxygenLevelsSafe)).GoTo(this.low.idle);
		this.critical.ToggleUrge(Db.Get().Urges.FindOxygenRefill).Exit(new StateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.State.Callback(BionicOxygenTankMonitor.DisableOxygenSourceSensors)).DefaultState(this.critical.enableSensors)
			.ToggleExpression(Db.Get().Expressions.RecoverBreath, null)
			.Update(delegate(BionicOxygenTankMonitor.Instance smi, float dt)
			{
				if (smi.master.gameObject.GetAmounts().Get("Breath").value <= DUPLICANTSTATS.BIONICS.Breath.SUFFOCATE_AMOUNT)
				{
					smi.isRecoveringFromSuffocation = true;
				}
			}, UpdateRate.SIM_200ms, false)
			.Exit(delegate(BionicOxygenTankMonitor.Instance smi)
			{
				smi.isRecoveringFromSuffocation = false;
			});
		this.critical.enableSensors.Enter(new StateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.State.Callback(BionicOxygenTankMonitor.EnableOxygenSourceSensors)).GoTo(this.critical.oxygenCanisterMode);
		this.critical.oxygenCanisterMode.DefaultState(this.critical.oxygenCanisterMode.running);
		this.critical.oxygenCanisterMode.running.OnSignal(this.ClosestOxygenSourceChanged, this.critical.environmentAbsorbMode, (BionicOxygenTankMonitor.Instance smi) => !BionicOxygenTankMonitor.FindOxygenSourceChoreIsRunning(smi) && BionicOxygenTankMonitor.NoOxygenSourceAvailableButAbsorbCellAvailable(smi)).OnSignal(this.OxygenSourceItemLostSignal, this.critical.environmentAbsorbMode, new Func<BionicOxygenTankMonitor.Instance, bool>(BionicOxygenTankMonitor.NoOxygenSourceAvailableButAbsorbCellAvailable)).OnSignal(this.AbsorbCellChangedSignal, this.critical.environmentAbsorbMode, (BionicOxygenTankMonitor.Instance smi) => !BionicOxygenTankMonitor.FindOxygenSourceChoreIsRunning(smi) && BionicOxygenTankMonitor.NoOxygenSourceAvailableButAbsorbCellAvailable(smi))
			.Update(new Action<BionicOxygenTankMonitor.Instance, float>(BionicOxygenTankMonitor.UpdateAbsorbCellIfNoOxygenSourceAvailable), UpdateRate.SIM_200ms, false)
			.ToggleChore((BionicOxygenTankMonitor.Instance smi) => new FindAndConsumeOxygenSourceChore(smi.master, true), this.critical.oxygenCanisterMode.ends, this.critical.oxygenCanisterMode.ends);
		this.critical.oxygenCanisterMode.ends.EnterTransition(this.low, GameStateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.Not(new StateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.Transition.ConditionCallback(BionicOxygenTankMonitor.AreOxygenLevelsCritical))).GoTo(this.critical.oxygenCanisterMode.running);
		this.critical.environmentAbsorbMode.DefaultState(this.critical.environmentAbsorbMode.running);
		this.critical.environmentAbsorbMode.running.OnSignal(this.ClosestOxygenSourceChanged, this.critical.oxygenCanisterMode, new Func<BionicOxygenTankMonitor.Instance, bool>(BionicOxygenTankMonitor.OxygenSourceItemAvailableAndAbsorbChoreNotStarted)).ToggleChore((BionicOxygenTankMonitor.Instance smi) => new BionicMassOxygenAbsorbChore(smi.master, true), this.critical.environmentAbsorbMode.ends, this.critical.environmentAbsorbMode.ends);
		this.critical.environmentAbsorbMode.ends.EnterTransition(this.low, GameStateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.Not(new StateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.Transition.ConditionCallback(BionicOxygenTankMonitor.AreOxygenLevelsCritical))).GoTo(this.critical.oxygenCanisterMode);
	}

	// Token: 0x060049AF RID: 18863 RVA: 0x001AB25D File Offset: 0x001A945D
	public static bool IsAllowedToSeekOxygenBySchedule(BionicOxygenTankMonitor.Instance smi)
	{
		return smi.IsAllowedToSeekOxygenBySchedule;
	}

	// Token: 0x060049B0 RID: 18864 RVA: 0x001AB265 File Offset: 0x001A9465
	public static bool IsNotAllowedToSeekOxygenSourceItemsByScheduleAndSeekChoreHasNotBegun(BionicOxygenTankMonitor.Instance smi)
	{
		return !BionicOxygenTankMonitor.IsAllowedToSeekOxygenBySchedule(smi) && !BionicOxygenTankMonitor.FindOxygenSourceChoreIsRunning(smi);
	}

	// Token: 0x060049B1 RID: 18865 RVA: 0x001AB27A File Offset: 0x001A947A
	public static bool IsNotAllowedToSeekOxygenSourceItemsByScheduleAndAbsorbChoreHasNotBegun(BionicOxygenTankMonitor.Instance smi)
	{
		return !BionicOxygenTankMonitor.IsAllowedToSeekOxygenBySchedule(smi) && !BionicOxygenTankMonitor.AbsorbChoreIsRunning(smi);
	}

	// Token: 0x060049B2 RID: 18866 RVA: 0x001AB28F File Offset: 0x001A948F
	public static bool AreOxygenLevelsSafe(BionicOxygenTankMonitor.Instance smi)
	{
		return smi.OxygenPercentage >= 0.85f;
	}

	// Token: 0x060049B3 RID: 18867 RVA: 0x001AB2A1 File Offset: 0x001A94A1
	public static bool AreOxygenLevelsCritical(BionicOxygenTankMonitor.Instance smi)
	{
		return smi.OxygenPercentage <= 0f;
	}

	// Token: 0x060049B4 RID: 18868 RVA: 0x001AB2B3 File Offset: 0x001A94B3
	public static bool AreOxygenLevelsCriticalAndNotConsumingOxygen(BionicOxygenTankMonitor.Instance smi)
	{
		return BionicOxygenTankMonitor.AreOxygenLevelsCritical(smi) && !BionicOxygenTankMonitor.IsConsumingOxygen(smi);
	}

	// Token: 0x060049B5 RID: 18869 RVA: 0x001AB2C8 File Offset: 0x001A94C8
	public static bool IsThereAnOxygenSourceItemAvailable(BionicOxygenTankMonitor.Instance smi)
	{
		return smi.GetClosestOxygenSource() != null;
	}

	// Token: 0x060049B6 RID: 18870 RVA: 0x001AB2D6 File Offset: 0x001A94D6
	public static bool AbsorbCellUnavailable(BionicOxygenTankMonitor.Instance smi)
	{
		return smi.AbsorbOxygenCell == Grid.InvalidCell;
	}

	// Token: 0x060049B7 RID: 18871 RVA: 0x001AB2E5 File Offset: 0x001A94E5
	public static bool AbsorbCellAvailable(BionicOxygenTankMonitor.Instance smi)
	{
		return smi.AbsorbOxygenCell != Grid.InvalidCell;
	}

	// Token: 0x060049B8 RID: 18872 RVA: 0x001AB2F7 File Offset: 0x001A94F7
	public static bool NoOxygenSourceAvailable(BionicOxygenTankMonitor.Instance smi)
	{
		return smi.GetClosestOxygenSource() == null;
	}

	// Token: 0x060049B9 RID: 18873 RVA: 0x001AB305 File Offset: 0x001A9505
	public static bool NoOxygenSourceAvailableButAbsorbCellAvailable(BionicOxygenTankMonitor.Instance smi)
	{
		return BionicOxygenTankMonitor.NoOxygenSourceAvailable(smi) && BionicOxygenTankMonitor.AbsorbCellAvailable(smi);
	}

	// Token: 0x060049BA RID: 18874 RVA: 0x001AB317 File Offset: 0x001A9517
	public static bool OxygenSourceItemAvailableAndAbsorbChoreNotStarted(BionicOxygenTankMonitor.Instance smi)
	{
		return BionicOxygenTankMonitor.IsThereAnOxygenSourceItemAvailable(smi) && !BionicOxygenTankMonitor.AbsorbChoreIsRunning(smi);
	}

	// Token: 0x060049BB RID: 18875 RVA: 0x001AB32C File Offset: 0x001A952C
	public static bool AbsorbChoreIsRunning(BionicOxygenTankMonitor.Instance smi)
	{
		return BionicOxygenTankMonitor.ChoreIsRunning(smi, Db.Get().ChoreTypes.BionicAbsorbOxygen) || BionicOxygenTankMonitor.ChoreIsRunning(smi, Db.Get().ChoreTypes.BionicAbsorbOxygen_Critical);
	}

	// Token: 0x060049BC RID: 18876 RVA: 0x001AB35C File Offset: 0x001A955C
	public static bool FindOxygenSourceChoreIsRunning(BionicOxygenTankMonitor.Instance smi)
	{
		return BionicOxygenTankMonitor.ChoreIsRunning(smi, Db.Get().ChoreTypes.FindOxygenSourceItem) || BionicOxygenTankMonitor.ChoreIsRunning(smi, Db.Get().ChoreTypes.FindOxygenSourceItem_Critical);
	}

	// Token: 0x060049BD RID: 18877 RVA: 0x001AB38C File Offset: 0x001A958C
	public static bool ChoreIsRunning(BionicOxygenTankMonitor.Instance smi, ChoreType type)
	{
		return smi.ChoreIsRunning(type);
	}

	// Token: 0x060049BE RID: 18878 RVA: 0x001AB395 File Offset: 0x001A9595
	public static bool IsConsumingOxygen(BionicOxygenTankMonitor.Instance smi)
	{
		return smi.IsConsumingOxygen();
	}

	// Token: 0x060049BF RID: 18879 RVA: 0x001AB39D File Offset: 0x001A959D
	public static void StartWithFullTank(BionicOxygenTankMonitor.Instance smi)
	{
		smi.AddFirstTimeSpawnedOxygen();
	}

	// Token: 0x060049C0 RID: 18880 RVA: 0x001AB3A5 File Offset: 0x001A95A5
	public static void EnableOxygenSourceSensors(BionicOxygenTankMonitor.Instance smi)
	{
		smi.SetOxygenSourceSensorsActiveState(true);
	}

	// Token: 0x060049C1 RID: 18881 RVA: 0x001AB3AE File Offset: 0x001A95AE
	public static void DisableOxygenSourceSensors(BionicOxygenTankMonitor.Instance smi)
	{
		smi.SetOxygenSourceSensorsActiveState(false);
	}

	// Token: 0x060049C2 RID: 18882 RVA: 0x001AB3B7 File Offset: 0x001A95B7
	public static void UpdateAbsorbCellIfNoOxygenSourceAvailable(BionicOxygenTankMonitor.Instance smi, float dt)
	{
		if (BionicOxygenTankMonitor.NoOxygenSourceAvailable(smi))
		{
			smi.UpdatePotentialCellToAbsorbOxygen(Grid.InvalidCell);
		}
	}

	// Token: 0x04003091 RID: 12433
	public const SimHashes INITIAL_TANK_ELEMENT = SimHashes.Oxygen;

	// Token: 0x04003092 RID: 12434
	public static readonly Tag INITIAL_TANK_ELEMENT_TAG = SimHashes.Oxygen.CreateTag();

	// Token: 0x04003093 RID: 12435
	public const float SAFE_TRESHOLD = 0.85f;

	// Token: 0x04003094 RID: 12436
	public const float CRITICAL_TRESHOLD = 0f;

	// Token: 0x04003095 RID: 12437
	public const float OXYGEN_TANK_CAPACITY_IN_SECONDS = 2400f;

	// Token: 0x04003096 RID: 12438
	public static readonly float OXYGEN_TANK_CAPACITY_KG = 2400f * DUPLICANTSTATS.BIONICS.BaseStats.OXYGEN_USED_PER_SECOND;

	// Token: 0x04003097 RID: 12439
	public static float INITIAL_OXYGEN_TEMP = DUPLICANTSTATS.BIONICS.Temperature.Internal.IDEAL;

	// Token: 0x04003098 RID: 12440
	public static float SECONDS_PER_PATH_COST_UNIT = 0.3f;

	// Token: 0x04003099 RID: 12441
	public GameStateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.State fistSpawn;

	// Token: 0x0400309A RID: 12442
	public GameStateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.State safe;

	// Token: 0x0400309B RID: 12443
	public BionicOxygenTankMonitor.LowOxygenStates low;

	// Token: 0x0400309C RID: 12444
	public BionicOxygenTankMonitor.SeekOxygenStates critical;

	// Token: 0x0400309D RID: 12445
	private StateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.BoolParameter HasSpawnedBefore;

	// Token: 0x0400309E RID: 12446
	public StateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.Signal AbsorbCellChangedSignal;

	// Token: 0x0400309F RID: 12447
	public StateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.Signal OxygenSourceItemLostSignal;

	// Token: 0x040030A0 RID: 12448
	public StateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.Signal ClosestOxygenSourceChanged;

	// Token: 0x020019FD RID: 6653
	public interface IChore
	{
		// Token: 0x0600A18D RID: 41357
		bool IsConsumingOxygen();
	}

	// Token: 0x020019FE RID: 6654
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020019FF RID: 6655
	public class ChoreState : GameStateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.State
	{
		// Token: 0x04007E4C RID: 32332
		public GameStateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.State running;

		// Token: 0x04007E4D RID: 32333
		public GameStateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.State ends;
	}

	// Token: 0x02001A00 RID: 6656
	public class SeekOxygenStates : GameStateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.State
	{
		// Token: 0x04007E4E RID: 32334
		public GameStateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.State enableSensors;

		// Token: 0x04007E4F RID: 32335
		public BionicOxygenTankMonitor.ChoreState oxygenCanisterMode;

		// Token: 0x04007E50 RID: 32336
		public BionicOxygenTankMonitor.ChoreState environmentAbsorbMode;
	}

	// Token: 0x02001A01 RID: 6657
	public class LowOxygenStates : GameStateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.State
	{
		// Token: 0x04007E51 RID: 32337
		public GameStateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.State idle;

		// Token: 0x04007E52 RID: 32338
		public BionicOxygenTankMonitor.SeekOxygenStates schedule;
	}

	// Token: 0x02001A02 RID: 6658
	public new class Instance : GameStateMachine<BionicOxygenTankMonitor, BionicOxygenTankMonitor.Instance, IStateMachineTarget, BionicOxygenTankMonitor.Def>.GameInstance, OxygenBreather.IGasProvider
	{
		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x0600A192 RID: 41362 RVA: 0x0039EEC0 File Offset: 0x0039D0C0
		public bool IsAllowedToSeekOxygenBySchedule
		{
			get
			{
				return ScheduleManager.Instance.IsAllowed(this.schedulable, Db.Get().ScheduleBlockTypes.Eat);
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x0600A193 RID: 41363 RVA: 0x0039EEE1 File Offset: 0x0039D0E1
		public bool IsEmpty
		{
			get
			{
				return this.AvailableOxygen == 0f;
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x0600A194 RID: 41364 RVA: 0x0039EEF0 File Offset: 0x0039D0F0
		public float OxygenPercentage
		{
			get
			{
				return this.AvailableOxygen / this.storage.capacityKg;
			}
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x0600A195 RID: 41365 RVA: 0x0039EF04 File Offset: 0x0039D104
		public float AvailableOxygen
		{
			get
			{
				return this.storage.GetMassAvailable(GameTags.Breathable);
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x0600A196 RID: 41366 RVA: 0x0039EF16 File Offset: 0x0039D116
		public float SpaceAvailableInTank
		{
			get
			{
				return this.storage.capacityKg - this.AvailableOxygen;
			}
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x0600A198 RID: 41368 RVA: 0x0039EF33 File Offset: 0x0039D133
		// (set) Token: 0x0600A197 RID: 41367 RVA: 0x0039EF2A File Offset: 0x0039D12A
		public int AbsorbOxygenCell { get; private set; } = Grid.InvalidCell;

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x0600A19A RID: 41370 RVA: 0x0039EF44 File Offset: 0x0039D144
		// (set) Token: 0x0600A199 RID: 41369 RVA: 0x0039EF3B File Offset: 0x0039D13B
		public Storage storage { get; private set; }

		// Token: 0x0600A19B RID: 41371 RVA: 0x0039EF4C File Offset: 0x0039D14C
		public Instance(IStateMachineTarget master, BionicOxygenTankMonitor.Def def)
			: base(master, def)
		{
			this.query = new AbsorbCellQuery();
			NameDisplayScreen.Instance.RegisterComponent(base.gameObject, this, false);
			Sensors component = base.GetComponent<Sensors>();
			this.schedulable = base.GetComponent<Schedulable>();
			this.navigator = base.GetComponent<Navigator>();
			float movementSpeedMultiplier = BipedTransitionLayer.GetMovementSpeedMultiplier(Db.Get().AttributeConverters.MovementSpeed.Lookup(this.navigator.gameObject));
			this.movementRate = movementSpeedMultiplier / BionicOxygenTankMonitor.SECONDS_PER_PATH_COST_UNIT;
			this.oxygenBreather = base.GetComponent<OxygenBreather>();
			this.brain = base.GetComponent<MinionBrain>();
			this.dataHolder = base.GetComponent<MinionStorageDataHolder>();
			MinionStorageDataHolder minionStorageDataHolder = this.dataHolder;
			minionStorageDataHolder.OnCopyBegins = (Action<StoredMinionIdentity>)Delegate.Combine(minionStorageDataHolder.OnCopyBegins, new Action<StoredMinionIdentity>(this.OnCopyMinionBegins));
			this.oxygenSourceSensors = new ClosestPickupableSensor<Pickupable>[] { component.GetSensor<ClosestOxygenCanisterSensor>() };
			for (int i = 0; i < this.oxygenSourceSensors.Length; i++)
			{
				ClosestPickupableSensor<Pickupable> closestPickupableSensor = this.oxygenSourceSensors[i];
				closestPickupableSensor.OnItemChanged = (Action<Pickupable>)Delegate.Combine(closestPickupableSensor.OnItemChanged, new Action<Pickupable>(this.OnOxygenSourceSensorItemChanged));
			}
			this.storage = base.gameObject.GetComponents<Storage>().FindFirst((Storage s) => s.storageID == GameTags.StoragesIds.BionicOxygenTankStorage);
			this.oxygenTankAmountInstance = Db.Get().Amounts.BionicOxygenTank.Lookup(base.gameObject);
			this.airConsumptionRate = Db.Get().Attributes.AirConsumptionRate.Lookup(base.gameObject);
			Storage storage = this.storage;
			storage.OnStorageChange = (Action<GameObject>)Delegate.Combine(storage.OnStorageChange, new Action<GameObject>(this.OnOxygenTankStorageChanged));
			this.choreDriver = base.gameObject.GetComponent<ChoreDriver>();
		}

		// Token: 0x0600A19C RID: 41372 RVA: 0x0039F124 File Offset: 0x0039D324
		public bool ChoreIsRunning(ChoreType type)
		{
			if (this.choreDriver == null)
			{
				return false;
			}
			Chore currentChore = this.choreDriver.GetCurrentChore();
			return currentChore != null && currentChore.choreType == type;
		}

		// Token: 0x0600A19D RID: 41373 RVA: 0x0039F15C File Offset: 0x0039D35C
		public bool IsConsumingOxygen()
		{
			this.choreDriver = base.smi.GetComponent<ChoreDriver>();
			if (this.choreDriver == null)
			{
				return false;
			}
			BionicOxygenTankMonitor.IChore chore = this.choreDriver.GetCurrentChore() as BionicOxygenTankMonitor.IChore;
			return chore != null && chore.IsConsumingOxygen();
		}

		// Token: 0x0600A19E RID: 41374 RVA: 0x0039F1A6 File Offset: 0x0039D3A6
		public Pickupable GetClosestOxygenSource()
		{
			return this.closestOxygenSource;
		}

		// Token: 0x0600A19F RID: 41375 RVA: 0x0039F1AE File Offset: 0x0039D3AE
		private void OnOxygenSourceSensorItemChanged(object o)
		{
			this.CompareOxygenSources();
		}

		// Token: 0x0600A1A0 RID: 41376 RVA: 0x0039F1B6 File Offset: 0x0039D3B6
		private void OnOxygenTankStorageChanged(object o)
		{
			this.RefreshAmountInstance();
		}

		// Token: 0x0600A1A1 RID: 41377 RVA: 0x0039F1BE File Offset: 0x0039D3BE
		public void RefreshAmountInstance()
		{
			this.oxygenTankAmountInstance.SetValue(this.AvailableOxygen);
		}

		// Token: 0x0600A1A2 RID: 41378 RVA: 0x0039F1D4 File Offset: 0x0039D3D4
		public void AddFirstTimeSpawnedOxygen()
		{
			this.storage.AddElement(SimHashes.Oxygen, this.storage.capacityKg - this.AvailableOxygen, BionicOxygenTankMonitor.INITIAL_OXYGEN_TEMP, byte.MaxValue, 0, false, true);
			base.sm.HasSpawnedBefore.Set(true, this, false);
		}

		// Token: 0x0600A1A3 RID: 41379 RVA: 0x0039F228 File Offset: 0x0039D428
		private void OnCopyMinionBegins(StoredMinionIdentity destination)
		{
			MinionStorageDataHolder.DataPackData dataPackData = new MinionStorageDataHolder.DataPackData
			{
				Bools = new bool[] { base.sm.HasSpawnedBefore.Get(this) }
			};
			this.dataHolder.UpdateData(dataPackData);
		}

		// Token: 0x0600A1A4 RID: 41380 RVA: 0x0039F269 File Offset: 0x0039D469
		public override void StartSM()
		{
			base.StartSM();
			this.RefreshAmountInstance();
		}

		// Token: 0x0600A1A5 RID: 41381 RVA: 0x0039F278 File Offset: 0x0039D478
		public override void PostParamsInitialized()
		{
			MinionStorageDataHolder.DataPack dataPack = this.dataHolder.GetDataPack<BionicOxygenTankMonitor.Instance>();
			if (dataPack != null && dataPack.IsStoringNewData)
			{
				MinionStorageDataHolder.DataPackData dataPackData = dataPack.ReadData();
				if (dataPackData != null)
				{
					bool flag = dataPackData.Bools[0];
					base.sm.HasSpawnedBefore.Set(flag, this, false);
				}
			}
			base.PostParamsInitialized();
		}

		// Token: 0x0600A1A6 RID: 41382 RVA: 0x0039F2CC File Offset: 0x0039D4CC
		private void CompareOxygenSources()
		{
			Pickupable pickupable = null;
			float num = 2.1474836E+09f;
			for (int i = 0; i < this.oxygenSourceSensors.Length; i++)
			{
				ClosestPickupableSensor<Pickupable> closestPickupableSensor = this.oxygenSourceSensors[i];
				int itemNavCost = closestPickupableSensor.GetItemNavCost();
				if ((float)itemNavCost < num)
				{
					num = (float)itemNavCost;
					pickupable = closestPickupableSensor.GetItem();
				}
			}
			if (pickupable != null && base.IsInsideState(base.sm.critical))
			{
				float num2 = num / this.movementRate * this.oxygenBreather.ConsumptionRate;
				if (this.oxygenBreather.GetAmounts().Get(Db.Get().Amounts.Breath).value < num2)
				{
					pickupable = null;
				}
			}
			if (this.closestOxygenSource != pickupable)
			{
				this.closestOxygenSource = pickupable;
				base.sm.ClosestOxygenSourceChanged.Trigger(this);
			}
		}

		// Token: 0x0600A1A7 RID: 41383 RVA: 0x0039F39C File Offset: 0x0039D59C
		public void UpdatePotentialCellToAbsorbOxygen(int previouslyReservedCell)
		{
			float num = this.brain.GetAmounts().Get(Db.Get().Amounts.Breath).value / this.brain.GetAmounts().Get(Db.Get().Amounts.Breath).GetMax();
			this.query.Reset(this.brain, BionicOxygenTankMonitor.AreOxygenLevelsCritical(this), this.AvailableOxygen, num, previouslyReservedCell, this.isRecoveringFromSuffocation);
			this.navigator.RunQuery(base.smi.query);
			int num2 = base.smi.query.GetResultCell();
			if (num2 == Grid.PosToCell(base.gameObject) && !GasBreatherFromWorldProvider.GetBestBreathableCellAroundSpecificCell(num2, GasBreatherFromWorldProvider.DEFAULT_BREATHABLE_OFFSETS, this.oxygenBreather).IsBreathable)
			{
				num2 = PathFinder.InvalidCell;
			}
			bool flag = this.AbsorbOxygenCell != num2;
			this.AbsorbOxygenCell = num2;
			if (flag)
			{
				base.sm.AbsorbCellChangedSignal.Trigger(this);
			}
		}

		// Token: 0x0600A1A8 RID: 41384 RVA: 0x0039F492 File Offset: 0x0039D692
		public float AddGas(Sim.MassConsumedCallback mass_cb_info)
		{
			return this.AddGas(ElementLoader.elements[(int)mass_cb_info.elemIdx].id, mass_cb_info.mass, mass_cb_info.temperature, mass_cb_info.diseaseIdx, mass_cb_info.diseaseCount);
		}

		// Token: 0x0600A1A9 RID: 41385 RVA: 0x0039F4C8 File Offset: 0x0039D6C8
		public float AddGas(SimHashes element, float mass, float temperature, byte disseaseIDX = 255, int _disseaseCount = 0)
		{
			float num = Mathf.Min(mass, this.SpaceAvailableInTank);
			float num2 = mass - num;
			float num3 = num / mass;
			int num4 = Mathf.CeilToInt((float)_disseaseCount * num3);
			this.storage.AddElement(element, num, temperature, disseaseIDX, num4, false, true);
			return num2;
		}

		// Token: 0x0600A1AA RID: 41386 RVA: 0x0039F508 File Offset: 0x0039D708
		public void SetOxygenSourceSensorsActiveState(bool shouldItBeActive)
		{
			for (int i = 0; i < this.oxygenSourceSensors.Length; i++)
			{
				ClosestPickupableSensor<Pickupable> closestPickupableSensor = this.oxygenSourceSensors[i];
				closestPickupableSensor.SetActive(shouldItBeActive);
				if (shouldItBeActive)
				{
					closestPickupableSensor.Update();
				}
			}
		}

		// Token: 0x0600A1AB RID: 41387 RVA: 0x0039F544 File Offset: 0x0039D744
		public bool ConsumeGas(OxygenBreather oxygen_breather, float amount)
		{
			if (this.IsEmpty)
			{
				return false;
			}
			SimHashes simHashes = SimHashes.Vacuum;
			float num = 0f;
			float num2;
			SimUtil.DiseaseInfo diseaseInfo;
			this.storage.ConsumeAndGetDisease(GameTags.Breathable, amount, out num2, out diseaseInfo, out num, out simHashes);
			OxygenBreather.BreathableGasConsumed(oxygen_breather, simHashes, amount, num, diseaseInfo.idx, diseaseInfo.count);
			return true;
		}

		// Token: 0x0600A1AC RID: 41388 RVA: 0x0039F596 File Offset: 0x0039D796
		public void OnSetOxygenBreather(OxygenBreather oxygen_breather)
		{
		}

		// Token: 0x0600A1AD RID: 41389 RVA: 0x0039F598 File Offset: 0x0039D798
		public void OnClearOxygenBreather(OxygenBreather oxygen_breather)
		{
		}

		// Token: 0x0600A1AE RID: 41390 RVA: 0x0039F59A File Offset: 0x0039D79A
		public bool IsLowOxygen()
		{
			return this.OxygenPercentage <= 0f;
		}

		// Token: 0x0600A1AF RID: 41391 RVA: 0x0039F5AC File Offset: 0x0039D7AC
		public bool HasOxygen()
		{
			return !this.IsEmpty;
		}

		// Token: 0x0600A1B0 RID: 41392 RVA: 0x0039F5B7 File Offset: 0x0039D7B7
		public bool IsBlocked()
		{
			return false;
		}

		// Token: 0x0600A1B1 RID: 41393 RVA: 0x0039F5BA File Offset: 0x0039D7BA
		public bool ShouldEmitCO2()
		{
			return false;
		}

		// Token: 0x0600A1B2 RID: 41394 RVA: 0x0039F5BD File Offset: 0x0039D7BD
		public bool ShouldStoreCO2()
		{
			return false;
		}

		// Token: 0x0600A1B3 RID: 41395 RVA: 0x0039F5C0 File Offset: 0x0039D7C0
		protected override void OnCleanUp()
		{
			if (this.dataHolder != null)
			{
				MinionStorageDataHolder minionStorageDataHolder = this.dataHolder;
				minionStorageDataHolder.OnCopyBegins = (Action<StoredMinionIdentity>)Delegate.Remove(minionStorageDataHolder.OnCopyBegins, new Action<StoredMinionIdentity>(this.OnCopyMinionBegins));
			}
			if (this.storage != null)
			{
				Storage storage = this.storage;
				storage.OnStorageChange = (Action<GameObject>)Delegate.Remove(storage.OnStorageChange, new Action<GameObject>(this.OnOxygenTankStorageChanged));
			}
			base.OnCleanUp();
		}

		// Token: 0x04007E53 RID: 32339
		public AttributeInstance airConsumptionRate;

		// Token: 0x04007E54 RID: 32340
		private Schedulable schedulable;

		// Token: 0x04007E55 RID: 32341
		private AmountInstance oxygenTankAmountInstance;

		// Token: 0x04007E56 RID: 32342
		private ClosestPickupableSensor<Pickupable>[] oxygenSourceSensors;

		// Token: 0x04007E57 RID: 32343
		private Pickupable closestOxygenSource;

		// Token: 0x04007E58 RID: 32344
		private Navigator navigator;

		// Token: 0x04007E59 RID: 32345
		private float movementRate;

		// Token: 0x04007E5A RID: 32346
		private AbsorbCellQuery query;

		// Token: 0x04007E5B RID: 32347
		private OxygenBreather oxygenBreather;

		// Token: 0x04007E5C RID: 32348
		private MinionBrain brain;

		// Token: 0x04007E5D RID: 32349
		private MinionStorageDataHolder dataHolder;

		// Token: 0x04007E5E RID: 32350
		private ChoreDriver choreDriver;

		// Token: 0x04007E5F RID: 32351
		public bool isRecoveringFromSuffocation;
	}
}
