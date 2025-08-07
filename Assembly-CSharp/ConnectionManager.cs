using System;
using KSerialization;
using STRINGS;

// Token: 0x0200073D RID: 1853
public class ConnectionManager : KMonoBehaviour, ISaveLoadable, IToggleHandler
{
	// Token: 0x1700027F RID: 639
	// (get) Token: 0x06002EDC RID: 11996 RVA: 0x0010CC00 File Offset: 0x0010AE00
	// (set) Token: 0x06002EDD RID: 11997 RVA: 0x0010CC08 File Offset: 0x0010AE08
	public bool IsConnected
	{
		get
		{
			return this.connected;
		}
		set
		{
			this.connected = value;
			if (this.connectedMeter != null)
			{
				this.connectedMeter.SetPositionPercent(value ? 1f : 0f);
			}
		}
	}

	// Token: 0x17000280 RID: 640
	// (get) Token: 0x06002EDE RID: 11998 RVA: 0x0010CC33 File Offset: 0x0010AE33
	public bool WaitingForToggle
	{
		get
		{
			return this.toggleQueued;
		}
	}

	// Token: 0x06002EDF RID: 11999 RVA: 0x0010CC3B File Offset: 0x0010AE3B
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.toggleIdx = this.toggleable.SetTarget(this);
		base.Subscribe<ConnectionManager>(493375141, ConnectionManager.OnRefreshUserMenuDelegate);
	}

	// Token: 0x06002EE0 RID: 12000 RVA: 0x0010CC68 File Offset: 0x0010AE68
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.toggleQueued)
		{
			this.OnMenuToggle();
		}
		this.connectedMeter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_connected_target", "meter_connected", Meter.Offset.NoChange, Grid.SceneLayer.NoLayer, GeothermalVentConfig.CONNECTED_SYMBOLS);
		this.connectedMeter.SetPositionPercent(this.IsConnected ? 1f : 0f);
	}

	// Token: 0x06002EE1 RID: 12001 RVA: 0x0010CCCB File Offset: 0x0010AECB
	public void HandleToggle()
	{
		this.toggleQueued = false;
		Prioritizable.RemoveRef(base.gameObject);
		this.OnToggle();
	}

	// Token: 0x06002EE2 RID: 12002 RVA: 0x0010CCE5 File Offset: 0x0010AEE5
	private void OnToggle()
	{
		this.IsConnected = !this.IsConnected;
		Game.Instance.userMenu.Refresh(base.gameObject);
	}

	// Token: 0x06002EE3 RID: 12003 RVA: 0x0010CD0C File Offset: 0x0010AF0C
	private void OnMenuToggle()
	{
		if (!this.toggleable.IsToggleQueued(this.toggleIdx))
		{
			if (this.IsConnected)
			{
				base.Trigger(2108245096, "BuildingDisabled");
			}
			this.toggleQueued = true;
			Prioritizable.AddRef(base.gameObject);
		}
		else
		{
			this.toggleQueued = false;
			Prioritizable.RemoveRef(base.gameObject);
		}
		this.toggleable.Toggle(this.toggleIdx);
		Game.Instance.userMenu.Refresh(base.gameObject);
	}

	// Token: 0x06002EE4 RID: 12004 RVA: 0x0010CD90 File Offset: 0x0010AF90
	private void OnRefreshUserMenu(object data)
	{
		if (!this.showButton)
		{
			return;
		}
		bool isConnected = this.IsConnected;
		bool flag = this.toggleable.IsToggleQueued(this.toggleIdx);
		KIconButtonMenu.ButtonInfo buttonInfo;
		if ((isConnected && !flag) || (!isConnected && flag))
		{
			buttonInfo = new KIconButtonMenu.ButtonInfo("action_building_disabled", COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.BUTTONS.DISCONNECT_TITLE, new global::System.Action(this.OnMenuToggle), global::Action.ToggleEnabled, null, null, null, COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.BUTTONS.DISCONNECT_TOOLTIP, true);
		}
		else
		{
			buttonInfo = new KIconButtonMenu.ButtonInfo("action_building_disabled", COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.BUTTONS.RECONNECT_TITLE, new global::System.Action(this.OnMenuToggle), global::Action.ToggleEnabled, null, null, null, COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.BUTTONS.RECONNECT_TOOLTIP, true);
		}
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
	}

	// Token: 0x06002EE5 RID: 12005 RVA: 0x0010CE54 File Offset: 0x0010B054
	bool IToggleHandler.IsHandlerOn()
	{
		return this.IsConnected;
	}

	// Token: 0x04001BB4 RID: 7092
	[MyCmpAdd]
	private ToggleGeothermalVentConnection toggleable;

	// Token: 0x04001BB5 RID: 7093
	[MyCmpGet]
	private GeothermalVent vent;

	// Token: 0x04001BB6 RID: 7094
	private int toggleIdx;

	// Token: 0x04001BB7 RID: 7095
	private MeterController connectedMeter;

	// Token: 0x04001BB8 RID: 7096
	public bool showButton;

	// Token: 0x04001BB9 RID: 7097
	[Serialize]
	private bool connected;

	// Token: 0x04001BBA RID: 7098
	[Serialize]
	private bool toggleQueued;

	// Token: 0x04001BBB RID: 7099
	private static readonly EventSystem.IntraObjectHandler<ConnectionManager> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<ConnectionManager>(delegate(ConnectionManager component, object data)
	{
		component.OnRefreshUserMenu(data);
	});
}
