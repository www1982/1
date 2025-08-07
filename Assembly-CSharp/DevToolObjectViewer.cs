using System;

// Token: 0x02000677 RID: 1655
public class DevToolObjectViewer<T> : DevTool
{
	// Token: 0x0600289A RID: 10394 RVA: 0x000E940B File Offset: 0x000E760B
	public DevToolObjectViewer(Func<T> getValue)
	{
		this.getValue = getValue;
		this.Name = typeof(T).Name;
	}

	// Token: 0x0600289B RID: 10395 RVA: 0x000E9430 File Offset: 0x000E7630
	protected override void RenderTo(DevPanel panel)
	{
		T t = this.getValue();
		this.Name = t.GetType().Name;
		ImGuiEx.DrawObject(t, null);
	}

	// Token: 0x040017EC RID: 6124
	private Func<T> getValue;
}
