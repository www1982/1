using System;
using UnityEngine;

// Token: 0x02000689 RID: 1673
public class DevToolWarmthZonesVisualizer : DevTool
{
	// Token: 0x06002902 RID: 10498 RVA: 0x000EE4FC File Offset: 0x000EC6FC
	private void SetupColors()
	{
		if (this.colors == null)
		{
			this.colors = new Color[3];
			for (int i = 1; i <= 3; i++)
			{
				this.colors[i - 1] = this.CreateColorForWarmthValue(i);
			}
		}
	}

	// Token: 0x06002903 RID: 10499 RVA: 0x000EE540 File Offset: 0x000EC740
	private Color CreateColorForWarmthValue(int warmValue)
	{
		float num = (float)Mathf.Clamp(warmValue, 1, 3) / 3f;
		Color color = this.WARM_CELL_COLOR * num;
		color.a = this.WARM_CELL_COLOR.a;
		return color;
	}

	// Token: 0x06002904 RID: 10500 RVA: 0x000EE580 File Offset: 0x000EC780
	private Color GetBorderColor(int warmValue)
	{
		int num = Mathf.Clamp(warmValue, 0, 3);
		return this.colors[num];
	}

	// Token: 0x06002905 RID: 10501 RVA: 0x000EE5A4 File Offset: 0x000EC7A4
	private Color GetFillColor(int warmValue)
	{
		Color borderColor = this.GetBorderColor(warmValue);
		borderColor.a = 0.3f;
		return borderColor;
	}

	// Token: 0x06002906 RID: 10502 RVA: 0x000EE5C8 File Offset: 0x000EC7C8
	protected override void RenderTo(DevPanel panel)
	{
		this.SetupColors();
		foreach (int num in WarmthProvider.WarmCells.Keys)
		{
			if (Grid.IsValidCell(num) && WarmthProvider.IsWarmCell(num))
			{
				int warmthValue = WarmthProvider.GetWarmthValue(num);
				Option<ValueTuple<Vector2, Vector2>> screenRect = new DevToolEntityTarget.ForSimCell(num).GetScreenRect();
				string text = warmthValue.ToString();
				DevToolEntity.DrawScreenRect(screenRect.Unwrap(), text, this.GetBorderColor(warmthValue - 1), this.GetFillColor(warmthValue - 1), new Option<DevToolUtil.TextAlignment>(DevToolUtil.TextAlignment.Center));
			}
		}
	}

	// Token: 0x0400182B RID: 6187
	private const int MAX_COLOR_VARIANTS = 3;

	// Token: 0x0400182C RID: 6188
	private Color WARM_CELL_COLOR = Color.red;

	// Token: 0x0400182D RID: 6189
	private Color[] colors;
}
