using System;
using System.Collections.Generic;
using Klei;
using ProcGen;

// Token: 0x0200089A RID: 2202
public static class TemplateCache
{
	// Token: 0x17000439 RID: 1081
	// (get) Token: 0x06003CEA RID: 15594 RVA: 0x00152F3B File Offset: 0x0015113B
	// (set) Token: 0x06003CEB RID: 15595 RVA: 0x00152F42 File Offset: 0x00151142
	public static bool Initted { get; private set; }

	// Token: 0x06003CEC RID: 15596 RVA: 0x00152F4A File Offset: 0x0015114A
	public static void Init()
	{
		if (TemplateCache.Initted)
		{
			return;
		}
		TemplateCache.templates = new Dictionary<string, TemplateContainer>();
		TemplateCache.Initted = true;
	}

	// Token: 0x06003CED RID: 15597 RVA: 0x00152F64 File Offset: 0x00151164
	public static void Clear()
	{
		TemplateCache.templates = null;
		TemplateCache.Initted = false;
	}

	// Token: 0x06003CEE RID: 15598 RVA: 0x00152F74 File Offset: 0x00151174
	public static string RewriteTemplatePath(string scopePath)
	{
		string text;
		string text2;
		SettingsCache.GetDlcIdAndPath(scopePath, out text, out text2);
		return SettingsCache.GetAbsoluteContentPath(text, "templates/" + text2);
	}

	// Token: 0x06003CEF RID: 15599 RVA: 0x00152F9C File Offset: 0x0015119C
	public static string RewriteTemplateYaml(string scopePath)
	{
		return TemplateCache.RewriteTemplatePath(scopePath) + ".yaml";
	}

	// Token: 0x06003CF0 RID: 15600 RVA: 0x00152FB0 File Offset: 0x001511B0
	public static TemplateContainer GetTemplate(string templatePath)
	{
		if (!TemplateCache.templates.ContainsKey(templatePath))
		{
			TemplateCache.templates.Add(templatePath, null);
		}
		if (TemplateCache.templates[templatePath] == null)
		{
			string text = TemplateCache.RewriteTemplateYaml(templatePath);
			TemplateContainer templateContainer = YamlIO.LoadFile<TemplateContainer>(text, null, null);
			if (templateContainer == null)
			{
				Debug.LogWarning("Missing template [" + text + "]");
			}
			templateContainer.name = templatePath;
			TemplateCache.templates[templatePath] = templateContainer;
		}
		return TemplateCache.templates[templatePath];
	}

	// Token: 0x06003CF1 RID: 15601 RVA: 0x00153029 File Offset: 0x00151229
	public static bool TemplateExists(string templatePath)
	{
		return FileSystem.FileExists(TemplateCache.RewriteTemplateYaml(templatePath));
	}

	// Token: 0x0400255A RID: 9562
	private const string defaultAssetFolder = "bases";

	// Token: 0x0400255B RID: 9563
	private static Dictionary<string, TemplateContainer> templates;
}
