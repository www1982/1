using System;

// Token: 0x020000DB RID: 219
public class PlayAnimsStates : GameStateMachine<PlayAnimsStates, PlayAnimsStates.Instance, IStateMachineTarget, PlayAnimsStates.Def>
{
	// Token: 0x060003DF RID: 991 RVA: 0x00020A18 File Offset: 0x0001EC18
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.animating;
		GameStateMachine<PlayAnimsStates, PlayAnimsStates.Instance, IStateMachineTarget, PlayAnimsStates.Def>.State root = this.root;
		string text = "Unused";
		string text2 = "Unused";
		string text3 = "";
		StatusItem.IconType iconType = StatusItem.IconType.Info;
		NotificationType notificationType = NotificationType.Neutral;
		bool flag = false;
		StatusItemCategory main = Db.Get().StatusItemCategories.Main;
		root.ToggleStatusItem(text, text2, text3, iconType, notificationType, flag, default(HashedString), 129022, (string str, PlayAnimsStates.Instance smi) => smi.def.statusItemName, (string str, PlayAnimsStates.Instance smi) => smi.def.statusItemTooltip, main);
		this.animating.Enter("PlayAnims", delegate(PlayAnimsStates.Instance smi)
		{
			smi.PlayAnims();
		}).OnAnimQueueComplete(this.done).EventHandler(GameHashes.TagsChanged, delegate(PlayAnimsStates.Instance smi, object obj)
		{
			smi.HandleTagsChanged(obj);
		});
		this.done.PlayAnim("idle_loop", KAnim.PlayMode.Loop).BehaviourComplete((PlayAnimsStates.Instance smi) => smi.def.tag, false);
	}

	// Token: 0x040002E6 RID: 742
	public GameStateMachine<PlayAnimsStates, PlayAnimsStates.Instance, IStateMachineTarget, PlayAnimsStates.Def>.State animating;

	// Token: 0x040002E7 RID: 743
	public GameStateMachine<PlayAnimsStates, PlayAnimsStates.Instance, IStateMachineTarget, PlayAnimsStates.Def>.State done;

	// Token: 0x020010AD RID: 4269
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06008072 RID: 32882 RVA: 0x0032D720 File Offset: 0x0032B920
		public Def(Tag tag, bool loop, string anim, string status_item_name, string status_item_tooltip)
			: this(tag, loop, new string[] { anim }, status_item_name, status_item_tooltip)
		{
		}

		// Token: 0x06008073 RID: 32883 RVA: 0x0032D738 File Offset: 0x0032B938
		public Def(Tag tag, bool loop, string[] anims, string status_item_name, string status_item_tooltip)
		{
			this.tag = tag;
			this.loop = loop;
			this.anims = anims;
			this.statusItemName = status_item_name;
			this.statusItemTooltip = status_item_tooltip;
		}

		// Token: 0x06008074 RID: 32884 RVA: 0x0032D765 File Offset: 0x0032B965
		public override string ToString()
		{
			return this.tag.ToString() + "(PlayAnimsStates)";
		}

		// Token: 0x0400610B RID: 24843
		public Tag tag;

		// Token: 0x0400610C RID: 24844
		public string[] anims;

		// Token: 0x0400610D RID: 24845
		public bool loop;

		// Token: 0x0400610E RID: 24846
		public string statusItemName;

		// Token: 0x0400610F RID: 24847
		public string statusItemTooltip;
	}

	// Token: 0x020010AE RID: 4270
	public new class Instance : GameStateMachine<PlayAnimsStates, PlayAnimsStates.Instance, IStateMachineTarget, PlayAnimsStates.Def>.GameInstance
	{
		// Token: 0x06008075 RID: 32885 RVA: 0x0032D782 File Offset: 0x0032B982
		public Instance(Chore<PlayAnimsStates.Instance> chore, PlayAnimsStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, def.tag);
		}

		// Token: 0x06008076 RID: 32886 RVA: 0x0032D7A8 File Offset: 0x0032B9A8
		public void PlayAnims()
		{
			if (base.def.anims == null || base.def.anims.Length == 0)
			{
				return;
			}
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			for (int i = 0; i < base.def.anims.Length; i++)
			{
				KAnim.PlayMode playMode = KAnim.PlayMode.Once;
				if (base.def.loop && i == base.def.anims.Length - 1)
				{
					playMode = KAnim.PlayMode.Loop;
				}
				if (i == 0)
				{
					component.Play(base.def.anims[i], playMode, 1f, 0f);
				}
				else
				{
					component.Queue(base.def.anims[i], playMode, 1f, 0f);
				}
			}
		}

		// Token: 0x06008077 RID: 32887 RVA: 0x0032D861 File Offset: 0x0032BA61
		public void HandleTagsChanged(object obj)
		{
			if (!base.smi.HasTag(base.smi.def.tag))
			{
				base.smi.GoTo(null);
			}
		}
	}
}
