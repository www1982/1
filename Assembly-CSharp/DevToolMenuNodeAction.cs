using System;

// Token: 0x02000675 RID: 1653
public class DevToolMenuNodeAction : IMenuNode
{
	// Token: 0x0600288D RID: 10381 RVA: 0x000E8AEC File Offset: 0x000E6CEC
	public DevToolMenuNodeAction(string name, global::System.Action onClickFn)
	{
		this.name = name;
		this.onClickFn = onClickFn;
	}

	// Token: 0x0600288E RID: 10382 RVA: 0x000E8B02 File Offset: 0x000E6D02
	public string GetName()
	{
		return this.name;
	}

	// Token: 0x0600288F RID: 10383 RVA: 0x000E8B0A File Offset: 0x000E6D0A
	public void Draw()
	{
		if (ImGuiEx.MenuItem(this.name, this.isEnabledFn == null || this.isEnabledFn()))
		{
			this.onClickFn();
		}
	}

	// Token: 0x040017DE RID: 6110
	public string name;

	// Token: 0x040017DF RID: 6111
	public global::System.Action onClickFn;

	// Token: 0x040017E0 RID: 6112
	public Func<bool> isEnabledFn;
}
