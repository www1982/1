using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005A2 RID: 1442
public class ShakeHarvestMonitor : GameStateMachine<ShakeHarvestMonitor, ShakeHarvestMonitor.Instance, IStateMachineTarget, ShakeHarvestMonitor.Def>
{
	// Token: 0x060020E5 RID: 8421 RVA: 0x000BDB44 File Offset: 0x000BBD44
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.cooldown;
		this.cooldown.Update(delegate(ShakeHarvestMonitor.Instance smi, float dt)
		{
			this.elapsedTime.Set(this.elapsedTime.Get(smi) + dt, smi, false);
		}, UpdateRate.SIM_200ms, false).ParamTransition<float>(this.elapsedTime, this.harvest, (ShakeHarvestMonitor.Instance smi, float elapsedTime) => elapsedTime > smi.def.cooldownDuration);
		this.harvest.DefaultState(this.harvest.seek).ParamTransition<float>(this.elapsedTime, this.cooldown, GameStateMachine<ShakeHarvestMonitor, ShakeHarvestMonitor.Instance, IStateMachineTarget, ShakeHarvestMonitor.Def>.IsLTEZero);
		this.harvest.seek.PreBrainUpdate(delegate(ShakeHarvestMonitor.Instance smi)
		{
			this.plant.Set(smi.Seek(), smi);
		}).ParamTransition<GameObject>(this.plant, this.harvest.execute, GameStateMachine<ShakeHarvestMonitor, ShakeHarvestMonitor.Instance, IStateMachineTarget, ShakeHarvestMonitor.Def>.IsNotNull);
		this.harvest.execute.Enter(delegate(ShakeHarvestMonitor.Instance smi)
		{
			this.plant.Get(smi).AddTag(ShakeHarvestMonitor.Reserved);
		}).OnSignal(this.failed, this.harvest.seek).ToggleBehaviour(GameTags.Creatures.WantsToHarvest, (ShakeHarvestMonitor.Instance smi) => this.plant.Get(smi) != null, delegate(ShakeHarvestMonitor.Instance smi)
		{
			this.elapsedTime.Set(0f, smi, false);
		})
			.Exit(delegate(ShakeHarvestMonitor.Instance smi)
			{
				GameObject gameObject = this.plant.Get(smi);
				if (gameObject != null)
				{
					gameObject.RemoveTag(ShakeHarvestMonitor.Reserved);
					this.plant.Set(null, smi);
				}
			});
	}

	// Token: 0x04001323 RID: 4899
	public static readonly Tag Reserved = GameTags.Creatures.ReservedByCreature;

	// Token: 0x04001324 RID: 4900
	public GameStateMachine<ShakeHarvestMonitor, ShakeHarvestMonitor.Instance, IStateMachineTarget, ShakeHarvestMonitor.Def>.State cooldown;

	// Token: 0x04001325 RID: 4901
	public ShakeHarvestMonitor.HarvestStates harvest;

	// Token: 0x04001326 RID: 4902
	public StateMachine<ShakeHarvestMonitor, ShakeHarvestMonitor.Instance, IStateMachineTarget, ShakeHarvestMonitor.Def>.FloatParameter elapsedTime = new StateMachine<ShakeHarvestMonitor, ShakeHarvestMonitor.Instance, IStateMachineTarget, ShakeHarvestMonitor.Def>.FloatParameter(float.MaxValue);

	// Token: 0x04001327 RID: 4903
	public StateMachine<ShakeHarvestMonitor, ShakeHarvestMonitor.Instance, IStateMachineTarget, ShakeHarvestMonitor.Def>.TargetParameter plant;

	// Token: 0x04001328 RID: 4904
	public StateMachine<ShakeHarvestMonitor, ShakeHarvestMonitor.Instance, IStateMachineTarget, ShakeHarvestMonitor.Def>.Signal failed;

	// Token: 0x02001428 RID: 5160
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06008CAF RID: 36015 RVA: 0x00356924 File Offset: 0x00354B24
		public Navigator.Scanner<KPrefabID> PlantSeeker
		{
			get
			{
				if (this.plantSeeker == null)
				{
					this.plantSeeker = new Navigator.Scanner<KPrefabID>(this.radius, GameScenePartitioner.Instance.plants, new Func<KPrefabID, bool>(this.IsHarvestablePlant));
					this.plantSeeker.SetDynamicOffsetsFn(delegate(KPrefabID plant, List<CellOffset> offsets)
					{
						ShakeHarvestMonitor.Def.GetApproachOffsets(plant.gameObject, offsets);
					});
				}
				return this.plantSeeker;
			}
		}

		// Token: 0x06008CB0 RID: 36016 RVA: 0x00356990 File Offset: 0x00354B90
		private bool IsHarvestablePlant(KPrefabID plant)
		{
			if (plant == null)
			{
				return false;
			}
			if (plant.pendingDestruction)
			{
				return false;
			}
			if (plant.HasTag(ShakeHarvestMonitor.Reserved))
			{
				return false;
			}
			if (!this.harvestablePlants.Contains(plant.PrefabID()))
			{
				return false;
			}
			Harvestable component = plant.GetComponent<Harvestable>();
			return !(component == null) && component.CanBeHarvested;
		}

		// Token: 0x06008CB1 RID: 36017 RVA: 0x003569F4 File Offset: 0x00354BF4
		public static void GetApproachOffsets(GameObject plant, List<CellOffset> offsets)
		{
			Extents extents = plant.GetComponent<OccupyArea>().GetExtents();
			int num = -1;
			int width = extents.width;
			for (int num2 = 0; num2 != extents.height; num2++)
			{
				int num3 = num2;
				offsets.Add(new CellOffset(num, num3));
				offsets.Add(new CellOffset(width, num3));
			}
		}

		// Token: 0x04006BC9 RID: 27593
		public float cooldownDuration;

		// Token: 0x04006BCA RID: 27594
		public HashSet<Tag> harvestablePlants = new HashSet<Tag>();

		// Token: 0x04006BCB RID: 27595
		public int radius = 10;

		// Token: 0x04006BCC RID: 27596
		private Navigator.Scanner<KPrefabID> plantSeeker;
	}

	// Token: 0x02001429 RID: 5161
	public class HarvestStates : GameStateMachine<ShakeHarvestMonitor, ShakeHarvestMonitor.Instance, IStateMachineTarget, ShakeHarvestMonitor.Def>.State
	{
		// Token: 0x04006BCD RID: 27597
		public GameStateMachine<ShakeHarvestMonitor, ShakeHarvestMonitor.Instance, IStateMachineTarget, ShakeHarvestMonitor.Def>.State seek;

		// Token: 0x04006BCE RID: 27598
		public GameStateMachine<ShakeHarvestMonitor, ShakeHarvestMonitor.Instance, IStateMachineTarget, ShakeHarvestMonitor.Def>.State execute;
	}

	// Token: 0x0200142A RID: 5162
	public new class Instance : GameStateMachine<ShakeHarvestMonitor, ShakeHarvestMonitor.Instance, IStateMachineTarget, ShakeHarvestMonitor.Def>.GameInstance
	{
		// Token: 0x06008CB4 RID: 36020 RVA: 0x00356A69 File Offset: 0x00354C69
		public Instance(IStateMachineTarget master, ShakeHarvestMonitor.Def def)
			: base(master, def)
		{
			this.navigator = base.GetComponent<Navigator>();
		}

		// Token: 0x06008CB5 RID: 36021 RVA: 0x00356A7F File Offset: 0x00354C7F
		public KPrefabID Seek()
		{
			return base.def.PlantSeeker.Scan(Grid.PosToXY(base.smi.transform.GetPosition()), this.navigator);
		}

		// Token: 0x04006BCF RID: 27599
		private readonly Navigator navigator;
	}
}
