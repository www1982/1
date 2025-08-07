using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C90 RID: 3216
public class CodexLabelWithIcon : CodexWidget<CodexLabelWithIcon>
{
	// Token: 0x17000736 RID: 1846
	// (get) Token: 0x060062E9 RID: 25321 RVA: 0x0025204A File Offset: 0x0025024A
	// (set) Token: 0x060062EA RID: 25322 RVA: 0x00252052 File Offset: 0x00250252
	public CodexImage icon { get; set; }

	// Token: 0x17000737 RID: 1847
	// (get) Token: 0x060062EB RID: 25323 RVA: 0x0025205B File Offset: 0x0025025B
	// (set) Token: 0x060062EC RID: 25324 RVA: 0x00252063 File Offset: 0x00250263
	public CodexText label { get; set; }

	// Token: 0x060062ED RID: 25325 RVA: 0x0025206C File Offset: 0x0025026C
	public CodexLabelWithIcon()
	{
	}

	// Token: 0x060062EE RID: 25326 RVA: 0x00252074 File Offset: 0x00250274
	public CodexLabelWithIcon(string text, CodexTextStyle style, global::Tuple<Sprite, Color> coloredSprite)
	{
		this.icon = new CodexImage(coloredSprite);
		this.label = new CodexText(text, style, null);
	}

	// Token: 0x060062EF RID: 25327 RVA: 0x00252096 File Offset: 0x00250296
	public CodexLabelWithIcon(string text, CodexTextStyle style, global::Tuple<Sprite, Color> coloredSprite, int iconWidth, int iconHeight)
	{
		this.icon = new CodexImage(iconWidth, iconHeight, coloredSprite);
		this.label = new CodexText(text, style, null);
	}

	// Token: 0x060062F0 RID: 25328 RVA: 0x002520BC File Offset: 0x002502BC
	public override void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		this.icon.ConfigureImage(contentGameObject.GetComponentInChildren<Image>());
		if (this.icon.preferredWidth != -1 && this.icon.preferredHeight != -1)
		{
			LayoutElement component = contentGameObject.GetComponentInChildren<Image>().GetComponent<LayoutElement>();
			component.minWidth = (float)this.icon.preferredHeight;
			component.minHeight = (float)this.icon.preferredWidth;
			component.preferredHeight = (float)this.icon.preferredHeight;
			component.preferredWidth = (float)this.icon.preferredWidth;
		}
		this.label.ConfigureLabel(contentGameObject.GetComponentInChildren<LocText>(), textStyles);
	}
}
