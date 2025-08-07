using System;
using UnityEngine;

// Token: 0x02000B1E RID: 2846
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/SolidConduitConsumer")]
public class SolidConduitConsumer : KMonoBehaviour, IConduitConsumer
{
	// Token: 0x170005E2 RID: 1506
	// (get) Token: 0x060053E2 RID: 21474 RVA: 0x001E8123 File Offset: 0x001E6323
	public Storage Storage
	{
		get
		{
			return this.storage;
		}
	}

	// Token: 0x170005E3 RID: 1507
	// (get) Token: 0x060053E3 RID: 21475 RVA: 0x001E812B File Offset: 0x001E632B
	public ConduitType ConduitType
	{
		get
		{
			return ConduitType.Solid;
		}
	}

	// Token: 0x170005E4 RID: 1508
	// (get) Token: 0x060053E4 RID: 21476 RVA: 0x001E812E File Offset: 0x001E632E
	public bool IsConsuming
	{
		get
		{
			return this.consuming;
		}
	}

	// Token: 0x170005E5 RID: 1509
	// (get) Token: 0x060053E5 RID: 21477 RVA: 0x001E8138 File Offset: 0x001E6338
	public bool IsConnected
	{
		get
		{
			GameObject gameObject = Grid.Objects[this.utilityCell, 20];
			return gameObject != null && gameObject.GetComponent<BuildingComplete>() != null;
		}
	}

	// Token: 0x060053E6 RID: 21478 RVA: 0x001E816F File Offset: 0x001E636F
	private SolidConduitFlow GetConduitFlow()
	{
		return Game.Instance.solidConduitFlow;
	}

	// Token: 0x060053E7 RID: 21479 RVA: 0x001E817C File Offset: 0x001E637C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.utilityCell = this.GetInputCell();
		ScenePartitionerLayer scenePartitionerLayer = GameScenePartitioner.Instance.objectLayers[20];
		this.partitionerEntry = GameScenePartitioner.Instance.Add("SolidConduitConsumer.OnSpawn", base.gameObject, this.utilityCell, scenePartitionerLayer, new Action<object>(this.OnConduitConnectionChanged));
		this.GetConduitFlow().AddConduitUpdater(new Action<float>(this.ConduitUpdate), ConduitFlowPriority.Default);
		this.OnConduitConnectionChanged(null);
	}

	// Token: 0x060053E8 RID: 21480 RVA: 0x001E81F6 File Offset: 0x001E63F6
	protected override void OnCleanUp()
	{
		this.GetConduitFlow().RemoveConduitUpdater(new Action<float>(this.ConduitUpdate));
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		base.OnCleanUp();
	}

	// Token: 0x060053E9 RID: 21481 RVA: 0x001E8225 File Offset: 0x001E6425
	private void OnConduitConnectionChanged(object data)
	{
		this.consuming = this.consuming && this.IsConnected;
		base.Trigger(-2094018600, this.IsConnected);
	}

	// Token: 0x060053EA RID: 21482 RVA: 0x001E8254 File Offset: 0x001E6454
	private void ConduitUpdate(float dt)
	{
		bool flag = false;
		SolidConduitFlow conduitFlow = this.GetConduitFlow();
		if (this.IsConnected)
		{
			SolidConduitFlow.ConduitContents contents = conduitFlow.GetContents(this.utilityCell);
			if (contents.pickupableHandle.IsValid() && (this.alwaysConsume || this.operational.IsOperational))
			{
				float num = ((this.capacityTag != GameTags.Any) ? this.storage.GetMassAvailable(this.capacityTag) : this.storage.MassStored());
				float num2 = Mathf.Min(this.storage.capacityKg, this.capacityKG);
				float num3 = Mathf.Max(0f, num2 - num);
				if (num3 > 0f)
				{
					Pickupable pickupable = conduitFlow.GetPickupable(contents.pickupableHandle);
					if (pickupable.PrimaryElement.Mass <= num3 || pickupable.PrimaryElement.Mass > num2)
					{
						Pickupable pickupable2 = conduitFlow.RemovePickupable(this.utilityCell);
						if (pickupable2)
						{
							this.storage.Store(pickupable2.gameObject, true, false, true, false);
							flag = true;
						}
					}
				}
			}
		}
		if (this.storage != null)
		{
			this.storage.storageNetworkID = this.GetConnectedNetworkID();
		}
		this.consuming = flag;
	}

	// Token: 0x060053EB RID: 21483 RVA: 0x001E8394 File Offset: 0x001E6594
	private int GetConnectedNetworkID()
	{
		GameObject gameObject = Grid.Objects[this.utilityCell, 20];
		SolidConduit solidConduit = ((gameObject != null) ? gameObject.GetComponent<SolidConduit>() : null);
		UtilityNetwork utilityNetwork = ((solidConduit != null) ? solidConduit.GetNetwork() : null);
		if (utilityNetwork == null)
		{
			return -1;
		}
		return utilityNetwork.id;
	}

	// Token: 0x060053EC RID: 21484 RVA: 0x001E83E8 File Offset: 0x001E65E8
	private int GetInputCell()
	{
		if (this.useSecondaryInput)
		{
			foreach (ISecondaryInput secondaryInput in base.GetComponents<ISecondaryInput>())
			{
				if (secondaryInput.HasSecondaryConduitType(ConduitType.Solid))
				{
					return Grid.OffsetCell(this.building.NaturalBuildingCell(), secondaryInput.GetSecondaryConduitOffset(ConduitType.Solid));
				}
			}
			return Grid.OffsetCell(this.building.NaturalBuildingCell(), CellOffset.none);
		}
		return this.building.GetUtilityInputCell();
	}

	// Token: 0x04003862 RID: 14434
	[SerializeField]
	public Tag capacityTag = GameTags.Any;

	// Token: 0x04003863 RID: 14435
	[SerializeField]
	public float capacityKG = float.PositiveInfinity;

	// Token: 0x04003864 RID: 14436
	[SerializeField]
	public bool alwaysConsume;

	// Token: 0x04003865 RID: 14437
	[SerializeField]
	public bool useSecondaryInput;

	// Token: 0x04003866 RID: 14438
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04003867 RID: 14439
	[MyCmpReq]
	private Building building;

	// Token: 0x04003868 RID: 14440
	[MyCmpGet]
	public Storage storage;

	// Token: 0x04003869 RID: 14441
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x0400386A RID: 14442
	private int utilityCell = -1;

	// Token: 0x0400386B RID: 14443
	private bool consuming;
}
