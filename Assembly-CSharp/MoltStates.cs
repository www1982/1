using System;
using Klei;
using STRINGS;
using UnityEngine;

// Token: 0x02000100 RID: 256
public class MoltStates : GameStateMachine<MoltStates, MoltStates.Instance, IStateMachineTarget, MoltStates.Def>
{
	// Token: 0x0600049B RID: 1179 RVA: 0x00025A78 File Offset: 0x00023C78
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.moltpre;
		GameStateMachine<MoltStates, MoltStates.Instance, IStateMachineTarget, MoltStates.Def>.State root = this.root;
		string text = CREATURES.STATUSITEMS.MOLTING.NAME;
		string text2 = CREATURES.STATUSITEMS.MOLTING.TOOLTIP;
		string text3 = "";
		StatusItem.IconType iconType = StatusItem.IconType.Info;
		NotificationType notificationType = NotificationType.Neutral;
		bool flag = false;
		StatusItemCategory main = Db.Get().StatusItemCategories.Main;
		root.ToggleStatusItem(text, text2, text3, iconType, notificationType, flag, default(HashedString), 129022, null, null, main);
		this.moltpre.Enter(new StateMachine<MoltStates, MoltStates.Instance, IStateMachineTarget, MoltStates.Def>.State.Callback(MoltStates.Molt)).QueueAnim("lay_egg_pre", false, null).OnAnimQueueComplete(this.moltpst);
		this.moltpst.QueueAnim("lay_egg_pst", false, null).OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.ScalesGrown, false);
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x00025B35 File Offset: 0x00023D35
	private static void Molt(MoltStates.Instance smi)
	{
		smi.eggPos = smi.transform.GetPosition();
		smi.GetSMI<ScaleGrowthMonitor.Instance>().Shear();
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x00025B54 File Offset: 0x00023D54
	private static int GetMoveAsideCell(MoltStates.Instance smi)
	{
		int num = 1;
		if (GenericGameSettings.instance.acceleratedLifecycle)
		{
			num = 8;
		}
		int num2 = Grid.PosToCell(smi);
		if (Grid.IsValidCell(num2))
		{
			int num3 = Grid.OffsetCell(num2, num, 0);
			if (Grid.IsValidCell(num3) && !Grid.Solid[num3])
			{
				return num3;
			}
			int num4 = Grid.OffsetCell(num2, -num, 0);
			if (Grid.IsValidCell(num4))
			{
				return num4;
			}
		}
		return Grid.InvalidCell;
	}

	// Token: 0x04000353 RID: 851
	public GameStateMachine<MoltStates, MoltStates.Instance, IStateMachineTarget, MoltStates.Def>.State moltpre;

	// Token: 0x04000354 RID: 852
	public GameStateMachine<MoltStates, MoltStates.Instance, IStateMachineTarget, MoltStates.Def>.State moltpst;

	// Token: 0x04000355 RID: 853
	public GameStateMachine<MoltStates, MoltStates.Instance, IStateMachineTarget, MoltStates.Def>.State behaviourcomplete;

	// Token: 0x02001124 RID: 4388
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001125 RID: 4389
	public new class Instance : GameStateMachine<MoltStates, MoltStates.Instance, IStateMachineTarget, MoltStates.Def>.GameInstance
	{
		// Token: 0x06008193 RID: 33171 RVA: 0x0032F3F3 File Offset: 0x0032D5F3
		public Instance(Chore<MoltStates.Instance> chore, MoltStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.ScalesGrown);
		}

		// Token: 0x04006211 RID: 25105
		public Vector3 eggPos;
	}
}
