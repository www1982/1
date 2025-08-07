using System;
using UnityEngine;

// Token: 0x020005E7 RID: 1511
[AddComponentMenu("KMonoBehaviour/scripts/NavPathDrawer")]
public class NavPathDrawer : KMonoBehaviour
{
	// Token: 0x17000170 RID: 368
	// (get) Token: 0x06002334 RID: 9012 RVA: 0x000CA133 File Offset: 0x000C8333
	// (set) Token: 0x06002335 RID: 9013 RVA: 0x000CA13A File Offset: 0x000C833A
	public static NavPathDrawer Instance { get; private set; }

	// Token: 0x06002336 RID: 9014 RVA: 0x000CA142 File Offset: 0x000C8342
	public static void DestroyInstance()
	{
		NavPathDrawer.Instance = null;
	}

	// Token: 0x06002337 RID: 9015 RVA: 0x000CA14C File Offset: 0x000C834C
	protected override void OnPrefabInit()
	{
		Shader shader = Shader.Find("Lines/Colored Blended");
		this.material = new Material(shader);
		NavPathDrawer.Instance = this;
	}

	// Token: 0x06002338 RID: 9016 RVA: 0x000CA176 File Offset: 0x000C8376
	protected override void OnCleanUp()
	{
		NavPathDrawer.Instance = null;
	}

	// Token: 0x06002339 RID: 9017 RVA: 0x000CA17E File Offset: 0x000C837E
	public void DrawPath(Vector3 navigator_pos, PathFinder.Path path)
	{
		this.navigatorPos = navigator_pos;
		this.navigatorPos.y = this.navigatorPos.y + 0.5f;
		this.path = path;
	}

	// Token: 0x0600233A RID: 9018 RVA: 0x000CA1AA File Offset: 0x000C83AA
	public Navigator GetNavigator()
	{
		return this.navigator;
	}

	// Token: 0x0600233B RID: 9019 RVA: 0x000CA1B2 File Offset: 0x000C83B2
	public void SetNavigator(Navigator navigator)
	{
		this.navigator = navigator;
	}

	// Token: 0x0600233C RID: 9020 RVA: 0x000CA1BB File Offset: 0x000C83BB
	public void ClearNavigator()
	{
		this.navigator = null;
	}

	// Token: 0x0600233D RID: 9021 RVA: 0x000CA1C4 File Offset: 0x000C83C4
	private void DrawPath(PathFinder.Path path, Vector3 navigator_pos, Color color)
	{
		if (path.nodes != null && path.nodes.Count > 1)
		{
			GL.PushMatrix();
			this.material.SetPass(0);
			GL.Begin(1);
			GL.Color(color);
			GL.Vertex(navigator_pos);
			GL.Vertex(NavTypeHelper.GetNavPos(path.nodes[1].cell, path.nodes[1].navType));
			for (int i = 1; i < path.nodes.Count - 1; i++)
			{
				if ((int)Grid.WorldIdx[path.nodes[i].cell] == ClusterManager.Instance.activeWorldId && (int)Grid.WorldIdx[path.nodes[i + 1].cell] == ClusterManager.Instance.activeWorldId)
				{
					Vector3 navPos = NavTypeHelper.GetNavPos(path.nodes[i].cell, path.nodes[i].navType);
					Vector3 navPos2 = NavTypeHelper.GetNavPos(path.nodes[i + 1].cell, path.nodes[i + 1].navType);
					GL.Vertex(navPos);
					GL.Vertex(navPos2);
				}
			}
			GL.End();
			GL.PopMatrix();
		}
	}

	// Token: 0x0600233E RID: 9022 RVA: 0x000CA310 File Offset: 0x000C8510
	private void OnPostRender()
	{
		this.DrawPath(this.path, this.navigatorPos, Color.white);
		this.path = default(PathFinder.Path);
		this.DebugDrawSelectedNavigator();
		if (this.navigator != null)
		{
			GL.PushMatrix();
			this.material.SetPass(0);
			GL.Begin(1);
			PathFinderQuery pathFinderQuery = PathFinderQueries.drawNavGridQuery.Reset(null);
			this.navigator.RunQuery(pathFinderQuery);
			GL.End();
			GL.PopMatrix();
		}
	}

	// Token: 0x0600233F RID: 9023 RVA: 0x000CA390 File Offset: 0x000C8590
	private void DebugDrawSelectedNavigator()
	{
		if (!DebugHandler.DebugPathFinding)
		{
			return;
		}
		if (SelectTool.Instance == null)
		{
			return;
		}
		if (SelectTool.Instance.selected == null)
		{
			return;
		}
		Navigator component = SelectTool.Instance.selected.GetComponent<Navigator>();
		if (component == null)
		{
			return;
		}
		int mouseCell = DebugHandler.GetMouseCell();
		if (Grid.IsValidCell(mouseCell))
		{
			PathFinder.PotentialPath potentialPath = new PathFinder.PotentialPath(Grid.PosToCell(component), component.CurrentNavType, component.flags);
			PathFinder.Path path = default(PathFinder.Path);
			PathFinder.UpdatePath(component.NavGrid, component.GetCurrentAbilities(), potentialPath, PathFinderQueries.cellQuery.Reset(mouseCell), ref path);
			string text = "";
			text = text + "Source: " + Grid.PosToCell(component).ToString() + "\n";
			text = text + "Dest: " + mouseCell.ToString() + "\n";
			text = text + "Cost: " + path.cost.ToString();
			this.DrawPath(path, component.GetComponent<KAnimControllerBase>().GetPivotSymbolPosition(), Color.green);
			DebugText.Instance.Draw(text, Grid.CellToPosCCC(mouseCell, Grid.SceneLayer.Move), Color.white);
		}
	}

	// Token: 0x0400146C RID: 5228
	private PathFinder.Path path;

	// Token: 0x0400146D RID: 5229
	public Material material;

	// Token: 0x0400146E RID: 5230
	private Vector3 navigatorPos;

	// Token: 0x0400146F RID: 5231
	private Navigator navigator;
}
