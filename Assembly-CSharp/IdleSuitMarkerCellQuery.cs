using System;

// Token: 0x020004E1 RID: 1249
public class IdleSuitMarkerCellQuery : PathFinderQuery
{
	// Token: 0x06001ABF RID: 6847 RVA: 0x0009366E File Offset: 0x0009186E
	public IdleSuitMarkerCellQuery(bool is_rotated, int marker_x)
	{
		this.targetCell = Grid.InvalidCell;
		this.isRotated = is_rotated;
		this.markerX = marker_x;
	}

	// Token: 0x06001AC0 RID: 6848 RVA: 0x00093690 File Offset: 0x00091890
	public override bool IsMatch(int cell, int parent_cell, int cost)
	{
		if (!Grid.PreventIdleTraversal[cell] && Grid.CellToXY(cell).x < this.markerX != this.isRotated)
		{
			this.targetCell = cell;
		}
		return this.targetCell != Grid.InvalidCell;
	}

	// Token: 0x06001AC1 RID: 6849 RVA: 0x000936DC File Offset: 0x000918DC
	public override int GetResultCell()
	{
		return this.targetCell;
	}

	// Token: 0x04000F98 RID: 3992
	private int targetCell;

	// Token: 0x04000F99 RID: 3993
	private bool isRotated;

	// Token: 0x04000F9A RID: 3994
	private int markerX;
}
