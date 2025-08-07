using System;
using UnityEngine;

// Token: 0x0200060D RID: 1549
[AddComponentMenu("KMonoBehaviour/scripts/ScannerNetworkVisualizer")]
public class ScannerNetworkVisualizer : KMonoBehaviour
{
	// Token: 0x060024DB RID: 9435 RVA: 0x000D2A93 File Offset: 0x000D0C93
	protected override void OnSpawn()
	{
		Components.ScannerVisualizers.Add(base.gameObject.GetMyWorldId(), this);
	}

	// Token: 0x060024DC RID: 9436 RVA: 0x000D2AAB File Offset: 0x000D0CAB
	protected override void OnCleanUp()
	{
		Components.ScannerVisualizers.Remove(base.gameObject.GetMyWorldId(), this);
	}

	// Token: 0x0400159D RID: 5533
	public Vector2I OriginOffset = new Vector2I(0, 0);

	// Token: 0x0400159E RID: 5534
	public int RangeMin;

	// Token: 0x0400159F RID: 5535
	public int RangeMax;
}
