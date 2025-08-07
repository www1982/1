using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000C89 RID: 3209
public class CodexTextWithTooltip : CodexWidget<CodexTextWithTooltip>
{
	// Token: 0x1700072B RID: 1835
	// (get) Token: 0x060062BB RID: 25275 RVA: 0x00251C2E File Offset: 0x0024FE2E
	// (set) Token: 0x060062BC RID: 25276 RVA: 0x00251C36 File Offset: 0x0024FE36
	public string text { get; set; }

	// Token: 0x1700072C RID: 1836
	// (get) Token: 0x060062BD RID: 25277 RVA: 0x00251C3F File Offset: 0x0024FE3F
	// (set) Token: 0x060062BE RID: 25278 RVA: 0x00251C47 File Offset: 0x0024FE47
	public string tooltip { get; set; }

	// Token: 0x1700072D RID: 1837
	// (get) Token: 0x060062BF RID: 25279 RVA: 0x00251C50 File Offset: 0x0024FE50
	// (set) Token: 0x060062C0 RID: 25280 RVA: 0x00251C58 File Offset: 0x0024FE58
	public CodexTextStyle style { get; set; }

	// Token: 0x1700072E RID: 1838
	// (get) Token: 0x060062C2 RID: 25282 RVA: 0x00251C74 File Offset: 0x0024FE74
	// (set) Token: 0x060062C1 RID: 25281 RVA: 0x00251C61 File Offset: 0x0024FE61
	public string stringKey
	{
		get
		{
			return "--> " + (this.text ?? "NULL");
		}
		set
		{
			this.text = Strings.Get(value);
		}
	}

	// Token: 0x060062C3 RID: 25283 RVA: 0x00251C8F File Offset: 0x0024FE8F
	public CodexTextWithTooltip()
	{
		this.style = CodexTextStyle.Body;
	}

	// Token: 0x060062C4 RID: 25284 RVA: 0x00251C9E File Offset: 0x0024FE9E
	public CodexTextWithTooltip(string text, string tooltip, CodexTextStyle style = CodexTextStyle.Body)
	{
		this.text = text;
		this.style = style;
		this.tooltip = tooltip;
	}

	// Token: 0x060062C5 RID: 25285 RVA: 0x00251CBC File Offset: 0x0024FEBC
	public void ConfigureLabel(LocText label, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		label.gameObject.SetActive(true);
		label.AllowLinks = this.style == CodexTextStyle.Body;
		label.textStyleSetting = textStyles[this.style];
		label.text = this.text;
		label.ApplySettings();
	}

	// Token: 0x060062C6 RID: 25286 RVA: 0x00251D08 File Offset: 0x0024FF08
	public void ConfigureTooltip(ToolTip tooltip)
	{
		tooltip.SetSimpleTooltip(this.tooltip);
	}

	// Token: 0x060062C7 RID: 25287 RVA: 0x00251D16 File Offset: 0x0024FF16
	public override void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		this.ConfigureLabel(contentGameObject.GetComponent<LocText>(), textStyles);
		this.ConfigureTooltip(contentGameObject.GetComponent<ToolTip>());
		base.ConfigurePreferredLayout(contentGameObject);
	}
}
