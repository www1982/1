using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000C35 RID: 3125
public class RootMenu : KScreen
{
	// Token: 0x06005EF1 RID: 24305 RVA: 0x0022CBBA File Offset: 0x0022ADBA
	public static void DestroyInstance()
	{
		RootMenu.Instance = null;
	}

	// Token: 0x170006E7 RID: 1767
	// (get) Token: 0x06005EF2 RID: 24306 RVA: 0x0022CBC2 File Offset: 0x0022ADC2
	// (set) Token: 0x06005EF3 RID: 24307 RVA: 0x0022CBC9 File Offset: 0x0022ADC9
	public static RootMenu Instance { get; private set; }

	// Token: 0x06005EF4 RID: 24308 RVA: 0x0022CBD1 File Offset: 0x0022ADD1
	public override float GetSortKey()
	{
		return -1f;
	}

	// Token: 0x06005EF5 RID: 24309 RVA: 0x0022CBD8 File Offset: 0x0022ADD8
	protected override void OnPrefabInit()
	{
		RootMenu.Instance = this;
		base.Subscribe(Game.Instance.gameObject, -1503271301, new Action<object>(this.OnSelectObject));
		base.Subscribe(Game.Instance.gameObject, 288942073, new Action<object>(this.OnUIClear));
		base.Subscribe(Game.Instance.gameObject, -809948329, new Action<object>(this.OnBuildingStatechanged));
		base.OnPrefabInit();
	}

	// Token: 0x06005EF6 RID: 24310 RVA: 0x0022CC58 File Offset: 0x0022AE58
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.detailsScreen = Util.KInstantiateUI(this.detailsScreenPrefab, base.gameObject, true).GetComponent<DetailsScreen>();
		this.detailsScreen.gameObject.SetActive(true);
		this.userMenuParent = this.detailsScreen.UserMenuPanel.gameObject;
		this.userMenu = Util.KInstantiateUI(this.userMenuPrefab.gameObject, this.userMenuParent, false).GetComponent<UserMenuScreen>();
		this.detailsScreen.gameObject.SetActive(false);
		this.userMenu.gameObject.SetActive(false);
	}

	// Token: 0x06005EF7 RID: 24311 RVA: 0x0022CCF3 File Offset: 0x0022AEF3
	private void OnClickCommon()
	{
		this.CloseSubMenus();
	}

	// Token: 0x06005EF8 RID: 24312 RVA: 0x0022CCFB File Offset: 0x0022AEFB
	public void AddSubMenu(KScreen sub_menu)
	{
		if (sub_menu.activateOnSpawn)
		{
			sub_menu.Show(true);
		}
		this.subMenus.Add(sub_menu);
	}

	// Token: 0x06005EF9 RID: 24313 RVA: 0x0022CD18 File Offset: 0x0022AF18
	public void RemoveSubMenu(KScreen sub_menu)
	{
		this.subMenus.Remove(sub_menu);
	}

	// Token: 0x06005EFA RID: 24314 RVA: 0x0022CD28 File Offset: 0x0022AF28
	private void CloseSubMenus()
	{
		foreach (KScreen kscreen in this.subMenus)
		{
			if (kscreen != null)
			{
				if (kscreen.activateOnSpawn)
				{
					kscreen.gameObject.SetActive(false);
				}
				else
				{
					kscreen.Deactivate();
				}
			}
		}
		this.subMenus.Clear();
	}

	// Token: 0x06005EFB RID: 24315 RVA: 0x0022CDA4 File Offset: 0x0022AFA4
	private void OnSelectObject(object data)
	{
		GameObject gameObject = (GameObject)data;
		bool flag = false;
		if (gameObject != null)
		{
			KPrefabID component = gameObject.GetComponent<KPrefabID>();
			if (component != null && !component.IsInitialized())
			{
				return;
			}
			flag = component != null || CellSelectionObject.IsSelectionObject(gameObject);
		}
		if (gameObject != this.selectedGO)
		{
			if (this.selectedGO != null)
			{
				this.selectedGO.Unsubscribe(1980521255, new Action<object>(this.TriggerRefresh));
			}
			this.selectedGO = null;
			this.CloseSubMenus();
			if (flag)
			{
				this.selectedGO = gameObject;
				this.selectedGO.Subscribe(1980521255, new Action<object>(this.TriggerRefresh));
				this.AddSubMenu(this.detailsScreen);
				this.AddSubMenu(this.userMenu);
			}
			this.userMenu.SetSelected(this.selectedGO);
		}
		this.Refresh();
	}

	// Token: 0x06005EFC RID: 24316 RVA: 0x0022CE8D File Offset: 0x0022B08D
	public void TriggerRefresh(object obj)
	{
		this.Refresh();
	}

	// Token: 0x06005EFD RID: 24317 RVA: 0x0022CE95 File Offset: 0x0022B095
	public void Refresh()
	{
		if (this.selectedGO == null)
		{
			return;
		}
		this.detailsScreen.Refresh(this.selectedGO);
		this.userMenu.Refresh(this.selectedGO);
	}

	// Token: 0x06005EFE RID: 24318 RVA: 0x0022CEC8 File Offset: 0x0022B0C8
	private void OnBuildingStatechanged(object data)
	{
		GameObject gameObject = (GameObject)data;
		if (gameObject == this.selectedGO)
		{
			this.OnSelectObject(gameObject);
		}
	}

	// Token: 0x06005EFF RID: 24319 RVA: 0x0022CEF4 File Offset: 0x0022B0F4
	public override void OnKeyDown(KButtonEvent e)
	{
		if (!e.Consumed && e.TryConsume(global::Action.Escape) && SelectTool.Instance.enabled)
		{
			if (!this.canTogglePauseScreen)
			{
				return;
			}
			if (this.AreSubMenusOpen())
			{
				KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Back", false));
				this.CloseSubMenus();
				SelectTool.Instance.Select(null, false);
			}
			else if (e.IsAction(global::Action.Escape))
			{
				if (!SelectTool.Instance.enabled)
				{
					KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Close", false));
				}
				if (PlayerController.Instance.IsUsingDefaultTool())
				{
					if (SelectTool.Instance.selected != null)
					{
						SelectTool.Instance.Select(null, false);
					}
					else
					{
						CameraController.Instance.ForcePanningState(false);
						this.TogglePauseScreen();
					}
				}
				else
				{
					Game.Instance.Trigger(288942073, null);
				}
				ToolMenu.Instance.ClearSelection();
				SelectTool.Instance.Activate();
			}
		}
		base.OnKeyDown(e);
	}

	// Token: 0x06005F00 RID: 24320 RVA: 0x0022CFF6 File Offset: 0x0022B1F6
	public override void OnKeyUp(KButtonEvent e)
	{
		base.OnKeyUp(e);
		if (!e.Consumed && e.TryConsume(global::Action.AlternateView) && this.tileScreenInst != null)
		{
			this.tileScreenInst.Deactivate();
			this.tileScreenInst = null;
		}
	}

	// Token: 0x06005F01 RID: 24321 RVA: 0x0022D031 File Offset: 0x0022B231
	public void TogglePauseScreen()
	{
		PauseScreen.Instance.Show(true);
	}

	// Token: 0x06005F02 RID: 24322 RVA: 0x0022D03E File Offset: 0x0022B23E
	public void ExternalClose()
	{
		this.OnClickCommon();
	}

	// Token: 0x06005F03 RID: 24323 RVA: 0x0022D046 File Offset: 0x0022B246
	private void OnUIClear(object data)
	{
		this.CloseSubMenus();
		SelectTool.Instance.Select(null, true);
		if (global::UnityEngine.EventSystems.EventSystem.current != null)
		{
			global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
			return;
		}
		global::Debug.LogWarning("OnUIClear() Event system is null");
	}

	// Token: 0x06005F04 RID: 24324 RVA: 0x0022D07D File Offset: 0x0022B27D
	protected override void OnActivate()
	{
		base.OnActivate();
	}

	// Token: 0x06005F05 RID: 24325 RVA: 0x0022D085 File Offset: 0x0022B285
	private bool AreSubMenusOpen()
	{
		return this.subMenus.Count > 0;
	}

	// Token: 0x06005F06 RID: 24326 RVA: 0x0022D098 File Offset: 0x0022B298
	private KToggleMenu.ToggleInfo[] GetFillers()
	{
		HashSet<Tag> hashSet = new HashSet<Tag>();
		List<KToggleMenu.ToggleInfo> list = new List<KToggleMenu.ToggleInfo>();
		foreach (Pickupable pickupable in Components.Pickupables.Items)
		{
			KPrefabID kprefabID = pickupable.KPrefabID;
			if (kprefabID.HasTag(GameTags.Filler) && hashSet.Add(kprefabID.PrefabTag))
			{
				string text = kprefabID.GetComponent<PrimaryElement>().Element.id.ToString();
				list.Add(new KToggleMenu.ToggleInfo(text, null, global::Action.NumActions));
			}
		}
		return list.ToArray();
	}

	// Token: 0x06005F07 RID: 24327 RVA: 0x0022D14C File Offset: 0x0022B34C
	public bool IsBuildingChorePanelActive()
	{
		return this.detailsScreen != null && this.detailsScreen.GetActiveTab() is BuildingChoresPanel;
	}

	// Token: 0x04003F4E RID: 16206
	private DetailsScreen detailsScreen;

	// Token: 0x04003F4F RID: 16207
	private UserMenuScreen userMenu;

	// Token: 0x04003F50 RID: 16208
	[SerializeField]
	private GameObject detailsScreenPrefab;

	// Token: 0x04003F51 RID: 16209
	[SerializeField]
	private UserMenuScreen userMenuPrefab;

	// Token: 0x04003F52 RID: 16210
	private GameObject userMenuParent;

	// Token: 0x04003F53 RID: 16211
	[SerializeField]
	private TileScreen tileScreen;

	// Token: 0x04003F55 RID: 16213
	public KScreen buildMenu;

	// Token: 0x04003F56 RID: 16214
	private List<KScreen> subMenus = new List<KScreen>();

	// Token: 0x04003F57 RID: 16215
	private TileScreen tileScreenInst;

	// Token: 0x04003F58 RID: 16216
	public bool canTogglePauseScreen = true;

	// Token: 0x04003F59 RID: 16217
	public GameObject selectedGO;
}
