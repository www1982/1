using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000168 RID: 360
[EntityConfigOrder(2)]
public class BabyStegoConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060006DD RID: 1757 RVA: 0x00030195 File Offset: 0x0002E395
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x060006DE RID: 1758 RVA: 0x0003019C File Offset: 0x0002E39C
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x0003019F File Offset: 0x0002E39F
	public GameObject CreatePrefab()
	{
		GameObject gameObject = StegoConfig.CreateStego("StegoBaby", CREATURES.SPECIES.STEGO.BABY.NAME, CREATURES.SPECIES.STEGO.BABY.DESC, "baby_stego_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "Stego", null, false, 5f);
		return gameObject;
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x000301DD File Offset: 0x0002E3DD
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x000301DF File Offset: 0x0002E3DF
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000530 RID: 1328
	public const string ID = "StegoBaby";
}
