using System;
using UnityEngine;

// Token: 0x02000A40 RID: 2624
public class OffsetTracker
{
	// Token: 0x06004C32 RID: 19506 RVA: 0x001BA4DC File Offset: 0x001B86DC
	public virtual CellOffset[] GetOffsets(int current_cell)
	{
		if (current_cell != this.previousCell)
		{
			global::Debug.Assert(!OffsetTracker.isExecutingWithinJob, "OffsetTracker.GetOffsets() is making a mutating call but is currently executing within a job");
			this.UpdateCell(this.previousCell, current_cell);
			this.previousCell = current_cell;
		}
		if (this.offsets == null)
		{
			global::Debug.Assert(!OffsetTracker.isExecutingWithinJob, "OffsetTracker.GetOffsets() is making a mutating call but is currently executing within a job");
			this.UpdateOffsets(this.previousCell);
		}
		return this.offsets;
	}

	// Token: 0x06004C33 RID: 19507 RVA: 0x001BA544 File Offset: 0x001B8744
	public virtual bool ValidateOffsets(int current_cell)
	{
		return current_cell == this.previousCell && this.offsets != null;
	}

	// Token: 0x06004C34 RID: 19508 RVA: 0x001BA55C File Offset: 0x001B875C
	public void ForceRefresh()
	{
		int num = this.previousCell;
		this.previousCell = Grid.InvalidCell;
		this.Refresh(num);
	}

	// Token: 0x06004C35 RID: 19509 RVA: 0x001BA582 File Offset: 0x001B8782
	public void Refresh(int cell)
	{
		this.GetOffsets(cell);
	}

	// Token: 0x06004C36 RID: 19510 RVA: 0x001BA58C File Offset: 0x001B878C
	protected virtual void UpdateCell(int previous_cell, int current_cell)
	{
	}

	// Token: 0x06004C37 RID: 19511 RVA: 0x001BA58E File Offset: 0x001B878E
	protected virtual void UpdateOffsets(int current_cell)
	{
	}

	// Token: 0x06004C38 RID: 19512 RVA: 0x001BA590 File Offset: 0x001B8790
	public virtual void Clear()
	{
	}

	// Token: 0x06004C39 RID: 19513 RVA: 0x001BA592 File Offset: 0x001B8792
	public virtual void DebugDrawExtents()
	{
	}

	// Token: 0x06004C3A RID: 19514 RVA: 0x001BA594 File Offset: 0x001B8794
	public virtual void DebugDrawEditor()
	{
	}

	// Token: 0x06004C3B RID: 19515 RVA: 0x001BA598 File Offset: 0x001B8798
	public virtual void DebugDrawOffsets(int cell)
	{
		foreach (CellOffset cellOffset in this.GetOffsets(cell))
		{
			int num = Grid.OffsetCell(cell, cellOffset);
			Gizmos.color = new Color(0f, 1f, 0f, 0.25f);
			Gizmos.DrawWireCube(Grid.CellToPosCCC(num, Grid.SceneLayer.Move), new Vector3(0.95f, 0.95f, 0.95f));
		}
	}

	// Token: 0x04003282 RID: 12930
	public static bool isExecutingWithinJob;

	// Token: 0x04003283 RID: 12931
	protected CellOffset[] offsets;

	// Token: 0x04003284 RID: 12932
	protected int previousCell = Grid.InvalidCell;
}
