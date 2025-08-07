using System;
using TUNING;

// Token: 0x0200078A RID: 1930
public class EmptyMilkSeparatorWorkable : Workable
{
	// Token: 0x060032F7 RID: 13047 RVA: 0x0011EC24 File Offset: 0x0011CE24
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workLayer = Grid.SceneLayer.BuildingFront;
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Cleaning;
		this.workingStatusItem = Db.Get().MiscStatusItems.Cleaning;
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_milk_separator_kanim") };
		this.attributeConverter = Db.Get().AttributeConverters.TidyingSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		base.SetWorkTime(15f);
		this.synchronizeAnims = true;
	}

	// Token: 0x060032F8 RID: 13048 RVA: 0x0011ECC4 File Offset: 0x0011CEC4
	public override void OnPendingCompleteWork(WorkerBase worker)
	{
		global::System.Action onWork_PST_Begins = this.OnWork_PST_Begins;
		if (onWork_PST_Begins != null)
		{
			onWork_PST_Begins();
		}
		base.OnPendingCompleteWork(worker);
	}

	// Token: 0x04001E87 RID: 7815
	public global::System.Action OnWork_PST_Begins;
}
