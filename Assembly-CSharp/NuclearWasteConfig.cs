using System;
using UnityEngine;

// Token: 0x02000355 RID: 853
public class NuclearWasteConfig : IOreConfig
{
	// Token: 0x1700004B RID: 75
	// (get) Token: 0x06001195 RID: 4501 RVA: 0x00066BAD File Offset: 0x00064DAD
	public SimHashes ElementID
	{
		get
		{
			return SimHashes.NuclearWaste;
		}
	}

	// Token: 0x06001196 RID: 4502 RVA: 0x00066BB4 File Offset: 0x00064DB4
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLiquidOreEntity(this.ElementID, null);
		Sublimates sublimates = gameObject.AddOrGet<Sublimates>();
		sublimates.decayStorage = true;
		sublimates.spawnFXHash = SpawnFXHashes.NuclearWasteDrip;
		sublimates.info = new Sublimates.Info(0.066f, 6.6f, 1000f, 0f, this.ElementID, byte.MaxValue, 0);
		return gameObject;
	}
}
