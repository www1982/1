using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000B2F RID: 2863
[SerializationConfig(MemberSerialization.OptIn)]
public class ClusterFXEntity : ClusterGridEntity
{
	// Token: 0x170005FB RID: 1531
	// (get) Token: 0x0600548C RID: 21644 RVA: 0x001EBB14 File Offset: 0x001E9D14
	public override string Name
	{
		get
		{
			return UI.SPACEDESTINATIONS.TELESCOPE_TARGET.NAME;
		}
	}

	// Token: 0x170005FC RID: 1532
	// (get) Token: 0x0600548D RID: 21645 RVA: 0x001EBB20 File Offset: 0x001E9D20
	public override EntityLayer Layer
	{
		get
		{
			return EntityLayer.FX;
		}
	}

	// Token: 0x170005FD RID: 1533
	// (get) Token: 0x0600548E RID: 21646 RVA: 0x001EBB24 File Offset: 0x001E9D24
	public override List<ClusterGridEntity.AnimConfig> AnimConfigs
	{
		get
		{
			return new List<ClusterGridEntity.AnimConfig>
			{
				new ClusterGridEntity.AnimConfig
				{
					animFile = Assets.GetAnim(this.kAnimName),
					initialAnim = this.animName,
					playMode = this.animPlayMode,
					animOffset = this.animOffset
				}
			};
		}
	}

	// Token: 0x170005FE RID: 1534
	// (get) Token: 0x0600548F RID: 21647 RVA: 0x001EBB83 File Offset: 0x001E9D83
	public override bool IsVisible
	{
		get
		{
			return true;
		}
	}

	// Token: 0x170005FF RID: 1535
	// (get) Token: 0x06005490 RID: 21648 RVA: 0x001EBB86 File Offset: 0x001E9D86
	public override ClusterRevealLevel IsVisibleInFOW
	{
		get
		{
			return ClusterRevealLevel.Visible;
		}
	}

	// Token: 0x06005491 RID: 21649 RVA: 0x001EBB89 File Offset: 0x001E9D89
	public void Init(AxialI location, Vector3 animOffset)
	{
		base.Location = location;
		this.animOffset = animOffset;
	}

	// Token: 0x040038DA RID: 14554
	[SerializeField]
	public string kAnimName;

	// Token: 0x040038DB RID: 14555
	[SerializeField]
	public string animName;

	// Token: 0x040038DC RID: 14556
	public KAnim.PlayMode animPlayMode = KAnim.PlayMode.Once;

	// Token: 0x040038DD RID: 14557
	public Vector3 animOffset;
}
