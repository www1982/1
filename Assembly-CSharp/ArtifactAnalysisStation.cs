using System;
using System.Collections.Generic;
using KSerialization;

// Token: 0x020006D9 RID: 1753
public class ArtifactAnalysisStation : GameStateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>
{
	// Token: 0x06002B36 RID: 11062 RVA: 0x000F9BF0 File Offset: 0x000F7DF0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.inoperational;
		this.inoperational.EventTransition(GameHashes.OperationalChanged, this.ready, new StateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>.Transition.ConditionCallback(this.IsOperational));
		this.operational.EventTransition(GameHashes.OperationalChanged, this.inoperational, GameStateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>.Not(new StateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>.Transition.ConditionCallback(this.IsOperational))).EventTransition(GameHashes.OnStorageChange, this.ready, new StateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>.Transition.ConditionCallback(this.HasArtifactToStudy));
		this.ready.EventTransition(GameHashes.OperationalChanged, this.inoperational, GameStateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>.Not(new StateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>.Transition.ConditionCallback(this.IsOperational))).EventTransition(GameHashes.OnStorageChange, this.operational, GameStateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>.Not(new StateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>.Transition.ConditionCallback(this.HasArtifactToStudy))).ToggleChore(new Func<ArtifactAnalysisStation.StatesInstance, Chore>(this.CreateChore), new Action<ArtifactAnalysisStation.StatesInstance, Chore>(ArtifactAnalysisStation.SetRemoteChore), this.operational);
	}

	// Token: 0x06002B37 RID: 11063 RVA: 0x000F9CD8 File Offset: 0x000F7ED8
	private static void SetRemoteChore(ArtifactAnalysisStation.StatesInstance smi, Chore chore)
	{
		smi.remoteChore.SetChore(chore);
	}

	// Token: 0x06002B38 RID: 11064 RVA: 0x000F9CE6 File Offset: 0x000F7EE6
	private bool HasArtifactToStudy(ArtifactAnalysisStation.StatesInstance smi)
	{
		return smi.storage.GetMassAvailable(GameTags.CharmedArtifact) >= 1f;
	}

	// Token: 0x06002B39 RID: 11065 RVA: 0x000F9D02 File Offset: 0x000F7F02
	private bool IsOperational(ArtifactAnalysisStation.StatesInstance smi)
	{
		return smi.GetComponent<Operational>().IsOperational;
	}

	// Token: 0x06002B3A RID: 11066 RVA: 0x000F9D10 File Offset: 0x000F7F10
	private Chore CreateChore(ArtifactAnalysisStation.StatesInstance smi)
	{
		return new WorkChore<ArtifactAnalysisStationWorkable>(Db.Get().ChoreTypes.AnalyzeArtifact, smi.workable, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
	}

	// Token: 0x04001988 RID: 6536
	public GameStateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>.State inoperational;

	// Token: 0x04001989 RID: 6537
	public GameStateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>.State operational;

	// Token: 0x0400198A RID: 6538
	public GameStateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>.State ready;

	// Token: 0x0200155D RID: 5469
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x0200155E RID: 5470
	public class StatesInstance : GameStateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>.GameInstance
	{
		// Token: 0x060090FB RID: 37115 RVA: 0x00362529 File Offset: 0x00360729
		public StatesInstance(IStateMachineTarget master, ArtifactAnalysisStation.Def def)
			: base(master, def)
		{
			this.workable.statesInstance = this;
		}

		// Token: 0x060090FC RID: 37116 RVA: 0x0036253F File Offset: 0x0036073F
		public override void StartSM()
		{
			base.StartSM();
		}

		// Token: 0x04006F7C RID: 28540
		[MyCmpReq]
		public Storage storage;

		// Token: 0x04006F7D RID: 28541
		[MyCmpReq]
		public ManualDeliveryKG manualDelivery;

		// Token: 0x04006F7E RID: 28542
		[MyCmpReq]
		public ArtifactAnalysisStationWorkable workable;

		// Token: 0x04006F7F RID: 28543
		[MyCmpAdd]
		public ManuallySetRemoteWorkTargetComponent remoteChore;

		// Token: 0x04006F80 RID: 28544
		[Serialize]
		private HashSet<Tag> forbiddenSeeds;
	}
}
