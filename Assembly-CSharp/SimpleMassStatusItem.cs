using System;
using UnityEngine;

// Token: 0x02000610 RID: 1552
[AddComponentMenu("KMonoBehaviour/scripts/SimpleMassStatusItem")]
public class SimpleMassStatusItem : KMonoBehaviour
{
	// Token: 0x060024E4 RID: 9444 RVA: 0x000D2BC6 File Offset: 0x000D0DC6
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.GetComponent<KSelectable>().AddStatusItem(Db.Get().MiscStatusItems.OreMass, base.gameObject);
	}

	// Token: 0x040015A1 RID: 5537
	public string symbolPrefix = "";
}
