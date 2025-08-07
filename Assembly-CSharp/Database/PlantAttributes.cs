using System;
using Klei.AI;

namespace Database
{
	// Token: 0x02000F02 RID: 3842
	public class PlantAttributes : ResourceSet<Klei.AI.Attribute>
	{
		// Token: 0x060079CB RID: 31179 RVA: 0x002FFF70 File Offset: 0x002FE170
		public PlantAttributes(ResourceSet parent)
			: base("PlantAttributes", parent)
		{
			this.WiltTempRangeMod = base.Add(new Klei.AI.Attribute("WiltTempRangeMod", false, Klei.AI.Attribute.Display.Normal, false, 1f, null, null, null, null));
			this.WiltTempRangeMod.SetFormatter(new PercentAttributeFormatter());
			this.YieldAmount = base.Add(new Klei.AI.Attribute("YieldAmount", false, Klei.AI.Attribute.Display.Normal, false, 0f, null, null, null, null));
			this.YieldAmount.SetFormatter(new PercentAttributeFormatter());
			this.HarvestTime = base.Add(new Klei.AI.Attribute("HarvestTime", false, Klei.AI.Attribute.Display.Normal, false, 0f, null, null, null, null));
			this.HarvestTime.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.Time, GameUtil.TimeSlice.None));
			this.DecorBonus = base.Add(new Klei.AI.Attribute("DecorBonus", false, Klei.AI.Attribute.Display.Normal, false, 0f, null, null, null, null));
			this.DecorBonus.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None));
			this.MinLightLux = base.Add(new Klei.AI.Attribute("MinLightLux", false, Klei.AI.Attribute.Display.Normal, false, 0f, null, null, null, null));
			this.MinLightLux.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.Lux, GameUtil.TimeSlice.None));
			this.FertilizerUsageMod = base.Add(new Klei.AI.Attribute("FertilizerUsageMod", false, Klei.AI.Attribute.Display.Normal, false, 1f, null, null, null, null));
			this.FertilizerUsageMod.SetFormatter(new PercentAttributeFormatter());
			this.MinRadiationThreshold = base.Add(new Klei.AI.Attribute("MinRadiationThreshold", false, Klei.AI.Attribute.Display.Normal, false, 0f, null, null, null, null));
			this.MinRadiationThreshold.SetFormatter(new RadsPerCycleAttributeFormatter());
			this.MaxRadiationThreshold = base.Add(new Klei.AI.Attribute("MaxRadiationThreshold", false, Klei.AI.Attribute.Display.Normal, false, 0f, null, null, null, null));
			this.MaxRadiationThreshold.SetFormatter(new RadsPerCycleAttributeFormatter());
		}

		// Token: 0x04005898 RID: 22680
		public Klei.AI.Attribute WiltTempRangeMod;

		// Token: 0x04005899 RID: 22681
		public Klei.AI.Attribute YieldAmount;

		// Token: 0x0400589A RID: 22682
		public Klei.AI.Attribute HarvestTime;

		// Token: 0x0400589B RID: 22683
		public Klei.AI.Attribute DecorBonus;

		// Token: 0x0400589C RID: 22684
		public Klei.AI.Attribute MinLightLux;

		// Token: 0x0400589D RID: 22685
		public Klei.AI.Attribute FertilizerUsageMod;

		// Token: 0x0400589E RID: 22686
		public Klei.AI.Attribute MinRadiationThreshold;

		// Token: 0x0400589F RID: 22687
		public Klei.AI.Attribute MaxRadiationThreshold;
	}
}
