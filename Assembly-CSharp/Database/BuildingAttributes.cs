using System;
using Klei.AI;

namespace Database
{
	// Token: 0x02000EDE RID: 3806
	public class BuildingAttributes : ResourceSet<Klei.AI.Attribute>
	{
		// Token: 0x06007949 RID: 31049 RVA: 0x002EEF98 File Offset: 0x002ED198
		public BuildingAttributes(ResourceSet parent)
			: base("BuildingAttributes", parent)
		{
			this.Decor = base.Add(new Klei.AI.Attribute("Decor", true, Klei.AI.Attribute.Display.General, false, 0f, null, null, null, null));
			this.DecorRadius = base.Add(new Klei.AI.Attribute("DecorRadius", true, Klei.AI.Attribute.Display.General, false, 0f, null, null, null, null));
			this.NoisePollution = base.Add(new Klei.AI.Attribute("NoisePollution", true, Klei.AI.Attribute.Display.General, false, 0f, null, null, null, null));
			this.NoisePollutionRadius = base.Add(new Klei.AI.Attribute("NoisePollutionRadius", true, Klei.AI.Attribute.Display.General, false, 0f, null, null, null, null));
			this.Hygiene = base.Add(new Klei.AI.Attribute("Hygiene", true, Klei.AI.Attribute.Display.General, false, 0f, null, null, null, null));
			this.Comfort = base.Add(new Klei.AI.Attribute("Comfort", true, Klei.AI.Attribute.Display.General, false, 0f, null, null, null, null));
			this.OverheatTemperature = base.Add(new Klei.AI.Attribute("OverheatTemperature", true, Klei.AI.Attribute.Display.General, false, 0f, null, null, null, null));
			this.OverheatTemperature.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.Temperature, GameUtil.TimeSlice.ModifyOnly));
			this.FatalTemperature = base.Add(new Klei.AI.Attribute("FatalTemperature", true, Klei.AI.Attribute.Display.General, false, 0f, null, null, null, null));
			this.FatalTemperature.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.Temperature, GameUtil.TimeSlice.ModifyOnly));
		}

		// Token: 0x04005487 RID: 21639
		public Klei.AI.Attribute Decor;

		// Token: 0x04005488 RID: 21640
		public Klei.AI.Attribute DecorRadius;

		// Token: 0x04005489 RID: 21641
		public Klei.AI.Attribute NoisePollution;

		// Token: 0x0400548A RID: 21642
		public Klei.AI.Attribute NoisePollutionRadius;

		// Token: 0x0400548B RID: 21643
		public Klei.AI.Attribute Hygiene;

		// Token: 0x0400548C RID: 21644
		public Klei.AI.Attribute Comfort;

		// Token: 0x0400548D RID: 21645
		public Klei.AI.Attribute OverheatTemperature;

		// Token: 0x0400548E RID: 21646
		public Klei.AI.Attribute FatalTemperature;
	}
}
