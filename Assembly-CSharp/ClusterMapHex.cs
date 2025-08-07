using System;
using System.Collections.Generic;
using System.Linq;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C74 RID: 3188
public class ClusterMapHex : MultiToggle, ICanvasRaycastFilter
{
	// Token: 0x170006F8 RID: 1784
	// (get) Token: 0x06006161 RID: 24929 RVA: 0x002429B4 File Offset: 0x00240BB4
	// (set) Token: 0x06006162 RID: 24930 RVA: 0x002429BC File Offset: 0x00240BBC
	public AxialI location { get; private set; }

	// Token: 0x06006163 RID: 24931 RVA: 0x002429C8 File Offset: 0x00240BC8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.rectTransform = base.GetComponent<RectTransform>();
		this.onClick = new global::System.Action(this.TrySelect);
		this.onDoubleClick = new Func<bool>(this.TryGoTo);
		this.onEnter = new global::System.Action(this.OnHover);
		this.onExit = new global::System.Action(this.OnUnhover);
	}

	// Token: 0x06006164 RID: 24932 RVA: 0x00242A2F File Offset: 0x00240C2F
	public void SetLocation(AxialI location)
	{
		this.location = location;
	}

	// Token: 0x06006165 RID: 24933 RVA: 0x00242A38 File Offset: 0x00240C38
	public void SetRevealed(ClusterRevealLevel level)
	{
		this._revealLevel = level;
		switch (level)
		{
		case ClusterRevealLevel.Hidden:
			this.fogOfWar.gameObject.SetActive(true);
			this.peekedTile.gameObject.SetActive(false);
			return;
		case ClusterRevealLevel.Peeked:
			this.fogOfWar.gameObject.SetActive(false);
			this.peekedTile.gameObject.SetActive(true);
			return;
		case ClusterRevealLevel.Visible:
			this.fogOfWar.gameObject.SetActive(false);
			this.peekedTile.gameObject.SetActive(false);
			return;
		default:
			return;
		}
	}

	// Token: 0x06006166 RID: 24934 RVA: 0x00242AC7 File Offset: 0x00240CC7
	public void SetDestinationStatus(string fail_reason)
	{
		this.m_tooltip.ClearMultiStringTooltip();
		this.UpdateHoverColors(string.IsNullOrEmpty(fail_reason));
		if (!string.IsNullOrEmpty(fail_reason))
		{
			this.m_tooltip.AddMultiStringTooltip(fail_reason, this.invalidDestinationTooltipStyle);
		}
	}

	// Token: 0x06006167 RID: 24935 RVA: 0x00242AFC File Offset: 0x00240CFC
	public void SetDestinationStatus(string fail_reason, int pathLength, int rocketRange, bool repeat)
	{
		this.m_tooltip.ClearMultiStringTooltip();
		if (pathLength > 0)
		{
			string text = (repeat ? UI.CLUSTERMAP.TOOLTIP_PATH_LENGTH_RETURN : UI.CLUSTERMAP.TOOLTIP_PATH_LENGTH);
			if (repeat)
			{
				pathLength *= 2;
			}
			text = string.Format(text, pathLength, GameUtil.GetFormattedRocketRange(rocketRange, true));
			this.m_tooltip.AddMultiStringTooltip(text, this.informationTooltipStyle);
		}
		this.UpdateHoverColors(string.IsNullOrEmpty(fail_reason));
		if (!string.IsNullOrEmpty(fail_reason))
		{
			this.m_tooltip.AddMultiStringTooltip(fail_reason, this.invalidDestinationTooltipStyle);
		}
	}

	// Token: 0x06006168 RID: 24936 RVA: 0x00242B84 File Offset: 0x00240D84
	public void UpdateToggleState(ClusterMapHex.ToggleState state)
	{
		int num = -1;
		switch (state)
		{
		case ClusterMapHex.ToggleState.Unselected:
			num = 0;
			break;
		case ClusterMapHex.ToggleState.Selected:
			num = 1;
			break;
		case ClusterMapHex.ToggleState.OrbitHighlight:
			num = 2;
			break;
		}
		base.ChangeState(num);
	}

	// Token: 0x06006169 RID: 24937 RVA: 0x00242BB8 File Offset: 0x00240DB8
	private void TrySelect()
	{
		if (DebugHandler.InstantBuildMode)
		{
			SaveGame.Instance.GetSMI<ClusterFogOfWarManager.Instance>().RevealLocation(this.location, 0, 2);
		}
		ClusterMapScreen.Instance.SelectHex(this);
	}

	// Token: 0x0600616A RID: 24938 RVA: 0x00242BE4 File Offset: 0x00240DE4
	private bool TryGoTo()
	{
		List<WorldContainer> list = (from entity in ClusterGrid.Instance.GetVisibleEntitiesAtCell(this.location)
			select entity.GetComponent<WorldContainer>() into x
			where x != null
			select x).ToList<WorldContainer>();
		if (list.Count == 1)
		{
			CameraController.Instance.ActiveWorldStarWipe(list[0].id, null);
			return true;
		}
		return false;
	}

	// Token: 0x0600616B RID: 24939 RVA: 0x00242C74 File Offset: 0x00240E74
	private void OnHover()
	{
		this.m_tooltip.ClearMultiStringTooltip();
		string text = "";
		switch (this._revealLevel)
		{
		case ClusterRevealLevel.Hidden:
			text = UI.CLUSTERMAP.TOOLTIP_HIDDEN_HEX;
			break;
		case ClusterRevealLevel.Peeked:
		{
			List<ClusterGridEntity> hiddenEntitiesOfLayerAtCell = ClusterGrid.Instance.GetHiddenEntitiesOfLayerAtCell(this.location, EntityLayer.Asteroid);
			List<ClusterGridEntity> hiddenEntitiesOfLayerAtCell2 = ClusterGrid.Instance.GetHiddenEntitiesOfLayerAtCell(this.location, EntityLayer.POI);
			text = ((hiddenEntitiesOfLayerAtCell.Count > 0 || hiddenEntitiesOfLayerAtCell2.Count > 0) ? UI.CLUSTERMAP.TOOLTIP_PEEKED_HEX_WITH_OBJECT : UI.CLUSTERMAP.TOOLTIP_HIDDEN_HEX);
			break;
		}
		case ClusterRevealLevel.Visible:
			if (ClusterGrid.Instance.GetEntitiesOnCell(this.location).Count == 0)
			{
				text = UI.CLUSTERMAP.TOOLTIP_EMPTY_HEX;
			}
			break;
		}
		if (!text.IsNullOrWhiteSpace())
		{
			this.m_tooltip.AddMultiStringTooltip(text, this.informationTooltipStyle);
		}
		this.UpdateHoverColors(true);
		ClusterMapScreen.Instance.OnHoverHex(this);
	}

	// Token: 0x0600616C RID: 24940 RVA: 0x00242D50 File Offset: 0x00240F50
	private void OnUnhover()
	{
		if (ClusterMapScreen.Instance != null)
		{
			ClusterMapScreen.Instance.OnUnhoverHex(this);
		}
	}

	// Token: 0x0600616D RID: 24941 RVA: 0x00242D6C File Offset: 0x00240F6C
	private void UpdateHoverColors(bool validDestination)
	{
		Color color = (validDestination ? this.hoverColorValid : this.hoverColorInvalid);
		for (int i = 0; i < this.states.Length; i++)
		{
			this.states[i].color_on_hover = color;
			for (int j = 0; j < this.states[i].additional_display_settings.Length; j++)
			{
				this.states[i].additional_display_settings[j].color_on_hover = color;
			}
		}
		base.RefreshHoverColor();
	}

	// Token: 0x0600616E RID: 24942 RVA: 0x00242DF4 File Offset: 0x00240FF4
	public bool IsRaycastLocationValid(Vector2 inputPoint, Camera eventCamera)
	{
		Vector2 vector = this.rectTransform.position;
		float num = Mathf.Abs(inputPoint.x - vector.x);
		float num2 = Mathf.Abs(inputPoint.y - vector.y);
		Vector2 vector2 = this.rectTransform.lossyScale;
		return num <= vector2.x && num2 <= vector2.y && vector2.y * vector2.x - vector2.y / 2f * num - vector2.x * num2 >= 0f;
	}

	// Token: 0x04004200 RID: 16896
	private RectTransform rectTransform;

	// Token: 0x04004201 RID: 16897
	public Color hoverColorValid;

	// Token: 0x04004202 RID: 16898
	public Color hoverColorInvalid;

	// Token: 0x04004203 RID: 16899
	public Image fogOfWar;

	// Token: 0x04004204 RID: 16900
	public Image peekedTile;

	// Token: 0x04004205 RID: 16901
	public TextStyleSetting invalidDestinationTooltipStyle;

	// Token: 0x04004206 RID: 16902
	public TextStyleSetting informationTooltipStyle;

	// Token: 0x04004207 RID: 16903
	[MyCmpGet]
	private ToolTip m_tooltip;

	// Token: 0x04004208 RID: 16904
	private ClusterRevealLevel _revealLevel;

	// Token: 0x02001E37 RID: 7735
	public enum ToggleState
	{
		// Token: 0x04008CEC RID: 36076
		Unselected,
		// Token: 0x04008CED RID: 36077
		Selected,
		// Token: 0x04008CEE RID: 36078
		OrbitHighlight
	}
}
