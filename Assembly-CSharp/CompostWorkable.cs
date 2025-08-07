using System;
using TUNING;
using UnityEngine;

// Token: 0x020006F5 RID: 1781
[AddComponentMenu("KMonoBehaviour/Workable/CompostWorkable")]
public class CompostWorkable : Workable
{
	// Token: 0x06002C94 RID: 11412 RVA: 0x00101424 File Offset: 0x000FF624
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.attributeConverter = Db.Get().AttributeConverters.TidyingSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Basekeeping.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
	}

	// Token: 0x06002C95 RID: 11413 RVA: 0x0010147C File Offset: 0x000FF67C
	protected override void OnStartWork(WorkerBase worker)
	{
	}

	// Token: 0x06002C96 RID: 11414 RVA: 0x0010147E File Offset: 0x000FF67E
	protected override void OnStopWork(WorkerBase worker)
	{
	}
}
