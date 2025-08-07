using System;
using System.Collections.Generic;
using KSerialization;

// Token: 0x02000B3F RID: 2879
[SerializationConfig(MemberSerialization.OptIn)]
public class HarvestablePOIClusterGridEntity : ClusterGridEntity
{
	// Token: 0x17000633 RID: 1587
	// (get) Token: 0x060055A8 RID: 21928 RVA: 0x001F1C73 File Offset: 0x001EFE73
	public override string Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x17000634 RID: 1588
	// (get) Token: 0x060055A9 RID: 21929 RVA: 0x001F1C7B File Offset: 0x001EFE7B
	public override EntityLayer Layer
	{
		get
		{
			return EntityLayer.POI;
		}
	}

	// Token: 0x17000635 RID: 1589
	// (get) Token: 0x060055AA RID: 21930 RVA: 0x001F1C80 File Offset: 0x001EFE80
	public override List<ClusterGridEntity.AnimConfig> AnimConfigs
	{
		get
		{
			return new List<ClusterGridEntity.AnimConfig>
			{
				new ClusterGridEntity.AnimConfig
				{
					animFile = Assets.GetAnim("harvestable_space_poi_kanim"),
					initialAnim = (this.m_Anim.IsNullOrWhiteSpace() ? "cloud" : this.m_Anim)
				}
			};
		}
	}

	// Token: 0x17000636 RID: 1590
	// (get) Token: 0x060055AB RID: 21931 RVA: 0x001F1CD8 File Offset: 0x001EFED8
	public override bool IsVisible
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000637 RID: 1591
	// (get) Token: 0x060055AC RID: 21932 RVA: 0x001F1CDB File Offset: 0x001EFEDB
	public override ClusterRevealLevel IsVisibleInFOW
	{
		get
		{
			return ClusterRevealLevel.Peeked;
		}
	}

	// Token: 0x060055AD RID: 21933 RVA: 0x001F1CDE File Offset: 0x001EFEDE
	public void Init(AxialI location)
	{
		base.Location = location;
	}

	// Token: 0x04003934 RID: 14644
	public string m_name;

	// Token: 0x04003935 RID: 14645
	public string m_Anim;
}
