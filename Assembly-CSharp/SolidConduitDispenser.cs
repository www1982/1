using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000B1F RID: 2847
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/SolidConduitDispenser")]
public class SolidConduitDispenser : KMonoBehaviour, ISaveLoadable, IConduitDispenser
{
	// Token: 0x170005E6 RID: 1510
	// (get) Token: 0x060053EE RID: 21486 RVA: 0x001E847D File Offset: 0x001E667D
	public Storage Storage
	{
		get
		{
			return this.storage;
		}
	}

	// Token: 0x170005E7 RID: 1511
	// (get) Token: 0x060053EF RID: 21487 RVA: 0x001E8485 File Offset: 0x001E6685
	public ConduitType ConduitType
	{
		get
		{
			return ConduitType.Solid;
		}
	}

	// Token: 0x170005E8 RID: 1512
	// (get) Token: 0x060053F0 RID: 21488 RVA: 0x001E8488 File Offset: 0x001E6688
	public SolidConduitFlow.ConduitContents ConduitContents
	{
		get
		{
			return this.GetConduitFlow().GetContents(this.utilityCell);
		}
	}

	// Token: 0x170005E9 RID: 1513
	// (get) Token: 0x060053F1 RID: 21489 RVA: 0x001E849B File Offset: 0x001E669B
	public bool IsDispensing
	{
		get
		{
			return this.dispensing;
		}
	}

	// Token: 0x060053F2 RID: 21490 RVA: 0x001E84A3 File Offset: 0x001E66A3
	public SolidConduitFlow GetConduitFlow()
	{
		return Game.Instance.solidConduitFlow;
	}

	// Token: 0x060053F3 RID: 21491 RVA: 0x001E84B0 File Offset: 0x001E66B0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.utilityCell = this.GetOutputCell();
		ScenePartitionerLayer scenePartitionerLayer = GameScenePartitioner.Instance.objectLayers[20];
		this.partitionerEntry = GameScenePartitioner.Instance.Add("SolidConduitConsumer.OnSpawn", base.gameObject, this.utilityCell, scenePartitionerLayer, new Action<object>(this.OnConduitConnectionChanged));
		this.GetConduitFlow().AddConduitUpdater(new Action<float>(this.ConduitUpdate), ConduitFlowPriority.Dispense);
		this.OnConduitConnectionChanged(null);
	}

	// Token: 0x060053F4 RID: 21492 RVA: 0x001E852B File Offset: 0x001E672B
	protected override void OnCleanUp()
	{
		this.GetConduitFlow().RemoveConduitUpdater(new Action<float>(this.ConduitUpdate));
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		base.OnCleanUp();
	}

	// Token: 0x060053F5 RID: 21493 RVA: 0x001E855A File Offset: 0x001E675A
	private void OnConduitConnectionChanged(object data)
	{
		this.dispensing = this.dispensing && this.IsConnected;
		base.Trigger(-2094018600, this.IsConnected);
	}

	// Token: 0x060053F6 RID: 21494 RVA: 0x001E858C File Offset: 0x001E678C
	private void ConduitUpdate(float dt)
	{
		bool flag = false;
		this.operational.SetFlag(SolidConduitDispenser.outputConduitFlag, this.IsConnected);
		if (this.operational.IsOperational || this.alwaysDispense)
		{
			SolidConduitFlow conduitFlow = this.GetConduitFlow();
			if (conduitFlow.HasConduit(this.utilityCell) && conduitFlow.IsConduitEmpty(this.utilityCell))
			{
				Pickupable pickupable = this.FindSuitableItem();
				if (pickupable)
				{
					if (pickupable.PrimaryElement.Mass > 20f)
					{
						pickupable = pickupable.Take(Mathf.Max(20f, pickupable.PrimaryElement.MassPerUnit));
					}
					conduitFlow.AddPickupable(this.utilityCell, pickupable);
					flag = true;
				}
			}
		}
		this.storage.storageNetworkID = this.GetConnectedNetworkID();
		this.dispensing = flag;
	}

	// Token: 0x060053F7 RID: 21495 RVA: 0x001E8650 File Offset: 0x001E6850
	private bool isSolid(GameObject o)
	{
		PrimaryElement component = o.GetComponent<PrimaryElement>();
		return (component != null && component.Element.IsSolid) || Assets.GetPrefab(o.name) != null;
	}

	// Token: 0x060053F8 RID: 21496 RVA: 0x001E8694 File Offset: 0x001E6894
	private Pickupable FindSuitableItem()
	{
		List<GameObject> items = this.storage.items;
		if (items.Count < 1)
		{
			return null;
		}
		this.round_robin_index %= items.Count;
		GameObject gameObject = items[this.round_robin_index];
		this.round_robin_index++;
		if (this.solidOnly && !this.isSolid(gameObject))
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < items.Count)
			{
				gameObject = items[(this.round_robin_index + num) % items.Count];
				if (this.isSolid(gameObject))
				{
					flag = true;
				}
				num++;
			}
			if (!flag)
			{
				return null;
			}
		}
		if (!gameObject)
		{
			return null;
		}
		return gameObject.GetComponent<Pickupable>();
	}

	// Token: 0x170005EA RID: 1514
	// (get) Token: 0x060053F9 RID: 21497 RVA: 0x001E8744 File Offset: 0x001E6944
	public bool IsConnected
	{
		get
		{
			GameObject gameObject = Grid.Objects[this.utilityCell, 20];
			return gameObject != null && gameObject.GetComponent<BuildingComplete>() != null;
		}
	}

	// Token: 0x060053FA RID: 21498 RVA: 0x001E877C File Offset: 0x001E697C
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

	// Token: 0x060053FB RID: 21499 RVA: 0x001E87D0 File Offset: 0x001E69D0
	private int GetOutputCell()
	{
		Building component = base.GetComponent<Building>();
		if (this.useSecondaryOutput)
		{
			foreach (ISecondaryOutput secondaryOutput in base.GetComponents<ISecondaryOutput>())
			{
				if (secondaryOutput.HasSecondaryConduitType(ConduitType.Solid))
				{
					return Grid.OffsetCell(component.NaturalBuildingCell(), secondaryOutput.GetSecondaryConduitOffset(ConduitType.Solid));
				}
			}
			return Grid.OffsetCell(component.NaturalBuildingCell(), CellOffset.none);
		}
		return component.GetUtilityOutputCell();
	}

	// Token: 0x0400386C RID: 14444
	[SerializeField]
	public SimHashes[] elementFilter;

	// Token: 0x0400386D RID: 14445
	[SerializeField]
	public bool invertElementFilter;

	// Token: 0x0400386E RID: 14446
	[SerializeField]
	public bool alwaysDispense;

	// Token: 0x0400386F RID: 14447
	[SerializeField]
	public bool useSecondaryOutput;

	// Token: 0x04003870 RID: 14448
	[SerializeField]
	public bool solidOnly;

	// Token: 0x04003871 RID: 14449
	private static readonly Operational.Flag outputConduitFlag = new Operational.Flag("output_conduit", Operational.Flag.Type.Functional);

	// Token: 0x04003872 RID: 14450
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04003873 RID: 14451
	[MyCmpReq]
	public Storage storage;

	// Token: 0x04003874 RID: 14452
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04003875 RID: 14453
	private int utilityCell = -1;

	// Token: 0x04003876 RID: 14454
	private bool dispensing;

	// Token: 0x04003877 RID: 14455
	private int round_robin_index;
}
