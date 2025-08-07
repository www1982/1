using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000324 RID: 804
public class PinkRockConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06001094 RID: 4244 RVA: 0x000629CD File Offset: 0x00060BCD
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x06001095 RID: 4245 RVA: 0x000629D4 File Offset: 0x00060BD4
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06001096 RID: 4246 RVA: 0x000629D8 File Offset: 0x00060BD8
	public GameObject CreatePrefab()
	{
		string id = this.ID;
		string text = global::STRINGS.CREATURES.SPECIES.PINKROCK.NAME;
		string text2 = global::STRINGS.CREATURES.SPECIES.PINKROCK.DESC;
		float num = 25f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(id, text, text2, num, Assets.GetAnim("pinkrock_kanim"), "idle", Grid.SceneLayer.Building, 1, 1, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Experimental }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 235.15f;
		gameObject.AddOrGet<Carvable>().dropItemPrefabId = "PinkRockCarved";
		gameObject.AddOrGet<Prioritizable>();
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		Light2D light2D = gameObject.AddOrGet<Light2D>();
		light2D.overlayColour = LIGHT2D.PINKROCK_COLOR;
		light2D.Color = LIGHT2D.PINKROCK_COLOR;
		light2D.Range = 2f;
		light2D.Angle = 0f;
		light2D.Direction = LIGHT2D.PINKROCK_DIRECTION;
		light2D.Offset = LIGHT2D.PINKROCK_OFFSET;
		light2D.shape = global::LightShape.Circle;
		light2D.drawOverlay = true;
		return gameObject;
	}

	// Token: 0x06001097 RID: 4247 RVA: 0x00062AE8 File Offset: 0x00060CE8
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06001098 RID: 4248 RVA: 0x00062AEA File Offset: 0x00060CEA
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000A85 RID: 2693
	public string ID = "PinkRock";
}
