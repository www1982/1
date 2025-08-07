using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CAC RID: 3244
public class CustomizableDialogScreen : KModalScreen
{
	// Token: 0x060063C4 RID: 25540 RVA: 0x0025733F File Offset: 0x0025553F
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.gameObject.SetActive(false);
		this.buttons = new List<CustomizableDialogScreen.Button>();
	}

	// Token: 0x060063C5 RID: 25541 RVA: 0x0025735E File Offset: 0x0025555E
	public override bool IsModal()
	{
		return true;
	}

	// Token: 0x060063C6 RID: 25542 RVA: 0x00257364 File Offset: 0x00255564
	public void AddOption(string text, global::System.Action action)
	{
		GameObject gameObject = Util.KInstantiateUI(this.buttonPrefab, this.buttonPanel, true);
		this.buttons.Add(new CustomizableDialogScreen.Button
		{
			label = text,
			action = action,
			gameObject = gameObject
		});
	}

	// Token: 0x060063C7 RID: 25543 RVA: 0x002573B0 File Offset: 0x002555B0
	public void PopupConfirmDialog(string text, string title_text = null, Sprite image_sprite = null)
	{
		foreach (CustomizableDialogScreen.Button button in this.buttons)
		{
			button.gameObject.GetComponentInChildren<LocText>().text = button.label;
			button.gameObject.GetComponent<KButton>().onClick += button.action;
		}
		if (image_sprite != null)
		{
			this.image.sprite = image_sprite;
			this.image.gameObject.SetActive(true);
		}
		if (title_text != null)
		{
			this.titleText.text = title_text;
		}
		this.popupMessage.text = text;
	}

	// Token: 0x060063C8 RID: 25544 RVA: 0x0025746C File Offset: 0x0025566C
	protected override void OnDeactivate()
	{
		if (this.onDeactivateCB != null)
		{
			this.onDeactivateCB();
		}
		base.OnDeactivate();
	}

	// Token: 0x040043E1 RID: 17377
	public global::System.Action onDeactivateCB;

	// Token: 0x040043E2 RID: 17378
	[SerializeField]
	private GameObject buttonPrefab;

	// Token: 0x040043E3 RID: 17379
	[SerializeField]
	private GameObject buttonPanel;

	// Token: 0x040043E4 RID: 17380
	[SerializeField]
	private LocText titleText;

	// Token: 0x040043E5 RID: 17381
	[SerializeField]
	private LocText popupMessage;

	// Token: 0x040043E6 RID: 17382
	[SerializeField]
	private Image image;

	// Token: 0x040043E7 RID: 17383
	private List<CustomizableDialogScreen.Button> buttons;

	// Token: 0x02001E76 RID: 7798
	private struct Button
	{
		// Token: 0x04008D95 RID: 36245
		public global::System.Action action;

		// Token: 0x04008D96 RID: 36246
		public GameObject gameObject;

		// Token: 0x04008D97 RID: 36247
		public string label;
	}
}
