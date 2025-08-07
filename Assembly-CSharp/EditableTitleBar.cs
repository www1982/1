using System;
using System.Collections;
using STRINGS;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000CC5 RID: 3269
public class EditableTitleBar : TitleBar
{
	// Token: 0x14000027 RID: 39
	// (add) Token: 0x060064BF RID: 25791 RVA: 0x0025DFE4 File Offset: 0x0025C1E4
	// (remove) Token: 0x060064C0 RID: 25792 RVA: 0x0025E01C File Offset: 0x0025C21C
	public event Action<string> OnNameChanged;

	// Token: 0x14000028 RID: 40
	// (add) Token: 0x060064C1 RID: 25793 RVA: 0x0025E054 File Offset: 0x0025C254
	// (remove) Token: 0x060064C2 RID: 25794 RVA: 0x0025E08C File Offset: 0x0025C28C
	public event global::System.Action OnStartedEditing;

	// Token: 0x060064C3 RID: 25795 RVA: 0x0025E0C4 File Offset: 0x0025C2C4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.randomNameButton != null)
		{
			this.randomNameButton.onClick += this.GenerateRandomName;
		}
		if (this.editNameButton != null)
		{
			this.EnableEditButtonClick();
		}
		if (this.inputField != null)
		{
			this.inputField.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEdit));
		}
	}

	// Token: 0x060064C4 RID: 25796 RVA: 0x0025E13C File Offset: 0x0025C33C
	public void UpdateRenameTooltip(GameObject target)
	{
		if (this.editNameButton != null && target != null)
		{
			if (target.GetComponent<MinionBrain>() != null)
			{
				this.editNameButton.GetComponent<ToolTip>().toolTip = UI.TOOLTIPS.EDITNAME;
			}
			if (target.GetComponent<ClustercraftExteriorDoor>() != null || target.GetComponent<CommandModule>() != null)
			{
				this.editNameButton.GetComponent<ToolTip>().toolTip = UI.TOOLTIPS.EDITNAMEROCKET;
				return;
			}
			this.editNameButton.GetComponent<ToolTip>().toolTip = string.Format(UI.TOOLTIPS.EDITNAMEGENERIC, target.GetProperName());
		}
	}

	// Token: 0x060064C5 RID: 25797 RVA: 0x0025E1EC File Offset: 0x0025C3EC
	private void OnEndEdit(string finalStr)
	{
		finalStr = Localization.FilterDirtyWords(finalStr);
		this.SetEditingState(false);
		if (string.IsNullOrEmpty(finalStr))
		{
			return;
		}
		if (this.OnNameChanged != null)
		{
			this.OnNameChanged(finalStr);
		}
		this.titleText.text = finalStr;
		if (this.postEndEdit != null)
		{
			base.StopCoroutine(this.postEndEdit);
		}
		if (base.gameObject.activeInHierarchy && base.enabled)
		{
			this.postEndEdit = base.StartCoroutine(this.PostOnEndEditRoutine());
		}
	}

	// Token: 0x060064C6 RID: 25798 RVA: 0x0025E26C File Offset: 0x0025C46C
	private IEnumerator PostOnEndEditRoutine()
	{
		int i = 0;
		while (i < 10)
		{
			int num = i;
			i = num + 1;
			yield return SequenceUtil.WaitForEndOfFrame;
		}
		this.EnableEditButtonClick();
		if (this.randomNameButton != null)
		{
			this.randomNameButton.gameObject.SetActive(false);
		}
		yield break;
	}

	// Token: 0x060064C7 RID: 25799 RVA: 0x0025E27B File Offset: 0x0025C47B
	private IEnumerator PreToggleNameEditingRoutine()
	{
		yield return SequenceUtil.WaitForEndOfFrame;
		this.ToggleNameEditing();
		this.preToggleNameEditing = null;
		yield break;
	}

	// Token: 0x060064C8 RID: 25800 RVA: 0x0025E28A File Offset: 0x0025C48A
	private void EnableEditButtonClick()
	{
		this.editNameButton.onClick += delegate
		{
			if (this.preToggleNameEditing != null)
			{
				return;
			}
			this.preToggleNameEditing = base.StartCoroutine(this.PreToggleNameEditingRoutine());
		};
	}

	// Token: 0x060064C9 RID: 25801 RVA: 0x0025E2A4 File Offset: 0x0025C4A4
	private void GenerateRandomName()
	{
		if (this.postEndEdit != null)
		{
			base.StopCoroutine(this.postEndEdit);
		}
		string text = GameUtil.GenerateRandomDuplicantName();
		if (this.OnNameChanged != null)
		{
			this.OnNameChanged(text);
		}
		this.titleText.text = text;
		this.SetEditingState(true);
	}

	// Token: 0x060064CA RID: 25802 RVA: 0x0025E2F4 File Offset: 0x0025C4F4
	private void ToggleNameEditing()
	{
		this.editNameButton.ClearOnClick();
		bool flag = !this.inputField.gameObject.activeInHierarchy;
		if (this.randomNameButton != null)
		{
			this.randomNameButton.gameObject.SetActive(flag);
		}
		this.SetEditingState(flag);
	}

	// Token: 0x060064CB RID: 25803 RVA: 0x0025E348 File Offset: 0x0025C548
	private void SetEditingState(bool state)
	{
		this.titleText.gameObject.SetActive(!state);
		if (this.setCameraControllerState)
		{
			CameraController.Instance.DisableUserCameraControl = state;
		}
		if (this.inputField == null)
		{
			return;
		}
		this.inputField.gameObject.SetActive(state);
		if (state)
		{
			this.inputField.text = this.titleText.text;
			this.inputField.Select();
			this.inputField.ActivateInputField();
			if (this.OnStartedEditing != null)
			{
				this.OnStartedEditing();
				return;
			}
		}
		else
		{
			this.inputField.DeactivateInputField();
		}
	}

	// Token: 0x060064CC RID: 25804 RVA: 0x0025E3EA File Offset: 0x0025C5EA
	public void ForceStopEditing()
	{
		if (this.postEndEdit != null)
		{
			base.StopCoroutine(this.postEndEdit);
		}
		this.editNameButton.ClearOnClick();
		this.SetEditingState(false);
		this.EnableEditButtonClick();
	}

	// Token: 0x060064CD RID: 25805 RVA: 0x0025E418 File Offset: 0x0025C618
	public void SetUserEditable(bool editable)
	{
		this.userEditable = editable;
		this.editNameButton.gameObject.SetActive(editable);
		this.editNameButton.ClearOnClick();
		this.EnableEditButtonClick();
	}

	// Token: 0x040044CF RID: 17615
	public KButton editNameButton;

	// Token: 0x040044D0 RID: 17616
	public KButton randomNameButton;

	// Token: 0x040044D1 RID: 17617
	public KInputTextField inputField;

	// Token: 0x040044D4 RID: 17620
	private Coroutine postEndEdit;

	// Token: 0x040044D5 RID: 17621
	private Coroutine preToggleNameEditing;
}
