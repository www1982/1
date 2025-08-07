using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200015C RID: 348
public class ShockwormConfig : IEntityConfig
{
	// Token: 0x06000694 RID: 1684 RVA: 0x0002EFC4 File Offset: 0x0002D1C4
	public GameObject CreatePrefab()
	{
		string text = "ShockWorm";
		string text2 = global::STRINGS.CREATURES.SPECIES.SHOCKWORM.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.SHOCKWORM.DESC;
		float num = 50f;
		EffectorValues tier = DECOR.BONUS.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("shockworm_kanim"), "idle", Grid.SceneLayer.Creatures, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 293f);
		FactionManager.FactionID factionID = FactionManager.FactionID.Hostile;
		string text4 = null;
		string text5 = "FlyerNavGrid1x2";
		NavType navType = NavType.Hover;
		int num2 = 32;
		float num3 = 2f;
		string text6 = "Meat";
		float num4 = 3f;
		bool flag = true;
		bool flag2 = true;
		float freezing_ = global::TUNING.CREATURES.TEMPERATURE.FREEZING_2;
		EntityTemplates.ExtendEntityToBasicCreature(gameObject, factionID, text4, text5, navType, num2, num3, text6, num4, flag, flag2, global::TUNING.CREATURES.TEMPERATURE.FREEZING_1, global::TUNING.CREATURES.TEMPERATURE.HOT_1, freezing_, global::TUNING.CREATURES.TEMPERATURE.HOT_2);
		gameObject.AddOrGet<LoopingSounds>();
		gameObject.AddWeapon(3f, 6f, AttackProperties.DamageType.Standard, AttackProperties.TargetType.AreaOfEffect, 10, 4f).AddEffect("WasAttacked", 1f);
		SoundEventVolumeCache.instance.AddVolume("shockworm_kanim", "Shockworm_attack_arc", NOISE_POLLUTION.CREATURES.TIER6);
		return gameObject;
	}

	// Token: 0x06000695 RID: 1685 RVA: 0x0002F0A7 File Offset: 0x0002D2A7
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000696 RID: 1686 RVA: 0x0002F0A9 File Offset: 0x0002D2A9
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004F3 RID: 1267
	public const string ID = "ShockWorm";
}
