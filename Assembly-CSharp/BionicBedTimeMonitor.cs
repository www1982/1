using System;
using UnityEngine;

// Token: 0x020009D1 RID: 2513
public class BionicBedTimeMonitor : GameStateMachine<BionicBedTimeMonitor, BionicBedTimeMonitor.Instance, IStateMachineTarget, BionicBedTimeMonitor.Def>
{
	// Token: 0x06004990 RID: 18832 RVA: 0x001AA1EC File Offset: 0x001A83EC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.notAllowed;
		this.notAllowed.ScheduleChange(this.bedTime, new StateMachine<BionicBedTimeMonitor, BionicBedTimeMonitor.Instance, IStateMachineTarget, BionicBedTimeMonitor.Def>.Transition.ConditionCallback(BionicBedTimeMonitor.CanGoToBedTime)).EventTransition(GameHashes.BionicOnline, this.bedTime, new StateMachine<BionicBedTimeMonitor, BionicBedTimeMonitor.Instance, IStateMachineTarget, BionicBedTimeMonitor.Def>.Transition.ConditionCallback(BionicBedTimeMonitor.CanGoToBedTime));
		this.bedTime.DefaultState(this.bedTime.runChore);
		this.bedTime.runChore.ToggleChore((BionicBedTimeMonitor.Instance smi) => new BionicBedTimeModeChore(smi.master), this.bedTime.choreEnded, this.bedTime.choreEnded).DefaultState(this.bedTime.runChore.notStarted);
		this.bedTime.runChore.notStarted.EventTransition(GameHashes.BeginChore, this.bedTime.runChore.running, new StateMachine<BionicBedTimeMonitor, BionicBedTimeMonitor.Instance, IStateMachineTarget, BionicBedTimeMonitor.Def>.Transition.ConditionCallback(BionicBedTimeMonitor.ChoreIsRunning)).ScheduleChange(this.notAllowed, GameStateMachine<BionicBedTimeMonitor, BionicBedTimeMonitor.Instance, IStateMachineTarget, BionicBedTimeMonitor.Def>.Not(new StateMachine<BionicBedTimeMonitor, BionicBedTimeMonitor.Instance, IStateMachineTarget, BionicBedTimeMonitor.Def>.Transition.ConditionCallback(BionicBedTimeMonitor.CanGoToBedTime))).EventTransition(GameHashes.BionicOffline, this.notAllowed, null);
		this.bedTime.runChore.running.EventTransition(GameHashes.EndChore, this.bedTime.runChore.notStarted, GameStateMachine<BionicBedTimeMonitor, BionicBedTimeMonitor.Instance, IStateMachineTarget, BionicBedTimeMonitor.Def>.Not(new StateMachine<BionicBedTimeMonitor, BionicBedTimeMonitor.Instance, IStateMachineTarget, BionicBedTimeMonitor.Def>.Transition.ConditionCallback(BionicBedTimeMonitor.ChoreIsRunning))).DefaultState(this.bedTime.runChore.running.traveling);
		this.bedTime.runChore.running.traveling.TagTransition(GameTags.BionicBedTime, this.bedTime.runChore.running.defragmenting, false);
		this.bedTime.runChore.running.defragmenting.TagTransition(GameTags.BionicBedTime, this.bedTime.runChore.running.traveling, true).Enter(new StateMachine<BionicBedTimeMonitor, BionicBedTimeMonitor.Instance, IStateMachineTarget, BionicBedTimeMonitor.Def>.State.Callback(BionicBedTimeMonitor.EnableLight)).Exit(new StateMachine<BionicBedTimeMonitor, BionicBedTimeMonitor.Instance, IStateMachineTarget, BionicBedTimeMonitor.Def>.State.Callback(BionicBedTimeMonitor.DisableLight));
		this.bedTime.choreEnded.ScheduleChange(this.notAllowed, GameStateMachine<BionicBedTimeMonitor, BionicBedTimeMonitor.Instance, IStateMachineTarget, BionicBedTimeMonitor.Def>.Not(new StateMachine<BionicBedTimeMonitor, BionicBedTimeMonitor.Instance, IStateMachineTarget, BionicBedTimeMonitor.Def>.Transition.ConditionCallback(BionicBedTimeMonitor.CanGoToBedTime))).EventTransition(GameHashes.BionicOffline, this.notAllowed, null).GoTo(this.bedTime.runChore);
	}

	// Token: 0x06004991 RID: 18833 RVA: 0x001AA445 File Offset: 0x001A8645
	public static bool CanGoToBedTime(BionicBedTimeMonitor.Instance smi)
	{
		return BionicBedTimeMonitor.IsOnline(smi) && BionicBedTimeMonitor.ScheduleIsInBedTime(smi);
	}

	// Token: 0x06004992 RID: 18834 RVA: 0x001AA457 File Offset: 0x001A8657
	private static void EnableLight(BionicBedTimeMonitor.Instance smi)
	{
		smi.EnableLight();
	}

	// Token: 0x06004993 RID: 18835 RVA: 0x001AA45F File Offset: 0x001A865F
	private static void DisableLight(BionicBedTimeMonitor.Instance smi)
	{
		smi.DisableLight();
	}

	// Token: 0x06004994 RID: 18836 RVA: 0x001AA467 File Offset: 0x001A8667
	private static bool IsOnline(BionicBedTimeMonitor.Instance smi)
	{
		return smi.IsOnline;
	}

	// Token: 0x06004995 RID: 18837 RVA: 0x001AA46F File Offset: 0x001A866F
	private static bool ScheduleIsInBedTime(BionicBedTimeMonitor.Instance smi)
	{
		return smi.IsScheduleInBedTime;
	}

	// Token: 0x06004996 RID: 18838 RVA: 0x001AA478 File Offset: 0x001A8678
	public static bool ChoreIsRunning(BionicBedTimeMonitor.Instance smi)
	{
		ChoreDriver component = smi.GetComponent<ChoreDriver>();
		Chore chore = ((component == null) ? null : component.GetCurrentChore());
		return chore != null && chore.choreType == Db.Get().ChoreTypes.BionicBedtimeMode;
	}

	// Token: 0x0400307D RID: 12413
	private const float LIGHT_RADIUS = 3f;

	// Token: 0x0400307E RID: 12414
	private const int LIGHT_LUX = 1800;

	// Token: 0x0400307F RID: 12415
	public GameStateMachine<BionicBedTimeMonitor, BionicBedTimeMonitor.Instance, IStateMachineTarget, BionicBedTimeMonitor.Def>.State notAllowed;

	// Token: 0x04003080 RID: 12416
	public BionicBedTimeMonitor.BedTimeStates bedTime;

	// Token: 0x020019EF RID: 6639
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020019F0 RID: 6640
	public class DefragmentingStates : GameStateMachine<BionicBedTimeMonitor, BionicBedTimeMonitor.Instance, IStateMachineTarget, BionicBedTimeMonitor.Def>.State
	{
		// Token: 0x04007E2B RID: 32299
		public GameStateMachine<BionicBedTimeMonitor, BionicBedTimeMonitor.Instance, IStateMachineTarget, BionicBedTimeMonitor.Def>.State traveling;

		// Token: 0x04007E2C RID: 32300
		public GameStateMachine<BionicBedTimeMonitor, BionicBedTimeMonitor.Instance, IStateMachineTarget, BionicBedTimeMonitor.Def>.State defragmenting;
	}

	// Token: 0x020019F1 RID: 6641
	public class ChoreStates : GameStateMachine<BionicBedTimeMonitor, BionicBedTimeMonitor.Instance, IStateMachineTarget, BionicBedTimeMonitor.Def>.State
	{
		// Token: 0x04007E2D RID: 32301
		public GameStateMachine<BionicBedTimeMonitor, BionicBedTimeMonitor.Instance, IStateMachineTarget, BionicBedTimeMonitor.Def>.State notStarted;

		// Token: 0x04007E2E RID: 32302
		public BionicBedTimeMonitor.DefragmentingStates running;
	}

	// Token: 0x020019F2 RID: 6642
	public class BedTimeStates : GameStateMachine<BionicBedTimeMonitor, BionicBedTimeMonitor.Instance, IStateMachineTarget, BionicBedTimeMonitor.Def>.State
	{
		// Token: 0x04007E2F RID: 32303
		public BionicBedTimeMonitor.ChoreStates runChore;

		// Token: 0x04007E30 RID: 32304
		public GameStateMachine<BionicBedTimeMonitor, BionicBedTimeMonitor.Instance, IStateMachineTarget, BionicBedTimeMonitor.Def>.State choreEnded;
	}

	// Token: 0x020019F3 RID: 6643
	public new class Instance : GameStateMachine<BionicBedTimeMonitor, BionicBedTimeMonitor.Instance, IStateMachineTarget, BionicBedTimeMonitor.Def>.GameInstance
	{
		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x0600A161 RID: 41313 RVA: 0x0039E84E File Offset: 0x0039CA4E
		public bool IsOnline
		{
			get
			{
				return this.batteryMonitor != null && this.batteryMonitor.IsOnline;
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x0600A162 RID: 41314 RVA: 0x0039E865 File Offset: 0x0039CA65
		public bool IsBedTimeChoreRunning
		{
			get
			{
				return this.prefabID.HasTag(GameTags.BionicBedTime);
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x0600A163 RID: 41315 RVA: 0x0039E877 File Offset: 0x0039CA77
		public bool IsScheduleInBedTime
		{
			get
			{
				return this.schedulable.IsAllowed(Db.Get().ScheduleBlockTypes.Sleep);
			}
		}

		// Token: 0x0600A164 RID: 41316 RVA: 0x0039E893 File Offset: 0x0039CA93
		public Instance(IStateMachineTarget master, BionicBedTimeMonitor.Def def)
			: base(master, def)
		{
			this.batteryMonitor = base.gameObject.GetSMI<BionicBatteryMonitor.Instance>();
			this.prefabID = base.GetComponent<KPrefabID>();
			this.schedulable = base.GetComponent<Schedulable>();
		}

		// Token: 0x0600A165 RID: 41317 RVA: 0x0039E8C8 File Offset: 0x0039CAC8
		public void EnableLight()
		{
			this.lightSymbolTracker = base.gameObject.AddOrGet<LightSymbolTracker>();
			this.lightSymbolTracker.targetSymbol = "snapTo_mouth";
			this.lightSymbolTracker.enabled = true;
			this.light = base.gameObject.AddOrGet<Light2D>();
			this.light.Lux = 1800;
			this.light.Range = 3f;
			this.light.enabled = true;
			this.light.drawOverlay = true;
			this.light.Color = new Color(0f, 0.3137255f, 1f, 1f);
			this.light.overlayColour = new Color(1f, 1f, 1f, 1f);
			this.light.FullRefresh();
		}

		// Token: 0x0600A166 RID: 41318 RVA: 0x0039E9A3 File Offset: 0x0039CBA3
		public void DisableLight()
		{
			if (this.light != null)
			{
				this.light.enabled = false;
			}
			if (this.lightSymbolTracker != null)
			{
				this.lightSymbolTracker.enabled = false;
			}
		}

		// Token: 0x04007E31 RID: 32305
		private Light2D light;

		// Token: 0x04007E32 RID: 32306
		private LightSymbolTracker lightSymbolTracker;

		// Token: 0x04007E33 RID: 32307
		private BionicBatteryMonitor.Instance batteryMonitor;

		// Token: 0x04007E34 RID: 32308
		private Schedulable schedulable;

		// Token: 0x04007E35 RID: 32309
		private KPrefabID prefabID;
	}
}
