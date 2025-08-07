using System;
using TUNING;
using UnityEngine;

// Token: 0x0200074B RID: 1867
[AddComponentMenu("KMonoBehaviour/Workable/IceCooledFanWorkable")]
public class IceCooledFanWorkable : Workable
{
	// Token: 0x06002F6C RID: 12140 RVA: 0x0010FA87 File Offset: 0x0010DC87
	private IceCooledFanWorkable()
	{
		this.showProgressBar = false;
	}

	// Token: 0x06002F6D RID: 12141 RVA: 0x0010FA98 File Offset: 0x0010DC98
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.attributeConverter = Db.Get().AttributeConverters.MachinerySpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.MOST_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Technicals.Id;
		this.skillExperienceMultiplier = SKILLS.MOST_DAY_EXPERIENCE;
		this.workerStatusItem = null;
	}

	// Token: 0x06002F6E RID: 12142 RVA: 0x0010FAF7 File Offset: 0x0010DCF7
	protected override void OnSpawn()
	{
		GameScheduler.Instance.Schedule("InsulationTutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Insulation, true);
		}, null, null);
		base.OnSpawn();
	}

	// Token: 0x06002F6F RID: 12143 RVA: 0x0010FB35 File Offset: 0x0010DD35
	protected override void OnStartWork(WorkerBase worker)
	{
		this.operational.SetActive(true, false);
	}

	// Token: 0x06002F70 RID: 12144 RVA: 0x0010FB44 File Offset: 0x0010DD44
	protected override void OnStopWork(WorkerBase worker)
	{
		this.operational.SetActive(false, false);
	}

	// Token: 0x06002F71 RID: 12145 RVA: 0x0010FB53 File Offset: 0x0010DD53
	protected override void OnCompleteWork(WorkerBase worker)
	{
		this.operational.SetActive(false, false);
	}

	// Token: 0x04001C25 RID: 7205
	[MyCmpGet]
	private Operational operational;
}
