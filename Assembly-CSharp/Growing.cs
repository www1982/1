using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TemplateClasses;
using UnityEngine;

// Token: 0x02000870 RID: 2160
public class Growing : StateMachineComponent<Growing.StatesInstance>, IGameObjectEffectDescriptor, IManageGrowingStates
{
	// Token: 0x06003B56 RID: 15190 RVA: 0x001492FC File Offset: 0x001474FC
	protected override void OnPrefabInit()
	{
		Amounts amounts = base.gameObject.GetAmounts();
		this.maturity = amounts.Get(Db.Get().Amounts.Maturity);
		this.oldAge = amounts.Add(new AmountInstance(Db.Get().Amounts.OldAge, base.gameObject));
		this.oldAge.maxAttribute.ClearModifiers();
		this.oldAge.maxAttribute.Add(new AttributeModifier(Db.Get().Amounts.OldAge.maxAttribute.Id, this.maxAge, null, false, false, true));
		base.OnPrefabInit();
		base.Subscribe<Growing>(1119167081, Growing.OnNewGameSpawnDelegate);
		base.Subscribe<Growing>(1272413801, Growing.ResetGrowthDelegate);
	}

	// Token: 0x06003B57 RID: 15191 RVA: 0x001493C6 File Offset: 0x001475C6
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		base.gameObject.AddTag(GameTags.GrowingPlant);
	}

	// Token: 0x06003B58 RID: 15192 RVA: 0x001493EC File Offset: 0x001475EC
	private void OnNewGameSpawn(object data)
	{
		Prefab prefab = (Prefab)data;
		if (prefab.amounts != null)
		{
			foreach (Prefab.template_amount_value template_amount_value in prefab.amounts)
			{
				if (template_amount_value.id == this.maturity.amount.Id && template_amount_value.value == this.GetMaxMaturity())
				{
					return;
				}
			}
		}
		if (this.maturity == null)
		{
			KCrashReporter.ReportDevNotification("Maturity.OnNewGameSpawn", Environment.StackTrace, "", false, null);
		}
		this.maturity.SetValue(this.maturity.maxAttribute.GetTotalValue() * this.MaxMaturityValuePercentageToSpawnWith * global::UnityEngine.Random.Range(0f, 1f));
	}

	// Token: 0x06003B59 RID: 15193 RVA: 0x001494A0 File Offset: 0x001476A0
	public void OverrideMaturityLevel(float percent)
	{
		float num = this.maturity.GetMax() * percent;
		this.maturity.SetValue(num);
	}

	// Token: 0x06003B5A RID: 15194 RVA: 0x001494C8 File Offset: 0x001476C8
	public bool ReachedNextHarvest()
	{
		return this.PercentOfCurrentHarvest() >= 1f;
	}

	// Token: 0x06003B5B RID: 15195 RVA: 0x001494DA File Offset: 0x001476DA
	public bool IsGrown()
	{
		return this.maturity.value == this.maturity.GetMax();
	}

	// Token: 0x06003B5C RID: 15196 RVA: 0x001494F4 File Offset: 0x001476F4
	public bool CanGrow()
	{
		return !this.IsGrown();
	}

	// Token: 0x06003B5D RID: 15197 RVA: 0x001494FF File Offset: 0x001476FF
	public bool IsGrowing()
	{
		return this.maturity.GetDelta() > 0f;
	}

	// Token: 0x06003B5E RID: 15198 RVA: 0x00149513 File Offset: 0x00147713
	public void ClampGrowthToHarvest()
	{
		this.maturity.value = this.maturity.GetMax();
	}

	// Token: 0x06003B5F RID: 15199 RVA: 0x0014952B File Offset: 0x0014772B
	public float GetMaxMaturity()
	{
		return this.maturity.GetMax();
	}

	// Token: 0x06003B60 RID: 15200 RVA: 0x00149538 File Offset: 0x00147738
	public float PercentOfCurrentHarvest()
	{
		return this.maturity.value / this.maturity.GetMax();
	}

	// Token: 0x06003B61 RID: 15201 RVA: 0x00149551 File Offset: 0x00147751
	public float TimeUntilNextHarvest()
	{
		return (this.maturity.GetMax() - this.maturity.value) / this.maturity.GetDelta();
	}

	// Token: 0x06003B62 RID: 15202 RVA: 0x00149576 File Offset: 0x00147776
	public float DomesticGrowthTime()
	{
		return this.maturity.GetMax() / base.smi.baseGrowingRate.Value;
	}

	// Token: 0x06003B63 RID: 15203 RVA: 0x00149594 File Offset: 0x00147794
	public float WildGrowthTime()
	{
		return this.maturity.GetMax() / base.smi.wildGrowingRate.Value;
	}

	// Token: 0x06003B64 RID: 15204 RVA: 0x001495B2 File Offset: 0x001477B2
	public float PercentGrown()
	{
		return this.maturity.value / this.maturity.GetMax();
	}

	// Token: 0x06003B65 RID: 15205 RVA: 0x001495CB File Offset: 0x001477CB
	public void ResetGrowth(object data = null)
	{
		this.maturity.value = 0f;
	}

	// Token: 0x06003B66 RID: 15206 RVA: 0x001495DD File Offset: 0x001477DD
	public float PercentOldAge()
	{
		if (!this.shouldGrowOld)
		{
			return 0f;
		}
		return this.oldAge.value / this.oldAge.GetMax();
	}

	// Token: 0x06003B67 RID: 15207 RVA: 0x00149604 File Offset: 0x00147804
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		Klei.AI.Attribute maxAttribute = Db.Get().Amounts.Maturity.maxAttribute;
		list.Add(new Descriptor(go.GetComponent<Modifiers>().GetPreModifiedAttributeDescription(maxAttribute), go.GetComponent<Modifiers>().GetPreModifiedAttributeToolTip(maxAttribute), Descriptor.DescriptorType.Requirement, false));
		return list;
	}

	// Token: 0x06003B68 RID: 15208 RVA: 0x00149650 File Offset: 0x00147850
	public void ConsumeMass(float mass_to_consume)
	{
		float value = this.maturity.value;
		mass_to_consume = Mathf.Min(mass_to_consume, value);
		this.maturity.value = this.maturity.value - mass_to_consume;
		base.gameObject.Trigger(-1793167409, null);
	}

	// Token: 0x06003B69 RID: 15209 RVA: 0x0014969C File Offset: 0x0014789C
	public void ConsumeGrowthUnits(float units_to_consume, float unit_maturity_ratio)
	{
		float num = units_to_consume / unit_maturity_ratio;
		global::Debug.Assert(num <= this.maturity.value);
		this.maturity.value -= num;
		base.gameObject.Trigger(-1793167409, null);
	}

	// Token: 0x06003B6A RID: 15210 RVA: 0x001496E7 File Offset: 0x001478E7
	public Crop GetCropComponent()
	{
		return base.GetComponent<Crop>();
	}

	// Token: 0x06003B6B RID: 15211 RVA: 0x001496EF File Offset: 0x001478EF
	public bool IsWildPlanted()
	{
		return !this.rm.Replanted;
	}

	// Token: 0x04002460 RID: 9312
	public Func<GameObject, bool> CustomGrowStallCondition_IsStalled;

	// Token: 0x04002461 RID: 9313
	public float MaxMaturityValuePercentageToSpawnWith = 1f;

	// Token: 0x04002462 RID: 9314
	public float GROWTH_RATE = 0.0016666667f;

	// Token: 0x04002463 RID: 9315
	public float WILD_GROWTH_RATE = 0.00041666668f;

	// Token: 0x04002464 RID: 9316
	public bool shouldGrowOld = true;

	// Token: 0x04002465 RID: 9317
	public float maxAge = 2400f;

	// Token: 0x04002466 RID: 9318
	private AmountInstance maturity;

	// Token: 0x04002467 RID: 9319
	private AmountInstance oldAge;

	// Token: 0x04002468 RID: 9320
	[MyCmpGet]
	private WiltCondition wiltCondition;

	// Token: 0x04002469 RID: 9321
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x0400246A RID: 9322
	[MyCmpReq]
	private Modifiers modifiers;

	// Token: 0x0400246B RID: 9323
	[MyCmpReq]
	private ReceptacleMonitor rm;

	// Token: 0x0400246C RID: 9324
	private static readonly EventSystem.IntraObjectHandler<Growing> OnNewGameSpawnDelegate = new EventSystem.IntraObjectHandler<Growing>(delegate(Growing component, object data)
	{
		component.OnNewGameSpawn(data);
	});

	// Token: 0x0400246D RID: 9325
	private static readonly EventSystem.IntraObjectHandler<Growing> ResetGrowthDelegate = new EventSystem.IntraObjectHandler<Growing>(delegate(Growing component, object data)
	{
		component.ResetGrowth(data);
	});

	// Token: 0x02001814 RID: 6164
	public class StatesInstance : GameStateMachine<Growing.States, Growing.StatesInstance, Growing, object>.GameInstance
	{
		// Token: 0x06009B71 RID: 39793 RVA: 0x0038D95C File Offset: 0x0038BB5C
		public StatesInstance(Growing master)
			: base(master)
		{
			this.baseGrowingRate = new AttributeModifier(master.maturity.deltaAttribute.Id, master.GROWTH_RATE, CREATURES.STATS.MATURITY.GROWING, false, false, true);
			this.wildGrowingRate = new AttributeModifier(master.maturity.deltaAttribute.Id, master.WILD_GROWTH_RATE, CREATURES.STATS.MATURITY.GROWINGWILD, false, false, true);
			this.getOldRate = new AttributeModifier(master.oldAge.deltaAttribute.Id, master.shouldGrowOld ? 1f : 0f, null, false, false, true);
			this.harvestable = base.GetComponent<Harvestable>();
		}

		// Token: 0x06009B72 RID: 39794 RVA: 0x0038DA0B File Offset: 0x0038BC0B
		public bool IsGrown()
		{
			return base.master.IsGrown();
		}

		// Token: 0x06009B73 RID: 39795 RVA: 0x0038DA18 File Offset: 0x0038BC18
		public bool ReachedNextHarvest()
		{
			return base.master.ReachedNextHarvest();
		}

		// Token: 0x06009B74 RID: 39796 RVA: 0x0038DA25 File Offset: 0x0038BC25
		public void ClampGrowthToHarvest()
		{
			base.master.ClampGrowthToHarvest();
		}

		// Token: 0x06009B75 RID: 39797 RVA: 0x0038DA32 File Offset: 0x0038BC32
		public bool IsWilting()
		{
			return base.master.wiltCondition != null && base.master.wiltCondition.IsWilting();
		}

		// Token: 0x06009B76 RID: 39798 RVA: 0x0038DA5C File Offset: 0x0038BC5C
		public bool IsStalledByCustomCondition()
		{
			bool flag = false;
			if (base.master.CustomGrowStallCondition_IsStalled != null)
			{
				flag = base.master.CustomGrowStallCondition_IsStalled(base.master.gameObject);
			}
			return flag;
		}

		// Token: 0x06009B77 RID: 39799 RVA: 0x0038DA95 File Offset: 0x0038BC95
		public bool CanExitStalled()
		{
			return !this.IsWilting() && (base.master.CustomGrowStallCondition_IsStalled == null || !base.master.CustomGrowStallCondition_IsStalled(base.master.gameObject));
		}

		// Token: 0x040077D7 RID: 30679
		public AttributeModifier baseGrowingRate;

		// Token: 0x040077D8 RID: 30680
		public AttributeModifier wildGrowingRate;

		// Token: 0x040077D9 RID: 30681
		public AttributeModifier getOldRate;

		// Token: 0x040077DA RID: 30682
		public Harvestable harvestable;
	}

	// Token: 0x02001815 RID: 6165
	public class States : GameStateMachine<Growing.States, Growing.StatesInstance, Growing>
	{
		// Token: 0x06009B78 RID: 39800 RVA: 0x0038DAD0 File Offset: 0x0038BCD0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.growing;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.growing.EventTransition(GameHashes.Wilt, this.stalled, (Growing.StatesInstance smi) => smi.IsWilting()).EventTransition(GameHashes.CropSleep, this.stalled, (Growing.StatesInstance smi) => smi.IsStalledByCustomCondition()).EventTransition(GameHashes.ReceptacleMonitorChange, this.growing.planted, (Growing.StatesInstance smi) => !smi.master.IsWildPlanted())
				.EventTransition(GameHashes.ReceptacleMonitorChange, this.growing.wild, (Growing.StatesInstance smi) => smi.master.IsWildPlanted())
				.EventTransition(GameHashes.PlanterStorage, this.growing.planted, (Growing.StatesInstance smi) => !smi.master.IsWildPlanted())
				.EventTransition(GameHashes.PlanterStorage, this.growing.wild, (Growing.StatesInstance smi) => smi.master.IsWildPlanted())
				.TriggerOnEnter(GameHashes.Grow, null)
				.Update("CheckGrown", delegate(Growing.StatesInstance smi, float dt)
				{
					if (smi.ReachedNextHarvest())
					{
						smi.GoTo(this.grown);
					}
				}, UpdateRate.SIM_4000ms, false)
				.ToggleStatusItem(Db.Get().CreatureStatusItems.Growing, (Growing.StatesInstance smi) => smi.master.GetComponent<IManageGrowingStates>())
				.Enter(delegate(Growing.StatesInstance smi)
				{
					GameStateMachine<Growing.States, Growing.StatesInstance, Growing, object>.State state = (smi.master.IsWildPlanted() ? this.growing.wild : this.growing.planted);
					smi.GoTo(state);
				});
			this.growing.wild.ToggleAttributeModifier("GrowingWild", (Growing.StatesInstance smi) => smi.wildGrowingRate, null);
			this.growing.planted.ToggleAttributeModifier("Growing", (Growing.StatesInstance smi) => smi.baseGrowingRate, null);
			this.stalled.EventTransition(GameHashes.WiltRecover, this.growing, (Growing.StatesInstance smi) => smi.CanExitStalled()).EventTransition(GameHashes.CropWakeUp, this.growing, (Growing.StatesInstance smi) => smi.CanExitStalled());
			this.grown.DefaultState(this.grown.idle).TriggerOnEnter(GameHashes.Grow, null).Update("CheckNotGrown", delegate(Growing.StatesInstance smi, float dt)
			{
				if (!smi.ReachedNextHarvest())
				{
					smi.GoTo(this.growing);
				}
			}, UpdateRate.SIM_4000ms, false)
				.ToggleAttributeModifier("GettingOld", (Growing.StatesInstance smi) => smi.getOldRate, null)
				.Enter(delegate(Growing.StatesInstance smi)
				{
					smi.ClampGrowthToHarvest();
				})
				.Exit(delegate(Growing.StatesInstance smi)
				{
					smi.master.oldAge.SetValue(0f);
				});
			this.grown.idle.Update("CheckNotGrown", delegate(Growing.StatesInstance smi, float dt)
			{
				if (smi.master.shouldGrowOld && smi.master.oldAge.value >= smi.master.oldAge.GetMax() && smi.harvestable && smi.harvestable.CanBeHarvested)
				{
					if (smi.harvestable.harvestDesignatable != null)
					{
						bool harvestWhenReady = smi.harvestable.harvestDesignatable.HarvestWhenReady;
						smi.harvestable.ForceCancelHarvest(null);
						smi.harvestable.Harvest();
						if (harvestWhenReady && smi.harvestable != null)
						{
							smi.harvestable.harvestDesignatable.SetHarvestWhenReady(true);
						}
					}
					else
					{
						smi.harvestable.ForceCancelHarvest(null);
						smi.harvestable.Harvest();
					}
					smi.master.maturity.SetValue(0f);
					smi.master.oldAge.SetValue(0f);
				}
			}, UpdateRate.SIM_4000ms, false);
		}

		// Token: 0x040077DB RID: 30683
		public Growing.States.GrowingStates growing;

		// Token: 0x040077DC RID: 30684
		public GameStateMachine<Growing.States, Growing.StatesInstance, Growing, object>.State stalled;

		// Token: 0x040077DD RID: 30685
		public Growing.States.GrownStates grown;

		// Token: 0x02002821 RID: 10273
		public class GrowingStates : GameStateMachine<Growing.States, Growing.StatesInstance, Growing, object>.State
		{
			// Token: 0x0400B1DC RID: 45532
			public GameStateMachine<Growing.States, Growing.StatesInstance, Growing, object>.State wild;

			// Token: 0x0400B1DD RID: 45533
			public GameStateMachine<Growing.States, Growing.StatesInstance, Growing, object>.State planted;
		}

		// Token: 0x02002822 RID: 10274
		public class GrownStates : GameStateMachine<Growing.States, Growing.StatesInstance, Growing, object>.State
		{
			// Token: 0x0400B1DE RID: 45534
			public GameStateMachine<Growing.States, Growing.StatesInstance, Growing, object>.State idle;
		}
	}
}
