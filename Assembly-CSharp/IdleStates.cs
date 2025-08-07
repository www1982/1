using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000FA RID: 250
public class IdleStates : GameStateMachine<IdleStates, IdleStates.Instance, IStateMachineTarget, IdleStates.Def>
{
	// Token: 0x06000482 RID: 1154 RVA: 0x00024FBC File Offset: 0x000231BC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.loop;
		GameStateMachine<IdleStates, IdleStates.Instance, IStateMachineTarget, IdleStates.Def>.State state = this.root.Exit("StopNavigator", delegate(IdleStates.Instance smi)
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
		this.loop.Enter(new StateMachine<IdleStates, IdleStates.Instance, IStateMachineTarget, IdleStates.Def>.State.Callback(this.PlayIdle)).ToggleScheduleCallback("IdleMove", (IdleStates.Instance smi) => (float)global::UnityEngine.Random.Range(3, 10), delegate(IdleStates.Instance smi)
		{
			smi.GoTo(this.move);
		});
		this.move.Enter(new StateMachine<IdleStates, IdleStates.Instance, IStateMachineTarget, IdleStates.Def>.State.Callback(this.MoveToNewCell)).EventTransition(GameHashes.DestinationReached, this.loop, null).EventTransition(GameHashes.NavigationFailed, this.loop, null);
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x000250D4 File Offset: 0x000232D4
	public void MoveToNewCell(IdleStates.Instance smi)
	{
		if (smi.HasTag(GameTags.StationaryIdling))
		{
			smi.GoTo(smi.sm.loop);
			return;
		}
		Navigator component = smi.GetComponent<Navigator>();
		IdleStates.MoveCellQuery moveCellQuery = new IdleStates.MoveCellQuery(component.CurrentNavType);
		moveCellQuery.allowLiquid = smi.gameObject.HasTag(GameTags.Amphibious);
		moveCellQuery.submerged = smi.gameObject.HasTag(GameTags.Creatures.Submerged);
		int num = Grid.PosToCell(component);
		if (component.CurrentNavType == NavType.Hover && CellSelectionObject.IsExposedToSpace(num))
		{
			int num2 = 0;
			int num3 = num;
			for (int i = 0; i < 10; i++)
			{
				num3 = Grid.CellBelow(num3);
				if (!Grid.IsValidCell(num3) || Grid.IsSolidCell(num3) || !CellSelectionObject.IsExposedToSpace(num3))
				{
					break;
				}
				num2++;
			}
			moveCellQuery.lowerCellBias = num2 == 10;
		}
		component.RunQuery(moveCellQuery);
		component.GoTo(moveCellQuery.GetResultCell(), null);
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x000251B8 File Offset: 0x000233B8
	public void PlayIdle(IdleStates.Instance smi)
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

	// Token: 0x04000343 RID: 835
	private GameStateMachine<IdleStates, IdleStates.Instance, IStateMachineTarget, IdleStates.Def>.State loop;

	// Token: 0x04000344 RID: 836
	private GameStateMachine<IdleStates, IdleStates.Instance, IStateMachineTarget, IdleStates.Def>.State move;

	// Token: 0x02001112 RID: 4370
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040061E2 RID: 25058
		public IdleStates.Def.IdleAnimCallback customIdleAnim;

		// Token: 0x040061E3 RID: 25059
		public PriorityScreen.PriorityClass priorityClass;

		// Token: 0x02002612 RID: 9746
		// (Invoke) Token: 0x0600C260 RID: 49760
		public delegate HashedString IdleAnimCallback(IdleStates.Instance smi, ref HashedString pre_anim);
	}

	// Token: 0x02001113 RID: 4371
	public new class Instance : GameStateMachine<IdleStates, IdleStates.Instance, IStateMachineTarget, IdleStates.Def>.GameInstance
	{
		// Token: 0x06008165 RID: 33125 RVA: 0x0032EE88 File Offset: 0x0032D088
		public Instance(Chore<IdleStates.Instance> chore, IdleStates.Def def)
			: base(chore, def)
		{
			chore.masterPriority.priority_class = def.priorityClass;
		}
	}

	// Token: 0x02001114 RID: 4372
	public class MoveCellQuery : PathFinderQuery
	{
		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06008166 RID: 33126 RVA: 0x0032EEA3 File Offset: 0x0032D0A3
		// (set) Token: 0x06008167 RID: 33127 RVA: 0x0032EEAB File Offset: 0x0032D0AB
		public bool allowLiquid { get; set; }

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06008168 RID: 33128 RVA: 0x0032EEB4 File Offset: 0x0032D0B4
		// (set) Token: 0x06008169 RID: 33129 RVA: 0x0032EEBC File Offset: 0x0032D0BC
		public bool submerged { get; set; }

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x0600816A RID: 33130 RVA: 0x0032EEC5 File Offset: 0x0032D0C5
		// (set) Token: 0x0600816B RID: 33131 RVA: 0x0032EECD File Offset: 0x0032D0CD
		public bool lowerCellBias { get; set; }

		// Token: 0x0600816C RID: 33132 RVA: 0x0032EED6 File Offset: 0x0032D0D6
		public MoveCellQuery(NavType navType)
		{
			this.navType = navType;
			this.maxIterations = global::UnityEngine.Random.Range(5, 25);
		}

		// Token: 0x0600816D RID: 33133 RVA: 0x0032EF00 File Offset: 0x0032D100
		public override bool IsMatch(int cell, int parent_cell, int cost)
		{
			if (!Grid.IsValidCell(cell))
			{
				return false;
			}
			GameObject gameObject;
			Grid.ObjectLayers[1].TryGetValue(cell, out gameObject);
			if (gameObject != null)
			{
				BuildingUnderConstruction component = gameObject.GetComponent<BuildingUnderConstruction>();
				if (component != null && (component.Def.IsFoundation || component.HasTag(GameTags.NoCreatureIdling)))
				{
					return false;
				}
			}
			bool flag = this.submerged || Grid.IsNavigatableLiquid(cell);
			bool flag2 = this.navType != NavType.Swim;
			bool flag3 = this.navType == NavType.Swim || this.allowLiquid;
			if (flag && !flag3)
			{
				return false;
			}
			if (!flag && !flag2)
			{
				return false;
			}
			if (this.targetCell == Grid.InvalidCell || !this.lowerCellBias)
			{
				this.targetCell = cell;
			}
			else
			{
				int num = Grid.CellRow(this.targetCell);
				if (Grid.CellRow(cell) < num)
				{
					this.targetCell = cell;
				}
			}
			int num2 = this.maxIterations - 1;
			this.maxIterations = num2;
			return num2 <= 0;
		}

		// Token: 0x0600816E RID: 33134 RVA: 0x0032EFF8 File Offset: 0x0032D1F8
		public override int GetResultCell()
		{
			return this.targetCell;
		}

		// Token: 0x040061E4 RID: 25060
		private NavType navType;

		// Token: 0x040061E5 RID: 25061
		private int targetCell = Grid.InvalidCell;

		// Token: 0x040061E6 RID: 25062
		private int maxIterations;
	}
}
