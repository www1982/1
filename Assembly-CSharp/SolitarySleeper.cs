using System;

// Token: 0x02000A2E RID: 2606
[SkipSaveFileSerialization]
public class SolitarySleeper : StateMachineComponent<SolitarySleeper.StatesInstance>
{
	// Token: 0x06004BA3 RID: 19363 RVA: 0x001B6B47 File Offset: 0x001B4D47
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x06004BA4 RID: 19364 RVA: 0x001B6B54 File Offset: 0x001B4D54
	protected bool IsUncomfortable()
	{
		if (!base.gameObject.GetSMI<StaminaMonitor.Instance>().IsSleeping())
		{
			return false;
		}
		int num = 5;
		bool flag = true;
		bool flag2 = true;
		int num2 = Grid.PosToCell(base.gameObject);
		for (int i = 1; i < num; i++)
		{
			int num3 = Grid.OffsetCell(num2, i, 0);
			int num4 = Grid.OffsetCell(num2, -i, 0);
			if (Grid.Solid[num4])
			{
				flag = false;
			}
			if (Grid.Solid[num3])
			{
				flag2 = false;
			}
			foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.Items)
			{
				if (flag && Grid.PosToCell(minionIdentity.gameObject) == num4)
				{
					return true;
				}
				if (flag2 && Grid.PosToCell(minionIdentity.gameObject) == num3)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x02001AF2 RID: 6898
	public class StatesInstance : GameStateMachine<SolitarySleeper.States, SolitarySleeper.StatesInstance, SolitarySleeper, object>.GameInstance
	{
		// Token: 0x0600A5A1 RID: 42401 RVA: 0x003A980A File Offset: 0x003A7A0A
		public StatesInstance(SolitarySleeper master)
			: base(master)
		{
		}
	}

	// Token: 0x02001AF3 RID: 6899
	public class States : GameStateMachine<SolitarySleeper.States, SolitarySleeper.StatesInstance, SolitarySleeper>
	{
		// Token: 0x0600A5A2 RID: 42402 RVA: 0x003A9814 File Offset: 0x003A7A14
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.satisfied;
			this.root.TagTransition(GameTags.Dead, null, false).EventTransition(GameHashes.NewDay, this.satisfied, null).Update("SolitarySleeperCheck", delegate(SolitarySleeper.StatesInstance smi, float dt)
			{
				if (smi.master.IsUncomfortable())
				{
					if (smi.GetCurrentState() != this.suffering)
					{
						smi.GoTo(this.suffering);
						return;
					}
				}
				else if (smi.GetCurrentState() != this.satisfied)
				{
					smi.GoTo(this.satisfied);
				}
			}, UpdateRate.SIM_4000ms, false);
			this.suffering.AddEffect("PeopleTooCloseWhileSleeping").ToggleExpression(Db.Get().Expressions.Uncomfortable, null).Update("PeopleTooCloseSleepFail", delegate(SolitarySleeper.StatesInstance smi, float dt)
			{
				smi.master.gameObject.Trigger(1338475637, this);
			}, UpdateRate.SIM_1000ms, false);
			this.satisfied.DoNothing();
		}

		// Token: 0x04008163 RID: 33123
		public GameStateMachine<SolitarySleeper.States, SolitarySleeper.StatesInstance, SolitarySleeper, object>.State satisfied;

		// Token: 0x04008164 RID: 33124
		public GameStateMachine<SolitarySleeper.States, SolitarySleeper.StatesInstance, SolitarySleeper, object>.State suffering;
	}
}
