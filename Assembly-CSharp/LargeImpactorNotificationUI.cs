using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000B48 RID: 2888
public class LargeImpactorNotificationUI : KMonoBehaviour, ISim200ms
{
	// Token: 0x060055ED RID: 21997 RVA: 0x001F3198 File Offset: 0x001F1398
	protected override void OnSpawn()
	{
		GameplayEventInstance gameplayEventInstance = GameplayEventManager.Instance.GetGameplayEventInstance(Db.Get().GameplayEvents.LargeImpactor.Id, -1);
		LargeImpactorEvent.StatesInstance statesInstance = (LargeImpactorEvent.StatesInstance)gameplayEventInstance.smi;
		this.rangeVisualizer = statesInstance.impactorInstance.GetComponent<LargeImpactorVisualizer>();
		this.asteroidBackground = statesInstance.impactorInstance.GetComponent<ParallaxBackgroundObject>();
		this.statusMonitor = statesInstance.impactorInstance.GetSMI<LargeImpactorStatus.Instance>();
		LargeImpactorStatus.Instance instance = this.statusMonitor;
		instance.OnDamaged = (Action<int>)Delegate.Combine(instance.OnDamaged, new Action<int>(this.OnAsteroidDamaged));
		Game.Instance.Subscribe(445618876, new Action<object>(this.OnScreenResolutionChanged));
		Game.Instance.Subscribe(-810220474, new Action<object>(this.OnScreenResolutionChanged));
		this.cyclesLabelEffects.InitializeCycleLabelFocusMonitor();
		this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.ToggleVisibility));
		this.toggle.SetIsOnWithoutNotify(this.rangeVisualizer != null && this.rangeVisualizer.Visible);
		this.toggle.offEffectDuration = this.rangeVisualizer.FoldEffectDuration;
		LargeImpactorCrashStamp component = statesInstance.impactorInstance.GetComponent<LargeImpactorCrashStamp>();
		this.midSkyCell = Grid.FindMidSkyCellAlignedWithCellInWorld(Grid.XYToCell(component.stampLocation.x, component.stampLocation.y), gameplayEventInstance.worldId);
		this.RefreshTogglePositionInRangeVisualizer();
		this.RefreshValues();
	}

	// Token: 0x060055EE RID: 21998 RVA: 0x001F3310 File Offset: 0x001F1510
	private void OnScreenResolutionChanged(object data)
	{
		this.RefreshTogglePositionInRangeVisualizer();
	}

	// Token: 0x060055EF RID: 21999 RVA: 0x001F3318 File Offset: 0x001F1518
	private void RefreshTogglePositionInRangeVisualizer()
	{
		if (this.rangeVisualizer != null)
		{
			RectTransform rectTransform = this.toggle.rectTransform();
			Vector3 vector = rectTransform.TransformPoint(rectTransform.rect.center);
			Vector2 vector2 = RectTransformUtility.WorldToScreenPoint(null, vector);
			Vector2 vector3 = new Vector2(vector2.x / (float)Screen.width, vector2.y / (float)Screen.height);
			this.rangeVisualizer.ScreenSpaceNotificationTogglePosition = vector3;
		}
	}

	// Token: 0x060055F0 RID: 22000 RVA: 0x001F338C File Offset: 0x001F158C
	public void Sim200ms(float dt)
	{
		this.RefreshValues();
	}

	// Token: 0x060055F1 RID: 22001 RVA: 0x001F3394 File Offset: 0x001F1594
	public void RefreshValues()
	{
		float num = (float)this.statusMonitor.Health / (float)this.statusMonitor.def.MAX_HEALTH;
		float num2 = this.statusMonitor.TimeRemainingBeforeCollision / LargeImpactorEvent.GetImpactTime();
		this.healthbar.fillAmount = num;
		this.clock.SetLargeImpactorTime(num2);
		string[] array = GameUtil.GetFormattedCycles(this.statusMonitor.TimeRemainingBeforeCollision, "F1", false).Split(' ', StringSplitOptions.None);
		this.numberOfCyclesLabel.SetText(array[0]);
		if (this.rangeVisualizer != null && this.toggle.isOn != this.rangeVisualizer.Visible)
		{
			this.toggle.isOn = this.rangeVisualizer.Visible;
		}
	}

	// Token: 0x060055F2 RID: 22002 RVA: 0x001F3453 File Offset: 0x001F1653
	private void OnAsteroidDamaged(int newHealth)
	{
		this.hitEffects.PlayHitEffect();
		KFMOD.PlayUISound(GlobalAssets.GetSound("Notification_Imperative_hit", false));
		this.RefreshValues();
	}

	// Token: 0x060055F3 RID: 22003 RVA: 0x001F3476 File Offset: 0x001F1676
	public void ToggleVisibility(bool shouldBeVisible)
	{
		if (this.rangeVisualizer != null)
		{
			KFMOD.PlayUISound(GlobalAssets.GetSound(shouldBeVisible ? "HUD_Demolior_LandingZone_toggle_on" : "HUD_Demolior_LandingZone_toggle_off", false));
			this.RefreshTogglePositionInRangeVisualizer();
			this.rangeVisualizer.SetFoldedState(!shouldBeVisible);
		}
	}

	// Token: 0x060055F4 RID: 22004 RVA: 0x001F34B8 File Offset: 0x001F16B8
	public void OnPlayerClickedNotification()
	{
		GameUtil.FocusCamera(this.midSkyCell, true);
		KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Click_Open ", false));
		KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Demolior_Click_focus", false));
		if (this.asteroidBackground != null)
		{
			this.asteroidBackground.PlayPlayerClickFeedback();
		}
	}

	// Token: 0x060055F5 RID: 22005 RVA: 0x001F350C File Offset: 0x001F170C
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		this.cyclesLabelEffects.AbortCycleLabelFocusMonitor();
		if (this.statusMonitor != null)
		{
			LargeImpactorStatus.Instance instance = this.statusMonitor;
			instance.OnDamaged = (Action<int>)Delegate.Remove(instance.OnDamaged, new Action<int>(this.OnAsteroidDamaged));
		}
		Game.Instance.Unsubscribe(445618876, new Action<object>(this.OnScreenResolutionChanged));
		Game.Instance.Unsubscribe(-810220474, new Action<object>(this.OnScreenResolutionChanged));
	}

	// Token: 0x060055F6 RID: 22006 RVA: 0x001F358F File Offset: 0x001F178F
	protected override void OnCmpEnable()
	{
		if (base.isSpawned)
		{
			this.cyclesLabelEffects.InitializeCycleLabelFocusMonitor();
		}
	}

	// Token: 0x060055F7 RID: 22007 RVA: 0x001F35A4 File Offset: 0x001F17A4
	protected override void OnCmpDisable()
	{
		this.cyclesLabelEffects.AbortCycleLabelFocusMonitor();
	}

	// Token: 0x04003951 RID: 14673
	public Image healthbar;

	// Token: 0x04003952 RID: 14674
	public LargeImpactorNotificationUI_Clock clock;

	// Token: 0x04003953 RID: 14675
	public KToggleSlider toggle;

	// Token: 0x04003954 RID: 14676
	public LargeImpactorUINotificationHitEffects hitEffects;

	// Token: 0x04003955 RID: 14677
	public LargeImpactorNotificationUI_CycleLabelEffects cyclesLabelEffects;

	// Token: 0x04003956 RID: 14678
	public LocText numberOfCyclesLabel;

	// Token: 0x04003957 RID: 14679
	private LargeImpactorStatus.Instance statusMonitor;

	// Token: 0x04003958 RID: 14680
	private LargeImpactorVisualizer rangeVisualizer;

	// Token: 0x04003959 RID: 14681
	private ParallaxBackgroundObject asteroidBackground;

	// Token: 0x0400395A RID: 14682
	private int midSkyCell = Grid.InvalidCell;

	// Token: 0x0400395B RID: 14683
	private const string Hit_SFX = "Notification_Imperative_hit";

	// Token: 0x0400395C RID: 14684
	private const string Click_SFX = "HUD_Click_Open ";

	// Token: 0x0400395D RID: 14685
	private const string Focus_SFX = "HUD_Demolior_Click_focus";

	// Token: 0x0400395E RID: 14686
	private const string ToggleOff_SFX = "HUD_Demolior_LandingZone_toggle_off";

	// Token: 0x0400395F RID: 14687
	private const string ToggleOn_SFX = "HUD_Demolior_LandingZone_toggle_on";
}
