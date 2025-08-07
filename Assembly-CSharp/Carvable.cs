using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000803 RID: 2051
[AddComponentMenu("KMonoBehaviour/Workable/Carvable")]
public class Carvable : Workable, IDigActionEntity
{
	// Token: 0x170003C1 RID: 961
	// (get) Token: 0x060037C8 RID: 14280 RVA: 0x001355CB File Offset: 0x001337CB
	public bool IsMarkedForCarve
	{
		get
		{
			return this.isMarkedForCarve;
		}
	}

	// Token: 0x060037C9 RID: 14281 RVA: 0x001355D4 File Offset: 0x001337D4
	protected Carvable()
	{
		this.buttonLabel = UI.USERMENUACTIONS.CARVE.NAME;
		this.buttonTooltip = UI.USERMENUACTIONS.CARVE.TOOLTIP;
		this.cancelButtonLabel = UI.USERMENUACTIONS.CANCELCARVE.NAME;
		this.cancelButtonTooltip = UI.USERMENUACTIONS.CANCELCARVE.TOOLTIP;
	}

	// Token: 0x060037CA RID: 14282 RVA: 0x00135630 File Offset: 0x00133830
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.pendingStatusItem = new StatusItem("PendingCarve", "MISC", "status_item_pending_carve", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
		this.workerStatusItem = new StatusItem("Carving", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
		this.workerStatusItem.resolveStringCallback = delegate(string str, object data)
		{
			Workable workable = (Workable)data;
			if (workable != null && workable.GetComponent<KSelectable>() != null)
			{
				str = str.Replace("{Target}", workable.GetComponent<KSelectable>().GetName());
			}
			return str;
		};
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_sculpture_kanim") };
		this.synchronizeAnims = false;
	}

	// Token: 0x060037CB RID: 14283 RVA: 0x001356E4 File Offset: 0x001338E4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.SetWorkTime(10f);
		base.Subscribe<Carvable>(2127324410, Carvable.OnCancelDelegate);
		base.Subscribe<Carvable>(493375141, Carvable.OnRefreshUserMenuDelegate);
		this.faceTargetWhenWorking = true;
		Prioritizable.AddRef(base.gameObject);
		OccupyArea component = base.gameObject.GetComponent<OccupyArea>();
		int num = Grid.PosToCell(this);
		foreach (CellOffset cellOffset in component.OccupiedCellsOffsets)
		{
			Grid.ObjectLayers[5][Grid.OffsetCell(num, cellOffset)] = base.gameObject;
		}
		if (this.isMarkedForCarve)
		{
			this.MarkForCarve(true);
		}
	}

	// Token: 0x060037CC RID: 14284 RVA: 0x0013578C File Offset: 0x0013398C
	public void Carve()
	{
		this.isMarkedForCarve = false;
		this.chore = null;
		base.GetComponent<KSelectable>().RemoveStatusItem(this.pendingStatusItem, false);
		base.GetComponent<KSelectable>().RemoveStatusItem(this.workerStatusItem, false);
		Game.Instance.userMenu.Refresh(base.gameObject);
		this.ProducePickupable(this.dropItemPrefabId);
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060037CD RID: 14285 RVA: 0x001357FC File Offset: 0x001339FC
	public void MarkForCarve(bool instantOnDebug = true)
	{
		if (DebugHandler.InstantBuildMode && instantOnDebug)
		{
			this.Carve();
			return;
		}
		if (this.chore == null)
		{
			this.isMarkedForCarve = true;
			this.chore = new WorkChore<Carvable>(Db.Get().ChoreTypes.Dig, this, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
			this.chore.AddPrecondition(ChorePreconditions.instance.IsNotARobot, null);
			base.GetComponent<KSelectable>().AddStatusItem(this.pendingStatusItem, this);
		}
	}

	// Token: 0x060037CE RID: 14286 RVA: 0x0013587D File Offset: 0x00133A7D
	protected override void OnCompleteWork(WorkerBase worker)
	{
		this.Carve();
	}

	// Token: 0x060037CF RID: 14287 RVA: 0x00135888 File Offset: 0x00133A88
	private void OnCancel(object data)
	{
		if (this.chore != null)
		{
			this.chore.Cancel("Cancel uproot");
			this.chore = null;
			base.GetComponent<KSelectable>().RemoveStatusItem(this.pendingStatusItem, false);
		}
		this.isMarkedForCarve = false;
		Game.Instance.userMenu.Refresh(base.gameObject);
	}

	// Token: 0x060037D0 RID: 14288 RVA: 0x001358E3 File Offset: 0x00133AE3
	private void OnClickCarve()
	{
		this.MarkForCarve(true);
	}

	// Token: 0x060037D1 RID: 14289 RVA: 0x001358EC File Offset: 0x00133AEC
	protected void OnClickCancelCarve()
	{
		this.OnCancel(null);
	}

	// Token: 0x060037D2 RID: 14290 RVA: 0x001358F8 File Offset: 0x00133AF8
	private void OnRefreshUserMenu(object data)
	{
		if (!this.showUserMenuButtons)
		{
			return;
		}
		KIconButtonMenu.ButtonInfo buttonInfo = ((this.chore != null) ? new KIconButtonMenu.ButtonInfo("action_carve", this.cancelButtonLabel, new global::System.Action(this.OnClickCancelCarve), global::Action.NumActions, null, null, null, this.cancelButtonTooltip, true) : new KIconButtonMenu.ButtonInfo("action_carve", this.buttonLabel, new global::System.Action(this.OnClickCarve), global::Action.NumActions, null, null, null, this.buttonTooltip, true));
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
	}

	// Token: 0x060037D3 RID: 14291 RVA: 0x0013598C File Offset: 0x00133B8C
	protected override void OnCleanUp()
	{
		OccupyArea component = base.gameObject.GetComponent<OccupyArea>();
		int num = Grid.PosToCell(this);
		foreach (CellOffset cellOffset in component.OccupiedCellsOffsets)
		{
			if (Grid.ObjectLayers[5][Grid.OffsetCell(num, cellOffset)] == base.gameObject)
			{
				Grid.ObjectLayers[5][Grid.OffsetCell(num, cellOffset)] = null;
			}
		}
		base.OnCleanUp();
	}

	// Token: 0x060037D4 RID: 14292 RVA: 0x00135A01 File Offset: 0x00133C01
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		base.GetComponent<KSelectable>().RemoveStatusItem(this.pendingStatusItem, false);
	}

	// Token: 0x060037D5 RID: 14293 RVA: 0x00135A20 File Offset: 0x00133C20
	private GameObject ProducePickupable(string pickupablePrefabId)
	{
		if (pickupablePrefabId != null)
		{
			Vector3 vector = base.gameObject.transform.GetPosition() + new Vector3(0f, 0.5f, 0f);
			GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab(new Tag(pickupablePrefabId)), vector, Grid.SceneLayer.Ore, null, 0);
			PrimaryElement component = base.gameObject.GetComponent<PrimaryElement>();
			gameObject.GetComponent<PrimaryElement>().Temperature = component.Temperature;
			gameObject.SetActive(true);
			string properName = gameObject.GetProperName();
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, properName, gameObject.transform, 1.5f, false);
			return gameObject;
		}
		return null;
	}

	// Token: 0x060037D6 RID: 14294 RVA: 0x00135AC3 File Offset: 0x00133CC3
	public void Dig()
	{
		this.Carve();
	}

	// Token: 0x060037D7 RID: 14295 RVA: 0x00135ACB File Offset: 0x00133CCB
	public void MarkForDig(bool instantOnDebug = true)
	{
		this.MarkForCarve(instantOnDebug);
	}

	// Token: 0x040021DD RID: 8669
	[Serialize]
	protected bool isMarkedForCarve;

	// Token: 0x040021DE RID: 8670
	protected Chore chore;

	// Token: 0x040021DF RID: 8671
	private string buttonLabel;

	// Token: 0x040021E0 RID: 8672
	private string buttonTooltip;

	// Token: 0x040021E1 RID: 8673
	private string cancelButtonLabel;

	// Token: 0x040021E2 RID: 8674
	private string cancelButtonTooltip;

	// Token: 0x040021E3 RID: 8675
	private StatusItem pendingStatusItem;

	// Token: 0x040021E4 RID: 8676
	public bool showUserMenuButtons = true;

	// Token: 0x040021E5 RID: 8677
	public string dropItemPrefabId;

	// Token: 0x040021E6 RID: 8678
	public HandleVector<int>.Handle partitionerEntry;

	// Token: 0x040021E7 RID: 8679
	private static readonly EventSystem.IntraObjectHandler<Carvable> OnCancelDelegate = new EventSystem.IntraObjectHandler<Carvable>(delegate(Carvable component, object data)
	{
		component.OnCancel(data);
	});

	// Token: 0x040021E8 RID: 8680
	private static readonly EventSystem.IntraObjectHandler<Carvable> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<Carvable>(delegate(Carvable component, object data)
	{
		component.OnRefreshUserMenu(data);
	});
}
