using System;
using UnityEngine;

// Token: 0x02000300 RID: 768
public class DeployingScoutLanderFXConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000FCB RID: 4043 RVA: 0x0005F0CF File Offset: 0x0005D2CF
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000FCC RID: 4044 RVA: 0x0005F0D6 File Offset: 0x0005D2D6
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000FCD RID: 4045 RVA: 0x0005F0D9 File Offset: 0x0005D2D9
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity("DeployingScoutLanderFXConfig", "DeployingScoutLanderFXConfig", false);
		ClusterFXEntity clusterFXEntity = gameObject.AddOrGet<ClusterFXEntity>();
		clusterFXEntity.kAnimName = "rover01_kanim";
		clusterFXEntity.animName = "landing";
		clusterFXEntity.animPlayMode = KAnim.PlayMode.Loop;
		return gameObject;
	}

	// Token: 0x06000FCE RID: 4046 RVA: 0x0005F10D File Offset: 0x0005D30D
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000FCF RID: 4047 RVA: 0x0005F10F File Offset: 0x0005D30F
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000A13 RID: 2579
	public const string ID = "DeployingScoutLanderFXConfig";
}
