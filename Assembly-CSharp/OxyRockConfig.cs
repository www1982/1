using System;
using UnityEngine;

// Token: 0x02000356 RID: 854
public class OxyRockConfig : IOreConfig
{
	// Token: 0x1700004C RID: 76
	// (get) Token: 0x06001198 RID: 4504 RVA: 0x00066C17 File Offset: 0x00064E17
	public SimHashes ElementID
	{
		get
		{
			return SimHashes.OxyRock;
		}
	}

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x06001199 RID: 4505 RVA: 0x00066C1E File Offset: 0x00064E1E
	public SimHashes SublimeElementID
	{
		get
		{
			return SimHashes.Oxygen;
		}
	}

	// Token: 0x0600119A RID: 4506 RVA: 0x00066C28 File Offset: 0x00064E28
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateSolidOreEntity(this.ElementID, null);
		Sublimates sublimates = gameObject.AddOrGet<Sublimates>();
		sublimates.spawnFXHash = SpawnFXHashes.OxygenEmissionBubbles;
		sublimates.info = new Sublimates.Info(0.010000001f, 0.0050000004f, 1.8f, 0.7f, this.SublimeElementID, byte.MaxValue, 0);
		return gameObject;
	}
}
