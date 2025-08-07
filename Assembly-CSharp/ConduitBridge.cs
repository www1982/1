using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006FD RID: 1789
[AddComponentMenu("KMonoBehaviour/scripts/ConduitBridge")]
public class ConduitBridge : ConduitBridgeBase, IBridgedNetworkItem
{
	// Token: 0x06002CC3 RID: 11459 RVA: 0x00101AE1 File Offset: 0x000FFCE1
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.accumulator = Game.Instance.accumulators.Add("Flow", this);
	}

	// Token: 0x06002CC4 RID: 11460 RVA: 0x00101B04 File Offset: 0x000FFD04
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Building component = base.GetComponent<Building>();
		this.inputCell = component.GetUtilityInputCell();
		this.outputCell = component.GetUtilityOutputCell();
		Conduit.GetFlowManager(this.type).AddConduitUpdater(new Action<float>(this.ConduitUpdate), ConduitFlowPriority.Default);
	}

	// Token: 0x06002CC5 RID: 11461 RVA: 0x00101B53 File Offset: 0x000FFD53
	protected override void OnCleanUp()
	{
		Conduit.GetFlowManager(this.type).RemoveConduitUpdater(new Action<float>(this.ConduitUpdate));
		Game.Instance.accumulators.Remove(this.accumulator);
		base.OnCleanUp();
	}

	// Token: 0x06002CC6 RID: 11462 RVA: 0x00101B90 File Offset: 0x000FFD90
	private void ConduitUpdate(float dt)
	{
		ConduitFlow flowManager = Conduit.GetFlowManager(this.type);
		if (!flowManager.HasConduit(this.inputCell) || !flowManager.HasConduit(this.outputCell))
		{
			base.SendEmptyOnMassTransfer();
			return;
		}
		ConduitFlow.ConduitContents contents = flowManager.GetContents(this.inputCell);
		float num = contents.mass;
		if (this.desiredMassTransfer != null)
		{
			num = this.desiredMassTransfer(dt, contents.element, contents.mass, contents.temperature, contents.diseaseIdx, contents.diseaseCount, null);
		}
		if (num > 0f)
		{
			int num2 = (int)(num / contents.mass * (float)contents.diseaseCount);
			float num3 = flowManager.AddElement(this.outputCell, contents.element, num, contents.temperature, contents.diseaseIdx, num2);
			if (num3 <= 0f)
			{
				base.SendEmptyOnMassTransfer();
				return;
			}
			flowManager.RemoveElement(this.inputCell, num3);
			Game.Instance.accumulators.Accumulate(this.accumulator, contents.mass);
			if (this.OnMassTransfer != null)
			{
				this.OnMassTransfer(contents.element, num3, contents.temperature, contents.diseaseIdx, num2, null);
				return;
			}
		}
		else
		{
			base.SendEmptyOnMassTransfer();
		}
	}

	// Token: 0x06002CC7 RID: 11463 RVA: 0x00101CC4 File Offset: 0x000FFEC4
	public void AddNetworks(ICollection<UtilityNetwork> networks)
	{
		IUtilityNetworkMgr networkManager = Conduit.GetNetworkManager(this.type);
		UtilityNetwork utilityNetwork = networkManager.GetNetworkForCell(this.inputCell);
		if (utilityNetwork != null)
		{
			networks.Add(utilityNetwork);
		}
		utilityNetwork = networkManager.GetNetworkForCell(this.outputCell);
		if (utilityNetwork != null)
		{
			networks.Add(utilityNetwork);
		}
	}

	// Token: 0x06002CC8 RID: 11464 RVA: 0x00101D0C File Offset: 0x000FFF0C
	public bool IsConnectedToNetworks(ICollection<UtilityNetwork> networks)
	{
		bool flag = false;
		IUtilityNetworkMgr networkManager = Conduit.GetNetworkManager(this.type);
		return flag || networks.Contains(networkManager.GetNetworkForCell(this.inputCell)) || networks.Contains(networkManager.GetNetworkForCell(this.outputCell));
	}

	// Token: 0x06002CC9 RID: 11465 RVA: 0x00101D53 File Offset: 0x000FFF53
	public int GetNetworkCell()
	{
		return this.inputCell;
	}

	// Token: 0x04001A68 RID: 6760
	[SerializeField]
	public ConduitType type;

	// Token: 0x04001A69 RID: 6761
	private int inputCell;

	// Token: 0x04001A6A RID: 6762
	private int outputCell;

	// Token: 0x04001A6B RID: 6763
	private HandleVector<int>.Handle accumulator = HandleVector<int>.InvalidHandle;
}
