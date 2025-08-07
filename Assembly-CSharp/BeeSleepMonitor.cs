using System;
using UnityEngine;

// Token: 0x020000D8 RID: 216
public class BeeSleepMonitor : GameStateMachine<BeeSleepMonitor, BeeSleepMonitor.Instance, IStateMachineTarget, BeeSleepMonitor.Def>
{
	// Token: 0x060003D2 RID: 978 RVA: 0x00020576 File Offset: 0x0001E776
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.Update(new Action<BeeSleepMonitor.Instance, float>(this.UpdateCO2Exposure), UpdateRate.SIM_1000ms, false).ToggleBehaviour(GameTags.Creatures.BeeWantsToSleep, new StateMachine<BeeSleepMonitor, BeeSleepMonitor.Instance, IStateMachineTarget, BeeSleepMonitor.Def>.Transition.ConditionCallback(this.ShouldSleep), null);
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x000205B1 File Offset: 0x0001E7B1
	public bool ShouldSleep(BeeSleepMonitor.Instance smi)
	{
		return smi.CO2Exposure >= 5f;
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x000205C4 File Offset: 0x0001E7C4
	public void UpdateCO2Exposure(BeeSleepMonitor.Instance smi, float dt)
	{
		if (this.IsInCO2(smi))
		{
			smi.CO2Exposure += 1f;
		}
		else
		{
			smi.CO2Exposure -= 0.5f;
		}
		smi.CO2Exposure = Mathf.Clamp(smi.CO2Exposure, 0f, 10f);
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x0002061C File Offset: 0x0001E81C
	public bool IsInCO2(BeeSleepMonitor.Instance smi)
	{
		int num = Grid.PosToCell(smi.gameObject);
		return Grid.IsValidCell(num) && Grid.Element[num].id == SimHashes.CarbonDioxide;
	}

	// Token: 0x020010A4 RID: 4260
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020010A5 RID: 4261
	public new class Instance : GameStateMachine<BeeSleepMonitor, BeeSleepMonitor.Instance, IStateMachineTarget, BeeSleepMonitor.Def>.GameInstance
	{
		// Token: 0x06008062 RID: 32866 RVA: 0x0032D5E5 File Offset: 0x0032B7E5
		public Instance(IStateMachineTarget master, BeeSleepMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x040060FF RID: 24831
		public float CO2Exposure;
	}
}
