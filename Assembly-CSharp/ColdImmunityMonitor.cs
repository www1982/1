using System;
using Klei.AI;
using UnityEngine;

// Token: 0x020009DD RID: 2525
public class ColdImmunityMonitor : GameStateMachine<ColdImmunityMonitor, ColdImmunityMonitor.Instance>
{
	// Token: 0x060049F9 RID: 18937 RVA: 0x001AC844 File Offset: 0x001AAA44
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.idle;
		this.idle.DefaultState(this.idle.feelingFine).TagTransition(GameTags.FeelingCold, this.cold, false).ParamTransition<float>(this.coldCountdown, this.cold, GameStateMachine<ColdImmunityMonitor, ColdImmunityMonitor.Instance, IStateMachineTarget, object>.IsGTZero);
		this.idle.feelingFine.DoNothing();
		this.idle.leftWithDesireToWarmupAfterBeingCold.Enter(new StateMachine<ColdImmunityMonitor, ColdImmunityMonitor.Instance, IStateMachineTarget, object>.State.Callback(ColdImmunityMonitor.UpdateWarmUpCell)).Update(new Action<ColdImmunityMonitor.Instance, float>(ColdImmunityMonitor.UpdateWarmUpCell), UpdateRate.RENDER_1000ms, false).ToggleChore(new Func<ColdImmunityMonitor.Instance, Chore>(ColdImmunityMonitor.CreateRecoverFromChillyBonesChore), this.idle.feelingFine, this.idle.feelingFine);
		this.cold.DefaultState(this.cold.exiting).TagTransition(GameTags.FeelingWarm, this.idle, false).ToggleAnims("anim_idle_cold_kanim", 0f)
			.ToggleAnims("anim_loco_run_cold_kanim", 0f)
			.ToggleAnims("anim_loco_walk_cold_kanim", 0f)
			.ToggleExpression(Db.Get().Expressions.Cold, null)
			.ToggleThought(Db.Get().Thoughts.Cold, null)
			.ToggleEffect("ColdAir")
			.Enter(new StateMachine<ColdImmunityMonitor, ColdImmunityMonitor.Instance, IStateMachineTarget, object>.State.Callback(ColdImmunityMonitor.UpdateWarmUpCell))
			.Update(new Action<ColdImmunityMonitor.Instance, float>(ColdImmunityMonitor.UpdateWarmUpCell), UpdateRate.RENDER_1000ms, false)
			.ToggleChore(new Func<ColdImmunityMonitor.Instance, Chore>(ColdImmunityMonitor.CreateRecoverFromChillyBonesChore), this.idle, this.cold);
		this.cold.exiting.EventHandlerTransition(GameHashes.EffectAdded, this.idle, new Func<ColdImmunityMonitor.Instance, object, bool>(ColdImmunityMonitor.HasImmunityEffect)).TagTransition(GameTags.FeelingCold, this.cold.idle, false).ToggleStatusItem(Db.Get().DuplicantStatusItems.ExitingCold, null)
			.ParamTransition<float>(this.coldCountdown, this.idle.leftWithDesireToWarmupAfterBeingCold, GameStateMachine<ColdImmunityMonitor, ColdImmunityMonitor.Instance, IStateMachineTarget, object>.IsZero)
			.Update(new Action<ColdImmunityMonitor.Instance, float>(ColdImmunityMonitor.ColdTimerUpdate), UpdateRate.SIM_200ms, false)
			.Exit(new StateMachine<ColdImmunityMonitor, ColdImmunityMonitor.Instance, IStateMachineTarget, object>.State.Callback(ColdImmunityMonitor.ClearTimer));
		this.cold.idle.Enter(new StateMachine<ColdImmunityMonitor, ColdImmunityMonitor.Instance, IStateMachineTarget, object>.State.Callback(ColdImmunityMonitor.ResetColdTimer)).ToggleStatusItem(Db.Get().DuplicantStatusItems.Cold, (ColdImmunityMonitor.Instance smi) => smi).TagTransition(GameTags.FeelingCold, this.cold.exiting, true);
	}

	// Token: 0x060049FA RID: 18938 RVA: 0x001ACAC8 File Offset: 0x001AACC8
	public static bool OnEffectAdded(ColdImmunityMonitor.Instance smi, object data)
	{
		return true;
	}

	// Token: 0x060049FB RID: 18939 RVA: 0x001ACACB File Offset: 0x001AACCB
	public static void ClearTimer(ColdImmunityMonitor.Instance smi)
	{
		smi.sm.coldCountdown.Set(0f, smi, false);
	}

	// Token: 0x060049FC RID: 18940 RVA: 0x001ACAE5 File Offset: 0x001AACE5
	public static void ResetColdTimer(ColdImmunityMonitor.Instance smi)
	{
		smi.sm.coldCountdown.Set(5f, smi, false);
	}

	// Token: 0x060049FD RID: 18941 RVA: 0x001ACB00 File Offset: 0x001AAD00
	public static void ColdTimerUpdate(ColdImmunityMonitor.Instance smi, float dt)
	{
		float num = Mathf.Clamp(smi.ColdCountdown - dt, 0f, 5f);
		smi.sm.coldCountdown.Set(num, smi, false);
	}

	// Token: 0x060049FE RID: 18942 RVA: 0x001ACB39 File Offset: 0x001AAD39
	private static void UpdateWarmUpCell(ColdImmunityMonitor.Instance smi, float dt)
	{
		smi.UpdateWarmUpCell();
	}

	// Token: 0x060049FF RID: 18943 RVA: 0x001ACB41 File Offset: 0x001AAD41
	private static void UpdateWarmUpCell(ColdImmunityMonitor.Instance smi)
	{
		smi.UpdateWarmUpCell();
	}

	// Token: 0x06004A00 RID: 18944 RVA: 0x001ACB4C File Offset: 0x001AAD4C
	public static bool HasImmunityEffect(ColdImmunityMonitor.Instance smi, object data)
	{
		Effects component = smi.GetComponent<Effects>();
		return component != null && component.HasEffect("WarmTouch");
	}

	// Token: 0x06004A01 RID: 18945 RVA: 0x001ACB76 File Offset: 0x001AAD76
	private static Chore CreateRecoverFromChillyBonesChore(ColdImmunityMonitor.Instance smi)
	{
		return new RecoverFromColdChore(smi.master);
	}

	// Token: 0x040030C5 RID: 12485
	private const float EFFECT_DURATION = 5f;

	// Token: 0x040030C6 RID: 12486
	public ColdImmunityMonitor.IdleStates idle;

	// Token: 0x040030C7 RID: 12487
	public ColdImmunityMonitor.ColdStates cold;

	// Token: 0x040030C8 RID: 12488
	public StateMachine<ColdImmunityMonitor, ColdImmunityMonitor.Instance, IStateMachineTarget, object>.FloatParameter coldCountdown;

	// Token: 0x02001A1F RID: 6687
	public class ColdStates : GameStateMachine<ColdImmunityMonitor, ColdImmunityMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x04007EC3 RID: 32451
		public GameStateMachine<ColdImmunityMonitor, ColdImmunityMonitor.Instance, IStateMachineTarget, object>.State idle;

		// Token: 0x04007EC4 RID: 32452
		public GameStateMachine<ColdImmunityMonitor, ColdImmunityMonitor.Instance, IStateMachineTarget, object>.State exiting;

		// Token: 0x04007EC5 RID: 32453
		public GameStateMachine<ColdImmunityMonitor, ColdImmunityMonitor.Instance, IStateMachineTarget, object>.State resetChore;
	}

	// Token: 0x02001A20 RID: 6688
	public class IdleStates : GameStateMachine<ColdImmunityMonitor, ColdImmunityMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x04007EC6 RID: 32454
		public GameStateMachine<ColdImmunityMonitor, ColdImmunityMonitor.Instance, IStateMachineTarget, object>.State feelingFine;

		// Token: 0x04007EC7 RID: 32455
		public GameStateMachine<ColdImmunityMonitor, ColdImmunityMonitor.Instance, IStateMachineTarget, object>.State leftWithDesireToWarmupAfterBeingCold;
	}

	// Token: 0x02001A21 RID: 6689
	public new class Instance : GameStateMachine<ColdImmunityMonitor, ColdImmunityMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x0600A24E RID: 41550 RVA: 0x003A0D3D File Offset: 0x0039EF3D
		// (set) Token: 0x0600A24F RID: 41551 RVA: 0x003A0D45 File Offset: 0x0039EF45
		public ColdImmunityProvider.Instance NearestImmunityProvider { get; private set; }

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x0600A250 RID: 41552 RVA: 0x003A0D4E File Offset: 0x0039EF4E
		// (set) Token: 0x0600A251 RID: 41553 RVA: 0x003A0D56 File Offset: 0x0039EF56
		public int WarmUpCell { get; private set; }

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x0600A252 RID: 41554 RVA: 0x003A0D5F File Offset: 0x0039EF5F
		public float ColdCountdown
		{
			get
			{
				return base.smi.sm.coldCountdown.Get(this);
			}
		}

		// Token: 0x0600A253 RID: 41555 RVA: 0x003A0D77 File Offset: 0x0039EF77
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x0600A254 RID: 41556 RVA: 0x003A0D80 File Offset: 0x0039EF80
		public override void StartSM()
		{
			this.navigator = base.gameObject.GetComponent<Navigator>();
			base.StartSM();
		}

		// Token: 0x0600A255 RID: 41557 RVA: 0x003A0D9C File Offset: 0x0039EF9C
		public void UpdateWarmUpCell()
		{
			int myWorldId = this.navigator.GetMyWorldId();
			int num = Grid.InvalidCell;
			int num2 = int.MaxValue;
			ColdImmunityProvider.Instance instance = null;
			foreach (StateMachine.Instance instance2 in Components.EffectImmunityProviderStations.Items.FindAll((StateMachine.Instance t) => t is ColdImmunityProvider.Instance))
			{
				ColdImmunityProvider.Instance instance3 = instance2 as ColdImmunityProvider.Instance;
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
			this.WarmUpCell = num;
		}

		// Token: 0x04007ECA RID: 32458
		private Navigator navigator;
	}
}
