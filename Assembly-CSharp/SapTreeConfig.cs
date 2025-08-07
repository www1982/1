using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001A8 RID: 424
public class SapTreeConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600082A RID: 2090 RVA: 0x0003772E File Offset: 0x0003592E
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x00037735 File Offset: 0x00035935
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x00037738 File Offset: 0x00035938
	public GameObject CreatePrefab()
	{
		string text = "SapTree";
		string text2 = global::STRINGS.CREATURES.SPECIES.SAPTREE.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.SAPTREE.DESC;
		float num = 1f;
		EffectorValues positive_DECOR_EFFECT = SapTreeConfig.POSITIVE_DECOR_EFFECT;
		KAnimFile anim = Assets.GetAnim("gravitas_sap_tree_kanim");
		string text4 = "idle";
		Grid.SceneLayer sceneLayer = Grid.SceneLayer.BuildingFront;
		int num2 = 5;
		int num3 = 5;
		EffectorValues effectorValues = positive_DECOR_EFFECT;
		List<Tag> list = new List<Tag> { GameTags.Decoration };
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, anim, text4, sceneLayer, num2, num3, effectorValues, default(EffectorValues), SimHashes.Creature, list, 293f);
		SapTree.Def def = gameObject.AddOrGetDef<SapTree.Def>();
		def.foodSenseArea = new Vector2I(5, 1);
		def.massEatRate = 0.05f;
		def.kcalorieToKGConversionRatio = 0.005f;
		def.stomachSize = 5f;
		def.oozeRate = 2f;
		def.oozeOffsets = new List<Vector3>
		{
			new Vector3(-2f, 2f),
			new Vector3(2f, 1f)
		};
		def.attackSenseArea = new Vector2I(5, 5);
		def.attackCooldown = 5f;
		gameObject.AddOrGet<Storage>();
		FactionAlignment factionAlignment = gameObject.AddOrGet<FactionAlignment>();
		factionAlignment.Alignment = FactionManager.FactionID.Hostile;
		factionAlignment.canBePlayerTargeted = false;
		gameObject.AddOrGet<RangedAttackable>();
		gameObject.AddWeapon(5f, 5f, AttackProperties.DamageType.Standard, AttackProperties.TargetType.AreaOfEffect, 1, 2f);
		gameObject.AddOrGet<WiltCondition>();
		gameObject.AddOrGet<TemperatureVulnerable>().Configure(173.15f, 0f, 373.15f, 1023.15f);
		gameObject.AddOrGet<EntombVulnerable>();
		gameObject.AddOrGet<LoopingSounds>();
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		return gameObject;
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x000378BC File Offset: 0x00035ABC
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x000378BE File Offset: 0x00035ABE
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000614 RID: 1556
	public const string ID = "SapTree";

	// Token: 0x04000615 RID: 1557
	public static readonly EffectorValues POSITIVE_DECOR_EFFECT = DECOR.BONUS.TIER5;

	// Token: 0x04000616 RID: 1558
	private const int WIDTH = 5;

	// Token: 0x04000617 RID: 1559
	private const int HEIGHT = 5;

	// Token: 0x04000618 RID: 1560
	private const int ATTACK_RADIUS = 2;

	// Token: 0x04000619 RID: 1561
	public const float MASS_EAT_RATE = 0.05f;

	// Token: 0x0400061A RID: 1562
	public const float KCAL_TO_KG_RATIO = 0.005f;
}
