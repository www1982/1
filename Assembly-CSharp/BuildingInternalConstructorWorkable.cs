using System;
using TUNING;

// Token: 0x020006CF RID: 1743
public class BuildingInternalConstructorWorkable : Workable
{
	// Token: 0x06002B05 RID: 11013 RVA: 0x000F8C60 File Offset: 0x000F6E60
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.attributeConverter = Db.Get().AttributeConverters.ConstructionSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.MOST_DAY_EXPERIENCE;
		this.minimumAttributeMultiplier = 0.75f;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Building.Id;
		this.skillExperienceMultiplier = SKILLS.MOST_DAY_EXPERIENCE;
		this.resetProgressOnStop = false;
		this.multitoolContext = "build";
		this.multitoolHitEffectTag = EffectConfigs.BuildSplashId;
		this.workingPstComplete = null;
		this.workingPstFailed = null;
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
	}

	// Token: 0x06002B06 RID: 11014 RVA: 0x000F8D03 File Offset: 0x000F6F03
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.constructorInstance = this.GetSMI<BuildingInternalConstructor.Instance>();
	}

	// Token: 0x06002B07 RID: 11015 RVA: 0x000F8D17 File Offset: 0x000F6F17
	protected override void OnCompleteWork(WorkerBase worker)
	{
		this.constructorInstance.ConstructionComplete(false);
	}

	// Token: 0x0400195D RID: 6493
	private BuildingInternalConstructor.Instance constructorInstance;
}
