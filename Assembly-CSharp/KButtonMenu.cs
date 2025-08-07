using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000CF5 RID: 3317
public class KButtonMenu : KScreen
{
	// Token: 0x06006626 RID: 26150 RVA: 0x00269369 File Offset: 0x00267569
	protected override void OnActivate()
	{
		base.ConsumeMouseScroll = this.ShouldConsumeMouseScroll;
		this.RefreshButtons();
	}

	// Token: 0x06006627 RID: 26151 RVA: 0x0026937D File Offset: 0x0026757D
	public void SetButtons(IList<KButtonMenu.ButtonInfo> buttons)
	{
		this.buttons = buttons;
		if (this.activateOnSpawn)
		{
			this.RefreshButtons();
		}
	}

	// Token: 0x06006628 RID: 26152 RVA: 0x00269394 File Offset: 0x00267594
	public virtual void RefreshButtons()
	{
		if (this.buttonObjects != null)
		{
			for (int i = 0; i < this.buttonObjects.Length; i++)
			{
				global::UnityEngine.Object.Destroy(this.buttonObjects[i]);
			}
			this.buttonObjects = null;
		}
		if (this.buttons == null)
		{
			return;
		}
		this.buttonObjects = new GameObject[this.buttons.Count];
		for (int j = 0; j < this.buttons.Count; j++)
		{
			KButtonMenu.ButtonInfo binfo = this.buttons[j];
			GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.buttonPrefab, Vector3.zero, Quaternion.identity);
			this.buttonObjects[j] = gameObject;
			Transform transform = ((this.buttonParent != null) ? this.buttonParent : base.transform);
			gameObject.transform.SetParent(transform, false);
			gameObject.SetActive(true);
			gameObject.name = binfo.text + "Button";
			LocText[] componentsInChildren = gameObject.GetComponentsInChildren<LocText>(true);
			if (componentsInChildren != null)
			{
				foreach (LocText locText in componentsInChildren)
				{
					locText.text = ((locText.name == "Hotkey") ? GameUtil.GetActionString(binfo.shortcutKey) : binfo.text);
					locText.color = (binfo.isEnabled ? new Color(1f, 1f, 1f) : new Color(0.5f, 0.5f, 0.5f));
				}
			}
			ToolTip componentInChildren = gameObject.GetComponentInChildren<ToolTip>();
			if (binfo.toolTip != null && binfo.toolTip != "" && componentInChildren != null)
			{
				componentInChildren.toolTip = binfo.toolTip;
			}
			KButtonMenu screen = this;
			KButton button = gameObject.GetComponent<KButton>();
			button.isInteractable = binfo.isEnabled;
			if (binfo.popupOptions == null && binfo.onPopulatePopup == null)
			{
				UnityAction onClick = binfo.onClick;
				global::System.Action action = delegate
				{
					onClick();
					if (!this.keepMenuOpen && screen != null)
					{
						screen.Deactivate();
					}
				};
				button.onClick += action;
			}
			else
			{
				button.onClick += delegate
				{
					this.SetupPopupMenu(binfo, button);
				};
			}
			binfo.uibutton = button;
			KButtonMenu.ButtonInfo.HoverCallback onHover = binfo.onHover;
		}
		this.Update();
	}

	// Token: 0x06006629 RID: 26153 RVA: 0x00269648 File Offset: 0x00267848
	protected Button.ButtonClickedEvent SetupPopupMenu(KButtonMenu.ButtonInfo binfo, KButton button)
	{
		Button.ButtonClickedEvent buttonClickedEvent = new Button.ButtonClickedEvent();
		UnityAction unityAction = delegate
		{
			List<KButtonMenu.ButtonInfo> list = new List<KButtonMenu.ButtonInfo>();
			if (binfo.onPopulatePopup != null)
			{
				binfo.popupOptions = binfo.onPopulatePopup();
			}
			string[] popupOptions = binfo.popupOptions;
			for (int i = 0; i < popupOptions.Length; i++)
			{
				string text = popupOptions[i];
				string delegate_str = text;
				list.Add(new KButtonMenu.ButtonInfo(delegate_str, delegate
				{
					binfo.onPopupClick(delegate_str);
					if (!this.keepMenuOpen)
					{
						this.Deactivate();
					}
				}, global::Action.NumActions, null, null, null, true, null, null, null));
			}
			KButtonMenu component = Util.KInstantiate(ScreenPrefabs.Instance.ButtonGrid.gameObject, null, null).GetComponent<KButtonMenu>();
			component.SetButtons(list.ToArray());
			RootMenu.Instance.AddSubMenu(component);
			Game.Instance.LocalPlayer.ScreenManager.ActivateScreen(component.gameObject, null, GameScreenManager.UIRenderTarget.ScreenSpaceOverlay);
			Vector3 vector = default(Vector3);
			if (Util.IsOnLeftSideOfScreen(button.transform.GetPosition()))
			{
				vector.x = button.GetComponent<RectTransform>().rect.width * 0.25f;
			}
			else
			{
				vector.x = -button.GetComponent<RectTransform>().rect.width * 0.25f;
			}
			component.transform.SetPosition(button.transform.GetPosition() + vector);
		};
		binfo.onClick = unityAction;
		buttonClickedEvent.AddListener(unityAction);
		return buttonClickedEvent;
	}

	// Token: 0x0600662A RID: 26154 RVA: 0x00269698 File Offset: 0x00267898
	public override void OnKeyDown(KButtonEvent e)
	{
		if (this.buttons == null)
		{
			return;
		}
		for (int i = 0; i < this.buttons.Count; i++)
		{
			KButtonMenu.ButtonInfo buttonInfo = this.buttons[i];
			if (e.TryConsume(buttonInfo.shortcutKey))
			{
				this.buttonObjects[i].GetComponent<KButton>().PlayPointerDownSound();
				this.buttonObjects[i].GetComponent<KButton>().SignalClick(KKeyCode.Mouse0);
				break;
			}
		}
		base.OnKeyDown(e);
	}

	// Token: 0x0600662B RID: 26155 RVA: 0x00269711 File Offset: 0x00267911
	protected override void OnPrefabInit()
	{
		base.Subscribe<KButtonMenu>(315865555, KButtonMenu.OnSetActivatorDelegate);
	}

	// Token: 0x0600662C RID: 26156 RVA: 0x00269724 File Offset: 0x00267924
	private void OnSetActivator(object data)
	{
		this.go = (GameObject)data;
		this.Update();
	}

	// Token: 0x0600662D RID: 26157 RVA: 0x00269738 File Offset: 0x00267938
	protected override void OnDeactivate()
	{
	}

	// Token: 0x0600662E RID: 26158 RVA: 0x0026973C File Offset: 0x0026793C
	private void Update()
	{
		if (!this.followGameObject || this.go == null || base.canvas == null)
		{
			return;
		}
		Vector3 vector = Camera.main.WorldToViewportPoint(this.go.transform.GetPosition());
		RectTransform component = base.GetComponent<RectTransform>();
		RectTransform component2 = base.canvas.GetComponent<RectTransform>();
		if (component != null)
		{
			component.anchoredPosition = new Vector2(vector.x * component2.sizeDelta.x - component2.sizeDelta.x * 0.5f, vector.y * component2.sizeDelta.y - component2.sizeDelta.y * 0.5f);
		}
	}

	// Token: 0x040045FC RID: 17916
	[SerializeField]
	protected bool followGameObject;

	// Token: 0x040045FD RID: 17917
	[SerializeField]
	protected bool keepMenuOpen;

	// Token: 0x040045FE RID: 17918
	[SerializeField]
	protected Transform buttonParent;

	// Token: 0x040045FF RID: 17919
	public GameObject buttonPrefab;

	// Token: 0x04004600 RID: 17920
	public bool ShouldConsumeMouseScroll;

	// Token: 0x04004601 RID: 17921
	[NonSerialized]
	public GameObject[] buttonObjects;

	// Token: 0x04004602 RID: 17922
	protected GameObject go;

	// Token: 0x04004603 RID: 17923
	protected IList<KButtonMenu.ButtonInfo> buttons;

	// Token: 0x04004604 RID: 17924
	private static readonly EventSystem.IntraObjectHandler<KButtonMenu> OnSetActivatorDelegate = new EventSystem.IntraObjectHandler<KButtonMenu>(delegate(KButtonMenu component, object data)
	{
		component.OnSetActivator(data);
	});

	// Token: 0x02001EB8 RID: 7864
	public class ButtonInfo
	{
		// Token: 0x0600B132 RID: 45362 RVA: 0x003D4228 File Offset: 0x003D2428
		public ButtonInfo(string text = null, UnityAction on_click = null, global::Action shortcut_key = global::Action.NumActions, KButtonMenu.ButtonInfo.HoverCallback on_hover = null, string tool_tip = null, GameObject visualizer = null, bool is_enabled = true, string[] popup_options = null, Action<string> on_popup_click = null, Func<string[]> on_populate_popup = null)
		{
			this.text = text;
			this.shortcutKey = shortcut_key;
			this.onClick = on_click;
			this.onHover = on_hover;
			this.visualizer = visualizer;
			this.toolTip = tool_tip;
			this.isEnabled = is_enabled;
			this.uibutton = null;
			this.popupOptions = popup_options;
			this.onPopupClick = on_popup_click;
			this.onPopulatePopup = on_populate_popup;
		}

		// Token: 0x0600B133 RID: 45363 RVA: 0x003D4298 File Offset: 0x003D2498
		public ButtonInfo(string text, global::Action shortcutKey, UnityAction onClick, KButtonMenu.ButtonInfo.HoverCallback onHover = null, object userData = null)
		{
			this.text = text;
			this.shortcutKey = shortcutKey;
			this.onClick = onClick;
			this.onHover = onHover;
			this.userData = userData;
			this.visualizer = null;
			this.uibutton = null;
		}

		// Token: 0x0600B134 RID: 45364 RVA: 0x003D42E8 File Offset: 0x003D24E8
		public ButtonInfo(string text, GameObject visualizer, global::Action shortcutKey, UnityAction onClick, KButtonMenu.ButtonInfo.HoverCallback onHover = null, object userData = null)
		{
			this.text = text;
			this.shortcutKey = shortcutKey;
			this.onClick = onClick;
			this.onHover = onHover;
			this.visualizer = visualizer;
			this.userData = userData;
			this.uibutton = null;
		}

		// Token: 0x04008E9F RID: 36511
		public string text;

		// Token: 0x04008EA0 RID: 36512
		public global::Action shortcutKey;

		// Token: 0x04008EA1 RID: 36513
		public GameObject visualizer;

		// Token: 0x04008EA2 RID: 36514
		public UnityAction onClick;

		// Token: 0x04008EA3 RID: 36515
		public KButtonMenu.ButtonInfo.HoverCallback onHover;

		// Token: 0x04008EA4 RID: 36516
		public FMODAsset clickSound;

		// Token: 0x04008EA5 RID: 36517
		public KButton uibutton;

		// Token: 0x04008EA6 RID: 36518
		public string toolTip;

		// Token: 0x04008EA7 RID: 36519
		public bool isEnabled = true;

		// Token: 0x04008EA8 RID: 36520
		public string[] popupOptions;

		// Token: 0x04008EA9 RID: 36521
		public Action<string> onPopupClick;

		// Token: 0x04008EAA RID: 36522
		public Func<string[]> onPopulatePopup;

		// Token: 0x04008EAB RID: 36523
		public object userData;

		// Token: 0x020028FF RID: 10495
		// (Invoke) Token: 0x0600CDF5 RID: 52725
		public delegate void HoverCallback(GameObject hoverTarget);

		// Token: 0x02002900 RID: 10496
		// (Invoke) Token: 0x0600CDF9 RID: 52729
		public delegate void Callback();
	}
}
