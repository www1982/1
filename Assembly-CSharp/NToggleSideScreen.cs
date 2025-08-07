using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000E11 RID: 3601
public class NToggleSideScreen : SideScreenContent
{
	// Token: 0x060071A2 RID: 29090 RVA: 0x002B34D6 File Offset: 0x002B16D6
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x060071A3 RID: 29091 RVA: 0x002B34DE File Offset: 0x002B16DE
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<INToggleSideScreenControl>() != null;
	}

	// Token: 0x060071A4 RID: 29092 RVA: 0x002B34EC File Offset: 0x002B16EC
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.target = target.GetComponent<INToggleSideScreenControl>();
		if (this.target == null)
		{
			return;
		}
		this.titleKey = this.target.SidescreenTitleKey;
		base.gameObject.SetActive(true);
		this.Refresh();
	}

	// Token: 0x060071A5 RID: 29093 RVA: 0x002B3538 File Offset: 0x002B1738
	private void Refresh()
	{
		for (int i = 0; i < Mathf.Max(this.target.Options.Count, this.buttonList.Count); i++)
		{
			if (i >= this.target.Options.Count)
			{
				this.buttonList[i].gameObject.SetActive(false);
			}
			else
			{
				if (i >= this.buttonList.Count)
				{
					KToggle ktoggle = Util.KInstantiateUI<KToggle>(this.buttonPrefab.gameObject, this.ContentContainer, false);
					int idx = i;
					ktoggle.onClick += delegate
					{
						this.target.QueueSelectedOption(idx);
						this.Refresh();
					};
					this.buttonList.Add(ktoggle);
				}
				this.buttonList[i].GetComponentInChildren<LocText>().text = this.target.Options[i];
				this.buttonList[i].GetComponentInChildren<ToolTip>().toolTip = this.target.Tooltips[i];
				if (this.target.SelectedOption == i && this.target.QueuedOption == i)
				{
					this.buttonList[i].isOn = true;
					ImageToggleState[] array = this.buttonList[i].GetComponentsInChildren<ImageToggleState>();
					for (int j = 0; j < array.Length; j++)
					{
						array[j].SetActive();
					}
					this.buttonList[i].GetComponent<ImageToggleStateThrobber>().enabled = false;
				}
				else if (this.target.QueuedOption == i)
				{
					this.buttonList[i].isOn = true;
					ImageToggleState[] array = this.buttonList[i].GetComponentsInChildren<ImageToggleState>();
					for (int j = 0; j < array.Length; j++)
					{
						array[j].SetActive();
					}
					this.buttonList[i].GetComponent<ImageToggleStateThrobber>().enabled = true;
				}
				else
				{
					this.buttonList[i].isOn = false;
					foreach (ImageToggleState imageToggleState in this.buttonList[i].GetComponentsInChildren<ImageToggleState>())
					{
						imageToggleState.SetInactive();
						imageToggleState.SetInactive();
					}
					this.buttonList[i].GetComponent<ImageToggleStateThrobber>().enabled = false;
				}
				this.buttonList[i].gameObject.SetActive(true);
			}
		}
		this.description.text = this.target.Description;
		this.description.gameObject.SetActive(!string.IsNullOrEmpty(this.target.Description));
	}

	// Token: 0x04004E32 RID: 20018
	[SerializeField]
	private KToggle buttonPrefab;

	// Token: 0x04004E33 RID: 20019
	[SerializeField]
	private LocText description;

	// Token: 0x04004E34 RID: 20020
	private INToggleSideScreenControl target;

	// Token: 0x04004E35 RID: 20021
	private List<KToggle> buttonList = new List<KToggle>();
}
