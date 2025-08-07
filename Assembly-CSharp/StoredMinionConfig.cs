using System;
using Klei.AI;
using UnityEngine;

// Token: 0x02000329 RID: 809
public class StoredMinionConfig : IEntityConfig
{
	// Token: 0x060010B0 RID: 4272 RVA: 0x00062CF0 File Offset: 0x00060EF0
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(StoredMinionConfig.ID, StoredMinionConfig.ID, true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<KPrefabID>();
		gameObject.AddOrGet<Traits>();
		gameObject.AddOrGet<Schedulable>();
		gameObject.AddOrGet<StoredMinionIdentity>();
		gameObject.AddOrGet<KSelectable>().IsSelectable = false;
		gameObject.AddOrGet<MinionModifiers>().addBaseTraits = false;
		return gameObject;
	}

	// Token: 0x060010B1 RID: 4273 RVA: 0x00062D48 File Offset: 0x00060F48
	public void OnPrefabInit(GameObject go)
	{
		GameObject prefab = Assets.GetPrefab(BionicMinionConfig.ID);
		if (prefab != null)
		{
			StoredMinionIdentity.IStoredMinionExtension[] components = prefab.GetComponents<StoredMinionIdentity.IStoredMinionExtension>();
			if (components != null)
			{
				for (int i = 0; i < components.Length; i++)
				{
					components[i].AddStoredMinionGameObjectRequirements(go);
				}
			}
		}
	}

	// Token: 0x060010B2 RID: 4274 RVA: 0x00062D8F File Offset: 0x00060F8F
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000A8D RID: 2701
	public static string ID = "StoredMinion";
}
