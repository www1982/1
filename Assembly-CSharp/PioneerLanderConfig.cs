using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200036C RID: 876
public class PioneerLanderConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060011F8 RID: 4600 RVA: 0x00068F73 File Offset: 0x00067173
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060011F9 RID: 4601 RVA: 0x00068F7A File Offset: 0x0006717A
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060011FA RID: 4602 RVA: 0x00068F80 File Offset: 0x00067180
	public GameObject CreatePrefab()
	{
		string text = "PioneerLander";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PIONEERLANDER.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PIONEERLANDER.DESC;
		float num = 400f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("rocket_pioneer_cargo_lander_kanim"), "grounded", Grid.SceneLayer.Building, 3, 3, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.RoomProberBuilding }, 293f);
		gameObject.AddOrGetDef<CargoLander.Def>().previewTag = "PioneerLander_Preview".ToTag();
		CargoDropperMinion.Def def = gameObject.AddOrGetDef<CargoDropperMinion.Def>();
		def.kAnimName = "anim_interacts_pioneer_cargo_lander_kanim";
		def.animName = "enter";
		gameObject.AddOrGet<MinionStorage>();
		gameObject.AddOrGet<Prioritizable>();
		Prioritizable.AddRef(gameObject);
		gameObject.AddOrGet<Operational>();
		gameObject.AddOrGet<Deconstructable>().audioSize = "large";
		gameObject.AddOrGet<Storable>();
		Placeable placeable = gameObject.AddOrGet<Placeable>();
		placeable.kAnimName = "rocket_pioneer_cargo_lander_kanim";
		placeable.animName = "place";
		placeable.placementRules = new List<Placeable.PlacementRules>
		{
			Placeable.PlacementRules.OnFoundation,
			Placeable.PlacementRules.VisibleToSpace,
			Placeable.PlacementRules.RestrictToWorld
		};
		placeable.checkRootCellOnly = true;
		EntityTemplates.CreateAndRegisterPreview("PioneerLander_Preview", Assets.GetAnim("rocket_pioneer_cargo_lander_kanim"), "place", ObjectLayer.Building, 3, 3);
		return gameObject;
	}

	// Token: 0x060011FB RID: 4603 RVA: 0x000690B6 File Offset: 0x000672B6
	public void OnPrefabInit(GameObject inst)
	{
		OccupyArea component = inst.GetComponent<OccupyArea>();
		component.ApplyToCells = false;
		component.objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x060011FC RID: 4604 RVA: 0x000690D4 File Offset: 0x000672D4
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000B70 RID: 2928
	public const string ID = "PioneerLander";

	// Token: 0x04000B71 RID: 2929
	public const string PREVIEW_ID = "PioneerLander_Preview";

	// Token: 0x04000B72 RID: 2930
	public const float MASS = 400f;
}
