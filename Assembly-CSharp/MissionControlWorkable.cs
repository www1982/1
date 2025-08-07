using System;
using TUNING;
using UnityEngine;

// Token: 0x02000791 RID: 1937
public class MissionControlWorkable : Workable
{
	// Token: 0x1700032F RID: 815
	// (get) Token: 0x06003320 RID: 13088 RVA: 0x0011FF73 File Offset: 0x0011E173
	// (set) Token: 0x06003321 RID: 13089 RVA: 0x0011FF7B File Offset: 0x0011E17B
	public Spacecraft TargetSpacecraft
	{
		get
		{
			return this.targetSpacecraft;
		}
		set
		{
			base.WorkTimeRemaining = this.GetWorkTime();
			this.targetSpacecraft = value;
		}
	}

	// Token: 0x06003322 RID: 13090 RVA: 0x0011FF90 File Offset: 0x0011E190
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.requiredSkillPerk = Db.Get().SkillPerks.CanMissionControl.Id;
		this.workerStatusItem = Db.Get().DuplicantStatusItems.MissionControlling;
		this.attributeConverter = Db.Get().AttributeConverters.ResearchSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Research.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_mission_control_station_kanim") };
		base.SetWorkTime(90f);
		this.showProgressBar = true;
		this.lightEfficiencyBonus = true;
	}

	// Token: 0x06003323 RID: 13091 RVA: 0x0012004E File Offset: 0x0011E24E
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.MissionControlWorkables.Add(this);
	}

	// Token: 0x06003324 RID: 13092 RVA: 0x00120061 File Offset: 0x0011E261
	protected override void OnCleanUp()
	{
		Components.MissionControlWorkables.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06003325 RID: 13093 RVA: 0x00120074 File Offset: 0x0011E274
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		this.workStatusItem = base.gameObject.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.MissionControlAssistingRocket, this.TargetSpacecraft);
		this.operational.SetActive(true, false);
	}

	// Token: 0x06003326 RID: 13094 RVA: 0x001200C0 File Offset: 0x0011E2C0
	public override float GetEfficiencyMultiplier(WorkerBase worker)
	{
		return base.GetEfficiencyMultiplier(worker) * Mathf.Clamp01(this.GetSMI<SkyVisibilityMonitor.Instance>().PercentClearSky);
	}

	// Token: 0x06003327 RID: 13095 RVA: 0x001200DA File Offset: 0x0011E2DA
	protected override bool OnWorkTick(WorkerBase worker, float dt)
	{
		if (this.TargetSpacecraft == null)
		{
			worker.StopWork();
			return true;
		}
		return base.OnWorkTick(worker, dt);
	}

	// Token: 0x06003328 RID: 13096 RVA: 0x001200F4 File Offset: 0x0011E2F4
	protected override void OnCompleteWork(WorkerBase worker)
	{
		global::Debug.Assert(this.TargetSpacecraft != null);
		base.gameObject.GetSMI<MissionControl.Instance>().ApplyEffect(this.TargetSpacecraft);
		base.OnCompleteWork(worker);
	}

	// Token: 0x06003329 RID: 13097 RVA: 0x00120121 File Offset: 0x0011E321
	protected override void OnStopWork(WorkerBase worker)
	{
		base.OnStopWork(worker);
		base.gameObject.GetComponent<KSelectable>().RemoveStatusItem(this.workStatusItem, false);
		this.TargetSpacecraft = null;
		this.operational.SetActive(false, false);
	}

	// Token: 0x04001EAC RID: 7852
	private Spacecraft targetSpacecraft;

	// Token: 0x04001EAD RID: 7853
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001EAE RID: 7854
	private Guid workStatusItem = Guid.Empty;
}
