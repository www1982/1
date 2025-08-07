using System;
using System.Collections.Generic;

// Token: 0x02000E92 RID: 3730
public static class WorldGenProgressStages
{
	// Token: 0x040052C0 RID: 21184
	public static KeyValuePair<WorldGenProgressStages.Stages, float>[] StageWeights = new KeyValuePair<WorldGenProgressStages.Stages, float>[]
	{
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.Failure, 0f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.SetupNoise, 0.01f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.GenerateNoise, 1f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.GenerateSolarSystem, 0.01f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.WorldLayout, 1f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.CompleteLayout, 0.01f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.NoiseMapBuilder, 9f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.ClearingLevel, 0.5f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.Processing, 1f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.Borders, 0.1f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.ProcessRivers, 0.1f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.ConvertCellsToEdges, 0f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.DrawWorldBorder, 0.2f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.PlaceTemplates, 5f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.SettleSim, 6f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.DetectNaturalCavities, 6f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.PlacingCreatures, 0.01f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.Complete, 0f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.NumberOfStages, 0f)
	};

	// Token: 0x02002086 RID: 8326
	public enum Stages
	{
		// Token: 0x04009484 RID: 38020
		Failure,
		// Token: 0x04009485 RID: 38021
		SetupNoise,
		// Token: 0x04009486 RID: 38022
		GenerateNoise,
		// Token: 0x04009487 RID: 38023
		GenerateSolarSystem,
		// Token: 0x04009488 RID: 38024
		WorldLayout,
		// Token: 0x04009489 RID: 38025
		CompleteLayout,
		// Token: 0x0400948A RID: 38026
		NoiseMapBuilder,
		// Token: 0x0400948B RID: 38027
		ClearingLevel,
		// Token: 0x0400948C RID: 38028
		Processing,
		// Token: 0x0400948D RID: 38029
		Borders,
		// Token: 0x0400948E RID: 38030
		ProcessRivers,
		// Token: 0x0400948F RID: 38031
		ConvertCellsToEdges,
		// Token: 0x04009490 RID: 38032
		DrawWorldBorder,
		// Token: 0x04009491 RID: 38033
		PlaceTemplates,
		// Token: 0x04009492 RID: 38034
		SettleSim,
		// Token: 0x04009493 RID: 38035
		DetectNaturalCavities,
		// Token: 0x04009494 RID: 38036
		PlacingCreatures,
		// Token: 0x04009495 RID: 38037
		Complete,
		// Token: 0x04009496 RID: 38038
		NumberOfStages
	}
}
