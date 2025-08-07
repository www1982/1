using System;
using UnityEngine;

// Token: 0x0200046A RID: 1130
public class RationalAi : GameStateMachine<RationalAi, RationalAi.Instance>
{
	// Token: 0x060017D2 RID: 6098 RVA: 0x00083EF8 File Offset: 0x000820F8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleStateMachine((RationalAi.Instance smi) => new DeathMonitor.Instance(smi.master, new DeathMonitor.Def())).Enter(delegate(RationalAi.Instance smi)
		{
			if (smi.HasTag(GameTags.Dead))
			{
				smi.GoTo(this.dead);
				return;
			}
			smi.GoTo(this.alive);
		});
		this.alive.TagTransition(GameTags.Dead, this.dead, false).Exit(new StateMachine<RationalAi, RationalAi.Instance, IStateMachineTarget, object>.State.Callback(RationalAi.IncreaseDeathCounterIfDying)).ToggleStateMachineList(new Func<RationalAi.Instance, Func<RationalAi.Instance, StateMachine.Instance>[]>(RationalAi.GetStateMachinesToRunWhenAlive));
		this.dead.ToggleStateMachine((RationalAi.Instance smi) => new FallWhenDeadMonitor.Instance(smi.master)).ToggleBrain("dead").Enter("RefreshUserMenu", delegate(RationalAi.Instance smi)
		{
			smi.RefreshUserMenu();
		})
			.Enter("DropStorage", delegate(RationalAi.Instance smi)
			{
				smi.GetComponent<Storage>().DropAll(false, false, default(Vector3), true, null);
			});
	}

	// Token: 0x060017D3 RID: 6099 RVA: 0x0008400A File Offset: 0x0008220A
	public static Func<RationalAi.Instance, StateMachine.Instance>[] GetStateMachinesToRunWhenAlive(RationalAi.Instance smi)
	{
		return smi.stateMachinesToRunWhenAlive;
	}

	// Token: 0x060017D4 RID: 6100 RVA: 0x00084012 File Offset: 0x00082212
	private static void IncreaseDeathCounterIfDying(RationalAi.Instance smi)
	{
		if (smi.HasTag(GameTags.Dead))
		{
			SaveGame.Instance.ColonyAchievementTracker.deadDupeCounter++;
		}
	}

	// Token: 0x04000DC0 RID: 3520
	public GameStateMachine<RationalAi, RationalAi.Instance, IStateMachineTarget, object>.State alive;

	// Token: 0x04000DC1 RID: 3521
	public GameStateMachine<RationalAi, RationalAi.Instance, IStateMachineTarget, object>.State dead;

	// Token: 0x02001250 RID: 4688
	public new class Instance : GameStateMachine<RationalAi, RationalAi.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x060085D2 RID: 34258 RVA: 0x0033997C File Offset: 0x00337B7C
		public Instance(IStateMachineTarget master, Tag minionModel)
			: base(master)
		{
			this.MinionModel = minionModel;
			ChoreConsumer component = base.GetComponent<ChoreConsumer>();
			component.AddUrge(Db.Get().Urges.EmoteHighPriority);
			component.AddUrge(Db.Get().Urges.EmoteIdle);
			component.prioritizeBrainIfNoChore = true;
		}

		// Token: 0x060085D3 RID: 34259 RVA: 0x003399CD File Offset: 0x00337BCD
		public void RefreshUserMenu()
		{
			Game.Instance.userMenu.Refresh(base.master.gameObject);
		}

		// Token: 0x0400659F RID: 26015
		public Tag MinionModel;

		// Token: 0x040065A0 RID: 26016
		public Func<RationalAi.Instance, StateMachine.Instance>[] stateMachinesToRunWhenAlive;
	}
}
