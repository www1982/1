using System;
using KSerialization;
using UnityEngine;

// Token: 0x020007B4 RID: 1972
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/Refinery")]
public class Refinery : KMonoBehaviour
{
	// Token: 0x06003486 RID: 13446 RVA: 0x001263CC File Offset: 0x001245CC
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x020016D7 RID: 5847
	[Serializable]
	public struct OrderSaveData
	{
		// Token: 0x060096D1 RID: 38609 RVA: 0x0037A1C4 File Offset: 0x003783C4
		public OrderSaveData(string id, bool infinite)
		{
			this.id = id;
			this.infinite = infinite;
		}

		// Token: 0x040073F7 RID: 29687
		public string id;

		// Token: 0x040073F8 RID: 29688
		public bool infinite;
	}
}
