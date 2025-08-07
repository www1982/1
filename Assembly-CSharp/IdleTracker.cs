using System;
using System.Collections.Generic;

// Token: 0x02000636 RID: 1590
public class IdleTracker : WorldTracker
{
	// Token: 0x0600266F RID: 9839 RVA: 0x000DB290 File Offset: 0x000D9490
	public IdleTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x06002670 RID: 9840 RVA: 0x000DB29C File Offset: 0x000D949C
	public override void UpdateData()
	{
		this.objectsOfInterest.Clear();
		int num = 0;
		List<MinionIdentity> worldItems = Components.LiveMinionIdentities.GetWorldItems(base.WorldID, false);
		for (int i = 0; i < worldItems.Count; i++)
		{
			if (worldItems[i].HasTag(GameTags.Idle))
			{
				num++;
				this.objectsOfInterest.Add(worldItems[i].gameObject);
			}
		}
		base.AddPoint((float)num);
	}

	// Token: 0x06002671 RID: 9841 RVA: 0x000DB30F File Offset: 0x000D950F
	public override string FormatValueString(float value)
	{
		return value.ToString();
	}
}
