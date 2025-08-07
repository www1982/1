using System;
using UnityEngine;

// Token: 0x02000351 RID: 849
public class BleachStoneConfig : IOreConfig
{
	// Token: 0x17000046 RID: 70
	// (get) Token: 0x06001188 RID: 4488 RVA: 0x00066867 File Offset: 0x00064A67
	public SimHashes ElementID
	{
		get
		{
			return SimHashes.BleachStone;
		}
	}

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x06001189 RID: 4489 RVA: 0x0006686E File Offset: 0x00064A6E
	public SimHashes SublimeElementID
	{
		get
		{
			return SimHashes.ChlorineGas;
		}
	}

	// Token: 0x0600118A RID: 4490 RVA: 0x00066878 File Offset: 0x00064A78
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateSolidOreEntity(this.ElementID, null);
		Sublimates sublimates = gameObject.AddOrGet<Sublimates>();
		sublimates.spawnFXHash = SpawnFXHashes.BleachStoneEmissionBubbles;
		sublimates.info = new Sublimates.Info(0.00020000001f, 0.0025000002f, 1.8f, 0.5f, this.SublimeElementID, byte.MaxValue, 0);
		return gameObject;
	}
}
