using System;
using System.Collections.Generic;
using Klei.AI;

// Token: 0x02000637 RID: 1591
public class RadiationTracker : WorldTracker
{
	// Token: 0x06002672 RID: 9842 RVA: 0x000DB318 File Offset: 0x000D9518
	public RadiationTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x06002673 RID: 9843 RVA: 0x000DB324 File Offset: 0x000D9524
	public override void UpdateData()
	{
		float num = 0f;
		List<MinionIdentity> worldItems = Components.MinionIdentities.GetWorldItems(base.WorldID, false);
		if (worldItems.Count == 0)
		{
			base.AddPoint(0f);
			return;
		}
		foreach (MinionIdentity minionIdentity in worldItems)
		{
			num += minionIdentity.GetAmounts().Get(Db.Get().Amounts.RadiationBalance.Id).value;
		}
		float num2 = num / (float)worldItems.Count;
		base.AddPoint(num2);
	}

	// Token: 0x06002674 RID: 9844 RVA: 0x000DB3D4 File Offset: 0x000D95D4
	public override string FormatValueString(float value)
	{
		return GameUtil.GetFormattedRads(value, GameUtil.TimeSlice.None);
	}
}
