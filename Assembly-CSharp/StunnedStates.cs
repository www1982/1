using System;
using System.Collections.Generic;
using STRINGS;

// Token: 0x0200010A RID: 266
public class StunnedStates : GameStateMachine<StunnedStates, StunnedStates.Instance, IStateMachineTarget, StunnedStates.Def>
{
	// Token: 0x060004DB RID: 1243 RVA: 0x00027BAC File Offset: 0x00025DAC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.init;
		this.init.TagTransition(GameTags.Creatures.StunnedForCapture, this.stun_for_capture, false).TagTransition(GameTags.Creatures.StunnedBeingEaten, this.stun_for_being_eaten, false);
		GameStateMachine<StunnedStates, StunnedStates.Instance, IStateMachineTarget, StunnedStates.Def>.State state = this.stun_for_capture;
		string text = CREATURES.STATUSITEMS.GETTING_WRANGLED.NAME;
		string text2 = CREATURES.STATUSITEMS.GETTING_WRANGLED.TOOLTIP;
		string text3 = "";
		StatusItem.IconType iconType = StatusItem.IconType.Info;
		NotificationType notificationType = NotificationType.Neutral;
		bool flag = false;
		StatusItemCategory main = Db.Get().StatusItemCategories.Main;
		state.ToggleStatusItem(text, text2, text3, iconType, notificationType, flag, default(HashedString), 129022, null, null, main).PlayAnim("idle_loop", KAnim.PlayMode.Loop).TagTransition(GameTags.Creatures.StunnedForCapture, null, true);
		this.stun_for_being_eaten.PlayAnim("eaten", KAnim.PlayMode.Once).TagTransition(GameTags.Creatures.StunnedBeingEaten, null, true);
	}

	// Token: 0x04000375 RID: 885
	private static List<Tag> StunnedTags = new List<Tag>
	{
		GameTags.Creatures.StunnedForCapture,
		GameTags.Creatures.StunnedBeingEaten
	};

	// Token: 0x04000376 RID: 886
	public GameStateMachine<StunnedStates, StunnedStates.Instance, IStateMachineTarget, StunnedStates.Def>.State init;

	// Token: 0x04000377 RID: 887
	public GameStateMachine<StunnedStates, StunnedStates.Instance, IStateMachineTarget, StunnedStates.Def>.State stun_for_capture;

	// Token: 0x04000378 RID: 888
	public GameStateMachine<StunnedStates, StunnedStates.Instance, IStateMachineTarget, StunnedStates.Def>.State stun_for_being_eaten;

	// Token: 0x02001149 RID: 4425
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x0200114A RID: 4426
	public new class Instance : GameStateMachine<StunnedStates, StunnedStates.Instance, IStateMachineTarget, StunnedStates.Def>.GameInstance
	{
		// Token: 0x06008202 RID: 33282 RVA: 0x0033095E File Offset: 0x0032EB5E
		public Instance(Chore<StunnedStates.Instance> chore, StunnedStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(StunnedStates.Instance.IsStunned, null);
		}

		// Token: 0x0400628B RID: 25227
		public static readonly Chore.Precondition IsStunned = new Chore.Precondition
		{
			id = "IsStunned",
			fn = delegate(ref Chore.Precondition.Context context, object data)
			{
				return context.consumerState.prefabid.HasAnyTags(StunnedStates.StunnedTags);
			}
		};
	}
}
