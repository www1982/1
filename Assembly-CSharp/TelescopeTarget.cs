using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;

// Token: 0x02000B6D RID: 2925
[SerializationConfig(MemberSerialization.OptIn)]
public class TelescopeTarget : ClusterGridEntity
{
	// Token: 0x17000661 RID: 1633
	// (get) Token: 0x06005759 RID: 22361 RVA: 0x001FADA6 File Offset: 0x001F8FA6
	public override string Name
	{
		get
		{
			return UI.SPACEDESTINATIONS.TELESCOPE_TARGET.NAME;
		}
	}

	// Token: 0x17000662 RID: 1634
	// (get) Token: 0x0600575A RID: 22362 RVA: 0x001FADB2 File Offset: 0x001F8FB2
	public override EntityLayer Layer
	{
		get
		{
			return EntityLayer.Telescope;
		}
	}

	// Token: 0x17000663 RID: 1635
	// (get) Token: 0x0600575B RID: 22363 RVA: 0x001FADB8 File Offset: 0x001F8FB8
	public override List<ClusterGridEntity.AnimConfig> AnimConfigs
	{
		get
		{
			return new List<ClusterGridEntity.AnimConfig>
			{
				new ClusterGridEntity.AnimConfig
				{
					animFile = Assets.GetAnim("telescope_target_kanim"),
					initialAnim = "idle"
				}
			};
		}
	}

	// Token: 0x17000664 RID: 1636
	// (get) Token: 0x0600575C RID: 22364 RVA: 0x001FADFB File Offset: 0x001F8FFB
	public override bool IsVisible
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000665 RID: 1637
	// (get) Token: 0x0600575D RID: 22365 RVA: 0x001FADFE File Offset: 0x001F8FFE
	public override ClusterRevealLevel IsVisibleInFOW
	{
		get
		{
			return ClusterRevealLevel.Visible;
		}
	}

	// Token: 0x0600575E RID: 22366 RVA: 0x001FAE01 File Offset: 0x001F9001
	public void Init(AxialI location)
	{
		base.Location = location;
	}

	// Token: 0x0600575F RID: 22367 RVA: 0x001FAE0A File Offset: 0x001F900A
	public void SetTargetMeteorShower(ClusterMapMeteorShower.Instance meteorShower)
	{
		this.targetMeteorShower = meteorShower;
	}

	// Token: 0x06005760 RID: 22368 RVA: 0x001FAE13 File Offset: 0x001F9013
	public override bool ShowName()
	{
		return true;
	}

	// Token: 0x06005761 RID: 22369 RVA: 0x001FAE16 File Offset: 0x001F9016
	public override bool ShowProgressBar()
	{
		return true;
	}

	// Token: 0x06005762 RID: 22370 RVA: 0x001FAE19 File Offset: 0x001F9019
	public override float GetProgress()
	{
		if (this.targetMeteorShower != null)
		{
			return this.targetMeteorShower.IdentifyingProgress;
		}
		return SaveGame.Instance.GetSMI<ClusterFogOfWarManager.Instance>().GetRevealCompleteFraction(base.Location);
	}

	// Token: 0x04003A5F RID: 14943
	private ClusterMapMeteorShower.Instance targetMeteorShower;
}
