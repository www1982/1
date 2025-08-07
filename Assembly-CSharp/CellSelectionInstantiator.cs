using System;
using UnityEngine;

// Token: 0x02000804 RID: 2052
public class CellSelectionInstantiator : MonoBehaviour
{
	// Token: 0x060037D9 RID: 14297 RVA: 0x00135B0C File Offset: 0x00133D0C
	private void Awake()
	{
		GameObject gameObject = Util.KInstantiate(this.CellSelectionPrefab, null, "WorldSelectionCollider");
		GameObject gameObject2 = Util.KInstantiate(this.CellSelectionPrefab, null, "WorldSelectionCollider");
		CellSelectionObject component = gameObject.GetComponent<CellSelectionObject>();
		CellSelectionObject component2 = gameObject2.GetComponent<CellSelectionObject>();
		component.alternateSelectionObject = component2;
		component2.alternateSelectionObject = component;
	}

	// Token: 0x040021E9 RID: 8681
	public GameObject CellSelectionPrefab;
}
