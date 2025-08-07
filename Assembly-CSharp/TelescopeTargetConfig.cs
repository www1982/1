using System;
using UnityEngine;

// Token: 0x0200032B RID: 811
public class TelescopeTargetConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060010B9 RID: 4281 RVA: 0x00062DD3 File Offset: 0x00060FD3
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060010BA RID: 4282 RVA: 0x00062DDA File Offset: 0x00060FDA
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060010BB RID: 4283 RVA: 0x00062DDD File Offset: 0x00060FDD
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity("TelescopeTarget", "TelescopeTarget", true);
		gameObject.AddOrGet<TelescopeTarget>();
		return gameObject;
	}

	// Token: 0x060010BC RID: 4284 RVA: 0x00062DF6 File Offset: 0x00060FF6
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060010BD RID: 4285 RVA: 0x00062DF8 File Offset: 0x00060FF8
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000A90 RID: 2704
	public const string ID = "TelescopeTarget";
}
