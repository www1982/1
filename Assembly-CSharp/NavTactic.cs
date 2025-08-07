using System;
using UnityEngine;

// Token: 0x020005EA RID: 1514
public class NavTactic
{
	// Token: 0x06002349 RID: 9033 RVA: 0x000CA5EA File Offset: 0x000C87EA
	public NavTactic(int preferredRange, int rangePenalty = 1, int overlapPenalty = 1, int pathCostPenalty = 1)
	{
		this._overlapPenalty = overlapPenalty;
		this._preferredRange = preferredRange;
		this._rangePenalty = rangePenalty;
		this._pathCostPenalty = pathCostPenalty;
	}

	// Token: 0x0600234A RID: 9034 RVA: 0x000CA624 File Offset: 0x000C8824
	public NavTactic(int preferredRange, int rangePenalty, int overlapPenalty, int pathCostPenalty, int xPenalty, int preferredX, int yPenalty, int preferredY)
	{
		this._overlapPenalty = overlapPenalty;
		this._preferredRange = preferredRange;
		this._rangePenalty = rangePenalty;
		this._pathCostPenalty = pathCostPenalty;
		this._pathXCostPenalty = xPenalty;
		this._preferredX = preferredX;
		this._pathYCostPenalty = yPenalty;
		this._preferredY = preferredY;
	}

	// Token: 0x0600234B RID: 9035 RVA: 0x000CA68C File Offset: 0x000C888C
	public int GetCellPreferences(int root, CellOffset[] offsets, Navigator navigator)
	{
		int num = NavigationReservations.InvalidReservation;
		int num2 = int.MaxValue;
		for (int i = 0; i < offsets.Length; i++)
		{
			int num3 = Grid.OffsetCell(root, offsets[i]);
			int num4 = 0;
			num4 += this._overlapPenalty * NavigationReservations.Instance.GetOccupancyCount(num3);
			num4 += this._rangePenalty * Mathf.Abs(this._preferredRange - Grid.GetCellDistance(root, num3));
			num4 += this._pathCostPenalty * Mathf.Max(navigator.GetNavigationCost(num3), 0);
			num4 += this._pathXCostPenalty * Mathf.Abs(this._preferredX - Mathf.Abs(Grid.CellColumn(root) - Grid.CellColumn(num3)));
			num4 += this._pathYCostPenalty * Mathf.Abs(this._preferredY - Mathf.Abs(Grid.CellRow(root) - Grid.CellRow(num3)));
			if (num4 < num2 && navigator.CanReach(num3))
			{
				num2 = num4;
				num = num3;
			}
		}
		return num;
	}

	// Token: 0x04001478 RID: 5240
	private int _overlapPenalty = 3;

	// Token: 0x04001479 RID: 5241
	private int _preferredRange;

	// Token: 0x0400147A RID: 5242
	private int _rangePenalty = 2;

	// Token: 0x0400147B RID: 5243
	private int _pathCostPenalty = 1;

	// Token: 0x0400147C RID: 5244
	private int _pathXCostPenalty;

	// Token: 0x0400147D RID: 5245
	private int _preferredX;

	// Token: 0x0400147E RID: 5246
	private int _pathYCostPenalty;

	// Token: 0x0400147F RID: 5247
	private int _preferredY;
}
