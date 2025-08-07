using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000706 RID: 1798
public class ContactConductivePipeBridge : GameStateMachine<ContactConductivePipeBridge, ContactConductivePipeBridge.Instance, IStateMachineTarget, ContactConductivePipeBridge.Def>
{
	// Token: 0x06002D17 RID: 11543 RVA: 0x00102B0C File Offset: 0x00100D0C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.noLiquid;
		this.noLiquid.PlayAnim("off", KAnim.PlayMode.Once).ParamTransition<float>(this.noLiquidTimer, this.withLiquid, GameStateMachine<ContactConductivePipeBridge, ContactConductivePipeBridge.Instance, IStateMachineTarget, ContactConductivePipeBridge.Def>.IsGTZero);
		this.withLiquid.Update(new Action<ContactConductivePipeBridge.Instance, float>(ContactConductivePipeBridge.ExpirationTimerUpdate), UpdateRate.SIM_200ms, false).PlayAnim("on", KAnim.PlayMode.Loop).ParamTransition<float>(this.noLiquidTimer, this.noLiquid, GameStateMachine<ContactConductivePipeBridge, ContactConductivePipeBridge.Instance, IStateMachineTarget, ContactConductivePipeBridge.Def>.IsLTEZero);
	}

	// Token: 0x06002D18 RID: 11544 RVA: 0x00102B84 File Offset: 0x00100D84
	private static void ExpirationTimerUpdate(ContactConductivePipeBridge.Instance smi, float dt)
	{
		float num = smi.sm.noLiquidTimer.Get(smi);
		num -= dt;
		smi.sm.noLiquidTimer.Set(num, smi, false);
	}

	// Token: 0x06002D19 RID: 11545 RVA: 0x00102BBC File Offset: 0x00100DBC
	private static float CalculateMaxWattsTransfered(float buildingTemperature, float building_thermal_conductivity, float content_temperature, float content_thermal_conductivity)
	{
		float num = 1f;
		float num2 = 1f;
		float num3 = 50f;
		float num4 = content_temperature - buildingTemperature;
		float num5 = (content_thermal_conductivity + building_thermal_conductivity) * 0.5f;
		return num4 * num5 * num * num3 / num2;
	}

	// Token: 0x06002D1A RID: 11546 RVA: 0x00102BF0 File Offset: 0x00100DF0
	private static float GetKilloJoulesTransfered(float maxWattsTransfered, float dt, float building_Temperature, float building_heat_capacity, float content_temperature, float content_heat_capacity)
	{
		float num = maxWattsTransfered * dt / 1000f;
		float num2 = Mathf.Min(content_temperature, building_Temperature);
		float num3 = Mathf.Max(content_temperature, building_Temperature);
		float num4 = content_temperature - num / content_heat_capacity;
		float num5 = building_Temperature + num / building_heat_capacity;
		float num6 = Mathf.Clamp(num4, num2, num3);
		num5 = Mathf.Clamp(num5, num2, num3);
		float num7 = Mathf.Abs(num6 - content_temperature);
		float num8 = Mathf.Abs(num5 - building_Temperature);
		float num9 = num7 * content_heat_capacity;
		float num10 = num8 * building_heat_capacity;
		return Mathf.Min(num9, num10) * Mathf.Sign(maxWattsTransfered);
	}

	// Token: 0x06002D1B RID: 11547 RVA: 0x00102C64 File Offset: 0x00100E64
	private static float GetFinalContentTemperature(float KJT, float building_Temperature, float building_heat_capacity, float content_temperature, float content_heat_capacity)
	{
		float num = -KJT;
		float num2 = Mathf.Max(0f, content_temperature + num / content_heat_capacity);
		float num3 = Mathf.Max(0f, building_Temperature - num / building_heat_capacity);
		if ((content_temperature - building_Temperature) * (num2 - num3) < 0f)
		{
			return content_temperature * content_heat_capacity / (content_heat_capacity + building_heat_capacity) + building_Temperature * building_heat_capacity / (content_heat_capacity + building_heat_capacity);
		}
		return num2;
	}

	// Token: 0x06002D1C RID: 11548 RVA: 0x00102CB8 File Offset: 0x00100EB8
	private static float GetFinalBuildingTemperature(float content_temperature, float content_final_temperature, float content_heat_capacity, float building_temperature, float building_heat_capacity)
	{
		float num = (content_temperature - content_final_temperature) * content_heat_capacity;
		float num2 = Mathf.Min(content_temperature, building_temperature);
		float num3 = Mathf.Max(content_temperature, building_temperature);
		float num4 = num / building_heat_capacity;
		return Mathf.Clamp(building_temperature + num4, num2, num3);
	}

	// Token: 0x04001A88 RID: 6792
	private const string loopAnimName = "on";

	// Token: 0x04001A89 RID: 6793
	private const string loopAnim_noWater = "off";

	// Token: 0x04001A8A RID: 6794
	private GameStateMachine<ContactConductivePipeBridge, ContactConductivePipeBridge.Instance, IStateMachineTarget, ContactConductivePipeBridge.Def>.State withLiquid;

	// Token: 0x04001A8B RID: 6795
	private GameStateMachine<ContactConductivePipeBridge, ContactConductivePipeBridge.Instance, IStateMachineTarget, ContactConductivePipeBridge.Def>.State noLiquid;

	// Token: 0x04001A8C RID: 6796
	private StateMachine<ContactConductivePipeBridge, ContactConductivePipeBridge.Instance, IStateMachineTarget, ContactConductivePipeBridge.Def>.FloatParameter noLiquidTimer;

	// Token: 0x0200159A RID: 5530
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04007053 RID: 28755
		public ConduitType type = ConduitType.Liquid;

		// Token: 0x04007054 RID: 28756
		public float pumpKGRate;
	}

	// Token: 0x0200159B RID: 5531
	public new class Instance : GameStateMachine<ContactConductivePipeBridge, ContactConductivePipeBridge.Instance, IStateMachineTarget, ContactConductivePipeBridge.Def>.GameInstance
	{
		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x0600921C RID: 37404 RVA: 0x00365738 File Offset: 0x00363938
		public Tag tag
		{
			get
			{
				if (this.type != ConduitType.Liquid)
				{
					return GameTags.Gas;
				}
				return GameTags.Liquid;
			}
		}

		// Token: 0x0600921D RID: 37405 RVA: 0x0036574E File Offset: 0x0036394E
		public Instance(IStateMachineTarget master, ContactConductivePipeBridge.Def def)
			: base(master, def)
		{
		}

		// Token: 0x0600921E RID: 37406 RVA: 0x00365770 File Offset: 0x00363970
		public override void StartSM()
		{
			base.StartSM();
			this.inputCell = this.building.GetUtilityInputCell();
			this.outputCell = this.building.GetUtilityOutputCell();
			this.structureHandle = GameComps.StructureTemperatures.GetHandle(base.gameObject);
			Conduit.GetFlowManager(this.type).AddConduitUpdater(new Action<float>(this.Flow), ConduitFlowPriority.Default);
		}

		// Token: 0x0600921F RID: 37407 RVA: 0x003657D8 File Offset: 0x003639D8
		protected override void OnCleanUp()
		{
			base.OnCleanUp();
			Conduit.GetFlowManager(this.type).RemoveConduitUpdater(new Action<float>(this.Flow));
		}

		// Token: 0x06009220 RID: 37408 RVA: 0x003657FC File Offset: 0x003639FC
		private void Flow(float dt)
		{
			ConduitFlow flowManager = Conduit.GetFlowManager(this.type);
			if (flowManager.HasConduit(this.inputCell) && flowManager.HasConduit(this.outputCell))
			{
				ConduitFlow.ConduitContents contents = flowManager.GetContents(this.inputCell);
				ConduitFlow.ConduitContents contents2 = flowManager.GetContents(this.outputCell);
				float num = Mathf.Min(contents.mass, base.def.pumpKGRate * dt);
				if (flowManager.CanMergeContents(contents, contents2, num))
				{
					base.smi.sm.noLiquidTimer.Set(1.5f, base.smi, false);
					float amountAllowedForMerging = flowManager.GetAmountAllowedForMerging(contents, contents2, num);
					if (amountAllowedForMerging > 0f)
					{
						float num2 = this.ExchangeStorageTemperatureWithBuilding(contents, amountAllowedForMerging, dt);
						float num3 = ((base.def.type == ConduitType.Liquid) ? Game.Instance.liquidConduitFlow : Game.Instance.gasConduitFlow).AddElement(this.outputCell, contents.element, amountAllowedForMerging, num2, contents.diseaseIdx, contents.diseaseCount);
						if (amountAllowedForMerging != num3)
						{
							global::Debug.Log("Mass Differs By: " + (amountAllowedForMerging - num3).ToString());
						}
						flowManager.RemoveElement(this.inputCell, num3);
					}
				}
			}
		}

		// Token: 0x06009221 RID: 37409 RVA: 0x00365938 File Offset: 0x00363B38
		private float ExchangeStorageTemperatureWithBuilding(ConduitFlow.ConduitContents content, float mass, float dt)
		{
			PrimaryElement component = this.building.GetComponent<PrimaryElement>();
			float num = component.Element.thermalConductivity * this.building.Def.ThermalConductivity;
			if (mass > 0f)
			{
				Element element = ElementLoader.FindElementByHash(content.element);
				float num2 = mass * element.specificHeatCapacity;
				float num3 = this.building.Def.MassForTemperatureModification * component.Element.specificHeatCapacity;
				float temperature = component.Temperature;
				float temperature2 = content.temperature;
				float num4 = ContactConductivePipeBridge.CalculateMaxWattsTransfered(temperature, num, temperature2, element.thermalConductivity);
				float finalContentTemperature = ContactConductivePipeBridge.GetFinalContentTemperature(ContactConductivePipeBridge.GetKilloJoulesTransfered(num4, dt, temperature, num3, temperature2, num2), temperature, num3, temperature2, num2);
				float finalBuildingTemperature = ContactConductivePipeBridge.GetFinalBuildingTemperature(temperature2, finalContentTemperature, num2, temperature, num3);
				float num5 = Mathf.Sign(num4) * Mathf.Abs(finalBuildingTemperature - temperature) * num3;
				if ((finalBuildingTemperature >= 0f && finalBuildingTemperature <= 10000f) & (finalContentTemperature >= 0f && finalContentTemperature <= 10000f))
				{
					GameComps.StructureTemperatures.ProduceEnergy(base.smi.structureHandle, num5, BUILDING.STATUSITEMS.OPERATINGENERGY.PIPECONTENTS_TRANSFER, Time.time);
					return finalContentTemperature;
				}
			}
			return 0f;
		}

		// Token: 0x04007055 RID: 28757
		public ConduitType type = ConduitType.Liquid;

		// Token: 0x04007056 RID: 28758
		public HandleVector<int>.Handle structureHandle;

		// Token: 0x04007057 RID: 28759
		public int inputCell = -1;

		// Token: 0x04007058 RID: 28760
		public int outputCell = -1;

		// Token: 0x04007059 RID: 28761
		[MyCmpGet]
		public Building building;
	}
}
