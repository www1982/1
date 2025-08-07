using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000E46 RID: 3654
[AddComponentMenu("KMonoBehaviour/scripts/TreeFilterableSideScreenRow")]
public class TreeFilterableSideScreenRow : KMonoBehaviour
{
	// Token: 0x17000801 RID: 2049
	// (get) Token: 0x0600740D RID: 29709 RVA: 0x002C1161 File Offset: 0x002BF361
	// (set) Token: 0x0600740E RID: 29710 RVA: 0x002C1169 File Offset: 0x002BF369
	public bool ArrowExpanded { get; private set; }

	// Token: 0x17000802 RID: 2050
	// (get) Token: 0x0600740F RID: 29711 RVA: 0x002C1172 File Offset: 0x002BF372
	// (set) Token: 0x06007410 RID: 29712 RVA: 0x002C117A File Offset: 0x002BF37A
	public TreeFilterableSideScreen Parent
	{
		get
		{
			return this.parent;
		}
		set
		{
			this.parent = value;
		}
	}

	// Token: 0x06007411 RID: 29713 RVA: 0x002C1184 File Offset: 0x002BF384
	public TreeFilterableSideScreenRow.State GetState()
	{
		bool flag = false;
		bool flag2 = false;
		foreach (TreeFilterableSideScreenElement treeFilterableSideScreenElement in this.rowElements)
		{
			if (this.parent.GetElementTagAcceptedState(treeFilterableSideScreenElement.GetElementTag()))
			{
				flag = true;
			}
			else
			{
				flag2 = true;
			}
		}
		if (flag && !flag2)
		{
			return TreeFilterableSideScreenRow.State.On;
		}
		if (!flag && flag2)
		{
			return TreeFilterableSideScreenRow.State.Off;
		}
		if (flag && flag2)
		{
			return TreeFilterableSideScreenRow.State.Mixed;
		}
		if (this.rowElements.Count <= 0)
		{
			return TreeFilterableSideScreenRow.State.Off;
		}
		return TreeFilterableSideScreenRow.State.On;
	}

	// Token: 0x06007412 RID: 29714 RVA: 0x002C1218 File Offset: 0x002BF418
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		MultiToggle multiToggle = this.checkBoxToggle;
		multiToggle.onClick = (global::System.Action)Delegate.Combine(multiToggle.onClick, new global::System.Action(delegate
		{
			if (this.parent.CurrentSearchValue == "")
			{
				TreeFilterableSideScreenRow.State state = this.GetState();
				if (state > TreeFilterableSideScreenRow.State.Mixed)
				{
					if (state == TreeFilterableSideScreenRow.State.On)
					{
						this.ChangeCheckBoxState(TreeFilterableSideScreenRow.State.Off);
						return;
					}
				}
				else
				{
					this.ChangeCheckBoxState(TreeFilterableSideScreenRow.State.On);
				}
			}
		}));
	}

	// Token: 0x06007413 RID: 29715 RVA: 0x002C1247 File Offset: 0x002BF447
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		this.SetArrowToggleState(this.GetState() > TreeFilterableSideScreenRow.State.Off);
	}

	// Token: 0x06007414 RID: 29716 RVA: 0x002C125E File Offset: 0x002BF45E
	protected override void OnCmpDisable()
	{
		this.SetArrowToggleState(false);
		base.OnCmpDisable();
	}

	// Token: 0x06007415 RID: 29717 RVA: 0x002C126D File Offset: 0x002BF46D
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x06007416 RID: 29718 RVA: 0x002C1275 File Offset: 0x002BF475
	public void UpdateCheckBoxVisualState()
	{
		this.checkBoxToggle.ChangeState((int)this.GetState());
		this.visualDirty = false;
	}

	// Token: 0x06007417 RID: 29719 RVA: 0x002C1290 File Offset: 0x002BF490
	public void ChangeCheckBoxState(TreeFilterableSideScreenRow.State newState)
	{
		switch (newState)
		{
		case TreeFilterableSideScreenRow.State.Off:
		{
			for (int i = 0; i < this.rowElements.Count; i++)
			{
				this.rowElements[i].SetCheckBox(false);
			}
			break;
		}
		case TreeFilterableSideScreenRow.State.On:
		{
			for (int j = 0; j < this.rowElements.Count; j++)
			{
				this.rowElements[j].SetCheckBox(true);
			}
			break;
		}
		}
		this.visualDirty = true;
	}

	// Token: 0x06007418 RID: 29720 RVA: 0x002C130A File Offset: 0x002BF50A
	private void ArrowToggleClicked()
	{
		this.SetArrowToggleState(!this.ArrowExpanded);
		this.RefreshArrowToggleState();
	}

	// Token: 0x06007419 RID: 29721 RVA: 0x002C1321 File Offset: 0x002BF521
	public void SetArrowToggleState(bool state)
	{
		this.ArrowExpanded = state;
		this.RefreshArrowToggleState();
	}

	// Token: 0x0600741A RID: 29722 RVA: 0x002C1330 File Offset: 0x002BF530
	private void RefreshArrowToggleState()
	{
		this.arrowToggle.ChangeState(this.ArrowExpanded ? 1 : 0);
		this.elementGroup.SetActive(this.ArrowExpanded);
		this.bgImg.enabled = this.ArrowExpanded;
	}

	// Token: 0x0600741B RID: 29723 RVA: 0x002C136B File Offset: 0x002BF56B
	private void ArrowToggleDisabledClick()
	{
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Negative", false));
	}

	// Token: 0x0600741C RID: 29724 RVA: 0x002C137D File Offset: 0x002BF57D
	public void ShowToggleBox(bool show)
	{
		this.checkBoxToggle.gameObject.SetActive(show);
	}

	// Token: 0x0600741D RID: 29725 RVA: 0x002C1390 File Offset: 0x002BF590
	private void OnElementSelectionChanged(Tag t, bool state)
	{
		if (state)
		{
			this.parent.AddTag(t);
		}
		else
		{
			this.parent.RemoveTag(t);
		}
		this.visualDirty = true;
	}

	// Token: 0x0600741E RID: 29726 RVA: 0x002C13B8 File Offset: 0x002BF5B8
	public void SetElement(Tag mainElementTag, bool state, Dictionary<Tag, bool> filterMap)
	{
		this.subTags.Clear();
		this.rowElements.Clear();
		this.elementName.text = mainElementTag.ProperName();
		this.bgImg.enabled = false;
		string text = string.Format(UI.UISIDESCREENS.TREEFILTERABLESIDESCREEN.CATEGORYBUTTONTOOLTIP, mainElementTag.ProperName());
		this.checkBoxToggle.GetComponent<ToolTip>().SetSimpleTooltip(text);
		if (filterMap.Count == 0)
		{
			if (this.elementGroup.activeInHierarchy)
			{
				this.elementGroup.SetActive(false);
			}
			this.arrowToggle.onClick = new global::System.Action(this.ArrowToggleDisabledClick);
			this.arrowToggle.ChangeState(0);
		}
		else
		{
			this.arrowToggle.onClick = new global::System.Action(this.ArrowToggleClicked);
			this.arrowToggle.ChangeState(0);
			foreach (KeyValuePair<Tag, bool> keyValuePair in filterMap)
			{
				TreeFilterableSideScreenElement freeElement = this.parent.elementPool.GetFreeElement(this.elementGroup, true);
				freeElement.Parent = this.parent;
				freeElement.SetTag(keyValuePair.Key);
				freeElement.SetCheckBox(keyValuePair.Value);
				freeElement.OnSelectionChanged = new Action<Tag, bool>(this.OnElementSelectionChanged);
				freeElement.SetCheckBox(this.parent.IsTagAllowed(keyValuePair.Key));
				this.rowElements.Add(freeElement);
				this.subTags.Add(keyValuePair.Key);
			}
		}
		this.UpdateCheckBoxVisualState();
	}

	// Token: 0x0600741F RID: 29727 RVA: 0x002C1558 File Offset: 0x002BF758
	public void RefreshRowElements()
	{
		foreach (TreeFilterableSideScreenElement treeFilterableSideScreenElement in this.rowElements)
		{
			treeFilterableSideScreenElement.SetCheckBox(this.parent.IsTagAllowed(treeFilterableSideScreenElement.GetElementTag()));
		}
	}

	// Token: 0x06007420 RID: 29728 RVA: 0x002C15BC File Offset: 0x002BF7BC
	public void FilterAgainstSearch(Tag thisCategoryTag, string search)
	{
		bool flag = false;
		bool flag2 = thisCategoryTag.ProperNameStripLink().ToUpper().Contains(search.ToUpper());
		search = search.ToUpper();
		foreach (TreeFilterableSideScreenElement treeFilterableSideScreenElement in this.rowElements)
		{
			bool flag3 = flag2 || treeFilterableSideScreenElement.GetElementTag().ProperNameStripLink().ToUpper()
				.Contains(search.ToUpper());
			treeFilterableSideScreenElement.gameObject.SetActive(flag3);
			flag = flag || flag3;
		}
		base.gameObject.SetActive(flag);
		if (search != "" && flag && this.arrowToggle.CurrentState == 0)
		{
			this.SetArrowToggleState(true);
		}
	}

	// Token: 0x04004FE6 RID: 20454
	public bool visualDirty;

	// Token: 0x04004FE7 RID: 20455
	public bool standardCommodity = true;

	// Token: 0x04004FE8 RID: 20456
	[SerializeField]
	private LocText elementName;

	// Token: 0x04004FE9 RID: 20457
	[SerializeField]
	private GameObject elementGroup;

	// Token: 0x04004FEA RID: 20458
	[SerializeField]
	private MultiToggle checkBoxToggle;

	// Token: 0x04004FEB RID: 20459
	[SerializeField]
	private MultiToggle arrowToggle;

	// Token: 0x04004FEC RID: 20460
	[SerializeField]
	private KImage bgImg;

	// Token: 0x04004FED RID: 20461
	private List<Tag> subTags = new List<Tag>();

	// Token: 0x04004FEE RID: 20462
	private List<TreeFilterableSideScreenElement> rowElements = new List<TreeFilterableSideScreenElement>();

	// Token: 0x04004FEF RID: 20463
	private TreeFilterableSideScreen parent;

	// Token: 0x0200203E RID: 8254
	public enum State
	{
		// Token: 0x04009373 RID: 37747
		Off,
		// Token: 0x04009374 RID: 37748
		Mixed,
		// Token: 0x04009375 RID: 37749
		On
	}
}
