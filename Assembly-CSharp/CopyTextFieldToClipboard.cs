using System;
using UnityEngine;

// Token: 0x02000E81 RID: 3713
[AddComponentMenu("KMonoBehaviour/scripts/CopyTextFieldToClipboard")]
public class CopyTextFieldToClipboard : KMonoBehaviour
{
	// Token: 0x0600765D RID: 30301 RVA: 0x002D4640 File Offset: 0x002D2840
	protected override void OnPrefabInit()
	{
		this.button.onClick += this.OnClick;
	}

	// Token: 0x0600765E RID: 30302 RVA: 0x002D4659 File Offset: 0x002D2859
	private void OnClick()
	{
		TextEditor textEditor = new TextEditor();
		textEditor.text = this.GetText();
		textEditor.SelectAll();
		textEditor.Copy();
	}

	// Token: 0x04005231 RID: 21041
	public KButton button;

	// Token: 0x04005232 RID: 21042
	public Func<string> GetText;
}
