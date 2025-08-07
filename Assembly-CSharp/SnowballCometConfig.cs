using System;
using STRINGS;
using UnityEngine;

// Token: 0x020002F6 RID: 758
public class SnowballCometConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000F86 RID: 3974 RVA: 0x0005E612 File Offset: 0x0005C812
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000F87 RID: 3975 RVA: 0x0005E619 File Offset: 0x0005C819
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000F88 RID: 3976 RVA: 0x0005E61C File Offset: 0x0005C81C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = BaseCometConfig.BaseComet(SnowballCometConfig.ID, UI.SPACEDESTINATIONS.COMETS.SNOWBALLCOMET.NAME, "meteor_snow_kanim", SimHashes.Snow, new Vector2(3f, 20f), new Vector2(253.15f, 263.15f), "Meteor_snowball_Impact", 5, SimHashes.Void, SpawnFXHashes.None, 0.3f);
		Comet component = gameObject.GetComponent<Comet>();
		component.entityDamage = 0;
		component.totalTileDamage = 0f;
		component.splashRadius = 1;
		component.addTiles = 3;
		component.addTilesMinHeight = 1;
		component.addTilesMaxHeight = 2;
		return gameObject;
	}

	// Token: 0x06000F89 RID: 3977 RVA: 0x0005E6A9 File Offset: 0x0005C8A9
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000F8A RID: 3978 RVA: 0x0005E6AB File Offset: 0x0005C8AB
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000A05 RID: 2565
	public static string ID = "SnowballComet";
}
