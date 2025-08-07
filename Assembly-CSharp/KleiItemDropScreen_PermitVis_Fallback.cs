using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D05 RID: 3333
public class KleiItemDropScreen_PermitVis_Fallback : KMonoBehaviour
{
	// Token: 0x060066DF RID: 26335 RVA: 0x0026DBF5 File Offset: 0x0026BDF5
	public void ConfigureWith(DropScreenPresentationInfo info)
	{
		this.sprite.sprite = info.Sprite;
	}

	// Token: 0x0400468A RID: 18058
	[SerializeField]
	private Image sprite;
}
