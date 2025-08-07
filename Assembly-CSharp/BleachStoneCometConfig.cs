using System;
using STRINGS;
using UnityEngine;

// Token: 0x020002FE RID: 766
public class BleachStoneCometConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000FBE RID: 4030 RVA: 0x0005EF8F File Offset: 0x0005D18F
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000FBF RID: 4031 RVA: 0x0005EF96 File Offset: 0x0005D196
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000FC0 RID: 4032 RVA: 0x0005EF9C File Offset: 0x0005D19C
	public GameObject CreatePrefab()
	{
		float mass = ElementLoader.FindElementByHash(SimHashes.OxyRock).defaultValues.mass;
		GameObject gameObject = BaseCometConfig.BaseComet(BleachStoneCometConfig.ID, UI.SPACEDESTINATIONS.COMETS.BLEACHSTONECOMET.NAME, "meteor_bleachstone_kanim", SimHashes.BleachStone, new Vector2(mass * 0.8f * 1f, mass * 1.2f * 1f), new Vector2(310.15f, 323.15f), "Meteor_dust_heavy_Impact", 1, SimHashes.ChlorineGas, SpawnFXHashes.MeteorImpactIce, 0.6f);
		Comet component = gameObject.GetComponent<Comet>();
		component.explosionOreCount = new Vector2I(2, 4);
		component.explosionSpeedRange = new Vector2(4f, 7f);
		component.entityDamage = 0;
		component.totalTileDamage = 0f;
		component.addTiles = 1;
		component.addTilesMinHeight = 1;
		component.addTilesMaxHeight = 1;
		return gameObject;
	}

	// Token: 0x06000FC1 RID: 4033 RVA: 0x0005F06D File Offset: 0x0005D26D
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000FC2 RID: 4034 RVA: 0x0005F06F File Offset: 0x0005D26F
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000A10 RID: 2576
	public static string ID = "BleachStoneComet";

	// Token: 0x04000A11 RID: 2577
	private const int ADDED_CELLS = 1;
}
