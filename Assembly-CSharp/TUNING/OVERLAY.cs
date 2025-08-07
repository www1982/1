using System;
using STRINGS;
using UnityEngine;

namespace TUNING
{
	// Token: 0x02000F7F RID: 3967
	public class OVERLAY
	{
		// Token: 0x02002130 RID: 8496
		public class TEMPERATURE_LEGEND
		{
			// Token: 0x040097D2 RID: 38866
			public static readonly LegendEntry MAXHOT = new LegendEntry(UI.OVERLAYS.TEMPERATURE.MAXHOT, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0f, 0f, 0f), null, null, true);

			// Token: 0x040097D3 RID: 38867
			public static readonly LegendEntry EXTREMEHOT = new LegendEntry(UI.OVERLAYS.TEMPERATURE.EXTREMEHOT, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0f, 0f, 0f), null, null, true);

			// Token: 0x040097D4 RID: 38868
			public static readonly LegendEntry VERYHOT = new LegendEntry(UI.OVERLAYS.TEMPERATURE.VERYHOT, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(1f, 0f, 0f), null, null, true);

			// Token: 0x040097D5 RID: 38869
			public static readonly LegendEntry HOT = new LegendEntry(UI.OVERLAYS.TEMPERATURE.HOT, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0f, 1f, 0f), null, null, true);

			// Token: 0x040097D6 RID: 38870
			public static readonly LegendEntry TEMPERATE = new LegendEntry(UI.OVERLAYS.TEMPERATURE.TEMPERATE, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0f, 0f, 0f), null, null, true);

			// Token: 0x040097D7 RID: 38871
			public static readonly LegendEntry COLD = new LegendEntry(UI.OVERLAYS.TEMPERATURE.COLD, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0f, 0f, 1f), null, null, true);

			// Token: 0x040097D8 RID: 38872
			public static readonly LegendEntry VERYCOLD = new LegendEntry(UI.OVERLAYS.TEMPERATURE.VERYCOLD, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0f, 0f, 1f), null, null, true);

			// Token: 0x040097D9 RID: 38873
			public static readonly LegendEntry EXTREMECOLD = new LegendEntry(UI.OVERLAYS.TEMPERATURE.EXTREMECOLD, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0f, 0f, 0f), null, null, true);
		}

		// Token: 0x02002131 RID: 8497
		public class HEATFLOW_LEGEND
		{
			// Token: 0x040097DA RID: 38874
			public static readonly LegendEntry HEATING = new LegendEntry(UI.OVERLAYS.HEATFLOW.HEATING, UI.OVERLAYS.HEATFLOW.TOOLTIPS.HEATING, new Color(0f, 0f, 0f), null, null, true);

			// Token: 0x040097DB RID: 38875
			public static readonly LegendEntry NEUTRAL = new LegendEntry(UI.OVERLAYS.HEATFLOW.NEUTRAL, UI.OVERLAYS.HEATFLOW.TOOLTIPS.NEUTRAL, new Color(0f, 0f, 0f), null, null, true);

			// Token: 0x040097DC RID: 38876
			public static readonly LegendEntry COOLING = new LegendEntry(UI.OVERLAYS.HEATFLOW.COOLING, UI.OVERLAYS.HEATFLOW.TOOLTIPS.COOLING, new Color(0f, 0f, 0f), null, null, true);
		}

		// Token: 0x02002132 RID: 8498
		public class POWER_LEGEND
		{
			// Token: 0x040097DD RID: 38877
			public const float WATTAGE_WARNING_THRESHOLD = 0.75f;
		}
	}
}
