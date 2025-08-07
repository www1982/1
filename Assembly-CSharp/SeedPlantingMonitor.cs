using System;
using UnityEngine;

// Token: 0x02000108 RID: 264
public class SeedPlantingMonitor : GameStateMachine<SeedPlantingMonitor, SeedPlantingMonitor.Instance, IStateMachineTarget, SeedPlantingMonitor.Def>
{
	// Token: 0x060004C7 RID: 1223 RVA: 0x00027010 File Offset: 0x00025210
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleBehaviour(GameTags.Creatures.WantsToPlantSeed, new StateMachine<SeedPlantingMonitor, SeedPlantingMonitor.Instance, IStateMachineTarget, SeedPlantingMonitor.Def>.Transition.ConditionCallback(SeedPlantingMonitor.ShouldSearchForSeeds), delegate(SeedPlantingMonitor.Instance smi)
		{
			smi.RefreshSearchTime();
		});
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x00027061 File Offset: 0x00025261
	public static bool ShouldSearchForSeeds(SeedPlantingMonitor.Instance smi)
	{
		return Time.time >= smi.nextSearchTime;
	}

	// Token: 0x0200113F RID: 4415
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006256 RID: 25174
		public float searchMinInterval = 60f;

		// Token: 0x04006257 RID: 25175
		public float searchMaxInterval = 300f;
	}

	// Token: 0x02001140 RID: 4416
	public new class Instance : GameStateMachine<SeedPlantingMonitor, SeedPlantingMonitor.Instance, IStateMachineTarget, SeedPlantingMonitor.Def>.GameInstance
	{
		// Token: 0x060081E0 RID: 33248 RVA: 0x0032FFD3 File Offset: 0x0032E1D3
		public Instance(IStateMachineTarget master, SeedPlantingMonitor.Def def)
			: base(master, def)
		{
			this.RefreshSearchTime();
		}

		// Token: 0x060081E1 RID: 33249 RVA: 0x0032FFE3 File Offset: 0x0032E1E3
		public void RefreshSearchTime()
		{
			this.nextSearchTime = Time.time + Mathf.Lerp(base.def.searchMinInterval, base.def.searchMaxInterval, global::UnityEngine.Random.value);
		}

		// Token: 0x04006258 RID: 25176
		public float nextSearchTime;
	}
}
