using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200016A RID: 362
[EntityConfigOrder(2)]
public class BabyWoodDeerConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060006EB RID: 1771 RVA: 0x00030593 File Offset: 0x0002E793
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x0003059A File Offset: 0x0002E79A
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x000305A0 File Offset: 0x0002E7A0
	public GameObject CreatePrefab()
	{
		GameObject gameObject = WoodDeerConfig.CreateWoodDeer("WoodDeerBaby", CREATURES.SPECIES.WOODDEER.BABY.NAME, CREATURES.SPECIES.WOODDEER.BABY.DESC, "baby_ice_floof_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "WoodDeer", null, false, 5f).AddOrGetDef<BabyMonitor.Def>().configureAdultOnMaturation = delegate(GameObject go)
		{
			AmountInstance amountInstance = Db.Get().Amounts.ScaleGrowth.Lookup(go);
			amountInstance.value = amountInstance.GetMax() * WoodDeerConfig.ANTLER_STARTING_GROWTH_PCT;
		};
		return gameObject;
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x00030611 File Offset: 0x0002E811
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x00030613 File Offset: 0x0002E813
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000542 RID: 1346
	public const string ID = "WoodDeerBaby";
}
