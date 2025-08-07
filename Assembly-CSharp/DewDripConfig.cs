using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000184 RID: 388
public class DewDripConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000768 RID: 1896 RVA: 0x0003309E File Offset: 0x0003129E
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x000330A5 File Offset: 0x000312A5
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x000330A8 File Offset: 0x000312A8
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity(DewDripConfig.ID, ITEMS.INDUSTRIAL_PRODUCTS.DEWDRIP.NAME, ITEMS.INDUSTRIAL_PRODUCTS.DEWDRIP.DESC, 1f, true, Assets.GetAnim("brackorb_kanim"), "object", Grid.SceneLayer.BuildingBack, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.45f, true, 0, SimHashes.Creature, new List<Tag> { GameTags.IndustrialIngredient });
		gameObject.AddOrGet<EntitySplitter>();
		gameObject.AddOrGet<PrefabAttributeModifiers>().AddAttributeDescriptor(this.decorModifier);
		return gameObject;
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x00033129 File Offset: 0x00031329
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600076C RID: 1900 RVA: 0x0003312B File Offset: 0x0003132B
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400058F RID: 1423
	public static string ID = "DewDrip";

	// Token: 0x04000590 RID: 1424
	private AttributeModifier decorModifier = new AttributeModifier("Decor", 0.1f, ITEMS.INDUSTRIAL_PRODUCTS.DEWDRIP.NAME, true, false, true);
}
