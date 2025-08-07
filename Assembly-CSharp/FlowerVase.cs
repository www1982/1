using System;

// Token: 0x0200072B RID: 1835
public class FlowerVase : StateMachineComponent<FlowerVase.SMInstance>
{
	// Token: 0x06002E4D RID: 11853 RVA: 0x00109648 File Offset: 0x00107848
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06002E4E RID: 11854 RVA: 0x00109650 File Offset: 0x00107850
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x04001B57 RID: 6999
	[MyCmpReq]
	private PlantablePlot plantablePlot;

	// Token: 0x04001B58 RID: 7000
	[MyCmpReq]
	private KBoxCollider2D boxCollider;

	// Token: 0x020015D4 RID: 5588
	public class SMInstance : GameStateMachine<FlowerVase.States, FlowerVase.SMInstance, FlowerVase, object>.GameInstance
	{
		// Token: 0x060092CE RID: 37582 RVA: 0x00368302 File Offset: 0x00366502
		public SMInstance(FlowerVase master)
			: base(master)
		{
		}
	}

	// Token: 0x020015D5 RID: 5589
	public class States : GameStateMachine<FlowerVase.States, FlowerVase.SMInstance, FlowerVase>
	{
		// Token: 0x060092CF RID: 37583 RVA: 0x0036830C File Offset: 0x0036650C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.empty;
			this.empty.EventTransition(GameHashes.OccupantChanged, this.full, (FlowerVase.SMInstance smi) => smi.master.plantablePlot.Occupant != null).PlayAnim("off");
			this.full.EventTransition(GameHashes.OccupantChanged, this.empty, (FlowerVase.SMInstance smi) => smi.master.plantablePlot.Occupant == null).PlayAnim("on");
		}

		// Token: 0x040070F6 RID: 28918
		public GameStateMachine<FlowerVase.States, FlowerVase.SMInstance, FlowerVase, object>.State empty;

		// Token: 0x040070F7 RID: 28919
		public GameStateMachine<FlowerVase.States, FlowerVase.SMInstance, FlowerVase, object>.State full;
	}
}
