using System;
using Klei;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200033C RID: 828
public class MorbRoverMaker : GameStateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>
{
	// Token: 0x0600111A RID: 4378 RVA: 0x00064220 File Offset: 0x00062420
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.no_operational;
		this.root.Update(new Action<MorbRoverMaker.Instance, float>(MorbRoverMaker.GermsRequiredFeedbackUpdate), UpdateRate.SIM_1000ms, false);
		this.no_operational.Enter(delegate(MorbRoverMaker.Instance smi)
		{
			MorbRoverMaker.DisableManualDelivery(smi, "Disable manual delivery while no operational. in case players disabled the machine on purpose for this reason");
		}).TagTransition(GameTags.Operational, this.operational, false);
		this.operational.TagTransition(GameTags.Operational, this.no_operational, true).DefaultState(this.operational.covered);
		this.operational.covered.ToggleStatusItem(Db.Get().BuildingStatusItems.MorbRoverMakerDusty, null).ParamTransition<bool>(this.WasUncoverByDuplicant, this.operational.idle, GameStateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.IsTrue).Enter(delegate(MorbRoverMaker.Instance smi)
		{
			MorbRoverMaker.DisableManualDelivery(smi, "Machine can't ask for materials if it has not been investigated by a dupe");
		})
			.DefaultState(this.operational.covered.idle);
		this.operational.covered.idle.PlayAnim("dusty").ParamTransition<bool>(this.UncoverOrderRequested, this.operational.covered.careOrderGiven, GameStateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.IsTrue);
		this.operational.covered.careOrderGiven.PlayAnim("dusty").Enter(new StateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.State.Callback(MorbRoverMaker.StartWorkChore_RevealMachine)).Exit(new StateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.State.Callback(MorbRoverMaker.CancelWorkChore_RevealMachine))
			.WorkableCompleteTransition((MorbRoverMaker.Instance smi) => smi.GetWorkable_RevealMachine(), this.operational.covered.complete)
			.ParamTransition<bool>(this.UncoverOrderRequested, this.operational.covered.idle, GameStateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.IsFalse);
		this.operational.covered.complete.Enter(new StateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.State.Callback(MorbRoverMaker.SetUncovered));
		this.operational.idle.Enter(delegate(MorbRoverMaker.Instance smi)
		{
			MorbRoverMaker.EnableManualDelivery(smi, "Operational and discovered");
		}).EnterTransition(this.operational.crafting, new StateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.Transition.ConditionCallback(MorbRoverMaker.ShouldBeCrafting)).EnterTransition(this.operational.waitingForMorb, new StateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.Transition.ConditionCallback(MorbRoverMaker.IsCraftingCompleted))
			.EventTransition(GameHashes.OnStorageChange, this.operational.crafting, new StateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.Transition.ConditionCallback(MorbRoverMaker.ShouldBeCrafting))
			.PlayAnim("idle")
			.ToggleStatusItem(Db.Get().BuildingStatusItems.MorbRoverMakerGermCollectionProgress, null);
		this.operational.crafting.DefaultState(this.operational.crafting.pre).ToggleStatusItem(Db.Get().BuildingStatusItems.MorbRoverMakerGermCollectionProgress, null).ToggleStatusItem(Db.Get().BuildingStatusItems.MorbRoverMakerCraftingBody, null);
		this.operational.crafting.conflict.Enter(new StateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.State.Callback(MorbRoverMaker.ResetRoverBodyCraftingProgress)).GoTo(this.operational.idle);
		this.operational.crafting.pre.EventTransition(GameHashes.OnStorageChange, this.operational.crafting.conflict, GameStateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.Not(new StateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.Transition.ConditionCallback(MorbRoverMaker.ShouldBeCrafting))).PlayAnim("crafting_pre").OnAnimQueueComplete(this.operational.crafting.loop);
		this.operational.crafting.loop.EventTransition(GameHashes.OnStorageChange, this.operational.crafting.conflict, GameStateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.Not(new StateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.Transition.ConditionCallback(MorbRoverMaker.ShouldBeCrafting))).Update(new Action<MorbRoverMaker.Instance, float>(MorbRoverMaker.CraftingUpdate), UpdateRate.SIM_200ms, false).PlayAnim("crafting_loop", KAnim.PlayMode.Loop)
			.ParamTransition<float>(this.CraftProgress, this.operational.crafting.pst, GameStateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.IsOne);
		this.operational.crafting.pst.Enter(new StateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.State.Callback(MorbRoverMaker.ConsumeRoverBodyCraftingMaterials)).PlayAnim("crafting_pst").OnAnimQueueComplete(this.operational.waitingForMorb);
		this.operational.waitingForMorb.PlayAnim("crafting_complete").ParamTransition<long>(this.Germs, this.operational.doctor, new StateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.Parameter<long>.Callback(MorbRoverMaker.HasEnoughGerms)).ToggleStatusItem(Db.Get().BuildingStatusItems.MorbRoverMakerGermCollectionProgress, null);
		this.operational.doctor.Enter(new StateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.State.Callback(MorbRoverMaker.StartWorkChore_ReleaseRover)).Exit(new StateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.State.Callback(MorbRoverMaker.CancelWorkChore_ReleaseRover)).WorkableCompleteTransition((MorbRoverMaker.Instance smi) => smi.GetWorkable_ReleaseRover(), this.operational.finish)
			.DefaultState(this.operational.doctor.needed);
		this.operational.doctor.needed.PlayAnim("waiting", KAnim.PlayMode.Loop).WorkableStartTransition((MorbRoverMaker.Instance smi) => smi.GetWorkable_ReleaseRover(), this.operational.doctor.working).ToggleStatusItem(Db.Get().BuildingStatusItems.MorbRoverMakerReadyForDoctor, null);
		this.operational.doctor.working.WorkableStopTransition((MorbRoverMaker.Instance smi) => smi.GetWorkable_ReleaseRover(), this.operational.doctor.needed);
		this.operational.finish.Enter(new StateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.State.Callback(MorbRoverMaker.SpawnRover)).GoTo(this.operational.idle);
	}

	// Token: 0x0600111B RID: 4379 RVA: 0x000647E9 File Offset: 0x000629E9
	public static bool ShouldBeCrafting(MorbRoverMaker.Instance smi)
	{
		return smi.HasMaterialsForRover && smi.RoverDevelopment_Progress < 1f;
	}

	// Token: 0x0600111C RID: 4380 RVA: 0x00064802 File Offset: 0x00062A02
	public static bool IsCraftingCompleted(MorbRoverMaker.Instance smi)
	{
		return smi.RoverDevelopment_Progress == 1f;
	}

	// Token: 0x0600111D RID: 4381 RVA: 0x00064811 File Offset: 0x00062A11
	public static bool HasEnoughGerms(MorbRoverMaker.Instance smi, long germCount)
	{
		return germCount >= smi.def.GERMS_PER_ROVER;
	}

	// Token: 0x0600111E RID: 4382 RVA: 0x00064824 File Offset: 0x00062A24
	public static void StartWorkChore_ReleaseRover(MorbRoverMaker.Instance smi)
	{
		smi.CreateWorkChore_ReleaseRover();
	}

	// Token: 0x0600111F RID: 4383 RVA: 0x0006482C File Offset: 0x00062A2C
	public static void CancelWorkChore_ReleaseRover(MorbRoverMaker.Instance smi)
	{
		smi.CancelWorkChore_ReleaseRover();
	}

	// Token: 0x06001120 RID: 4384 RVA: 0x00064834 File Offset: 0x00062A34
	public static void StartWorkChore_RevealMachine(MorbRoverMaker.Instance smi)
	{
		smi.CreateWorkChore_RevealMachine();
	}

	// Token: 0x06001121 RID: 4385 RVA: 0x0006483C File Offset: 0x00062A3C
	public static void CancelWorkChore_RevealMachine(MorbRoverMaker.Instance smi)
	{
		smi.CancelWorkChore_RevealMachine();
	}

	// Token: 0x06001122 RID: 4386 RVA: 0x00064844 File Offset: 0x00062A44
	public static void SetUncovered(MorbRoverMaker.Instance smi)
	{
		smi.Uncover();
	}

	// Token: 0x06001123 RID: 4387 RVA: 0x0006484C File Offset: 0x00062A4C
	public static void SpawnRover(MorbRoverMaker.Instance smi)
	{
		smi.SpawnRover();
	}

	// Token: 0x06001124 RID: 4388 RVA: 0x00064854 File Offset: 0x00062A54
	public static void EnableManualDelivery(MorbRoverMaker.Instance smi, string reason)
	{
		smi.EnableManualDelivery(reason);
	}

	// Token: 0x06001125 RID: 4389 RVA: 0x0006485D File Offset: 0x00062A5D
	public static void DisableManualDelivery(MorbRoverMaker.Instance smi, string reason)
	{
		smi.DisableManualDelivery(reason);
	}

	// Token: 0x06001126 RID: 4390 RVA: 0x00064866 File Offset: 0x00062A66
	public static void ConsumeRoverBodyCraftingMaterials(MorbRoverMaker.Instance smi)
	{
		smi.ConsumeRoverBodyCraftingMaterials();
	}

	// Token: 0x06001127 RID: 4391 RVA: 0x0006486E File Offset: 0x00062A6E
	public static void ResetRoverBodyCraftingProgress(MorbRoverMaker.Instance smi)
	{
		smi.SetRoverDevelopmentProgress(0f);
	}

	// Token: 0x06001128 RID: 4392 RVA: 0x0006487C File Offset: 0x00062A7C
	public static void CraftingUpdate(MorbRoverMaker.Instance smi, float dt)
	{
		float num = Mathf.Clamp((smi.RoverDevelopment_Progress * smi.def.ROVER_CRAFTING_DURATION + dt) / smi.def.ROVER_CRAFTING_DURATION, 0f, 1f);
		smi.SetRoverDevelopmentProgress(num);
	}

	// Token: 0x06001129 RID: 4393 RVA: 0x000648C0 File Offset: 0x00062AC0
	public static void GermsRequiredFeedbackUpdate(MorbRoverMaker.Instance smi, float dt)
	{
		if ((GameClock.Instance.GetTime() - smi.lastTimeGermsAdded > smi.def.FEEDBACK_NO_GERMS_DETECTED_TIMEOUT) & (smi.MorbDevelopment_Progress < 1f) & !smi.IsInsideState(smi.sm.operational.doctor) & smi.HasBeenRevealed)
		{
			smi.ShowGermRequiredStatusItemAlert();
			return;
		}
		smi.HideGermRequiredStatusItemAlert();
	}

	// Token: 0x04000AB9 RID: 2745
	private const string ROBOT_PROGRESS_METER_TARGET_NAME = "meter_robot_target";

	// Token: 0x04000ABA RID: 2746
	private const string ROBOT_PROGRESS_METER_ANIMATION_NAME = "meter_robot";

	// Token: 0x04000ABB RID: 2747
	private const string COVERED_IDLE_ANIM_NAME = "dusty";

	// Token: 0x04000ABC RID: 2748
	private const string IDLE_ANIM_NAME = "idle";

	// Token: 0x04000ABD RID: 2749
	private const string CRAFT_PRE_ANIM_NAME = "crafting_pre";

	// Token: 0x04000ABE RID: 2750
	private const string CRAFT_LOOP_ANIM_NAME = "crafting_loop";

	// Token: 0x04000ABF RID: 2751
	private const string CRAFT_PST_ANIM_NAME = "crafting_pst";

	// Token: 0x04000AC0 RID: 2752
	private const string CRAFT_COMPLETED_ANIM_NAME = "crafting_complete";

	// Token: 0x04000AC1 RID: 2753
	private const string WAITING_FOR_DOCTOR_ANIM_NAME = "waiting";

	// Token: 0x04000AC2 RID: 2754
	public StateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.BoolParameter UncoverOrderRequested;

	// Token: 0x04000AC3 RID: 2755
	public StateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.BoolParameter WasUncoverByDuplicant;

	// Token: 0x04000AC4 RID: 2756
	public StateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.LongParameter Germs;

	// Token: 0x04000AC5 RID: 2757
	public StateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.FloatParameter CraftProgress;

	// Token: 0x04000AC6 RID: 2758
	public GameStateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.State no_operational;

	// Token: 0x04000AC7 RID: 2759
	public MorbRoverMaker.OperationalStates operational;

	// Token: 0x020011E1 RID: 4577
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x0600842E RID: 33838 RVA: 0x00335000 File Offset: 0x00333200
		public float GetConduitMaxPackageMass()
		{
			ConduitType germ_INTAKE_CONDUIT_TYPE = this.GERM_INTAKE_CONDUIT_TYPE;
			if (germ_INTAKE_CONDUIT_TYPE == ConduitType.Gas)
			{
				return 1f;
			}
			if (germ_INTAKE_CONDUIT_TYPE != ConduitType.Liquid)
			{
				return 1f;
			}
			return 10f;
		}

		// Token: 0x0400646C RID: 25708
		public float FEEDBACK_NO_GERMS_DETECTED_TIMEOUT = 2f;

		// Token: 0x0400646D RID: 25709
		public Tag ROVER_PREFAB_ID;

		// Token: 0x0400646E RID: 25710
		public float INITIAL_MORB_DEVELOPMENT_PERCENTAGE;

		// Token: 0x0400646F RID: 25711
		public float ROVER_CRAFTING_DURATION;

		// Token: 0x04006470 RID: 25712
		public float METAL_PER_ROVER;

		// Token: 0x04006471 RID: 25713
		public long GERMS_PER_ROVER;

		// Token: 0x04006472 RID: 25714
		public int MAX_GERMS_TAKEN_PER_PACKAGE;

		// Token: 0x04006473 RID: 25715
		public int GERM_TYPE;

		// Token: 0x04006474 RID: 25716
		public SimHashes ROVER_MATERIAL;

		// Token: 0x04006475 RID: 25717
		public ConduitType GERM_INTAKE_CONDUIT_TYPE;
	}

	// Token: 0x020011E2 RID: 4578
	public class CoverStates : GameStateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.State
	{
		// Token: 0x04006476 RID: 25718
		public GameStateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.State idle;

		// Token: 0x04006477 RID: 25719
		public GameStateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.State careOrderGiven;

		// Token: 0x04006478 RID: 25720
		public GameStateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.State complete;
	}

	// Token: 0x020011E3 RID: 4579
	public class OperationalStates : GameStateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.State
	{
		// Token: 0x04006479 RID: 25721
		public MorbRoverMaker.CoverStates covered;

		// Token: 0x0400647A RID: 25722
		public GameStateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.State idle;

		// Token: 0x0400647B RID: 25723
		public MorbRoverMaker.CraftingStates crafting;

		// Token: 0x0400647C RID: 25724
		public GameStateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.State waitingForMorb;

		// Token: 0x0400647D RID: 25725
		public MorbRoverMaker.DoctorStates doctor;

		// Token: 0x0400647E RID: 25726
		public GameStateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.State finish;
	}

	// Token: 0x020011E4 RID: 4580
	public class DoctorStates : GameStateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.State
	{
		// Token: 0x0400647F RID: 25727
		public GameStateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.State needed;

		// Token: 0x04006480 RID: 25728
		public GameStateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.State working;
	}

	// Token: 0x020011E5 RID: 4581
	public class CraftingStates : GameStateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.State
	{
		// Token: 0x04006481 RID: 25729
		public GameStateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.State conflict;

		// Token: 0x04006482 RID: 25730
		public GameStateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.State pre;

		// Token: 0x04006483 RID: 25731
		public GameStateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.State loop;

		// Token: 0x04006484 RID: 25732
		public GameStateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.State pst;
	}

	// Token: 0x020011E6 RID: 4582
	public new class Instance : GameStateMachine<MorbRoverMaker, MorbRoverMaker.Instance, IStateMachineTarget, MorbRoverMaker.Def>.GameInstance, ISidescreenButtonControl
	{
		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06008434 RID: 33844 RVA: 0x00335062 File Offset: 0x00333262
		public long MorbDevelopment_GermsCollected
		{
			get
			{
				return base.sm.Germs.Get(base.smi);
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06008435 RID: 33845 RVA: 0x0033507A File Offset: 0x0033327A
		public long MorbDevelopment_RemainingGerms
		{
			get
			{
				return base.def.GERMS_PER_ROVER - this.MorbDevelopment_GermsCollected;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06008436 RID: 33846 RVA: 0x0033508E File Offset: 0x0033328E
		public float MorbDevelopment_Progress
		{
			get
			{
				return Mathf.Clamp((float)this.MorbDevelopment_GermsCollected / (float)base.def.GERMS_PER_ROVER, 0f, 1f);
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06008437 RID: 33847 RVA: 0x003350B3 File Offset: 0x003332B3
		public bool HasMaterialsForRover
		{
			get
			{
				return this.storage.GetMassAvailable(base.def.ROVER_MATERIAL) >= base.def.METAL_PER_ROVER;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06008438 RID: 33848 RVA: 0x003350DB File Offset: 0x003332DB
		public float RoverDevelopment_Progress
		{
			get
			{
				return base.sm.CraftProgress.Get(base.smi);
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06008439 RID: 33849 RVA: 0x003350F3 File Offset: 0x003332F3
		public bool HasBeenRevealed
		{
			get
			{
				return base.sm.WasUncoverByDuplicant.Get(base.smi);
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x0600843A RID: 33850 RVA: 0x0033510B File Offset: 0x0033330B
		public bool CanPumpGerms
		{
			get
			{
				return this.operational && this.MorbDevelopment_Progress < 1f && this.HasBeenRevealed;
			}
		}

		// Token: 0x0600843B RID: 33851 RVA: 0x0033512F File Offset: 0x0033332F
		public Workable GetWorkable_RevealMachine()
		{
			return this.workable_reveal;
		}

		// Token: 0x0600843C RID: 33852 RVA: 0x00335137 File Offset: 0x00333337
		public Workable GetWorkable_ReleaseRover()
		{
			return this.workable_release;
		}

		// Token: 0x0600843D RID: 33853 RVA: 0x00335140 File Offset: 0x00333340
		public void ShowGermRequiredStatusItemAlert()
		{
			if (this.germsRequiredAlertStatusItemHandle == default(Guid))
			{
				this.germsRequiredAlertStatusItemHandle = this.selectable.AddStatusItem(Db.Get().BuildingStatusItems.MorbRoverMakerNoGermsConsumedAlert, base.smi);
			}
		}

		// Token: 0x0600843E RID: 33854 RVA: 0x0033518C File Offset: 0x0033338C
		public void HideGermRequiredStatusItemAlert()
		{
			if (this.germsRequiredAlertStatusItemHandle != default(Guid))
			{
				this.selectable.RemoveStatusItem(this.germsRequiredAlertStatusItemHandle, false);
				this.germsRequiredAlertStatusItemHandle = default(Guid);
			}
		}

		// Token: 0x0600843F RID: 33855 RVA: 0x003351D0 File Offset: 0x003333D0
		public Instance(IStateMachineTarget master, MorbRoverMaker.Def def)
			: base(master, def)
		{
			this.RobotProgressMeter = new MeterController(this.buildingAnimCtr, "meter_robot_target", "meter_robot", Meter.Offset.UserSpecified, Grid.SceneLayer.BuildingFront, Array.Empty<string>());
		}

		// Token: 0x06008440 RID: 33856 RVA: 0x00335238 File Offset: 0x00333438
		public override void StartSM()
		{
			Building component = base.GetComponent<Building>();
			this.inputCell = component.GetUtilityInputCell();
			this.outputCell = component.GetUtilityOutputCell();
			base.StartSM();
			if (!this.HasBeenRevealed)
			{
				base.sm.Germs.Set(0L, base.smi, false);
				this.AddGerms((long)((float)base.def.GERMS_PER_ROVER * base.def.INITIAL_MORB_DEVELOPMENT_PERCENTAGE), false);
			}
			Conduit.GetFlowManager(base.def.GERM_INTAKE_CONDUIT_TYPE).AddConduitUpdater(new Action<float>(this.Flow), ConduitFlowPriority.Default);
			this.UpdateMeters();
		}

		// Token: 0x06008441 RID: 33857 RVA: 0x003352D4 File Offset: 0x003334D4
		public void AddGerms(long amount, bool playAnimations = true)
		{
			long num = this.MorbDevelopment_GermsCollected + amount;
			base.sm.Germs.Set(num, base.smi, false);
			this.UpdateMeters();
			if (amount > 0L)
			{
				if (playAnimations)
				{
					this.capsule.PlayPumpGermsAnimation();
				}
				Action<long> germsAdded = this.GermsAdded;
				if (germsAdded != null)
				{
					germsAdded(amount);
				}
				this.lastTimeGermsAdded = GameClock.Instance.GetTime();
			}
		}

		// Token: 0x06008442 RID: 33858 RVA: 0x00335340 File Offset: 0x00333540
		public long RemoveGerms(long amount)
		{
			long num = amount.Min(this.MorbDevelopment_GermsCollected);
			long num2 = this.MorbDevelopment_GermsCollected - num;
			base.sm.Germs.Set(num2, base.smi, false);
			this.UpdateMeters();
			return num;
		}

		// Token: 0x06008443 RID: 33859 RVA: 0x00335383 File Offset: 0x00333583
		public void EnableManualDelivery(string reason)
		{
			this.manualDelivery.Pause(false, reason);
		}

		// Token: 0x06008444 RID: 33860 RVA: 0x00335392 File Offset: 0x00333592
		public void DisableManualDelivery(string reason)
		{
			this.manualDelivery.Pause(true, reason);
		}

		// Token: 0x06008445 RID: 33861 RVA: 0x003353A1 File Offset: 0x003335A1
		public void SetRoverDevelopmentProgress(float value)
		{
			base.sm.CraftProgress.Set(value, base.smi, false);
			this.UpdateMeters();
		}

		// Token: 0x06008446 RID: 33862 RVA: 0x003353C4 File Offset: 0x003335C4
		public void UpdateMeters()
		{
			this.RobotProgressMeter.SetPositionPercent(this.RoverDevelopment_Progress);
			this.capsule.SetMorbDevelopmentProgress(this.MorbDevelopment_Progress);
			this.capsule.SetGermMeterProgress(this.HasBeenRevealed ? this.MorbDevelopment_Progress : 0f);
		}

		// Token: 0x06008447 RID: 33863 RVA: 0x00335413 File Offset: 0x00333613
		public void Uncover()
		{
			base.sm.WasUncoverByDuplicant.Set(true, base.smi, false);
			global::System.Action onUncovered = this.OnUncovered;
			if (onUncovered == null)
			{
				return;
			}
			onUncovered();
		}

		// Token: 0x06008448 RID: 33864 RVA: 0x00335440 File Offset: 0x00333640
		public void CreateWorkChore_ReleaseRover()
		{
			if (this.workChore_releaseRover == null)
			{
				this.workChore_releaseRover = new WorkChore<MorbRoverMakerWorkable>(Db.Get().ChoreTypes.Doctor, this.workable_release, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
			}
		}

		// Token: 0x06008449 RID: 33865 RVA: 0x00335486 File Offset: 0x00333686
		public void CancelWorkChore_ReleaseRover()
		{
			if (this.workChore_releaseRover != null)
			{
				this.workChore_releaseRover.Cancel("MorbRoverMaker.CancelWorkChore_ReleaseRover");
				this.workChore_releaseRover = null;
			}
		}

		// Token: 0x0600844A RID: 33866 RVA: 0x003354A8 File Offset: 0x003336A8
		public void CreateWorkChore_RevealMachine()
		{
			if (this.workChore_revealMachine == null)
			{
				this.workChore_revealMachine = new WorkChore<MorbRoverMakerRevealWorkable>(Db.Get().ChoreTypes.Repair, this.workable_reveal, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
			}
		}

		// Token: 0x0600844B RID: 33867 RVA: 0x003354EE File Offset: 0x003336EE
		public void CancelWorkChore_RevealMachine()
		{
			if (this.workChore_revealMachine != null)
			{
				this.workChore_revealMachine.Cancel("MorbRoverMaker.CancelWorkChore_RevealMachine");
				this.workChore_revealMachine = null;
			}
		}

		// Token: 0x0600844C RID: 33868 RVA: 0x00335510 File Offset: 0x00333710
		public void ConsumeRoverBodyCraftingMaterials()
		{
			float num = 0f;
			this.storage.ConsumeAndGetDisease(base.def.ROVER_MATERIAL.CreateTag(), base.def.METAL_PER_ROVER, out num, out this.lastastMaterialsConsumedDiseases, out this.lastastMaterialsConsumedTemp);
		}

		// Token: 0x0600844D RID: 33869 RVA: 0x00335558 File Offset: 0x00333758
		public void SpawnRover()
		{
			if (this.RoverDevelopment_Progress == 1f)
			{
				this.RemoveGerms(base.def.GERMS_PER_ROVER);
				GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab(base.def.ROVER_PREFAB_ID), base.gameObject.transform.GetPosition(), Grid.SceneLayer.Creatures, null, 0);
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (this.lastastMaterialsConsumedDiseases.idx != 255)
				{
					component.AddDisease(this.lastastMaterialsConsumedDiseases.idx, this.lastastMaterialsConsumedDiseases.count, "From the materials provided for its creation");
				}
				if (this.lastastMaterialsConsumedTemp > 0f)
				{
					component.SetMassTemperature(component.Mass, this.lastastMaterialsConsumedTemp);
				}
				gameObject.SetActive(true);
				this.SetRoverDevelopmentProgress(0f);
				Action<GameObject> onRoverSpawned = this.OnRoverSpawned;
				if (onRoverSpawned == null)
				{
					return;
				}
				onRoverSpawned(gameObject);
			}
		}

		// Token: 0x0600844E RID: 33870 RVA: 0x00335630 File Offset: 0x00333830
		private void Flow(float dt)
		{
			if (this.CanPumpGerms)
			{
				ConduitFlow flowManager = Conduit.GetFlowManager(base.def.GERM_INTAKE_CONDUIT_TYPE);
				int num = 0;
				if (flowManager.HasConduit(this.inputCell) && flowManager.HasConduit(this.outputCell))
				{
					ConduitFlow.ConduitContents contents = flowManager.GetContents(this.inputCell);
					ConduitFlow.ConduitContents contents2 = flowManager.GetContents(this.outputCell);
					float num2 = Mathf.Min(contents.mass, base.def.GetConduitMaxPackageMass() * dt);
					if (flowManager.CanMergeContents(contents, contents2, num2))
					{
						float amountAllowedForMerging = flowManager.GetAmountAllowedForMerging(contents, contents2, num2);
						if (amountAllowedForMerging > 0f)
						{
							ConduitFlow conduitFlow = ((base.def.GERM_INTAKE_CONDUIT_TYPE == ConduitType.Liquid) ? Game.Instance.liquidConduitFlow : Game.Instance.gasConduitFlow);
							int num3 = contents.diseaseCount;
							if (contents.diseaseIdx != 255 && (int)contents.diseaseIdx == base.def.GERM_TYPE)
							{
								num = (int)this.MorbDevelopment_RemainingGerms.Min((long)base.def.MAX_GERMS_TAKEN_PER_PACKAGE).Min((long)contents.diseaseCount);
								num3 -= num;
							}
							float num4 = conduitFlow.AddElement(this.outputCell, contents.element, amountAllowedForMerging, contents.temperature, contents.diseaseIdx, num3);
							if (amountAllowedForMerging != num4)
							{
								global::Debug.Log("[Morb Rover Maker] Mass Differs By: " + (amountAllowedForMerging - num4).ToString());
							}
							flowManager.RemoveElement(this.inputCell, num4);
						}
					}
				}
				if (num > 0)
				{
					this.AddGerms((long)num, true);
				}
			}
		}

		// Token: 0x0600844F RID: 33871 RVA: 0x003357B2 File Offset: 0x003339B2
		protected override void OnCleanUp()
		{
			base.OnCleanUp();
			Conduit.GetFlowManager(base.def.GERM_INTAKE_CONDUIT_TYPE).RemoveConduitUpdater(new Action<float>(this.Flow));
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06008450 RID: 33872 RVA: 0x003357DB File Offset: 0x003339DB
		public string SidescreenButtonText
		{
			get
			{
				return this.HasBeenRevealed ? CODEX.STORY_TRAITS.MORB_ROVER_MAKER.UI_SIDESCREENS.DROP_INVENTORY : (base.sm.UncoverOrderRequested.Get(base.smi) ? CODEX.STORY_TRAITS.MORB_ROVER_MAKER.UI_SIDESCREENS.CANCEL_REVEAL_BTN : CODEX.STORY_TRAITS.MORB_ROVER_MAKER.UI_SIDESCREENS.REVEAL_BTN);
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06008451 RID: 33873 RVA: 0x00335815 File Offset: 0x00333A15
		public string SidescreenButtonTooltip
		{
			get
			{
				return this.HasBeenRevealed ? CODEX.STORY_TRAITS.MORB_ROVER_MAKER.UI_SIDESCREENS.DROP_INVENTORY_TOOLTIP : (base.sm.UncoverOrderRequested.Get(base.smi) ? CODEX.STORY_TRAITS.MORB_ROVER_MAKER.UI_SIDESCREENS.CANCEL_REVEAL_BTN_TOOLTIP : CODEX.STORY_TRAITS.MORB_ROVER_MAKER.UI_SIDESCREENS.REVEAL_BTN_TOOLTIP);
			}
		}

		// Token: 0x06008452 RID: 33874 RVA: 0x0033584F File Offset: 0x00333A4F
		public bool SidescreenEnabled()
		{
			return true;
		}

		// Token: 0x06008453 RID: 33875 RVA: 0x00335852 File Offset: 0x00333A52
		public bool SidescreenButtonInteractable()
		{
			return true;
		}

		// Token: 0x06008454 RID: 33876 RVA: 0x00335855 File Offset: 0x00333A55
		public int HorizontalGroupID()
		{
			return 0;
		}

		// Token: 0x06008455 RID: 33877 RVA: 0x00335858 File Offset: 0x00333A58
		public int ButtonSideScreenSortOrder()
		{
			return 20;
		}

		// Token: 0x06008456 RID: 33878 RVA: 0x0033585C File Offset: 0x00333A5C
		public void SetButtonTextOverride(ButtonMenuTextOverride textOverride)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008457 RID: 33879 RVA: 0x00335864 File Offset: 0x00333A64
		public void OnSidescreenButtonPressed()
		{
			if (this.HasBeenRevealed)
			{
				this.storage.DropAll(false, false, default(Vector3), true, null);
				return;
			}
			bool flag = base.smi.sm.UncoverOrderRequested.Get(base.smi);
			base.smi.sm.UncoverOrderRequested.Set(!flag, base.smi, false);
		}

		// Token: 0x04006485 RID: 25733
		public Action<long> GermsAdded;

		// Token: 0x04006486 RID: 25734
		public global::System.Action OnUncovered;

		// Token: 0x04006487 RID: 25735
		public Action<GameObject> OnRoverSpawned;

		// Token: 0x04006488 RID: 25736
		[MyCmpGet]
		private MorbRoverMakerRevealWorkable workable_reveal;

		// Token: 0x04006489 RID: 25737
		[MyCmpGet]
		private MorbRoverMakerWorkable workable_release;

		// Token: 0x0400648A RID: 25738
		[MyCmpGet]
		private Operational operational;

		// Token: 0x0400648B RID: 25739
		[MyCmpGet]
		private KBatchedAnimController buildingAnimCtr;

		// Token: 0x0400648C RID: 25740
		[MyCmpGet]
		private ManualDeliveryKG manualDelivery;

		// Token: 0x0400648D RID: 25741
		[MyCmpGet]
		private Storage storage;

		// Token: 0x0400648E RID: 25742
		[MyCmpGet]
		private MorbRoverMaker_Capsule capsule;

		// Token: 0x0400648F RID: 25743
		[MyCmpGet]
		private KSelectable selectable;

		// Token: 0x04006490 RID: 25744
		private MeterController RobotProgressMeter;

		// Token: 0x04006491 RID: 25745
		private int inputCell = -1;

		// Token: 0x04006492 RID: 25746
		private int outputCell = -1;

		// Token: 0x04006493 RID: 25747
		private Chore workChore_revealMachine;

		// Token: 0x04006494 RID: 25748
		private Chore workChore_releaseRover;

		// Token: 0x04006495 RID: 25749
		[Serialize]
		private float lastastMaterialsConsumedTemp = -1f;

		// Token: 0x04006496 RID: 25750
		[Serialize]
		private SimUtil.DiseaseInfo lastastMaterialsConsumedDiseases = SimUtil.DiseaseInfo.Invalid;

		// Token: 0x04006497 RID: 25751
		public float lastTimeGermsAdded = -1f;

		// Token: 0x04006498 RID: 25752
		private Guid germsRequiredAlertStatusItemHandle;
	}
}
