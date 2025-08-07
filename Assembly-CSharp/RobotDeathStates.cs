using System;
using STRINGS;

// Token: 0x02000105 RID: 261
public class RobotDeathStates : GameStateMachine<RobotDeathStates, RobotDeathStates.Instance, IStateMachineTarget, RobotDeathStates.Def>
{
	// Token: 0x060004B0 RID: 1200 RVA: 0x000265BC File Offset: 0x000247BC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.loop;
		GameStateMachine<RobotDeathStates, RobotDeathStates.Instance, IStateMachineTarget, RobotDeathStates.Def>.State state = this.loop;
		string text = CREATURES.STATUSITEMS.DEAD.NAME;
		string text2 = CREATURES.STATUSITEMS.DEAD.TOOLTIP;
		string text3 = "";
		StatusItem.IconType iconType = StatusItem.IconType.Info;
		NotificationType notificationType = NotificationType.Neutral;
		bool flag = false;
		StatusItemCategory main = Db.Get().StatusItemCategories.Main;
		state.ToggleStatusItem(text, text2, text3, iconType, notificationType, flag, default(HashedString), 129022, null, null, main).PlayAnim((RobotDeathStates.Instance smi) => smi.def.deathAnim, KAnim.PlayMode.Once).OnAnimQueueComplete(this.pst);
		this.pst.TriggerOnEnter(GameHashes.DeathAnimComplete, null).TriggerOnEnter(GameHashes.Died, (RobotDeathStates.Instance smi) => smi.gameObject).BehaviourComplete(GameTags.Creatures.Die, false);
	}

	// Token: 0x04000361 RID: 865
	private GameStateMachine<RobotDeathStates, RobotDeathStates.Instance, IStateMachineTarget, RobotDeathStates.Def>.State loop;

	// Token: 0x04000362 RID: 866
	private GameStateMachine<RobotDeathStates, RobotDeathStates.Instance, IStateMachineTarget, RobotDeathStates.Def>.State pst;

	// Token: 0x02001136 RID: 4406
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006246 RID: 25158
		public string deathAnim = "death";
	}

	// Token: 0x02001137 RID: 4407
	public new class Instance : GameStateMachine<RobotDeathStates, RobotDeathStates.Instance, IStateMachineTarget, RobotDeathStates.Def>.GameInstance
	{
		// Token: 0x060081CD RID: 33229 RVA: 0x0032FE34 File Offset: 0x0032E034
		public Instance(Chore<RobotDeathStates.Instance> chore, RobotDeathStates.Def def)
			: base(chore, def)
		{
			chore.choreType.interruptPriority = Db.Get().ChoreTypes.Die.interruptPriority;
			chore.masterPriority.priority_class = PriorityScreen.PriorityClass.compulsory;
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.Die);
		}
	}
}
