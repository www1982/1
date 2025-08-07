using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000D9 RID: 217
public class BuzzStates : GameStateMachine<BuzzStates, BuzzStates.Instance, IStateMachineTarget, BuzzStates.Def>
{
	// Token: 0x060003D7 RID: 983 RVA: 0x0002065C File Offset: 0x0001E85C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.idle;
		GameStateMachine<BuzzStates, BuzzStates.Instance, IStateMachineTarget, BuzzStates.Def>.State state = this.root.Exit("StopNavigator", delegate(BuzzStates.Instance smi)
		{
			smi.GetComponent<Navigator>().Stop(false, true);
		});
		string text = CREATURES.STATUSITEMS.IDLE.NAME;
		string text2 = CREATURES.STATUSITEMS.IDLE.TOOLTIP;
		string text3 = "";
		StatusItem.IconType iconType = StatusItem.IconType.Info;
		NotificationType notificationType = NotificationType.Neutral;
		bool flag = false;
		StatusItemCategory main = Db.Get().StatusItemCategories.Main;
		state.ToggleStatusItem(text, text2, text3, iconType, notificationType, flag, default(HashedString), 129022, null, null, main).ToggleTag(GameTags.Idle);
		this.idle.Enter(new StateMachine<BuzzStates, BuzzStates.Instance, IStateMachineTarget, BuzzStates.Def>.State.Callback(this.PlayIdle)).ToggleScheduleCallback("DoBuzz", (BuzzStates.Instance smi) => (float)global::UnityEngine.Random.Range(3, 10), delegate(BuzzStates.Instance smi)
		{
			this.numMoves.Set(global::UnityEngine.Random.Range(4, 6), smi, false);
			smi.GoTo(this.buzz.move);
		});
		this.buzz.ParamTransition<int>(this.numMoves, this.idle, (BuzzStates.Instance smi, int p) => p <= 0);
		this.buzz.move.Enter(new StateMachine<BuzzStates, BuzzStates.Instance, IStateMachineTarget, BuzzStates.Def>.State.Callback(this.MoveToNewCell)).EventTransition(GameHashes.DestinationReached, this.buzz.pause, null).EventTransition(GameHashes.NavigationFailed, this.buzz.pause, null);
		this.buzz.pause.Enter(delegate(BuzzStates.Instance smi)
		{
			this.numMoves.Set(this.numMoves.Get(smi) - 1, smi, false);
			smi.GoTo(this.buzz.move);
		});
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x000207D8 File Offset: 0x0001E9D8
	public void MoveToNewCell(BuzzStates.Instance smi)
	{
		Navigator component = smi.GetComponent<Navigator>();
		BuzzStates.MoveCellQuery moveCellQuery = new BuzzStates.MoveCellQuery(component.CurrentNavType);
		moveCellQuery.allowLiquid = smi.gameObject.HasTag(GameTags.Amphibious);
		component.RunQuery(moveCellQuery);
		component.GoTo(moveCellQuery.GetResultCell(), null);
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x00020824 File Offset: 0x0001EA24
	public void PlayIdle(BuzzStates.Instance smi)
	{
		KAnimControllerBase component = smi.GetComponent<KAnimControllerBase>();
		Navigator component2 = smi.GetComponent<Navigator>();
		NavType navType = component2.CurrentNavType;
		if (smi.GetComponent<Facing>().GetFacing())
		{
			navType = NavGrid.MirrorNavType(navType);
		}
		if (smi.def.customIdleAnim != null)
		{
			HashedString invalid = HashedString.Invalid;
			HashedString hashedString = smi.def.customIdleAnim(smi, ref invalid);
			if (hashedString != HashedString.Invalid)
			{
				if (invalid != HashedString.Invalid)
				{
					component.Play(invalid, KAnim.PlayMode.Once, 1f, 0f);
				}
				component.Queue(hashedString, KAnim.PlayMode.Loop, 1f, 0f);
				return;
			}
		}
		HashedString idleAnim = component2.NavGrid.GetIdleAnim(navType);
		component.Play(idleAnim, KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x040002DE RID: 734
	private StateMachine<BuzzStates, BuzzStates.Instance, IStateMachineTarget, BuzzStates.Def>.IntParameter numMoves;

	// Token: 0x040002DF RID: 735
	private BuzzStates.BuzzingStates buzz;

	// Token: 0x040002E0 RID: 736
	public GameStateMachine<BuzzStates, BuzzStates.Instance, IStateMachineTarget, BuzzStates.Def>.State idle;

	// Token: 0x040002E1 RID: 737
	public GameStateMachine<BuzzStates, BuzzStates.Instance, IStateMachineTarget, BuzzStates.Def>.State move;

	// Token: 0x020010A6 RID: 4262
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006100 RID: 24832
		public BuzzStates.Def.IdleAnimCallback customIdleAnim;

		// Token: 0x0200260C RID: 9740
		// (Invoke) Token: 0x0600C24F RID: 49743
		public delegate HashedString IdleAnimCallback(BuzzStates.Instance smi, ref HashedString pre_anim);
	}

	// Token: 0x020010A7 RID: 4263
	public new class Instance : GameStateMachine<BuzzStates, BuzzStates.Instance, IStateMachineTarget, BuzzStates.Def>.GameInstance
	{
		// Token: 0x06008064 RID: 32868 RVA: 0x0032D5F7 File Offset: 0x0032B7F7
		public Instance(Chore<BuzzStates.Instance> chore, BuzzStates.Def def)
			: base(chore, def)
		{
		}
	}

	// Token: 0x020010A8 RID: 4264
	public class BuzzingStates : GameStateMachine<BuzzStates, BuzzStates.Instance, IStateMachineTarget, BuzzStates.Def>.State
	{
		// Token: 0x04006101 RID: 24833
		public GameStateMachine<BuzzStates, BuzzStates.Instance, IStateMachineTarget, BuzzStates.Def>.State move;

		// Token: 0x04006102 RID: 24834
		public GameStateMachine<BuzzStates, BuzzStates.Instance, IStateMachineTarget, BuzzStates.Def>.State pause;
	}

	// Token: 0x020010A9 RID: 4265
	public class MoveCellQuery : PathFinderQuery
	{
		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06008066 RID: 32870 RVA: 0x0032D609 File Offset: 0x0032B809
		// (set) Token: 0x06008067 RID: 32871 RVA: 0x0032D611 File Offset: 0x0032B811
		public bool allowLiquid { get; set; }

		// Token: 0x06008068 RID: 32872 RVA: 0x0032D61A File Offset: 0x0032B81A
		public MoveCellQuery(NavType navType)
		{
			this.navType = navType;
			this.maxIterations = global::UnityEngine.Random.Range(5, 25);
		}

		// Token: 0x06008069 RID: 32873 RVA: 0x0032D644 File Offset: 0x0032B844
		public override bool IsMatch(int cell, int parent_cell, int cost)
		{
			if (!Grid.IsValidCell(cell))
			{
				return false;
			}
			bool flag = this.navType != NavType.Swim;
			bool flag2 = this.navType == NavType.Swim || this.allowLiquid;
			bool flag3 = Grid.IsSubstantialLiquid(cell, 0.35f);
			if (flag3 && !flag2)
			{
				return false;
			}
			if (!flag3 && !flag)
			{
				return false;
			}
			this.targetCell = cell;
			int num = this.maxIterations - 1;
			this.maxIterations = num;
			return num <= 0;
		}

		// Token: 0x0600806A RID: 32874 RVA: 0x0032D6B5 File Offset: 0x0032B8B5
		public override int GetResultCell()
		{
			return this.targetCell;
		}

		// Token: 0x04006103 RID: 24835
		private NavType navType;

		// Token: 0x04006104 RID: 24836
		private int targetCell = Grid.InvalidCell;

		// Token: 0x04006105 RID: 24837
		private int maxIterations;
	}
}
