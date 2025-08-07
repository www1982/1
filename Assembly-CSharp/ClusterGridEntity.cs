using System;
using System.Collections.Generic;
using KSerialization;
using ProcGen;
using UnityEngine;

// Token: 0x02000B73 RID: 2931
public abstract class ClusterGridEntity : KMonoBehaviour
{
	// Token: 0x17000671 RID: 1649
	// (get) Token: 0x060057B2 RID: 22450
	public abstract string Name { get; }

	// Token: 0x17000672 RID: 1650
	// (get) Token: 0x060057B3 RID: 22451
	public abstract EntityLayer Layer { get; }

	// Token: 0x17000673 RID: 1651
	// (get) Token: 0x060057B4 RID: 22452
	public abstract List<ClusterGridEntity.AnimConfig> AnimConfigs { get; }

	// Token: 0x17000674 RID: 1652
	// (get) Token: 0x060057B5 RID: 22453
	public abstract bool IsVisible { get; }

	// Token: 0x060057B6 RID: 22454 RVA: 0x001FC764 File Offset: 0x001FA964
	public virtual bool ShowName()
	{
		return false;
	}

	// Token: 0x060057B7 RID: 22455 RVA: 0x001FC767 File Offset: 0x001FA967
	public virtual bool ShowProgressBar()
	{
		return false;
	}

	// Token: 0x060057B8 RID: 22456 RVA: 0x001FC76A File Offset: 0x001FA96A
	public virtual float GetProgress()
	{
		return 0f;
	}

	// Token: 0x060057B9 RID: 22457 RVA: 0x001FC771 File Offset: 0x001FA971
	public virtual bool SpaceOutInSameHex()
	{
		return false;
	}

	// Token: 0x060057BA RID: 22458 RVA: 0x001FC774 File Offset: 0x001FA974
	public virtual bool KeepRotationWhenSpacingOutInHex()
	{
		return false;
	}

	// Token: 0x060057BB RID: 22459 RVA: 0x001FC777 File Offset: 0x001FA977
	public virtual bool ShowPath()
	{
		return true;
	}

	// Token: 0x060057BC RID: 22460 RVA: 0x001FC77A File Offset: 0x001FA97A
	public virtual void OnClusterMapIconShown(ClusterRevealLevel levelUsed)
	{
	}

	// Token: 0x17000675 RID: 1653
	// (get) Token: 0x060057BD RID: 22461
	public abstract ClusterRevealLevel IsVisibleInFOW { get; }

	// Token: 0x17000676 RID: 1654
	// (get) Token: 0x060057BE RID: 22462 RVA: 0x001FC77C File Offset: 0x001FA97C
	// (set) Token: 0x060057BF RID: 22463 RVA: 0x001FC784 File Offset: 0x001FA984
	public AxialI Location
	{
		get
		{
			return this.m_location;
		}
		set
		{
			if (value != this.m_location)
			{
				AxialI location = this.m_location;
				this.m_location = value;
				if (base.gameObject.GetSMI<StateMachine.Instance>() == null)
				{
					this.positionDirty = true;
				}
				this.SendClusterLocationChangedEvent(location, this.m_location);
			}
		}
	}

	// Token: 0x060057C0 RID: 22464 RVA: 0x001FC7D0 File Offset: 0x001FA9D0
	protected override void OnSpawn()
	{
		ClusterGrid.Instance.RegisterEntity(this);
		if (this.m_selectable != null)
		{
			this.m_selectable.SetName(this.Name);
		}
		if (!this.isWorldEntity)
		{
			this.m_transform.SetLocalPosition(new Vector3(-1f, 0f, 0f));
		}
		if (ClusterMapScreen.Instance != null)
		{
			ClusterMapScreen.Instance.Trigger(1980521255, null);
		}
	}

	// Token: 0x060057C1 RID: 22465 RVA: 0x001FC84C File Offset: 0x001FAA4C
	protected override void OnCleanUp()
	{
		ClusterGrid.Instance.UnregisterEntity(this);
	}

	// Token: 0x060057C2 RID: 22466 RVA: 0x001FC85C File Offset: 0x001FAA5C
	public virtual Sprite GetUISprite()
	{
		if (DlcManager.FeatureClusterSpaceEnabled())
		{
			List<ClusterGridEntity.AnimConfig> animConfigs = this.AnimConfigs;
			if (animConfigs.Count > 0)
			{
				return Def.GetUISpriteFromMultiObjectAnim(animConfigs[0].animFile, "ui", false, "");
			}
		}
		else
		{
			WorldContainer component = base.GetComponent<WorldContainer>();
			if (component != null)
			{
				global::ProcGen.World worldData = SettingsCache.worlds.GetWorldData(component.worldName);
				if (worldData == null)
				{
					return null;
				}
				return Assets.GetSprite(worldData.asteroidIcon);
			}
		}
		return null;
	}

	// Token: 0x060057C3 RID: 22467 RVA: 0x001FC8D8 File Offset: 0x001FAAD8
	public void SendClusterLocationChangedEvent(AxialI oldLocation, AxialI newLocation)
	{
		ClusterLocationChangedEvent clusterLocationChangedEvent = new ClusterLocationChangedEvent
		{
			entity = this,
			oldLocation = oldLocation,
			newLocation = newLocation
		};
		base.Trigger(-1298331547, clusterLocationChangedEvent);
		Game.Instance.Trigger(-1298331547, clusterLocationChangedEvent);
		if (this.m_selectable != null && this.m_selectable.IsSelected)
		{
			DetailsScreen.Instance.Refresh(base.gameObject);
		}
	}

	// Token: 0x04003A7E RID: 14974
	[Serialize]
	protected AxialI m_location;

	// Token: 0x04003A7F RID: 14975
	public bool positionDirty;

	// Token: 0x04003A80 RID: 14976
	[MyCmpGet]
	protected KSelectable m_selectable;

	// Token: 0x04003A81 RID: 14977
	[MyCmpReq]
	private Transform m_transform;

	// Token: 0x04003A82 RID: 14978
	public bool isWorldEntity;

	// Token: 0x02001CA8 RID: 7336
	public struct AnimConfig
	{
		// Token: 0x040086F4 RID: 34548
		public KAnimFile animFile;

		// Token: 0x040086F5 RID: 34549
		public string initialAnim;

		// Token: 0x040086F6 RID: 34550
		public KAnim.PlayMode playMode;

		// Token: 0x040086F7 RID: 34551
		public string symbolSwapTarget;

		// Token: 0x040086F8 RID: 34552
		public string symbolSwapSymbol;

		// Token: 0x040086F9 RID: 34553
		public Vector3 animOffset;

		// Token: 0x040086FA RID: 34554
		public float animPlaySpeedModifier;
	}
}
