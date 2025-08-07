using System;
using UnityEngine;

// Token: 0x02000CED RID: 3309
[AddComponentMenu("KMonoBehaviour/scripts/InfoScreenLineItem")]
public class InfoScreenLineItem : KMonoBehaviour
{
	// Token: 0x060065D5 RID: 26069 RVA: 0x00265049 File Offset: 0x00263249
	public void SetText(string text)
	{
		this.locText.text = text;
	}

	// Token: 0x060065D6 RID: 26070 RVA: 0x00265057 File Offset: 0x00263257
	public void SetTooltip(string tooltip)
	{
		this.toolTip.toolTip = tooltip;
	}

	// Token: 0x040045BF RID: 17855
	[SerializeField]
	private LocText locText;

	// Token: 0x040045C0 RID: 17856
	[SerializeField]
	private ToolTip toolTip;

	// Token: 0x040045C1 RID: 17857
	private string text;

	// Token: 0x040045C2 RID: 17858
	private string tooltip;
}
