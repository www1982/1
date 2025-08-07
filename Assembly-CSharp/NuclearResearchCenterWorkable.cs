using System;
using KSerialization;
using TUNING;
using UnityEngine;

// Token: 0x02000A38 RID: 2616
public class NuclearResearchCenterWorkable : Workable
{
	// Token: 0x06004BEB RID: 19435 RVA: 0x001B7B04 File Offset: 0x001B5D04
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Researching;
		this.attributeConverter = Db.Get().AttributeConverters.ResearchSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.ALL_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Research.Id;
		this.skillExperienceMultiplier = SKILLS.ALL_DAY_EXPERIENCE;
		this.radiationStorage = base.GetComponent<HighEnergyParticleStorage>();
		this.nrc = base.GetComponent<NuclearResearchCenter>();
		this.lightEfficiencyBonus = true;
	}

	// Token: 0x06004BEC RID: 19436 RVA: 0x001B7B90 File Offset: 0x001B5D90
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.SetWorkTime(float.PositiveInfinity);
	}

	// Token: 0x06004BED RID: 19437 RVA: 0x001B7BA4 File Offset: 0x001B5DA4
	protected override bool OnWorkTick(WorkerBase worker, float dt)
	{
		float num = dt / this.nrc.timePerPoint;
		if (Game.Instance.FastWorkersModeActive)
		{
			num *= 2f;
		}
		this.radiationStorage.ConsumeAndGet(num * this.nrc.materialPerPoint);
		this.pointsProduced += num;
		if (this.pointsProduced >= 1f)
		{
			int num2 = Mathf.FloorToInt(this.pointsProduced);
			this.pointsProduced -= (float)num2;
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Research, Research.Instance.GetResearchType("nuclear").name, base.transform, 1.5f, false);
			Research.Instance.AddResearchPoints("nuclear", (float)num2);
		}
		TechInstance activeResearch = Research.Instance.GetActiveResearch();
		return this.radiationStorage.IsEmpty() || activeResearch == null || activeResearch.PercentageCompleteResearchType("nuclear") >= 1f;
	}

	// Token: 0x06004BEE RID: 19438 RVA: 0x001B7C9A File Offset: 0x001B5E9A
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.ComplexFabricatorResearching, this.nrc);
	}

	// Token: 0x06004BEF RID: 19439 RVA: 0x001B7CC4 File Offset: 0x001B5EC4
	protected override void OnAbortWork(WorkerBase worker)
	{
		base.OnAbortWork(worker);
	}

	// Token: 0x06004BF0 RID: 19440 RVA: 0x001B7CCD File Offset: 0x001B5ECD
	protected override void OnStopWork(WorkerBase worker)
	{
		base.OnStopWork(worker);
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.ComplexFabricatorResearching, this.nrc);
	}

	// Token: 0x06004BF1 RID: 19441 RVA: 0x001B7CFC File Offset: 0x001B5EFC
	public override float GetPercentComplete()
	{
		if (Research.Instance.GetActiveResearch() == null)
		{
			return 0f;
		}
		float num = Research.Instance.GetActiveResearch().progressInventory.PointsByTypeID["nuclear"];
		float num2 = 0f;
		if (!Research.Instance.GetActiveResearch().tech.costsByResearchTypeID.TryGetValue("nuclear", out num2))
		{
			return 1f;
		}
		return num / num2;
	}

	// Token: 0x06004BF2 RID: 19442 RVA: 0x001B7D6B File Offset: 0x001B5F6B
	public override bool InstantlyFinish(WorkerBase worker)
	{
		return false;
	}

	// Token: 0x04003254 RID: 12884
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04003255 RID: 12885
	[Serialize]
	private float pointsProduced;

	// Token: 0x04003256 RID: 12886
	private NuclearResearchCenter nrc;

	// Token: 0x04003257 RID: 12887
	private HighEnergyParticleStorage radiationStorage;
}
