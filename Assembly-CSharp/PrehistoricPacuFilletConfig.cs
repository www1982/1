using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001EA RID: 490
public class PrehistoricPacuFilletConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060009CB RID: 2507 RVA: 0x0003DEA6 File Offset: 0x0003C0A6
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x0003DEAD File Offset: 0x0003C0AD
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x0003DEB0 File Offset: 0x0003C0B0
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("PrehistoricPacuFillet", global::STRINGS.ITEMS.FOOD.PREHISTORICPACUFILLET.NAME, global::STRINGS.ITEMS.FOOD.PREHISTORICPACUFILLET.DESC, 1f, false, Assets.GetAnim("jawboFillet_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToFood(gameObject, FOOD.FOOD_TYPES.JAWBOFILLET);
		return gameObject;
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x0003DF16 File Offset: 0x0003C116
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x0003DF18 File Offset: 0x0003C118
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006EC RID: 1772
	public const string ID = "PrehistoricPacuFillet";
}
