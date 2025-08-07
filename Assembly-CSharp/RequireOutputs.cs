using System;
using UnityEngine;

// Token: 0x02000ABE RID: 2750
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/RequireOutputs")]
public class RequireOutputs : KMonoBehaviour
{
	// Token: 0x06004FD1 RID: 20433 RVA: 0x001CE034 File Offset: 0x001CC234
	protected override void OnSpawn()
	{
		base.OnSpawn();
		ScenePartitionerLayer scenePartitionerLayer = null;
		Building component = base.GetComponent<Building>();
		this.utilityCell = component.GetUtilityOutputCell();
		this.conduitType = component.Def.OutputConduitType;
		switch (component.Def.OutputConduitType)
		{
		case ConduitType.Gas:
			scenePartitionerLayer = GameScenePartitioner.Instance.gasConduitsLayer;
			break;
		case ConduitType.Liquid:
			scenePartitionerLayer = GameScenePartitioner.Instance.liquidConduitsLayer;
			break;
		case ConduitType.Solid:
			scenePartitionerLayer = GameScenePartitioner.Instance.solidConduitsLayer;
			break;
		}
		this.UpdateConnectionState(true);
		this.UpdatePipeRoomState(true);
		if (scenePartitionerLayer != null)
		{
			this.partitionerEntry = GameScenePartitioner.Instance.Add("RequireOutputs", base.gameObject, this.utilityCell, scenePartitionerLayer, delegate(object data)
			{
				this.UpdateConnectionState(false);
			});
		}
		this.GetConduitFlow().AddConduitUpdater(new Action<float>(this.UpdatePipeState), ConduitFlowPriority.First);
	}

	// Token: 0x06004FD2 RID: 20434 RVA: 0x001CE10C File Offset: 0x001CC30C
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		IConduitFlow conduitFlow = this.GetConduitFlow();
		if (conduitFlow != null)
		{
			conduitFlow.RemoveConduitUpdater(new Action<float>(this.UpdatePipeState));
		}
		base.OnCleanUp();
	}

	// Token: 0x06004FD3 RID: 20435 RVA: 0x001CE14C File Offset: 0x001CC34C
	private void UpdateConnectionState(bool force_update = false)
	{
		this.connected = this.IsConnected(this.utilityCell);
		if (this.connected != this.previouslyConnected || force_update)
		{
			this.operational.SetFlag(RequireOutputs.outputConnectedFlag, this.connected);
			this.previouslyConnected = this.connected;
			StatusItem statusItem = null;
			switch (this.conduitType)
			{
			case ConduitType.Gas:
				statusItem = Db.Get().BuildingStatusItems.NeedGasOut;
				break;
			case ConduitType.Liquid:
				statusItem = Db.Get().BuildingStatusItems.NeedLiquidOut;
				break;
			case ConduitType.Solid:
				statusItem = Db.Get().BuildingStatusItems.NeedSolidOut;
				break;
			}
			this.hasPipeGuid = this.selectable.ToggleStatusItem(statusItem, this.hasPipeGuid, !this.connected, this);
		}
	}

	// Token: 0x06004FD4 RID: 20436 RVA: 0x001CE21C File Offset: 0x001CC41C
	private bool OutputPipeIsEmpty()
	{
		if (this.ignoreFullPipe)
		{
			return true;
		}
		bool flag = true;
		if (this.connected)
		{
			flag = this.GetConduitFlow().IsConduitEmpty(this.utilityCell);
		}
		return flag;
	}

	// Token: 0x06004FD5 RID: 20437 RVA: 0x001CE250 File Offset: 0x001CC450
	private void UpdatePipeState(float dt)
	{
		this.UpdatePipeRoomState(false);
	}

	// Token: 0x06004FD6 RID: 20438 RVA: 0x001CE25C File Offset: 0x001CC45C
	private void UpdatePipeRoomState(bool force_update = false)
	{
		bool flag = this.OutputPipeIsEmpty();
		if (flag != this.previouslyHadRoom || force_update)
		{
			this.operational.SetFlag(RequireOutputs.pipesHaveRoomFlag, flag);
			this.previouslyHadRoom = flag;
			StatusItem statusItem = Db.Get().BuildingStatusItems.ConduitBlockedMultiples;
			if (this.conduitType == ConduitType.Solid)
			{
				statusItem = Db.Get().BuildingStatusItems.SolidConduitBlockedMultiples;
			}
			this.pipeBlockedGuid = this.selectable.ToggleStatusItem(statusItem, this.pipeBlockedGuid, !flag, null);
		}
	}

	// Token: 0x06004FD7 RID: 20439 RVA: 0x001CE2E0 File Offset: 0x001CC4E0
	private IConduitFlow GetConduitFlow()
	{
		switch (this.conduitType)
		{
		case ConduitType.Gas:
			return Game.Instance.gasConduitFlow;
		case ConduitType.Liquid:
			return Game.Instance.liquidConduitFlow;
		case ConduitType.Solid:
			return Game.Instance.solidConduitFlow;
		default:
			global::Debug.LogWarning("GetConduitFlow() called with unexpected conduitType: " + this.conduitType.ToString());
			return null;
		}
	}

	// Token: 0x06004FD8 RID: 20440 RVA: 0x001CE34C File Offset: 0x001CC54C
	private bool IsConnected(int cell)
	{
		return RequireOutputs.IsConnected(cell, this.conduitType);
	}

	// Token: 0x06004FD9 RID: 20441 RVA: 0x001CE35C File Offset: 0x001CC55C
	public static bool IsConnected(int cell, ConduitType conduitType)
	{
		ObjectLayer objectLayer = ObjectLayer.NumLayers;
		switch (conduitType)
		{
		case ConduitType.Gas:
			objectLayer = ObjectLayer.GasConduit;
			break;
		case ConduitType.Liquid:
			objectLayer = ObjectLayer.LiquidConduit;
			break;
		case ConduitType.Solid:
			objectLayer = ObjectLayer.SolidConduit;
			break;
		}
		GameObject gameObject = Grid.Objects[cell, (int)objectLayer];
		return gameObject != null && gameObject.GetComponent<BuildingComplete>() != null;
	}

	// Token: 0x040035C7 RID: 13767
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x040035C8 RID: 13768
	[MyCmpReq]
	private Operational operational;

	// Token: 0x040035C9 RID: 13769
	public bool ignoreFullPipe;

	// Token: 0x040035CA RID: 13770
	private int utilityCell;

	// Token: 0x040035CB RID: 13771
	private ConduitType conduitType;

	// Token: 0x040035CC RID: 13772
	private static readonly Operational.Flag outputConnectedFlag = new Operational.Flag("output_connected", Operational.Flag.Type.Requirement);

	// Token: 0x040035CD RID: 13773
	private static readonly Operational.Flag pipesHaveRoomFlag = new Operational.Flag("pipesHaveRoom", Operational.Flag.Type.Requirement);

	// Token: 0x040035CE RID: 13774
	private bool previouslyConnected = true;

	// Token: 0x040035CF RID: 13775
	private bool previouslyHadRoom = true;

	// Token: 0x040035D0 RID: 13776
	private bool connected;

	// Token: 0x040035D1 RID: 13777
	private Guid hasPipeGuid;

	// Token: 0x040035D2 RID: 13778
	private Guid pipeBlockedGuid;

	// Token: 0x040035D3 RID: 13779
	private HandleVector<int>.Handle partitionerEntry;
}
