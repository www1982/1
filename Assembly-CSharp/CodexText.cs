using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000C88 RID: 3208
public class CodexText : CodexWidget<CodexText>
{
	// Token: 0x17000727 RID: 1831
	// (get) Token: 0x060062AF RID: 25263 RVA: 0x00251B39 File Offset: 0x0024FD39
	// (set) Token: 0x060062B0 RID: 25264 RVA: 0x00251B41 File Offset: 0x0024FD41
	public string text { get; set; }

	// Token: 0x17000728 RID: 1832
	// (get) Token: 0x060062B1 RID: 25265 RVA: 0x00251B4A File Offset: 0x0024FD4A
	// (set) Token: 0x060062B2 RID: 25266 RVA: 0x00251B52 File Offset: 0x0024FD52
	public string messageID { get; set; }

	// Token: 0x17000729 RID: 1833
	// (get) Token: 0x060062B3 RID: 25267 RVA: 0x00251B5B File Offset: 0x0024FD5B
	// (set) Token: 0x060062B4 RID: 25268 RVA: 0x00251B63 File Offset: 0x0024FD63
	public CodexTextStyle style { get; set; }

	// Token: 0x1700072A RID: 1834
	// (get) Token: 0x060062B6 RID: 25270 RVA: 0x00251B7F File Offset: 0x0024FD7F
	// (set) Token: 0x060062B5 RID: 25269 RVA: 0x00251B6C File Offset: 0x0024FD6C
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

	// Token: 0x060062B7 RID: 25271 RVA: 0x00251B9A File Offset: 0x0024FD9A
	public CodexText()
	{
		this.style = CodexTextStyle.Body;
	}

	// Token: 0x060062B8 RID: 25272 RVA: 0x00251BA9 File Offset: 0x0024FDA9
	public CodexText(string text, CodexTextStyle style = CodexTextStyle.Body, string id = null)
	{
		this.text = text;
		this.style = style;
		if (id != null)
		{
			this.messageID = id;
		}
	}

	// Token: 0x060062B9 RID: 25273 RVA: 0x00251BCC File Offset: 0x0024FDCC
	public void ConfigureLabel(LocText label, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		label.gameObject.SetActive(true);
		label.AllowLinks = this.style == CodexTextStyle.Body;
		label.textStyleSetting = textStyles[this.style];
		label.text = this.text;
		label.ApplySettings();
	}

	// Token: 0x060062BA RID: 25274 RVA: 0x00251C18 File Offset: 0x0024FE18
	public override void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		this.ConfigureLabel(contentGameObject.GetComponent<LocText>(), textStyles);
		base.ConfigurePreferredLayout(contentGameObject);
	}
}
