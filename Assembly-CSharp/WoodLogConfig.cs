using System;
using UnityEngine;

// Token: 0x0200025D RID: 605
public class WoodLogConfig : IOreConfig
{
	// Token: 0x1700001E RID: 30
	// (get) Token: 0x06000C41 RID: 3137 RVA: 0x000499B0 File Offset: 0x00047BB0
	public SimHashes ElementID
	{
		get
		{
			return SimHashes.WoodLog;
		}
	}

	// Token: 0x06000C42 RID: 3138 RVA: 0x000499B8 File Offset: 0x00047BB8
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateSolidOreEntity(this.ElementID, null);
		KPrefabID component = gameObject.GetComponent<KPrefabID>();
		component.prefabInitFn += this.OnInit;
		component.prefabSpawnFn += this.OnSpawn;
		component.RemoveTag(GameTags.HideFromSpawnTool);
		return gameObject;
	}

	// Token: 0x06000C43 RID: 3139 RVA: 0x00049A05 File Offset: 0x00047C05
	public void OnInit(GameObject inst)
	{
		PrimaryElement component = inst.GetComponent<PrimaryElement>();
		component.SetElement(this.ElementID, true);
		Element element = component.Element;
	}

	// Token: 0x06000C44 RID: 3140 RVA: 0x00049A20 File Offset: 0x00047C20
	public void OnSpawn(GameObject inst)
	{
		inst.GetComponent<PrimaryElement>().SetElement(this.ElementID, true);
	}

	// Token: 0x04000873 RID: 2163
	public const string ID = "WoodLog";

	// Token: 0x04000874 RID: 2164
	public const float C02MassEmissionWhenBurned = 0.142f;

	// Token: 0x04000875 RID: 2165
	public const float HeatWhenBurned = 7500f;

	// Token: 0x04000876 RID: 2166
	public const float EnergyWhenBurned = 250f;

	// Token: 0x04000877 RID: 2167
	public static readonly Tag TAG = TagManager.Create("WoodLog");
}
