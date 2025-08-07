using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C8A RID: 3210
public class CodexImage : CodexWidget<CodexImage>
{
	// Token: 0x1700072F RID: 1839
	// (get) Token: 0x060062C8 RID: 25288 RVA: 0x00251D38 File Offset: 0x0024FF38
	// (set) Token: 0x060062C9 RID: 25289 RVA: 0x00251D40 File Offset: 0x0024FF40
	public Sprite sprite { get; set; }

	// Token: 0x17000730 RID: 1840
	// (get) Token: 0x060062CA RID: 25290 RVA: 0x00251D49 File Offset: 0x0024FF49
	// (set) Token: 0x060062CB RID: 25291 RVA: 0x00251D51 File Offset: 0x0024FF51
	public Color color { get; set; }

	// Token: 0x17000731 RID: 1841
	// (get) Token: 0x060062CD RID: 25293 RVA: 0x00251D6D File Offset: 0x0024FF6D
	// (set) Token: 0x060062CC RID: 25292 RVA: 0x00251D5A File Offset: 0x0024FF5A
	public string spriteName
	{
		get
		{
			return "--> " + ((this.sprite == null) ? "NULL" : this.sprite.ToString());
		}
		set
		{
			this.sprite = Assets.GetSprite(value);
		}
	}

	// Token: 0x17000732 RID: 1842
	// (get) Token: 0x060062CF RID: 25295 RVA: 0x00251E00 File Offset: 0x00250000
	// (set) Token: 0x060062CE RID: 25294 RVA: 0x00251D9C File Offset: 0x0024FF9C
	public string batchedAnimPrefabSourceID
	{
		get
		{
			return "--> " + ((this.sprite == null) ? "NULL" : this.sprite.ToString());
		}
		set
		{
			GameObject gameObject = Assets.TryGetPrefab(value);
			KBatchedAnimController kbatchedAnimController = ((gameObject != null) ? gameObject.GetComponent<KBatchedAnimController>() : null);
			KAnimFile kanimFile = ((kbatchedAnimController != null) ? kbatchedAnimController.AnimFiles[0] : null);
			this.sprite = ((kanimFile != null) ? Def.GetUISpriteFromMultiObjectAnim(kanimFile, "ui", false, "") : null);
		}
	}

	// Token: 0x17000733 RID: 1843
	// (get) Token: 0x060062D1 RID: 25297 RVA: 0x00251E68 File Offset: 0x00250068
	// (set) Token: 0x060062D0 RID: 25296 RVA: 0x00251E2C File Offset: 0x0025002C
	public string elementIcon
	{
		get
		{
			return "";
		}
		set
		{
			global::Tuple<Sprite, Color> uisprite = Def.GetUISprite(value.ToTag(), "ui", false);
			this.sprite = uisprite.first;
			this.color = uisprite.second;
		}
	}

	// Token: 0x060062D2 RID: 25298 RVA: 0x00251E6F File Offset: 0x0025006F
	public CodexImage()
	{
		this.color = Color.white;
	}

	// Token: 0x060062D3 RID: 25299 RVA: 0x00251E82 File Offset: 0x00250082
	public CodexImage(int preferredWidth, int preferredHeight, Sprite sprite, Color color)
		: base(preferredWidth, preferredHeight)
	{
		this.sprite = sprite;
		this.color = color;
	}

	// Token: 0x060062D4 RID: 25300 RVA: 0x00251E9B File Offset: 0x0025009B
	public CodexImage(int preferredWidth, int preferredHeight, Sprite sprite)
		: this(preferredWidth, preferredHeight, sprite, Color.white)
	{
	}

	// Token: 0x060062D5 RID: 25301 RVA: 0x00251EAB File Offset: 0x002500AB
	public CodexImage(int preferredWidth, int preferredHeight, global::Tuple<Sprite, Color> coloredSprite)
		: this(preferredWidth, preferredHeight, coloredSprite.first, coloredSprite.second)
	{
	}

	// Token: 0x060062D6 RID: 25302 RVA: 0x00251EC1 File Offset: 0x002500C1
	public CodexImage(global::Tuple<Sprite, Color> coloredSprite)
		: this(-1, -1, coloredSprite)
	{
	}

	// Token: 0x060062D7 RID: 25303 RVA: 0x00251ECC File Offset: 0x002500CC
	public void ConfigureImage(Image image)
	{
		image.sprite = this.sprite;
		image.color = this.color;
	}

	// Token: 0x060062D8 RID: 25304 RVA: 0x00251EE6 File Offset: 0x002500E6
	public override void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		this.ConfigureImage(contentGameObject.GetComponent<Image>());
		base.ConfigurePreferredLayout(contentGameObject);
	}
}
