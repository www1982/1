using System;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000DE2 RID: 3554
public class ClusterGridWorldSideScreen : SideScreenContent
{
	// Token: 0x06007046 RID: 28742 RVA: 0x002AAF24 File Offset: 0x002A9124
	protected override void OnSpawn()
	{
		this.viewButton.onClick += this.OnClickView;
	}

	// Token: 0x06007047 RID: 28743 RVA: 0x002AAF3D File Offset: 0x002A913D
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<AsteroidGridEntity>() != null;
	}

	// Token: 0x06007048 RID: 28744 RVA: 0x002AAF4C File Offset: 0x002A914C
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.targetEntity = target.GetComponent<AsteroidGridEntity>();
		this.icon.sprite = Def.GetUISprite(this.targetEntity, "ui", false).first;
		WorldContainer component = this.targetEntity.GetComponent<WorldContainer>();
		bool flag = component != null && component.IsDiscovered;
		this.viewButton.isInteractable = flag;
		if (!flag)
		{
			this.viewButton.GetComponent<ToolTip>().SetSimpleTooltip(UI.UISIDESCREENS.CLUSTERWORLDSIDESCREEN.VIEW_WORLD_DISABLE_TOOLTIP);
			return;
		}
		this.viewButton.GetComponent<ToolTip>().SetSimpleTooltip(UI.UISIDESCREENS.CLUSTERWORLDSIDESCREEN.VIEW_WORLD_TOOLTIP);
	}

	// Token: 0x06007049 RID: 28745 RVA: 0x002AAFF0 File Offset: 0x002A91F0
	private void OnClickView()
	{
		WorldContainer component = this.targetEntity.GetComponent<WorldContainer>();
		if (!component.IsDupeVisited)
		{
			component.LookAtSurface();
		}
		ClusterManager.Instance.SetActiveWorld(component.id);
		ManagementMenu.Instance.CloseAll();
	}

	// Token: 0x04004D37 RID: 19767
	public Image icon;

	// Token: 0x04004D38 RID: 19768
	public KButton viewButton;

	// Token: 0x04004D39 RID: 19769
	private AsteroidGridEntity targetEntity;
}
