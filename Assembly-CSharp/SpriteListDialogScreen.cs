using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E59 RID: 3673
public class SpriteListDialogScreen : KModalScreen
{
	// Token: 0x060074ED RID: 29933 RVA: 0x002CA153 File Offset: 0x002C8353
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.gameObject.SetActive(false);
		this.buttons = new List<SpriteListDialogScreen.Button>();
	}

	// Token: 0x060074EE RID: 29934 RVA: 0x002CA172 File Offset: 0x002C8372
	public override bool IsModal()
	{
		return true;
	}

	// Token: 0x060074EF RID: 29935 RVA: 0x002CA175 File Offset: 0x002C8375
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape))
		{
			this.Deactivate();
			return;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x060074F0 RID: 29936 RVA: 0x002CA190 File Offset: 0x002C8390
	public void AddOption(string text, global::System.Action action)
	{
		GameObject gameObject = Util.KInstantiateUI(this.buttonPrefab, this.buttonPanel, true);
		this.buttons.Add(new SpriteListDialogScreen.Button
		{
			label = text,
			action = action,
			gameObject = gameObject
		});
	}

	// Token: 0x060074F1 RID: 29937 RVA: 0x002CA1DC File Offset: 0x002C83DC
	public void AddListRow(Sprite sprite, string text, float width = -1f, float height = -1f)
	{
		GameObject gameObject = Util.KInstantiateUI(this.listPrefab, this.listPanel, true);
		gameObject.GetComponentInChildren<LocText>().text = text;
		Image componentInChildren = gameObject.GetComponentInChildren<Image>();
		componentInChildren.sprite = sprite;
		if (sprite == null)
		{
			Color color = componentInChildren.color;
			color.a = 0f;
			componentInChildren.color = color;
		}
		if (width >= 0f || height >= 0f)
		{
			componentInChildren.GetComponent<AspectRatioFitter>().enabled = false;
			LayoutElement component = componentInChildren.GetComponent<LayoutElement>();
			component.minWidth = width;
			component.preferredWidth = width;
			component.minHeight = height;
			component.preferredHeight = height;
			return;
		}
		AspectRatioFitter component2 = componentInChildren.GetComponent<AspectRatioFitter>();
		float num = ((sprite == null) ? 1f : (sprite.rect.width / sprite.rect.height));
		component2.aspectRatio = num;
	}

	// Token: 0x060074F2 RID: 29938 RVA: 0x002CA2B4 File Offset: 0x002C84B4
	public void PopupConfirmDialog(string text, string title_text = null)
	{
		foreach (SpriteListDialogScreen.Button button in this.buttons)
		{
			button.gameObject.GetComponentInChildren<LocText>().text = button.label;
			button.gameObject.GetComponent<KButton>().onClick += button.action;
		}
		if (title_text != null)
		{
			this.titleText.text = title_text;
		}
		this.popupMessage.text = text;
	}

	// Token: 0x060074F3 RID: 29939 RVA: 0x002CA348 File Offset: 0x002C8548
	protected override void OnDeactivate()
	{
		if (this.onDeactivateCB != null)
		{
			this.onDeactivateCB();
		}
		base.OnDeactivate();
	}

	// Token: 0x040050DD RID: 20701
	public global::System.Action onDeactivateCB;

	// Token: 0x040050DE RID: 20702
	[SerializeField]
	private GameObject buttonPrefab;

	// Token: 0x040050DF RID: 20703
	[SerializeField]
	private GameObject buttonPanel;

	// Token: 0x040050E0 RID: 20704
	[SerializeField]
	private LocText titleText;

	// Token: 0x040050E1 RID: 20705
	[SerializeField]
	private LocText popupMessage;

	// Token: 0x040050E2 RID: 20706
	[SerializeField]
	private GameObject listPanel;

	// Token: 0x040050E3 RID: 20707
	[SerializeField]
	private GameObject listPrefab;

	// Token: 0x040050E4 RID: 20708
	private List<SpriteListDialogScreen.Button> buttons;

	// Token: 0x02002054 RID: 8276
	private struct Button
	{
		// Token: 0x040093B7 RID: 37815
		public global::System.Action action;

		// Token: 0x040093B8 RID: 37816
		public GameObject gameObject;

		// Token: 0x040093B9 RID: 37817
		public string label;
	}
}
