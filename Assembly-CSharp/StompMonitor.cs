using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005A5 RID: 1445
public class StompMonitor : GameStateMachine<StompMonitor, StompMonitor.Instance, IStateMachineTarget, StompMonitor.Def>
{
	// Token: 0x060020FC RID: 8444 RVA: 0x000BE70C File Offset: 0x000BC90C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.cooldown;
		this.cooldown.ParamTransition<float>(this.TimeSinceLastStomp, this.stomp, new StateMachine<StompMonitor, StompMonitor.Instance, IStateMachineTarget, StompMonitor.Def>.Parameter<float>.Callback(StompMonitor.IsTimeToStomp)).Update(new Action<StompMonitor.Instance, float>(StompMonitor.CooldownTick), UpdateRate.SIM_200ms, false);
		this.stomp.ParamTransition<float>(this.TimeSinceLastStomp, this.cooldown, GameStateMachine<StompMonitor, StompMonitor.Instance, IStateMachineTarget, StompMonitor.Def>.IsLTEZero).DefaultState(this.stomp.lookingForTarget);
		this.stomp.lookingForTarget.ParamTransition<GameObject>(this.TargetPlant, this.stomp.stomping, GameStateMachine<StompMonitor, StompMonitor.Instance, IStateMachineTarget, StompMonitor.Def>.IsNotNull).PreBrainUpdate(new Action<StompMonitor.Instance>(StompMonitor.LookForTarget));
		this.stomp.stomping.Enter(new StateMachine<StompMonitor, StompMonitor.Instance, IStateMachineTarget, StompMonitor.Def>.State.Callback(StompMonitor.ReservePlant)).OnSignal(this.StompStateFailed, this.stomp.lookingForTarget).ToggleBehaviour(GameTags.Creatures.WantsToStomp, (StompMonitor.Instance smi) => smi.Target != null, new Action<StompMonitor.Instance>(StompMonitor.OnStompCompleted))
			.Exit(new StateMachine<StompMonitor, StompMonitor.Instance, IStateMachineTarget, StompMonitor.Def>.State.Callback(StompMonitor.UnreserveAndClearPlantTarget));
	}

	// Token: 0x060020FD RID: 8445 RVA: 0x000BE83D File Offset: 0x000BCA3D
	private static void ReservePlant(StompMonitor.Instance smi)
	{
		smi.Target.AddTag(StompMonitor.ReservedForStomp);
	}

	// Token: 0x060020FE RID: 8446 RVA: 0x000BE84F File Offset: 0x000BCA4F
	private static bool IsTimeToStomp(StompMonitor.Instance smi, float timeSinceLastStomp)
	{
		return timeSinceLastStomp > smi.def.Cooldown;
	}

	// Token: 0x060020FF RID: 8447 RVA: 0x000BE85F File Offset: 0x000BCA5F
	private static void CooldownTick(StompMonitor.Instance smi, float dt)
	{
		smi.sm.TimeSinceLastStomp.Set(smi.TimeSinceLastStomp + dt, smi, false);
	}

	// Token: 0x06002100 RID: 8448 RVA: 0x000BE87C File Offset: 0x000BCA7C
	private static void OnStompCompleted(StompMonitor.Instance smi)
	{
		smi.sm.TimeSinceLastStomp.Set(0f, smi, false);
	}

	// Token: 0x06002101 RID: 8449 RVA: 0x000BE896 File Offset: 0x000BCA96
	private static void LookForTarget(StompMonitor.Instance smi)
	{
		smi.LookForTarget();
	}

	// Token: 0x06002102 RID: 8450 RVA: 0x000BE89E File Offset: 0x000BCA9E
	private static void UnreserveAndClearPlantTarget(StompMonitor.Instance smi)
	{
		if (smi.Target != null)
		{
			smi.Target.RemoveTag(StompMonitor.ReservedForStomp);
		}
		smi.sm.TargetPlant.Set(null, smi);
	}

	// Token: 0x04001333 RID: 4915
	public static readonly Tag ReservedForStomp = GameTags.Creatures.ReservedByCreature;

	// Token: 0x04001334 RID: 4916
	public GameStateMachine<StompMonitor, StompMonitor.Instance, IStateMachineTarget, StompMonitor.Def>.State cooldown;

	// Token: 0x04001335 RID: 4917
	public StompMonitor.StompBehaviourStates stomp;

	// Token: 0x04001336 RID: 4918
	public StateMachine<StompMonitor, StompMonitor.Instance, IStateMachineTarget, StompMonitor.Def>.FloatParameter TimeSinceLastStomp = new StateMachine<StompMonitor, StompMonitor.Instance, IStateMachineTarget, StompMonitor.Def>.FloatParameter(float.MaxValue);

	// Token: 0x04001337 RID: 4919
	public StateMachine<StompMonitor, StompMonitor.Instance, IStateMachineTarget, StompMonitor.Def>.TargetParameter TargetPlant;

	// Token: 0x04001338 RID: 4920
	public StateMachine<StompMonitor, StompMonitor.Instance, IStateMachineTarget, StompMonitor.Def>.Signal StompStateFailed;

	// Token: 0x02001432 RID: 5170
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06008CCF RID: 36047 RVA: 0x0035711C File Offset: 0x0035531C
		public Navigator.Scanner<KPrefabID> PlantSeeker
		{
			get
			{
				if (this.plantSeeker == null)
				{
					this.plantSeeker = new Navigator.Scanner<KPrefabID>(this.radius, GameScenePartitioner.Instance.plants, new Func<KPrefabID, bool>(StompMonitor.Def.IsPlantTargetCandidate));
					this.plantSeeker.SetDynamicOffsetsFn(delegate(KPrefabID plant, List<CellOffset> offsets)
					{
						StompMonitor.Def.GetObjectCellsOffsetsWithExtraBottomPadding(plant.gameObject, offsets);
					});
				}
				return this.plantSeeker;
			}
		}

		// Token: 0x06008CD0 RID: 36048 RVA: 0x00357188 File Offset: 0x00355388
		private static bool IsPlantTargetCandidate(KPrefabID plant)
		{
			return !(plant == null) && !plant.pendingDestruction && !plant.HasTag(StompMonitor.ReservedForStomp) && plant.HasTag(GameTags.GrowingPlant) && plant.HasTag(GameTags.FullyGrown);
		}

		// Token: 0x06008CD1 RID: 36049 RVA: 0x003571C8 File Offset: 0x003553C8
		public static void GetObjectCellsOffsetsWithExtraBottomPadding(GameObject obj, List<CellOffset> offsets)
		{
			OccupyArea component = obj.GetComponent<OccupyArea>();
			int widthInCells = component.GetWidthInCells();
			int num = int.MaxValue;
			int num2 = int.MaxValue;
			for (int i = 0; i < component.OccupiedCellsOffsets.Length; i++)
			{
				CellOffset cellOffset = component.OccupiedCellsOffsets[i];
				offsets.Add(cellOffset);
				num = Mathf.Min(num, cellOffset.x);
				num2 = Mathf.Min(num2, cellOffset.y);
			}
			for (int j = 0; j < widthInCells; j++)
			{
				CellOffset cellOffset2 = new CellOffset(num + j, num2 - 1);
				offsets.Add(cellOffset2);
			}
		}

		// Token: 0x04006BE1 RID: 27617
		public float Cooldown;

		// Token: 0x04006BE2 RID: 27618
		public int radius = 10;

		// Token: 0x04006BE3 RID: 27619
		private Navigator.Scanner<KPrefabID> plantSeeker;
	}

	// Token: 0x02001433 RID: 5171
	public class StompBehaviourStates : GameStateMachine<StompMonitor, StompMonitor.Instance, IStateMachineTarget, StompMonitor.Def>.State
	{
		// Token: 0x04006BE4 RID: 27620
		public GameStateMachine<StompMonitor, StompMonitor.Instance, IStateMachineTarget, StompMonitor.Def>.State lookingForTarget;

		// Token: 0x04006BE5 RID: 27621
		public GameStateMachine<StompMonitor, StompMonitor.Instance, IStateMachineTarget, StompMonitor.Def>.State stomping;
	}

	// Token: 0x02001434 RID: 5172
	public new class Instance : GameStateMachine<StompMonitor, StompMonitor.Instance, IStateMachineTarget, StompMonitor.Def>.GameInstance
	{
		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06008CD4 RID: 36052 RVA: 0x00357277 File Offset: 0x00355477
		public GameObject Target
		{
			get
			{
				return base.sm.TargetPlant.Get(this);
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06008CD5 RID: 36053 RVA: 0x0035728A File Offset: 0x0035548A
		public float TimeSinceLastStomp
		{
			get
			{
				return base.sm.TimeSinceLastStomp.Get(this);
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06008CD6 RID: 36054 RVA: 0x0035729D File Offset: 0x0035549D
		// (set) Token: 0x06008CD7 RID: 36055 RVA: 0x003572A5 File Offset: 0x003554A5
		public Navigator Navigator { get; private set; }

		// Token: 0x06008CD8 RID: 36056 RVA: 0x003572AE File Offset: 0x003554AE
		public Instance(IStateMachineTarget master, StompMonitor.Def def)
			: base(master, def)
		{
			this.Navigator = base.GetComponent<Navigator>();
		}

		// Token: 0x06008CD9 RID: 36057 RVA: 0x003572C4 File Offset: 0x003554C4
		public void LookForTarget()
		{
			KPrefabID kprefabID = base.def.PlantSeeker.Scan(Grid.PosToXY(base.transform.GetPosition()), this.Navigator);
			base.sm.TargetPlant.Set(kprefabID, this);
		}
	}
}
