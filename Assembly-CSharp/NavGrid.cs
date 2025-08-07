using System;
using System.Collections.Generic;
using HUSL;
using UnityEngine;

// Token: 0x020004CC RID: 1228
public class NavGrid
{
	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x06001A38 RID: 6712 RVA: 0x000913B5 File Offset: 0x0008F5B5
	// (set) Token: 0x06001A39 RID: 6713 RVA: 0x000913BD File Offset: 0x0008F5BD
	public NavTable NavTable { get; private set; }

	// Token: 0x170000A5 RID: 165
	// (get) Token: 0x06001A3A RID: 6714 RVA: 0x000913C6 File Offset: 0x0008F5C6
	// (set) Token: 0x06001A3B RID: 6715 RVA: 0x000913CE File Offset: 0x0008F5CE
	public NavGrid.Transition[] transitions { get; set; }

	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x06001A3C RID: 6716 RVA: 0x000913D7 File Offset: 0x0008F5D7
	// (set) Token: 0x06001A3D RID: 6717 RVA: 0x000913DF File Offset: 0x0008F5DF
	public NavGrid.Transition[][] transitionsByNavType { get; private set; }

	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x06001A3E RID: 6718 RVA: 0x000913E8 File Offset: 0x0008F5E8
	// (set) Token: 0x06001A3F RID: 6719 RVA: 0x000913F0 File Offset: 0x0008F5F0
	public int updateRangeX { get; private set; }

	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x06001A40 RID: 6720 RVA: 0x000913F9 File Offset: 0x0008F5F9
	// (set) Token: 0x06001A41 RID: 6721 RVA: 0x00091401 File Offset: 0x0008F601
	public int updateRangeY { get; private set; }

	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x06001A42 RID: 6722 RVA: 0x0009140A File Offset: 0x0008F60A
	// (set) Token: 0x06001A43 RID: 6723 RVA: 0x00091412 File Offset: 0x0008F612
	public int maxLinksPerCell { get; private set; }

	// Token: 0x06001A44 RID: 6724 RVA: 0x0009141B File Offset: 0x0008F61B
	public static NavType MirrorNavType(NavType nav_type)
	{
		if (nav_type == NavType.LeftWall)
		{
			return NavType.RightWall;
		}
		if (nav_type == NavType.RightWall)
		{
			return NavType.LeftWall;
		}
		return nav_type;
	}

	// Token: 0x06001A45 RID: 6725 RVA: 0x0009142C File Offset: 0x0008F62C
	public NavGrid(string id, NavGrid.Transition[] transitions, NavGrid.NavTypeData[] nav_type_data, CellOffset[] bounding_offsets, NavTableValidator[] validators, int update_range_x, int update_range_y, int max_links_per_cell)
	{
		this.DirtyBitFlags = new byte[(Grid.CellCount + 7) / 8];
		this.DirtyCells = new List<int>();
		this.id = id;
		this.Validators = validators;
		this.navTypeData = nav_type_data;
		this.transitions = transitions;
		this.boundingOffsets = bounding_offsets;
		List<NavType> list = new List<NavType>();
		this.updateRangeX = update_range_x;
		this.updateRangeY = update_range_y;
		this.maxLinksPerCell = max_links_per_cell + 1;
		for (int i = 0; i < transitions.Length; i++)
		{
			DebugUtil.Assert(i >= 0 && i <= 255);
			transitions[i].id = (byte)i;
			if (!list.Contains(transitions[i].start))
			{
				list.Add(transitions[i].start);
			}
			if (!list.Contains(transitions[i].end))
			{
				list.Add(transitions[i].end);
			}
		}
		this.ValidNavTypes = list.ToArray();
		this.DebugViewLinkType = new bool[this.ValidNavTypes.Length];
		this.DebugViewValidCellsType = new bool[this.ValidNavTypes.Length];
		foreach (NavType navType in this.ValidNavTypes)
		{
			this.GetNavTypeData(navType);
		}
		this.Links = new NavGrid.Link[this.maxLinksPerCell * Grid.CellCount];
		this.NavTable = new NavTable(Grid.CellCount);
		this.transitions = transitions;
		this.transitionsByNavType = new NavGrid.Transition[11][];
		for (int k = 0; k < 11; k++)
		{
			List<NavGrid.Transition> list2 = new List<NavGrid.Transition>();
			NavType navType2 = (NavType)k;
			foreach (NavGrid.Transition transition in transitions)
			{
				if (transition.start == navType2)
				{
					list2.Add(transition);
				}
			}
			this.transitionsByNavType[k] = list2.ToArray();
		}
		foreach (NavTableValidator navTableValidator in validators)
		{
			navTableValidator.onDirty = (Action<int>)Delegate.Combine(navTableValidator.onDirty, new Action<int>(this.AddDirtyCell));
		}
		this.potentialScratchPad = new PathFinder.PotentialScratchPad(this.maxLinksPerCell);
		this.InitializeGraph();
	}

	// Token: 0x06001A46 RID: 6726 RVA: 0x00091678 File Offset: 0x0008F878
	public NavGrid.NavTypeData GetNavTypeData(NavType nav_type)
	{
		foreach (NavGrid.NavTypeData navTypeData in this.navTypeData)
		{
			if (navTypeData.navType == nav_type)
			{
				return navTypeData;
			}
		}
		throw new Exception("Missing nav type data for nav type:" + nav_type.ToString());
	}

	// Token: 0x06001A47 RID: 6727 RVA: 0x000916CC File Offset: 0x0008F8CC
	public bool HasNavTypeData(NavType nav_type)
	{
		NavGrid.NavTypeData[] array = this.navTypeData;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].navType == nav_type)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001A48 RID: 6728 RVA: 0x00091700 File Offset: 0x0008F900
	public HashedString GetIdleAnim(NavType nav_type)
	{
		return this.GetNavTypeData(nav_type).idleAnim;
	}

	// Token: 0x06001A49 RID: 6729 RVA: 0x0009170E File Offset: 0x0008F90E
	public void InitializeGraph()
	{
		NavGridUpdater.InitializeNavGrid(this.NavTable, this.Validators, this.boundingOffsets, this.maxLinksPerCell, this.Links, this.transitionsByNavType);
	}

	// Token: 0x06001A4A RID: 6730 RVA: 0x0009173C File Offset: 0x0008F93C
	public void UpdateGraph()
	{
		int count = this.DirtyCells.Count;
		for (int i = 0; i < count; i++)
		{
			int num;
			int num2;
			Grid.CellToXY(this.DirtyCells[i], out num, out num2);
			int num3 = Grid.ClampX(num - this.updateRangeX);
			int num4 = Grid.ClampY(num2 - this.updateRangeY);
			int num5 = Grid.ClampX(num + this.updateRangeX);
			int num6 = Grid.ClampY(num2 + this.updateRangeY);
			for (int j = num4; j <= num6; j++)
			{
				for (int k = num3; k <= num5; k++)
				{
					this.AddDirtyCell(Grid.XYToCell(k, j));
				}
			}
		}
		this.UpdateGraph(this.DirtyCells);
		foreach (int num7 in this.DirtyCells)
		{
			this.DirtyBitFlags[num7 / 8] = 0;
		}
		this.DirtyCells.Clear();
	}

	// Token: 0x06001A4B RID: 6731 RVA: 0x0009184C File Offset: 0x0008FA4C
	public void UpdateGraph(IEnumerable<int> dirty_nav_cells)
	{
		NavGridUpdater.UpdateNavGrid(this.NavTable, this.Validators, this.boundingOffsets, this.maxLinksPerCell, this.Links, this.transitionsByNavType, this.teleportTransitions, dirty_nav_cells);
		if (this.OnNavGridUpdateComplete != null)
		{
			this.OnNavGridUpdateComplete(dirty_nav_cells);
		}
	}

	// Token: 0x06001A4C RID: 6732 RVA: 0x0009189D File Offset: 0x0008FA9D
	public static void DebugDrawPath(int start_cell, int end_cell)
	{
		Grid.CellToPosCCF(start_cell, Grid.SceneLayer.Move);
		Grid.CellToPosCCF(end_cell, Grid.SceneLayer.Move);
	}

	// Token: 0x06001A4D RID: 6733 RVA: 0x000918B4 File Offset: 0x0008FAB4
	public static void DebugDrawPath(PathFinder.Path path)
	{
		if (path.nodes != null)
		{
			for (int i = 0; i < path.nodes.Count - 1; i++)
			{
				NavGrid.DebugDrawPath(path.nodes[i].cell, path.nodes[i + 1].cell);
			}
		}
	}

	// Token: 0x06001A4E RID: 6734 RVA: 0x0009190C File Offset: 0x0008FB0C
	private void DebugDrawValidCells()
	{
		Color white = Color.white;
		int cellCount = Grid.CellCount;
		for (int i = 0; i < cellCount; i++)
		{
			for (int j = 0; j < 11; j++)
			{
				NavType navType = (NavType)j;
				if (this.NavTable.IsValid(i, navType) && this.DrawNavTypeCell(navType, ref white))
				{
					DebugExtension.DebugPoint(NavTypeHelper.GetNavPos(i, navType), white, 1f, 0f, false);
				}
			}
		}
	}

	// Token: 0x06001A4F RID: 6735 RVA: 0x00091978 File Offset: 0x0008FB78
	private void DebugDrawLinks()
	{
		Color white = Color.white;
		for (int i = 0; i < Grid.CellCount; i++)
		{
			int num = i * this.maxLinksPerCell;
			for (int num2 = this.Links[num].link; num2 != NavGrid.InvalidCell; num2 = this.Links[num].link)
			{
				NavTypeHelper.GetNavPos(i, this.Links[num].startNavType);
				if (this.DrawNavTypeLink(this.Links[num].startNavType, ref white) || this.DrawNavTypeLink(this.Links[num].endNavType, ref white))
				{
					NavTypeHelper.GetNavPos(num2, this.Links[num].endNavType);
				}
				num++;
			}
		}
	}

	// Token: 0x06001A50 RID: 6736 RVA: 0x00091A48 File Offset: 0x0008FC48
	private bool DrawNavTypeLink(NavType nav_type, ref Color color)
	{
		color = this.NavTypeColor(nav_type);
		if (this.DebugViewLinksAll)
		{
			return true;
		}
		for (int i = 0; i < this.ValidNavTypes.Length; i++)
		{
			if (this.ValidNavTypes[i] == nav_type)
			{
				return this.DebugViewLinkType[i];
			}
		}
		return false;
	}

	// Token: 0x06001A51 RID: 6737 RVA: 0x00091A94 File Offset: 0x0008FC94
	private bool DrawNavTypeCell(NavType nav_type, ref Color color)
	{
		color = this.NavTypeColor(nav_type);
		if (this.DebugViewValidCellsAll)
		{
			return true;
		}
		for (int i = 0; i < this.ValidNavTypes.Length; i++)
		{
			if (this.ValidNavTypes[i] == nav_type)
			{
				return this.DebugViewValidCellsType[i];
			}
		}
		return false;
	}

	// Token: 0x06001A52 RID: 6738 RVA: 0x00091AE0 File Offset: 0x0008FCE0
	public void DebugUpdate()
	{
		if (this.DebugViewValidCells)
		{
			this.DebugDrawValidCells();
		}
		if (this.DebugViewLinks)
		{
			this.DebugDrawLinks();
		}
	}

	// Token: 0x06001A53 RID: 6739 RVA: 0x00091B00 File Offset: 0x0008FD00
	public void AddDirtyCell(int cell)
	{
		if (Grid.IsValidCell(cell) && ((int)this.DirtyBitFlags[cell / 8] & (1 << cell % 8)) == 0)
		{
			this.DirtyCells.Add(cell);
			byte[] dirtyBitFlags = this.DirtyBitFlags;
			int num = cell / 8;
			dirtyBitFlags[num] |= (byte)(1 << cell % 8);
		}
	}

	// Token: 0x06001A54 RID: 6740 RVA: 0x00091B54 File Offset: 0x0008FD54
	public void Clear()
	{
		NavTableValidator[] validators = this.Validators;
		for (int i = 0; i < validators.Length; i++)
		{
			validators[i].Clear();
		}
	}

	// Token: 0x06001A55 RID: 6741 RVA: 0x00091B80 File Offset: 0x0008FD80
	public Color NavTypeColor(NavType navType)
	{
		if (this.debugColorLookup == null)
		{
			this.debugColorLookup = new Color[11];
			for (int i = 0; i < 11; i++)
			{
				double num = (double)i / 11.0;
				IList<double> list = ColorConverter.HUSLToRGB(new double[]
				{
					num * 360.0,
					100.0,
					50.0
				});
				this.debugColorLookup[i] = new Color((float)list[0], (float)list[1], (float)list[2]);
			}
		}
		return this.debugColorLookup[(int)navType];
	}

	// Token: 0x04000F2A RID: 3882
	public bool DebugViewAllPaths;

	// Token: 0x04000F2B RID: 3883
	public bool DebugViewValidCells;

	// Token: 0x04000F2C RID: 3884
	public bool[] DebugViewValidCellsType;

	// Token: 0x04000F2D RID: 3885
	public bool DebugViewValidCellsAll;

	// Token: 0x04000F2E RID: 3886
	public bool DebugViewLinks;

	// Token: 0x04000F2F RID: 3887
	public bool[] DebugViewLinkType;

	// Token: 0x04000F30 RID: 3888
	public bool DebugViewLinksAll;

	// Token: 0x04000F31 RID: 3889
	public static int InvalidHandle = -1;

	// Token: 0x04000F32 RID: 3890
	public static int InvalidIdx = -1;

	// Token: 0x04000F33 RID: 3891
	public static int InvalidCell = -1;

	// Token: 0x04000F34 RID: 3892
	public Dictionary<int, int> teleportTransitions = new Dictionary<int, int>();

	// Token: 0x04000F35 RID: 3893
	public NavGrid.Link[] Links;

	// Token: 0x04000F37 RID: 3895
	private byte[] DirtyBitFlags;

	// Token: 0x04000F38 RID: 3896
	private List<int> DirtyCells;

	// Token: 0x04000F39 RID: 3897
	private NavTableValidator[] Validators = new NavTableValidator[0];

	// Token: 0x04000F3A RID: 3898
	private CellOffset[] boundingOffsets;

	// Token: 0x04000F3B RID: 3899
	public string id;

	// Token: 0x04000F3C RID: 3900
	public bool updateEveryFrame;

	// Token: 0x04000F3D RID: 3901
	public PathFinder.PotentialScratchPad potentialScratchPad;

	// Token: 0x04000F3E RID: 3902
	public Action<IEnumerable<int>> OnNavGridUpdateComplete;

	// Token: 0x04000F41 RID: 3905
	public NavType[] ValidNavTypes;

	// Token: 0x04000F42 RID: 3906
	public NavGrid.NavTypeData[] navTypeData;

	// Token: 0x04000F46 RID: 3910
	private Color[] debugColorLookup;

	// Token: 0x02001328 RID: 4904
	public struct Link
	{
		// Token: 0x060088DA RID: 35034 RVA: 0x0034A024 File Offset: 0x00348224
		public Link(int link, NavType start_nav_type, NavType end_nav_type, byte transition_id, byte cost)
		{
			this.link = link;
			this.startNavType = start_nav_type;
			this.endNavType = end_nav_type;
			this.transitionId = transition_id;
			this.cost = cost;
		}

		// Token: 0x04006880 RID: 26752
		public int link;

		// Token: 0x04006881 RID: 26753
		public NavType startNavType;

		// Token: 0x04006882 RID: 26754
		public NavType endNavType;

		// Token: 0x04006883 RID: 26755
		public byte transitionId;

		// Token: 0x04006884 RID: 26756
		public byte cost;
	}

	// Token: 0x02001329 RID: 4905
	public struct NavTypeData
	{
		// Token: 0x04006885 RID: 26757
		public NavType navType;

		// Token: 0x04006886 RID: 26758
		public Vector2 animControllerOffset;

		// Token: 0x04006887 RID: 26759
		public bool flipX;

		// Token: 0x04006888 RID: 26760
		public bool flipY;

		// Token: 0x04006889 RID: 26761
		public float rotation;

		// Token: 0x0400688A RID: 26762
		public HashedString idleAnim;
	}

	// Token: 0x0200132A RID: 4906
	public struct Transition
	{
		// Token: 0x060088DB RID: 35035 RVA: 0x0034A04C File Offset: 0x0034824C
		public override string ToString()
		{
			return string.Format("{0}: {1}->{2} ({3}); offset {4},{5}", new object[] { this.id, this.start, this.end, this.startAxis, this.x, this.y });
		}

		// Token: 0x060088DC RID: 35036 RVA: 0x0034A0C0 File Offset: 0x003482C0
		public Transition(NavType start, NavType end, int x, int y, NavAxis start_axis, bool is_looping, bool loop_has_pre, bool is_escape, int cost, string anim, CellOffset[] void_offsets, CellOffset[] solid_offsets, NavOffset[] valid_nav_offsets, NavOffset[] invalid_nav_offsets, bool critter = false, float animSpeed = 1f, bool useOffsetX = false)
		{
			DebugUtil.Assert(cost <= 255 && cost >= 0);
			this.id = byte.MaxValue;
			this.start = start;
			this.end = end;
			this.x = x;
			this.y = y;
			this.startAxis = start_axis;
			this.isLooping = is_looping;
			this.isEscape = is_escape;
			this.anim = anim;
			this.preAnim = "";
			this.cost = (byte)cost;
			if (string.IsNullOrEmpty(this.anim))
			{
				this.anim = string.Concat(new string[]
				{
					start.ToString().ToLower(),
					"_",
					end.ToString().ToLower(),
					"_",
					x.ToString(),
					"_",
					y.ToString()
				});
			}
			if (this.isLooping)
			{
				if (loop_has_pre)
				{
					this.preAnim = this.anim + "_pre";
				}
				this.anim += "_loop";
			}
			if (this.startAxis != NavAxis.NA)
			{
				this.anim += ((this.startAxis == NavAxis.X) ? "_x" : "_y");
			}
			this.voidOffsets = void_offsets;
			this.solidOffsets = solid_offsets;
			this.validNavOffsets = valid_nav_offsets;
			this.invalidNavOffsets = invalid_nav_offsets;
			this.isCritter = critter;
			this.useXOffset = useOffsetX;
			this.animSpeed = animSpeed;
		}

		// Token: 0x060088DD RID: 35037 RVA: 0x0034A254 File Offset: 0x00348454
		public int IsValid(int cell, NavTable nav_table)
		{
			if (!Grid.IsCellOffsetValid(cell, this.x, this.y))
			{
				return Grid.InvalidCell;
			}
			int num = Grid.OffsetCell(cell, this.x, this.y);
			if (!nav_table.IsValid(num, this.end))
			{
				return Grid.InvalidCell;
			}
			Grid.BuildFlags buildFlags = Grid.BuildFlags.Solid | Grid.BuildFlags.DupeImpassable;
			if (this.isCritter)
			{
				buildFlags |= Grid.BuildFlags.CritterImpassable;
			}
			foreach (CellOffset cellOffset in this.voidOffsets)
			{
				int num2 = Grid.OffsetCell(cell, cellOffset.x, cellOffset.y);
				if (Grid.IsValidCell(num2) && (Grid.BuildMasks[num2] & buildFlags) != ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor))
				{
					if (this.isCritter)
					{
						return Grid.InvalidCell;
					}
					if ((Grid.BuildMasks[num2] & Grid.BuildFlags.DupePassable) == ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor))
					{
						return Grid.InvalidCell;
					}
				}
			}
			foreach (CellOffset cellOffset2 in this.solidOffsets)
			{
				int num3 = Grid.OffsetCell(cell, cellOffset2.x, cellOffset2.y);
				if (Grid.IsValidCell(num3) && !Grid.Solid[num3])
				{
					return Grid.InvalidCell;
				}
			}
			foreach (NavOffset navOffset in this.validNavOffsets)
			{
				int num4 = Grid.OffsetCell(cell, navOffset.offset.x, navOffset.offset.y);
				if (!nav_table.IsValid(num4, navOffset.navType))
				{
					return Grid.InvalidCell;
				}
			}
			foreach (NavOffset navOffset2 in this.invalidNavOffsets)
			{
				int num5 = Grid.OffsetCell(cell, navOffset2.offset.x, navOffset2.offset.y);
				if (nav_table.IsValid(num5, navOffset2.navType))
				{
					return Grid.InvalidCell;
				}
			}
			if (this.start == NavType.Tube)
			{
				if (this.end == NavType.Tube)
				{
					GameObject gameObject = Grid.Objects[cell, 9];
					GameObject gameObject2 = Grid.Objects[num, 9];
					TravelTubeUtilityNetworkLink travelTubeUtilityNetworkLink = (gameObject ? gameObject.GetComponent<TravelTubeUtilityNetworkLink>() : null);
					TravelTubeUtilityNetworkLink travelTubeUtilityNetworkLink2 = (gameObject2 ? gameObject2.GetComponent<TravelTubeUtilityNetworkLink>() : null);
					if (travelTubeUtilityNetworkLink)
					{
						int num6;
						int num7;
						travelTubeUtilityNetworkLink.GetCells(out num6, out num7);
						if (num != num6 && num != num7)
						{
							return Grid.InvalidCell;
						}
						UtilityConnections utilityConnections = UtilityConnectionsExtensions.DirectionFromToCell(cell, num);
						if (utilityConnections == (UtilityConnections)0)
						{
							return Grid.InvalidCell;
						}
						if (Game.Instance.travelTubeSystem.GetConnections(num, false) != utilityConnections)
						{
							return Grid.InvalidCell;
						}
					}
					else if (travelTubeUtilityNetworkLink2)
					{
						int num8;
						int num9;
						travelTubeUtilityNetworkLink2.GetCells(out num8, out num9);
						if (cell != num8 && cell != num9)
						{
							return Grid.InvalidCell;
						}
						UtilityConnections utilityConnections2 = UtilityConnectionsExtensions.DirectionFromToCell(num, cell);
						if (utilityConnections2 == (UtilityConnections)0)
						{
							return Grid.InvalidCell;
						}
						if (Game.Instance.travelTubeSystem.GetConnections(cell, false) != utilityConnections2)
						{
							return Grid.InvalidCell;
						}
					}
					else
					{
						bool flag = this.startAxis == NavAxis.X;
						int num10 = cell;
						for (int j = 0; j < 2; j++)
						{
							if ((flag && j == 0) || (!flag && j == 1))
							{
								int num11 = ((this.x > 0) ? 1 : (-1));
								for (int k = 0; k < Mathf.Abs(this.x); k++)
								{
									UtilityConnections connections = Game.Instance.travelTubeSystem.GetConnections(num10, false);
									if (num11 > 0 && (connections & UtilityConnections.Right) == (UtilityConnections)0)
									{
										return Grid.InvalidCell;
									}
									if (num11 < 0 && (connections & UtilityConnections.Left) == (UtilityConnections)0)
									{
										return Grid.InvalidCell;
									}
									num10 = Grid.OffsetCell(num10, num11, 0);
								}
							}
							else
							{
								int num12 = ((this.y > 0) ? 1 : (-1));
								for (int l = 0; l < Mathf.Abs(this.y); l++)
								{
									UtilityConnections connections2 = Game.Instance.travelTubeSystem.GetConnections(num10, false);
									if (num12 > 0 && (connections2 & UtilityConnections.Up) == (UtilityConnections)0)
									{
										return Grid.InvalidCell;
									}
									if (num12 < 0 && (connections2 & UtilityConnections.Down) == (UtilityConnections)0)
									{
										return Grid.InvalidCell;
									}
									num10 = Grid.OffsetCell(num10, 0, num12);
								}
							}
						}
					}
				}
				else
				{
					UtilityConnections connections3 = Game.Instance.travelTubeSystem.GetConnections(cell, false);
					if (this.y > 0)
					{
						if (connections3 != UtilityConnections.Down)
						{
							return Grid.InvalidCell;
						}
					}
					else if (this.x > 0)
					{
						if (connections3 != UtilityConnections.Left)
						{
							return Grid.InvalidCell;
						}
					}
					else if (this.x < 0)
					{
						if (connections3 != UtilityConnections.Right)
						{
							return Grid.InvalidCell;
						}
					}
					else
					{
						if (this.y >= 0)
						{
							return Grid.InvalidCell;
						}
						if (connections3 != UtilityConnections.Up)
						{
							return Grid.InvalidCell;
						}
					}
				}
			}
			else if (this.start == NavType.Floor && this.end == NavType.Tube)
			{
				int num13 = Grid.OffsetCell(cell, this.x, this.y);
				if (Game.Instance.travelTubeSystem.GetConnections(num13, false) != UtilityConnections.Up)
				{
					return Grid.InvalidCell;
				}
			}
			return num;
		}

		// Token: 0x0400688B RID: 26763
		public NavType start;

		// Token: 0x0400688C RID: 26764
		public NavType end;

		// Token: 0x0400688D RID: 26765
		public NavAxis startAxis;

		// Token: 0x0400688E RID: 26766
		public int x;

		// Token: 0x0400688F RID: 26767
		public int y;

		// Token: 0x04006890 RID: 26768
		public byte id;

		// Token: 0x04006891 RID: 26769
		public byte cost;

		// Token: 0x04006892 RID: 26770
		public bool isLooping;

		// Token: 0x04006893 RID: 26771
		public bool isEscape;

		// Token: 0x04006894 RID: 26772
		public string preAnim;

		// Token: 0x04006895 RID: 26773
		public string anim;

		// Token: 0x04006896 RID: 26774
		public float animSpeed;

		// Token: 0x04006897 RID: 26775
		public CellOffset[] voidOffsets;

		// Token: 0x04006898 RID: 26776
		public CellOffset[] solidOffsets;

		// Token: 0x04006899 RID: 26777
		public NavOffset[] validNavOffsets;

		// Token: 0x0400689A RID: 26778
		public NavOffset[] invalidNavOffsets;

		// Token: 0x0400689B RID: 26779
		public bool isCritter;

		// Token: 0x0400689C RID: 26780
		public bool useXOffset;
	}
}
