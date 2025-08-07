using System;
using STRINGS;
using UnityEngine;

// Token: 0x020002EE RID: 750
public class FullereneCometConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000F56 RID: 3926 RVA: 0x0005DAA2 File Offset: 0x0005BCA2
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000F57 RID: 3927 RVA: 0x0005DAA9 File Offset: 0x0005BCA9
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000F58 RID: 3928 RVA: 0x0005DAAC File Offset: 0x0005BCAC
	public GameObject CreatePrefab()
	{
		GameObject gameObject = BaseCometConfig.BaseComet(FullereneCometConfig.ID, UI.SPACEDESTINATIONS.COMETS.FULLERENECOMET.NAME, "meteor_fullerene_kanim", SimHashes.Fullerene, new Vector2(3f, 20f), new Vector2(323.15f, 423.15f), "Meteor_Medium_Impact", 1, SimHashes.CarbonDioxide, SpawnFXHashes.MeteorImpactMetal, 0.6f);
		Comet component = gameObject.GetComponent<Comet>();
		component.explosionOreCount = new Vector2I(2, 4);
		component.entityDamage = 15;
		component.totalTileDamage = 0.5f;
		component.affectedByDifficulty = false;
		return gameObject;
	}

	// Token: 0x06000F59 RID: 3929 RVA: 0x0005DB36 File Offset: 0x0005BD36
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000F5A RID: 3930 RVA: 0x0005DB38 File Offset: 0x0005BD38
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x040009F8 RID: 2552
	public static readonly string ID = "FullereneComet";
}
