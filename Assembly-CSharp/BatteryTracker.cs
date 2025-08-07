using System;
using UnityEngine;

// Token: 0x02000632 RID: 1586
public class BatteryTracker : WorldTracker
{
	// Token: 0x06002663 RID: 9827 RVA: 0x000DB052 File Offset: 0x000D9252
	public BatteryTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x06002664 RID: 9828 RVA: 0x000DB05C File Offset: 0x000D925C
	public override void UpdateData()
	{
		float num = 0f;
		foreach (UtilityNetwork utilityNetwork in Game.Instance.electricalConduitSystem.GetNetworks())
		{
			ElectricalUtilityNetwork electricalUtilityNetwork = (ElectricalUtilityNetwork)utilityNetwork;
			if (electricalUtilityNetwork.allWires != null && electricalUtilityNetwork.allWires.Count != 0)
			{
				int num2 = Grid.PosToCell(electricalUtilityNetwork.allWires[0]);
				if ((int)Grid.WorldIdx[num2] == base.WorldID)
				{
					ushort circuitID = Game.Instance.circuitManager.GetCircuitID(num2);
					foreach (Battery battery in Game.Instance.circuitManager.GetBatteriesOnCircuit(circuitID))
					{
						num += battery.JoulesAvailable;
					}
				}
			}
		}
		base.AddPoint(Mathf.Round(num));
	}

	// Token: 0x06002665 RID: 9829 RVA: 0x000DB168 File Offset: 0x000D9368
	public override string FormatValueString(float value)
	{
		return GameUtil.GetFormattedJoules(value, "F1", GameUtil.TimeSlice.None);
	}
}
