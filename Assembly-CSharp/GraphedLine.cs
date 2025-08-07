using System;
using UnityEngine;
using UnityEngine.UI.Extensions;

// Token: 0x02000CDC RID: 3292
[AddComponentMenu("KMonoBehaviour/scripts/GraphedLine")]
[Serializable]
public class GraphedLine : KMonoBehaviour
{
	// Token: 0x1700075F RID: 1887
	// (get) Token: 0x06006570 RID: 25968 RVA: 0x00262981 File Offset: 0x00260B81
	public int PointCount
	{
		get
		{
			return this.points.Length;
		}
	}

	// Token: 0x06006571 RID: 25969 RVA: 0x0026298B File Offset: 0x00260B8B
	public void SetPoints(Vector2[] points)
	{
		this.points = points;
		this.UpdatePoints();
	}

	// Token: 0x06006572 RID: 25970 RVA: 0x0026299C File Offset: 0x00260B9C
	private void UpdatePoints()
	{
		Vector2[] array = new Vector2[this.points.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = this.layer.graph.GetRelativePosition(this.points[i]);
		}
		this.line_renderer.Points = array;
	}

	// Token: 0x06006573 RID: 25971 RVA: 0x002629F4 File Offset: 0x00260BF4
	public Vector2 GetClosestDataToPointOnXAxis(Vector2 toPoint)
	{
		float num = toPoint.x / this.layer.graph.rectTransform().sizeDelta.x;
		float num2 = this.layer.graph.axis_x.min_value + this.layer.graph.axis_x.range * num;
		Vector2 vector = Vector2.zero;
		foreach (Vector2 vector2 in this.points)
		{
			if (Mathf.Abs(vector2.x - num2) < Mathf.Abs(vector.x - num2))
			{
				vector = vector2;
			}
		}
		return vector;
	}

	// Token: 0x06006574 RID: 25972 RVA: 0x00262A9B File Offset: 0x00260C9B
	public void HidePointHighlight()
	{
		if (this.highlightPoint != null)
		{
			this.highlightPoint.SetActive(false);
		}
	}

	// Token: 0x06006575 RID: 25973 RVA: 0x00262AB8 File Offset: 0x00260CB8
	public void SetPointHighlight(Vector2 point)
	{
		if (this.highlightPoint == null)
		{
			return;
		}
		this.highlightPoint.SetActive(true);
		Vector2 relativePosition = this.layer.graph.GetRelativePosition(point);
		this.highlightPoint.rectTransform().SetLocalPosition(new Vector2(relativePosition.x * this.layer.graph.rectTransform().sizeDelta.x - this.layer.graph.rectTransform().sizeDelta.x / 2f, relativePosition.y * this.layer.graph.rectTransform().sizeDelta.y - this.layer.graph.rectTransform().sizeDelta.y / 2f));
		ToolTip component = this.layer.graph.GetComponent<ToolTip>();
		component.ClearMultiStringTooltip();
		component.tooltipPositionOffset = new Vector2(this.highlightPoint.rectTransform().localPosition.x, this.layer.graph.rectTransform().rect.height / 2f - 12f);
		component.SetSimpleTooltip(string.Concat(new string[]
		{
			this.layer.graph.axis_x.name,
			" ",
			point.x.ToString(),
			", ",
			Mathf.RoundToInt(point.y).ToString(),
			" ",
			this.layer.graph.axis_y.name
		}));
		ToolTipScreen.Instance.SetToolTip(component);
	}

	// Token: 0x0400456D RID: 17773
	public UILineRenderer line_renderer;

	// Token: 0x0400456E RID: 17774
	public LineLayer layer;

	// Token: 0x0400456F RID: 17775
	private Vector2[] points = new Vector2[0];

	// Token: 0x04004570 RID: 17776
	[SerializeField]
	private GameObject highlightPoint;
}
