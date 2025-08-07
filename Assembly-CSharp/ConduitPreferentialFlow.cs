using System;
using UnityEngine;

// Token: 0x02000700 RID: 1792
[AddComponentMenu("KMonoBehaviour/scripts/ConduitPreferentialFlow")]
public class ConduitPreferentialFlow : KMonoBehaviour, ISecondaryInput
{
	// Token: 0x06002CD3 RID: 11475 RVA: 0x00101FB0 File Offset: 0x001001B0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Building component = base.GetComponent<Building>();
		this.inputCell = component.GetUtilityInputCell();
		this.outputCell = component.GetUtilityOutputCell();
		int num = Grid.PosToCell(base.transform.GetPosition());
		CellOffset rotatedOffset = component.GetRotatedOffset(this.portInfo.offset);
		int num2 = Grid.OffsetCell(num, rotatedOffset);
		Conduit.GetFlowManager(this.portInfo.conduitType).AddConduitUpdater(new Action<float>(this.ConduitUpdate), ConduitFlowPriority.Default);
		IUtilityNetworkMgr networkManager = Conduit.GetNetworkManager(this.portInfo.conduitType);
		this.secondaryInput = new FlowUtilityNetwork.NetworkItem(this.portInfo.conduitType, Endpoint.Sink, num2, base.gameObject);
		networkManager.AddToNetworks(this.secondaryInput.Cell, this.secondaryInput, true);
	}

	// Token: 0x06002CD4 RID: 11476 RVA: 0x00102074 File Offset: 0x00100274
	protected override void OnCleanUp()
	{
		Conduit.GetNetworkManager(this.portInfo.conduitType).RemoveFromNetworks(this.secondaryInput.Cell, this.secondaryInput, true);
		Conduit.GetFlowManager(this.portInfo.conduitType).RemoveConduitUpdater(new Action<float>(this.ConduitUpdate));
		base.OnCleanUp();
	}

	// Token: 0x06002CD5 RID: 11477 RVA: 0x001020D0 File Offset: 0x001002D0
	private void ConduitUpdate(float dt)
	{
		ConduitFlow flowManager = Conduit.GetFlowManager(this.portInfo.conduitType);
		if (!flowManager.HasConduit(this.outputCell))
		{
			return;
		}
		int cell = this.inputCell;
		ConduitFlow.ConduitContents conduitContents = flowManager.GetContents(cell);
		if (conduitContents.mass <= 0f)
		{
			cell = this.secondaryInput.Cell;
			conduitContents = flowManager.GetContents(cell);
		}
		if (conduitContents.mass > 0f)
		{
			float num = flowManager.AddElement(this.outputCell, conduitContents.element, conduitContents.mass, conduitContents.temperature, conduitContents.diseaseIdx, conduitContents.diseaseCount);
			if (num > 0f)
			{
				flowManager.RemoveElement(cell, num);
			}
		}
	}

	// Token: 0x06002CD6 RID: 11478 RVA: 0x00102179 File Offset: 0x00100379
	public bool HasSecondaryConduitType(ConduitType type)
	{
		return this.portInfo.conduitType == type;
	}

	// Token: 0x06002CD7 RID: 11479 RVA: 0x00102189 File Offset: 0x00100389
	public CellOffset GetSecondaryConduitOffset(ConduitType type)
	{
		if (this.portInfo.conduitType == type)
		{
			return this.portInfo.offset;
		}
		return CellOffset.none;
	}

	// Token: 0x04001A72 RID: 6770
	[SerializeField]
	public ConduitPortInfo portInfo;

	// Token: 0x04001A73 RID: 6771
	private int inputCell;

	// Token: 0x04001A74 RID: 6772
	private int outputCell;

	// Token: 0x04001A75 RID: 6773
	private FlowUtilityNetwork.NetworkItem secondaryInput;
}
