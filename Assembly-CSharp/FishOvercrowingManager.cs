using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000920 RID: 2336
[AddComponentMenu("KMonoBehaviour/scripts/FishOvercrowingManager")]
public class FishOvercrowingManager : KMonoBehaviour, ISim1000ms
{
	// Token: 0x0600412B RID: 16683 RVA: 0x0016E296 File Offset: 0x0016C496
	public static void DestroyInstance()
	{
		FishOvercrowingManager.Instance = null;
	}

	// Token: 0x0600412C RID: 16684 RVA: 0x0016E29E File Offset: 0x0016C49E
	protected override void OnPrefabInit()
	{
		FishOvercrowingManager.Instance = this;
		this.cells = new FishOvercrowingManager.Cell[Grid.CellCount];
	}

	// Token: 0x0600412D RID: 16685 RVA: 0x0016E2B6 File Offset: 0x0016C4B6
	public void Add(FishOvercrowdingMonitor.Instance fish)
	{
		this.fishes.Add(fish);
	}

	// Token: 0x0600412E RID: 16686 RVA: 0x0016E2C4 File Offset: 0x0016C4C4
	public void Remove(FishOvercrowdingMonitor.Instance fish)
	{
		this.fishes.Remove(fish);
	}

	// Token: 0x0600412F RID: 16687 RVA: 0x0016E2D4 File Offset: 0x0016C4D4
	public void Sim1000ms(float dt)
	{
		int num = this.versionCounter;
		this.versionCounter = num + 1;
		int num2 = num;
		int num3 = 1;
		this.cavityIdToCavityInfo.Clear();
		this.cellToFishCount.Clear();
		ListPool<FishOvercrowingManager.FishInfo, FishOvercrowingManager>.PooledList pooledList = ListPool<FishOvercrowingManager.FishInfo, FishOvercrowingManager>.Allocate();
		foreach (FishOvercrowdingMonitor.Instance instance in this.fishes)
		{
			int num4 = Grid.PosToCell(instance);
			if (Grid.IsValidCell(num4))
			{
				FishOvercrowingManager.FishInfo fishInfo = new FishOvercrowingManager.FishInfo
				{
					cell = num4,
					fish = instance
				};
				pooledList.Add(fishInfo);
				int num5 = 0;
				this.cellToFishCount.TryGetValue(num4, out num5);
				num5++;
				this.cellToFishCount[num4] = num5;
			}
		}
		foreach (FishOvercrowingManager.FishInfo fishInfo2 in pooledList)
		{
			ListPool<int, FishOvercrowingManager>.PooledList pooledList2 = ListPool<int, FishOvercrowingManager>.Allocate();
			pooledList2.Add(fishInfo2.cell);
			int i = 0;
			int num6 = num3++;
			while (i < pooledList2.Count)
			{
				int num7 = pooledList2[i++];
				if (Grid.IsValidCell(num7))
				{
					FishOvercrowingManager.Cell cell = this.cells[num7];
					if (cell.version != num2 && Grid.IsLiquid(num7))
					{
						cell.cavityId = num6;
						cell.version = num2;
						int num8 = 0;
						this.cellToFishCount.TryGetValue(num7, out num8);
						FishOvercrowingManager.CavityInfo cavityInfo = default(FishOvercrowingManager.CavityInfo);
						if (!this.cavityIdToCavityInfo.TryGetValue(num6, out cavityInfo))
						{
							cavityInfo = default(FishOvercrowingManager.CavityInfo);
							cavityInfo.fishPrefabs = new List<KPrefabID>();
						}
						cavityInfo.fishCount += num8;
						cavityInfo.cellCount++;
						this.cavityIdToCavityInfo[num6] = cavityInfo;
						pooledList2.Add(Grid.CellLeft(num7));
						pooledList2.Add(Grid.CellRight(num7));
						pooledList2.Add(Grid.CellAbove(num7));
						pooledList2.Add(Grid.CellBelow(num7));
						this.cells[num7] = cell;
					}
				}
			}
			pooledList2.Recycle();
		}
		foreach (FishOvercrowingManager.FishInfo fishInfo3 in pooledList)
		{
			FishOvercrowingManager.Cell cell2 = this.cells[fishInfo3.cell];
			FishOvercrowingManager.CavityInfo cavityInfo2 = default(FishOvercrowingManager.CavityInfo);
			if (this.cavityIdToCavityInfo.TryGetValue(cell2.cavityId, out cavityInfo2))
			{
				cavityInfo2.fishPrefabs.Add(fishInfo3.fish.GetComponent<KPrefabID>());
			}
			fishInfo3.fish.SetOvercrowdingInfo(cavityInfo2.cellCount, cavityInfo2.fishCount);
		}
		pooledList.Recycle();
	}

	// Token: 0x06004130 RID: 16688 RVA: 0x0016E5F4 File Offset: 0x0016C7F4
	public int GetFishCavityCount(int cell, HashSet<Tag> accepted_tags)
	{
		int num = 0;
		FishOvercrowingManager.Cell cell2 = this.cells[cell];
		FishOvercrowingManager.CavityInfo cavityInfo = default(FishOvercrowingManager.CavityInfo);
		if (this.cavityIdToCavityInfo.TryGetValue(cell2.cavityId, out cavityInfo))
		{
			foreach (KPrefabID kprefabID in cavityInfo.fishPrefabs)
			{
				if (!kprefabID.HasTag(GameTags.Creatures.Bagged) && !kprefabID.HasTag(GameTags.Trapped) && accepted_tags.Contains(kprefabID.PrefabTag))
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x040028B3 RID: 10419
	public static FishOvercrowingManager Instance;

	// Token: 0x040028B4 RID: 10420
	private List<FishOvercrowdingMonitor.Instance> fishes = new List<FishOvercrowdingMonitor.Instance>();

	// Token: 0x040028B5 RID: 10421
	private Dictionary<int, FishOvercrowingManager.CavityInfo> cavityIdToCavityInfo = new Dictionary<int, FishOvercrowingManager.CavityInfo>();

	// Token: 0x040028B6 RID: 10422
	private Dictionary<int, int> cellToFishCount = new Dictionary<int, int>();

	// Token: 0x040028B7 RID: 10423
	private FishOvercrowingManager.Cell[] cells;

	// Token: 0x040028B8 RID: 10424
	private int versionCounter = 1;

	// Token: 0x020018BB RID: 6331
	private struct Cell
	{
		// Token: 0x040079B9 RID: 31161
		public int version;

		// Token: 0x040079BA RID: 31162
		public int cavityId;
	}

	// Token: 0x020018BC RID: 6332
	private struct FishInfo
	{
		// Token: 0x040079BB RID: 31163
		public int cell;

		// Token: 0x040079BC RID: 31164
		public FishOvercrowdingMonitor.Instance fish;
	}

	// Token: 0x020018BD RID: 6333
	private struct CavityInfo
	{
		// Token: 0x040079BD RID: 31165
		public List<KPrefabID> fishPrefabs;

		// Token: 0x040079BE RID: 31166
		public int fishCount;

		// Token: 0x040079BF RID: 31167
		public int cellCount;
	}
}
