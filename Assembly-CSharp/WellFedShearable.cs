using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200088B RID: 2187
public class WellFedShearable : GameStateMachine<WellFedShearable, WellFedShearable.Instance, IStateMachineTarget, WellFedShearable.Def>
{
	// Token: 0x06003C31 RID: 15409 RVA: 0x0014D52C File Offset: 0x0014B72C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.growing;
		this.root.Enter(delegate(WellFedShearable.Instance smi)
		{
			WellFedShearable.UpdateScales(smi, 0f);
		}).Enter(delegate(WellFedShearable.Instance smi)
		{
			if (smi.def.hideSymbols != null)
			{
				foreach (KAnimHashedString kanimHashedString in smi.def.hideSymbols)
				{
					smi.animController.SetSymbolVisiblity(kanimHashedString, false);
				}
			}
		}).Update(new Action<WellFedShearable.Instance, float>(WellFedShearable.UpdateScales), UpdateRate.SIM_1000ms, false)
			.EventHandler(GameHashes.CaloriesConsumed, delegate(WellFedShearable.Instance smi, object data)
			{
				smi.OnCaloriesConsumed(data);
			});
		this.growing.Enter(delegate(WellFedShearable.Instance smi)
		{
			WellFedShearable.UpdateScales(smi, 0f);
		}).Transition(this.fullyGrown, new StateMachine<WellFedShearable, WellFedShearable.Instance, IStateMachineTarget, WellFedShearable.Def>.Transition.ConditionCallback(WellFedShearable.AreScalesFullyGrown), UpdateRate.SIM_1000ms);
		this.fullyGrown.Enter(delegate(WellFedShearable.Instance smi)
		{
			WellFedShearable.UpdateScales(smi, 0f);
		}).ToggleBehaviour(GameTags.Creatures.ScalesGrown, (WellFedShearable.Instance smi) => smi.HasTag(GameTags.Creatures.CanMolt), null).EventTransition(GameHashes.Molt, this.growing, GameStateMachine<WellFedShearable, WellFedShearable.Instance, IStateMachineTarget, WellFedShearable.Def>.Not(new StateMachine<WellFedShearable, WellFedShearable.Instance, IStateMachineTarget, WellFedShearable.Def>.Transition.ConditionCallback(WellFedShearable.AreScalesFullyGrown)))
			.Transition(this.growing, GameStateMachine<WellFedShearable, WellFedShearable.Instance, IStateMachineTarget, WellFedShearable.Def>.Not(new StateMachine<WellFedShearable, WellFedShearable.Instance, IStateMachineTarget, WellFedShearable.Def>.Transition.ConditionCallback(WellFedShearable.AreScalesFullyGrown)), UpdateRate.SIM_1000ms);
	}

	// Token: 0x06003C32 RID: 15410 RVA: 0x0014D6A2 File Offset: 0x0014B8A2
	private static bool AreScalesFullyGrown(WellFedShearable.Instance smi)
	{
		return smi.scaleGrowth.value >= smi.scaleGrowth.GetMax();
	}

	// Token: 0x06003C33 RID: 15411 RVA: 0x0014D6C0 File Offset: 0x0014B8C0
	private static void UpdateScales(WellFedShearable.Instance smi, float dt)
	{
		int num = (int)((float)smi.def.levelCount * smi.scaleGrowth.value / 100f);
		if (smi.currentScaleLevel != num)
		{
			for (int i = 0; i < smi.def.scaleGrowthSymbols.Length; i++)
			{
				bool flag = i <= num - 1;
				smi.animController.SetSymbolVisiblity(smi.def.scaleGrowthSymbols[i], flag);
			}
			smi.currentScaleLevel = num;
		}
	}

	// Token: 0x040024E8 RID: 9448
	public GameStateMachine<WellFedShearable, WellFedShearable.Instance, IStateMachineTarget, WellFedShearable.Def>.State growing;

	// Token: 0x040024E9 RID: 9449
	public GameStateMachine<WellFedShearable, WellFedShearable.Instance, IStateMachineTarget, WellFedShearable.Def>.State fullyGrown;

	// Token: 0x02001851 RID: 6225
	public class Def : StateMachine.BaseDef, IGameObjectEffectDescriptor
	{
		// Token: 0x06009C23 RID: 39971 RVA: 0x0039025B File Offset: 0x0038E45B
		public override void Configure(GameObject prefab)
		{
			prefab.GetComponent<Modifiers>().initialAmounts.Add(Db.Get().Amounts.ScaleGrowth.Id);
		}

		// Token: 0x06009C24 RID: 39972 RVA: 0x00390284 File Offset: 0x0038E484
		public List<Descriptor> GetDescriptors(GameObject obj)
		{
			return new List<Descriptor>
			{
				new Descriptor(UI.BUILDINGEFFECTS.SCALE_GROWTH.Replace("{Item}", this.itemDroppedOnShear.ProperName()).Replace("{Amount}", GameUtil.GetFormattedMass(this.dropMass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")).Replace("{Time}", GameUtil.GetFormattedCycles(this.growthDurationCycles * 600f, "F1", false)), UI.BUILDINGEFFECTS.TOOLTIPS.SCALE_GROWTH_FED.Replace("{Item}", this.itemDroppedOnShear.ProperName()).Replace("{Amount}", GameUtil.GetFormattedMass(this.dropMass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")).Replace("{Time}", GameUtil.GetFormattedCycles(this.growthDurationCycles * 600f, "F1", false)), Descriptor.DescriptorType.Effect, false)
			};
		}

		// Token: 0x04007889 RID: 30857
		public string effectId;

		// Token: 0x0400788A RID: 30858
		public float caloriesPerCycle;

		// Token: 0x0400788B RID: 30859
		public float growthDurationCycles;

		// Token: 0x0400788C RID: 30860
		public int levelCount;

		// Token: 0x0400788D RID: 30861
		public Tag itemDroppedOnShear;

		// Token: 0x0400788E RID: 30862
		public float dropMass;

		// Token: 0x0400788F RID: 30863
		public Tag requiredDiet = null;

		// Token: 0x04007890 RID: 30864
		public KAnimHashedString[] scaleGrowthSymbols = WellFedShearable.Def.SCALE_SYMBOL_NAMES;

		// Token: 0x04007891 RID: 30865
		public KAnimHashedString[] hideSymbols;

		// Token: 0x04007892 RID: 30866
		public static KAnimHashedString[] SCALE_SYMBOL_NAMES = new KAnimHashedString[] { "scale_0", "scale_1", "scale_2", "scale_3", "scale_4" };
	}

	// Token: 0x02001852 RID: 6226
	public new class Instance : GameStateMachine<WellFedShearable, WellFedShearable.Instance, IStateMachineTarget, WellFedShearable.Def>.GameInstance, IShearable
	{
		// Token: 0x06009C27 RID: 39975 RVA: 0x003903E4 File Offset: 0x0038E5E4
		public Instance(IStateMachineTarget master, WellFedShearable.Def def)
			: base(master, def)
		{
			this.scaleGrowth = Db.Get().Amounts.ScaleGrowth.Lookup(base.gameObject);
			this.scaleGrowth.value = this.scaleGrowth.GetMax();
		}

		// Token: 0x06009C28 RID: 39976 RVA: 0x00390436 File Offset: 0x0038E636
		public bool IsFullyGrown()
		{
			return this.currentScaleLevel == base.def.levelCount;
		}

		// Token: 0x06009C29 RID: 39977 RVA: 0x0039044C File Offset: 0x0038E64C
		public void OnCaloriesConsumed(object data)
		{
			CreatureCalorieMonitor.CaloriesConsumedEvent caloriesConsumedEvent = (CreatureCalorieMonitor.CaloriesConsumedEvent)data;
			if (base.def.requiredDiet != null && caloriesConsumedEvent.tag != base.def.requiredDiet)
			{
				return;
			}
			EffectInstance effectInstance = this.effects.Get(base.smi.def.effectId);
			if (effectInstance == null)
			{
				effectInstance = this.effects.Add(base.smi.def.effectId, true);
			}
			effectInstance.timeRemaining += caloriesConsumedEvent.calories / base.smi.def.caloriesPerCycle * 600f;
		}

		// Token: 0x06009C2A RID: 39978 RVA: 0x003904F7 File Offset: 0x0038E6F7
		public void Shear()
		{
			this.scaleGrowth.value = 0f;
			WellFedShearable.UpdateScales(this, 0f);
		}

		// Token: 0x06009C2B RID: 39979 RVA: 0x00390514 File Offset: 0x0038E714
		public global::Tuple<Tag, float> GetItemDroppedOnShear()
		{
			return new global::Tuple<Tag, float>(base.def.itemDroppedOnShear, base.def.dropMass);
		}

		// Token: 0x04007893 RID: 30867
		[MyCmpGet]
		private Effects effects;

		// Token: 0x04007894 RID: 30868
		[MyCmpGet]
		public KBatchedAnimController animController;

		// Token: 0x04007895 RID: 30869
		public AmountInstance scaleGrowth;

		// Token: 0x04007896 RID: 30870
		public int currentScaleLevel = -1;
	}
}
