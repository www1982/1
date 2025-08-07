using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003E0 RID: 992
public class MissileSetLockerConfig : IEntityConfig
{
	// Token: 0x06001449 RID: 5193 RVA: 0x000746FC File Offset: 0x000728FC
	public GameObject CreatePrefab()
	{
		string text = "MissileSetLocker";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.MISSILESETLOCKER.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.MISSILESETLOCKER.DESC;
		float num = 100f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("armoury_locker_kanim"), "on", Grid.SceneLayer.Building, 1, 2, tier, tier2, SimHashes.Creature, new List<Tag>
		{
			GameTags.Gravitas,
			GameTags.TemplateBuilding
		}, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		Workable workable = gameObject.AddOrGet<Workable>();
		workable.synchronizeAnims = true;
		workable.resetProgressOnStop = true;
		SetLocker setLocker = gameObject.AddOrGet<SetLocker>();
		setLocker.overrideAnim = "anim_interacts_locker_kanim";
		setLocker.skipAnim = true;
		setLocker.dropOffset = new Vector2I(0, 1);
		setLocker.numDataBanks = new int[] { 1, 4 };
		LoreBearerUtil.AddLoreTo(gameObject);
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x0600144A RID: 5194 RVA: 0x00074804 File Offset: 0x00072A04
	public void OnPrefabInit(GameObject inst)
	{
		SetLocker component = inst.GetComponent<SetLocker>();
		component.possible_contents_ids = new string[][] { new string[] { "MissileLongRange" } };
		component.ChooseContents();
	}

	// Token: 0x0600144B RID: 5195 RVA: 0x0007483B File Offset: 0x00072A3B
	public void OnSpawn(GameObject inst)
	{
	}
}
