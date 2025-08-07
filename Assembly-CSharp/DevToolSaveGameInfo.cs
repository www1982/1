using System;
using ImGuiNET;

// Token: 0x0200067C RID: 1660
public class DevToolSaveGameInfo : DevTool
{
	// Token: 0x060028A6 RID: 10406 RVA: 0x000E9C5C File Offset: 0x000E7E5C
	protected override void RenderTo(DevPanel panel)
	{
		if (Game.Instance == null)
		{
			ImGui.Text("No game loaded");
			return;
		}
		ImGui.Text("Seed: " + CustomGameSettings.Instance.GetSettingsCoordinate());
		ImGui.Text("Generated: " + Game.Instance.dateGenerated);
		ImGui.Text("DebugWasUsed: " + Game.Instance.debugWasUsed.ToString());
		ImGui.Text("Content Enabled: ");
		foreach (string text in SaveLoader.Instance.GameInfo.dlcIds)
		{
			string text2 = ((text == "") ? "VANILLA_ID" : text);
			ImGui.Text(" - " + text2);
		}
		ImGui.PushItemWidth(100f);
		ImGui.NewLine();
		ImGui.Text("Changelists played on");
		ImGui.InputText("Search", ref this.clSearch, 10U);
		ImGui.PopItemWidth();
		foreach (uint num in Game.Instance.changelistsPlayedOn)
		{
			if (this.clSearch.IsNullOrWhiteSpace() || num.ToString().Contains(this.clSearch))
			{
				ImGui.Text(num.ToString());
			}
		}
		ImGui.NewLine();
	}

	// Token: 0x040017EE RID: 6126
	private string clSearch = "";
}
