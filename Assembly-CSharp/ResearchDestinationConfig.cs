using System;
using UnityEngine;

// Token: 0x02000326 RID: 806
public class ResearchDestinationConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600109F RID: 4255 RVA: 0x00062B3B File Offset: 0x00060D3B
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060010A0 RID: 4256 RVA: 0x00062B42 File Offset: 0x00060D42
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060010A1 RID: 4257 RVA: 0x00062B45 File Offset: 0x00060D45
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity("ResearchDestination", "ResearchDestination", true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<ResearchDestination>();
		return gameObject;
	}

	// Token: 0x060010A2 RID: 4258 RVA: 0x00062B65 File Offset: 0x00060D65
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060010A3 RID: 4259 RVA: 0x00062B67 File Offset: 0x00060D67
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000A87 RID: 2695
	public const string ID = "ResearchDestination";
}
