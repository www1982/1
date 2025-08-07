using System;
using Database;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D1B RID: 3355
public class KleiPermitDioramaVis_WiresAndAutomation : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x0600674F RID: 26447 RVA: 0x0026F8C0 File Offset: 0x0026DAC0
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x06006750 RID: 26448 RVA: 0x0026F8C8 File Offset: 0x0026DAC8
	public void ConfigureSetup()
	{
	}

	// Token: 0x06006751 RID: 26449 RVA: 0x0026F8CC File Offset: 0x0026DACC
	public void ConfigureWith(PermitResource permit)
	{
		PermitPresentationInfo permitPresentationInfo = permit.GetPermitPresentationInfo();
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

	// Token: 0x040046D8 RID: 18136
	[SerializeField]
	private Image itemSprite;

	// Token: 0x040046D9 RID: 18137
	private bool itemSpriteDidInit;

	// Token: 0x040046DA RID: 18138
	private Vector2 itemSpritePosStart;

	// Token: 0x040046DB RID: 18139
	private Vector2 itemSpritePosEnd;
}
