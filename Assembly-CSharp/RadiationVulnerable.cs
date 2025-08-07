using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200087E RID: 2174
public class RadiationVulnerable : GameStateMachine<RadiationVulnerable, RadiationVulnerable.StatesInstance, IStateMachineTarget, RadiationVulnerable.Def>
{
	// Token: 0x06003BD3 RID: 15315 RVA: 0x0014BCC4 File Offset: 0x00149EC4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.comfortable;
		this.comfortable.Transition(this.too_dark, (RadiationVulnerable.StatesInstance smi) => smi.GetRadiationThresholdCrossed() == -1, UpdateRate.SIM_1000ms).Transition(this.too_bright, (RadiationVulnerable.StatesInstance smi) => smi.GetRadiationThresholdCrossed() == 1, UpdateRate.SIM_1000ms).TriggerOnEnter(GameHashes.RadiationComfort, null);
		this.too_dark.Transition(this.comfortable, (RadiationVulnerable.StatesInstance smi) => smi.GetRadiationThresholdCrossed() != -1, UpdateRate.SIM_1000ms).TriggerOnEnter(GameHashes.RadiationDiscomfort, null);
		this.too_bright.Transition(this.comfortable, (RadiationVulnerable.StatesInstance smi) => smi.GetRadiationThresholdCrossed() != 1, UpdateRate.SIM_1000ms).TriggerOnEnter(GameHashes.RadiationDiscomfort, null);
	}

	// Token: 0x040024B5 RID: 9397
	public GameStateMachine<RadiationVulnerable, RadiationVulnerable.StatesInstance, IStateMachineTarget, RadiationVulnerable.Def>.State comfortable;

	// Token: 0x040024B6 RID: 9398
	public GameStateMachine<RadiationVulnerable, RadiationVulnerable.StatesInstance, IStateMachineTarget, RadiationVulnerable.Def>.State too_dark;

	// Token: 0x040024B7 RID: 9399
	public GameStateMachine<RadiationVulnerable, RadiationVulnerable.StatesInstance, IStateMachineTarget, RadiationVulnerable.Def>.State too_bright;

	// Token: 0x02001835 RID: 6197
	public class Def : StateMachine.BaseDef, IGameObjectEffectDescriptor
	{
		// Token: 0x06009BDF RID: 39903 RVA: 0x0038F774 File Offset: 0x0038D974
		public List<Descriptor> GetDescriptors(GameObject go)
		{
			Modifiers component = go.GetComponent<Modifiers>();
			float preModifiedAttributeValue = component.GetPreModifiedAttributeValue(Db.Get().PlantAttributes.MinRadiationThreshold);
			string preModifiedAttributeFormattedValue = component.GetPreModifiedAttributeFormattedValue(Db.Get().PlantAttributes.MinRadiationThreshold);
			string preModifiedAttributeFormattedValue2 = component.GetPreModifiedAttributeFormattedValue(Db.Get().PlantAttributes.MaxRadiationThreshold);
			MutantPlant component2 = go.GetComponent<MutantPlant>();
			bool flag = component2 != null && component2.IsOriginal;
			if (preModifiedAttributeValue <= 0f)
			{
				return new List<Descriptor>
				{
					new Descriptor(UI.GAMEOBJECTEFFECTS.REQUIRES_NO_MIN_RADIATION.Replace("{MaxRads}", preModifiedAttributeFormattedValue2), UI.GAMEOBJECTEFFECTS.TOOLTIPS.REQUIRES_NO_MIN_RADIATION.Replace("{MaxRads}", preModifiedAttributeFormattedValue2) + (flag ? UI.GAMEOBJECTEFFECTS.TOOLTIPS.MUTANT_SEED_TOOLTIP.ToString() : ""), Descriptor.DescriptorType.Requirement, false)
				};
			}
			return new List<Descriptor>
			{
				new Descriptor(UI.GAMEOBJECTEFFECTS.REQUIRES_RADIATION.Replace("{MinRads}", preModifiedAttributeFormattedValue).Replace("{MaxRads}", preModifiedAttributeFormattedValue2), UI.GAMEOBJECTEFFECTS.TOOLTIPS.REQUIRES_RADIATION.Replace("{MinRads}", preModifiedAttributeFormattedValue).Replace("{MaxRads}", preModifiedAttributeFormattedValue2) + (flag ? UI.GAMEOBJECTEFFECTS.TOOLTIPS.MUTANT_SEED_TOOLTIP.ToString() : ""), Descriptor.DescriptorType.Requirement, false)
			};
		}
	}

	// Token: 0x02001836 RID: 6198
	public class StatesInstance : GameStateMachine<RadiationVulnerable, RadiationVulnerable.StatesInstance, IStateMachineTarget, RadiationVulnerable.Def>.GameInstance, IWiltCause
	{
		// Token: 0x06009BE1 RID: 39905 RVA: 0x0038F8A4 File Offset: 0x0038DAA4
		public StatesInstance(IStateMachineTarget master, RadiationVulnerable.Def def)
			: base(master, def)
		{
			this.minRadiationAttributeInstance = Db.Get().PlantAttributes.MinRadiationThreshold.Lookup(base.gameObject);
			this.maxRadiationAttributeInstance = Db.Get().PlantAttributes.MaxRadiationThreshold.Lookup(base.gameObject);
		}

		// Token: 0x06009BE2 RID: 39906 RVA: 0x0038F8FC File Offset: 0x0038DAFC
		public int GetRadiationThresholdCrossed()
		{
			int num = Grid.PosToCell(base.master.gameObject);
			if (!Grid.IsValidCell(num))
			{
				return 0;
			}
			if (Grid.Radiation[num] < this.minRadiationAttributeInstance.GetTotalValue())
			{
				return -1;
			}
			if (Grid.Radiation[num] <= this.maxRadiationAttributeInstance.GetTotalValue())
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06009BE3 RID: 39907 RVA: 0x0038F959 File Offset: 0x0038DB59
		public WiltCondition.Condition[] Conditions
		{
			get
			{
				return new WiltCondition.Condition[] { WiltCondition.Condition.Radiation };
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06009BE4 RID: 39908 RVA: 0x0038F968 File Offset: 0x0038DB68
		public string WiltStateString
		{
			get
			{
				if (base.smi.IsInsideState(base.smi.sm.too_dark))
				{
					return Db.Get().CreatureStatusItems.Crop_Too_NonRadiated.GetName(this);
				}
				if (base.smi.IsInsideState(base.smi.sm.too_bright))
				{
					return Db.Get().CreatureStatusItems.Crop_Too_Radiated.GetName(this);
				}
				return "";
			}
		}

		// Token: 0x04007846 RID: 30790
		private AttributeInstance minRadiationAttributeInstance;

		// Token: 0x04007847 RID: 30791
		private AttributeInstance maxRadiationAttributeInstance;
	}
}
