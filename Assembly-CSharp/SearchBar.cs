using System;
using STRINGS;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000E2B RID: 3627
public class SearchBar : KMonoBehaviour
{
	// Token: 0x170007E9 RID: 2025
	// (get) Token: 0x060072BB RID: 29371 RVA: 0x002B984A File Offset: 0x002B7A4A
	public string CurrentSearchValue
	{
		get
		{
			if (!string.IsNullOrEmpty(this.inputField.text))
			{
				return this.inputField.text;
			}
			return "";
		}
	}

	// Token: 0x170007EA RID: 2026
	// (get) Token: 0x060072BC RID: 29372 RVA: 0x002B986F File Offset: 0x002B7A6F
	public bool IsInputFieldEmpty
	{
		get
		{
			return this.inputField.text == "";
		}
	}

	// Token: 0x170007EB RID: 2027
	// (get) Token: 0x060072BE RID: 29374 RVA: 0x002B988F File Offset: 0x002B7A8F
	// (set) Token: 0x060072BD RID: 29373 RVA: 0x002B9886 File Offset: 0x002B7A86
	public bool isEditing { get; protected set; }

	// Token: 0x060072BF RID: 29375 RVA: 0x002B9897 File Offset: 0x002B7A97
	public virtual void SetPlaceholder(string text)
	{
		this.inputField.placeholder.GetComponent<TextMeshProUGUI>().text = text;
	}

	// Token: 0x060072C0 RID: 29376 RVA: 0x002B98B0 File Offset: 0x002B7AB0
	protected override void OnSpawn()
	{
		this.inputField.ActivateInputField();
		KInputTextField kinputTextField = this.inputField;
		kinputTextField.onFocus = (global::System.Action)Delegate.Combine(kinputTextField.onFocus, new global::System.Action(this.OnFocus));
		this.inputField.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEdit));
		this.inputField.onValueChanged.AddListener(new UnityAction<string>(this.OnValueChanged));
		this.clearButton.onClick += this.ClearSearch;
		this.SetPlaceholder(UI.UISIDESCREENS.TREEFILTERABLESIDESCREEN.SEARCH_PLACEHOLDER);
	}

	// Token: 0x060072C1 RID: 29377 RVA: 0x002B9952 File Offset: 0x002B7B52
	protected void SetEditingState(bool editing)
	{
		this.isEditing = editing;
		Action<bool> editingStateChanged = this.EditingStateChanged;
		if (editingStateChanged != null)
		{
			editingStateChanged(this.isEditing);
		}
		KScreenManager.Instance.RefreshStack();
	}

	// Token: 0x060072C2 RID: 29378 RVA: 0x002B997C File Offset: 0x002B7B7C
	protected virtual void OnValueChanged(string value)
	{
		Action<string> valueChanged = this.ValueChanged;
		if (valueChanged == null)
		{
			return;
		}
		valueChanged(value);
	}

	// Token: 0x060072C3 RID: 29379 RVA: 0x002B998F File Offset: 0x002B7B8F
	protected virtual void OnEndEdit(string value)
	{
		this.SetEditingState(false);
	}

	// Token: 0x060072C4 RID: 29380 RVA: 0x002B9998 File Offset: 0x002B7B98
	protected virtual void OnFocus()
	{
		this.SetEditingState(true);
		UISounds.PlaySound(UISounds.Sound.ClickHUD);
		global::System.Action focused = this.Focused;
		if (focused == null)
		{
			return;
		}
		focused();
	}

	// Token: 0x060072C5 RID: 29381 RVA: 0x002B99B7 File Offset: 0x002B7BB7
	public virtual void ClearSearch()
	{
		this.SetValue("");
	}

	// Token: 0x060072C6 RID: 29382 RVA: 0x002B99C4 File Offset: 0x002B7BC4
	public void SetValue(string value)
	{
		this.inputField.text = value;
		Action<string> valueChanged = this.ValueChanged;
		if (valueChanged == null)
		{
			return;
		}
		valueChanged(value);
	}

	// Token: 0x04004EFF RID: 20223
	[SerializeField]
	protected KInputTextField inputField;

	// Token: 0x04004F00 RID: 20224
	[SerializeField]
	protected KButton clearButton;

	// Token: 0x04004F02 RID: 20226
	public Action<string> ValueChanged;

	// Token: 0x04004F03 RID: 20227
	public Action<bool> EditingStateChanged;

	// Token: 0x04004F04 RID: 20228
	public global::System.Action Focused;
}
