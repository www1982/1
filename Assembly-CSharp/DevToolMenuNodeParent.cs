using System;
using System.Collections.Generic;
using ImGuiNET;

// Token: 0x02000674 RID: 1652
public class DevToolMenuNodeParent : IMenuNode
{
	// Token: 0x06002889 RID: 10377 RVA: 0x000E8A58 File Offset: 0x000E6C58
	public DevToolMenuNodeParent(string name)
	{
		this.name = name;
		this.children = new List<IMenuNode>();
	}

	// Token: 0x0600288A RID: 10378 RVA: 0x000E8A72 File Offset: 0x000E6C72
	public void AddChild(IMenuNode menuNode)
	{
		this.children.Add(menuNode);
	}

	// Token: 0x0600288B RID: 10379 RVA: 0x000E8A80 File Offset: 0x000E6C80
	public string GetName()
	{
		return this.name;
	}

	// Token: 0x0600288C RID: 10380 RVA: 0x000E8A88 File Offset: 0x000E6C88
	public void Draw()
	{
		if (ImGui.BeginMenu(this.name))
		{
			foreach (IMenuNode menuNode in this.children)
			{
				menuNode.Draw();
			}
			ImGui.EndMenu();
		}
	}

	// Token: 0x040017DC RID: 6108
	public string name;

	// Token: 0x040017DD RID: 6109
	public List<IMenuNode> children;
}
