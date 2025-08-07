using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CEF RID: 3311
[AddComponentMenu("KMonoBehaviour/scripts/InfoScreenSpriteItem")]
public class InfoScreenSpriteItem : KMonoBehaviour
{
	// Token: 0x060065DA RID: 26074 RVA: 0x00265084 File Offset: 0x00263284
	public void SetSprite(Sprite sprite)
	{
		this.image.sprite = sprite;
		float num = sprite.rect.width / sprite.rect.height;
		this.layout.preferredWidth = this.layout.preferredHeight * num;
	}

	// Token: 0x040045C4 RID: 17860
	[SerializeField]
	private Image image;

	// Token: 0x040045C5 RID: 17861
	[SerializeField]
	private LayoutElement layout;
}
