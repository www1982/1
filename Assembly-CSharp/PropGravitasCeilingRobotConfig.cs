using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000393 RID: 915
public class PropGravitasCeilingRobotConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060012AA RID: 4778 RVA: 0x0006BC98 File Offset: 0x00069E98
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060012AB RID: 4779 RVA: 0x0006BC9F File Offset: 0x00069E9F
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060012AC RID: 4780 RVA: 0x0006BCA4 File Offset: 0x00069EA4
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasCeilingRobot";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASCEILINGROBOT.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASCEILINGROBOT.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_ceiling_robot_kanim"), "off", Grid.SceneLayer.Building, 2, 4, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x060012AD RID: 4781 RVA: 0x0006BD37 File Offset: 0x00069F37
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x060012AE RID: 4782 RVA: 0x0006BD4E File Offset: 0x00069F4E
	public void OnSpawn(GameObject inst)
	{
	}
}
