using System;
using STRINGS;
using UnityEngine;

// Token: 0x020002FB RID: 763
public class AlgaeCometConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000FA9 RID: 4009 RVA: 0x0005ED45 File Offset: 0x0005CF45
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000FAA RID: 4010 RVA: 0x0005ED4C File Offset: 0x0005CF4C
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000FAB RID: 4011 RVA: 0x0005ED50 File Offset: 0x0005CF50
	public GameObject CreatePrefab()
	{
		GameObject gameObject = BaseCometConfig.BaseComet(AlgaeCometConfig.ID, UI.SPACEDESTINATIONS.COMETS.ALGAECOMET.NAME, "meteor_algae_kanim", SimHashes.Algae, new Vector2(3f, 20f), new Vector2(310.15f, 323.15f), "Meteor_algae_Impact", 7, SimHashes.Void, SpawnFXHashes.MeteorImpactAlgae, 0.3f);
		Comet component = gameObject.GetComponent<Comet>();
		component.explosionOreCount = new Vector2I(2, 4);
		component.explosionSpeedRange = new Vector2(4f, 7f);
		component.entityDamage = 0;
		component.totalTileDamage = 0f;
		return gameObject;
	}

	// Token: 0x06000FAC RID: 4012 RVA: 0x0005EDE7 File Offset: 0x0005CFE7
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000FAD RID: 4013 RVA: 0x0005EDE9 File Offset: 0x0005CFE9
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000A0C RID: 2572
	public static string ID = "AlgaeComet";
}
