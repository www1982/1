using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000C19 RID: 3097
public class FileNameDialog : KModalScreen
{
	// Token: 0x06005DC4 RID: 24004 RVA: 0x00224AE1 File Offset: 0x00222CE1
	public override float GetSortKey()
	{
		return 150f;
	}

	// Token: 0x06005DC5 RID: 24005 RVA: 0x00224AE8 File Offset: 0x00222CE8
	public void SetTextAndSelect(string text)
	{
		if (this.inputField == null)
		{
			return;
		}
		this.inputField.text = text;
		this.inputField.Select();
	}

	// Token: 0x06005DC6 RID: 24006 RVA: 0x00224B10 File Offset: 0x00222D10
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.confirmButton.onClick += this.OnConfirm;
		this.cancelButton.onClick += this.OnCancel;
		this.closeButton.onClick += this.OnCancel;
		this.inputField.onValueChanged.AddListener(delegate
		{
			Util.ScrubInputField(this.inputField, false, false);
		});
		this.inputField.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEdit));
	}

	// Token: 0x06005DC7 RID: 24007 RVA: 0x00224BA0 File Offset: 0x00222DA0
	protected override void OnActivate()
	{
		base.OnActivate();
		this.inputField.Select();
		this.inputField.ActivateInputField();
		CameraController.Instance.DisableUserCameraControl = true;
	}

	// Token: 0x06005DC8 RID: 24008 RVA: 0x00224BC9 File Offset: 0x00222DC9
	protected override void OnDeactivate()
	{
		CameraController.Instance.DisableUserCameraControl = false;
		base.OnDeactivate();
	}

	// Token: 0x06005DC9 RID: 24009 RVA: 0x00224BDC File Offset: 0x00222DDC
	public void OnConfirm()
	{
		if (this.onConfirm != null && !string.IsNullOrEmpty(this.inputField.text))
		{
			string text = this.inputField.text;
			if (!text.EndsWith(".sav"))
			{
				text += ".sav";
			}
			this.onConfirm(text);
			this.Deactivate();
		}
	}

	// Token: 0x06005DCA RID: 24010 RVA: 0x00224C3A File Offset: 0x00222E3A
	private void OnEndEdit(string str)
	{
		if (Localization.HasDirtyWords(str))
		{
			this.inputField.text = "";
		}
	}

	// Token: 0x06005DCB RID: 24011 RVA: 0x00224C54 File Offset: 0x00222E54
	public void OnCancel()
	{
		if (this.onCancel != null)
		{
			this.onCancel();
		}
		this.Deactivate();
	}

	// Token: 0x06005DCC RID: 24012 RVA: 0x00224C6F File Offset: 0x00222E6F
	public override void OnKeyUp(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape))
		{
			this.Deactivate();
		}
		else if (e.TryConsume(global::Action.DialogSubmit))
		{
			this.OnConfirm();
		}
		e.Consumed = true;
	}

	// Token: 0x06005DCD RID: 24013 RVA: 0x00224C9C File Offset: 0x00222E9C
	public override void OnKeyDown(KButtonEvent e)
	{
		e.Consumed = true;
	}

	// Token: 0x04003E6B RID: 15979
	public Action<string> onConfirm;

	// Token: 0x04003E6C RID: 15980
	public global::System.Action onCancel;

	// Token: 0x04003E6D RID: 15981
	[SerializeField]
	private KInputTextField inputField;

	// Token: 0x04003E6E RID: 15982
	[SerializeField]
	private KButton confirmButton;

	// Token: 0x04003E6F RID: 15983
	[SerializeField]
	private KButton cancelButton;

	// Token: 0x04003E70 RID: 15984
	[SerializeField]
	private KButton closeButton;
}
