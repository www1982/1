using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020006CC RID: 1740
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/BuildingEnabledButton")]
public class BuildingEnabledButton : KMonoBehaviour, ISaveLoadable, IToggleHandler
{
	// Token: 0x17000210 RID: 528
	// (get) Token: 0x06002AE4 RID: 10980 RVA: 0x000F82FB File Offset: 0x000F64FB
	// (set) Token: 0x06002AE5 RID: 10981 RVA: 0x000F8320 File Offset: 0x000F6520
	public bool IsEnabled
	{
		get
		{
			return this.Operational != null && this.Operational.GetFlag(BuildingEnabledButton.EnabledFlag);
		}
		set
		{
			this.Operational.SetFlag(BuildingEnabledButton.EnabledFlag, value);
			Game.Instance.userMenu.Refresh(base.gameObject);
			this.buildingEnabled = value;
			base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.BuildingDisabled, !this.buildingEnabled, null);
			base.Trigger(1088293757, this.buildingEnabled);
		}
	}

	// Token: 0x17000211 RID: 529
	// (get) Token: 0x06002AE6 RID: 10982 RVA: 0x000F8395 File Offset: 0x000F6595
	public bool WaitingForDisable
	{
		get
		{
			return this.IsEnabled && this.Toggleable.IsToggleQueued(this.ToggleIdx);
		}
	}

	// Token: 0x06002AE7 RID: 10983 RVA: 0x000F83B2 File Offset: 0x000F65B2
	protected override void OnPrefabInit()
	{
		this.ToggleIdx = this.Toggleable.SetTarget(this);
		base.Subscribe<BuildingEnabledButton>(493375141, BuildingEnabledButton.OnRefreshUserMenuDelegate);
	}

	// Token: 0x06002AE8 RID: 10984 RVA: 0x000F83D7 File Offset: 0x000F65D7
	protected override void OnSpawn()
	{
		this.IsEnabled = this.buildingEnabled;
		if (this.queuedToggle)
		{
			this.OnMenuToggle();
		}
	}

	// Token: 0x06002AE9 RID: 10985 RVA: 0x000F83F3 File Offset: 0x000F65F3
	public void HandleToggle()
	{
		this.queuedToggle = false;
		Prioritizable.RemoveRef(base.gameObject);
		this.OnToggle();
	}

	// Token: 0x06002AEA RID: 10986 RVA: 0x000F840D File Offset: 0x000F660D
	public bool IsHandlerOn()
	{
		return this.IsEnabled;
	}

	// Token: 0x06002AEB RID: 10987 RVA: 0x000F8415 File Offset: 0x000F6615
	private void OnToggle()
	{
		this.IsEnabled = !this.IsEnabled;
		Game.Instance.userMenu.Refresh(base.gameObject);
	}

	// Token: 0x06002AEC RID: 10988 RVA: 0x000F843C File Offset: 0x000F663C
	private void OnMenuToggle()
	{
		if (!this.Toggleable.IsToggleQueued(this.ToggleIdx))
		{
			if (this.IsEnabled)
			{
				base.Trigger(2108245096, "BuildingDisabled");
			}
			this.queuedToggle = true;
			Prioritizable.AddRef(base.gameObject);
		}
		else
		{
			this.queuedToggle = false;
			Prioritizable.RemoveRef(base.gameObject);
		}
		this.Toggleable.Toggle(this.ToggleIdx);
		Game.Instance.userMenu.Refresh(base.gameObject);
	}

	// Token: 0x06002AED RID: 10989 RVA: 0x000F84C0 File Offset: 0x000F66C0
	private void OnRefreshUserMenu(object data)
	{
		bool isEnabled = this.IsEnabled;
		bool flag = this.Toggleable.IsToggleQueued(this.ToggleIdx);
		KIconButtonMenu.ButtonInfo buttonInfo;
		if ((isEnabled && !flag) || (!isEnabled && flag))
		{
			buttonInfo = new KIconButtonMenu.ButtonInfo("action_building_disabled", UI.USERMENUACTIONS.ENABLEBUILDING.NAME, new global::System.Action(this.OnMenuToggle), global::Action.ToggleEnabled, null, null, null, UI.USERMENUACTIONS.ENABLEBUILDING.TOOLTIP, true);
		}
		else
		{
			buttonInfo = new KIconButtonMenu.ButtonInfo("action_building_disabled", UI.USERMENUACTIONS.ENABLEBUILDING.NAME_OFF, new global::System.Action(this.OnMenuToggle), global::Action.ToggleEnabled, null, null, null, UI.USERMENUACTIONS.ENABLEBUILDING.TOOLTIP_OFF, true);
		}
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
	}

	// Token: 0x04001948 RID: 6472
	[MyCmpAdd]
	private Toggleable Toggleable;

	// Token: 0x04001949 RID: 6473
	[MyCmpReq]
	private Operational Operational;

	// Token: 0x0400194A RID: 6474
	private int ToggleIdx;

	// Token: 0x0400194B RID: 6475
	[Serialize]
	private bool buildingEnabled = true;

	// Token: 0x0400194C RID: 6476
	[Serialize]
	private bool queuedToggle;

	// Token: 0x0400194D RID: 6477
	public static readonly Operational.Flag EnabledFlag = new Operational.Flag("building_enabled", Operational.Flag.Type.Functional);

	// Token: 0x0400194E RID: 6478
	private static readonly EventSystem.IntraObjectHandler<BuildingEnabledButton> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<BuildingEnabledButton>(delegate(BuildingEnabledButton component, object data)
	{
		component.OnRefreshUserMenu(data);
	});
}
