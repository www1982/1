using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000188 RID: 392
public class FeatherFabricConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600077F RID: 1919 RVA: 0x000337D8 File Offset: 0x000319D8
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x000337DF File Offset: 0x000319DF
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x000337E4 File Offset: 0x000319E4
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity(FeatherFabricConfig.ID, global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.FEATHER_FABRIC.NAME, global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.FEATHER_FABRIC.DESC, 1f, true, Assets.GetAnim("feather_kanim"), "object", Grid.SceneLayer.BuildingBack, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.45f, true, SORTORDER.BUILDINGELEMENTS + BasicFabricTuning.SORTORDER, SimHashes.Creature, new List<Tag>
		{
			GameTags.IndustrialIngredient,
			GameTags.BuildingFiber
		});
		gameObject.AddOrGet<EntitySplitter>();
		KBoxCollider2D kboxCollider2D = gameObject.AddOrGet<KBoxCollider2D>();
		kboxCollider2D.offset = new Vector2f(0f, 0.3f);
		kboxCollider2D.size = new Vector2f(0.8f, 0.8f);
		gameObject.AddOrGet<PrefabAttributeModifiers>().AddAttributeDescriptor(this.decorModifier);
		return gameObject;
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x000338B3 File Offset: 0x00031AB3
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x000338B5 File Offset: 0x00031AB5
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400059D RID: 1437
	public static string ID = "FeatherFabric";

	// Token: 0x0400059E RID: 1438
	private AttributeModifier decorModifier = new AttributeModifier("Decor", 0.1f, global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.BASIC_FABRIC.NAME, true, false, true);
}
