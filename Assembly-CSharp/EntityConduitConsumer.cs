using System;
using STRINGS;
using UnityEngine;

// Token: 0x020008E9 RID: 2281
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/SpawnableConduitConsumer")]
public class EntityConduitConsumer : KMonoBehaviour, IConduitConsumer
{
	// Token: 0x17000485 RID: 1157
	// (get) Token: 0x06003FB1 RID: 16305 RVA: 0x00165C94 File Offset: 0x00163E94
	public Storage Storage
	{
		get
		{
			return this.storage;
		}
	}

	// Token: 0x17000486 RID: 1158
	// (get) Token: 0x06003FB2 RID: 16306 RVA: 0x00165C9C File Offset: 0x00163E9C
	public ConduitType ConduitType
	{
		get
		{
			return this.conduitType;
		}
	}

	// Token: 0x17000487 RID: 1159
	// (get) Token: 0x06003FB3 RID: 16307 RVA: 0x00165CA4 File Offset: 0x00163EA4
	public bool IsConnected
	{
		get
		{
			return Grid.Objects[this.utilityCell, (this.conduitType == ConduitType.Gas) ? 12 : 16] != null;
		}
	}

	// Token: 0x17000488 RID: 1160
	// (get) Token: 0x06003FB4 RID: 16308 RVA: 0x00165CCC File Offset: 0x00163ECC
	public bool CanConsume
	{
		get
		{
			bool flag = false;
			if (this.IsConnected)
			{
				flag = this.GetConduitManager().GetContents(this.utilityCell).mass > 0f;
			}
			return flag;
		}
	}

	// Token: 0x17000489 RID: 1161
	// (get) Token: 0x06003FB5 RID: 16309 RVA: 0x00165D08 File Offset: 0x00163F08
	public float stored_mass
	{
		get
		{
			if (this.storage == null)
			{
				return 0f;
			}
			if (!(this.capacityTag != GameTags.Any))
			{
				return this.storage.MassStored();
			}
			return this.storage.GetMassAvailable(this.capacityTag);
		}
	}

	// Token: 0x1700048A RID: 1162
	// (get) Token: 0x06003FB6 RID: 16310 RVA: 0x00165D58 File Offset: 0x00163F58
	public float space_remaining_kg
	{
		get
		{
			float num = this.capacityKG - this.stored_mass;
			if (!(this.storage == null))
			{
				return Mathf.Min(this.storage.RemainingCapacity(), num);
			}
			return num;
		}
	}

	// Token: 0x06003FB7 RID: 16311 RVA: 0x00165D94 File Offset: 0x00163F94
	public void SetConduitData(ConduitType type)
	{
		this.conduitType = type;
	}

	// Token: 0x1700048B RID: 1163
	// (get) Token: 0x06003FB8 RID: 16312 RVA: 0x00165D9D File Offset: 0x00163F9D
	public ConduitType TypeOfConduit
	{
		get
		{
			return this.conduitType;
		}
	}

	// Token: 0x1700048C RID: 1164
	// (get) Token: 0x06003FB9 RID: 16313 RVA: 0x00165DA5 File Offset: 0x00163FA5
	public bool IsAlmostEmpty
	{
		get
		{
			return !this.ignoreMinMassCheck && this.MassAvailable < this.ConsumptionRate * 30f;
		}
	}

	// Token: 0x1700048D RID: 1165
	// (get) Token: 0x06003FBA RID: 16314 RVA: 0x00165DC5 File Offset: 0x00163FC5
	public bool IsEmpty
	{
		get
		{
			return !this.ignoreMinMassCheck && (this.MassAvailable == 0f || this.MassAvailable < this.ConsumptionRate);
		}
	}

	// Token: 0x1700048E RID: 1166
	// (get) Token: 0x06003FBB RID: 16315 RVA: 0x00165DEE File Offset: 0x00163FEE
	public float ConsumptionRate
	{
		get
		{
			return this.consumptionRate;
		}
	}

	// Token: 0x1700048F RID: 1167
	// (get) Token: 0x06003FBC RID: 16316 RVA: 0x00165DF6 File Offset: 0x00163FF6
	// (set) Token: 0x06003FBD RID: 16317 RVA: 0x00165E0B File Offset: 0x0016400B
	public bool IsSatisfied
	{
		get
		{
			return this.satisfied || !this.isConsuming;
		}
		set
		{
			this.satisfied = value || this.forceAlwaysSatisfied;
		}
	}

	// Token: 0x06003FBE RID: 16318 RVA: 0x00165E20 File Offset: 0x00164020
	private ConduitFlow GetConduitManager()
	{
		ConduitType conduitType = this.conduitType;
		if (conduitType == ConduitType.Gas)
		{
			return Game.Instance.gasConduitFlow;
		}
		if (conduitType != ConduitType.Liquid)
		{
			return null;
		}
		return Game.Instance.liquidConduitFlow;
	}

	// Token: 0x17000490 RID: 1168
	// (get) Token: 0x06003FBF RID: 16319 RVA: 0x00165E58 File Offset: 0x00164058
	public float MassAvailable
	{
		get
		{
			ConduitFlow conduitManager = this.GetConduitManager();
			int inputCell = this.GetInputCell(conduitManager.conduitType);
			return conduitManager.GetContents(inputCell).mass;
		}
	}

	// Token: 0x06003FC0 RID: 16320 RVA: 0x00165E88 File Offset: 0x00164088
	private int GetInputCell(ConduitType inputConduitType)
	{
		return this.occupyArea.GetOffsetCellWithRotation(this.offset);
	}

	// Token: 0x06003FC1 RID: 16321 RVA: 0x00165E9C File Offset: 0x0016409C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		ConduitFlow conduitManager = this.GetConduitManager();
		this.utilityCell = this.GetInputCell(conduitManager.conduitType);
		ScenePartitionerLayer scenePartitionerLayer = GameScenePartitioner.Instance.objectLayers[(this.conduitType == ConduitType.Gas) ? 12 : 16];
		this.partitionerEntry = GameScenePartitioner.Instance.Add("ConduitConsumer.OnSpawn", base.gameObject, this.utilityCell, scenePartitionerLayer, new Action<object>(this.OnConduitConnectionChanged));
		this.GetConduitManager().AddConduitUpdater(new Action<float>(this.ConduitUpdate), ConduitFlowPriority.Default);
		this.endpoint = new FlowUtilityNetwork.NetworkItem(conduitManager.conduitType, Endpoint.Sink, this.utilityCell, base.gameObject);
		if (conduitManager.conduitType == ConduitType.Solid)
		{
			Game.Instance.solidConduitSystem.AddToNetworks(this.utilityCell, this.endpoint, true);
		}
		else
		{
			Conduit.GetNetworkManager(conduitManager.conduitType).AddToNetworks(this.utilityCell, this.endpoint, true);
		}
		EntityCellVisualizer.Ports ports = EntityCellVisualizer.Ports.LiquidIn;
		if (conduitManager.conduitType == ConduitType.Solid)
		{
			ports = EntityCellVisualizer.Ports.SolidIn;
		}
		else if (conduitManager.conduitType == ConduitType.Gas)
		{
			ports = EntityCellVisualizer.Ports.GasIn;
		}
		this.cellVisualizer.AddPort(ports, this.offset);
		this.OnConduitConnectionChanged(null);
	}

	// Token: 0x06003FC2 RID: 16322 RVA: 0x00165FC0 File Offset: 0x001641C0
	protected override void OnCleanUp()
	{
		if (this.endpoint.ConduitType == ConduitType.Solid)
		{
			Game.Instance.solidConduitSystem.RemoveFromNetworks(this.endpoint.Cell, this.endpoint, true);
		}
		else
		{
			Conduit.GetNetworkManager(this.endpoint.ConduitType).RemoveFromNetworks(this.endpoint.Cell, this.endpoint, true);
		}
		this.GetConduitManager().RemoveConduitUpdater(new Action<float>(this.ConduitUpdate));
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		base.OnCleanUp();
	}

	// Token: 0x06003FC3 RID: 16323 RVA: 0x00166052 File Offset: 0x00164252
	private void OnConduitConnectionChanged(object data)
	{
		base.Trigger(-2094018600, this.IsConnected);
	}

	// Token: 0x06003FC4 RID: 16324 RVA: 0x0016606A File Offset: 0x0016426A
	public void SetOnState(bool onState)
	{
		this.isOn = onState;
	}

	// Token: 0x06003FC5 RID: 16325 RVA: 0x00166074 File Offset: 0x00164274
	private void ConduitUpdate(float dt)
	{
		if (this.isConsuming && this.isOn)
		{
			ConduitFlow conduitManager = this.GetConduitManager();
			this.Consume(dt, conduitManager);
		}
	}

	// Token: 0x06003FC6 RID: 16326 RVA: 0x001660A0 File Offset: 0x001642A0
	private void Consume(float dt, ConduitFlow conduit_mgr)
	{
		this.IsSatisfied = false;
		this.consumedLastTick = false;
		this.utilityCell = this.GetInputCell(conduit_mgr.conduitType);
		if (!this.IsConnected)
		{
			return;
		}
		ConduitFlow.ConduitContents contents = conduit_mgr.GetContents(this.utilityCell);
		if (contents.mass <= 0f)
		{
			return;
		}
		this.IsSatisfied = true;
		if (!this.alwaysConsume && !this.operational.MeetsRequirements(this.OperatingRequirement))
		{
			return;
		}
		float num = this.ConsumptionRate * dt;
		num = Mathf.Min(num, this.space_remaining_kg);
		Element element = ElementLoader.FindElementByHash(contents.element);
		if (contents.element != this.lastConsumedElement)
		{
			DiscoveredResources.Instance.Discover(element.tag, element.materialCategory);
		}
		float num2 = 0f;
		if (num > 0f)
		{
			ConduitFlow.ConduitContents conduitContents = conduit_mgr.RemoveElement(this.utilityCell, num);
			num2 = conduitContents.mass;
			this.lastConsumedElement = conduitContents.element;
		}
		bool flag = element.HasTag(this.capacityTag);
		if (num2 > 0f && this.capacityTag != GameTags.Any && !flag)
		{
			base.Trigger(-794517298, new BuildingHP.DamageSourceInfo
			{
				damage = 1,
				source = BUILDINGS.DAMAGESOURCES.BAD_INPUT_ELEMENT,
				popString = UI.GAMEOBJECTEFFECTS.DAMAGE_POPS.WRONG_ELEMENT
			});
		}
		if (flag || this.wrongElementResult == EntityConduitConsumer.WrongElementResult.Store || contents.element == SimHashes.Vacuum || this.capacityTag == GameTags.Any)
		{
			if (num2 > 0f)
			{
				this.consumedLastTick = true;
				int num3 = (int)((float)contents.diseaseCount * (num2 / contents.mass));
				Element element2 = ElementLoader.FindElementByHash(contents.element);
				ConduitType conduitType = this.conduitType;
				if (conduitType != ConduitType.Gas)
				{
					if (conduitType == ConduitType.Liquid)
					{
						if (element2.IsLiquid)
						{
							this.storage.AddLiquid(contents.element, num2, contents.temperature, contents.diseaseIdx, num3, this.keepZeroMassObject, false);
							return;
						}
						global::Debug.LogWarning("Liquid conduit consumer consuming non liquid: " + element2.id.ToString());
						return;
					}
				}
				else
				{
					if (element2.IsGas)
					{
						this.storage.AddGasChunk(contents.element, num2, contents.temperature, contents.diseaseIdx, num3, this.keepZeroMassObject, false);
						return;
					}
					global::Debug.LogWarning("Gas conduit consumer consuming non gas: " + element2.id.ToString());
					return;
				}
			}
		}
		else if (num2 > 0f)
		{
			this.consumedLastTick = true;
			if (this.wrongElementResult == EntityConduitConsumer.WrongElementResult.Dump)
			{
				int num4 = (int)((float)contents.diseaseCount * (num2 / contents.mass));
				SimMessages.AddRemoveSubstance(Grid.PosToCell(base.transform.GetPosition()), contents.element, CellEventLogger.Instance.ConduitConsumerWrongElement, num2, contents.temperature, contents.diseaseIdx, num4, true, -1);
			}
		}
	}

	// Token: 0x04002787 RID: 10119
	private FlowUtilityNetwork.NetworkItem endpoint;

	// Token: 0x04002788 RID: 10120
	[SerializeField]
	public ConduitType conduitType;

	// Token: 0x04002789 RID: 10121
	[SerializeField]
	public bool ignoreMinMassCheck;

	// Token: 0x0400278A RID: 10122
	[SerializeField]
	public Tag capacityTag = GameTags.Any;

	// Token: 0x0400278B RID: 10123
	[SerializeField]
	public float capacityKG = float.PositiveInfinity;

	// Token: 0x0400278C RID: 10124
	[SerializeField]
	public bool forceAlwaysSatisfied;

	// Token: 0x0400278D RID: 10125
	[SerializeField]
	public bool alwaysConsume;

	// Token: 0x0400278E RID: 10126
	[SerializeField]
	public bool keepZeroMassObject = true;

	// Token: 0x0400278F RID: 10127
	[SerializeField]
	public bool isOn = true;

	// Token: 0x04002790 RID: 10128
	[NonSerialized]
	public bool isConsuming = true;

	// Token: 0x04002791 RID: 10129
	[NonSerialized]
	public bool consumedLastTick = true;

	// Token: 0x04002792 RID: 10130
	[MyCmpReq]
	public Operational operational;

	// Token: 0x04002793 RID: 10131
	[MyCmpReq]
	private OccupyArea occupyArea;

	// Token: 0x04002794 RID: 10132
	[MyCmpReq]
	private EntityCellVisualizer cellVisualizer;

	// Token: 0x04002795 RID: 10133
	public Operational.State OperatingRequirement;

	// Token: 0x04002796 RID: 10134
	[MyCmpGet]
	public Storage storage;

	// Token: 0x04002797 RID: 10135
	public CellOffset offset;

	// Token: 0x04002798 RID: 10136
	private int utilityCell = -1;

	// Token: 0x04002799 RID: 10137
	public float consumptionRate = float.PositiveInfinity;

	// Token: 0x0400279A RID: 10138
	public SimHashes lastConsumedElement = SimHashes.Vacuum;

	// Token: 0x0400279B RID: 10139
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x0400279C RID: 10140
	private bool satisfied;

	// Token: 0x0400279D RID: 10141
	public EntityConduitConsumer.WrongElementResult wrongElementResult;

	// Token: 0x02001896 RID: 6294
	public enum WrongElementResult
	{
		// Token: 0x0400795D RID: 31069
		Destroy,
		// Token: 0x0400795E RID: 31070
		Dump,
		// Token: 0x0400795F RID: 31071
		Store
	}
}
