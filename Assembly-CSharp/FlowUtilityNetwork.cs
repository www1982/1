using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000BCB RID: 3019
public class FlowUtilityNetwork : UtilityNetwork
{
	// Token: 0x1700068F RID: 1679
	// (get) Token: 0x06005A68 RID: 23144 RVA: 0x0020B062 File Offset: 0x00209262
	public bool HasSinks
	{
		get
		{
			return this.sinks.Count > 0;
		}
	}

	// Token: 0x06005A69 RID: 23145 RVA: 0x0020B072 File Offset: 0x00209272
	public int GetActiveCount()
	{
		return this.sinks.Count;
	}

	// Token: 0x06005A6A RID: 23146 RVA: 0x0020B080 File Offset: 0x00209280
	public override void AddItem(object generic_item)
	{
		FlowUtilityNetwork.IItem item = (FlowUtilityNetwork.IItem)generic_item;
		if (item != null)
		{
			switch (item.EndpointType)
			{
			case Endpoint.Source:
				if (this.sources.Contains(item))
				{
					return;
				}
				this.sources.Add(item);
				item.Network = this;
				return;
			case Endpoint.Sink:
				if (this.sinks.Contains(item))
				{
					return;
				}
				this.sinks.Add(item);
				item.Network = this;
				return;
			case Endpoint.Conduit:
				this.conduitCount++;
				return;
			default:
				item.Network = this;
				break;
			}
		}
	}

	// Token: 0x06005A6B RID: 23147 RVA: 0x0020B110 File Offset: 0x00209310
	public override void Reset(UtilityNetworkGridNode[] grid)
	{
		for (int i = 0; i < this.sinks.Count; i++)
		{
			FlowUtilityNetwork.IItem item = this.sinks[i];
			item.Network = null;
			UtilityNetworkGridNode utilityNetworkGridNode = grid[item.Cell];
			utilityNetworkGridNode.networkIdx = -1;
			grid[item.Cell] = utilityNetworkGridNode;
		}
		for (int j = 0; j < this.sources.Count; j++)
		{
			FlowUtilityNetwork.IItem item2 = this.sources[j];
			item2.Network = null;
			UtilityNetworkGridNode utilityNetworkGridNode2 = grid[item2.Cell];
			utilityNetworkGridNode2.networkIdx = -1;
			grid[item2.Cell] = utilityNetworkGridNode2;
		}
		this.conduitCount = 0;
		for (int k = 0; k < this.conduits.Count; k++)
		{
			FlowUtilityNetwork.IItem item3 = this.conduits[k];
			item3.Network = null;
			UtilityNetworkGridNode utilityNetworkGridNode3 = grid[item3.Cell];
			utilityNetworkGridNode3.networkIdx = -1;
			grid[item3.Cell] = utilityNetworkGridNode3;
		}
	}

	// Token: 0x04003C02 RID: 15362
	public List<FlowUtilityNetwork.IItem> sources = new List<FlowUtilityNetwork.IItem>();

	// Token: 0x04003C03 RID: 15363
	public List<FlowUtilityNetwork.IItem> sinks = new List<FlowUtilityNetwork.IItem>();

	// Token: 0x04003C04 RID: 15364
	public List<FlowUtilityNetwork.IItem> conduits = new List<FlowUtilityNetwork.IItem>();

	// Token: 0x04003C05 RID: 15365
	public int conduitCount;

	// Token: 0x02001CFE RID: 7422
	public interface IItem
	{
		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x0600ACBA RID: 44218
		int Cell { get; }

		// Token: 0x17000BEC RID: 3052
		// (set) Token: 0x0600ACBB RID: 44219
		FlowUtilityNetwork Network { set; }

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x0600ACBC RID: 44220
		Endpoint EndpointType { get; }

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x0600ACBD RID: 44221
		ConduitType ConduitType { get; }

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x0600ACBE RID: 44222
		GameObject GameObject { get; }
	}

	// Token: 0x02001CFF RID: 7423
	public class NetworkItem : FlowUtilityNetwork.IItem
	{
		// Token: 0x0600ACBF RID: 44223 RVA: 0x003C2929 File Offset: 0x003C0B29
		public NetworkItem(ConduitType conduit_type, Endpoint endpoint_type, int cell, GameObject parent)
		{
			this.conduitType = conduit_type;
			this.endpointType = endpoint_type;
			this.cell = cell;
			this.parent = parent;
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x0600ACC0 RID: 44224 RVA: 0x003C294E File Offset: 0x003C0B4E
		public Endpoint EndpointType
		{
			get
			{
				return this.endpointType;
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x0600ACC1 RID: 44225 RVA: 0x003C2956 File Offset: 0x003C0B56
		public ConduitType ConduitType
		{
			get
			{
				return this.conduitType;
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x0600ACC2 RID: 44226 RVA: 0x003C295E File Offset: 0x003C0B5E
		public int Cell
		{
			get
			{
				return this.cell;
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x0600ACC3 RID: 44227 RVA: 0x003C2966 File Offset: 0x003C0B66
		// (set) Token: 0x0600ACC4 RID: 44228 RVA: 0x003C296E File Offset: 0x003C0B6E
		public FlowUtilityNetwork Network
		{
			get
			{
				return this.network;
			}
			set
			{
				this.network = value;
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x0600ACC5 RID: 44229 RVA: 0x003C2977 File Offset: 0x003C0B77
		public GameObject GameObject
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x040087E0 RID: 34784
		private int cell;

		// Token: 0x040087E1 RID: 34785
		private FlowUtilityNetwork network;

		// Token: 0x040087E2 RID: 34786
		private Endpoint endpointType;

		// Token: 0x040087E3 RID: 34787
		private ConduitType conduitType;

		// Token: 0x040087E4 RID: 34788
		private GameObject parent;
	}
}
