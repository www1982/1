using System;
using UnityEngine;

// Token: 0x0200062E RID: 1582
public class BreathabilityTracker : WorldTracker
{
	// Token: 0x06002655 RID: 9813 RVA: 0x000DADF3 File Offset: 0x000D8FF3
	public BreathabilityTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x06002656 RID: 9814 RVA: 0x000DADFC File Offset: 0x000D8FFC
	public override void UpdateData()
	{
		float num = 0f;
		if (Components.LiveMinionIdentities.GetWorldItems(base.WorldID, false).Count == 0)
		{
			base.AddPoint(0f);
			return;
		}
		int num2 = 0;
		foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.GetWorldItems(base.WorldID, false))
		{
			OxygenBreather component = minionIdentity.GetComponent<OxygenBreather>();
			if (!(component == null))
			{
				OxygenBreather.IGasProvider currentGasProvider = component.GetCurrentGasProvider();
				num2++;
				if (!component.IsOutOfOxygen)
				{
					num += 100f;
					if (currentGasProvider.IsLowOxygen())
					{
						num -= 50f;
					}
				}
			}
		}
		num /= (float)num2;
		base.AddPoint((float)Mathf.RoundToInt(num));
	}

	// Token: 0x06002657 RID: 9815 RVA: 0x000DAECC File Offset: 0x000D90CC
	public override string FormatValueString(float value)
	{
		return value.ToString() + "%";
	}
}
