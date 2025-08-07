using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C91 RID: 3217
public class CodexLabelWithLargeIcon : CodexLabelWithIcon
{
	// Token: 0x17000738 RID: 1848
	// (get) Token: 0x060062F1 RID: 25329 RVA: 0x0025215A File Offset: 0x0025035A
	// (set) Token: 0x060062F2 RID: 25330 RVA: 0x00252162 File Offset: 0x00250362
	public string linkID { get; set; }

	// Token: 0x060062F3 RID: 25331 RVA: 0x0025216B File Offset: 0x0025036B
	public CodexLabelWithLargeIcon()
	{
	}

	// Token: 0x060062F4 RID: 25332 RVA: 0x00252174 File Offset: 0x00250374
	public CodexLabelWithLargeIcon(string text, CodexTextStyle style, global::Tuple<Sprite, Color> coloredSprite, string targetEntrylinkID)
		: base(text, style, coloredSprite, 128, 128)
	{
		base.icon = new CodexImage(128, 128, coloredSprite);
		base.label = new CodexText(text, style, null);
		this.linkID = targetEntrylinkID;
	}

	// Token: 0x060062F5 RID: 25333 RVA: 0x002521C0 File Offset: 0x002503C0
	public override void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		base.icon.ConfigureImage(contentGameObject.GetComponentsInChildren<Image>()[1]);
		if (base.icon.preferredWidth != -1 && base.icon.preferredHeight != -1)
		{
			LayoutElement component = contentGameObject.GetComponentsInChildren<Image>()[1].GetComponent<LayoutElement>();
			component.minWidth = (float)base.icon.preferredHeight;
			component.minHeight = (float)base.icon.preferredWidth;
			component.preferredHeight = (float)base.icon.preferredHeight;
			component.preferredWidth = (float)base.icon.preferredWidth;
		}
		base.label.text = UI.StripLinkFormatting(base.label.text);
		base.label.ConfigureLabel(contentGameObject.GetComponentInChildren<LocText>(), textStyles);
		contentGameObject.GetComponent<KButton>().ClearOnClick();
		contentGameObject.GetComponent<KButton>().onClick += delegate
		{
			ManagementMenu.Instance.codexScreen.ChangeArticle(this.linkID, false, default(Vector3), CodexScreen.HistoryDirection.NewArticle);
		};
	}
}
