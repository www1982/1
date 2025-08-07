using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020007DA RID: 2010
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/Switch")]
public class Switch : KMonoBehaviour, ISaveLoadable, IToggleHandler
{
	// Token: 0x17000398 RID: 920
	// (get) Token: 0x06003640 RID: 13888 RVA: 0x0012E327 File Offset: 0x0012C527
	public bool IsSwitchedOn
	{
		get
		{
			return this.switchedOn;
		}
	}

	// Token: 0x14000013 RID: 19
	// (add) Token: 0x06003641 RID: 13889 RVA: 0x0012E330 File Offset: 0x0012C530
	// (remove) Token: 0x06003642 RID: 13890 RVA: 0x0012E368 File Offset: 0x0012C568
	public event Action<bool> OnToggle;

	// Token: 0x06003643 RID: 13891 RVA: 0x0012E39D File Offset: 0x0012C59D
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.switchedOn = this.defaultState;
	}

	// Token: 0x06003644 RID: 13892 RVA: 0x0012E3B4 File Offset: 0x0012C5B4
	protected override void OnSpawn()
	{
		this.openToggleIndex = this.openSwitch.SetTarget(this);
		if (this.OnToggle != null)
		{
			this.OnToggle(this.switchedOn);
		}
		if (this.manuallyControlled)
		{
			base.Subscribe<Switch>(493375141, Switch.OnRefreshUserMenuDelegate);
		}
		this.UpdateSwitchStatus();
	}

	// Token: 0x06003645 RID: 13893 RVA: 0x0012E40B File Offset: 0x0012C60B
	public void HandleToggle()
	{
		this.Toggle();
	}

	// Token: 0x06003646 RID: 13894 RVA: 0x0012E413 File Offset: 0x0012C613
	public bool IsHandlerOn()
	{
		return this.switchedOn;
	}

	// Token: 0x06003647 RID: 13895 RVA: 0x0012E41B File Offset: 0x0012C61B
	private void OnMinionToggle()
	{
		if (!DebugHandler.InstantBuildMode)
		{
			this.openSwitch.Toggle(this.openToggleIndex);
			return;
		}
		this.Toggle();
	}

	// Token: 0x06003648 RID: 13896 RVA: 0x0012E43C File Offset: 0x0012C63C
	protected virtual void Toggle()
	{
		this.SetState(!this.switchedOn);
	}

	// Token: 0x06003649 RID: 13897 RVA: 0x0012E450 File Offset: 0x0012C650
	protected virtual void SetState(bool on)
	{
		if (this.switchedOn != on)
		{
			this.switchedOn = on;
			this.UpdateSwitchStatus();
			if (this.OnToggle != null)
			{
				this.OnToggle(this.switchedOn);
			}
			if (this.manuallyControlled)
			{
				Game.Instance.userMenu.Refresh(base.gameObject);
			}
		}
	}

	// Token: 0x0600364A RID: 13898 RVA: 0x0012E4AC File Offset: 0x0012C6AC
	protected virtual void OnRefreshUserMenu(object data)
	{
		LocString locString = (this.switchedOn ? BUILDINGS.PREFABS.SWITCH.TURN_OFF : BUILDINGS.PREFABS.SWITCH.TURN_ON);
		LocString locString2 = (this.switchedOn ? BUILDINGS.PREFABS.SWITCH.TURN_OFF_TOOLTIP : BUILDINGS.PREFABS.SWITCH.TURN_ON_TOOLTIP);
		Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("action_power", locString, new global::System.Action(this.OnMinionToggle), global::Action.ToggleEnabled, null, null, null, locString2, true), 1f);
	}

	// Token: 0x0600364B RID: 13899 RVA: 0x0012E528 File Offset: 0x0012C728
	protected virtual void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.SwitchStatusActive : Db.Get().BuildingStatusItems.SwitchStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x040020C4 RID: 8388
	[SerializeField]
	public bool manuallyControlled = true;

	// Token: 0x040020C5 RID: 8389
	[SerializeField]
	public bool defaultState = true;

	// Token: 0x040020C6 RID: 8390
	[Serialize]
	protected bool switchedOn = true;

	// Token: 0x040020C7 RID: 8391
	[MyCmpAdd]
	private Toggleable openSwitch;

	// Token: 0x040020C8 RID: 8392
	private int openToggleIndex;

	// Token: 0x040020CA RID: 8394
	private static readonly EventSystem.IntraObjectHandler<Switch> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<Switch>(delegate(Switch component, object data)
	{
		component.OnRefreshUserMenu(data);
	});
}
