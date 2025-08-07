using System;

// Token: 0x02000055 RID: 85
public class LightController : GameStateMachine<LightController, LightController.Instance>
{
	// Token: 0x060001A4 RID: 420 RVA: 0x0000B934 File Offset: 0x00009B34
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.on, (LightController.Instance smi) => smi.GetComponent<Operational>().IsOperational);
		this.on.PlayAnim("on").EventTransition(GameHashes.OperationalChanged, this.off, (LightController.Instance smi) => !smi.GetComponent<Operational>().IsOperational).ToggleStatusItem(Db.Get().BuildingStatusItems.EmittingLight, null)
			.Enter("SetActive", delegate(LightController.Instance smi)
			{
				smi.GetComponent<Operational>().SetActive(true, false);
			});
	}

	// Token: 0x040000FF RID: 255
	public GameStateMachine<LightController, LightController.Instance, IStateMachineTarget, object>.State off;

	// Token: 0x04000100 RID: 256
	public GameStateMachine<LightController, LightController.Instance, IStateMachineTarget, object>.State on;

	// Token: 0x0200103B RID: 4155
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x0200103C RID: 4156
	public new class Instance : GameStateMachine<LightController, LightController.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06007F61 RID: 32609 RVA: 0x0032BFDB File Offset: 0x0032A1DB
		public Instance(IStateMachineTarget master, LightController.Def def)
			: base(master, def)
		{
		}
	}
}
