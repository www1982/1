using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E3B RID: 3643
public class SpecialCargoBayClusterSideScreen : ReceptacleSideScreen
{
	// Token: 0x0600736E RID: 29550 RVA: 0x002BDC2B File Offset: 0x002BBE2B
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x0600736F RID: 29551 RVA: 0x002BDC33 File Offset: 0x002BBE33
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<SpecialCargoBayClusterReceptacle>() != null;
	}

	// Token: 0x06007370 RID: 29552 RVA: 0x002BDC41 File Offset: 0x002BBE41
	protected override bool RequiresAvailableAmountToDeposit()
	{
		return false;
	}

	// Token: 0x06007371 RID: 29553 RVA: 0x002BDC44 File Offset: 0x002BBE44
	protected override void UpdateState(object data)
	{
		base.UpdateState(data);
		this.SetDescriptionSidescreenFoldState(this.targetReceptacle != null && this.targetReceptacle.Occupant == null);
	}

	// Token: 0x06007372 RID: 29554 RVA: 0x002BDC78 File Offset: 0x002BBE78
	protected override void SetResultDescriptions(GameObject go)
	{
		base.SetResultDescriptions(go);
		if (this.targetReceptacle != null && this.targetReceptacle.Occupant != null)
		{
			this.descriptionLabel.SetText("");
			this.SetDescriptionSidescreenFoldState(false);
			return;
		}
		this.SetDescriptionSidescreenFoldState(true);
	}

	// Token: 0x06007373 RID: 29555 RVA: 0x002BDCCC File Offset: 0x002BBECC
	public void SetDescriptionSidescreenFoldState(bool visible)
	{
		this.descriptionContent.minHeight = (visible ? this.descriptionLayoutDefaultSize : 0f);
	}

	// Token: 0x04004F71 RID: 20337
	public LayoutElement descriptionContent;

	// Token: 0x04004F72 RID: 20338
	public float descriptionLayoutDefaultSize = -1f;
}
