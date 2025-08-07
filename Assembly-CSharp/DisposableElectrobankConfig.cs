using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000301 RID: 769
public class DisposableElectrobankConfig : IMultiEntityConfig
{
	// Token: 0x06000FD1 RID: 4049 RVA: 0x0005F11C File Offset: 0x0005D31C
	public List<GameObject> CreatePrefabs()
	{
		List<GameObject> list = new List<GameObject>();
		if (!DlcManager.IsContentSubscribed("DLC3_ID"))
		{
			return list;
		}
		list.Add(this.CreateDisposableElectrobank("DisposableElectrobank_RawMetal", global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.ELECTROBANK_METAL_ORE.NAME, global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.ELECTROBANK_METAL_ORE.DESC, 20f, SimHashes.Cuprite, "electrobank_popcan_kanim", DlcManager.DLC3, null, "object"));
		if (DlcManager.IsExpansion1Active())
		{
			GameObject gameObject = this.CreateDisposableElectrobank("DisposableElectrobank_UraniumOre", global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.ELECTROBANK_URANIUM_ORE.NAME, global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.ELECTROBANK_URANIUM_ORE.DESC, 10f, SimHashes.UraniumOre, "electrobank_uranium_kanim", DlcManager.EXPANSION1.Append(DlcManager.DLC3), null, "object");
			RadiationEmitter radiationEmitter = gameObject.AddOrGet<RadiationEmitter>();
			radiationEmitter.emitType = RadiationEmitter.RadiationEmitterType.Constant;
			radiationEmitter.radiusProportionalToRads = false;
			radiationEmitter.emitRadiusX = 5;
			radiationEmitter.emitRadiusY = radiationEmitter.emitRadiusX;
			radiationEmitter.emitRads = 60f;
			radiationEmitter.emissionOffset = new Vector3(0f, 0f, 0f);
			list.Add(gameObject);
			gameObject.GetComponent<Electrobank>().radioactivityTuning = radiationEmitter.emitRads;
		}
		list.RemoveAll((GameObject t) => t == null);
		return list;
	}

	// Token: 0x06000FD2 RID: 4050 RVA: 0x0005F244 File Offset: 0x0005D444
	private GameObject CreateDisposableElectrobank(string id, LocString name, LocString description, float mass, SimHashes element, string animName, string[] requiredDlcIDs = null, string[] forbiddenDlcIds = null, string initialAnim = "object")
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity(id, name, description, mass, true, Assets.GetAnim(animName), initialAnim, Grid.SceneLayer.Ore, EntityTemplates.CollisionShape.RECTANGLE, 0.5f, 0.8f, true, 0, SimHashes.Creature, new List<Tag>
		{
			GameTags.ChargedPortableBattery,
			GameTags.PedestalDisplayable,
			GameTags.DisposablePortableBattery
		});
		if (!Assets.IsTagCountable(GameTags.ChargedPortableBattery))
		{
			Assets.AddCountableTag(GameTags.ChargedPortableBattery);
		}
		gameObject.AddComponent<Electrobank>();
		gameObject.AddOrGet<OccupyArea>().SetCellOffsets(EntityTemplates.GenerateOffsets(1, 1));
		gameObject.AddOrGet<DecorProvider>().SetValues(DECOR.PENALTY.TIER0);
		KPrefabID component = gameObject.GetComponent<KPrefabID>();
		component.requiredDlcIds = requiredDlcIDs;
		component.forbiddenDlcIds = forbiddenDlcIds;
		return gameObject;
	}

	// Token: 0x06000FD3 RID: 4051 RVA: 0x0005F302 File Offset: 0x0005D502
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000FD4 RID: 4052 RVA: 0x0005F304 File Offset: 0x0005D504
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000A14 RID: 2580
	public const string ID = "DisposableElectrobank_";

	// Token: 0x04000A15 RID: 2581
	public const float MASS = 20f;

	// Token: 0x04000A16 RID: 2582
	public static Dictionary<Tag, ComplexRecipe> recipes = new Dictionary<Tag, ComplexRecipe>();

	// Token: 0x04000A17 RID: 2583
	public const string ID_METAL_ORE = "DisposableElectrobank_RawMetal";

	// Token: 0x04000A18 RID: 2584
	public const string ID_URANIUM_ORE = "DisposableElectrobank_UraniumOre";
}
