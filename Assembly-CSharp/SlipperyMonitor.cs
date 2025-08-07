using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000A0D RID: 2573
public class SlipperyMonitor : GameStateMachine<SlipperyMonitor, SlipperyMonitor.Instance, IStateMachineTarget, SlipperyMonitor.Def>
{
	// Token: 0x06004AE9 RID: 19177 RVA: 0x001B24A8 File Offset: 0x001B06A8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.safe;
		this.safe.EventTransition(GameHashes.NavigationCellChanged, this.unsafeCell, new StateMachine<SlipperyMonitor, SlipperyMonitor.Instance, IStateMachineTarget, SlipperyMonitor.Def>.Transition.ConditionCallback(SlipperyMonitor.IsStandingOnASlipperyCell));
		this.unsafeCell.EventTransition(GameHashes.NavigationCellChanged, this.safe, GameStateMachine<SlipperyMonitor, SlipperyMonitor.Instance, IStateMachineTarget, SlipperyMonitor.Def>.Not(new StateMachine<SlipperyMonitor, SlipperyMonitor.Instance, IStateMachineTarget, SlipperyMonitor.Def>.Transition.ConditionCallback(SlipperyMonitor.IsStandingOnASlipperyCell))).DefaultState(this.unsafeCell.atRisk);
		this.unsafeCell.atRisk.EventTransition(GameHashes.EquipmentChanged, this.unsafeCell.immune, new StateMachine<SlipperyMonitor, SlipperyMonitor.Instance, IStateMachineTarget, SlipperyMonitor.Def>.Transition.ConditionCallback(this.IsImmuneToSlipperySurfaces)).EventTransition(GameHashes.EffectAdded, this.unsafeCell.immune, new StateMachine<SlipperyMonitor, SlipperyMonitor.Instance, IStateMachineTarget, SlipperyMonitor.Def>.Transition.ConditionCallback(this.IsImmuneToSlipperySurfaces)).DefaultState(this.unsafeCell.atRisk.idle);
		this.unsafeCell.atRisk.idle.EventHandlerTransition(GameHashes.NavigationCellChanged, this.unsafeCell.atRisk.slip, new Func<SlipperyMonitor.Instance, object, bool>(SlipperyMonitor.RollDTwenty));
		this.unsafeCell.atRisk.slip.ToggleReactable(new Func<SlipperyMonitor.Instance, Reactable>(this.GetReactable)).ScheduleGoTo(8f, this.unsafeCell.atRisk.idle);
		this.unsafeCell.immune.EventTransition(GameHashes.EquipmentChanged, this.unsafeCell.atRisk, GameStateMachine<SlipperyMonitor, SlipperyMonitor.Instance, IStateMachineTarget, SlipperyMonitor.Def>.Not(new StateMachine<SlipperyMonitor, SlipperyMonitor.Instance, IStateMachineTarget, SlipperyMonitor.Def>.Transition.ConditionCallback(this.IsImmuneToSlipperySurfaces))).EventTransition(GameHashes.EffectRemoved, this.unsafeCell.atRisk, GameStateMachine<SlipperyMonitor, SlipperyMonitor.Instance, IStateMachineTarget, SlipperyMonitor.Def>.Not(new StateMachine<SlipperyMonitor, SlipperyMonitor.Instance, IStateMachineTarget, SlipperyMonitor.Def>.Transition.ConditionCallback(this.IsImmuneToSlipperySurfaces)));
	}

	// Token: 0x06004AEA RID: 19178 RVA: 0x001B264D File Offset: 0x001B084D
	public bool IsImmuneToSlipperySurfaces(SlipperyMonitor.Instance smi)
	{
		return smi.IsImmune;
	}

	// Token: 0x06004AEB RID: 19179 RVA: 0x001B2655 File Offset: 0x001B0855
	public Reactable GetReactable(SlipperyMonitor.Instance smi)
	{
		return smi.CreateReactable();
	}

	// Token: 0x06004AEC RID: 19180 RVA: 0x001B2660 File Offset: 0x001B0860
	private static bool IsStandingOnASlipperyCell(SlipperyMonitor.Instance smi)
	{
		int num = Grid.PosToCell(smi);
		int num2 = Grid.OffsetCell(num, 0, -1);
		return (Grid.IsValidCell(num) && Grid.Element[num].IsSlippery) || (Grid.IsValidCell(num2) && Grid.Element[num2].IsSolid && Grid.Element[num2].IsSlippery);
	}

	// Token: 0x06004AED RID: 19181 RVA: 0x001B26C2 File Offset: 0x001B08C2
	private static bool RollDTwenty(SlipperyMonitor.Instance smi, object o)
	{
		return global::UnityEngine.Random.value <= 0.05f;
	}

	// Token: 0x04003196 RID: 12694
	public const string EFFECT_NAME = "RecentlySlippedTracker";

	// Token: 0x04003197 RID: 12695
	public const float SLIP_FAIL_TIMEOUT = 8f;

	// Token: 0x04003198 RID: 12696
	public const float PROBABILITY_OF_SLIP = 0.05f;

	// Token: 0x04003199 RID: 12697
	public const float STRESS_DAMAGE = 3f;

	// Token: 0x0400319A RID: 12698
	public GameStateMachine<SlipperyMonitor, SlipperyMonitor.Instance, IStateMachineTarget, SlipperyMonitor.Def>.State safe;

	// Token: 0x0400319B RID: 12699
	public SlipperyMonitor.UnsafeCellState unsafeCell;

	// Token: 0x02001A9D RID: 6813
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001A9E RID: 6814
	public class UnsafeCellState : GameStateMachine<SlipperyMonitor, SlipperyMonitor.Instance, IStateMachineTarget, SlipperyMonitor.Def>.State
	{
		// Token: 0x04008047 RID: 32839
		public SlipperyMonitor.RiskStates atRisk;

		// Token: 0x04008048 RID: 32840
		public GameStateMachine<SlipperyMonitor, SlipperyMonitor.Instance, IStateMachineTarget, SlipperyMonitor.Def>.State immune;
	}

	// Token: 0x02001A9F RID: 6815
	public class RiskStates : GameStateMachine<SlipperyMonitor, SlipperyMonitor.Instance, IStateMachineTarget, SlipperyMonitor.Def>.State
	{
		// Token: 0x04008049 RID: 32841
		public GameStateMachine<SlipperyMonitor, SlipperyMonitor.Instance, IStateMachineTarget, SlipperyMonitor.Def>.State idle;

		// Token: 0x0400804A RID: 32842
		public GameStateMachine<SlipperyMonitor, SlipperyMonitor.Instance, IStateMachineTarget, SlipperyMonitor.Def>.State slip;
	}

	// Token: 0x02001AA0 RID: 6816
	public new class Instance : GameStateMachine<SlipperyMonitor, SlipperyMonitor.Instance, IStateMachineTarget, SlipperyMonitor.Def>.GameInstance
	{
		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x0600A44E RID: 42062 RVA: 0x003A5DC1 File Offset: 0x003A3FC1
		public bool IsImmune
		{
			get
			{
				return this.effects.HasEffect("RecentlySlippedTracker") || this.effects.HasImmunityTo(this.effect);
			}
		}

		// Token: 0x0600A44F RID: 42063 RVA: 0x003A5DE8 File Offset: 0x003A3FE8
		public Instance(IStateMachineTarget master, SlipperyMonitor.Def def)
			: base(master, def)
		{
			this.effects = base.GetComponent<Effects>();
			this.effect = Db.Get().effects.Get("RecentlySlippedTracker");
		}

		// Token: 0x0600A450 RID: 42064 RVA: 0x003A5E18 File Offset: 0x003A4018
		public SlipperyMonitor.SlipReactable CreateReactable()
		{
			return new SlipperyMonitor.SlipReactable(this);
		}

		// Token: 0x0400804B RID: 32843
		private Effect effect;

		// Token: 0x0400804C RID: 32844
		public Effects effects;
	}

	// Token: 0x02001AA1 RID: 6817
	public class SlipReactable : Reactable
	{
		// Token: 0x0600A451 RID: 42065 RVA: 0x003A5E20 File Offset: 0x003A4020
		public SlipReactable(SlipperyMonitor.Instance _smi)
			: base(_smi.gameObject, "Slip", Db.Get().ChoreTypes.Slip, 1, 1, false, 0f, 0f, 8f, 0f, ObjectLayer.NumLayers)
		{
			this.smi = _smi;
		}

		// Token: 0x0600A452 RID: 42066 RVA: 0x003A5E74 File Offset: 0x003A4074
		public override bool InternalCanBegin(GameObject new_reactor, Navigator.ActiveTransition transition)
		{
			if (this.reactor != null)
			{
				return false;
			}
			if (new_reactor == null)
			{
				return false;
			}
			if (this.gameObject != new_reactor)
			{
				return false;
			}
			if (this.smi == null)
			{
				return false;
			}
			Navigator component = new_reactor.GetComponent<Navigator>();
			return !(component == null) && component.CurrentNavType != NavType.Tube && component.CurrentNavType != NavType.Ladder && component.CurrentNavType != NavType.Pole;
		}

		// Token: 0x0600A453 RID: 42067 RVA: 0x003A5EE8 File Offset: 0x003A40E8
		protected override void InternalBegin()
		{
			this.startTime = Time.time;
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, DUPLICANTS.MODIFIERS.SLIPPED.NAME, this.gameObject.transform, 1.5f, false);
			KBatchedAnimController component = this.reactor.GetComponent<KBatchedAnimController>();
			component.AddAnimOverrides(Assets.GetAnim("anim_slip_kanim"), 1f);
			component.Play("slip_pre", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue("slip_loop", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue("slip_pst", KAnim.PlayMode.Once, 1f, 0f);
			this.reactor.GetComponent<KSelectable>().AddStatusItem(Db.Get().DuplicantStatusItems.Slippering, null);
		}

		// Token: 0x0600A454 RID: 42068 RVA: 0x003A5FC6 File Offset: 0x003A41C6
		public override void Update(float dt)
		{
			if (Time.time - this.startTime > 4.3f)
			{
				base.Cleanup();
				this.ApplyStress();
				this.ApplyTrackerEffect();
			}
		}

		// Token: 0x0600A455 RID: 42069 RVA: 0x003A5FED File Offset: 0x003A41ED
		public void ApplyTrackerEffect()
		{
			this.smi.effects.Add("RecentlySlippedTracker", true);
		}

		// Token: 0x0600A456 RID: 42070 RVA: 0x003A6008 File Offset: 0x003A4208
		private void ApplyStress()
		{
			this.smi.master.gameObject.GetAmounts().Get(Db.Get().Amounts.Stress.Id).ApplyDelta(3f);
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, 3f.ToString() + "% " + Db.Get().Amounts.Stress.Name, this.gameObject.transform, 1.5f, false);
			ReportManager.Instance.ReportValue(ReportManager.ReportType.StressDelta, 3f, DUPLICANTS.MODIFIERS.SLIPPED.NAME, this.gameObject.GetProperName());
		}

		// Token: 0x0600A457 RID: 42071 RVA: 0x003A60C4 File Offset: 0x003A42C4
		protected override void InternalEnd()
		{
			if (this.reactor != null)
			{
				KBatchedAnimController component = this.reactor.GetComponent<KBatchedAnimController>();
				if (component != null)
				{
					this.reactor.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().DuplicantStatusItems.Slippering, false);
					component.RemoveAnimOverrides(Assets.GetAnim("anim_slip_kanim"));
				}
			}
		}

		// Token: 0x0600A458 RID: 42072 RVA: 0x003A612A File Offset: 0x003A432A
		protected override void InternalCleanup()
		{
		}

		// Token: 0x0400804D RID: 32845
		private SlipperyMonitor.Instance smi;

		// Token: 0x0400804E RID: 32846
		private float startTime;

		// Token: 0x0400804F RID: 32847
		private const string ANIM_FILE_NAME = "anim_slip_kanim";

		// Token: 0x04008050 RID: 32848
		private const float DURATION = 4.3f;
	}
}
