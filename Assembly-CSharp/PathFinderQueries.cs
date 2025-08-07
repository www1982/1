using System;

// Token: 0x020004D3 RID: 1235
public static class PathFinderQueries
{
	// Token: 0x06001A80 RID: 6784 RVA: 0x000929E0 File Offset: 0x00090BE0
	public static void Reset()
	{
		PathFinderQueries.cellQuery = new CellQuery();
		PathFinderQueries.cellCostQuery = new CellCostQuery();
		PathFinderQueries.cellArrayQuery = new CellArrayQuery();
		PathFinderQueries.cellOffsetQuery = new CellOffsetQuery();
		PathFinderQueries.safeCellQuery = new SafeCellQuery();
		PathFinderQueries.idleCellQuery = new IdleCellQuery();
		PathFinderQueries.drawNavGridQuery = new DrawNavGridQuery();
		PathFinderQueries.plantableCellQuery = new PlantableCellQuery();
		PathFinderQueries.mineableCellQuery = new MineableCellQuery();
		PathFinderQueries.staterpillarCellQuery = new StaterpillarCellQuery();
		PathFinderQueries.floorCellQuery = new FloorCellQuery();
		PathFinderQueries.buildingPlacementQuery = new BuildingPlacementQuery();
	}

	// Token: 0x04000F53 RID: 3923
	public static CellQuery cellQuery = new CellQuery();

	// Token: 0x04000F54 RID: 3924
	public static CellCostQuery cellCostQuery = new CellCostQuery();

	// Token: 0x04000F55 RID: 3925
	public static CellArrayQuery cellArrayQuery = new CellArrayQuery();

	// Token: 0x04000F56 RID: 3926
	public static CellOffsetQuery cellOffsetQuery = new CellOffsetQuery();

	// Token: 0x04000F57 RID: 3927
	public static SafeCellQuery safeCellQuery = new SafeCellQuery();

	// Token: 0x04000F58 RID: 3928
	public static IdleCellQuery idleCellQuery = new IdleCellQuery();

	// Token: 0x04000F59 RID: 3929
	public static DrawNavGridQuery drawNavGridQuery = new DrawNavGridQuery();

	// Token: 0x04000F5A RID: 3930
	public static PlantableCellQuery plantableCellQuery = new PlantableCellQuery();

	// Token: 0x04000F5B RID: 3931
	public static MineableCellQuery mineableCellQuery = new MineableCellQuery();

	// Token: 0x04000F5C RID: 3932
	public static StaterpillarCellQuery staterpillarCellQuery = new StaterpillarCellQuery();

	// Token: 0x04000F5D RID: 3933
	public static FloorCellQuery floorCellQuery = new FloorCellQuery();

	// Token: 0x04000F5E RID: 3934
	public static BuildingPlacementQuery buildingPlacementQuery = new BuildingPlacementQuery();
}
