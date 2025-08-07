using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CFF RID: 3327
public class KleiInventoryUISubcategory : KMonoBehaviour
{
	// Token: 0x17000767 RID: 1895
	// (get) Token: 0x060066AE RID: 26286 RVA: 0x0026D147 File Offset: 0x0026B347
	public bool IsOpen
	{
		get
		{
			return this.stateExpanded;
		}
	}

	// Token: 0x060066AF RID: 26287 RVA: 0x0026D14F File Offset: 0x0026B34F
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.expandButton.onClick = delegate
		{
			this.ToggleOpen(!this.stateExpanded);
		};
	}

	// Token: 0x060066B0 RID: 26288 RVA: 0x0026D16E File Offset: 0x0026B36E
	public void SetIdentity(string label, Sprite icon)
	{
		this.label.SetText(label);
		this.icon.sprite = icon;
	}

	// Token: 0x060066B1 RID: 26289 RVA: 0x0026D188 File Offset: 0x0026B388
	public void RefreshDisplay()
	{
		foreach (GameObject gameObject in this.dummyItems)
		{
			gameObject.SetActive(false);
		}
		int num = 0;
		for (int i = 0; i < this.gridLayout.transform.childCount; i++)
		{
			if (this.gridLayout.transform.GetChild(i).gameObject.activeSelf)
			{
				num++;
			}
		}
		base.gameObject.SetActive(num != 0);
		int j = 0;
		int num2 = num % this.gridLayout.constraintCount;
		if (num2 > 0)
		{
			j = this.gridLayout.constraintCount - num2;
		}
		while (j > this.dummyItems.Count)
		{
			this.dummyItems.Add(Util.KInstantiateUI(this.dummyPrefab, this.gridLayout.gameObject, false));
		}
		for (int k = 0; k < j; k++)
		{
			this.dummyItems[k].SetActive(true);
			this.dummyItems[k].transform.SetAsLastSibling();
		}
		this.headerLayout.minWidth = base.transform.parent.rectTransform().rect.width - 8f;
	}

	// Token: 0x060066B2 RID: 26290 RVA: 0x0026D2E8 File Offset: 0x0026B4E8
	public void ToggleOpen(bool open)
	{
		this.gridLayout.gameObject.SetActive(open);
		this.stateExpanded = open;
		this.expandButton.ChangeState(this.stateExpanded ? 1 : 0);
	}

	// Token: 0x0400465F RID: 18015
	[SerializeField]
	private GameObject dummyPrefab;

	// Token: 0x04004660 RID: 18016
	public string subcategoryID;

	// Token: 0x04004661 RID: 18017
	public GridLayoutGroup gridLayout;

	// Token: 0x04004662 RID: 18018
	public List<GameObject> dummyItems;

	// Token: 0x04004663 RID: 18019
	[SerializeField]
	private LayoutElement headerLayout;

	// Token: 0x04004664 RID: 18020
	[SerializeField]
	private Image icon;

	// Token: 0x04004665 RID: 18021
	[SerializeField]
	private LocText label;

	// Token: 0x04004666 RID: 18022
	[SerializeField]
	private MultiToggle expandButton;

	// Token: 0x04004667 RID: 18023
	private bool stateExpanded = true;
}
