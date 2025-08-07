using System;
using TUNING;
using UnityEngine;

// Token: 0x02000AD7 RID: 2775
[AddComponentMenu("KMonoBehaviour/scripts/RobotExhaustPipe")]
public class RobotExhaustPipe : KMonoBehaviour, ISim4000ms
{
	// Token: 0x060050BA RID: 20666 RVA: 0x001D485C File Offset: 0x001D2A5C
	public void Sim4000ms(float dt)
	{
		Facing component = base.GetComponent<Facing>();
		bool flag = false;
		if (component)
		{
			flag = component.GetFacing();
		}
		CO2Manager.instance.SpawnBreath(Grid.CellToPos(Grid.PosToCell(base.gameObject)), dt * this.CO2_RATE, 303.15f, flag);
	}

	// Token: 0x04003647 RID: 13895
	private float CO2_RATE = DUPLICANTSTATS.STANDARD.BaseStats.OXYGEN_USED_PER_SECOND * DUPLICANTSTATS.STANDARD.BaseStats.OXYGEN_TO_CO2_CONVERSION / 2f;
}
