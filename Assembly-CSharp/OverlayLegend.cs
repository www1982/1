using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D91 RID: 3473
public class OverlayLegend : KScreen
{
	// Token: 0x06006C42 RID: 27714 RVA: 0x0028DC64 File Offset: 0x0028BE64
	[ContextMenu("Set all fonts color")]
	public void SetAllFontsColor()
	{
		foreach (OverlayLegend.OverlayInfo overlayInfo in this.overlayInfoList)
		{
			for (int i = 0; i < overlayInfo.infoUnits.Count; i++)
			{
				if (overlayInfo.infoUnits[i].fontColor == Color.clear)
				{
					overlayInfo.infoUnits[i].fontColor = Color.white;
				}
			}
		}
	}

	// Token: 0x06006C43 RID: 27715 RVA: 0x0028DCFC File Offset: 0x0028BEFC
	[ContextMenu("Set all tooltips")]
	public void SetAllTooltips()
	{
		foreach (OverlayLegend.OverlayInfo overlayInfo in this.overlayInfoList)
		{
			string text = overlayInfo.name;
			text = text.Replace("NAME", "");
			for (int i = 0; i < overlayInfo.infoUnits.Count; i++)
			{
				string text2 = overlayInfo.infoUnits[i].description;
				text2 = text2.Replace(text, "");
				text2 = text + "TOOLTIPS." + text2;
				overlayInfo.infoUnits[i].tooltip = text2;
			}
		}
	}

	// Token: 0x06006C44 RID: 27716 RVA: 0x0028DDC0 File Offset: 0x0028BFC0
	[ContextMenu("Set Sliced for empty icons")]
	public void SetSlicedForEmptyIcons()
	{
		foreach (OverlayLegend.OverlayInfo overlayInfo in this.overlayInfoList)
		{
			for (int i = 0; i < overlayInfo.infoUnits.Count; i++)
			{
				if (overlayInfo.infoUnits[i].icon == this.emptySprite)
				{
					overlayInfo.infoUnits[i].sliceIcon = true;
				}
			}
		}
	}

	// Token: 0x06006C45 RID: 27717 RVA: 0x0028DE54 File Offset: 0x0028C054
	protected override void OnSpawn()
	{
		base.ConsumeMouseScroll = true;
		base.OnSpawn();
		if (OverlayLegend.Instance == null)
		{
			OverlayLegend.Instance = this;
			this.activeUnitObjs = new List<GameObject>();
			this.inactiveUnitObjs = new List<GameObject>();
			foreach (OverlayLegend.OverlayInfo overlayInfo in this.overlayInfoList)
			{
				overlayInfo.name = Strings.Get(overlayInfo.name);
				for (int i = 0; i < overlayInfo.infoUnits.Count; i++)
				{
					overlayInfo.infoUnits[i].description = Strings.Get(overlayInfo.infoUnits[i].description);
					if (!string.IsNullOrEmpty(overlayInfo.infoUnits[i].tooltip))
					{
						overlayInfo.infoUnits[i].tooltip = Strings.Get(overlayInfo.infoUnits[i].tooltip);
					}
				}
			}
			base.GetComponent<LayoutElement>().minWidth = (float)(DlcManager.FeatureClusterSpaceEnabled() ? 322 : 288);
			this.ClearLegend();
			return;
		}
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06006C46 RID: 27718 RVA: 0x0028DFB0 File Offset: 0x0028C1B0
	protected override void OnLoadLevel()
	{
		OverlayLegend.Instance = null;
		this.activeDiagrams.Clear();
		global::UnityEngine.Object.Destroy(base.gameObject);
		base.OnLoadLevel();
	}

	// Token: 0x06006C47 RID: 27719 RVA: 0x0028DFD4 File Offset: 0x0028C1D4
	private void SetLegend(OverlayLegend.OverlayInfo overlayInfo)
	{
		if (overlayInfo == null)
		{
			this.ClearLegend();
			return;
		}
		if (!overlayInfo.isProgrammaticallyPopulated && (overlayInfo.infoUnits == null || overlayInfo.infoUnits.Count == 0))
		{
			this.ClearLegend();
			return;
		}
		this.Show(true);
		this.title.text = overlayInfo.name;
		if (overlayInfo.isProgrammaticallyPopulated)
		{
			this.PopulateGeneratedLegend(overlayInfo, false);
		}
		else
		{
			this.PopulateOverlayInfoUnits(overlayInfo, false);
			this.PopulateOverlayDiagrams(overlayInfo, false);
		}
		this.ConfigureUIHeight();
	}

	// Token: 0x06006C48 RID: 27720 RVA: 0x0028E050 File Offset: 0x0028C250
	public void SetLegend(OverlayModes.Mode mode, bool refreshing = false)
	{
		if (this.currentMode != null && this.currentMode.ViewMode() == mode.ViewMode() && !refreshing)
		{
			return;
		}
		this.ClearLegend();
		OverlayLegend.OverlayInfo overlayInfo = this.overlayInfoList.Find((OverlayLegend.OverlayInfo ol) => ol.mode == mode.ViewMode());
		this.currentMode = mode;
		this.SetLegend(overlayInfo);
	}

	// Token: 0x06006C49 RID: 27721 RVA: 0x0028E0C4 File Offset: 0x0028C2C4
	public GameObject GetFreeUnitObject()
	{
		if (this.inactiveUnitObjs.Count == 0)
		{
			this.inactiveUnitObjs.Add(Util.KInstantiateUI(this.unitPrefab, this.inactiveUnitsParent, false));
		}
		GameObject gameObject = this.inactiveUnitObjs[0];
		this.inactiveUnitObjs.RemoveAt(0);
		this.activeUnitObjs.Add(gameObject);
		return gameObject;
	}

	// Token: 0x06006C4A RID: 27722 RVA: 0x0028E124 File Offset: 0x0028C324
	private void RemoveActiveObjects()
	{
		while (this.activeUnitObjs.Count > 0)
		{
			this.activeUnitObjs[0].transform.Find("Icon").GetComponent<Image>().enabled = false;
			this.activeUnitObjs[0].GetComponentInChildren<LocText>().enabled = false;
			this.activeUnitObjs[0].transform.SetParent(this.inactiveUnitsParent.transform);
			this.activeUnitObjs[0].SetActive(false);
			this.inactiveUnitObjs.Add(this.activeUnitObjs[0]);
			this.activeUnitObjs.RemoveAt(0);
		}
	}

	// Token: 0x06006C4B RID: 27723 RVA: 0x0028E1DA File Offset: 0x0028C3DA
	public void ClearLegend()
	{
		this.RemoveActiveObjects();
		this.ClearFilters();
		this.ClearDiagrams();
		this.Show(false);
	}

	// Token: 0x06006C4C RID: 27724 RVA: 0x0028E1F5 File Offset: 0x0028C3F5
	public void ClearFilters()
	{
		if (this.filterMenu != null)
		{
			global::UnityEngine.Object.Destroy(this.filterMenu.gameObject);
		}
		this.filterMenu = null;
	}

	// Token: 0x06006C4D RID: 27725 RVA: 0x0028E21C File Offset: 0x0028C41C
	public void ClearDiagrams()
	{
		for (int i = 0; i < this.activeDiagrams.Count; i++)
		{
			if (this.activeDiagrams[i] != null)
			{
				global::UnityEngine.Object.Destroy(this.activeDiagrams[i]);
			}
		}
		this.activeDiagrams.Clear();
		Vector2 sizeDelta = this.diagramsParent.GetComponent<RectTransform>().sizeDelta;
		sizeDelta.y = 0f;
		this.diagramsParent.GetComponent<RectTransform>().sizeDelta = sizeDelta;
	}

	// Token: 0x06006C4E RID: 27726 RVA: 0x0028E2A0 File Offset: 0x0028C4A0
	public OverlayLegend.OverlayInfo GetOverlayInfo(OverlayModes.Mode mode)
	{
		for (int i = 0; i < this.overlayInfoList.Count; i++)
		{
			if (this.overlayInfoList[i].mode == mode.ViewMode())
			{
				return this.overlayInfoList[i];
			}
		}
		return null;
	}

	// Token: 0x06006C4F RID: 27727 RVA: 0x0028E2F0 File Offset: 0x0028C4F0
	private void PopulateOverlayInfoUnits(OverlayLegend.OverlayInfo overlayInfo, bool isRefresh = false)
	{
		if (overlayInfo.infoUnits != null && overlayInfo.infoUnits.Count > 0)
		{
			this.activeUnitsParent.SetActive(true);
			using (List<OverlayLegend.OverlayInfoUnit>.Enumerator enumerator = overlayInfo.infoUnits.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					OverlayLegend.OverlayInfoUnit overlayInfoUnit = enumerator.Current;
					GameObject freeUnitObject = this.GetFreeUnitObject();
					if (overlayInfoUnit.icon != null)
					{
						Image component = freeUnitObject.transform.Find("Icon").GetComponent<Image>();
						component.gameObject.SetActive(true);
						component.sprite = overlayInfoUnit.icon;
						component.color = overlayInfoUnit.color;
						component.enabled = true;
						component.type = (overlayInfoUnit.sliceIcon ? Image.Type.Sliced : Image.Type.Simple);
					}
					else
					{
						freeUnitObject.transform.Find("Icon").gameObject.SetActive(false);
					}
					if (!string.IsNullOrEmpty(overlayInfoUnit.description))
					{
						LocText componentInChildren = freeUnitObject.GetComponentInChildren<LocText>();
						componentInChildren.text = string.Format(overlayInfoUnit.description, overlayInfoUnit.formatData);
						componentInChildren.color = overlayInfoUnit.fontColor;
						componentInChildren.enabled = true;
					}
					ToolTip component2 = freeUnitObject.GetComponent<ToolTip>();
					if (!string.IsNullOrEmpty(overlayInfoUnit.tooltip))
					{
						component2.toolTip = string.Format(overlayInfoUnit.tooltip, overlayInfoUnit.tooltipFormatData);
						component2.enabled = true;
					}
					else
					{
						component2.enabled = false;
					}
					freeUnitObject.SetActive(true);
					freeUnitObject.transform.SetParent(this.activeUnitsParent.transform);
				}
				return;
			}
		}
		this.activeUnitsParent.SetActive(false);
	}

	// Token: 0x06006C50 RID: 27728 RVA: 0x0028E49C File Offset: 0x0028C69C
	private void PopulateOverlayDiagrams(OverlayLegend.OverlayInfo overlayInfo, bool isRefresh = false)
	{
		if (!isRefresh)
		{
			if (overlayInfo.mode == OverlayModes.Temperature.ID)
			{
				Game.TemperatureOverlayModes temperatureOverlayMode = Game.Instance.temperatureOverlayMode;
				if (temperatureOverlayMode != Game.TemperatureOverlayModes.AbsoluteTemperature)
				{
					if (temperatureOverlayMode == Game.TemperatureOverlayModes.RelativeTemperature)
					{
						this.ClearDiagrams();
						overlayInfo = this.overlayInfoList.Find((OverlayLegend.OverlayInfo match) => match.name == UI.OVERLAYS.RELATIVETEMPERATURE.NAME);
					}
				}
				else
				{
					SimDebugView.Instance.user_temperatureThresholds[0] = 0f;
					SimDebugView.Instance.user_temperatureThresholds[1] = 2073f;
				}
			}
			if (overlayInfo.diagrams != null && overlayInfo.diagrams.Count > 0)
			{
				this.diagramsParent.SetActive(true);
				using (List<GameObject>.Enumerator enumerator = overlayInfo.diagrams.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GameObject gameObject = enumerator.Current;
						GameObject gameObject2 = Util.KInstantiateUI(gameObject, this.diagramsParent, false);
						this.activeDiagrams.Add(gameObject2);
					}
					return;
				}
			}
			this.diagramsParent.SetActive(false);
		}
	}

	// Token: 0x06006C51 RID: 27729 RVA: 0x0028E5B8 File Offset: 0x0028C7B8
	private void PopulateGeneratedLegend(OverlayLegend.OverlayInfo info, bool isRefresh = false)
	{
		if (isRefresh)
		{
			this.RemoveActiveObjects();
			this.ClearDiagrams();
		}
		if (info.infoUnits != null && info.infoUnits.Count > 0)
		{
			this.PopulateOverlayInfoUnits(info, isRefresh);
		}
		this.PopulateOverlayDiagrams(info, false);
		List<LegendEntry> customLegendData = this.currentMode.GetCustomLegendData();
		if (customLegendData != null)
		{
			this.activeUnitsParent.SetActive(true);
			using (List<LegendEntry>.Enumerator enumerator = customLegendData.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					LegendEntry legendEntry = enumerator.Current;
					GameObject freeUnitObject = this.GetFreeUnitObject();
					Image component = freeUnitObject.transform.Find("Icon").GetComponent<Image>();
					component.gameObject.SetActive(legendEntry.displaySprite);
					component.sprite = legendEntry.sprite;
					component.color = legendEntry.colour;
					component.enabled = true;
					component.type = Image.Type.Simple;
					LocText componentInChildren = freeUnitObject.GetComponentInChildren<LocText>();
					componentInChildren.text = legendEntry.name;
					componentInChildren.color = Color.white;
					componentInChildren.enabled = true;
					ToolTip component2 = freeUnitObject.GetComponent<ToolTip>();
					component2.enabled = legendEntry.desc != null || legendEntry.desc_arg != null;
					component2.toolTip = ((legendEntry.desc_arg == null) ? legendEntry.desc : string.Format(legendEntry.desc, legendEntry.desc_arg));
					freeUnitObject.SetActive(true);
					freeUnitObject.transform.SetParent(this.activeUnitsParent.transform);
				}
				goto IL_0165;
			}
		}
		this.activeUnitsParent.SetActive(false);
		IL_0165:
		if (!isRefresh && this.currentMode.legendFilters != null)
		{
			GameObject gameObject = Util.KInstantiateUI(this.toolParameterMenuPrefab, this.diagramsParent.transform.parent.gameObject, false);
			gameObject.transform.SetAsFirstSibling();
			this.filterMenu = gameObject.GetComponent<ToolParameterMenu>();
			this.filterMenu.PopulateMenu(this.currentMode.legendFilters);
			this.filterMenu.onParametersChanged += this.OnFiltersChanged;
			this.OnFiltersChanged();
		}
		this.ConfigureUIHeight();
	}

	// Token: 0x06006C52 RID: 27730 RVA: 0x0028E7BC File Offset: 0x0028C9BC
	private void OnFiltersChanged()
	{
		this.currentMode.OnFiltersChanged();
		this.PopulateGeneratedLegend(this.GetOverlayInfo(this.currentMode), true);
		Game.Instance.ForceOverlayUpdate(false);
	}

	// Token: 0x06006C53 RID: 27731 RVA: 0x0028E7E7 File Offset: 0x0028C9E7
	private void DisableOverlay()
	{
		this.filterMenu.onParametersChanged -= this.OnFiltersChanged;
		this.filterMenu.ClearMenu();
		this.filterMenu.gameObject.SetActive(false);
		this.filterMenu = null;
	}

	// Token: 0x06006C54 RID: 27732 RVA: 0x0028E824 File Offset: 0x0028CA24
	private void ConfigureUIHeight()
	{
		this.scrollRectLayout.enabled = false;
		this.scrollRectLayout.GetComponent<VerticalLayoutGroup>().enabled = true;
		LayoutRebuilder.ForceRebuildLayoutImmediate(base.gameObject.rectTransform());
		this.scrollRectLayout.preferredWidth = this.scrollRectLayout.rectTransform().sizeDelta.x;
		float y = this.scrollRectLayout.rectTransform().sizeDelta.y;
		this.scrollRectLayout.preferredHeight = Mathf.Min(y, 512f);
		this.scrollRectLayout.GetComponent<VerticalLayoutGroup>().enabled = false;
		this.scrollRectLayout.enabled = true;
		LayoutRebuilder.ForceRebuildLayoutImmediate(base.gameObject.rectTransform());
	}

	// Token: 0x040049CE RID: 18894
	public static OverlayLegend Instance;

	// Token: 0x040049CF RID: 18895
	[SerializeField]
	private LocText title;

	// Token: 0x040049D0 RID: 18896
	[SerializeField]
	private Sprite emptySprite;

	// Token: 0x040049D1 RID: 18897
	[SerializeField]
	private List<OverlayLegend.OverlayInfo> overlayInfoList;

	// Token: 0x040049D2 RID: 18898
	[SerializeField]
	private GameObject unitPrefab;

	// Token: 0x040049D3 RID: 18899
	[SerializeField]
	private GameObject activeUnitsParent;

	// Token: 0x040049D4 RID: 18900
	[SerializeField]
	private GameObject diagramsParent;

	// Token: 0x040049D5 RID: 18901
	[SerializeField]
	private GameObject inactiveUnitsParent;

	// Token: 0x040049D6 RID: 18902
	[SerializeField]
	private GameObject toolParameterMenuPrefab;

	// Token: 0x040049D7 RID: 18903
	[SerializeField]
	private LayoutElement scrollRectLayout;

	// Token: 0x040049D8 RID: 18904
	private ToolParameterMenu filterMenu;

	// Token: 0x040049D9 RID: 18905
	private OverlayModes.Mode currentMode;

	// Token: 0x040049DA RID: 18906
	private List<GameObject> inactiveUnitObjs;

	// Token: 0x040049DB RID: 18907
	private List<GameObject> activeUnitObjs;

	// Token: 0x040049DC RID: 18908
	private List<GameObject> activeDiagrams = new List<GameObject>();

	// Token: 0x02001F8B RID: 8075
	[Serializable]
	public class OverlayInfoUnit
	{
		// Token: 0x0600B395 RID: 45973 RVA: 0x003DADE0 File Offset: 0x003D8FE0
		public OverlayInfoUnit(Sprite icon, string description, Color color, Color fontColor, object formatData = null, bool sliceIcon = false)
		{
			this.icon = icon;
			this.description = description;
			this.color = color;
			this.fontColor = fontColor;
			this.formatData = formatData;
			this.sliceIcon = sliceIcon;
		}

		// Token: 0x0400913C RID: 37180
		public Sprite icon;

		// Token: 0x0400913D RID: 37181
		public string description;

		// Token: 0x0400913E RID: 37182
		public string tooltip;

		// Token: 0x0400913F RID: 37183
		public Color color;

		// Token: 0x04009140 RID: 37184
		public Color fontColor;

		// Token: 0x04009141 RID: 37185
		public object formatData;

		// Token: 0x04009142 RID: 37186
		public object tooltipFormatData;

		// Token: 0x04009143 RID: 37187
		public bool sliceIcon;
	}

	// Token: 0x02001F8C RID: 8076
	[Serializable]
	public class OverlayInfo
	{
		// Token: 0x04009144 RID: 37188
		public string name;

		// Token: 0x04009145 RID: 37189
		public HashedString mode;

		// Token: 0x04009146 RID: 37190
		public List<OverlayLegend.OverlayInfoUnit> infoUnits;

		// Token: 0x04009147 RID: 37191
		public List<GameObject> diagrams;

		// Token: 0x04009148 RID: 37192
		public bool isProgrammaticallyPopulated;
	}
}
