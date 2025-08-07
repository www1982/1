using System;
using UnityEngine;

// Token: 0x02000730 RID: 1840
public class FoodSmoker : GameStateMachine<FoodSmoker, FoodSmoker.StatesInstance, IStateMachineTarget, FoodSmoker.Def>
{
	// Token: 0x06002E65 RID: 11877 RVA: 0x0010A298 File Offset: 0x00108498
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.working;
		this.working.Enter(delegate(FoodSmoker.StatesInstance smi)
		{
			smi.complexFabricator.SetQueueDirty();
			smi.operational.SetFlag(FoodSmoker.foodSmokerFlag, true);
		}).EnterTransition(this.requestEmpty, (FoodSmoker.StatesInstance smi) => smi.RequiresEmptying()).EventHandlerTransition(GameHashes.FabricatorOrderCompleted, this.requestEmpty, (FoodSmoker.StatesInstance smi, object data) => smi.RequiresEmptying());
		this.requestEmpty.ToggleRecurringChore(new Func<FoodSmoker.StatesInstance, Chore>(this.CreateChore), new Action<FoodSmoker.StatesInstance, Chore>(FoodSmoker.SetRemoteChore), (FoodSmoker.StatesInstance smi) => smi.RequiresEmptying()).EventHandlerTransition(GameHashes.OnStorageChange, this.working, (FoodSmoker.StatesInstance smi, object data) => !smi.RequiresEmptying()).Enter(delegate(FoodSmoker.StatesInstance smi)
		{
			smi.operational.SetFlag(FoodSmoker.foodSmokerFlag, false);
		})
			.ToggleStatusItem(Db.Get().BuildingStatusItems.AwaitingEmptyBuilding, null);
	}

	// Token: 0x06002E66 RID: 11878 RVA: 0x0010A3DC File Offset: 0x001085DC
	private static void SetRemoteChore(FoodSmoker.StatesInstance smi, Chore chore)
	{
		smi.remoteChore.SetChore(chore);
	}

	// Token: 0x06002E67 RID: 11879 RVA: 0x0010A3EC File Offset: 0x001085EC
	private Chore CreateChore(FoodSmoker.StatesInstance smi)
	{
		WorkChore<FoodSmokerWorkableEmpty> workChore = new WorkChore<FoodSmokerWorkableEmpty>(Db.Get().ChoreTypes.Cook, smi.master.GetComponent<FoodSmokerWorkableEmpty>(), null, true, new Action<Chore>(smi.OnEmptyComplete), null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		workChore.AddPrecondition(ChorePreconditions.instance.IsNotARobot, null);
		workChore.AddPrecondition(ChorePreconditions.instance.HasSkillPerk, Db.Get().SkillPerks.CanGasRange.Id);
		return workChore;
	}

	// Token: 0x04001B77 RID: 7031
	private static readonly Operational.Flag foodSmokerFlag = new Operational.Flag("food_smoker", Operational.Flag.Type.Requirement);

	// Token: 0x04001B78 RID: 7032
	private GameStateMachine<FoodSmoker, FoodSmoker.StatesInstance, IStateMachineTarget, FoodSmoker.Def>.State working;

	// Token: 0x04001B79 RID: 7033
	private GameStateMachine<FoodSmoker, FoodSmoker.StatesInstance, IStateMachineTarget, FoodSmoker.Def>.State requestEmpty;

	// Token: 0x020015DE RID: 5598
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020015DF RID: 5599
	public class StatesInstance : GameStateMachine<FoodSmoker, FoodSmoker.StatesInstance, IStateMachineTarget, FoodSmoker.Def>.GameInstance
	{
		// Token: 0x06009311 RID: 37649 RVA: 0x0036945C File Offset: 0x0036765C
		public StatesInstance(IStateMachineTarget master, FoodSmoker.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06009312 RID: 37650 RVA: 0x00369466 File Offset: 0x00367666
		public bool RequiresEmptying()
		{
			return !this.complexFabricator.outStorage.IsEmpty();
		}

		// Token: 0x06009313 RID: 37651 RVA: 0x0036947C File Offset: 0x0036767C
		public void OnEmptyComplete(Chore obj)
		{
			Vector3 vector = Grid.CellToPosLCC(Grid.PosToCell(this), Grid.SceneLayer.Ore);
			this.complexFabricator.outStorage.DropAll(vector, false, true, default(Vector3), true, null);
		}

		// Token: 0x04007125 RID: 28965
		[MyCmpAdd]
		public ManuallySetRemoteWorkTargetComponent remoteChore;

		// Token: 0x04007126 RID: 28966
		[MyCmpReq]
		public Operational operational;

		// Token: 0x04007127 RID: 28967
		[MyCmpReq]
		public ComplexFabricator complexFabricator;
	}
}
