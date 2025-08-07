using System;
using System.Collections.Generic;

// Token: 0x020004CD RID: 1229
public class NavGridUpdater
{
	// Token: 0x06001A57 RID: 6743 RVA: 0x00091C3A File Offset: 0x0008FE3A
	public static void InitializeNavGrid(NavTable nav_table, NavTableValidator[] validators, CellOffset[] bounding_offsets, int max_links_per_cell, NavGrid.Link[] links, NavGrid.Transition[][] transitions_by_nav_type)
	{
		NavGridUpdater.MarkValidCells(nav_table, validators, bounding_offsets);
		NavGridUpdater.CreateLinks(nav_table, max_links_per_cell, links, transitions_by_nav_type, new Dictionary<int, int>());
	}

	// Token: 0x06001A58 RID: 6744 RVA: 0x00091C54 File Offset: 0x0008FE54
	public static void UpdateNavGrid(NavTable nav_table, NavTableValidator[] validators, CellOffset[] bounding_offsets, int max_links_per_cell, NavGrid.Link[] links, NavGrid.Transition[][] transitions_by_nav_type, Dictionary<int, int> teleport_transitions, IEnumerable<int> dirty_nav_cells)
	{
		NavGridUpdater.UpdateValidCells(dirty_nav_cells, nav_table, validators, bounding_offsets);
		NavGridUpdater.UpdateLinks(dirty_nav_cells, nav_table, max_links_per_cell, links, transitions_by_nav_type, teleport_transitions);
	}

	// Token: 0x06001A59 RID: 6745 RVA: 0x00091C70 File Offset: 0x0008FE70
	private static void UpdateValidCells(IEnumerable<int> dirty_solid_cells, NavTable nav_table, NavTableValidator[] validators, CellOffset[] bounding_offsets)
	{
		foreach (int num in dirty_solid_cells)
		{
			for (int i = 0; i < validators.Length; i++)
			{
				validators[i].UpdateCell(num, nav_table, bounding_offsets);
			}
		}
	}

	// Token: 0x06001A5A RID: 6746 RVA: 0x00091CCC File Offset: 0x0008FECC
	private static void CreateLinksForCell(int cell, NavTable nav_table, int max_links_per_cell, NavGrid.Link[] links, NavGrid.Transition[][] transitions_by_nav_type, Dictionary<int, int> teleport_transitions)
	{
		NavGridUpdater.CreateLinks(cell, nav_table, max_links_per_cell, links, transitions_by_nav_type, teleport_transitions);
	}

	// Token: 0x06001A5B RID: 6747 RVA: 0x00091CDC File Offset: 0x0008FEDC
	private static void UpdateLinks(IEnumerable<int> dirty_nav_cells, NavTable nav_table, int max_links_per_cell, NavGrid.Link[] links, NavGrid.Transition[][] transitions_by_nav_type, Dictionary<int, int> teleport_transitions)
	{
		foreach (int num in dirty_nav_cells)
		{
			NavGridUpdater.CreateLinksForCell(num, nav_table, max_links_per_cell, links, transitions_by_nav_type, teleport_transitions);
		}
	}

	// Token: 0x06001A5C RID: 6748 RVA: 0x00091D28 File Offset: 0x0008FF28
	private static void CreateLinks(NavTable nav_table, int max_links_per_cell, NavGrid.Link[] links, NavGrid.Transition[][] transitions_by_nav_type, Dictionary<int, int> teleport_transitions)
	{
		WorkItemCollection<NavGridUpdater.CreateLinkWorkItem, object> workItemCollection = new WorkItemCollection<NavGridUpdater.CreateLinkWorkItem, object>();
		workItemCollection.Reset(null);
		for (int i = 0; i < Grid.HeightInCells; i++)
		{
			workItemCollection.Add(new NavGridUpdater.CreateLinkWorkItem(Grid.OffsetCell(0, new CellOffset(0, i)), nav_table, max_links_per_cell, links, transitions_by_nav_type, teleport_transitions));
		}
		GlobalJobManager.Run(workItemCollection);
	}

	// Token: 0x06001A5D RID: 6749 RVA: 0x00091D78 File Offset: 0x0008FF78
	private static void CreateLinks(int cell, NavTable nav_table, int max_links_per_cell, NavGrid.Link[] links, NavGrid.Transition[][] transitions_by_nav_type, Dictionary<int, int> teleport_transitions)
	{
		int num = cell * max_links_per_cell;
		int num2 = 0;
		for (int i = 0; i < 11; i++)
		{
			NavType navType = (NavType)i;
			NavGrid.Transition[] array = transitions_by_nav_type[i];
			if (array != null && nav_table.IsValid(cell, navType))
			{
				NavGrid.Transition[] array2 = array;
				for (int j = 0; j < array2.Length; j++)
				{
					NavGrid.Transition transition;
					if ((transition = array2[j]).start == NavType.Teleport && teleport_transitions.ContainsKey(cell))
					{
						int num3;
						int num4;
						Grid.CellToXY(cell, out num3, out num4);
						int num5 = teleport_transitions[cell];
						int num6;
						int num7;
						Grid.CellToXY(teleport_transitions[cell], out num6, out num7);
						transition.x = num6 - num3;
						transition.y = num7 - num4;
					}
					int num8 = transition.IsValid(cell, nav_table);
					if (num8 != Grid.InvalidCell)
					{
						links[num] = new NavGrid.Link(num8, transition.start, transition.end, transition.id, transition.cost);
						num++;
						num2++;
					}
				}
			}
		}
		if (num2 >= max_links_per_cell)
		{
			Debug.LogError("Out of nav links. Need to increase maxLinksPerCell:" + max_links_per_cell.ToString());
		}
		links[num].link = Grid.InvalidCell;
	}

	// Token: 0x06001A5E RID: 6750 RVA: 0x00091EA4 File Offset: 0x000900A4
	private static void MarkValidCells(NavTable nav_table, NavTableValidator[] validators, CellOffset[] bounding_offsets)
	{
		WorkItemCollection<NavGridUpdater.MarkValidCellWorkItem, object> workItemCollection = new WorkItemCollection<NavGridUpdater.MarkValidCellWorkItem, object>();
		workItemCollection.Reset(null);
		for (int i = 0; i < Grid.HeightInCells; i++)
		{
			workItemCollection.Add(new NavGridUpdater.MarkValidCellWorkItem(Grid.OffsetCell(0, new CellOffset(0, i)), nav_table, bounding_offsets, validators));
		}
		GlobalJobManager.Run(workItemCollection);
	}

	// Token: 0x06001A5F RID: 6751 RVA: 0x00091EEF File Offset: 0x000900EF
	public static void DebugDrawPath(int start_cell, int end_cell)
	{
		Grid.CellToPosCCF(start_cell, Grid.SceneLayer.Move);
		Grid.CellToPosCCF(end_cell, Grid.SceneLayer.Move);
	}

	// Token: 0x06001A60 RID: 6752 RVA: 0x00091F04 File Offset: 0x00090104
	public static void DebugDrawPath(PathFinder.Path path)
	{
		if (path.nodes != null)
		{
			for (int i = 0; i < path.nodes.Count - 1; i++)
			{
				NavGridUpdater.DebugDrawPath(path.nodes[i].cell, path.nodes[i + 1].cell);
			}
		}
	}

	// Token: 0x04000F47 RID: 3911
	public static int InvalidHandle = -1;

	// Token: 0x04000F48 RID: 3912
	public static int InvalidIdx = -1;

	// Token: 0x04000F49 RID: 3913
	public static int InvalidCell = -1;

	// Token: 0x0200132B RID: 4907
	private struct CreateLinkWorkItem : IWorkItem<object>
	{
		// Token: 0x060088DE RID: 35038 RVA: 0x0034A6EE File Offset: 0x003488EE
		public CreateLinkWorkItem(int start_cell, NavTable nav_table, int max_links_per_cell, NavGrid.Link[] links, NavGrid.Transition[][] transitions_by_nav_type, Dictionary<int, int> teleport_transitions)
		{
			this.startCell = start_cell;
			this.navTable = nav_table;
			this.maxLinksPerCell = max_links_per_cell;
			this.links = links;
			this.transitionsByNavType = transitions_by_nav_type;
			this.teleportTransitions = teleport_transitions;
		}

		// Token: 0x060088DF RID: 35039 RVA: 0x0034A720 File Offset: 0x00348920
		public void Run(object shared_data, int threadIndex)
		{
			for (int i = 0; i < Grid.WidthInCells; i++)
			{
				NavGridUpdater.CreateLinksForCell(this.startCell + i, this.navTable, this.maxLinksPerCell, this.links, this.transitionsByNavType, this.teleportTransitions);
			}
		}

		// Token: 0x0400689D RID: 26781
		private int startCell;

		// Token: 0x0400689E RID: 26782
		private NavTable navTable;

		// Token: 0x0400689F RID: 26783
		private int maxLinksPerCell;

		// Token: 0x040068A0 RID: 26784
		private NavGrid.Link[] links;

		// Token: 0x040068A1 RID: 26785
		private NavGrid.Transition[][] transitionsByNavType;

		// Token: 0x040068A2 RID: 26786
		private Dictionary<int, int> teleportTransitions;
	}

	// Token: 0x0200132C RID: 4908
	private struct MarkValidCellWorkItem : IWorkItem<object>
	{
		// Token: 0x060088E0 RID: 35040 RVA: 0x0034A768 File Offset: 0x00348968
		public MarkValidCellWorkItem(int start_cell, NavTable nav_table, CellOffset[] bounding_offsets, NavTableValidator[] validators)
		{
			this.startCell = start_cell;
			this.navTable = nav_table;
			this.boundingOffsets = bounding_offsets;
			this.validators = validators;
		}

		// Token: 0x060088E1 RID: 35041 RVA: 0x0034A788 File Offset: 0x00348988
		public void Run(object shared_data, int threadIndex)
		{
			for (int i = 0; i < Grid.WidthInCells; i++)
			{
				int num = this.startCell + i;
				NavTableValidator[] array = this.validators;
				for (int j = 0; j < array.Length; j++)
				{
					array[j].UpdateCell(num, this.navTable, this.boundingOffsets);
				}
			}
		}

		// Token: 0x040068A3 RID: 26787
		private NavTable navTable;

		// Token: 0x040068A4 RID: 26788
		private CellOffset[] boundingOffsets;

		// Token: 0x040068A5 RID: 26789
		private NavTableValidator[] validators;

		// Token: 0x040068A6 RID: 26790
		private int startCell;
	}
}
