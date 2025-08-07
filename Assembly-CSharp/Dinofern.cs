using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000A60 RID: 2656
public class Dinofern : StateMachineComponent<Dinofern.StatesInstance>
{
	// Token: 0x06004CF5 RID: 19701 RVA: 0x001BE188 File Offset: 0x001BC388
	protected void DestroySelf(object callbackParam)
	{
		CreatureHelpers.DeselectCreature(base.gameObject);
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x06004CF6 RID: 19702 RVA: 0x001BE1A0 File Offset: 0x001BC3A0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06004CF7 RID: 19703 RVA: 0x001BE1B3 File Offset: 0x001BC3B3
	public void SetConsumptionRate()
	{
		if (this.receptacleMonitor.Replanted)
		{
			this.elementConsumer.consumptionRate = 0.09f;
			return;
		}
		this.elementConsumer.consumptionRate = 0.0225f;
	}

	// Token: 0x0400331B RID: 13083
	[MyCmpReq]
	private WiltCondition wiltCondition;

	// Token: 0x0400331C RID: 13084
	[MyCmpReq]
	private ElementConsumer elementConsumer;

	// Token: 0x0400331D RID: 13085
	[MyCmpReq]
	private ReceptacleMonitor receptacleMonitor;

	// Token: 0x0400331E RID: 13086
	private Growing growing;

	// Token: 0x02001B21 RID: 6945
	public class StatesInstance : GameStateMachine<Dinofern.States, Dinofern.StatesInstance, Dinofern, object>.GameInstance
	{
		// Token: 0x0600A62E RID: 42542 RVA: 0x003ABBFA File Offset: 0x003A9DFA
		public StatesInstance(Dinofern master)
			: base(master)
		{
			master.growing = base.GetComponent<Growing>();
		}
	}

	// Token: 0x02001B22 RID: 6946
	public class States : GameStateMachine<Dinofern.States, Dinofern.StatesInstance, Dinofern>
	{
		// Token: 0x0600A62F RID: 42543 RVA: 0x003ABC10 File Offset: 0x003A9E10
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			default_state = this.grow;
			GameStateMachine<Dinofern.States, Dinofern.StatesInstance, Dinofern, object>.State state = this.dead;
			string text = CREATURES.STATUSITEMS.DEAD.NAME;
			string text2 = CREATURES.STATUSITEMS.DEAD.TOOLTIP;
			string text3 = "";
			StatusItem.IconType iconType = StatusItem.IconType.Info;
			NotificationType notificationType = NotificationType.Neutral;
			bool flag = false;
			StatusItemCategory main = Db.Get().StatusItemCategories.Main;
			state.ToggleStatusItem(text, text2, text3, iconType, notificationType, flag, default(HashedString), 129022, null, null, main).Enter(delegate(Dinofern.StatesInstance smi)
			{
				GameUtil.KInstantiate(Assets.GetPrefab(EffectConfigs.PlantDeathId), smi.master.transform.GetPosition(), Grid.SceneLayer.FXFront, null, 0).SetActive(true);
				smi.master.Trigger(1623392196, null);
				smi.master.GetComponent<KBatchedAnimController>().StopAndClear();
				global::UnityEngine.Object.Destroy(smi.master.GetComponent<KBatchedAnimController>());
				smi.Schedule(0.5f, new Action<object>(smi.master.DestroySelf), null);
			});
			this.blocked_from_growing.ToggleStatusItem(Db.Get().MiscStatusItems.RegionIsBlocked, null).EventTransition(GameHashes.EntombedChanged, this.alive, (Dinofern.StatesInstance smi) => this.alive.ForceUpdateStatus(smi.master.gameObject)).EventTransition(GameHashes.TooColdWarning, this.alive, (Dinofern.StatesInstance smi) => this.alive.ForceUpdateStatus(smi.master.gameObject))
				.EventTransition(GameHashes.TooHotWarning, this.alive, (Dinofern.StatesInstance smi) => this.alive.ForceUpdateStatus(smi.master.gameObject))
				.TagTransition(GameTags.Uprooted, this.dead, false);
			this.grow.Enter(delegate(Dinofern.StatesInstance smi)
			{
				if (smi.master.receptacleMonitor.HasReceptacle() && !this.alive.ForceUpdateStatus(smi.master.gameObject))
				{
					smi.GoTo(this.blocked_from_growing);
				}
			}).EventTransition(GameHashes.AnimQueueComplete, this.alive, null);
			this.alive.InitializeStates(this.masterTarget, this.dead).DefaultState(this.alive.growing);
			this.alive.growing.Transition(this.alive.mature, (Dinofern.StatesInstance smi) => smi.master.growing.IsGrown(), UpdateRate.SIM_200ms).EventTransition(GameHashes.Wilt, this.alive.wilting, (Dinofern.StatesInstance smi) => smi.master.wiltCondition.IsWilting()).Enter(delegate(Dinofern.StatesInstance smi)
			{
				smi.master.elementConsumer.EnableConsumption(true);
			})
				.Exit(delegate(Dinofern.StatesInstance smi)
				{
					smi.master.elementConsumer.EnableConsumption(false);
				});
			this.alive.mature.Transition(this.alive.growing, (Dinofern.StatesInstance smi) => !smi.master.growing.IsGrown(), UpdateRate.SIM_200ms).EventTransition(GameHashes.Wilt, this.alive.wilting, (Dinofern.StatesInstance smi) => smi.master.wiltCondition.IsWilting());
			this.alive.wilting.EventTransition(GameHashes.WiltRecover, this.alive.growing, (Dinofern.StatesInstance smi) => !smi.master.wiltCondition.IsWilting());
		}

		// Token: 0x040081B7 RID: 33207
		public GameStateMachine<Dinofern.States, Dinofern.StatesInstance, Dinofern, object>.State grow;

		// Token: 0x040081B8 RID: 33208
		public GameStateMachine<Dinofern.States, Dinofern.StatesInstance, Dinofern, object>.State blocked_from_growing;

		// Token: 0x040081B9 RID: 33209
		public Dinofern.States.AliveStates alive;

		// Token: 0x040081BA RID: 33210
		public GameStateMachine<Dinofern.States, Dinofern.StatesInstance, Dinofern, object>.State dead;

		// Token: 0x02002881 RID: 10369
		public class AliveStates : GameStateMachine<Dinofern.States, Dinofern.StatesInstance, Dinofern, object>.PlantAliveSubState
		{
			// Token: 0x0400B39B RID: 45979
			public GameStateMachine<Dinofern.States, Dinofern.StatesInstance, Dinofern, object>.State growing;

			// Token: 0x0400B39C RID: 45980
			public GameStateMachine<Dinofern.States, Dinofern.StatesInstance, Dinofern, object>.State mature;

			// Token: 0x0400B39D RID: 45981
			public GameStateMachine<Dinofern.States, Dinofern.StatesInstance, Dinofern, object>.State wilting;
		}
	}
}
