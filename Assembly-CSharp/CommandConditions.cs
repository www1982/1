using System;

// Token: 0x02000B61 RID: 2913
public class CommandConditions : KMonoBehaviour
{
	// Token: 0x040039FF RID: 14847
	public ConditionDestinationReachable reachable;

	// Token: 0x04003A00 RID: 14848
	public CargoBayIsEmpty cargoEmpty;

	// Token: 0x04003A01 RID: 14849
	public ConditionHasMinimumMass destHasResources;

	// Token: 0x04003A02 RID: 14850
	public ConditionAllModulesComplete allModulesComplete;

	// Token: 0x04003A03 RID: 14851
	public ConditionHasCargoBayForNoseconeHarvest HasCargoBayForNoseconeHarvest;

	// Token: 0x04003A04 RID: 14852
	public ConditionHasEngine hasEngine;

	// Token: 0x04003A05 RID: 14853
	public ConditionHasNosecone hasNosecone;

	// Token: 0x04003A06 RID: 14854
	public ConditionOnLaunchPad onLaunchPad;

	// Token: 0x04003A07 RID: 14855
	public ConditionFlightPathIsClear flightPathIsClear;
}
