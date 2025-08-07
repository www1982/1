using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000C49 RID: 3145
public class DetailsPanelDrawer
{
	// Token: 0x06006014 RID: 24596 RVA: 0x0023651C File Offset: 0x0023471C
	public DetailsPanelDrawer(GameObject label_prefab, GameObject parent)
	{
		this.parent = parent;
		this.labelPrefab = label_prefab;
		this.stringformatter = new UIStringFormatter();
		this.floatFormatter = new UIFloatFormatter();
	}

	// Token: 0x06006015 RID: 24597 RVA: 0x00236554 File Offset: 0x00234754
	public DetailsPanelDrawer NewLabel(string text)
	{
		DetailsPanelDrawer.Label label = default(DetailsPanelDrawer.Label);
		if (this.activeLabelCount >= this.labels.Count)
		{
			label.text = Util.KInstantiate(this.labelPrefab, this.parent, null).GetComponent<LocText>();
			label.tooltip = label.text.GetComponent<ToolTip>();
			label.text.transform.localScale = new Vector3(1f, 1f, 1f);
			this.labels.Add(label);
		}
		else
		{
			label = this.labels[this.activeLabelCount];
		}
		this.activeLabelCount++;
		label.text.text = text;
		label.tooltip.toolTip = "";
		label.tooltip.OnToolTip = null;
		label.text.gameObject.SetActive(true);
		return this;
	}

	// Token: 0x06006016 RID: 24598 RVA: 0x00236638 File Offset: 0x00234838
	public DetailsPanelDrawer BeginDrawing()
	{
		return this;
	}

	// Token: 0x06006017 RID: 24599 RVA: 0x0023663B File Offset: 0x0023483B
	public DetailsPanelDrawer EndDrawing()
	{
		return this;
	}

	// Token: 0x0400411D RID: 16669
	private List<DetailsPanelDrawer.Label> labels = new List<DetailsPanelDrawer.Label>();

	// Token: 0x0400411E RID: 16670
	private int activeLabelCount;

	// Token: 0x0400411F RID: 16671
	private UIStringFormatter stringformatter;

	// Token: 0x04004120 RID: 16672
	private UIFloatFormatter floatFormatter;

	// Token: 0x04004121 RID: 16673
	private GameObject parent;

	// Token: 0x04004122 RID: 16674
	private GameObject labelPrefab;

	// Token: 0x02001E12 RID: 7698
	private struct Label
	{
		// Token: 0x04008C7D RID: 35965
		public LocText text;

		// Token: 0x04008C7E RID: 35966
		public ToolTip tooltip;
	}
}
