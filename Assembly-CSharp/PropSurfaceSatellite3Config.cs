using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003B0 RID: 944
public class PropSurfaceSatellite3Config : IEntityConfig
{
	// Token: 0x06001337 RID: 4919 RVA: 0x0006D750 File Offset: 0x0006B950
	public GameObject CreatePrefab()
	{
		string id = PropSurfaceSatellite3Config.ID;
		string text = global::STRINGS.BUILDINGS.PREFABS.PROPSURFACESATELLITE3.NAME;
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPSURFACESATELLITE3.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(id, text, text2, num, Assets.GetAnim("satellite3_kanim"), "off", Grid.SceneLayer.Building, 6, 6, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
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
		LoreBearerUtil.AddLoreTo(gameObject);
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06001338 RID: 4920 RVA: 0x0006D830 File Offset: 0x0006BA30
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

	// Token: 0x06001339 RID: 4921 RVA: 0x0006D8A8 File Offset: 0x0006BAA8
	public void OnSpawn(GameObject inst)
	{
		inst.Subscribe(-372600542, delegate(object locker)
		{
			this.OnLockerLooted(inst);
		});
		RadiationEmitter component = inst.GetComponent<RadiationEmitter>();
		if (component != null)
		{
			component.SetEmitting(true);
		}
	}

	// Token: 0x0600133A RID: 4922 RVA: 0x0006D902 File Offset: 0x0006BB02
	private void OnLockerLooted(GameObject inst)
	{
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(ArtifactSelector.Instance.GetUniqueArtifactID(ArtifactType.Any)), inst.transform.position);
		gameObject.GetComponent<KPrefabID>().AddTag(GameTags.TerrestrialArtifact, true);
		gameObject.SetActive(true);
	}

	// Token: 0x04000B9A RID: 2970
	public static string ID = "PropSurfaceSatellite3";
}
