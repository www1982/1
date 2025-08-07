using System;
using UnityEngine;

// Token: 0x020005E6 RID: 1510
[AddComponentMenu("KMonoBehaviour/scripts/AntiCluster")]
public class MoverLayerOccupier : KMonoBehaviour, ISim200ms
{
	// Token: 0x0600232D RID: 9005 RVA: 0x000C9F3C File Offset: 0x000C813C
	private void RefreshCellOccupy()
	{
		int num = Grid.PosToCell(this);
		foreach (CellOffset cellOffset in this.cellOffsets)
		{
			int num2 = Grid.OffsetCell(num, cellOffset);
			if (this.previousCell != Grid.InvalidCell)
			{
				int num3 = Grid.OffsetCell(this.previousCell, cellOffset);
				this.UpdateCell(num3, num2);
			}
			else
			{
				this.UpdateCell(this.previousCell, num2);
			}
		}
		this.previousCell = num;
	}

	// Token: 0x0600232E RID: 9006 RVA: 0x000C9FB2 File Offset: 0x000C81B2
	public void Sim200ms(float dt)
	{
		this.RefreshCellOccupy();
	}

	// Token: 0x0600232F RID: 9007 RVA: 0x000C9FBC File Offset: 0x000C81BC
	private void UpdateCell(int previous_cell, int current_cell)
	{
		foreach (ObjectLayer objectLayer in this.objectLayers)
		{
			if (previous_cell != Grid.InvalidCell && previous_cell != current_cell && Grid.Objects[previous_cell, (int)objectLayer] == base.gameObject)
			{
				Grid.Objects[previous_cell, (int)objectLayer] = null;
			}
			GameObject gameObject = Grid.Objects[current_cell, (int)objectLayer];
			if (gameObject == null)
			{
				Grid.Objects[current_cell, (int)objectLayer] = base.gameObject;
			}
			else
			{
				KPrefabID component = base.GetComponent<KPrefabID>();
				KPrefabID component2 = gameObject.GetComponent<KPrefabID>();
				if (component.InstanceID > component2.InstanceID)
				{
					Grid.Objects[current_cell, (int)objectLayer] = base.gameObject;
				}
			}
		}
	}

	// Token: 0x06002330 RID: 9008 RVA: 0x000CA074 File Offset: 0x000C8274
	private void CleanUpOccupiedCells()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		foreach (CellOffset cellOffset in this.cellOffsets)
		{
			int num2 = Grid.OffsetCell(num, cellOffset);
			foreach (ObjectLayer objectLayer in this.objectLayers)
			{
				if (Grid.Objects[num2, (int)objectLayer] == base.gameObject)
				{
					Grid.Objects[num2, (int)objectLayer] = null;
				}
			}
		}
	}

	// Token: 0x06002331 RID: 9009 RVA: 0x000CA104 File Offset: 0x000C8304
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.RefreshCellOccupy();
	}

	// Token: 0x06002332 RID: 9010 RVA: 0x000CA112 File Offset: 0x000C8312
	protected override void OnCleanUp()
	{
		this.CleanUpOccupiedCells();
		base.OnCleanUp();
	}

	// Token: 0x04001469 RID: 5225
	private int previousCell = Grid.InvalidCell;

	// Token: 0x0400146A RID: 5226
	public ObjectLayer[] objectLayers;

	// Token: 0x0400146B RID: 5227
	public CellOffset[] cellOffsets;
}
