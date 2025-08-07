using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D96 RID: 3478
public class PlanBuildingToggle : KToggle
{
	// Token: 0x06006C9F RID: 27807 RVA: 0x00290988 File Offset: 0x0028EB88
	public void Config(BuildingDef def, PlanScreen planScreen, HashedString buildingCategory, bool? passesSearchFilter)
	{
		this.def = def;
		this.planScreen = planScreen;
		this.buildingCategory = buildingCategory;
		this.techItem = Db.Get().TechItems.TryGet(def.PrefabID);
		this.gameSubscriptions.Add(Game.Instance.Subscribe(-107300940, new Action<object>(this.CheckResearch)));
		this.gameSubscriptions.Add(Game.Instance.Subscribe(-1948169901, new Action<object>(this.CheckResearch)));
		this.gameSubscriptions.Add(Game.Instance.Subscribe(1557339983, new Action<object>(this.CheckResearch)));
		this.sprite = def.GetUISprite("ui", false);
		base.onClick += delegate
		{
			PlanScreen.Instance.OnSelectBuilding(this.gameObject, def, null);
			this.RefreshDisplay();
		};
		if (BUILDINGS.PLANSUBCATEGORYSORTING.ContainsKey(def.PrefabID))
		{
			Strings.TryGet("STRINGS.UI.NEWBUILDCATEGORIES." + BUILDINGS.PLANSUBCATEGORYSORTING[def.PrefabID].ToUpper() + ".NAME", out this.subcategoryName);
		}
		else
		{
			global::Debug.LogWarning("Building " + def.PrefabID + " has not been added to plan screen subcategory organization in BuildingTuning.cs");
		}
		this.CheckResearch(null);
		this.Refresh(passesSearchFilter);
	}

	// Token: 0x06006CA0 RID: 27808 RVA: 0x00290AFC File Offset: 0x0028ECFC
	protected override void OnDestroy()
	{
		if (Game.Instance != null)
		{
			foreach (int num in this.gameSubscriptions)
			{
				Game.Instance.Unsubscribe(num);
			}
		}
		this.gameSubscriptions.Clear();
		base.OnDestroy();
	}

	// Token: 0x06006CA1 RID: 27809 RVA: 0x00290B74 File Offset: 0x0028ED74
	private void CheckResearch(object data = null)
	{
		this.researchComplete = PlanScreen.TechRequirementsMet(this.techItem);
	}

	// Token: 0x06006CA2 RID: 27810 RVA: 0x00290B88 File Offset: 0x0028ED88
	private bool StandardDisplayFilter()
	{
		return (this.researchComplete || DebugHandler.InstantBuildMode || Game.Instance.SandboxModeActive) && (this.planScreen.ActiveCategoryToggleInfo == null || this.buildingCategory == (HashedString)this.planScreen.ActiveCategoryToggleInfo.userData);
	}

	// Token: 0x06006CA3 RID: 27811 RVA: 0x00290BE4 File Offset: 0x0028EDE4
	public bool Refresh(bool? passesSearchFilter)
	{
		bool flag = passesSearchFilter ?? this.StandardDisplayFilter();
		bool flag2 = base.gameObject.activeSelf != flag;
		if (flag2)
		{
			base.gameObject.SetActive(flag);
		}
		if (base.gameObject.activeSelf)
		{
			this.PositionTooltip();
			this.RefreshLabel();
			this.RefreshDisplay();
		}
		return flag2;
	}

	// Token: 0x06006CA4 RID: 27812 RVA: 0x00290C4C File Offset: 0x0028EE4C
	public void SwitchViewMode(bool listView)
	{
		this.text.gameObject.SetActive(!listView);
		this.text_listView.gameObject.SetActive(listView);
		this.buildingIcon.gameObject.SetActive(!listView);
		this.buildingIcon_listView.gameObject.SetActive(listView);
	}

	// Token: 0x06006CA5 RID: 27813 RVA: 0x00290CA4 File Offset: 0x0028EEA4
	private void RefreshLabel()
	{
		if (this.text != null)
		{
			this.text.fontSize = (float)(ScreenResolutionMonitor.UsingGamepadUIMode() ? PlanScreen.fontSizeBigMode : PlanScreen.fontSizeStandardMode);
			this.text_listView.fontSize = (float)(ScreenResolutionMonitor.UsingGamepadUIMode() ? PlanScreen.fontSizeBigMode : PlanScreen.fontSizeStandardMode);
			this.text.text = this.def.Name;
			this.text_listView.text = this.def.Name;
		}
	}

	// Token: 0x06006CA6 RID: 27814 RVA: 0x00290D2C File Offset: 0x0028EF2C
	private void RefreshDisplay()
	{
		PlanScreen.RequirementsState buildableState = PlanScreen.Instance.GetBuildableState(this.def);
		bool flag = buildableState == PlanScreen.RequirementsState.Complete || DebugHandler.InstantBuildMode || Game.Instance.SandboxModeActive;
		bool flag2 = base.gameObject == PlanScreen.Instance.SelectedBuildingGameObject;
		if (flag2 && flag)
		{
			this.toggle.ChangeState(1);
		}
		else if (!flag2 && flag)
		{
			this.toggle.ChangeState(0);
		}
		else if (flag2 && !flag)
		{
			this.toggle.ChangeState(3);
		}
		else if (!flag2 && !flag)
		{
			this.toggle.ChangeState(2);
		}
		this.RefreshBuildingButtonIconAndColors(flag);
		this.RefreshFG(buildableState);
	}

	// Token: 0x06006CA7 RID: 27815 RVA: 0x00290DDC File Offset: 0x0028EFDC
	private void PositionTooltip()
	{
		this.tooltip.overrideParentObject = (PlanScreen.Instance.ProductInfoScreen.gameObject.activeSelf ? PlanScreen.Instance.ProductInfoScreen.rectTransform() : PlanScreen.Instance.buildingGroupsRoot);
		this.tooltip.tooltipPivot = Vector2.zero;
		this.tooltip.parentPositionAnchor = new Vector2(1f, 0f);
		this.tooltip.tooltipPositionOffset = new Vector2(4f, 0f);
		this.tooltip.ClearMultiStringTooltip();
		string name = this.def.Name;
		string effect = this.def.Effect;
		this.tooltip.AddMultiStringTooltip(name, PlanScreen.Instance.buildingToolTipSettings.BuildButtonName);
		this.tooltip.AddMultiStringTooltip(effect, PlanScreen.Instance.buildingToolTipSettings.BuildButtonDescription);
	}

	// Token: 0x06006CA8 RID: 27816 RVA: 0x00290EC4 File Offset: 0x0028F0C4
	private void RefreshBuildingButtonIconAndColors(bool buttonAvailable)
	{
		if (this.sprite == null)
		{
			this.sprite = PlanScreen.Instance.defaultBuildingIconSprite;
		}
		this.buildingIcon.sprite = this.sprite;
		this.buildingIcon.SetNativeSize();
		this.buildingIcon_listView.sprite = this.sprite;
		float num = (ScreenResolutionMonitor.UsingGamepadUIMode() ? 3.25f : 4f);
		this.buildingIcon.rectTransform().sizeDelta /= num;
		Material material = (buttonAvailable ? PlanScreen.Instance.defaultUIMaterial : PlanScreen.Instance.desaturatedUIMaterial);
		if (this.buildingIcon.material != material)
		{
			this.buildingIcon.material = material;
			this.buildingIcon_listView.material = material;
		}
	}

	// Token: 0x06006CA9 RID: 27817 RVA: 0x00290F94 File Offset: 0x0028F194
	private void RefreshFG(PlanScreen.RequirementsState requirementsState)
	{
		if (requirementsState == PlanScreen.RequirementsState.Tech)
		{
			this.fgImage.sprite = PlanScreen.Instance.Overlay_NeedTech;
			this.fgImage.gameObject.SetActive(true);
		}
		else
		{
			this.fgImage.gameObject.SetActive(false);
		}
		string tooltipForRequirementsState = PlanScreen.GetTooltipForRequirementsState(this.def, requirementsState);
		if (tooltipForRequirementsState != null)
		{
			this.tooltip.AddMultiStringTooltip("\n", PlanScreen.Instance.buildingToolTipSettings.ResearchRequirement);
			this.tooltip.AddMultiStringTooltip(tooltipForRequirementsState, PlanScreen.Instance.buildingToolTipSettings.ResearchRequirement);
		}
	}

	// Token: 0x04004A05 RID: 18949
	private BuildingDef def;

	// Token: 0x04004A06 RID: 18950
	private HashedString buildingCategory;

	// Token: 0x04004A07 RID: 18951
	private TechItem techItem;

	// Token: 0x04004A08 RID: 18952
	private List<int> gameSubscriptions = new List<int>();

	// Token: 0x04004A09 RID: 18953
	private bool researchComplete;

	// Token: 0x04004A0A RID: 18954
	private Sprite sprite;

	// Token: 0x04004A0B RID: 18955
	[SerializeField]
	private MultiToggle toggle;

	// Token: 0x04004A0C RID: 18956
	[SerializeField]
	private ToolTip tooltip;

	// Token: 0x04004A0D RID: 18957
	[SerializeField]
	private LocText text;

	// Token: 0x04004A0E RID: 18958
	[SerializeField]
	private LocText text_listView;

	// Token: 0x04004A0F RID: 18959
	[SerializeField]
	private Image buildingIcon;

	// Token: 0x04004A10 RID: 18960
	[SerializeField]
	private Image buildingIcon_listView;

	// Token: 0x04004A11 RID: 18961
	[SerializeField]
	private Image fgIcon;

	// Token: 0x04004A12 RID: 18962
	[SerializeField]
	private PlanScreen planScreen;

	// Token: 0x04004A13 RID: 18963
	private StringEntry subcategoryName;
}
