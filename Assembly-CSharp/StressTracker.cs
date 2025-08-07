using System;
using Klei.AI;
using UnityEngine;

// Token: 0x02000633 RID: 1587
public class StressTracker : WorldTracker
{
	// Token: 0x06002666 RID: 9830 RVA: 0x000DB176 File Offset: 0x000D9376
	public StressTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x06002667 RID: 9831 RVA: 0x000DB180 File Offset: 0x000D9380
	public override void UpdateData()
	{
		float num = 0f;
		for (int i = 0; i < Components.LiveMinionIdentities.Count; i++)
		{
			if (Components.LiveMinionIdentities[i].GetMyWorldId() == base.WorldID)
			{
				num = Mathf.Max(num, Components.LiveMinionIdentities[i].gameObject.GetAmounts().GetValue(Db.Get().Amounts.Stress.Id));
			}
		}
		base.AddPoint(Mathf.Round(num));
	}

	// Token: 0x06002668 RID: 9832 RVA: 0x000DB201 File Offset: 0x000D9401
	public override string FormatValueString(float value)
	{
		return value.ToString() + "%";
	}
}
