using System;
using UnityEngine;

// Token: 0x020009D2 RID: 2514
public class BionicMicrochipMonitor : GameStateMachine<BionicMicrochipMonitor, BionicMicrochipMonitor.Instance, IStateMachineTarget, BionicMicrochipMonitor.Def>
{
	// Token: 0x06004998 RID: 18840 RVA: 0x001AA4C4 File Offset: 0x001A86C4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.idle;
		this.idle.TagTransition(GameTags.BionicBedTime, this.production, false);
		this.production.TagTransition(GameTags.BionicBedTime, this.idle, true).Enter(new StateMachine<BionicMicrochipMonitor, BionicMicrochipMonitor.Instance, IStateMachineTarget, BionicMicrochipMonitor.Def>.State.Callback(BionicMicrochipMonitor.CreateProgresesBar)).Exit(new StateMachine<BionicMicrochipMonitor, BionicMicrochipMonitor.Instance, IStateMachineTarget, BionicMicrochipMonitor.Def>.State.Callback(BionicMicrochipMonitor.ClearProgressBar))
			.ToggleStatusItem(Db.Get().DuplicantStatusItems.BionicMicrochipGeneration, null)
			.DefaultState(this.production.charging);
		this.production.charging.ParamTransition<float>(this.Progress, this.production.produceOne, GameStateMachine<BionicMicrochipMonitor, BionicMicrochipMonitor.Instance, IStateMachineTarget, BionicMicrochipMonitor.Def>.IsGTEOne).Update(new Action<BionicMicrochipMonitor.Instance, float>(BionicMicrochipMonitor.ProgressUpdate), UpdateRate.SIM_200ms, false);
		this.production.produceOne.Enter(new StateMachine<BionicMicrochipMonitor, BionicMicrochipMonitor.Instance, IStateMachineTarget, BionicMicrochipMonitor.Def>.State.Callback(BionicMicrochipMonitor.CreateMicrochip)).Enter(new StateMachine<BionicMicrochipMonitor, BionicMicrochipMonitor.Instance, IStateMachineTarget, BionicMicrochipMonitor.Def>.State.Callback(BionicMicrochipMonitor.ResetProgress)).GoTo(this.production.charging);
	}

	// Token: 0x06004999 RID: 18841 RVA: 0x001AA5CF File Offset: 0x001A87CF
	public static void ClearProgressBar(BionicMicrochipMonitor.Instance smi)
	{
		smi.ClearProgressBar();
	}

	// Token: 0x0600499A RID: 18842 RVA: 0x001AA5D7 File Offset: 0x001A87D7
	public static void CreateProgresesBar(BionicMicrochipMonitor.Instance smi)
	{
		smi.CreateProgressBar();
	}

	// Token: 0x0600499B RID: 18843 RVA: 0x001AA5DF File Offset: 0x001A87DF
	public static void ResetProgress(BionicMicrochipMonitor.Instance smi)
	{
		smi.sm.Progress.Set(0f, smi, false);
	}

	// Token: 0x0600499C RID: 18844 RVA: 0x001AA5F9 File Offset: 0x001A87F9
	public static void CreateMicrochip(BionicMicrochipMonitor.Instance smi)
	{
		smi.CreateMicrochip();
	}

	// Token: 0x0600499D RID: 18845 RVA: 0x001AA604 File Offset: 0x001A8804
	public static void ProgressUpdate(BionicMicrochipMonitor.Instance smi, float dt)
	{
		float num = dt / 150f;
		float progress = smi.Progress;
		smi.sm.Progress.Set(progress + num, smi, false);
	}

	// Token: 0x04003081 RID: 12417
	public const float MICROCHIP_PRODUCTION_TIME = 150f;

	// Token: 0x04003082 RID: 12418
	public GameStateMachine<BionicMicrochipMonitor, BionicMicrochipMonitor.Instance, IStateMachineTarget, BionicMicrochipMonitor.Def>.State idle;

	// Token: 0x04003083 RID: 12419
	public BionicMicrochipMonitor.ProductionStates production;

	// Token: 0x04003084 RID: 12420
	public StateMachine<BionicMicrochipMonitor, BionicMicrochipMonitor.Instance, IStateMachineTarget, BionicMicrochipMonitor.Def>.FloatParameter Progress;

	// Token: 0x020019F5 RID: 6645
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020019F6 RID: 6646
	public class ProductionStates : GameStateMachine<BionicMicrochipMonitor, BionicMicrochipMonitor.Instance, IStateMachineTarget, BionicMicrochipMonitor.Def>.State
	{
		// Token: 0x04007E38 RID: 32312
		public GameStateMachine<BionicMicrochipMonitor, BionicMicrochipMonitor.Instance, IStateMachineTarget, BionicMicrochipMonitor.Def>.State charging;

		// Token: 0x04007E39 RID: 32313
		public GameStateMachine<BionicMicrochipMonitor, BionicMicrochipMonitor.Instance, IStateMachineTarget, BionicMicrochipMonitor.Def>.State produceOne;
	}

	// Token: 0x020019F7 RID: 6647
	public new class Instance : GameStateMachine<BionicMicrochipMonitor, BionicMicrochipMonitor.Instance, IStateMachineTarget, BionicMicrochipMonitor.Def>.GameInstance
	{
		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x0600A16C RID: 41324 RVA: 0x0039EA0A File Offset: 0x0039CC0A
		public float Progress
		{
			get
			{
				return base.sm.Progress.Get(this);
			}
		}

		// Token: 0x0600A16D RID: 41325 RVA: 0x0039EA1D File Offset: 0x0039CC1D
		public Instance(IStateMachineTarget master, BionicMicrochipMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x0600A16E RID: 41326 RVA: 0x0039EA27 File Offset: 0x0039CC27
		public void CreateMicrochip()
		{
			Util.KInstantiate(Assets.GetPrefab(PowerStationToolsConfig.tag), Grid.CellToPos(Grid.PosToCell(base.smi.gameObject), CellAlignment.Top, Grid.SceneLayer.Ore)).SetActive(true);
		}

		// Token: 0x0600A16F RID: 41327 RVA: 0x0039EA58 File Offset: 0x0039CC58
		public void CreateProgressBar()
		{
			this.progressBar = ProgressBar.CreateProgressBar(base.gameObject, () => this.Progress);
			base.smi.progressBar.SetVisibility(true);
			base.smi.progressBar.barColor = Color.green;
		}

		// Token: 0x0600A170 RID: 41328 RVA: 0x0039EAA8 File Offset: 0x0039CCA8
		public void ClearProgressBar()
		{
			if (this.progressBar != null)
			{
				Util.KDestroyGameObject(base.smi.progressBar.gameObject);
				this.progressBar = null;
			}
		}

		// Token: 0x04007E3A RID: 32314
		public ProgressBar progressBar;
	}
}
