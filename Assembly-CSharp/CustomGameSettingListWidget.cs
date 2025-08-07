using System;
using Klei.CustomSettings;
using UnityEngine;

// Token: 0x02000CA7 RID: 3239
public class CustomGameSettingListWidget : CustomGameSettingWidget
{
	// Token: 0x0600639E RID: 25502 RVA: 0x00256C53 File Offset: 0x00254E53
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.CycleLeft.onClick += this.DoCycleLeft;
		this.CycleRight.onClick += this.DoCycleRight;
	}

	// Token: 0x0600639F RID: 25503 RVA: 0x00256C89 File Offset: 0x00254E89
	public void Initialize(ListSettingConfig config, Func<SettingConfig, SettingLevel> getCallback, Func<ListSettingConfig, int, SettingLevel> cycleCallback)
	{
		this.config = config;
		this.Label.text = config.label;
		this.ToolTip.toolTip = config.tooltip;
		this.getCallback = getCallback;
		this.cycleCallback = cycleCallback;
	}

	// Token: 0x060063A0 RID: 25504 RVA: 0x00256CC4 File Offset: 0x00254EC4
	public override void Refresh()
	{
		base.Refresh();
		SettingLevel settingLevel = this.getCallback(this.config);
		this.ValueLabel.text = settingLevel.label;
		this.ValueToolTip.toolTip = settingLevel.tooltip;
		this.CycleLeft.isInteractable = !this.config.IsFirstLevel(settingLevel.id);
		this.CycleRight.isInteractable = !this.config.IsLastLevel(settingLevel.id);
	}

	// Token: 0x060063A1 RID: 25505 RVA: 0x00256D49 File Offset: 0x00254F49
	private void DoCycleLeft()
	{
		this.cycleCallback(this.config, -1);
		base.Notify();
	}

	// Token: 0x060063A2 RID: 25506 RVA: 0x00256D64 File Offset: 0x00254F64
	private void DoCycleRight()
	{
		this.cycleCallback(this.config, 1);
		base.Notify();
	}

	// Token: 0x040043C3 RID: 17347
	[SerializeField]
	private LocText Label;

	// Token: 0x040043C4 RID: 17348
	[SerializeField]
	private ToolTip ToolTip;

	// Token: 0x040043C5 RID: 17349
	[SerializeField]
	private LocText ValueLabel;

	// Token: 0x040043C6 RID: 17350
	[SerializeField]
	private ToolTip ValueToolTip;

	// Token: 0x040043C7 RID: 17351
	[SerializeField]
	private KButton CycleLeft;

	// Token: 0x040043C8 RID: 17352
	[SerializeField]
	private KButton CycleRight;

	// Token: 0x040043C9 RID: 17353
	private ListSettingConfig config;

	// Token: 0x040043CA RID: 17354
	protected Func<ListSettingConfig, int, SettingLevel> cycleCallback;

	// Token: 0x040043CB RID: 17355
	protected Func<SettingConfig, SettingLevel> getCallback;
}
