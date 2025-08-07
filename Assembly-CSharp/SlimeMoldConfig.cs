using System;
using UnityEngine;

// Token: 0x02000357 RID: 855
public class SlimeMoldConfig : IOreConfig
{
	// Token: 0x1700004E RID: 78
	// (get) Token: 0x0600119C RID: 4508 RVA: 0x00066C84 File Offset: 0x00064E84
	public SimHashes ElementID
	{
		get
		{
			return SimHashes.SlimeMold;
		}
	}

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x0600119D RID: 4509 RVA: 0x00066C8B File Offset: 0x00064E8B
	public SimHashes SublimeElementID
	{
		get
		{
			return SimHashes.ContaminatedOxygen;
		}
	}

	// Token: 0x0600119E RID: 4510 RVA: 0x00066C94 File Offset: 0x00064E94
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateSolidOreEntity(this.ElementID, null);
		Sublimates sublimates = gameObject.AddOrGet<Sublimates>();
		sublimates.spawnFXHash = SpawnFXHashes.ContaminatedOxygenBubble;
		sublimates.info = new Sublimates.Info(0.025f, 0.125f, 1.8f, 0f, this.SublimeElementID, byte.MaxValue, 0);
		return gameObject;
	}
}
