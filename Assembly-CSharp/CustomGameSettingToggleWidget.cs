using System;
using Klei.CustomSettings;
using UnityEngine;

// Token: 0x02000CA9 RID: 3241
public class CustomGameSettingToggleWidget : CustomGameSettingWidget
{
	// Token: 0x060063AD RID: 25517 RVA: 0x0025700F File Offset: 0x0025520F
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		MultiToggle toggle = this.Toggle;
		toggle.onClick = (global::System.Action)Delegate.Combine(toggle.onClick, new global::System.Action(this.ToggleSetting));
	}

	// Token: 0x060063AE RID: 25518 RVA: 0x0025703E File Offset: 0x0025523E
	public void Initialize(ToggleSettingConfig config, Func<SettingConfig, SettingLevel> getCurrentSettingCallback, Func<ToggleSettingConfig, SettingLevel> toggleCallback)
	{
		this.config = config;
		this.Label.text = config.label;
		this.ToolTip.toolTip = config.tooltip;
		this.getCurrentSettingCallback = getCurrentSettingCallback;
		this.toggleCallback = toggleCallback;
	}

	// Token: 0x060063AF RID: 25519 RVA: 0x00257078 File Offset: 0x00255278
	public override void Refresh()
	{
		base.Refresh();
		SettingLevel settingLevel = this.getCurrentSettingCallback(this.config);
		this.Toggle.ChangeState(this.config.IsOnLevel(settingLevel.id) ? 1 : 0);
		this.ToggleToolTip.toolTip = settingLevel.tooltip;
	}

	// Token: 0x060063B0 RID: 25520 RVA: 0x002570D0 File Offset: 0x002552D0
	public void ToggleSetting()
	{
		this.toggleCallback(this.config);
		base.Notify();
	}

	// Token: 0x040043D5 RID: 17365
	[SerializeField]
	private LocText Label;

	// Token: 0x040043D6 RID: 17366
	[SerializeField]
	private ToolTip ToolTip;

	// Token: 0x040043D7 RID: 17367
	[SerializeField]
	private MultiToggle Toggle;

	// Token: 0x040043D8 RID: 17368
	[SerializeField]
	private ToolTip ToggleToolTip;

	// Token: 0x040043D9 RID: 17369
	private ToggleSettingConfig config;

	// Token: 0x040043DA RID: 17370
	protected Func<SettingConfig, SettingLevel> getCurrentSettingCallback;

	// Token: 0x040043DB RID: 17371
	protected Func<ToggleSettingConfig, SettingLevel> toggleCallback;
}
