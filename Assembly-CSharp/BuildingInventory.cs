using System;
using System.Collections.Generic;

// Token: 0x020006D0 RID: 1744
public class BuildingInventory : KMonoBehaviour
{
	// Token: 0x06002B09 RID: 11017 RVA: 0x000F8D2D File Offset: 0x000F6F2D
	public static void DestroyInstance()
	{
		BuildingInventory.Instance = null;
	}

	// Token: 0x06002B0A RID: 11018 RVA: 0x000F8D35 File Offset: 0x000F6F35
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		BuildingInventory.Instance = this;
	}

	// Token: 0x06002B0B RID: 11019 RVA: 0x000F8D43 File Offset: 0x000F6F43
	public HashSet<BuildingComplete> GetBuildings(Tag tag)
	{
		return this.Buildings[tag];
	}

	// Token: 0x06002B0C RID: 11020 RVA: 0x000F8D51 File Offset: 0x000F6F51
	public int BuildingCount(Tag tag)
	{
		if (!this.Buildings.ContainsKey(tag))
		{
			return 0;
		}
		return this.Buildings[tag].Count;
	}

	// Token: 0x06002B0D RID: 11021 RVA: 0x000F8D74 File Offset: 0x000F6F74
	public int BuildingCountForWorld_BAD_PERF(Tag tag, int worldId)
	{
		if (!this.Buildings.ContainsKey(tag))
		{
			return 0;
		}
		int num = 0;
		using (HashSet<BuildingComplete>.Enumerator enumerator = this.Buildings[tag].GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.GetMyWorldId() == worldId)
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x06002B0E RID: 11022 RVA: 0x000F8DE4 File Offset: 0x000F6FE4
	public void RegisterBuilding(BuildingComplete building)
	{
		Tag prefabTag = building.prefabid.PrefabTag;
		HashSet<BuildingComplete> hashSet;
		if (!this.Buildings.TryGetValue(prefabTag, out hashSet))
		{
			hashSet = new HashSet<BuildingComplete>();
			this.Buildings[prefabTag] = hashSet;
		}
		hashSet.Add(building);
	}

	// Token: 0x06002B0F RID: 11023 RVA: 0x000F8E28 File Offset: 0x000F7028
	public void UnregisterBuilding(BuildingComplete building)
	{
		Tag prefabTag = building.prefabid.PrefabTag;
		HashSet<BuildingComplete> hashSet;
		if (!this.Buildings.TryGetValue(prefabTag, out hashSet))
		{
			DebugUtil.DevLogError(string.Format("Unregistering building {0} before it was registered.", prefabTag));
			return;
		}
		DebugUtil.DevAssert(hashSet.Remove(building), string.Format("Building {0} was not found to be removed", prefabTag), null);
	}

	// Token: 0x0400195E RID: 6494
	public static BuildingInventory Instance;

	// Token: 0x0400195F RID: 6495
	private Dictionary<Tag, HashSet<BuildingComplete>> Buildings = new Dictionary<Tag, HashSet<BuildingComplete>>();
}
