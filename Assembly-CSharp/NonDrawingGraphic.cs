using System;
using UnityEngine.UI;

// Token: 0x02000D7B RID: 3451
public class NonDrawingGraphic : Graphic
{
	// Token: 0x06006B59 RID: 27481 RVA: 0x00288B8D File Offset: 0x00286D8D
	public override void SetMaterialDirty()
	{
	}

	// Token: 0x06006B5A RID: 27482 RVA: 0x00288B8F File Offset: 0x00286D8F
	public override void SetVerticesDirty()
	{
	}

	// Token: 0x06006B5B RID: 27483 RVA: 0x00288B91 File Offset: 0x00286D91
	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();
	}
}
