using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000A72 RID: 2674
public class VineMother : PlantBranchGrowerBase<VineMother, VineMother.Instance, IStateMachineTarget, VineMother.Def>
{
	// Token: 0x06004D6B RID: 19819 RVA: 0x001C0738 File Offset: 0x001BE938
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.growing;
		this.growing.InitializeStates(this.masterTarget, this.dead).DefaultState(this.growing.growing);
		this.growing.growing.ParamTransition<bool>(this.IsGrown, this.grown, GameStateMachine<VineMother, VineMother.Instance, IStateMachineTarget, VineMother.Def>.IsTrue).PlayAnim("grow", KAnim.PlayMode.Once).OnAnimQueueComplete(this.growing.growing_pst);
		this.growing.growing_pst.Enter(new StateMachine<VineMother, VineMother.Instance, IStateMachineTarget, VineMother.Def>.State.Callback(VineMother.MarkAsGrown)).PlayAnim("grow_pst", KAnim.PlayMode.Once).OnAnimQueueComplete(this.grown);
		this.grown.InitializeStates(this.masterTarget, this.dead).DefaultState(this.grown.growingBranches);
		this.grown.growingBranches.EventTransition(GameHashes.Wilt, this.grown.wilt, (VineMother.Instance smi) => smi.IsWilting).ParamTransition<GameObject>(this.LeftBranch, this.grown.idle, (VineMother.Instance smi, GameObject b) => VineMother.HasGrownAllBranches(smi)).ParamTransition<GameObject>(this.RightBranch, this.grown.idle, (VineMother.Instance smi, GameObject b) => VineMother.HasGrownAllBranches(smi))
			.PlayAnim("idle_full", KAnim.PlayMode.Loop)
			.Enter(new StateMachine<VineMother, VineMother.Instance, IStateMachineTarget, VineMother.Def>.State.Callback(VineMother.SpawnBranchesIfNewGameSpawn))
			.Update(new Action<VineMother.Instance, float>(VineMother.AttemptToSpawnBranches), UpdateRate.SIM_4000ms, false)
			.DefaultState(this.grown.growingBranches.growing);
		this.grown.growingBranches.growing.ParamTransition<GameObject>(this.LeftBranch, this.grown.growingBranches.blocked, (VineMother.Instance smi, GameObject b) => VineMother.HasNoBranches(smi)).ParamTransition<GameObject>(this.RightBranch, this.grown.growingBranches.blocked, (VineMother.Instance smi, GameObject b) => VineMother.HasNoBranches(smi));
		this.grown.growingBranches.blocked.ParamTransition<GameObject>(this.LeftBranch, this.grown.growingBranches.growing, GameStateMachine<VineMother, VineMother.Instance, IStateMachineTarget, VineMother.Def>.IsNotNull).ParamTransition<GameObject>(this.RightBranch, this.grown.growingBranches.growing, GameStateMachine<VineMother, VineMother.Instance, IStateMachineTarget, VineMother.Def>.IsNotNull);
		this.grown.idle.EventTransition(GameHashes.Wilt, this.grown.wilt, (VineMother.Instance smi) => smi.IsWilting).ParamTransition<GameObject>(this.LeftBranch, this.grown.growingBranches, GameStateMachine<VineMother, VineMother.Instance, IStateMachineTarget, VineMother.Def>.IsNull).ParamTransition<GameObject>(this.RightBranch, this.grown.growingBranches, GameStateMachine<VineMother, VineMother.Instance, IStateMachineTarget, VineMother.Def>.IsNull)
			.PlayAnim("idle_full", KAnim.PlayMode.Loop);
		this.grown.wilt.EventTransition(GameHashes.WiltRecover, this.grown.idle, (VineMother.Instance smi) => !smi.IsWilting).PlayAnim("wilt3", KAnim.PlayMode.Loop);
		this.dead.ToggleMainStatusItem(Db.Get().CreatureStatusItems.Dead, null).Enter(delegate(VineMother.Instance smi)
		{
			if (!smi.IsWild && !smi.GetComponent<KPrefabID>().HasTag(GameTags.Uprooted))
			{
				Notifier notifier = smi.gameObject.AddOrGet<Notifier>();
				Notification notification = VineMother.CreateDeathNotification(smi);
				notifier.Add(notification, "");
			}
			GameUtil.KInstantiate(Assets.GetPrefab(EffectConfigs.PlantDeathId), smi.transform.GetPosition(), Grid.SceneLayer.FXFront, null, 0).SetActive(true);
			smi.Trigger(1623392196, null);
			smi.DestroySelf(null);
		});
	}

	// Token: 0x06004D6C RID: 19820 RVA: 0x001C0AE4 File Offset: 0x001BECE4
	private static void MarkAsGrown(VineMother.Instance smi)
	{
		smi.sm.IsGrown.Set(true, smi, false);
	}

	// Token: 0x06004D6D RID: 19821 RVA: 0x001C0AFA File Offset: 0x001BECFA
	private static bool HasNoBranches(VineMother.Instance smi)
	{
		return smi.LeftBranch == null && smi.RightBranch == null;
	}

	// Token: 0x06004D6E RID: 19822 RVA: 0x001C0B18 File Offset: 0x001BED18
	private static bool HasGrownAllBranches(VineMother.Instance smi)
	{
		return smi.HasGrownAllBranches;
	}

	// Token: 0x06004D6F RID: 19823 RVA: 0x001C0B20 File Offset: 0x001BED20
	private static void SpawnBranchesIfNewGameSpawn(VineMother.Instance smi)
	{
		if (smi.IsNewGameSpawned)
		{
			VineMother.AttemptToSpawnBranches(smi);
		}
	}

	// Token: 0x06004D70 RID: 19824 RVA: 0x001C0B30 File Offset: 0x001BED30
	private static void AttemptToSpawnBranches(VineMother.Instance smi, float dt)
	{
		VineMother.AttemptToSpawnBranches(smi);
	}

	// Token: 0x06004D71 RID: 19825 RVA: 0x001C0B38 File Offset: 0x001BED38
	private static void AttemptToSpawnBranches(VineMother.Instance smi)
	{
		smi.AttemptToSpawnBranches();
	}

	// Token: 0x06004D72 RID: 19826 RVA: 0x001C0B40 File Offset: 0x001BED40
	public static Notification CreateDeathNotification(VineMother.Instance smi)
	{
		return new Notification(CREATURES.STATUSITEMS.PLANTDEATH.NOTIFICATION, NotificationType.Bad, (List<Notification> notificationList, object data) => CREATURES.STATUSITEMS.PLANTDEATH.NOTIFICATION_TOOLTIP + notificationList.ReduceMessages(false), "/t• " + smi.gameObject.GetProperName(), true, 0f, null, null, null, true, false, false);
	}

	// Token: 0x04003378 RID: 13176
	private const string GROW_ANIM_NAME = "grow";

	// Token: 0x04003379 RID: 13177
	private const string GROW_PST_ANIM_NAME = "grow_pst";

	// Token: 0x0400337A RID: 13178
	private const string IDLE_ANIM_NAME = "idle_full";

	// Token: 0x0400337B RID: 13179
	private const string WILT_ANIM_NAME = "wilt3";

	// Token: 0x0400337C RID: 13180
	public GameStateMachine<VineMother, VineMother.Instance, IStateMachineTarget, VineMother.Def>.State dead;

	// Token: 0x0400337D RID: 13181
	public VineMother.GrowingStates growing;

	// Token: 0x0400337E RID: 13182
	public VineMother.GrownStates grown;

	// Token: 0x0400337F RID: 13183
	public StateMachine<VineMother, VineMother.Instance, IStateMachineTarget, VineMother.Def>.BoolParameter IsGrown;

	// Token: 0x04003380 RID: 13184
	public StateMachine<VineMother, VineMother.Instance, IStateMachineTarget, VineMother.Def>.TargetParameter LeftBranch;

	// Token: 0x04003381 RID: 13185
	public StateMachine<VineMother, VineMother.Instance, IStateMachineTarget, VineMother.Def>.TargetParameter RightBranch;

	// Token: 0x02001B56 RID: 6998
	public class Def : PlantBranchGrowerBase<VineMother, VineMother.Instance, IStateMachineTarget, VineMother.Def>.PlantBranchGrowerBaseDef
	{
	}

	// Token: 0x02001B57 RID: 6999
	public class GrowingBranchesStates : GameStateMachine<VineMother, VineMother.Instance, IStateMachineTarget, VineMother.Def>.State
	{
		// Token: 0x04008285 RID: 33413
		public GameStateMachine<VineMother, VineMother.Instance, IStateMachineTarget, VineMother.Def>.State growing;

		// Token: 0x04008286 RID: 33414
		public GameStateMachine<VineMother, VineMother.Instance, IStateMachineTarget, VineMother.Def>.State blocked;
	}

	// Token: 0x02001B58 RID: 7000
	public class GrownStates : GameStateMachine<VineMother, VineMother.Instance, IStateMachineTarget, VineMother.Def>.PlantAliveSubState
	{
		// Token: 0x04008287 RID: 33415
		public VineMother.GrowingBranchesStates growingBranches;

		// Token: 0x04008288 RID: 33416
		public GameStateMachine<VineMother, VineMother.Instance, IStateMachineTarget, VineMother.Def>.State idle;

		// Token: 0x04008289 RID: 33417
		public GameStateMachine<VineMother, VineMother.Instance, IStateMachineTarget, VineMother.Def>.State wilt;
	}

	// Token: 0x02001B59 RID: 7001
	public class GrowingStates : GameStateMachine<VineMother, VineMother.Instance, IStateMachineTarget, VineMother.Def>.PlantAliveSubState
	{
		// Token: 0x0400828A RID: 33418
		public GameStateMachine<VineMother, VineMother.Instance, IStateMachineTarget, VineMother.Def>.State growing;

		// Token: 0x0400828B RID: 33419
		public GameStateMachine<VineMother, VineMother.Instance, IStateMachineTarget, VineMother.Def>.State growing_pst;
	}

	// Token: 0x02001B5A RID: 7002
	public new class Instance : GameStateMachine<VineMother, VineMother.Instance, IStateMachineTarget, VineMother.Def>.GameInstance
	{
		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x0600A755 RID: 42837 RVA: 0x003B0CED File Offset: 0x003AEEED
		public GameObject LeftBranch
		{
			get
			{
				return base.sm.LeftBranch.Get(this);
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x0600A756 RID: 42838 RVA: 0x003B0D00 File Offset: 0x003AEF00
		public GameObject RightBranch
		{
			get
			{
				return base.sm.RightBranch.Get(this);
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x0600A757 RID: 42839 RVA: 0x003B0D13 File Offset: 0x003AEF13
		public bool HasGrownAllBranches
		{
			get
			{
				return this.LeftBranch != null && this.RightBranch != null;
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x0600A758 RID: 42840 RVA: 0x003B0D31 File Offset: 0x003AEF31
		public bool IsGrown
		{
			get
			{
				return this.growing.IsGrown();
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x0600A759 RID: 42841 RVA: 0x003B0D3E File Offset: 0x003AEF3E
		public bool IsWild
		{
			get
			{
				return !this.receptacleMonitor.Replanted;
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x0600A75A RID: 42842 RVA: 0x003B0D50 File Offset: 0x003AEF50
		public bool IsOnPlanterBox
		{
			get
			{
				return !this.IsWild && this.receptacleMonitor.smi.ReceptacleObject != null && this.receptacleMonitor.smi.ReceptacleObject is PlantablePlot && (this.receptacleMonitor.smi.ReceptacleObject as PlantablePlot).IsOffGround;
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x0600A75B RID: 42843 RVA: 0x003B0DB0 File Offset: 0x003AEFB0
		public int PlanterboxCell
		{
			get
			{
				if (!this.IsWild)
				{
					return Grid.PosToCell(this.receptacleMonitor.smi.ReceptacleObject);
				}
				return Grid.InvalidCell;
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x0600A75C RID: 42844 RVA: 0x003B0DD5 File Offset: 0x003AEFD5
		public bool IsWilting
		{
			get
			{
				return this.wiltCondition.IsWilting();
			}
		}

		// Token: 0x0600A75D RID: 42845 RVA: 0x003B0DE4 File Offset: 0x003AEFE4
		public Instance(IStateMachineTarget master, VineMother.Def def)
			: base(master, def)
		{
			this.growing = base.GetComponent<Growing>();
			this.receptacleMonitor = base.GetComponent<ReceptacleMonitor>();
			this.wiltCondition = base.GetComponent<WiltCondition>();
			base.Subscribe(1119167081, new Action<object>(this.OnSpawnedByDiscovered));
			base.Subscribe(-266953818, delegate(object obj)
			{
				this.UpdateAutoHarvestValue();
			});
		}

		// Token: 0x0600A75E RID: 42846 RVA: 0x003B0E4C File Offset: 0x003AF04C
		public void AttemptToSpawnBranches()
		{
			int num = Grid.PosToCell(base.gameObject);
			if (this.LeftBranch == null)
			{
				int num2 = Grid.OffsetCell(num, CellOffset.left);
				if (VineBranch.IsCellAvailable(base.gameObject, num2, null))
				{
					GameObject gameObject = this.SpawnBranchOnCell(num2);
					base.sm.LeftBranch.Set(gameObject, this, false);
					if (this.IsNewGameSpawned)
					{
						gameObject.Trigger(1119167081, null);
					}
				}
			}
			if (this.RightBranch == null)
			{
				int num3 = Grid.OffsetCell(num, CellOffset.right);
				if (VineBranch.IsCellAvailable(base.gameObject, num3, null))
				{
					GameObject gameObject2 = this.SpawnBranchOnCell(num3);
					base.sm.RightBranch.Set(gameObject2, this, false);
					if (this.IsNewGameSpawned)
					{
						gameObject2.Trigger(1119167081, null);
					}
				}
			}
			if (this.IsNewGameSpawned)
			{
				this.IsNewGameSpawned = false;
			}
		}

		// Token: 0x0600A75F RID: 42847 RVA: 0x003B0F29 File Offset: 0x003AF129
		public void DestroySelf(object o)
		{
			CreatureHelpers.DeselectCreature(base.gameObject);
			Util.KDestroyGameObject(base.gameObject);
		}

		// Token: 0x0600A760 RID: 42848 RVA: 0x003B0F41 File Offset: 0x003AF141
		private void OnSpawnedByDiscovered(object o)
		{
			this.IsNewGameSpawned = true;
			VineMother.MarkAsGrown(this);
		}

		// Token: 0x0600A761 RID: 42849 RVA: 0x003B0F50 File Offset: 0x003AF150
		private GameObject SpawnBranchOnCell(int cell)
		{
			Vector3 vector = Grid.CellToPosCBC(cell, Grid.SceneLayer.BuildingFront);
			GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(base.def.BRANCH_PREFAB_NAME), vector);
			gameObject.SetActive(true);
			gameObject.GetSMI<VineBranch.Instance>().SetupRootInformation(this);
			return gameObject;
		}

		// Token: 0x0600A762 RID: 42850 RVA: 0x003B0F94 File Offset: 0x003AF194
		public void UpdateAutoHarvestValue()
		{
			HarvestDesignatable component = base.GetComponent<HarvestDesignatable>();
			if (component != null)
			{
				if (this.LeftBranch != null)
				{
					VineBranch.Instance smi = this.LeftBranch.GetSMI<VineBranch.Instance>();
					if (smi != null)
					{
						smi.SetAutoHarvestInChainReaction(component.HarvestWhenReady);
					}
				}
				if (this.RightBranch != null)
				{
					VineBranch.Instance smi2 = this.RightBranch.GetSMI<VineBranch.Instance>();
					if (smi2 != null)
					{
						smi2.SetAutoHarvestInChainReaction(component.HarvestWhenReady);
					}
				}
			}
		}

		// Token: 0x0400828C RID: 33420
		public bool IsNewGameSpawned;

		// Token: 0x0400828D RID: 33421
		private Growing growing;

		// Token: 0x0400828E RID: 33422
		private ReceptacleMonitor receptacleMonitor;

		// Token: 0x0400828F RID: 33423
		private WiltCondition wiltCondition;
	}
}
