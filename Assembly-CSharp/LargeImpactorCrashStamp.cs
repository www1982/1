using System;
using System.Collections.Generic;
using KSerialization;
using ProcGen;
using TemplateClasses;
using UnityEngine;

// Token: 0x02000B45 RID: 2885
public class LargeImpactorCrashStamp : KMonoBehaviour
{
	// Token: 0x17000638 RID: 1592
	// (get) Token: 0x060055C1 RID: 21953 RVA: 0x001F228D File Offset: 0x001F048D
	// (set) Token: 0x060055C0 RID: 21952 RVA: 0x001F2284 File Offset: 0x001F0484
	public TemplateContainer asteroidTemplate { get; private set; }

	// Token: 0x060055C2 RID: 21954 RVA: 0x001F2298 File Offset: 0x001F0498
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (DlcManager.IsExpansion1Active())
		{
			base.GetComponent<ClusterDestinationSelector>();
			ClusterMapLargeImpactor.Def def = this.GetDef<ClusterMapLargeImpactor.Def>();
			this.targetWorldId = def.destinationWorldID;
		}
		else
		{
			this.targetWorldId = ClusterManager.Instance.GetStartWorld().id;
		}
		this.InitializeData();
	}

	// Token: 0x060055C3 RID: 21955 RVA: 0x001F22EC File Offset: 0x001F04EC
	private void InitializeData()
	{
		this.asteroidTemplate = TemplateCache.GetTemplate(this.largeStampTemplate);
		if (this.stampLocation == Vector2I.zero)
		{
			this.stampLocation = this.FindIdealLocation(ClusterManager.Instance.GetWorld(this.targetWorldId), this.asteroidTemplate);
		}
		this.ConvertToCellIndices();
		this.SetVisualization();
	}

	// Token: 0x060055C4 RID: 21956 RVA: 0x001F234C File Offset: 0x001F054C
	private Vector2I FindIdealLocation(WorldContainer world, TemplateContainer template)
	{
		RectInt templateBounds = template.GetTemplateBounds(0);
		int num = world.WorldSize.y + world.WorldOffset.y - (templateBounds.height + templateBounds.yMin) - 1;
		Vector2I worldOffset = world.WorldOffset;
		int num2 = worldOffset.X + world.WorldSize.x / 2;
		using (List<Telepad>.Enumerator enumerator = Components.Telepads.Items.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Telepad telepad = enumerator.Current;
				if (telepad.GetMyWorldId() == world.id)
				{
					Vector2I vector2I = Grid.PosToXY(telepad.transform.position);
					vector2I.Y += templateBounds.height / 2 + 20;
					if (Grid.IsValidCell(Grid.XYToCell(vector2I.X, vector2I.Y)))
					{
						return vector2I;
					}
				}
			}
			goto IL_0173;
		}
		IL_00E5:
		foreach (Cell cell in template.cells)
		{
			if ((float)cell.location_y >= templateBounds.center.y)
			{
				int num3 = Grid.XYToCell(num2 + cell.location_x, num + cell.location_y);
				if (Grid.IsValidCell(num3) && global::World.Instance.zoneRenderData.GetSubWorldZoneType(num3) != SubWorld.ZoneType.Space)
				{
					return new Vector2I(num2, num - 50);
				}
			}
		}
		num--;
		IL_0173:
		if (num <= world.WorldOffset.y)
		{
			return new Vector2I(num2, world.WorldSize.y + world.WorldOffset.y - (templateBounds.yMax - templateBounds.yMin));
		}
		goto IL_00E5;
	}

	// Token: 0x060055C5 RID: 21957 RVA: 0x001F2528 File Offset: 0x001F0728
	private void ConvertToCellIndices()
	{
		this.asteroidCellIndices = new List<int>(this.asteroidTemplate.cells.Count);
		foreach (Cell cell in this.asteroidTemplate.cells)
		{
			CellOffset cellOffset;
			if (!this.TemplateBottomCellsOffsets.TryGetValue(cell.location_x, out cellOffset))
			{
				cellOffset = new CellOffset(cell.location_x, int.MaxValue);
			}
			if (cell.location_y < cellOffset.y)
			{
				cellOffset.y = cell.location_y;
			}
			this.TemplateBottomCellsOffsets[cell.location_x] = cellOffset;
			int num = Grid.XYToCell(this.stampLocation.X + cell.location_x, this.stampLocation.Y + cell.location_y);
			this.asteroidCellIndices.Add(num);
		}
	}

	// Token: 0x060055C6 RID: 21958 RVA: 0x001F2624 File Offset: 0x001F0824
	private bool IsCellOutsideOfImpactSite(int cell)
	{
		return !this.asteroidCellIndices.Contains(cell);
	}

	// Token: 0x060055C7 RID: 21959 RVA: 0x001F2638 File Offset: 0x001F0838
	public void RevealFogOfWar(int revealRadiusPerCell)
	{
		int num = Grid.XYToCell(this.stampLocation.x, this.stampLocation.y);
		RectInt templateBounds = this.asteroidTemplate.GetTemplateBounds(0);
		for (int i = templateBounds.xMin; i < templateBounds.xMax; i++)
		{
			for (int j = templateBounds.yMin; j < templateBounds.yMax; j++)
			{
				int num2 = Grid.OffsetCell(num, i, j);
				if (Grid.IsValidCell(num2))
				{
					Vector2I vector2I = Grid.CellToXY(num2);
					GridVisibility.Reveal(vector2I.x, vector2I.y, revealRadiusPerCell, 1f);
				}
			}
		}
	}

	// Token: 0x060055C8 RID: 21960 RVA: 0x001F26D4 File Offset: 0x001F08D4
	private void SetVisualization()
	{
		LargeImpactorVisualizer component = base.GetComponent<LargeImpactorVisualizer>();
		Vector2I vector2I = Grid.PosToXY(component.gameObject.transform.position);
		RectInt templateBounds = this.asteroidTemplate.GetTemplateBounds(0);
		component.RangeMin.x = templateBounds.xMin;
		component.RangeMin.y = templateBounds.yMin;
		component.RangeMax.x = templateBounds.xMax;
		component.RangeMax.y = templateBounds.yMax;
		component.TexSize = new Vector2I(templateBounds.size.x + 1, templateBounds.size.y + 1);
		component.OriginOffset = this.stampLocation - vector2I;
		component.BlockingCb = (int cell) => this.IsCellOutsideOfImpactSite(cell);
	}

	// Token: 0x04003946 RID: 14662
	public string largeStampTemplate;

	// Token: 0x04003948 RID: 14664
	[Serialize]
	public Vector2I stampLocation = Vector2I.zero;

	// Token: 0x04003949 RID: 14665
	public Dictionary<int, CellOffset> TemplateBottomCellsOffsets = new Dictionary<int, CellOffset>();

	// Token: 0x0400394A RID: 14666
	private List<int> asteroidCellIndices;

	// Token: 0x0400394B RID: 14667
	private int targetWorldId;
}
