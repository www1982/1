using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000166 RID: 358
[EntityConfigOrder(2)]
public class BabyStaterpillarLiquidConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060006D0 RID: 1744 RVA: 0x0002FF31 File Offset: 0x0002E131
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060006D1 RID: 1745 RVA: 0x0002FF38 File Offset: 0x0002E138
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x0002FF3B File Offset: 0x0002E13B
	public GameObject CreatePrefab()
	{
		GameObject gameObject = StaterpillarLiquidConfig.CreateStaterpillarLiquid("StaterpillarLiquidBaby", CREATURES.SPECIES.STATERPILLAR.VARIANT_LIQUID.BABY.NAME, CREATURES.SPECIES.STATERPILLAR.VARIANT_LIQUID.BABY.DESC, "baby_caterpillar_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "StaterpillarLiquid", null, false, 5f);
		return gameObject;
	}

	// Token: 0x060006D3 RID: 1747 RVA: 0x0002FF79 File Offset: 0x0002E179
	public void OnPrefabInit(GameObject prefab)
	{
		prefab.GetComponent<KBatchedAnimController>().SetSymbolVisiblity("electric_bolt_c_bloom", false);
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x0002FF91 File Offset: 0x0002E191
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400052A RID: 1322
	public const string ID = "StaterpillarLiquidBaby";
}
