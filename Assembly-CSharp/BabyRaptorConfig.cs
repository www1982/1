using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000157 RID: 343
[EntityConfigOrder(2)]
public class BabyRaptorConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000672 RID: 1650 RVA: 0x0002E66E File Offset: 0x0002C86E
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x0002E675 File Offset: 0x0002C875
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x0002E678 File Offset: 0x0002C878
	public GameObject CreatePrefab()
	{
		GameObject gameObject = RaptorConfig.CreateRaptor("RaptorBaby", CREATURES.SPECIES.RAPTOR.BABY.NAME, CREATURES.SPECIES.RAPTOR.BABY.DESC, "baby_raptor_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "Raptor", null, false, 5f).AddOrGetDef<BabyMonitor.Def>().configureAdultOnMaturation = delegate(GameObject go)
		{
			AmountInstance amountInstance = Db.Get().Amounts.ScaleGrowth.Lookup(go);
			amountInstance.value = amountInstance.GetMax() * RaptorConfig.SCALE_INITIAL_GROWTH_PCT;
		};
		return gameObject;
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x0002E6E9 File Offset: 0x0002C8E9
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x0002E6EB File Offset: 0x0002C8EB
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004DD RID: 1245
	public const string ID = "RaptorBaby";
}
