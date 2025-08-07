using System;
using STRINGS;
using UnityEngine;

// Token: 0x020001A2 RID: 418
public class NiobiumGeyserConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600080F RID: 2063 RVA: 0x00036B82 File Offset: 0x00034D82
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x00036B89 File Offset: 0x00034D89
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x00036B8C File Offset: 0x00034D8C
	public GameObject CreatePrefab()
	{
		GeyserConfigurator.GeyserType geyserType = new GeyserConfigurator.GeyserType("molten_niobium", SimHashes.MoltenNiobium, GeyserConfigurator.GeyserShape.Molten, 3500f, 800f, 1600f, 150f, null, null, 6000f, 12000f, 0.005f, 0.01f, 15000f, 135000f, 0.4f, 0.8f, 372.15f);
		GameObject gameObject = GeyserGenericConfig.CreateGeyser("NiobiumGeyser", "geyser_molten_niobium_kanim", 3, 3, CREATURES.SPECIES.GEYSER.MOLTEN_NIOBIUM.NAME, CREATURES.SPECIES.GEYSER.MOLTEN_NIOBIUM.DESC, geyserType.idHash, geyserType.geyserTemperature, DlcManager.EXPANSION1, null);
		gameObject.GetComponent<KPrefabID>().AddTag(GameTags.DeprecatedContent, false);
		return gameObject;
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x00036C35 File Offset: 0x00034E35
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x00036C37 File Offset: 0x00034E37
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000600 RID: 1536
	public const string ID = "NiobiumGeyser";
}
