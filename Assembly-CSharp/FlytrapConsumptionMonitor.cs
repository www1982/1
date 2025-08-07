using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200086D RID: 2157
public class FlytrapConsumptionMonitor : StateMachineComponent<FlytrapConsumptionMonitor.Instance>, IGameObjectEffectDescriptor, IPlantConsumeEntities
{
	// Token: 0x06003B33 RID: 15155 RVA: 0x00148CEC File Offset: 0x00146EEC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06003B34 RID: 15156 RVA: 0x00148CFF File Offset: 0x00146EFF
	public string GetConsumableEntitiesCategoryName()
	{
		return CREATURES.SPECIES.FLYTRAPPLANT.VICTIM_IDENTIFIER;
	}

	// Token: 0x06003B35 RID: 15157 RVA: 0x00148D0B File Offset: 0x00146F0B
	public bool AreEntitiesConsumptionRequirementsSatisfied()
	{
		return base.smi != null && base.smi.HasEaten;
	}

	// Token: 0x06003B36 RID: 15158 RVA: 0x00148D22 File Offset: 0x00146F22
	public string GetRequirementText()
	{
		return CREATURES.SPECIES.FLYTRAPPLANT.PLANT_HUNGER_REQUIREMENT;
	}

	// Token: 0x06003B37 RID: 15159 RVA: 0x00148D2E File Offset: 0x00146F2E
	public string GetConsumedEntityName()
	{
		if (base.smi != null)
		{
			return base.smi.LastConsumedEntityName;
		}
		return "Unknown Critter";
	}

	// Token: 0x06003B38 RID: 15160 RVA: 0x00148D4C File Offset: 0x00146F4C
	public List<KPrefabID> GetPrefabsOfPossiblePrey()
	{
		List<GameObject> prefabsWithTag = Assets.GetPrefabsWithTag(FlytrapConsumptionMonitor.CONSUMABLE_TAG);
		List<KPrefabID> list = new List<KPrefabID>();
		for (int i = 0; i < prefabsWithTag.Count; i++)
		{
			KPrefabID component = prefabsWithTag[i].GetComponent<KPrefabID>();
			if (this.IsEntityEdible(component) && !list.Contains(component) && Game.IsCorrectDlcActiveForCurrentSave(component))
			{
				list.Add(component);
			}
		}
		return list;
	}

	// Token: 0x06003B39 RID: 15161 RVA: 0x00148DAC File Offset: 0x00146FAC
	public string[] GetFormattedPossiblePreyList()
	{
		List<string> list = new List<string>();
		foreach (KPrefabID kprefabID in this.GetPrefabsOfPossiblePrey())
		{
			CreatureBrain component = kprefabID.GetComponent<CreatureBrain>();
			if (component != null)
			{
				string text = component.species.ProperName();
				if (!list.Contains(text))
				{
					list.Add(text);
				}
			}
		}
		return list.ToArray();
	}

	// Token: 0x06003B3A RID: 15162 RVA: 0x00148E30 File Offset: 0x00147030
	public bool IsEntityEdible(GameObject entity)
	{
		return !(entity == null) && this.IsEntityEdible(entity.GetComponent<KPrefabID>());
	}

	// Token: 0x06003B3B RID: 15163 RVA: 0x00148E49 File Offset: 0x00147049
	public bool IsEntityEdible(KPrefabID entity)
	{
		return !(entity == null) && entity.HasTag(FlytrapConsumptionMonitor.CONSUMABLE_TAG) && entity.GetComponent<CreatureBrain>() != null && entity.GetComponent<OccupyArea>().OccupiedCellsOffsets.Length <= 1;
	}

	// Token: 0x06003B3C RID: 15164 RVA: 0x00148E88 File Offset: 0x00147088
	public List<Descriptor> GetDescriptors(GameObject obj)
	{
		return new List<Descriptor>
		{
			new Descriptor(this.GetRequirementText(), "", Descriptor.DescriptorType.Requirement, false)
		};
	}

	// Token: 0x06003B3D RID: 15165 RVA: 0x00148EA7 File Offset: 0x001470A7
	public static bool IsWilted(FlytrapConsumptionMonitor.Instance smi)
	{
		return smi.IsWilted;
	}

	// Token: 0x06003B3E RID: 15166 RVA: 0x00148EAF File Offset: 0x001470AF
	public static void CompleteEat(FlytrapConsumptionMonitor.Instance smi)
	{
		smi.sm.HasEaten.Set(true, smi, false);
	}

	// Token: 0x06003B3F RID: 15167 RVA: 0x00148EC8 File Offset: 0x001470C8
	public static void RetriggerGrowAnimationIfInGrowState(FlytrapConsumptionMonitor.Instance smi)
	{
		StandardCropPlant component = smi.GetComponent<StandardCropPlant>();
		if (component == null || component.smi == null)
		{
			return;
		}
		if (component.smi.IsInsideState(component.smi.sm.alive.idle))
		{
			KBatchedAnimController component2 = smi.GetComponent<KBatchedAnimController>();
			if (component2 != null)
			{
				component2.Play(component.anims.grow, component.anims.grow_playmode, 1f, 0f);
			}
		}
	}

	// Token: 0x06003B40 RID: 15168 RVA: 0x00148F4B File Offset: 0x0014714B
	public static void BecomeHungry(FlytrapConsumptionMonitor.Instance smi)
	{
		smi.sm.HasEaten.Set(false, smi, false);
	}

	// Token: 0x06003B41 RID: 15169 RVA: 0x00148F61 File Offset: 0x00147161
	public static void RegisterVictimProximityMonitor(FlytrapConsumptionMonitor.Instance smi)
	{
		smi.RegisterVictimProximityMonitor();
	}

	// Token: 0x06003B42 RID: 15170 RVA: 0x00148F69 File Offset: 0x00147169
	public static void UnregisterVictimProximityMonitor(FlytrapConsumptionMonitor.Instance smi)
	{
		smi.UnregisterVictimProximityMonitor();
	}

	// Token: 0x06003B43 RID: 15171 RVA: 0x00148F74 File Offset: 0x00147174
	public static void SetAndPlayConsumeCropPlantAnimations(FlytrapConsumptionMonitor.Instance smi)
	{
		StandardCropPlant component = smi.GetComponent<StandardCropPlant>();
		if (component == null || component.smi == null)
		{
			return;
		}
		component.anims = FlytrapConsumptionMonitor.EATING_STATE_ANIM_SET;
		component.smi.GoTo(component.smi.sm.alive.pre_idle);
	}

	// Token: 0x06003B44 RID: 15172 RVA: 0x00148FC8 File Offset: 0x001471C8
	public static void SetCropPlantAnimationsToAwaitPrey(FlytrapConsumptionMonitor.Instance smi)
	{
		FlytrapConsumptionMonitor.SetCropPlantAnimationSet(smi, FlytrapConsumptionMonitor.HUNGRY_STATE_ANIM_SET);
		FlytrapConsumptionMonitor.RetriggerGrowAnimationIfInGrowState(smi);
		StandardCropPlant component = smi.GetComponent<StandardCropPlant>();
		if (component == null || component.smi == null)
		{
			return;
		}
		component.preventGrowPositionUpdate = true;
	}

	// Token: 0x06003B45 RID: 15173 RVA: 0x00149008 File Offset: 0x00147208
	public static void RestoreDefaultCropPlantAnimations(FlytrapConsumptionMonitor.Instance smi)
	{
		FlytrapConsumptionMonitor.SetCropPlantAnimationSet(smi, FlyTrapPlantConfig.Default_StandardCropAnimSet);
		StandardCropPlant component = smi.GetComponent<StandardCropPlant>();
		if (component == null || component.smi == null)
		{
			return;
		}
		component.preventGrowPositionUpdate = false;
	}

	// Token: 0x06003B46 RID: 15174 RVA: 0x00149040 File Offset: 0x00147240
	private static void SetCropPlantAnimationSet(FlytrapConsumptionMonitor.Instance smi, StandardCropPlant.AnimSet set)
	{
		StandardCropPlant component = smi.GetComponent<StandardCropPlant>();
		if (component == null || component.smi == null)
		{
			return;
		}
		component.anims = set;
	}

	// Token: 0x04002455 RID: 9301
	public const string AWAIT_PREY_ANIM_NAME = "awaiting_prey";

	// Token: 0x04002456 RID: 9302
	public const string EAT_ANIM_NAME = "consume";

	// Token: 0x04002457 RID: 9303
	private const string CONSUMED_ENTITY_NAME_FALLBACK = "Unknown Critter";

	// Token: 0x04002458 RID: 9304
	private static Tag CONSUMABLE_TAG = GameTags.Creatures.Flyer;

	// Token: 0x04002459 RID: 9305
	public static readonly StandardCropPlant.AnimSet HUNGRY_STATE_ANIM_SET = new StandardCropPlant.AnimSet(FlyTrapPlantConfig.Default_StandardCropAnimSet)
	{
		grow = "awaiting_prey",
		wilt_base = "flower_wilt",
		grow_playmode = KAnim.PlayMode.Loop
	};

	// Token: 0x0400245A RID: 9306
	public static readonly StandardCropPlant.AnimSet EATING_STATE_ANIM_SET = new StandardCropPlant.AnimSet(FlyTrapPlantConfig.Default_StandardCropAnimSet)
	{
		pre_grow = "consume",
		grow_playmode = KAnim.PlayMode.Paused
	};

	// Token: 0x02001812 RID: 6162
	public class States : GameStateMachine<FlytrapConsumptionMonitor.States, FlytrapConsumptionMonitor.Instance, FlytrapConsumptionMonitor>
	{
		// Token: 0x06009B67 RID: 39783 RVA: 0x0038D5A8 File Offset: 0x0038B7A8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.serializable = StateMachine.SerializeType.ParamsOnly;
			default_state = this.hungry;
			this.hungry.ParamTransition<bool>(this.HasEaten, this.satisfied, GameStateMachine<FlytrapConsumptionMonitor.States, FlytrapConsumptionMonitor.Instance, FlytrapConsumptionMonitor, object>.IsTrue).Toggle("Toggle Standard Crop Plant Animations", new StateMachine<FlytrapConsumptionMonitor.States, FlytrapConsumptionMonitor.Instance, FlytrapConsumptionMonitor, object>.State.Callback(FlytrapConsumptionMonitor.SetCropPlantAnimationsToAwaitPrey), new StateMachine<FlytrapConsumptionMonitor.States, FlytrapConsumptionMonitor.Instance, FlytrapConsumptionMonitor, object>.State.Callback(FlytrapConsumptionMonitor.RestoreDefaultCropPlantAnimations)).ToggleAttributeModifier("Pause Growing", (FlytrapConsumptionMonitor.Instance smi) => smi.pauseGrowing, null)
				.DefaultState(this.hungry.idle);
			this.hungry.idle.EventTransition(GameHashes.Wilt, this.hungry.wilt, new StateMachine<FlytrapConsumptionMonitor.States, FlytrapConsumptionMonitor.Instance, FlytrapConsumptionMonitor, object>.Transition.ConditionCallback(FlytrapConsumptionMonitor.IsWilted)).ToggleStatusItem(Db.Get().CreatureStatusItems.CarnivorousPlantAwaitingVictim, (FlytrapConsumptionMonitor.Instance smi) => smi.master.GetComponent<IPlantConsumeEntities>()).Enter(new StateMachine<FlytrapConsumptionMonitor.States, FlytrapConsumptionMonitor.Instance, FlytrapConsumptionMonitor, object>.State.Callback(FlytrapConsumptionMonitor.RegisterVictimProximityMonitor))
				.TriggerOnEnter(GameHashes.CropSleep, null)
				.OnSignal(this.EatSignal, this.hungry.complete)
				.Exit(new StateMachine<FlytrapConsumptionMonitor.States, FlytrapConsumptionMonitor.Instance, FlytrapConsumptionMonitor, object>.State.Callback(FlytrapConsumptionMonitor.UnregisterVictimProximityMonitor));
			this.hungry.complete.Enter(new StateMachine<FlytrapConsumptionMonitor.States, FlytrapConsumptionMonitor.Instance, FlytrapConsumptionMonitor, object>.State.Callback(FlytrapConsumptionMonitor.SetAndPlayConsumeCropPlantAnimations)).Enter(new StateMachine<FlytrapConsumptionMonitor.States, FlytrapConsumptionMonitor.Instance, FlytrapConsumptionMonitor, object>.State.Callback(FlytrapConsumptionMonitor.CompleteEat));
			this.hungry.wilt.EventTransition(GameHashes.WiltRecover, this.hungry.idle, GameStateMachine<FlytrapConsumptionMonitor.States, FlytrapConsumptionMonitor.Instance, FlytrapConsumptionMonitor, object>.Not(new StateMachine<FlytrapConsumptionMonitor.States, FlytrapConsumptionMonitor.Instance, FlytrapConsumptionMonitor, object>.Transition.ConditionCallback(FlytrapConsumptionMonitor.IsWilted)));
			this.satisfied.Enter(new StateMachine<FlytrapConsumptionMonitor.States, FlytrapConsumptionMonitor.Instance, FlytrapConsumptionMonitor, object>.State.Callback(FlytrapConsumptionMonitor.RetriggerGrowAnimationIfInGrowState)).TriggerOnEnter(GameHashes.CropWakeUp, null).ParamTransition<bool>(this.HasEaten, this.hungry, GameStateMachine<FlytrapConsumptionMonitor.States, FlytrapConsumptionMonitor.Instance, FlytrapConsumptionMonitor, object>.IsFalse)
				.EventHandler(GameHashes.Harvest, new StateMachine<FlytrapConsumptionMonitor.States, FlytrapConsumptionMonitor.Instance, FlytrapConsumptionMonitor, object>.State.Callback(FlytrapConsumptionMonitor.BecomeHungry));
		}

		// Token: 0x040077CD RID: 30669
		public FlytrapConsumptionMonitor.States.HungryStates hungry;

		// Token: 0x040077CE RID: 30670
		public GameStateMachine<FlytrapConsumptionMonitor.States, FlytrapConsumptionMonitor.Instance, FlytrapConsumptionMonitor, object>.State satisfied;

		// Token: 0x040077CF RID: 30671
		public StateMachine<FlytrapConsumptionMonitor.States, FlytrapConsumptionMonitor.Instance, FlytrapConsumptionMonitor, object>.BoolParameter HasEaten;

		// Token: 0x040077D0 RID: 30672
		public StateMachine<FlytrapConsumptionMonitor.States, FlytrapConsumptionMonitor.Instance, FlytrapConsumptionMonitor, object>.Signal EatSignal;

		// Token: 0x0200281F RID: 10271
		public class HungryStates : GameStateMachine<FlytrapConsumptionMonitor.States, FlytrapConsumptionMonitor.Instance, FlytrapConsumptionMonitor, object>.State
		{
			// Token: 0x0400B1D6 RID: 45526
			public GameStateMachine<FlytrapConsumptionMonitor.States, FlytrapConsumptionMonitor.Instance, FlytrapConsumptionMonitor, object>.State wilt;

			// Token: 0x0400B1D7 RID: 45527
			public GameStateMachine<FlytrapConsumptionMonitor.States, FlytrapConsumptionMonitor.Instance, FlytrapConsumptionMonitor, object>.State idle;

			// Token: 0x0400B1D8 RID: 45528
			public GameStateMachine<FlytrapConsumptionMonitor.States, FlytrapConsumptionMonitor.Instance, FlytrapConsumptionMonitor, object>.State complete;
		}
	}

	// Token: 0x02001813 RID: 6163
	public class Instance : GameStateMachine<FlytrapConsumptionMonitor.States, FlytrapConsumptionMonitor.Instance, FlytrapConsumptionMonitor, object>.GameInstance
	{
		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06009B69 RID: 39785 RVA: 0x0038D797 File Offset: 0x0038B997
		public bool HasEaten
		{
			get
			{
				return base.sm.HasEaten.Get(this);
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06009B6A RID: 39786 RVA: 0x0038D7AA File Offset: 0x0038B9AA
		public bool IsWilted
		{
			get
			{
				return this.wiltCondition.IsWilting();
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06009B6B RID: 39787 RVA: 0x0038D7B7 File Offset: 0x0038B9B7
		public string LastConsumedEntityName
		{
			get
			{
				if (!string.IsNullOrEmpty(this.lastConsumedEntityPrefabID))
				{
					return Assets.GetPrefab(this.lastConsumedEntityPrefabID).GetProperName();
				}
				return "Unknown Critter";
			}
		}

		// Token: 0x06009B6C RID: 39788 RVA: 0x0038D7E4 File Offset: 0x0038B9E4
		public Instance(FlytrapConsumptionMonitor master)
			: base(master)
		{
			Amounts amounts = base.gameObject.GetAmounts();
			this.maturity = amounts.Get(Db.Get().Amounts.Maturity);
			this.pauseGrowing = new AttributeModifier(this.maturity.deltaAttribute.Id, -1f, CREATURES.SPECIES.FLYTRAPPLANT.HUNGRY, true, false, true);
			this.wiltCondition = base.GetComponent<WiltCondition>();
			this.growing = base.GetComponent<Growing>();
			this.growing.CustomGrowStallCondition_IsStalled = new Func<GameObject, bool>(this.ShouldStallGrowingComponent);
		}

		// Token: 0x06009B6D RID: 39789 RVA: 0x0038D886 File Offset: 0x0038BA86
		private bool ShouldStallGrowingComponent(GameObject plantGameObject)
		{
			return !this.HasEaten;
		}

		// Token: 0x06009B6E RID: 39790 RVA: 0x0038D894 File Offset: 0x0038BA94
		public void RegisterVictimProximityMonitor()
		{
			OccupyArea component = base.GetComponent<OccupyArea>();
			this.partitionerEntry = GameScenePartitioner.Instance.Add("FlytrapConsumptionMonitor.hungry.idle", base.gameObject, component.GetExtents(), GameScenePartitioner.Instance.pickupablesChangedLayer, new Action<object>(this.OnPickupableLayerObjectDetected));
		}

		// Token: 0x06009B6F RID: 39791 RVA: 0x0038D8DF File Offset: 0x0038BADF
		public void UnregisterVictimProximityMonitor()
		{
			GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
			this.partitionerEntry = HandleVector<int>.InvalidHandle;
		}

		// Token: 0x06009B70 RID: 39792 RVA: 0x0038D8FC File Offset: 0x0038BAFC
		public void OnPickupableLayerObjectDetected(object obj)
		{
			Pickupable pickupable = obj as Pickupable;
			if (base.master.IsEntityEdible(pickupable.gameObject))
			{
				this.lastConsumedEntityPrefabID = pickupable.PrefabID().ToString();
				pickupable.gameObject.DeleteObject();
				base.sm.EatSignal.Trigger(this);
			}
		}

		// Token: 0x040077D1 RID: 30673
		public AttributeModifier pauseGrowing;

		// Token: 0x040077D2 RID: 30674
		[Serialize]
		private string lastConsumedEntityPrefabID;

		// Token: 0x040077D3 RID: 30675
		private Growing growing;

		// Token: 0x040077D4 RID: 30676
		private WiltCondition wiltCondition;

		// Token: 0x040077D5 RID: 30677
		private AmountInstance maturity;

		// Token: 0x040077D6 RID: 30678
		private HandleVector<int>.Handle partitionerEntry = HandleVector<int>.InvalidHandle;
	}
}
