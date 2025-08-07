using System;
using STRINGS;
using UnityEngine;

// Token: 0x020002F4 RID: 756
public class UraniumCometConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000F78 RID: 3960 RVA: 0x0005E3BA File Offset: 0x0005C5BA
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000F79 RID: 3961 RVA: 0x0005E3C1 File Offset: 0x0005C5C1
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000F7A RID: 3962 RVA: 0x0005E3C4 File Offset: 0x0005C5C4
	public GameObject CreatePrefab()
	{
		float mass = ElementLoader.FindElementByHash(SimHashes.UraniumOre).defaultValues.mass;
		GameObject gameObject = BaseCometConfig.BaseComet(UraniumCometConfig.ID, UI.SPACEDESTINATIONS.COMETS.URANIUMORECOMET.NAME, "meteor_uranium_kanim", SimHashes.UraniumOre, new Vector2(mass * 0.8f * 6f, mass * 1.2f * 6f), new Vector2(323.15f, 403.15f), "Meteor_Nuclear_Impact", 3, SimHashes.CarbonDioxide, SpawnFXHashes.MeteorImpactUranium, 0.6f);
		Comet component = gameObject.GetComponent<Comet>();
		component.explosionOreCount = new Vector2I(1, 2);
		component.entityDamage = 15;
		component.totalTileDamage = 0f;
		component.addTiles = 6;
		component.addTilesMinHeight = 1;
		component.addTilesMaxHeight = 1;
		return gameObject;
	}

	// Token: 0x06000F7B RID: 3963 RVA: 0x0005E481 File Offset: 0x0005C681
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000F7C RID: 3964 RVA: 0x0005E483 File Offset: 0x0005C683
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x040009FF RID: 2559
	public static readonly string ID = "UraniumComet";

	// Token: 0x04000A00 RID: 2560
	private const SimHashes element = SimHashes.UraniumOre;

	// Token: 0x04000A01 RID: 2561
	private const int ADDED_CELLS = 6;
}
