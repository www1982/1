using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000C8 RID: 200
[EntityConfigOrder(2)]
public class BabyBeeConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000373 RID: 883 RVA: 0x0001E583 File Offset: 0x0001C783
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000374 RID: 884 RVA: 0x0001E58A File Offset: 0x0001C78A
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000375 RID: 885 RVA: 0x0001E590 File Offset: 0x0001C790
	public GameObject CreatePrefab()
	{
		GameObject gameObject = BeeConfig.CreateBee("BeeBaby", CREATURES.SPECIES.BEE.BABY.NAME, CREATURES.SPECIES.BEE.BABY.DESC, "baby_blarva_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "Bee", null, true, 2f);
		gameObject.GetComponent<KPrefabID>().AddTag(GameTags.Creatures.Walker, false);
		return gameObject;
	}

	// Token: 0x06000376 RID: 886 RVA: 0x0001E5EA File Offset: 0x0001C7EA
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000377 RID: 887 RVA: 0x0001E5EC File Offset: 0x0001C7EC
	public void OnSpawn(GameObject inst)
	{
		BaseBeeConfig.SetupLoopingSounds(inst);
	}

	// Token: 0x040002AC RID: 684
	public const string ID = "BeeBaby";
}
