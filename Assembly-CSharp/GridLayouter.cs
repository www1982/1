using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CDF RID: 3295
public class GridLayouter
{
	// Token: 0x0600658A RID: 25994 RVA: 0x00263E38 File Offset: 0x00262038
	[Conditional("UNITY_EDITOR")]
	private void ValidateImportantFieldsAreSet()
	{
		global::Debug.Assert(this.minCellSize >= 0f, string.Format("[{0} Error] Minimum cell size is invalid. Given: {1}", "GridLayouter", this.minCellSize));
		global::Debug.Assert(this.maxCellSize >= 0f, string.Format("[{0} Error] Maximum cell size is invalid. Given: {1}", "GridLayouter", this.maxCellSize));
		global::Debug.Assert(this.targetGridLayouts != null, string.Format("[{0} Error] Target grid layout is invalid. Given: {1}", "GridLayouter", this.targetGridLayouts));
	}

	// Token: 0x0600658B RID: 25995 RVA: 0x00263EC8 File Offset: 0x002620C8
	public void CheckIfShouldResizeGrid()
	{
		Vector2 vector = new Vector2((float)Screen.width, (float)Screen.height);
		if (vector != this.oldScreenSize)
		{
			this.RequestGridResize();
		}
		this.oldScreenSize = vector;
		float @float = KPlayerPrefs.GetFloat(KCanvasScaler.UIScalePrefKey);
		if (@float != this.oldScreenScale)
		{
			this.RequestGridResize();
		}
		this.oldScreenScale = @float;
		this.ResizeGridIfRequested();
	}

	// Token: 0x0600658C RID: 25996 RVA: 0x00263F2A File Offset: 0x0026212A
	public void RequestGridResize()
	{
		this.framesLeftToResizeGrid = 3;
	}

	// Token: 0x0600658D RID: 25997 RVA: 0x00263F33 File Offset: 0x00262133
	private void ResizeGridIfRequested()
	{
		if (this.framesLeftToResizeGrid > 0)
		{
			this.ImmediateSizeGridToScreenResolution();
			this.framesLeftToResizeGrid--;
			if (this.framesLeftToResizeGrid == 0 && this.OnSizeGridComplete != null)
			{
				this.OnSizeGridComplete();
			}
		}
	}

	// Token: 0x0600658E RID: 25998 RVA: 0x00263F70 File Offset: 0x00262170
	public void ImmediateSizeGridToScreenResolution()
	{
		foreach (GridLayoutGroup gridLayoutGroup in this.targetGridLayouts)
		{
			float num = ((this.overrideParentForSizeReference != null) ? this.overrideParentForSizeReference.rect.size.x : gridLayoutGroup.transform.parent.rectTransform().rect.size.x) - (float)gridLayoutGroup.padding.left - (float)gridLayoutGroup.padding.right;
			float x = gridLayoutGroup.spacing.x;
			int num2 = GridLayouter.<ImmediateSizeGridToScreenResolution>g__GetCellCountToFit|12_1(this.maxCellSize, x, num) + 1;
			float num3;
			for (num3 = GridLayouter.<ImmediateSizeGridToScreenResolution>g__GetCellSize|12_0(num, x, num2); num3 < this.minCellSize; num3 = Mathf.Min(this.maxCellSize, GridLayouter.<ImmediateSizeGridToScreenResolution>g__GetCellSize|12_0(num, x, num2)))
			{
				num2--;
				if (num2 <= 0)
				{
					num2 = 1;
					num3 = this.minCellSize;
					break;
				}
			}
			gridLayoutGroup.childAlignment = ((num2 == 1) ? TextAnchor.UpperCenter : TextAnchor.UpperLeft);
			gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
			gridLayoutGroup.constraintCount = num2;
			gridLayoutGroup.cellSize = Vector2.one * num3;
		}
	}

	// Token: 0x06006590 RID: 26000 RVA: 0x002640E6 File Offset: 0x002622E6
	[CompilerGenerated]
	internal static float <ImmediateSizeGridToScreenResolution>g__GetCellSize|12_0(float workingWidth, float spacingSize, int count)
	{
		return (workingWidth - (spacingSize * (float)count - 1f)) / (float)count;
	}

	// Token: 0x06006591 RID: 26001 RVA: 0x002640F8 File Offset: 0x002622F8
	[CompilerGenerated]
	internal static int <ImmediateSizeGridToScreenResolution>g__GetCellCountToFit|12_1(float cellSize, float spacingSize, float workingWidth)
	{
		int num = 0;
		for (float num2 = cellSize; num2 < workingWidth; num2 += cellSize + spacingSize)
		{
			num++;
		}
		return num;
	}

	// Token: 0x04004581 RID: 17793
	public float minCellSize = -1f;

	// Token: 0x04004582 RID: 17794
	public float maxCellSize = -1f;

	// Token: 0x04004583 RID: 17795
	public List<GridLayoutGroup> targetGridLayouts;

	// Token: 0x04004584 RID: 17796
	public RectTransform overrideParentForSizeReference;

	// Token: 0x04004585 RID: 17797
	public global::System.Action OnSizeGridComplete;

	// Token: 0x04004586 RID: 17798
	private Vector2 oldScreenSize;

	// Token: 0x04004587 RID: 17799
	private float oldScreenScale;

	// Token: 0x04004588 RID: 17800
	private int framesLeftToResizeGrid;
}
