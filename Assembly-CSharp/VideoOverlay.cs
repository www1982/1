using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000E79 RID: 3705
[AddComponentMenu("KMonoBehaviour/scripts/VideoOverlay")]
public class VideoOverlay : KMonoBehaviour
{
	// Token: 0x06007621 RID: 30241 RVA: 0x002D33E0 File Offset: 0x002D15E0
	public void SetText(List<string> strings)
	{
		if (strings.Count != this.textFields.Count)
		{
			DebugUtil.LogErrorArgs(new object[]
			{
				base.name,
				"expects",
				this.textFields.Count,
				"strings passed to it, got",
				strings.Count
			});
		}
		for (int i = 0; i < this.textFields.Count; i++)
		{
			this.textFields[i].text = strings[i];
		}
	}

	// Token: 0x040051EE RID: 20974
	public List<LocText> textFields;
}
