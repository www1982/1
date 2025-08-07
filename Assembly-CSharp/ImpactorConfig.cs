using System;
using UnityEngine;

// Token: 0x0200030F RID: 783
public class ImpactorConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600101F RID: 4127 RVA: 0x00060D63 File Offset: 0x0005EF63
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x06001020 RID: 4128 RVA: 0x00060D6A File Offset: 0x0005EF6A
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06001021 RID: 4129 RVA: 0x00060D6D File Offset: 0x0005EF6D
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity("Impactor", "ImpactorInstance", false);
		gameObject.AddOrGet<ParallaxBackgroundObject>();
		gameObject.AddOrGet<SaveLoadRoot>();
		return gameObject;
	}

	// Token: 0x06001022 RID: 4130 RVA: 0x00060D8D File Offset: 0x0005EF8D
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06001023 RID: 4131 RVA: 0x00060D8F File Offset: 0x0005EF8F
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000A4C RID: 2636
	public const string ID = "Impactor";
}
