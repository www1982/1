using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C99 RID: 3225
public class CodexCritterLifecycleWidget : CodexWidget<CodexCritterLifecycleWidget>
{
	// Token: 0x06006325 RID: 25381 RVA: 0x00253D94 File Offset: 0x00251F94
	private CodexCritterLifecycleWidget()
	{
	}

	// Token: 0x06006326 RID: 25382 RVA: 0x00253D9C File Offset: 0x00251F9C
	public override void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		HierarchyReferences component = contentGameObject.GetComponent<HierarchyReferences>();
		component.GetReference<Image>("EggIcon").sprite = null;
		component.GetReference<Image>("EggIcon").color = Color.white;
		component.GetReference<LocText>("EggToBabyLabel").text = "";
		component.GetReference<Image>("BabyIcon").sprite = null;
		component.GetReference<Image>("BabyIcon").color = Color.white;
		component.GetReference<LocText>("BabyToAdultLabel").text = "";
		component.GetReference<Image>("AdultIcon").sprite = null;
		component.GetReference<Image>("AdultIcon").color = Color.white;
	}
}
