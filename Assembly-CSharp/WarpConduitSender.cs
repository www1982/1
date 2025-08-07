using System;
using UnityEngine;

// Token: 0x020007F2 RID: 2034
public class WarpConduitSender : StateMachineComponent<WarpConduitSender.StatesInstance>, ISecondaryInput
{
	// Token: 0x0600374D RID: 14157 RVA: 0x001335A4 File Offset: 0x001317A4
	private bool IsSending()
	{
		return base.smi.master.gasPort.IsOn() || base.smi.master.liquidPort.IsOn() || base.smi.master.solidPort.IsOn();
	}

	// Token: 0x0600374E RID: 14158 RVA: 0x001335F8 File Offset: 0x001317F8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Storage[] components = base.GetComponents<Storage>();
		this.gasStorage = components[0];
		this.liquidStorage = components[1];
		this.solidStorage = components[2];
		this.gasPort = new WarpConduitSender.ConduitPort(base.gameObject, this.gasPortInfo, 1, this.gasStorage);
		this.liquidPort = new WarpConduitSender.ConduitPort(base.gameObject, this.liquidPortInfo, 2, this.liquidStorage);
		this.solidPort = new WarpConduitSender.ConduitPort(base.gameObject, this.solidPortInfo, 3, this.solidStorage);
		Vector3 position = this.liquidPort.airlock.gameObject.transform.position;
		this.liquidPort.airlock.gameObject.GetComponent<KBatchedAnimController>().transform.position = position + new Vector3(0f, 0f, -0.1f);
		this.liquidPort.airlock.gameObject.GetComponent<KBatchedAnimController>().enabled = false;
		this.liquidPort.airlock.gameObject.GetComponent<KBatchedAnimController>().enabled = true;
		this.FindPartner();
		WarpConduitStatus.UpdateWarpConduitsOperational(base.gameObject, (this.receiver != null) ? this.receiver.gameObject : null);
		base.smi.StartSM();
	}

	// Token: 0x0600374F RID: 14159 RVA: 0x00133749 File Offset: 0x00131949
	public void OnActivatedChanged(object data)
	{
		WarpConduitStatus.UpdateWarpConduitsOperational(base.gameObject, (this.receiver != null) ? this.receiver.gameObject : null);
	}

	// Token: 0x06003750 RID: 14160 RVA: 0x00133774 File Offset: 0x00131974
	private void FindPartner()
	{
		SaveGame.Instance.GetComponent<WorldGenSpawner>().SpawnTag("WarpConduitReceiver");
		foreach (WarpConduitReceiver warpConduitReceiver in global::UnityEngine.Object.FindObjectsOfType<WarpConduitReceiver>())
		{
			if (warpConduitReceiver.GetMyWorldId() != this.GetMyWorldId())
			{
				this.receiver = warpConduitReceiver;
				break;
			}
		}
		if (this.receiver == null)
		{
			global::Debug.LogWarning("No warp conduit receiver found - maybe POI stomping or failure to spawn?");
			return;
		}
		this.receiver.SetStorage(this.gasStorage, this.liquidStorage, this.solidStorage);
	}

	// Token: 0x06003751 RID: 14161 RVA: 0x001337FC File Offset: 0x001319FC
	protected override void OnCleanUp()
	{
		Conduit.GetNetworkManager(this.liquidPortInfo.conduitType).RemoveFromNetworks(this.liquidPort.inputCell, this.liquidPort.networkItem, true);
		Conduit.GetNetworkManager(this.gasPortInfo.conduitType).RemoveFromNetworks(this.gasPort.inputCell, this.gasPort.networkItem, true);
		Game.Instance.solidConduitSystem.RemoveFromNetworks(this.solidPort.inputCell, this.solidPort.solidConsumer, true);
		base.OnCleanUp();
	}

	// Token: 0x06003752 RID: 14162 RVA: 0x0013388D File Offset: 0x00131A8D
	bool ISecondaryInput.HasSecondaryConduitType(ConduitType type)
	{
		return this.liquidPortInfo.conduitType == type || this.gasPortInfo.conduitType == type || this.solidPortInfo.conduitType == type;
	}

	// Token: 0x06003753 RID: 14163 RVA: 0x001338BC File Offset: 0x00131ABC
	public CellOffset GetSecondaryConduitOffset(ConduitType type)
	{
		if (this.liquidPortInfo.conduitType == type)
		{
			return this.liquidPortInfo.offset;
		}
		if (this.gasPortInfo.conduitType == type)
		{
			return this.gasPortInfo.offset;
		}
		if (this.solidPortInfo.conduitType == type)
		{
			return this.solidPortInfo.offset;
		}
		return CellOffset.none;
	}

	// Token: 0x0400218B RID: 8587
	[MyCmpReq]
	private Operational operational;

	// Token: 0x0400218C RID: 8588
	public Storage gasStorage;

	// Token: 0x0400218D RID: 8589
	public Storage liquidStorage;

	// Token: 0x0400218E RID: 8590
	public Storage solidStorage;

	// Token: 0x0400218F RID: 8591
	public WarpConduitReceiver receiver;

	// Token: 0x04002190 RID: 8592
	[SerializeField]
	public ConduitPortInfo liquidPortInfo;

	// Token: 0x04002191 RID: 8593
	private WarpConduitSender.ConduitPort liquidPort;

	// Token: 0x04002192 RID: 8594
	[SerializeField]
	public ConduitPortInfo gasPortInfo;

	// Token: 0x04002193 RID: 8595
	private WarpConduitSender.ConduitPort gasPort;

	// Token: 0x04002194 RID: 8596
	[SerializeField]
	public ConduitPortInfo solidPortInfo;

	// Token: 0x04002195 RID: 8597
	private WarpConduitSender.ConduitPort solidPort;

	// Token: 0x02001750 RID: 5968
	private class ConduitPort
	{
		// Token: 0x0600989A RID: 39066 RVA: 0x00381828 File Offset: 0x0037FA28
		public ConduitPort(GameObject parent, ConduitPortInfo info, int number, Storage targetStorage)
		{
			this.portInfo = info;
			this.inputCell = Grid.OffsetCell(Grid.PosToCell(parent), this.portInfo.offset);
			if (this.portInfo.conduitType != ConduitType.Solid)
			{
				ConduitConsumer conduitConsumer = parent.AddComponent<ConduitConsumer>();
				conduitConsumer.conduitType = this.portInfo.conduitType;
				conduitConsumer.useSecondaryInput = true;
				conduitConsumer.storage = targetStorage;
				conduitConsumer.capacityKG = targetStorage.capacityKg;
				conduitConsumer.alwaysConsume = false;
				this.conduitConsumer = conduitConsumer;
				this.conduitConsumer.keepZeroMassObject = false;
				IUtilityNetworkMgr networkManager = Conduit.GetNetworkManager(this.portInfo.conduitType);
				this.networkItem = new FlowUtilityNetwork.NetworkItem(this.portInfo.conduitType, Endpoint.Sink, this.inputCell, parent);
				networkManager.AddToNetworks(this.inputCell, this.networkItem, true);
			}
			else
			{
				this.solidConsumer = parent.AddComponent<SolidConduitConsumer>();
				this.solidConsumer.useSecondaryInput = true;
				this.solidConsumer.storage = targetStorage;
				this.networkItem = new FlowUtilityNetwork.NetworkItem(ConduitType.Solid, Endpoint.Sink, this.inputCell, parent);
				Game.Instance.solidConduitSystem.AddToNetworks(this.inputCell, this.networkItem, true);
			}
			string text = "airlock_" + number.ToString();
			string text2 = "airlock_target_" + number.ToString();
			this.pre = "airlock_" + number.ToString() + "_pre";
			this.loop = "airlock_" + number.ToString() + "_loop";
			this.pst = "airlock_" + number.ToString() + "_pst";
			this.airlock = new MeterController(parent.GetComponent<KBatchedAnimController>(), text2, text, Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { text2 });
		}

		// Token: 0x0600989B RID: 39067 RVA: 0x003819EC File Offset: 0x0037FBEC
		public bool IsOn()
		{
			if (this.solidConsumer != null)
			{
				return this.solidConsumer.IsConsuming;
			}
			return this.conduitConsumer != null && (this.conduitConsumer.IsConnected && this.conduitConsumer.IsSatisfied) && this.conduitConsumer.consumedLastTick;
		}

		// Token: 0x0600989C RID: 39068 RVA: 0x00381A4C File Offset: 0x0037FC4C
		public void Update()
		{
			bool flag = this.IsOn();
			if (flag != this.open)
			{
				this.open = flag;
				if (this.open)
				{
					this.airlock.meterController.Play(this.pre, KAnim.PlayMode.Once, 1f, 0f);
					this.airlock.meterController.Queue(this.loop, KAnim.PlayMode.Loop, 1f, 0f);
					return;
				}
				this.airlock.meterController.Play(this.pst, KAnim.PlayMode.Once, 1f, 0f);
			}
		}

		// Token: 0x04007550 RID: 30032
		public ConduitPortInfo portInfo;

		// Token: 0x04007551 RID: 30033
		public int inputCell;

		// Token: 0x04007552 RID: 30034
		public FlowUtilityNetwork.NetworkItem networkItem;

		// Token: 0x04007553 RID: 30035
		private ConduitConsumer conduitConsumer;

		// Token: 0x04007554 RID: 30036
		public SolidConduitConsumer solidConsumer;

		// Token: 0x04007555 RID: 30037
		public MeterController airlock;

		// Token: 0x04007556 RID: 30038
		private bool open;

		// Token: 0x04007557 RID: 30039
		private string pre;

		// Token: 0x04007558 RID: 30040
		private string loop;

		// Token: 0x04007559 RID: 30041
		private string pst;
	}

	// Token: 0x02001751 RID: 5969
	public class StatesInstance : GameStateMachine<WarpConduitSender.States, WarpConduitSender.StatesInstance, WarpConduitSender, object>.GameInstance
	{
		// Token: 0x0600989D RID: 39069 RVA: 0x00381AEE File Offset: 0x0037FCEE
		public StatesInstance(WarpConduitSender smi)
			: base(smi)
		{
		}
	}

	// Token: 0x02001752 RID: 5970
	public class States : GameStateMachine<WarpConduitSender.States, WarpConduitSender.StatesInstance, WarpConduitSender>
	{
		// Token: 0x0600989E RID: 39070 RVA: 0x00381AF8 File Offset: 0x0037FCF8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.off;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.root.EventHandler(GameHashes.BuildingActivated, delegate(WarpConduitSender.StatesInstance smi, object data)
			{
				smi.master.OnActivatedChanged(data);
			});
			this.off.PlayAnim("off").Enter(delegate(WarpConduitSender.StatesInstance smi)
			{
				smi.master.gasPort.Update();
				smi.master.liquidPort.Update();
				smi.master.solidPort.Update();
			}).EventTransition(GameHashes.OperationalChanged, this.on, (WarpConduitSender.StatesInstance smi) => smi.GetComponent<Operational>().IsOperational);
			this.on.DefaultState(this.on.waiting).Update(delegate(WarpConduitSender.StatesInstance smi, float dt)
			{
				smi.master.gasPort.Update();
				smi.master.liquidPort.Update();
				smi.master.solidPort.Update();
			}, UpdateRate.SIM_1000ms, false);
			this.on.working.PlayAnim("working_pre").QueueAnim("working_loop", true, null).ToggleMainStatusItem(Db.Get().BuildingStatusItems.Working, null)
				.Update(delegate(WarpConduitSender.StatesInstance smi, float dt)
				{
					if (!smi.master.IsSending())
					{
						smi.GoTo(this.on.waiting);
					}
				}, UpdateRate.SIM_1000ms, false)
				.Exit(delegate(WarpConduitSender.StatesInstance smi)
				{
					smi.Play("working_pst", KAnim.PlayMode.Once);
				});
			this.on.waiting.QueueAnim("idle", false, null).ToggleMainStatusItem(Db.Get().BuildingStatusItems.Normal, null).Update(delegate(WarpConduitSender.StatesInstance smi, float dt)
			{
				if (smi.master.IsSending())
				{
					smi.GoTo(this.on.working);
				}
			}, UpdateRate.SIM_1000ms, false);
		}

		// Token: 0x0400755A RID: 30042
		public GameStateMachine<WarpConduitSender.States, WarpConduitSender.StatesInstance, WarpConduitSender, object>.State off;

		// Token: 0x0400755B RID: 30043
		public WarpConduitSender.States.onStates on;

		// Token: 0x020027FB RID: 10235
		public class onStates : GameStateMachine<WarpConduitSender.States, WarpConduitSender.StatesInstance, WarpConduitSender, object>.State
		{
			// Token: 0x0400B166 RID: 45414
			public GameStateMachine<WarpConduitSender.States, WarpConduitSender.StatesInstance, WarpConduitSender, object>.State working;

			// Token: 0x0400B167 RID: 45415
			public GameStateMachine<WarpConduitSender.States, WarpConduitSender.StatesInstance, WarpConduitSender, object>.State waiting;
		}
	}
}
