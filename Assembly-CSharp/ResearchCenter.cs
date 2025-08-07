using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000ACA RID: 2762
[AddComponentMenu("KMonoBehaviour/Workable/ResearchCenter")]
public class ResearchCenter : Workable, IGameObjectEffectDescriptor, ISim200ms, IResearchCenter
{
	// Token: 0x0600503A RID: 20538 RVA: 0x001D05BC File Offset: 0x001CE7BC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Researching;
		this.attributeConverter = Db.Get().AttributeConverters.ResearchSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.ALL_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Research.Id;
		this.skillExperienceMultiplier = SKILLS.ALL_DAY_EXPERIENCE;
		ElementConverter elementConverter = this.elementConverter;
		elementConverter.onConvertMass = (Action<float>)Delegate.Combine(elementConverter.onConvertMass, new Action<float>(this.ConvertMassToResearchPoints));
	}

	// Token: 0x0600503B RID: 20539 RVA: 0x001D0650 File Offset: 0x001CE850
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<ResearchCenter>(-1914338957, ResearchCenter.UpdateWorkingStateDelegate);
		base.Subscribe<ResearchCenter>(-125623018, ResearchCenter.UpdateWorkingStateDelegate);
		base.Subscribe<ResearchCenter>(187661686, ResearchCenter.UpdateWorkingStateDelegate);
		base.Subscribe<ResearchCenter>(-1697596308, ResearchCenter.CheckHasMaterialDelegate);
		Components.ResearchCenters.Add(this);
		this.UpdateWorkingState(null);
	}

	// Token: 0x0600503C RID: 20540 RVA: 0x001D06BC File Offset: 0x001CE8BC
	private void ConvertMassToResearchPoints(float mass_consumed)
	{
		this.remainder_mass_points += mass_consumed / this.mass_per_point - (float)Mathf.FloorToInt(mass_consumed / this.mass_per_point);
		int num = Mathf.FloorToInt(mass_consumed / this.mass_per_point);
		num += Mathf.FloorToInt(this.remainder_mass_points);
		this.remainder_mass_points -= (float)Mathf.FloorToInt(this.remainder_mass_points);
		ResearchType researchType = Research.Instance.GetResearchType(this.research_point_type_id);
		if (num > 0)
		{
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Research, researchType.name, base.transform, 1.5f, false);
			for (int i = 0; i < num; i++)
			{
				Research.Instance.AddResearchPoints(this.research_point_type_id, 1f);
			}
		}
	}

	// Token: 0x0600503D RID: 20541 RVA: 0x001D0780 File Offset: 0x001CE980
	public void Sim200ms(float dt)
	{
		if (!this.operational.IsActive && this.operational.IsOperational && this.chore == null && this.HasMaterial())
		{
			this.chore = this.CreateChore();
			base.SetWorkTime(float.PositiveInfinity);
		}
	}

	// Token: 0x0600503E RID: 20542 RVA: 0x001D07D0 File Offset: 0x001CE9D0
	protected virtual Chore CreateChore()
	{
		return new WorkChore<ResearchCenter>(Db.Get().ChoreTypes.Research, this, null, true, null, null, null, true, null, false, true, null, true, true, true, PriorityScreen.PriorityClass.basic, 5, false, true)
		{
			preemption_cb = new Func<Chore.Precondition.Context, bool>(ResearchCenter.CanPreemptCB)
		};
	}

	// Token: 0x0600503F RID: 20543 RVA: 0x001D0818 File Offset: 0x001CEA18
	private static bool CanPreemptCB(Chore.Precondition.Context context)
	{
		WorkerBase component = context.chore.driver.GetComponent<WorkerBase>();
		float num = Db.Get().AttributeConverters.ResearchSpeed.Lookup(component).Evaluate();
		WorkerBase worker = context.consumerState.worker;
		return Db.Get().AttributeConverters.ResearchSpeed.Lookup(worker).Evaluate() > num && context.chore.gameObject.GetComponent<ResearchCenter>().GetPercentComplete() < 1f;
	}

	// Token: 0x06005040 RID: 20544 RVA: 0x001D0898 File Offset: 0x001CEA98
	public override float GetPercentComplete()
	{
		if (Research.Instance.GetActiveResearch() == null)
		{
			return 0f;
		}
		float num = Research.Instance.GetActiveResearch().progressInventory.PointsByTypeID[this.research_point_type_id];
		float num2 = 0f;
		if (!Research.Instance.GetActiveResearch().tech.costsByResearchTypeID.TryGetValue(this.research_point_type_id, out num2))
		{
			return 1f;
		}
		return num / num2;
	}

	// Token: 0x06005041 RID: 20545 RVA: 0x001D0909 File Offset: 0x001CEB09
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.ComplexFabricatorResearching, this);
		this.operational.SetActive(true, false);
	}

	// Token: 0x06005042 RID: 20546 RVA: 0x001D093C File Offset: 0x001CEB3C
	protected override bool OnWorkTick(WorkerBase worker, float dt)
	{
		float efficiencyMultiplier = this.GetEfficiencyMultiplier(worker);
		float num = 2f + efficiencyMultiplier;
		if (Game.Instance.FastWorkersModeActive)
		{
			num *= 2f;
		}
		this.elementConverter.SetWorkSpeedMultiplier(num);
		return base.OnWorkTick(worker, dt);
	}

	// Token: 0x06005043 RID: 20547 RVA: 0x001D0981 File Offset: 0x001CEB81
	protected override void OnStopWork(WorkerBase worker)
	{
		base.OnStopWork(worker);
		base.ShowProgressBar(false);
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.ComplexFabricatorResearching, this);
		this.operational.SetActive(false, false);
	}

	// Token: 0x06005044 RID: 20548 RVA: 0x001D09C0 File Offset: 0x001CEBC0
	protected bool ResearchComponentCompleted()
	{
		TechInstance activeResearch = Research.Instance.GetActiveResearch();
		if (activeResearch != null)
		{
			float num = 0f;
			float num2 = 0f;
			activeResearch.progressInventory.PointsByTypeID.TryGetValue(this.research_point_type_id, out num);
			activeResearch.tech.costsByResearchTypeID.TryGetValue(this.research_point_type_id, out num2);
			if (num >= num2)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06005045 RID: 20549 RVA: 0x001D0A20 File Offset: 0x001CEC20
	protected bool IsAllResearchComplete()
	{
		using (List<Tech>.Enumerator enumerator = Db.Get().Techs.resources.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.IsComplete())
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06005046 RID: 20550 RVA: 0x001D0A84 File Offset: 0x001CEC84
	protected virtual void UpdateWorkingState(object data)
	{
		bool flag = false;
		bool flag2 = false;
		TechInstance activeResearch = Research.Instance.GetActiveResearch();
		if (activeResearch != null)
		{
			flag = true;
			if (activeResearch.tech.costsByResearchTypeID.ContainsKey(this.research_point_type_id) && Research.Instance.Get(activeResearch.tech).progressInventory.PointsByTypeID[this.research_point_type_id] < activeResearch.tech.costsByResearchTypeID[this.research_point_type_id])
			{
				flag2 = true;
			}
		}
		if (this.operational.GetFlag(EnergyConsumer.PoweredFlag) && !this.IsAllResearchComplete())
		{
			if (flag)
			{
				base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.NoResearchSelected, false);
				if (!flag2 && !this.ResearchComponentCompleted())
				{
					base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.NoResearchSelected, false);
					base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.NoApplicableResearchSelected, null);
				}
				else
				{
					base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.NoApplicableResearchSelected, false);
				}
			}
			else
			{
				base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.NoResearchSelected, null);
				base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.NoApplicableResearchSelected, false);
			}
		}
		else
		{
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.NoResearchSelected, false);
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.NoApplicableResearchSelected, false);
		}
		this.operational.SetFlag(ResearchCenter.ResearchSelectedFlag, flag && flag2);
		if ((!flag || !flag2) && base.worker)
		{
			base.StopWork(base.worker, true);
		}
	}

	// Token: 0x06005047 RID: 20551 RVA: 0x001D0C49 File Offset: 0x001CEE49
	private void ClearResearchScreen()
	{
		Game.Instance.Trigger(-1974454597, null);
	}

	// Token: 0x06005048 RID: 20552 RVA: 0x001D0C5B File Offset: 0x001CEE5B
	public string GetResearchType()
	{
		return this.research_point_type_id;
	}

	// Token: 0x06005049 RID: 20553 RVA: 0x001D0C63 File Offset: 0x001CEE63
	private void CheckHasMaterial(object o = null)
	{
		if (!this.HasMaterial() && this.chore != null)
		{
			this.chore.Cancel("No material remaining");
			this.chore = null;
		}
	}

	// Token: 0x0600504A RID: 20554 RVA: 0x001D0C8C File Offset: 0x001CEE8C
	private bool HasMaterial()
	{
		return this.storage.MassStored() > 0f;
	}

	// Token: 0x0600504B RID: 20555 RVA: 0x001D0CA0 File Offset: 0x001CEEA0
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Research.Instance.Unsubscribe(-1914338957, new Action<object>(this.UpdateWorkingState));
		Research.Instance.Unsubscribe(-125623018, new Action<object>(this.UpdateWorkingState));
		base.Unsubscribe(-1852328367, new Action<object>(this.UpdateWorkingState));
		Components.ResearchCenters.Remove(this);
		this.ClearResearchScreen();
	}

	// Token: 0x0600504C RID: 20556 RVA: 0x001D0D14 File Offset: 0x001CEF14
	public string GetStatusString()
	{
		string text = RESEARCH.MESSAGING.NORESEARCHSELECTED;
		if (Research.Instance.GetActiveResearch() != null)
		{
			text = "<b>" + Research.Instance.GetActiveResearch().tech.Name + "</b>";
			int num = 0;
			foreach (KeyValuePair<string, float> keyValuePair in Research.Instance.GetActiveResearch().progressInventory.PointsByTypeID)
			{
				if (Research.Instance.GetActiveResearch().tech.costsByResearchTypeID[keyValuePair.Key] != 0f)
				{
					num++;
				}
			}
			foreach (KeyValuePair<string, float> keyValuePair2 in Research.Instance.GetActiveResearch().progressInventory.PointsByTypeID)
			{
				if (Research.Instance.GetActiveResearch().tech.costsByResearchTypeID[keyValuePair2.Key] != 0f && keyValuePair2.Key == this.research_point_type_id)
				{
					text = text + "\n   - " + Research.Instance.researchTypes.GetResearchType(keyValuePair2.Key).name;
					text = string.Concat(new string[]
					{
						text,
						": ",
						keyValuePair2.Value.ToString(),
						"/",
						Research.Instance.GetActiveResearch().tech.costsByResearchTypeID[keyValuePair2.Key].ToString()
					});
				}
			}
			foreach (KeyValuePair<string, float> keyValuePair3 in Research.Instance.GetActiveResearch().progressInventory.PointsByTypeID)
			{
				if (Research.Instance.GetActiveResearch().tech.costsByResearchTypeID[keyValuePair3.Key] != 0f && !(keyValuePair3.Key == this.research_point_type_id))
				{
					if (num > 1)
					{
						text = text + "\n   - " + string.Format(RESEARCH.MESSAGING.RESEARCHTYPEALSOREQUIRED, Research.Instance.researchTypes.GetResearchType(keyValuePair3.Key).name);
					}
					else
					{
						text = text + "\n   - " + string.Format(RESEARCH.MESSAGING.RESEARCHTYPEREQUIRED, Research.Instance.researchTypes.GetResearchType(keyValuePair3.Key).name);
					}
				}
			}
		}
		return text;
	}

	// Token: 0x0600504D RID: 20557 RVA: 0x001D0FF4 File Offset: 0x001CF1F4
	public override List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> descriptors = base.GetDescriptors(go);
		descriptors.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.RESEARCH_MATERIALS, this.inputMaterial.ProperName(), GameUtil.GetFormattedByTag(this.inputMaterial, this.mass_per_point, GameUtil.TimeSlice.None)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.RESEARCH_MATERIALS, this.inputMaterial.ProperName(), GameUtil.GetFormattedByTag(this.inputMaterial, this.mass_per_point, GameUtil.TimeSlice.None)), Descriptor.DescriptorType.Requirement, false));
		descriptors.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.PRODUCES_RESEARCH_POINTS, Research.Instance.researchTypes.GetResearchType(this.research_point_type_id).name), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.PRODUCES_RESEARCH_POINTS, Research.Instance.researchTypes.GetResearchType(this.research_point_type_id).name), Descriptor.DescriptorType.Effect, false));
		return descriptors;
	}

	// Token: 0x0600504E RID: 20558 RVA: 0x001D10CC File Offset: 0x001CF2CC
	public override bool InstantlyFinish(WorkerBase worker)
	{
		return false;
	}

	// Token: 0x04003603 RID: 13827
	private Chore chore;

	// Token: 0x04003604 RID: 13828
	[MyCmpAdd]
	protected Notifier notifier;

	// Token: 0x04003605 RID: 13829
	[MyCmpAdd]
	protected Operational operational;

	// Token: 0x04003606 RID: 13830
	[MyCmpAdd]
	protected Storage storage;

	// Token: 0x04003607 RID: 13831
	[MyCmpGet]
	private ElementConverter elementConverter;

	// Token: 0x04003608 RID: 13832
	[SerializeField]
	public string research_point_type_id;

	// Token: 0x04003609 RID: 13833
	[SerializeField]
	public Tag inputMaterial;

	// Token: 0x0400360A RID: 13834
	[SerializeField]
	public float mass_per_point;

	// Token: 0x0400360B RID: 13835
	[SerializeField]
	private float remainder_mass_points;

	// Token: 0x0400360C RID: 13836
	public static readonly Operational.Flag ResearchSelectedFlag = new Operational.Flag("researchSelected", Operational.Flag.Type.Requirement);

	// Token: 0x0400360D RID: 13837
	private static readonly EventSystem.IntraObjectHandler<ResearchCenter> UpdateWorkingStateDelegate = new EventSystem.IntraObjectHandler<ResearchCenter>(delegate(ResearchCenter component, object data)
	{
		component.UpdateWorkingState(data);
	});

	// Token: 0x0400360E RID: 13838
	private static readonly EventSystem.IntraObjectHandler<ResearchCenter> CheckHasMaterialDelegate = new EventSystem.IntraObjectHandler<ResearchCenter>(delegate(ResearchCenter component, object data)
	{
		component.CheckHasMaterial(data);
	});
}
