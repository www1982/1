using System;
using UnityEngine;

// Token: 0x02000CEE RID: 3310
[AddComponentMenu("KMonoBehaviour/scripts/InfoScreenPlainText")]
public class InfoScreenPlainText : KMonoBehaviour
{
	// Token: 0x060065D8 RID: 26072 RVA: 0x0026506D File Offset: 0x0026326D
	public void SetText(string text)
	{
		this.locText.text = text;
	}

	// Token: 0x040045C3 RID: 17859
	[SerializeField]
	private LocText locText;
}
