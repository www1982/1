using System;
using KSerialization;
using STRINGS;

// Token: 0x02000B2E RID: 2862
public class ClusterDestinationSelector : KMonoBehaviour
{
	// Token: 0x06005483 RID: 21635 RVA: 0x001EB9D9 File Offset: 0x001E9BD9
	protected override void OnPrefabInit()
	{
		base.Subscribe<ClusterDestinationSelector>(-1298331547, this.OnClusterLocationChangedDelegate);
	}

	// Token: 0x06005484 RID: 21636 RVA: 0x001EB9ED File Offset: 0x001E9BED
	protected virtual void OnClusterLocationChanged(object data)
	{
		if (((ClusterLocationChangedEvent)data).newLocation == this.m_destination)
		{
			base.Trigger(1796608350, data);
		}
	}

	// Token: 0x06005485 RID: 21637 RVA: 0x001EBA13 File Offset: 0x001E9C13
	public int GetDestinationWorld()
	{
		return ClusterUtil.GetAsteroidWorldIdAtLocation(this.m_destination);
	}

	// Token: 0x06005486 RID: 21638 RVA: 0x001EBA20 File Offset: 0x001E9C20
	public virtual AxialI GetDestination()
	{
		return this.m_destination;
	}

	// Token: 0x06005487 RID: 21639 RVA: 0x001EBA28 File Offset: 0x001E9C28
	public virtual ClusterGridEntity GetClusterEntityTarget()
	{
		return null;
	}

	// Token: 0x06005488 RID: 21640 RVA: 0x001EBA2C File Offset: 0x001E9C2C
	public virtual void SetDestination(AxialI location)
	{
		if (this.requireAsteroidDestination)
		{
			Debug.Assert(ClusterUtil.GetAsteroidWorldIdAtLocation(location) != -1, string.Format("Cannot SetDestination to {0} as there is no world there", location));
		}
		this.m_destination = location;
		base.Trigger(543433792, location);
	}

	// Token: 0x06005489 RID: 21641 RVA: 0x001EBA7A File Offset: 0x001E9C7A
	public bool HasAsteroidDestination()
	{
		return ClusterUtil.GetAsteroidWorldIdAtLocation(this.m_destination) != -1;
	}

	// Token: 0x0600548A RID: 21642 RVA: 0x001EBA8D File Offset: 0x001E9C8D
	public virtual bool IsAtDestination()
	{
		return this.GetMyWorldLocation() == this.m_destination;
	}

	// Token: 0x040038CE RID: 14542
	[Serialize]
	protected AxialI m_destination;

	// Token: 0x040038CF RID: 14543
	public bool assignable;

	// Token: 0x040038D0 RID: 14544
	public bool requireAsteroidDestination;

	// Token: 0x040038D1 RID: 14545
	[Serialize]
	public bool canNavigateFogOfWar;

	// Token: 0x040038D2 RID: 14546
	public bool dodgesHiddenAsteroids;

	// Token: 0x040038D3 RID: 14547
	public bool requireLaunchPadOnAsteroidDestination;

	// Token: 0x040038D4 RID: 14548
	public bool shouldPointTowardsPath;

	// Token: 0x040038D5 RID: 14549
	public string sidescreenTitleString = UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.TITLE;

	// Token: 0x040038D6 RID: 14550
	public string changeTargetButtonTooltipString = UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.CHANGE_DESTINATION_BUTTON_TOOLTIP;

	// Token: 0x040038D7 RID: 14551
	public string clearTargetButtonTooltipString = UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.CLEAR_DESTINATION_BUTTON_TOOLTIP;

	// Token: 0x040038D8 RID: 14552
	public EntityLayer requiredEntityLayer = EntityLayer.None;

	// Token: 0x040038D9 RID: 14553
	private EventSystem.IntraObjectHandler<ClusterDestinationSelector> OnClusterLocationChangedDelegate = new EventSystem.IntraObjectHandler<ClusterDestinationSelector>(delegate(ClusterDestinationSelector cmp, object data)
	{
		cmp.OnClusterLocationChanged(data);
	});
}
