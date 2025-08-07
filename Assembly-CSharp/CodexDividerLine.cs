using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C8B RID: 3211
public class CodexDividerLine : CodexWidget<CodexDividerLine>
{
	// Token: 0x060062D9 RID: 25305 RVA: 0x00251EFB File Offset: 0x002500FB
	public override void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		contentGameObject.GetComponent<LayoutElement>().minWidth = 530f;
	}
}
