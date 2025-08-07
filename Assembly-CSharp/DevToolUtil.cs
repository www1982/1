using System;
using UnityEngine;

// Token: 0x02000688 RID: 1672
public static class DevToolUtil
{
	// Token: 0x060028F4 RID: 10484 RVA: 0x000EE284 File Offset: 0x000EC484
	public static DevPanel Open(DevTool devTool)
	{
		return DevToolManager.Instance.panels.AddPanelFor(devTool);
	}

	// Token: 0x060028F5 RID: 10485 RVA: 0x000EE296 File Offset: 0x000EC496
	public static DevPanel Open<T>() where T : DevTool, new()
	{
		return DevToolManager.Instance.panels.AddPanelFor<T>();
	}

	// Token: 0x060028F6 RID: 10486 RVA: 0x000EE2A7 File Offset: 0x000EC4A7
	public static DevPanel DebugObject<T>(T obj)
	{
		return DevToolUtil.Open(new DevToolObjectViewer<T>(() => obj));
	}

	// Token: 0x060028F7 RID: 10487 RVA: 0x000EE2CA File Offset: 0x000EC4CA
	public static DevPanel DebugObject<T>(Func<T> get_obj_fn)
	{
		return DevToolUtil.Open(new DevToolObjectViewer<T>(get_obj_fn));
	}

	// Token: 0x060028F8 RID: 10488 RVA: 0x000EE2D7 File Offset: 0x000EC4D7
	public static void Close(DevTool devTool)
	{
		devTool.ClosePanel();
	}

	// Token: 0x060028F9 RID: 10489 RVA: 0x000EE2DF File Offset: 0x000EC4DF
	public static void Close(DevPanel devPanel)
	{
		devPanel.Close();
	}

	// Token: 0x060028FA RID: 10490 RVA: 0x000EE2E7 File Offset: 0x000EC4E7
	public static string GenerateDevToolName(DevTool devTool)
	{
		return DevToolUtil.GenerateDevToolName(devTool.GetType());
	}

	// Token: 0x060028FB RID: 10491 RVA: 0x000EE2F4 File Offset: 0x000EC4F4
	public static string GenerateDevToolName(Type devToolType)
	{
		string text;
		if (DevToolManager.Instance != null && DevToolManager.Instance.devToolNameDict.TryGetValue(devToolType, out text))
		{
			return text;
		}
		string text2 = devToolType.Name;
		if (text2.StartsWith("DevTool_"))
		{
			text2 = text2.Substring("DevTool_".Length);
		}
		else if (text2.StartsWith("DevTool"))
		{
			text2 = text2.Substring("DevTool".Length);
		}
		return text2;
	}

	// Token: 0x060028FC RID: 10492 RVA: 0x000EE364 File Offset: 0x000EC564
	public static bool CanRevealAndFocus(GameObject gameObject)
	{
		int num;
		return DevToolUtil.TryGetCellIndexFor(gameObject, out num);
	}

	// Token: 0x060028FD RID: 10493 RVA: 0x000EE37C File Offset: 0x000EC57C
	public static void RevealAndFocus(GameObject gameObject)
	{
		int num;
		if (DevToolUtil.TryGetCellIndexFor(gameObject, out num))
		{
			return;
		}
		DevToolUtil.RevealAndFocusAt(num);
		if (!gameObject.GetComponent<KSelectable>().IsNullOrDestroyed())
		{
			SelectTool.Instance.Select(gameObject.GetComponent<KSelectable>(), false);
			return;
		}
		SelectTool.Instance.Select(null, false);
	}

	// Token: 0x060028FE RID: 10494 RVA: 0x000EE3C8 File Offset: 0x000EC5C8
	public static void FocusCameraOnCell(int cellIndex)
	{
		Vector3 vector = Grid.CellToPos2D(cellIndex);
		CameraController.Instance.SetPosition(vector);
	}

	// Token: 0x060028FF RID: 10495 RVA: 0x000EE3E7 File Offset: 0x000EC5E7
	public static bool TryGetCellIndexFor(GameObject gameObject, out int cellIndex)
	{
		cellIndex = -1;
		if (gameObject.IsNullOrDestroyed())
		{
			return false;
		}
		if (!gameObject.GetComponent<RectTransform>().IsNullOrDestroyed())
		{
			return false;
		}
		cellIndex = Grid.PosToCell(gameObject);
		return true;
	}

	// Token: 0x06002900 RID: 10496 RVA: 0x000EE410 File Offset: 0x000EC610
	public static bool TryGetCellIndexForUniqueBuilding(string prefabId, out int index)
	{
		index = -1;
		BuildingComplete[] array = global::UnityEngine.Object.FindObjectsOfType<BuildingComplete>(true);
		if (array == null)
		{
			return false;
		}
		foreach (BuildingComplete buildingComplete in array)
		{
			if (prefabId == buildingComplete.Def.PrefabID)
			{
				index = buildingComplete.GetCell();
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002901 RID: 10497 RVA: 0x000EE460 File Offset: 0x000EC660
	public static void RevealAndFocusAt(int cellIndex)
	{
		int num;
		int num2;
		Grid.CellToXY(cellIndex, out num, out num2);
		GridVisibility.Reveal(num + 2, num2 + 2, 10, 10f);
		DevToolUtil.FocusCameraOnCell(cellIndex);
		int num3;
		if (DevToolUtil.TryGetCellIndexForUniqueBuilding("Headquarters", out num3))
		{
			Vector3 vector = Grid.CellToPos2D(cellIndex);
			Vector3 vector2 = Grid.CellToPos2D(num3);
			float num4 = 2f / Vector3.Distance(vector, vector2);
			for (float num5 = 0f; num5 < 1f; num5 += num4)
			{
				int num6;
				int num7;
				Grid.PosToXY(Vector3.Lerp(vector, vector2, num5), out num6, out num7);
				GridVisibility.Reveal(num6 + 2, num7 + 2, 4, 4f);
			}
		}
	}

	// Token: 0x02001505 RID: 5381
	public enum TextAlignment
	{
		// Token: 0x04006E8E RID: 28302
		Center,
		// Token: 0x04006E8F RID: 28303
		Left,
		// Token: 0x04006E90 RID: 28304
		Right
	}
}
