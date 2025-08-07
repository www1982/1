using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

// Token: 0x02000A3B RID: 2619
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/OccupyArea")]
public class OccupyArea : KMonoBehaviour
{
	// Token: 0x17000535 RID: 1333
	// (get) Token: 0x06004C02 RID: 19458 RVA: 0x001B80C1 File Offset: 0x001B62C1
	public CellOffset[] OccupiedCellsOffsets
	{
		get
		{
			this.UpdateRotatedCells();
			return this._RotatedOccupiedCellsOffsets;
		}
	}

	// Token: 0x17000536 RID: 1334
	// (get) Token: 0x06004C03 RID: 19459 RVA: 0x001B80CF File Offset: 0x001B62CF
	// (set) Token: 0x06004C04 RID: 19460 RVA: 0x001B80D7 File Offset: 0x001B62D7
	public bool ApplyToCells
	{
		get
		{
			return this.applyToCells;
		}
		set
		{
			if (value != this.applyToCells)
			{
				if (value)
				{
					this.UpdateOccupiedArea();
				}
				else
				{
					this.ClearOccupiedArea();
				}
				this.applyToCells = value;
			}
		}
	}

	// Token: 0x06004C05 RID: 19461 RVA: 0x001B80FA File Offset: 0x001B62FA
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.facing != null)
		{
			this.facingLeft = this.facing.facingLeft;
		}
		if (this.applyToCells)
		{
			this.UpdateOccupiedArea();
		}
	}

	// Token: 0x06004C06 RID: 19462 RVA: 0x001B812F File Offset: 0x001B632F
	private void ValidatePosition()
	{
		if (!Grid.IsValidCell(Grid.PosToCell(this)))
		{
			global::Debug.LogWarning(base.name + " is outside the grid! DELETING!");
			Util.KDestroyGameObject(base.gameObject);
		}
	}

	// Token: 0x06004C07 RID: 19463 RVA: 0x001B815E File Offset: 0x001B635E
	[OnSerializing]
	private void OnSerializing()
	{
		this.ValidatePosition();
	}

	// Token: 0x06004C08 RID: 19464 RVA: 0x001B8166 File Offset: 0x001B6366
	[OnDeserialized]
	private void OnDeserialized()
	{
		this.ValidatePosition();
	}

	// Token: 0x06004C09 RID: 19465 RVA: 0x001B8170 File Offset: 0x001B6370
	public int GetOffsetCellWithRotation(CellOffset cellOffset)
	{
		CellOffset cellOffset2 = cellOffset;
		if (this.rotatable != null)
		{
			cellOffset2 = this.rotatable.GetRotatedCellOffset(cellOffset);
		}
		return Grid.OffsetCell(Grid.PosToCell(base.gameObject), cellOffset2);
	}

	// Token: 0x06004C0A RID: 19466 RVA: 0x001B81AB File Offset: 0x001B63AB
	public void SetCellOffsets(CellOffset[] cells)
	{
		this._UnrotatedOccupiedCellsOffsets = cells;
		this._RotatedOccupiedCellsOffsets = cells;
		this.UpdateRotatedCells();
	}

	// Token: 0x06004C0B RID: 19467 RVA: 0x001B81C4 File Offset: 0x001B63C4
	private void UpdateRotatedCells()
	{
		if (this.rotatable != null && this.appliedOrientation != this.rotatable.Orientation)
		{
			this._RotatedOccupiedCellsOffsets = new CellOffset[this._UnrotatedOccupiedCellsOffsets.Length];
			for (int i = 0; i < this._UnrotatedOccupiedCellsOffsets.Length; i++)
			{
				CellOffset cellOffset = this._UnrotatedOccupiedCellsOffsets[i];
				this._RotatedOccupiedCellsOffsets[i] = this.rotatable.GetRotatedCellOffset(cellOffset);
			}
			this.appliedOrientation = this.rotatable.Orientation;
			return;
		}
		if (this.facing != null && this.facingLeft != this.facing.facingLeft)
		{
			this.facingLeft = this.facing.facingLeft;
			this._RotatedOccupiedCellsOffsets = new CellOffset[this._UnrotatedOccupiedCellsOffsets.Length];
			for (int j = 0; j < this._UnrotatedOccupiedCellsOffsets.Length; j++)
			{
				CellOffset cellOffset2 = this._UnrotatedOccupiedCellsOffsets[j];
				cellOffset2.x *= ((!this.facingLeft) ? (-1) : 1);
				this._RotatedOccupiedCellsOffsets[j] = cellOffset2;
			}
		}
	}

	// Token: 0x06004C0C RID: 19468 RVA: 0x001B82D8 File Offset: 0x001B64D8
	public bool CheckIsOccupying(int checkCell)
	{
		int num = Grid.PosToCell(base.gameObject);
		if (checkCell == num)
		{
			return true;
		}
		foreach (CellOffset cellOffset in this.OccupiedCellsOffsets)
		{
			if (Grid.OffsetCell(num, cellOffset) == checkCell)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004C0D RID: 19469 RVA: 0x001B8321 File Offset: 0x001B6521
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		this.ClearOccupiedArea();
	}

	// Token: 0x06004C0E RID: 19470 RVA: 0x001B8330 File Offset: 0x001B6530
	private void ClearOccupiedArea()
	{
		if (this.occupiedGridCells == null)
		{
			return;
		}
		foreach (ObjectLayer objectLayer in this.objectLayers)
		{
			if (objectLayer != ObjectLayer.NumLayers)
			{
				foreach (int num in this.occupiedGridCells)
				{
					if (Grid.Objects[num, (int)objectLayer] == base.gameObject)
					{
						Grid.Objects[num, (int)objectLayer] = null;
					}
				}
			}
		}
	}

	// Token: 0x06004C0F RID: 19471 RVA: 0x001B83AC File Offset: 0x001B65AC
	public void UpdateOccupiedArea()
	{
		if (this.objectLayers.Length == 0)
		{
			return;
		}
		if (this.occupiedGridCells == null)
		{
			this.occupiedGridCells = new int[this.OccupiedCellsOffsets.Length];
		}
		this.ClearOccupiedArea();
		int num = Grid.PosToCell(base.gameObject);
		foreach (ObjectLayer objectLayer in this.objectLayers)
		{
			if (objectLayer != ObjectLayer.NumLayers)
			{
				for (int j = 0; j < this.OccupiedCellsOffsets.Length; j++)
				{
					CellOffset cellOffset = this.OccupiedCellsOffsets[j];
					int num2 = Grid.OffsetCell(num, cellOffset);
					Grid.Objects[num2, (int)objectLayer] = base.gameObject;
					this.occupiedGridCells[j] = num2;
				}
			}
		}
	}

	// Token: 0x06004C10 RID: 19472 RVA: 0x001B845C File Offset: 0x001B665C
	public int GetWidthInCells()
	{
		int num = int.MaxValue;
		int num2 = int.MinValue;
		foreach (CellOffset cellOffset in this.OccupiedCellsOffsets)
		{
			num = Math.Min(num, cellOffset.x);
			num2 = Math.Max(num2, cellOffset.x);
		}
		return num2 - num + 1;
	}

	// Token: 0x06004C11 RID: 19473 RVA: 0x001B84B4 File Offset: 0x001B66B4
	public int GetHeightInCells()
	{
		int num = int.MaxValue;
		int num2 = int.MinValue;
		foreach (CellOffset cellOffset in this.OccupiedCellsOffsets)
		{
			num = Math.Min(num, cellOffset.y);
			num2 = Math.Max(num2, cellOffset.y);
		}
		return num2 - num + 1;
	}

	// Token: 0x06004C12 RID: 19474 RVA: 0x001B850C File Offset: 0x001B670C
	public Extents GetExtents()
	{
		return new Extents(Grid.PosToCell(base.gameObject), this.OccupiedCellsOffsets);
	}

	// Token: 0x06004C13 RID: 19475 RVA: 0x001B8524 File Offset: 0x001B6724
	public Extents GetExtents(Orientation orientation)
	{
		return new Extents(Grid.PosToCell(base.gameObject), this.OccupiedCellsOffsets, orientation);
	}

	// Token: 0x06004C14 RID: 19476 RVA: 0x001B8540 File Offset: 0x001B6740
	private void OnDrawGizmosSelected()
	{
		int num = Grid.PosToCell(base.gameObject);
		if (this.OccupiedCellsOffsets != null)
		{
			foreach (CellOffset cellOffset in this.OccupiedCellsOffsets)
			{
				Gizmos.color = Color.cyan;
				Gizmos.DrawWireCube(Grid.CellToPos(Grid.OffsetCell(num, cellOffset)) + Vector3.right / 2f + Vector3.up / 2f, Vector3.one);
			}
		}
		if (this.AboveOccupiedCellOffsets != null)
		{
			foreach (CellOffset cellOffset2 in this.AboveOccupiedCellOffsets)
			{
				Gizmos.color = Color.blue;
				Gizmos.DrawWireCube(Grid.CellToPos(Grid.OffsetCell(num, cellOffset2)) + Vector3.right / 2f + Vector3.up / 2f, Vector3.one * 0.9f);
			}
		}
		if (this.BelowOccupiedCellOffsets != null)
		{
			foreach (CellOffset cellOffset3 in this.BelowOccupiedCellOffsets)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawWireCube(Grid.CellToPos(Grid.OffsetCell(num, cellOffset3)) + Vector3.right / 2f + Vector3.up / 2f, Vector3.one * 0.9f);
			}
		}
	}

	// Token: 0x06004C15 RID: 19477 RVA: 0x001B86B8 File Offset: 0x001B68B8
	public bool CanOccupyArea(int rootCell, ObjectLayer layer)
	{
		for (int i = 0; i < this.OccupiedCellsOffsets.Length; i++)
		{
			CellOffset cellOffset = this.OccupiedCellsOffsets[i];
			int num = Grid.OffsetCell(rootCell, cellOffset);
			if (Grid.Objects[num, (int)layer] != null)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06004C16 RID: 19478 RVA: 0x001B8704 File Offset: 0x001B6904
	public bool TestArea(int rootCell, object data, Func<int, object, bool> testDelegate)
	{
		for (int i = 0; i < this.OccupiedCellsOffsets.Length; i++)
		{
			CellOffset cellOffset = this.OccupiedCellsOffsets[i];
			int num = Grid.OffsetCell(rootCell, cellOffset);
			if (!testDelegate(num, data))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06004C17 RID: 19479 RVA: 0x001B8748 File Offset: 0x001B6948
	public bool TestAreaAbove(int rootCell, object data, Func<int, object, bool> testDelegate)
	{
		if (this.AboveOccupiedCellOffsets == null)
		{
			List<CellOffset> list = new List<CellOffset>();
			for (int i = 0; i < this.OccupiedCellsOffsets.Length; i++)
			{
				CellOffset cellOffset = new CellOffset(this.OccupiedCellsOffsets[i].x, this.OccupiedCellsOffsets[i].y + 1);
				if (Array.IndexOf<CellOffset>(this.OccupiedCellsOffsets, cellOffset) == -1)
				{
					list.Add(cellOffset);
				}
			}
			this.AboveOccupiedCellOffsets = list.ToArray();
		}
		for (int j = 0; j < this.AboveOccupiedCellOffsets.Length; j++)
		{
			int num = Grid.OffsetCell(rootCell, this.AboveOccupiedCellOffsets[j]);
			if (!testDelegate(num, data))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06004C18 RID: 19480 RVA: 0x001B87F8 File Offset: 0x001B69F8
	public bool TestAreaBelow(int rootCell, object data, Func<int, object, bool> testDelegate)
	{
		if (this.BelowOccupiedCellOffsets == null)
		{
			List<CellOffset> list = new List<CellOffset>();
			for (int i = 0; i < this.OccupiedCellsOffsets.Length; i++)
			{
				CellOffset cellOffset = new CellOffset(this.OccupiedCellsOffsets[i].x, this.OccupiedCellsOffsets[i].y - 1);
				if (Array.IndexOf<CellOffset>(this.OccupiedCellsOffsets, cellOffset) == -1)
				{
					list.Add(cellOffset);
				}
			}
			this.BelowOccupiedCellOffsets = list.ToArray();
		}
		for (int j = 0; j < this.BelowOccupiedCellOffsets.Length; j++)
		{
			int num = Grid.OffsetCell(rootCell, this.BelowOccupiedCellOffsets[j]);
			if (!testDelegate(num, data))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x04003260 RID: 12896
	private CellOffset[] AboveOccupiedCellOffsets;

	// Token: 0x04003261 RID: 12897
	private CellOffset[] BelowOccupiedCellOffsets;

	// Token: 0x04003262 RID: 12898
	private int[] occupiedGridCells;

	// Token: 0x04003263 RID: 12899
	[MyCmpGet]
	private Rotatable rotatable;

	// Token: 0x04003264 RID: 12900
	private Orientation appliedOrientation;

	// Token: 0x04003265 RID: 12901
	[MyCmpGet]
	private Facing facing;

	// Token: 0x04003266 RID: 12902
	private bool facingLeft;

	// Token: 0x04003267 RID: 12903
	public CellOffset[] _UnrotatedOccupiedCellsOffsets;

	// Token: 0x04003268 RID: 12904
	public CellOffset[] _RotatedOccupiedCellsOffsets;

	// Token: 0x04003269 RID: 12905
	public ObjectLayer[] objectLayers = new ObjectLayer[0];

	// Token: 0x0400326A RID: 12906
	[SerializeField]
	private bool applyToCells = true;
}
