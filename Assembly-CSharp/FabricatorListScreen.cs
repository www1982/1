using System;
using System.Collections.Generic;

// Token: 0x02000CC9 RID: 3273
public class FabricatorListScreen : KToggleMenu
{
	// Token: 0x060064E0 RID: 25824 RVA: 0x0025F164 File Offset: 0x0025D364
	private void Refresh()
	{
		List<KToggleMenu.ToggleInfo> list = new List<KToggleMenu.ToggleInfo>();
		foreach (Fabricator fabricator in Components.Fabricators.Items)
		{
			KSelectable component = fabricator.GetComponent<KSelectable>();
			list.Add(new KToggleMenu.ToggleInfo(component.GetName(), fabricator, global::Action.NumActions));
		}
		base.Setup(list);
	}

	// Token: 0x060064E1 RID: 25825 RVA: 0x0025F1E0 File Offset: 0x0025D3E0
	protected override void OnSpawn()
	{
		base.onSelect += this.OnClickFabricator;
	}

	// Token: 0x060064E2 RID: 25826 RVA: 0x0025F1F4 File Offset: 0x0025D3F4
	protected override void OnActivate()
	{
		base.OnActivate();
		this.Refresh();
	}

	// Token: 0x060064E3 RID: 25827 RVA: 0x0025F204 File Offset: 0x0025D404
	private void OnClickFabricator(KToggleMenu.ToggleInfo toggle_info)
	{
		Fabricator fabricator = (Fabricator)toggle_info.userData;
		SelectTool.Instance.Select(fabricator.GetComponent<KSelectable>(), false);
	}
}
