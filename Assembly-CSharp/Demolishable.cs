using System;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020005A9 RID: 1449
[RequireComponent(typeof(Prioritizable))]
public class Demolishable : Workable
{
	// Token: 0x17000148 RID: 328
	// (get) Token: 0x06002127 RID: 8487 RVA: 0x000BF795 File Offset: 0x000BD995
	public bool HasBeenDestroyed
	{
		get
		{
			return this.destroyed;
		}
	}

	// Token: 0x17000149 RID: 329
	// (get) Token: 0x06002128 RID: 8488 RVA: 0x000BF7A0 File Offset: 0x000BD9A0
	private CellOffset[] placementOffsets
	{
		get
		{
			Building component = base.GetComponent<Building>();
			if (component != null)
			{
				return component.Def.PlacementOffsets;
			}
			OccupyArea component2 = base.GetComponent<OccupyArea>();
			if (component2 != null)
			{
				return component2.OccupiedCellsOffsets;
			}
			global::Debug.Assert(false, "Ack! We put a Demolishable on something that's neither a Building nor OccupyArea!", this);
			return null;
		}
	}

	// Token: 0x06002129 RID: 8489 RVA: 0x000BF7F0 File Offset: 0x000BD9F0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.requiredSkillPerk = Db.Get().SkillPerks.CanDemolish.Id;
		this.faceTargetWhenWorking = true;
		this.synchronizeAnims = false;
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Deconstructing;
		this.attributeConverter = Db.Get().AttributeConverters.ConstructionSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.MOST_DAY_EXPERIENCE;
		this.minimumAttributeMultiplier = 0.75f;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Building.Id;
		this.skillExperienceMultiplier = SKILLS.MOST_DAY_EXPERIENCE;
		this.multitoolContext = "demolish";
		this.multitoolHitEffectTag = EffectConfigs.DemolishSplashId;
		this.workingPstComplete = null;
		this.workingPstFailed = null;
		Building component = base.GetComponent<Building>();
		if (component != null && component.Def.IsTilePiece)
		{
			base.SetWorkTime(component.Def.ConstructionTime * 0.5f);
			return;
		}
		base.SetWorkTime(30f);
	}

	// Token: 0x0600212A RID: 8490 RVA: 0x000BF900 File Offset: 0x000BDB00
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<Demolishable>(493375141, Demolishable.OnRefreshUserMenuDelegate);
		base.Subscribe<Demolishable>(-111137758, Demolishable.OnRefreshUserMenuDelegate);
		base.Subscribe<Demolishable>(2127324410, Demolishable.OnCancelDelegate);
		base.Subscribe<Demolishable>(-790448070, Demolishable.OnDeconstructDelegate);
		CellOffset[][] array = OffsetGroups.InvertedStandardTable;
		CellOffset[] array2 = null;
		Building component = base.GetComponent<Building>();
		if (component != null && component.Def.IsTilePiece)
		{
			array = OffsetGroups.InvertedStandardTableWithCorners;
			array2 = component.Def.ConstructionOffsetFilter;
		}
		CellOffset[][] array3 = OffsetGroups.BuildReachabilityTable(this.placementOffsets, array, array2);
		base.SetOffsetTable(array3);
		if (this.isMarkedForDemolition)
		{
			this.QueueDemolition();
		}
	}

	// Token: 0x0600212B RID: 8491 RVA: 0x000BF9B1 File Offset: 0x000BDBB1
	protected override void OnStartWork(WorkerBase worker)
	{
		this.progressBar.barColor = ProgressBarsConfig.Instance.GetBarColor("DeconstructBar");
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.PendingDemolition, false);
	}

	// Token: 0x0600212C RID: 8492 RVA: 0x000BF9E9 File Offset: 0x000BDBE9
	protected override void OnCompleteWork(WorkerBase worker)
	{
		this.TriggerDestroy();
	}

	// Token: 0x0600212D RID: 8493 RVA: 0x000BF9F1 File Offset: 0x000BDBF1
	private void TriggerDestroy()
	{
		if (this == null || this.destroyed)
		{
			return;
		}
		this.destroyed = true;
		this.isMarkedForDemolition = false;
		base.gameObject.DeleteObject();
	}

	// Token: 0x0600212E RID: 8494 RVA: 0x000BFA20 File Offset: 0x000BDC20
	private void QueueDemolition()
	{
		if (DebugHandler.InstantBuildMode)
		{
			this.OnCompleteWork(null);
			return;
		}
		if (this.chore == null)
		{
			Prioritizable.AddRef(base.gameObject);
			this.requiredSkillPerk = Db.Get().SkillPerks.CanDemolish.Id;
			this.chore = new WorkChore<Demolishable>(Db.Get().ChoreTypes.Demolish, this, null, true, null, null, null, true, null, false, false, Assets.GetAnim("anim_interacts_clothingfactory_kanim"), true, true, true, PriorityScreen.PriorityClass.basic, 5, true, true);
			base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.PendingDemolition, this);
			this.isMarkedForDemolition = true;
			base.Trigger(2108245096, "Demolish");
		}
		this.UpdateStatusItem(null);
	}

	// Token: 0x0600212F RID: 8495 RVA: 0x000BFAE0 File Offset: 0x000BDCE0
	private void OnRefreshUserMenu(object data)
	{
		if (!this.allowDemolition)
		{
			return;
		}
		KIconButtonMenu.ButtonInfo buttonInfo = ((this.chore == null) ? new KIconButtonMenu.ButtonInfo("action_deconstruct", UI.USERMENUACTIONS.DEMOLISH.NAME, new global::System.Action(this.OnDemolish), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.DEMOLISH.TOOLTIP, true) : new KIconButtonMenu.ButtonInfo("action_deconstruct", UI.USERMENUACTIONS.DEMOLISH.NAME_OFF, new global::System.Action(this.OnDemolish), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.DEMOLISH.TOOLTIP_OFF, true));
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 0f);
	}

	// Token: 0x06002130 RID: 8496 RVA: 0x000BFB84 File Offset: 0x000BDD84
	public void CancelDemolition()
	{
		if (this.chore != null)
		{
			this.chore.Cancel("Cancelled demolition");
			this.chore = null;
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.PendingDemolition, false);
			base.ShowProgressBar(false);
			this.isMarkedForDemolition = false;
			Prioritizable.RemoveRef(base.gameObject);
		}
		this.UpdateStatusItem(null);
	}

	// Token: 0x06002131 RID: 8497 RVA: 0x000BFBEC File Offset: 0x000BDDEC
	private void OnCancel(object data)
	{
		this.CancelDemolition();
	}

	// Token: 0x06002132 RID: 8498 RVA: 0x000BFBF4 File Offset: 0x000BDDF4
	private void OnDemolish(object data)
	{
		if (this.allowDemolition || DebugHandler.InstantBuildMode)
		{
			this.QueueDemolition();
		}
	}

	// Token: 0x06002133 RID: 8499 RVA: 0x000BFC0B File Offset: 0x000BDE0B
	private void OnDemolish()
	{
		if (this.chore == null)
		{
			this.QueueDemolition();
			return;
		}
		this.CancelDemolition();
	}

	// Token: 0x06002134 RID: 8500 RVA: 0x000BFC22 File Offset: 0x000BDE22
	protected override void UpdateStatusItem(object data = null)
	{
		this.shouldShowSkillPerkStatusItem = this.isMarkedForDemolition;
		base.UpdateStatusItem(data);
	}

	// Token: 0x04001353 RID: 4947
	public Chore chore;

	// Token: 0x04001354 RID: 4948
	public bool allowDemolition = true;

	// Token: 0x04001355 RID: 4949
	[Serialize]
	private bool isMarkedForDemolition;

	// Token: 0x04001356 RID: 4950
	private bool destroyed;

	// Token: 0x04001357 RID: 4951
	private static readonly EventSystem.IntraObjectHandler<Demolishable> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<Demolishable>(delegate(Demolishable component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x04001358 RID: 4952
	private static readonly EventSystem.IntraObjectHandler<Demolishable> OnCancelDelegate = new EventSystem.IntraObjectHandler<Demolishable>(delegate(Demolishable component, object data)
	{
		component.OnCancel(data);
	});

	// Token: 0x04001359 RID: 4953
	private static readonly EventSystem.IntraObjectHandler<Demolishable> OnDeconstructDelegate = new EventSystem.IntraObjectHandler<Demolishable>(delegate(Demolishable component, object data)
	{
		component.OnDemolish(data);
	});
}
