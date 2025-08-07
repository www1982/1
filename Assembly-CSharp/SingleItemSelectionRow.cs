using System;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E33 RID: 3635
public class SingleItemSelectionRow : KMonoBehaviour
{
	// Token: 0x170007F2 RID: 2034
	// (get) Token: 0x0600731D RID: 29469 RVA: 0x002BC9F8 File Offset: 0x002BABF8
	public virtual string InvalidTagTitle
	{
		get
		{
			return UI.UISIDESCREENS.SINGLEITEMSELECTIONSIDESCREEN.NO_SELECTION;
		}
	}

	// Token: 0x170007F3 RID: 2035
	// (get) Token: 0x0600731E RID: 29470 RVA: 0x002BCA04 File Offset: 0x002BAC04
	// (set) Token: 0x0600731F RID: 29471 RVA: 0x002BCA0C File Offset: 0x002BAC0C
	public Tag InvalidTag { get; protected set; } = GameTags.Void;

	// Token: 0x170007F4 RID: 2036
	// (get) Token: 0x06007320 RID: 29472 RVA: 0x002BCA15 File Offset: 0x002BAC15
	// (set) Token: 0x06007321 RID: 29473 RVA: 0x002BCA1D File Offset: 0x002BAC1D
	public new Tag tag { get; protected set; }

	// Token: 0x170007F5 RID: 2037
	// (get) Token: 0x06007322 RID: 29474 RVA: 0x002BCA26 File Offset: 0x002BAC26
	public bool IsVisible
	{
		get
		{
			return base.gameObject.activeSelf;
		}
	}

	// Token: 0x170007F6 RID: 2038
	// (get) Token: 0x06007323 RID: 29475 RVA: 0x002BCA33 File Offset: 0x002BAC33
	// (set) Token: 0x06007324 RID: 29476 RVA: 0x002BCA3B File Offset: 0x002BAC3B
	public bool IsSelected { get; protected set; }

	// Token: 0x06007325 RID: 29477 RVA: 0x002BCA44 File Offset: 0x002BAC44
	protected override void OnPrefabInit()
	{
		this.regularColor = this.outline.color;
		base.OnPrefabInit();
	}

	// Token: 0x06007326 RID: 29478 RVA: 0x002BCA60 File Offset: 0x002BAC60
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.button != null)
		{
			this.button.onPointerEnter += delegate
			{
				if (!this.IsSelected)
				{
					this.outline.color = this.outlineHighLightColor;
				}
			};
			this.button.onPointerExit += delegate
			{
				if (!this.IsSelected)
				{
					this.outline.color = this.regularColor;
				}
			};
			this.button.onClick += this.OnItemClicked;
		}
	}

	// Token: 0x06007327 RID: 29479 RVA: 0x002BCAC7 File Offset: 0x002BACC7
	public virtual void SetVisibleState(bool isVisible)
	{
		base.gameObject.SetActive(isVisible);
	}

	// Token: 0x06007328 RID: 29480 RVA: 0x002BCAD5 File Offset: 0x002BACD5
	protected virtual void OnItemClicked()
	{
		Action<SingleItemSelectionRow> clicked = this.Clicked;
		if (clicked == null)
		{
			return;
		}
		clicked(this);
	}

	// Token: 0x06007329 RID: 29481 RVA: 0x002BCAE8 File Offset: 0x002BACE8
	public virtual void SetTag(Tag tag)
	{
		this.tag = tag;
		this.SetText((tag == this.InvalidTag) ? this.InvalidTagTitle : tag.ProperName());
		if (tag != this.InvalidTag)
		{
			global::Tuple<Sprite, Color> uisprite = Def.GetUISprite(tag, "ui", false);
			this.SetIcon(uisprite.first, uisprite.second);
			return;
		}
		this.SetIcon(null, Color.white);
	}

	// Token: 0x0600732A RID: 29482 RVA: 0x002BCB5D File Offset: 0x002BAD5D
	protected virtual void SetText(string assignmentStr)
	{
		this.labelText.text = ((!string.IsNullOrEmpty(assignmentStr)) ? assignmentStr : "-");
	}

	// Token: 0x0600732B RID: 29483 RVA: 0x002BCB7A File Offset: 0x002BAD7A
	public virtual void SetSelected(bool selected)
	{
		this.IsSelected = selected;
		this.outline.color = (selected ? this.outlineHighLightColor : this.outlineDefaultColor);
		this.BG.color = (selected ? this.BGHighLightColor : Color.white);
	}

	// Token: 0x0600732C RID: 29484 RVA: 0x002BCBBA File Offset: 0x002BADBA
	protected virtual void SetIcon(Sprite sprite, Color color)
	{
		this.icon.sprite = sprite;
		this.icon.color = color;
		this.icon.gameObject.SetActive(sprite != null);
	}

	// Token: 0x04004F4A RID: 20298
	[SerializeField]
	protected Image icon;

	// Token: 0x04004F4B RID: 20299
	[SerializeField]
	protected LocText labelText;

	// Token: 0x04004F4C RID: 20300
	[SerializeField]
	protected Image BG;

	// Token: 0x04004F4D RID: 20301
	[SerializeField]
	protected Image outline;

	// Token: 0x04004F4E RID: 20302
	[SerializeField]
	protected Color outlineHighLightColor = new Color32(168, 74, 121, byte.MaxValue);

	// Token: 0x04004F4F RID: 20303
	[SerializeField]
	protected Color BGHighLightColor = new Color32(168, 74, 121, 80);

	// Token: 0x04004F50 RID: 20304
	[SerializeField]
	protected Color outlineDefaultColor = new Color32(204, 204, 204, byte.MaxValue);

	// Token: 0x04004F51 RID: 20305
	protected Color regularColor = Color.white;

	// Token: 0x04004F52 RID: 20306
	[SerializeField]
	public KButton button;

	// Token: 0x04004F56 RID: 20310
	public Action<SingleItemSelectionRow> Clicked;
}
