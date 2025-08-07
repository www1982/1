using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000043 RID: 67
[SerializationConfig(MemberSerialization.OptIn)]
public class ClusterMapLongRangeMissile : GameStateMachine<ClusterMapLongRangeMissile, ClusterMapLongRangeMissile.StatesInstance, IStateMachineTarget, ClusterMapLongRangeMissile.Def>
{
	// Token: 0x06000140 RID: 320 RVA: 0x00009E50 File Offset: 0x00008050
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.initialization;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.root.ToggleTag(GameTags.EntityInSpace);
		this.initialization.Enter(delegate(ClusterMapLongRangeMissile.StatesInstance smi)
		{
			if (smi.exploded)
			{
				smi.GoTo(smi.sm.cleanup);
				return;
			}
			if (this.targetObject.Get(smi) != null)
			{
				smi.GoTo(smi.sm.travelling.moving);
				return;
			}
			smi.GoTo(smi.sm.contact);
		});
		this.travelling.ToggleStatusItem(Db.Get().MiscStatusItems.LongRangeMissileTTI, null).OnTargetLost(this.targetObject, this.contact).Target(this.targetObject)
			.EventHandler(GameHashes.ClusterLocationChanged, new StateMachine<ClusterMapLongRangeMissile, ClusterMapLongRangeMissile.StatesInstance, IStateMachineTarget, ClusterMapLongRangeMissile.Def>.State.Callback(ClusterMapLongRangeMissile.UpdatePath))
			.Target(this.masterTarget);
		this.travelling.moving.ToggleTag(GameTags.LongRangeMissileMoving).EnterTransition(this.travelling.idle, (ClusterMapLongRangeMissile.StatesInstance smi) => !smi.IsTraveling()).EventTransition(GameHashes.ClusterDestinationReached, this.travelling.idle, null);
		this.travelling.idle.ToggleTag(GameTags.LongRangeMissileIdle).Transition(this.contact, new StateMachine<ClusterMapLongRangeMissile, ClusterMapLongRangeMissile.StatesInstance, IStateMachineTarget, ClusterMapLongRangeMissile.Def>.Transition.ConditionCallback(ClusterMapLongRangeMissile.HitTarget), UpdateRate.SIM_1000ms).Transition(this.contact, GameStateMachine<ClusterMapLongRangeMissile, ClusterMapLongRangeMissile.StatesInstance, IStateMachineTarget, ClusterMapLongRangeMissile.Def>.Not(new StateMachine<ClusterMapLongRangeMissile, ClusterMapLongRangeMissile.StatesInstance, IStateMachineTarget, ClusterMapLongRangeMissile.Def>.Transition.ConditionCallback(ClusterMapLongRangeMissile.CanHitTarget)), UpdateRate.SIM_1000ms);
		this.contact.Enter(new StateMachine<ClusterMapLongRangeMissile, ClusterMapLongRangeMissile.StatesInstance, IStateMachineTarget, ClusterMapLongRangeMissile.Def>.State.Callback(ClusterMapLongRangeMissile.TriggerDamage)).EnterTransition(this.exploding_with_visual, new StateMachine<ClusterMapLongRangeMissile, ClusterMapLongRangeMissile.StatesInstance, IStateMachineTarget, ClusterMapLongRangeMissile.Def>.Transition.ConditionCallback(ClusterMapLongRangeMissile.HasVisualizer)).EnterTransition(this.cleanup, GameStateMachine<ClusterMapLongRangeMissile, ClusterMapLongRangeMissile.StatesInstance, IStateMachineTarget, ClusterMapLongRangeMissile.Def>.Not(new StateMachine<ClusterMapLongRangeMissile, ClusterMapLongRangeMissile.StatesInstance, IStateMachineTarget, ClusterMapLongRangeMissile.Def>.Transition.ConditionCallback(ClusterMapLongRangeMissile.HasVisualizer)));
		this.exploding_with_visual.ToggleTag(GameTags.LongRangeMissileExploding).EventTransition(GameHashes.RocketExploded, this.cleanup, null);
		this.cleanup.Enter(delegate(ClusterMapLongRangeMissile.StatesInstance smi)
		{
			smi.gameObject.DeleteObject();
		}).GoTo(null);
	}

	// Token: 0x06000141 RID: 321 RVA: 0x0000A032 File Offset: 0x00008232
	private static bool HasVisualizer(ClusterMapLongRangeMissile.StatesInstance smi)
	{
		return smi != null && ClusterMapScreen.Instance.GetEntityVisAnim(smi.GetComponent<ClusterGridEntity>()) != null;
	}

	// Token: 0x06000142 RID: 322 RVA: 0x0000A050 File Offset: 0x00008250
	public static void TriggerDamage(ClusterMapLongRangeMissile.StatesInstance smi)
	{
		GameObject gameObject = smi.sm.targetObject.Get(smi);
		if (gameObject != null && ClusterMapLongRangeMissile.CanHitTarget(smi))
		{
			gameObject.Trigger(-2056344675, MissileLongRangeConfig.DamageEventPayload.sharedInstance);
		}
		smi.exploded = true;
	}

	// Token: 0x06000143 RID: 323 RVA: 0x0000A098 File Offset: 0x00008298
	public static bool HitTarget(ClusterMapLongRangeMissile.StatesInstance smi)
	{
		ClusterGridEntity clusterGridEntity = smi.sm.targetObject.Get<ClusterGridEntity>(smi);
		return !(clusterGridEntity == null) && clusterGridEntity.Location == smi.sm.destinationHex.Get(smi);
	}

	// Token: 0x06000144 RID: 324 RVA: 0x0000A0DE File Offset: 0x000082DE
	public static bool CanHitTarget(ClusterMapLongRangeMissile.StatesInstance smi)
	{
		return smi.sm.targetObject.Get(smi) != null;
	}

	// Token: 0x06000145 RID: 325 RVA: 0x0000A0F8 File Offset: 0x000082F8
	private static void UpdatePath(ClusterMapLongRangeMissile.StatesInstance smi)
	{
		ClusterDestinationSelector component = smi.GetComponent<ClusterDestinationSelector>();
		if (component == null)
		{
			return;
		}
		ClusterGridEntity clusterGridEntity = smi.sm.targetObject.Get<ClusterGridEntity>(smi);
		if (clusterGridEntity == null)
		{
			return;
		}
		ClusterGridEntity component2 = smi.GetComponent<ClusterGridEntity>();
		AxialI axialI = ClusterMapLongRangeMissile.StatesInstance.FindInterceptPoint(component2.Location, clusterGridEntity, component, 99999);
		if (axialI != smi.sm.destinationHex.Get(smi))
		{
			smi.Travel(component2.Location, axialI);
		}
	}

	// Token: 0x040000C7 RID: 199
	public StateMachine<ClusterMapLongRangeMissile, ClusterMapLongRangeMissile.StatesInstance, IStateMachineTarget, ClusterMapLongRangeMissile.Def>.TargetParameter targetObject;

	// Token: 0x040000C8 RID: 200
	public StateMachine<ClusterMapLongRangeMissile, ClusterMapLongRangeMissile.StatesInstance, IStateMachineTarget, ClusterMapLongRangeMissile.Def>.AxialIParameter destinationHex;

	// Token: 0x040000C9 RID: 201
	public GameStateMachine<ClusterMapLongRangeMissile, ClusterMapLongRangeMissile.StatesInstance, IStateMachineTarget, ClusterMapLongRangeMissile.Def>.State initialization;

	// Token: 0x040000CA RID: 202
	public ClusterMapLongRangeMissile.TravellingStates travelling;

	// Token: 0x040000CB RID: 203
	public GameStateMachine<ClusterMapLongRangeMissile, ClusterMapLongRangeMissile.StatesInstance, IStateMachineTarget, ClusterMapLongRangeMissile.Def>.State contact;

	// Token: 0x040000CC RID: 204
	public GameStateMachine<ClusterMapLongRangeMissile, ClusterMapLongRangeMissile.StatesInstance, IStateMachineTarget, ClusterMapLongRangeMissile.Def>.State exploding_with_visual;

	// Token: 0x040000CD RID: 205
	public GameStateMachine<ClusterMapLongRangeMissile, ClusterMapLongRangeMissile.StatesInstance, IStateMachineTarget, ClusterMapLongRangeMissile.Def>.State cleanup;

	// Token: 0x02001030 RID: 4144
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001031 RID: 4145
	public class TravellingStates : GameStateMachine<ClusterMapLongRangeMissile, ClusterMapLongRangeMissile.StatesInstance, IStateMachineTarget, ClusterMapLongRangeMissile.Def>.State
	{
		// Token: 0x04006004 RID: 24580
		public GameStateMachine<ClusterMapLongRangeMissile, ClusterMapLongRangeMissile.StatesInstance, IStateMachineTarget, ClusterMapLongRangeMissile.Def>.State moving;

		// Token: 0x04006005 RID: 24581
		public GameStateMachine<ClusterMapLongRangeMissile, ClusterMapLongRangeMissile.StatesInstance, IStateMachineTarget, ClusterMapLongRangeMissile.Def>.State idle;
	}

	// Token: 0x02001032 RID: 4146
	public class StatesInstance : GameStateMachine<ClusterMapLongRangeMissile, ClusterMapLongRangeMissile.StatesInstance, IStateMachineTarget, ClusterMapLongRangeMissile.Def>.GameInstance
	{
		// Token: 0x06007F45 RID: 32581 RVA: 0x0032BCAC File Offset: 0x00329EAC
		public StatesInstance(IStateMachineTarget master, ClusterMapLongRangeMissile.Def def)
			: base(master, def)
		{
			this.animController = base.GetComponent<KBatchedAnimController>();
		}

		// Token: 0x06007F46 RID: 32582 RVA: 0x0032BCC2 File Offset: 0x00329EC2
		public void Setup(AxialI source, ClusterGridEntity target)
		{
			base.sm.targetObject.Set(target.gameObject, this, false);
			this.Travel(source, ClusterMapLongRangeMissile.StatesInstance.FindInterceptPoint(source, target, base.GetComponent<ClusterDestinationSelector>(), 99999));
		}

		// Token: 0x06007F47 RID: 32583 RVA: 0x0032BCF8 File Offset: 0x00329EF8
		public static AxialI FindInterceptPoint(AxialI source, ClusterGridEntity target, ClusterDestinationSelector selector, int maxGridRange = 99999)
		{
			ClusterTraveler component = target.GetComponent<ClusterTraveler>();
			if (component != null)
			{
				List<AxialI> currentPath = component.CurrentPath;
				AxialI axialI = target.Location;
				foreach (AxialI axialI2 in currentPath)
				{
					float num = component.TravelETA(axialI2);
					List<AxialI> path = ClusterGrid.Instance.GetPath(source, axialI2, selector);
					if (path != null && path.Count != 0 && path.Count <= maxGridRange && (float)path.Count * 600f / 10f < num)
					{
						return axialI;
					}
					axialI = axialI2;
				}
			}
			return target.Location;
		}

		// Token: 0x06007F48 RID: 32584 RVA: 0x0032BDB8 File Offset: 0x00329FB8
		public float InterceptETA()
		{
			ClusterTraveler component = base.GetComponent<ClusterTraveler>();
			float num = 0f;
			float num2 = component.TravelETA();
			GameObject gameObject = base.sm.targetObject.Get(this);
			if (gameObject != null)
			{
				ClusterTraveler component2 = gameObject.GetComponent<ClusterTraveler>();
				if (component2 != null)
				{
					num = component2.TravelETA(component.Destination);
				}
			}
			return Mathf.Max(num, num2);
		}

		// Token: 0x06007F49 RID: 32585 RVA: 0x0032BE1B File Offset: 0x0032A01B
		public void Travel(AxialI source, AxialI destination)
		{
			base.GetComponent<BallisticClusterGridEntity>().Configure(source, destination);
			base.sm.destinationHex.Set(destination, this, false);
			this.GoTo(base.sm.travelling.moving);
		}

		// Token: 0x06007F4A RID: 32586 RVA: 0x0032BE54 File Offset: 0x0032A054
		public bool IsTraveling()
		{
			return base.GetComponent<ClusterTraveler>().CurrentPath.Count != 0;
		}

		// Token: 0x04006006 RID: 24582
		[Serialize]
		public bool exploded;

		// Token: 0x04006007 RID: 24583
		public KBatchedAnimController animController;
	}
}
