using System;
using System.Collections.Generic;
using ImGuiNET;

// Token: 0x02000680 RID: 1664
public class DevToolSpaceScannerNetwork : DevTool
{
	// Token: 0x060028BB RID: 10427 RVA: 0x000EC75C File Offset: 0x000EA95C
	public DevToolSpaceScannerNetwork()
	{
		this.tableDrawer = ImGuiObjectTableDrawer<DevToolSpaceScannerNetwork.Entry>.New().Column("WorldId", (DevToolSpaceScannerNetwork.Entry e) => e.worldId).Column("Network Quality (0->1)", (DevToolSpaceScannerNetwork.Entry e) => e.networkQuality)
			.Column("Targets Detected", (DevToolSpaceScannerNetwork.Entry e) => e.targetsString)
			.FixedHeight(300f)
			.Build();
	}

	// Token: 0x060028BC RID: 10428 RVA: 0x000EC804 File Offset: 0x000EAA04
	protected override void RenderTo(DevPanel panel)
	{
		if (Game.Instance == null)
		{
			ImGui.Text("Game instance is null");
			return;
		}
		if (Game.Instance.spaceScannerNetworkManager == null)
		{
			ImGui.Text("SpaceScannerNetworkQualityManager instance is null");
			return;
		}
		if (ClusterManager.Instance == null)
		{
			ImGui.Text("ClusterManager instance is null");
			return;
		}
		if (ImGui.CollapsingHeader("Worlds Data"))
		{
			this.tableDrawer.Draw(DevToolSpaceScannerNetwork.GetData());
		}
		if (ImGui.CollapsingHeader("Full DevToolSpaceScannerNetwork Info"))
		{
			ImGuiEx.DrawObject(Game.Instance.spaceScannerNetworkManager, null);
		}
	}

	// Token: 0x060028BD RID: 10429 RVA: 0x000EC898 File Offset: 0x000EAA98
	public static IEnumerable<DevToolSpaceScannerNetwork.Entry> GetData()
	{
		foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
		{
			yield return new DevToolSpaceScannerNetwork.Entry(worldContainer.id, Game.Instance.spaceScannerNetworkManager.GetQualityForWorld(worldContainer.id), DevToolSpaceScannerNetwork.GetTargetsString(worldContainer));
		}
		List<WorldContainer>.Enumerator enumerator = default(List<WorldContainer>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x060028BE RID: 10430 RVA: 0x000EC8A4 File Offset: 0x000EAAA4
	public static string GetTargetsString(WorldContainer world)
	{
		SpaceScannerWorldData spaceScannerWorldData;
		if (!Game.Instance.spaceScannerNetworkManager.DEBUG_GetWorldIdToDataMap().TryGetValue(world.id, out spaceScannerWorldData))
		{
			return "<none>";
		}
		if (spaceScannerWorldData.targetIdsDetected.Count == 0)
		{
			return "<none>";
		}
		return string.Join(",", spaceScannerWorldData.targetIdsDetected);
	}

	// Token: 0x04001814 RID: 6164
	private ImGuiObjectTableDrawer<DevToolSpaceScannerNetwork.Entry> tableDrawer;

	// Token: 0x020014FB RID: 5371
	public readonly struct Entry
	{
		// Token: 0x06008F82 RID: 36738 RVA: 0x0035DB3B File Offset: 0x0035BD3B
		public Entry(int worldId, float networkQuality, string targetsString)
		{
			this.worldId = worldId;
			this.networkQuality = networkQuality;
			this.targetsString = targetsString;
		}

		// Token: 0x04006E6D RID: 28269
		public readonly int worldId;

		// Token: 0x04006E6E RID: 28270
		public readonly float networkQuality;

		// Token: 0x04006E6F RID: 28271
		public readonly string targetsString;
	}
}
