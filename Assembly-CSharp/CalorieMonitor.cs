using System;
using Klei.AI;
using TUNING;

// Token: 0x020009DB RID: 2523
public class CalorieMonitor : GameStateMachine<CalorieMonitor, CalorieMonitor.Instance>
{
	// Token: 0x060049F1 RID: 18929 RVA: 0x001AC318 File Offset: 0x001AA518
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.satisfied.Transition(this.hungry, (CalorieMonitor.Instance smi) => smi.IsHungry(), UpdateRate.SIM_200ms);
		this.hungry.DefaultState(this.hungry.normal).Transition(this.satisfied, (CalorieMonitor.Instance smi) => smi.IsSatisfied(), UpdateRate.SIM_200ms).EventTransition(GameHashes.BeginChore, this.eating, (CalorieMonitor.Instance smi) => smi.IsEating());
		this.hungry.working.EventTransition(GameHashes.ScheduleBlocksChanged, this.hungry.normal, (CalorieMonitor.Instance smi) => smi.IsEatTime()).Transition(this.hungry.starving, (CalorieMonitor.Instance smi) => smi.IsStarving(), UpdateRate.SIM_200ms).ToggleStatusItem(Db.Get().DuplicantStatusItems.Hungry, null);
		this.hungry.normal.EventTransition(GameHashes.ScheduleBlocksChanged, this.hungry.working, (CalorieMonitor.Instance smi) => !smi.IsEatTime()).Transition(this.hungry.starving, (CalorieMonitor.Instance smi) => smi.IsStarving(), UpdateRate.SIM_200ms).ToggleStatusItem(Db.Get().DuplicantStatusItems.Hungry, null)
			.ToggleUrge(Db.Get().Urges.Eat)
			.ToggleExpression(Db.Get().Expressions.Hungry, null)
			.ToggleThought(Db.Get().Thoughts.Starving, null);
		this.hungry.starving.Transition(this.hungry.normal, (CalorieMonitor.Instance smi) => !smi.IsStarving(), UpdateRate.SIM_200ms).Transition(this.depleted, (CalorieMonitor.Instance smi) => smi.IsDepleted(), UpdateRate.SIM_200ms).ToggleStatusItem(Db.Get().DuplicantStatusItems.Starving, null)
			.ToggleUrge(Db.Get().Urges.Eat)
			.ToggleExpression(Db.Get().Expressions.Hungry, null)
			.ToggleThought(Db.Get().Thoughts.Starving, null);
		this.eating.EventTransition(GameHashes.EndChore, this.satisfied, (CalorieMonitor.Instance smi) => !smi.IsEating());
		this.depleted.ToggleTag(GameTags.CaloriesDepleted).Enter(delegate(CalorieMonitor.Instance smi)
		{
			smi.Kill();
		});
	}

	// Token: 0x040030B9 RID: 12473
	public GameStateMachine<CalorieMonitor, CalorieMonitor.Instance, IStateMachineTarget, object>.State satisfied;

	// Token: 0x040030BA RID: 12474
	public CalorieMonitor.HungryState hungry;

	// Token: 0x040030BB RID: 12475
	public GameStateMachine<CalorieMonitor, CalorieMonitor.Instance, IStateMachineTarget, object>.State eating;

	// Token: 0x040030BC RID: 12476
	public GameStateMachine<CalorieMonitor, CalorieMonitor.Instance, IStateMachineTarget, object>.State incapacitated;

	// Token: 0x040030BD RID: 12477
	public GameStateMachine<CalorieMonitor, CalorieMonitor.Instance, IStateMachineTarget, object>.State depleted;

	// Token: 0x02001A1A RID: 6682
	public class HungryState : GameStateMachine<CalorieMonitor, CalorieMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x04007EAF RID: 32431
		public GameStateMachine<CalorieMonitor, CalorieMonitor.Instance, IStateMachineTarget, object>.State working;

		// Token: 0x04007EB0 RID: 32432
		public GameStateMachine<CalorieMonitor, CalorieMonitor.Instance, IStateMachineTarget, object>.State normal;

		// Token: 0x04007EB1 RID: 32433
		public GameStateMachine<CalorieMonitor, CalorieMonitor.Instance, IStateMachineTarget, object>.State starving;
	}

	// Token: 0x02001A1B RID: 6683
	public new class Instance : GameStateMachine<CalorieMonitor, CalorieMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600A22E RID: 41518 RVA: 0x003A0AE1 File Offset: 0x0039ECE1
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.calories = Db.Get().Amounts.Calories.Lookup(base.gameObject);
		}

		// Token: 0x0600A22F RID: 41519 RVA: 0x003A0B0A File Offset: 0x0039ED0A
		private float GetCalories0to1()
		{
			return this.calories.value / this.calories.GetMax();
		}

		// Token: 0x0600A230 RID: 41520 RVA: 0x003A0B23 File Offset: 0x0039ED23
		public bool IsEatTime()
		{
			return base.master.GetComponent<Schedulable>().IsAllowed(Db.Get().ScheduleBlockTypes.Eat);
		}

		// Token: 0x0600A231 RID: 41521 RVA: 0x003A0B44 File Offset: 0x0039ED44
		public bool IsHungry()
		{
			return this.GetCalories0to1() < DUPLICANTSTATS.STANDARD.BaseStats.HUNGRY_THRESHOLD;
		}

		// Token: 0x0600A232 RID: 41522 RVA: 0x003A0B5D File Offset: 0x0039ED5D
		public bool IsStarving()
		{
			return this.GetCalories0to1() < DUPLICANTSTATS.STANDARD.BaseStats.STARVING_THRESHOLD;
		}

		// Token: 0x0600A233 RID: 41523 RVA: 0x003A0B76 File Offset: 0x0039ED76
		public bool IsSatisfied()
		{
			return this.GetCalories0to1() > DUPLICANTSTATS.STANDARD.BaseStats.SATISFIED_THRESHOLD;
		}

		// Token: 0x0600A234 RID: 41524 RVA: 0x003A0B90 File Offset: 0x0039ED90
		public bool IsEating()
		{
			ChoreDriver component = base.master.GetComponent<ChoreDriver>();
			return component.HasChore() && component.GetCurrentChore().choreType.urge == Db.Get().Urges.Eat;
		}

		// Token: 0x0600A235 RID: 41525 RVA: 0x003A0BD4 File Offset: 0x0039EDD4
		public bool IsDepleted()
		{
			return this.calories.value <= 0f;
		}

		// Token: 0x0600A236 RID: 41526 RVA: 0x003A0BEB File Offset: 0x0039EDEB
		public bool ShouldExitInfirmary()
		{
			return !this.IsStarving();
		}

		// Token: 0x0600A237 RID: 41527 RVA: 0x003A0BF6 File Offset: 0x0039EDF6
		public void Kill()
		{
			if (base.gameObject.GetSMI<DeathMonitor.Instance>() != null)
			{
				base.gameObject.GetSMI<DeathMonitor.Instance>().Kill(Db.Get().Deaths.Starvation);
			}
		}

		// Token: 0x04007EB2 RID: 32434
		public AmountInstance calories;
	}
}
