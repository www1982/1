using System;
using UnityEngine;

// Token: 0x020004FE RID: 1278
public class MingleCellSensor : Sensor
{
	// Token: 0x06001B59 RID: 7001 RVA: 0x0009648E File Offset: 0x0009468E
	public MingleCellSensor(Sensors sensors)
		: base(sensors)
	{
		this.navigator = base.GetComponent<Navigator>();
		this.brain = base.GetComponent<MinionBrain>();
	}

	// Token: 0x06001B5A RID: 7002 RVA: 0x000964B0 File Offset: 0x000946B0
	public override void Update()
	{
		this.cell = Grid.InvalidCell;
		int num = int.MaxValue;
		ListPool<int, MingleCellSensor>.PooledList pooledList = ListPool<int, MingleCellSensor>.Allocate();
		int num2 = 50;
		foreach (int num3 in Game.Instance.mingleCellTracker.mingleCells)
		{
			if (this.brain.IsCellClear(num3))
			{
				int navigationCost = this.navigator.GetNavigationCost(num3);
				if (navigationCost != -1)
				{
					if (num3 == Grid.InvalidCell || navigationCost < num)
					{
						this.cell = num3;
						num = navigationCost;
					}
					if (navigationCost < num2)
					{
						pooledList.Add(num3);
					}
				}
			}
		}
		if (pooledList.Count > 0)
		{
			this.cell = pooledList[global::UnityEngine.Random.Range(0, pooledList.Count)];
		}
		pooledList.Recycle();
	}

	// Token: 0x06001B5B RID: 7003 RVA: 0x00096590 File Offset: 0x00094790
	public int GetCell()
	{
		return this.cell;
	}

	// Token: 0x04001022 RID: 4130
	private MinionBrain brain;

	// Token: 0x04001023 RID: 4131
	private Navigator navigator;

	// Token: 0x04001024 RID: 4132
	private int cell;
}
