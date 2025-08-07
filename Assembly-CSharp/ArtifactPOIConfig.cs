using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002DF RID: 735
public class ArtifactPOIConfig : IMultiEntityConfig
{
	// Token: 0x06000EE7 RID: 3815 RVA: 0x000584E8 File Offset: 0x000566E8
	public List<GameObject> CreatePrefabs()
	{
		List<GameObject> list = new List<GameObject>();
		foreach (ArtifactPOIConfig.ArtifactPOIParams artifactPOIParams in this.GenerateConfigs())
		{
			list.Add(ArtifactPOIConfig.CreateArtifactPOI(artifactPOIParams.id, artifactPOIParams.anim, Strings.Get(artifactPOIParams.nameStringKey), Strings.Get(artifactPOIParams.descStringKey), artifactPOIParams.poiType.idHash));
		}
		return list;
	}

	// Token: 0x06000EE8 RID: 3816 RVA: 0x00058580 File Offset: 0x00056780
	public static GameObject CreateArtifactPOI(string id, string anim, string name, string desc, HashedString poiType)
	{
		GameObject gameObject = EntityTemplates.CreateEntity(id, id, true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<ArtifactPOIConfigurator>().presetType = poiType;
		ArtifactPOIClusterGridEntity artifactPOIClusterGridEntity = gameObject.AddOrGet<ArtifactPOIClusterGridEntity>();
		artifactPOIClusterGridEntity.m_name = name;
		artifactPOIClusterGridEntity.m_Anim = anim;
		gameObject.AddOrGetDef<ArtifactPOIStates.Def>();
		LoreBearerUtil.AddLoreTo(gameObject, new LoreBearerAction(LoreBearerUtil.UnlockNextSpaceEntry));
		return gameObject;
	}

	// Token: 0x06000EE9 RID: 3817 RVA: 0x000585D5 File Offset: 0x000567D5
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000EEA RID: 3818 RVA: 0x000585D7 File Offset: 0x000567D7
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x06000EEB RID: 3819 RVA: 0x000585DC File Offset: 0x000567DC
	private List<ArtifactPOIConfig.ArtifactPOIParams> GenerateConfigs()
	{
		List<ArtifactPOIConfig.ArtifactPOIParams> list = new List<ArtifactPOIConfig.ArtifactPOIParams>();
		if (!DlcManager.IsExpansion1Active())
		{
			return list;
		}
		list.Add(new ArtifactPOIConfig.ArtifactPOIParams("station_1", new ArtifactPOIConfigurator.ArtifactPOIType("GravitasSpaceStation1", null, false, 30000f, 60000f, DlcManager.EXPANSION1, null)));
		list.Add(new ArtifactPOIConfig.ArtifactPOIParams("station_2", new ArtifactPOIConfigurator.ArtifactPOIType("GravitasSpaceStation2", null, false, 30000f, 60000f, DlcManager.EXPANSION1, null)));
		list.Add(new ArtifactPOIConfig.ArtifactPOIParams("station_3", new ArtifactPOIConfigurator.ArtifactPOIType("GravitasSpaceStation3", null, false, 30000f, 60000f, DlcManager.EXPANSION1, null)));
		list.Add(new ArtifactPOIConfig.ArtifactPOIParams("station_4", new ArtifactPOIConfigurator.ArtifactPOIType("GravitasSpaceStation4", null, false, 30000f, 60000f, DlcManager.EXPANSION1, null)));
		list.Add(new ArtifactPOIConfig.ArtifactPOIParams("station_5", new ArtifactPOIConfigurator.ArtifactPOIType("GravitasSpaceStation5", null, false, 30000f, 60000f, DlcManager.EXPANSION1, null)));
		list.Add(new ArtifactPOIConfig.ArtifactPOIParams("station_6", new ArtifactPOIConfigurator.ArtifactPOIType("GravitasSpaceStation6", null, false, 30000f, 60000f, DlcManager.EXPANSION1, null)));
		list.Add(new ArtifactPOIConfig.ArtifactPOIParams("station_7", new ArtifactPOIConfigurator.ArtifactPOIType("GravitasSpaceStation7", null, false, 30000f, 60000f, DlcManager.EXPANSION1, null)));
		list.Add(new ArtifactPOIConfig.ArtifactPOIParams("station_8", new ArtifactPOIConfigurator.ArtifactPOIType("GravitasSpaceStation8", null, false, 30000f, 60000f, DlcManager.EXPANSION1, null)));
		list.Add(new ArtifactPOIConfig.ArtifactPOIParams("russels_teapot", new ArtifactPOIConfigurator.ArtifactPOIType("RussellsTeapot", "artifact_TeaPot", true, 30000f, 60000f, DlcManager.EXPANSION1, null)));
		list.RemoveAll((ArtifactPOIConfig.ArtifactPOIParams poi) => !DlcManager.IsCorrectDlcSubscribed(poi.poiType));
		return list;
	}

	// Token: 0x040009B9 RID: 2489
	public const string GravitasSpaceStation1 = "GravitasSpaceStation1";

	// Token: 0x040009BA RID: 2490
	public const string GravitasSpaceStation2 = "GravitasSpaceStation2";

	// Token: 0x040009BB RID: 2491
	public const string GravitasSpaceStation3 = "GravitasSpaceStation3";

	// Token: 0x040009BC RID: 2492
	public const string GravitasSpaceStation4 = "GravitasSpaceStation4";

	// Token: 0x040009BD RID: 2493
	public const string GravitasSpaceStation5 = "GravitasSpaceStation5";

	// Token: 0x040009BE RID: 2494
	public const string GravitasSpaceStation6 = "GravitasSpaceStation6";

	// Token: 0x040009BF RID: 2495
	public const string GravitasSpaceStation7 = "GravitasSpaceStation7";

	// Token: 0x040009C0 RID: 2496
	public const string GravitasSpaceStation8 = "GravitasSpaceStation8";

	// Token: 0x040009C1 RID: 2497
	public const string RussellsTeapot = "RussellsTeapot";

	// Token: 0x020011B1 RID: 4529
	public struct ArtifactPOIParams
	{
		// Token: 0x06008380 RID: 33664 RVA: 0x00334398 File Offset: 0x00332598
		public ArtifactPOIParams(string anim, ArtifactPOIConfigurator.ArtifactPOIType poiType)
		{
			this.id = "ArtifactSpacePOI_" + poiType.id;
			this.anim = anim;
			this.nameStringKey = new StringKey("STRINGS.UI.SPACEDESTINATIONS.ARTIFACT_POI." + poiType.id.ToUpper() + ".NAME");
			this.descStringKey = new StringKey("STRINGS.UI.SPACEDESTINATIONS.ARTIFACT_POI." + poiType.id.ToUpper() + ".DESC");
			this.poiType = poiType;
		}

		// Token: 0x040063CA RID: 25546
		public string id;

		// Token: 0x040063CB RID: 25547
		public string anim;

		// Token: 0x040063CC RID: 25548
		public StringKey nameStringKey;

		// Token: 0x040063CD RID: 25549
		public StringKey descStringKey;

		// Token: 0x040063CE RID: 25550
		public ArtifactPOIConfigurator.ArtifactPOIType poiType;
	}
}
