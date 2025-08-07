using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200030E RID: 782
public class IceBellyPoopConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06001019 RID: 4121 RVA: 0x00060C86 File Offset: 0x0005EE86
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x0600101A RID: 4122 RVA: 0x00060C8D File Offset: 0x0005EE8D
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600101B RID: 4123 RVA: 0x00060C90 File Offset: 0x0005EE90
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("IceBellyPoop", global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.ICE_BELLY_POOP.NAME, global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.ICE_BELLY_POOP.DESC, 100f, false, Assets.GetAnim("bammoth_poop_kanim"), "idle3", Grid.SceneLayer.BuildingBack, EntityTemplates.CollisionShape.CIRCLE, 0.4f, 0.4f, true, 0, SimHashes.Creature, new List<Tag> { GameTags.PedestalDisplayable });
		gameObject.GetComponent<KCollider2D>().offset = new Vector2(0f, 0.05f);
		gameObject.AddTag(GameTags.IndustrialProduct);
		gameObject.AddOrGet<OccupyArea>().SetCellOffsets(EntityTemplates.GenerateOffsets(1, 1));
		DecorProvider decorProvider = gameObject.AddOrGet<DecorProvider>();
		decorProvider.SetValues(DECOR.PENALTY.TIER3);
		decorProvider.overrideName = global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.ICE_BELLY_POOP.NAME;
		gameObject.AddOrGet<EntitySplitter>();
		return gameObject;
	}

	// Token: 0x0600101C RID: 4124 RVA: 0x00060D57 File Offset: 0x0005EF57
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600101D RID: 4125 RVA: 0x00060D59 File Offset: 0x0005EF59
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000A4B RID: 2635
	public const string ID = "IceBellyPoop";
}
