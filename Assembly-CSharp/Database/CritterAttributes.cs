using System;
using Klei.AI;

namespace Database
{
	// Token: 0x02000EE9 RID: 3817
	public class CritterAttributes : ResourceSet<Klei.AI.Attribute>
	{
		// Token: 0x06007973 RID: 31091 RVA: 0x002F963C File Offset: 0x002F783C
		public CritterAttributes(ResourceSet parent)
			: base("CritterAttributes", parent)
		{
			this.Happiness = base.Add(new Klei.AI.Attribute("Happiness", Strings.Get("STRINGS.CREATURES.STATS.HAPPINESS.NAME"), "", Strings.Get("STRINGS.CREATURES.STATS.HAPPINESS.TOOLTIP"), 0f, Klei.AI.Attribute.Display.General, false, "ui_icon_happiness", null, null));
			this.Happiness.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None));
			this.Metabolism = base.Add(new Klei.AI.Attribute("Metabolism", false, Klei.AI.Attribute.Display.Details, false, 100f, "ui_icon_metabolism", null, null, null));
			this.Metabolism.SetFormatter(new ToPercentAttributeFormatter(100f, GameUtil.TimeSlice.None));
		}

		// Token: 0x040056D4 RID: 22228
		public Klei.AI.Attribute Happiness;

		// Token: 0x040056D5 RID: 22229
		public Klei.AI.Attribute Metabolism;
	}
}
