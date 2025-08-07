using System;
using Database;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D17 RID: 3351
public class KleiPermitDioramaVis_Fallback : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x06006737 RID: 26423 RVA: 0x0026F3B1 File Offset: 0x0026D5B1
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x06006738 RID: 26424 RVA: 0x0026F3B9 File Offset: 0x0026D5B9
	public void ConfigureSetup()
	{
	}

	// Token: 0x06006739 RID: 26425 RVA: 0x0026F3BB File Offset: 0x0026D5BB
	public void ConfigureWith(PermitResource permit)
	{
		this.sprite.sprite = PermitPresentationInfo.GetUnknownSprite();
		this.editorOnlyErrorMessageParent.gameObject.SetActive(false);
	}

	// Token: 0x0600673A RID: 26426 RVA: 0x0026F3DE File Offset: 0x0026D5DE
	public KleiPermitDioramaVis_Fallback WithError(string error)
	{
		this.error = error;
		global::Debug.Log("[KleiInventoryScreen Error] Had to use fallback vis. " + error);
		return this;
	}

	// Token: 0x040046C3 RID: 18115
	[SerializeField]
	private Image sprite;

	// Token: 0x040046C4 RID: 18116
	[SerializeField]
	private RectTransform editorOnlyErrorMessageParent;

	// Token: 0x040046C5 RID: 18117
	[SerializeField]
	private TextMeshProUGUI editorOnlyErrorMessageText;

	// Token: 0x040046C6 RID: 18118
	private Option<string> error;
}
