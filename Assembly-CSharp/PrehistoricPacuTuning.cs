using System;
using System.Collections.Generic;
using TUNING;

// Token: 0x020000B8 RID: 184
public static class PrehistoricPacuTuning
{
	// Token: 0x04000266 RID: 614
	public const float LIFESPAWN = 100f;

	// Token: 0x04000267 RID: 615
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BASE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "PrehistoricPacuEgg".ToTag(),
			weight = 1f
		}
	};

	// Token: 0x04000268 RID: 616
	public const int PACUS_EATEN_PER_CYCLE = 1;

	// Token: 0x04000269 RID: 617
	public const float KG_PACU_MEAT_EATEN_PER_CYCLE = 1f;

	// Token: 0x0400026A RID: 618
	public static float STANDARD_STARVE_CYCLES = 5f;

	// Token: 0x0400026B RID: 619
	public static float STANDARD_CALORIES_PER_CYCLE = 100000f;

	// Token: 0x0400026C RID: 620
	public static float STANDARD_STOMACH_SIZE = PrehistoricPacuTuning.STANDARD_CALORIES_PER_CYCLE * PrehistoricPacuTuning.STANDARD_STARVE_CYCLES;

	// Token: 0x0400026D RID: 621
	public static int PEN_SIZE_PER_CREATURE = CREATURES.SPACE_REQUIREMENTS.TIER3;

	// Token: 0x0400026E RID: 622
	public const float POOP_MASS_KG = 60f;

	// Token: 0x0400026F RID: 623
	public static Tag POOP_ELEMENT = SimHashes.Rust.CreateTag();

	// Token: 0x04000270 RID: 624
	public static float EGG_MASS = 4f;
}
