using System;
using UnityEngine;

// Token: 0x02000358 RID: 856
public class ToxicSandConfig : IOreConfig
{
	// Token: 0x17000050 RID: 80
	// (get) Token: 0x060011A0 RID: 4512 RVA: 0x00066CF0 File Offset: 0x00064EF0
	public SimHashes ElementID
	{
		get
		{
			return SimHashes.ToxicSand;
		}
	}

	// Token: 0x17000051 RID: 81
	// (get) Token: 0x060011A1 RID: 4513 RVA: 0x00066CF7 File Offset: 0x00064EF7
	public SimHashes SublimeElementID
	{
		get
		{
			return SimHashes.ContaminatedOxygen;
		}
	}

	// Token: 0x060011A2 RID: 4514 RVA: 0x00066D00 File Offset: 0x00064F00
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateSolidOreEntity(this.ElementID, null);
		Sublimates sublimates = gameObject.AddOrGet<Sublimates>();
		sublimates.spawnFXHash = SpawnFXHashes.ContaminatedOxygenBubble;
		sublimates.info = new Sublimates.Info(2.0000001E-05f, 0.05f, 1.8f, 0.5f, this.SublimeElementID, byte.MaxValue, 0);
		return gameObject;
	}
}
