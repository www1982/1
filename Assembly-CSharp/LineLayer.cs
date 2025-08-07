using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CDD RID: 3293
public class LineLayer : GraphLayer
{
	// Token: 0x06006577 RID: 25975 RVA: 0x00262C90 File Offset: 0x00260E90
	private void InitAreaTexture()
	{
		if (this.areaTexture != null)
		{
			return;
		}
		this.areaTexture = new Texture2D(96, 32);
		this.areaFill.sprite = Sprite.Create(this.areaTexture, new Rect(0f, 0f, (float)this.areaTexture.width, (float)this.areaTexture.height), new Vector2(0.5f, 0.5f), 100f);
	}

	// Token: 0x06006578 RID: 25976 RVA: 0x00262D0C File Offset: 0x00260F0C
	public virtual GraphedLine NewLine(global::Tuple<float, float>[] points, string ID = "")
	{
		Vector2[] array = new Vector2[points.Length];
		for (int i = 0; i < points.Length; i++)
		{
			array[i] = new Vector2(points[i].first, points[i].second);
		}
		if (this.fillAreaUnderLine)
		{
			this.InitAreaTexture();
			Vector2 vector = this.CalculateMin(points);
			Vector2 vector2 = this.CalculateMax(points) - vector;
			this.areaTexture.filterMode = FilterMode.Point;
			for (int j = 0; j < this.areaTexture.width; j++)
			{
				float num = vector.x + vector2.x * ((float)j / (float)this.areaTexture.width);
				if (points.Length > 1)
				{
					int num2 = 1;
					for (int k = 1; k < points.Length; k++)
					{
						if (points[k].first >= num)
						{
							num2 = k;
							break;
						}
					}
					Vector2 vector3 = new Vector2(points[num2].first - points[num2 - 1].first, points[num2].second - points[num2 - 1].second);
					float num3 = (num - points[num2 - 1].first) / vector3.x;
					bool flag = false;
					int num4 = -1;
					for (int l = this.areaTexture.height - 1; l >= 0; l--)
					{
						if (!flag && vector.y + vector2.y * ((float)l / (float)this.areaTexture.height) < points[num2 - 1].second + vector3.y * num3)
						{
							flag = true;
							num4 = l;
						}
						Color color = (flag ? new Color(1f, 1f, 1f, Mathf.Lerp(1f, this.fillAlphaMin, Mathf.Clamp((float)(num4 - l) / this.fillFadePixels, 0f, 1f))) : Color.clear);
						this.areaTexture.SetPixel(j, l, color);
					}
				}
			}
			this.areaTexture.Apply();
			this.areaFill.color = this.line_formatting[0].color;
		}
		return this.NewLine(array, ID);
	}

	// Token: 0x06006579 RID: 25977 RVA: 0x00262F30 File Offset: 0x00261130
	private GraphedLine FindLine(string ID)
	{
		string text = string.Format("line_{0}", ID);
		foreach (GraphedLine graphedLine in this.lines)
		{
			if (graphedLine.name == text)
			{
				return graphedLine.GetComponent<GraphedLine>();
			}
		}
		GameObject gameObject = Util.KInstantiateUI(this.prefab_line, this.line_container, true);
		gameObject.name = text;
		GraphedLine component = gameObject.GetComponent<GraphedLine>();
		this.lines.Add(component);
		return component;
	}

	// Token: 0x0600657A RID: 25978 RVA: 0x00262FD0 File Offset: 0x002611D0
	public virtual void RefreshLine(global::Tuple<float, float>[] data, string ID)
	{
		this.FillArea(data);
		Vector2[] array2;
		if (data.Length > this.compressDataToPointCount)
		{
			Vector2[] array = new Vector2[this.compressDataToPointCount];
			if (this.compressType == LineLayer.DataScalingType.DropValues)
			{
				float num = (float)(data.Length - this.compressDataToPointCount + 1);
				float num2 = (float)data.Length / num;
				int num3 = 0;
				float num4 = 0f;
				for (int i = 0; i < data.Length; i++)
				{
					num4 += 1f;
					if (num4 >= num2)
					{
						num4 -= num2;
					}
					else
					{
						array[num3] = new Vector2(data[i].first, data[i].second);
						num3++;
					}
				}
				if (array[this.compressDataToPointCount - 1] == Vector2.zero)
				{
					array[this.compressDataToPointCount - 1] = array[this.compressDataToPointCount - 2];
				}
			}
			else
			{
				int num5 = data.Length / this.compressDataToPointCount;
				for (int j = 0; j < this.compressDataToPointCount; j++)
				{
					if (j > 0)
					{
						float num6 = 0f;
						LineLayer.DataScalingType dataScalingType = this.compressType;
						if (dataScalingType != LineLayer.DataScalingType.Average)
						{
							if (dataScalingType == LineLayer.DataScalingType.Max)
							{
								for (int k = 0; k < num5; k++)
								{
									num6 = Mathf.Max(num6, data[j * num5 - k].second);
								}
							}
						}
						else
						{
							for (int l = 0; l < num5; l++)
							{
								num6 += data[j * num5 - l].second;
							}
							num6 /= (float)num5;
						}
						array[j] = new Vector2(data[j * num5].first, num6);
					}
				}
			}
			array2 = array;
		}
		else
		{
			array2 = new Vector2[data.Length];
			for (int m = 0; m < data.Length; m++)
			{
				array2[m] = new Vector2(data[m].first, data[m].second);
			}
		}
		GraphedLine graphedLine = this.FindLine(ID);
		graphedLine.SetPoints(array2);
		graphedLine.line_renderer.color = this.line_formatting[this.lines.Count % this.line_formatting.Length].color;
		graphedLine.line_renderer.LineThickness = (float)this.line_formatting[this.lines.Count % this.line_formatting.Length].thickness;
	}

	// Token: 0x0600657B RID: 25979 RVA: 0x0026321C File Offset: 0x0026141C
	private void FillArea(global::Tuple<float, float>[] points)
	{
		if (this.fillAreaUnderLine)
		{
			this.InitAreaTexture();
			Vector2 vector;
			Vector2 vector2;
			this.CalculateMinMax(points, out vector, out vector2);
			Vector2 vector3 = vector2 - vector;
			this.areaTexture.filterMode = FilterMode.Point;
			Vector2 vector4 = default(Vector2);
			for (int i = 0; i < this.areaTexture.width; i++)
			{
				float num = vector.x + vector3.x * ((float)i / (float)this.areaTexture.width);
				if (points.Length > 1)
				{
					int num2 = 1;
					for (int j = 1; j < points.Length; j++)
					{
						if (points[j].first >= num)
						{
							num2 = j;
							break;
						}
					}
					vector4.x = points[num2].first - points[num2 - 1].first;
					vector4.y = points[num2].second - points[num2 - 1].second;
					float num3 = (num - points[num2 - 1].first) / vector4.x;
					bool flag = false;
					int num4 = -1;
					for (int k = this.areaTexture.height - 1; k >= 0; k--)
					{
						if (!flag && vector.y + vector3.y * ((float)k / (float)this.areaTexture.height) < points[num2 - 1].second + vector4.y * num3)
						{
							flag = true;
							num4 = k;
						}
						Color color = (flag ? new Color(1f, 1f, 1f, Mathf.Lerp(1f, this.fillAlphaMin, Mathf.Clamp((float)(num4 - k) / this.fillFadePixels, 0f, 1f))) : Color.clear);
						this.areaTexture.SetPixel(i, k, color);
					}
				}
			}
			this.areaTexture.Apply();
			this.areaFill.color = this.line_formatting[0].color;
		}
	}

	// Token: 0x0600657C RID: 25980 RVA: 0x00263410 File Offset: 0x00261610
	private void CalculateMinMax(global::Tuple<float, float>[] points, out Vector2 min, out Vector2 max)
	{
		max = new Vector2(float.NegativeInfinity, float.NegativeInfinity);
		min = new Vector2(float.PositiveInfinity, 0f);
		for (int i = 0; i < points.Length; i++)
		{
			max = new Vector2(Mathf.Max(points[i].first, max.x), Mathf.Max(points[i].second, max.y));
			min = new Vector2(Mathf.Min(points[i].first, min.x), Mathf.Min(points[i].second, min.y));
		}
	}

	// Token: 0x0600657D RID: 25981 RVA: 0x002634B8 File Offset: 0x002616B8
	protected Vector2 CalculateMax(global::Tuple<float, float>[] points)
	{
		Vector2 vector = new Vector2(float.NegativeInfinity, float.NegativeInfinity);
		for (int i = 0; i < points.Length; i++)
		{
			vector = new Vector2(Mathf.Max(points[i].first, vector.x), Mathf.Max(points[i].second, vector.y));
		}
		return vector;
	}

	// Token: 0x0600657E RID: 25982 RVA: 0x00263514 File Offset: 0x00261714
	protected Vector2 CalculateMin(global::Tuple<float, float>[] points)
	{
		Vector2 vector = new Vector2(float.PositiveInfinity, 0f);
		for (int i = 0; i < points.Length; i++)
		{
			vector = new Vector2(Mathf.Min(points[i].first, vector.x), Mathf.Min(points[i].second, vector.y));
		}
		return vector;
	}

	// Token: 0x0600657F RID: 25983 RVA: 0x00263570 File Offset: 0x00261770
	public GraphedLine NewLine(Vector2[] points, string ID = "")
	{
		GameObject gameObject = Util.KInstantiateUI(this.prefab_line, this.line_container, true);
		if (ID == "")
		{
			ID = this.lines.Count.ToString();
		}
		gameObject.name = string.Format("line_{0}", ID);
		GraphedLine component = gameObject.GetComponent<GraphedLine>();
		if (points.Length > this.compressDataToPointCount)
		{
			Vector2[] array = new Vector2[this.compressDataToPointCount];
			if (this.compressType == LineLayer.DataScalingType.DropValues)
			{
				float num = (float)(points.Length - this.compressDataToPointCount + 1);
				float num2 = (float)points.Length / num;
				int num3 = 0;
				float num4 = 0f;
				for (int i = 0; i < points.Length; i++)
				{
					num4 += 1f;
					if (num4 >= num2)
					{
						num4 -= num2;
					}
					else
					{
						array[num3] = points[i];
						num3++;
					}
				}
				if (array[this.compressDataToPointCount - 1] == Vector2.zero)
				{
					array[this.compressDataToPointCount - 1] = array[this.compressDataToPointCount - 2];
				}
			}
			else
			{
				int num5 = points.Length / this.compressDataToPointCount;
				for (int j = 0; j < this.compressDataToPointCount; j++)
				{
					if (j > 0)
					{
						float num6 = 0f;
						LineLayer.DataScalingType dataScalingType = this.compressType;
						if (dataScalingType != LineLayer.DataScalingType.Average)
						{
							if (dataScalingType == LineLayer.DataScalingType.Max)
							{
								for (int k = 0; k < num5; k++)
								{
									num6 = Mathf.Max(num6, points[j * num5 - k].y);
								}
							}
						}
						else
						{
							for (int l = 0; l < num5; l++)
							{
								num6 += points[j * num5 - l].y;
							}
							num6 /= (float)num5;
						}
						array[j] = new Vector2(points[j * num5].x, num6);
					}
				}
			}
			points = array;
		}
		component.SetPoints(points);
		component.line_renderer.color = this.line_formatting[this.lines.Count % this.line_formatting.Length].color;
		component.line_renderer.LineThickness = (float)this.line_formatting[this.lines.Count % this.line_formatting.Length].thickness;
		this.lines.Add(component);
		return component;
	}

	// Token: 0x06006580 RID: 25984 RVA: 0x002637CC File Offset: 0x002619CC
	public void ClearLines()
	{
		foreach (GraphedLine graphedLine in this.lines)
		{
			if (graphedLine != null && graphedLine.gameObject != null)
			{
				global::UnityEngine.Object.DestroyImmediate(graphedLine.gameObject);
			}
		}
		this.lines.Clear();
	}

	// Token: 0x06006581 RID: 25985 RVA: 0x00263848 File Offset: 0x00261A48
	private void Update()
	{
		RectTransform component = base.gameObject.GetComponent<RectTransform>();
		if (!RectTransformUtility.RectangleContainsScreenPoint(component, Input.mousePosition))
		{
			for (int i = 0; i < this.lines.Count; i++)
			{
				this.lines[i].HidePointHighlight();
			}
			return;
		}
		Vector2 vector = Vector2.zero;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(base.gameObject.GetComponent<RectTransform>(), Input.mousePosition, null, out vector);
		vector += component.sizeDelta / 2f;
		for (int j = 0; j < this.lines.Count; j++)
		{
			if (this.lines[j].PointCount != 0)
			{
				Vector2 closestDataToPointOnXAxis = this.lines[j].GetClosestDataToPointOnXAxis(vector);
				if (!float.IsInfinity(closestDataToPointOnXAxis.x) && !float.IsNaN(closestDataToPointOnXAxis.x) && !float.IsInfinity(closestDataToPointOnXAxis.y) && !float.IsNaN(closestDataToPointOnXAxis.y))
				{
					this.lines[j].SetPointHighlight(closestDataToPointOnXAxis);
				}
				else
				{
					this.lines[j].HidePointHighlight();
				}
			}
		}
	}

	// Token: 0x04004571 RID: 17777
	[Header("Lines")]
	public LineLayer.LineFormat[] line_formatting;

	// Token: 0x04004572 RID: 17778
	public Image areaFill;

	// Token: 0x04004573 RID: 17779
	public GameObject prefab_line;

	// Token: 0x04004574 RID: 17780
	public GameObject line_container;

	// Token: 0x04004575 RID: 17781
	private List<GraphedLine> lines = new List<GraphedLine>();

	// Token: 0x04004576 RID: 17782
	protected float fillAlphaMin = 0.33f;

	// Token: 0x04004577 RID: 17783
	protected float fillFadePixels = 15f;

	// Token: 0x04004578 RID: 17784
	public bool fillAreaUnderLine;

	// Token: 0x04004579 RID: 17785
	private Texture2D areaTexture;

	// Token: 0x0400457A RID: 17786
	private int compressDataToPointCount = 256;

	// Token: 0x0400457B RID: 17787
	private LineLayer.DataScalingType compressType = LineLayer.DataScalingType.DropValues;

	// Token: 0x02001EA7 RID: 7847
	[Serializable]
	public struct LineFormat
	{
		// Token: 0x04008E2F RID: 36399
		public Color color;

		// Token: 0x04008E30 RID: 36400
		public int thickness;
	}

	// Token: 0x02001EA8 RID: 7848
	public enum DataScalingType
	{
		// Token: 0x04008E32 RID: 36402
		Average,
		// Token: 0x04008E33 RID: 36403
		Max,
		// Token: 0x04008E34 RID: 36404
		DropValues
	}
}
