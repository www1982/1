using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x0200083C RID: 2108
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/ConduitDispenser")]
public class ConduitDispenser : KMonoBehaviour, ISaveLoadable, IConduitDispenser
{
	// Token: 0x170003F8 RID: 1016
	// (get) Token: 0x060039BF RID: 14783 RVA: 0x00140C2F File Offset: 0x0013EE2F
	public Storage Storage
	{
		get
		{
			return this.storage;
		}
	}

	// Token: 0x170003F9 RID: 1017
	// (get) Token: 0x060039C0 RID: 14784 RVA: 0x00140C37 File Offset: 0x0013EE37
	public ConduitType ConduitType
	{
		get
		{
			return this.conduitType;
		}
	}

	// Token: 0x170003FA RID: 1018
	// (get) Token: 0x060039C1 RID: 14785 RVA: 0x00140C3F File Offset: 0x0013EE3F
	public ConduitFlow.ConduitContents ConduitContents
	{
		get
		{
			return this.GetConduitManager().GetContents(this.utilityCell);
		}
	}

	// Token: 0x060039C2 RID: 14786 RVA: 0x00140C52 File Offset: 0x0013EE52
	public void SetConduitData(ConduitType type)
	{
		this.conduitType = type;
	}

	// Token: 0x060039C3 RID: 14787 RVA: 0x00140C5C File Offset: 0x0013EE5C
	public ConduitFlow GetConduitManager()
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

	// Token: 0x060039C4 RID: 14788 RVA: 0x00140C91 File Offset: 0x0013EE91
	private void OnConduitConnectionChanged(object data)
	{
		base.Trigger(-2094018600, this.IsConnected);
	}

	// Token: 0x060039C5 RID: 14789 RVA: 0x00140CAC File Offset: 0x0013EEAC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		GameScheduler.Instance.Schedule("PlumbingTutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Plumbing, true);
		}, null, null);
		ConduitFlow conduitManager = this.GetConduitManager();
		this.utilityCell = this.GetOutputCell(conduitManager.conduitType);
		ScenePartitionerLayer scenePartitionerLayer = GameScenePartitioner.Instance.objectLayers[(this.conduitType == ConduitType.Gas) ? 12 : 16];
		this.partitionerEntry = GameScenePartitioner.Instance.Add("ConduitConsumer.OnSpawn", base.gameObject, this.utilityCell, scenePartitionerLayer, new Action<object>(this.OnConduitConnectionChanged));
		this.GetConduitManager().AddConduitUpdater(new Action<float>(this.ConduitUpdate), ConduitFlowPriority.Dispense);
		this.OnConduitConnectionChanged(null);
	}

	// Token: 0x060039C6 RID: 14790 RVA: 0x00140D77 File Offset: 0x0013EF77
	protected override void OnCleanUp()
	{
		this.GetConduitManager().RemoveConduitUpdater(new Action<float>(this.ConduitUpdate));
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		base.OnCleanUp();
	}

	// Token: 0x060039C7 RID: 14791 RVA: 0x00140DA6 File Offset: 0x0013EFA6
	public void SetOnState(bool onState)
	{
		this.isOn = onState;
	}

	// Token: 0x060039C8 RID: 14792 RVA: 0x00140DAF File Offset: 0x0013EFAF
	private void ConduitUpdate(float dt)
	{
		if (this.operational != null)
		{
			this.operational.SetFlag(ConduitDispenser.outputConduitFlag, this.IsConnected);
		}
		this.blocked = false;
		if (this.isOn)
		{
			this.Dispense(dt);
		}
	}

	// Token: 0x060039C9 RID: 14793 RVA: 0x00140DEC File Offset: 0x0013EFEC
	private void Dispense(float dt)
	{
		if ((this.operational != null && this.operational.IsOperational) || this.alwaysDispense)
		{
			if (this.building != null && this.building.Def.CanMove)
			{
				this.utilityCell = this.GetOutputCell(this.GetConduitManager().conduitType);
			}
			PrimaryElement primaryElement = this.FindSuitableElement();
			if (primaryElement != null)
			{
				primaryElement.KeepZeroMassObject = true;
				this.empty = false;
				float num = this.GetConduitManager().AddElement(this.utilityCell, primaryElement.ElementID, primaryElement.Mass, primaryElement.Temperature, primaryElement.DiseaseIdx, primaryElement.DiseaseCount);
				if (num > 0f)
				{
					int num2 = (int)(num / primaryElement.Mass * (float)primaryElement.DiseaseCount);
					primaryElement.ModifyDiseaseCount(-num2, "ConduitDispenser.ConduitUpdate");
					primaryElement.Mass -= num;
					this.storage.Trigger(-1697596308, primaryElement.gameObject);
					return;
				}
				this.blocked = true;
				return;
			}
			else
			{
				this.empty = true;
			}
		}
	}

	// Token: 0x060039CA RID: 14794 RVA: 0x00140F04 File Offset: 0x0013F104
	private PrimaryElement FindSuitableElement()
	{
		List<GameObject> items = this.storage.items;
		int count = items.Count;
		for (int i = 0; i < count; i++)
		{
			int num = (i + this.elementOutputOffset) % count;
			PrimaryElement component = items[num].GetComponent<PrimaryElement>();
			if (component != null && component.Mass > 0f && ((this.conduitType == ConduitType.Liquid) ? component.Element.IsLiquid : component.Element.IsGas) && (this.elementFilter == null || this.elementFilter.Length == 0 || (!this.invertElementFilter && this.IsFilteredElement(component.ElementID)) || (this.invertElementFilter && !this.IsFilteredElement(component.ElementID))))
			{
				this.elementOutputOffset = (this.elementOutputOffset + 1) % count;
				return component;
			}
		}
		return null;
	}

	// Token: 0x060039CB RID: 14795 RVA: 0x00140FE4 File Offset: 0x0013F1E4
	private bool IsFilteredElement(SimHashes element)
	{
		for (int num = 0; num != this.elementFilter.Length; num++)
		{
			if (this.elementFilter[num] == element)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x170003FB RID: 1019
	// (get) Token: 0x060039CC RID: 14796 RVA: 0x00141014 File Offset: 0x0013F214
	public bool IsConnected
	{
		get
		{
			GameObject gameObject = Grid.Objects[this.utilityCell, (this.conduitType == ConduitType.Gas) ? 12 : 16];
			return gameObject != null && gameObject.GetComponent<BuildingComplete>() != null;
		}
	}

	// Token: 0x060039CD RID: 14797 RVA: 0x00141058 File Offset: 0x0013F258
	private int GetOutputCell(ConduitType outputConduitType)
	{
		Building component = base.GetComponent<Building>();
		if (!(component != null))
		{
			return Grid.OffsetCell(Grid.PosToCell(this), this.noBuildingOutputCellOffset);
		}
		if (this.useSecondaryOutput)
		{
			ISecondaryOutput[] components = base.GetComponents<ISecondaryOutput>();
			foreach (ISecondaryOutput secondaryOutput in components)
			{
				if (secondaryOutput.HasSecondaryConduitType(outputConduitType))
				{
					return Grid.OffsetCell(component.NaturalBuildingCell(), secondaryOutput.GetSecondaryConduitOffset(outputConduitType));
				}
			}
			return Grid.OffsetCell(component.NaturalBuildingCell(), components[0].GetSecondaryConduitOffset(outputConduitType));
		}
		return component.GetUtilityOutputCell();
	}

	// Token: 0x0400237F RID: 9087
	[SerializeField]
	public ConduitType conduitType;

	// Token: 0x04002380 RID: 9088
	[SerializeField]
	public SimHashes[] elementFilter;

	// Token: 0x04002381 RID: 9089
	[SerializeField]
	public bool invertElementFilter;

	// Token: 0x04002382 RID: 9090
	[SerializeField]
	public bool alwaysDispense;

	// Token: 0x04002383 RID: 9091
	[SerializeField]
	public bool isOn = true;

	// Token: 0x04002384 RID: 9092
	[SerializeField]
	public bool blocked;

	// Token: 0x04002385 RID: 9093
	[SerializeField]
	public bool empty = true;

	// Token: 0x04002386 RID: 9094
	[SerializeField]
	public bool useSecondaryOutput;

	// Token: 0x04002387 RID: 9095
	[SerializeField]
	public CellOffset noBuildingOutputCellOffset;

	// Token: 0x04002388 RID: 9096
	private static readonly Operational.Flag outputConduitFlag = new Operational.Flag("output_conduit", Operational.Flag.Type.Functional);

	// Token: 0x04002389 RID: 9097
	[MyCmpGet]
	private Operational operational;

	// Token: 0x0400238A RID: 9098
	[MyCmpReq]
	public Storage storage;

	// Token: 0x0400238B RID: 9099
	[MyCmpGet]
	private Building building;

	// Token: 0x0400238C RID: 9100
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x0400238D RID: 9101
	private int utilityCell = -1;

	// Token: 0x0400238E RID: 9102
	private int elementOutputOffset;
}
