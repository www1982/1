using System;
using UnityEngine;

// Token: 0x020007F0 RID: 2032
public class WarpConduitReceiver : StateMachineComponent<WarpConduitReceiver.StatesInstance>, ISecondaryOutput
{
	// Token: 0x06003742 RID: 14146 RVA: 0x001330A8 File Offset: 0x001312A8
	private bool IsReceiving()
	{
		return base.smi.master.gasPort.IsOn() || base.smi.master.liquidPort.IsOn() || base.smi.master.solidPort.IsOn();
	}

	// Token: 0x06003743 RID: 14147 RVA: 0x001330FA File Offset: 0x001312FA
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.FindPartner();
		if (this.solidPort.solidDispenser != null)
		{
			this.solidPort.solidDispenser.solidOnly = true;
		}
		base.smi.StartSM();
	}

	// Token: 0x06003744 RID: 14148 RVA: 0x00133138 File Offset: 0x00131338
	private void FindPartner()
	{
		if (this.senderGasStorage != null)
		{
			return;
		}
		WarpConduitSender warpConduitSender = null;
		SaveGame.Instance.GetComponent<WorldGenSpawner>().SpawnTag("WarpConduitSender");
		foreach (WarpConduitSender warpConduitSender2 in global::UnityEngine.Object.FindObjectsOfType<WarpConduitSender>())
		{
			if (warpConduitSender2.GetMyWorldId() != this.GetMyWorldId())
			{
				warpConduitSender = warpConduitSender2;
				break;
			}
		}
		if (warpConduitSender == null)
		{
			global::Debug.LogWarning("No warp conduit sender found - maybe POI stomping or failure to spawn?");
			return;
		}
		this.SetStorage(warpConduitSender.gasStorage, warpConduitSender.liquidStorage, warpConduitSender.solidStorage);
		WarpConduitStatus.UpdateWarpConduitsOperational(warpConduitSender.gameObject, base.gameObject);
	}

	// Token: 0x06003745 RID: 14149 RVA: 0x001331D4 File Offset: 0x001313D4
	protected override void OnCleanUp()
	{
		Conduit.GetNetworkManager(this.liquidPortInfo.conduitType).RemoveFromNetworks(this.liquidPort.outputCell, this.liquidPort.networkItem, true);
		if (this.gasPort.portInfo != null)
		{
			Conduit.GetNetworkManager(this.gasPort.portInfo.conduitType).RemoveFromNetworks(this.gasPort.outputCell, this.gasPort.networkItem, true);
		}
		else
		{
			global::Debug.LogWarning("Conduit Receiver gasPort portInfo is null in OnCleanUp");
		}
		Game.Instance.solidConduitSystem.RemoveFromNetworks(this.solidPort.outputCell, this.solidPort.networkItem, true);
		base.OnCleanUp();
	}

	// Token: 0x06003746 RID: 14150 RVA: 0x00133283 File Offset: 0x00131483
	public void OnActivatedChanged(object data)
	{
		if (this.senderGasStorage == null)
		{
			this.FindPartner();
		}
		WarpConduitStatus.UpdateWarpConduitsOperational((this.senderGasStorage != null) ? this.senderGasStorage.gameObject : null, base.gameObject);
	}

	// Token: 0x06003747 RID: 14151 RVA: 0x001332C0 File Offset: 0x001314C0
	public void SetStorage(Storage gasStorage, Storage liquidStorage, Storage solidStorage)
	{
		this.senderGasStorage = gasStorage;
		this.senderLiquidStorage = liquidStorage;
		this.senderSolidStorage = solidStorage;
		this.gasPort.SetPortInfo(base.gameObject, this.gasPortInfo, gasStorage, 1);
		this.liquidPort.SetPortInfo(base.gameObject, this.liquidPortInfo, liquidStorage, 2);
		this.solidPort.SetPortInfo(base.gameObject, this.solidPortInfo, solidStorage, 3);
		Vector3 position = this.liquidPort.airlock.gameObject.transform.position;
		this.liquidPort.airlock.gameObject.GetComponent<KBatchedAnimController>().transform.position = position + new Vector3(0f, 0f, -0.1f);
		this.liquidPort.airlock.gameObject.GetComponent<KBatchedAnimController>().enabled = false;
		this.liquidPort.airlock.gameObject.GetComponent<KBatchedAnimController>().enabled = true;
	}

	// Token: 0x06003748 RID: 14152 RVA: 0x001333B7 File Offset: 0x001315B7
	public bool HasSecondaryConduitType(ConduitType type)
	{
		return type == this.gasPortInfo.conduitType || type == this.liquidPortInfo.conduitType || type == this.solidPortInfo.conduitType;
	}

	// Token: 0x06003749 RID: 14153 RVA: 0x001333E8 File Offset: 0x001315E8
	public CellOffset GetSecondaryConduitOffset(ConduitType type)
	{
		if (type == this.gasPortInfo.conduitType)
		{
			return this.gasPortInfo.offset;
		}
		if (type == this.liquidPortInfo.conduitType)
		{
			return this.liquidPortInfo.offset;
		}
		if (type == this.solidPortInfo.conduitType)
		{
			return this.solidPortInfo.offset;
		}
		return CellOffset.none;
	}

	// Token: 0x04002181 RID: 8577
	[SerializeField]
	public ConduitPortInfo liquidPortInfo;

	// Token: 0x04002182 RID: 8578
	private WarpConduitReceiver.ConduitPort liquidPort;

	// Token: 0x04002183 RID: 8579
	[SerializeField]
	public ConduitPortInfo solidPortInfo;

	// Token: 0x04002184 RID: 8580
	private WarpConduitReceiver.ConduitPort solidPort;

	// Token: 0x04002185 RID: 8581
	[SerializeField]
	public ConduitPortInfo gasPortInfo;

	// Token: 0x04002186 RID: 8582
	private WarpConduitReceiver.ConduitPort gasPort;

	// Token: 0x04002187 RID: 8583
	public Storage senderGasStorage;

	// Token: 0x04002188 RID: 8584
	public Storage senderLiquidStorage;

	// Token: 0x04002189 RID: 8585
	public Storage senderSolidStorage;

	// Token: 0x0200174D RID: 5965
	public struct ConduitPort
	{
		// Token: 0x06009892 RID: 39058 RVA: 0x003813A4 File Offset: 0x0037F5A4
		public void SetPortInfo(GameObject parent, ConduitPortInfo info, Storage senderStorage, int number)
		{
			this.portInfo = info;
			this.outputCell = Grid.OffsetCell(Grid.PosToCell(parent), this.portInfo.offset);
			if (this.portInfo.conduitType != ConduitType.Solid)
			{
				ConduitDispenser conduitDispenser = parent.AddComponent<ConduitDispenser>();
				conduitDispenser.conduitType = this.portInfo.conduitType;
				conduitDispenser.useSecondaryOutput = true;
				conduitDispenser.alwaysDispense = true;
				conduitDispenser.storage = senderStorage;
				this.dispenser = conduitDispenser;
				IUtilityNetworkMgr networkManager = Conduit.GetNetworkManager(this.portInfo.conduitType);
				this.networkItem = new FlowUtilityNetwork.NetworkItem(this.portInfo.conduitType, Endpoint.Source, this.outputCell, parent);
				networkManager.AddToNetworks(this.outputCell, this.networkItem, true);
			}
			else
			{
				SolidConduitDispenser solidConduitDispenser = parent.AddComponent<SolidConduitDispenser>();
				solidConduitDispenser.storage = senderStorage;
				solidConduitDispenser.alwaysDispense = true;
				solidConduitDispenser.useSecondaryOutput = true;
				this.solidDispenser = solidConduitDispenser;
				this.networkItem = new FlowUtilityNetwork.NetworkItem(ConduitType.Solid, Endpoint.Source, this.outputCell, parent);
				Game.Instance.solidConduitSystem.AddToNetworks(this.outputCell, this.networkItem, true);
			}
			string text = "airlock_" + number.ToString();
			string text2 = "airlock_target_" + number.ToString();
			this.pre = "airlock_" + number.ToString() + "_pre";
			this.loop = "airlock_" + number.ToString() + "_loop";
			this.pst = "airlock_" + number.ToString() + "_pst";
			this.airlock = new MeterController(parent.GetComponent<KBatchedAnimController>(), text2, text, Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { text2 });
		}

		// Token: 0x06009893 RID: 39059 RVA: 0x00381544 File Offset: 0x0037F744
		public bool IsOn()
		{
			if (this.solidDispenser != null)
			{
				return this.solidDispenser.IsDispensing;
			}
			return this.dispenser != null && !this.dispenser.blocked && !this.dispenser.empty;
		}

		// Token: 0x06009894 RID: 39060 RVA: 0x00381598 File Offset: 0x0037F798
		public void UpdatePortAnim()
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

		// Token: 0x04007544 RID: 30020
		public ConduitPortInfo portInfo;

		// Token: 0x04007545 RID: 30021
		public int outputCell;

		// Token: 0x04007546 RID: 30022
		public FlowUtilityNetwork.NetworkItem networkItem;

		// Token: 0x04007547 RID: 30023
		public ConduitDispenser dispenser;

		// Token: 0x04007548 RID: 30024
		public SolidConduitDispenser solidDispenser;

		// Token: 0x04007549 RID: 30025
		public MeterController airlock;

		// Token: 0x0400754A RID: 30026
		private bool open;

		// Token: 0x0400754B RID: 30027
		private string pre;

		// Token: 0x0400754C RID: 30028
		private string loop;

		// Token: 0x0400754D RID: 30029
		private string pst;
	}

	// Token: 0x0200174E RID: 5966
	public class StatesInstance : GameStateMachine<WarpConduitReceiver.States, WarpConduitReceiver.StatesInstance, WarpConduitReceiver, object>.GameInstance
	{
		// Token: 0x06009895 RID: 39061 RVA: 0x0038163A File Offset: 0x0037F83A
		public StatesInstance(WarpConduitReceiver master)
			: base(master)
		{
		}
	}

	// Token: 0x0200174F RID: 5967
	public class States : GameStateMachine<WarpConduitReceiver.States, WarpConduitReceiver.StatesInstance, WarpConduitReceiver>
	{
		// Token: 0x06009896 RID: 39062 RVA: 0x00381644 File Offset: 0x0037F844
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.off;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.root.EventHandler(GameHashes.BuildingActivated, delegate(WarpConduitReceiver.StatesInstance smi, object data)
			{
				smi.master.OnActivatedChanged(data);
			});
			this.off.PlayAnim("off").Enter(delegate(WarpConduitReceiver.StatesInstance smi)
			{
				smi.master.gasPort.UpdatePortAnim();
				smi.master.liquidPort.UpdatePortAnim();
				smi.master.solidPort.UpdatePortAnim();
			}).EventTransition(GameHashes.OperationalFlagChanged, this.on, (WarpConduitReceiver.StatesInstance smi) => smi.GetComponent<Operational>().GetFlag(WarpConduitStatus.warpConnectedFlag));
			this.on.DefaultState(this.on.idle).Update(delegate(WarpConduitReceiver.StatesInstance smi, float dt)
			{
				smi.master.gasPort.UpdatePortAnim();
				smi.master.liquidPort.UpdatePortAnim();
				smi.master.solidPort.UpdatePortAnim();
			}, UpdateRate.SIM_1000ms, false);
			this.on.idle.QueueAnim("idle", false, null).ToggleMainStatusItem(Db.Get().BuildingStatusItems.Normal, null).Update(delegate(WarpConduitReceiver.StatesInstance smi, float dt)
			{
				if (smi.master.IsReceiving())
				{
					smi.GoTo(this.on.working);
				}
			}, UpdateRate.SIM_1000ms, false);
			this.on.working.PlayAnim("working_pre").QueueAnim("working_loop", true, null).ToggleMainStatusItem(Db.Get().BuildingStatusItems.Working, null)
				.Update(delegate(WarpConduitReceiver.StatesInstance smi, float dt)
				{
					if (!smi.master.IsReceiving())
					{
						smi.GoTo(this.on.idle);
					}
				}, UpdateRate.SIM_1000ms, false)
				.Exit(delegate(WarpConduitReceiver.StatesInstance smi)
				{
					smi.Play("working_pst", KAnim.PlayMode.Once);
				});
		}

		// Token: 0x0400754E RID: 30030
		public GameStateMachine<WarpConduitReceiver.States, WarpConduitReceiver.StatesInstance, WarpConduitReceiver, object>.State off;

		// Token: 0x0400754F RID: 30031
		public WarpConduitReceiver.States.onStates on;

		// Token: 0x020027F9 RID: 10233
		public class onStates : GameStateMachine<WarpConduitReceiver.States, WarpConduitReceiver.StatesInstance, WarpConduitReceiver, object>.State
		{
			// Token: 0x0400B15E RID: 45406
			public GameStateMachine<WarpConduitReceiver.States, WarpConduitReceiver.StatesInstance, WarpConduitReceiver, object>.State working;

			// Token: 0x0400B15F RID: 45407
			public GameStateMachine<WarpConduitReceiver.States, WarpConduitReceiver.StatesInstance, WarpConduitReceiver, object>.State idle;
		}
	}
}
