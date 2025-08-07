using System;

// Token: 0x020004CF RID: 1231
public class NavTable
{
	// Token: 0x06001A66 RID: 6758 RVA: 0x00091F84 File Offset: 0x00090184
	public NavTable(int cell_count)
	{
		this.ValidCells = new short[cell_count];
		this.NavTypeMasks = new short[11];
		for (short num = 0; num < 11; num += 1)
		{
			this.NavTypeMasks[(int)num] = (short)(1 << (int)num);
		}
	}

	// Token: 0x06001A67 RID: 6759 RVA: 0x00091FCD File Offset: 0x000901CD
	public bool IsValid(int cell, NavType nav_type = NavType.Floor)
	{
		return Grid.IsValidCell(cell) && (this.NavTypeMasks[(int)nav_type] & this.ValidCells[cell]) != 0;
	}

	// Token: 0x06001A68 RID: 6760 RVA: 0x00091FF0 File Offset: 0x000901F0
	public void SetValid(int cell, NavType nav_type, bool is_valid)
	{
		short num = this.NavTypeMasks[(int)nav_type];
		short num2 = this.ValidCells[cell];
		if ((num2 & num) != 0 != is_valid)
		{
			if (is_valid)
			{
				this.ValidCells[cell] = num | num2;
			}
			else
			{
				this.ValidCells[cell] = ~num & num2;
			}
			if (this.OnValidCellChanged != null)
			{
				this.OnValidCellChanged(cell, nav_type);
			}
		}
	}

	// Token: 0x04000F4A RID: 3914
	public Action<int, NavType> OnValidCellChanged;

	// Token: 0x04000F4B RID: 3915
	private short[] NavTypeMasks;

	// Token: 0x04000F4C RID: 3916
	private short[] ValidCells;
}
