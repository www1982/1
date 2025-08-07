using System;
using System.Collections.Generic;
using ImGuiNET;
using UnityEngine;

// Token: 0x0200068C RID: 1676
public class DevTool_StoryTraits_Reveal : DevTool
{
	// Token: 0x0600290F RID: 10511 RVA: 0x000EE85C File Offset: 0x000ECA5C
	protected override void RenderTo(DevPanel panel)
	{
		int num;
		bool flag = DevToolUtil.TryGetCellIndexForUniqueBuilding("Headquarters", out num);
		if (ImGuiEx.Button("Focus on headquaters", flag))
		{
			DevToolUtil.FocusCameraOnCell(num);
		}
		if (!flag)
		{
			ImGuiEx.TooltipForPrevious("Couldn't find headquaters");
		}
		if (ImGui.CollapsingHeader("Search world for entity", ImGuiTreeNodeFlags.DefaultOpen))
		{
			IReadOnlyList<WorldGenSpawner.Spawnable> allSpawnables = this.GetAllSpawnables();
			if (allSpawnables == null)
			{
				ImGui.Text("Couldn't find a list of spawnables");
				return;
			}
			foreach (string text in this.GetPrefabIDsToSearchFor())
			{
				int num2;
				bool cellIndexForSpawnable = this.GetCellIndexForSpawnable(text, allSpawnables, out num2);
				string text2 = "\"" + text + "\"";
				bool flag2 = cellIndexForSpawnable;
				if (ImGuiEx.Button("Reveal and focus on " + text2, flag2))
				{
					DevToolUtil.RevealAndFocusAt(num2);
				}
				if (!flag2)
				{
					ImGuiEx.TooltipForPrevious("Couldn't find a cell that contained a spawnable with component " + text2);
				}
			}
		}
	}

	// Token: 0x06002910 RID: 10512 RVA: 0x000EE948 File Offset: 0x000ECB48
	public IEnumerable<string> GetPrefabIDsToSearchFor()
	{
		yield return "MegaBrainTank";
		yield return "GravitasCreatureManipulator";
		yield return "LonelyMinionHouse";
		yield return "FossilDig";
		yield break;
	}

	// Token: 0x06002911 RID: 10513 RVA: 0x000EE954 File Offset: 0x000ECB54
	private bool GetCellIndexForSpawnable(string prefabId, IReadOnlyList<WorldGenSpawner.Spawnable> spawnablesToSearch, out int cellIndex)
	{
		foreach (WorldGenSpawner.Spawnable spawnable in spawnablesToSearch)
		{
			if (prefabId == spawnable.spawnInfo.id)
			{
				cellIndex = spawnable.cell;
				return true;
			}
		}
		cellIndex = -1;
		return false;
	}

	// Token: 0x06002912 RID: 10514 RVA: 0x000EE9BC File Offset: 0x000ECBBC
	private IReadOnlyList<WorldGenSpawner.Spawnable> GetAllSpawnables()
	{
		WorldGenSpawner worldGenSpawner = global::UnityEngine.Object.FindObjectOfType<WorldGenSpawner>(true);
		if (worldGenSpawner == null)
		{
			return null;
		}
		IReadOnlyList<WorldGenSpawner.Spawnable> spawnables = worldGenSpawner.GetSpawnables();
		if (spawnables == null)
		{
			return null;
		}
		return spawnables;
	}
}
