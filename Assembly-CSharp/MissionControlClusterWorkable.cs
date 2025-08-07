using System;
using TUNING;
using UnityEngine;

// Token: 0x02000790 RID: 1936
public class MissionControlClusterWorkable : Workable
{
	// Token: 0x1700032E RID: 814
	// (get) Token: 0x06003314 RID: 13076 RVA: 0x0011FD45 File Offset: 0x0011DF45
	// (set) Token: 0x06003315 RID: 13077 RVA: 0x0011FD4D File Offset: 0x0011DF4D
	public Clustercraft TargetClustercraft
	{
		get
		{
			return this.targetClustercraft;
		}
		set
		{
			base.WorkTimeRemaining = this.GetWorkTime();
			this.targetClustercraft = value;
		}
	}

	// Token: 0x06003316 RID: 13078 RVA: 0x0011FD64 File Offset: 0x0011DF64
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

	// Token: 0x06003317 RID: 13079 RVA: 0x0011FE22 File Offset: 0x0011E022
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.MissionControlClusterWorkables.Add(this);
	}

	// Token: 0x06003318 RID: 13080 RVA: 0x0011FE35 File Offset: 0x0011E035
	protected override void OnCleanUp()
	{
		Components.MissionControlClusterWorkables.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06003319 RID: 13081 RVA: 0x0011FE48 File Offset: 0x0011E048
	public static bool IsRocketInRange(AxialI worldLocation, AxialI rocketLocation)
	{
		return AxialUtil.GetDistance(worldLocation, rocketLocation) <= 2;
	}

	// Token: 0x0600331A RID: 13082 RVA: 0x0011FE58 File Offset: 0x0011E058
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		this.workStatusItem = base.gameObject.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.MissionControlAssistingRocket, this.TargetClustercraft);
		this.operational.SetActive(true, false);
	}

	// Token: 0x0600331B RID: 13083 RVA: 0x0011FEA4 File Offset: 0x0011E0A4
	public override float GetEfficiencyMultiplier(WorkerBase worker)
	{
		return base.GetEfficiencyMultiplier(worker) * Mathf.Clamp01(this.GetSMI<SkyVisibilityMonitor.Instance>().PercentClearSky);
	}

	// Token: 0x0600331C RID: 13084 RVA: 0x0011FEBE File Offset: 0x0011E0BE
	protected override bool OnWorkTick(WorkerBase worker, float dt)
	{
		if (this.TargetClustercraft == null || !MissionControlClusterWorkable.IsRocketInRange(base.gameObject.GetMyWorldLocation(), this.TargetClustercraft.Location))
		{
			worker.StopWork();
			return true;
		}
		return base.OnWorkTick(worker, dt);
	}

	// Token: 0x0600331D RID: 13085 RVA: 0x0011FEFB File Offset: 0x0011E0FB
	protected override void OnCompleteWork(WorkerBase worker)
	{
		global::Debug.Assert(this.TargetClustercraft != null);
		base.gameObject.GetSMI<MissionControlCluster.Instance>().ApplyEffect(this.TargetClustercraft);
		base.OnCompleteWork(worker);
	}

	// Token: 0x0600331E RID: 13086 RVA: 0x0011FF2B File Offset: 0x0011E12B
	protected override void OnStopWork(WorkerBase worker)
	{
		base.OnStopWork(worker);
		base.gameObject.GetComponent<KSelectable>().RemoveStatusItem(this.workStatusItem, false);
		this.TargetClustercraft = null;
		this.operational.SetActive(false, false);
	}

	// Token: 0x04001EA9 RID: 7849
	private Clustercraft targetClustercraft;

	// Token: 0x04001EAA RID: 7850
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001EAB RID: 7851
	private Guid workStatusItem = Guid.Empty;
}
