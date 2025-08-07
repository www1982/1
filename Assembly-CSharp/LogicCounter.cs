using System;
using KSerialization;
using UnityEngine;

// Token: 0x0200075E RID: 1886
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicCounter : Switch, ISaveLoadable
{
	// Token: 0x06003033 RID: 12339 RVA: 0x00114089 File Offset: 0x00112289
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicCounter>(-905833192, LogicCounter.OnCopySettingsDelegate);
	}

	// Token: 0x06003034 RID: 12340 RVA: 0x001140A4 File Offset: 0x001122A4
	private void OnCopySettings(object data)
	{
		LogicCounter component = ((GameObject)data).GetComponent<LogicCounter>();
		if (component != null)
		{
			this.maxCount = component.maxCount;
			this.resetCountAtMax = component.resetCountAtMax;
			this.advancedMode = component.advancedMode;
		}
	}

	// Token: 0x06003035 RID: 12341 RVA: 0x001140EC File Offset: 0x001122EC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.OnToggle += this.OnSwitchToggled;
		LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
		logicCircuitManager.onLogicTick = (global::System.Action)Delegate.Combine(logicCircuitManager.onLogicTick, new global::System.Action(this.LogicTick));
		if (this.maxCount == 0)
		{
			this.maxCount = 10;
		}
		base.Subscribe<LogicCounter>(-801688580, LogicCounter.OnLogicValueChangedDelegate);
		this.UpdateLogicCircuit();
		this.UpdateVisualState(true);
		this.wasOn = this.switchedOn;
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		this.meter = new MeterController(component, "meter_target", component.FlipY ? "meter_dn" : "meter_up", Meter.Offset.UserSpecified, Grid.SceneLayer.LogicGatesFront, Vector3.zero, null);
		this.UpdateMeter();
	}

	// Token: 0x06003036 RID: 12342 RVA: 0x001141B1 File Offset: 0x001123B1
	protected override void OnCleanUp()
	{
		LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
		logicCircuitManager.onLogicTick = (global::System.Action)Delegate.Remove(logicCircuitManager.onLogicTick, new global::System.Action(this.LogicTick));
	}

	// Token: 0x06003037 RID: 12343 RVA: 0x001141DE File Offset: 0x001123DE
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateLogicCircuit();
		this.UpdateVisualState(false);
	}

	// Token: 0x06003038 RID: 12344 RVA: 0x001141ED File Offset: 0x001123ED
	public void UpdateLogicCircuit()
	{
		if (this.receivedFirstSignal)
		{
			base.GetComponent<LogicPorts>().SendSignal(LogicCounter.OUTPUT_PORT_ID, this.switchedOn ? 1 : 0);
		}
	}

	// Token: 0x06003039 RID: 12345 RVA: 0x00114214 File Offset: 0x00112414
	public void UpdateMeter()
	{
		float num = (float)(this.currentCount % (this.advancedMode ? this.maxCount : 10));
		this.meter.SetPositionPercent(num / 9f);
	}

	// Token: 0x0600303A RID: 12346 RVA: 0x00114250 File Offset: 0x00112450
	public void UpdateVisualState(bool force = false)
	{
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		if (!this.receivedFirstSignal)
		{
			component.Play("off", KAnim.PlayMode.Once, 1f, 0f);
			return;
		}
		if (this.wasOn != this.switchedOn || force)
		{
			int num = (this.switchedOn ? 4 : 0) + (this.wasResetting ? 2 : 0) + (this.wasIncrementing ? 1 : 0);
			this.wasOn = this.switchedOn;
			component.Play("on_" + num.ToString(), KAnim.PlayMode.Once, 1f, 0f);
		}
	}

	// Token: 0x0600303B RID: 12347 RVA: 0x001142F8 File Offset: 0x001124F8
	public void OnLogicValueChanged(object data)
	{
		LogicValueChanged logicValueChanged = (LogicValueChanged)data;
		if (logicValueChanged.portID == LogicCounter.INPUT_PORT_ID)
		{
			int newValue = logicValueChanged.newValue;
			this.receivedFirstSignal = true;
			if (LogicCircuitNetwork.IsBitActive(0, newValue))
			{
				if (!this.wasIncrementing)
				{
					this.wasIncrementing = true;
					if (!this.wasResetting)
					{
						if (this.currentCount == this.maxCount || this.currentCount >= 10)
						{
							this.currentCount = 0;
						}
						this.currentCount++;
						this.UpdateMeter();
						this.SetCounterState();
						if (this.currentCount == this.maxCount && this.resetCountAtMax)
						{
							this.resetRequested = true;
						}
					}
				}
			}
			else
			{
				this.wasIncrementing = false;
			}
		}
		else
		{
			if (!(logicValueChanged.portID == LogicCounter.RESET_PORT_ID))
			{
				return;
			}
			int newValue2 = logicValueChanged.newValue;
			this.receivedFirstSignal = true;
			if (LogicCircuitNetwork.IsBitActive(0, newValue2))
			{
				if (!this.wasResetting)
				{
					this.wasResetting = true;
					this.ResetCounter();
				}
			}
			else
			{
				this.wasResetting = false;
			}
		}
		this.UpdateVisualState(true);
		this.UpdateLogicCircuit();
	}

	// Token: 0x0600303C RID: 12348 RVA: 0x00114410 File Offset: 0x00112610
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x0600303D RID: 12349 RVA: 0x00114463 File Offset: 0x00112663
	public void ResetCounter()
	{
		this.resetRequested = false;
		this.currentCount = 0;
		this.SetState(false);
		if (this.advancedMode)
		{
			this.pulsingActive = false;
			this.pulseTicksRemaining = 0;
		}
		this.UpdateVisualState(true);
		this.UpdateMeter();
		this.UpdateLogicCircuit();
	}

	// Token: 0x0600303E RID: 12350 RVA: 0x001144A4 File Offset: 0x001126A4
	public void LogicTick()
	{
		if (this.resetRequested)
		{
			this.ResetCounter();
		}
		if (this.pulsingActive)
		{
			this.pulseTicksRemaining--;
			if (this.pulseTicksRemaining <= 0)
			{
				this.pulsingActive = false;
				this.SetState(false);
				this.UpdateVisualState(false);
				this.UpdateMeter();
				this.UpdateLogicCircuit();
			}
		}
	}

	// Token: 0x0600303F RID: 12351 RVA: 0x00114500 File Offset: 0x00112700
	public void SetCounterState()
	{
		this.SetState(this.advancedMode ? (this.currentCount % this.maxCount == 0) : (this.currentCount == this.maxCount));
		if (this.advancedMode && this.currentCount % this.maxCount == 0)
		{
			this.pulsingActive = true;
			this.pulseTicksRemaining = 2;
		}
	}

	// Token: 0x04001CC7 RID: 7367
	[Serialize]
	public int maxCount;

	// Token: 0x04001CC8 RID: 7368
	[Serialize]
	public int currentCount;

	// Token: 0x04001CC9 RID: 7369
	[Serialize]
	public bool resetCountAtMax;

	// Token: 0x04001CCA RID: 7370
	[Serialize]
	public bool advancedMode;

	// Token: 0x04001CCB RID: 7371
	private bool wasOn;

	// Token: 0x04001CCC RID: 7372
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001CCD RID: 7373
	private static readonly EventSystem.IntraObjectHandler<LogicCounter> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicCounter>(delegate(LogicCounter component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x04001CCE RID: 7374
	private static readonly EventSystem.IntraObjectHandler<LogicCounter> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<LogicCounter>(delegate(LogicCounter component, object data)
	{
		component.OnLogicValueChanged(data);
	});

	// Token: 0x04001CCF RID: 7375
	public static readonly HashedString INPUT_PORT_ID = new HashedString("LogicCounterInput");

	// Token: 0x04001CD0 RID: 7376
	public static readonly HashedString RESET_PORT_ID = new HashedString("LogicCounterReset");

	// Token: 0x04001CD1 RID: 7377
	public static readonly HashedString OUTPUT_PORT_ID = new HashedString("LogicCounterOutput");

	// Token: 0x04001CD2 RID: 7378
	private bool resetRequested;

	// Token: 0x04001CD3 RID: 7379
	[Serialize]
	private bool wasResetting;

	// Token: 0x04001CD4 RID: 7380
	[Serialize]
	private bool wasIncrementing;

	// Token: 0x04001CD5 RID: 7381
	[Serialize]
	public bool receivedFirstSignal;

	// Token: 0x04001CD6 RID: 7382
	private bool pulsingActive;

	// Token: 0x04001CD7 RID: 7383
	private const int pulseLength = 1;

	// Token: 0x04001CD8 RID: 7384
	private int pulseTicksRemaining;

	// Token: 0x04001CD9 RID: 7385
	private MeterController meter;
}
