using System;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CA1 RID: 3233
public class ConfirmDialogScreen : KModalScreen
{
	// Token: 0x06006379 RID: 25465 RVA: 0x00255E81 File Offset: 0x00254081
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600637A RID: 25466 RVA: 0x00255E95 File Offset: 0x00254095
	public override bool IsModal()
	{
		return true;
	}

	// Token: 0x0600637B RID: 25467 RVA: 0x00255E98 File Offset: 0x00254098
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape))
		{
			this.OnSelect_CANCEL();
			return;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x0600637C RID: 25468 RVA: 0x00255EB4 File Offset: 0x002540B4
	public void PopupConfirmDialog(string text, global::System.Action on_confirm, global::System.Action on_cancel, string configurable_text = null, global::System.Action on_configurable_clicked = null, string title_text = null, string confirm_text = null, string cancel_text = null, Sprite image_sprite = null)
	{
		while (base.transform.parent.GetComponent<Canvas>() == null && base.transform.parent.parent != null)
		{
			base.transform.SetParent(base.transform.parent.parent);
		}
		base.transform.SetAsLastSibling();
		this.confirmAction = on_confirm;
		this.cancelAction = on_cancel;
		this.configurableAction = on_configurable_clicked;
		int num = 0;
		if (this.confirmAction != null)
		{
			num++;
		}
		if (this.cancelAction != null)
		{
			num++;
		}
		if (this.configurableAction != null)
		{
			num++;
		}
		this.confirmButton.GetComponentInChildren<LocText>().text = ((confirm_text == null) ? UI.CONFIRMDIALOG.OK.text : confirm_text);
		this.cancelButton.GetComponentInChildren<LocText>().text = ((cancel_text == null) ? UI.CONFIRMDIALOG.CANCEL.text : cancel_text);
		this.confirmButton.GetComponent<KButton>().onClick += this.OnSelect_OK;
		this.cancelButton.GetComponent<KButton>().onClick += this.OnSelect_CANCEL;
		this.configurableButton.GetComponent<KButton>().onClick += this.OnSelect_third;
		this.cancelButton.SetActive(on_cancel != null);
		if (this.configurableButton != null)
		{
			this.configurableButton.SetActive(this.configurableAction != null);
			if (configurable_text != null)
			{
				this.configurableButton.GetComponentInChildren<LocText>().text = configurable_text;
			}
		}
		if (image_sprite != null)
		{
			this.image.sprite = image_sprite;
			this.image.gameObject.SetActive(true);
		}
		if (title_text != null)
		{
			this.titleText.key = "";
			this.titleText.text = title_text;
		}
		this.popupMessage.text = text;
	}

	// Token: 0x0600637D RID: 25469 RVA: 0x00256089 File Offset: 0x00254289
	public void OnSelect_OK()
	{
		if (this.deactivateOnConfirmAction)
		{
			this.Deactivate();
		}
		if (this.confirmAction != null)
		{
			this.confirmAction();
		}
	}

	// Token: 0x0600637E RID: 25470 RVA: 0x002560AC File Offset: 0x002542AC
	public void OnSelect_CANCEL()
	{
		if (this.deactivateOnCancelAction)
		{
			this.Deactivate();
		}
		if (this.cancelAction != null)
		{
			this.cancelAction();
		}
	}

	// Token: 0x0600637F RID: 25471 RVA: 0x002560CF File Offset: 0x002542CF
	public void OnSelect_third()
	{
		if (this.deactivateOnConfigurableAction)
		{
			this.Deactivate();
		}
		if (this.configurableAction != null)
		{
			this.configurableAction();
		}
	}

	// Token: 0x06006380 RID: 25472 RVA: 0x002560F2 File Offset: 0x002542F2
	protected override void OnDeactivate()
	{
		if (this.onDeactivateCB != null)
		{
			this.onDeactivateCB();
		}
		base.OnDeactivate();
	}

	// Token: 0x0400439B RID: 17307
	private global::System.Action confirmAction;

	// Token: 0x0400439C RID: 17308
	private global::System.Action cancelAction;

	// Token: 0x0400439D RID: 17309
	private global::System.Action configurableAction;

	// Token: 0x0400439E RID: 17310
	public bool deactivateOnConfigurableAction = true;

	// Token: 0x0400439F RID: 17311
	public bool deactivateOnConfirmAction = true;

	// Token: 0x040043A0 RID: 17312
	public bool deactivateOnCancelAction = true;

	// Token: 0x040043A1 RID: 17313
	public global::System.Action onDeactivateCB;

	// Token: 0x040043A2 RID: 17314
	[SerializeField]
	private GameObject confirmButton;

	// Token: 0x040043A3 RID: 17315
	[SerializeField]
	private GameObject cancelButton;

	// Token: 0x040043A4 RID: 17316
	[SerializeField]
	private GameObject configurableButton;

	// Token: 0x040043A5 RID: 17317
	[SerializeField]
	private LocText titleText;

	// Token: 0x040043A6 RID: 17318
	[SerializeField]
	private LocText popupMessage;

	// Token: 0x040043A7 RID: 17319
	[SerializeField]
	private Image image;
}
