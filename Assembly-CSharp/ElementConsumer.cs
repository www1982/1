using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020008DD RID: 2269
[SkipSaveFileSerialization]
[SerializationConfig(MemberSerialization.OptIn)]
public class ElementConsumer : SimComponent, ISaveLoadable, IGameObjectEffectDescriptor
{
	// Token: 0x1400001A RID: 26
	// (add) Token: 0x06003EFC RID: 16124 RVA: 0x00162660 File Offset: 0x00160860
	// (remove) Token: 0x06003EFD RID: 16125 RVA: 0x00162698 File Offset: 0x00160898
	public event Action<Sim.ConsumedMassInfo> OnElementConsumed;

	// Token: 0x17000468 RID: 1128
	// (get) Token: 0x06003EFE RID: 16126 RVA: 0x001626CD File Offset: 0x001608CD
	public float AverageConsumeRate
	{
		get
		{
			return Game.Instance.accumulators.GetAverageRate(this.accumulator);
		}
	}

	// Token: 0x06003EFF RID: 16127 RVA: 0x001626E4 File Offset: 0x001608E4
	public static void ClearInstanceMap()
	{
		ElementConsumer.handleInstanceMap.Clear();
	}

	// Token: 0x06003F00 RID: 16128 RVA: 0x001626F0 File Offset: 0x001608F0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.accumulator = Game.Instance.accumulators.Add("Element", this);
		if (this.elementToConsume == SimHashes.Void)
		{
			throw new ArgumentException("No consumable elements specified");
		}
		if (!this.ignoreActiveChanged)
		{
			base.Subscribe<ElementConsumer>(824508782, ElementConsumer.OnActiveChangedDelegate);
		}
		if (this.capacityKG != float.PositiveInfinity)
		{
			this.hasAvailableCapacity = !this.IsStorageFull();
			base.Subscribe<ElementConsumer>(-1697596308, ElementConsumer.OnStorageChangeDelegate);
		}
	}

	// Token: 0x06003F01 RID: 16129 RVA: 0x0016277C File Offset: 0x0016097C
	protected override void OnCleanUp()
	{
		Game.Instance.accumulators.Remove(this.accumulator);
		base.OnCleanUp();
	}

	// Token: 0x06003F02 RID: 16130 RVA: 0x0016279A File Offset: 0x0016099A
	protected virtual bool IsActive()
	{
		return this.operational == null || this.operational.IsActive;
	}

	// Token: 0x06003F03 RID: 16131 RVA: 0x001627B8 File Offset: 0x001609B8
	public void EnableConsumption(bool enabled)
	{
		bool flag = this.consumptionEnabled;
		this.consumptionEnabled = enabled;
		if (!Sim.IsValidHandle(this.simHandle))
		{
			return;
		}
		if (enabled != flag)
		{
			this.UpdateSimData();
		}
	}

	// Token: 0x06003F04 RID: 16132 RVA: 0x001627EC File Offset: 0x001609EC
	private bool IsStorageFull()
	{
		PrimaryElement primaryElement = this.storage.FindPrimaryElement(this.elementToConsume);
		return primaryElement != null && primaryElement.Mass >= this.capacityKG;
	}

	// Token: 0x06003F05 RID: 16133 RVA: 0x00162827 File Offset: 0x00160A27
	public void RefreshConsumptionRate()
	{
		if (!Sim.IsValidHandle(this.simHandle))
		{
			return;
		}
		this.UpdateSimData();
	}

	// Token: 0x06003F06 RID: 16134 RVA: 0x00162840 File Offset: 0x00160A40
	private void UpdateSimData()
	{
		global::Debug.Assert(Sim.IsValidHandle(this.simHandle));
		int sampleCell = this.GetSampleCell();
		float num = ((this.consumptionEnabled && this.hasAvailableCapacity) ? this.consumptionRate : 0f);
		SimMessages.SetElementConsumerData(this.simHandle, sampleCell, num);
		this.UpdateStatusItem();
	}

	// Token: 0x06003F07 RID: 16135 RVA: 0x00162898 File Offset: 0x00160A98
	public static void AddMass(Sim.ConsumedMassInfo consumed_info)
	{
		if (!Sim.IsValidHandle(consumed_info.simHandle))
		{
			return;
		}
		ElementConsumer elementConsumer;
		if (ElementConsumer.handleInstanceMap.TryGetValue(consumed_info.simHandle, out elementConsumer))
		{
			elementConsumer.AddMassInternal(consumed_info);
		}
	}

	// Token: 0x06003F08 RID: 16136 RVA: 0x001628CE File Offset: 0x00160ACE
	private int GetSampleCell()
	{
		return Grid.PosToCell(base.transform.GetPosition() + this.sampleCellOffset);
	}

	// Token: 0x06003F09 RID: 16137 RVA: 0x001628EC File Offset: 0x00160AEC
	private void AddMassInternal(Sim.ConsumedMassInfo consumed_info)
	{
		if (consumed_info.mass > 0f)
		{
			if (this.storeOnConsume)
			{
				Element element = ElementLoader.elements[(int)consumed_info.removedElemIdx];
				if (this.elementToConsume == SimHashes.Vacuum || this.elementToConsume == element.id)
				{
					if (element.IsLiquid)
					{
						this.storage.AddLiquid(element.id, consumed_info.mass, consumed_info.temperature, consumed_info.diseaseIdx, consumed_info.diseaseCount, true, true);
					}
					else if (element.IsGas)
					{
						this.storage.AddGasChunk(element.id, consumed_info.mass, consumed_info.temperature, consumed_info.diseaseIdx, consumed_info.diseaseCount, true, true);
					}
				}
			}
			else
			{
				this.consumedTemperature = GameUtil.GetFinalTemperature(consumed_info.temperature, consumed_info.mass, this.consumedTemperature, this.consumedMass);
				this.consumedMass += consumed_info.mass;
				if (this.OnElementConsumed != null)
				{
					this.OnElementConsumed(consumed_info);
				}
			}
		}
		Game.Instance.accumulators.Accumulate(this.accumulator, consumed_info.mass);
	}

	// Token: 0x17000469 RID: 1129
	// (get) Token: 0x06003F0A RID: 16138 RVA: 0x00162A18 File Offset: 0x00160C18
	public bool IsElementAvailable
	{
		get
		{
			int sampleCell = this.GetSampleCell();
			SimHashes id = Grid.Element[sampleCell].id;
			return this.elementToConsume == id && Grid.Mass[sampleCell] >= this.minimumMass;
		}
	}

	// Token: 0x06003F0B RID: 16139 RVA: 0x00162A5C File Offset: 0x00160C5C
	private void UpdateStatusItem()
	{
		if (this.showInStatusPanel)
		{
			if (this.statusHandle == Guid.Empty && this.IsActive() && this.consumptionEnabled)
			{
				this.statusHandle = this.selectable.AddStatusItem(Db.Get().BuildingStatusItems.ElementConsumer, this);
				return;
			}
			if (this.statusHandle != Guid.Empty && !this.consumptionEnabled)
			{
				base.GetComponent<KSelectable>().RemoveStatusItem(this.statusHandle, false);
				this.statusHandle = Guid.Empty;
				return;
			}
		}
		else if (this.statusHandle != Guid.Empty)
		{
			base.GetComponent<KSelectable>().RemoveStatusItem(this.statusHandle, false);
			this.statusHandle = Guid.Empty;
		}
	}

	// Token: 0x06003F0C RID: 16140 RVA: 0x00162B20 File Offset: 0x00160D20
	private void OnStorageChange(object data)
	{
		bool flag = !this.IsStorageFull();
		if (flag != this.hasAvailableCapacity)
		{
			this.hasAvailableCapacity = flag;
			this.RefreshConsumptionRate();
		}
	}

	// Token: 0x06003F0D RID: 16141 RVA: 0x00162B4D File Offset: 0x00160D4D
	protected override void OnCmpEnable()
	{
		if (!base.isSpawned)
		{
			return;
		}
		if (!this.IsActive())
		{
			return;
		}
		this.UpdateStatusItem();
	}

	// Token: 0x06003F0E RID: 16142 RVA: 0x00162B67 File Offset: 0x00160D67
	protected override void OnCmpDisable()
	{
		this.UpdateStatusItem();
	}

	// Token: 0x06003F0F RID: 16143 RVA: 0x00162B70 File Offset: 0x00160D70
	public List<Descriptor> RequirementDescriptors()
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.isRequired && this.showDescriptor)
		{
			Element element = ElementLoader.FindElementByHash(this.elementToConsume);
			string text = element.tag.ProperName();
			if (element.IsVacuum)
			{
				if (this.configuration == ElementConsumer.Configuration.AllGas)
				{
					text = ELEMENTS.STATE.GAS;
				}
				else if (this.configuration == ElementConsumer.Configuration.AllLiquid)
				{
					text = ELEMENTS.STATE.LIQUID;
				}
				else
				{
					text = UI.BUILDINGEFFECTS.CONSUMESANYELEMENT;
				}
			}
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.REQUIRESELEMENT, text), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.REQUIRESELEMENT, text), Descriptor.DescriptorType.Requirement);
			list.Add(descriptor);
		}
		return list;
	}

	// Token: 0x06003F10 RID: 16144 RVA: 0x00162C28 File Offset: 0x00160E28
	public List<Descriptor> EffectDescriptors()
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.showDescriptor)
		{
			Element element = ElementLoader.FindElementByHash(this.elementToConsume);
			string text = element.tag.ProperName();
			if (element.IsVacuum)
			{
				if (this.configuration == ElementConsumer.Configuration.AllGas)
				{
					text = ELEMENTS.STATE.GAS;
				}
				else if (this.configuration == ElementConsumer.Configuration.AllLiquid)
				{
					text = ELEMENTS.STATE.LIQUID;
				}
				else
				{
					text = UI.BUILDINGEFFECTS.CONSUMESANYELEMENT;
				}
			}
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTCONSUMED, text, GameUtil.GetFormattedMass(this.consumptionRate / 100f * 100f, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTCONSUMED, text, GameUtil.GetFormattedMass(this.consumptionRate / 100f * 100f, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), Descriptor.DescriptorType.Effect);
			list.Add(descriptor);
		}
		return list;
	}

	// Token: 0x06003F11 RID: 16145 RVA: 0x00162D14 File Offset: 0x00160F14
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

	// Token: 0x06003F12 RID: 16146 RVA: 0x00162DB0 File Offset: 0x00160FB0
	private void OnActiveChanged(object data)
	{
		bool isActive = this.operational.IsActive;
		this.EnableConsumption(isActive);
	}

	// Token: 0x06003F13 RID: 16147 RVA: 0x00162DD0 File Offset: 0x00160FD0
	protected override void OnSimUnregister()
	{
		global::Debug.Assert(Sim.IsValidHandle(this.simHandle));
		ElementConsumer.handleInstanceMap.Remove(this.simHandle);
		ElementConsumer.StaticUnregister(this.simHandle);
	}

	// Token: 0x06003F14 RID: 16148 RVA: 0x00162DFE File Offset: 0x00160FFE
	protected override void OnSimRegister(HandleVector<Game.ComplexCallbackInfo<int>>.Handle cb_handle)
	{
		SimMessages.AddElementConsumer(this.GetSampleCell(), this.configuration, this.elementToConsume, this.consumptionRadius, cb_handle.index);
	}

	// Token: 0x06003F15 RID: 16149 RVA: 0x00162E24 File Offset: 0x00161024
	protected override Action<int> GetStaticUnregister()
	{
		return new Action<int>(ElementConsumer.StaticUnregister);
	}

	// Token: 0x06003F16 RID: 16150 RVA: 0x00162E32 File Offset: 0x00161032
	private static void StaticUnregister(int sim_handle)
	{
		global::Debug.Assert(Sim.IsValidHandle(sim_handle));
		SimMessages.RemoveElementConsumer(-1, sim_handle);
	}

	// Token: 0x06003F17 RID: 16151 RVA: 0x00162E46 File Offset: 0x00161046
	protected override void OnSimRegistered()
	{
		if (this.consumptionEnabled)
		{
			this.UpdateSimData();
		}
		ElementConsumer.handleInstanceMap[this.simHandle] = this;
	}

	// Token: 0x0400271D RID: 10013
	[HashedEnum]
	[SerializeField]
	public SimHashes elementToConsume = SimHashes.Vacuum;

	// Token: 0x0400271E RID: 10014
	[SerializeField]
	public float consumptionRate;

	// Token: 0x0400271F RID: 10015
	[SerializeField]
	public byte consumptionRadius = 1;

	// Token: 0x04002720 RID: 10016
	[SerializeField]
	public float minimumMass;

	// Token: 0x04002721 RID: 10017
	[SerializeField]
	public bool showInStatusPanel = true;

	// Token: 0x04002722 RID: 10018
	[SerializeField]
	public Vector3 sampleCellOffset;

	// Token: 0x04002723 RID: 10019
	[SerializeField]
	public float capacityKG = float.PositiveInfinity;

	// Token: 0x04002724 RID: 10020
	[SerializeField]
	public ElementConsumer.Configuration configuration;

	// Token: 0x04002725 RID: 10021
	[Serialize]
	[NonSerialized]
	public float consumedMass;

	// Token: 0x04002726 RID: 10022
	[Serialize]
	[NonSerialized]
	public float consumedTemperature;

	// Token: 0x04002727 RID: 10023
	[SerializeField]
	public bool storeOnConsume;

	// Token: 0x04002728 RID: 10024
	[MyCmpGet]
	public Storage storage;

	// Token: 0x04002729 RID: 10025
	[MyCmpGet]
	private Operational operational;

	// Token: 0x0400272A RID: 10026
	[MyCmpGet]
	private KSelectable selectable;

	// Token: 0x0400272C RID: 10028
	private HandleVector<int>.Handle accumulator = HandleVector<int>.InvalidHandle;

	// Token: 0x0400272D RID: 10029
	public bool ignoreActiveChanged;

	// Token: 0x0400272E RID: 10030
	private Guid statusHandle;

	// Token: 0x0400272F RID: 10031
	public bool showDescriptor = true;

	// Token: 0x04002730 RID: 10032
	public bool isRequired = true;

	// Token: 0x04002731 RID: 10033
	private bool consumptionEnabled;

	// Token: 0x04002732 RID: 10034
	private bool hasAvailableCapacity = true;

	// Token: 0x04002733 RID: 10035
	private static Dictionary<int, ElementConsumer> handleInstanceMap = new Dictionary<int, ElementConsumer>();

	// Token: 0x04002734 RID: 10036
	private static readonly EventSystem.IntraObjectHandler<ElementConsumer> OnActiveChangedDelegate = new EventSystem.IntraObjectHandler<ElementConsumer>(delegate(ElementConsumer component, object data)
	{
		component.OnActiveChanged(data);
	});

	// Token: 0x04002735 RID: 10037
	private static readonly EventSystem.IntraObjectHandler<ElementConsumer> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<ElementConsumer>(delegate(ElementConsumer component, object data)
	{
		component.OnStorageChange(data);
	});

	// Token: 0x02001889 RID: 6281
	public enum Configuration
	{
		// Token: 0x04007934 RID: 31028
		Element,
		// Token: 0x04007935 RID: 31029
		AllLiquid,
		// Token: 0x04007936 RID: 31030
		AllGas
	}
}
