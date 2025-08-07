using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000776 RID: 1910
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicTimeOfDaySensor : Switch, ISaveLoadable, ISim200ms
{
	// Token: 0x06003217 RID: 12823 RVA: 0x0011B51C File Offset: 0x0011971C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicTimeOfDaySensor>(-905833192, LogicTimeOfDaySensor.OnCopySettingsDelegate);
	}

	// Token: 0x06003218 RID: 12824 RVA: 0x0011B538 File Offset: 0x00119738
	private void OnCopySettings(object data)
	{
		LogicTimeOfDaySensor component = ((GameObject)data).GetComponent<LogicTimeOfDaySensor>();
		if (component != null)
		{
			this.startTime = component.startTime;
			this.duration = component.duration;
		}
	}

	// Token: 0x06003219 RID: 12825 RVA: 0x0011B572 File Offset: 0x00119772
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.OnToggle += this.OnSwitchToggled;
		this.UpdateLogicCircuit();
		this.UpdateVisualState(true);
		this.wasOn = this.switchedOn;
	}

	// Token: 0x0600321A RID: 12826 RVA: 0x0011B5A8 File Offset: 0x001197A8
	public void Sim200ms(float dt)
	{
		float currentCycleAsPercentage = GameClock.Instance.GetCurrentCycleAsPercentage();
		bool flag = false;
		if (currentCycleAsPercentage >= this.startTime && currentCycleAsPercentage < this.startTime + this.duration)
		{
			flag = true;
		}
		if (currentCycleAsPercentage < this.startTime + this.duration - 1f)
		{
			flag = true;
		}
		this.SetState(flag);
	}

	// Token: 0x0600321B RID: 12827 RVA: 0x0011B5FC File Offset: 0x001197FC
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateLogicCircuit();
		this.UpdateVisualState(false);
	}

	// Token: 0x0600321C RID: 12828 RVA: 0x0011B60B File Offset: 0x0011980B
	private void UpdateLogicCircuit()
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn ? 1 : 0);
	}

	// Token: 0x0600321D RID: 12829 RVA: 0x0011B62C File Offset: 0x0011982C
	private void UpdateVisualState(bool force = false)
	{
		if (this.wasOn != this.switchedOn || force)
		{
			this.wasOn = this.switchedOn;
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			component.Play(this.switchedOn ? "on_pre" : "on_pst", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue(this.switchedOn ? "on" : "off", KAnim.PlayMode.Once, 1f, 0f);
		}
	}

	// Token: 0x0600321E RID: 12830 RVA: 0x0011B6B4 File Offset: 0x001198B4
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x04001E14 RID: 7700
	[SerializeField]
	[Serialize]
	public float startTime;

	// Token: 0x04001E15 RID: 7701
	[SerializeField]
	[Serialize]
	public float duration = 1f;

	// Token: 0x04001E16 RID: 7702
	private bool wasOn;

	// Token: 0x04001E17 RID: 7703
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001E18 RID: 7704
	private static readonly EventSystem.IntraObjectHandler<LogicTimeOfDaySensor> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicTimeOfDaySensor>(delegate(LogicTimeOfDaySensor component, object data)
	{
		component.OnCopySettings(data);
	});
}
