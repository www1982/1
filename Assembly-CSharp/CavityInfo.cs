using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AE5 RID: 2789
public class CavityInfo
{
	// Token: 0x06005158 RID: 20824 RVA: 0x001D9364 File Offset: 0x001D7564
	public CavityInfo()
	{
		this.handle = HandleVector<int>.InvalidHandle;
		this.dirty = true;
	}

	// Token: 0x06005159 RID: 20825 RVA: 0x001D93B5 File Offset: 0x001D75B5
	public void AddBuilding(KPrefabID bc)
	{
		this.buildings.Add(bc);
		this.dirty = true;
	}

	// Token: 0x0600515A RID: 20826 RVA: 0x001D93CA File Offset: 0x001D75CA
	public void AddPlants(KPrefabID plant)
	{
		this.plants.Add(plant);
		this.dirty = true;
	}

	// Token: 0x0600515B RID: 20827 RVA: 0x001D93E0 File Offset: 0x001D75E0
	public void RemoveFromCavity(KPrefabID id, List<KPrefabID> listToRemove)
	{
		int num = -1;
		for (int i = 0; i < listToRemove.Count; i++)
		{
			if (id.InstanceID == listToRemove[i].InstanceID)
			{
				num = i;
				break;
			}
		}
		if (num >= 0)
		{
			listToRemove.RemoveAt(num);
		}
	}

	// Token: 0x0600515C RID: 20828 RVA: 0x001D9424 File Offset: 0x001D7624
	public void OnEnter(object data)
	{
		foreach (KPrefabID kprefabID in this.buildings)
		{
			if (kprefabID != null)
			{
				kprefabID.Trigger(-832141045, data);
			}
		}
	}

	// Token: 0x0600515D RID: 20829 RVA: 0x001D9488 File Offset: 0x001D7688
	public Vector3 GetCenter()
	{
		return new Vector3((float)(this.minX + (this.maxX - this.minX) / 2), (float)(this.minY + (this.maxY - this.minY) / 2));
	}

	// Token: 0x040036C8 RID: 14024
	public HandleVector<int>.Handle handle;

	// Token: 0x040036C9 RID: 14025
	public bool dirty;

	// Token: 0x040036CA RID: 14026
	public int numCells;

	// Token: 0x040036CB RID: 14027
	public int maxX;

	// Token: 0x040036CC RID: 14028
	public int maxY;

	// Token: 0x040036CD RID: 14029
	public int minX;

	// Token: 0x040036CE RID: 14030
	public int minY;

	// Token: 0x040036CF RID: 14031
	public Room room;

	// Token: 0x040036D0 RID: 14032
	public List<KPrefabID> buildings = new List<KPrefabID>();

	// Token: 0x040036D1 RID: 14033
	public List<KPrefabID> plants = new List<KPrefabID>();

	// Token: 0x040036D2 RID: 14034
	public List<KPrefabID> creatures = new List<KPrefabID>();

	// Token: 0x040036D3 RID: 14035
	public List<KPrefabID> eggs = new List<KPrefabID>();
}
