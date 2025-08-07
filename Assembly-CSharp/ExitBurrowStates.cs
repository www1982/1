using System;
using STRINGS;

// Token: 0x020000EA RID: 234
public class ExitBurrowStates : GameStateMachine<ExitBurrowStates, ExitBurrowStates.Instance, IStateMachineTarget, ExitBurrowStates.Def>
{
	// Token: 0x06000443 RID: 1091 RVA: 0x000236B4 File Offset: 0x000218B4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.exiting;
		GameStateMachine<ExitBurrowStates, ExitBurrowStates.Instance, IStateMachineTarget, ExitBurrowStates.Def>.State root = this.root;
		string text = CREATURES.STATUSITEMS.EMERGING.NAME;
		string text2 = CREATURES.STATUSITEMS.EMERGING.TOOLTIP;
		string text3 = "";
		StatusItem.IconType iconType = StatusItem.IconType.Info;
		NotificationType notificationType = NotificationType.Neutral;
		bool flag = false;
		StatusItemCategory main = Db.Get().StatusItemCategories.Main;
		root.ToggleStatusItem(text, text2, text3, iconType, notificationType, flag, default(HashedString), 129022, null, null, main);
		this.exiting.PlayAnim("emerge").Enter(new StateMachine<ExitBurrowStates, ExitBurrowStates.Instance, IStateMachineTarget, ExitBurrowStates.Def>.State.Callback(ExitBurrowStates.MoveToCellAbove)).OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.WantsToExitBurrow, false);
	}

	// Token: 0x06000444 RID: 1092 RVA: 0x00023751 File Offset: 0x00021951
	private static void MoveToCellAbove(ExitBurrowStates.Instance smi)
	{
		smi.transform.SetPosition(Grid.CellToPosCBC(Grid.CellAbove(Grid.PosToCell(smi.transform.GetPosition())), Grid.SceneLayer.Creatures));
	}

	// Token: 0x04000320 RID: 800
	private GameStateMachine<ExitBurrowStates, ExitBurrowStates.Instance, IStateMachineTarget, ExitBurrowStates.Def>.State exiting;

	// Token: 0x04000321 RID: 801
	private GameStateMachine<ExitBurrowStates, ExitBurrowStates.Instance, IStateMachineTarget, ExitBurrowStates.Def>.State behaviourcomplete;

	// Token: 0x020010E5 RID: 4325
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020010E6 RID: 4326
	public new class Instance : GameStateMachine<ExitBurrowStates, ExitBurrowStates.Instance, IStateMachineTarget, ExitBurrowStates.Def>.GameInstance
	{
		// Token: 0x06008101 RID: 33025 RVA: 0x0032E420 File Offset: 0x0032C620
		public Instance(Chore<ExitBurrowStates.Instance> chore, ExitBurrowStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.WantsToExitBurrow);
		}
	}
}
