using System;
using STRINGS;
using UnityEngine;

// Token: 0x020002F5 RID: 757
public class SlimeCometConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000F7F RID: 3967 RVA: 0x0005E499 File Offset: 0x0005C699
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000F80 RID: 3968 RVA: 0x0005E4A0 File Offset: 0x0005C6A0
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000F81 RID: 3969 RVA: 0x0005E4A4 File Offset: 0x0005C6A4
	public GameObject CreatePrefab()
	{
		float mass = ElementLoader.FindElementByHash(SimHashes.SlimeMold).defaultValues.mass;
		GameObject gameObject = BaseCometConfig.BaseComet(SlimeCometConfig.ID, UI.SPACEDESTINATIONS.COMETS.SLIMECOMET.NAME, "meteor_slime_kanim", SimHashes.SlimeMold, new Vector2(mass * 0.8f * 2f, mass * 1.2f * 2f), new Vector2(310.15f, 323.15f), "Meteor_slimeball_Impact", 7, SimHashes.ContaminatedOxygen, SpawnFXHashes.MeteorImpactSlime, 0.6f);
		Comet component = gameObject.GetComponent<Comet>();
		component.entityDamage = 0;
		component.totalTileDamage = 0f;
		component.explosionOreCount = new Vector2I(1, 2);
		component.explosionSpeedRange = new Vector2(4f, 7f);
		component.addTiles = 2;
		component.addTilesMinHeight = 1;
		component.addTilesMaxHeight = 2;
		component.diseaseIdx = Db.Get().Diseases.GetIndex("SlimeLung");
		component.addDiseaseCount = (int)(component.EXHAUST_RATE * 100000f);
		return gameObject;
	}

	// Token: 0x06000F82 RID: 3970 RVA: 0x0005E5A7 File Offset: 0x0005C7A7
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000F83 RID: 3971 RVA: 0x0005E5AC File Offset: 0x0005C7AC
	public void OnSpawn(GameObject go)
	{
		go.GetComponent<PrimaryElement>().AddDisease(Db.Get().Diseases.GetIndex("SlimeLung"), (int)(global::UnityEngine.Random.Range(0.9f, 1.2f) * 50f * 100000f), "Meteor");
	}

	// Token: 0x04000A02 RID: 2562
	public static string ID = "SlimeComet";

	// Token: 0x04000A03 RID: 2563
	public const int ADDED_CELLS = 2;

	// Token: 0x04000A04 RID: 2564
	private const SimHashes element = SimHashes.SlimeMold;
}
