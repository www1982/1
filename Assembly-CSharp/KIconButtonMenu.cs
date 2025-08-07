using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000CF7 RID: 3319
public class KIconButtonMenu : KScreen
{
	// Token: 0x06006639 RID: 26169 RVA: 0x00269AC8 File Offset: 0x00267CC8
	protected override void OnActivate()
	{
		base.OnActivate();
		this.RefreshButtons();
	}

	// Token: 0x0600663A RID: 26170 RVA: 0x00269AD6 File Offset: 0x00267CD6
	public void SetButtons(IList<KIconButtonMenu.ButtonInfo> buttons)
	{
		this.buttons = buttons;
		if (this.activateOnSpawn)
		{
			this.RefreshButtons();
		}
	}

	// Token: 0x0600663B RID: 26171 RVA: 0x00269AF0 File Offset: 0x00267CF0
	public void RefreshButtonTooltip()
	{
		for (int i = 0; i < this.buttons.Count; i++)
		{
			KIconButtonMenu.ButtonInfo buttonInfo = this.buttons[i];
			if (buttonInfo.buttonGo == null || buttonInfo == null)
			{
				return;
			}
			ToolTip componentInChildren = buttonInfo.buttonGo.GetComponentInChildren<ToolTip>();
			if (buttonInfo.text != null && buttonInfo.text != "" && componentInChildren != null)
			{
				componentInChildren.toolTip = buttonInfo.GetTooltipText();
				LocText componentInChildren2 = buttonInfo.buttonGo.GetComponentInChildren<LocText>();
				if (componentInChildren2 != null)
				{
					componentInChildren2.text = buttonInfo.text;
				}
			}
		}
	}

	// Token: 0x0600663C RID: 26172 RVA: 0x00269B94 File Offset: 0x00267D94
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
		if (this.buttons == null || this.buttons.Count == 0)
		{
			return;
		}
		this.buttonObjects = new GameObject[this.buttons.Count];
		for (int j = 0; j < this.buttons.Count; j++)
		{
			KIconButtonMenu.ButtonInfo buttonInfo = this.buttons[j];
			if (buttonInfo != null)
			{
				GameObject binstance = global::UnityEngine.Object.Instantiate<GameObject>(this.buttonPrefab, Vector3.zero, Quaternion.identity);
				buttonInfo.buttonGo = binstance;
				this.buttonObjects[j] = binstance;
				Transform transform = ((this.buttonParent != null) ? this.buttonParent : base.transform);
				binstance.transform.SetParent(transform, false);
				binstance.SetActive(true);
				binstance.name = buttonInfo.text + "Button";
				KButton component = binstance.GetComponent<KButton>();
				if (component != null && buttonInfo.onClick != null)
				{
					component.onClick += buttonInfo.onClick;
				}
				Image image = null;
				if (component)
				{
					image = component.fgImage;
				}
				if (image != null)
				{
					image.gameObject.SetActive(false);
					foreach (Sprite sprite in this.icons)
					{
						if (sprite != null && sprite.name == buttonInfo.iconName)
						{
							image.sprite = sprite;
							image.gameObject.SetActive(true);
							break;
						}
					}
				}
				if (buttonInfo.texture != null)
				{
					RawImage componentInChildren = binstance.GetComponentInChildren<RawImage>();
					if (componentInChildren != null)
					{
						componentInChildren.gameObject.SetActive(true);
						componentInChildren.texture = buttonInfo.texture;
					}
				}
				ToolTip componentInChildren2 = binstance.GetComponentInChildren<ToolTip>();
				if (buttonInfo.text != null && buttonInfo.text != "" && componentInChildren2 != null)
				{
					componentInChildren2.toolTip = buttonInfo.GetTooltipText();
					LocText componentInChildren3 = binstance.GetComponentInChildren<LocText>();
					if (componentInChildren3 != null)
					{
						componentInChildren3.text = buttonInfo.text;
					}
				}
				if (buttonInfo.onToolTip != null)
				{
					componentInChildren2.OnToolTip = buttonInfo.onToolTip;
				}
				KIconButtonMenu screen = this;
				global::System.Action onClick = buttonInfo.onClick;
				global::System.Action action = delegate
				{
					onClick.Signal();
					if (!this.keepMenuOpen && screen != null)
					{
						screen.Deactivate();
					}
					if (binstance != null)
					{
						KToggle component3 = binstance.GetComponent<KToggle>();
						if (component3 != null)
						{
							this.SelectToggle(component3);
						}
					}
				};
				KToggle componentInChildren4 = binstance.GetComponentInChildren<KToggle>();
				if (componentInChildren4 != null)
				{
					ToggleGroup component2 = base.GetComponent<ToggleGroup>();
					if (component2 == null)
					{
						component2 = this.externalToggleGroup;
					}
					componentInChildren4.group = component2;
					componentInChildren4.onClick += action;
					Navigation navigation = componentInChildren4.navigation;
					navigation.mode = (this.automaticNavigation ? Navigation.Mode.Automatic : Navigation.Mode.None);
					componentInChildren4.navigation = navigation;
				}
				else
				{
					KBasicToggle componentInChildren5 = binstance.GetComponentInChildren<KBasicToggle>();
					if (componentInChildren5 != null)
					{
						componentInChildren5.onClick += action;
					}
				}
				if (component != null)
				{
					component.isInteractable = buttonInfo.isInteractable;
				}
				buttonInfo.onCreate.Signal(buttonInfo);
			}
		}
		this.Update();
	}

	// Token: 0x0600663D RID: 26173 RVA: 0x00269F04 File Offset: 0x00268104
	public override void OnKeyDown(KButtonEvent e)
	{
		if (this.buttons == null)
		{
			return;
		}
		if (!base.gameObject.activeSelf || !base.enabled)
		{
			return;
		}
		for (int i = 0; i < this.buttons.Count; i++)
		{
			KIconButtonMenu.ButtonInfo buttonInfo = this.buttons[i];
			if (e.TryConsume(buttonInfo.shortcutKey))
			{
				this.buttonObjects[i].GetComponent<KButton>().PlayPointerDownSound();
				this.buttonObjects[i].GetComponent<KButton>().SignalClick(KKeyCode.Mouse0);
				break;
			}
		}
		base.OnKeyDown(e);
	}

	// Token: 0x0600663E RID: 26174 RVA: 0x00269F93 File Offset: 0x00268193
	protected override void OnPrefabInit()
	{
		base.Subscribe<KIconButtonMenu>(315865555, KIconButtonMenu.OnSetActivatorDelegate);
	}

	// Token: 0x0600663F RID: 26175 RVA: 0x00269FA6 File Offset: 0x002681A6
	private void OnSetActivator(object data)
	{
		this.go = (GameObject)data;
		this.Update();
	}

	// Token: 0x06006640 RID: 26176 RVA: 0x00269FBC File Offset: 0x002681BC
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

	// Token: 0x06006641 RID: 26177 RVA: 0x0026A078 File Offset: 0x00268278
	protected void SelectToggle(KToggle selectedToggle)
	{
		if (global::UnityEngine.EventSystems.EventSystem.current == null || !global::UnityEngine.EventSystems.EventSystem.current.enabled)
		{
			return;
		}
		if (this.currentlySelectedToggle == selectedToggle)
		{
			this.currentlySelectedToggle = null;
		}
		else
		{
			this.currentlySelectedToggle = selectedToggle;
		}
		GameObject[] array = this.buttonObjects;
		for (int i = 0; i < array.Length; i++)
		{
			KToggle component = array[i].GetComponent<KToggle>();
			if (component != null)
			{
				if (component == this.currentlySelectedToggle)
				{
					component.Select();
					component.isOn = true;
				}
				else
				{
					component.Deselect();
					component.isOn = false;
				}
			}
		}
	}

	// Token: 0x06006642 RID: 26178 RVA: 0x0026A110 File Offset: 0x00268310
	public void ClearSelection()
	{
		foreach (GameObject gameObject in this.buttonObjects)
		{
			KToggle component = gameObject.GetComponent<KToggle>();
			if (component != null)
			{
				component.Deselect();
				component.isOn = false;
			}
			else
			{
				KBasicToggle component2 = gameObject.GetComponent<KBasicToggle>();
				if (component2 != null)
				{
					component2.isOn = false;
				}
			}
			ImageToggleState component3 = gameObject.GetComponent<ImageToggleState>();
			if (component3.GetIsActive())
			{
				component3.SetInactive();
			}
		}
		ToggleGroup component4 = base.GetComponent<ToggleGroup>();
		if (component4 != null)
		{
			component4.SetAllTogglesOff(true);
		}
		this.SelectToggle(null);
	}

	// Token: 0x04004609 RID: 17929
	[SerializeField]
	protected bool followGameObject;

	// Token: 0x0400460A RID: 17930
	[SerializeField]
	protected bool keepMenuOpen;

	// Token: 0x0400460B RID: 17931
	[SerializeField]
	protected bool automaticNavigation = true;

	// Token: 0x0400460C RID: 17932
	[SerializeField]
	protected Transform buttonParent;

	// Token: 0x0400460D RID: 17933
	[SerializeField]
	private GameObject buttonPrefab;

	// Token: 0x0400460E RID: 17934
	[SerializeField]
	protected Sprite[] icons;

	// Token: 0x0400460F RID: 17935
	[SerializeField]
	private ToggleGroup externalToggleGroup;

	// Token: 0x04004610 RID: 17936
	protected KToggle currentlySelectedToggle;

	// Token: 0x04004611 RID: 17937
	[NonSerialized]
	public GameObject[] buttonObjects;

	// Token: 0x04004612 RID: 17938
	[SerializeField]
	public TextStyleSetting ToggleToolTipTextStyleSetting;

	// Token: 0x04004613 RID: 17939
	private UnityAction inputChangeReceiver;

	// Token: 0x04004614 RID: 17940
	protected GameObject go;

	// Token: 0x04004615 RID: 17941
	protected IList<KIconButtonMenu.ButtonInfo> buttons;

	// Token: 0x04004616 RID: 17942
	private static readonly global::EventSystem.IntraObjectHandler<KIconButtonMenu> OnSetActivatorDelegate = new global::EventSystem.IntraObjectHandler<KIconButtonMenu>(delegate(KIconButtonMenu component, object data)
	{
		component.OnSetActivator(data);
	});

	// Token: 0x02001EBF RID: 7871
	public class ButtonInfo
	{
		// Token: 0x0600B141 RID: 45377 RVA: 0x003D45B4 File Offset: 0x003D27B4
		public ButtonInfo(string iconName = "", string text = "", global::System.Action on_click = null, global::Action shortcutKey = global::Action.NumActions, Action<GameObject> on_refresh = null, Action<KIconButtonMenu.ButtonInfo> on_create = null, Texture texture = null, string tooltipText = "", bool is_interactable = true)
		{
			this.iconName = iconName;
			this.text = text;
			this.shortcutKey = shortcutKey;
			this.onClick = on_click;
			this.onCreate = on_create;
			this.texture = texture;
			this.tooltipText = tooltipText;
			this.isInteractable = is_interactable;
		}

		// Token: 0x0600B142 RID: 45378 RVA: 0x003D4604 File Offset: 0x003D2804
		public string GetTooltipText()
		{
			string text = ((this.tooltipText == "") ? this.text : this.tooltipText);
			if (this.shortcutKey != global::Action.NumActions)
			{
				text = GameUtil.ReplaceHotkeyString(text, this.shortcutKey);
			}
			return text;
		}

		// Token: 0x04008EBA RID: 36538
		public string iconName;

		// Token: 0x04008EBB RID: 36539
		public string text;

		// Token: 0x04008EBC RID: 36540
		public string tooltipText;

		// Token: 0x04008EBD RID: 36541
		public string[] multiText;

		// Token: 0x04008EBE RID: 36542
		public global::Action shortcutKey;

		// Token: 0x04008EBF RID: 36543
		public bool isInteractable;

		// Token: 0x04008EC0 RID: 36544
		public Action<KIconButtonMenu.ButtonInfo> onCreate;

		// Token: 0x04008EC1 RID: 36545
		public global::System.Action onClick;

		// Token: 0x04008EC2 RID: 36546
		public Func<string> onToolTip;

		// Token: 0x04008EC3 RID: 36547
		public GameObject buttonGo;

		// Token: 0x04008EC4 RID: 36548
		public object userData;

		// Token: 0x04008EC5 RID: 36549
		public Texture texture;

		// Token: 0x02002901 RID: 10497
		// (Invoke) Token: 0x0600CDFD RID: 52733
		public delegate void Callback();
	}
}
