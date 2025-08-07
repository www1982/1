using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000E40 RID: 3648
public class TemporalTearSideScreen : SideScreenContent
{
	// Token: 0x170007FB RID: 2043
	// (get) Token: 0x0600739C RID: 29596 RVA: 0x002BEA2C File Offset: 0x002BCC2C
	private CraftModuleInterface craftModuleInterface
	{
		get
		{
			return this.targetCraft.GetComponent<CraftModuleInterface>();
		}
	}

	// Token: 0x0600739D RID: 29597 RVA: 0x002BEA39 File Offset: 0x002BCC39
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		base.ConsumeMouseScroll = true;
	}

	// Token: 0x0600739E RID: 29598 RVA: 0x002BEA49 File Offset: 0x002BCC49
	public override float GetSortKey()
	{
		return 21f;
	}

	// Token: 0x0600739F RID: 29599 RVA: 0x002BEA50 File Offset: 0x002BCC50
	public override bool IsValidForTarget(GameObject target)
	{
		Clustercraft component = target.GetComponent<Clustercraft>();
		TemporalTear temporalTear = ClusterManager.Instance.GetComponent<ClusterPOIManager>().GetTemporalTear();
		return component != null && temporalTear != null && temporalTear.Location == component.Location;
	}

	// Token: 0x060073A0 RID: 29600 RVA: 0x002BEA9C File Offset: 0x002BCC9C
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.targetCraft = target.GetComponent<Clustercraft>();
		KButton reference = base.GetComponent<HierarchyReferences>().GetReference<KButton>("button");
		reference.ClearOnClick();
		reference.onClick += delegate
		{
			target.GetComponent<Clustercraft>();
			ClusterManager.Instance.GetComponent<ClusterPOIManager>().GetTemporalTear().ConsumeCraft(this.targetCraft);
		};
		this.RefreshPanel(null);
	}

	// Token: 0x060073A1 RID: 29601 RVA: 0x002BEB08 File Offset: 0x002BCD08
	private void RefreshPanel(object data = null)
	{
		TemporalTear temporalTear = ClusterManager.Instance.GetComponent<ClusterPOIManager>().GetTemporalTear();
		HierarchyReferences component = base.GetComponent<HierarchyReferences>();
		bool flag = temporalTear.IsOpen();
		component.GetReference<LocText>("label").SetText(flag ? UI.UISIDESCREENS.TEMPORALTEARSIDESCREEN.BUTTON_OPEN : UI.UISIDESCREENS.TEMPORALTEARSIDESCREEN.BUTTON_CLOSED);
		component.GetReference<KButton>("button").isInteractable = flag;
	}

	// Token: 0x04004F94 RID: 20372
	private Clustercraft targetCraft;
}
