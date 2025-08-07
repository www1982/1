using System;
using TUNING;
using UnityEngine;

// Token: 0x020003BA RID: 954
public class ReanimateBionicWorkable : Workable
{
	// Token: 0x0600136B RID: 4971 RVA: 0x0006E8B0 File Offset: 0x0006CAB0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workAnims = new HashedString[] { "offline_battery_change_pre", "offline_battery_change_loop" };
		this.workingPstComplete = new HashedString[] { "offline_battery_change_pst" };
		this.workingPstFailed = new HashedString[] { "offline_battery_change_failed" };
		base.SetWorkTime(30f);
		this.readyForSkillWorkStatusItem = Db.Get().DuplicantStatusItems.BionicRequiresSkillPerk;
		base.SetWorkerStatusItem(Db.Get().DuplicantStatusItems.InstallingElectrobank);
		this.workingStatusItem = Db.Get().DuplicantStatusItems.BionicBeingRebooted;
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_bionic_kanim") };
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		this.lightEfficiencyBonus = true;
		this.synchronizeAnims = true;
		this.resetProgressOnStop = false;
	}

	// Token: 0x0600136C RID: 4972 RVA: 0x0006E9C0 File Offset: 0x0006CBC0
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		Vector3 position = worker.transform.GetPosition();
		position.x = base.transform.GetPosition().x;
		position.z = Grid.GetLayerZ(Grid.SceneLayer.Creatures);
		worker.transform.SetPosition(position);
	}

	// Token: 0x0600136D RID: 4973 RVA: 0x0006EA14 File Offset: 0x0006CC14
	protected override void OnStopWork(WorkerBase worker)
	{
		Vector3 position = worker.transform.GetPosition();
		position.z = Grid.GetLayerZ(Grid.SceneLayer.Move);
		worker.transform.SetPosition(position);
		base.OnStopWork(worker);
	}
}
