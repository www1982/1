using System;
using UnityEngine;

// Token: 0x02000492 RID: 1170
public class ReactEmoteChore : Chore<ReactEmoteChore.StatesInstance>
{
	// Token: 0x06001884 RID: 6276 RVA: 0x00088EB8 File Offset: 0x000870B8
	public ReactEmoteChore(IStateMachineTarget target, ChoreType chore_type, EmoteReactable reactable, HashedString emote_kanim, HashedString[] emote_anims, KAnim.PlayMode play_mode, Func<StatusItem> get_status_item)
		: base(chore_type, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.basic, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		this.AddPrecondition(ChorePreconditions.instance.IsMoving, null);
		this.AddPrecondition(ChorePreconditions.instance.IsOffLadder, null);
		this.AddPrecondition(ChorePreconditions.instance.NotInTube, null);
		this.AddPrecondition(ChorePreconditions.instance.IsAwake, null);
		this.getStatusItem = get_status_item;
		base.smi = new ReactEmoteChore.StatesInstance(this, target.gameObject, reactable, emote_kanim, emote_anims, play_mode);
	}

	// Token: 0x06001885 RID: 6277 RVA: 0x00088F44 File Offset: 0x00087144
	protected override StatusItem GetStatusItem()
	{
		if (this.getStatusItem == null)
		{
			return base.GetStatusItem();
		}
		return this.getStatusItem();
	}

	// Token: 0x06001886 RID: 6278 RVA: 0x00088F60 File Offset: 0x00087160
	public override string ToString()
	{
		HashedString hashedString;
		if (base.smi.emoteKAnim.IsValid)
		{
			string text = "ReactEmoteChore<";
			hashedString = base.smi.emoteKAnim;
			return text + hashedString.ToString() + ">";
		}
		string text2 = "ReactEmoteChore<";
		hashedString = base.smi.emoteAnims[0];
		return text2 + hashedString.ToString() + ">";
	}

	// Token: 0x04000E3F RID: 3647
	private Func<StatusItem> getStatusItem;

	// Token: 0x020012A8 RID: 4776
	public class StatesInstance : GameStateMachine<ReactEmoteChore.States, ReactEmoteChore.StatesInstance, ReactEmoteChore, object>.GameInstance
	{
		// Token: 0x06008735 RID: 34613 RVA: 0x00342C88 File Offset: 0x00340E88
		public StatesInstance(ReactEmoteChore master, GameObject emoter, EmoteReactable reactable, HashedString emote_kanim, HashedString[] emote_anims, KAnim.PlayMode mode)
			: base(master)
		{
			this.emoteKAnim = emote_kanim;
			this.emoteAnims = emote_anims;
			this.mode = mode;
			base.sm.reactable.Set(reactable, base.smi, false);
			base.sm.emoter.Set(emoter, base.smi, false);
		}

		// Token: 0x04006708 RID: 26376
		public HashedString[] emoteAnims;

		// Token: 0x04006709 RID: 26377
		public HashedString emoteKAnim;

		// Token: 0x0400670A RID: 26378
		public KAnim.PlayMode mode = KAnim.PlayMode.Once;
	}

	// Token: 0x020012A9 RID: 4777
	public class States : GameStateMachine<ReactEmoteChore.States, ReactEmoteChore.StatesInstance, ReactEmoteChore>
	{
		// Token: 0x06008736 RID: 34614 RVA: 0x00342CF0 File Offset: 0x00340EF0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
			base.Target(this.emoter);
			this.root.ToggleThought((ReactEmoteChore.StatesInstance smi) => this.reactable.Get(smi).thought).ToggleExpression((ReactEmoteChore.StatesInstance smi) => this.reactable.Get(smi).expression).ToggleAnims((ReactEmoteChore.StatesInstance smi) => smi.emoteKAnim)
				.ToggleThought(Db.Get().Thoughts.Unhappy, null)
				.PlayAnims((ReactEmoteChore.StatesInstance smi) => smi.emoteAnims, (ReactEmoteChore.StatesInstance smi) => smi.mode)
				.OnAnimQueueComplete(null)
				.Enter(delegate(ReactEmoteChore.StatesInstance smi)
				{
					smi.master.GetComponent<Facing>().Face(Grid.CellToPos(this.reactable.Get(smi).sourceCell));
				});
		}

		// Token: 0x0400670B RID: 26379
		public StateMachine<ReactEmoteChore.States, ReactEmoteChore.StatesInstance, ReactEmoteChore, object>.TargetParameter emoter;

		// Token: 0x0400670C RID: 26380
		public StateMachine<ReactEmoteChore.States, ReactEmoteChore.StatesInstance, ReactEmoteChore, object>.ObjectParameter<EmoteReactable> reactable;
	}
}
