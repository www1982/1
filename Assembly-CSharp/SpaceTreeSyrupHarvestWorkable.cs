using System;
using TUNING;

// Token: 0x020001AE RID: 430
public class SpaceTreeSyrupHarvestWorkable : Workable
{
	// Token: 0x06000894 RID: 2196 RVA: 0x0003A640 File Offset: 0x00038840
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetWorkerStatusItem(Db.Get().DuplicantStatusItems.Harvesting);
		this.workAnims = new HashedString[] { "syrup_harvest_trunk_pre", "syrup_harvest_trunk_loop" };
		this.workingPstComplete = new HashedString[] { "syrup_harvest_trunk_pst" };
		this.workingPstFailed = new HashedString[] { "syrup_harvest_trunk_loop" };
		this.attributeConverter = Db.Get().AttributeConverters.HarvestSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Farming.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		this.requiredSkillPerk = Db.Get().SkillPerks.CanFarmTinker.Id;
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_syrup_tree_kanim") };
		this.synchronizeAnims = true;
		this.shouldShowSkillPerkStatusItem = false;
		base.SetWorkTime(10f);
		this.resetProgressOnStop = true;
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x0003A76D File Offset: 0x0003896D
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x0003A775 File Offset: 0x00038975
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
	}
}
