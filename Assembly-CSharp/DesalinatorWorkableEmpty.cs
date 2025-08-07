using System;
using KSerialization;
using TUNING;
using UnityEngine;

// Token: 0x0200070D RID: 1805
[AddComponentMenu("KMonoBehaviour/Workable/DesalinatorWorkableEmpty")]
public class DesalinatorWorkableEmpty : Workable
{
	// Token: 0x06002D4B RID: 11595 RVA: 0x00103CD4 File Offset: 0x00101ED4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Cleaning;
		this.workingStatusItem = Db.Get().MiscStatusItems.Cleaning;
		this.attributeConverter = Db.Get().AttributeConverters.TidyingSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_desalinator_kanim") };
		this.workAnims = DesalinatorWorkableEmpty.WORK_ANIMS;
		this.workingPstComplete = new HashedString[] { DesalinatorWorkableEmpty.PST_ANIM };
		this.workingPstFailed = new HashedString[] { DesalinatorWorkableEmpty.PST_ANIM };
		this.synchronizeAnims = false;
	}

	// Token: 0x06002D4C RID: 11596 RVA: 0x00103D91 File Offset: 0x00101F91
	protected override void OnCompleteWork(WorkerBase worker)
	{
		this.timesCleaned++;
		base.OnCompleteWork(worker);
	}

	// Token: 0x04001AAB RID: 6827
	[Serialize]
	public int timesCleaned;

	// Token: 0x04001AAC RID: 6828
	private static readonly HashedString[] WORK_ANIMS = new HashedString[] { "salt_pre", "salt_loop" };

	// Token: 0x04001AAD RID: 6829
	private static readonly HashedString PST_ANIM = new HashedString("salt_pst");
}
