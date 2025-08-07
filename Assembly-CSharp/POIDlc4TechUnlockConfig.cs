using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003E2 RID: 994
public class POIDlc4TechUnlockConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06001453 RID: 5203 RVA: 0x00074A05 File Offset: 0x00072C05
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x06001454 RID: 5204 RVA: 0x00074A0C File Offset: 0x00072C0C
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06001455 RID: 5205 RVA: 0x00074A10 File Offset: 0x00072C10
	public GameObject CreatePrefab()
	{
		string text = "POIDlc4TechUnlock";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.DLC4POITECHUNLOCKS.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.DLC4POITECHUNLOCKS.DESC;
		float num = 100f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("research_unlock_dino_kanim"), "on", Grid.SceneLayer.Building, 3, 3, tier, tier2, SimHashes.Creature, new List<Tag>
		{
			GameTags.Gravitas,
			RoomConstraints.ConstraintTags.LightSource,
			GameTags.RoomProberBuilding
		}, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		gameObject.AddOrGet<Demolishable>();
		POITechItemUnlockWorkable poitechItemUnlockWorkable = gameObject.AddOrGet<POITechItemUnlockWorkable>();
		poitechItemUnlockWorkable.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_research_unlock_dino_kanim") };
		poitechItemUnlockWorkable.workTime = 5f;
		POITechItemUnlocks.Def def = gameObject.AddOrGetDef<POITechItemUnlocks.Def>();
		def.POITechUnlockIDs = new List<string> { "MissileFabricator", "MissileLauncher" };
		def.PopUpName = global::STRINGS.BUILDINGS.PREFABS.DLC4POITECHUNLOCKS.NAME;
		def.animName = "meteor_blast_kanim";
		Light2D light2D = gameObject.AddComponent<Light2D>();
		light2D.Color = LIGHT2D.POI_TECH_UNLOCK_COLOR;
		light2D.Range = 5f;
		light2D.Angle = 2.6f;
		light2D.Direction = LIGHT2D.POI_TECH_DIRECTION;
		light2D.Offset = LIGHT2D.POI_TECH_UNLOCK_OFFSET;
		light2D.overlayColour = LIGHT2D.POI_TECH_UNLOCK_OVERLAYCOLOR;
		light2D.shape = global::LightShape.Cone;
		light2D.drawOverlay = true;
		light2D.Lux = 1800;
		gameObject.AddOrGet<Prioritizable>();
		return gameObject;
	}

	// Token: 0x06001456 RID: 5206 RVA: 0x00074BA3 File Offset: 0x00072DA3
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06001457 RID: 5207 RVA: 0x00074BA5 File Offset: 0x00072DA5
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000C47 RID: 3143
	public const string ID = "POIDlc4TechUnlock";
}
