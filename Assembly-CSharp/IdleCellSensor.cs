using System;
using UnityEngine;

// Token: 0x020004FD RID: 1277
public class IdleCellSensor : Sensor
{
	// Token: 0x06001B56 RID: 6998 RVA: 0x000963DC File Offset: 0x000945DC
	public IdleCellSensor(Sensors sensors)
		: base(sensors)
	{
		this.navigator = base.GetComponent<Navigator>();
		this.brain = base.GetComponent<MinionBrain>();
		this.prefabid = base.GetComponent<KPrefabID>();
	}

	// Token: 0x06001B57 RID: 6999 RVA: 0x0009640C File Offset: 0x0009460C
	public override void Update()
	{
		if (!this.prefabid.HasTag(GameTags.Idle))
		{
			this.cell = Grid.InvalidCell;
			return;
		}
		MinionPathFinderAbilities minionPathFinderAbilities = (MinionPathFinderAbilities)this.navigator.GetCurrentAbilities();
		minionPathFinderAbilities.SetIdleNavMaskEnabled(true);
		IdleCellQuery idleCellQuery = PathFinderQueries.idleCellQuery.Reset(this.brain, global::UnityEngine.Random.Range(30, 60));
		this.navigator.RunQuery(idleCellQuery);
		minionPathFinderAbilities.SetIdleNavMaskEnabled(false);
		this.cell = idleCellQuery.GetResultCell();
	}

	// Token: 0x06001B58 RID: 7000 RVA: 0x00096486 File Offset: 0x00094686
	public int GetCell()
	{
		return this.cell;
	}

	// Token: 0x0400101E RID: 4126
	private MinionBrain brain;

	// Token: 0x0400101F RID: 4127
	private Navigator navigator;

	// Token: 0x04001020 RID: 4128
	private KPrefabID prefabid;

	// Token: 0x04001021 RID: 4129
	private int cell;
}
