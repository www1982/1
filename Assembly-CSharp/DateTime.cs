using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000CAF RID: 3247
public class DateTime : KScreen
{
	// Token: 0x060063D2 RID: 25554 RVA: 0x00257669 File Offset: 0x00255869
	public static void DestroyInstance()
	{
		global::DateTime.Instance = null;
	}

	// Token: 0x060063D3 RID: 25555 RVA: 0x00257671 File Offset: 0x00255871
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		global::DateTime.Instance = this;
		this.milestoneEffect.gameObject.SetActive(false);
	}

	// Token: 0x060063D4 RID: 25556 RVA: 0x00257690 File Offset: 0x00255890
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.tooltip.OnComplexToolTip = new ToolTip.ComplexTooltipDelegate(this.BuildTooltip);
		Game.Instance.Subscribe(2070437606, new Action<object>(this.OnMilestoneDayReached));
		Game.Instance.Subscribe(-720092972, new Action<object>(this.OnMilestoneDayApproaching));
	}

	// Token: 0x060063D5 RID: 25557 RVA: 0x002576F4 File Offset: 0x002558F4
	private List<global::Tuple<string, TextStyleSetting>> BuildTooltip()
	{
		List<global::Tuple<string, TextStyleSetting>> colonyToolTip = SaveGame.Instance.GetColonyToolTip();
		if (TimeOfDay.IsMilestoneApproaching)
		{
			colonyToolTip.Add(new global::Tuple<string, TextStyleSetting>(" ", null));
			colonyToolTip.Add(new global::Tuple<string, TextStyleSetting>(UI.ASTEROIDCLOCK.MILESTONE_TITLE.text, ToolTipScreen.Instance.defaultTooltipHeaderStyle));
			colonyToolTip.Add(new global::Tuple<string, TextStyleSetting>(UI.ASTEROIDCLOCK.MILESTONE_DESCRIPTION.text.Replace("{0}", (GameClock.Instance.GetCycle() + 2).ToString()), ToolTipScreen.Instance.defaultTooltipBodyStyle));
		}
		return colonyToolTip;
	}

	// Token: 0x060063D6 RID: 25558 RVA: 0x00257781 File Offset: 0x00255981
	private void Update()
	{
		if (GameClock.Instance != null && this.displayedDayCount != GameUtil.GetCurrentCycle())
		{
			this.text.text = this.Days();
			this.displayedDayCount = GameUtil.GetCurrentCycle();
		}
	}

	// Token: 0x060063D7 RID: 25559 RVA: 0x002577B9 File Offset: 0x002559B9
	private void OnMilestoneDayApproaching(object data)
	{
		int num = (int)data;
		this.milestoneEffect.gameObject.SetActive(true);
		this.milestoneEffect.Play("100fx_pre", KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x060063D8 RID: 25560 RVA: 0x002577F3 File Offset: 0x002559F3
	private void OnMilestoneDayReached(object data)
	{
		int num = (int)data;
		this.milestoneEffect.gameObject.SetActive(true);
		this.milestoneEffect.Play("100fx", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x060063D9 RID: 25561 RVA: 0x00257830 File Offset: 0x00255A30
	private string Days()
	{
		return GameUtil.GetCurrentCycle().ToString();
	}

	// Token: 0x040043F0 RID: 17392
	public static global::DateTime Instance;

	// Token: 0x040043F1 RID: 17393
	private const string MILESTONE_ANTICIPATION_ANIMATION_NAME = "100fx_pre";

	// Token: 0x040043F2 RID: 17394
	private const string MILESTONE_ANIMATION_NAME = "100fx";

	// Token: 0x040043F3 RID: 17395
	public LocText day;

	// Token: 0x040043F4 RID: 17396
	private int displayedDayCount = -1;

	// Token: 0x040043F5 RID: 17397
	[SerializeField]
	private KBatchedAnimController milestoneEffect;

	// Token: 0x040043F6 RID: 17398
	[SerializeField]
	private LocText text;

	// Token: 0x040043F7 RID: 17399
	[SerializeField]
	private ToolTip tooltip;

	// Token: 0x040043F8 RID: 17400
	[SerializeField]
	private TextStyleSetting tooltipstyle_Days;

	// Token: 0x040043F9 RID: 17401
	[SerializeField]
	private TextStyleSetting tooltipstyle_Playtime;

	// Token: 0x040043FA RID: 17402
	[SerializeField]
	public KToggle scheduleToggle;
}
