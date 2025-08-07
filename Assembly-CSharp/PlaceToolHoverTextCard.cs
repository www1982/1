using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000C30 RID: 3120
public class PlaceToolHoverTextCard : HoverTextConfiguration
{
	// Token: 0x06005ED2 RID: 24274 RVA: 0x0022C3F8 File Offset: 0x0022A5F8
	public override void UpdateHoverElements(List<KSelectable> hoverObjects)
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
		this.ActionName = UI.TOOLS.PLACE.TOOLACTION;
		if (this.currentPlaceable != null && this.currentPlaceable.GetProperName() != null)
		{
			this.ToolName = string.Format(UI.TOOLS.PLACE.NAME, this.currentPlaceable.GetProperName());
		}
		base.DrawTitle(instance, hoverTextDrawer);
		base.DrawInstructions(instance, hoverTextDrawer);
		int num2 = 26;
		int num3 = 8;
		string text;
		if (this.currentPlaceable != null && !this.currentPlaceable.IsValidPlaceLocation(num, out text))
		{
			hoverTextDrawer.NewLine(num2);
			hoverTextDrawer.AddIndent(num3);
			hoverTextDrawer.DrawText(text, this.HoverTextStyleSettings[1]);
		}
		hoverTextDrawer.EndShadowBar();
		hoverTextDrawer.EndDrawing();
	}

	// Token: 0x04003F41 RID: 16193
	public Placeable currentPlaceable;
}
