using System;
using ImGuiNET;

// Token: 0x0200066E RID: 1646
public class DevToolMenuFontSize
{
	// Token: 0x170001EF RID: 495
	// (get) Token: 0x06002857 RID: 10327 RVA: 0x000E60DD File Offset: 0x000E42DD
	// (set) Token: 0x06002856 RID: 10326 RVA: 0x000E60D4 File Offset: 0x000E42D4
	public bool initialized { get; private set; }

	// Token: 0x06002858 RID: 10328 RVA: 0x000E60E8 File Offset: 0x000E42E8
	public void RefreshFontSize()
	{
		DevToolMenuFontSize.FontSizeCategory @int = (DevToolMenuFontSize.FontSizeCategory)KPlayerPrefs.GetInt("Imgui_font_size_category", 2);
		this.SetFontSizeCategory(@int);
	}

	// Token: 0x06002859 RID: 10329 RVA: 0x000E6108 File Offset: 0x000E4308
	public void InitializeIfNeeded()
	{
		if (!this.initialized)
		{
			this.initialized = true;
			this.RefreshFontSize();
		}
	}

	// Token: 0x0600285A RID: 10330 RVA: 0x000E6120 File Offset: 0x000E4320
	public void DrawMenu()
	{
		if (ImGui.BeginMenu("Settings"))
		{
			bool flag = this.fontSizeCategory == DevToolMenuFontSize.FontSizeCategory.Fabric;
			bool flag2 = this.fontSizeCategory == DevToolMenuFontSize.FontSizeCategory.Small;
			bool flag3 = this.fontSizeCategory == DevToolMenuFontSize.FontSizeCategory.Regular;
			bool flag4 = this.fontSizeCategory == DevToolMenuFontSize.FontSizeCategory.Large;
			if (ImGui.BeginMenu("Size"))
			{
				if (ImGui.Checkbox("Original Font", ref flag) && this.fontSizeCategory != DevToolMenuFontSize.FontSizeCategory.Fabric)
				{
					this.SetFontSizeCategory(DevToolMenuFontSize.FontSizeCategory.Fabric);
				}
				if (ImGui.Checkbox("Small Text", ref flag2) && this.fontSizeCategory != DevToolMenuFontSize.FontSizeCategory.Small)
				{
					this.SetFontSizeCategory(DevToolMenuFontSize.FontSizeCategory.Small);
				}
				if (ImGui.Checkbox("Regular Text", ref flag3) && this.fontSizeCategory != DevToolMenuFontSize.FontSizeCategory.Regular)
				{
					this.SetFontSizeCategory(DevToolMenuFontSize.FontSizeCategory.Regular);
				}
				if (ImGui.Checkbox("Large Text", ref flag4) && this.fontSizeCategory != DevToolMenuFontSize.FontSizeCategory.Large)
				{
					this.SetFontSizeCategory(DevToolMenuFontSize.FontSizeCategory.Large);
				}
				ImGui.EndMenu();
			}
			ImGui.EndMenu();
		}
	}

	// Token: 0x0600285B RID: 10331 RVA: 0x000E61F4 File Offset: 0x000E43F4
	public unsafe void SetFontSizeCategory(DevToolMenuFontSize.FontSizeCategory size)
	{
		this.fontSizeCategory = size;
		KPlayerPrefs.SetInt("Imgui_font_size_category", (int)size);
		ImGuiIOPtr io = ImGui.GetIO();
		if (size < (DevToolMenuFontSize.FontSizeCategory)io.Fonts.Fonts.Size)
		{
			ImFontPtr imFontPtr = *io.Fonts.Fonts[(int)size];
			io.NativePtr->FontDefault = imFontPtr;
		}
	}

	// Token: 0x040017A4 RID: 6052
	public const string SETTINGS_KEY_FONT_SIZE_CATEGORY = "Imgui_font_size_category";

	// Token: 0x040017A5 RID: 6053
	private DevToolMenuFontSize.FontSizeCategory fontSizeCategory;

	// Token: 0x020014F2 RID: 5362
	public enum FontSizeCategory
	{
		// Token: 0x04006E57 RID: 28247
		Fabric,
		// Token: 0x04006E58 RID: 28248
		Small,
		// Token: 0x04006E59 RID: 28249
		Regular,
		// Token: 0x04006E5A RID: 28250
		Large
	}
}
