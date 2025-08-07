using System;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E42 RID: 3650
public class TimeRangeSideScreen : SideScreenContent, IRender200ms
{
	// Token: 0x060073B6 RID: 29622 RVA: 0x002BF254 File Offset: 0x002BD454
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.labelHeaderStart.text = UI.UISIDESCREENS.TIME_RANGE_SIDE_SCREEN.ON;
		this.labelHeaderDuration.text = UI.UISIDESCREENS.TIME_RANGE_SIDE_SCREEN.DURATION;
	}

	// Token: 0x060073B7 RID: 29623 RVA: 0x002BF286 File Offset: 0x002BD486
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<LogicTimeOfDaySensor>() != null;
	}

	// Token: 0x060073B8 RID: 29624 RVA: 0x002BF294 File Offset: 0x002BD494
	public override void SetTarget(GameObject target)
	{
		this.imageActiveZone.color = GlobalAssets.Instance.colorSet.logicOnSidescreen;
		this.imageInactiveZone.color = GlobalAssets.Instance.colorSet.logicOffSidescreen;
		base.SetTarget(target);
		this.targetTimedSwitch = target.GetComponent<LogicTimeOfDaySensor>();
		this.duration.onValueChanged.RemoveAllListeners();
		this.startTime.onValueChanged.RemoveAllListeners();
		this.startTime.value = this.targetTimedSwitch.startTime;
		this.duration.value = this.targetTimedSwitch.duration;
		this.ChangeSetting();
		this.startTime.onValueChanged.AddListener(delegate(float value)
		{
			this.ChangeSetting();
		});
		this.duration.onValueChanged.AddListener(delegate(float value)
		{
			this.ChangeSetting();
		});
	}

	// Token: 0x060073B9 RID: 29625 RVA: 0x002BF37C File Offset: 0x002BD57C
	private void ChangeSetting()
	{
		this.targetTimedSwitch.startTime = this.startTime.value;
		this.targetTimedSwitch.duration = this.duration.value;
		this.imageActiveZone.rectTransform.rotation = Quaternion.identity;
		this.imageActiveZone.rectTransform.Rotate(0f, 0f, this.NormalizedValueToDegrees(this.startTime.value));
		this.imageActiveZone.fillAmount = this.duration.value;
		this.labelValueStart.text = GameUtil.GetFormattedPercent(this.targetTimedSwitch.startTime * 100f, GameUtil.TimeSlice.None);
		this.labelValueDuration.text = GameUtil.GetFormattedPercent(this.targetTimedSwitch.duration * 100f, GameUtil.TimeSlice.None);
		this.endIndicator.rotation = Quaternion.identity;
		this.endIndicator.Rotate(0f, 0f, this.NormalizedValueToDegrees(this.startTime.value + this.duration.value));
		this.startTime.SetTooltipText(string.Format(UI.UISIDESCREENS.TIME_RANGE_SIDE_SCREEN.ON_TOOLTIP, GameUtil.GetFormattedPercent(this.targetTimedSwitch.startTime * 100f, GameUtil.TimeSlice.None)));
		this.duration.SetTooltipText(string.Format(UI.UISIDESCREENS.TIME_RANGE_SIDE_SCREEN.DURATION_TOOLTIP, GameUtil.GetFormattedPercent(this.targetTimedSwitch.duration * 100f, GameUtil.TimeSlice.None)));
	}

	// Token: 0x060073BA RID: 29626 RVA: 0x002BF4F3 File Offset: 0x002BD6F3
	public void Render200ms(float dt)
	{
		this.currentTimeMarker.rotation = Quaternion.identity;
		this.currentTimeMarker.Rotate(0f, 0f, this.NormalizedValueToDegrees(GameClock.Instance.GetCurrentCycleAsPercentage()));
	}

	// Token: 0x060073BB RID: 29627 RVA: 0x002BF52A File Offset: 0x002BD72A
	private float NormalizedValueToDegrees(float value)
	{
		return 360f * value;
	}

	// Token: 0x060073BC RID: 29628 RVA: 0x002BF533 File Offset: 0x002BD733
	private float SecondsToDegrees(float seconds)
	{
		return 360f * (seconds / 600f);
	}

	// Token: 0x060073BD RID: 29629 RVA: 0x002BF542 File Offset: 0x002BD742
	private float DegreesToNormalizedValue(float degrees)
	{
		return degrees / 360f;
	}

	// Token: 0x04004FA2 RID: 20386
	public Image imageInactiveZone;

	// Token: 0x04004FA3 RID: 20387
	public Image imageActiveZone;

	// Token: 0x04004FA4 RID: 20388
	private LogicTimeOfDaySensor targetTimedSwitch;

	// Token: 0x04004FA5 RID: 20389
	public KSlider startTime;

	// Token: 0x04004FA6 RID: 20390
	public KSlider duration;

	// Token: 0x04004FA7 RID: 20391
	public RectTransform endIndicator;

	// Token: 0x04004FA8 RID: 20392
	public LocText labelHeaderStart;

	// Token: 0x04004FA9 RID: 20393
	public LocText labelHeaderDuration;

	// Token: 0x04004FAA RID: 20394
	public LocText labelValueStart;

	// Token: 0x04004FAB RID: 20395
	public LocText labelValueDuration;

	// Token: 0x04004FAC RID: 20396
	public RectTransform currentTimeMarker;
}
