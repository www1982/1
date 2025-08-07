using System;
using Klei.AI;
using UnityEngine;

// Token: 0x020009F3 RID: 2547
public class HeatImmunityMonitor : GameStateMachine<HeatImmunityMonitor, HeatImmunityMonitor.Instance>
{
	// Token: 0x06004A59 RID: 19033 RVA: 0x001AED50 File Offset: 0x001ACF50
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.idle;
		this.idle.DefaultState(this.idle.feelingFine).TagTransition(GameTags.FeelingWarm, this.warm, false).ParamTransition<float>(this.heatCountdown, this.warm, GameStateMachine<HeatImmunityMonitor, HeatImmunityMonitor.Instance, IStateMachineTarget, object>.IsGTZero);
		this.idle.feelingFine.DoNothing();
		this.idle.leftWithDesireToCooldownAfterBeingWarm.Enter(new StateMachine<HeatImmunityMonitor, HeatImmunityMonitor.Instance, IStateMachineTarget, object>.State.Callback(HeatImmunityMonitor.UpdateShelterCell)).Update(new Action<HeatImmunityMonitor.Instance, float>(HeatImmunityMonitor.UpdateShelterCell), UpdateRate.RENDER_1000ms, false).ToggleChore(new Func<HeatImmunityMonitor.Instance, Chore>(HeatImmunityMonitor.CreateRecoverFromOverheatChore), this.idle.feelingFine, this.idle.feelingFine);
		this.warm.DefaultState(this.warm.exiting).TagTransition(GameTags.FeelingCold, this.idle, false).ToggleAnims("anim_idle_hot_kanim", 0f)
			.ToggleAnims("anim_loco_run_hot_kanim", 0f)
			.ToggleAnims("anim_loco_walk_hot_kanim", 0f)
			.ToggleExpression(Db.Get().Expressions.Hot, null)
			.ToggleThought(Db.Get().Thoughts.Hot, null)
			.ToggleEffect("WarmAir")
			.Enter(new StateMachine<HeatImmunityMonitor, HeatImmunityMonitor.Instance, IStateMachineTarget, object>.State.Callback(HeatImmunityMonitor.UpdateShelterCell))
			.Update(new Action<HeatImmunityMonitor.Instance, float>(HeatImmunityMonitor.UpdateShelterCell), UpdateRate.RENDER_1000ms, false)
			.ToggleChore(new Func<HeatImmunityMonitor.Instance, Chore>(HeatImmunityMonitor.CreateRecoverFromOverheatChore), this.idle, this.warm);
		this.warm.exiting.EventHandlerTransition(GameHashes.EffectAdded, this.idle, new Func<HeatImmunityMonitor.Instance, object, bool>(HeatImmunityMonitor.HasImmunityEffect)).TagTransition(GameTags.FeelingWarm, this.warm.idle, false).ToggleStatusItem(Db.Get().DuplicantStatusItems.ExitingHot, null)
			.ParamTransition<float>(this.heatCountdown, this.idle.leftWithDesireToCooldownAfterBeingWarm, GameStateMachine<HeatImmunityMonitor, HeatImmunityMonitor.Instance, IStateMachineTarget, object>.IsZero)
			.Update(new Action<HeatImmunityMonitor.Instance, float>(HeatImmunityMonitor.HeatTimerUpdate), UpdateRate.SIM_200ms, false)
			.Exit(new StateMachine<HeatImmunityMonitor, HeatImmunityMonitor.Instance, IStateMachineTarget, object>.State.Callback(HeatImmunityMonitor.ClearTimer));
		this.warm.idle.Enter(new StateMachine<HeatImmunityMonitor, HeatImmunityMonitor.Instance, IStateMachineTarget, object>.State.Callback(HeatImmunityMonitor.ResetHeatTimer)).ToggleStatusItem(Db.Get().DuplicantStatusItems.Hot, (HeatImmunityMonitor.Instance smi) => smi).TagTransition(GameTags.FeelingWarm, this.warm.exiting, true);
	}

	// Token: 0x06004A5A RID: 19034 RVA: 0x001AEFD4 File Offset: 0x001AD1D4
	public static bool OnEffectAdded(HeatImmunityMonitor.Instance smi, object data)
	{
		return true;
	}

	// Token: 0x06004A5B RID: 19035 RVA: 0x001AEFD7 File Offset: 0x001AD1D7
	public static void ClearTimer(HeatImmunityMonitor.Instance smi)
	{
		smi.sm.heatCountdown.Set(0f, smi, false);
	}

	// Token: 0x06004A5C RID: 19036 RVA: 0x001AEFF1 File Offset: 0x001AD1F1
	public static void ResetHeatTimer(HeatImmunityMonitor.Instance smi)
	{
		smi.sm.heatCountdown.Set(5f, smi, false);
	}

	// Token: 0x06004A5D RID: 19037 RVA: 0x001AF00C File Offset: 0x001AD20C
	public static void HeatTimerUpdate(HeatImmunityMonitor.Instance smi, float dt)
	{
		float num = Mathf.Clamp(smi.HeatCountdown - dt, 0f, 5f);
		smi.sm.heatCountdown.Set(num, smi, false);
	}

	// Token: 0x06004A5E RID: 19038 RVA: 0x001AF045 File Offset: 0x001AD245
	private static void UpdateShelterCell(HeatImmunityMonitor.Instance smi, float dt)
	{
		smi.UpdateShelterCell();
	}

	// Token: 0x06004A5F RID: 19039 RVA: 0x001AF04D File Offset: 0x001AD24D
	private static void UpdateShelterCell(HeatImmunityMonitor.Instance smi)
	{
		smi.UpdateShelterCell();
	}

	// Token: 0x06004A60 RID: 19040 RVA: 0x001AF058 File Offset: 0x001AD258
	public static bool HasImmunityEffect(HeatImmunityMonitor.Instance smi, object data)
	{
		Effects component = smi.GetComponent<Effects>();
		return component != null && component.HasEffect("RefreshingTouch");
	}

	// Token: 0x06004A61 RID: 19041 RVA: 0x001AF082 File Offset: 0x001AD282
	private static Chore CreateRecoverFromOverheatChore(HeatImmunityMonitor.Instance smi)
	{
		return new RecoverFromHeatChore(smi.master);
	}

	// Token: 0x0400311A RID: 12570
	private const float EFFECT_DURATION = 5f;

	// Token: 0x0400311B RID: 12571
	public HeatImmunityMonitor.IdleStates idle;

	// Token: 0x0400311C RID: 12572
	public HeatImmunityMonitor.WarmStates warm;

	// Token: 0x0400311D RID: 12573
	public StateMachine<HeatImmunityMonitor, HeatImmunityMonitor.Instance, IStateMachineTarget, object>.FloatParameter heatCountdown;

	// Token: 0x02001A5C RID: 6748
	public class WarmStates : GameStateMachine<HeatImmunityMonitor, HeatImmunityMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x04007F8A RID: 32650
		public GameStateMachine<HeatImmunityMonitor, HeatImmunityMonitor.Instance, IStateMachineTarget, object>.State idle;

		// Token: 0x04007F8B RID: 32651
		public GameStateMachine<HeatImmunityMonitor, HeatImmunityMonitor.Instance, IStateMachineTarget, object>.State exiting;

		// Token: 0x04007F8C RID: 32652
		public GameStateMachine<HeatImmunityMonitor, HeatImmunityMonitor.Instance, IStateMachineTarget, object>.State resetChore;
	}

	// Token: 0x02001A5D RID: 6749
	public class IdleStates : GameStateMachine<HeatImmunityMonitor, HeatImmunityMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x04007F8D RID: 32653
		public GameStateMachine<HeatImmunityMonitor, HeatImmunityMonitor.Instance, IStateMachineTarget, object>.State feelingFine;

		// Token: 0x04007F8E RID: 32654
		public GameStateMachine<HeatImmunityMonitor, HeatImmunityMonitor.Instance, IStateMachineTarget, object>.State leftWithDesireToCooldownAfterBeingWarm;
	}

	// Token: 0x02001A5E RID: 6750
	public new class Instance : GameStateMachine<HeatImmunityMonitor, HeatImmunityMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x0600A349 RID: 41801 RVA: 0x003A3BB9 File Offset: 0x003A1DB9
		// (set) Token: 0x0600A34A RID: 41802 RVA: 0x003A3BC1 File Offset: 0x003A1DC1
		public HeatImmunityProvider.Instance NearestImmunityProvider { get; private set; }

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x0600A34B RID: 41803 RVA: 0x003A3BCA File Offset: 0x003A1DCA
		// (set) Token: 0x0600A34C RID: 41804 RVA: 0x003A3BD2 File Offset: 0x003A1DD2
		public int ShelterCell { get; private set; }

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x0600A34D RID: 41805 RVA: 0x003A3BDB File Offset: 0x003A1DDB
		public float HeatCountdown
		{
			get
			{
				return base.smi.sm.heatCountdown.Get(this);
			}
		}

		// Token: 0x0600A34E RID: 41806 RVA: 0x003A3BF3 File Offset: 0x003A1DF3
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x0600A34F RID: 41807 RVA: 0x003A3BFC File Offset: 0x003A1DFC
		public override void StartSM()
		{
			this.navigator = base.gameObject.GetComponent<Navigator>();
			base.StartSM();
		}

		// Token: 0x0600A350 RID: 41808 RVA: 0x003A3C18 File Offset: 0x003A1E18
		public void UpdateShelterCell()
		{
			int myWorldId = this.navigator.GetMyWorldId();
			int num = Grid.InvalidCell;
			int num2 = int.MaxValue;
			HeatImmunityProvider.Instance instance = null;
			foreach (StateMachine.Instance instance2 in Components.EffectImmunityProviderStations.Items.FindAll((StateMachine.Instance t) => t is HeatImmunityProvider.Instance))
			{
				HeatImmunityProvider.Instance instance3 = instance2 as HeatImmunityProvider.Instance;
				if (instance3.GetMyWorldId() == myWorldId)
				{
					int maxValue = int.MaxValue;
					int bestAvailableCell = instance3.GetBestAvailableCell(this.navigator, out maxValue);
					if (maxValue < num2)
					{
						num2 = maxValue;
						instance = instance3;
						num = bestAvailableCell;
					}
				}
			}
			this.NearestImmunityProvider = instance;
			this.ShelterCell = num;
		}

		// Token: 0x04007F91 RID: 32657
		private Navigator navigator;
	}
}
