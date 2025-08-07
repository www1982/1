using System;
using UnityEngine;

// Token: 0x02000B4C RID: 2892
public class LargeImpactorStatus : GameStateMachine<LargeImpactorStatus, LargeImpactorStatus.Instance, IStateMachineTarget, LargeImpactorStatus.Def>
{
	// Token: 0x0600561A RID: 22042 RVA: 0x001F3A90 File Offset: 0x001F1C90
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.alive;
		this.alive.ParamTransition<bool>(this.HasArrived, this.landing, GameStateMachine<LargeImpactorStatus, LargeImpactorStatus.Instance, IStateMachineTarget, LargeImpactorStatus.Def>.IsTrue).ParamTransition<int>(this.Health, this.destroyed, GameStateMachine<LargeImpactorStatus, LargeImpactorStatus.Instance, IStateMachineTarget, LargeImpactorStatus.Def>.IsZero_Int).EventHandler(GameHashes.MissileDamageEncountered, new GameStateMachine<LargeImpactorStatus, LargeImpactorStatus.Instance, IStateMachineTarget, LargeImpactorStatus.Def>.GameEvent.Callback(LargeImpactorStatus.HandleIncommingDamage))
			.ToggleStatusItem(Db.Get().MiscStatusItems.ImpactorHealth, null)
			.EventTransition(GameHashes.ClusterDestinationReached, this.landing, null)
			.UpdateTransition(this.landing, new Func<LargeImpactorStatus.Instance, float, bool>(LargeImpactorStatus.CheckArrivalUpdate), UpdateRate.SIM_200ms, false);
		this.landing.Enter(new StateMachine<LargeImpactorStatus, LargeImpactorStatus.Instance, IStateMachineTarget, LargeImpactorStatus.Def>.State.Callback(LargeImpactorStatus.SetHasArrived)).TriggerOnEnter(GameHashes.LargeImpactorArrived, null);
		this.destroyed.TriggerOnEnter(GameHashes.Died, null);
	}

	// Token: 0x0600561B RID: 22043 RVA: 0x001F3B69 File Offset: 0x001F1D69
	private static void HandleIncommingDamage(LargeImpactorStatus.Instance smi, object obj)
	{
		LargeImpactorStatus.DealDamage(smi, (obj as MissileLongRangeConfig.DamageEventPayload).damage);
	}

	// Token: 0x0600561C RID: 22044 RVA: 0x001F3B7C File Offset: 0x001F1D7C
	private static void SetHasArrived(LargeImpactorStatus.Instance smi)
	{
		smi.sm.HasArrived.Set(true, smi, false);
	}

	// Token: 0x0600561D RID: 22045 RVA: 0x001F3B92 File Offset: 0x001F1D92
	private static void DealDamage(LargeImpactorStatus.Instance smi, int damage)
	{
		smi.DealDamage(damage);
	}

	// Token: 0x0600561E RID: 22046 RVA: 0x001F3B9B File Offset: 0x001F1D9B
	private static void DeleteObject(LargeImpactorStatus.Instance smi)
	{
		smi.gameObject.DeleteObject();
	}

	// Token: 0x0600561F RID: 22047 RVA: 0x001F3BA8 File Offset: 0x001F1DA8
	private static bool CheckArrivalUpdate(LargeImpactorStatus.Instance smi, float dt)
	{
		return smi.TimeRemainingBeforeCollision <= 0f;
	}

	// Token: 0x0400399C RID: 14748
	public StateMachine<LargeImpactorStatus, LargeImpactorStatus.Instance, IStateMachineTarget, LargeImpactorStatus.Def>.IntParameter Health;

	// Token: 0x0400399D RID: 14749
	public StateMachine<LargeImpactorStatus, LargeImpactorStatus.Instance, IStateMachineTarget, LargeImpactorStatus.Def>.BoolParameter HasArrived;

	// Token: 0x0400399E RID: 14750
	public GameStateMachine<LargeImpactorStatus, LargeImpactorStatus.Instance, IStateMachineTarget, LargeImpactorStatus.Def>.State alive;

	// Token: 0x0400399F RID: 14751
	public GameStateMachine<LargeImpactorStatus, LargeImpactorStatus.Instance, IStateMachineTarget, LargeImpactorStatus.Def>.State landing;

	// Token: 0x040039A0 RID: 14752
	public GameStateMachine<LargeImpactorStatus, LargeImpactorStatus.Instance, IStateMachineTarget, LargeImpactorStatus.Def>.State destroyed;

	// Token: 0x02001C72 RID: 7282
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x0400865F RID: 34399
		public int MAX_HEALTH;

		// Token: 0x04008660 RID: 34400
		public string EventID;
	}

	// Token: 0x02001C73 RID: 7283
	public new class Instance : GameStateMachine<LargeImpactorStatus, LargeImpactorStatus.Instance, IStateMachineTarget, LargeImpactorStatus.Def>.GameInstance
	{
		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x0600AB1B RID: 43803 RVA: 0x003BCF66 File Offset: 0x003BB166
		public int Health
		{
			get
			{
				return base.sm.Health.Get(this);
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x0600AB1C RID: 43804 RVA: 0x003BCF79 File Offset: 0x003BB179
		public float ArrivalTime
		{
			get
			{
				if (!(this.clusterTraveler == null))
				{
					return this.ArrivalTime_SO;
				}
				return this.ArrivalTime_Vanilla;
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x0600AB1D RID: 43805 RVA: 0x003BCF96 File Offset: 0x003BB196
		public float TimeRemainingBeforeCollision
		{
			get
			{
				if (!(this.clusterTraveler == null))
				{
					return this.TimeRemainingBeforeCollision_SO;
				}
				return this.TimeRemainingBeforeCollision_Vanilla;
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x0600AB1E RID: 43806 RVA: 0x003BCFB3 File Offset: 0x003BB1B3
		private float ArrivalTime_Vanilla
		{
			get
			{
				return this.eventInstance.eventStartTime * 600f + LargeImpactorEvent.GetImpactTime();
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x0600AB1F RID: 43807 RVA: 0x003BCFCC File Offset: 0x003BB1CC
		private float TimeRemainingBeforeCollision_Vanilla
		{
			get
			{
				return Mathf.Clamp(this.ArrivalTime_Vanilla - GameUtil.GetCurrentTimeInCycles() * 600f, 0f, float.MaxValue);
			}
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x0600AB20 RID: 43808 RVA: 0x003BCFEF File Offset: 0x003BB1EF
		private float ArrivalTime_SO
		{
			get
			{
				return GameUtil.GetCurrentTimeInCycles() * 600f + this.TimeRemainingBeforeCollision_SO;
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x0600AB21 RID: 43809 RVA: 0x003BD003 File Offset: 0x003BB203
		private float TimeRemainingBeforeCollision_SO
		{
			get
			{
				return Mathf.Clamp(this.clusterTraveler.EstimatedTimeToReachDestination(), 0f, float.MaxValue);
			}
		}

		// Token: 0x0600AB22 RID: 43810 RVA: 0x003BD01F File Offset: 0x003BB21F
		public Instance(IStateMachineTarget master, LargeImpactorStatus.Def def)
			: base(master, def)
		{
			base.sm.Health.Set(def.MAX_HEALTH, base.smi, false);
		}

		// Token: 0x0600AB23 RID: 43811 RVA: 0x003BD047 File Offset: 0x003BB247
		public override void StartSM()
		{
			this.clusterTraveler = base.GetComponent<ClusterTraveler>();
			this.eventInstance = GameplayEventManager.Instance.GetGameplayEventInstance(base.def.EventID, -1);
			base.StartSM();
		}

		// Token: 0x0600AB24 RID: 43812 RVA: 0x003BD07C File Offset: 0x003BB27C
		public void DealDamage(int damage)
		{
			int num = Mathf.Clamp(this.Health - damage, 0, base.def.MAX_HEALTH);
			base.sm.Health.Set(num, this, false);
			Action<int> onDamaged = this.OnDamaged;
			if (onDamaged == null)
			{
				return;
			}
			onDamaged(this.Health);
		}

		// Token: 0x04008661 RID: 34401
		public Action<int> OnDamaged;

		// Token: 0x04008662 RID: 34402
		private ClusterTraveler clusterTraveler;

		// Token: 0x04008663 RID: 34403
		private GameplayEventInstance eventInstance;
	}
}
