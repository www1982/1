using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000B59 RID: 2905
[AddComponentMenu("KMonoBehaviour/scripts/OxidizerTank")]
public class OxidizerTank : KMonoBehaviour, IUserControlledCapacity
{
	// Token: 0x1700064A RID: 1610
	// (get) Token: 0x0600567E RID: 22142 RVA: 0x001F5556 File Offset: 0x001F3756
	public bool IsSuspended
	{
		get
		{
			return this.isSuspended;
		}
	}

	// Token: 0x1700064B RID: 1611
	// (get) Token: 0x0600567F RID: 22143 RVA: 0x001F555E File Offset: 0x001F375E
	// (set) Token: 0x06005680 RID: 22144 RVA: 0x001F5568 File Offset: 0x001F3768
	public float UserMaxCapacity
	{
		get
		{
			return this.targetFillMass;
		}
		set
		{
			this.targetFillMass = value;
			this.storage.capacityKg = this.targetFillMass;
			ConduitConsumer component = base.GetComponent<ConduitConsumer>();
			if (component != null)
			{
				component.capacityKG = this.targetFillMass;
			}
			base.Trigger(-945020481, this);
			this.OnStorageCapacityChanged(this.targetFillMass);
			if (this.filteredStorage != null)
			{
				this.filteredStorage.FilterChanged();
			}
		}
	}

	// Token: 0x1700064C RID: 1612
	// (get) Token: 0x06005681 RID: 22145 RVA: 0x001F55D4 File Offset: 0x001F37D4
	public float MinCapacity
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x1700064D RID: 1613
	// (get) Token: 0x06005682 RID: 22146 RVA: 0x001F55DB File Offset: 0x001F37DB
	public float MaxCapacity
	{
		get
		{
			return this.maxFillMass;
		}
	}

	// Token: 0x1700064E RID: 1614
	// (get) Token: 0x06005683 RID: 22147 RVA: 0x001F55E3 File Offset: 0x001F37E3
	public float AmountStored
	{
		get
		{
			return this.storage.MassStored();
		}
	}

	// Token: 0x1700064F RID: 1615
	// (get) Token: 0x06005684 RID: 22148 RVA: 0x001F55F0 File Offset: 0x001F37F0
	public float TotalOxidizerPower
	{
		get
		{
			float num = 0f;
			foreach (GameObject gameObject in this.storage.items)
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				float num2;
				if (DlcManager.FeatureClusterSpaceEnabled())
				{
					num2 = Clustercraft.dlc1OxidizerEfficiencies[component.ElementID.CreateTag()];
				}
				else
				{
					num2 = RocketStats.oxidizerEfficiencies[component.ElementID.CreateTag()];
				}
				num += component.Mass * num2;
			}
			return num;
		}
	}

	// Token: 0x17000650 RID: 1616
	// (get) Token: 0x06005685 RID: 22149 RVA: 0x001F5694 File Offset: 0x001F3894
	public bool WholeValues
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000651 RID: 1617
	// (get) Token: 0x06005686 RID: 22150 RVA: 0x001F5697 File Offset: 0x001F3897
	public LocString CapacityUnits
	{
		get
		{
			return GameUtil.GetCurrentMassUnit(false);
		}
	}

	// Token: 0x06005687 RID: 22151 RVA: 0x001F56A0 File Offset: 0x001F38A0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<OxidizerTank>(-905833192, OxidizerTank.OnCopySettingsDelegate);
		if (this.supportsMultipleOxidizers)
		{
			this.filteredStorage = new FilteredStorage(this, null, this, true, Db.Get().ChoreTypes.Fetch);
			this.filteredStorage.FilterChanged();
			KBatchedAnimTracker componentInChildren = base.gameObject.GetComponentInChildren<KBatchedAnimTracker>();
			componentInChildren.forceAlwaysAlive = true;
			componentInChildren.matchParentOffset = true;
			return;
		}
		this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_fill", "meter_frame", "meter_OL" });
		KBatchedAnimTracker component = this.meter.gameObject.GetComponent<KBatchedAnimTracker>();
		component.matchParentOffset = true;
		component.forceAlwaysAlive = true;
	}

	// Token: 0x06005688 RID: 22152 RVA: 0x001F5770 File Offset: 0x001F3970
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.discoverResourcesOnSpawn != null)
		{
			foreach (SimHashes simHashes in this.discoverResourcesOnSpawn)
			{
				Element element = ElementLoader.FindElementByHash(simHashes);
				DiscoveredResources.Instance.Discover(element.tag, element.GetMaterialCategoryTag());
			}
		}
		base.GetComponent<KBatchedAnimController>().Play("grounded", KAnim.PlayMode.Loop, 1f, 0f);
		RocketModuleCluster component = base.GetComponent<RocketModuleCluster>();
		if (component != null)
		{
			global::Debug.Assert(DlcManager.IsExpansion1Active(), "EXP1 not active but trying to use EXP1 rockety system");
			component.AddModuleCondition(ProcessCondition.ProcessConditionType.RocketStorage, new ConditionSufficientOxidizer(this));
		}
		this.UserMaxCapacity = Mathf.Min(this.UserMaxCapacity, this.maxFillMass);
		base.Subscribe<OxidizerTank>(-887025858, OxidizerTank.OnRocketLandedDelegate);
		base.Subscribe<OxidizerTank>(-1697596308, OxidizerTank.OnStorageChangeDelegate);
	}

	// Token: 0x06005689 RID: 22153 RVA: 0x001F586C File Offset: 0x001F3A6C
	public float GetTotalOxidizerAvailable()
	{
		float num = 0f;
		foreach (Tag tag in this.oxidizerTypes)
		{
			num += this.storage.GetAmountAvailable(tag);
		}
		return num;
	}

	// Token: 0x0600568A RID: 22154 RVA: 0x001F58AC File Offset: 0x001F3AAC
	public Dictionary<Tag, float> GetOxidizersAvailable()
	{
		Dictionary<Tag, float> dictionary = new Dictionary<Tag, float>();
		foreach (Tag tag in this.oxidizerTypes)
		{
			dictionary[tag] = this.storage.GetAmountAvailable(tag);
		}
		return dictionary;
	}

	// Token: 0x0600568B RID: 22155 RVA: 0x001F58F0 File Offset: 0x001F3AF0
	private void OnStorageChange(object data)
	{
		this.RefreshMeter();
	}

	// Token: 0x0600568C RID: 22156 RVA: 0x001F58F8 File Offset: 0x001F3AF8
	private void OnStorageCapacityChanged(float newCapacity)
	{
		this.RefreshMeter();
	}

	// Token: 0x0600568D RID: 22157 RVA: 0x001F5900 File Offset: 0x001F3B00
	private void RefreshMeter()
	{
		if (this.filteredStorage != null)
		{
			this.filteredStorage.FilterChanged();
		}
		if (this.meter != null)
		{
			this.meter.SetPositionPercent(this.storage.MassStored() / this.storage.capacityKg);
		}
	}

	// Token: 0x0600568E RID: 22158 RVA: 0x001F593F File Offset: 0x001F3B3F
	private void OnRocketLanded(object data)
	{
		if (this.consumeOnLand)
		{
			this.storage.ConsumeAllIgnoringDisease();
		}
		if (this.filteredStorage != null)
		{
			this.filteredStorage.FilterChanged();
		}
	}

	// Token: 0x0600568F RID: 22159 RVA: 0x001F5968 File Offset: 0x001F3B68
	private void OnCopySettings(object data)
	{
		OxidizerTank component = ((GameObject)data).GetComponent<OxidizerTank>();
		if (component != null)
		{
			this.UserMaxCapacity = component.UserMaxCapacity;
		}
	}

	// Token: 0x06005690 RID: 22160 RVA: 0x001F5998 File Offset: 0x001F3B98
	[ContextMenu("Fill Tank")]
	public void DEBUG_FillTank(SimHashes element)
	{
		base.GetComponent<FlatTagFilterable>().selectedTags.Add(element.CreateTag());
		if (ElementLoader.FindElementByHash(element).IsLiquid)
		{
			this.storage.AddLiquid(element, this.targetFillMass, ElementLoader.FindElementByHash(element).defaultValues.temperature, 0, 0, false, true);
			return;
		}
		if (ElementLoader.FindElementByHash(element).IsSolid)
		{
			GameObject gameObject = ElementLoader.FindElementByHash(element).substance.SpawnResource(base.gameObject.transform.GetPosition(), this.targetFillMass, 300f, byte.MaxValue, 0, false, false, false);
			this.storage.Store(gameObject, false, false, true, false);
		}
	}

	// Token: 0x06005691 RID: 22161 RVA: 0x001F5A44 File Offset: 0x001F3C44
	public OxidizerTank()
	{
		Tag[] array2;
		if (!DlcManager.IsExpansion1Active())
		{
			Tag[] array = new Tag[2];
			array[0] = SimHashes.OxyRock.CreateTag();
			array2 = array;
			array[1] = SimHashes.LiquidOxygen.CreateTag();
		}
		else
		{
			Tag[] array3 = new Tag[3];
			array3[0] = SimHashes.OxyRock.CreateTag();
			array3[1] = SimHashes.LiquidOxygen.CreateTag();
			array2 = array3;
			array3[2] = SimHashes.Fertilizer.CreateTag();
		}
		this.oxidizerTypes = array2;
		base..ctor();
	}

	// Token: 0x040039D2 RID: 14802
	public Storage storage;

	// Token: 0x040039D3 RID: 14803
	public bool supportsMultipleOxidizers;

	// Token: 0x040039D4 RID: 14804
	private MeterController meter;

	// Token: 0x040039D5 RID: 14805
	private bool isSuspended;

	// Token: 0x040039D6 RID: 14806
	public bool consumeOnLand = true;

	// Token: 0x040039D7 RID: 14807
	[Serialize]
	public float maxFillMass;

	// Token: 0x040039D8 RID: 14808
	[Serialize]
	public float targetFillMass;

	// Token: 0x040039D9 RID: 14809
	public List<SimHashes> discoverResourcesOnSpawn;

	// Token: 0x040039DA RID: 14810
	[SerializeField]
	private Tag[] oxidizerTypes;

	// Token: 0x040039DB RID: 14811
	private FilteredStorage filteredStorage;

	// Token: 0x040039DC RID: 14812
	private static readonly EventSystem.IntraObjectHandler<OxidizerTank> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<OxidizerTank>(delegate(OxidizerTank component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x040039DD RID: 14813
	private static readonly EventSystem.IntraObjectHandler<OxidizerTank> OnRocketLandedDelegate = new EventSystem.IntraObjectHandler<OxidizerTank>(delegate(OxidizerTank component, object data)
	{
		component.OnRocketLanded(data);
	});

	// Token: 0x040039DE RID: 14814
	private static readonly EventSystem.IntraObjectHandler<OxidizerTank> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<OxidizerTank>(delegate(OxidizerTank component, object data)
	{
		component.OnStorageChange(data);
	});
}
