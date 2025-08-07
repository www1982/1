using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000E36 RID: 3638
public class SingleItemSelectionSideScreen_SelectedItemSection : KMonoBehaviour
{
	// Token: 0x170007F8 RID: 2040
	// (get) Token: 0x0600734E RID: 29518 RVA: 0x002BD574 File Offset: 0x002BB774
	// (set) Token: 0x0600734D RID: 29517 RVA: 0x002BD56B File Offset: 0x002BB76B
	public Tag Item { get; private set; }

	// Token: 0x0600734F RID: 29519 RVA: 0x002BD57C File Offset: 0x002BB77C
	public void Clear()
	{
		this.SetItem(null);
	}

	// Token: 0x06007350 RID: 29520 RVA: 0x002BD58C File Offset: 0x002BB78C
	public void SetItem(Tag item)
	{
		this.Item = item;
		if (this.Item != GameTags.Void)
		{
			this.SetTitleText(UI.UISIDESCREENS.SINGLEITEMSELECTIONSIDESCREEN.CURRENT_ITEM_SELECTED_SECTION.TITLE);
			this.SetContentText(this.Item.ProperName());
			global::Tuple<Sprite, Color> uisprite = Def.GetUISprite(this.Item, "ui", false);
			this.SetImage(uisprite.first, uisprite.second);
			return;
		}
		this.SetTitleText(UI.UISIDESCREENS.SINGLEITEMSELECTIONSIDESCREEN.CURRENT_ITEM_SELECTED_SECTION.NO_ITEM_TITLE);
		this.SetContentText(UI.UISIDESCREENS.SINGLEITEMSELECTIONSIDESCREEN.CURRENT_ITEM_SELECTED_SECTION.NO_ITEM_MESSAGE);
		this.SetImage(null, Color.white);
	}

	// Token: 0x06007351 RID: 29521 RVA: 0x002BD629 File Offset: 0x002BB829
	private void SetTitleText(string text)
	{
		this.title.text = text;
	}

	// Token: 0x06007352 RID: 29522 RVA: 0x002BD637 File Offset: 0x002BB837
	private void SetContentText(string text)
	{
		this.contentText.text = text;
	}

	// Token: 0x06007353 RID: 29523 RVA: 0x002BD645 File Offset: 0x002BB845
	private void SetImage(Sprite sprite, Color color)
	{
		this.image.sprite = sprite;
		this.image.color = color;
		this.image.gameObject.SetActive(sprite != null);
	}

	// Token: 0x04004F63 RID: 20323
	[Header("References")]
	[SerializeField]
	private LocText title;

	// Token: 0x04004F64 RID: 20324
	[SerializeField]
	private LocText contentText;

	// Token: 0x04004F65 RID: 20325
	[SerializeField]
	private KImage image;
}
