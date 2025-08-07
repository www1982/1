using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200039B RID: 923
public class PropGravitasFirstAidKitConfig : IEntityConfig
{
	// Token: 0x060012D5 RID: 4821 RVA: 0x0006C2E4 File Offset: 0x0006A4E4
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasFirstAidKit";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASFIRSTAIDKIT.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASFIRSTAIDKIT.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_first_aid_kit_kanim"), "off", Grid.SceneLayer.Building, 1, 1, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		Workable workable = gameObject.AddOrGet<Workable>();
		workable.synchronizeAnims = false;
		workable.resetProgressOnStop = true;
		SetLocker setLocker = gameObject.AddOrGet<SetLocker>();
		setLocker.overrideAnim = "anim_interacts_clothingfactory_kanim";
		setLocker.dropOffset = new Vector2I(0, 1);
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x060012D6 RID: 4822 RVA: 0x0006C3A8 File Offset: 0x0006A5A8
	public static string[][] GetLockerBaseContents()
	{
		string text = (DlcManager.FeatureRadiationEnabled() ? "BasicRadPill" : "IntermediateCure");
		return new string[][]
		{
			new string[] { "BasicCure", "BasicCure", "BasicCure" },
			new string[] { text, text }
		};
	}

	// Token: 0x060012D7 RID: 4823 RVA: 0x0006C401 File Offset: 0x0006A601
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		SetLocker component = inst.GetComponent<SetLocker>();
		component.possible_contents_ids = PropGravitasFirstAidKitConfig.GetLockerBaseContents();
		component.ChooseContents();
	}

	// Token: 0x060012D8 RID: 4824 RVA: 0x0006C42E File Offset: 0x0006A62E
	public void OnSpawn(GameObject inst)
	{
	}
}
