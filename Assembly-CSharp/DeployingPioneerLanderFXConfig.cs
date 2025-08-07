using System;
using UnityEngine;

// Token: 0x020002FF RID: 767
public class DeployingPioneerLanderFXConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000FC5 RID: 4037 RVA: 0x0005F085 File Offset: 0x0005D285
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000FC6 RID: 4038 RVA: 0x0005F08C File Offset: 0x0005D28C
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000FC7 RID: 4039 RVA: 0x0005F08F File Offset: 0x0005D28F
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity("DeployingPioneerLanderFX", "DeployingPioneerLanderFX", false);
		ClusterFXEntity clusterFXEntity = gameObject.AddOrGet<ClusterFXEntity>();
		clusterFXEntity.kAnimName = "pioneer01_kanim";
		clusterFXEntity.animName = "landing";
		clusterFXEntity.animPlayMode = KAnim.PlayMode.Loop;
		return gameObject;
	}

	// Token: 0x06000FC8 RID: 4040 RVA: 0x0005F0C3 File Offset: 0x0005D2C3
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000FC9 RID: 4041 RVA: 0x0005F0C5 File Offset: 0x0005D2C5
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000A12 RID: 2578
	public const string ID = "DeployingPioneerLanderFX";
}
