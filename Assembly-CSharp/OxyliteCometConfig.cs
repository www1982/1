using System;
using STRINGS;
using UnityEngine;

// Token: 0x020002FD RID: 765
public class OxyliteCometConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000FB7 RID: 4023 RVA: 0x0005EEBB File Offset: 0x0005D0BB
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000FB8 RID: 4024 RVA: 0x0005EEC2 File Offset: 0x0005D0C2
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000FB9 RID: 4025 RVA: 0x0005EEC8 File Offset: 0x0005D0C8
	public GameObject CreatePrefab()
	{
		float mass = ElementLoader.FindElementByHash(SimHashes.OxyRock).defaultValues.mass;
		GameObject gameObject = BaseCometConfig.BaseComet(OxyliteCometConfig.ID, UI.SPACEDESTINATIONS.COMETS.OXYLITECOMET.NAME, "meteor_oxylite_kanim", SimHashes.OxyRock, new Vector2(mass * 0.8f * 6f, mass * 1.2f * 6f), new Vector2(310.15f, 323.15f), "Meteor_dust_heavy_Impact", 0, SimHashes.Oxygen, SpawnFXHashes.MeteorImpactIce, 0.6f);
		Comet component = gameObject.GetComponent<Comet>();
		component.entityDamage = 0;
		component.totalTileDamage = 0f;
		component.addTiles = 6;
		component.addTilesMinHeight = 2;
		component.addTilesMaxHeight = 8;
		return gameObject;
	}

	// Token: 0x06000FBA RID: 4026 RVA: 0x0005EF77 File Offset: 0x0005D177
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000FBB RID: 4027 RVA: 0x0005EF79 File Offset: 0x0005D179
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000A0E RID: 2574
	public static string ID = "OxyliteComet";

	// Token: 0x04000A0F RID: 2575
	private const int ADDED_CELLS = 6;
}
