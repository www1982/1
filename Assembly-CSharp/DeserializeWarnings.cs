using System;
using UnityEngine;

// Token: 0x020008A0 RID: 2208
[AddComponentMenu("KMonoBehaviour/scripts/DeserializeWarnings")]
public class DeserializeWarnings : KMonoBehaviour
{
	// Token: 0x06003D71 RID: 15729 RVA: 0x00156C77 File Offset: 0x00154E77
	public static void DestroyInstance()
	{
		DeserializeWarnings.Instance = null;
	}

	// Token: 0x06003D72 RID: 15730 RVA: 0x00156C7F File Offset: 0x00154E7F
	protected override void OnPrefabInit()
	{
		DeserializeWarnings.Instance = this;
	}

	// Token: 0x040025F8 RID: 9720
	public DeserializeWarnings.Warning BuildingTemeperatureIsZeroKelvin;

	// Token: 0x040025F9 RID: 9721
	public DeserializeWarnings.Warning PipeContentsTemperatureIsNan;

	// Token: 0x040025FA RID: 9722
	public DeserializeWarnings.Warning PrimaryElementTemperatureIsNan;

	// Token: 0x040025FB RID: 9723
	public DeserializeWarnings.Warning PrimaryElementHasNoElement;

	// Token: 0x040025FC RID: 9724
	public static DeserializeWarnings Instance;

	// Token: 0x02001867 RID: 6247
	public struct Warning
	{
		// Token: 0x06009C7C RID: 40060 RVA: 0x00390CFC File Offset: 0x0038EEFC
		public void Warn(string message, GameObject obj = null)
		{
			if (!this.isSet)
			{
				global::Debug.LogWarning(message, obj);
				this.isSet = true;
			}
		}

		// Token: 0x040078DD RID: 30941
		private bool isSet;
	}
}
