using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CF8 RID: 3320
public class KIconToggleMenu : KScreen
{
	// Token: 0x14000029 RID: 41
	// (add) Token: 0x06006645 RID: 26181 RVA: 0x0026A1D4 File Offset: 0x002683D4
	// (remove) Token: 0x06006646 RID: 26182 RVA: 0x0026A20C File Offset: 0x0026840C
	public event KIconToggleMenu.OnSelect onSelect;

	// Token: 0x06006647 RID: 26183 RVA: 0x0026A241 File Offset: 0x00268441
	public void Setup(IList<KIconToggleMenu.ToggleInfo> toggleInfo)
	{
		this.toggleInfo = toggleInfo;
		this.RefreshButtons();
	}

	// Token: 0x06006648 RID: 26184 RVA: 0x0026A250 File Offset: 0x00268450
	protected void Setup()
	{
		this.RefreshButtons();
	}

	// Token: 0x06006649 RID: 26185 RVA: 0x0026A258 File Offset: 0x00268458
	protected virtual void RefreshButtons()
	{
		foreach (KToggle ktoggle in this.toggles)
		{
			if (ktoggle != null)
			{
				if (!this.dontDestroyToggles.Contains(ktoggle))
				{
					global::UnityEngine.Object.Destroy(ktoggle.gameObject);
				}
				else
				{
					ktoggle.ClearOnClick();
				}
			}
		}
		this.toggles.Clear();
		this.dontDestroyToggles.Clear();
		if (this.toggleInfo == null)
		{
			return;
		}
		Transform transform = ((this.toggleParent != null) ? this.toggleParent : base.transform);
		for (int i = 0; i < this.toggleInfo.Count; i++)
		{
			int idx = i;
			KIconToggleMenu.ToggleInfo toggleInfo = this.toggleInfo[i];
			KToggle ktoggle2;
			if (toggleInfo.instanceOverride != null)
			{
				ktoggle2 = toggleInfo.instanceOverride;
				this.dontDestroyToggles.Add(ktoggle2);
			}
			else if (toggleInfo.prefabOverride)
			{
				ktoggle2 = Util.KInstantiateUI<KToggle>(toggleInfo.prefabOverride.gameObject, transform.gameObject, true);
			}
			else
			{
				ktoggle2 = Util.KInstantiateUI<KToggle>(this.prefab.gameObject, transform.gameObject, true);
			}
			ktoggle2.Deselect();
			ktoggle2.gameObject.name = "Toggle:" + toggleInfo.text;
			ktoggle2.group = this.group;
			ktoggle2.onClick += delegate
			{
				this.OnClick(idx);
			};
			LocText componentInChildren = ktoggle2.transform.GetComponentInChildren<LocText>();
			if (componentInChildren != null)
			{
				componentInChildren.SetText(toggleInfo.text);
			}
			if (toggleInfo.getSpriteCB != null)
			{
				ktoggle2.fgImage.sprite = toggleInfo.getSpriteCB();
			}
			else if (toggleInfo.icon != null)
			{
				ktoggle2.fgImage.sprite = Assets.GetSprite(toggleInfo.icon);
			}
			toggleInfo.SetToggle(ktoggle2);
			this.toggles.Add(ktoggle2);
		}
	}

	// Token: 0x0600664A RID: 26186 RVA: 0x0026A480 File Offset: 0x00268680
	public Sprite GetIcon(string name)
	{
		foreach (Sprite sprite in this.icons)
		{
			if (sprite.name == name)
			{
				return sprite;
			}
		}
		return null;
	}

	// Token: 0x0600664B RID: 26187 RVA: 0x0026A4B8 File Offset: 0x002686B8
	public virtual void ClearSelection()
	{
		if (this.toggles == null)
		{
			return;
		}
		foreach (KToggle ktoggle in this.toggles)
		{
			ktoggle.Deselect();
			ktoggle.ClearAnimState();
		}
		this.selected = -1;
	}

	// Token: 0x0600664C RID: 26188 RVA: 0x0026A520 File Offset: 0x00268720
	private void OnClick(int i)
	{
		if (this.onSelect == null)
		{
			return;
		}
		this.selected = i;
		this.onSelect(this.toggleInfo[i]);
		if (!this.toggles[i].isOn)
		{
			this.selected = -1;
		}
		for (int j = 0; j < this.toggles.Count; j++)
		{
			if (j != this.selected)
			{
				this.toggles[j].isOn = false;
			}
		}
	}

	// Token: 0x0600664D RID: 26189 RVA: 0x0026A5A0 File Offset: 0x002687A0
	public override void OnKeyDown(KButtonEvent e)
	{
		if (this.toggles == null)
		{
			return;
		}
		if (this.toggleInfo == null)
		{
			return;
		}
		for (int i = 0; i < this.toggleInfo.Count; i++)
		{
			if (this.toggles[i].isActiveAndEnabled)
			{
				global::Action hotKey = this.toggleInfo[i].hotKey;
				if (hotKey != global::Action.NumActions && e.TryConsume(hotKey))
				{
					if (this.selected != i || this.repeatKeyDownToggles)
					{
						this.toggles[i].Click();
						if (this.selected == i)
						{
							this.toggles[i].Deselect();
						}
						this.selected = i;
						return;
					}
					break;
				}
			}
		}
	}

	// Token: 0x0600664E RID: 26190 RVA: 0x0026A652 File Offset: 0x00268852
	public virtual void Close()
	{
		this.ClearSelection();
		this.Show(false);
	}

	// Token: 0x04004617 RID: 17943
	[SerializeField]
	private Transform toggleParent;

	// Token: 0x04004618 RID: 17944
	[SerializeField]
	private KToggle prefab;

	// Token: 0x04004619 RID: 17945
	[SerializeField]
	private ToggleGroup group;

	// Token: 0x0400461A RID: 17946
	[SerializeField]
	private Sprite[] icons;

	// Token: 0x0400461B RID: 17947
	[SerializeField]
	public TextStyleSetting ToggleToolTipTextStyleSetting;

	// Token: 0x0400461C RID: 17948
	[SerializeField]
	public TextStyleSetting ToggleToolTipHeaderTextStyleSetting;

	// Token: 0x0400461D RID: 17949
	[SerializeField]
	protected bool repeatKeyDownToggles = true;

	// Token: 0x0400461E RID: 17950
	protected KToggle currentlySelectedToggle;

	// Token: 0x04004620 RID: 17952
	protected IList<KIconToggleMenu.ToggleInfo> toggleInfo;

	// Token: 0x04004621 RID: 17953
	protected List<KToggle> toggles = new List<KToggle>();

	// Token: 0x04004622 RID: 17954
	private List<KToggle> dontDestroyToggles = new List<KToggle>();

	// Token: 0x04004623 RID: 17955
	protected int selected = -1;

	// Token: 0x02001EC2 RID: 7874
	// (Invoke) Token: 0x0600B149 RID: 45385
	public delegate void OnSelect(KIconToggleMenu.ToggleInfo toggleInfo);

	// Token: 0x02001EC3 RID: 7875
	public class ToggleInfo
	{
		// Token: 0x0600B14C RID: 45388 RVA: 0x003D46E4 File Offset: 0x003D28E4
		public ToggleInfo(string text, string icon, object user_data = null, global::Action hotkey = global::Action.NumActions, string tooltip = "", string tooltip_header = "")
		{
			this.text = text;
			this.userData = user_data;
			this.icon = icon;
			this.hotKey = hotkey;
			this.tooltip = tooltip;
			this.tooltipHeader = tooltip_header;
			this.getTooltipText = new ToolTip.ComplexTooltipDelegate(this.DefaultGetTooltipText);
		}

		// Token: 0x0600B14D RID: 45389 RVA: 0x003D4737 File Offset: 0x003D2937
		public ToggleInfo(string text, object user_data, global::Action hotkey, Func<Sprite> get_sprite_cb)
		{
			this.text = text;
			this.userData = user_data;
			this.hotKey = hotkey;
			this.getSpriteCB = get_sprite_cb;
		}

		// Token: 0x0600B14E RID: 45390 RVA: 0x003D475C File Offset: 0x003D295C
		public virtual void SetToggle(KToggle toggle)
		{
			this.toggle = toggle;
			toggle.GetComponent<ToolTip>().OnComplexToolTip = this.getTooltipText;
		}

		// Token: 0x0600B14F RID: 45391 RVA: 0x003D4778 File Offset: 0x003D2978
		protected virtual List<global::Tuple<string, TextStyleSetting>> DefaultGetTooltipText()
		{
			List<global::Tuple<string, TextStyleSetting>> list = new List<global::Tuple<string, TextStyleSetting>>();
			if (this.tooltipHeader != null)
			{
				list.Add(new global::Tuple<string, TextStyleSetting>(this.tooltipHeader, ToolTipScreen.Instance.defaultTooltipHeaderStyle));
			}
			list.Add(new global::Tuple<string, TextStyleSetting>(this.tooltip, ToolTipScreen.Instance.defaultTooltipBodyStyle));
			return list;
		}

		// Token: 0x04008ECB RID: 36555
		public string text;

		// Token: 0x04008ECC RID: 36556
		public object userData;

		// Token: 0x04008ECD RID: 36557
		public string icon;

		// Token: 0x04008ECE RID: 36558
		public string tooltip;

		// Token: 0x04008ECF RID: 36559
		public string tooltipHeader;

		// Token: 0x04008ED0 RID: 36560
		public KToggle toggle;

		// Token: 0x04008ED1 RID: 36561
		public global::Action hotKey;

		// Token: 0x04008ED2 RID: 36562
		public ToolTip.ComplexTooltipDelegate getTooltipText;

		// Token: 0x04008ED3 RID: 36563
		public Func<Sprite> getSpriteCB;

		// Token: 0x04008ED4 RID: 36564
		public KToggle prefabOverride;

		// Token: 0x04008ED5 RID: 36565
		public KToggle instanceOverride;
	}
}
