using System;
using System.Linq;
using ImGuiNET;
using UnityEngine;

// Token: 0x02000676 RID: 1654
public class DevToolNavGrid : DevTool
{
	// Token: 0x06002890 RID: 10384 RVA: 0x000E8B3A File Offset: 0x000E6D3A
	public DevToolNavGrid()
	{
		DevToolNavGrid.Instance = this;
	}

	// Token: 0x06002891 RID: 10385 RVA: 0x000E8B48 File Offset: 0x000E6D48
	private bool Init()
	{
		if (Pathfinding.Instance == null)
		{
			return false;
		}
		if (this.navGridNames != null)
		{
			return true;
		}
		this.navGridNames = (from x in Pathfinding.Instance.GetNavGrids()
			select x.id).ToArray<string>();
		return true;
	}

	// Token: 0x06002892 RID: 10386 RVA: 0x000E8BA8 File Offset: 0x000E6DA8
	protected override void RenderTo(DevPanel panel)
	{
		if (this.Init())
		{
			this.Contents();
			return;
		}
		ImGui.Text("Game not initialized");
	}

	// Token: 0x06002893 RID: 10387 RVA: 0x000E8BC3 File Offset: 0x000E6DC3
	public void SetCell(int cell)
	{
		this.selectedCell = cell;
	}

	// Token: 0x06002894 RID: 10388 RVA: 0x000E8BCC File Offset: 0x000E6DCC
	private void Contents()
	{
		ImGui.Combo("Nav Grid ID", ref this.selectedNavGrid, this.navGridNames, this.navGridNames.Length);
		NavGrid navGrid = Pathfinding.Instance.GetNavGrid(this.navGridNames[this.selectedNavGrid]);
		ImGui.Text("Max Links per cell: " + navGrid.maxLinksPerCell.ToString());
		ImGui.Spacing();
		if (ImGui.Button("Calculate Stats"))
		{
			this.linkStats = new int[navGrid.maxLinksPerCell];
			this.highestLinkCell = 0;
			this.highestLinkCount = 0;
			for (int i = 0; i < Grid.CellCount; i++)
			{
				int num = 0;
				for (int j = 0; j < navGrid.maxLinksPerCell; j++)
				{
					int num2 = i * navGrid.maxLinksPerCell + j;
					if (navGrid.Links[num2].link == Grid.InvalidCell)
					{
						break;
					}
					num++;
				}
				if (num > this.highestLinkCount)
				{
					this.highestLinkCell = i;
					this.highestLinkCount = num;
				}
				this.linkStats[num]++;
			}
		}
		ImGui.SameLine();
		if (ImGui.Button("Clear"))
		{
			this.linkStats = null;
		}
		ImGui.SameLine();
		if (ImGui.Button("Rescan"))
		{
			navGrid.InitializeGraph();
		}
		if (this.linkStats != null)
		{
			ImGui.Text("Highest link count: " + this.highestLinkCount.ToString());
			ImGui.Text(string.Format("Utilized percentage: {0} %", (float)this.highestLinkCount / (float)navGrid.maxLinksPerCell * 100f));
			ImGui.SameLine();
			if (ImGui.Button(string.Format("Select {0}", this.highestLinkCell)))
			{
				this.selectedCell = this.highestLinkCell;
			}
			for (int k = 0; k < this.linkStats.Length; k++)
			{
				if (this.linkStats[k] > 0)
				{
					ImGui.Text(string.Format("\t{0}: {1}", k, this.linkStats[k]));
				}
			}
		}
		ImGui.Checkbox("DrawDebugPath", ref DebugHandler.DebugPathFinding);
		if (Camera.main != null && SelectTool.Instance != null)
		{
			GameObject gameObject = null;
			ImGui.Checkbox("Lock", ref this.follow);
			if (this.follow)
			{
				if (this.lockObject == null && SelectTool.Instance.selected != null)
				{
					this.lockObject = SelectTool.Instance.selected.gameObject;
				}
				gameObject = this.lockObject;
			}
			else if (SelectTool.Instance.selected != null)
			{
				gameObject = SelectTool.Instance.selected.gameObject;
				this.lockObject = null;
			}
			if (gameObject != null)
			{
				Navigator component = gameObject.GetComponent<Navigator>();
				if (component != null)
				{
					Vector2 positionFor = DevToolEntity.GetPositionFor(component.gameObject);
					ImGui.GetBackgroundDrawList().AddCircleFilled(positionFor, 10f, ImGui.GetColorU32(Color.green));
					Vector2 screenPosition = DevToolEntity.GetScreenPosition(component.GetComponent<KBatchedAnimController>().GetPivotSymbolPosition());
					ImGui.GetBackgroundDrawList().AddCircleFilled(screenPosition, 10f, ImGui.GetColorU32(Color.blue));
					TransitionDriver transitionDriver = component.transitionDriver;
					if (transitionDriver.GetTransition != null)
					{
						Vector3 position = component.transform.GetPosition();
						Vector2 vector = gameObject.GetComponent<KBoxCollider2D>().size / 2f;
						if (transitionDriver.GetTransition.x > 0)
						{
							position.x += vector.x;
						}
						else if (transitionDriver.GetTransition.x < 0)
						{
							position.x -= vector.x;
						}
						Vector2 screenPosition2 = DevToolEntity.GetScreenPosition(position);
						ImGui.GetBackgroundDrawList().AddCircleFilled(screenPosition2, 10f, ImGui.GetColorU32(Color.magenta));
					}
				}
			}
		}
		ImGui.Spacing();
		ImGui.Checkbox("Draw Links", ref this.drawLinks);
		if (this.drawLinks)
		{
			this.DebugDrawLinks(navGrid);
		}
		ImGui.Spacing();
		int num3;
		int num4;
		Grid.CellToXY(this.selectedCell, out num3, out num4);
		ImGui.Text(string.Format("Selected Cell: {0} ({1},{2})", this.selectedCell, num3, num4));
		if (Grid.IsValidCell(this.selectedCell) && navGrid.Links != null && navGrid.Links.Length > navGrid.maxLinksPerCell * this.selectedCell)
		{
			for (int l = 0; l < navGrid.maxLinksPerCell; l++)
			{
				int num5 = this.selectedCell * navGrid.maxLinksPerCell + l;
				NavGrid.Link link = navGrid.Links[num5];
				if (link.link == Grid.InvalidCell)
				{
					break;
				}
				this.DrawLink(l, link, navGrid);
			}
		}
	}

	// Token: 0x06002895 RID: 10389 RVA: 0x000E90A0 File Offset: 0x000E72A0
	private void DrawLink(int idx, NavGrid.Link l, NavGrid navGrid)
	{
		NavGrid.Transition transition = navGrid.transitions[(int)l.transitionId];
		ImGui.Text(string.Format("   {0} -> {1} x:{2} y:{3} anim:{4} cost:{5}", new object[] { transition.start, transition.end, transition.x, transition.y, transition.anim, transition.cost }));
	}

	// Token: 0x06002896 RID: 10390 RVA: 0x000E9124 File Offset: 0x000E7324
	private void DebugDrawLinks(NavGrid navGrid)
	{
		if (Camera.main == null)
		{
			return;
		}
		Camera main = Camera.main;
		int pixelHeight = main.pixelHeight;
		Color white = Color.white;
		for (int i = 0; i < Grid.CellCount; i++)
		{
			int num = i * navGrid.maxLinksPerCell;
			for (int num2 = navGrid.Links[num].link; num2 != NavGrid.InvalidCell; num2 = navGrid.Links[num].link)
			{
				if (this.DrawNavTypeLink(navGrid, num, ref white))
				{
					Vector3 navPos = NavTypeHelper.GetNavPos(i, navGrid.Links[num].startNavType);
					Vector3 navPos2 = NavTypeHelper.GetNavPos(num2, navGrid.Links[num].endNavType);
					if (this.IsInCameraView(main, navPos) && this.IsInCameraView(main, navPos2))
					{
						Vector2 vector = main.WorldToScreenPoint(navPos);
						Vector2 vector2 = main.WorldToScreenPoint(navPos2);
						vector.y = (float)pixelHeight - vector.y;
						vector2.y = (float)pixelHeight - vector2.y;
						uint colorU = ImGui.GetColorU32(white);
						this.DrawArrowLink(vector, vector2, colorU);
					}
				}
				num++;
			}
		}
	}

	// Token: 0x06002897 RID: 10391 RVA: 0x000E9268 File Offset: 0x000E7468
	private bool IsInCameraView(Camera camera, Vector3 pos)
	{
		Vector3 vector = camera.WorldToViewportPoint(pos);
		return vector.x >= 0f && vector.y >= 0f && vector.x <= 1f && vector.y <= 1f;
	}

	// Token: 0x06002898 RID: 10392 RVA: 0x000E92B8 File Offset: 0x000E74B8
	private bool DrawNavTypeLink(NavGrid navGrid, int end_cell_idx, ref Color color)
	{
		for (int i = 0; i < navGrid.ValidNavTypes.Length; i++)
		{
			if (navGrid.ValidNavTypes[i] == navGrid.Links[end_cell_idx].startNavType)
			{
				color = navGrid.NavTypeColor(navGrid.Links[end_cell_idx].startNavType);
				return true;
			}
			if (navGrid.ValidNavTypes[i] == navGrid.Links[end_cell_idx].endNavType)
			{
				color = navGrid.NavTypeColor(navGrid.Links[end_cell_idx].endNavType);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002899 RID: 10393 RVA: 0x000E9350 File Offset: 0x000E7550
	private void DrawArrowLink(Vector2 start, Vector2 end, uint color)
	{
		ImDrawListPtr backgroundDrawList = ImGui.GetBackgroundDrawList();
		Vector2 vector = end - start;
		float magnitude = vector.magnitude;
		if (magnitude > 0f)
		{
			vector *= 1f / Mathf.Sqrt(magnitude);
		}
		Vector2 vector2 = end - vector * 1f + new Vector2(-vector.y, vector.x) * 1f;
		Vector2 vector3 = end - vector * 1f - new Vector2(-vector.y, vector.x) * 1f;
		backgroundDrawList.AddLine(start, end, color);
		backgroundDrawList.AddTriangleFilled(end, vector2, vector3, color);
	}

	// Token: 0x040017E1 RID: 6113
	private const string INVALID_OVERLAY_MODE_STR = "None";

	// Token: 0x040017E2 RID: 6114
	private string[] navGridNames;

	// Token: 0x040017E3 RID: 6115
	private int selectedNavGrid;

	// Token: 0x040017E4 RID: 6116
	private bool drawLinks;

	// Token: 0x040017E5 RID: 6117
	public static DevToolNavGrid Instance;

	// Token: 0x040017E6 RID: 6118
	private int[] linkStats;

	// Token: 0x040017E7 RID: 6119
	private int highestLinkCell;

	// Token: 0x040017E8 RID: 6120
	private int highestLinkCount;

	// Token: 0x040017E9 RID: 6121
	private int selectedCell;

	// Token: 0x040017EA RID: 6122
	private bool follow;

	// Token: 0x040017EB RID: 6123
	private GameObject lockObject;
}
