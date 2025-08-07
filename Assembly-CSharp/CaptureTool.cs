using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200096A RID: 2410
public class CaptureTool : DragTool
{
	// Token: 0x06004565 RID: 17765 RVA: 0x00190190 File Offset: 0x0018E390
	protected override void OnDragComplete(Vector3 downPos, Vector3 upPos)
	{
		Vector2 regularizedPos = base.GetRegularizedPos(Vector2.Min(downPos, upPos), true);
		Vector2 regularizedPos2 = base.GetRegularizedPos(Vector2.Max(downPos, upPos), false);
		CaptureTool.MarkForCapture(regularizedPos, regularizedPos2, true);
	}

	// Token: 0x06004566 RID: 17766 RVA: 0x001901D8 File Offset: 0x0018E3D8
	public static void MarkForCapture(Vector2 min, Vector2 max, bool mark)
	{
		foreach (Capturable capturable in Components.Capturables.Items)
		{
			Vector2 vector = Grid.PosToXY(capturable.transform.GetPosition());
			if (vector.x >= min.x && vector.x < max.x && vector.y >= min.y && vector.y < max.y)
			{
				if (capturable.allowCapture)
				{
					PrioritySetting lastSelectedPriority = ToolMenu.Instance.PriorityScreen.GetLastSelectedPriority();
					capturable.MarkForCapture(mark, lastSelectedPriority, true);
				}
				else if (mark)
				{
					PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, UI.TOOLS.CAPTURE.NOT_CAPTURABLE, null, capturable.transform.GetPosition(), 1.5f, false, false);
				}
			}
		}
	}

	// Token: 0x06004567 RID: 17767 RVA: 0x001902D8 File Offset: 0x0018E4D8
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		ToolMenu.Instance.PriorityScreen.Show(true);
	}

	// Token: 0x06004568 RID: 17768 RVA: 0x001902F0 File Offset: 0x0018E4F0
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		ToolMenu.Instance.PriorityScreen.Show(false);
	}
}
