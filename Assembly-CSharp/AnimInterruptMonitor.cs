using System;

// Token: 0x0200084F RID: 2127
public class AnimInterruptMonitor : GameStateMachine<AnimInterruptMonitor, AnimInterruptMonitor.Instance, IStateMachineTarget, AnimInterruptMonitor.Def>
{
	// Token: 0x06003A75 RID: 14965 RVA: 0x0014553C File Offset: 0x0014373C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleBehaviour(GameTags.Creatures.Behaviours.PlayInterruptAnim, new StateMachine<AnimInterruptMonitor, AnimInterruptMonitor.Instance, IStateMachineTarget, AnimInterruptMonitor.Def>.Transition.ConditionCallback(AnimInterruptMonitor.ShoulPlayAnim), new Action<AnimInterruptMonitor.Instance>(AnimInterruptMonitor.ClearAnim));
	}

	// Token: 0x06003A76 RID: 14966 RVA: 0x0014556F File Offset: 0x0014376F
	private static bool ShoulPlayAnim(AnimInterruptMonitor.Instance smi)
	{
		return smi.anims != null;
	}

	// Token: 0x06003A77 RID: 14967 RVA: 0x0014557A File Offset: 0x0014377A
	private static void ClearAnim(AnimInterruptMonitor.Instance smi)
	{
		smi.anims = null;
	}

	// Token: 0x020017CD RID: 6093
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020017CE RID: 6094
	public new class Instance : GameStateMachine<AnimInterruptMonitor, AnimInterruptMonitor.Instance, IStateMachineTarget, AnimInterruptMonitor.Def>.GameInstance
	{
		// Token: 0x06009A7A RID: 39546 RVA: 0x0038A86A File Offset: 0x00388A6A
		public Instance(IStateMachineTarget master, AnimInterruptMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06009A7B RID: 39547 RVA: 0x0038A874 File Offset: 0x00388A74
		public void PlayAnim(HashedString anim)
		{
			this.PlayAnimSequence(new HashedString[] { anim });
		}

		// Token: 0x06009A7C RID: 39548 RVA: 0x0038A88A File Offset: 0x00388A8A
		public void PlayAnimSequence(HashedString[] anims)
		{
			this.anims = anims;
			base.GetComponent<CreatureBrain>().UpdateBrain();
		}

		// Token: 0x0400770A RID: 30474
		public HashedString[] anims;
	}
}
