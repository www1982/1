using System;
using UnityEngine;

// Token: 0x02000480 RID: 1152
public class FartChore : Chore<FartChore.StatesInstance>
{
	// Token: 0x0600182B RID: 6187 RVA: 0x00086AF4 File Offset: 0x00084CF4
	public FartChore(IStateMachineTarget target, ChoreType chore_type, float mass, SimHashes element_id, byte disease_idx, int disease_count, float overpressureThreshold)
		: base(chore_type, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new FartChore.StatesInstance(this, target.gameObject);
		this.mass = mass;
		this.element_id = element_id;
		this.disease_idx = disease_idx;
		this.disease_count = disease_count;
		this.overpressureThreshold = overpressureThreshold;
	}

	// Token: 0x0600182C RID: 6188 RVA: 0x00086B54 File Offset: 0x00084D54
	private bool CheckIsOverpressure(int cell)
	{
		return Grid.Mass[cell] > this.overpressureThreshold;
	}

	// Token: 0x0600182D RID: 6189 RVA: 0x00086B69 File Offset: 0x00084D69
	public static void CreateEmission(FartChore.StatesInstance smi)
	{
		smi.master.DoFart();
	}

	// Token: 0x0600182E RID: 6190 RVA: 0x00086B78 File Offset: 0x00084D78
	public void DoFart()
	{
		if (this.mass <= 0f)
		{
			return;
		}
		Element element = ElementLoader.FindElementByHash(this.element_id);
		float temperature = base.smi.master.GetComponent<PrimaryElement>().Temperature;
		if (element.IsGas || element.IsLiquid)
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			if (this.CheckIsOverpressure(num))
			{
				return;
			}
			SimMessages.AddRemoveSubstance(num, this.element_id, CellEventLogger.Instance.ElementConsumerSimUpdate, this.mass, temperature, this.disease_idx, this.disease_count, true, -1);
		}
		else if (element.IsSolid)
		{
			element.substance.SpawnResource(base.transform.GetPosition() + new Vector3(0f, 0.5f, 0f), this.mass, temperature, this.disease_idx, this.disease_count, false, true, false);
		}
		PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, element.name, this.gameObject.transform, 1.5f, false);
	}

	// Token: 0x04000E18 RID: 3608
	private float mass;

	// Token: 0x04000E19 RID: 3609
	private SimHashes element_id;

	// Token: 0x04000E1A RID: 3610
	private byte disease_idx;

	// Token: 0x04000E1B RID: 3611
	private int disease_count;

	// Token: 0x04000E1C RID: 3612
	private float overpressureThreshold;

	// Token: 0x0200127C RID: 4732
	public class StatesInstance : GameStateMachine<FartChore.States, FartChore.StatesInstance, FartChore, object>.GameInstance
	{
		// Token: 0x060086A0 RID: 34464 RVA: 0x0033E8AF File Offset: 0x0033CAAF
		public StatesInstance(FartChore master, GameObject farter)
			: base(master)
		{
			base.sm.farter.Set(farter, base.smi, false);
		}
	}

	// Token: 0x0200127D RID: 4733
	public class States : GameStateMachine<FartChore.States, FartChore.StatesInstance, FartChore>
	{
		// Token: 0x060086A1 RID: 34465 RVA: 0x0033E8D4 File Offset: 0x0033CAD4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
			base.Target(this.farter);
			this.root.PlayAnim("fart").ScheduleGoTo(10f, this.finish).OnAnimQueueComplete(this.finish);
			this.finish.Enter(new StateMachine<FartChore.States, FartChore.StatesInstance, FartChore, object>.State.Callback(FartChore.CreateEmission)).ReturnSuccess();
		}

		// Token: 0x04006671 RID: 26225
		public StateMachine<FartChore.States, FartChore.StatesInstance, FartChore, object>.TargetParameter farter;

		// Token: 0x04006672 RID: 26226
		public GameStateMachine<FartChore.States, FartChore.StatesInstance, FartChore, object>.State finish;
	}
}
