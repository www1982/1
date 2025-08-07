using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000A66 RID: 2662
public class PlantBranch : GameStateMachine<PlantBranch, PlantBranch.Instance, IStateMachineTarget, PlantBranch.Def>
{
	// Token: 0x06004D0F RID: 19727 RVA: 0x001BE68A File Offset: 0x001BC88A
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.root;
	}

	// Token: 0x0400333F RID: 13119
	private StateMachine<PlantBranch, PlantBranch.Instance, IStateMachineTarget, PlantBranch.Def>.TargetParameter Trunk;

	// Token: 0x02001B2D RID: 6957
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040081CC RID: 33228
		public Action<PlantBranchGrower.Instance, PlantBranch.Instance> animationSetupCallback;

		// Token: 0x040081CD RID: 33229
		public Action<PlantBranch.Instance> onEarlySpawn;
	}

	// Token: 0x02001B2E RID: 6958
	public new class Instance : GameStateMachine<PlantBranch, PlantBranch.Instance, IStateMachineTarget, PlantBranch.Def>.GameInstance, IWiltCause
	{
		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x0600A655 RID: 42581 RVA: 0x003ACC94 File Offset: 0x003AAE94
		public bool HasTrunk
		{
			get
			{
				return this.trunk != null && !this.trunk.IsNullOrDestroyed() && !this.trunk.isMasterNull;
			}
		}

		// Token: 0x0600A656 RID: 42582 RVA: 0x003ACCBB File Offset: 0x003AAEBB
		public Instance(IStateMachineTarget master, PlantBranch.Def def)
			: base(master, def)
		{
			this.SetOccupyGridSpace(true);
			base.Subscribe(1272413801, new Action<object>(this.OnHarvest));
		}

		// Token: 0x0600A657 RID: 42583 RVA: 0x003ACCF4 File Offset: 0x003AAEF4
		public override void StartSM()
		{
			base.StartSM();
			Action<PlantBranch.Instance> onEarlySpawn = base.def.onEarlySpawn;
			if (onEarlySpawn != null)
			{
				onEarlySpawn(this);
			}
			this.trunk = this.GetTrunk();
			if (!this.HasTrunk)
			{
				global::Debug.LogWarning("Tree Branch loaded with missing trunk reference. Destroying...");
				Util.KDestroyGameObject(base.gameObject);
				return;
			}
			this.SubscribeToTrunk();
			Action<PlantBranchGrower.Instance, PlantBranch.Instance> animationSetupCallback = base.def.animationSetupCallback;
			if (animationSetupCallback == null)
			{
				return;
			}
			animationSetupCallback(this.trunk, this);
		}

		// Token: 0x0600A658 RID: 42584 RVA: 0x003ACD6A File Offset: 0x003AAF6A
		private void OnHarvest(object data)
		{
			if (this.HasTrunk)
			{
				this.trunk.OnBrancHarvested(this);
			}
		}

		// Token: 0x0600A659 RID: 42585 RVA: 0x003ACD80 File Offset: 0x003AAF80
		protected override void OnCleanUp()
		{
			this.UnsubscribeToTrunk();
			this.SetOccupyGridSpace(false);
			base.OnCleanUp();
		}

		// Token: 0x0600A65A RID: 42586 RVA: 0x003ACD98 File Offset: 0x003AAF98
		private void SetOccupyGridSpace(bool active)
		{
			int num = Grid.PosToCell(base.gameObject);
			if (!active)
			{
				if (Grid.Objects[num, 5] == base.gameObject)
				{
					Grid.Objects[num, 5] = null;
				}
				return;
			}
			GameObject gameObject = Grid.Objects[num, 5];
			if (gameObject != null && gameObject != base.gameObject)
			{
				global::Debug.LogWarningFormat(base.gameObject, "PlantBranch.SetOccupyGridSpace already occupied by {0}", new object[] { gameObject });
				Util.KDestroyGameObject(base.gameObject);
				return;
			}
			Grid.Objects[num, 5] = base.gameObject;
		}

		// Token: 0x0600A65B RID: 42587 RVA: 0x003ACE38 File Offset: 0x003AB038
		public void SetTrunk(PlantBranchGrower.Instance trunk)
		{
			this.trunk = trunk;
			base.smi.sm.Trunk.Set(trunk.gameObject, this, false);
			this.SubscribeToTrunk();
			Action<PlantBranchGrower.Instance, PlantBranch.Instance> animationSetupCallback = base.def.animationSetupCallback;
			if (animationSetupCallback == null)
			{
				return;
			}
			animationSetupCallback(trunk, this);
		}

		// Token: 0x0600A65C RID: 42588 RVA: 0x003ACE87 File Offset: 0x003AB087
		public PlantBranchGrower.Instance GetTrunk()
		{
			if (base.smi.sm.Trunk.IsNull(this))
			{
				return null;
			}
			return base.sm.Trunk.Get(this).GetSMI<PlantBranchGrower.Instance>();
		}

		// Token: 0x0600A65D RID: 42589 RVA: 0x003ACEBC File Offset: 0x003AB0BC
		private void SubscribeToTrunk()
		{
			if (!this.HasTrunk)
			{
				return;
			}
			if (this.trunkWiltHandle == -1)
			{
				this.trunkWiltHandle = this.trunk.gameObject.Subscribe(-724860998, new Action<object>(this.OnTrunkWilt));
			}
			if (this.trunkWiltRecoverHandle == -1)
			{
				this.trunkWiltRecoverHandle = this.trunk.gameObject.Subscribe(712767498, new Action<object>(this.OnTrunkRecover));
			}
			base.Trigger(912965142, !this.trunk.GetComponent<WiltCondition>().IsWilting());
			ReceptacleMonitor component = base.GetComponent<ReceptacleMonitor>();
			PlantablePlot receptacle = this.trunk.GetComponent<ReceptacleMonitor>().GetReceptacle();
			component.SetReceptacle(receptacle);
			this.trunk.RefreshBranchZPositionOffset(base.gameObject);
			base.GetComponent<BudUprootedMonitor>().SetParentObject(this.trunk.GetComponent<KPrefabID>());
		}

		// Token: 0x0600A65E RID: 42590 RVA: 0x003ACF9C File Offset: 0x003AB19C
		private void UnsubscribeToTrunk()
		{
			if (!this.HasTrunk)
			{
				return;
			}
			this.trunk.gameObject.Unsubscribe(this.trunkWiltHandle);
			this.trunk.gameObject.Unsubscribe(this.trunkWiltRecoverHandle);
			this.trunkWiltHandle = -1;
			this.trunkWiltRecoverHandle = -1;
			this.trunk.OnBranchRemoved(base.gameObject);
		}

		// Token: 0x0600A65F RID: 42591 RVA: 0x003ACFFD File Offset: 0x003AB1FD
		private void OnTrunkWilt(object data = null)
		{
			base.Trigger(912965142, false);
		}

		// Token: 0x0600A660 RID: 42592 RVA: 0x003AD010 File Offset: 0x003AB210
		private void OnTrunkRecover(object data = null)
		{
			base.Trigger(912965142, true);
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x0600A661 RID: 42593 RVA: 0x003AD023 File Offset: 0x003AB223
		public string WiltStateString
		{
			get
			{
				return "    • " + DUPLICANTS.STATS.TRUNKHEALTH.NAME;
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x0600A662 RID: 42594 RVA: 0x003AD039 File Offset: 0x003AB239
		public WiltCondition.Condition[] Conditions
		{
			get
			{
				return new WiltCondition.Condition[] { WiltCondition.Condition.UnhealthyRoot };
			}
		}

		// Token: 0x040081CE RID: 33230
		public PlantBranchGrower.Instance trunk;

		// Token: 0x040081CF RID: 33231
		private int trunkWiltHandle = -1;

		// Token: 0x040081D0 RID: 33232
		private int trunkWiltRecoverHandle = -1;
	}
}
