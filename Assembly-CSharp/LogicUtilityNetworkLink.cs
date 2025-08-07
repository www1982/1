using System;
using System.Collections.Generic;

// Token: 0x020009B9 RID: 2489
public class LogicUtilityNetworkLink : UtilityNetworkLink, IHaveUtilityNetworkMgr, IBridgedNetworkItem
{
	// Token: 0x0600489B RID: 18587 RVA: 0x001A355A File Offset: 0x001A175A
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x0600489C RID: 18588 RVA: 0x001A3562 File Offset: 0x001A1762
	protected override void OnConnect(int cell1, int cell2)
	{
		this.cell_one = cell1;
		this.cell_two = cell2;
		Game.Instance.logicCircuitSystem.AddLink(cell1, cell2);
		Game.Instance.logicCircuitManager.Connect(this);
	}

	// Token: 0x0600489D RID: 18589 RVA: 0x001A3593 File Offset: 0x001A1793
	protected override void OnDisconnect(int cell1, int cell2)
	{
		Game.Instance.logicCircuitSystem.RemoveLink(cell1, cell2);
		Game.Instance.logicCircuitManager.Disconnect(this);
	}

	// Token: 0x0600489E RID: 18590 RVA: 0x001A35B6 File Offset: 0x001A17B6
	public IUtilityNetworkMgr GetNetworkManager()
	{
		return Game.Instance.logicCircuitSystem;
	}

	// Token: 0x0600489F RID: 18591 RVA: 0x001A35C4 File Offset: 0x001A17C4
	public void AddNetworks(ICollection<UtilityNetwork> networks)
	{
		int networkCell = base.GetNetworkCell();
		UtilityNetwork networkForCell = this.GetNetworkManager().GetNetworkForCell(networkCell);
		if (networkForCell != null)
		{
			networks.Add(networkForCell);
		}
	}

	// Token: 0x060048A0 RID: 18592 RVA: 0x001A35F0 File Offset: 0x001A17F0
	public bool IsConnectedToNetworks(ICollection<UtilityNetwork> networks)
	{
		int networkCell = base.GetNetworkCell();
		UtilityNetwork networkForCell = this.GetNetworkManager().GetNetworkForCell(networkCell);
		return networks.Contains(networkForCell);
	}

	// Token: 0x04002FE5 RID: 12261
	public LogicWire.BitDepth bitDepth;

	// Token: 0x04002FE6 RID: 12262
	public int cell_one;

	// Token: 0x04002FE7 RID: 12263
	public int cell_two;
}
