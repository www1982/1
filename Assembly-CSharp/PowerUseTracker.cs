using System;
using UnityEngine;

// Token: 0x02000631 RID: 1585
public class PowerUseTracker : WorldTracker
{
	// Token: 0x06002660 RID: 9824 RVA: 0x000DAF7E File Offset: 0x000D917E
	public PowerUseTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x06002661 RID: 9825 RVA: 0x000DAF88 File Offset: 0x000D9188
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
					num += Game.Instance.circuitManager.GetWattsUsedByCircuit(Game.Instance.circuitManager.GetCircuitID(num2));
				}
			}
		}
		base.AddPoint(Mathf.Round(num));
	}

	// Token: 0x06002662 RID: 9826 RVA: 0x000DB048 File Offset: 0x000D9248
	public override string FormatValueString(float value)
	{
		return GameUtil.GetFormattedWattage(value, GameUtil.WattageFormatterUnit.Automatic, true);
	}
}
