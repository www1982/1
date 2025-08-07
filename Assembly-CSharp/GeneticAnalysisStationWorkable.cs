using System;
using TUNING;
using UnityEngine;

// Token: 0x02000737 RID: 1847
public class GeneticAnalysisStationWorkable : Workable
{
	// Token: 0x06002EA1 RID: 11937 RVA: 0x0010B0A4 File Offset: 0x001092A4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.requiredSkillPerk = Db.Get().SkillPerks.CanIdentifyMutantSeeds.Id;
		this.workerStatusItem = Db.Get().DuplicantStatusItems.AnalyzingGenes;
		this.attributeConverter = Db.Get().AttributeConverters.ResearchSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Research.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_genetic_analysisstation_kanim") };
		base.SetWorkTime(150f);
		this.showProgressBar = true;
		this.lightEfficiencyBonus = true;
	}

	// Token: 0x06002EA2 RID: 11938 RVA: 0x0010B162 File Offset: 0x00109362
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.ComplexFabricatorResearching, this.storage.FindFirst(GameTags.UnidentifiedSeed));
	}

	// Token: 0x06002EA3 RID: 11939 RVA: 0x0010B196 File Offset: 0x00109396
	protected override void OnStopWork(WorkerBase worker)
	{
		base.OnStopWork(worker);
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.ComplexFabricatorResearching, false);
	}

	// Token: 0x06002EA4 RID: 11940 RVA: 0x0010B1BB File Offset: 0x001093BB
	protected override void OnCompleteWork(WorkerBase worker)
	{
		base.OnCompleteWork(worker);
		this.IdentifyMutant();
	}

	// Token: 0x06002EA5 RID: 11941 RVA: 0x0010B1CC File Offset: 0x001093CC
	public void IdentifyMutant()
	{
		GameObject gameObject = this.storage.FindFirst(GameTags.UnidentifiedSeed);
		DebugUtil.DevAssertArgs(gameObject != null, new object[] { "AAACCCCKKK!! GeneticAnalysisStation finished studying a seed but we don't have one in storage??" });
		if (gameObject != null)
		{
			Pickupable component = gameObject.GetComponent<Pickupable>();
			Pickupable pickupable;
			if (component.PrimaryElement.Units > 1f)
			{
				pickupable = component.TakeUnit(1f);
			}
			else
			{
				pickupable = this.storage.Drop(gameObject, true).GetComponent<Pickupable>();
			}
			pickupable.transform.SetPosition(base.transform.GetPosition() + this.finishedSeedDropOffset);
			MutantPlant component2 = pickupable.GetComponent<MutantPlant>();
			PlantSubSpeciesCatalog.Instance.IdentifySubSpecies(component2.SubSpeciesID);
			component2.Analyze();
			SaveGame.Instance.ColonyAchievementTracker.LogAnalyzedSeed(component2.SpeciesID);
		}
	}

	// Token: 0x04001B92 RID: 7058
	[MyCmpAdd]
	public Notifier notifier;

	// Token: 0x04001B93 RID: 7059
	[MyCmpReq]
	public Storage storage;

	// Token: 0x04001B94 RID: 7060
	[SerializeField]
	public Vector3 finishedSeedDropOffset;

	// Token: 0x04001B95 RID: 7061
	private Notification notification;

	// Token: 0x04001B96 RID: 7062
	public GeneticAnalysisStation.StatesInstance statesInstance;
}
