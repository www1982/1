using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C8D RID: 3213
public class CodexIndentedLabelWithIcon : CodexWidget<CodexIndentedLabelWithIcon>
{
	// Token: 0x17000734 RID: 1844
	// (get) Token: 0x060062DD RID: 25309 RVA: 0x00251F1F File Offset: 0x0025011F
	// (set) Token: 0x060062DE RID: 25310 RVA: 0x00251F27 File Offset: 0x00250127
	public CodexImage icon { get; set; }

	// Token: 0x17000735 RID: 1845
	// (get) Token: 0x060062DF RID: 25311 RVA: 0x00251F30 File Offset: 0x00250130
	// (set) Token: 0x060062E0 RID: 25312 RVA: 0x00251F38 File Offset: 0x00250138
	public CodexText label { get; set; }

	// Token: 0x060062E1 RID: 25313 RVA: 0x00251F41 File Offset: 0x00250141
	public CodexIndentedLabelWithIcon()
	{
	}

	// Token: 0x060062E2 RID: 25314 RVA: 0x00251F49 File Offset: 0x00250149
	public CodexIndentedLabelWithIcon(string text, CodexTextStyle style, global::Tuple<Sprite, Color> coloredSprite)
	{
		this.icon = new CodexImage(coloredSprite);
		this.label = new CodexText(text, style, null);
	}

	// Token: 0x060062E3 RID: 25315 RVA: 0x00251F6B File Offset: 0x0025016B
	public CodexIndentedLabelWithIcon(string text, CodexTextStyle style, global::Tuple<Sprite, Color> coloredSprite, int iconWidth, int iconHeight)
	{
		this.icon = new CodexImage(iconWidth, iconHeight, coloredSprite);
		this.label = new CodexText(text, style, null);
	}

	// Token: 0x060062E4 RID: 25316 RVA: 0x00251F94 File Offset: 0x00250194
	public override void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		Image componentInChildren = contentGameObject.GetComponentInChildren<Image>();
		this.icon.ConfigureImage(componentInChildren);
		this.label.ConfigureLabel(contentGameObject.GetComponentInChildren<LocText>(), textStyles);
		if (this.icon.preferredWidth != -1 && this.icon.preferredHeight != -1)
		{
			LayoutElement component = componentInChildren.GetComponent<LayoutElement>();
			component.minWidth = (float)this.icon.preferredHeight;
			component.minHeight = (float)this.icon.preferredWidth;
			component.preferredHeight = (float)this.icon.preferredHeight;
			component.preferredWidth = (float)this.icon.preferredWidth;
		}
	}
}
