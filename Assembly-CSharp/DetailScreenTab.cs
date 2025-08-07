using System;
using UnityEngine;

// Token: 0x02000CBE RID: 3262
public abstract class DetailScreenTab : TargetPanel
{
	// Token: 0x06006468 RID: 25704
	public abstract override bool IsValidForTarget(GameObject target);

	// Token: 0x06006469 RID: 25705 RVA: 0x0025B7C8 File Offset: 0x002599C8
	protected override void OnSelectTarget(GameObject target)
	{
		base.OnSelectTarget(target);
	}

	// Token: 0x0600646A RID: 25706 RVA: 0x0025B7D4 File Offset: 0x002599D4
	protected CollapsibleDetailContentPanel CreateCollapsableSection(string title = null)
	{
		CollapsibleDetailContentPanel collapsibleDetailContentPanel = Util.KInstantiateUI<CollapsibleDetailContentPanel>(ScreenPrefabs.Instance.CollapsableContentPanel, base.gameObject, false);
		if (!string.IsNullOrEmpty(title))
		{
			collapsibleDetailContentPanel.SetTitle(title);
		}
		return collapsibleDetailContentPanel;
	}

	// Token: 0x0600646B RID: 25707 RVA: 0x0025B808 File Offset: 0x00259A08
	private void Update()
	{
		this.Refresh(false);
	}

	// Token: 0x0600646C RID: 25708 RVA: 0x0025B811 File Offset: 0x00259A11
	protected virtual void Refresh(bool force = false)
	{
	}
}
