using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200094D RID: 2381
public class HEPFuelTank : KMonoBehaviour, IFuelTank, IUserControlledCapacity
{
	// Token: 0x170004D5 RID: 1237
	// (get) Token: 0x06004422 RID: 17442 RVA: 0x0018840C File Offset: 0x0018660C
	public IStorage Storage
	{
		get
		{
			return this.hepStorage;
		}
	}

	// Token: 0x170004D6 RID: 1238
	// (get) Token: 0x06004423 RID: 17443 RVA: 0x00188414 File Offset: 0x00186614
	public bool ConsumeFuelOnLand
	{
		get
		{
			return this.consumeFuelOnLand;
		}
	}

	// Token: 0x06004424 RID: 17444 RVA: 0x0018841C File Offset: 0x0018661C
	public void DEBUG_FillTank()
	{
		this.hepStorage.Store(this.hepStorage.RemainingCapacity());
	}

	// Token: 0x170004D7 RID: 1239
	// (get) Token: 0x06004425 RID: 17445 RVA: 0x00188435 File Offset: 0x00186635
	// (set) Token: 0x06004426 RID: 17446 RVA: 0x00188442 File Offset: 0x00186642
	public float UserMaxCapacity
	{
		get
		{
			return this.hepStorage.capacity;
		}
		set
		{
			this.hepStorage.capacity = value;
			base.Trigger(-795826715, this);
		}
	}

	// Token: 0x170004D8 RID: 1240
	// (get) Token: 0x06004427 RID: 17447 RVA: 0x0018845C File Offset: 0x0018665C
	public float MinCapacity
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x170004D9 RID: 1241
	// (get) Token: 0x06004428 RID: 17448 RVA: 0x00188463 File Offset: 0x00186663
	public float MaxCapacity
	{
		get
		{
			return this.physicalFuelCapacity;
		}
	}

	// Token: 0x170004DA RID: 1242
	// (get) Token: 0x06004429 RID: 17449 RVA: 0x0018846B File Offset: 0x0018666B
	public float AmountStored
	{
		get
		{
			return this.hepStorage.Particles;
		}
	}

	// Token: 0x170004DB RID: 1243
	// (get) Token: 0x0600442A RID: 17450 RVA: 0x00188478 File Offset: 0x00186678
	public bool WholeValues
	{
		get
		{
			return false;
		}
	}

	// Token: 0x170004DC RID: 1244
	// (get) Token: 0x0600442B RID: 17451 RVA: 0x0018847B File Offset: 0x0018667B
	public LocString CapacityUnits
	{
		get
		{
			return UI.UNITSUFFIXES.HIGHENERGYPARTICLES.PARTRICLES;
		}
	}

	// Token: 0x0600442C RID: 17452 RVA: 0x00188484 File Offset: 0x00186684
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.GetComponent<RocketModule>().AddModuleCondition(ProcessCondition.ProcessConditionType.RocketStorage, new ConditionProperlyFueled(this));
		this.m_meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_fill", "meter_frame", "meter_OL" });
		this.m_meter.gameObject.GetComponent<KBatchedAnimTracker>().matchParentOffset = true;
		this.OnStorageChange(null);
		base.Subscribe<HEPFuelTank>(-795826715, HEPFuelTank.OnStorageChangedDelegate);
		base.Subscribe<HEPFuelTank>(-1837862626, HEPFuelTank.OnStorageChangedDelegate);
	}

	// Token: 0x0600442D RID: 17453 RVA: 0x0018852D File Offset: 0x0018672D
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<HEPFuelTank>(-905833192, HEPFuelTank.OnCopySettingsDelegate);
	}

	// Token: 0x0600442E RID: 17454 RVA: 0x00188546 File Offset: 0x00186746
	private void OnStorageChange(object data)
	{
		this.m_meter.SetPositionPercent(this.hepStorage.Particles / Mathf.Max(1f, this.hepStorage.capacity));
	}

	// Token: 0x0600442F RID: 17455 RVA: 0x00188574 File Offset: 0x00186774
	private void OnCopySettings(object data)
	{
		HEPFuelTank component = ((GameObject)data).GetComponent<HEPFuelTank>();
		if (component != null)
		{
			this.UserMaxCapacity = component.UserMaxCapacity;
		}
	}

	// Token: 0x04002DA3 RID: 11683
	[MyCmpReq]
	public HighEnergyParticleStorage hepStorage;

	// Token: 0x04002DA4 RID: 11684
	[Serialize]
	public float userMaxCapacity;

	// Token: 0x04002DA5 RID: 11685
	public float physicalFuelCapacity;

	// Token: 0x04002DA6 RID: 11686
	private MeterController m_meter;

	// Token: 0x04002DA7 RID: 11687
	public bool consumeFuelOnLand;

	// Token: 0x04002DA8 RID: 11688
	private static readonly EventSystem.IntraObjectHandler<HEPFuelTank> OnStorageChangedDelegate = new EventSystem.IntraObjectHandler<HEPFuelTank>(delegate(HEPFuelTank component, object data)
	{
		component.OnStorageChange(data);
	});

	// Token: 0x04002DA9 RID: 11689
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04002DAA RID: 11690
	private static readonly EventSystem.IntraObjectHandler<HEPFuelTank> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<HEPFuelTank>(delegate(HEPFuelTank component, object data)
	{
		component.OnCopySettings(data);
	});
}
