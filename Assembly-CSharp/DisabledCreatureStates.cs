using System;
using STRINGS;

// Token: 0x020000E5 RID: 229
public class DisabledCreatureStates : GameStateMachine<DisabledCreatureStates, DisabledCreatureStates.Instance, IStateMachineTarget, DisabledCreatureStates.Def>
{
	// Token: 0x0600041D RID: 1053 RVA: 0x00022650 File Offset: 0x00020850
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.disableCreature;
		GameStateMachine<DisabledCreatureStates, DisabledCreatureStates.Instance, IStateMachineTarget, DisabledCreatureStates.Def>.State root = this.root;
		string text = CREATURES.STATUSITEMS.DISABLED.NAME;
		string text2 = CREATURES.STATUSITEMS.DISABLED.TOOLTIP;
		string text3 = "";
		StatusItem.IconType iconType = StatusItem.IconType.Info;
		NotificationType notificationType = NotificationType.Neutral;
		bool flag = false;
		StatusItemCategory main = Db.Get().StatusItemCategories.Main;
		root.ToggleStatusItem(text, text2, text3, iconType, notificationType, flag, default(HashedString), 129022, null, null, main).TagTransition(GameTags.Creatures.Behaviours.DisableCreature, this.behaviourcomplete, true);
		this.disableCreature.PlayAnim((DisabledCreatureStates.Instance smi) => smi.def.disabledAnim, KAnim.PlayMode.Once);
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.Behaviours.DisableCreature, false);
	}

	// Token: 0x04000308 RID: 776
	public GameStateMachine<DisabledCreatureStates, DisabledCreatureStates.Instance, IStateMachineTarget, DisabledCreatureStates.Def>.State disableCreature;

	// Token: 0x04000309 RID: 777
	public GameStateMachine<DisabledCreatureStates, DisabledCreatureStates.Instance, IStateMachineTarget, DisabledCreatureStates.Def>.State behaviourcomplete;

	// Token: 0x020010D2 RID: 4306
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x060080CE RID: 32974 RVA: 0x0032DF78 File Offset: 0x0032C178
		public Def(string anim)
		{
			this.disabledAnim = anim;
		}

		// Token: 0x0400616A RID: 24938
		public string disabledAnim = "off";
	}

	// Token: 0x020010D3 RID: 4307
	public new class Instance : GameStateMachine<DisabledCreatureStates, DisabledCreatureStates.Instance, IStateMachineTarget, DisabledCreatureStates.Def>.GameInstance
	{
		// Token: 0x060080CF RID: 32975 RVA: 0x0032DF92 File Offset: 0x0032C192
		public Instance(Chore<DisabledCreatureStates.Instance> chore, DisabledCreatureStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.HasTag, GameTags.Creatures.Behaviours.DisableCreature);
		}
	}
}
