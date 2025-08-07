using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000758 RID: 1880
public class LiquidCooledRefinery : ComplexFabricator
{
	// Token: 0x06002FDD RID: 12253 RVA: 0x00112135 File Offset: 0x00110335
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LiquidCooledRefinery>(-1697596308, LiquidCooledRefinery.OnStorageChangeDelegate);
	}

	// Token: 0x06002FDE RID: 12254 RVA: 0x00112150 File Offset: 0x00110350
	protected override void OnSpawn()
	{
		base.OnSpawn();
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		this.meter_coolant = new MeterController(component, "meter_target", "meter_coolant", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Vector3.zero, null);
		this.meter_metal = new MeterController(component, "meter_target_metal", "meter_metal", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Vector3.zero, null);
		this.meter_metal.SetPositionPercent(1f);
		this.smi = new LiquidCooledRefinery.StatesInstance(this);
		this.smi.StartSM();
		Game.Instance.liquidConduitFlow.AddConduitUpdater(new Action<float>(this.OnConduitUpdate), ConduitFlowPriority.Default);
		Building component2 = base.GetComponent<Building>();
		this.outputCell = component2.GetUtilityOutputCell();
		this.workable.OnWorkTickActions = delegate(WorkerBase worker, float dt)
		{
			float percentComplete = this.workable.GetPercentComplete();
			this.meter_metal.SetPositionPercent(percentComplete);
		};
	}

	// Token: 0x06002FDF RID: 12255 RVA: 0x00112215 File Offset: 0x00110415
	protected override void OnCleanUp()
	{
		Game.Instance.liquidConduitFlow.RemoveConduitUpdater(new Action<float>(this.OnConduitUpdate));
		base.OnCleanUp();
	}

	// Token: 0x06002FE0 RID: 12256 RVA: 0x00112238 File Offset: 0x00110438
	private void OnConduitUpdate(float dt)
	{
		bool flag = Game.Instance.liquidConduitFlow.GetContents(this.outputCell).mass > 0f;
		this.smi.sm.outputBlocked.Set(flag, this.smi, false);
		this.operational.SetFlag(LiquidCooledRefinery.coolantOutputPipeEmpty, !flag);
	}

	// Token: 0x06002FE1 RID: 12257 RVA: 0x0011229C File Offset: 0x0011049C
	public bool HasEnoughCoolant()
	{
		return this.inStorage.GetAmountAvailable(this.coolantTag) + this.buildStorage.GetAmountAvailable(this.coolantTag) >= this.minCoolantMass;
	}

	// Token: 0x06002FE2 RID: 12258 RVA: 0x001122CC File Offset: 0x001104CC
	private void OnStorageChange(object data)
	{
		float amountAvailable = this.inStorage.GetAmountAvailable(this.coolantTag);
		float capacityKG = this.conduitConsumer.capacityKG;
		float num = Mathf.Clamp01(amountAvailable / capacityKG);
		if (this.meter_coolant != null)
		{
			this.meter_coolant.SetPositionPercent(num);
		}
	}

	// Token: 0x06002FE3 RID: 12259 RVA: 0x00112312 File Offset: 0x00110512
	protected override bool HasIngredients(ComplexRecipe recipe, Storage storage)
	{
		return storage.GetAmountAvailable(this.coolantTag) >= this.minCoolantMass && base.HasIngredients(recipe, storage);
	}

	// Token: 0x06002FE4 RID: 12260 RVA: 0x00112334 File Offset: 0x00110534
	protected override void TransferCurrentRecipeIngredientsForBuild()
	{
		base.TransferCurrentRecipeIngredientsForBuild();
		float num = this.minCoolantMass;
		while (this.buildStorage.GetAmountAvailable(this.coolantTag) < this.minCoolantMass && this.inStorage.GetAmountAvailable(this.coolantTag) > 0f && num > 0f)
		{
			float num2 = this.inStorage.Transfer(this.buildStorage, this.coolantTag, num, false, true);
			num -= num2;
		}
	}

	// Token: 0x06002FE5 RID: 12261 RVA: 0x001123A8 File Offset: 0x001105A8
	protected override List<GameObject> SpawnOrderProduct(ComplexRecipe recipe)
	{
		List<GameObject> list = base.SpawnOrderProduct(recipe);
		PrimaryElement component = list[0].GetComponent<PrimaryElement>();
		component.Temperature = this.outputTemperature;
		float num = GameUtil.CalculateEnergyDeltaForElementChange(component.Element.specificHeatCapacity, component.Mass, component.Element.highTemp, this.outputTemperature);
		ListPool<GameObject, LiquidCooledRefinery>.PooledList pooledList = ListPool<GameObject, LiquidCooledRefinery>.Allocate();
		this.buildStorage.Find(this.coolantTag, pooledList);
		float num2 = 0f;
		foreach (GameObject gameObject in pooledList)
		{
			PrimaryElement component2 = gameObject.GetComponent<PrimaryElement>();
			if (component2.Mass != 0f)
			{
				num2 += component2.Mass * component2.Element.specificHeatCapacity;
			}
		}
		foreach (GameObject gameObject2 in pooledList)
		{
			PrimaryElement component3 = gameObject2.GetComponent<PrimaryElement>();
			if (component3.Mass != 0f)
			{
				float num3 = component3.Mass * component3.Element.specificHeatCapacity / num2;
				float num4 = -num * num3 * this.thermalFudge;
				float num5 = GameUtil.CalculateTemperatureChange(component3.Element.specificHeatCapacity, component3.Mass, num4);
				float temperature = component3.Temperature;
				component3.Temperature += num5;
			}
		}
		this.buildStorage.Transfer(this.outStorage, this.coolantTag, float.MaxValue, false, true);
		pooledList.Recycle();
		return list;
	}

	// Token: 0x06002FE6 RID: 12262 RVA: 0x00112554 File Offset: 0x00110754
	public override List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> descriptors = base.GetDescriptors(go);
		descriptors.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.COOLANT, this.coolantTag.ProperName(), GameUtil.GetFormattedMass(this.minCoolantMass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.COOLANT, this.coolantTag.ProperName(), GameUtil.GetFormattedMass(this.minCoolantMass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), Descriptor.DescriptorType.Requirement, false));
		return descriptors;
	}

	// Token: 0x06002FE7 RID: 12263 RVA: 0x001125D0 File Offset: 0x001107D0
	public override List<Descriptor> AdditionalEffectsForRecipe(ComplexRecipe recipe)
	{
		List<Descriptor> list = base.AdditionalEffectsForRecipe(recipe);
		PrimaryElement component = Assets.GetPrefab(recipe.results[0].material).GetComponent<PrimaryElement>();
		PrimaryElement primaryElement = this.inStorage.FindFirstWithMass(this.coolantTag, 0f);
		string text = UI.BUILDINGEFFECTS.TOOLTIPS.REFINEMENT_ENERGY_HAS_COOLANT;
		if (primaryElement == null)
		{
			primaryElement = Assets.GetPrefab(GameTags.Water).GetComponent<PrimaryElement>();
			text = UI.BUILDINGEFFECTS.TOOLTIPS.REFINEMENT_ENERGY_NO_COOLANT;
		}
		float num = -GameUtil.CalculateEnergyDeltaForElementChange(component.Element.specificHeatCapacity, recipe.results[0].amount, component.Element.highTemp, this.outputTemperature);
		float num2 = GameUtil.CalculateTemperatureChange(primaryElement.Element.specificHeatCapacity, this.minCoolantMass, num * this.thermalFudge);
		list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.REFINEMENT_ENERGY, GameUtil.GetFormattedJoules(num, "F1", GameUtil.TimeSlice.None)), string.Format(text, GameUtil.GetFormattedJoules(num, "F1", GameUtil.TimeSlice.None), primaryElement.GetProperName(), GameUtil.GetFormattedTemperature(num2, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Relative, true, false)), Descriptor.DescriptorType.Effect, false));
		return list;
	}

	// Token: 0x04001C8A RID: 7306
	[MyCmpReq]
	private ConduitConsumer conduitConsumer;

	// Token: 0x04001C8B RID: 7307
	public static readonly Operational.Flag coolantOutputPipeEmpty = new Operational.Flag("coolantOutputPipeEmpty", Operational.Flag.Type.Requirement);

	// Token: 0x04001C8C RID: 7308
	private int outputCell;

	// Token: 0x04001C8D RID: 7309
	public Tag coolantTag;

	// Token: 0x04001C8E RID: 7310
	public float minCoolantMass = 100f;

	// Token: 0x04001C8F RID: 7311
	public float thermalFudge = 0.8f;

	// Token: 0x04001C90 RID: 7312
	public float outputTemperature = 313.15f;

	// Token: 0x04001C91 RID: 7313
	private MeterController meter_coolant;

	// Token: 0x04001C92 RID: 7314
	private MeterController meter_metal;

	// Token: 0x04001C93 RID: 7315
	private LiquidCooledRefinery.StatesInstance smi;

	// Token: 0x04001C94 RID: 7316
	private static readonly EventSystem.IntraObjectHandler<LiquidCooledRefinery> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<LiquidCooledRefinery>(delegate(LiquidCooledRefinery component, object data)
	{
		component.OnStorageChange(data);
	});

	// Token: 0x02001636 RID: 5686
	public class StatesInstance : GameStateMachine<LiquidCooledRefinery.States, LiquidCooledRefinery.StatesInstance, LiquidCooledRefinery, object>.GameInstance
	{
		// Token: 0x06009442 RID: 37954 RVA: 0x0036F3B3 File Offset: 0x0036D5B3
		public StatesInstance(LiquidCooledRefinery master)
			: base(master)
		{
		}
	}

	// Token: 0x02001637 RID: 5687
	public class States : GameStateMachine<LiquidCooledRefinery.States, LiquidCooledRefinery.StatesInstance, LiquidCooledRefinery>
	{
		// Token: 0x06009443 RID: 37955 RVA: 0x0036F3BC File Offset: 0x0036D5BC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			if (LiquidCooledRefinery.States.waitingForCoolantStatus == null)
			{
				LiquidCooledRefinery.States.waitingForCoolantStatus = new StatusItem("waitingForCoolantStatus", BUILDING.STATUSITEMS.ENOUGH_COOLANT.NAME, BUILDING.STATUSITEMS.ENOUGH_COOLANT.TOOLTIP, "status_item_no_liquid_to_pump", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, 129022, true, null);
				LiquidCooledRefinery.States.waitingForCoolantStatus.resolveStringCallback = delegate(string str, object obj)
				{
					LiquidCooledRefinery liquidCooledRefinery = (LiquidCooledRefinery)obj;
					return string.Format(str, liquidCooledRefinery.coolantTag.ProperName(), GameUtil.GetFormattedMass(liquidCooledRefinery.minCoolantMass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				};
			}
			default_state = this.waiting_for_coolant;
			this.waiting_for_coolant.ToggleStatusItem(LiquidCooledRefinery.States.waitingForCoolantStatus, (LiquidCooledRefinery.StatesInstance smi) => smi.master).EventTransition(GameHashes.OnStorageChange, this.ready, (LiquidCooledRefinery.StatesInstance smi) => smi.master.HasEnoughCoolant()).ParamTransition<bool>(this.outputBlocked, this.output_blocked, GameStateMachine<LiquidCooledRefinery.States, LiquidCooledRefinery.StatesInstance, LiquidCooledRefinery, object>.IsTrue);
			this.ready.EventTransition(GameHashes.OnStorageChange, this.waiting_for_coolant, (LiquidCooledRefinery.StatesInstance smi) => !smi.master.HasEnoughCoolant()).ParamTransition<bool>(this.outputBlocked, this.output_blocked, GameStateMachine<LiquidCooledRefinery.States, LiquidCooledRefinery.StatesInstance, LiquidCooledRefinery, object>.IsTrue).Enter(delegate(LiquidCooledRefinery.StatesInstance smi)
			{
				smi.master.SetQueueDirty();
			});
			this.output_blocked.ToggleStatusItem(Db.Get().BuildingStatusItems.OutputPipeFull, null).ParamTransition<bool>(this.outputBlocked, this.waiting_for_coolant, GameStateMachine<LiquidCooledRefinery.States, LiquidCooledRefinery.StatesInstance, LiquidCooledRefinery, object>.IsFalse);
		}

		// Token: 0x04007229 RID: 29225
		public static StatusItem waitingForCoolantStatus;

		// Token: 0x0400722A RID: 29226
		public StateMachine<LiquidCooledRefinery.States, LiquidCooledRefinery.StatesInstance, LiquidCooledRefinery, object>.BoolParameter outputBlocked;

		// Token: 0x0400722B RID: 29227
		public GameStateMachine<LiquidCooledRefinery.States, LiquidCooledRefinery.StatesInstance, LiquidCooledRefinery, object>.State waiting_for_coolant;

		// Token: 0x0400722C RID: 29228
		public GameStateMachine<LiquidCooledRefinery.States, LiquidCooledRefinery.StatesInstance, LiquidCooledRefinery, object>.State ready;

		// Token: 0x0400722D RID: 29229
		public GameStateMachine<LiquidCooledRefinery.States, LiquidCooledRefinery.StatesInstance, LiquidCooledRefinery, object>.State output_blocked;
	}
}
