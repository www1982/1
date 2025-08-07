using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000397 RID: 919
public class PropGravitasDeskPodiumDLC4Config : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060012BE RID: 4798 RVA: 0x0006BFAA File Offset: 0x0006A1AA
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x060012BF RID: 4799 RVA: 0x0006BFB1 File Offset: 0x0006A1B1
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060012C0 RID: 4800 RVA: 0x0006BFB4 File Offset: 0x0006A1B4
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasDeskPodiumDLC4";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASDESKPODIUM.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASDESKPODIUM.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_desk_podium_kanim"), "off", Grid.SceneLayer.Building, 1, 2, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject, new string[] { "dlc4surfacepoi" });
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x060012C1 RID: 4801 RVA: 0x0006C05D File Offset: 0x0006A25D
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x060012C2 RID: 4802 RVA: 0x0006C074 File Offset: 0x0006A274
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000B91 RID: 2961
	public static string ID = "PropGravitasDeskPodiumDLC4";
}
