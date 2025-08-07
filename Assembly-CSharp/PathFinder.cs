using System;
using System.Collections.Generic;
using System.Diagnostics;

// Token: 0x020004D2 RID: 1234
public class PathFinder
{
	// Token: 0x06001A70 RID: 6768 RVA: 0x00092200 File Offset: 0x00090400
	public static void Initialize()
	{
		NavType[] array = new NavType[11];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (NavType)i;
		}
		PathFinder.PathGrid = new PathGrid(Grid.WidthInCells, Grid.HeightInCells, false, array);
		for (int j = 0; j < Grid.CellCount; j++)
		{
			if (Grid.Visible[j] > 0 || Grid.Spawnable[j] > 0)
			{
				ListPool<int, PathFinder>.PooledList pooledList = ListPool<int, PathFinder>.Allocate();
				GameUtil.FloodFillConditional(j, PathFinder.allowPathfindingFloodFillCb, pooledList, null);
				Grid.AllowPathfinding[j] = true;
				pooledList.Recycle();
			}
		}
		Grid.OnReveal = (Action<int>)Delegate.Combine(Grid.OnReveal, new Action<int>(PathFinder.OnReveal));
	}

	// Token: 0x06001A71 RID: 6769 RVA: 0x000922A7 File Offset: 0x000904A7
	private static void OnReveal(int cell)
	{
	}

	// Token: 0x06001A72 RID: 6770 RVA: 0x000922A9 File Offset: 0x000904A9
	public static void UpdatePath(NavGrid nav_grid, PathFinderAbilities abilities, PathFinder.PotentialPath potential_path, PathFinderQuery query, ref PathFinder.Path path)
	{
		PathFinder.Run(nav_grid, abilities, potential_path, query, ref path);
	}

	// Token: 0x06001A73 RID: 6771 RVA: 0x000922B8 File Offset: 0x000904B8
	public static bool ValidatePath(NavGrid nav_grid, PathFinderAbilities abilities, ref PathFinder.Path path)
	{
		if (!path.IsValid())
		{
			return false;
		}
		for (int i = 0; i < path.nodes.Count; i++)
		{
			PathFinder.Path.Node node = path.nodes[i];
			if (i < path.nodes.Count - 1)
			{
				PathFinder.Path.Node node2 = path.nodes[i + 1];
				int num = node.cell * nav_grid.maxLinksPerCell;
				bool flag = false;
				NavGrid.Link link = nav_grid.Links[num];
				while (link.link != PathFinder.InvalidHandle)
				{
					if (link.link == node2.cell && node2.navType == link.endNavType && node.navType == link.startNavType)
					{
						PathFinder.PotentialPath potentialPath = new PathFinder.PotentialPath(node.cell, node.navType, PathFinder.PotentialPath.Flags.None);
						flag = abilities.TraversePath(ref potentialPath, node.cell, node.navType, 0, (int)link.transitionId, false);
						if (flag)
						{
							break;
						}
					}
					num++;
					link = nav_grid.Links[num];
				}
				if (!flag)
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06001A74 RID: 6772 RVA: 0x000923CC File Offset: 0x000905CC
	public static void Run(NavGrid nav_grid, PathFinderAbilities abilities, PathFinder.PotentialPath potential_path, PathFinderQuery query)
	{
		int invalidCell = PathFinder.InvalidCell;
		NavType navType = NavType.NumNavTypes;
		query.ClearResult();
		if (!Grid.IsValidCell(potential_path.cell))
		{
			return;
		}
		PathFinder.FindPaths(nav_grid, ref abilities, potential_path, query, PathFinder.Temp.Potentials, ref invalidCell, ref navType);
		if (invalidCell != PathFinder.InvalidCell)
		{
			bool flag = false;
			PathFinder.Cell cell = PathFinder.PathGrid.GetCell(invalidCell, navType, out flag);
			query.SetResult(invalidCell, cell.cost, navType);
		}
	}

	// Token: 0x06001A75 RID: 6773 RVA: 0x00092430 File Offset: 0x00090630
	public static void Run(NavGrid nav_grid, PathFinderAbilities abilities, PathFinder.PotentialPath potential_path, PathFinderQuery query, ref PathFinder.Path path)
	{
		PathFinder.Run(nav_grid, abilities, potential_path, query);
		if (query.GetResultCell() != PathFinder.InvalidCell)
		{
			PathFinder.BuildResultPath(query.GetResultCell(), query.GetResultNavType(), ref path);
			return;
		}
		path.Clear();
	}

	// Token: 0x06001A76 RID: 6774 RVA: 0x00092464 File Offset: 0x00090664
	private static void BuildResultPath(int path_cell, NavType path_nav_type, ref PathFinder.Path path)
	{
		if (path_cell != PathFinder.InvalidCell)
		{
			bool flag = false;
			PathFinder.Cell cell = PathFinder.PathGrid.GetCell(path_cell, path_nav_type, out flag);
			path.Clear();
			path.cost = cell.cost;
			while (path_cell != PathFinder.InvalidCell)
			{
				path.AddNode(new PathFinder.Path.Node
				{
					cell = path_cell,
					navType = cell.navType,
					transitionId = cell.transitionId
				});
				path_cell = cell.parent;
				if (path_cell != PathFinder.InvalidCell)
				{
					cell = PathFinder.PathGrid.GetCell(path_cell, cell.parentNavType, out flag);
				}
			}
			if (path.nodes != null)
			{
				for (int i = 0; i < path.nodes.Count / 2; i++)
				{
					PathFinder.Path.Node node = path.nodes[i];
					path.nodes[i] = path.nodes[path.nodes.Count - i - 1];
					path.nodes[path.nodes.Count - i - 1] = node;
				}
			}
		}
	}

	// Token: 0x06001A77 RID: 6775 RVA: 0x00092570 File Offset: 0x00090770
	private static void FindPaths(NavGrid nav_grid, ref PathFinderAbilities abilities, PathFinder.PotentialPath potential_path, PathFinderQuery query, PathFinder.PotentialList potentials, ref int result_cell, ref NavType result_nav_type)
	{
		potentials.Clear();
		PathFinder.PathGrid.ResetUpdate();
		PathFinder.PathGrid.BeginUpdate(potential_path.cell, false);
		bool flag;
		PathFinder.Cell cell = PathFinder.PathGrid.GetCell(potential_path, out flag);
		PathFinder.AddPotential(potential_path, Grid.InvalidCell, NavType.NumNavTypes, 0, 0, potentials, PathFinder.PathGrid, ref cell);
		int num = int.MaxValue;
		while (potentials.Count > 0)
		{
			KeyValuePair<int, PathFinder.PotentialPath> keyValuePair = potentials.Next();
			cell = PathFinder.PathGrid.GetCell(keyValuePair.Value, out flag);
			if (cell.cost == keyValuePair.Key)
			{
				if (cell.navType != NavType.Tube && query.IsMatch(keyValuePair.Value.cell, cell.parent, cell.cost) && cell.cost < num)
				{
					result_cell = keyValuePair.Value.cell;
					num = cell.cost;
					result_nav_type = cell.navType;
					break;
				}
				PathFinder.AddPotentials(nav_grid.potentialScratchPad, keyValuePair.Value, cell.cost, ref abilities, query, nav_grid.maxLinksPerCell, nav_grid.Links, potentials, PathFinder.PathGrid, cell.parent, cell.parentNavType);
			}
		}
		PathFinder.PathGrid.EndUpdate(true);
	}

	// Token: 0x06001A78 RID: 6776 RVA: 0x000926AA File Offset: 0x000908AA
	public static void AddPotential(PathFinder.PotentialPath potential_path, int parent_cell, NavType parent_nav_type, int cost, byte transition_id, PathFinder.PotentialList potentials, PathGrid path_grid, ref PathFinder.Cell cell_data)
	{
		cell_data.cost = cost;
		cell_data.parent = parent_cell;
		cell_data.SetNavTypes(potential_path.navType, parent_nav_type);
		cell_data.transitionId = transition_id;
		potentials.Add(cost, potential_path);
		path_grid.SetCell(potential_path, ref cell_data);
	}

	// Token: 0x06001A79 RID: 6777 RVA: 0x000926E6 File Offset: 0x000908E6
	[Conditional("ENABLE_PATH_DETAILS")]
	private static void BeginDetailSample(string region_name)
	{
	}

	// Token: 0x06001A7A RID: 6778 RVA: 0x000926E8 File Offset: 0x000908E8
	[Conditional("ENABLE_PATH_DETAILS")]
	private static void EndDetailSample(string region_name)
	{
	}

	// Token: 0x06001A7B RID: 6779 RVA: 0x000926EC File Offset: 0x000908EC
	public static bool IsSubmerged(int cell)
	{
		if (!Grid.IsValidCell(cell))
		{
			return false;
		}
		int num = Grid.CellAbove(cell);
		return (Grid.IsValidCell(num) && Grid.Element[num].IsLiquid) || (Grid.Element[cell].IsLiquid && Grid.IsValidCell(num) && Grid.Solid[num]);
	}

	// Token: 0x06001A7C RID: 6780 RVA: 0x00092748 File Offset: 0x00090948
	public static void AddPotentials(PathFinder.PotentialScratchPad potential_scratch_pad, PathFinder.PotentialPath potential, int cost, ref PathFinderAbilities abilities, PathFinderQuery query, int max_links_per_cell, NavGrid.Link[] links, PathFinder.PotentialList potentials, PathGrid path_grid, int parent_cell, NavType parent_nav_type)
	{
		if (!Grid.IsValidCell(potential.cell))
		{
			return;
		}
		int num = 0;
		NavGrid.Link[] linksWithCorrectNavType = potential_scratch_pad.linksWithCorrectNavType;
		int num2 = potential.cell * max_links_per_cell;
		NavGrid.Link link = links[num2];
		for (int num3 = link.link; num3 != PathFinder.InvalidHandle; num3 = link.link)
		{
			if (link.startNavType == potential.navType && (parent_cell != num3 || parent_nav_type != link.startNavType))
			{
				linksWithCorrectNavType[num++] = link;
			}
			num2++;
			link = links[num2];
		}
		int num4 = 0;
		PathFinder.PotentialScratchPad.PathGridCellData[] linksInCellRange = potential_scratch_pad.linksInCellRange;
		for (int i = 0; i < num; i++)
		{
			NavGrid.Link link2 = linksWithCorrectNavType[i];
			int link3 = link2.link;
			bool flag = false;
			PathFinder.Cell cell = path_grid.GetCell(link3, link2.endNavType, out flag);
			if (flag)
			{
				int num5 = cost + (int)link2.cost;
				bool flag2 = cell.cost == -1;
				bool flag3 = num5 < cell.cost;
				if (flag2 || flag3)
				{
					linksInCellRange[num4++] = new PathFinder.PotentialScratchPad.PathGridCellData
					{
						pathGridCell = cell,
						link = link2
					};
				}
			}
		}
		for (int j = 0; j < num4; j++)
		{
			PathFinder.PotentialScratchPad.PathGridCellData pathGridCellData = linksInCellRange[j];
			int link4 = pathGridCellData.link.link;
			pathGridCellData.isSubmerged = PathFinder.IsSubmerged(link4);
			linksInCellRange[j] = pathGridCellData;
		}
		for (int k = 0; k < num4; k++)
		{
			PathFinder.PotentialScratchPad.PathGridCellData pathGridCellData2 = linksInCellRange[k];
			NavGrid.Link link5 = pathGridCellData2.link;
			int link6 = link5.link;
			PathFinder.Cell pathGridCell = pathGridCellData2.pathGridCell;
			int num6 = cost + (int)link5.cost;
			PathFinder.PotentialPath potentialPath = potential;
			potentialPath.cell = link6;
			potentialPath.navType = link5.endNavType;
			if (pathGridCellData2.isSubmerged)
			{
				int submergedPathCostPenalty = abilities.GetSubmergedPathCostPenalty(potentialPath, link5);
				num6 += submergedPathCostPenalty;
			}
			PathFinder.PotentialPath.Flags flags = potentialPath.flags;
			bool flag4 = abilities.TraversePath(ref potentialPath, potential.cell, potential.navType, num6, (int)link5.transitionId, pathGridCellData2.isSubmerged);
			PathFinder.PotentialPath.Flags flags2 = potentialPath.flags;
			if (flag4)
			{
				PathFinder.AddPotential(potentialPath, potential.cell, potential.navType, num6, link5.transitionId, potentials, path_grid, ref pathGridCell);
			}
		}
	}

	// Token: 0x06001A7D RID: 6781 RVA: 0x00092993 File Offset: 0x00090B93
	public static void DestroyStatics()
	{
		PathFinder.PathGrid.OnCleanUp();
		PathFinder.PathGrid = null;
		PathFinder.Temp.Potentials.Clear();
	}

	// Token: 0x04000F4E RID: 3918
	public static int InvalidHandle = -1;

	// Token: 0x04000F4F RID: 3919
	public static int InvalidIdx = -1;

	// Token: 0x04000F50 RID: 3920
	public static int InvalidCell = -1;

	// Token: 0x04000F51 RID: 3921
	public static PathGrid PathGrid;

	// Token: 0x04000F52 RID: 3922
	private static readonly Func<int, bool> allowPathfindingFloodFillCb = delegate(int cell)
	{
		if (Grid.Solid[cell])
		{
			return false;
		}
		if (Grid.AllowPathfinding[cell])
		{
			return false;
		}
		Grid.AllowPathfinding[cell] = true;
		return true;
	};

	// Token: 0x0200132D RID: 4909
	public struct Cell
	{
		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x060088E2 RID: 35042 RVA: 0x0034A7D8 File Offset: 0x003489D8
		public NavType navType
		{
			get
			{
				return (NavType)(this.navTypes & 15);
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x060088E3 RID: 35043 RVA: 0x0034A7E4 File Offset: 0x003489E4
		public NavType parentNavType
		{
			get
			{
				return (NavType)(this.navTypes >> 4);
			}
		}

		// Token: 0x060088E4 RID: 35044 RVA: 0x0034A7F0 File Offset: 0x003489F0
		public void SetNavTypes(NavType type, NavType parent_type)
		{
			this.navTypes = (byte)(type | (parent_type << 4));
		}

		// Token: 0x040068A7 RID: 26791
		public int cost;

		// Token: 0x040068A8 RID: 26792
		public int parent;

		// Token: 0x040068A9 RID: 26793
		public short queryId;

		// Token: 0x040068AA RID: 26794
		private byte navTypes;

		// Token: 0x040068AB RID: 26795
		public byte transitionId;
	}

	// Token: 0x0200132E RID: 4910
	public struct PotentialPath
	{
		// Token: 0x060088E5 RID: 35045 RVA: 0x0034A80D File Offset: 0x00348A0D
		public PotentialPath(int cell, NavType nav_type, PathFinder.PotentialPath.Flags flags)
		{
			this.cell = cell;
			this.navType = nav_type;
			this.flags = flags;
		}

		// Token: 0x060088E6 RID: 35046 RVA: 0x0034A824 File Offset: 0x00348A24
		public void SetFlags(PathFinder.PotentialPath.Flags new_flags)
		{
			this.flags |= new_flags;
		}

		// Token: 0x060088E7 RID: 35047 RVA: 0x0034A834 File Offset: 0x00348A34
		public void ClearFlags(PathFinder.PotentialPath.Flags new_flags)
		{
			this.flags &= ~new_flags;
		}

		// Token: 0x060088E8 RID: 35048 RVA: 0x0034A846 File Offset: 0x00348A46
		public bool HasFlag(PathFinder.PotentialPath.Flags flag)
		{
			return this.HasAnyFlag(flag);
		}

		// Token: 0x060088E9 RID: 35049 RVA: 0x0034A84F File Offset: 0x00348A4F
		public bool HasAnyFlag(PathFinder.PotentialPath.Flags mask)
		{
			return (this.flags & mask) > PathFinder.PotentialPath.Flags.None;
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x060088EA RID: 35050 RVA: 0x0034A85C File Offset: 0x00348A5C
		// (set) Token: 0x060088EB RID: 35051 RVA: 0x0034A864 File Offset: 0x00348A64
		public PathFinder.PotentialPath.Flags flags { readonly get; private set; }

		// Token: 0x040068AC RID: 26796
		public int cell;

		// Token: 0x040068AD RID: 26797
		public NavType navType;

		// Token: 0x02002697 RID: 9879
		[Flags]
		public enum Flags : byte
		{
			// Token: 0x0400AB88 RID: 43912
			None = 0,
			// Token: 0x0400AB89 RID: 43913
			HasAtmoSuit = 1,
			// Token: 0x0400AB8A RID: 43914
			HasJetPack = 2,
			// Token: 0x0400AB8B RID: 43915
			HasOxygenMask = 4,
			// Token: 0x0400AB8C RID: 43916
			PerformSuitChecks = 8,
			// Token: 0x0400AB8D RID: 43917
			HasLeadSuit = 16
		}
	}

	// Token: 0x0200132F RID: 4911
	public struct Path
	{
		// Token: 0x060088EC RID: 35052 RVA: 0x0034A86D File Offset: 0x00348A6D
		public void AddNode(PathFinder.Path.Node node)
		{
			if (this.nodes == null)
			{
				this.nodes = new List<PathFinder.Path.Node>();
			}
			this.nodes.Add(node);
		}

		// Token: 0x060088ED RID: 35053 RVA: 0x0034A88E File Offset: 0x00348A8E
		public bool IsValid()
		{
			return this.nodes != null && this.nodes.Count > 1;
		}

		// Token: 0x060088EE RID: 35054 RVA: 0x0034A8A8 File Offset: 0x00348AA8
		public bool HasArrived()
		{
			return this.nodes != null && this.nodes.Count > 0;
		}

		// Token: 0x060088EF RID: 35055 RVA: 0x0034A8C2 File Offset: 0x00348AC2
		public void Clear()
		{
			this.cost = 0;
			if (this.nodes != null)
			{
				this.nodes.Clear();
			}
		}

		// Token: 0x040068AF RID: 26799
		public int cost;

		// Token: 0x040068B0 RID: 26800
		public List<PathFinder.Path.Node> nodes;

		// Token: 0x02002698 RID: 9880
		public struct Node
		{
			// Token: 0x0400AB8E RID: 43918
			public int cell;

			// Token: 0x0400AB8F RID: 43919
			public NavType navType;

			// Token: 0x0400AB90 RID: 43920
			public byte transitionId;
		}
	}

	// Token: 0x02001330 RID: 4912
	public class PotentialList
	{
		// Token: 0x060088F0 RID: 35056 RVA: 0x0034A8DE File Offset: 0x00348ADE
		public KeyValuePair<int, PathFinder.PotentialPath> Next()
		{
			return this.queue.Dequeue();
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x060088F1 RID: 35057 RVA: 0x0034A8EB File Offset: 0x00348AEB
		public int Count
		{
			get
			{
				return this.queue.Count;
			}
		}

		// Token: 0x060088F2 RID: 35058 RVA: 0x0034A8F8 File Offset: 0x00348AF8
		public void Add(int cost, PathFinder.PotentialPath path)
		{
			this.queue.Enqueue(cost, path);
		}

		// Token: 0x060088F3 RID: 35059 RVA: 0x0034A907 File Offset: 0x00348B07
		public void Clear()
		{
			this.queue.Clear();
		}

		// Token: 0x040068B1 RID: 26801
		private PathFinder.PotentialList.HOTQueue<PathFinder.PotentialPath> queue = new PathFinder.PotentialList.HOTQueue<PathFinder.PotentialPath>();

		// Token: 0x02002699 RID: 9881
		public class PriorityQueue<TValue>
		{
			// Token: 0x0600C445 RID: 50245 RVA: 0x0040D7D1 File Offset: 0x0040B9D1
			public PriorityQueue()
			{
				this._baseHeap = new List<KeyValuePair<int, TValue>>();
			}

			// Token: 0x0600C446 RID: 50246 RVA: 0x0040D7E4 File Offset: 0x0040B9E4
			public void Enqueue(int priority, TValue value)
			{
				this.Insert(priority, value);
			}

			// Token: 0x0600C447 RID: 50247 RVA: 0x0040D7EE File Offset: 0x0040B9EE
			public KeyValuePair<int, TValue> Dequeue()
			{
				KeyValuePair<int, TValue> keyValuePair = this._baseHeap[0];
				this.DeleteRoot();
				return keyValuePair;
			}

			// Token: 0x0600C448 RID: 50248 RVA: 0x0040D802 File Offset: 0x0040BA02
			public KeyValuePair<int, TValue> Peek()
			{
				if (this.Count > 0)
				{
					return this._baseHeap[0];
				}
				throw new InvalidOperationException("Priority queue is empty");
			}

			// Token: 0x0600C449 RID: 50249 RVA: 0x0040D824 File Offset: 0x0040BA24
			private void ExchangeElements(int pos1, int pos2)
			{
				KeyValuePair<int, TValue> keyValuePair = this._baseHeap[pos1];
				this._baseHeap[pos1] = this._baseHeap[pos2];
				this._baseHeap[pos2] = keyValuePair;
			}

			// Token: 0x0600C44A RID: 50250 RVA: 0x0040D864 File Offset: 0x0040BA64
			private void Insert(int priority, TValue value)
			{
				KeyValuePair<int, TValue> keyValuePair = new KeyValuePair<int, TValue>(priority, value);
				this._baseHeap.Add(keyValuePair);
				this.HeapifyFromEndToBeginning(this._baseHeap.Count - 1);
			}

			// Token: 0x0600C44B RID: 50251 RVA: 0x0040D89C File Offset: 0x0040BA9C
			private int HeapifyFromEndToBeginning(int pos)
			{
				if (pos >= this._baseHeap.Count)
				{
					return -1;
				}
				while (pos > 0)
				{
					int num = (pos - 1) / 2;
					if (this._baseHeap[num].Key - this._baseHeap[pos].Key <= 0)
					{
						break;
					}
					this.ExchangeElements(num, pos);
					pos = num;
				}
				return pos;
			}

			// Token: 0x0600C44C RID: 50252 RVA: 0x0040D8FC File Offset: 0x0040BAFC
			private void DeleteRoot()
			{
				if (this._baseHeap.Count <= 1)
				{
					this._baseHeap.Clear();
					return;
				}
				this._baseHeap[0] = this._baseHeap[this._baseHeap.Count - 1];
				this._baseHeap.RemoveAt(this._baseHeap.Count - 1);
				this.HeapifyFromBeginningToEnd(0);
			}

			// Token: 0x0600C44D RID: 50253 RVA: 0x0040D968 File Offset: 0x0040BB68
			private void HeapifyFromBeginningToEnd(int pos)
			{
				int count = this._baseHeap.Count;
				if (pos >= count)
				{
					return;
				}
				for (;;)
				{
					int num = pos;
					int num2 = 2 * pos + 1;
					int num3 = 2 * pos + 2;
					if (num2 < count && this._baseHeap[num].Key - this._baseHeap[num2].Key > 0)
					{
						num = num2;
					}
					if (num3 < count && this._baseHeap[num].Key - this._baseHeap[num3].Key > 0)
					{
						num = num3;
					}
					if (num == pos)
					{
						break;
					}
					this.ExchangeElements(num, pos);
					pos = num;
				}
			}

			// Token: 0x0600C44E RID: 50254 RVA: 0x0040DA10 File Offset: 0x0040BC10
			public void Clear()
			{
				this._baseHeap.Clear();
			}

			// Token: 0x17000CD0 RID: 3280
			// (get) Token: 0x0600C44F RID: 50255 RVA: 0x0040DA1D File Offset: 0x0040BC1D
			public int Count
			{
				get
				{
					return this._baseHeap.Count;
				}
			}

			// Token: 0x0400AB91 RID: 43921
			private List<KeyValuePair<int, TValue>> _baseHeap;
		}

		// Token: 0x0200269A RID: 9882
		private class HOTQueue<TValue>
		{
			// Token: 0x0600C450 RID: 50256 RVA: 0x0040DA2C File Offset: 0x0040BC2C
			public KeyValuePair<int, TValue> Dequeue()
			{
				if (this.hotQueue.Count == 0)
				{
					PathFinder.PotentialList.PriorityQueue<TValue> priorityQueue = this.hotQueue;
					this.hotQueue = this.coldQueue;
					this.coldQueue = priorityQueue;
					this.hotThreshold = this.coldThreshold;
				}
				this.count--;
				return this.hotQueue.Dequeue();
			}

			// Token: 0x0600C451 RID: 50257 RVA: 0x0040DA88 File Offset: 0x0040BC88
			public void Enqueue(int priority, TValue value)
			{
				if (priority <= this.hotThreshold)
				{
					this.hotQueue.Enqueue(priority, value);
				}
				else
				{
					this.coldQueue.Enqueue(priority, value);
					this.coldThreshold = Math.Max(this.coldThreshold, priority);
				}
				this.count++;
			}

			// Token: 0x0600C452 RID: 50258 RVA: 0x0040DADC File Offset: 0x0040BCDC
			public KeyValuePair<int, TValue> Peek()
			{
				if (this.hotQueue.Count == 0)
				{
					PathFinder.PotentialList.PriorityQueue<TValue> priorityQueue = this.hotQueue;
					this.hotQueue = this.coldQueue;
					this.coldQueue = priorityQueue;
					this.hotThreshold = this.coldThreshold;
				}
				return this.hotQueue.Peek();
			}

			// Token: 0x0600C453 RID: 50259 RVA: 0x0040DB27 File Offset: 0x0040BD27
			public void Clear()
			{
				this.count = 0;
				this.hotThreshold = int.MinValue;
				this.hotQueue.Clear();
				this.coldThreshold = int.MinValue;
				this.coldQueue.Clear();
			}

			// Token: 0x17000CD1 RID: 3281
			// (get) Token: 0x0600C454 RID: 50260 RVA: 0x0040DB5C File Offset: 0x0040BD5C
			public int Count
			{
				get
				{
					return this.count;
				}
			}

			// Token: 0x0400AB92 RID: 43922
			private PathFinder.PotentialList.PriorityQueue<TValue> hotQueue = new PathFinder.PotentialList.PriorityQueue<TValue>();

			// Token: 0x0400AB93 RID: 43923
			private PathFinder.PotentialList.PriorityQueue<TValue> coldQueue = new PathFinder.PotentialList.PriorityQueue<TValue>();

			// Token: 0x0400AB94 RID: 43924
			private int hotThreshold = int.MinValue;

			// Token: 0x0400AB95 RID: 43925
			private int coldThreshold = int.MinValue;

			// Token: 0x0400AB96 RID: 43926
			private int count;
		}
	}

	// Token: 0x02001331 RID: 4913
	private class Temp
	{
		// Token: 0x040068B2 RID: 26802
		public static PathFinder.PotentialList Potentials = new PathFinder.PotentialList();
	}

	// Token: 0x02001332 RID: 4914
	public class PotentialScratchPad
	{
		// Token: 0x060088F7 RID: 35063 RVA: 0x0034A93B File Offset: 0x00348B3B
		public PotentialScratchPad(int max_links_per_cell)
		{
			this.linksWithCorrectNavType = new NavGrid.Link[max_links_per_cell];
			this.linksInCellRange = new PathFinder.PotentialScratchPad.PathGridCellData[max_links_per_cell];
		}

		// Token: 0x040068B3 RID: 26803
		public NavGrid.Link[] linksWithCorrectNavType;

		// Token: 0x040068B4 RID: 26804
		public PathFinder.PotentialScratchPad.PathGridCellData[] linksInCellRange;

		// Token: 0x0200269B RID: 9883
		public struct PathGridCellData
		{
			// Token: 0x0400AB97 RID: 43927
			public PathFinder.Cell pathGridCell;

			// Token: 0x0400AB98 RID: 43928
			public NavGrid.Link link;

			// Token: 0x0400AB99 RID: 43929
			public bool isSubmerged;
		}
	}
}
