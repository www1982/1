using System;

// Token: 0x02000098 RID: 152
public static class BeeHiveTuning
{
	// Token: 0x040001B8 RID: 440
	public static float ORE_DELIVERY_AMOUNT = 1f;

	// Token: 0x040001B9 RID: 441
	public static float KG_ORE_EATEN_PER_CYCLE = BeeHiveTuning.ORE_DELIVERY_AMOUNT * 10f;

	// Token: 0x040001BA RID: 442
	public static float STANDARD_CALORIES_PER_CYCLE = 1500000f;

	// Token: 0x040001BB RID: 443
	public static float STANDARD_STARVE_CYCLES = 30f;

	// Token: 0x040001BC RID: 444
	public static float STANDARD_STOMACH_SIZE = BeeHiveTuning.STANDARD_CALORIES_PER_CYCLE * BeeHiveTuning.STANDARD_STARVE_CYCLES;

	// Token: 0x040001BD RID: 445
	public static float CALORIES_PER_KG_OF_ORE = BeeHiveTuning.STANDARD_CALORIES_PER_CYCLE / BeeHiveTuning.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x040001BE RID: 446
	public static float POOP_CONVERSTION_RATE = 0.9f;

	// Token: 0x040001BF RID: 447
	public static Tag CONSUMED_ORE = SimHashes.UraniumOre.CreateTag();

	// Token: 0x040001C0 RID: 448
	public static Tag PRODUCED_ORE = SimHashes.EnrichedUranium.CreateTag();

	// Token: 0x040001C1 RID: 449
	public static float HIVE_GROWTH_TIME = 2f;

	// Token: 0x040001C2 RID: 450
	public static float WASTE_DROPPED_ON_DEATH = 5f;

	// Token: 0x040001C3 RID: 451
	public static int GERMS_DROPPED_ON_DEATH = 10000;
}
