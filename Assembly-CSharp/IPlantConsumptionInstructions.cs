using System;

// Token: 0x0200058E RID: 1422
public interface IPlantConsumptionInstructions
{
	// Token: 0x06002075 RID: 8309
	CellOffset[] GetAllowedOffsets();

	// Token: 0x06002076 RID: 8310
	float ConsumePlant(float desiredUnitsToConsume);

	// Token: 0x06002077 RID: 8311
	float PlantProductGrowthPerCycle();

	// Token: 0x06002078 RID: 8312
	bool CanPlantBeEaten();

	// Token: 0x06002079 RID: 8313
	string GetFormattedConsumptionPerCycle(float consumer_caloriesLossPerCaloriesPerKG);

	// Token: 0x0600207A RID: 8314
	Diet.Info.FoodType GetDietFoodType();
}
