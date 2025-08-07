using System;

// Token: 0x02000726 RID: 1830
public class FarmTile : StateMachineComponent<FarmTile.SMInstance>
{
	// Token: 0x06002E1B RID: 11803 RVA: 0x001089BF File Offset: 0x00106BBF
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x04001B39 RID: 6969
	[MyCmpReq]
	private PlantablePlot plantablePlot;

	// Token: 0x04001B3A RID: 6970
	[MyCmpReq]
	private Storage storage;

	// Token: 0x020015CA RID: 5578
	public class SMInstance : GameStateMachine<FarmTile.States, FarmTile.SMInstance, FarmTile, object>.GameInstance
	{
		// Token: 0x060092B4 RID: 37556 RVA: 0x003679EE File Offset: 0x00365BEE
		public SMInstance(FarmTile master)
			: base(master)
		{
		}

		// Token: 0x060092B5 RID: 37557 RVA: 0x003679F8 File Offset: 0x00365BF8
		public bool HasWater()
		{
			PrimaryElement primaryElement = base.master.storage.FindPrimaryElement(SimHashes.Water);
			return primaryElement != null && primaryElement.Mass > 0f;
		}
	}

	// Token: 0x020015CB RID: 5579
	public class States : GameStateMachine<FarmTile.States, FarmTile.SMInstance, FarmTile>
	{
		// Token: 0x060092B6 RID: 37558 RVA: 0x00367A34 File Offset: 0x00365C34
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.empty;
			this.empty.EventTransition(GameHashes.OccupantChanged, this.full, (FarmTile.SMInstance smi) => smi.master.plantablePlot.Occupant != null);
			this.empty.wet.EventTransition(GameHashes.OnStorageChange, this.empty.dry, (FarmTile.SMInstance smi) => !smi.HasWater());
			this.empty.dry.EventTransition(GameHashes.OnStorageChange, this.empty.wet, (FarmTile.SMInstance smi) => !smi.HasWater());
			this.full.EventTransition(GameHashes.OccupantChanged, this.empty, (FarmTile.SMInstance smi) => smi.master.plantablePlot.Occupant == null);
			this.full.wet.EventTransition(GameHashes.OnStorageChange, this.full.dry, (FarmTile.SMInstance smi) => !smi.HasWater());
			this.full.dry.EventTransition(GameHashes.OnStorageChange, this.full.wet, (FarmTile.SMInstance smi) => !smi.HasWater());
		}

		// Token: 0x040070DB RID: 28891
		public FarmTile.States.FarmStates empty;

		// Token: 0x040070DC RID: 28892
		public FarmTile.States.FarmStates full;

		// Token: 0x02002779 RID: 10105
		public class FarmStates : GameStateMachine<FarmTile.States, FarmTile.SMInstance, FarmTile, object>.State
		{
			// Token: 0x0400AE83 RID: 44675
			public GameStateMachine<FarmTile.States, FarmTile.SMInstance, FarmTile, object>.State wet;

			// Token: 0x0400AE84 RID: 44676
			public GameStateMachine<FarmTile.States, FarmTile.SMInstance, FarmTile, object>.State dry;
		}
	}
}
