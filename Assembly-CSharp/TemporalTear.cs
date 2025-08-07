using System;
using System.Collections.Generic;
using KSerialization;

// Token: 0x02000B6E RID: 2926
[SerializationConfig(MemberSerialization.OptIn)]
public class TemporalTear : ClusterGridEntity
{
	// Token: 0x17000666 RID: 1638
	// (get) Token: 0x06005764 RID: 22372 RVA: 0x001FAE4C File Offset: 0x001F904C
	public override string Name
	{
		get
		{
			return Db.Get().SpaceDestinationTypes.Wormhole.typeName;
		}
	}

	// Token: 0x17000667 RID: 1639
	// (get) Token: 0x06005765 RID: 22373 RVA: 0x001FAE62 File Offset: 0x001F9062
	public override EntityLayer Layer
	{
		get
		{
			return EntityLayer.POI;
		}
	}

	// Token: 0x17000668 RID: 1640
	// (get) Token: 0x06005766 RID: 22374 RVA: 0x001FAE68 File Offset: 0x001F9068
	public override List<ClusterGridEntity.AnimConfig> AnimConfigs
	{
		get
		{
			return new List<ClusterGridEntity.AnimConfig>
			{
				new ClusterGridEntity.AnimConfig
				{
					animFile = Assets.GetAnim("temporal_tear_kanim"),
					initialAnim = "closed_loop"
				}
			};
		}
	}

	// Token: 0x17000669 RID: 1641
	// (get) Token: 0x06005767 RID: 22375 RVA: 0x001FAEAB File Offset: 0x001F90AB
	public override bool IsVisible
	{
		get
		{
			return true;
		}
	}

	// Token: 0x1700066A RID: 1642
	// (get) Token: 0x06005768 RID: 22376 RVA: 0x001FAEAE File Offset: 0x001F90AE
	public override ClusterRevealLevel IsVisibleInFOW
	{
		get
		{
			return ClusterRevealLevel.Peeked;
		}
	}

	// Token: 0x06005769 RID: 22377 RVA: 0x001FAEB1 File Offset: 0x001F90B1
	protected override void OnSpawn()
	{
		base.OnSpawn();
		ClusterManager.Instance.GetComponent<ClusterPOIManager>().RegisterTemporalTear(this);
		this.UpdateStatus();
	}

	// Token: 0x0600576A RID: 22378 RVA: 0x001FAED0 File Offset: 0x001F90D0
	public void UpdateStatus()
	{
		KSelectable component = base.GetComponent<KSelectable>();
		ClusterMapVisualizer clusterMapVisualizer = null;
		if (ClusterMapScreen.Instance != null)
		{
			clusterMapVisualizer = ClusterMapScreen.Instance.GetEntityVisAnim(this);
		}
		if (this.IsOpen())
		{
			if (clusterMapVisualizer != null)
			{
				clusterMapVisualizer.PlayAnim("open_loop", KAnim.PlayMode.Loop);
			}
			component.RemoveStatusItem(Db.Get().MiscStatusItems.TearClosed, false);
			component.AddStatusItem(Db.Get().MiscStatusItems.TearOpen, null);
			return;
		}
		if (clusterMapVisualizer != null)
		{
			clusterMapVisualizer.PlayAnim("closed_loop", KAnim.PlayMode.Loop);
		}
		component.RemoveStatusItem(Db.Get().MiscStatusItems.TearOpen, false);
		base.GetComponent<KSelectable>().AddStatusItem(Db.Get().MiscStatusItems.TearClosed, null);
	}

	// Token: 0x0600576B RID: 22379 RVA: 0x001FAF93 File Offset: 0x001F9193
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x0600576C RID: 22380 RVA: 0x001FAF9C File Offset: 0x001F919C
	public void ConsumeCraft(Clustercraft craft)
	{
		if (this.m_open && craft.Location == base.Location && !craft.IsFlightInProgress())
		{
			for (int i = 0; i < Components.MinionIdentities.Count; i++)
			{
				MinionIdentity minionIdentity = Components.MinionIdentities[i];
				if (minionIdentity != null && minionIdentity.GetMyWorldId() == craft.ModuleInterface.GetInteriorWorld().id)
				{
					Util.KDestroyGameObject(minionIdentity.gameObject);
				}
			}
			craft.DestroyCraftAndModules();
			this.m_hasConsumedCraft = true;
		}
	}

	// Token: 0x0600576D RID: 22381 RVA: 0x001FB026 File Offset: 0x001F9226
	public void Open()
	{
		this.m_open = true;
		this.UpdateStatus();
	}

	// Token: 0x0600576E RID: 22382 RVA: 0x001FB035 File Offset: 0x001F9235
	public bool IsOpen()
	{
		return this.m_open;
	}

	// Token: 0x0600576F RID: 22383 RVA: 0x001FB03D File Offset: 0x001F923D
	public bool HasConsumedCraft()
	{
		return this.m_hasConsumedCraft;
	}

	// Token: 0x04003A60 RID: 14944
	[Serialize]
	private bool m_open;

	// Token: 0x04003A61 RID: 14945
	[Serialize]
	private bool m_hasConsumedCraft;
}
