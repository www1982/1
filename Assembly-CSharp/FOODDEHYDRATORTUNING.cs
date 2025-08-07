using System;

// Token: 0x02000208 RID: 520
public class FOODDEHYDRATORTUNING
{
	// Token: 0x0400072E RID: 1838
	public const float INTERNAL_WORK_TIME = 250f;

	// Token: 0x0400072F RID: 1839
	public const float DUPE_WORK_TIME = 50f;

	// Token: 0x04000730 RID: 1840
	public const float GAS_CONSUMPTION_PER_SECOND = 0.020000001f;

	// Token: 0x04000731 RID: 1841
	public const float REQUIRED_FUEL_AMOUNT = 5.0000005f;

	// Token: 0x04000732 RID: 1842
	public const float CO2_EMIT_RATE = 0.0050000004f;

	// Token: 0x04000733 RID: 1843
	public const float CO2_EMIT_TEMPERATURE = 348.15f;

	// Token: 0x04000734 RID: 1844
	public const float PLASTIC_KG = 12f;

	// Token: 0x04000735 RID: 1845
	public const float WATER_OUTPUT_KG = 6f;

	// Token: 0x04000736 RID: 1846
	public const float FOOD_PACKETS = 6f;

	// Token: 0x04000737 RID: 1847
	public const float KCAL_PER_PACKET = 1000f;

	// Token: 0x04000738 RID: 1848
	public const float FOOD_KCAL = 6000000f;

	// Token: 0x04000739 RID: 1849
	public static Tag FUEL_TAG = SimHashes.Methane.CreateTag();
}
