using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000164 RID: 356
[EntityConfigOrder(2)]
public class BabyStaterpillarGasConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060006C2 RID: 1730 RVA: 0x0002FB5D File Offset: 0x0002DD5D
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x0002FB64 File Offset: 0x0002DD64
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x0002FB67 File Offset: 0x0002DD67
	public GameObject CreatePrefab()
	{
		GameObject gameObject = StaterpillarGasConfig.CreateStaterpillarGas("StaterpillarGasBaby", CREATURES.SPECIES.STATERPILLAR.VARIANT_GAS.BABY.NAME, CREATURES.SPECIES.STATERPILLAR.VARIANT_GAS.BABY.DESC, "baby_caterpillar_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "StaterpillarGas", null, false, 5f);
		return gameObject;
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x0002FBA5 File Offset: 0x0002DDA5
	public void OnPrefabInit(GameObject prefab)
	{
		prefab.GetComponent<KBatchedAnimController>().SetSymbolVisiblity("electric_bolt_c_bloom", false);
	}

	// Token: 0x060006C6 RID: 1734 RVA: 0x0002FBBD File Offset: 0x0002DDBD
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400051E RID: 1310
	public const string ID = "StaterpillarGasBaby";
}
