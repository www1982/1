using System;
using System.Collections.Generic;
using KSerialization;

// Token: 0x02000B26 RID: 2854
[SerializationConfig(MemberSerialization.OptIn)]
public class ArtifactPOIClusterGridEntity : ClusterGridEntity
{
	// Token: 0x170005EE RID: 1518
	// (get) Token: 0x0600543E RID: 21566 RVA: 0x001EA465 File Offset: 0x001E8665
	public override string Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x170005EF RID: 1519
	// (get) Token: 0x0600543F RID: 21567 RVA: 0x001EA46D File Offset: 0x001E866D
	public override EntityLayer Layer
	{
		get
		{
			return EntityLayer.POI;
		}
	}

	// Token: 0x170005F0 RID: 1520
	// (get) Token: 0x06005440 RID: 21568 RVA: 0x001EA470 File Offset: 0x001E8670
	public override List<ClusterGridEntity.AnimConfig> AnimConfigs
	{
		get
		{
			return new List<ClusterGridEntity.AnimConfig>
			{
				new ClusterGridEntity.AnimConfig
				{
					animFile = Assets.GetAnim("gravitas_space_poi_kanim"),
					initialAnim = (this.m_Anim.IsNullOrWhiteSpace() ? "station_1" : this.m_Anim)
				}
			};
		}
	}

	// Token: 0x170005F1 RID: 1521
	// (get) Token: 0x06005441 RID: 21569 RVA: 0x001EA4C8 File Offset: 0x001E86C8
	public override bool IsVisible
	{
		get
		{
			return true;
		}
	}

	// Token: 0x170005F2 RID: 1522
	// (get) Token: 0x06005442 RID: 21570 RVA: 0x001EA4CB File Offset: 0x001E86CB
	public override ClusterRevealLevel IsVisibleInFOW
	{
		get
		{
			return ClusterRevealLevel.Peeked;
		}
	}

	// Token: 0x06005443 RID: 21571 RVA: 0x001EA4CE File Offset: 0x001E86CE
	public void Init(AxialI location)
	{
		base.Location = location;
	}

	// Token: 0x040038AA RID: 14506
	public string m_name;

	// Token: 0x040038AB RID: 14507
	public string m_Anim;
}
