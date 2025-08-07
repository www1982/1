using System;
using STRINGS;
using UnityEngine;

// Token: 0x020002FC RID: 764
public class PhosphoricCometConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000FB0 RID: 4016 RVA: 0x0005EDFF File Offset: 0x0005CFFF
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000FB1 RID: 4017 RVA: 0x0005EE06 File Offset: 0x0005D006
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000FB2 RID: 4018 RVA: 0x0005EE0C File Offset: 0x0005D00C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = BaseCometConfig.BaseComet(PhosphoricCometConfig.ID, UI.SPACEDESTINATIONS.COMETS.PHOSPHORICCOMET.NAME, "meteor_phosphoric_kanim", SimHashes.Phosphorite, new Vector2(3f, 20f), new Vector2(310.15f, 323.15f), "Meteor_dust_heavy_Impact", 0, SimHashes.Void, SpawnFXHashes.MeteorImpactPhosphoric, 0.3f);
		Comet component = gameObject.GetComponent<Comet>();
		component.explosionOreCount = new Vector2I(1, 2);
		component.explosionSpeedRange = new Vector2(4f, 7f);
		component.entityDamage = 0;
		component.totalTileDamage = 0f;
		return gameObject;
	}

	// Token: 0x06000FB3 RID: 4019 RVA: 0x0005EEA3 File Offset: 0x0005D0A3
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000FB4 RID: 4020 RVA: 0x0005EEA5 File Offset: 0x0005D0A5
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000A0D RID: 2573
	public static string ID = "PhosphoricComet";
}
