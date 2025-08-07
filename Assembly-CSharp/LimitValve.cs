using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000755 RID: 1877
[SerializationConfig(MemberSerialization.OptIn)]
public class LimitValve : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x1700028C RID: 652
	// (get) Token: 0x06002FBC RID: 12220 RVA: 0x00111564 File Offset: 0x0010F764
	public float RemainingCapacity
	{
		get
		{
			return Mathf.Max(0f, this.m_limit - this.m_amount);
		}
	}

	// Token: 0x06002FBD RID: 12221 RVA: 0x0011157D File Offset: 0x0010F77D
	public NonLinearSlider.Range[] GetRanges()
	{
		if (this.sliderRanges != null && this.sliderRanges.Length != 0)
		{
			return this.sliderRanges;
		}
		return NonLinearSlider.GetDefaultRange(this.maxLimitKg);
	}

	// Token: 0x1700028D RID: 653
	// (get) Token: 0x06002FBE RID: 12222 RVA: 0x001115A2 File Offset: 0x0010F7A2
	// (set) Token: 0x06002FBF RID: 12223 RVA: 0x001115AA File Offset: 0x0010F7AA
	public float Limit
	{
		get
		{
			return this.m_limit;
		}
		set
		{
			this.m_limit = value;
			this.Refresh();
		}
	}

	// Token: 0x1700028E RID: 654
	// (get) Token: 0x06002FC0 RID: 12224 RVA: 0x001115B9 File Offset: 0x0010F7B9
	// (set) Token: 0x06002FC1 RID: 12225 RVA: 0x001115C1 File Offset: 0x0010F7C1
	public float Amount
	{
		get
		{
			return this.m_amount;
		}
		set
		{
			this.m_amount = value;
			base.Trigger(-1722241721, this.Amount);
			this.Refresh();
		}
	}

	// Token: 0x06002FC2 RID: 12226 RVA: 0x001115E6 File Offset: 0x0010F7E6
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LimitValve>(-905833192, LimitValve.OnCopySettingsDelegate);
	}

	// Token: 0x06002FC3 RID: 12227 RVA: 0x00111600 File Offset: 0x0010F800
	protected override void OnSpawn()
	{
		LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
		logicCircuitManager.onLogicTick = (global::System.Action)Delegate.Combine(logicCircuitManager.onLogicTick, new global::System.Action(this.LogicTick));
		base.Subscribe<LimitValve>(-801688580, LimitValve.OnLogicValueChangedDelegate);
		if (this.conduitType == ConduitType.Gas || this.conduitType == ConduitType.Liquid)
		{
			ConduitBridge conduitBridge = this.conduitBridge;
			conduitBridge.desiredMassTransfer = (ConduitBridgeBase.DesiredMassTransfer)Delegate.Combine(conduitBridge.desiredMassTransfer, new ConduitBridgeBase.DesiredMassTransfer(this.DesiredMassTransfer));
			ConduitBridge conduitBridge2 = this.conduitBridge;
			conduitBridge2.OnMassTransfer = (ConduitBridgeBase.ConduitBridgeEvent)Delegate.Combine(conduitBridge2.OnMassTransfer, new ConduitBridgeBase.ConduitBridgeEvent(this.OnMassTransfer));
		}
		else if (this.conduitType == ConduitType.Solid)
		{
			SolidConduitBridge solidConduitBridge = this.solidConduitBridge;
			solidConduitBridge.desiredMassTransfer = (ConduitBridgeBase.DesiredMassTransfer)Delegate.Combine(solidConduitBridge.desiredMassTransfer, new ConduitBridgeBase.DesiredMassTransfer(this.DesiredMassTransfer));
			SolidConduitBridge solidConduitBridge2 = this.solidConduitBridge;
			solidConduitBridge2.OnMassTransfer = (ConduitBridgeBase.ConduitBridgeEvent)Delegate.Combine(solidConduitBridge2.OnMassTransfer, new ConduitBridgeBase.ConduitBridgeEvent(this.OnMassTransfer));
		}
		if (this.limitMeter == null)
		{
			this.limitMeter = new MeterController(this.controller, "meter_target_counter", "meter_counter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_target_counter" });
		}
		this.Refresh();
		base.OnSpawn();
	}

	// Token: 0x06002FC4 RID: 12228 RVA: 0x00111742 File Offset: 0x0010F942
	protected override void OnCleanUp()
	{
		LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
		logicCircuitManager.onLogicTick = (global::System.Action)Delegate.Remove(logicCircuitManager.onLogicTick, new global::System.Action(this.LogicTick));
		base.OnCleanUp();
	}

	// Token: 0x06002FC5 RID: 12229 RVA: 0x00111775 File Offset: 0x0010F975
	private void LogicTick()
	{
		if (this.m_resetRequested)
		{
			this.ResetAmount();
		}
	}

	// Token: 0x06002FC6 RID: 12230 RVA: 0x00111785 File Offset: 0x0010F985
	public void ResetAmount()
	{
		this.m_resetRequested = false;
		this.Amount = 0f;
	}

	// Token: 0x06002FC7 RID: 12231 RVA: 0x0011179C File Offset: 0x0010F99C
	private float DesiredMassTransfer(float dt, SimHashes element, float mass, float temperature, byte disease_idx, int disease_count, Pickupable pickupable)
	{
		if (!this.operational.IsOperational)
		{
			return 0f;
		}
		if (this.conduitType == ConduitType.Solid && pickupable != null && GameTags.DisplayAsUnits.Contains(pickupable.KPrefabID.PrefabID()))
		{
			float num = pickupable.PrimaryElement.Units;
			if (this.RemainingCapacity < num)
			{
				num = (float)Mathf.FloorToInt(this.RemainingCapacity);
			}
			return num * pickupable.PrimaryElement.MassPerUnit;
		}
		return Mathf.Min(mass, this.RemainingCapacity);
	}

	// Token: 0x06002FC8 RID: 12232 RVA: 0x00111828 File Offset: 0x0010FA28
	private void OnMassTransfer(SimHashes element, float transferredMass, float temperature, byte disease_idx, int disease_count, Pickupable pickupable)
	{
		if (!LogicCircuitNetwork.IsBitActive(0, this.ports.GetInputValue(LimitValve.RESET_PORT_ID)))
		{
			if (this.conduitType == ConduitType.Gas || this.conduitType == ConduitType.Liquid)
			{
				this.Amount += transferredMass;
			}
			else if (this.conduitType == ConduitType.Solid && pickupable != null)
			{
				this.Amount += transferredMass / pickupable.PrimaryElement.MassPerUnit;
			}
		}
		this.operational.SetActive(this.operational.IsOperational && transferredMass > 0f, false);
		this.Refresh();
	}

	// Token: 0x06002FC9 RID: 12233 RVA: 0x001118C8 File Offset: 0x0010FAC8
	private void Refresh()
	{
		if (this.operational == null)
		{
			return;
		}
		this.ports.SendSignal(LimitValve.OUTPUT_PORT_ID, (this.RemainingCapacity <= 0f) ? 1 : 0);
		this.operational.SetFlag(LimitValve.limitNotReached, this.RemainingCapacity > 0f);
		if (this.RemainingCapacity > 0f)
		{
			this.limitMeter.meterController.Play("meter_counter", KAnim.PlayMode.Paused, 1f, 0f);
			this.limitMeter.SetPositionPercent(this.Amount / this.Limit);
			this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.LimitValveLimitNotReached, this);
			return;
		}
		this.limitMeter.meterController.Play("meter_on", KAnim.PlayMode.Paused, 1f, 0f);
		this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.LimitValveLimitReached, this);
	}

	// Token: 0x06002FCA RID: 12234 RVA: 0x001119E8 File Offset: 0x0010FBE8
	public void OnLogicValueChanged(object data)
	{
		LogicValueChanged logicValueChanged = (LogicValueChanged)data;
		if (logicValueChanged.portID == LimitValve.RESET_PORT_ID && LogicCircuitNetwork.IsBitActive(0, logicValueChanged.newValue))
		{
			this.ResetAmount();
		}
	}

	// Token: 0x06002FCB RID: 12235 RVA: 0x00111A24 File Offset: 0x0010FC24
	private void OnCopySettings(object data)
	{
		LimitValve component = ((GameObject)data).GetComponent<LimitValve>();
		if (component != null)
		{
			this.Limit = component.Limit;
		}
	}

	// Token: 0x04001C68 RID: 7272
	public static readonly HashedString RESET_PORT_ID = new HashedString("LimitValveReset");

	// Token: 0x04001C69 RID: 7273
	public static readonly HashedString OUTPUT_PORT_ID = new HashedString("LimitValveOutput");

	// Token: 0x04001C6A RID: 7274
	public static readonly Operational.Flag limitNotReached = new Operational.Flag("limitNotReached", Operational.Flag.Type.Requirement);

	// Token: 0x04001C6B RID: 7275
	public ConduitType conduitType;

	// Token: 0x04001C6C RID: 7276
	public float maxLimitKg = 100f;

	// Token: 0x04001C6D RID: 7277
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001C6E RID: 7278
	[MyCmpReq]
	private LogicPorts ports;

	// Token: 0x04001C6F RID: 7279
	[MyCmpGet]
	private KBatchedAnimController controller;

	// Token: 0x04001C70 RID: 7280
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x04001C71 RID: 7281
	[MyCmpGet]
	private ConduitBridge conduitBridge;

	// Token: 0x04001C72 RID: 7282
	[MyCmpGet]
	private SolidConduitBridge solidConduitBridge;

	// Token: 0x04001C73 RID: 7283
	[Serialize]
	[SerializeField]
	private float m_limit;

	// Token: 0x04001C74 RID: 7284
	[Serialize]
	private float m_amount;

	// Token: 0x04001C75 RID: 7285
	[Serialize]
	private bool m_resetRequested;

	// Token: 0x04001C76 RID: 7286
	private MeterController limitMeter;

	// Token: 0x04001C77 RID: 7287
	public bool displayUnitsInsteadOfMass;

	// Token: 0x04001C78 RID: 7288
	public NonLinearSlider.Range[] sliderRanges;

	// Token: 0x04001C79 RID: 7289
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001C7A RID: 7290
	private static readonly EventSystem.IntraObjectHandler<LimitValve> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<LimitValve>(delegate(LimitValve component, object data)
	{
		component.OnLogicValueChanged(data);
	});

	// Token: 0x04001C7B RID: 7291
	private static readonly EventSystem.IntraObjectHandler<LimitValve> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LimitValve>(delegate(LimitValve component, object data)
	{
		component.OnCopySettings(data);
	});
}
