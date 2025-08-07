using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B49 RID: 2889
public class LargeImpactorNotificationUI_Clock : KMonoBehaviour, ISim4000ms
{
	// Token: 0x060055F9 RID: 22009 RVA: 0x001F35C4 File Offset: 0x001F17C4
	protected override void OnSpawn()
	{
		this.color_circleFillOriginalColor = this.TimerOutCircleFill.color;
		this.color_needleTrailBgOriginalColor = this.NeedleTrailBg.color;
		this.color_timerOutCircleBGOriginalColor = this.TimerOutCircleBG.color;
		this.softRed = new Color(1f, 0f, 0f, this.color_needleTrailBgOriginalColor.a);
		GameClock.Instance.Subscribe(631075836, new Action<object>(this.OnNewCycleReached));
		this.UpdateSmallNeedlePosition();
		this.InitializeAnimationCoroutine();
		this.hasSpawned = true;
	}

	// Token: 0x060055FA RID: 22010 RVA: 0x001F3658 File Offset: 0x001F1858
	private void OnNewCycleReached(object data)
	{
		this.PlayReminderAnimation();
	}

	// Token: 0x060055FB RID: 22011 RVA: 0x001F3660 File Offset: 0x001F1860
	public void SetLargeImpactorTime(float normalizedValue)
	{
		this.lastLargeImpactorTime = normalizedValue;
		this.SetNeedleRotation(this.LargeNeedle.rectTransform, 1f - this.lastLargeImpactorTime);
		if (!this.hasPlayedEntryAnimation)
		{
			return;
		}
		this.TimerOutCircleFill.fillAmount = this.lastLargeImpactorTime;
	}

	// Token: 0x060055FC RID: 22012 RVA: 0x001F36A0 File Offset: 0x001F18A0
	private void SetNeedleRotation(RectTransform needle, float normalizedTime)
	{
		needle.localRotation = Quaternion.Euler(0f, 0f, -360f * normalizedTime);
		if (needle.gameObject == this.LargeNeedle.gameObject)
		{
			this.NeedleTrailBg.fillAmount = normalizedTime;
		}
	}

	// Token: 0x060055FD RID: 22013 RVA: 0x001F36ED File Offset: 0x001F18ED
	public void Sim4000ms(float dt)
	{
		this.UpdateSmallNeedlePosition();
	}

	// Token: 0x060055FE RID: 22014 RVA: 0x001F36F8 File Offset: 0x001F18F8
	private void UpdateSmallNeedlePosition()
	{
		float currentCycleAsPercentage = GameClock.Instance.GetCurrentCycleAsPercentage();
		this.SetNeedleRotation(this.SmallNeedlePivot, currentCycleAsPercentage);
	}

	// Token: 0x060055FF RID: 22015 RVA: 0x001F371D File Offset: 0x001F191D
	private void InitializeAnimationCoroutine()
	{
		this.AbortCoroutine();
		this.animationCoroutine = base.StartCoroutine(this.AnimationCoroutineLogic());
	}

	// Token: 0x06005600 RID: 22016 RVA: 0x001F3737 File Offset: 0x001F1937
	private void AbortCoroutine()
	{
		if (this.animationCoroutine != null)
		{
			base.StopAllCoroutines();
		}
		this.animationCoroutine = null;
	}

	// Token: 0x06005601 RID: 22017 RVA: 0x001F374E File Offset: 0x001F194E
	public void PlayReminderAnimation()
	{
		this.reminderAnimationTimer = 0f;
	}

	// Token: 0x06005602 RID: 22018 RVA: 0x001F375B File Offset: 0x001F195B
	private IEnumerator AnimationCoroutineLogic()
	{
		if (!this.hasPlayedEntryAnimation)
		{
			GameClock.Instance.GetCurrentCycleAsPercentage();
			float num = this.entryAnimationDuration / 600f;
			yield return this.Interpolate(delegate(float n)
			{
				this.TimerOutCircleFill.fillAmount = n * this.lastLargeImpactorTime;
			}, this.entryAnimationDuration, null);
			this.hasPlayedEntryAnimation = true;
		}
		for (;;)
		{
			if (this.reminderAnimationTimer < 0f)
			{
				yield return null;
			}
			if (this.reminderAnimationTimer >= 0f && this.reminderAnimationTimer < this.reminderAnimationDuration)
			{
				float num2 = Mathf.Abs(Mathf.Sin(this.reminderAnimationTimer / this.reminderAnimationDuration * 3.1415927f * 16f));
				this.TimerOutCircleBG.color = Color.Lerp(this.color_timerOutCircleBGOriginalColor, Color.red, num2);
				this.NeedleTrailBg.color = Color.Lerp(this.color_needleTrailBgOriginalColor, this.softRed, num2);
				this.reminderAnimationTimer += Time.deltaTime;
				yield return null;
			}
			if (this.reminderAnimationTimer >= this.reminderAnimationDuration)
			{
				this.TimerOutCircleBG.color = this.color_timerOutCircleBGOriginalColor;
				this.NeedleTrailBg.color = this.color_needleTrailBgOriginalColor;
				this.reminderAnimationTimer = -1f;
				yield return null;
			}
		}
		yield break;
	}

	// Token: 0x06005603 RID: 22019 RVA: 0x001F376A File Offset: 0x001F196A
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		if (this.hasSpawned)
		{
			this.InitializeAnimationCoroutine();
		}
	}

	// Token: 0x06005604 RID: 22020 RVA: 0x001F3780 File Offset: 0x001F1980
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		this.AbortCoroutine();
	}

	// Token: 0x06005605 RID: 22021 RVA: 0x001F378E File Offset: 0x001F198E
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		GameClock.Instance.Unsubscribe(631075836, new Action<object>(this.OnNewCycleReached));
	}

	// Token: 0x04003960 RID: 14688
	public KImage LargeNeedle;

	// Token: 0x04003961 RID: 14689
	public RectTransform SmallNeedlePivot;

	// Token: 0x04003962 RID: 14690
	public KImage NeedleTrailBg;

	// Token: 0x04003963 RID: 14691
	public Image TimerOutCircleFill;

	// Token: 0x04003964 RID: 14692
	public Image TimerOutCircleBG;

	// Token: 0x04003965 RID: 14693
	private Color color_circleFillOriginalColor;

	// Token: 0x04003966 RID: 14694
	private Color color_needleTrailBgOriginalColor;

	// Token: 0x04003967 RID: 14695
	private Color color_timerOutCircleBGOriginalColor;

	// Token: 0x04003968 RID: 14696
	private Color softRed;

	// Token: 0x04003969 RID: 14697
	private Coroutine animationCoroutine;

	// Token: 0x0400396A RID: 14698
	private bool hasSpawned;

	// Token: 0x0400396B RID: 14699
	private float entryAnimationDuration = 1f;

	// Token: 0x0400396C RID: 14700
	private float reminderAnimationDuration = 16f;

	// Token: 0x0400396D RID: 14701
	private bool hasPlayedEntryAnimation;

	// Token: 0x0400396E RID: 14702
	private float reminderAnimationTimer = -1f;

	// Token: 0x0400396F RID: 14703
	private float lastLargeImpactorTime = -1f;

	// Token: 0x04003970 RID: 14704
	private const int reminderSetting_BlinkTimes = 16;
}
