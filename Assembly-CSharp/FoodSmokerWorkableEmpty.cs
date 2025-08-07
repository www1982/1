using System;
using TUNING;

// Token: 0x02000731 RID: 1841
public class FoodSmokerWorkableEmpty : Workable
{
	// Token: 0x06002E6A RID: 11882 RVA: 0x0010A484 File Offset: 0x00108684
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Emptying;
		this.workAnims = FoodSmokerWorkableEmpty.WORK_ANIMS;
		this.workingPstComplete = FoodSmokerWorkableEmpty.WORK_ANIMS_PST;
		this.workingPstFailed = FoodSmokerWorkableEmpty.WORK_ANIMS_FAIL_PST;
		this.requiredSkillPerk = Db.Get().SkillPerks.CanGasRange.Id;
		this.attributeConverter = Db.Get().AttributeConverters.CookingSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.FULL_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Cooking.Id;
		this.skillExperienceMultiplier = SKILLS.FULL_EXPERIENCE;
	}

	// Token: 0x04001B7A RID: 7034
	private static readonly HashedString[] WORK_ANIMS = new HashedString[] { "empty_pre", "empty_loop" };

	// Token: 0x04001B7B RID: 7035
	private static readonly HashedString[] WORK_ANIMS_PST = new HashedString[] { "empty_pst" };

	// Token: 0x04001B7C RID: 7036
	private static readonly HashedString[] WORK_ANIMS_FAIL_PST = new HashedString[] { "" };
}
