using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000C97 RID: 3223
public class CodexCollapsibleHeader : CodexWidget<CodexCollapsibleHeader>
{
	// Token: 0x1700073A RID: 1850
	// (get) Token: 0x06006315 RID: 25365 RVA: 0x00253C13 File Offset: 0x00251E13
	// (set) Token: 0x06006316 RID: 25366 RVA: 0x00253C3A File Offset: 0x00251E3A
	protected GameObject ContentsGameObject
	{
		get
		{
			if (this.contentsGameObject == null)
			{
				this.contentsGameObject = this.contents.go;
			}
			return this.contentsGameObject;
		}
		set
		{
			this.contentsGameObject = value;
		}
	}

	// Token: 0x06006317 RID: 25367 RVA: 0x00253C43 File Offset: 0x00251E43
	public CodexCollapsibleHeader(string label, ContentContainer contents)
	{
		this.label = label;
		this.contents = contents;
	}

	// Token: 0x06006318 RID: 25368 RVA: 0x00253C5C File Offset: 0x00251E5C
	public override void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		HierarchyReferences component = contentGameObject.GetComponent<HierarchyReferences>();
		LocText reference = component.GetReference<LocText>("Label");
		reference.text = this.label;
		reference.textStyleSetting = textStyles[CodexTextStyle.Subtitle];
		reference.ApplySettings();
		MultiToggle reference2 = component.GetReference<MultiToggle>("ExpandToggle");
		reference2.ChangeState(1);
		reference2.onClick = delegate
		{
			this.ToggleCategoryOpen(contentGameObject, !this.ContentsGameObject.activeSelf);
		};
	}

	// Token: 0x06006319 RID: 25369 RVA: 0x00253CD3 File Offset: 0x00251ED3
	private void ToggleCategoryOpen(GameObject header, bool open)
	{
		header.GetComponent<HierarchyReferences>().GetReference<MultiToggle>("ExpandToggle").ChangeState(open ? 1 : 0);
		this.ContentsGameObject.SetActive(open);
	}

	// Token: 0x04004305 RID: 17157
	protected ContentContainer contents;

	// Token: 0x04004306 RID: 17158
	private string label;

	// Token: 0x04004307 RID: 17159
	private GameObject contentsGameObject;
}
