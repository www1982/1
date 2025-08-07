using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200086C RID: 2156
public interface IPlantConsumeEntities
{
	// Token: 0x06003B2C RID: 15148
	string GetConsumableEntitiesCategoryName();

	// Token: 0x06003B2D RID: 15149
	string GetRequirementText();

	// Token: 0x06003B2E RID: 15150
	List<KPrefabID> GetPrefabsOfPossiblePrey();

	// Token: 0x06003B2F RID: 15151
	string[] GetFormattedPossiblePreyList();

	// Token: 0x06003B30 RID: 15152
	bool IsEntityEdible(GameObject entity);

	// Token: 0x06003B31 RID: 15153
	string GetConsumedEntityName();

	// Token: 0x06003B32 RID: 15154
	bool AreEntitiesConsumptionRequirementsSatisfied();
}
