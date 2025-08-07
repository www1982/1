using System;
using UnityEngine;

// Token: 0x02000E45 RID: 3653
[AddComponentMenu("KMonoBehaviour/scripts/TreeFilterableSideScreenElement")]
public class TreeFilterableSideScreenElement : KMonoBehaviour
{
	// Token: 0x06007400 RID: 29696 RVA: 0x002C0F6C File Offset: 0x002BF16C
	public Tag GetElementTag()
	{
		return this.elementTag;
	}

	// Token: 0x170007FF RID: 2047
	// (get) Token: 0x06007401 RID: 29697 RVA: 0x002C0F74 File Offset: 0x002BF174
	public bool IsSelected
	{
		get
		{
			return this.checkBox.CurrentState == 1;
		}
	}

	// Token: 0x06007402 RID: 29698 RVA: 0x002C0F84 File Offset: 0x002BF184
	public MultiToggle GetCheckboxToggle()
	{
		return this.checkBox;
	}

	// Token: 0x17000800 RID: 2048
	// (get) Token: 0x06007403 RID: 29699 RVA: 0x002C0F8C File Offset: 0x002BF18C
	// (set) Token: 0x06007404 RID: 29700 RVA: 0x002C0F94 File Offset: 0x002BF194
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

	// Token: 0x06007405 RID: 29701 RVA: 0x002C0F9D File Offset: 0x002BF19D
	private void Initialize()
	{
		if (this.initialized)
		{
			return;
		}
		this.checkBoxImg = this.checkBox.gameObject.GetComponentInChildrenOnly<KImage>();
		this.checkBox.onClick = new global::System.Action(this.CheckBoxClicked);
		this.initialized = true;
	}

	// Token: 0x06007406 RID: 29702 RVA: 0x002C0FDC File Offset: 0x002BF1DC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.Initialize();
	}

	// Token: 0x06007407 RID: 29703 RVA: 0x002C0FEC File Offset: 0x002BF1EC
	public Sprite GetStorageObjectSprite(Tag t)
	{
		Sprite sprite = null;
		GameObject prefab = Assets.GetPrefab(t);
		if (prefab != null)
		{
			KBatchedAnimController component = prefab.GetComponent<KBatchedAnimController>();
			if (component != null)
			{
				sprite = Def.GetUISpriteFromMultiObjectAnim(component.AnimFiles[0], "ui", false, "");
			}
		}
		return sprite;
	}

	// Token: 0x06007408 RID: 29704 RVA: 0x002C1038 File Offset: 0x002BF238
	public void SetSprite(Tag t)
	{
		global::Tuple<Sprite, Color> uisprite = Def.GetUISprite(t, "ui", false);
		this.elementImg.sprite = uisprite.first;
		this.elementImg.color = uisprite.second;
		this.elementImg.gameObject.SetActive(true);
	}

	// Token: 0x06007409 RID: 29705 RVA: 0x002C108C File Offset: 0x002BF28C
	public void SetTag(Tag newTag)
	{
		this.Initialize();
		this.elementTag = newTag;
		this.SetSprite(this.elementTag);
		string text = this.elementTag.ProperName();
		if (this.parent.IsStorage)
		{
			float amountInStorage = this.parent.GetAmountInStorage(this.elementTag);
			text = text + ": " + GameUtil.GetFormattedMass(amountInStorage, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}");
		}
		this.elementName.text = text;
	}

	// Token: 0x0600740A RID: 29706 RVA: 0x002C1103 File Offset: 0x002BF303
	private void CheckBoxClicked()
	{
		this.SetCheckBox(!this.parent.IsTagAllowed(this.GetElementTag()));
	}

	// Token: 0x0600740B RID: 29707 RVA: 0x002C111F File Offset: 0x002BF31F
	public void SetCheckBox(bool checkBoxState)
	{
		this.checkBox.ChangeState(checkBoxState ? 1 : 0);
		this.checkBoxImg.enabled = checkBoxState;
		if (this.OnSelectionChanged != null)
		{
			this.OnSelectionChanged(this.GetElementTag(), checkBoxState);
		}
	}

	// Token: 0x04004FDE RID: 20446
	[SerializeField]
	private LocText elementName;

	// Token: 0x04004FDF RID: 20447
	[SerializeField]
	private MultiToggle checkBox;

	// Token: 0x04004FE0 RID: 20448
	[SerializeField]
	private KImage elementImg;

	// Token: 0x04004FE1 RID: 20449
	private KImage checkBoxImg;

	// Token: 0x04004FE2 RID: 20450
	private Tag elementTag;

	// Token: 0x04004FE3 RID: 20451
	public Action<Tag, bool> OnSelectionChanged;

	// Token: 0x04004FE4 RID: 20452
	private TreeFilterableSideScreen parent;

	// Token: 0x04004FE5 RID: 20453
	private bool initialized;
}
