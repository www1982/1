using System;
using UnityEngine;

// Token: 0x02000E1C RID: 3612
public class ProgressBarSideScreen : SideScreenContent, IRender1000ms
{
	// Token: 0x0600722D RID: 29229 RVA: 0x002B6B0F File Offset: 0x002B4D0F
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x0600722E RID: 29230 RVA: 0x002B6B17 File Offset: 0x002B4D17
	public override int GetSideScreenSortOrder()
	{
		return -10;
	}

	// Token: 0x0600722F RID: 29231 RVA: 0x002B6B1B File Offset: 0x002B4D1B
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<IProgressBarSideScreen>() != null;
	}

	// Token: 0x06007230 RID: 29232 RVA: 0x002B6B26 File Offset: 0x002B4D26
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.targetObject = target.GetComponent<IProgressBarSideScreen>();
		this.RefreshBar();
	}

	// Token: 0x06007231 RID: 29233 RVA: 0x002B6B44 File Offset: 0x002B4D44
	private void RefreshBar()
	{
		this.progressBar.SetMaxValue(this.targetObject.GetProgressBarMaxValue());
		this.progressBar.SetFillPercentage(this.targetObject.GetProgressBarFillPercentage());
		this.progressBar.label.SetText(this.targetObject.GetProgressBarLabel());
		this.label.SetText(this.targetObject.GetProgressBarTitleLabel());
		this.progressBar.GetComponentInChildren<ToolTip>().SetSimpleTooltip(this.targetObject.GetProgressBarTooltip());
	}

	// Token: 0x06007232 RID: 29234 RVA: 0x002B6BC9 File Offset: 0x002B4DC9
	public void Render1000ms(float dt)
	{
		this.RefreshBar();
	}

	// Token: 0x04004E9A RID: 20122
	public LocText label;

	// Token: 0x04004E9B RID: 20123
	public GenericUIProgressBar progressBar;

	// Token: 0x04004E9C RID: 20124
	public IProgressBarSideScreen targetObject;
}
