using System;
using UnityEngine;

// Token: 0x02000588 RID: 1416
public class AliveEntityPoker : GameStateMachine<AliveEntityPoker, AliveEntityPoker.Instance, IStateMachineTarget, AliveEntityPoker.Def>
{
	// Token: 0x06002059 RID: 8281 RVA: 0x000BAC4C File Offset: 0x000B8E4C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.Never;
		default_state = this.approach;
		this.root.Enter(new StateMachine<AliveEntityPoker, AliveEntityPoker.Instance, IStateMachineTarget, AliveEntityPoker.Def>.State.Callback(AliveEntityPoker.RefreshTarget)).TagTransition(AliveEntityPoker.BehaviourTag, null, true);
		this.approach.InitializeStates(this.poker, this.victim, (AliveEntityPoker.Instance smi) => smi.VictimPokeOffsets, this.poke, this.failed, null).ToggleMainStatusItem(new Func<AliveEntityPoker.Instance, StatusItem>(AliveEntityPoker.GetGoingToPokeStatusItem), null);
		this.poke.ToggleAnims((AliveEntityPoker.Instance smi) => smi.def.PokeAnimFileName).OnTargetLost(this.victim, null).DefaultState(this.poke.pre)
			.ToggleMainStatusItem(new Func<AliveEntityPoker.Instance, StatusItem>(AliveEntityPoker.GetPokingStatusItem), null);
		this.poke.pre.PlayAnim((AliveEntityPoker.Instance smi) => smi.def.PokeAnim_Pre, KAnim.PlayMode.Once).OnAnimQueueComplete(this.poke.loop);
		this.poke.loop.PlayAnim((AliveEntityPoker.Instance smi) => smi.def.PokeAnim_Loop, KAnim.PlayMode.Once).OnAnimQueueComplete(this.poke.pst);
		this.poke.pst.PlayAnim((AliveEntityPoker.Instance smi) => smi.def.PokeAnim_Pst, KAnim.PlayMode.Once).OnAnimQueueComplete(this.complete);
		this.complete.TriggerOnEnter(GameHashes.EntityPoked, (AliveEntityPoker.Instance smi) => smi.CurrentVictim).BehaviourComplete(AliveEntityPoker.BehaviourTag, false);
		this.failed.Target(this.poker).TriggerOnEnter(GameHashes.TargetLost, null).EnterGoTo(null);
	}

	// Token: 0x0600205A RID: 8282 RVA: 0x000BAE57 File Offset: 0x000B9057
	public static StatusItem GetGoingToPokeStatusItem(AliveEntityPoker.Instance smi)
	{
		return AliveEntityPoker.GetStatusItem(smi, smi.def.statusItemSTR_goingToPoke);
	}

	// Token: 0x0600205B RID: 8283 RVA: 0x000BAE6A File Offset: 0x000B906A
	public static StatusItem GetPokingStatusItem(AliveEntityPoker.Instance smi)
	{
		return AliveEntityPoker.GetStatusItem(smi, smi.def.statusItemSTR_poking);
	}

	// Token: 0x0600205C RID: 8284 RVA: 0x000BAE80 File Offset: 0x000B9080
	private static StatusItem GetStatusItem(AliveEntityPoker.Instance smi, string address)
	{
		string text = Strings.Get(address + ".NAME");
		string text2 = Strings.Get(address + ".TOOLTIP");
		return new StatusItem(smi.GetCurrentState().longName, text, text2, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, true, null);
	}

	// Token: 0x0600205D RID: 8285 RVA: 0x000BAEE3 File Offset: 0x000B90E3
	public static void ClearPreviousVictim(AliveEntityPoker.Instance smi)
	{
		smi.sm.victim.Set(null, smi);
	}

	// Token: 0x0600205E RID: 8286 RVA: 0x000BAEF8 File Offset: 0x000B90F8
	public static void RefreshTarget(AliveEntityPoker.Instance smi)
	{
		PokeMonitor.Instance smi2 = smi.GetSMI<PokeMonitor.Instance>();
		smi.sm.victim.Set(smi2.Target, smi, false);
		smi.VictimPokeOffsets = smi2.TargetOffsets;
	}

	// Token: 0x040012D6 RID: 4822
	public static readonly Tag BehaviourTag = GameTags.Creatures.UrgeToPoke;

	// Token: 0x040012D7 RID: 4823
	public GameStateMachine<AliveEntityPoker, AliveEntityPoker.Instance, IStateMachineTarget, AliveEntityPoker.Def>.ApproachSubState<Pickupable> approach;

	// Token: 0x040012D8 RID: 4824
	public AliveEntityPoker.PokeStates poke;

	// Token: 0x040012D9 RID: 4825
	public GameStateMachine<AliveEntityPoker, AliveEntityPoker.Instance, IStateMachineTarget, AliveEntityPoker.Def>.State complete;

	// Token: 0x040012DA RID: 4826
	public GameStateMachine<AliveEntityPoker, AliveEntityPoker.Instance, IStateMachineTarget, AliveEntityPoker.Def>.State failed;

	// Token: 0x040012DB RID: 4827
	public StateMachine<AliveEntityPoker, AliveEntityPoker.Instance, IStateMachineTarget, AliveEntityPoker.Def>.TargetParameter poker;

	// Token: 0x040012DC RID: 4828
	public StateMachine<AliveEntityPoker, AliveEntityPoker.Instance, IStateMachineTarget, AliveEntityPoker.Def>.TargetParameter victim;

	// Token: 0x020013D5 RID: 5077
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006AC4 RID: 27332
		public string PokeAnimFileName;

		// Token: 0x04006AC5 RID: 27333
		public string PokeAnim_Pre;

		// Token: 0x04006AC6 RID: 27334
		public string PokeAnim_Loop;

		// Token: 0x04006AC7 RID: 27335
		public string PokeAnim_Pst;

		// Token: 0x04006AC8 RID: 27336
		public string statusItemSTR_goingToPoke;

		// Token: 0x04006AC9 RID: 27337
		public string statusItemSTR_poking;
	}

	// Token: 0x020013D6 RID: 5078
	public class PokeStates : GameStateMachine<AliveEntityPoker, AliveEntityPoker.Instance, IStateMachineTarget, AliveEntityPoker.Def>.State
	{
		// Token: 0x04006ACA RID: 27338
		public GameStateMachine<AliveEntityPoker, AliveEntityPoker.Instance, IStateMachineTarget, AliveEntityPoker.Def>.State pre;

		// Token: 0x04006ACB RID: 27339
		public GameStateMachine<AliveEntityPoker, AliveEntityPoker.Instance, IStateMachineTarget, AliveEntityPoker.Def>.State loop;

		// Token: 0x04006ACC RID: 27340
		public GameStateMachine<AliveEntityPoker, AliveEntityPoker.Instance, IStateMachineTarget, AliveEntityPoker.Def>.State pst;
	}

	// Token: 0x020013D7 RID: 5079
	public new class Instance : GameStateMachine<AliveEntityPoker, AliveEntityPoker.Instance, IStateMachineTarget, AliveEntityPoker.Def>.GameInstance
	{
		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06008B84 RID: 35716 RVA: 0x00352F38 File Offset: 0x00351138
		public GameObject CurrentVictim
		{
			get
			{
				return base.sm.victim.Get(this);
			}
		}

		// Token: 0x06008B85 RID: 35717 RVA: 0x00352F4C File Offset: 0x0035114C
		public Instance(Chore<AliveEntityPoker.Instance> chore, AliveEntityPoker.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.UrgeToPoke);
			base.sm.poker.Set(base.smi.gameObject, base.smi, false);
		}

		// Token: 0x04006ACD RID: 27341
		public CellOffset[] VictimPokeOffsets;
	}
}
