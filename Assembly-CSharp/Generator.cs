using System;
using System.Collections.Generic;
using System.Diagnostics;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000936 RID: 2358
[SerializationConfig(MemberSerialization.OptIn)]
[DebuggerDisplay("{name}")]
[AddComponentMenu("KMonoBehaviour/scripts/Generator")]
public class Generator : KMonoBehaviour, ISaveLoadable, IEnergyProducer, ICircuitConnected
{
	// Token: 0x170004BC RID: 1212
	// (get) Token: 0x060042DF RID: 17119 RVA: 0x00180097 File Offset: 0x0017E297
	public int PowerDistributionOrder
	{
		get
		{
			return this.powerDistributionOrder;
		}
	}

	// Token: 0x170004BD RID: 1213
	// (get) Token: 0x060042E0 RID: 17120 RVA: 0x0018009F File Offset: 0x0017E29F
	public virtual float Capacity
	{
		get
		{
			return this.capacity;
		}
	}

	// Token: 0x170004BE RID: 1214
	// (get) Token: 0x060042E1 RID: 17121 RVA: 0x001800A7 File Offset: 0x0017E2A7
	public virtual bool IsEmpty
	{
		get
		{
			return this.joulesAvailable <= 0f;
		}
	}

	// Token: 0x170004BF RID: 1215
	// (get) Token: 0x060042E2 RID: 17122 RVA: 0x001800B9 File Offset: 0x0017E2B9
	public virtual float JoulesAvailable
	{
		get
		{
			return this.joulesAvailable;
		}
	}

	// Token: 0x170004C0 RID: 1216
	// (get) Token: 0x060042E3 RID: 17123 RVA: 0x001800C1 File Offset: 0x0017E2C1
	public float WattageRating
	{
		get
		{
			return this.building.Def.GeneratorWattageRating * this.Efficiency;
		}
	}

	// Token: 0x170004C1 RID: 1217
	// (get) Token: 0x060042E4 RID: 17124 RVA: 0x001800DA File Offset: 0x0017E2DA
	public float BaseWattageRating
	{
		get
		{
			return this.building.Def.GeneratorWattageRating;
		}
	}

	// Token: 0x170004C2 RID: 1218
	// (get) Token: 0x060042E5 RID: 17125 RVA: 0x001800EC File Offset: 0x0017E2EC
	public float PercentFull
	{
		get
		{
			if (this.Capacity == 0f)
			{
				return 1f;
			}
			return this.joulesAvailable / this.Capacity;
		}
	}

	// Token: 0x170004C3 RID: 1219
	// (get) Token: 0x060042E6 RID: 17126 RVA: 0x0018010E File Offset: 0x0017E30E
	// (set) Token: 0x060042E7 RID: 17127 RVA: 0x00180116 File Offset: 0x0017E316
	public int PowerCell { get; private set; }

	// Token: 0x170004C4 RID: 1220
	// (get) Token: 0x060042E8 RID: 17128 RVA: 0x0018011F File Offset: 0x0017E31F
	public ushort CircuitID
	{
		get
		{
			return Game.Instance.circuitManager.GetCircuitID(this);
		}
	}

	// Token: 0x170004C5 RID: 1221
	// (get) Token: 0x060042E9 RID: 17129 RVA: 0x00180131 File Offset: 0x0017E331
	private float Efficiency
	{
		get
		{
			return Mathf.Max(1f + this.generatorOutputAttribute.GetTotalValue() / 100f, 0f);
		}
	}

	// Token: 0x170004C6 RID: 1222
	// (get) Token: 0x060042EA RID: 17130 RVA: 0x00180154 File Offset: 0x0017E354
	// (set) Token: 0x060042EB RID: 17131 RVA: 0x0018015C File Offset: 0x0017E35C
	public bool IsVirtual { get; protected set; }

	// Token: 0x170004C7 RID: 1223
	// (get) Token: 0x060042EC RID: 17132 RVA: 0x00180165 File Offset: 0x0017E365
	// (set) Token: 0x060042ED RID: 17133 RVA: 0x0018016D File Offset: 0x0017E36D
	public object VirtualCircuitKey { get; protected set; }

	// Token: 0x060042EE RID: 17134 RVA: 0x00180178 File Offset: 0x0017E378
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Attributes attributes = base.gameObject.GetAttributes();
		this.generatorOutputAttribute = attributes.Add(Db.Get().Attributes.GeneratorOutput);
	}

	// Token: 0x060042EF RID: 17135 RVA: 0x001801B4 File Offset: 0x0017E3B4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.Generators.Add(this);
		this.cachedPrefabId = base.gameObject.PrefabID();
		base.Subscribe<Generator>(-1582839653, Generator.OnTagsChangedDelegate);
		this.OnTagsChanged(null);
		this.capacity = Generator.CalculateCapacity(this.building.Def, null);
		this.PowerCell = this.building.GetPowerOutputCell();
		this.CheckConnectionStatus();
		Game.Instance.energySim.AddGenerator(this);
	}

	// Token: 0x060042F0 RID: 17136 RVA: 0x00180239 File Offset: 0x0017E439
	private void OnTagsChanged(object data)
	{
		if (this.HasAllTags(this.connectedTags))
		{
			Game.Instance.circuitManager.Connect(this);
			return;
		}
		Game.Instance.circuitManager.Disconnect(this);
	}

	// Token: 0x060042F1 RID: 17137 RVA: 0x0018026A File Offset: 0x0017E46A
	public virtual bool IsProducingPower()
	{
		return this.operational.IsActive;
	}

	// Token: 0x060042F2 RID: 17138 RVA: 0x00180277 File Offset: 0x0017E477
	public virtual void EnergySim200ms(float dt)
	{
		this.CheckConnectionStatus();
	}

	// Token: 0x060042F3 RID: 17139 RVA: 0x00180280 File Offset: 0x0017E480
	private void SetStatusItem(StatusItem status_item)
	{
		if (status_item != this.currentStatusItem && this.currentStatusItem != null)
		{
			this.statusItemID = this.selectable.RemoveStatusItem(this.statusItemID, false);
		}
		if (status_item != null && this.statusItemID == Guid.Empty)
		{
			this.statusItemID = this.selectable.AddStatusItem(status_item, this);
		}
		this.currentStatusItem = status_item;
	}

	// Token: 0x060042F4 RID: 17140 RVA: 0x001802E8 File Offset: 0x0017E4E8
	private void CheckConnectionStatus()
	{
		if (this.CircuitID == 65535)
		{
			if (this.showConnectedConsumerStatusItems)
			{
				this.SetStatusItem(Db.Get().BuildingStatusItems.NoWireConnected);
			}
			this.operational.SetFlag(Generator.generatorConnectedFlag, false);
			return;
		}
		if (!Game.Instance.circuitManager.HasConsumers(this.CircuitID) && !Game.Instance.circuitManager.HasBatteries(this.CircuitID))
		{
			if (this.showConnectedConsumerStatusItems)
			{
				this.SetStatusItem(Db.Get().BuildingStatusItems.NoPowerConsumers);
			}
			this.operational.SetFlag(Generator.generatorConnectedFlag, true);
			return;
		}
		this.SetStatusItem(null);
		this.operational.SetFlag(Generator.generatorConnectedFlag, true);
	}

	// Token: 0x060042F5 RID: 17141 RVA: 0x001803A6 File Offset: 0x0017E5A6
	protected override void OnCleanUp()
	{
		Game.Instance.energySim.RemoveGenerator(this);
		Game.Instance.circuitManager.Disconnect(this);
		Components.Generators.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x060042F6 RID: 17142 RVA: 0x001803D9 File Offset: 0x0017E5D9
	public static float CalculateCapacity(BuildingDef def, Element element)
	{
		if (element == null)
		{
			return def.GeneratorBaseCapacity;
		}
		return def.GeneratorBaseCapacity * (1f + (element.HasTag(GameTags.RefinedMetal) ? 1f : 0f));
	}

	// Token: 0x060042F7 RID: 17143 RVA: 0x0018040B File Offset: 0x0017E60B
	public void ResetJoules()
	{
		this.joulesAvailable = 0f;
	}

	// Token: 0x060042F8 RID: 17144 RVA: 0x00180418 File Offset: 0x0017E618
	public virtual void ApplyDeltaJoules(float joulesDelta, bool canOverPower = false)
	{
		this.joulesAvailable = Mathf.Clamp(this.joulesAvailable + joulesDelta, 0f, canOverPower ? float.MaxValue : this.Capacity);
	}

	// Token: 0x060042F9 RID: 17145 RVA: 0x00180444 File Offset: 0x0017E644
	public void GenerateJoules(float joulesAvailable, bool canOverPower = false)
	{
		ReportManager.Instance.ReportValue(ReportManager.ReportType.EnergyCreated, joulesAvailable, this.selectable.GetProperName(), null);
		float num = this.joulesAvailable + joulesAvailable;
		this.joulesAvailable = Mathf.Clamp(num, 0f, canOverPower ? float.MaxValue : this.Capacity);
		if (num > joulesAvailable)
		{
			ReportManager.Instance.ReportValue(ReportManager.ReportType.EnergyWasted, this.joulesAvailable - num, StringFormatter.Replace(BUILDINGS.PREFABS.GENERATOR.OVERPRODUCTION, "{Generator}", base.gameObject.GetProperName()), null);
		}
		if (!Game.Instance.savedInfo.powerCreatedbyGeneratorType.ContainsKey(this.cachedPrefabId))
		{
			Game.Instance.savedInfo.powerCreatedbyGeneratorType.Add(this.cachedPrefabId, 0f);
		}
		Dictionary<Tag, float> powerCreatedbyGeneratorType = Game.Instance.savedInfo.powerCreatedbyGeneratorType;
		Tag tag = this.cachedPrefabId;
		powerCreatedbyGeneratorType[tag] += this.joulesAvailable;
	}

	// Token: 0x060042FA RID: 17146 RVA: 0x00180533 File Offset: 0x0017E733
	public void AssignJoulesAvailable(float joulesAvailable)
	{
		this.joulesAvailable = joulesAvailable;
	}

	// Token: 0x060042FB RID: 17147 RVA: 0x0018053C File Offset: 0x0017E73C
	public virtual void ConsumeEnergy(float joules)
	{
		this.joulesAvailable = Mathf.Max(0f, this.JoulesAvailable - joules);
	}

	// Token: 0x04002CA3 RID: 11427
	protected const int SimUpdateSortKey = 1001;

	// Token: 0x04002CA4 RID: 11428
	[MyCmpReq]
	protected Building building;

	// Token: 0x04002CA5 RID: 11429
	[MyCmpReq]
	protected Operational operational;

	// Token: 0x04002CA6 RID: 11430
	[MyCmpReq]
	protected KSelectable selectable;

	// Token: 0x04002CA7 RID: 11431
	[Serialize]
	private float joulesAvailable;

	// Token: 0x04002CA8 RID: 11432
	[SerializeField]
	public int powerDistributionOrder;

	// Token: 0x04002CA9 RID: 11433
	private Tag cachedPrefabId;

	// Token: 0x04002CAA RID: 11434
	public static readonly Operational.Flag generatorConnectedFlag = new Operational.Flag("GeneratorConnected", Operational.Flag.Type.Requirement);

	// Token: 0x04002CAB RID: 11435
	protected static readonly Operational.Flag wireConnectedFlag = new Operational.Flag("generatorWireConnected", Operational.Flag.Type.Requirement);

	// Token: 0x04002CAC RID: 11436
	private float capacity;

	// Token: 0x04002CB0 RID: 11440
	public static readonly Tag[] DEFAULT_CONNECTED_TAGS = new Tag[] { GameTags.Operational };

	// Token: 0x04002CB1 RID: 11441
	[SerializeField]
	public Tag[] connectedTags = Generator.DEFAULT_CONNECTED_TAGS;

	// Token: 0x04002CB2 RID: 11442
	public bool showConnectedConsumerStatusItems = true;

	// Token: 0x04002CB3 RID: 11443
	private StatusItem currentStatusItem;

	// Token: 0x04002CB4 RID: 11444
	private Guid statusItemID;

	// Token: 0x04002CB5 RID: 11445
	private AttributeInstance generatorOutputAttribute;

	// Token: 0x04002CB6 RID: 11446
	private static readonly EventSystem.IntraObjectHandler<Generator> OnTagsChangedDelegate = new EventSystem.IntraObjectHandler<Generator>(delegate(Generator component, object data)
	{
		component.OnTagsChanged(data);
	});
}
