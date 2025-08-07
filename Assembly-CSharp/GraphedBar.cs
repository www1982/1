using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CD9 RID: 3289
[AddComponentMenu("KMonoBehaviour/scripts/GraphedBar")]
[Serializable]
public class GraphedBar : KMonoBehaviour
{
	// Token: 0x0600656A RID: 25962 RVA: 0x002627FF File Offset: 0x002609FF
	public void SetFormat(GraphedBarFormatting format)
	{
		this.format = format;
	}

	// Token: 0x0600656B RID: 25963 RVA: 0x00262808 File Offset: 0x00260A08
	public void SetValues(int[] values, float x_position)
	{
		this.ClearValues();
		base.gameObject.rectTransform().anchorMin = new Vector2(x_position, 0f);
		base.gameObject.rectTransform().anchorMax = new Vector2(x_position, 1f);
		base.gameObject.rectTransform().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)this.format.width);
		for (int i = 0; i < values.Length; i++)
		{
			GameObject gameObject = Util.KInstantiateUI(this.prefab_segment, this.segments_container, true);
			LayoutElement component = gameObject.GetComponent<LayoutElement>();
			component.preferredHeight = (float)values[i];
			component.minWidth = (float)this.format.width;
			gameObject.GetComponent<Image>().color = this.format.colors[i % this.format.colors.Length];
			this.segments.Add(gameObject);
		}
	}

	// Token: 0x0600656C RID: 25964 RVA: 0x002628E8 File Offset: 0x00260AE8
	public void ClearValues()
	{
		foreach (GameObject gameObject in this.segments)
		{
			global::UnityEngine.Object.DestroyImmediate(gameObject);
		}
		this.segments.Clear();
	}

	// Token: 0x04004566 RID: 17766
	public GameObject segments_container;

	// Token: 0x04004567 RID: 17767
	public GameObject prefab_segment;

	// Token: 0x04004568 RID: 17768
	private List<GameObject> segments = new List<GameObject>();

	// Token: 0x04004569 RID: 17769
	private GraphedBarFormatting format;
}
