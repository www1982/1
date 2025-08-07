using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000A63 RID: 2659
public class JungleGasPlant : StateMachineComponent<JungleGasPlant.StatesInstance>
{
	// Token: 0x06004D01 RID: 19713 RVA: 0x001BE492 File Offset: 0x001BC692
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06004D02 RID: 19714 RVA: 0x001BE4A5 File Offset: 0x001BC6A5
	protected void DestroySelf(object callbackParam)
	{
		CreatureHelpers.DeselectCreature(base.gameObject);
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x0400332D RID: 13101
	[MyCmpReq]
	private ReceptacleMonitor rm;

	// Token: 0x0400332E RID: 13102
	[MyCmpReq]
	private Growing growing;

	// Token: 0x0400332F RID: 13103
	[MyCmpReq]
	private WiltCondition wiltCondition;

	// Token: 0x04003330 RID: 13104
	[MyCmpReq]
	private ElementEmitter elementEmitter;

	// Token: 0x02001B26 RID: 6950
	public class StatesInstance : GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.GameInstance
	{
		// Token: 0x0600A63F RID: 42559 RVA: 0x003AC28B File Offset: 0x003AA48B
		public StatesInstance(JungleGasPlant master)
			: base(master)
		{
		}
	}

	// Token: 0x02001B27 RID: 6951
	public class States : GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant>
	{
		// Token: 0x0600A640 RID: 42560 RVA: 0x003AC294 File Offset: 0x003AA494
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.alive.seed_grow;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.root.Enter(delegate(JungleGasPlant.StatesInstance smi)
			{
				if (smi.master.rm.Replanted && !this.alive.ForceUpdateStatus(smi.master.gameObject))
				{
					smi.GoTo(this.blocked_from_growing);
					return;
				}
				smi.GoTo(this.alive.seed_grow);
			});
			GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.State state = this.dead;
			string text = CREATURES.STATUSITEMS.DEAD.NAME;
			string text2 = CREATURES.STATUSITEMS.DEAD.TOOLTIP;
			string text3 = "";
			StatusItem.IconType iconType = StatusItem.IconType.Info;
			NotificationType notificationType = NotificationType.Neutral;
			bool flag = false;
			StatusItemCategory main = Db.Get().StatusItemCategories.Main;
			state.ToggleStatusItem(text, text2, text3, iconType, notificationType, flag, default(HashedString), 129022, null, null, main).Enter(delegate(JungleGasPlant.StatesInstance smi)
			{
				GameUtil.KInstantiate(Assets.GetPrefab(EffectConfigs.PlantDeathId), smi.master.transform.GetPosition(), Grid.SceneLayer.FXFront, null, 0).SetActive(true);
				smi.master.Trigger(1623392196, null);
				smi.master.GetComponent<KBatchedAnimController>().StopAndClear();
				global::UnityEngine.Object.Destroy(smi.master.GetComponent<KBatchedAnimController>());
				smi.Schedule(0.5f, new Action<object>(smi.master.DestroySelf), null);
			});
			this.blocked_from_growing.ToggleStatusItem(Db.Get().MiscStatusItems.RegionIsBlocked, null).TagTransition(GameTags.Entombed, this.alive.seed_grow, true).EventTransition(GameHashes.TooColdWarning, this.alive.seed_grow, null)
				.EventTransition(GameHashes.TooHotWarning, this.alive.seed_grow, null)
				.TagTransition(GameTags.Uprooted, this.dead, false);
			this.alive.InitializeStates(this.masterTarget, this.dead);
			this.alive.seed_grow.QueueAnim("seed_grow", false, null).EventTransition(GameHashes.AnimQueueComplete, this.alive.idle, null).EventTransition(GameHashes.Wilt, this.alive.wilting, (JungleGasPlant.StatesInstance smi) => smi.master.wiltCondition.IsWilting());
			this.alive.idle.EventTransition(GameHashes.Wilt, this.alive.wilting, (JungleGasPlant.StatesInstance smi) => smi.master.wiltCondition.IsWilting()).EventTransition(GameHashes.Grow, this.alive.grown, (JungleGasPlant.StatesInstance smi) => smi.master.growing.IsGrown()).PlayAnim("idle_loop", KAnim.PlayMode.Loop);
			this.alive.grown.DefaultState(this.alive.grown.pre).EventTransition(GameHashes.Wilt, this.alive.wilting, (JungleGasPlant.StatesInstance smi) => smi.master.wiltCondition.IsWilting()).Enter(delegate(JungleGasPlant.StatesInstance smi)
			{
				smi.master.elementEmitter.SetEmitting(true);
			})
				.Exit(delegate(JungleGasPlant.StatesInstance smi)
				{
					smi.master.elementEmitter.SetEmitting(false);
				});
			this.alive.grown.pre.PlayAnim("grow", KAnim.PlayMode.Once).OnAnimQueueComplete(this.alive.grown.idle);
			this.alive.grown.idle.PlayAnim("idle_bloom_loop", KAnim.PlayMode.Loop);
			this.alive.wilting.pre.DefaultState(this.alive.wilting.pre).PlayAnim("wilt_pre", KAnim.PlayMode.Once).OnAnimQueueComplete(this.alive.wilting.idle)
				.EventTransition(GameHashes.WiltRecover, this.alive.wilting.pst, (JungleGasPlant.StatesInstance smi) => !smi.master.wiltCondition.IsWilting());
			this.alive.wilting.idle.PlayAnim("idle_wilt_loop", KAnim.PlayMode.Loop).EventTransition(GameHashes.WiltRecover, this.alive.wilting.pst, (JungleGasPlant.StatesInstance smi) => !smi.master.wiltCondition.IsWilting());
			this.alive.wilting.pst.PlayAnim("wilt_pst", KAnim.PlayMode.Once).OnAnimQueueComplete(this.alive.idle);
		}

		// Token: 0x040081C0 RID: 33216
		public GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.State blocked_from_growing;

		// Token: 0x040081C1 RID: 33217
		public JungleGasPlant.States.AliveStates alive;

		// Token: 0x040081C2 RID: 33218
		public GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.State dead;

		// Token: 0x02002886 RID: 10374
		public class AliveStates : GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.PlantAliveSubState
		{
			// Token: 0x0400B3B1 RID: 46001
			public GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.State seed_grow;

			// Token: 0x0400B3B2 RID: 46002
			public GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.State idle;

			// Token: 0x0400B3B3 RID: 46003
			public JungleGasPlant.States.WiltingState wilting;

			// Token: 0x0400B3B4 RID: 46004
			public JungleGasPlant.States.GrownState grown;

			// Token: 0x0400B3B5 RID: 46005
			public GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.State destroy;
		}

		// Token: 0x02002887 RID: 10375
		public class GrownState : GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.State
		{
			// Token: 0x0400B3B6 RID: 46006
			public GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.State pre;

			// Token: 0x0400B3B7 RID: 46007
			public GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.State idle;
		}

		// Token: 0x02002888 RID: 10376
		public class WiltingState : GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.State
		{
			// Token: 0x0400B3B8 RID: 46008
			public GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.State pre;

			// Token: 0x0400B3B9 RID: 46009
			public GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.State idle;

			// Token: 0x0400B3BA RID: 46010
			public GameStateMachine<JungleGasPlant.States, JungleGasPlant.StatesInstance, JungleGasPlant, object>.State pst;
		}
	}
}
