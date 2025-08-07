using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000C8E RID: 3214
public class CodexLargeSpacer : CodexWidget<CodexLargeSpacer>
{
	// Token: 0x060062E5 RID: 25317 RVA: 0x0025202F File Offset: 0x0025022F
	public override void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		base.ConfigurePreferredLayout(contentGameObject);
	}
}
