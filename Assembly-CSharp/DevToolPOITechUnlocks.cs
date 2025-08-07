using System;
using ImGuiNET;

// Token: 0x02000678 RID: 1656
public class DevToolPOITechUnlocks : DevTool
{
	// Token: 0x0600289D RID: 10397 RVA: 0x000E9480 File Offset: 0x000E7680
	protected override void RenderTo(DevPanel panel)
	{
		if (Research.Instance == null)
		{
			return;
		}
		foreach (TechItem techItem in Db.Get().TechItems.resources)
		{
			if (techItem.isPOIUnlock)
			{
				ImGui.Text(techItem.Id);
				ImGui.SameLine();
				bool flag = techItem.IsComplete();
				if (ImGui.Checkbox("Unlocked ", ref flag))
				{
					techItem.POIUnlocked();
				}
			}
		}
	}
}
