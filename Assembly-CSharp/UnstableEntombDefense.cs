using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020001B7 RID: 439
public class UnstableEntombDefense : GameStateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>
{
	// Token: 0x060008C6 RID: 2246 RVA: 0x0003B400 File Offset: 0x00039600
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.disabled;
		this.disabled.EventTransition(GameHashes.Died, this.dead, null).ParamTransition<bool>(this.Active, this.active, GameStateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>.IsTrue);
		this.active.EventTransition(GameHashes.Died, this.dead, null).ParamTransition<bool>(this.Active, this.disabled, GameStateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>.IsFalse).DefaultState(this.active.safe);
		this.active.safe.DefaultState(this.active.safe.idle);
		this.active.safe.idle.ParamTransition<float>(this.TimeBeforeNextReaction, this.active.threatened, (UnstableEntombDefense.Instance smi, float p) => GameStateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>.IsGTZero(smi, p) && UnstableEntombDefense.IsEntombedByUnstable(smi)).EventTransition(GameHashes.EntombedChanged, this.active.safe.newThreat, new StateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>.Transition.ConditionCallback(UnstableEntombDefense.IsEntombedByUnstable));
		this.active.safe.newThreat.Enter(new StateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>.State.Callback(UnstableEntombDefense.ResetCooldown)).GoTo(this.active.threatened);
		this.active.threatened.EventTransition(GameHashes.Died, this.dead, null).Exit(new StateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>.State.Callback(UnstableEntombDefense.ResetCooldown)).EventTransition(GameHashes.EntombedChanged, this.active.safe, GameStateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>.Not(new StateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>.Transition.ConditionCallback(UnstableEntombDefense.IsEntombedByUnstable)))
			.DefaultState(this.active.threatened.inCooldown);
		this.active.threatened.inCooldown.ParamTransition<float>(this.TimeBeforeNextReaction, this.active.threatened.react, GameStateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>.IsLTEZero).Update(new Action<UnstableEntombDefense.Instance, float>(UnstableEntombDefense.CooldownTick), UpdateRate.SIM_200ms, false);
		this.active.threatened.react.TriggerOnEnter(GameHashes.EntombDefenseReactionBegins, null).PlayAnim((UnstableEntombDefense.Instance smi) => smi.UnentombAnimName, KAnim.PlayMode.Once).OnAnimQueueComplete(this.active.threatened.complete)
			.ScheduleGoTo(2f, this.active.threatened.complete);
		this.active.threatened.complete.TriggerOnEnter(GameHashes.EntombDefenseReact, null).Enter(new StateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>.State.Callback(UnstableEntombDefense.AttemptToBreakFree)).Enter(new StateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>.State.Callback(UnstableEntombDefense.ResetCooldown))
			.GoTo(this.active.threatened.inCooldown);
		this.dead.DoNothing();
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x0003B6C3 File Offset: 0x000398C3
	public static void ResetCooldown(UnstableEntombDefense.Instance smi)
	{
		smi.sm.TimeBeforeNextReaction.Set(smi.def.Cooldown, smi, false);
	}

	// Token: 0x060008C8 RID: 2248 RVA: 0x0003B6E3 File Offset: 0x000398E3
	public static bool IsEntombedByUnstable(UnstableEntombDefense.Instance smi)
	{
		return smi.IsEntombed && smi.IsInPressenceOfUnstableSolids();
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x0003B6F5 File Offset: 0x000398F5
	public static void AttemptToBreakFree(UnstableEntombDefense.Instance smi)
	{
		smi.AttackUnstableCells();
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x0003B700 File Offset: 0x00039900
	public static void CooldownTick(UnstableEntombDefense.Instance smi, float dt)
	{
		float num = smi.RemainingCooldown - dt;
		smi.sm.TimeBeforeNextReaction.Set(num, smi, false);
	}

	// Token: 0x04000674 RID: 1652
	public UnstableEntombDefense.ActiveState active;

	// Token: 0x04000675 RID: 1653
	public GameStateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>.State disabled;

	// Token: 0x04000676 RID: 1654
	public GameStateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>.State dead;

	// Token: 0x04000677 RID: 1655
	public StateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>.FloatParameter TimeBeforeNextReaction;

	// Token: 0x04000678 RID: 1656
	public StateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>.BoolParameter Active = new StateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>.BoolParameter(true);

	// Token: 0x0200117A RID: 4474
	public class Def : StateMachine.BaseDef, IGameObjectEffectDescriptor
	{
		// Token: 0x060082AD RID: 33453 RVA: 0x00332504 File Offset: 0x00330704
		public List<Descriptor> GetDescriptors(GameObject go)
		{
			List<Descriptor> list = new List<Descriptor>();
			UnstableEntombDefense.Instance smi = go.GetSMI<UnstableEntombDefense.Instance>();
			if (smi != null)
			{
				Descriptor stateDescriptor = smi.GetStateDescriptor();
				if (stateDescriptor.type == Descriptor.DescriptorType.Effect)
				{
					list.Add(stateDescriptor);
				}
			}
			return list;
		}

		// Token: 0x04006347 RID: 25415
		public float Cooldown = 5f;

		// Token: 0x04006348 RID: 25416
		public string defaultAnimName = "";
	}

	// Token: 0x0200117B RID: 4475
	public class SafeStates : GameStateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>.State
	{
		// Token: 0x04006349 RID: 25417
		public GameStateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>.State idle;

		// Token: 0x0400634A RID: 25418
		public GameStateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>.State newThreat;
	}

	// Token: 0x0200117C RID: 4476
	public class ThreatenedStates : GameStateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>.State
	{
		// Token: 0x0400634B RID: 25419
		public GameStateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>.State inCooldown;

		// Token: 0x0400634C RID: 25420
		public GameStateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>.State react;

		// Token: 0x0400634D RID: 25421
		public GameStateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>.State complete;
	}

	// Token: 0x0200117D RID: 4477
	public class ActiveState : GameStateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>.State
	{
		// Token: 0x0400634E RID: 25422
		public UnstableEntombDefense.SafeStates safe;

		// Token: 0x0400634F RID: 25423
		public UnstableEntombDefense.ThreatenedStates threatened;
	}

	// Token: 0x0200117E RID: 4478
	public new class Instance : GameStateMachine<UnstableEntombDefense, UnstableEntombDefense.Instance, IStateMachineTarget, UnstableEntombDefense.Def>.GameInstance
	{
		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x060082B2 RID: 33458 RVA: 0x0033256F File Offset: 0x0033076F
		public float RemainingCooldown
		{
			get
			{
				return base.sm.TimeBeforeNextReaction.Get(this);
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x060082B3 RID: 33459 RVA: 0x00332582 File Offset: 0x00330782
		public bool IsEntombed
		{
			get
			{
				return this.entombVulnerable.GetEntombed;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x060082B4 RID: 33460 RVA: 0x0033258F File Offset: 0x0033078F
		public bool IsActive
		{
			get
			{
				return base.sm.Active.Get(this);
			}
		}

		// Token: 0x060082B5 RID: 33461 RVA: 0x003325A2 File Offset: 0x003307A2
		public Instance(IStateMachineTarget master, UnstableEntombDefense.Def def)
			: base(master, def)
		{
			this.UnentombAnimName = ((this.UnentombAnimName == null) ? def.defaultAnimName : this.UnentombAnimName);
		}

		// Token: 0x060082B6 RID: 33462 RVA: 0x003325C8 File Offset: 0x003307C8
		public bool IsInPressenceOfUnstableSolids()
		{
			int num = Grid.PosToCell(this);
			CellOffset[] occupiedCellsOffsets = this.occupyArea.OccupiedCellsOffsets;
			for (int i = 0; i < occupiedCellsOffsets.Length; i++)
			{
				int num2 = Grid.OffsetCell(num, occupiedCellsOffsets[i]);
				if (Grid.IsValidCell(num2) && Grid.Solid[num2] && Grid.Element[num2].IsUnstable)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060082B7 RID: 33463 RVA: 0x0033262C File Offset: 0x0033082C
		public void AttackUnstableCells()
		{
			int num = Grid.PosToCell(this);
			CellOffset[] occupiedCellsOffsets = this.occupyArea.OccupiedCellsOffsets;
			for (int i = 0; i < occupiedCellsOffsets.Length; i++)
			{
				int num2 = Grid.OffsetCell(num, occupiedCellsOffsets[i]);
				if (Grid.IsValidCell(num2) && Grid.Solid[num2] && Grid.Element[num2].IsUnstable)
				{
					SimMessages.Dig(num2, -1, false);
				}
			}
		}

		// Token: 0x060082B8 RID: 33464 RVA: 0x00332693 File Offset: 0x00330893
		public void SetActive(bool active)
		{
			base.sm.Active.Set(active, this, false);
		}

		// Token: 0x060082B9 RID: 33465 RVA: 0x003326AC File Offset: 0x003308AC
		public Descriptor GetStateDescriptor()
		{
			if (base.IsInsideState(base.sm.disabled))
			{
				return new Descriptor(UI.BUILDINGEFFECTS.UNSTABLEENTOMBDEFENSEOFF, UI.BUILDINGEFFECTS.TOOLTIPS.UNSTABLEENTOMBDEFENSEOFF, Descriptor.DescriptorType.Effect, false);
			}
			if (base.IsInsideState(base.sm.active.safe))
			{
				return new Descriptor(UI.BUILDINGEFFECTS.UNSTABLEENTOMBDEFENSEREADY, UI.BUILDINGEFFECTS.TOOLTIPS.UNSTABLEENTOMBDEFENSEREADY, Descriptor.DescriptorType.Effect, false);
			}
			if (base.IsInsideState(base.sm.active.threatened.inCooldown))
			{
				return new Descriptor(UI.BUILDINGEFFECTS.UNSTABLEENTOMBDEFENSETHREATENED, UI.BUILDINGEFFECTS.TOOLTIPS.UNSTABLEENTOMBDEFENSETHREATENED, Descriptor.DescriptorType.Effect, false);
			}
			if (base.IsInsideState(base.sm.active.threatened.react))
			{
				return new Descriptor(UI.BUILDINGEFFECTS.UNSTABLEENTOMBDEFENSEREACTING, UI.BUILDINGEFFECTS.TOOLTIPS.UNSTABLEENTOMBDEFENSEREACTING, Descriptor.DescriptorType.Effect, false);
			}
			return new Descriptor
			{
				type = Descriptor.DescriptorType.Detail
			};
		}

		// Token: 0x04006350 RID: 25424
		public string UnentombAnimName;

		// Token: 0x04006351 RID: 25425
		[MyCmpGet]
		private EntombVulnerable entombVulnerable;

		// Token: 0x04006352 RID: 25426
		[MyCmpGet]
		private OccupyArea occupyArea;
	}
}
