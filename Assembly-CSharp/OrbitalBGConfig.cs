using System;
using UnityEngine;

// Token: 0x02000322 RID: 802
public class OrbitalBGConfig : IEntityConfig
{
	// Token: 0x06001089 RID: 4233 RVA: 0x00062852 File Offset: 0x00060A52
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(OrbitalBGConfig.ID, OrbitalBGConfig.ID, false);
		gameObject.AddOrGet<LoopingSounds>();
		gameObject.AddOrGet<OrbitalObject>();
		gameObject.AddOrGet<SaveLoadRoot>();
		return gameObject;
	}

	// Token: 0x0600108A RID: 4234 RVA: 0x00062879 File Offset: 0x00060A79
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x0600108B RID: 4235 RVA: 0x0006287B File Offset: 0x00060A7B
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000A83 RID: 2691
	public static string ID = "OrbitalBG";
}
