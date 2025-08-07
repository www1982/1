using System;
using UnityEngine;

// Token: 0x02000E54 RID: 3668
[AddComponentMenu("KMonoBehaviour/scripts/SpawnScreen")]
public class SpawnScreen : KMonoBehaviour
{
	// Token: 0x060074C3 RID: 29891 RVA: 0x002C9676 File Offset: 0x002C7876
	protected override void OnPrefabInit()
	{
		Util.KInstantiateUI(this.Screen, base.gameObject, false);
	}

	// Token: 0x040050C3 RID: 20675
	public GameObject Screen;
}
