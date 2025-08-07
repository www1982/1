using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000370 RID: 880
public class DigPlacerConfig : CommonPlacerConfig, IEntityConfig
{
	// Token: 0x0600120A RID: 4618 RVA: 0x00069680 File Offset: 0x00067880
	public GameObject CreatePrefab()
	{
		GameObject gameObject = base.CreatePrefab(DigPlacerConfig.ID, MISC.PLACERS.DIGPLACER.NAME, Assets.instance.digPlacerAssets.materials[0]);
		Diggable diggable = gameObject.AddOrGet<Diggable>();
		diggable.workTime = 5f;
		diggable.synchronizeAnims = false;
		diggable.workAnims = new HashedString[] { "place", "release" };
		diggable.materials = Assets.instance.digPlacerAssets.materials;
		diggable.materialDisplay = gameObject.GetComponentInChildren<MeshRenderer>(true);
		gameObject.AddOrGet<CancellableDig>();
		return gameObject;
	}

	// Token: 0x0600120B RID: 4619 RVA: 0x00069725 File Offset: 0x00067925
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x0600120C RID: 4620 RVA: 0x00069727 File Offset: 0x00067927
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000B75 RID: 2933
	public static string ID = "DigPlacer";

	// Token: 0x020011F3 RID: 4595
	[Serializable]
	public class DigPlacerAssets
	{
		// Token: 0x040064BE RID: 25790
		public Material[] materials;
	}
}
