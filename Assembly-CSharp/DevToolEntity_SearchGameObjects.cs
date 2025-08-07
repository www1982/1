using System;
using ImGuiNET;

// Token: 0x0200066D RID: 1645
public class DevToolEntity_SearchGameObjects : DevTool
{
	// Token: 0x06002854 RID: 10324 RVA: 0x000E60B9 File Offset: 0x000E42B9
	public DevToolEntity_SearchGameObjects(Action<DevToolEntityTarget> onSelectionMadeFn)
	{
		this.onSelectionMadeFn = onSelectionMadeFn;
	}

	// Token: 0x06002855 RID: 10325 RVA: 0x000E60C8 File Offset: 0x000E42C8
	protected override void RenderTo(DevPanel panel)
	{
		ImGui.Text("Not implemented yet");
	}

	// Token: 0x040017A3 RID: 6051
	private Action<DevToolEntityTarget> onSelectionMadeFn;
}
