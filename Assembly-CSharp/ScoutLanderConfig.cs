using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003DC RID: 988
public class ScoutLanderConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06001434 RID: 5172 RVA: 0x00074171 File Offset: 0x00072371
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06001435 RID: 5173 RVA: 0x00074178 File Offset: 0x00072378
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06001436 RID: 5174 RVA: 0x0007417C File Offset: 0x0007237C
	public GameObject CreatePrefab()
	{
		string text = "ScoutLander";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.SCOUTLANDER.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.SCOUTLANDER.DESC;
		float num = 400f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("rocket_scout_cargo_lander_kanim"), "grounded", Grid.SceneLayer.Building, 3, 3, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.RoomProberBuilding }, 293f);
		gameObject.AddOrGetDef<CargoLander.Def>().previewTag = "ScoutLander_Preview".ToTag();
		gameObject.AddOrGetDef<CargoDropperStorage.Def>();
		gameObject.AddOrGet<Prioritizable>();
		Prioritizable.AddRef(gameObject);
		gameObject.AddOrGet<Operational>();
		Storage storage = gameObject.AddComponent<Storage>();
		storage.showInUI = true;
		storage.allowItemRemoval = false;
		storage.capacityKg = 2000f;
		storage.SetDefaultStoredItemModifiers(Storage.StandardInsulatedStorage);
		gameObject.AddOrGet<Deconstructable>().audioSize = "large";
		gameObject.AddOrGet<Storable>();
		Placeable placeable = gameObject.AddOrGet<Placeable>();
		placeable.kAnimName = "rocket_scout_cargo_lander_kanim";
		placeable.animName = "place";
		placeable.placementRules = new List<Placeable.PlacementRules>
		{
			Placeable.PlacementRules.OnFoundation,
			Placeable.PlacementRules.VisibleToSpace,
			Placeable.PlacementRules.RestrictToWorld
		};
		placeable.checkRootCellOnly = true;
		EntityTemplates.CreateAndRegisterPreview("ScoutLander_Preview", Assets.GetAnim("rocket_scout_cargo_lander_kanim"), "place", ObjectLayer.Building, 3, 3);
		return gameObject;
	}

	// Token: 0x06001437 RID: 5175 RVA: 0x000742C0 File Offset: 0x000724C0
	public void OnPrefabInit(GameObject inst)
	{
		OccupyArea component = inst.GetComponent<OccupyArea>();
		component.ApplyToCells = false;
		component.objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06001438 RID: 5176 RVA: 0x000742DE File Offset: 0x000724DE
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000C41 RID: 3137
	public const string ID = "ScoutLander";

	// Token: 0x04000C42 RID: 3138
	public const string PREVIEW_ID = "ScoutLander_Preview";

	// Token: 0x04000C43 RID: 3139
	public const float MASS = 400f;
}
