using System;
using KSerialization;
using TUNING;
using UnityEngine;

// Token: 0x0200094F RID: 2383
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/Workable/Harvestable")]
public class Harvestable : Workable
{
	// Token: 0x170004E0 RID: 1248
	// (get) Token: 0x0600444A RID: 17482 RVA: 0x00188D21 File Offset: 0x00186F21
	// (set) Token: 0x0600444B RID: 17483 RVA: 0x00188D29 File Offset: 0x00186F29
	public WorkerBase completed_by { get; protected set; }

	// Token: 0x170004E1 RID: 1249
	// (get) Token: 0x0600444C RID: 17484 RVA: 0x00188D32 File Offset: 0x00186F32
	public bool CanBeHarvested
	{
		get
		{
			return this.canBeHarvested;
		}
	}

	// Token: 0x0600444D RID: 17485 RVA: 0x00188D3A File Offset: 0x00186F3A
	protected Harvestable()
	{
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
	}

	// Token: 0x0600444E RID: 17486 RVA: 0x00188D64 File Offset: 0x00186F64
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Harvesting;
		this.multitoolContext = "harvest";
		this.multitoolHitEffectTag = "fx_harvest_splash";
		this.harvestDesignatable = base.GetComponent<HarvestDesignatable>();
	}

	// Token: 0x0600444F RID: 17487 RVA: 0x00188DB8 File Offset: 0x00186FB8
	protected override void OnSpawn()
	{
		base.Subscribe<Harvestable>(2127324410, Harvestable.ForceCancelHarvestDelegate);
		base.SetWorkTime(10f);
		base.Subscribe<Harvestable>(2127324410, Harvestable.OnCancelDelegate);
		this.faceTargetWhenWorking = true;
		Components.Harvestables.Add(this);
		this.attributeConverter = Db.Get().AttributeConverters.HarvestSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Farming.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
	}

	// Token: 0x06004450 RID: 17488 RVA: 0x00188E49 File Offset: 0x00187049
	public void OnUprooted(object data)
	{
		if (this.canBeHarvested)
		{
			this.Harvest();
		}
	}

	// Token: 0x06004451 RID: 17489 RVA: 0x00188E5C File Offset: 0x0018705C
	public void Harvest()
	{
		if (this.harvestDesignatable != null)
		{
			this.harvestDesignatable.MarkedForHarvest = false;
		}
		this.chore = null;
		base.Trigger(1272413801, this);
		KSelectable component = base.GetComponent<KSelectable>();
		component.RemoveStatusItem(Db.Get().MiscStatusItems.PendingHarvest, false);
		component.RemoveStatusItem(Db.Get().MiscStatusItems.Operating, false);
		Game.Instance.userMenu.Refresh(base.gameObject);
	}

	// Token: 0x06004452 RID: 17490 RVA: 0x00188EE0 File Offset: 0x001870E0
	public void OnMarkedForHarvest()
	{
		KSelectable component = base.GetComponent<KSelectable>();
		if (this.chore == null)
		{
			this.chore = new WorkChore<Harvestable>(Db.Get().ChoreTypes.Harvest, this, null, true, null, null, null, true, null, false, true, null, true, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
			component.AddStatusItem(Db.Get().MiscStatusItems.PendingHarvest, this);
		}
	}

	// Token: 0x06004453 RID: 17491 RVA: 0x00188F40 File Offset: 0x00187140
	public void SetCanBeHarvested(bool state)
	{
		this.canBeHarvested = state;
		KSelectable component = base.GetComponent<KSelectable>();
		if (this.canBeHarvested)
		{
			component.AddStatusItem(this.readyForHarvestStatusItem, null);
			if (this.harvestDesignatable != null)
			{
				if (this.harvestDesignatable.HarvestWhenReady)
				{
					this.harvestDesignatable.MarkForHarvest();
				}
				else if (this.harvestDesignatable.InPlanterBox)
				{
					component.AddStatusItem(Db.Get().MiscStatusItems.NotMarkedForHarvest, this);
				}
			}
			else
			{
				this.OnMarkedForHarvest();
			}
		}
		else
		{
			component.RemoveStatusItem(this.readyForHarvestStatusItem, false);
			component.RemoveStatusItem(Db.Get().MiscStatusItems.NotMarkedForHarvest, false);
		}
		Game.Instance.userMenu.Refresh(base.gameObject);
	}

	// Token: 0x06004454 RID: 17492 RVA: 0x00189001 File Offset: 0x00187201
	protected override void OnCompleteWork(WorkerBase worker)
	{
		this.completed_by = worker;
		this.Harvest();
	}

	// Token: 0x06004455 RID: 17493 RVA: 0x00189010 File Offset: 0x00187210
	protected virtual void OnCancel(object data)
	{
		bool flag = data == null || (data is bool && !(bool)data);
		if (this.chore != null)
		{
			this.chore.Cancel("Cancel harvest");
			this.chore = null;
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.PendingHarvest, false);
			if (flag && this.harvestDesignatable != null)
			{
				this.harvestDesignatable.SetHarvestWhenReady(false);
			}
		}
		if (flag && this.harvestDesignatable != null)
		{
			this.harvestDesignatable.MarkedForHarvest = false;
		}
	}

	// Token: 0x06004456 RID: 17494 RVA: 0x001890AD File Offset: 0x001872AD
	public bool HasChore()
	{
		return this.chore != null;
	}

	// Token: 0x06004457 RID: 17495 RVA: 0x001890BA File Offset: 0x001872BA
	public virtual void ForceCancelHarvest(object data = null)
	{
		this.OnCancel(data);
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.PendingHarvest, false);
		Game.Instance.userMenu.Refresh(base.gameObject);
	}

	// Token: 0x06004458 RID: 17496 RVA: 0x001890F4 File Offset: 0x001872F4
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.Harvestables.Remove(this);
	}

	// Token: 0x06004459 RID: 17497 RVA: 0x00189107 File Offset: 0x00187307
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.PendingHarvest, false);
	}

	// Token: 0x04002DB8 RID: 11704
	public StatusItem readyForHarvestStatusItem = Db.Get().CreatureStatusItems.ReadyForHarvest;

	// Token: 0x04002DB9 RID: 11705
	public HarvestDesignatable harvestDesignatable;

	// Token: 0x04002DBA RID: 11706
	[Serialize]
	protected bool canBeHarvested;

	// Token: 0x04002DBC RID: 11708
	protected Chore chore;

	// Token: 0x04002DBD RID: 11709
	private static readonly EventSystem.IntraObjectHandler<Harvestable> ForceCancelHarvestDelegate = new EventSystem.IntraObjectHandler<Harvestable>(delegate(Harvestable component, object data)
	{
		component.ForceCancelHarvest(data);
	});

	// Token: 0x04002DBE RID: 11710
	private static readonly EventSystem.IntraObjectHandler<Harvestable> OnCancelDelegate = new EventSystem.IntraObjectHandler<Harvestable>(delegate(Harvestable component, object data)
	{
		component.OnCancel(data);
	});
}
