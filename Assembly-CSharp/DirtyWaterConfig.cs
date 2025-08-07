using System;
using UnityEngine;

// Token: 0x02000352 RID: 850
public class DirtyWaterConfig : IOreConfig
{
	// Token: 0x17000048 RID: 72
	// (get) Token: 0x0600118C RID: 4492 RVA: 0x000668D4 File Offset: 0x00064AD4
	public SimHashes ElementID
	{
		get
		{
			return SimHashes.DirtyWater;
		}
	}

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x0600118D RID: 4493 RVA: 0x000668DB File Offset: 0x00064ADB
	public SimHashes SublimeElementID
	{
		get
		{
			return SimHashes.ContaminatedOxygen;
		}
	}

	// Token: 0x0600118E RID: 4494 RVA: 0x000668E4 File Offset: 0x00064AE4
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLiquidOreEntity(this.ElementID, null);
		Sublimates sublimates = gameObject.AddOrGet<Sublimates>();
		sublimates.spawnFXHash = SpawnFXHashes.ContaminatedOxygenBubbleWater;
		sublimates.info = new Sublimates.Info(4.0000006E-05f, 0.025f, 1.8f, 1f, this.SublimeElementID, byte.MaxValue, 0);
		return gameObject;
	}
}
