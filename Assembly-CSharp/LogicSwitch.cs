using System;
using System.Collections;
using KSerialization;
using UnityEngine;

// Token: 0x02000774 RID: 1908
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicSwitch : Switch, IPlayerControlledToggle, ISim33ms
{
	// Token: 0x060031E4 RID: 12772 RVA: 0x0011ADFA File Offset: 0x00118FFA
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicSwitch>(-905833192, LogicSwitch.OnCopySettingsDelegate);
	}

	// Token: 0x060031E5 RID: 12773 RVA: 0x0011AE14 File Offset: 0x00119014
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.wasOn = this.switchedOn;
		this.UpdateLogicCircuit();
		base.GetComponent<KBatchedAnimController>().Play(this.switchedOn ? "on" : "off", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x060031E6 RID: 12774 RVA: 0x0011AE68 File Offset: 0x00119068
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x060031E7 RID: 12775 RVA: 0x0011AE70 File Offset: 0x00119070
	private void OnCopySettings(object data)
	{
		LogicSwitch component = ((GameObject)data).GetComponent<LogicSwitch>();
		if (component != null && this.switchedOn != component.switchedOn)
		{
			this.switchedOn = component.switchedOn;
			this.UpdateVisualization();
			this.UpdateLogicCircuit();
		}
	}

	// Token: 0x060031E8 RID: 12776 RVA: 0x0011AEB8 File Offset: 0x001190B8
	protected override void Toggle()
	{
		base.Toggle();
		this.UpdateVisualization();
		this.UpdateLogicCircuit();
	}

	// Token: 0x060031E9 RID: 12777 RVA: 0x0011AECC File Offset: 0x001190CC
	private void UpdateVisualization()
	{
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		if (this.wasOn != this.switchedOn)
		{
			component.Play(this.switchedOn ? "on_pre" : "on_pst", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue(this.switchedOn ? "on" : "off", KAnim.PlayMode.Once, 1f, 0f);
		}
		this.wasOn = this.switchedOn;
	}

	// Token: 0x060031EA RID: 12778 RVA: 0x0011AF4E File Offset: 0x0011914E
	private void UpdateLogicCircuit()
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn ? 1 : 0);
	}

	// Token: 0x060031EB RID: 12779 RVA: 0x0011AF6C File Offset: 0x0011916C
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSwitchStatusActive : Db.Get().BuildingStatusItems.LogicSwitchStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x060031EC RID: 12780 RVA: 0x0011AFBF File Offset: 0x001191BF
	public void Sim33ms(float dt)
	{
		if (this.ToggleRequested)
		{
			this.Toggle();
			this.ToggleRequested = false;
			this.GetSelectable().SetStatusItem(Db.Get().StatusItemCategories.Main, null, null);
		}
	}

	// Token: 0x060031ED RID: 12781 RVA: 0x0011AFF3 File Offset: 0x001191F3
	public void SetFirstFrameCallback(global::System.Action ffCb)
	{
		this.firstFrameCallback = ffCb;
		base.StartCoroutine(this.RunCallback());
	}

	// Token: 0x060031EE RID: 12782 RVA: 0x0011B009 File Offset: 0x00119209
	private IEnumerator RunCallback()
	{
		yield return null;
		if (this.firstFrameCallback != null)
		{
			this.firstFrameCallback();
			this.firstFrameCallback = null;
		}
		yield return null;
		yield break;
	}

	// Token: 0x060031EF RID: 12783 RVA: 0x0011B018 File Offset: 0x00119218
	public void ToggledByPlayer()
	{
		this.Toggle();
	}

	// Token: 0x060031F0 RID: 12784 RVA: 0x0011B020 File Offset: 0x00119220
	public bool ToggledOn()
	{
		return this.switchedOn;
	}

	// Token: 0x060031F1 RID: 12785 RVA: 0x0011B028 File Offset: 0x00119228
	public KSelectable GetSelectable()
	{
		return base.GetComponent<KSelectable>();
	}

	// Token: 0x170002FF RID: 767
	// (get) Token: 0x060031F2 RID: 12786 RVA: 0x0011B030 File Offset: 0x00119230
	public string SideScreenTitleKey
	{
		get
		{
			return "STRINGS.BUILDINGS.PREFABS.LOGICSWITCH.SIDESCREEN_TITLE";
		}
	}

	// Token: 0x17000300 RID: 768
	// (get) Token: 0x060031F3 RID: 12787 RVA: 0x0011B037 File Offset: 0x00119237
	// (set) Token: 0x060031F4 RID: 12788 RVA: 0x0011B03F File Offset: 0x0011923F
	public bool ToggleRequested { get; set; }

	// Token: 0x04001E01 RID: 7681
	public static readonly HashedString PORT_ID = "LogicSwitch";

	// Token: 0x04001E02 RID: 7682
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001E03 RID: 7683
	private static readonly EventSystem.IntraObjectHandler<LogicSwitch> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicSwitch>(delegate(LogicSwitch component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x04001E04 RID: 7684
	private bool wasOn;

	// Token: 0x04001E05 RID: 7685
	private global::System.Action firstFrameCallback;
}
