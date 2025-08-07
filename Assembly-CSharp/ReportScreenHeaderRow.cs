using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000DAA RID: 3498
[AddComponentMenu("KMonoBehaviour/scripts/ReportScreenHeaderRow")]
public class ReportScreenHeaderRow : KMonoBehaviour
{
	// Token: 0x06006DA3 RID: 28067 RVA: 0x00297FD4 File Offset: 0x002961D4
	public void SetLine(ReportManager.ReportGroup reportGroup)
	{
		LayoutElement component = this.name.GetComponent<LayoutElement>();
		component.minWidth = (component.preferredWidth = this.nameWidth);
		this.spacer.minWidth = this.groupSpacerWidth;
		this.name.text = reportGroup.stringKey;
	}

	// Token: 0x04004AF4 RID: 19188
	[SerializeField]
	public new LocText name;

	// Token: 0x04004AF5 RID: 19189
	[SerializeField]
	private LayoutElement spacer;

	// Token: 0x04004AF6 RID: 19190
	[SerializeField]
	private Image bgImage;

	// Token: 0x04004AF7 RID: 19191
	public float groupSpacerWidth;

	// Token: 0x04004AF8 RID: 19192
	private float nameWidth = 164f;

	// Token: 0x04004AF9 RID: 19193
	[SerializeField]
	private Color oddRowColor;
}
