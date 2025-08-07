using System;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000BC4 RID: 3012
[AddComponentMenu("KMonoBehaviour/Workable/Uprootable")]
public class Uprootable : Workable, IDigActionEntity
{
	// Token: 0x1700068C RID: 1676
	// (get) Token: 0x06005A31 RID: 23089 RVA: 0x00209F75 File Offset: 0x00208175
	public bool IsMarkedForUproot
	{
		get
		{
			return this.isMarkedForUproot;
		}
	}

	// Token: 0x06005A32 RID: 23090 RVA: 0x00209F7D File Offset: 0x0020817D
	public bool CanUproot()
	{
		return this.canBeUprooted && !this.uprootComplete;
	}

	// Token: 0x06005A33 RID: 23091 RVA: 0x00209F92 File Offset: 0x00208192
	public static bool CanUproot(GameObject plant, out Uprootable uprootable)
	{
		if (plant == null)
		{
			uprootable = null;
			return false;
		}
		uprootable = plant.GetComponent<Uprootable>();
		return uprootable != null && uprootable.CanUproot();
	}

	// Token: 0x06005A34 RID: 23092 RVA: 0x00209FC0 File Offset: 0x002081C0
	public static bool CanUproot(GameObject plant)
	{
		Uprootable uprootable;
		return Uprootable.CanUproot(plant, out uprootable);
	}

	// Token: 0x1700068D RID: 1677
	// (get) Token: 0x06005A35 RID: 23093 RVA: 0x00209FD5 File Offset: 0x002081D5
	public Storage GetPlanterStorage
	{
		get
		{
			return this.planterStorage;
		}
	}

	// Token: 0x06005A36 RID: 23094 RVA: 0x00209FE0 File Offset: 0x002081E0
	protected Uprootable()
	{
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
		this.buttonLabel = UI.USERMENUACTIONS.UPROOT.NAME;
		this.buttonTooltip = UI.USERMENUACTIONS.UPROOT.TOOLTIP;
		this.cancelButtonLabel = UI.USERMENUACTIONS.CANCELUPROOT.NAME;
		this.cancelButtonTooltip = UI.USERMENUACTIONS.CANCELUPROOT.TOOLTIP;
		this.pendingStatusItem = Db.Get().MiscStatusItems.PendingUproot;
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Uprooting;
	}

	// Token: 0x06005A37 RID: 23095 RVA: 0x0020A080 File Offset: 0x00208280
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.pendingStatusItem = Db.Get().MiscStatusItems.PendingUproot;
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Uprooting;
		this.attributeConverter = Db.Get().AttributeConverters.HarvestSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Farming.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		this.multitoolContext = "harvest";
		this.multitoolHitEffectTag = "fx_harvest_splash";
		base.Subscribe<Uprootable>(1309017699, Uprootable.OnPlanterStorageDelegate);
	}

	// Token: 0x06005A38 RID: 23096 RVA: 0x0020A134 File Offset: 0x00208334
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<Uprootable>(2127324410, Uprootable.ForceCancelUprootDelegate);
		base.SetWorkTime(12.5f);
		base.Subscribe<Uprootable>(2127324410, Uprootable.OnCancelDelegate);
		base.Subscribe<Uprootable>(493375141, Uprootable.OnRefreshUserMenuDelegate);
		this.faceTargetWhenWorking = true;
		Components.Uprootables.Add(this);
		this.area = base.GetComponent<OccupyArea>();
		Prioritizable.AddRef(base.gameObject);
		base.gameObject.AddTag(GameTags.Plant);
		Extents extents = new Extents(Grid.PosToCell(base.gameObject), base.gameObject.GetComponent<OccupyArea>().OccupiedCellsOffsets);
		this.partitionerEntry = GameScenePartitioner.Instance.Add(base.gameObject.name, base.gameObject.GetComponent<KPrefabID>(), extents, GameScenePartitioner.Instance.plants, null);
		GameScenePartitioner.Instance.TriggerEvent(extents, GameScenePartitioner.Instance.plantsChangedLayer, this);
		if (this.isMarkedForUproot)
		{
			this.MarkForUproot(true);
		}
	}

	// Token: 0x06005A39 RID: 23097 RVA: 0x0020A238 File Offset: 0x00208438
	private void OnPlanterStorage(object data)
	{
		this.planterStorage = (Storage)data;
		Prioritizable component = base.GetComponent<Prioritizable>();
		if (component != null)
		{
			component.showIcon = this.planterStorage == null;
		}
	}

	// Token: 0x06005A3A RID: 23098 RVA: 0x0020A273 File Offset: 0x00208473
	public bool IsInPlanterBox()
	{
		return this.planterStorage != null;
	}

	// Token: 0x06005A3B RID: 23099 RVA: 0x0020A284 File Offset: 0x00208484
	public void Uproot()
	{
		this.isMarkedForUproot = false;
		this.chore = null;
		this.uprootComplete = true;
		base.Trigger(-216549700, this);
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.PendingUproot, false);
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.Operating, false);
		Game.Instance.userMenu.Refresh(base.gameObject);
	}

	// Token: 0x06005A3C RID: 23100 RVA: 0x0020A2FF File Offset: 0x002084FF
	public void SetCanBeUprooted(bool state)
	{
		this.canBeUprooted = state;
		if (this.canBeUprooted)
		{
			this.SetUprootedComplete(false);
		}
		Game.Instance.userMenu.Refresh(base.gameObject);
	}

	// Token: 0x06005A3D RID: 23101 RVA: 0x0020A32C File Offset: 0x0020852C
	public void SetUprootedComplete(bool state)
	{
		this.uprootComplete = state;
	}

	// Token: 0x06005A3E RID: 23102 RVA: 0x0020A338 File Offset: 0x00208538
	public void MarkForUproot(bool instantOnDebug = true)
	{
		if (!this.canBeUprooted)
		{
			return;
		}
		if (DebugHandler.InstantBuildMode && instantOnDebug)
		{
			this.Uproot();
		}
		else if (this.chore == null)
		{
			ChoreType choreType = (this.choreTypeIdHash.IsValid ? Db.Get().ChoreTypes.GetByHash(this.choreTypeIdHash) : Db.Get().ChoreTypes.Uproot);
			this.chore = new WorkChore<Uprootable>(choreType, this, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
			base.GetComponent<KSelectable>().AddStatusItem(this.pendingStatusItem, this);
		}
		this.isMarkedForUproot = true;
	}

	// Token: 0x06005A3F RID: 23103 RVA: 0x0020A3D3 File Offset: 0x002085D3
	protected override void OnCompleteWork(WorkerBase worker)
	{
		this.Uproot();
	}

	// Token: 0x06005A40 RID: 23104 RVA: 0x0020A3DC File Offset: 0x002085DC
	private void OnCancel(object data)
	{
		if (this.chore != null)
		{
			this.chore.Cancel("Cancel uproot");
			this.chore = null;
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.PendingUproot, false);
		}
		this.isMarkedForUproot = false;
		this.choreTypeIdHash = HashedString.Invalid;
		Game.Instance.userMenu.Refresh(base.gameObject);
		base.Trigger(1198393204, null);
	}

	// Token: 0x06005A41 RID: 23105 RVA: 0x0020A457 File Offset: 0x00208657
	public bool HasChore()
	{
		return this.chore != null;
	}

	// Token: 0x06005A42 RID: 23106 RVA: 0x0020A464 File Offset: 0x00208664
	private void OnClickUproot()
	{
		this.MarkForUproot(true);
	}

	// Token: 0x06005A43 RID: 23107 RVA: 0x0020A46D File Offset: 0x0020866D
	protected void OnClickCancelUproot()
	{
		this.OnCancel(null);
	}

	// Token: 0x06005A44 RID: 23108 RVA: 0x0020A476 File Offset: 0x00208676
	public virtual void ForceCancelUproot(object data = null)
	{
		this.OnCancel(null);
	}

	// Token: 0x06005A45 RID: 23109 RVA: 0x0020A480 File Offset: 0x00208680
	private void OnRefreshUserMenu(object data)
	{
		if (!this.showUserMenuButtons)
		{
			return;
		}
		if (this.uprootComplete)
		{
			if (this.deselectOnUproot)
			{
				KSelectable component = base.GetComponent<KSelectable>();
				if (component != null && SelectTool.Instance.selected == component)
				{
					SelectTool.Instance.Select(null, false);
				}
			}
			return;
		}
		if (!this.canBeUprooted)
		{
			return;
		}
		KIconButtonMenu.ButtonInfo buttonInfo = ((this.chore != null) ? new KIconButtonMenu.ButtonInfo("action_uproot", this.cancelButtonLabel, new global::System.Action(this.OnClickCancelUproot), global::Action.NumActions, null, null, null, this.cancelButtonTooltip, true) : new KIconButtonMenu.ButtonInfo("action_uproot", this.buttonLabel, new global::System.Action(this.OnClickUproot), global::Action.NumActions, null, null, null, this.buttonTooltip, true));
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
	}

	// Token: 0x06005A46 RID: 23110 RVA: 0x0020A55C File Offset: 0x0020875C
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Extents extents = new Extents(Grid.PosToCell(base.gameObject), base.gameObject.GetComponent<OccupyArea>().OccupiedCellsOffsets);
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		GameScenePartitioner.Instance.TriggerEvent(extents, GameScenePartitioner.Instance.plantsChangedLayer, this);
		Components.Uprootables.Remove(this);
	}

	// Token: 0x06005A47 RID: 23111 RVA: 0x0020A5C2 File Offset: 0x002087C2
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.PendingUproot, false);
	}

	// Token: 0x06005A48 RID: 23112 RVA: 0x0020A5E7 File Offset: 0x002087E7
	public void Dig()
	{
		this.Uproot();
	}

	// Token: 0x06005A49 RID: 23113 RVA: 0x0020A5EF File Offset: 0x002087EF
	public void MarkForDig(bool instantOnDebug = true)
	{
		this.MarkForUproot(instantOnDebug);
	}

	// Token: 0x04003BDD RID: 15325
	[Serialize]
	protected bool isMarkedForUproot;

	// Token: 0x04003BDE RID: 15326
	protected bool uprootComplete;

	// Token: 0x04003BDF RID: 15327
	[MyCmpReq]
	private Prioritizable prioritizable;

	// Token: 0x04003BE0 RID: 15328
	[SerializeField]
	public HashedString choreTypeIdHash;

	// Token: 0x04003BE1 RID: 15329
	[Serialize]
	protected bool canBeUprooted = true;

	// Token: 0x04003BE2 RID: 15330
	public bool deselectOnUproot = true;

	// Token: 0x04003BE3 RID: 15331
	protected Chore chore;

	// Token: 0x04003BE4 RID: 15332
	private string buttonLabel;

	// Token: 0x04003BE5 RID: 15333
	private string buttonTooltip;

	// Token: 0x04003BE6 RID: 15334
	private string cancelButtonLabel;

	// Token: 0x04003BE7 RID: 15335
	private string cancelButtonTooltip;

	// Token: 0x04003BE8 RID: 15336
	private StatusItem pendingStatusItem;

	// Token: 0x04003BE9 RID: 15337
	public OccupyArea area;

	// Token: 0x04003BEA RID: 15338
	private Storage planterStorage;

	// Token: 0x04003BEB RID: 15339
	public bool showUserMenuButtons = true;

	// Token: 0x04003BEC RID: 15340
	public HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04003BED RID: 15341
	private static readonly EventSystem.IntraObjectHandler<Uprootable> OnPlanterStorageDelegate = new EventSystem.IntraObjectHandler<Uprootable>(delegate(Uprootable component, object data)
	{
		component.OnPlanterStorage(data);
	});

	// Token: 0x04003BEE RID: 15342
	private static readonly EventSystem.IntraObjectHandler<Uprootable> ForceCancelUprootDelegate = new EventSystem.IntraObjectHandler<Uprootable>(delegate(Uprootable component, object data)
	{
		component.ForceCancelUproot(data);
	});

	// Token: 0x04003BEF RID: 15343
	private static readonly EventSystem.IntraObjectHandler<Uprootable> OnCancelDelegate = new EventSystem.IntraObjectHandler<Uprootable>(delegate(Uprootable component, object data)
	{
		component.OnCancel(data);
	});

	// Token: 0x04003BF0 RID: 15344
	private static readonly EventSystem.IntraObjectHandler<Uprootable> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<Uprootable>(delegate(Uprootable component, object data)
	{
		component.OnRefreshUserMenu(data);
	});
}
