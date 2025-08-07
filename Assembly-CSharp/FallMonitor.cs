using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005B7 RID: 1463
public class FallMonitor : GameStateMachine<FallMonitor, FallMonitor.Instance>
{
	// Token: 0x060021C5 RID: 8645 RVA: 0x000C3464 File Offset: 0x000C1664
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.standing;
		this.root.TagTransition(GameTags.Stored, this.instorage, false).Update("CheckLanded", delegate(FallMonitor.Instance smi, float dt)
		{
			smi.UpdateFalling();
		}, UpdateRate.SIM_33ms, true);
		this.standing.ParamTransition<bool>(this.isEntombed, this.entombed, GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.IsTrue).ParamTransition<bool>(this.isFalling, this.falling_pre, GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.IsTrue);
		this.falling_pre.Enter("StopNavigator", delegate(FallMonitor.Instance smi)
		{
			smi.GetComponent<Navigator>().Stop(false, true);
		}).Enter("AttemptInitialRecovery", delegate(FallMonitor.Instance smi)
		{
			smi.AttemptInitialRecovery();
		}).GoTo(this.falling)
			.ToggleBrain("falling_pre");
		this.falling.ToggleBrain("falling").PlayAnim("fall_pre").QueueAnim("fall_loop", true, null)
			.ParamTransition<bool>(this.isEntombed, this.entombed, GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.IsTrue)
			.Transition(this.recoverladder, (FallMonitor.Instance smi) => smi.CanRecoverToLadder(), UpdateRate.SIM_33ms)
			.Transition(this.recoverpole, (FallMonitor.Instance smi) => smi.CanRecoverToPole(), UpdateRate.SIM_33ms)
			.ToggleGravity(this.landfloor);
		this.recoverinitialfall.ToggleBrain("recoverinitialfall").Enter("Recover", delegate(FallMonitor.Instance smi)
		{
			smi.Recover();
		}).EventTransition(GameHashes.DestinationReached, this.standing, null)
			.EventTransition(GameHashes.NavigationFailed, this.standing, null)
			.Exit(delegate(FallMonitor.Instance smi)
			{
				smi.RecoverEmote();
			});
		this.landfloor.Enter("Land", delegate(FallMonitor.Instance smi)
		{
			smi.LandFloor();
		}).GoTo(this.standing);
		this.recoverladder.ToggleBrain("recoverladder").PlayAnim("floor_ladder_0_0").Enter("MountLadder", delegate(FallMonitor.Instance smi)
		{
			smi.MountLadder();
		})
			.OnAnimQueueComplete(this.standing);
		this.recoverpole.ToggleBrain("recoverpole").PlayAnim("floor_pole_0_0").Enter("MountPole", delegate(FallMonitor.Instance smi)
		{
			smi.MountPole();
		})
			.OnAnimQueueComplete(this.standing);
		this.instorage.TagTransition(GameTags.Stored, this.standing, true);
		this.entombed.DefaultState(this.entombed.recovering);
		this.entombed.recovering.Enter("TryEntombedEscape", delegate(FallMonitor.Instance smi)
		{
			smi.TryEntombedEscape();
		});
		this.entombed.stuck.Enter("StopNavigator", delegate(FallMonitor.Instance smi)
		{
			smi.GetComponent<Navigator>().Stop(false, true);
		}).ToggleChore((FallMonitor.Instance smi) => new EntombedChore(smi.master, smi.entombedAnimOverride), this.standing).ParamTransition<bool>(this.isEntombed, this.standing, GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.IsFalse);
	}

	// Token: 0x040013AE RID: 5038
	public GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.State standing;

	// Token: 0x040013AF RID: 5039
	public GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.State falling_pre;

	// Token: 0x040013B0 RID: 5040
	public GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.State falling;

	// Token: 0x040013B1 RID: 5041
	public FallMonitor.EntombedStates entombed;

	// Token: 0x040013B2 RID: 5042
	public GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.State recoverladder;

	// Token: 0x040013B3 RID: 5043
	public GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.State recoverpole;

	// Token: 0x040013B4 RID: 5044
	public GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.State recoverinitialfall;

	// Token: 0x040013B5 RID: 5045
	public GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.State landfloor;

	// Token: 0x040013B6 RID: 5046
	public GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.State instorage;

	// Token: 0x040013B7 RID: 5047
	public StateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.BoolParameter isEntombed;

	// Token: 0x040013B8 RID: 5048
	public StateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.BoolParameter isFalling;

	// Token: 0x02001451 RID: 5201
	public class EntombedStates : GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x04006C34 RID: 27700
		public GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.State recovering;

		// Token: 0x04006C35 RID: 27701
		public GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.State stuck;
	}

	// Token: 0x02001452 RID: 5202
	public new class Instance : GameStateMachine<FallMonitor, FallMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008D39 RID: 36153 RVA: 0x00357BE0 File Offset: 0x00355DE0
		public Instance(IStateMachineTarget master, bool shouldPlayEmotes, string entombedAnimOverride = null)
			: base(master)
		{
			this.navigator = base.GetComponent<Navigator>();
			this.shouldPlayEmotes = shouldPlayEmotes;
			this.entombedAnimOverride = entombedAnimOverride;
			Pathfinding.Instance.FlushNavGridsOnLoad();
			base.Subscribe(915392638, new Action<object>(this.OnCellChanged));
			base.Subscribe(1027377649, new Action<object>(this.OnMovementStateChanged));
			base.Subscribe(387220196, new Action<object>(this.OnDestinationReached));
		}

		// Token: 0x06008D3A RID: 36154 RVA: 0x00357CE0 File Offset: 0x00355EE0
		private void OnDestinationReached(object data)
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			if (!this.safeCells.Contains(num))
			{
				this.safeCells.Add(num);
				if (this.safeCells.Count > this.MAX_CELLS_TRACKED)
				{
					this.safeCells.RemoveAt(0);
				}
			}
		}

		// Token: 0x06008D3B RID: 36155 RVA: 0x00357D38 File Offset: 0x00355F38
		private void OnMovementStateChanged(object data)
		{
			if ((GameHashes)data == GameHashes.ObjectMovementWakeUp)
			{
				int num = Grid.PosToCell(base.transform.GetPosition());
				if (!this.safeCells.Contains(num))
				{
					this.safeCells.Add(num);
					if (this.safeCells.Count > this.MAX_CELLS_TRACKED)
					{
						this.safeCells.RemoveAt(0);
					}
				}
			}
		}

		// Token: 0x06008D3C RID: 36156 RVA: 0x00357D9C File Offset: 0x00355F9C
		private void OnCellChanged(object data)
		{
			int num = (int)data;
			if (!this.safeCells.Contains(num))
			{
				this.safeCells.Add(num);
				if (this.safeCells.Count > this.MAX_CELLS_TRACKED)
				{
					this.safeCells.RemoveAt(0);
				}
			}
		}

		// Token: 0x06008D3D RID: 36157 RVA: 0x00357DEC File Offset: 0x00355FEC
		public void Recover()
		{
			int num = Grid.PosToCell(this.navigator);
			foreach (NavGrid.Transition transition in this.navigator.NavGrid.transitions)
			{
				if (transition.isEscape && this.navigator.CurrentNavType == transition.start)
				{
					int num2 = transition.IsValid(num, this.navigator.NavGrid.NavTable);
					if (Grid.InvalidCell != num2)
					{
						Vector2I vector2I = Grid.CellToXY(num);
						Vector2I vector2I2 = Grid.CellToXY(num2);
						this.flipRecoverEmote = vector2I2.x < vector2I.x;
						this.navigator.BeginTransition(transition);
						return;
					}
				}
			}
		}

		// Token: 0x06008D3E RID: 36158 RVA: 0x00357EA4 File Offset: 0x003560A4
		public void RecoverEmote()
		{
			if (!this.shouldPlayEmotes)
			{
				return;
			}
			if (global::UnityEngine.Random.Range(0, 9) == 8)
			{
				new EmoteChore(base.master.GetComponent<ChoreProvider>(), Db.Get().ChoreTypes.EmoteHighPriority, Db.Get().Emotes.Minion.CloseCall_Fall, KAnim.PlayMode.Once, 1, this.flipRecoverEmote);
			}
		}

		// Token: 0x06008D3F RID: 36159 RVA: 0x00357F01 File Offset: 0x00356101
		public void LandFloor()
		{
			this.navigator.SetCurrentNavType(NavType.Floor);
			base.GetComponent<Transform>().SetPosition(Grid.CellToPosCBC(Grid.PosToCell(base.GetComponent<Transform>().GetPosition()), Grid.SceneLayer.Move));
		}

		// Token: 0x06008D40 RID: 36160 RVA: 0x00357F34 File Offset: 0x00356134
		public void AttemptInitialRecovery()
		{
			if (base.gameObject.HasTag(GameTags.Incapacitated))
			{
				return;
			}
			int num = Grid.PosToCell(this.navigator);
			foreach (NavGrid.Transition transition in this.navigator.NavGrid.transitions)
			{
				if (transition.isEscape && this.navigator.CurrentNavType == transition.start)
				{
					int num2 = transition.IsValid(num, this.navigator.NavGrid.NavTable);
					if (Grid.InvalidCell != num2)
					{
						base.smi.GoTo(base.smi.sm.recoverinitialfall);
						return;
					}
				}
			}
		}

		// Token: 0x06008D41 RID: 36161 RVA: 0x00357FE4 File Offset: 0x003561E4
		public bool CanRecoverToLadder()
		{
			int num = Grid.PosToCell(base.master.transform.GetPosition());
			return this.navigator.NavGrid.NavTable.IsValid(num, NavType.Ladder) && !base.gameObject.HasTag(GameTags.Incapacitated);
		}

		// Token: 0x06008D42 RID: 36162 RVA: 0x00358035 File Offset: 0x00356235
		public void MountLadder()
		{
			this.navigator.SetCurrentNavType(NavType.Ladder);
			base.GetComponent<Transform>().SetPosition(Grid.CellToPosCBC(Grid.PosToCell(base.GetComponent<Transform>().GetPosition()), Grid.SceneLayer.Move));
		}

		// Token: 0x06008D43 RID: 36163 RVA: 0x00358068 File Offset: 0x00356268
		public bool CanRecoverToPole()
		{
			int num = Grid.PosToCell(base.master.transform.GetPosition());
			return this.navigator.NavGrid.NavTable.IsValid(num, NavType.Pole) && !base.gameObject.HasTag(GameTags.Incapacitated);
		}

		// Token: 0x06008D44 RID: 36164 RVA: 0x003580B9 File Offset: 0x003562B9
		public void MountPole()
		{
			this.navigator.SetCurrentNavType(NavType.Pole);
			base.GetComponent<Transform>().SetPosition(Grid.CellToPosCBC(Grid.PosToCell(base.GetComponent<Transform>().GetPosition()), Grid.SceneLayer.Move));
		}

		// Token: 0x06008D45 RID: 36165 RVA: 0x003580EC File Offset: 0x003562EC
		public void UpdateFalling()
		{
			bool flag = false;
			bool flag2 = false;
			if (!this.navigator.IsMoving() && this.navigator.CurrentNavType != NavType.Tube)
			{
				int num = Grid.PosToCell(base.transform.GetPosition());
				int num2 = Grid.CellAbove(num);
				bool flag3 = Grid.IsValidCell(num);
				bool flag4 = Grid.IsValidCell(num2);
				bool flag5 = this.IsValidNavCell(num);
				flag5 = flag5 && (!base.gameObject.HasTag(GameTags.Incapacitated) || (this.navigator.CurrentNavType != NavType.Ladder && this.navigator.CurrentNavType != NavType.Pole));
				flag2 = (!flag5 && flag3 && Grid.Solid[num] && !Grid.DupePassable[num]) || (flag4 && Grid.Solid[num2] && !Grid.DupePassable[num2]) || (flag3 && Grid.DupeImpassable[num]) || (flag4 && Grid.DupeImpassable[num2]);
				flag = !flag5 && !flag2;
				if ((!flag3 && flag4) || (flag4 && Grid.WorldIdx[num] != Grid.WorldIdx[num2] && Grid.IsWorldValidCell(num2)))
				{
					this.TeleportInWorld(num);
				}
			}
			base.sm.isFalling.Set(flag, base.smi, false);
			base.sm.isEntombed.Set(flag2, base.smi, false);
		}

		// Token: 0x06008D46 RID: 36166 RVA: 0x0035826C File Offset: 0x0035646C
		private void TeleportInWorld(int cell)
		{
			int num = Grid.CellAbove(cell);
			WorldContainer world = ClusterManager.Instance.GetWorld((int)Grid.WorldIdx[num]);
			if (world != null)
			{
				int safeCell = world.GetSafeCell();
				global::Debug.Log(string.Format("Teleporting {0} to {1}", this.navigator.name, safeCell));
				this.MoveToCell(safeCell, false);
				return;
			}
			global::Debug.LogError(string.Format("Unable to teleport {0} stuck on {1}", this.navigator.name, cell));
		}

		// Token: 0x06008D47 RID: 36167 RVA: 0x003582EB File Offset: 0x003564EB
		private bool IsValidNavCell(int cell)
		{
			return this.navigator.NavGrid.NavTable.IsValid(cell, this.navigator.CurrentNavType) && !Grid.DupeImpassable[cell];
		}

		// Token: 0x06008D48 RID: 36168 RVA: 0x00358320 File Offset: 0x00356520
		public void TryEntombedEscape()
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			int backCell = base.GetComponent<Facing>().GetBackCell();
			int num2 = Grid.CellAbove(backCell);
			int num3 = Grid.CellBelow(backCell);
			foreach (int num4 in new int[] { backCell, num2, num3 })
			{
				if (this.IsValidNavCell(num4) && !Grid.HasDoor[num4])
				{
					this.MoveToCell(num4, false);
					return;
				}
			}
			int num5 = Grid.PosToCell(base.transform.GetPosition());
			foreach (CellOffset cellOffset in this.entombedEscapeOffsets)
			{
				if (Grid.IsCellOffsetValid(num5, cellOffset))
				{
					int num6 = Grid.OffsetCell(num5, cellOffset);
					if (this.IsValidNavCell(num6) && !Grid.HasDoor[num6])
					{
						this.MoveToCell(num6, false);
						return;
					}
				}
			}
			for (int k = this.safeCells.Count - 1; k >= 0; k--)
			{
				int num7 = this.safeCells[k];
				if (num7 != num && this.IsValidNavCell(num7) && !Grid.HasDoor[num7])
				{
					this.MoveToCell(num7, false);
					return;
				}
			}
			foreach (CellOffset cellOffset2 in this.entombedEscapeOffsets)
			{
				if (Grid.IsCellOffsetValid(num5, cellOffset2))
				{
					int num8 = Grid.OffsetCell(num5, cellOffset2);
					int num9 = Grid.CellAbove(num8);
					if (Grid.IsValidCell(num9) && !Grid.Solid[num8] && !Grid.Solid[num9] && !Grid.DupeImpassable[num8] && !Grid.DupeImpassable[num9] && !Grid.HasDoor[num8] && !Grid.HasDoor[num9])
					{
						this.MoveToCell(num8, true);
						return;
					}
				}
			}
			this.GoTo(base.sm.entombed.stuck);
		}

		// Token: 0x06008D49 RID: 36169 RVA: 0x00358534 File Offset: 0x00356734
		private void MoveToCell(int cell, bool forceFloorNav = false)
		{
			base.transform.SetPosition(Grid.CellToPosCBC(cell, Grid.SceneLayer.Move));
			base.transform.GetComponent<Navigator>().Stop(false, true);
			if (base.gameObject.HasTag(GameTags.Incapacitated) || forceFloorNav)
			{
				base.transform.GetComponent<Navigator>().SetCurrentNavType(NavType.Floor);
			}
			this.UpdateFalling();
			if (base.sm.isEntombed.Get(base.smi))
			{
				this.GoTo(base.sm.entombed.stuck);
				return;
			}
			this.GoTo(base.sm.standing);
		}

		// Token: 0x04006C36 RID: 27702
		private CellOffset[] entombedEscapeOffsets = new CellOffset[]
		{
			new CellOffset(0, 1),
			new CellOffset(1, 0),
			new CellOffset(-1, 0),
			new CellOffset(1, 1),
			new CellOffset(-1, 1),
			new CellOffset(1, -1),
			new CellOffset(-1, -1)
		};

		// Token: 0x04006C37 RID: 27703
		private Navigator navigator;

		// Token: 0x04006C38 RID: 27704
		private bool shouldPlayEmotes;

		// Token: 0x04006C39 RID: 27705
		public string entombedAnimOverride;

		// Token: 0x04006C3A RID: 27706
		private List<int> safeCells = new List<int>();

		// Token: 0x04006C3B RID: 27707
		private int MAX_CELLS_TRACKED = 3;

		// Token: 0x04006C3C RID: 27708
		private bool flipRecoverEmote;
	}
}
