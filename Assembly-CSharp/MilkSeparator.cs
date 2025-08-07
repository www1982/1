using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000789 RID: 1929
public class MilkSeparator : GameStateMachine<MilkSeparator, MilkSeparator.Instance, IStateMachineTarget, MilkSeparator.Def>
{
	// Token: 0x060032EA RID: 13034 RVA: 0x0011E908 File Offset: 0x0011CB08
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.noOperational;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.root.EventHandler(GameHashes.OnStorageChange, new StateMachine<MilkSeparator, MilkSeparator.Instance, IStateMachineTarget, MilkSeparator.Def>.State.Callback(MilkSeparator.RefreshMeters));
		this.noOperational.TagTransition(GameTags.Operational, this.operational, false).PlayAnim("off");
		this.operational.TagTransition(GameTags.Operational, this.noOperational, true).PlayAnim("on").DefaultState(this.operational.idle);
		this.operational.idle.EventTransition(GameHashes.OnStorageChange, this.operational.working.pre, new StateMachine<MilkSeparator, MilkSeparator.Instance, IStateMachineTarget, MilkSeparator.Def>.Transition.ConditionCallback(MilkSeparator.CanBeginSeparate)).EnterTransition(this.operational.full, new StateMachine<MilkSeparator, MilkSeparator.Instance, IStateMachineTarget, MilkSeparator.Def>.Transition.ConditionCallback(MilkSeparator.RequiresEmptying));
		this.operational.working.pre.QueueAnim("separating_pre", false, null).OnAnimQueueComplete(this.operational.working.work);
		this.operational.working.work.Enter(new StateMachine<MilkSeparator, MilkSeparator.Instance, IStateMachineTarget, MilkSeparator.Def>.State.Callback(MilkSeparator.BeginSeparation)).PlayAnim("separating_loop", KAnim.PlayMode.Loop).EventTransition(GameHashes.OnStorageChange, this.operational.working.post, new StateMachine<MilkSeparator, MilkSeparator.Instance, IStateMachineTarget, MilkSeparator.Def>.Transition.ConditionCallback(MilkSeparator.CanNOTKeepSeparating))
			.Exit(new StateMachine<MilkSeparator, MilkSeparator.Instance, IStateMachineTarget, MilkSeparator.Def>.State.Callback(MilkSeparator.EndSeparation));
		this.operational.working.post.QueueAnim("separating_pst", false, null).OnAnimQueueComplete(this.operational.idle);
		this.operational.full.PlayAnim("ready").ToggleRecurringChore(new Func<MilkSeparator.Instance, Chore>(MilkSeparator.CreateEmptyChore), null).WorkableCompleteTransition((MilkSeparator.Instance smi) => smi.workable, this.operational.emptyComplete)
			.ToggleStatusItem(Db.Get().BuildingStatusItems.MilkSeparatorNeedsEmptying, null);
		this.operational.emptyComplete.Enter(new StateMachine<MilkSeparator, MilkSeparator.Instance, IStateMachineTarget, MilkSeparator.Def>.State.Callback(MilkSeparator.DropMilkFat)).ScheduleActionNextFrame("AfterMilkFatDrop", delegate(MilkSeparator.Instance smi)
		{
			smi.GoTo(this.operational.idle);
		});
	}

	// Token: 0x060032EB RID: 13035 RVA: 0x0011EB45 File Offset: 0x0011CD45
	public static void BeginSeparation(MilkSeparator.Instance smi)
	{
		smi.operational.SetActive(true, false);
	}

	// Token: 0x060032EC RID: 13036 RVA: 0x0011EB54 File Offset: 0x0011CD54
	public static void EndSeparation(MilkSeparator.Instance smi)
	{
		smi.operational.SetActive(false, false);
	}

	// Token: 0x060032ED RID: 13037 RVA: 0x0011EB63 File Offset: 0x0011CD63
	public static bool CanBeginSeparate(MilkSeparator.Instance smi)
	{
		return !smi.MilkFatLimitReached && smi.elementConverter.HasEnoughMassToStartConverting(false);
	}

	// Token: 0x060032EE RID: 13038 RVA: 0x0011EB7B File Offset: 0x0011CD7B
	public static bool CanKeepSeparating(MilkSeparator.Instance smi)
	{
		return !smi.MilkFatLimitReached && smi.elementConverter.CanConvertAtAll();
	}

	// Token: 0x060032EF RID: 13039 RVA: 0x0011EB92 File Offset: 0x0011CD92
	public static bool CanNOTKeepSeparating(MilkSeparator.Instance smi)
	{
		return !MilkSeparator.CanKeepSeparating(smi);
	}

	// Token: 0x060032F0 RID: 13040 RVA: 0x0011EB9D File Offset: 0x0011CD9D
	public static bool RequiresEmptying(MilkSeparator.Instance smi)
	{
		return smi.MilkFatLimitReached;
	}

	// Token: 0x060032F1 RID: 13041 RVA: 0x0011EBA5 File Offset: 0x0011CDA5
	public static bool ThereIsCapacityForMilkFat(MilkSeparator.Instance smi)
	{
		return !smi.MilkFatLimitReached;
	}

	// Token: 0x060032F2 RID: 13042 RVA: 0x0011EBB0 File Offset: 0x0011CDB0
	public static void DropMilkFat(MilkSeparator.Instance smi)
	{
		smi.DropMilkFat();
	}

	// Token: 0x060032F3 RID: 13043 RVA: 0x0011EBB8 File Offset: 0x0011CDB8
	public static void RefreshMeters(MilkSeparator.Instance smi)
	{
		smi.RefreshMeters();
	}

	// Token: 0x060032F4 RID: 13044 RVA: 0x0011EBC0 File Offset: 0x0011CDC0
	private static Chore CreateEmptyChore(MilkSeparator.Instance smi)
	{
		WorkChore<EmptyMilkSeparatorWorkable> workChore = new WorkChore<EmptyMilkSeparatorWorkable>(Db.Get().ChoreTypes.EmptyStorage, smi.workable, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		workChore.AddPrecondition(ChorePreconditions.instance.IsNotARobot, null);
		return workChore;
	}

	// Token: 0x04001E82 RID: 7810
	public const string WORK_PRE_ANIM_NAME = "separating_pre";

	// Token: 0x04001E83 RID: 7811
	public const string WORK_ANIM_NAME = "separating_loop";

	// Token: 0x04001E84 RID: 7812
	public const string WORK_POST_ANIM_NAME = "separating_pst";

	// Token: 0x04001E85 RID: 7813
	public GameStateMachine<MilkSeparator, MilkSeparator.Instance, IStateMachineTarget, MilkSeparator.Def>.State noOperational;

	// Token: 0x04001E86 RID: 7814
	public MilkSeparator.OperationalStates operational;

	// Token: 0x0200167D RID: 5757
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06009569 RID: 38249 RVA: 0x00373CF2 File Offset: 0x00371EF2
		public Def()
		{
			this.MILK_FAT_TAG = ElementLoader.FindElementByHash(SimHashes.MilkFat).tag;
			this.MILK_TAG = ElementLoader.FindElementByHash(SimHashes.Milk).tag;
		}

		// Token: 0x040072DE RID: 29406
		public float MILK_FAT_CAPACITY = 100f;

		// Token: 0x040072DF RID: 29407
		public Tag MILK_TAG;

		// Token: 0x040072E0 RID: 29408
		public Tag MILK_FAT_TAG;
	}

	// Token: 0x0200167E RID: 5758
	public class WorkingStates : GameStateMachine<MilkSeparator, MilkSeparator.Instance, IStateMachineTarget, MilkSeparator.Def>.State
	{
		// Token: 0x040072E1 RID: 29409
		public GameStateMachine<MilkSeparator, MilkSeparator.Instance, IStateMachineTarget, MilkSeparator.Def>.State pre;

		// Token: 0x040072E2 RID: 29410
		public GameStateMachine<MilkSeparator, MilkSeparator.Instance, IStateMachineTarget, MilkSeparator.Def>.State work;

		// Token: 0x040072E3 RID: 29411
		public GameStateMachine<MilkSeparator, MilkSeparator.Instance, IStateMachineTarget, MilkSeparator.Def>.State post;
	}

	// Token: 0x0200167F RID: 5759
	public class OperationalStates : GameStateMachine<MilkSeparator, MilkSeparator.Instance, IStateMachineTarget, MilkSeparator.Def>.State
	{
		// Token: 0x040072E4 RID: 29412
		public GameStateMachine<MilkSeparator, MilkSeparator.Instance, IStateMachineTarget, MilkSeparator.Def>.State idle;

		// Token: 0x040072E5 RID: 29413
		public MilkSeparator.WorkingStates working;

		// Token: 0x040072E6 RID: 29414
		public GameStateMachine<MilkSeparator, MilkSeparator.Instance, IStateMachineTarget, MilkSeparator.Def>.State full;

		// Token: 0x040072E7 RID: 29415
		public GameStateMachine<MilkSeparator, MilkSeparator.Instance, IStateMachineTarget, MilkSeparator.Def>.State emptyComplete;
	}

	// Token: 0x02001680 RID: 5760
	public new class Instance : GameStateMachine<MilkSeparator, MilkSeparator.Instance, IStateMachineTarget, MilkSeparator.Def>.GameInstance
	{
		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x0600956C RID: 38252 RVA: 0x00373D3F File Offset: 0x00371F3F
		public float MilkFatStored
		{
			get
			{
				return this.storage.GetAmountAvailable(base.def.MILK_FAT_TAG);
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x0600956D RID: 38253 RVA: 0x00373D57 File Offset: 0x00371F57
		public float MilkFatStoragePercentage
		{
			get
			{
				return Mathf.Clamp(this.MilkFatStored / base.def.MILK_FAT_CAPACITY, 0f, 1f);
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x0600956E RID: 38254 RVA: 0x00373D7A File Offset: 0x00371F7A
		public bool MilkFatLimitReached
		{
			get
			{
				return this.MilkFatStored >= base.def.MILK_FAT_CAPACITY;
			}
		}

		// Token: 0x0600956F RID: 38255 RVA: 0x00373D94 File Offset: 0x00371F94
		public Instance(IStateMachineTarget master, MilkSeparator.Def def)
			: base(master, def)
		{
			KAnimControllerBase component = base.GetComponent<KBatchedAnimController>();
			this.fatMeter = new MeterController(component, "meter_target_1", "meter_fat", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_target_1" });
		}

		// Token: 0x06009570 RID: 38256 RVA: 0x00373DD7 File Offset: 0x00371FD7
		public override void StartSM()
		{
			base.StartSM();
			this.workable.OnWork_PST_Begins = new global::System.Action(this.Play_Empty_MeterAnimation);
			this.RefreshMeters();
		}

		// Token: 0x06009571 RID: 38257 RVA: 0x00373DFC File Offset: 0x00371FFC
		private void Play_Empty_MeterAnimation()
		{
			this.fatMeter.SetPositionPercent(0f);
			this.fatMeter.meterController.Play("meter_fat_empty", KAnim.PlayMode.Once, 1f, 0f);
		}

		// Token: 0x06009572 RID: 38258 RVA: 0x00373E34 File Offset: 0x00372034
		public void DropMilkFat()
		{
			List<GameObject> list = new List<GameObject>();
			this.storage.Drop(base.def.MILK_FAT_TAG, list);
			Vector3 dropSpawnLocation = this.GetDropSpawnLocation();
			foreach (GameObject gameObject in list)
			{
				gameObject.transform.position = dropSpawnLocation;
			}
		}

		// Token: 0x06009573 RID: 38259 RVA: 0x00373EAC File Offset: 0x003720AC
		private Vector3 GetDropSpawnLocation()
		{
			bool flag;
			Vector3 vector = base.GetComponent<KBatchedAnimController>().GetSymbolTransform(new HashedString("milkfat"), out flag).GetColumn(3);
			vector.z = Grid.GetLayerZ(Grid.SceneLayer.Ore);
			int num = Grid.PosToCell(vector);
			if (Grid.IsValidCell(num) && !Grid.Solid[num])
			{
				return vector;
			}
			return base.transform.GetPosition();
		}

		// Token: 0x06009574 RID: 38260 RVA: 0x00373F18 File Offset: 0x00372118
		public void RefreshMeters()
		{
			if (this.fatMeter.meterController.currentAnim != "meter_fat")
			{
				this.fatMeter.meterController.Play("meter_fat", KAnim.PlayMode.Paused, 1f, 0f);
			}
			this.fatMeter.SetPositionPercent(this.MilkFatStoragePercentage);
		}

		// Token: 0x040072E8 RID: 29416
		[MyCmpGet]
		public EmptyMilkSeparatorWorkable workable;

		// Token: 0x040072E9 RID: 29417
		[MyCmpGet]
		public Operational operational;

		// Token: 0x040072EA RID: 29418
		[MyCmpGet]
		public ElementConverter elementConverter;

		// Token: 0x040072EB RID: 29419
		[MyCmpGet]
		private Storage storage;

		// Token: 0x040072EC RID: 29420
		private MeterController fatMeter;
	}
}
