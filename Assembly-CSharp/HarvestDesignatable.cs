using System;
using KSerialization;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200094E RID: 2382
[AddComponentMenu("KMonoBehaviour/scripts/HarvestDesignatable")]
public class HarvestDesignatable : KMonoBehaviour
{
	// Token: 0x170004DD RID: 1245
	// (get) Token: 0x06004432 RID: 17458 RVA: 0x001885E0 File Offset: 0x001867E0
	public bool InPlanterBox
	{
		get
		{
			return this.isInPlanterBox;
		}
	}

	// Token: 0x170004DE RID: 1246
	// (get) Token: 0x06004433 RID: 17459 RVA: 0x001885E8 File Offset: 0x001867E8
	// (set) Token: 0x06004434 RID: 17460 RVA: 0x001885F0 File Offset: 0x001867F0
	public bool MarkedForHarvest
	{
		get
		{
			return this.isMarkedForHarvest;
		}
		set
		{
			this.isMarkedForHarvest = value;
		}
	}

	// Token: 0x170004DF RID: 1247
	// (get) Token: 0x06004435 RID: 17461 RVA: 0x001885F9 File Offset: 0x001867F9
	public bool HarvestWhenReady
	{
		get
		{
			return this.harvestWhenReady;
		}
	}

	// Token: 0x06004436 RID: 17462 RVA: 0x00188601 File Offset: 0x00186801
	protected HarvestDesignatable()
	{
		this.onEnableOverlayDelegate = new Action<object>(this.OnEnableOverlay);
	}

	// Token: 0x06004437 RID: 17463 RVA: 0x00188634 File Offset: 0x00186834
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<HarvestDesignatable>(1309017699, HarvestDesignatable.SetInPlanterBoxTrueDelegate);
	}

	// Token: 0x06004438 RID: 17464 RVA: 0x00188650 File Offset: 0x00186850
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.growingStateManager = base.GetComponent<IManageGrowingStates>();
		if (this.growingStateManager == null)
		{
			this.growingStateManager = base.gameObject.GetSMI<IManageGrowingStates>();
		}
		if (this.isMarkedForHarvest)
		{
			this.MarkForHarvest();
		}
		Components.HarvestDesignatables.Add(this);
		base.Subscribe<HarvestDesignatable>(493375141, HarvestDesignatable.OnRefreshUserMenuDelegate);
		base.Subscribe<HarvestDesignatable>(2127324410, HarvestDesignatable.OnCancelDelegate);
		Game.Instance.Subscribe(1248612973, this.onEnableOverlayDelegate);
		Game.Instance.Subscribe(1798162660, this.onEnableOverlayDelegate);
		Game.Instance.Subscribe(2015652040, new Action<object>(this.OnDisableOverlay));
		Game.Instance.Subscribe(1983128072, new Action<object>(this.RefreshOverlayIcon));
		this.area = base.GetComponent<OccupyArea>();
	}

	// Token: 0x06004439 RID: 17465 RVA: 0x00188734 File Offset: 0x00186934
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.HarvestDesignatables.Remove(this);
		this.DestroyOverlayIcon();
		Game.Instance.Unsubscribe(1248612973, this.onEnableOverlayDelegate);
		Game.Instance.Unsubscribe(2015652040, new Action<object>(this.OnDisableOverlay));
		Game.Instance.Unsubscribe(1798162660, this.onEnableOverlayDelegate);
		Game.Instance.Unsubscribe(1983128072, new Action<object>(this.RefreshOverlayIcon));
	}

	// Token: 0x0600443A RID: 17466 RVA: 0x001887B8 File Offset: 0x001869B8
	private void DestroyOverlayIcon()
	{
		if (this.HarvestWhenReadyOverlayIcon != null)
		{
			global::UnityEngine.Object.Destroy(this.HarvestWhenReadyOverlayIcon.gameObject);
			this.HarvestWhenReadyOverlayIcon = null;
		}
	}

	// Token: 0x0600443B RID: 17467 RVA: 0x001887E0 File Offset: 0x001869E0
	private void CreateOverlayIcon()
	{
		if (this.HarvestWhenReadyOverlayIcon != null)
		{
			return;
		}
		if (base.GetComponent<AttackableBase>() == null)
		{
			this.HarvestWhenReadyOverlayIcon = Util.KInstantiate(Assets.UIPrefabs.HarvestWhenReadyOverlayIcon, GameScreenManager.Instance.worldSpaceCanvas, null).GetComponent<RectTransform>();
			Extents extents = base.GetComponent<OccupyArea>().GetExtents();
			Vector3 vector;
			if (base.GetComponent<KPrefabID>().HasTag(GameTags.Hanging))
			{
				vector = new Vector3((float)(extents.x + extents.width / 2) + 0.5f, (float)(extents.y + extents.height)) + this.iconOffset;
			}
			else
			{
				vector = new Vector3((float)(extents.x + extents.width / 2) + 0.5f, (float)extents.y) + this.iconOffset;
			}
			this.HarvestWhenReadyOverlayIcon.transform.SetPosition(vector);
			this.RefreshOverlayIcon(null);
		}
	}

	// Token: 0x0600443C RID: 17468 RVA: 0x001888D8 File Offset: 0x00186AD8
	private void OnDisableOverlay(object data)
	{
		this.DestroyOverlayIcon();
	}

	// Token: 0x0600443D RID: 17469 RVA: 0x001888E0 File Offset: 0x00186AE0
	private void OnEnableOverlay(object data)
	{
		if ((HashedString)data == OverlayModes.Harvest.ID)
		{
			this.CreateOverlayIcon();
			return;
		}
		this.DestroyOverlayIcon();
	}

	// Token: 0x0600443E RID: 17470 RVA: 0x00188904 File Offset: 0x00186B04
	private void RefreshOverlayIcon(object data = null)
	{
		if (this.HarvestWhenReadyOverlayIcon != null)
		{
			if ((Grid.IsVisible(Grid.PosToCell(base.gameObject)) && base.gameObject.GetMyWorldId() == ClusterManager.Instance.activeWorldId) || (CameraController.Instance != null && CameraController.Instance.FreeCameraEnabled))
			{
				if (!this.HarvestWhenReadyOverlayIcon.gameObject.activeSelf)
				{
					this.HarvestWhenReadyOverlayIcon.gameObject.SetActive(true);
				}
			}
			else if (this.HarvestWhenReadyOverlayIcon.gameObject.activeSelf)
			{
				this.HarvestWhenReadyOverlayIcon.gameObject.SetActive(false);
			}
			HierarchyReferences component = this.HarvestWhenReadyOverlayIcon.GetComponent<HierarchyReferences>();
			if (this.harvestWhenReady)
			{
				Image image = (Image)component.GetReference("On");
				image.gameObject.SetActive(true);
				image.color = GlobalAssets.Instance.colorSet.harvestEnabled;
				component.GetReference("Off").gameObject.SetActive(false);
				return;
			}
			component.GetReference("On").gameObject.SetActive(false);
			Image image2 = (Image)component.GetReference("Off");
			image2.gameObject.SetActive(true);
			image2.color = GlobalAssets.Instance.colorSet.harvestDisabled;
		}
	}

	// Token: 0x0600443F RID: 17471 RVA: 0x00188A58 File Offset: 0x00186C58
	public bool CanBeHarvested()
	{
		Harvestable component = base.GetComponent<Harvestable>();
		return !(component != null) || component.CanBeHarvested;
	}

	// Token: 0x06004440 RID: 17472 RVA: 0x00188A7D File Offset: 0x00186C7D
	public void SetInPlanterBox(bool state)
	{
		if (state)
		{
			if (!this.isInPlanterBox)
			{
				this.isInPlanterBox = true;
				this.SetHarvestWhenReady(this.defaultHarvestStateWhenPlanted);
				return;
			}
		}
		else
		{
			this.isInPlanterBox = false;
		}
	}

	// Token: 0x06004441 RID: 17473 RVA: 0x00188AA8 File Offset: 0x00186CA8
	public void SetHarvestWhenReady(bool state)
	{
		this.harvestWhenReady = state;
		if (this.harvestWhenReady && this.CanBeHarvested() && !this.isMarkedForHarvest)
		{
			this.MarkForHarvest();
		}
		if (this.isMarkedForHarvest && !this.harvestWhenReady)
		{
			this.OnCancel(null);
			if (this.CanBeHarvested() && this.isInPlanterBox)
			{
				base.GetComponent<KSelectable>().AddStatusItem(Db.Get().MiscStatusItems.NotMarkedForHarvest, this);
			}
		}
		base.Trigger(-266953818, null);
		this.RefreshOverlayIcon(null);
	}

	// Token: 0x06004442 RID: 17474 RVA: 0x00188B30 File Offset: 0x00186D30
	protected virtual void OnCancel(object data = null)
	{
	}

	// Token: 0x06004443 RID: 17475 RVA: 0x00188B34 File Offset: 0x00186D34
	public virtual void MarkForHarvest()
	{
		if (!this.CanBeHarvested())
		{
			return;
		}
		this.isMarkedForHarvest = true;
		Harvestable component = base.GetComponent<Harvestable>();
		if (component != null)
		{
			component.OnMarkedForHarvest();
		}
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.NotMarkedForHarvest, false);
	}

	// Token: 0x06004444 RID: 17476 RVA: 0x00188B83 File Offset: 0x00186D83
	protected virtual void OnClickHarvestWhenReady()
	{
		this.SetHarvestWhenReady(true);
	}

	// Token: 0x06004445 RID: 17477 RVA: 0x00188B8C File Offset: 0x00186D8C
	protected virtual void OnClickCancelHarvestWhenReady()
	{
		Harvestable component = base.GetComponent<Harvestable>();
		if (component != null)
		{
			component.Trigger(2127324410, null);
		}
		this.SetHarvestWhenReady(false);
	}

	// Token: 0x06004446 RID: 17478 RVA: 0x00188BBC File Offset: 0x00186DBC
	public virtual void OnRefreshUserMenu(object data)
	{
		if (this.showUserMenuButtons)
		{
			KIconButtonMenu.ButtonInfo buttonInfo = (this.harvestWhenReady ? new KIconButtonMenu.ButtonInfo("action_harvest", UI.USERMENUACTIONS.CANCEL_HARVEST_WHEN_READY.NAME, delegate
			{
				this.OnClickCancelHarvestWhenReady();
				PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, UI.GAMEOBJECTEFFECTS.PLANT_DO_NOT_HARVEST, base.transform, 1.5f, false);
			}, global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.CANCEL_HARVEST_WHEN_READY.TOOLTIP, true) : new KIconButtonMenu.ButtonInfo("action_harvest", UI.USERMENUACTIONS.HARVEST_WHEN_READY.NAME, delegate
			{
				this.OnClickHarvestWhenReady();
				PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, UI.GAMEOBJECTEFFECTS.PLANT_MARK_FOR_HARVEST, base.transform, 1.5f, false);
			}, global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.HARVEST_WHEN_READY.TOOLTIP, true));
			Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
		}
	}

	// Token: 0x04002DAB RID: 11691
	public Vector2 iconOffset = Vector2.zero;

	// Token: 0x04002DAC RID: 11692
	public bool defaultHarvestStateWhenPlanted = true;

	// Token: 0x04002DAD RID: 11693
	public OccupyArea area;

	// Token: 0x04002DAE RID: 11694
	[Serialize]
	protected bool isMarkedForHarvest;

	// Token: 0x04002DAF RID: 11695
	[Serialize]
	private bool isInPlanterBox;

	// Token: 0x04002DB0 RID: 11696
	public bool showUserMenuButtons = true;

	// Token: 0x04002DB1 RID: 11697
	public IManageGrowingStates growingStateManager;

	// Token: 0x04002DB2 RID: 11698
	[Serialize]
	protected bool harvestWhenReady;

	// Token: 0x04002DB3 RID: 11699
	public RectTransform HarvestWhenReadyOverlayIcon;

	// Token: 0x04002DB4 RID: 11700
	private Action<object> onEnableOverlayDelegate;

	// Token: 0x04002DB5 RID: 11701
	private static readonly EventSystem.IntraObjectHandler<HarvestDesignatable> OnCancelDelegate = new EventSystem.IntraObjectHandler<HarvestDesignatable>(delegate(HarvestDesignatable component, object data)
	{
		component.OnCancel(data);
	});

	// Token: 0x04002DB6 RID: 11702
	private static readonly EventSystem.IntraObjectHandler<HarvestDesignatable> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<HarvestDesignatable>(delegate(HarvestDesignatable component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x04002DB7 RID: 11703
	private static readonly EventSystem.IntraObjectHandler<HarvestDesignatable> SetInPlanterBoxTrueDelegate = new EventSystem.IntraObjectHandler<HarvestDesignatable>(delegate(HarvestDesignatable component, object data)
	{
		component.SetInPlanterBox(true);
	});
}
