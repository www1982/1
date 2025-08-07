using System;

// Token: 0x020007A9 RID: 1961
[SkipSaveFileSerialization]
public class PlanterBox : StateMachineComponent<PlanterBox.SMInstance>
{
	// Token: 0x060033F8 RID: 13304 RVA: 0x00123C18 File Offset: 0x00121E18
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x04001F47 RID: 8007
	[MyCmpReq]
	private PlantablePlot plantablePlot;

	// Token: 0x020016BD RID: 5821
	public class SMInstance : GameStateMachine<PlanterBox.States, PlanterBox.SMInstance, PlanterBox, object>.GameInstance
	{
		// Token: 0x06009663 RID: 38499 RVA: 0x00377E1E File Offset: 0x0037601E
		public SMInstance(PlanterBox master)
			: base(master)
		{
		}
	}

	// Token: 0x020016BE RID: 5822
	public class States : GameStateMachine<PlanterBox.States, PlanterBox.SMInstance, PlanterBox>
	{
		// Token: 0x06009664 RID: 38500 RVA: 0x00377E28 File Offset: 0x00376028
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.empty;
			this.empty.EventTransition(GameHashes.OccupantChanged, this.full, (PlanterBox.SMInstance smi) => smi.master.plantablePlot.Occupant != null).PlayAnim("off");
			this.full.EventTransition(GameHashes.OccupantChanged, this.empty, (PlanterBox.SMInstance smi) => smi.master.plantablePlot.Occupant == null).PlayAnim("on");
		}

		// Token: 0x040073A4 RID: 29604
		public GameStateMachine<PlanterBox.States, PlanterBox.SMInstance, PlanterBox, object>.State empty;

		// Token: 0x040073A5 RID: 29605
		public GameStateMachine<PlanterBox.States, PlanterBox.SMInstance, PlanterBox, object>.State full;
	}
}
