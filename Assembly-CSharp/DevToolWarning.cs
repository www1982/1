using System;
using ImGuiNET;
using STRINGS;
using UnityEngine;

// Token: 0x0200068A RID: 1674
public class DevToolWarning
{
	// Token: 0x06002908 RID: 10504 RVA: 0x000EE693 File Offset: 0x000EC893
	public DevToolWarning()
	{
		this.Name = UI.FRONTEND.DEVTOOLS.TITLE;
	}

	// Token: 0x06002909 RID: 10505 RVA: 0x000EE6AB File Offset: 0x000EC8AB
	public void DrawMenuBar()
	{
		if (ImGui.BeginMainMenuBar())
		{
			ImGui.Checkbox(this.Name, ref this.ShouldDrawWindow);
			ImGui.EndMainMenuBar();
		}
	}

	// Token: 0x0600290A RID: 10506 RVA: 0x000EE6CC File Offset: 0x000EC8CC
	public void DrawWindow(out bool isOpen)
	{
		ImGuiWindowFlags imGuiWindowFlags = ImGuiWindowFlags.None;
		isOpen = true;
		if (ImGui.Begin(this.Name + "###ID_DevToolWarning", ref isOpen, imGuiWindowFlags))
		{
			if (!isOpen)
			{
				ImGui.End();
				return;
			}
			ImGui.SetWindowSize(new Vector2(500f, 250f));
			ImGui.TextWrapped(UI.FRONTEND.DEVTOOLS.WARNING);
			ImGui.Spacing();
			ImGui.Spacing();
			ImGui.Spacing();
			ImGui.Spacing();
			ImGui.Checkbox(UI.FRONTEND.DEVTOOLS.DONTSHOW, ref this.showAgain);
			if (ImGui.Button(UI.FRONTEND.DEVTOOLS.BUTTON))
			{
				if (this.showAgain)
				{
					KPlayerPrefs.SetInt("ShowDevtools", 1);
				}
				DevToolManager.Instance.UserAcceptedWarning = true;
				isOpen = false;
			}
			ImGui.End();
		}
	}

	// Token: 0x0400182E RID: 6190
	private bool showAgain;

	// Token: 0x0400182F RID: 6191
	public string Name;

	// Token: 0x04001830 RID: 6192
	public bool ShouldDrawWindow;
}
