using System;
using UnityEngine;

// Token: 0x020008EF RID: 2287
[AddComponentMenu("KMonoBehaviour/scripts/EntityPrefabs")]
public class EntityPrefabs : KMonoBehaviour
{
	// Token: 0x17000491 RID: 1169
	// (get) Token: 0x06003FDB RID: 16347 RVA: 0x00166825 File Offset: 0x00164A25
	// (set) Token: 0x06003FDC RID: 16348 RVA: 0x0016682C File Offset: 0x00164A2C
	public static EntityPrefabs Instance { get; private set; }

	// Token: 0x06003FDD RID: 16349 RVA: 0x00166834 File Offset: 0x00164A34
	public static void DestroyInstance()
	{
		EntityPrefabs.Instance = null;
	}

	// Token: 0x06003FDE RID: 16350 RVA: 0x0016683C File Offset: 0x00164A3C
	protected override void OnPrefabInit()
	{
		EntityPrefabs.Instance = this;
	}

	// Token: 0x040027A1 RID: 10145
	public GameObject SelectMarker;

	// Token: 0x040027A2 RID: 10146
	public GameObject ForegroundLayer;
}
