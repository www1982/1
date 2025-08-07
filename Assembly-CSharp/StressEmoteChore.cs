using System;
using UnityEngine;

// Token: 0x0200049D RID: 1181
public class StressEmoteChore : Chore<StressEmoteChore.StatesInstance>
{
	// Token: 0x060018AC RID: 6316 RVA: 0x00089E5C File Offset: 0x0008805C
	public StressEmoteChore(IStateMachineTarget target, ChoreType chore_type, HashedString emote_kanim, HashedString[] emote_anims, KAnim.PlayMode play_mode, Func<StatusItem> get_status_item)
		: base(chore_type, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		this.AddPrecondition(ChorePreconditions.instance.IsMoving, null);
		this.AddPrecondition(ChorePreconditions.instance.IsOffLadder, null);
		this.AddPrecondition(ChorePreconditions.instance.NotInTube, null);
		this.AddPrecondition(ChorePreconditions.instance.IsAwake, null);
		this.getStatusItem = get_status_item;
		base.smi = new StressEmoteChore.StatesInstance(this, target.gameObject, emote_kanim, emote_anims, play_mode);
	}

	// Token: 0x060018AD RID: 6317 RVA: 0x00089EE6 File Offset: 0x000880E6
	protected override StatusItem GetStatusItem()
	{
		if (this.getStatusItem == null)
		{
			return base.GetStatusItem();
		}
		return this.getStatusItem();
	}

	// Token: 0x060018AE RID: 6318 RVA: 0x00089F04 File Offset: 0x00088104
	public override string ToString()
	{
		HashedString hashedString;
		if (base.smi.emoteKAnim.IsValid)
		{
			string text = "StressEmoteChore<";
			hashedString = base.smi.emoteKAnim;
			return text + hashedString.ToString() + ">";
		}
		string text2 = "StressEmoteChore<";
		hashedString = base.smi.emoteAnims[0];
		return text2 + hashedString.ToString() + ">";
	}

	// Token: 0x04000E48 RID: 3656
	private Func<StatusItem> getStatusItem;

	// Token: 0x020012C5 RID: 4805
	public class StatesInstance : GameStateMachine<StressEmoteChore.States, StressEmoteChore.StatesInstance, StressEmoteChore, object>.GameInstance
	{
		// Token: 0x060087BD RID: 34749 RVA: 0x00345F97 File Offset: 0x00344197
		public StatesInstance(StressEmoteChore master, GameObject emoter, HashedString emote_kanim, HashedString[] emote_anims, KAnim.PlayMode mode)
			: base(master)
		{
			this.emoteKAnim = emote_kanim;
			this.emoteAnims = emote_anims;
			this.mode = mode;
			base.sm.emoter.Set(emoter, base.smi, false);
		}

		// Token: 0x04006772 RID: 26482
		public HashedString[] emoteAnims;

		// Token: 0x04006773 RID: 26483
		public HashedString emoteKAnim;

		// Token: 0x04006774 RID: 26484
		public KAnim.PlayMode mode = KAnim.PlayMode.Once;
	}

	// Token: 0x020012C6 RID: 4806
	public class States : GameStateMachine<StressEmoteChore.States, StressEmoteChore.StatesInstance, StressEmoteChore>
	{
		// Token: 0x060087BE RID: 34750 RVA: 0x00345FD8 File Offset: 0x003441D8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
			base.Target(this.emoter);
			this.root.ToggleAnims((StressEmoteChore.StatesInstance smi) => smi.emoteKAnim).ToggleThought(Db.Get().Thoughts.Unhappy, null).PlayAnims((StressEmoteChore.StatesInstance smi) => smi.emoteAnims, (StressEmoteChore.StatesInstance smi) => smi.mode)
				.OnAnimQueueComplete(null);
		}

		// Token: 0x04006775 RID: 26485
		public StateMachine<StressEmoteChore.States, StressEmoteChore.StatesInstance, StressEmoteChore, object>.TargetParameter emoter;
	}
}
