using System;

// Token: 0x020004DC RID: 1244
public class CellOffsetQuery : CellArrayQuery
{
	// Token: 0x06001AAF RID: 6831 RVA: 0x0009340C File Offset: 0x0009160C
	public CellArrayQuery Reset(int cell, CellOffset[] offsets)
	{
		int[] array = new int[offsets.Length];
		for (int i = 0; i < offsets.Length; i++)
		{
			array[i] = Grid.OffsetCell(cell, offsets[i]);
		}
		base.Reset(array);
		return this;
	}
}
