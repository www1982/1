using System;
using System.Collections.Generic;
using System.Diagnostics;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020006DD RID: 1757
[SerializationConfig(MemberSerialization.OptIn)]
[DebuggerDisplay("{name}")]
[AddComponentMenu("KMonoBehaviour/scripts/Battery")]
public class Battery : KMonoBehaviour, IEnergyConsumer, ICircuitConnected, IGameObjectEffectDescriptor, IEnergyProducer
{
	// Token: 0x1700021B RID: 539
	// (get) Token: 0x06002B66 RID: 11110 RVA: 0x000FAC29 File Offset: 0x000F8E29
	// (set) Token: 0x06002B67 RID: 11111 RVA: 0x000FAC31 File Offset: 0x000F8E31
	public float WattsUsed { get; private set; }

	// Token: 0x1700021C RID: 540
	// (get) Token: 0x06002B68 RID: 11112 RVA: 0x000FAC3A File Offset: 0x000F8E3A
	public float WattsNeededWhenActive
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x1700021D RID: 541
	// (get) Token: 0x06002B69 RID: 11113 RVA: 0x000FAC41 File Offset: 0x000F8E41
	public float PercentFull
	{
		get
		{
			return this.joulesAvailable / this.capacity;
		}
	}

	// Token: 0x1700021E RID: 542
	// (get) Token: 0x06002B6A RID: 11114 RVA: 0x000FAC50 File Offset: 0x000F8E50
	public float PreviousPercentFull
	{
		get
		{
			return this.PreviousJoulesAvailable / this.capacity;
		}
	}

	// Token: 0x1700021F RID: 543
	// (get) Token: 0x06002B6B RID: 11115 RVA: 0x000FAC5F File Offset: 0x000F8E5F
	public float JoulesAvailable
	{
		get
		{
			return this.joulesAvailable;
		}
	}

	// Token: 0x17000220 RID: 544
	// (get) Token: 0x06002B6C RID: 11116 RVA: 0x000FAC67 File Offset: 0x000F8E67
	public float Capacity
	{
		get
		{
			return this.capacity;
		}
	}

	// Token: 0x17000221 RID: 545
	// (get) Token: 0x06002B6D RID: 11117 RVA: 0x000FAC6F File Offset: 0x000F8E6F
	// (set) Token: 0x06002B6E RID: 11118 RVA: 0x000FAC77 File Offset: 0x000F8E77
	public float ChargeCapacity { get; private set; }

	// Token: 0x17000222 RID: 546
	// (get) Token: 0x06002B6F RID: 11119 RVA: 0x000FAC80 File Offset: 0x000F8E80
	public int PowerSortOrder
	{
		get
		{
			return this.powerSortOrder;
		}
	}

	// Token: 0x17000223 RID: 547
	// (get) Token: 0x06002B70 RID: 11120 RVA: 0x000FAC88 File Offset: 0x000F8E88
	public string Name
	{
		get
		{
			return base.GetComponent<KSelectable>().GetName();
		}
	}

	// Token: 0x17000224 RID: 548
	// (get) Token: 0x06002B71 RID: 11121 RVA: 0x000FAC95 File Offset: 0x000F8E95
	// (set) Token: 0x06002B72 RID: 11122 RVA: 0x000FAC9D File Offset: 0x000F8E9D
	public int PowerCell { get; private set; }

	// Token: 0x17000225 RID: 549
	// (get) Token: 0x06002B73 RID: 11123 RVA: 0x000FACA6 File Offset: 0x000F8EA6
	public ushort CircuitID
	{
		get
		{
			return Game.Instance.circuitManager.GetCircuitID(this);
		}
	}

	// Token: 0x17000226 RID: 550
	// (get) Token: 0x06002B74 RID: 11124 RVA: 0x000FACB8 File Offset: 0x000F8EB8
	public bool IsConnected
	{
		get
		{
			return this.connectionStatus > CircuitManager.ConnectionStatus.NotConnected;
		}
	}

	// Token: 0x17000227 RID: 551
	// (get) Token: 0x06002B75 RID: 11125 RVA: 0x000FACC3 File Offset: 0x000F8EC3
	public bool IsPowered
	{
		get
		{
			return this.connectionStatus == CircuitManager.ConnectionStatus.Powered;
		}
	}

	// Token: 0x17000228 RID: 552
	// (get) Token: 0x06002B76 RID: 11126 RVA: 0x000FACCE File Offset: 0x000F8ECE
	// (set) Token: 0x06002B77 RID: 11127 RVA: 0x000FACD6 File Offset: 0x000F8ED6
	public bool IsVirtual { get; protected set; }

	// Token: 0x17000229 RID: 553
	// (get) Token: 0x06002B78 RID: 11128 RVA: 0x000FACDF File Offset: 0x000F8EDF
	// (set) Token: 0x06002B79 RID: 11129 RVA: 0x000FACE7 File Offset: 0x000F8EE7
	public object VirtualCircuitKey { get; protected set; }

	// Token: 0x06002B7A RID: 11130 RVA: 0x000FACF0 File Offset: 0x000F8EF0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.Batteries.Add(this);
		Building component = base.GetComponent<Building>();
		this.PowerCell = component.GetPowerInputCell();
		base.Subscribe<Battery>(-1582839653, Battery.OnTagsChangedDelegate);
		this.OnTagsChanged(null);
		this.meter = (base.GetComponent<PowerTransformer>() ? null : new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_fill", "meter_frame", "meter_OL" }));
		Game.Instance.circuitManager.Connect(this);
		Game.Instance.energySim.AddBattery(this);
	}

	// Token: 0x06002B7B RID: 11131 RVA: 0x000FADB0 File Offset: 0x000F8FB0
	private void OnTagsChanged(object data)
	{
		if (this.HasAllTags(this.connectedTags))
		{
			Game.Instance.circuitManager.Connect(this);
			base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, Db.Get().BuildingStatusItems.BatteryJoulesAvailable, this);
			return;
		}
		Game.Instance.circuitManager.Disconnect(this, false);
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.BatteryJoulesAvailable, false);
	}

	// Token: 0x06002B7C RID: 11132 RVA: 0x000FAE34 File Offset: 0x000F9034
	protected override void OnCleanUp()
	{
		Game.Instance.energySim.RemoveBattery(this);
		Game.Instance.circuitManager.Disconnect(this, true);
		Components.Batteries.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06002B7D RID: 11133 RVA: 0x000FAE68 File Offset: 0x000F9068
	public virtual void EnergySim200ms(float dt)
	{
		this.dt = dt;
		this.joulesConsumed = 0f;
		this.WattsUsed = 0f;
		this.ChargeCapacity = this.chargeWattage * dt;
		if (this.meter != null)
		{
			float percentFull = this.PercentFull;
			this.meter.SetPositionPercent(percentFull);
		}
		this.UpdateSounds();
		this.PreviousJoulesAvailable = this.JoulesAvailable;
		this.ConsumeEnergy(this.joulesLostPerSecond * dt, true);
	}

	// Token: 0x06002B7E RID: 11134 RVA: 0x000FAEDC File Offset: 0x000F90DC
	private void UpdateSounds()
	{
		float previousPercentFull = this.PreviousPercentFull;
		float percentFull = this.PercentFull;
		if (percentFull == 0f && previousPercentFull != 0f)
		{
			base.GetComponent<LoopingSounds>().PlayEvent(GameSoundEvents.BatteryDischarged);
		}
		if (percentFull > 0.999f && previousPercentFull <= 0.999f)
		{
			base.GetComponent<LoopingSounds>().PlayEvent(GameSoundEvents.BatteryFull);
		}
		if (percentFull < 0.25f && previousPercentFull >= 0.25f)
		{
			base.GetComponent<LoopingSounds>().PlayEvent(GameSoundEvents.BatteryWarning);
		}
	}

	// Token: 0x06002B7F RID: 11135 RVA: 0x000FAF58 File Offset: 0x000F9158
	public void SetConnectionStatus(CircuitManager.ConnectionStatus status)
	{
		this.connectionStatus = status;
		if (status == CircuitManager.ConnectionStatus.NotConnected)
		{
			this.operational.SetActive(false, false);
			return;
		}
		this.operational.SetActive(this.operational.IsOperational && this.JoulesAvailable > 0f, false);
	}

	// Token: 0x06002B80 RID: 11136 RVA: 0x000FAFA8 File Offset: 0x000F91A8
	public void AddEnergy(float joules)
	{
		this.joulesAvailable = Mathf.Min(this.capacity, this.JoulesAvailable + joules);
		this.joulesConsumed += joules;
		this.ChargeCapacity -= joules;
		this.WattsUsed = this.joulesConsumed / this.dt;
	}

	// Token: 0x06002B81 RID: 11137 RVA: 0x000FB000 File Offset: 0x000F9200
	public void ConsumeEnergy(float joules, bool report = false)
	{
		if (report)
		{
			float num = Mathf.Min(this.JoulesAvailable, joules);
			ReportManager.Instance.ReportValue(ReportManager.ReportType.EnergyWasted, -num, StringFormatter.Replace(BUILDINGS.PREFABS.BATTERY.CHARGE_LOSS, "{Battery}", this.GetProperName()), null);
		}
		this.joulesAvailable = Mathf.Max(0f, this.JoulesAvailable - joules);
	}

	// Token: 0x06002B82 RID: 11138 RVA: 0x000FB05E File Offset: 0x000F925E
	public void ConsumeEnergy(float joules)
	{
		this.ConsumeEnergy(joules, false);
	}

	// Token: 0x06002B83 RID: 11139 RVA: 0x000FB068 File Offset: 0x000F9268
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.powerTransformer == null)
		{
			list.Add(new Descriptor(UI.BUILDINGEFFECTS.REQUIRESPOWERGENERATOR, UI.BUILDINGEFFECTS.TOOLTIPS.REQUIRESPOWERGENERATOR, Descriptor.DescriptorType.Requirement, false));
			list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.BATTERYCAPACITY, GameUtil.GetFormattedJoules(this.capacity, "", GameUtil.TimeSlice.None)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.BATTERYCAPACITY, GameUtil.GetFormattedJoules(this.capacity, "", GameUtil.TimeSlice.None)), Descriptor.DescriptorType.Effect, false));
			list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.BATTERYLEAK, GameUtil.GetFormattedJoules(this.joulesLostPerSecond, "F1", GameUtil.TimeSlice.PerCycle)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.BATTERYLEAK, GameUtil.GetFormattedJoules(this.joulesLostPerSecond, "F1", GameUtil.TimeSlice.PerCycle)), Descriptor.DescriptorType.Effect, false));
		}
		else
		{
			list.Add(new Descriptor(UI.BUILDINGEFFECTS.TRANSFORMER_INPUT_WIRE, UI.BUILDINGEFFECTS.TOOLTIPS.TRANSFORMER_INPUT_WIRE, Descriptor.DescriptorType.Requirement, false));
			list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.TRANSFORMER_OUTPUT_WIRE, GameUtil.GetFormattedWattage(this.capacity, GameUtil.WattageFormatterUnit.Automatic, true)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.TRANSFORMER_OUTPUT_WIRE, GameUtil.GetFormattedWattage(this.capacity, GameUtil.WattageFormatterUnit.Automatic, true)), Descriptor.DescriptorType.Requirement, false));
		}
		return list;
	}

	// Token: 0x06002B84 RID: 11140 RVA: 0x000FB1B0 File Offset: 0x000F93B0
	[ContextMenu("Refill Power")]
	public void DEBUG_RefillPower()
	{
		this.joulesAvailable = this.capacity;
	}

	// Token: 0x040019AE RID: 6574
	[SerializeField]
	public float capacity;

	// Token: 0x040019AF RID: 6575
	[SerializeField]
	public float chargeWattage = float.PositiveInfinity;

	// Token: 0x040019B0 RID: 6576
	[Serialize]
	private float joulesAvailable;

	// Token: 0x040019B1 RID: 6577
	[MyCmpGet]
	protected Operational operational;

	// Token: 0x040019B2 RID: 6578
	[MyCmpGet]
	public PowerTransformer powerTransformer;

	// Token: 0x040019B3 RID: 6579
	protected MeterController meter;

	// Token: 0x040019B5 RID: 6581
	public float joulesLostPerSecond;

	// Token: 0x040019B7 RID: 6583
	[SerializeField]
	public int powerSortOrder;

	// Token: 0x040019BB RID: 6587
	private float PreviousJoulesAvailable;

	// Token: 0x040019BC RID: 6588
	private CircuitManager.ConnectionStatus connectionStatus;

	// Token: 0x040019BD RID: 6589
	public static readonly Tag[] DEFAULT_CONNECTED_TAGS = new Tag[] { GameTags.Operational };

	// Token: 0x040019BE RID: 6590
	[SerializeField]
	public Tag[] connectedTags = Battery.DEFAULT_CONNECTED_TAGS;

	// Token: 0x040019BF RID: 6591
	private static readonly EventSystem.IntraObjectHandler<Battery> OnTagsChangedDelegate = new EventSystem.IntraObjectHandler<Battery>(delegate(Battery component, object data)
	{
		component.OnTagsChanged(data);
	});

	// Token: 0x040019C0 RID: 6592
	private float dt;

	// Token: 0x040019C1 RID: 6593
	private float joulesConsumed;
}
