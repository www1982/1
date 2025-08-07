using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000BE4 RID: 3044
public class WireUtilitySemiVirtualNetworkLink : UtilityNetworkLink, IHaveUtilityNetworkMgr, ICircuitConnected
{
	// Token: 0x06005B41 RID: 23361 RVA: 0x0020F408 File Offset: 0x0020D608
	public Wire.WattageRating GetMaxWattageRating()
	{
		return this.maxWattageRating;
	}

	// Token: 0x06005B42 RID: 23362 RVA: 0x0020F410 File Offset: 0x0020D610
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06005B43 RID: 23363 RVA: 0x0020F418 File Offset: 0x0020D618
	protected override void OnSpawn()
	{
		RocketModuleCluster component = base.GetComponent<RocketModuleCluster>();
		if (component != null)
		{
			this.VirtualCircuitKey = component.CraftInterface;
		}
		else
		{
			CraftModuleInterface component2 = this.GetMyWorld().GetComponent<CraftModuleInterface>();
			if (component2 != null)
			{
				this.VirtualCircuitKey = component2;
			}
		}
		Game.Instance.electricalConduitSystem.AddToVirtualNetworks(this.VirtualCircuitKey, this, true);
		base.OnSpawn();
	}

	// Token: 0x06005B44 RID: 23364 RVA: 0x0020F47C File Offset: 0x0020D67C
	public void SetLinkConnected(bool connect)
	{
		if (connect && this.visualizeOnly)
		{
			this.visualizeOnly = false;
			if (base.isSpawned)
			{
				base.Connect();
				return;
			}
		}
		else if (!connect && !this.visualizeOnly)
		{
			if (base.isSpawned)
			{
				base.Disconnect();
			}
			this.visualizeOnly = true;
		}
	}

	// Token: 0x06005B45 RID: 23365 RVA: 0x0020F4CA File Offset: 0x0020D6CA
	protected override void OnDisconnect(int cell1, int cell2)
	{
		Game.Instance.electricalConduitSystem.RemoveSemiVirtualLink(cell1, this.VirtualCircuitKey);
	}

	// Token: 0x06005B46 RID: 23366 RVA: 0x0020F4E2 File Offset: 0x0020D6E2
	protected override void OnConnect(int cell1, int cell2)
	{
		Game.Instance.electricalConduitSystem.AddSemiVirtualLink(cell1, this.VirtualCircuitKey);
	}

	// Token: 0x06005B47 RID: 23367 RVA: 0x0020F4FA File Offset: 0x0020D6FA
	public IUtilityNetworkMgr GetNetworkManager()
	{
		return Game.Instance.electricalConduitSystem;
	}

	// Token: 0x17000698 RID: 1688
	// (get) Token: 0x06005B48 RID: 23368 RVA: 0x0020F506 File Offset: 0x0020D706
	// (set) Token: 0x06005B49 RID: 23369 RVA: 0x0020F50E File Offset: 0x0020D70E
	public bool IsVirtual { get; private set; }

	// Token: 0x17000699 RID: 1689
	// (get) Token: 0x06005B4A RID: 23370 RVA: 0x0020F517 File Offset: 0x0020D717
	public int PowerCell
	{
		get
		{
			return base.GetNetworkCell();
		}
	}

	// Token: 0x1700069A RID: 1690
	// (get) Token: 0x06005B4B RID: 23371 RVA: 0x0020F51F File Offset: 0x0020D71F
	// (set) Token: 0x06005B4C RID: 23372 RVA: 0x0020F527 File Offset: 0x0020D727
	public object VirtualCircuitKey { get; private set; }

	// Token: 0x06005B4D RID: 23373 RVA: 0x0020F530 File Offset: 0x0020D730
	public void AddNetworks(ICollection<UtilityNetwork> networks)
	{
		int networkCell = base.GetNetworkCell();
		UtilityNetwork networkForCell = this.GetNetworkManager().GetNetworkForCell(networkCell);
		if (networkForCell != null)
		{
			networks.Add(networkForCell);
		}
	}

	// Token: 0x06005B4E RID: 23374 RVA: 0x0020F55C File Offset: 0x0020D75C
	public bool IsConnectedToNetworks(ICollection<UtilityNetwork> networks)
	{
		int networkCell = base.GetNetworkCell();
		UtilityNetwork networkForCell = this.GetNetworkManager().GetNetworkForCell(networkCell);
		return networks.Contains(networkForCell);
	}

	// Token: 0x04003C8E RID: 15502
	[SerializeField]
	public Wire.WattageRating maxWattageRating;
}
