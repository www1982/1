using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000839 RID: 2105
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/ConduitConsumer")]
public class ConduitConsumer : KMonoBehaviour, IConduitConsumer
{
	// Token: 0x170003EA RID: 1002
	// (get) Token: 0x0600399F RID: 14751 RVA: 0x0014023E File Offset: 0x0013E43E
	public Storage Storage
	{
		get
		{
			return this.storage;
		}
	}

	// Token: 0x170003EB RID: 1003
	// (get) Token: 0x060039A0 RID: 14752 RVA: 0x00140246 File Offset: 0x0013E446
	public ConduitType ConduitType
	{
		get
		{
			return this.conduitType;
		}
	}

	// Token: 0x170003EC RID: 1004
	// (get) Token: 0x060039A1 RID: 14753 RVA: 0x0014024E File Offset: 0x0013E44E
	public bool IsConnected
	{
		get
		{
			return Grid.Objects[this.utilityCell, (this.conduitType == ConduitType.Gas) ? 12 : 16] != null && this.m_buildingComplete != null;
		}
	}

	// Token: 0x170003ED RID: 1005
	// (get) Token: 0x060039A2 RID: 14754 RVA: 0x00140288 File Offset: 0x0013E488
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

	// Token: 0x170003EE RID: 1006
	// (get) Token: 0x060039A3 RID: 14755 RVA: 0x001402C4 File Offset: 0x0013E4C4
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

	// Token: 0x170003EF RID: 1007
	// (get) Token: 0x060039A4 RID: 14756 RVA: 0x00140314 File Offset: 0x0013E514
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

	// Token: 0x060039A5 RID: 14757 RVA: 0x00140350 File Offset: 0x0013E550
	public void SetConduitData(ConduitType type)
	{
		this.conduitType = type;
	}

	// Token: 0x170003F0 RID: 1008
	// (get) Token: 0x060039A6 RID: 14758 RVA: 0x00140359 File Offset: 0x0013E559
	public ConduitType TypeOfConduit
	{
		get
		{
			return this.conduitType;
		}
	}

	// Token: 0x170003F1 RID: 1009
	// (get) Token: 0x060039A7 RID: 14759 RVA: 0x00140361 File Offset: 0x0013E561
	public bool IsAlmostEmpty
	{
		get
		{
			return !this.ignoreMinMassCheck && this.MassAvailable < this.ConsumptionRate * 30f;
		}
	}

	// Token: 0x170003F2 RID: 1010
	// (get) Token: 0x060039A8 RID: 14760 RVA: 0x00140381 File Offset: 0x0013E581
	public bool IsEmpty
	{
		get
		{
			return !this.ignoreMinMassCheck && (this.MassAvailable == 0f || this.MassAvailable < this.ConsumptionRate);
		}
	}

	// Token: 0x170003F3 RID: 1011
	// (get) Token: 0x060039A9 RID: 14761 RVA: 0x001403AA File Offset: 0x0013E5AA
	public float ConsumptionRate
	{
		get
		{
			return this.consumptionRate;
		}
	}

	// Token: 0x170003F4 RID: 1012
	// (get) Token: 0x060039AA RID: 14762 RVA: 0x001403B2 File Offset: 0x0013E5B2
	// (set) Token: 0x060039AB RID: 14763 RVA: 0x001403C7 File Offset: 0x0013E5C7
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

	// Token: 0x060039AC RID: 14764 RVA: 0x001403DC File Offset: 0x0013E5DC
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

	// Token: 0x170003F5 RID: 1013
	// (get) Token: 0x060039AD RID: 14765 RVA: 0x00140414 File Offset: 0x0013E614
	public float MassAvailable
	{
		get
		{
			ConduitFlow conduitManager = this.GetConduitManager();
			int inputCell = this.GetInputCell(conduitManager.conduitType);
			return conduitManager.GetContents(inputCell).mass;
		}
	}

	// Token: 0x060039AE RID: 14766 RVA: 0x00140444 File Offset: 0x0013E644
	protected virtual int GetInputCell(ConduitType inputConduitType)
	{
		if (this.useSecondaryInput)
		{
			ISecondaryInput[] components = base.GetComponents<ISecondaryInput>();
			foreach (ISecondaryInput secondaryInput in components)
			{
				if (secondaryInput.HasSecondaryConduitType(inputConduitType))
				{
					return Grid.OffsetCell(this.building.NaturalBuildingCell(), secondaryInput.GetSecondaryConduitOffset(inputConduitType));
				}
			}
			global::Debug.LogWarning("No secondaryInput of type was found");
			return Grid.OffsetCell(this.building.NaturalBuildingCell(), components[0].GetSecondaryConduitOffset(inputConduitType));
		}
		return this.building.GetUtilityInputCell();
	}

	// Token: 0x060039AF RID: 14767 RVA: 0x001404C4 File Offset: 0x0013E6C4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		GameScheduler.Instance.Schedule("PlumbingTutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Plumbing, true);
		}, null, null);
		ConduitFlow conduitManager = this.GetConduitManager();
		this.utilityCell = this.GetInputCell(conduitManager.conduitType);
		ScenePartitionerLayer scenePartitionerLayer = GameScenePartitioner.Instance.objectLayers[(this.conduitType == ConduitType.Gas) ? 12 : 16];
		this.partitionerEntry = GameScenePartitioner.Instance.Add("ConduitConsumer.OnSpawn", base.gameObject, this.utilityCell, scenePartitionerLayer, new Action<object>(this.OnConduitConnectionChanged));
		this.GetConduitManager().AddConduitUpdater(new Action<float>(this.ConduitUpdate), ConduitFlowPriority.Default);
		this.OnConduitConnectionChanged(null);
	}

	// Token: 0x060039B0 RID: 14768 RVA: 0x0014058E File Offset: 0x0013E78E
	protected override void OnCleanUp()
	{
		this.GetConduitManager().RemoveConduitUpdater(new Action<float>(this.ConduitUpdate));
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		base.OnCleanUp();
	}

	// Token: 0x060039B1 RID: 14769 RVA: 0x001405BD File Offset: 0x0013E7BD
	private void OnConduitConnectionChanged(object data)
	{
		base.Trigger(-2094018600, this.IsConnected);
	}

	// Token: 0x060039B2 RID: 14770 RVA: 0x001405D5 File Offset: 0x0013E7D5
	public void SetOnState(bool onState)
	{
		this.isOn = onState;
	}

	// Token: 0x060039B3 RID: 14771 RVA: 0x001405E0 File Offset: 0x0013E7E0
	private void ConduitUpdate(float dt)
	{
		if (this.isConsuming && this.isOn)
		{
			ConduitFlow conduitManager = this.GetConduitManager();
			this.Consume(dt, conduitManager);
		}
	}

	// Token: 0x060039B4 RID: 14772 RVA: 0x0014060C File Offset: 0x0013E80C
	private void Consume(float dt, ConduitFlow conduit_mgr)
	{
		this.IsSatisfied = false;
		this.consumedLastTick = false;
		if (this.building.Def.CanMove)
		{
			this.utilityCell = this.GetInputCell(conduit_mgr.conduitType);
		}
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
		if (flag || this.wrongElementResult == ConduitConsumer.WrongElementResult.Store || contents.element == SimHashes.Vacuum || this.capacityTag == GameTags.Any)
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
			if (this.wrongElementResult == ConduitConsumer.WrongElementResult.Dump)
			{
				int num4 = (int)((float)contents.diseaseCount * (num2 / contents.mass));
				SimMessages.AddRemoveSubstance(Grid.PosToCell(base.transform.GetPosition()), contents.element, CellEventLogger.Instance.ConduitConsumerWrongElement, num2, contents.temperature, contents.diseaseIdx, num4, true, -1);
			}
		}
	}

	// Token: 0x04002367 RID: 9063
	[SerializeField]
	public ConduitType conduitType;

	// Token: 0x04002368 RID: 9064
	[SerializeField]
	public bool ignoreMinMassCheck;

	// Token: 0x04002369 RID: 9065
	[SerializeField]
	public Tag capacityTag = GameTags.Any;

	// Token: 0x0400236A RID: 9066
	[SerializeField]
	public float capacityKG = float.PositiveInfinity;

	// Token: 0x0400236B RID: 9067
	[SerializeField]
	public bool forceAlwaysSatisfied;

	// Token: 0x0400236C RID: 9068
	[SerializeField]
	public bool alwaysConsume;

	// Token: 0x0400236D RID: 9069
	[SerializeField]
	public bool keepZeroMassObject = true;

	// Token: 0x0400236E RID: 9070
	[SerializeField]
	public bool useSecondaryInput;

	// Token: 0x0400236F RID: 9071
	[SerializeField]
	public bool isOn = true;

	// Token: 0x04002370 RID: 9072
	[NonSerialized]
	public bool isConsuming = true;

	// Token: 0x04002371 RID: 9073
	[NonSerialized]
	public bool consumedLastTick = true;

	// Token: 0x04002372 RID: 9074
	[MyCmpReq]
	public Operational operational;

	// Token: 0x04002373 RID: 9075
	[MyCmpReq]
	protected Building building;

	// Token: 0x04002374 RID: 9076
	public Operational.State OperatingRequirement;

	// Token: 0x04002375 RID: 9077
	public ISecondaryInput targetSecondaryInput;

	// Token: 0x04002376 RID: 9078
	[MyCmpGet]
	public Storage storage;

	// Token: 0x04002377 RID: 9079
	[MyCmpGet]
	private BuildingComplete m_buildingComplete;

	// Token: 0x04002378 RID: 9080
	private int utilityCell = -1;

	// Token: 0x04002379 RID: 9081
	public float consumptionRate = float.PositiveInfinity;

	// Token: 0x0400237A RID: 9082
	public SimHashes lastConsumedElement = SimHashes.Vacuum;

	// Token: 0x0400237B RID: 9083
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x0400237C RID: 9084
	private bool satisfied;

	// Token: 0x0400237D RID: 9085
	public ConduitConsumer.WrongElementResult wrongElementResult;

	// Token: 0x020017A9 RID: 6057
	public enum WrongElementResult
	{
		// Token: 0x04007674 RID: 30324
		Destroy,
		// Token: 0x04007675 RID: 30325
		Dump,
		// Token: 0x04007676 RID: 30326
		Store
	}
}
