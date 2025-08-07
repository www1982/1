using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003EB RID: 1003
public class VendingMachineConfig : IEntityConfig
{
	// Token: 0x06001483 RID: 5251 RVA: 0x000753DC File Offset: 0x000735DC
	public GameObject CreatePrefab()
	{
		string text = "VendingMachine";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.VENDINGMACHINE.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.VENDINGMACHINE.DESC;
		float num = 100f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("vendingmachine_kanim"), "on", Grid.SceneLayer.Building, 2, 3, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		Workable workable = gameObject.AddOrGet<Workable>();
		workable.synchronizeAnims = false;
		workable.resetProgressOnStop = true;
		SetLocker setLocker = gameObject.AddOrGet<SetLocker>();
		setLocker.machineSound = "VendingMachine_LP";
		setLocker.overrideAnim = "anim_break_kanim";
		setLocker.dropOffset = new Vector2I(1, 1);
		LoreBearerUtil.AddLoreTo(gameObject);
		gameObject.AddOrGet<LoopingSounds>();
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06001484 RID: 5252 RVA: 0x000754D0 File Offset: 0x000736D0
	public void OnPrefabInit(GameObject inst)
	{
		SetLocker component = inst.GetComponent<SetLocker>();
		component.possible_contents_ids = new string[][] { new string[] { "FieldRation" } };
		component.ChooseContents();
	}

	// Token: 0x06001485 RID: 5253 RVA: 0x00075507 File Offset: 0x00073707
	public void OnSpawn(GameObject inst)
	{
	}
}
