using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000CFB RID: 3323
public class KPopupMenu : KScreen
{
	// Token: 0x0600666C RID: 26220 RVA: 0x0026AB5C File Offset: 0x00268D5C
	public void SetOptions(IList<string> options)
	{
		List<KButtonMenu.ButtonInfo> list = new List<KButtonMenu.ButtonInfo>();
		for (int i = 0; i < options.Count; i++)
		{
			int index = i;
			string option = options[i];
			list.Add(new KButtonMenu.ButtonInfo(option, global::Action.NumActions, delegate
			{
				this.SelectOption(option, index);
			}, null, null));
		}
		this.Buttons = list.ToArray();
	}

	// Token: 0x0600666D RID: 26221 RVA: 0x0026ABD4 File Offset: 0x00268DD4
	public void OnClick()
	{
		if (this.Buttons != null)
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
				return;
			}
			this.buttonMenu.SetButtons(this.Buttons);
			this.buttonMenu.RefreshButtons();
			base.gameObject.SetActive(true);
		}
	}

	// Token: 0x0600666E RID: 26222 RVA: 0x0026AC2B File Offset: 0x00268E2B
	public void SelectOption(string option, int index)
	{
		if (this.OnSelect != null)
		{
			this.OnSelect(option, index);
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600666F RID: 26223 RVA: 0x0026AC4E File Offset: 0x00268E4E
	public IList<KButtonMenu.ButtonInfo> GetButtons()
	{
		return this.Buttons;
	}

	// Token: 0x0400462C RID: 17964
	[SerializeField]
	private KButtonMenu buttonMenu;

	// Token: 0x0400462D RID: 17965
	private KButtonMenu.ButtonInfo[] Buttons;

	// Token: 0x0400462E RID: 17966
	public Action<string, int> OnSelect;
}
