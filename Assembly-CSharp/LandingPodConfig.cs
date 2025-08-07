using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200026F RID: 623
public class LandingPodConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000C9C RID: 3228 RVA: 0x0004B6CA File Offset: 0x000498CA
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000C9D RID: 3229 RVA: 0x0004B6D1 File Offset: 0x000498D1
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000C9E RID: 3230 RVA: 0x0004B6D4 File Offset: 0x000498D4
	public GameObject CreatePrefab()
	{
		string text = "LandingPod";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.LANDING_POD.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.LANDING_POD.DESC;
		float num = 2000f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("rocket_puft_pod_kanim"), "grounded", Grid.SceneLayer.Building, 3, 3, tier, tier2, SimHashes.Creature, null, 293f);
		gameObject.AddOrGet<PodLander>();
		gameObject.AddOrGet<MinionStorage>();
		return gameObject;
	}

	// Token: 0x06000C9F RID: 3231 RVA: 0x0004B743 File Offset: 0x00049943
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06000CA0 RID: 3232 RVA: 0x0004B75A File Offset: 0x0004995A
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000899 RID: 2201
	public const string ID = "LandingPod";
}
