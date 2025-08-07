using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005E8 RID: 1512
[AddComponentMenu("KMonoBehaviour/scripts/NavigationReservations")]
public class NavigationReservations : KMonoBehaviour
{
	// Token: 0x06002341 RID: 9025 RVA: 0x000CA4C6 File Offset: 0x000C86C6
	public static void DestroyInstance()
	{
		NavigationReservations.Instance = null;
	}

	// Token: 0x06002342 RID: 9026 RVA: 0x000CA4CE File Offset: 0x000C86CE
	public int GetOccupancyCount(int cell)
	{
		if (this.cellOccupancyDensity.ContainsKey(cell))
		{
			return this.cellOccupancyDensity[cell];
		}
		return 0;
	}

	// Token: 0x06002343 RID: 9027 RVA: 0x000CA4EC File Offset: 0x000C86EC
	public void AddOccupancy(int cell)
	{
		if (!this.cellOccupancyDensity.ContainsKey(cell))
		{
			this.cellOccupancyDensity.Add(cell, 1);
			return;
		}
		Dictionary<int, int> dictionary = this.cellOccupancyDensity;
		dictionary[cell]++;
	}

	// Token: 0x06002344 RID: 9028 RVA: 0x000CA530 File Offset: 0x000C8730
	public void RemoveOccupancy(int cell)
	{
		int num = 0;
		if (this.cellOccupancyDensity.TryGetValue(cell, out num))
		{
			if (num == 1)
			{
				this.cellOccupancyDensity.Remove(cell);
				return;
			}
			this.cellOccupancyDensity[cell] = num - 1;
		}
	}

	// Token: 0x06002345 RID: 9029 RVA: 0x000CA570 File Offset: 0x000C8770
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		NavigationReservations.Instance = this;
	}

	// Token: 0x04001471 RID: 5233
	public static NavigationReservations Instance;

	// Token: 0x04001472 RID: 5234
	public static int InvalidReservation = -1;

	// Token: 0x04001473 RID: 5235
	private Dictionary<int, int> cellOccupancyDensity = new Dictionary<int, int>();
}
