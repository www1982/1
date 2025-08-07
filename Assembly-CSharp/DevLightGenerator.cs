using System;
using STRINGS;

// Token: 0x02000713 RID: 1811
public class DevLightGenerator : Light2D, IMultiSliderControl
{
	// Token: 0x06002D77 RID: 11639 RVA: 0x00104D83 File Offset: 0x00102F83
	public DevLightGenerator()
	{
		this.sliderControls = new ISliderControl[]
		{
			new DevLightGenerator.LuxController(this),
			new DevLightGenerator.RangeController(this),
			new DevLightGenerator.FalloffController(this)
		};
	}

	// Token: 0x1700026F RID: 623
	// (get) Token: 0x06002D78 RID: 11640 RVA: 0x00104DB2 File Offset: 0x00102FB2
	string IMultiSliderControl.SidescreenTitleKey
	{
		get
		{
			return "STRINGS.BUILDINGS.PREFABS.DEVLIGHTGENERATOR.NAME";
		}
	}

	// Token: 0x17000270 RID: 624
	// (get) Token: 0x06002D79 RID: 11641 RVA: 0x00104DB9 File Offset: 0x00102FB9
	ISliderControl[] IMultiSliderControl.sliderControls
	{
		get
		{
			return this.sliderControls;
		}
	}

	// Token: 0x06002D7A RID: 11642 RVA: 0x00104DC1 File Offset: 0x00102FC1
	bool IMultiSliderControl.SidescreenEnabled()
	{
		return true;
	}

	// Token: 0x04001AC0 RID: 6848
	protected ISliderControl[] sliderControls;

	// Token: 0x020015B1 RID: 5553
	protected class LuxController : ISingleSliderControl, ISliderControl
	{
		// Token: 0x0600925E RID: 37470 RVA: 0x00366AB4 File Offset: 0x00364CB4
		public LuxController(Light2D t)
		{
			this.target = t;
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x0600925F RID: 37471 RVA: 0x00366AC3 File Offset: 0x00364CC3
		public string SliderTitleKey
		{
			get
			{
				return "STRINGS.BUILDINGS.PREFABS.DEVLIGHTGENERATOR.BRIGHTNESS_LABEL";
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06009260 RID: 37472 RVA: 0x00366ACA File Offset: 0x00364CCA
		public string SliderUnits
		{
			get
			{
				return UI.UNITSUFFIXES.LIGHT.LUX;
			}
		}

		// Token: 0x06009261 RID: 37473 RVA: 0x00366AD6 File Offset: 0x00364CD6
		public float GetSliderMax(int index)
		{
			return 100000f;
		}

		// Token: 0x06009262 RID: 37474 RVA: 0x00366ADD File Offset: 0x00364CDD
		public float GetSliderMin(int index)
		{
			return 0f;
		}

		// Token: 0x06009263 RID: 37475 RVA: 0x00366AE4 File Offset: 0x00364CE4
		public string GetSliderTooltip(int index)
		{
			return string.Format(UI.GAMEOBJECTEFFECTS.EMITS_LIGHT_LUX, this.target.Lux);
		}

		// Token: 0x06009264 RID: 37476 RVA: 0x00366B05 File Offset: 0x00364D05
		public string GetSliderTooltipKey(int index)
		{
			return "<unused>";
		}

		// Token: 0x06009265 RID: 37477 RVA: 0x00366B0C File Offset: 0x00364D0C
		public float GetSliderValue(int index)
		{
			return (float)this.target.Lux;
		}

		// Token: 0x06009266 RID: 37478 RVA: 0x00366B1A File Offset: 0x00364D1A
		public void SetSliderValue(float value, int index)
		{
			this.target.Lux = (int)value;
			this.target.FullRefresh();
		}

		// Token: 0x06009267 RID: 37479 RVA: 0x00366B34 File Offset: 0x00364D34
		public int SliderDecimalPlaces(int index)
		{
			return 0;
		}

		// Token: 0x04007091 RID: 28817
		protected Light2D target;
	}

	// Token: 0x020015B2 RID: 5554
	protected class RangeController : ISingleSliderControl, ISliderControl
	{
		// Token: 0x06009268 RID: 37480 RVA: 0x00366B37 File Offset: 0x00364D37
		public RangeController(Light2D t)
		{
			this.target = t;
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06009269 RID: 37481 RVA: 0x00366B46 File Offset: 0x00364D46
		public string SliderTitleKey
		{
			get
			{
				return "STRINGS.BUILDINGS.PREFABS.DEVLIGHTGENERATOR.RANGE_LABEL";
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x0600926A RID: 37482 RVA: 0x00366B4D File Offset: 0x00364D4D
		public string SliderUnits
		{
			get
			{
				return UI.UNITSUFFIXES.TILES;
			}
		}

		// Token: 0x0600926B RID: 37483 RVA: 0x00366B59 File Offset: 0x00364D59
		public float GetSliderMax(int index)
		{
			return 20f;
		}

		// Token: 0x0600926C RID: 37484 RVA: 0x00366B60 File Offset: 0x00364D60
		public float GetSliderMin(int index)
		{
			return 1f;
		}

		// Token: 0x0600926D RID: 37485 RVA: 0x00366B67 File Offset: 0x00364D67
		public string GetSliderTooltip(int index)
		{
			return string.Format(UI.GAMEOBJECTEFFECTS.EMITS_LIGHT, this.target.Range);
		}

		// Token: 0x0600926E RID: 37486 RVA: 0x00366B88 File Offset: 0x00364D88
		public string GetSliderTooltipKey(int index)
		{
			return "";
		}

		// Token: 0x0600926F RID: 37487 RVA: 0x00366B8F File Offset: 0x00364D8F
		public float GetSliderValue(int index)
		{
			return this.target.Range;
		}

		// Token: 0x06009270 RID: 37488 RVA: 0x00366B9D File Offset: 0x00364D9D
		public void SetSliderValue(float value, int index)
		{
			this.target.Range = (float)((int)value);
			this.target.FullRefresh();
		}

		// Token: 0x06009271 RID: 37489 RVA: 0x00366BB8 File Offset: 0x00364DB8
		public int SliderDecimalPlaces(int index)
		{
			return 0;
		}

		// Token: 0x04007092 RID: 28818
		protected Light2D target;
	}

	// Token: 0x020015B3 RID: 5555
	protected class FalloffController : ISingleSliderControl, ISliderControl
	{
		// Token: 0x06009272 RID: 37490 RVA: 0x00366BBB File Offset: 0x00364DBB
		public FalloffController(Light2D t)
		{
			this.target = t;
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06009273 RID: 37491 RVA: 0x00366BCA File Offset: 0x00364DCA
		public string SliderTitleKey
		{
			get
			{
				return "STRINGS.BUILDINGS.PREFABS.DEVLIGHTGENERATOR.FALLOFF_LABEL";
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06009274 RID: 37492 RVA: 0x00366BD1 File Offset: 0x00364DD1
		public string SliderUnits
		{
			get
			{
				return UI.UNITSUFFIXES.PERCENT;
			}
		}

		// Token: 0x06009275 RID: 37493 RVA: 0x00366BDD File Offset: 0x00364DDD
		public float GetSliderMax(int index)
		{
			return 100f;
		}

		// Token: 0x06009276 RID: 37494 RVA: 0x00366BE4 File Offset: 0x00364DE4
		public float GetSliderMin(int index)
		{
			return 1f;
		}

		// Token: 0x06009277 RID: 37495 RVA: 0x00366BEB File Offset: 0x00364DEB
		public string GetSliderTooltip(int index)
		{
			return string.Format("{0}", this.target.FalloffRate * 100f);
		}

		// Token: 0x06009278 RID: 37496 RVA: 0x00366C0D File Offset: 0x00364E0D
		public string GetSliderTooltipKey(int index)
		{
			return "";
		}

		// Token: 0x06009279 RID: 37497 RVA: 0x00366C14 File Offset: 0x00364E14
		public float GetSliderValue(int index)
		{
			return this.target.FalloffRate * 100f;
		}

		// Token: 0x0600927A RID: 37498 RVA: 0x00366C28 File Offset: 0x00364E28
		public void SetSliderValue(float value, int index)
		{
			this.target.FalloffRate = value / 100f;
			this.target.FullRefresh();
		}

		// Token: 0x0600927B RID: 37499 RVA: 0x00366C47 File Offset: 0x00364E47
		public int SliderDecimalPlaces(int index)
		{
			return 0;
		}

		// Token: 0x04007093 RID: 28819
		protected Light2D target;
	}
}
