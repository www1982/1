using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020007B5 RID: 1973
public class RefrigeratorController : GameStateMachine<RefrigeratorController, RefrigeratorController.StatesInstance, IStateMachineTarget, RefrigeratorController.Def>
{
	// Token: 0x06003488 RID: 13448 RVA: 0x001263DC File Offset: 0x001245DC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.inoperational;
		this.inoperational.EventTransition(GameHashes.OperationalChanged, this.operational, new StateMachine<RefrigeratorController, RefrigeratorController.StatesInstance, IStateMachineTarget, RefrigeratorController.Def>.Transition.ConditionCallback(this.IsOperational));
		this.operational.DefaultState(this.operational.steady).EventTransition(GameHashes.OperationalChanged, this.inoperational, GameStateMachine<RefrigeratorController, RefrigeratorController.StatesInstance, IStateMachineTarget, RefrigeratorController.Def>.Not(new StateMachine<RefrigeratorController, RefrigeratorController.StatesInstance, IStateMachineTarget, RefrigeratorController.Def>.Transition.ConditionCallback(this.IsOperational))).Enter(delegate(RefrigeratorController.StatesInstance smi)
		{
			smi.operational.SetActive(true, false);
		})
			.Exit(delegate(RefrigeratorController.StatesInstance smi)
			{
				smi.operational.SetActive(false, false);
			});
		this.operational.cooling.Update("Cooling exhaust", delegate(RefrigeratorController.StatesInstance smi, float dt)
		{
			smi.ApplyCoolingExhaust(dt);
		}, UpdateRate.SIM_200ms, true).UpdateTransition(this.operational.steady, new Func<RefrigeratorController.StatesInstance, float, bool>(this.AllFoodCool), UpdateRate.SIM_4000ms, true).ToggleStatusItem(Db.Get().BuildingStatusItems.FridgeCooling, (RefrigeratorController.StatesInstance smi) => smi, Db.Get().StatusItemCategories.Main);
		this.operational.steady.Update("Cooling exhaust", delegate(RefrigeratorController.StatesInstance smi, float dt)
		{
			smi.ApplySteadyExhaust(dt);
		}, UpdateRate.SIM_200ms, true).UpdateTransition(this.operational.cooling, new Func<RefrigeratorController.StatesInstance, float, bool>(this.AnyWarmFood), UpdateRate.SIM_4000ms, true).ToggleStatusItem(Db.Get().BuildingStatusItems.FridgeSteady, (RefrigeratorController.StatesInstance smi) => smi, Db.Get().StatusItemCategories.Main)
			.Enter(delegate(RefrigeratorController.StatesInstance smi)
			{
				smi.SetEnergySaver(true);
			})
			.Exit(delegate(RefrigeratorController.StatesInstance smi)
			{
				smi.SetEnergySaver(false);
			});
	}

	// Token: 0x06003489 RID: 13449 RVA: 0x0012660C File Offset: 0x0012480C
	private bool AllFoodCool(RefrigeratorController.StatesInstance smi, float dt)
	{
		foreach (GameObject gameObject in smi.storage.items)
		{
			if (!(gameObject == null))
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (!(component == null) && component.Mass >= 0.01f && component.Temperature >= smi.def.simulatedInternalTemperature + smi.def.activeCoolingStopBuffer)
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x0600348A RID: 13450 RVA: 0x001266AC File Offset: 0x001248AC
	private bool AnyWarmFood(RefrigeratorController.StatesInstance smi, float dt)
	{
		foreach (GameObject gameObject in smi.storage.items)
		{
			if (!(gameObject == null))
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (!(component == null) && component.Mass >= 0.01f && component.Temperature >= smi.def.simulatedInternalTemperature + smi.def.activeCoolingStartBuffer)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600348B RID: 13451 RVA: 0x0012674C File Offset: 0x0012494C
	private bool IsOperational(RefrigeratorController.StatesInstance smi)
	{
		return smi.operational.IsOperational;
	}

	// Token: 0x04001FC3 RID: 8131
	public GameStateMachine<RefrigeratorController, RefrigeratorController.StatesInstance, IStateMachineTarget, RefrigeratorController.Def>.State inoperational;

	// Token: 0x04001FC4 RID: 8132
	public RefrigeratorController.OperationalStates operational;

	// Token: 0x020016D8 RID: 5848
	public class Def : StateMachine.BaseDef, IGameObjectEffectDescriptor
	{
		// Token: 0x060096D2 RID: 38610 RVA: 0x0037A1D4 File Offset: 0x003783D4
		public List<Descriptor> GetDescriptors(GameObject go)
		{
			List<Descriptor> list = new List<Descriptor>();
			list.AddRange(SimulatedTemperatureAdjuster.GetDescriptors(this.simulatedInternalTemperature));
			Descriptor descriptor = default(Descriptor);
			string formattedHeatEnergy = GameUtil.GetFormattedHeatEnergy(this.coolingHeatKW * 1000f, GameUtil.HeatEnergyFormatterUnit.Automatic);
			descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.HEATGENERATED, formattedHeatEnergy), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.HEATGENERATED, formattedHeatEnergy), Descriptor.DescriptorType.Effect);
			list.Add(descriptor);
			return list;
		}

		// Token: 0x040073F9 RID: 29689
		public float activeCoolingStartBuffer = 2f;

		// Token: 0x040073FA RID: 29690
		public float activeCoolingStopBuffer = 0.1f;

		// Token: 0x040073FB RID: 29691
		public float simulatedInternalTemperature = 274.15f;

		// Token: 0x040073FC RID: 29692
		public float simulatedInternalHeatCapacity = 400f;

		// Token: 0x040073FD RID: 29693
		public float simulatedThermalConductivity = 1000f;

		// Token: 0x040073FE RID: 29694
		public float powerSaverEnergyUsage;

		// Token: 0x040073FF RID: 29695
		public float coolingHeatKW;

		// Token: 0x04007400 RID: 29696
		public float steadyHeatKW;
	}

	// Token: 0x020016D9 RID: 5849
	public class OperationalStates : GameStateMachine<RefrigeratorController, RefrigeratorController.StatesInstance, IStateMachineTarget, RefrigeratorController.Def>.State
	{
		// Token: 0x04007401 RID: 29697
		public GameStateMachine<RefrigeratorController, RefrigeratorController.StatesInstance, IStateMachineTarget, RefrigeratorController.Def>.State cooling;

		// Token: 0x04007402 RID: 29698
		public GameStateMachine<RefrigeratorController, RefrigeratorController.StatesInstance, IStateMachineTarget, RefrigeratorController.Def>.State steady;
	}

	// Token: 0x020016DA RID: 5850
	public class StatesInstance : GameStateMachine<RefrigeratorController, RefrigeratorController.StatesInstance, IStateMachineTarget, RefrigeratorController.Def>.GameInstance
	{
		// Token: 0x060096D5 RID: 38613 RVA: 0x0037A288 File Offset: 0x00378488
		public StatesInstance(IStateMachineTarget master, RefrigeratorController.Def def)
			: base(master, def)
		{
			this.temperatureAdjuster = new SimulatedTemperatureAdjuster(def.simulatedInternalTemperature, def.simulatedInternalHeatCapacity, def.simulatedThermalConductivity, this.storage);
			this.structureTemperature = GameComps.StructureTemperatures.GetHandle(base.gameObject);
		}

		// Token: 0x060096D6 RID: 38614 RVA: 0x0037A2D6 File Offset: 0x003784D6
		protected override void OnCleanUp()
		{
			this.temperatureAdjuster.CleanUp();
			base.OnCleanUp();
		}

		// Token: 0x060096D7 RID: 38615 RVA: 0x0037A2E9 File Offset: 0x003784E9
		public float GetSaverPower()
		{
			return base.def.powerSaverEnergyUsage;
		}

		// Token: 0x060096D8 RID: 38616 RVA: 0x0037A2F6 File Offset: 0x003784F6
		public float GetNormalPower()
		{
			return base.GetComponent<EnergyConsumer>().WattsNeededWhenActive;
		}

		// Token: 0x060096D9 RID: 38617 RVA: 0x0037A304 File Offset: 0x00378504
		public void SetEnergySaver(bool energySaving)
		{
			EnergyConsumer component = base.GetComponent<EnergyConsumer>();
			if (energySaving)
			{
				component.BaseWattageRating = this.GetSaverPower();
				return;
			}
			component.BaseWattageRating = this.GetNormalPower();
		}

		// Token: 0x060096DA RID: 38618 RVA: 0x0037A334 File Offset: 0x00378534
		public void ApplyCoolingExhaust(float dt)
		{
			GameComps.StructureTemperatures.ProduceEnergy(this.structureTemperature, base.def.coolingHeatKW * dt, BUILDING.STATUSITEMS.OPERATINGENERGY.FOOD_TRANSFER, dt);
		}

		// Token: 0x060096DB RID: 38619 RVA: 0x0037A35E File Offset: 0x0037855E
		public void ApplySteadyExhaust(float dt)
		{
			GameComps.StructureTemperatures.ProduceEnergy(this.structureTemperature, base.def.steadyHeatKW * dt, BUILDING.STATUSITEMS.OPERATINGENERGY.FOOD_TRANSFER, dt);
		}

		// Token: 0x04007403 RID: 29699
		[MyCmpReq]
		public Operational operational;

		// Token: 0x04007404 RID: 29700
		[MyCmpReq]
		public Storage storage;

		// Token: 0x04007405 RID: 29701
		private HandleVector<int>.Handle structureTemperature;

		// Token: 0x04007406 RID: 29702
		private SimulatedTemperatureAdjuster temperatureAdjuster;
	}
}
