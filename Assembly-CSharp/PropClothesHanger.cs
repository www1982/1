using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000380 RID: 896
public class PropClothesHanger : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06001258 RID: 4696 RVA: 0x0006A9A4 File Offset: 0x00068BA4
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x06001259 RID: 4697 RVA: 0x0006A9AB File Offset: 0x00068BAB
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600125A RID: 4698 RVA: 0x0006A9B0 File Offset: 0x00068BB0
	public GameObject CreatePrefab()
	{
		string text = "PropClothesHanger";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPCLOTHESHANGER.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPCLOTHESHANGER.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("unlock_clothing_kanim"), "on", Grid.SceneLayer.Building, 1, 2, tier, tier2, SimHashes.Creature, new List<Tag>
		{
			GameTags.Gravitas,
			GameTags.RoomProberBuilding
		}, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Cinnabar, true);
		component.Temperature = 294.15f;
		gameObject.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		Workable workable = gameObject.AddOrGet<Workable>();
		workable.synchronizeAnims = false;
		workable.resetProgressOnStop = true;
		SetLocker setLocker = gameObject.AddOrGet<SetLocker>();
		setLocker.overrideAnim = "anim_interacts_clothingfactory_kanim";
		setLocker.dropOffset = new Vector2I(0, 1);
		setLocker.dropOnDeconstruct = true;
		gameObject.AddOrGet<Deconstructable>().audioSize = "small";
		return gameObject;
	}

	// Token: 0x0600125B RID: 4699 RVA: 0x0006AAA8 File Offset: 0x00068CA8
	public void OnPrefabInit(GameObject inst)
	{
		SetLocker component = inst.GetComponent<SetLocker>();
		component.possible_contents_ids = new string[][] { new string[] { "Warm_Vest" } };
		component.ChooseContents();
	}

	// Token: 0x0600125C RID: 4700 RVA: 0x0006AADF File Offset: 0x00068CDF
	public void OnSpawn(GameObject inst)
	{
		inst.GetComponent<Deconstructable>().SetWorkTime(5f);
	}
}
