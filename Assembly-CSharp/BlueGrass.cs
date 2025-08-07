using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000A5C RID: 2652
public class BlueGrass : StateMachineComponent<BlueGrass.StatesInstance>
{
	// Token: 0x06004CCF RID: 19663 RVA: 0x001BD988 File Offset: 0x001BBB88
	protected void DestroySelf(object callbackParam)
	{
		CreatureHelpers.DeselectCreature(base.gameObject);
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x06004CD0 RID: 19664 RVA: 0x001BD9A0 File Offset: 0x001BBBA0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06004CD1 RID: 19665 RVA: 0x001BD9B3 File Offset: 0x001BBBB3
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x06004CD2 RID: 19666 RVA: 0x001BD9BB File Offset: 0x001BBBBB
	protected override void OnPrefabInit()
	{
		base.Subscribe<BlueGrass>(1309017699, BlueGrass.OnReplantedDelegate);
		base.OnPrefabInit();
	}

	// Token: 0x06004CD3 RID: 19667 RVA: 0x001BD9D4 File Offset: 0x001BBBD4
	private void OnReplanted(object data = null)
	{
		this.SetConsumptionRate();
	}

	// Token: 0x06004CD4 RID: 19668 RVA: 0x001BD9DC File Offset: 0x001BBBDC
	public void SetConsumptionRate()
	{
		if (this.receptacleMonitor.Replanted)
		{
			this.elementConsumer.consumptionRate = 0.002f;
			return;
		}
		this.elementConsumer.consumptionRate = 0.0005f;
	}

	// Token: 0x040032F8 RID: 13048
	[MyCmpReq]
	private WiltCondition wiltCondition;

	// Token: 0x040032F9 RID: 13049
	[MyCmpReq]
	private ElementConsumer elementConsumer;

	// Token: 0x040032FA RID: 13050
	[MyCmpReq]
	private ReceptacleMonitor receptacleMonitor;

	// Token: 0x040032FB RID: 13051
	[MyCmpReq]
	private Growing growing;

	// Token: 0x040032FC RID: 13052
	private static readonly EventSystem.IntraObjectHandler<BlueGrass> OnReplantedDelegate = new EventSystem.IntraObjectHandler<BlueGrass>(delegate(BlueGrass component, object data)
	{
		component.OnReplanted(data);
	});

	// Token: 0x02001B18 RID: 6936
	public class StatesInstance : GameStateMachine<BlueGrass.States, BlueGrass.StatesInstance, BlueGrass, object>.GameInstance
	{
		// Token: 0x0600A60D RID: 42509 RVA: 0x003AAD82 File Offset: 0x003A8F82
		public StatesInstance(BlueGrass master)
			: base(master)
		{
		}
	}

	// Token: 0x02001B19 RID: 6937
	public class States : GameStateMachine<BlueGrass.States, BlueGrass.StatesInstance, BlueGrass>
	{
		// Token: 0x0600A60E RID: 42510 RVA: 0x003AAD8C File Offset: 0x003A8F8C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.grow;
			GameStateMachine<BlueGrass.States, BlueGrass.StatesInstance, BlueGrass, object>.State state = this.dead;
			string text = CREATURES.STATUSITEMS.DEAD.NAME;
			string text2 = CREATURES.STATUSITEMS.DEAD.TOOLTIP;
			string text3 = "";
			StatusItem.IconType iconType = StatusItem.IconType.Info;
			NotificationType notificationType = NotificationType.Neutral;
			bool flag = false;
			StatusItemCategory main = Db.Get().StatusItemCategories.Main;
			state.ToggleStatusItem(text, text2, text3, iconType, notificationType, flag, default(HashedString), 129022, null, null, main).Enter(delegate(BlueGrass.StatesInstance smi)
			{
				GameUtil.KInstantiate(Assets.GetPrefab(EffectConfigs.PlantDeathId), smi.master.transform.GetPosition(), Grid.SceneLayer.FXFront, null, 0).SetActive(true);
				smi.master.Trigger(1623392196, null);
				smi.master.GetComponent<KBatchedAnimController>().StopAndClear();
				global::UnityEngine.Object.Destroy(smi.master.GetComponent<KBatchedAnimController>());
				smi.Schedule(0.5f, new Action<object>(smi.master.DestroySelf), null);
			});
			this.blocked_from_growing.ToggleStatusItem(Db.Get().MiscStatusItems.RegionIsBlocked, null).EventTransition(GameHashes.EntombedChanged, this.alive, (BlueGrass.StatesInstance smi) => this.alive.ForceUpdateStatus(smi.master.gameObject)).EventTransition(GameHashes.TooColdWarning, this.alive, (BlueGrass.StatesInstance smi) => this.alive.ForceUpdateStatus(smi.master.gameObject))
				.EventTransition(GameHashes.TooHotWarning, this.alive, (BlueGrass.StatesInstance smi) => this.alive.ForceUpdateStatus(smi.master.gameObject))
				.TagTransition(GameTags.Uprooted, this.dead, false);
			this.grow.Enter(delegate(BlueGrass.StatesInstance smi)
			{
				if (smi.master.receptacleMonitor.HasReceptacle() && !this.alive.ForceUpdateStatus(smi.master.gameObject))
				{
					smi.GoTo(this.blocked_from_growing);
					return;
				}
				smi.GoTo(this.alive);
			});
			this.alive.InitializeStates(this.masterTarget, this.dead).DefaultState(this.alive.growing).Enter(delegate(BlueGrass.StatesInstance smi)
			{
				smi.master.SetConsumptionRate();
			});
			this.alive.growing.EventTransition(GameHashes.Wilt, this.alive.wilting, (BlueGrass.StatesInstance smi) => smi.master.wiltCondition.IsWilting()).Enter(delegate(BlueGrass.StatesInstance smi)
			{
				smi.master.elementConsumer.EnableConsumption(true);
			}).Exit(delegate(BlueGrass.StatesInstance smi)
			{
				smi.master.elementConsumer.EnableConsumption(false);
			})
				.EventTransition(GameHashes.Grow, this.alive.fullygrown, (BlueGrass.StatesInstance smi) => smi.master.growing.IsGrown());
			this.alive.fullygrown.EventTransition(GameHashes.Wilt, this.alive.wilting, (BlueGrass.StatesInstance smi) => smi.master.wiltCondition.IsWilting()).EventTransition(GameHashes.HarvestComplete, this.alive.growing, null);
			this.alive.wilting.EventTransition(GameHashes.WiltRecover, this.alive.growing, (BlueGrass.StatesInstance smi) => !smi.master.wiltCondition.IsWilting());
		}

		// Token: 0x040081A2 RID: 33186
		public GameStateMachine<BlueGrass.States, BlueGrass.StatesInstance, BlueGrass, object>.State grow;

		// Token: 0x040081A3 RID: 33187
		public GameStateMachine<BlueGrass.States, BlueGrass.StatesInstance, BlueGrass, object>.State blocked_from_growing;

		// Token: 0x040081A4 RID: 33188
		public BlueGrass.States.AliveStates alive;

		// Token: 0x040081A5 RID: 33189
		public GameStateMachine<BlueGrass.States, BlueGrass.StatesInstance, BlueGrass, object>.State dead;

		// Token: 0x02002879 RID: 10361
		public class AliveStates : GameStateMachine<BlueGrass.States, BlueGrass.StatesInstance, BlueGrass, object>.PlantAliveSubState
		{
			// Token: 0x0400B36A RID: 45930
			public GameStateMachine<BlueGrass.States, BlueGrass.StatesInstance, BlueGrass, object>.State growing;

			// Token: 0x0400B36B RID: 45931
			public GameStateMachine<BlueGrass.States, BlueGrass.StatesInstance, BlueGrass, object>.State fullygrown;

			// Token: 0x0400B36C RID: 45932
			public GameStateMachine<BlueGrass.States, BlueGrass.StatesInstance, BlueGrass, object>.State wilting;
		}
	}
}
