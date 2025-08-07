using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000BE3 RID: 3043
public class WireUtilityNetworkLink : UtilityNetworkLink, IWattageRating, IHaveUtilityNetworkMgr, IBridgedNetworkItem, ICircuitConnected
{
	// Token: 0x06005B34 RID: 23348 RVA: 0x0020F320 File Offset: 0x0020D520
	public Wire.WattageRating GetMaxWattageRating()
	{
		return this.maxWattageRating;
	}

	// Token: 0x06005B35 RID: 23349 RVA: 0x0020F328 File Offset: 0x0020D528
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x06005B36 RID: 23350 RVA: 0x0020F330 File Offset: 0x0020D530
	protected override void OnDisconnect(int cell1, int cell2)
	{
		Game.Instance.electricalConduitSystem.RemoveLink(cell1, cell2);
		Game.Instance.circuitManager.Disconnect(this);
	}

	// Token: 0x06005B37 RID: 23351 RVA: 0x0020F353 File Offset: 0x0020D553
	protected override void OnConnect(int cell1, int cell2)
	{
		Game.Instance.electricalConduitSystem.AddLink(cell1, cell2);
		Game.Instance.circuitManager.Connect(this);
	}

	// Token: 0x06005B38 RID: 23352 RVA: 0x0020F376 File Offset: 0x0020D576
	public IUtilityNetworkMgr GetNetworkManager()
	{
		return Game.Instance.electricalConduitSystem;
	}

	// Token: 0x17000695 RID: 1685
	// (get) Token: 0x06005B39 RID: 23353 RVA: 0x0020F382 File Offset: 0x0020D582
	// (set) Token: 0x06005B3A RID: 23354 RVA: 0x0020F38A File Offset: 0x0020D58A
	public bool IsVirtual { get; private set; }

	// Token: 0x17000696 RID: 1686
	// (get) Token: 0x06005B3B RID: 23355 RVA: 0x0020F393 File Offset: 0x0020D593
	public int PowerCell
	{
		get
		{
			return base.GetNetworkCell();
		}
	}

	// Token: 0x17000697 RID: 1687
	// (get) Token: 0x06005B3C RID: 23356 RVA: 0x0020F39B File Offset: 0x0020D59B
	// (set) Token: 0x06005B3D RID: 23357 RVA: 0x0020F3A3 File Offset: 0x0020D5A3
	public object VirtualCircuitKey { get; private set; }

	// Token: 0x06005B3E RID: 23358 RVA: 0x0020F3AC File Offset: 0x0020D5AC
	public void AddNetworks(ICollection<UtilityNetwork> networks)
	{
		int networkCell = base.GetNetworkCell();
		UtilityNetwork networkForCell = this.GetNetworkManager().GetNetworkForCell(networkCell);
		if (networkForCell != null)
		{
			networks.Add(networkForCell);
		}
	}

	// Token: 0x06005B3F RID: 23359 RVA: 0x0020F3D8 File Offset: 0x0020D5D8
	public bool IsConnectedToNetworks(ICollection<UtilityNetwork> networks)
	{
		int networkCell = base.GetNetworkCell();
		UtilityNetwork networkForCell = this.GetNetworkManager().GetNetworkForCell(networkCell);
		return networks.Contains(networkForCell);
	}

	// Token: 0x04003C8B RID: 15499
	[SerializeField]
	public Wire.WattageRating maxWattageRating;
}
