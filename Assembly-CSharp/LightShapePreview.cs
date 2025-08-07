using System;
using UnityEngine;

// Token: 0x020009A7 RID: 2471
[AddComponentMenu("KMonoBehaviour/scripts/LightShapePreview")]
public class LightShapePreview : KMonoBehaviour
{
	// Token: 0x060047CD RID: 18381 RVA: 0x0019E31C File Offset: 0x0019C51C
	private void Update()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		if (num != this.previousCell)
		{
			this.previousCell = num;
			LightGridManager.DestroyPreview();
			LightGridManager.CreatePreview(Grid.OffsetCell(num, this.offset), this.radius, this.shape, this.lux, this.width, this.direction);
		}
	}

	// Token: 0x060047CE RID: 18382 RVA: 0x0019E37E File Offset: 0x0019C57E
	protected override void OnCleanUp()
	{
		LightGridManager.DestroyPreview();
	}

	// Token: 0x04002F8A RID: 12170
	public float radius;

	// Token: 0x04002F8B RID: 12171
	public int lux;

	// Token: 0x04002F8C RID: 12172
	public int width;

	// Token: 0x04002F8D RID: 12173
	public DiscreteShadowCaster.Direction direction;

	// Token: 0x04002F8E RID: 12174
	public global::LightShape shape;

	// Token: 0x04002F8F RID: 12175
	public CellOffset offset;

	// Token: 0x04002F90 RID: 12176
	private int previousCell = -1;
}
