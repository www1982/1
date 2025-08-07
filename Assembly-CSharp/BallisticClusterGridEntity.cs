using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002E2 RID: 738
public class BallisticClusterGridEntity : ClusterGridEntity
{
	// Token: 0x1700003F RID: 63
	// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x00058897 File Offset: 0x00056A97
	public override string Name
	{
		get
		{
			return Strings.Get(this.nameKey);
		}
	}

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x000588A9 File Offset: 0x00056AA9
	public override EntityLayer Layer
	{
		get
		{
			return EntityLayer.Payload;
		}
	}

	// Token: 0x06000EF8 RID: 3832 RVA: 0x000588AC File Offset: 0x00056AAC
	public override bool KeepRotationWhenSpacingOutInHex()
	{
		return this.keepRotationWhenSpacingOutInHex;
	}

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x000588B4 File Offset: 0x00056AB4
	public override List<ClusterGridEntity.AnimConfig> AnimConfigs
	{
		get
		{
			return new List<ClusterGridEntity.AnimConfig>
			{
				new ClusterGridEntity.AnimConfig
				{
					animFile = Assets.GetAnim(this.clusterAnimName),
					initialAnim = "idle_loop",
					symbolSwapTarget = this.clusterAnimSymbolSwapTarget,
					symbolSwapSymbol = this.clusterAnimSymbolSwapSymbol
				}
			};
		}
	}

	// Token: 0x17000042 RID: 66
	// (get) Token: 0x06000EFA RID: 3834 RVA: 0x00058912 File Offset: 0x00056B12
	public override bool IsVisible
	{
		get
		{
			return !base.gameObject.HasTag(GameTags.ClusterEntityGrounded);
		}
	}

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x06000EFB RID: 3835 RVA: 0x00058927 File Offset: 0x00056B27
	public override ClusterRevealLevel IsVisibleInFOW
	{
		get
		{
			return ClusterRevealLevel.Visible;
		}
	}

	// Token: 0x06000EFC RID: 3836 RVA: 0x0005892A File Offset: 0x00056B2A
	public override bool SpaceOutInSameHex()
	{
		return true;
	}

	// Token: 0x06000EFD RID: 3837 RVA: 0x00058930 File Offset: 0x00056B30
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.m_clusterTraveler.getSpeedCB = new Func<float>(this.GetSpeed);
		this.m_clusterTraveler.getCanTravelCB = new Func<bool, bool>(this.CanTravel);
		this.m_clusterTraveler.onTravelCB = null;
	}

	// Token: 0x06000EFE RID: 3838 RVA: 0x0005897D File Offset: 0x00056B7D
	private float GetSpeed()
	{
		return 10f;
	}

	// Token: 0x06000EFF RID: 3839 RVA: 0x00058984 File Offset: 0x00056B84
	private bool CanTravel(bool tryingToLand)
	{
		return this.HasTag(GameTags.EntityInSpace);
	}

	// Token: 0x06000F00 RID: 3840 RVA: 0x00058991 File Offset: 0x00056B91
	public void Configure(AxialI source, AxialI destination)
	{
		this.m_location = source;
		this.m_destionationSelector.SetDestination(destination);
	}

	// Token: 0x06000F01 RID: 3841 RVA: 0x000589A6 File Offset: 0x00056BA6
	public override bool ShowPath()
	{
		return this.m_selectable.IsSelected;
	}

	// Token: 0x06000F02 RID: 3842 RVA: 0x000589B3 File Offset: 0x00056BB3
	public override bool ShowProgressBar()
	{
		return this.m_selectable.IsSelected && this.m_clusterTraveler.IsTraveling();
	}

	// Token: 0x06000F03 RID: 3843 RVA: 0x000589CF File Offset: 0x00056BCF
	public override float GetProgress()
	{
		return this.m_clusterTraveler.GetMoveProgress();
	}

	// Token: 0x06000F04 RID: 3844 RVA: 0x000589DC File Offset: 0x00056BDC
	public void SwapSymbolFromSameAnim(string targetSymbolName, string swappedSymbolName)
	{
		this.clusterAnimSymbolSwapTarget = targetSymbolName;
		this.clusterAnimSymbolSwapSymbol = swappedSymbolName;
	}

	// Token: 0x040009C4 RID: 2500
	[MyCmpReq]
	private ClusterDestinationSelector m_destionationSelector;

	// Token: 0x040009C5 RID: 2501
	[MyCmpReq]
	private ClusterTraveler m_clusterTraveler;

	// Token: 0x040009C6 RID: 2502
	[SerializeField]
	public string clusterAnimName;

	// Token: 0x040009C7 RID: 2503
	[SerializeField]
	public StringKey nameKey;

	// Token: 0x040009C8 RID: 2504
	private string clusterAnimSymbolSwapTarget;

	// Token: 0x040009C9 RID: 2505
	private string clusterAnimSymbolSwapSymbol;

	// Token: 0x040009CA RID: 2506
	public bool keepRotationWhenSpacingOutInHex;
}
