using System;
using System.Collections.Generic;
using System.Diagnostics;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020008E7 RID: 2279
[SerializationConfig(MemberSerialization.OptIn)]
public class EnergyGenerator : Generator, IGameObjectEffectDescriptor, ISingleSliderControl, ISliderControl
{
	// Token: 0x17000481 RID: 1153
	// (get) Token: 0x06003F8E RID: 16270 RVA: 0x00165107 File Offset: 0x00163307
	public string SliderTitleKey
	{
		get
		{
			return "STRINGS.UI.UISIDESCREENS.MANUALDELIVERYGENERATORSIDESCREEN.TITLE";
		}
	}

	// Token: 0x17000482 RID: 1154
	// (get) Token: 0x06003F8F RID: 16271 RVA: 0x0016510E File Offset: 0x0016330E
	public string SliderUnits
	{
		get
		{
			return UI.UNITSUFFIXES.PERCENT;
		}
	}

	// Token: 0x06003F90 RID: 16272 RVA: 0x0016511A File Offset: 0x0016331A
	public int SliderDecimalPlaces(int index)
	{
		return 0;
	}

	// Token: 0x06003F91 RID: 16273 RVA: 0x0016511D File Offset: 0x0016331D
	public float GetSliderMin(int index)
	{
		return 0f;
	}

	// Token: 0x06003F92 RID: 16274 RVA: 0x00165124 File Offset: 0x00163324
	public float GetSliderMax(int index)
	{
		return 100f;
	}

	// Token: 0x06003F93 RID: 16275 RVA: 0x0016512B File Offset: 0x0016332B
	public float GetSliderValue(int index)
	{
		return this.batteryRefillPercent * 100f;
	}

	// Token: 0x06003F94 RID: 16276 RVA: 0x00165139 File Offset: 0x00163339
	public void SetSliderValue(float value, int index)
	{
		this.batteryRefillPercent = value / 100f;
	}

	// Token: 0x06003F95 RID: 16277 RVA: 0x00165148 File Offset: 0x00163348
	string ISliderControl.GetSliderTooltip(int index)
	{
		ManualDeliveryKG component = base.GetComponent<ManualDeliveryKG>();
		return string.Format(Strings.Get("STRINGS.UI.UISIDESCREENS.MANUALDELIVERYGENERATORSIDESCREEN.TOOLTIP"), component.RequestedItemTag.ProperName(), this.batteryRefillPercent * 100f);
	}

	// Token: 0x06003F96 RID: 16278 RVA: 0x0016518C File Offset: 0x0016338C
	public string GetSliderTooltipKey(int index)
	{
		return "STRINGS.UI.UISIDESCREENS.MANUALDELIVERYGENERATORSIDESCREEN.TOOLTIP";
	}

	// Token: 0x06003F97 RID: 16279 RVA: 0x00165194 File Offset: 0x00163394
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		EnergyGenerator.EnsureStatusItemAvailable();
		base.Subscribe<EnergyGenerator>(824508782, EnergyGenerator.OnActiveChangedDelegate);
		if (!this.ignoreBatteryRefillPercent)
		{
			base.gameObject.AddOrGet<CopyBuildingSettings>();
			base.Subscribe<EnergyGenerator>(-905833192, EnergyGenerator.OnCopySettingsDelegate);
		}
	}

	// Token: 0x06003F98 RID: 16280 RVA: 0x001651E4 File Offset: 0x001633E4
	private void OnCopySettings(object data)
	{
		EnergyGenerator component = ((GameObject)data).GetComponent<EnergyGenerator>();
		if (component != null)
		{
			this.batteryRefillPercent = component.batteryRefillPercent;
		}
	}

	// Token: 0x06003F99 RID: 16281 RVA: 0x00165214 File Offset: 0x00163414
	protected void OnActiveChanged(object data)
	{
		StatusItem statusItem = (((Operational)data).IsActive ? Db.Get().BuildingStatusItems.Wattage : Db.Get().BuildingStatusItems.GeneratorOffline);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, this);
	}

	// Token: 0x06003F9A RID: 16282 RVA: 0x0016526C File Offset: 0x0016346C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.hasMeter)
		{
			this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", this.meterOffset, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_fill", "meter_frame", "meter_OL" });
		}
	}

	// Token: 0x06003F9B RID: 16283 RVA: 0x001652D0 File Offset: 0x001634D0
	private bool IsConvertible(float dt)
	{
		bool flag = true;
		foreach (EnergyGenerator.InputItem inputItem in this.formula.inputs)
		{
			float massAvailable = this.storage.GetMassAvailable(inputItem.tag);
			float num = inputItem.consumptionRate * dt;
			flag = flag && massAvailable >= num;
			if (!flag)
			{
				break;
			}
		}
		return flag;
	}

	// Token: 0x06003F9C RID: 16284 RVA: 0x00165334 File Offset: 0x00163534
	public override void EnergySim200ms(float dt)
	{
		base.EnergySim200ms(dt);
		if (this.hasMeter)
		{
			EnergyGenerator.InputItem inputItem = this.formula.inputs[0];
			float num = this.storage.GetMassAvailable(inputItem.tag) / inputItem.maxStoredMass;
			this.meter.SetPositionPercent(num);
		}
		ushort circuitID = base.CircuitID;
		this.operational.SetFlag(Generator.wireConnectedFlag, circuitID != ushort.MaxValue);
		bool flag = false;
		if (this.operational.IsOperational)
		{
			bool flag2 = false;
			List<Battery> batteriesOnCircuit = Game.Instance.circuitManager.GetBatteriesOnCircuit(circuitID);
			if (!this.ignoreBatteryRefillPercent && batteriesOnCircuit.Count > 0)
			{
				using (List<Battery>.Enumerator enumerator = batteriesOnCircuit.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Battery battery = enumerator.Current;
						if (this.batteryRefillPercent <= 0f && battery.PercentFull <= 0f)
						{
							flag2 = true;
							break;
						}
						if (battery.PercentFull < this.batteryRefillPercent)
						{
							flag2 = true;
							break;
						}
					}
					goto IL_0105;
				}
			}
			flag2 = true;
			IL_0105:
			if (!this.ignoreBatteryRefillPercent)
			{
				this.selectable.ToggleStatusItem(EnergyGenerator.batteriesSufficientlyFull, !flag2, null);
			}
			if (this.delivery != null)
			{
				this.delivery.Pause(!flag2, "Circuit has sufficient energy");
			}
			if (this.formula.inputs != null)
			{
				bool flag3 = this.IsConvertible(dt);
				this.selectable.ToggleStatusItem(Db.Get().BuildingStatusItems.NeedResourceMass, !flag3, this.formula);
				if (flag3)
				{
					foreach (EnergyGenerator.InputItem inputItem2 in this.formula.inputs)
					{
						float num2 = inputItem2.consumptionRate * dt;
						this.storage.ConsumeIgnoringDisease(inputItem2.tag, num2);
					}
					PrimaryElement component = base.GetComponent<PrimaryElement>();
					foreach (EnergyGenerator.OutputItem outputItem in this.formula.outputs)
					{
						this.Emit(outputItem, dt, component);
					}
					base.GenerateJoules(base.WattageRating * dt, false);
					this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Power, Db.Get().BuildingStatusItems.Wattage, this);
					flag = true;
				}
			}
		}
		this.operational.SetActive(flag, false);
	}

	// Token: 0x06003F9D RID: 16285 RVA: 0x001655B4 File Offset: 0x001637B4
	public List<Descriptor> RequirementDescriptors()
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.formula.inputs == null || this.formula.inputs.Length == 0)
		{
			return list;
		}
		for (int i = 0; i < this.formula.inputs.Length; i++)
		{
			EnergyGenerator.InputItem inputItem = this.formula.inputs[i];
			string text = inputItem.tag.ProperName();
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTCONSUMED, text, GameUtil.GetFormattedMass(inputItem.consumptionRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTCONSUMED, text, GameUtil.GetFormattedMass(inputItem.consumptionRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), Descriptor.DescriptorType.Requirement);
			list.Add(descriptor);
		}
		return list;
	}

	// Token: 0x06003F9E RID: 16286 RVA: 0x00165680 File Offset: 0x00163880
	public List<Descriptor> EffectDescriptors()
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.formula.outputs == null || this.formula.outputs.Length == 0)
		{
			return list;
		}
		for (int i = 0; i < this.formula.outputs.Length; i++)
		{
			EnergyGenerator.OutputItem outputItem = this.formula.outputs[i];
			string text = ElementLoader.FindElementByHash(outputItem.element).tag.ProperName();
			Descriptor descriptor = default(Descriptor);
			if (outputItem.minTemperature > 0f)
			{
				descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTEMITTED_MINORENTITYTEMP, text, GameUtil.GetFormattedMass(outputItem.creationRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), GameUtil.GetFormattedTemperature(outputItem.minTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTEMITTED_MINORENTITYTEMP, text, GameUtil.GetFormattedMass(outputItem.creationRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), GameUtil.GetFormattedTemperature(outputItem.minTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), Descriptor.DescriptorType.Effect);
			}
			else
			{
				descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTEMITTED_ENTITYTEMP, text, GameUtil.GetFormattedMass(outputItem.creationRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTEMITTED_ENTITYTEMP, text, GameUtil.GetFormattedMass(outputItem.creationRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), Descriptor.DescriptorType.Effect);
			}
			list.Add(descriptor);
		}
		return list;
	}

	// Token: 0x06003F9F RID: 16287 RVA: 0x001657D0 File Offset: 0x001639D0
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		foreach (Descriptor descriptor in this.RequirementDescriptors())
		{
			list.Add(descriptor);
		}
		foreach (Descriptor descriptor2 in this.EffectDescriptors())
		{
			list.Add(descriptor2);
		}
		return list;
	}

	// Token: 0x17000483 RID: 1155
	// (get) Token: 0x06003FA0 RID: 16288 RVA: 0x0016586C File Offset: 0x00163A6C
	public static StatusItem BatteriesSufficientlyFull
	{
		get
		{
			return EnergyGenerator.batteriesSufficientlyFull;
		}
	}

	// Token: 0x06003FA1 RID: 16289 RVA: 0x00165874 File Offset: 0x00163A74
	public static void EnsureStatusItemAvailable()
	{
		if (EnergyGenerator.batteriesSufficientlyFull == null)
		{
			EnergyGenerator.batteriesSufficientlyFull = new StatusItem("BatteriesSufficientlyFull", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
		}
	}

	// Token: 0x06003FA2 RID: 16290 RVA: 0x001658B0 File Offset: 0x00163AB0
	public static EnergyGenerator.Formula CreateSimpleFormula(Tag input_element, float input_mass_rate, float max_stored_input_mass, SimHashes output_element = SimHashes.Void, float output_mass_rate = 0f, bool store_output_mass = true, CellOffset output_offset = default(CellOffset), float min_output_temperature = 0f)
	{
		EnergyGenerator.Formula formula = default(EnergyGenerator.Formula);
		formula.inputs = new EnergyGenerator.InputItem[]
		{
			new EnergyGenerator.InputItem(input_element, input_mass_rate, max_stored_input_mass)
		};
		if (output_element != SimHashes.Void)
		{
			formula.outputs = new EnergyGenerator.OutputItem[]
			{
				new EnergyGenerator.OutputItem(output_element, output_mass_rate, store_output_mass, output_offset, min_output_temperature)
			};
		}
		else
		{
			formula.outputs = null;
		}
		return formula;
	}

	// Token: 0x06003FA3 RID: 16291 RVA: 0x00165918 File Offset: 0x00163B18
	private void Emit(EnergyGenerator.OutputItem output, float dt, PrimaryElement root_pe)
	{
		Element element = ElementLoader.FindElementByHash(output.element);
		float num = output.creationRate * dt;
		if (output.store)
		{
			if (element.IsGas)
			{
				this.storage.AddGasChunk(output.element, num, root_pe.Temperature, byte.MaxValue, 0, true, true);
				return;
			}
			if (element.IsLiquid)
			{
				this.storage.AddLiquid(output.element, num, root_pe.Temperature, byte.MaxValue, 0, true, true);
				return;
			}
			GameObject gameObject = element.substance.SpawnResource(base.transform.GetPosition(), num, root_pe.Temperature, byte.MaxValue, 0, false, false, false);
			this.storage.Store(gameObject, true, false, true, false);
			return;
		}
		else
		{
			int num2 = Grid.OffsetCell(Grid.PosToCell(base.transform.GetPosition()), output.emitOffset);
			float num3 = Mathf.Max(root_pe.Temperature, output.minTemperature);
			if (element.IsGas)
			{
				SimMessages.ModifyMass(num2, num, byte.MaxValue, 0, CellEventLogger.Instance.EnergyGeneratorModifyMass, num3, output.element);
				return;
			}
			if (element.IsLiquid)
			{
				ushort elementIndex = ElementLoader.GetElementIndex(output.element);
				FallingWater.instance.AddParticle(num2, elementIndex, num, num3, byte.MaxValue, 0, true, false, false, false);
				return;
			}
			element.substance.SpawnResource(Grid.CellToPosCCC(num2, Grid.SceneLayer.Front), num, num3, byte.MaxValue, 0, true, false, false);
			return;
		}
	}

	// Token: 0x04002778 RID: 10104
	[MyCmpAdd]
	private Storage storage;

	// Token: 0x04002779 RID: 10105
	[MyCmpGet]
	private ManualDeliveryKG delivery;

	// Token: 0x0400277A RID: 10106
	[SerializeField]
	[Serialize]
	private float batteryRefillPercent = 0.5f;

	// Token: 0x0400277B RID: 10107
	public bool ignoreBatteryRefillPercent;

	// Token: 0x0400277C RID: 10108
	public bool hasMeter = true;

	// Token: 0x0400277D RID: 10109
	private static StatusItem batteriesSufficientlyFull;

	// Token: 0x0400277E RID: 10110
	public Meter.Offset meterOffset;

	// Token: 0x0400277F RID: 10111
	[SerializeField]
	public EnergyGenerator.Formula formula;

	// Token: 0x04002780 RID: 10112
	private MeterController meter;

	// Token: 0x04002781 RID: 10113
	private static readonly EventSystem.IntraObjectHandler<EnergyGenerator> OnActiveChangedDelegate = new EventSystem.IntraObjectHandler<EnergyGenerator>(delegate(EnergyGenerator component, object data)
	{
		component.OnActiveChanged(data);
	});

	// Token: 0x04002782 RID: 10114
	private static readonly EventSystem.IntraObjectHandler<EnergyGenerator> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<EnergyGenerator>(delegate(EnergyGenerator component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x02001892 RID: 6290
	[DebuggerDisplay("{tag} -{consumptionRate} kg/s")]
	[Serializable]
	public struct InputItem
	{
		// Token: 0x06009D1C RID: 40220 RVA: 0x003922AC File Offset: 0x003904AC
		public InputItem(Tag tag, float consumption_rate, float max_stored_mass)
		{
			this.tag = tag;
			this.consumptionRate = consumption_rate;
			this.maxStoredMass = max_stored_mass;
		}

		// Token: 0x04007950 RID: 31056
		public Tag tag;

		// Token: 0x04007951 RID: 31057
		public float consumptionRate;

		// Token: 0x04007952 RID: 31058
		public float maxStoredMass;
	}

	// Token: 0x02001893 RID: 6291
	[DebuggerDisplay("{element} {creationRate} kg/s")]
	[Serializable]
	public struct OutputItem
	{
		// Token: 0x06009D1D RID: 40221 RVA: 0x003922C3 File Offset: 0x003904C3
		public OutputItem(SimHashes element, float creation_rate, bool store, float min_temperature = 0f)
		{
			this = new EnergyGenerator.OutputItem(element, creation_rate, store, CellOffset.none, min_temperature);
		}

		// Token: 0x06009D1E RID: 40222 RVA: 0x003922D5 File Offset: 0x003904D5
		public OutputItem(SimHashes element, float creation_rate, bool store, CellOffset emit_offset, float min_temperature = 0f)
		{
			this.element = element;
			this.creationRate = creation_rate;
			this.store = store;
			this.emitOffset = emit_offset;
			this.minTemperature = min_temperature;
		}

		// Token: 0x04007953 RID: 31059
		public SimHashes element;

		// Token: 0x04007954 RID: 31060
		public float creationRate;

		// Token: 0x04007955 RID: 31061
		public bool store;

		// Token: 0x04007956 RID: 31062
		public CellOffset emitOffset;

		// Token: 0x04007957 RID: 31063
		public float minTemperature;
	}

	// Token: 0x02001894 RID: 6292
	[Serializable]
	public struct Formula
	{
		// Token: 0x04007958 RID: 31064
		public EnergyGenerator.InputItem[] inputs;

		// Token: 0x04007959 RID: 31065
		public EnergyGenerator.OutputItem[] outputs;

		// Token: 0x0400795A RID: 31066
		public Tag meterTag;
	}
}
