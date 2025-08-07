using System;
using TUNING;
using UnityEngine;

// Token: 0x0200074A RID: 1866
[AddComponentMenu("KMonoBehaviour/Workable/HiveWorkableEmpty")]
public class HiveWorkableEmpty : Workable
{
	// Token: 0x06002F68 RID: 12136 RVA: 0x0010F978 File Offset: 0x0010DB78
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Emptying;
		this.attributeConverter = Db.Get().AttributeConverters.TidyingSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Basekeeping.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		this.workAnims = HiveWorkableEmpty.WORK_ANIMS;
		this.workingPstComplete = new HashedString[] { HiveWorkableEmpty.PST_ANIM };
		this.workingPstFailed = new HashedString[] { HiveWorkableEmpty.PST_ANIM };
	}

	// Token: 0x06002F69 RID: 12137 RVA: 0x0010FA20 File Offset: 0x0010DC20
	protected override void OnCompleteWork(WorkerBase worker)
	{
		base.OnCompleteWork(worker);
		if (!this.wasStung)
		{
			SaveGame.Instance.ColonyAchievementTracker.harvestAHiveWithoutGettingStung = true;
		}
	}

	// Token: 0x04001C22 RID: 7202
	private static readonly HashedString[] WORK_ANIMS = new HashedString[] { "working_pre", "working_loop" };

	// Token: 0x04001C23 RID: 7203
	private static readonly HashedString PST_ANIM = new HashedString("working_pst");

	// Token: 0x04001C24 RID: 7204
	public bool wasStung;
}
