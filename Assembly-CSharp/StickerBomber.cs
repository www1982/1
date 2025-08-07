using System;
using KSerialization;
using TUNING;
using UnityEngine;

// Token: 0x020004C6 RID: 1222
public class StickerBomber : GameStateMachine<StickerBomber, StickerBomber.Instance>
{
	// Token: 0x06001A26 RID: 6694 RVA: 0x0008FA84 File Offset: 0x0008DC84
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.neutral;
		this.root.TagTransition(GameTags.Dead, null, false);
		this.neutral.TagTransition(GameTags.Overjoyed, this.overjoyed, false).Exit(delegate(StickerBomber.Instance smi)
		{
			smi.nextStickerBomb = GameClock.Instance.GetTime() + TRAITS.JOY_REACTIONS.STICKER_BOMBER.TIME_PER_STICKER_BOMB;
		});
		this.overjoyed.TagTransition(GameTags.Overjoyed, this.neutral, true).DefaultState(this.overjoyed.idle).ToggleStatusItem(Db.Get().DuplicantStatusItems.JoyResponse_StickerBombing, null);
		this.overjoyed.idle.Transition(this.overjoyed.place_stickers, (StickerBomber.Instance smi) => GameClock.Instance.GetTime() >= smi.nextStickerBomb, UpdateRate.SIM_200ms);
		this.overjoyed.place_stickers.Exit(delegate(StickerBomber.Instance smi)
		{
			smi.nextStickerBomb = GameClock.Instance.GetTime() + TRAITS.JOY_REACTIONS.STICKER_BOMBER.TIME_PER_STICKER_BOMB;
		}).ToggleReactable((StickerBomber.Instance smi) => smi.CreateReactable()).OnSignal(this.doneStickerBomb, this.overjoyed.idle);
	}

	// Token: 0x04000F0A RID: 3850
	public StateMachine<StickerBomber, StickerBomber.Instance, IStateMachineTarget, object>.Signal doneStickerBomb;

	// Token: 0x04000F0B RID: 3851
	public GameStateMachine<StickerBomber, StickerBomber.Instance, IStateMachineTarget, object>.State neutral;

	// Token: 0x04000F0C RID: 3852
	public StickerBomber.OverjoyedStates overjoyed;

	// Token: 0x0200131D RID: 4893
	public class OverjoyedStates : GameStateMachine<StickerBomber, StickerBomber.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x0400685F RID: 26719
		public GameStateMachine<StickerBomber, StickerBomber.Instance, IStateMachineTarget, object>.State idle;

		// Token: 0x04006860 RID: 26720
		public GameStateMachine<StickerBomber, StickerBomber.Instance, IStateMachineTarget, object>.State place_stickers;
	}

	// Token: 0x0200131E RID: 4894
	public new class Instance : GameStateMachine<StickerBomber, StickerBomber.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x060088C1 RID: 35009 RVA: 0x00349DDB File Offset: 0x00347FDB
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x060088C2 RID: 35010 RVA: 0x00349DE4 File Offset: 0x00347FE4
		public Reactable CreateReactable()
		{
			return new StickerBomber.Instance.StickerBombReactable(base.master.gameObject, base.smi);
		}

		// Token: 0x04006861 RID: 26721
		[Serialize]
		public float nextStickerBomb;

		// Token: 0x02002696 RID: 9878
		private class StickerBombReactable : Reactable
		{
			// Token: 0x0600C43D RID: 50237 RVA: 0x0040D3A4 File Offset: 0x0040B5A4
			public StickerBombReactable(GameObject gameObject, StickerBomber.Instance stickerBomber)
				: base(gameObject, "StickerBombReactable", Db.Get().ChoreTypes.Build, 2, 1, false, 0f, 0f, float.PositiveInfinity, 0f, ObjectLayer.NumLayers)
			{
				this.preventChoreInterruption = true;
				this.stickerBomber = stickerBomber;
			}

			// Token: 0x0600C43E RID: 50238 RVA: 0x0040D484 File Offset: 0x0040B684
			public override bool InternalCanBegin(GameObject new_reactor, Navigator.ActiveTransition transition)
			{
				if (this.reactor != null)
				{
					return false;
				}
				if (new_reactor == null)
				{
					return false;
				}
				if (this.gameObject != new_reactor)
				{
					return false;
				}
				Navigator component = new_reactor.GetComponent<Navigator>();
				return !(component == null) && component.CurrentNavType != NavType.Tube && component.CurrentNavType != NavType.Ladder && component.CurrentNavType != NavType.Pole;
			}

			// Token: 0x0600C43F RID: 50239 RVA: 0x0040D4EC File Offset: 0x0040B6EC
			protected override void InternalBegin()
			{
				this.stickersToPlace = global::UnityEngine.Random.Range(4, 6);
				this.STICKER_PLACE_TIMER = this.TIME_PER_STICKER_PLACED;
				this.placementCell = this.FindPlacementCell();
				if (this.placementCell == 0)
				{
					base.End();
					return;
				}
				this.kbac = this.reactor.GetComponent<KBatchedAnimController>();
				this.kbac.AddAnimOverrides(this.animset, 0f);
				this.kbac.Play(this.pre_anim, KAnim.PlayMode.Once, 1f, 0f);
				this.kbac.Queue(this.loop_anim, KAnim.PlayMode.Loop, 1f, 0f);
			}

			// Token: 0x0600C440 RID: 50240 RVA: 0x0040D58C File Offset: 0x0040B78C
			public override void Update(float dt)
			{
				this.STICKER_PLACE_TIMER -= dt;
				if (this.STICKER_PLACE_TIMER <= 0f)
				{
					this.PlaceSticker();
					this.STICKER_PLACE_TIMER = this.TIME_PER_STICKER_PLACED;
				}
				if (this.stickersPlaced >= this.stickersToPlace)
				{
					this.kbac.Play(this.pst_anim, KAnim.PlayMode.Once, 1f, 0f);
					base.End();
				}
			}

			// Token: 0x0600C441 RID: 50241 RVA: 0x0040D5F8 File Offset: 0x0040B7F8
			protected override void InternalEnd()
			{
				if (this.kbac != null)
				{
					this.kbac.RemoveAnimOverrides(this.animset);
					this.kbac = null;
				}
				this.stickerBomber.sm.doneStickerBomb.Trigger(this.stickerBomber);
				this.stickersPlaced = 0;
			}

			// Token: 0x0600C442 RID: 50242 RVA: 0x0040D650 File Offset: 0x0040B850
			private int FindPlacementCell()
			{
				int num = Grid.PosToCell(this.reactor.transform.GetPosition() + Vector3.up);
				ListPool<int, PathFinder>.PooledList pooledList = ListPool<int, PathFinder>.Allocate();
				ListPool<int, PathFinder>.PooledList pooledList2 = ListPool<int, PathFinder>.Allocate();
				QueuePool<GameUtil.FloodFillInfo, Comet>.PooledQueue pooledQueue = QueuePool<GameUtil.FloodFillInfo, Comet>.Allocate();
				pooledQueue.Enqueue(new GameUtil.FloodFillInfo
				{
					cell = num,
					depth = 0
				});
				GameUtil.FloodFillConditional(pooledQueue, this.canPlaceStickerCb, pooledList, pooledList2, 2);
				if (pooledList2.Count > 0)
				{
					int random = pooledList2.GetRandom<int>();
					pooledList.Recycle();
					pooledList2.Recycle();
					pooledQueue.Recycle();
					return random;
				}
				pooledList.Recycle();
				pooledList2.Recycle();
				pooledQueue.Recycle();
				return 0;
			}

			// Token: 0x0600C443 RID: 50243 RVA: 0x0040D6F4 File Offset: 0x0040B8F4
			private void PlaceSticker()
			{
				this.stickersPlaced++;
				Vector3 vector = Grid.CellToPos(this.placementCell);
				int i = 10;
				while (i > 0)
				{
					i--;
					Vector3 vector2 = vector + new Vector3(global::UnityEngine.Random.Range(-this.tile_random_range, this.tile_random_range), global::UnityEngine.Random.Range(-this.tile_random_range, this.tile_random_range), -2.5f);
					if (StickerBomb.CanPlaceSticker(StickerBomb.BuildCellOffsets(vector2)))
					{
						GameObject gameObject = Util.KInstantiate(Assets.GetPrefab("StickerBomb".ToTag()), vector2, Quaternion.Euler(0f, 0f, global::UnityEngine.Random.Range(-this.tile_random_rotation, this.tile_random_rotation)), null, null, true, 0);
						StickerBomb component = gameObject.GetComponent<StickerBomb>();
						string stickerType = this.reactor.GetComponent<MinionIdentity>().stickerType;
						component.SetStickerType(stickerType);
						gameObject.SetActive(true);
						i = 0;
					}
				}
			}

			// Token: 0x0600C444 RID: 50244 RVA: 0x0040D7CF File Offset: 0x0040B9CF
			protected override void InternalCleanup()
			{
			}

			// Token: 0x0400AB79 RID: 43897
			private int stickersToPlace;

			// Token: 0x0400AB7A RID: 43898
			private int stickersPlaced;

			// Token: 0x0400AB7B RID: 43899
			private int placementCell;

			// Token: 0x0400AB7C RID: 43900
			private float tile_random_range = 1f;

			// Token: 0x0400AB7D RID: 43901
			private float tile_random_rotation = 90f;

			// Token: 0x0400AB7E RID: 43902
			private float TIME_PER_STICKER_PLACED = 0.66f;

			// Token: 0x0400AB7F RID: 43903
			private float STICKER_PLACE_TIMER;

			// Token: 0x0400AB80 RID: 43904
			private KBatchedAnimController kbac;

			// Token: 0x0400AB81 RID: 43905
			private KAnimFile animset = Assets.GetAnim("anim_stickers_kanim");

			// Token: 0x0400AB82 RID: 43906
			private HashedString pre_anim = "working_pre";

			// Token: 0x0400AB83 RID: 43907
			private HashedString loop_anim = "working_loop";

			// Token: 0x0400AB84 RID: 43908
			private HashedString pst_anim = "working_pst";

			// Token: 0x0400AB85 RID: 43909
			private StickerBomber.Instance stickerBomber;

			// Token: 0x0400AB86 RID: 43910
			private Func<int, bool> canPlaceStickerCb = (int cell) => !Grid.Solid[cell] && (!Grid.IsValidCell(Grid.CellLeft(cell)) || !Grid.Solid[Grid.CellLeft(cell)]) && (!Grid.IsValidCell(Grid.CellRight(cell)) || !Grid.Solid[Grid.CellRight(cell)]) && (!Grid.IsValidCell(Grid.OffsetCell(cell, 0, 1)) || !Grid.Solid[Grid.OffsetCell(cell, 0, 1)]) && (!Grid.IsValidCell(Grid.OffsetCell(cell, 0, -1)) || !Grid.Solid[Grid.OffsetCell(cell, 0, -1)]) && !Grid.IsCellOpenToSpace(cell);
		}
	}
}
