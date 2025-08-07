using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000327 RID: 807
public class SelfChargingElectrobankConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060010A5 RID: 4261 RVA: 0x00062B71 File Offset: 0x00060D71
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1.Append(DlcManager.DLC3);
	}

	// Token: 0x060010A6 RID: 4262 RVA: 0x00062B82 File Offset: 0x00060D82
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060010A7 RID: 4263 RVA: 0x00062B88 File Offset: 0x00060D88
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("SelfChargingElectrobank", global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.ELECTROBANK_SELFCHARGING.NAME, global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.ELECTROBANK_SELFCHARGING.DESC, 20f, true, Assets.GetAnim("electrobank_large_uranium_kanim"), "idle1", Grid.SceneLayer.Ore, EntityTemplates.CollisionShape.RECTANGLE, 0.5f, 0.8f, true, 0, SimHashes.EnrichedUranium, new List<Tag>
		{
			GameTags.ChargedPortableBattery,
			GameTags.PedestalDisplayable
		});
		RadiationEmitter radiationEmitter = gameObject.AddOrGet<RadiationEmitter>();
		radiationEmitter.emitType = RadiationEmitter.RadiationEmitterType.Constant;
		radiationEmitter.radiusProportionalToRads = false;
		radiationEmitter.emitRadiusX = 5;
		radiationEmitter.emitRadiusY = radiationEmitter.emitRadiusX;
		radiationEmitter.emitRads = 120f;
		radiationEmitter.emissionOffset = new Vector3(0f, 0f, 0f);
		if (!Assets.IsTagCountable(GameTags.ChargedPortableBattery))
		{
			Assets.AddCountableTag(GameTags.ChargedPortableBattery);
		}
		gameObject.GetComponent<KCollider2D>();
		gameObject.AddTag(GameTags.IndustrialProduct);
		SelfChargingElectrobank selfChargingElectrobank = gameObject.AddComponent<SelfChargingElectrobank>();
		selfChargingElectrobank.rechargeable = false;
		selfChargingElectrobank.keepEmpty = true;
		selfChargingElectrobank.radioactivityTuning = radiationEmitter.emitRads;
		gameObject.AddOrGet<OccupyArea>().SetCellOffsets(EntityTemplates.GenerateOffsets(1, 1));
		gameObject.AddOrGet<DecorProvider>().SetValues(DECOR.PENALTY.TIER0);
		return gameObject;
	}

	// Token: 0x060010A8 RID: 4264 RVA: 0x00062CB2 File Offset: 0x00060EB2
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060010A9 RID: 4265 RVA: 0x00062CB4 File Offset: 0x00060EB4
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000A88 RID: 2696
	public const string ID = "SelfChargingElectrobank";

	// Token: 0x04000A89 RID: 2697
	public const float MASS = 20f;

	// Token: 0x04000A8A RID: 2698
	public const float POWER_DURATION = 90000f;

	// Token: 0x04000A8B RID: 2699
	public const float SELF_CHARGE_WATTAGE = 60f;
}
