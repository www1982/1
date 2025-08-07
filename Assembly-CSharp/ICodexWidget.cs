using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000C85 RID: 3205
public interface ICodexWidget
{
	// Token: 0x06006296 RID: 25238
	void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles);
}
