using System;
using System.Collections.Generic;

// Token: 0x0200063A RID: 1594
public class WorkingToiletTracker : WorldTracker
{
	// Token: 0x0600267B RID: 9851 RVA: 0x000DB49C File Offset: 0x000D969C
	public WorkingToiletTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x0600267C RID: 9852 RVA: 0x000DB4A8 File Offset: 0x000D96A8
	public override void UpdateData()
	{
		int num = 0;
		using (IEnumerator<IUsable> enumerator = Components.Toilets.WorldItemsEnumerate(base.WorldID, true).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.IsUsable())
				{
					num++;
				}
			}
		}
		base.AddPoint((float)num);
	}

	// Token: 0x0600267D RID: 9853 RVA: 0x000DB510 File Offset: 0x000D9710
	public override string FormatValueString(float value)
	{
		return value.ToString();
	}
}
