using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200011C RID: 284
public class GlomConfig : IEntityConfig
{
	// Token: 0x0600053A RID: 1338 RVA: 0x000297E4 File Offset: 0x000279E4
	public GameObject CreatePrefab()
	{
		string text = global::STRINGS.CREATURES.SPECIES.GLOM.NAME;
		string text2 = "Glom";
		string text3 = text;
		string text4 = global::STRINGS.CREATURES.SPECIES.GLOM.DESC;
		float num = 25f;
		EffectorValues tier = DECOR.BONUS.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text2, text3, text4, num, Assets.GetAnim("glom_kanim"), "idle_loop", Grid.SceneLayer.Creatures, 1, 1, tier, default(EffectorValues), SimHashes.Creature, null, 293f);
		Db.Get().CreateTrait("GlomBaseTrait", text, text, null, false, null, true, true).Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, text, false, false, true));
		KPrefabID component = gameObject.GetComponent<KPrefabID>();
		component.AddTag(GameTags.Creatures.Walker, false);
		component.AddTag(GameTags.OriginalCreature, false);
		component.prefabInitFn += delegate(GameObject inst)
		{
			inst.GetAttributes().Add(Db.Get().Attributes.MaxUnderwaterTravelCost);
		};
		EntityTemplates.ExtendEntityToBasicCreature(gameObject, FactionManager.FactionID.Pest, "GlomBaseTrait", "WalkerNavGrid1x1", NavType.Floor, 32, 2f, "", 0f, true, true, 303.15f, 373.15f, 273.15f, 473.15f);
		gameObject.AddWeapon(1f, 1f, AttackProperties.DamageType.Standard, AttackProperties.TargetType.Single, 1, 0f);
		Pickupable pickupable = gameObject.AddOrGet<Pickupable>();
		int num2 = global::TUNING.CREATURES.SORTING.CRITTER_ORDER["Glom"];
		pickupable.sortOrder = num2;
		gameObject.AddOrGet<Trappable>();
		gameObject.AddOrGetDef<ThreatMonitor.Def>();
		gameObject.AddOrGetDef<CreatureFallMonitor.Def>();
		ElementDropperMonitor.Def def = gameObject.AddOrGetDef<ElementDropperMonitor.Def>();
		def.dirtyEmitElement = SimHashes.ContaminatedOxygen;
		def.dirtyProbabilityPercent = 25f;
		def.dirtyCellToTargetMass = 1f;
		def.dirtyMassPerDirty = 0.2f;
		def.dirtyMassReleaseOnDeath = 3f;
		def.emitDiseaseIdx = Db.Get().Diseases.GetIndex("SlimeLung");
		def.emitDiseasePerKg = 1000f;
		gameObject.AddOrGetDef<OvercrowdingMonitor.Def>().spaceRequiredPerCreature = 0;
		gameObject.AddOrGet<LoopingSounds>();
		gameObject.GetComponent<LoopingSounds>().updatePosition = true;
		gameObject.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = "SlimeLung";
		SoundEventVolumeCache.instance.AddVolume("glom_kanim", "Morb_movement_short", NOISE_POLLUTION.CREATURES.TIER2);
		SoundEventVolumeCache.instance.AddVolume("glom_kanim", "Morb_jump", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("glom_kanim", "Morb_land", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("glom_kanim", "Morb_expel", NOISE_POLLUTION.CREATURES.TIER4);
		EntityTemplates.CreateAndRegisterBaggedCreature(gameObject, true, false, false);
		ChoreTable.Builder builder = new ChoreTable.Builder().Add(new DeathStates.Def(), true, -1).Add(new TrappedStates.Def(), true, -1).Add(new BaggedStates.Def(), true, -1)
			.Add(new FallStates.Def(), true, -1)
			.Add(new StunnedStates.Def(), true, -1)
			.Add(new DrowningStates.Def(), true, -1)
			.Add(new DebugGoToStates.Def(), true, -1)
			.Add(new FleeStates.Def(), true, -1)
			.Add(new DropElementStates.Def(), true, -1)
			.Add(new IdleStates.Def(), true, -1);
		EntityTemplates.AddCreatureBrain(gameObject, builder, GameTags.Creatures.Species.GlomSpecies, null);
		return gameObject;
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x00029AE0 File Offset: 0x00027CE0
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600053C RID: 1340 RVA: 0x00029AE2 File Offset: 0x00027CE2
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003D6 RID: 982
	public const string ID = "Glom";

	// Token: 0x040003D7 RID: 983
	public const string BASE_TRAIT_ID = "GlomBaseTrait";

	// Token: 0x040003D8 RID: 984
	public const SimHashes dirtyEmitElement = SimHashes.ContaminatedOxygen;

	// Token: 0x040003D9 RID: 985
	public const float dirtyProbabilityPercent = 25f;

	// Token: 0x040003DA RID: 986
	public const float dirtyCellToTargetMass = 1f;

	// Token: 0x040003DB RID: 987
	public const float dirtyMassPerDirty = 0.2f;

	// Token: 0x040003DC RID: 988
	public const float dirtyMassReleaseOnDeath = 3f;

	// Token: 0x040003DD RID: 989
	public const string emitDisease = "SlimeLung";

	// Token: 0x040003DE RID: 990
	public const int emitDiseasePerKg = 1000;
}
