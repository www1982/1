using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000D68 RID: 3432
public abstract class MeterScreen_ValueTrackerDisplayer : KMonoBehaviour
{
	// Token: 0x06006A5D RID: 27229 RVA: 0x00281B62 File Offset: 0x0027FD62
	protected override void OnSpawn()
	{
		this.Tooltip.OnToolTip = new Func<string>(this.OnTooltip);
		base.OnSpawn();
	}

	// Token: 0x06006A5E RID: 27230 RVA: 0x00281B82 File Offset: 0x0027FD82
	public void Refresh()
	{
		this.RefreshWorldMinionIdentities();
		this.InternalRefresh();
	}

	// Token: 0x06006A5F RID: 27231
	protected abstract void InternalRefresh();

	// Token: 0x06006A60 RID: 27232
	protected abstract string OnTooltip();

	// Token: 0x06006A61 RID: 27233 RVA: 0x00281B90 File Offset: 0x0027FD90
	public virtual void OnClick(BaseEventData base_ev_data)
	{
	}

	// Token: 0x06006A62 RID: 27234 RVA: 0x00281B94 File Offset: 0x0027FD94
	private void RefreshWorldMinionIdentities()
	{
		this.worldLiveMinionIdentities = new List<MinionIdentity>(from x in Components.LiveMinionIdentities.GetWorldItems(ClusterManager.Instance.activeWorldId, false)
			where !x.IsNullOrDestroyed()
			select x);
	}

	// Token: 0x06006A63 RID: 27235 RVA: 0x00281BE5 File Offset: 0x0027FDE5
	protected virtual List<MinionIdentity> GetWorldMinionIdentities()
	{
		if (this.worldLiveMinionIdentities == null)
		{
			this.RefreshWorldMinionIdentities();
		}
		if (this.minionListCustomSortOperation != null)
		{
			this.worldLiveMinionIdentities = this.minionListCustomSortOperation(this.worldLiveMinionIdentities);
		}
		return this.worldLiveMinionIdentities;
	}

	// Token: 0x06006A64 RID: 27236 RVA: 0x00281C1C File Offset: 0x0027FE1C
	protected virtual List<MinionIdentity> GetAllMinionsFromAllWorlds()
	{
		List<MinionIdentity> list = new List<MinionIdentity>(Components.LiveMinionIdentities.Items.Where((MinionIdentity x) => !x.IsNullOrDestroyed()));
		if (this.minionListCustomSortOperation != null)
		{
			this.worldLiveMinionIdentities = this.minionListCustomSortOperation(list);
		}
		return list;
	}

	// Token: 0x04004882 RID: 18562
	public LocText Label;

	// Token: 0x04004883 RID: 18563
	public ToolTip Tooltip;

	// Token: 0x04004884 RID: 18564
	public GameObject diagnosticGraph;

	// Token: 0x04004885 RID: 18565
	public TextStyleSetting ToolTipStyle_Header;

	// Token: 0x04004886 RID: 18566
	public TextStyleSetting ToolTipStyle_Property;

	// Token: 0x04004887 RID: 18567
	protected Func<List<MinionIdentity>, List<MinionIdentity>> minionListCustomSortOperation;

	// Token: 0x04004888 RID: 18568
	private List<MinionIdentity> worldLiveMinionIdentities;
}
