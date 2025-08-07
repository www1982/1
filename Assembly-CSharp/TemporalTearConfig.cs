using System;
using UnityEngine;

// Token: 0x0200032C RID: 812
public class TemporalTearConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060010BF RID: 4287 RVA: 0x00062E02 File Offset: 0x00061002
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060010C0 RID: 4288 RVA: 0x00062E09 File Offset: 0x00061009
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060010C1 RID: 4289 RVA: 0x00062E0C File Offset: 0x0006100C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity("TemporalTear", "TemporalTear", true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<TemporalTear>();
		return gameObject;
	}

	// Token: 0x060010C2 RID: 4290 RVA: 0x00062E2C File Offset: 0x0006102C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060010C3 RID: 4291 RVA: 0x00062E2E File Offset: 0x0006102E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000A91 RID: 2705
	public const string ID = "TemporalTear";
}
