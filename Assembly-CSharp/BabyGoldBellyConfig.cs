using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200011E RID: 286
[EntityConfigOrder(2)]
public class BabyGoldBellyConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000546 RID: 1350 RVA: 0x00029DC4 File Offset: 0x00027FC4
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x00029DCB File Offset: 0x00027FCB
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x00029DD0 File Offset: 0x00027FD0
	public GameObject CreatePrefab()
	{
		GameObject gameObject = GoldBellyConfig.CreateGoldBelly("GoldBellyBaby", CREATURES.SPECIES.ICEBELLY.VARIANT_GOLD.BABY.NAME, CREATURES.SPECIES.ICEBELLY.VARIANT_GOLD.BABY.DESC, "baby_icebelly_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "GoldBelly", null, false, 5f).AddOrGetDef<BabyMonitor.Def>().configureAdultOnMaturation = delegate(GameObject go)
		{
			AmountInstance amountInstance = Db.Get().Amounts.ScaleGrowth.Lookup(go);
			amountInstance.value = amountInstance.GetMax() * GoldBellyConfig.SCALE_INITIAL_GROWTH_PCT;
		};
		return gameObject;
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x00029E41 File Offset: 0x00028041
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x00029E43 File Offset: 0x00028043
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003E9 RID: 1001
	public const string ID = "GoldBellyBaby";
}
