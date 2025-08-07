using System;
using KSerialization;
using UnityEngine;

// Token: 0x020006ED RID: 1773
[SerializationConfig(MemberSerialization.OptIn)]
public class CircuitSwitch : Switch, IPlayerControlledToggle, ISim33ms
{
	// Token: 0x06002C13 RID: 11283 RVA: 0x000FDA88 File Offset: 0x000FBC88
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<CircuitSwitch>(-905833192, CircuitSwitch.OnCopySettingsDelegate);
	}

	// Token: 0x06002C14 RID: 11284 RVA: 0x000FDAA4 File Offset: 0x000FBCA4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.OnToggle += this.CircuitOnToggle;
		int num = Grid.PosToCell(base.transform.GetPosition());
		GameObject gameObject = Grid.Objects[num, (int)this.objectLayer];
		Wire wire = ((gameObject != null) ? gameObject.GetComponent<Wire>() : null);
		if (wire == null)
		{
			this.wireConnectedGUID = base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.NoWireConnected, null);
		}
		this.AttachWire(wire);
		this.wasOn = this.switchedOn;
		this.UpdateCircuit(true);
		base.GetComponent<KBatchedAnimController>().Play(this.switchedOn ? "on" : "off", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x06002C15 RID: 11285 RVA: 0x000FDB74 File Offset: 0x000FBD74
	protected override void OnCleanUp()
	{
		if (this.attachedWire != null)
		{
			this.UnsubscribeFromWire(this.attachedWire);
		}
		bool switchedOn = this.switchedOn;
		this.switchedOn = true;
		this.UpdateCircuit(false);
		this.switchedOn = switchedOn;
	}

	// Token: 0x06002C16 RID: 11286 RVA: 0x000FDBB8 File Offset: 0x000FBDB8
	private void OnCopySettings(object data)
	{
		CircuitSwitch component = ((GameObject)data).GetComponent<CircuitSwitch>();
		if (component != null)
		{
			this.switchedOn = component.switchedOn;
			this.UpdateCircuit(true);
		}
	}

	// Token: 0x06002C17 RID: 11287 RVA: 0x000FDBF0 File Offset: 0x000FBDF0
	public bool IsConnected()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		GameObject gameObject = Grid.Objects[num, (int)this.objectLayer];
		return gameObject != null && gameObject.GetComponent<IDisconnectable>() != null;
	}

	// Token: 0x06002C18 RID: 11288 RVA: 0x000FDC34 File Offset: 0x000FBE34
	private void CircuitOnToggle(bool on)
	{
		this.UpdateCircuit(true);
	}

	// Token: 0x06002C19 RID: 11289 RVA: 0x000FDC40 File Offset: 0x000FBE40
	public void AttachWire(Wire wire)
	{
		if (wire == this.attachedWire)
		{
			return;
		}
		if (this.attachedWire != null)
		{
			this.UnsubscribeFromWire(this.attachedWire);
		}
		this.attachedWire = wire;
		if (this.attachedWire != null)
		{
			this.SubscribeToWire(this.attachedWire);
			this.UpdateCircuit(true);
			this.wireConnectedGUID = base.GetComponent<KSelectable>().RemoveStatusItem(this.wireConnectedGUID, false);
			return;
		}
		if (this.wireConnectedGUID == Guid.Empty)
		{
			this.wireConnectedGUID = base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.NoWireConnected, null);
		}
	}

	// Token: 0x06002C1A RID: 11290 RVA: 0x000FDCEA File Offset: 0x000FBEEA
	private void OnWireDestroyed(object data)
	{
		if (this.attachedWire != null)
		{
			this.attachedWire.Unsubscribe(1969584890, new Action<object>(this.OnWireDestroyed));
		}
	}

	// Token: 0x06002C1B RID: 11291 RVA: 0x000FDD16 File Offset: 0x000FBF16
	private void OnWireStateChanged(object data)
	{
		this.UpdateCircuit(true);
	}

	// Token: 0x06002C1C RID: 11292 RVA: 0x000FDD20 File Offset: 0x000FBF20
	private void SubscribeToWire(Wire wire)
	{
		wire.Subscribe(1969584890, new Action<object>(this.OnWireDestroyed));
		wire.Subscribe(-1735440190, new Action<object>(this.OnWireStateChanged));
		wire.Subscribe(774203113, new Action<object>(this.OnWireStateChanged));
	}

	// Token: 0x06002C1D RID: 11293 RVA: 0x000FDD78 File Offset: 0x000FBF78
	private void UnsubscribeFromWire(Wire wire)
	{
		wire.Unsubscribe(1969584890, new Action<object>(this.OnWireDestroyed));
		wire.Unsubscribe(-1735440190, new Action<object>(this.OnWireStateChanged));
		wire.Unsubscribe(774203113, new Action<object>(this.OnWireStateChanged));
	}

	// Token: 0x06002C1E RID: 11294 RVA: 0x000FDDCC File Offset: 0x000FBFCC
	private void UpdateCircuit(bool should_update_anim = true)
	{
		if (this.attachedWire != null)
		{
			if (this.switchedOn)
			{
				this.attachedWire.Connect();
			}
			else
			{
				this.attachedWire.Disconnect();
			}
		}
		if (should_update_anim && this.wasOn != this.switchedOn)
		{
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			component.Play(this.switchedOn ? "on_pre" : "on_pst", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue(this.switchedOn ? "on" : "off", KAnim.PlayMode.Once, 1f, 0f);
			Game.Instance.userMenu.Refresh(base.gameObject);
		}
		this.wasOn = this.switchedOn;
	}

	// Token: 0x06002C1F RID: 11295 RVA: 0x000FDE93 File Offset: 0x000FC093
	public void Sim33ms(float dt)
	{
		if (this.ToggleRequested)
		{
			this.Toggle();
			this.ToggleRequested = false;
			this.GetSelectable().SetStatusItem(Db.Get().StatusItemCategories.Main, null, null);
		}
	}

	// Token: 0x06002C20 RID: 11296 RVA: 0x000FDEC7 File Offset: 0x000FC0C7
	public void ToggledByPlayer()
	{
		this.Toggle();
	}

	// Token: 0x06002C21 RID: 11297 RVA: 0x000FDECF File Offset: 0x000FC0CF
	public bool ToggledOn()
	{
		return this.switchedOn;
	}

	// Token: 0x06002C22 RID: 11298 RVA: 0x000FDED7 File Offset: 0x000FC0D7
	public KSelectable GetSelectable()
	{
		return base.GetComponent<KSelectable>();
	}

	// Token: 0x17000240 RID: 576
	// (get) Token: 0x06002C23 RID: 11299 RVA: 0x000FDEDF File Offset: 0x000FC0DF
	public string SideScreenTitleKey
	{
		get
		{
			return "STRINGS.BUILDINGS.PREFABS.SWITCH.SIDESCREEN_TITLE";
		}
	}

	// Token: 0x17000241 RID: 577
	// (get) Token: 0x06002C24 RID: 11300 RVA: 0x000FDEE6 File Offset: 0x000FC0E6
	// (set) Token: 0x06002C25 RID: 11301 RVA: 0x000FDEEE File Offset: 0x000FC0EE
	public bool ToggleRequested { get; set; }

	// Token: 0x04001A09 RID: 6665
	[SerializeField]
	public ObjectLayer objectLayer;

	// Token: 0x04001A0A RID: 6666
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001A0B RID: 6667
	private static readonly EventSystem.IntraObjectHandler<CircuitSwitch> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<CircuitSwitch>(delegate(CircuitSwitch component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x04001A0C RID: 6668
	private Wire attachedWire;

	// Token: 0x04001A0D RID: 6669
	private Guid wireConnectedGUID;

	// Token: 0x04001A0E RID: 6670
	private bool wasOn;
}
