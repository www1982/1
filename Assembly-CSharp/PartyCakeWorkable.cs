using System;
using TUNING;

// Token: 0x020007A5 RID: 1957
public class PartyCakeWorkable : Workable
{
	// Token: 0x060033CB RID: 13259 RVA: 0x00122ACC File Offset: 0x00120CCC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Cooking;
		this.alwaysShowProgressBar = true;
		this.resetProgressOnStop = false;
		this.attributeConverter = Db.Get().AttributeConverters.CookingSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_desalinator_kanim") };
		this.workAnims = PartyCakeWorkable.WORK_ANIMS;
		this.workingPstComplete = new HashedString[] { PartyCakeWorkable.PST_ANIM };
		this.workingPstFailed = new HashedString[] { PartyCakeWorkable.PST_ANIM };
		this.synchronizeAnims = false;
	}

	// Token: 0x060033CC RID: 13260 RVA: 0x00122B82 File Offset: 0x00120D82
	protected override bool OnWorkTick(WorkerBase worker, float dt)
	{
		base.OnWorkTick(worker, dt);
		base.GetComponent<KBatchedAnimController>().SetPositionPercent(this.GetPercentComplete());
		return false;
	}

	// Token: 0x04001F25 RID: 7973
	private static readonly HashedString[] WORK_ANIMS = new HashedString[] { "salt_pre", "salt_loop" };

	// Token: 0x04001F26 RID: 7974
	private static readonly HashedString PST_ANIM = new HashedString("salt_pst");
}
