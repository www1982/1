using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;

// Token: 0x02000B5C RID: 2908
[SerializationConfig(MemberSerialization.OptIn)]
public class ResearchDestination : ClusterGridEntity
{
	// Token: 0x17000653 RID: 1619
	// (get) Token: 0x060056AB RID: 22187 RVA: 0x001F65B9 File Offset: 0x001F47B9
	public override string Name
	{
		get
		{
			return UI.SPACEDESTINATIONS.RESEARCHDESTINATION.NAME;
		}
	}

	// Token: 0x17000654 RID: 1620
	// (get) Token: 0x060056AC RID: 22188 RVA: 0x001F65C5 File Offset: 0x001F47C5
	public override EntityLayer Layer
	{
		get
		{
			return EntityLayer.POI;
		}
	}

	// Token: 0x17000655 RID: 1621
	// (get) Token: 0x060056AD RID: 22189 RVA: 0x001F65C8 File Offset: 0x001F47C8
	public override List<ClusterGridEntity.AnimConfig> AnimConfigs
	{
		get
		{
			return new List<ClusterGridEntity.AnimConfig>();
		}
	}

	// Token: 0x17000656 RID: 1622
	// (get) Token: 0x060056AE RID: 22190 RVA: 0x001F65CF File Offset: 0x001F47CF
	public override bool IsVisible
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000657 RID: 1623
	// (get) Token: 0x060056AF RID: 22191 RVA: 0x001F65D2 File Offset: 0x001F47D2
	public override ClusterRevealLevel IsVisibleInFOW
	{
		get
		{
			return ClusterRevealLevel.Peeked;
		}
	}

	// Token: 0x060056B0 RID: 22192 RVA: 0x001F65D5 File Offset: 0x001F47D5
	public void Init(AxialI location)
	{
		this.m_location = location;
	}
}
