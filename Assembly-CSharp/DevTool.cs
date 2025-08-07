using System;
using ImGuiNET;

// Token: 0x0200065E RID: 1630
public abstract class DevTool
{
	// Token: 0x1400000E RID: 14
	// (add) Token: 0x06002801 RID: 10241 RVA: 0x000E2958 File Offset: 0x000E0B58
	// (remove) Token: 0x06002802 RID: 10242 RVA: 0x000E2990 File Offset: 0x000E0B90
	public event global::System.Action OnInit;

	// Token: 0x1400000F RID: 15
	// (add) Token: 0x06002803 RID: 10243 RVA: 0x000E29C8 File Offset: 0x000E0BC8
	// (remove) Token: 0x06002804 RID: 10244 RVA: 0x000E2A00 File Offset: 0x000E0C00
	public event global::System.Action OnUpdate;

	// Token: 0x14000010 RID: 16
	// (add) Token: 0x06002805 RID: 10245 RVA: 0x000E2A38 File Offset: 0x000E0C38
	// (remove) Token: 0x06002806 RID: 10246 RVA: 0x000E2A70 File Offset: 0x000E0C70
	public event global::System.Action OnUninit;

	// Token: 0x06002807 RID: 10247 RVA: 0x000E2AA5 File Offset: 0x000E0CA5
	public DevTool()
	{
		this.Name = DevToolUtil.GenerateDevToolName(this);
	}

	// Token: 0x06002808 RID: 10248 RVA: 0x000E2AB9 File Offset: 0x000E0CB9
	public void DoImGui(DevPanel panel)
	{
		if (this.RequiresGameRunning && Game.Instance == null)
		{
			ImGui.Text("Game must be loaded to use this devtool.");
			return;
		}
		this.RenderTo(panel);
	}

	// Token: 0x06002809 RID: 10249 RVA: 0x000E2AE2 File Offset: 0x000E0CE2
	public void ClosePanel()
	{
		this.isRequestingToClosePanel = true;
	}

	// Token: 0x0600280A RID: 10250
	protected abstract void RenderTo(DevPanel panel);

	// Token: 0x0600280B RID: 10251 RVA: 0x000E2AEB File Offset: 0x000E0CEB
	public void Internal_TryInit()
	{
		if (this.didInit)
		{
			return;
		}
		this.didInit = true;
		if (this.OnInit != null)
		{
			this.OnInit();
		}
	}

	// Token: 0x0600280C RID: 10252 RVA: 0x000E2B10 File Offset: 0x000E0D10
	public void Internal_Update()
	{
		if (this.OnUpdate != null)
		{
			this.OnUpdate();
		}
	}

	// Token: 0x0600280D RID: 10253 RVA: 0x000E2B25 File Offset: 0x000E0D25
	public void Internal_Uninit()
	{
		if (this.OnUninit != null)
		{
			this.OnUninit();
		}
	}

	// Token: 0x0400177B RID: 6011
	public string Name;

	// Token: 0x0400177C RID: 6012
	public bool RequiresGameRunning;

	// Token: 0x0400177D RID: 6013
	public bool isRequestingToClosePanel;

	// Token: 0x0400177E RID: 6014
	public ImGuiWindowFlags drawFlags;

	// Token: 0x04001782 RID: 6018
	private bool didInit;
}
