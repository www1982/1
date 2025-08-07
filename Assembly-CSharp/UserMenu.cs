using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200064B RID: 1611
public class UserMenu
{
	// Token: 0x060026F0 RID: 9968 RVA: 0x000DEA73 File Offset: 0x000DCC73
	public void Refresh(GameObject go)
	{
		Game.Instance.Trigger(1980521255, go);
	}

	// Token: 0x060026F1 RID: 9969 RVA: 0x000DEA88 File Offset: 0x000DCC88
	public void AddButton(GameObject go, KIconButtonMenu.ButtonInfo button, float sort_order = 1f)
	{
		if (button.onClick != null)
		{
			global::System.Action callback = button.onClick;
			button.onClick = delegate
			{
				callback();
				Game.Instance.Trigger(1980521255, go);
			};
		}
		this.buttons.Add(new KeyValuePair<KIconButtonMenu.ButtonInfo, float>(button, sort_order));
	}

	// Token: 0x060026F2 RID: 9970 RVA: 0x000DEADA File Offset: 0x000DCCDA
	public void AddSlider(GameObject go, UserMenu.SliderInfo slider)
	{
		this.sliders.Add(slider);
	}

	// Token: 0x060026F3 RID: 9971 RVA: 0x000DEAE8 File Offset: 0x000DCCE8
	public void AppendToScreen(GameObject go, UserMenuScreen screen)
	{
		this.buttons.Clear();
		this.sliders.Clear();
		go.Trigger(493375141, null);
		if (this.buttons.Count > 0)
		{
			this.buttons.Sort(delegate(KeyValuePair<KIconButtonMenu.ButtonInfo, float> x, KeyValuePair<KIconButtonMenu.ButtonInfo, float> y)
			{
				if (x.Value == y.Value)
				{
					return 0;
				}
				if (x.Value > y.Value)
				{
					return 1;
				}
				return -1;
			});
			for (int i = 0; i < this.buttons.Count; i++)
			{
				this.sortedButtons.Add(this.buttons[i].Key);
			}
			screen.AddButtons(this.sortedButtons);
			this.sortedButtons.Clear();
		}
		if (this.sliders.Count > 0)
		{
			screen.AddSliders(this.sliders);
		}
	}

	// Token: 0x040016B9 RID: 5817
	public const float DECONSTRUCT_PRIORITY = 0f;

	// Token: 0x040016BA RID: 5818
	public const float DRAWPATHS_PRIORITY = 0.1f;

	// Token: 0x040016BB RID: 5819
	public const float FOLLOWCAM_PRIORITY = 0.3f;

	// Token: 0x040016BC RID: 5820
	public const float SETDIRECTION_PRIORITY = 0.4f;

	// Token: 0x040016BD RID: 5821
	public const float AUTOBOTTLE_PRIORITY = 0.4f;

	// Token: 0x040016BE RID: 5822
	public const float AUTOREPAIR_PRIORITY = 0.5f;

	// Token: 0x040016BF RID: 5823
	public const float DEFAULT_PRIORITY = 1f;

	// Token: 0x040016C0 RID: 5824
	public const float SUITEQUIP_PRIORITY = 2f;

	// Token: 0x040016C1 RID: 5825
	public const float AUTODISINFECT_PRIORITY = 10f;

	// Token: 0x040016C2 RID: 5826
	public const float ROCKETUSAGERESTRICTION_PRIORITY = 11f;

	// Token: 0x040016C3 RID: 5827
	private List<KeyValuePair<KIconButtonMenu.ButtonInfo, float>> buttons = new List<KeyValuePair<KIconButtonMenu.ButtonInfo, float>>();

	// Token: 0x040016C4 RID: 5828
	private List<UserMenu.SliderInfo> sliders = new List<UserMenu.SliderInfo>();

	// Token: 0x040016C5 RID: 5829
	private List<KIconButtonMenu.ButtonInfo> sortedButtons = new List<KIconButtonMenu.ButtonInfo>();

	// Token: 0x020014D1 RID: 5329
	public class SliderInfo
	{
		// Token: 0x04006DF8 RID: 28152
		public MinMaxSlider.LockingType lockType = MinMaxSlider.LockingType.Drag;

		// Token: 0x04006DF9 RID: 28153
		public MinMaxSlider.Mode mode;

		// Token: 0x04006DFA RID: 28154
		public Slider.Direction direction;

		// Token: 0x04006DFB RID: 28155
		public bool interactable = true;

		// Token: 0x04006DFC RID: 28156
		public bool lockRange;

		// Token: 0x04006DFD RID: 28157
		public string toolTip;

		// Token: 0x04006DFE RID: 28158
		public string toolTipMin;

		// Token: 0x04006DFF RID: 28159
		public string toolTipMax;

		// Token: 0x04006E00 RID: 28160
		public float minLimit;

		// Token: 0x04006E01 RID: 28161
		public float maxLimit = 100f;

		// Token: 0x04006E02 RID: 28162
		public float currentMinValue = 10f;

		// Token: 0x04006E03 RID: 28163
		public float currentMaxValue = 90f;

		// Token: 0x04006E04 RID: 28164
		public GameObject sliderGO;

		// Token: 0x04006E05 RID: 28165
		public Action<MinMaxSlider> onMinChange;

		// Token: 0x04006E06 RID: 28166
		public Action<MinMaxSlider> onMaxChange;
	}
}
