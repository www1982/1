using System;
using TUNING;

// Token: 0x02000342 RID: 834
public class MorbRoverMakerWorkable : Workable
{
	// Token: 0x0600113E RID: 4414 RVA: 0x00064F44 File Offset: 0x00063144
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workingStatusItem = Db.Get().BuildingStatusItems.MorbRoverMakerDoctorWorking;
		base.SetWorkerStatusItem(Db.Get().DuplicantStatusItems.MorbRoverMakerDoctorWorking);
		this.requiredSkillPerk = Db.Get().SkillPerks.CanAdvancedMedicine.Id;
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_gravitas_morb_tank_kanim") };
		this.attributeConverter = Db.Get().AttributeConverters.DoctorSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.BARELY_EVER_EXPERIENCE;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		this.lightEfficiencyBonus = true;
		this.synchronizeAnims = true;
		this.shouldShowSkillPerkStatusItem = true;
		base.SetWorkTime(90f);
		this.resetProgressOnStop = true;
	}

	// Token: 0x0600113F RID: 4415 RVA: 0x0006500B File Offset: 0x0006320B
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x06001140 RID: 4416 RVA: 0x00065013 File Offset: 0x00063213
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
	}

	// Token: 0x04000AE5 RID: 2789
	public const float DOCTOR_WORKING_TIME = 90f;
}
