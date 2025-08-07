using System;

// Token: 0x02000B0E RID: 2830
public class SimData
{
	// Token: 0x040037E3 RID: 14307
	public unsafe Sim.EmittedMassInfo* emittedMassEntries;

	// Token: 0x040037E4 RID: 14308
	public unsafe Sim.ElementChunkInfo* elementChunks;

	// Token: 0x040037E5 RID: 14309
	public unsafe Sim.BuildingTemperatureInfo* buildingTemperatures;

	// Token: 0x040037E6 RID: 14310
	public unsafe Sim.DiseaseEmittedInfo* diseaseEmittedInfos;

	// Token: 0x040037E7 RID: 14311
	public unsafe Sim.DiseaseConsumedInfo* diseaseConsumedInfos;
}
