using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000C1B RID: 3099
[AddComponentMenu("KMonoBehaviour/scripts/HoverTextConfiguration")]
public class HoverTextConfiguration : KMonoBehaviour
{
	// Token: 0x06005DD3 RID: 24019 RVA: 0x00224E19 File Offset: 0x00223019
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.ConfigureHoverScreen();
	}

	// Token: 0x06005DD4 RID: 24020 RVA: 0x00224E27 File Offset: 0x00223027
	protected virtual void ConfigureTitle(HoverTextScreen screen)
	{
		if (string.IsNullOrEmpty(this.ToolName))
		{
			this.ToolName = Strings.Get(this.ToolNameStringKey).String.ToUpper();
		}
	}

	// Token: 0x06005DD5 RID: 24021 RVA: 0x00224E51 File Offset: 0x00223051
	protected void DrawTitle(HoverTextScreen screen, HoverTextDrawer drawer)
	{
		drawer.DrawText(this.ToolName, this.ToolTitleTextStyle);
	}

	// Token: 0x06005DD6 RID: 24022 RVA: 0x00224E68 File Offset: 0x00223068
	protected void DrawInstructions(HoverTextScreen screen, HoverTextDrawer drawer)
	{
		TextStyleSetting standard = this.Styles_Instruction.Standard;
		drawer.NewLine(26);
		if (KInputManager.currentControllerIsGamepad)
		{
			drawer.DrawIcon(KInputManager.steamInputInterpreter.GetActionSprite(global::Action.MouseLeft, false), 20);
		}
		else
		{
			drawer.DrawIcon(screen.GetSprite("icon_mouse_left"), 20);
		}
		drawer.DrawText(this.ActionName, standard);
		drawer.AddIndent(8);
		if (KInputManager.currentControllerIsGamepad)
		{
			drawer.DrawIcon(KInputManager.steamInputInterpreter.GetActionSprite(global::Action.MouseRight, false), 20);
		}
		else
		{
			drawer.DrawIcon(screen.GetSprite("icon_mouse_right"), 20);
		}
		drawer.DrawText(this.backStr, standard);
	}

	// Token: 0x06005DD7 RID: 24023 RVA: 0x00224F0C File Offset: 0x0022310C
	public virtual void ConfigureHoverScreen()
	{
		if (!string.IsNullOrEmpty(this.ActionStringKey))
		{
			this.ActionName = Strings.Get(this.ActionStringKey);
		}
		HoverTextScreen instance = HoverTextScreen.Instance;
		this.ConfigureTitle(instance);
		this.backStr = UI.TOOLS.GENERIC.BACK.ToString().ToUpper();
	}

	// Token: 0x06005DD8 RID: 24024 RVA: 0x00224F60 File Offset: 0x00223160
	public virtual void UpdateHoverElements(List<KSelectable> hover_objects)
	{
		HoverTextScreen instance = HoverTextScreen.Instance;
		HoverTextDrawer hoverTextDrawer = instance.BeginDrawing();
		int num = Grid.PosToCell(Camera.main.ScreenToWorldPoint(KInputManager.GetMousePos()));
		if (!Grid.IsValidCell(num) || (int)Grid.WorldIdx[num] != ClusterManager.Instance.activeWorldId)
		{
			hoverTextDrawer.EndDrawing();
			return;
		}
		hoverTextDrawer.BeginShadowBar(false);
		this.DrawTitle(instance, hoverTextDrawer);
		this.DrawInstructions(HoverTextScreen.Instance, hoverTextDrawer);
		hoverTextDrawer.EndShadowBar();
		hoverTextDrawer.EndDrawing();
	}

	// Token: 0x04003E71 RID: 15985
	public TextStyleSetting[] HoverTextStyleSettings;

	// Token: 0x04003E72 RID: 15986
	public string ToolNameStringKey = "";

	// Token: 0x04003E73 RID: 15987
	public string ActionStringKey = "";

	// Token: 0x04003E74 RID: 15988
	[HideInInspector]
	public string ActionName = "";

	// Token: 0x04003E75 RID: 15989
	[HideInInspector]
	public string ToolName;

	// Token: 0x04003E76 RID: 15990
	protected string backStr;

	// Token: 0x04003E77 RID: 15991
	public TextStyleSetting ToolTitleTextStyle;

	// Token: 0x04003E78 RID: 15992
	public HoverTextConfiguration.TextStylePair Styles_Title;

	// Token: 0x04003E79 RID: 15993
	public HoverTextConfiguration.TextStylePair Styles_BodyText;

	// Token: 0x04003E7A RID: 15994
	public HoverTextConfiguration.TextStylePair Styles_Instruction;

	// Token: 0x04003E7B RID: 15995
	public HoverTextConfiguration.TextStylePair Styles_Warning;

	// Token: 0x04003E7C RID: 15996
	public HoverTextConfiguration.ValuePropertyTextStyles Styles_Values;

	// Token: 0x02001D5D RID: 7517
	[Serializable]
	public struct TextStylePair
	{
		// Token: 0x04008904 RID: 35076
		public TextStyleSetting Standard;

		// Token: 0x04008905 RID: 35077
		public TextStyleSetting Selected;
	}

	// Token: 0x02001D5E RID: 7518
	[Serializable]
	public struct ValuePropertyTextStyles
	{
		// Token: 0x04008906 RID: 35078
		public HoverTextConfiguration.TextStylePair Property;

		// Token: 0x04008907 RID: 35079
		public HoverTextConfiguration.TextStylePair Property_Decimal;

		// Token: 0x04008908 RID: 35080
		public HoverTextConfiguration.TextStylePair Property_Unit;
	}
}
