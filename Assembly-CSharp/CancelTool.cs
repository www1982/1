using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000969 RID: 2409
public class CancelTool : FilteredDragTool
{
	// Token: 0x0600455D RID: 17757 RVA: 0x001900A3 File Offset: 0x0018E2A3
	public static void DestroyInstance()
	{
		CancelTool.Instance = null;
	}

	// Token: 0x0600455E RID: 17758 RVA: 0x001900AB File Offset: 0x0018E2AB
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		CancelTool.Instance = this;
	}

	// Token: 0x0600455F RID: 17759 RVA: 0x001900B9 File Offset: 0x0018E2B9
	protected override void GetDefaultFilters(Dictionary<string, ToolParameterMenu.ToggleState> filters)
	{
		base.GetDefaultFilters(filters);
		filters.Add(ToolParameterMenu.FILTERLAYERS.CLEANANDCLEAR, ToolParameterMenu.ToggleState.Off);
		filters.Add(ToolParameterMenu.FILTERLAYERS.DIGPLACER, ToolParameterMenu.ToggleState.Off);
	}

	// Token: 0x06004560 RID: 17760 RVA: 0x001900DA File Offset: 0x0018E2DA
	protected override string GetConfirmSound()
	{
		return "Tile_Confirm_NegativeTool";
	}

	// Token: 0x06004561 RID: 17761 RVA: 0x001900E1 File Offset: 0x0018E2E1
	protected override string GetDragSound()
	{
		return "Tile_Drag_NegativeTool";
	}

	// Token: 0x06004562 RID: 17762 RVA: 0x001900E8 File Offset: 0x0018E2E8
	protected override void OnDragTool(int cell, int distFromOrigin)
	{
		for (int i = 0; i < 45; i++)
		{
			GameObject gameObject = Grid.Objects[cell, i];
			if (gameObject != null)
			{
				string filterLayerFromGameObject = this.GetFilterLayerFromGameObject(gameObject);
				if (base.IsActiveLayer(filterLayerFromGameObject))
				{
					gameObject.Trigger(2127324410, null);
				}
			}
		}
	}

	// Token: 0x06004563 RID: 17763 RVA: 0x00190138 File Offset: 0x0018E338
	protected override void OnDragComplete(Vector3 downPos, Vector3 upPos)
	{
		Vector2 regularizedPos = base.GetRegularizedPos(Vector2.Min(downPos, upPos), true);
		Vector2 regularizedPos2 = base.GetRegularizedPos(Vector2.Max(downPos, upPos), false);
		AttackTool.MarkForAttack(regularizedPos, regularizedPos2, false);
		CaptureTool.MarkForCapture(regularizedPos, regularizedPos2, false);
	}

	// Token: 0x04002E57 RID: 11863
	public static CancelTool Instance;
}
