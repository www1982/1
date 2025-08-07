using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200030B RID: 779
public class GoldBellyCrownConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06001007 RID: 4103 RVA: 0x0005FA2A File Offset: 0x0005DC2A
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x06001008 RID: 4104 RVA: 0x0005FA31 File Offset: 0x0005DC31
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06001009 RID: 4105 RVA: 0x0005FA34 File Offset: 0x0005DC34
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("GoldBellyCrown", global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.GOLD_BELLY_CROWN.NAME, global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.GOLD_BELLY_CROWN.DESC, 250f, true, Assets.GetAnim("bammoth_crown_kanim"), "idle1", Grid.SceneLayer.Ore, EntityTemplates.CollisionShape.RECTANGLE, 0.6f, 0.5f, true, 0, SimHashes.GoldAmalgam, new List<Tag> { GameTags.PedestalDisplayable });
		gameObject.GetComponent<KCollider2D>();
		gameObject.AddTag(GameTags.IndustrialProduct);
		gameObject.AddOrGet<OccupyArea>().SetCellOffsets(EntityTemplates.GenerateOffsets(1, 1));
		DecorProvider decorProvider = gameObject.AddOrGet<DecorProvider>();
		decorProvider.SetValues(DECOR.BONUS.TIER2);
		decorProvider.overrideName = global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.GOLD_BELLY_CROWN.NAME;
		return gameObject;
	}

	// Token: 0x0600100A RID: 4106 RVA: 0x0005FAE1 File Offset: 0x0005DCE1
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600100B RID: 4107 RVA: 0x0005FAE3 File Offset: 0x0005DCE3
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000A28 RID: 2600
	public const string ID = "GoldBellyCrown";

	// Token: 0x04000A29 RID: 2601
	public const float MASS_PER_UNIT = 250f;
}
