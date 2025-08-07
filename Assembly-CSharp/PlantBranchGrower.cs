using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000A67 RID: 2663
public class PlantBranchGrower : PlantBranchGrowerBase<PlantBranchGrower, PlantBranchGrower.Instance, IStateMachineTarget, PlantBranchGrower.Def>
{
	// Token: 0x06004D11 RID: 19729 RVA: 0x001BE6A4 File Offset: 0x001BC8A4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.wilt;
		this.worldgen.Update(new Action<PlantBranchGrower.Instance, float>(PlantBranchGrower.WorldGenUpdate), UpdateRate.RENDER_EVERY_TICK, false);
		this.wilt.TagTransition(GameTags.Wilting, this.maturing, true);
		this.maturing.TagTransition(GameTags.Wilting, this.wilt, false).EnterTransition(this.growingBranches, new StateMachine<PlantBranchGrower, PlantBranchGrower.Instance, IStateMachineTarget, PlantBranchGrower.Def>.Transition.ConditionCallback(PlantBranchGrower.IsMature)).EventTransition(GameHashes.Grow, this.growingBranches, null);
		this.growingBranches.TagTransition(GameTags.Wilting, this.wilt, false).EventTransition(GameHashes.ConsumePlant, this.maturing, GameStateMachine<PlantBranchGrower, PlantBranchGrower.Instance, IStateMachineTarget, PlantBranchGrower.Def>.Not(new StateMachine<PlantBranchGrower, PlantBranchGrower.Instance, IStateMachineTarget, PlantBranchGrower.Def>.Transition.ConditionCallback(PlantBranchGrower.IsMature))).EventTransition(GameHashes.TreeBranchCountChanged, this.fullyGrown, new StateMachine<PlantBranchGrower, PlantBranchGrower.Instance, IStateMachineTarget, PlantBranchGrower.Def>.Transition.ConditionCallback(PlantBranchGrower.AllBranchesCreated))
			.ToggleStatusItem((PlantBranchGrower.Instance smi) => smi.def.growingBranchesStatusItem, null)
			.Update(new Action<PlantBranchGrower.Instance, float>(PlantBranchGrower.GrowBranchUpdate), UpdateRate.SIM_4000ms, false);
		this.fullyGrown.TagTransition(GameTags.Wilting, this.wilt, false).EventTransition(GameHashes.ConsumePlant, this.maturing, GameStateMachine<PlantBranchGrower, PlantBranchGrower.Instance, IStateMachineTarget, PlantBranchGrower.Def>.Not(new StateMachine<PlantBranchGrower, PlantBranchGrower.Instance, IStateMachineTarget, PlantBranchGrower.Def>.Transition.ConditionCallback(PlantBranchGrower.IsMature))).EventTransition(GameHashes.TreeBranchCountChanged, this.growingBranches, new StateMachine<PlantBranchGrower, PlantBranchGrower.Instance, IStateMachineTarget, PlantBranchGrower.Def>.Transition.ConditionCallback(PlantBranchGrower.NotAllBranchesCreated));
	}

	// Token: 0x06004D12 RID: 19730 RVA: 0x001BE814 File Offset: 0x001BCA14
	public static bool NotAllBranchesCreated(PlantBranchGrower.Instance smi)
	{
		return smi.CurrentBranchCount < smi.MaxBranchesAllowedAtOnce;
	}

	// Token: 0x06004D13 RID: 19731 RVA: 0x001BE824 File Offset: 0x001BCA24
	public static bool AllBranchesCreated(PlantBranchGrower.Instance smi)
	{
		return smi.CurrentBranchCount >= smi.MaxBranchesAllowedAtOnce;
	}

	// Token: 0x06004D14 RID: 19732 RVA: 0x001BE837 File Offset: 0x001BCA37
	public static bool IsMature(PlantBranchGrower.Instance smi)
	{
		return smi.IsGrown;
	}

	// Token: 0x06004D15 RID: 19733 RVA: 0x001BE83F File Offset: 0x001BCA3F
	public static void GrowBranchUpdate(PlantBranchGrower.Instance smi, float dt)
	{
		smi.SpawnRandomBranch(0f);
	}

	// Token: 0x06004D16 RID: 19734 RVA: 0x001BE850 File Offset: 0x001BCA50
	public static void WorldGenUpdate(PlantBranchGrower.Instance smi, float dt)
	{
		float num = global::UnityEngine.Random.Range(0f, 1f);
		if (!smi.SpawnRandomBranch(num))
		{
			smi.GoTo(smi.sm.defaultState);
		}
	}

	// Token: 0x04003340 RID: 13120
	public GameStateMachine<PlantBranchGrower, PlantBranchGrower.Instance, IStateMachineTarget, PlantBranchGrower.Def>.State worldgen;

	// Token: 0x04003341 RID: 13121
	public GameStateMachine<PlantBranchGrower, PlantBranchGrower.Instance, IStateMachineTarget, PlantBranchGrower.Def>.State wilt;

	// Token: 0x04003342 RID: 13122
	public GameStateMachine<PlantBranchGrower, PlantBranchGrower.Instance, IStateMachineTarget, PlantBranchGrower.Def>.State maturing;

	// Token: 0x04003343 RID: 13123
	public GameStateMachine<PlantBranchGrower, PlantBranchGrower.Instance, IStateMachineTarget, PlantBranchGrower.Def>.State growingBranches;

	// Token: 0x04003344 RID: 13124
	public GameStateMachine<PlantBranchGrower, PlantBranchGrower.Instance, IStateMachineTarget, PlantBranchGrower.Def>.State fullyGrown;

	// Token: 0x02001B2F RID: 6959
	public class Def : PlantBranchGrowerBase<PlantBranchGrower, PlantBranchGrower.Instance, IStateMachineTarget, PlantBranchGrower.Def>.PlantBranchGrowerBaseDef
	{
		// Token: 0x040081D1 RID: 33233
		public CellOffset[] BRANCH_OFFSETS;

		// Token: 0x040081D2 RID: 33234
		public bool harvestOnDrown;

		// Token: 0x040081D3 RID: 33235
		public bool propagateHarvestDesignation = true;

		// Token: 0x040081D4 RID: 33236
		public Func<int, bool> additionalBranchGrowRequirements;

		// Token: 0x040081D5 RID: 33237
		public Action<PlantBranch.Instance, PlantBranchGrower.Instance> onBranchHarvested;

		// Token: 0x040081D6 RID: 33238
		public Action<PlantBranch.Instance, PlantBranchGrower.Instance> onBranchSpawned;

		// Token: 0x040081D7 RID: 33239
		public StatusItem growingBranchesStatusItem = Db.Get().MiscStatusItems.GrowingBranches;

		// Token: 0x040081D8 RID: 33240
		public Action<PlantBranchGrower.Instance> onEarlySpawn;
	}

	// Token: 0x02001B30 RID: 6960
	public new class Instance : GameStateMachine<PlantBranchGrower, PlantBranchGrower.Instance, IStateMachineTarget, PlantBranchGrower.Def>.GameInstance
	{
		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x0600A664 RID: 42596 RVA: 0x003AD06A File Offset: 0x003AB26A
		public bool IsUprooted
		{
			get
			{
				return this.uprootMonitor != null && this.uprootMonitor.IsUprooted;
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x0600A665 RID: 42597 RVA: 0x003AD087 File Offset: 0x003AB287
		public bool IsGrown
		{
			get
			{
				return this.growing == null || this.growing.PercentGrown() >= 1f;
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x0600A666 RID: 42598 RVA: 0x003AD0A8 File Offset: 0x003AB2A8
		public int MaxBranchesAllowedAtOnce
		{
			get
			{
				if (base.def.MAX_BRANCH_COUNT >= 0)
				{
					return Mathf.Min(base.def.MAX_BRANCH_COUNT, base.def.BRANCH_OFFSETS.Length);
				}
				return base.def.BRANCH_OFFSETS.Length;
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x0600A667 RID: 42599 RVA: 0x003AD0E4 File Offset: 0x003AB2E4
		public int CurrentBranchCount
		{
			get
			{
				int num = 0;
				if (this.branches != null)
				{
					int i = 0;
					while (i < this.branches.Length)
					{
						num += ((this.GetBranch(i++) != null) ? 1 : 0);
					}
				}
				return num;
			}
		}

		// Token: 0x0600A668 RID: 42600 RVA: 0x003AD128 File Offset: 0x003AB328
		public GameObject GetBranch(int idx)
		{
			if (this.branches != null && this.branches[idx] != null)
			{
				KPrefabID kprefabID = this.branches[idx].Get();
				if (kprefabID != null)
				{
					return kprefabID.gameObject;
				}
			}
			return null;
		}

		// Token: 0x0600A669 RID: 42601 RVA: 0x003AD166 File Offset: 0x003AB366
		protected override void OnCleanUp()
		{
			this.SetTrunkOccupyingCellsAsPlant(false);
			base.OnCleanUp();
		}

		// Token: 0x0600A66A RID: 42602 RVA: 0x003AD178 File Offset: 0x003AB378
		public Instance(IStateMachineTarget master, PlantBranchGrower.Def def)
			: base(master, def)
		{
			this.growing = base.GetComponent<IManageGrowingStates>();
			this.growing = ((this.growing != null) ? this.growing : base.gameObject.GetSMI<IManageGrowingStates>());
			this.SetTrunkOccupyingCellsAsPlant(true);
			base.Subscribe(1119167081, new Action<object>(this.OnNewGameSpawn));
			base.Subscribe(144050788, new Action<object>(this.OnUpdateRoom));
		}

		// Token: 0x0600A66B RID: 42603 RVA: 0x003AD1F0 File Offset: 0x003AB3F0
		public override void StartSM()
		{
			base.StartSM();
			Action<PlantBranchGrower.Instance> onEarlySpawn = base.def.onEarlySpawn;
			if (onEarlySpawn != null)
			{
				onEarlySpawn(this);
			}
			this.DefineBranchArray();
			base.Subscribe(-216549700, new Action<object>(this.OnUprooted));
			base.Subscribe(-266953818, delegate(object obj)
			{
				this.UpdateAutoHarvestValue(null);
			});
			if (base.def.harvestOnDrown)
			{
				base.Subscribe(-750750377, new Action<object>(this.OnUprooted));
			}
		}

		// Token: 0x0600A66C RID: 42604 RVA: 0x003AD274 File Offset: 0x003AB474
		private void OnUpdateRoom(object data)
		{
			if (this.branches == null)
			{
				return;
			}
			this.ActionPerBranch(delegate(GameObject branch)
			{
				branch.Trigger(144050788, data);
			});
		}

		// Token: 0x0600A66D RID: 42605 RVA: 0x003AD2AC File Offset: 0x003AB4AC
		private void SetTrunkOccupyingCellsAsPlant(bool doSet)
		{
			CellOffset[] occupiedCellsOffsets = base.GetComponent<OccupyArea>().OccupiedCellsOffsets;
			int num = Grid.PosToCell(base.gameObject);
			for (int i = 0; i < occupiedCellsOffsets.Length; i++)
			{
				int num2 = Grid.OffsetCell(num, occupiedCellsOffsets[i]);
				if (doSet)
				{
					Grid.Objects[num2, 5] = base.gameObject;
				}
				else if (Grid.Objects[num2, 5] == base.gameObject)
				{
					Grid.Objects[num2, 5] = null;
				}
			}
		}

		// Token: 0x0600A66E RID: 42606 RVA: 0x003AD32C File Offset: 0x003AB52C
		private void OnNewGameSpawn(object data)
		{
			this.DefineBranchArray();
			float num = 1f;
			if ((double)global::UnityEngine.Random.value < 0.1)
			{
				num = global::UnityEngine.Random.Range(0.75f, 0.99f);
			}
			else
			{
				this.GoTo(base.sm.worldgen);
			}
			this.growing.OverrideMaturityLevel(num);
		}

		// Token: 0x0600A66F RID: 42607 RVA: 0x003AD388 File Offset: 0x003AB588
		public void ManuallyDefineBranchArray(KPrefabID[] _branches)
		{
			this.DefineBranchArray();
			for (int i = 0; i < Mathf.Min(this.branches.Length, _branches.Length); i++)
			{
				KPrefabID kprefabID = _branches[i];
				if (kprefabID != null)
				{
					if (this.branches[i] == null)
					{
						this.branches[i] = new Ref<KPrefabID>();
					}
					this.branches[i].Set(kprefabID);
				}
				else
				{
					this.branches[i] = null;
				}
			}
		}

		// Token: 0x0600A670 RID: 42608 RVA: 0x003AD3F3 File Offset: 0x003AB5F3
		private void DefineBranchArray()
		{
			if (this.branches == null)
			{
				this.branches = new Ref<KPrefabID>[base.def.BRANCH_OFFSETS.Length];
			}
		}

		// Token: 0x0600A671 RID: 42609 RVA: 0x003AD418 File Offset: 0x003AB618
		public void ActionPerBranch(Action<GameObject> action)
		{
			for (int i = 0; i < this.branches.Length; i++)
			{
				GameObject branch = this.GetBranch(i);
				if (branch != null && action != null)
				{
					action(branch.gameObject);
				}
			}
		}

		// Token: 0x0600A672 RID: 42610 RVA: 0x003AD458 File Offset: 0x003AB658
		public GameObject[] GetExistingBranches()
		{
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < this.branches.Length; i++)
			{
				GameObject branch = this.GetBranch(i);
				if (branch != null)
				{
					list.Add(branch.gameObject);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600A673 RID: 42611 RVA: 0x003AD4A4 File Offset: 0x003AB6A4
		public void OnBranchRemoved(GameObject _branch)
		{
			for (int i = 0; i < this.branches.Length; i++)
			{
				GameObject branch = this.GetBranch(i);
				if (branch != null && branch == _branch)
				{
					this.branches[i] = null;
				}
			}
			base.gameObject.Trigger(-1586842875, null);
		}

		// Token: 0x0600A674 RID: 42612 RVA: 0x003AD4F8 File Offset: 0x003AB6F8
		public void OnBrancHarvested(PlantBranch.Instance branch)
		{
			Action<PlantBranch.Instance, PlantBranchGrower.Instance> onBranchHarvested = base.def.onBranchHarvested;
			if (onBranchHarvested == null)
			{
				return;
			}
			onBranchHarvested(branch, this);
		}

		// Token: 0x0600A675 RID: 42613 RVA: 0x003AD514 File Offset: 0x003AB714
		private void OnUprooted(object data = null)
		{
			for (int i = 0; i < this.branches.Length; i++)
			{
				GameObject branch = this.GetBranch(i);
				if (branch != null)
				{
					branch.Trigger(-216549700, null);
				}
			}
		}

		// Token: 0x0600A676 RID: 42614 RVA: 0x003AD554 File Offset: 0x003AB754
		public List<int> GetAvailableSpawnPositions()
		{
			PlantBranchGrower.Instance.spawn_choices.Clear();
			int num = Grid.PosToCell(this);
			for (int i = 0; i < base.def.BRANCH_OFFSETS.Length; i++)
			{
				int num2 = Grid.OffsetCell(num, base.def.BRANCH_OFFSETS[i]);
				if (this.GetBranch(i) == null && this.CanBranchGrowInCell(num2))
				{
					PlantBranchGrower.Instance.spawn_choices.Add(i);
				}
			}
			return PlantBranchGrower.Instance.spawn_choices;
		}

		// Token: 0x0600A677 RID: 42615 RVA: 0x003AD5CC File Offset: 0x003AB7CC
		public void RefreshBranchZPositionOffset(GameObject _branch)
		{
			if (this.branches != null)
			{
				for (int i = 0; i < this.branches.Length; i++)
				{
					GameObject branch = this.GetBranch(i);
					if (branch != null && branch == _branch)
					{
						Vector3 position = branch.transform.position;
						position.z = Grid.GetLayerZ(Grid.SceneLayer.BuildingFront) - 0.8f / (float)this.branches.Length * (float)i;
						branch.transform.SetPosition(position);
					}
				}
			}
		}

		// Token: 0x0600A678 RID: 42616 RVA: 0x003AD648 File Offset: 0x003AB848
		public bool SpawnRandomBranch(float growth_percentage = 0f)
		{
			if (this.IsUprooted)
			{
				return false;
			}
			if (this.CurrentBranchCount >= this.MaxBranchesAllowedAtOnce)
			{
				return false;
			}
			List<int> availableSpawnPositions = this.GetAvailableSpawnPositions();
			availableSpawnPositions.Shuffle<int>();
			if (availableSpawnPositions.Count > 0)
			{
				int num = availableSpawnPositions[0];
				PlantBranch.Instance instance = this.SpawnBranchAtIndex(num);
				IManageGrowingStates manageGrowingStates = instance.GetComponent<IManageGrowingStates>();
				manageGrowingStates = ((manageGrowingStates != null) ? manageGrowingStates : instance.gameObject.GetSMI<IManageGrowingStates>());
				if (manageGrowingStates != null)
				{
					manageGrowingStates.OverrideMaturityLevel(growth_percentage);
				}
				instance.StartSM();
				base.gameObject.Trigger(-1586842875, instance);
				Action<PlantBranch.Instance, PlantBranchGrower.Instance> onBranchSpawned = base.def.onBranchSpawned;
				if (onBranchSpawned != null)
				{
					onBranchSpawned(instance, this);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600A679 RID: 42617 RVA: 0x003AD6EC File Offset: 0x003AB8EC
		private PlantBranch.Instance SpawnBranchAtIndex(int idx)
		{
			if (idx < 0 || idx >= this.branches.Length)
			{
				return null;
			}
			GameObject branch = this.GetBranch(idx);
			if (branch != null)
			{
				return branch.GetSMI<PlantBranch.Instance>();
			}
			Vector3 vector = Grid.CellToPosCBC(Grid.OffsetCell(Grid.PosToCell(this), base.def.BRANCH_OFFSETS[idx]), Grid.SceneLayer.BuildingFront);
			GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(base.def.BRANCH_PREFAB_NAME), vector);
			gameObject.SetActive(true);
			PlantBranch.Instance smi = gameObject.GetSMI<PlantBranch.Instance>();
			MutantPlant component = base.GetComponent<MutantPlant>();
			if (component != null)
			{
				MutantPlant component2 = smi.GetComponent<MutantPlant>();
				if (component2 != null)
				{
					component.CopyMutationsTo(component2);
					PlantSubSpeciesCatalog.SubSpeciesInfo subSpeciesInfo = component2.GetSubSpeciesInfo();
					PlantSubSpeciesCatalog.Instance.DiscoverSubSpecies(subSpeciesInfo, component2);
					PlantSubSpeciesCatalog.Instance.IdentifySubSpecies(subSpeciesInfo.ID);
				}
			}
			this.UpdateAutoHarvestValue(smi);
			smi.SetTrunk(this);
			this.branches[idx] = new Ref<KPrefabID>();
			this.branches[idx].Set(smi.GetComponent<KPrefabID>());
			return smi;
		}

		// Token: 0x0600A67A RID: 42618 RVA: 0x003AD7F0 File Offset: 0x003AB9F0
		private bool CanBranchGrowInCell(int cell)
		{
			if (!Grid.IsValidCell(cell))
			{
				return false;
			}
			if (Grid.Solid[cell])
			{
				return false;
			}
			if (Grid.Objects[cell, 1] != null)
			{
				return false;
			}
			if (Grid.Objects[cell, 5] != null)
			{
				return false;
			}
			if (Grid.Foundation[cell])
			{
				return false;
			}
			int num = Grid.CellAbove(cell);
			return Grid.IsValidCell(num) && !Grid.IsSubstantialLiquid(num, 0.35f) && (base.def.additionalBranchGrowRequirements == null || base.def.additionalBranchGrowRequirements(cell));
		}

		// Token: 0x0600A67B RID: 42619 RVA: 0x003AD894 File Offset: 0x003ABA94
		public void UpdateAutoHarvestValue(PlantBranch.Instance specificBranch = null)
		{
			HarvestDesignatable component = base.GetComponent<HarvestDesignatable>();
			if (component != null && this.branches != null)
			{
				if (specificBranch != null)
				{
					HarvestDesignatable component2 = specificBranch.GetComponent<HarvestDesignatable>();
					if (component2 != null)
					{
						component2.SetHarvestWhenReady(component.HarvestWhenReady);
					}
					return;
				}
				if (base.def.propagateHarvestDesignation)
				{
					for (int i = 0; i < this.branches.Length; i++)
					{
						GameObject branch = this.GetBranch(i);
						if (branch != null)
						{
							HarvestDesignatable component3 = branch.GetComponent<HarvestDesignatable>();
							if (component3 != null)
							{
								component3.SetHarvestWhenReady(component.HarvestWhenReady);
							}
						}
					}
				}
			}
		}

		// Token: 0x040081D9 RID: 33241
		private IManageGrowingStates growing;

		// Token: 0x040081DA RID: 33242
		[MyCmpGet]
		private UprootedMonitor uprootMonitor;

		// Token: 0x040081DB RID: 33243
		[Serialize]
		private Ref<KPrefabID>[] branches;

		// Token: 0x040081DC RID: 33244
		private static List<int> spawn_choices = new List<int>();
	}
}
