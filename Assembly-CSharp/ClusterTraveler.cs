using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000B36 RID: 2870
public class ClusterTraveler : KMonoBehaviour, ISim200ms
{
	// Token: 0x17000608 RID: 1544
	// (get) Token: 0x060054BF RID: 21695 RVA: 0x001ECA14 File Offset: 0x001EAC14
	public List<AxialI> CurrentPath
	{
		get
		{
			if (this.m_cachedPath == null || this.m_destinationSelector.GetDestination() != this.m_cachedPathDestination)
			{
				this.m_cachedPathDestination = this.m_destinationSelector.GetDestination();
				this.m_cachedPath = ClusterGrid.Instance.GetPath(this.m_clusterGridEntity.Location, this.m_cachedPathDestination, this.m_destinationSelector);
			}
			return this.m_cachedPath;
		}
	}

	// Token: 0x17000609 RID: 1545
	// (get) Token: 0x060054C0 RID: 21696 RVA: 0x001ECA7F File Offset: 0x001EAC7F
	public AxialI CurrentLocation
	{
		get
		{
			return this.m_clusterGridEntity.Location;
		}
	}

	// Token: 0x1700060A RID: 1546
	// (get) Token: 0x060054C1 RID: 21697 RVA: 0x001ECA8C File Offset: 0x001EAC8C
	public AxialI Destination
	{
		get
		{
			List<AxialI> currentPath = this.CurrentPath;
			if (currentPath.Count == 0)
			{
				return this.CurrentLocation;
			}
			return currentPath[currentPath.Count - 1];
		}
	}

	// Token: 0x060054C2 RID: 21698 RVA: 0x001ECABD File Offset: 0x001EACBD
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Components.ClusterTravelers.Add(this);
	}

	// Token: 0x060054C3 RID: 21699 RVA: 0x001ECAD0 File Offset: 0x001EACD0
	protected override void OnCleanUp()
	{
		Components.ClusterTravelers.Remove(this);
		Game.Instance.Unsubscribe(-1991583975, new Action<object>(this.OnClusterFogOfWarRevealed));
		base.OnCleanUp();
	}

	// Token: 0x060054C4 RID: 21700 RVA: 0x001ECAFE File Offset: 0x001EACFE
	private void ForceRevealLocation(AxialI location)
	{
		if (!ClusterGrid.Instance.IsCellVisible(location))
		{
			SaveGame.Instance.GetSMI<ClusterFogOfWarManager.Instance>().RevealLocation(location, 0, this.peekRadius);
		}
	}

	// Token: 0x060054C5 RID: 21701 RVA: 0x001ECB24 File Offset: 0x001EAD24
	protected override void OnSpawn()
	{
		base.Subscribe<ClusterTraveler>(543433792, ClusterTraveler.ClusterDestinationChangedHandler);
		Game.Instance.Subscribe(-1991583975, new Action<object>(this.OnClusterFogOfWarRevealed));
		this.UpdateAnimationTags();
		this.MarkPathDirty();
		this.RevalidatePath(false);
		if (this.revealsFogOfWarAsItTravels)
		{
			this.ForceRevealLocation(this.m_clusterGridEntity.Location);
		}
	}

	// Token: 0x060054C6 RID: 21702 RVA: 0x001ECB8A File Offset: 0x001EAD8A
	private void MarkPathDirty()
	{
		this.m_isPathDirty = true;
	}

	// Token: 0x060054C7 RID: 21703 RVA: 0x001ECB93 File Offset: 0x001EAD93
	private void OnClusterFogOfWarRevealed(object data)
	{
		this.MarkPathDirty();
	}

	// Token: 0x060054C8 RID: 21704 RVA: 0x001ECB9B File Offset: 0x001EAD9B
	private void OnClusterDestinationChanged(object data)
	{
		if (this.m_destinationSelector.IsAtDestination())
		{
			this.m_movePotential = 0f;
			if (this.CurrentPath != null)
			{
				this.CurrentPath.Clear();
			}
		}
		this.MarkPathDirty();
	}

	// Token: 0x060054C9 RID: 21705 RVA: 0x001ECBCE File Offset: 0x001EADCE
	public int GetDestinationWorldID()
	{
		return this.m_destinationSelector.GetDestinationWorld();
	}

	// Token: 0x060054CA RID: 21706 RVA: 0x001ECBDB File Offset: 0x001EADDB
	public float EstimatedTimeToReachDestination()
	{
		if (this.CurrentPath == null || this.getSpeedCB == null)
		{
			return 0f;
		}
		return this.TravelETA(this.m_cachedPathDestination);
	}

	// Token: 0x060054CB RID: 21707 RVA: 0x001ECC00 File Offset: 0x001EAE00
	public float TravelETA(AxialI location)
	{
		if (this.CurrentPath == null || this.getSpeedCB == null)
		{
			return 0f;
		}
		int num = this.CurrentPath.IndexOf(location);
		if (num == -1)
		{
			return 0f;
		}
		return Mathf.Max(0f, (float)(num + 1) * 600f - this.m_movePotential) / this.getSpeedCB();
	}

	// Token: 0x060054CC RID: 21708 RVA: 0x001ECC61 File Offset: 0x001EAE61
	public float TravelETA()
	{
		if (!this.IsTraveling() || this.getSpeedCB == null)
		{
			return 0f;
		}
		return this.RemainingTravelDistance() / this.getSpeedCB();
	}

	// Token: 0x060054CD RID: 21709 RVA: 0x001ECC8C File Offset: 0x001EAE8C
	public float RemainingTravelDistance()
	{
		int num = this.RemainingTravelNodes();
		if (this.GetDestinationWorldID() >= 0)
		{
			num--;
			num = Mathf.Max(num, 0);
		}
		return (float)num * 600f - this.m_movePotential;
	}

	// Token: 0x060054CE RID: 21710 RVA: 0x001ECCC4 File Offset: 0x001EAEC4
	public int RemainingTravelNodes()
	{
		if (this.CurrentPath == null)
		{
			return 0;
		}
		int count = this.CurrentPath.Count;
		return Mathf.Max(0, count);
	}

	// Token: 0x060054CF RID: 21711 RVA: 0x001ECCEE File Offset: 0x001EAEEE
	public float GetMoveProgress()
	{
		return this.m_movePotential / 600f;
	}

	// Token: 0x060054D0 RID: 21712 RVA: 0x001ECCFC File Offset: 0x001EAEFC
	public bool IsTraveling()
	{
		return !this.m_destinationSelector.IsAtDestination();
	}

	// Token: 0x060054D1 RID: 21713 RVA: 0x001ECD0C File Offset: 0x001EAF0C
	public void Sim200ms(float dt)
	{
		if (!this.IsTraveling())
		{
			return;
		}
		bool flag = this.CurrentPath != null && this.CurrentPath.Count > 0;
		bool flag2 = this.m_destinationSelector.HasAsteroidDestination();
		bool flag3 = flag2 && flag && this.CurrentPath.Count == 1;
		if (this.getCanTravelCB != null && !this.getCanTravelCB(flag3))
		{
			return;
		}
		AxialI location = this.m_clusterGridEntity.Location;
		if (flag)
		{
			if (flag2)
			{
				bool requireLaunchPadOnAsteroidDestination = this.m_destinationSelector.requireLaunchPadOnAsteroidDestination;
			}
			if (!flag2 || this.CurrentPath.Count > 1 || !this.quickTravelToAsteroidIfInOrbit)
			{
				float num = dt * this.getSpeedCB();
				this.m_movePotential += num;
				if (this.m_movePotential >= 600f)
				{
					this.m_movePotential = 0f;
					if (this.AdvancePathOneStep())
					{
						global::Debug.Assert(ClusterGrid.Instance.GetVisibleEntityOfLayerAtCell(this.m_clusterGridEntity.Location, EntityLayer.Asteroid) == null || (flag2 && this.CurrentPath.Count == 0), string.Format("Somehow this clustercraft pathed through an asteroid at {0}", this.m_clusterGridEntity.Location));
						if (this.onTravelCB != null)
						{
							this.onTravelCB();
						}
					}
					else
					{
						this.m_movePotential = 600f;
					}
				}
			}
			else
			{
				this.AdvancePathOneStep();
			}
		}
		this.RevalidatePath(true);
	}

	// Token: 0x060054D2 RID: 21714 RVA: 0x001ECE7C File Offset: 0x001EB07C
	public bool AdvancePathOneStep()
	{
		if (this.validateTravelCB != null && !this.validateTravelCB(this.CurrentPath[0]))
		{
			return false;
		}
		AxialI axialI = this.CurrentPath[0];
		this.CurrentPath.RemoveAt(0);
		if (this.revealsFogOfWarAsItTravels)
		{
			this.ForceRevealLocation(axialI);
		}
		this.m_clusterGridEntity.Location = axialI;
		this.UpdateAnimationTags();
		return true;
	}

	// Token: 0x060054D3 RID: 21715 RVA: 0x001ECEE8 File Offset: 0x001EB0E8
	private void UpdateAnimationTags()
	{
		if (this.CurrentPath == null)
		{
			this.m_clusterGridEntity.RemoveTag(GameTags.BallisticEntityLaunching);
			this.m_clusterGridEntity.RemoveTag(GameTags.BallisticEntityLanding);
			this.m_clusterGridEntity.RemoveTag(GameTags.BallisticEntityMoving);
			return;
		}
		if (!(ClusterGrid.Instance.GetAsteroidAtCell(this.m_clusterGridEntity.Location) != null))
		{
			this.m_clusterGridEntity.AddTag(GameTags.BallisticEntityMoving);
			this.m_clusterGridEntity.RemoveTag(GameTags.BallisticEntityLanding);
			this.m_clusterGridEntity.RemoveTag(GameTags.BallisticEntityLaunching);
			return;
		}
		if (this.CurrentPath.Count == 0 || this.m_clusterGridEntity.Location == this.CurrentPath[this.CurrentPath.Count - 1])
		{
			this.m_clusterGridEntity.AddTag(GameTags.BallisticEntityLanding);
			this.m_clusterGridEntity.RemoveTag(GameTags.BallisticEntityLaunching);
			this.m_clusterGridEntity.RemoveTag(GameTags.BallisticEntityMoving);
			return;
		}
		this.m_clusterGridEntity.AddTag(GameTags.BallisticEntityLaunching);
		this.m_clusterGridEntity.RemoveTag(GameTags.BallisticEntityLanding);
		this.m_clusterGridEntity.RemoveTag(GameTags.BallisticEntityMoving);
	}

	// Token: 0x060054D4 RID: 21716 RVA: 0x001ED018 File Offset: 0x001EB218
	public void RevalidatePath(bool react_to_change = true)
	{
		string reason;
		List<AxialI> list;
		if (this.HasCurrentPathChanged(out reason, out list))
		{
			if (this.stopAndNotifyWhenPathChanges && react_to_change)
			{
				this.m_destinationSelector.SetDestination(this.m_destinationSelector.GetMyWorldLocation());
				string message = MISC.NOTIFICATIONS.BADROCKETPATH.TOOLTIP;
				Notification notification = new Notification(MISC.NOTIFICATIONS.BADROCKETPATH.NAME, NotificationType.BadMinor, (List<Notification> notificationList, object data) => message + notificationList.ReduceMessages(false) + "\n\n" + reason, null, true, 0f, null, null, null, true, false, false);
				base.GetComponent<Notifier>().Add(notification, "");
				return;
			}
			this.m_cachedPath = list;
		}
	}

	// Token: 0x060054D5 RID: 21717 RVA: 0x001ED0B0 File Offset: 0x001EB2B0
	private bool HasCurrentPathChanged(out string reason, out List<AxialI> updatedPath)
	{
		if (!this.m_isPathDirty)
		{
			reason = null;
			updatedPath = null;
			return false;
		}
		this.m_isPathDirty = false;
		updatedPath = ClusterGrid.Instance.GetPath(this.m_clusterGridEntity.Location, this.m_cachedPathDestination, this.m_destinationSelector, out reason, this.m_destinationSelector.dodgesHiddenAsteroids);
		if (updatedPath == null)
		{
			return true;
		}
		if (updatedPath.Count != this.m_cachedPath.Count)
		{
			return true;
		}
		for (int i = 0; i < this.m_cachedPath.Count; i++)
		{
			if (this.m_cachedPath[i] != updatedPath[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060054D6 RID: 21718 RVA: 0x001ED153 File Offset: 0x001EB353
	[ContextMenu("Fill Move Potential")]
	public void FillMovePotential()
	{
		this.m_movePotential = 600f;
	}

	// Token: 0x040038F2 RID: 14578
	[MyCmpReq]
	private ClusterDestinationSelector m_destinationSelector;

	// Token: 0x040038F3 RID: 14579
	[MyCmpReq]
	private ClusterGridEntity m_clusterGridEntity;

	// Token: 0x040038F4 RID: 14580
	[Serialize]
	private float m_movePotential;

	// Token: 0x040038F5 RID: 14581
	public Func<float> getSpeedCB;

	// Token: 0x040038F6 RID: 14582
	public Func<bool, bool> getCanTravelCB;

	// Token: 0x040038F7 RID: 14583
	public Func<AxialI, bool> validateTravelCB;

	// Token: 0x040038F8 RID: 14584
	public global::System.Action onTravelCB;

	// Token: 0x040038F9 RID: 14585
	private AxialI m_cachedPathDestination;

	// Token: 0x040038FA RID: 14586
	private List<AxialI> m_cachedPath;

	// Token: 0x040038FB RID: 14587
	private bool m_isPathDirty;

	// Token: 0x040038FC RID: 14588
	public bool revealsFogOfWarAsItTravels = true;

	// Token: 0x040038FD RID: 14589
	public bool quickTravelToAsteroidIfInOrbit = true;

	// Token: 0x040038FE RID: 14590
	public int peekRadius = 2;

	// Token: 0x040038FF RID: 14591
	public bool stopAndNotifyWhenPathChanges;

	// Token: 0x04003900 RID: 14592
	private static EventSystem.IntraObjectHandler<ClusterTraveler> ClusterDestinationChangedHandler = new EventSystem.IntraObjectHandler<ClusterTraveler>(delegate(ClusterTraveler cmp, object data)
	{
		cmp.OnClusterDestinationChanged(data);
	});

	// Token: 0x02001C50 RID: 7248
	public class BackgroundMotion : ParallaxBackgroundObject.IMotion
	{
		// Token: 0x0600AA82 RID: 43650 RVA: 0x003BADFD File Offset: 0x003B8FFD
		public BackgroundMotion(ClusterTraveler traveler)
		{
			this.traveler = traveler;
		}

		// Token: 0x0600AA83 RID: 43651 RVA: 0x003BAE0C File Offset: 0x003B900C
		public float GetETA()
		{
			return this.traveler.TravelETA();
		}

		// Token: 0x0600AA84 RID: 43652 RVA: 0x003BAE1C File Offset: 0x003B901C
		public float GetDuration()
		{
			if (this.duration == null)
			{
				float eta = this.GetETA();
				if (eta == 0f)
				{
					return 0f;
				}
				this.duration = new float?(eta);
			}
			return this.duration.Value;
		}

		// Token: 0x0600AA85 RID: 43653 RVA: 0x003BAE62 File Offset: 0x003B9062
		public void OnNormalizedDistanceChanged(float normalizedDistance)
		{
		}

		// Token: 0x040085EB RID: 34283
		private readonly ClusterTraveler traveler;

		// Token: 0x040085EC RID: 34284
		private float? duration;
	}
}
