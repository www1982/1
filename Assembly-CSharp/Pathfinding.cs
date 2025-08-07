using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005F6 RID: 1526
[AddComponentMenu("KMonoBehaviour/scripts/Pathfinding")]
public class Pathfinding : KMonoBehaviour
{
	// Token: 0x060023D4 RID: 9172 RVA: 0x000CC450 File Offset: 0x000CA650
	public static void DestroyInstance()
	{
		Pathfinding.Instance = null;
		OffsetTableTracker.OnPathfindingInvalidated();
	}

	// Token: 0x060023D5 RID: 9173 RVA: 0x000CC45D File Offset: 0x000CA65D
	protected override void OnPrefabInit()
	{
		Pathfinding.Instance = this;
	}

	// Token: 0x060023D6 RID: 9174 RVA: 0x000CC465 File Offset: 0x000CA665
	public void AddNavGrid(NavGrid nav_grid)
	{
		this.NavGrids.Add(nav_grid);
	}

	// Token: 0x060023D7 RID: 9175 RVA: 0x000CC474 File Offset: 0x000CA674
	public NavGrid GetNavGrid(string id)
	{
		foreach (NavGrid navGrid in this.NavGrids)
		{
			if (navGrid.id == id)
			{
				return navGrid;
			}
		}
		global::Debug.LogError("Could not find nav grid: " + id);
		return null;
	}

	// Token: 0x060023D8 RID: 9176 RVA: 0x000CC4E8 File Offset: 0x000CA6E8
	public List<NavGrid> GetNavGrids()
	{
		return this.NavGrids;
	}

	// Token: 0x060023D9 RID: 9177 RVA: 0x000CC4F0 File Offset: 0x000CA6F0
	public void ResetNavGrids()
	{
		foreach (NavGrid navGrid in this.NavGrids)
		{
			navGrid.InitializeGraph();
		}
	}

	// Token: 0x060023DA RID: 9178 RVA: 0x000CC540 File Offset: 0x000CA740
	public void FlushNavGridsOnLoad()
	{
		if (this.navGridsHaveBeenFlushedOnLoad)
		{
			return;
		}
		this.navGridsHaveBeenFlushedOnLoad = true;
		this.UpdateNavGrids(true);
	}

	// Token: 0x060023DB RID: 9179 RVA: 0x000CC55C File Offset: 0x000CA75C
	public void UpdateNavGrids(bool update_all = false)
	{
		update_all = true;
		if (update_all)
		{
			using (List<NavGrid>.Enumerator enumerator = this.NavGrids.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NavGrid navGrid = enumerator.Current;
					navGrid.UpdateGraph();
				}
				return;
			}
		}
		foreach (NavGrid navGrid2 in this.NavGrids)
		{
			if (navGrid2.updateEveryFrame)
			{
				navGrid2.UpdateGraph();
			}
		}
		this.NavGrids[this.UpdateIdx].UpdateGraph();
		this.UpdateIdx = (this.UpdateIdx + 1) % this.NavGrids.Count;
	}

	// Token: 0x060023DC RID: 9180 RVA: 0x000CC62C File Offset: 0x000CA82C
	public void RenderEveryTick()
	{
		foreach (NavGrid navGrid in this.NavGrids)
		{
			navGrid.DebugUpdate();
		}
	}

	// Token: 0x060023DD RID: 9181 RVA: 0x000CC67C File Offset: 0x000CA87C
	public void AddDirtyNavGridCell(int cell)
	{
		foreach (NavGrid navGrid in this.NavGrids)
		{
			navGrid.AddDirtyCell(cell);
		}
	}

	// Token: 0x060023DE RID: 9182 RVA: 0x000CC6D0 File Offset: 0x000CA8D0
	public void RefreshNavCell(int cell)
	{
		HashSet<int> hashSet = new HashSet<int>();
		hashSet.Add(cell);
		foreach (NavGrid navGrid in this.NavGrids)
		{
			navGrid.UpdateGraph(hashSet);
		}
	}

	// Token: 0x060023DF RID: 9183 RVA: 0x000CC730 File Offset: 0x000CA930
	protected override void OnCleanUp()
	{
		this.NavGrids.Clear();
		OffsetTableTracker.OnPathfindingInvalidated();
	}

	// Token: 0x040014DA RID: 5338
	private List<NavGrid> NavGrids = new List<NavGrid>();

	// Token: 0x040014DB RID: 5339
	private int UpdateIdx;

	// Token: 0x040014DC RID: 5340
	private bool navGridsHaveBeenFlushedOnLoad;

	// Token: 0x040014DD RID: 5341
	public static Pathfinding Instance;
}
