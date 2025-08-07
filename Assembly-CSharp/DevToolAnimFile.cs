using System;

// Token: 0x02000660 RID: 1632
public class DevToolAnimFile : DevTool
{
	// Token: 0x06002812 RID: 10258 RVA: 0x000E2E60 File Offset: 0x000E1060
	public DevToolAnimFile(KAnimFile animFile)
	{
		this.animFile = animFile;
		this.Name = "Anim File: \"" + animFile.name + "\"";
	}

	// Token: 0x06002813 RID: 10259 RVA: 0x000E2E8C File Offset: 0x000E108C
	protected override void RenderTo(DevPanel panel)
	{
		ImGuiEx.DrawObject(this.animFile, null);
		ImGuiEx.DrawObject(this.animFile.GetData(), null);
	}

	// Token: 0x04001783 RID: 6019
	private KAnimFile animFile;
}
