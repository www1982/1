using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000CD8 RID: 3288
public class BarLayer : GraphLayer
{
	// Token: 0x1700075D RID: 1885
	// (get) Token: 0x06006566 RID: 25958 RVA: 0x00262663 File Offset: 0x00260863
	public int bar_count
	{
		get
		{
			return this.bars.Count;
		}
	}

	// Token: 0x06006567 RID: 25959 RVA: 0x00262670 File Offset: 0x00260870
	public void NewBar(int[] values, float x_position, string ID = "")
	{
		GameObject gameObject = Util.KInstantiateUI(this.prefab_bar, this.bar_container, true);
		if (ID == "")
		{
			ID = this.bars.Count.ToString();
		}
		gameObject.name = string.Format("bar_{0}", ID);
		GraphedBar component = gameObject.GetComponent<GraphedBar>();
		component.SetFormat(this.bar_formats[this.bars.Count % this.bar_formats.Length]);
		int[] array = new int[values.Length];
		for (int i = 0; i < values.Length; i++)
		{
			array[i] = (int)(base.graph.rectTransform().rect.height * base.graph.GetRelativeSize(new Vector2(0f, (float)values[i])).y);
		}
		component.SetValues(array, base.graph.GetRelativePosition(new Vector2(x_position, 0f)).x);
		this.bars.Add(component);
	}

	// Token: 0x06006568 RID: 25960 RVA: 0x00262770 File Offset: 0x00260970
	public void ClearBars()
	{
		foreach (GraphedBar graphedBar in this.bars)
		{
			if (graphedBar != null && graphedBar.gameObject != null)
			{
				global::UnityEngine.Object.DestroyImmediate(graphedBar.gameObject);
			}
		}
		this.bars.Clear();
	}

	// Token: 0x04004562 RID: 17762
	public GameObject bar_container;

	// Token: 0x04004563 RID: 17763
	public GameObject prefab_bar;

	// Token: 0x04004564 RID: 17764
	public GraphedBarFormatting[] bar_formats;

	// Token: 0x04004565 RID: 17765
	private List<GraphedBar> bars = new List<GraphedBar>();
}
