using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C86 RID: 3206
public abstract class CodexWidget<SubClass> : ICodexWidget
{
	// Token: 0x1700071F RID: 1823
	// (get) Token: 0x06006297 RID: 25239 RVA: 0x00251A11 File Offset: 0x0024FC11
	// (set) Token: 0x06006298 RID: 25240 RVA: 0x00251A19 File Offset: 0x0024FC19
	public int preferredWidth { get; set; }

	// Token: 0x17000720 RID: 1824
	// (get) Token: 0x06006299 RID: 25241 RVA: 0x00251A22 File Offset: 0x0024FC22
	// (set) Token: 0x0600629A RID: 25242 RVA: 0x00251A2A File Offset: 0x0024FC2A
	public int preferredHeight { get; set; }

	// Token: 0x0600629B RID: 25243 RVA: 0x00251A33 File Offset: 0x0024FC33
	protected CodexWidget()
	{
		this.preferredWidth = -1;
		this.preferredHeight = -1;
	}

	// Token: 0x0600629C RID: 25244 RVA: 0x00251A49 File Offset: 0x0024FC49
	protected CodexWidget(int preferredWidth, int preferredHeight)
	{
		this.preferredWidth = preferredWidth;
		this.preferredHeight = preferredHeight;
	}

	// Token: 0x0600629D RID: 25245
	public abstract void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles);

	// Token: 0x0600629E RID: 25246 RVA: 0x00251A5F File Offset: 0x0024FC5F
	protected void ConfigurePreferredLayout(GameObject contentGameObject)
	{
		LayoutElement componentInChildren = contentGameObject.GetComponentInChildren<LayoutElement>();
		componentInChildren.minWidth = (float)this.preferredWidth;
		componentInChildren.minHeight = (float)this.preferredHeight;
		componentInChildren.preferredHeight = (float)this.preferredHeight;
		componentInChildren.preferredWidth = (float)this.preferredWidth;
	}
}
