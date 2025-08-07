using System;
using UnityEngine;

// Token: 0x02000305 RID: 773
public class ExplodingClusterShipConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000FE8 RID: 4072 RVA: 0x0005F64A File Offset: 0x0005D84A
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000FE9 RID: 4073 RVA: 0x0005F651 File Offset: 0x0005D851
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000FEA RID: 4074 RVA: 0x0005F654 File Offset: 0x0005D854
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity("ExplodingClusterShip", "ExplodingClusterShip", false);
		ClusterFXEntity clusterFXEntity = gameObject.AddOrGet<ClusterFXEntity>();
		clusterFXEntity.kAnimName = "rocket_self_destruct_kanim";
		clusterFXEntity.animName = "explode";
		return gameObject;
	}

	// Token: 0x06000FEB RID: 4075 RVA: 0x0005F681 File Offset: 0x0005D881
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000FEC RID: 4076 RVA: 0x0005F683 File Offset: 0x0005D883
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000A1F RID: 2591
	public const string ID = "ExplodingClusterShip";
}
