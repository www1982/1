using System;
using TUNING;
using UnityEngine;

// Token: 0x020000AF RID: 175
public static class MooTuning
{
	// Token: 0x04000233 RID: 563
	public static readonly float STANDARD_LIFESPAN = 75f;

	// Token: 0x04000234 RID: 564
	public static readonly float STANDARD_CALORIES_PER_CYCLE = 200000f;

	// Token: 0x04000235 RID: 565
	public static readonly float STANDARD_STARVE_CYCLES = 6f;

	// Token: 0x04000236 RID: 566
	public static readonly float STANDARD_STOMACH_SIZE = MooTuning.STANDARD_CALORIES_PER_CYCLE * MooTuning.STANDARD_STARVE_CYCLES;

	// Token: 0x04000237 RID: 567
	public static readonly int PEN_SIZE_PER_CREATURE = CREATURES.SPACE_REQUIREMENTS.TIER4;

	// Token: 0x04000238 RID: 568
	private static readonly float BECKONS_PER_LIFESPAN = 4f;

	// Token: 0x04000239 RID: 569
	private static readonly float BECKON_FUDGE_CYCLES = 11f;

	// Token: 0x0400023A RID: 570
	private static readonly float BECKON_CYCLES = Mathf.Floor((MooTuning.STANDARD_LIFESPAN - MooTuning.BECKON_FUDGE_CYCLES) / MooTuning.BECKONS_PER_LIFESPAN);

	// Token: 0x0400023B RID: 571
	public static readonly float WELLFED_EFFECT = 100f / (600f * MooTuning.BECKON_CYCLES);

	// Token: 0x0400023C RID: 572
	public static readonly float WELLFED_CALORIES_PER_CYCLE = MooTuning.STANDARD_CALORIES_PER_CYCLE * 0.9f;

	// Token: 0x0400023D RID: 573
	public static readonly float ELIGIBLE_MILKING_PERCENTAGE = 1f;

	// Token: 0x0400023E RID: 574
	public static readonly float MILK_PER_CYCLE = 50f;

	// Token: 0x0400023F RID: 575
	private static readonly float CYCLES_UNTIL_MILKING = 4f;

	// Token: 0x04000240 RID: 576
	public static readonly float MILK_CAPACITY = MooTuning.MILK_PER_CYCLE * MooTuning.CYCLES_UNTIL_MILKING;

	// Token: 0x04000241 RID: 577
	public static readonly float MILK_AMOUNT_AT_MILKING = MooTuning.MILK_PER_CYCLE * MooTuning.CYCLES_UNTIL_MILKING;

	// Token: 0x04000242 RID: 578
	public static readonly float MILK_PRODUCTION_PERCENTAGE_PER_SECOND = 100f / (600f * MooTuning.CYCLES_UNTIL_MILKING);
}
