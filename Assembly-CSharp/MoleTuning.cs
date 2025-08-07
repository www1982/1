using System;
using System.Collections.Generic;
using TUNING;

// Token: 0x020000AD RID: 173
public static class MoleTuning
{
	// Token: 0x04000228 RID: 552
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BASE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "MoleEgg".ToTag(),
			weight = 0.98f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "MoleDelicacyEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x04000229 RID: 553
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_DELICACY = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "MoleEgg".ToTag(),
			weight = 0.32f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "MoleDelicacyEgg".ToTag(),
			weight = 0.65f
		}
	};

	// Token: 0x0400022A RID: 554
	public static float STANDARD_CALORIES_PER_CYCLE = 4800000f;

	// Token: 0x0400022B RID: 555
	public static float STANDARD_STARVE_CYCLES = 10f;

	// Token: 0x0400022C RID: 556
	public static float STANDARD_STOMACH_SIZE = MoleTuning.STANDARD_CALORIES_PER_CYCLE * MoleTuning.STANDARD_STARVE_CYCLES;

	// Token: 0x0400022D RID: 557
	public static float DELICACY_STOMACH_SIZE = MoleTuning.STANDARD_STOMACH_SIZE / 2f;

	// Token: 0x0400022E RID: 558
	public static int PEN_SIZE_PER_CREATURE = CREATURES.SPACE_REQUIREMENTS.TIER2;

	// Token: 0x0400022F RID: 559
	public static float EGG_MASS = 2f;

	// Token: 0x04000230 RID: 560
	public static int DEPTH_TO_HIDE = 2;

	// Token: 0x04000231 RID: 561
	public static HashedString[] GINGER_SYMBOL_NAMES = new HashedString[] { "del_ginger", "del_ginger1", "del_ginger2", "del_ginger3", "del_ginger4", "del_ginger5" };
}
