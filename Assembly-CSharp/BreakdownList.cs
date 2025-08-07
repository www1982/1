using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E7E RID: 3710
[AddComponentMenu("KMonoBehaviour/scripts/BreakdownList")]
public class BreakdownList : KMonoBehaviour
{
	// Token: 0x06007643 RID: 30275 RVA: 0x002D3E8C File Offset: 0x002D208C
	public BreakdownListRow AddRow()
	{
		BreakdownListRow breakdownListRow;
		if (this.unusedListRows.Count > 0)
		{
			breakdownListRow = this.unusedListRows[0];
			this.unusedListRows.RemoveAt(0);
		}
		else
		{
			breakdownListRow = global::UnityEngine.Object.Instantiate<BreakdownListRow>(this.listRowTemplate);
		}
		breakdownListRow.gameObject.transform.SetParent(base.transform);
		breakdownListRow.gameObject.transform.SetAsLastSibling();
		this.listRows.Add(breakdownListRow);
		breakdownListRow.gameObject.SetActive(true);
		return breakdownListRow;
	}

	// Token: 0x06007644 RID: 30276 RVA: 0x002D3F0D File Offset: 0x002D210D
	public GameObject AddCustomRow(GameObject newRow)
	{
		newRow.transform.SetParent(base.transform);
		newRow.gameObject.transform.SetAsLastSibling();
		this.customRows.Add(newRow);
		newRow.SetActive(true);
		return newRow;
	}

	// Token: 0x06007645 RID: 30277 RVA: 0x002D3F44 File Offset: 0x002D2144
	public void ClearRows()
	{
		foreach (BreakdownListRow breakdownListRow in this.listRows)
		{
			this.unusedListRows.Add(breakdownListRow);
			breakdownListRow.gameObject.SetActive(false);
			breakdownListRow.ClearTooltip();
		}
		this.listRows.Clear();
		foreach (GameObject gameObject in this.customRows)
		{
			gameObject.SetActive(false);
		}
	}

	// Token: 0x06007646 RID: 30278 RVA: 0x002D3FFC File Offset: 0x002D21FC
	public void SetTitle(string title)
	{
		this.headerTitle.text = title;
	}

	// Token: 0x06007647 RID: 30279 RVA: 0x002D400A File Offset: 0x002D220A
	public void SetDescription(string description)
	{
		if (description != null && description.Length >= 0)
		{
			this.infoTextLabel.gameObject.SetActive(true);
			this.infoTextLabel.text = description;
			return;
		}
		this.infoTextLabel.gameObject.SetActive(false);
	}

	// Token: 0x06007648 RID: 30280 RVA: 0x002D4047 File Offset: 0x002D2247
	public void SetIcon(Sprite icon)
	{
		this.headerIcon.sprite = icon;
	}

	// Token: 0x04005214 RID: 21012
	public Image headerIcon;

	// Token: 0x04005215 RID: 21013
	public Sprite headerIconSprite;

	// Token: 0x04005216 RID: 21014
	public Image headerBar;

	// Token: 0x04005217 RID: 21015
	public LocText headerTitle;

	// Token: 0x04005218 RID: 21016
	public LocText headerValue;

	// Token: 0x04005219 RID: 21017
	public LocText infoTextLabel;

	// Token: 0x0400521A RID: 21018
	public BreakdownListRow listRowTemplate;

	// Token: 0x0400521B RID: 21019
	private List<BreakdownListRow> listRows = new List<BreakdownListRow>();

	// Token: 0x0400521C RID: 21020
	private List<BreakdownListRow> unusedListRows = new List<BreakdownListRow>();

	// Token: 0x0400521D RID: 21021
	private List<GameObject> customRows = new List<GameObject>();
}
