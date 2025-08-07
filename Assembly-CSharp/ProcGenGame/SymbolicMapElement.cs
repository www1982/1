using System;

namespace ProcGenGame
{
	// Token: 0x02000E99 RID: 3737
	public interface SymbolicMapElement
	{
		// Token: 0x0600771C RID: 30492
		void ConvertToMap(Chunk world, TerrainCell.SetValuesFunction SetValues, float temperatureMin, float temperatureRange, SeededRandom rnd);
	}
}
