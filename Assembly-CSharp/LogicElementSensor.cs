using System;
using KSerialization;

// Token: 0x02000762 RID: 1890
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicElementSensor : Switch, ISaveLoadable, ISim200ms
{
	// Token: 0x0600308F RID: 12431 RVA: 0x001152E2 File Offset: 0x001134E2
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.GetComponent<Filterable>().onFilterChanged += this.OnElementSelected;
	}

	// Token: 0x06003090 RID: 12432 RVA: 0x00115304 File Offset: 0x00113504
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.OnToggle += this.OnSwitchToggled;
		this.UpdateLogicCircuit();
		this.UpdateVisualState(true);
		this.wasOn = this.switchedOn;
		base.Subscribe<LogicElementSensor>(-592767678, LogicElementSensor.OnOperationalChangedDelegate);
	}

	// Token: 0x06003091 RID: 12433 RVA: 0x00115354 File Offset: 0x00113554
	public void Sim200ms(float dt)
	{
		int num = Grid.PosToCell(this);
		if (this.sampleIdx < 8)
		{
			this.samples[this.sampleIdx] = Grid.ElementIdx[num] == this.desiredElementIdx;
			this.sampleIdx++;
			return;
		}
		this.sampleIdx = 0;
		bool flag = true;
		bool[] array = this.samples;
		for (int i = 0; i < array.Length; i++)
		{
			flag = array[i] && flag;
		}
		if (base.IsSwitchedOn != flag)
		{
			this.Toggle();
		}
	}

	// Token: 0x06003092 RID: 12434 RVA: 0x001153D3 File Offset: 0x001135D3
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateLogicCircuit();
		this.UpdateVisualState(false);
	}

	// Token: 0x06003093 RID: 12435 RVA: 0x001153E4 File Offset: 0x001135E4
	private void UpdateLogicCircuit()
	{
		bool flag = this.switchedOn && base.GetComponent<Operational>().IsOperational;
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, flag ? 1 : 0);
	}

	// Token: 0x06003094 RID: 12436 RVA: 0x00115420 File Offset: 0x00113620
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

	// Token: 0x06003095 RID: 12437 RVA: 0x001154A8 File Offset: 0x001136A8
	private void OnElementSelected(Tag element_tag)
	{
		if (!element_tag.IsValid)
		{
			return;
		}
		Element element = ElementLoader.GetElement(element_tag);
		bool flag = true;
		if (element != null)
		{
			this.desiredElementIdx = ElementLoader.GetElementIndex(element.id);
			flag = element.id == SimHashes.Void || element.id == SimHashes.Vacuum;
		}
		base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.NoFilterElementSelected, flag, null);
	}

	// Token: 0x06003096 RID: 12438 RVA: 0x00115517 File Offset: 0x00113717
	private void OnOperationalChanged(object data)
	{
		this.UpdateLogicCircuit();
		this.UpdateVisualState(false);
	}

	// Token: 0x06003097 RID: 12439 RVA: 0x00115528 File Offset: 0x00113728
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x04001CFB RID: 7419
	private bool wasOn;

	// Token: 0x04001CFC RID: 7420
	public Element.State desiredState = Element.State.Gas;

	// Token: 0x04001CFD RID: 7421
	private const int WINDOW_SIZE = 8;

	// Token: 0x04001CFE RID: 7422
	private bool[] samples = new bool[8];

	// Token: 0x04001CFF RID: 7423
	private int sampleIdx;

	// Token: 0x04001D00 RID: 7424
	private ushort desiredElementIdx = ushort.MaxValue;

	// Token: 0x04001D01 RID: 7425
	private static readonly EventSystem.IntraObjectHandler<LogicElementSensor> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<LogicElementSensor>(delegate(LogicElementSensor component, object data)
	{
		component.OnOperationalChanged(data);
	});
}
