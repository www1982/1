using System;
using UnityEngine;

// Token: 0x020006FF RID: 1791
[AddComponentMenu("KMonoBehaviour/scripts/ConduitOverflow")]
public class ConduitOverflow : KMonoBehaviour, ISecondaryOutput
{
	// Token: 0x06002CCD RID: 11469 RVA: 0x00101DA0 File Offset: 0x000FFFA0
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
		this.secondaryOutput = new FlowUtilityNetwork.NetworkItem(this.portInfo.conduitType, Endpoint.Sink, num2, base.gameObject);
		networkManager.AddToNetworks(this.secondaryOutput.Cell, this.secondaryOutput, true);
	}

	// Token: 0x06002CCE RID: 11470 RVA: 0x00101E64 File Offset: 0x00100064
	protected override void OnCleanUp()
	{
		Conduit.GetNetworkManager(this.portInfo.conduitType).RemoveFromNetworks(this.secondaryOutput.Cell, this.secondaryOutput, true);
		Conduit.GetFlowManager(this.portInfo.conduitType).RemoveConduitUpdater(new Action<float>(this.ConduitUpdate));
		base.OnCleanUp();
	}

	// Token: 0x06002CCF RID: 11471 RVA: 0x00101EC0 File Offset: 0x001000C0
	private void ConduitUpdate(float dt)
	{
		ConduitFlow flowManager = Conduit.GetFlowManager(this.portInfo.conduitType);
		if (!flowManager.HasConduit(this.inputCell))
		{
			return;
		}
		ConduitFlow.ConduitContents contents = flowManager.GetContents(this.inputCell);
		if (contents.mass <= 0f)
		{
			return;
		}
		int cell = this.outputCell;
		ConduitFlow.ConduitContents conduitContents = flowManager.GetContents(cell);
		if (conduitContents.mass > 0f)
		{
			cell = this.secondaryOutput.Cell;
			conduitContents = flowManager.GetContents(cell);
		}
		if (conduitContents.mass <= 0f)
		{
			float num = flowManager.AddElement(cell, contents.element, contents.mass, contents.temperature, contents.diseaseIdx, contents.diseaseCount);
			if (num > 0f)
			{
				flowManager.RemoveElement(this.inputCell, num);
			}
		}
	}

	// Token: 0x06002CD0 RID: 11472 RVA: 0x00101F88 File Offset: 0x00100188
	public bool HasSecondaryConduitType(ConduitType type)
	{
		return this.portInfo.conduitType == type;
	}

	// Token: 0x06002CD1 RID: 11473 RVA: 0x00101F98 File Offset: 0x00100198
	public CellOffset GetSecondaryConduitOffset(ConduitType type)
	{
		return this.portInfo.offset;
	}

	// Token: 0x04001A6E RID: 6766
	[SerializeField]
	public ConduitPortInfo portInfo;

	// Token: 0x04001A6F RID: 6767
	private int inputCell;

	// Token: 0x04001A70 RID: 6768
	private int outputCell;

	// Token: 0x04001A71 RID: 6769
	private FlowUtilityNetwork.NetworkItem secondaryOutput;
}
