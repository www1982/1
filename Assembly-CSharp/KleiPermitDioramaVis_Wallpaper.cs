using System;
using Database;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D1A RID: 3354
public class KleiPermitDioramaVis_Wallpaper : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x06006749 RID: 26441 RVA: 0x0026F766 File Offset: 0x0026D966
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x0600674A RID: 26442 RVA: 0x0026F76E File Offset: 0x0026D96E
	public void ConfigureSetup()
	{
	}

	// Token: 0x0600674B RID: 26443 RVA: 0x0026F770 File Offset: 0x0026D970
	public void ConfigureWith(PermitResource permit)
	{
		PermitPresentationInfo permitPresentationInfo = permit.GetPermitPresentationInfo();
		this.itemSprite.rectTransform().sizeDelta = Vector2.one * 176f;
		this.itemSprite.sprite = permitPresentationInfo.sprite;
		if (!this.itemSpriteDidInit)
		{
			this.itemSpriteDidInit = true;
			this.itemSpritePosStart = this.itemSprite.rectTransform.anchoredPosition + new Vector2(0f, 16f);
			this.itemSpritePosEnd = this.itemSprite.rectTransform.anchoredPosition;
		}
		this.itemSprite.StartCoroutine(Updater.Parallel(new Updater[]
		{
			Updater.Ease(delegate(float alpha)
			{
				this.itemSprite.color = new Color(1f, 1f, 1f, alpha);
			}, 0f, 1f, 0.2f, Easing.SmoothStep, 0.1f),
			Updater.Ease(delegate(Vector2 position)
			{
				this.itemSprite.rectTransform.anchoredPosition = position;
			}, this.itemSpritePosStart, this.itemSpritePosEnd, 0.2f, Easing.SmoothStep, 0.1f)
		}));
	}

	// Token: 0x040046D4 RID: 18132
	[SerializeField]
	private Image itemSprite;

	// Token: 0x040046D5 RID: 18133
	private bool itemSpriteDidInit;

	// Token: 0x040046D6 RID: 18134
	private Vector2 itemSpritePosStart;

	// Token: 0x040046D7 RID: 18135
	private Vector2 itemSpritePosEnd;
}
