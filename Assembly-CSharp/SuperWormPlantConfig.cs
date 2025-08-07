using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001B0 RID: 432
public class SuperWormPlantConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600089C RID: 2204 RVA: 0x0003A9A6 File Offset: 0x00038BA6
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x0003A9AD File Offset: 0x00038BAD
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x0003A9B0 File Offset: 0x00038BB0
	public GameObject CreatePrefab()
	{
		GameObject gameObject = WormPlantConfig.BaseWormPlant("SuperWormPlant", global::STRINGS.CREATURES.SPECIES.SUPERWORMPLANT.NAME, global::STRINGS.CREATURES.SPECIES.SUPERWORMPLANT.DESC, "wormwood_kanim", SuperWormPlantConfig.SUPER_DECOR, "WormSuperFruit");
		gameObject.AddOrGet<SeedProducer>().Configure("WormPlantSeed", SeedProducer.ProductionType.Harvest, 1);
		return gameObject;
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x0003A9FC File Offset: 0x00038BFC
	public void OnPrefabInit(GameObject prefab)
	{
		TransformingPlant transformingPlant = prefab.AddOrGet<TransformingPlant>();
		transformingPlant.SubscribeToTransformEvent(GameHashes.HarvestComplete);
		transformingPlant.transformPlantId = "WormPlant";
		prefab.GetComponent<KAnimControllerBase>().SetSymbolVisiblity("flower", false);
		prefab.AddOrGet<StandardCropPlant>().anims = SuperWormPlantConfig.animSet;
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x0003AA4A File Offset: 0x00038C4A
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400065D RID: 1629
	public const string ID = "SuperWormPlant";

	// Token: 0x0400065E RID: 1630
	public static readonly EffectorValues SUPER_DECOR = DECOR.BONUS.TIER1;

	// Token: 0x0400065F RID: 1631
	public const string SUPER_CROP_ID = "WormSuperFruit";

	// Token: 0x04000660 RID: 1632
	public const int CROP_YIELD = 8;

	// Token: 0x04000661 RID: 1633
	private static StandardCropPlant.AnimSet animSet = new StandardCropPlant.AnimSet
	{
		grow = "super_grow",
		grow_pst = "super_grow_pst",
		idle_full = "super_idle_full",
		wilt_base = "super_wilt",
		harvest = "super_harvest"
	};
}
