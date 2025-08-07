using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003A4 RID: 932
public class PropGravitasRoboticTableConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06001301 RID: 4865 RVA: 0x0006CB2D File Offset: 0x0006AD2D
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06001302 RID: 4866 RVA: 0x0006CB34 File Offset: 0x0006AD34
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06001303 RID: 4867 RVA: 0x0006CB38 File Offset: 0x0006AD38
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasRobitcTable";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASROBTICTABLE.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASROBTICTABLE.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_robotic_table_kanim"), "off", Grid.SceneLayer.Building, 3, 3, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject);
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06001304 RID: 4868 RVA: 0x0006CBD1 File Offset: 0x0006ADD1
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06001305 RID: 4869 RVA: 0x0006CBE8 File Offset: 0x0006ADE8
	public void OnSpawn(GameObject inst)
	{
	}
}
