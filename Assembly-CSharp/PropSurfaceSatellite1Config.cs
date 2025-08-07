using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003AE RID: 942
public class PropSurfaceSatellite1Config : IEntityConfig
{
	// Token: 0x0600132D RID: 4909 RVA: 0x0006D3BC File Offset: 0x0006B5BC
	public GameObject CreatePrefab()
	{
		string text = "PropSurfaceSatellite1";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPSURFACESATELLITE1.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPSURFACESATELLITE1.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("satellite1_kanim"), "off", Grid.SceneLayer.Building, 3, 3, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		Workable workable = gameObject.AddOrGet<Workable>();
		workable.synchronizeAnims = false;
		workable.resetProgressOnStop = true;
		SetLocker setLocker = gameObject.AddOrGet<SetLocker>();
		setLocker.overrideAnim = "anim_interacts_clothingfactory_kanim";
		setLocker.dropOffset = new Vector2I(0, 1);
		setLocker.numDataBanks = new int[] { 4, 9 };
		gameObject.AddOrGet<Demolishable>();
		LoreBearerUtil.AddLoreTo(gameObject);
		return gameObject;
	}

	// Token: 0x0600132E RID: 4910 RVA: 0x0006D49C File Offset: 0x0006B69C
	public static string[][] GetLockerBaseContents()
	{
		return new string[][]
		{
			new string[]
			{
				DatabankHelper.ID,
				DatabankHelper.ID,
				DatabankHelper.ID
			},
			new string[] { "ColdBreatherSeed", "ColdBreatherSeed", "ColdBreatherSeed" },
			new string[] { "Atmo_Suit", "Glom", "Glom", "Glom" }
		};
	}

	// Token: 0x0600132F RID: 4911 RVA: 0x0006D51C File Offset: 0x0006B71C
	public void OnPrefabInit(GameObject inst)
	{
		SetLocker component = inst.GetComponent<SetLocker>();
		component.possible_contents_ids = PropSurfaceSatellite1Config.GetLockerBaseContents();
		component.ChooseContents();
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		RadiationEmitter radiationEmitter = inst.AddOrGet<RadiationEmitter>();
		radiationEmitter.emitType = RadiationEmitter.RadiationEmitterType.Constant;
		radiationEmitter.radiusProportionalToRads = false;
		radiationEmitter.emitRadiusX = 12;
		radiationEmitter.emitRadiusY = 12;
		radiationEmitter.emitRads = 2400f / ((float)radiationEmitter.emitRadiusX / 6f);
	}

	// Token: 0x06001330 RID: 4912 RVA: 0x0006D594 File Offset: 0x0006B794
	public void OnSpawn(GameObject inst)
	{
		RadiationEmitter component = inst.GetComponent<RadiationEmitter>();
		if (component != null)
		{
			component.SetEmitting(true);
		}
	}

	// Token: 0x04000B98 RID: 2968
	public const string ID = "PropSurfaceSatellite1";
}
