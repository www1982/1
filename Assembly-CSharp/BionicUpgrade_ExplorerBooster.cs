using System;
using UnityEngine;

// Token: 0x020006BB RID: 1723
public class BionicUpgrade_ExplorerBooster : GameStateMachine<BionicUpgrade_ExplorerBooster, BionicUpgrade_ExplorerBooster.Instance, IStateMachineTarget, BionicUpgrade_ExplorerBooster.Def>
{
	// Token: 0x06002A5A RID: 10842 RVA: 0x000F54C4 File Offset: 0x000F36C4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.not_ready;
		this.not_ready.ParamTransition<float>(this.Progress, this.ready, GameStateMachine<BionicUpgrade_ExplorerBooster, BionicUpgrade_ExplorerBooster.Instance, IStateMachineTarget, BionicUpgrade_ExplorerBooster.Def>.IsGTEOne).ToggleStatusItem(Db.Get().MiscStatusItems.BionicExplorerBooster, null);
		this.ready.ParamTransition<float>(this.Progress, this.not_ready, GameStateMachine<BionicUpgrade_ExplorerBooster, BionicUpgrade_ExplorerBooster.Instance, IStateMachineTarget, BionicUpgrade_ExplorerBooster.Def>.IsLTOne).ToggleStatusItem(Db.Get().MiscStatusItems.BionicExplorerBoosterReady, null);
	}

	// Token: 0x0400190A RID: 6410
	public const float DataGatheringDuration = 600f;

	// Token: 0x0400190B RID: 6411
	private StateMachine<BionicUpgrade_ExplorerBooster, BionicUpgrade_ExplorerBooster.Instance, IStateMachineTarget, BionicUpgrade_ExplorerBooster.Def>.FloatParameter Progress;

	// Token: 0x0400190C RID: 6412
	public GameStateMachine<BionicUpgrade_ExplorerBooster, BionicUpgrade_ExplorerBooster.Instance, IStateMachineTarget, BionicUpgrade_ExplorerBooster.Def>.State not_ready;

	// Token: 0x0400190D RID: 6413
	public GameStateMachine<BionicUpgrade_ExplorerBooster, BionicUpgrade_ExplorerBooster.Instance, IStateMachineTarget, BionicUpgrade_ExplorerBooster.Def>.State ready;

	// Token: 0x02001536 RID: 5430
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001537 RID: 5431
	public new class Instance : GameStateMachine<BionicUpgrade_ExplorerBooster, BionicUpgrade_ExplorerBooster.Instance, IStateMachineTarget, BionicUpgrade_ExplorerBooster.Def>.GameInstance
	{
		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06009054 RID: 36948 RVA: 0x0035FD78 File Offset: 0x0035DF78
		public bool IsBeingMonitored
		{
			get
			{
				return this.monitor != null;
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06009055 RID: 36949 RVA: 0x0035FD83 File Offset: 0x0035DF83
		public bool IsReady
		{
			get
			{
				return this.Progress == 1f;
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06009056 RID: 36950 RVA: 0x0035FD92 File Offset: 0x0035DF92
		public float Progress
		{
			get
			{
				return base.sm.Progress.Get(this);
			}
		}

		// Token: 0x06009057 RID: 36951 RVA: 0x0035FDA5 File Offset: 0x0035DFA5
		public Instance(IStateMachineTarget master, BionicUpgrade_ExplorerBooster.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06009058 RID: 36952 RVA: 0x0035FDAF File Offset: 0x0035DFAF
		public void SetMonitor(BionicUpgrade_ExplorerBoosterMonitor.Instance monitor)
		{
			this.monitor = monitor;
		}

		// Token: 0x06009059 RID: 36953 RVA: 0x0035FDB8 File Offset: 0x0035DFB8
		public void AddData(float dataProgressDelta)
		{
			float num = Mathf.Clamp(this.Progress + dataProgressDelta, 0f, 1f);
			this.SetDataProgress(num);
		}

		// Token: 0x0600905A RID: 36954 RVA: 0x0035FDE4 File Offset: 0x0035DFE4
		public void SetDataProgress(float dataProgress)
		{
			Mathf.Clamp(dataProgress, 0f, 1f);
			base.sm.Progress.Set(dataProgress, this, false);
		}

		// Token: 0x04006F11 RID: 28433
		private BionicUpgrade_ExplorerBoosterMonitor.Instance monitor;
	}
}
