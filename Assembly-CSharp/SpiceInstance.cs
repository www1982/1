using System;
using Klei.AI;

// Token: 0x0200040F RID: 1039
[Serializable]
public struct SpiceInstance
{
	// Token: 0x1700005A RID: 90
	// (get) Token: 0x0600155C RID: 5468 RVA: 0x00079A56 File Offset: 0x00077C56
	public AttributeModifier CalorieModifier
	{
		get
		{
			return SpiceGrinder.SettingOptions[this.Id].Spice.CalorieModifier;
		}
	}

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x0600155D RID: 5469 RVA: 0x00079A72 File Offset: 0x00077C72
	public AttributeModifier FoodModifier
	{
		get
		{
			return SpiceGrinder.SettingOptions[this.Id].Spice.FoodModifier;
		}
	}

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x0600155E RID: 5470 RVA: 0x00079A8E File Offset: 0x00077C8E
	public Effect StatBonus
	{
		get
		{
			return SpiceGrinder.SettingOptions[this.Id].StatBonus;
		}
	}

	// Token: 0x04000CB0 RID: 3248
	public Tag Id;

	// Token: 0x04000CB1 RID: 3249
	public float TotalKG;
}
