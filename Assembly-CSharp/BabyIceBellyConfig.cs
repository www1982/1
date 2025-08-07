using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000128 RID: 296
[EntityConfigOrder(2)]
public class BabyIceBellyConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600057D RID: 1405 RVA: 0x0002AA4F File Offset: 0x00028C4F
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x0002AA56 File Offset: 0x00028C56
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x0002AA5C File Offset: 0x00028C5C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = IceBellyConfig.CreateIceBelly("IceBellyBaby", CREATURES.SPECIES.ICEBELLY.BABY.NAME, CREATURES.SPECIES.ICEBELLY.BABY.DESC, "baby_icebelly_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "IceBelly", null, false, 5f).AddOrGetDef<BabyMonitor.Def>().configureAdultOnMaturation = delegate(GameObject go)
		{
			AmountInstance amountInstance = Db.Get().Amounts.ScaleGrowth.Lookup(go);
			amountInstance.value = amountInstance.GetMax() * IceBellyConfig.SCALE_INITIAL_GROWTH_PCT;
		};
		return gameObject;
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x0002AACD File Offset: 0x00028CCD
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x0002AACF File Offset: 0x00028CCF
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000415 RID: 1045
	public const string ID = "IceBellyBaby";
}
