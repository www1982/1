using System;

// Token: 0x02000A68 RID: 2664
public class PlantBranchGrowerBase<StateMachineType, StateMachineInstanceType, MasterType, DefType> : GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType> where StateMachineType : GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType> where StateMachineInstanceType : GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GameInstance where MasterType : IStateMachineTarget where DefType : PlantBranchGrowerBase<StateMachineType, StateMachineInstanceType, MasterType, DefType>.PlantBranchGrowerBaseDef
{
	// Token: 0x02001B32 RID: 6962
	public class PlantBranchGrowerBaseDef : StateMachine.BaseDef, IPlantBranchGrower
	{
		// Token: 0x0600A681 RID: 42625 RVA: 0x003AD95F File Offset: 0x003ABB5F
		public string GetPlantBranchPrefabName()
		{
			return this.BRANCH_PREFAB_NAME;
		}

		// Token: 0x0600A682 RID: 42626 RVA: 0x003AD967 File Offset: 0x003ABB67
		public int GetMaxBranchCount()
		{
			return this.MAX_BRANCH_COUNT;
		}

		// Token: 0x040081DF RID: 33247
		public int MAX_BRANCH_COUNT;

		// Token: 0x040081E0 RID: 33248
		public string BRANCH_PREFAB_NAME;
	}
}
