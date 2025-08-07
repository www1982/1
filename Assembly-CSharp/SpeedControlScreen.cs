using System;
using System.Collections;
using FMOD.Studio;
using STRINGS;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000E55 RID: 3669
public class SpeedControlScreen : KScreen
{
	// Token: 0x17000809 RID: 2057
	// (get) Token: 0x060074C5 RID: 29893 RVA: 0x002C9693 File Offset: 0x002C7893
	// (set) Token: 0x060074C6 RID: 29894 RVA: 0x002C969A File Offset: 0x002C789A
	public static SpeedControlScreen Instance { get; private set; }

	// Token: 0x060074C7 RID: 29895 RVA: 0x002C96A2 File Offset: 0x002C78A2
	public static void DestroyInstance()
	{
		SpeedControlScreen.Instance = null;
	}

	// Token: 0x1700080A RID: 2058
	// (get) Token: 0x060074C8 RID: 29896 RVA: 0x002C96AA File Offset: 0x002C78AA
	public bool IsPaused
	{
		get
		{
			return this.pauseCount > 0;
		}
	}

	// Token: 0x060074C9 RID: 29897 RVA: 0x002C96B8 File Offset: 0x002C78B8
	protected override void OnPrefabInit()
	{
		SpeedControlScreen.Instance = this;
		this.pauseButton = this.pauseButtonWidget.GetComponent<KToggle>();
		this.slowButton = this.speedButtonWidget_slow.GetComponent<KToggle>();
		this.mediumButton = this.speedButtonWidget_medium.GetComponent<KToggle>();
		this.fastButton = this.speedButtonWidget_fast.GetComponent<KToggle>();
		KToggle[] array = new KToggle[] { this.pauseButton, this.slowButton, this.mediumButton, this.fastButton };
		for (int i = 0; i < array.Length; i++)
		{
			array[i].soundPlayer.Enabled = false;
		}
		this.slowButton.onClick += delegate
		{
			this.PlaySpeedChangeSound(1f);
			this.SetSpeed(0);
		};
		this.mediumButton.onClick += delegate
		{
			this.PlaySpeedChangeSound(2f);
			this.SetSpeed(1);
		};
		this.fastButton.onClick += delegate
		{
			this.PlaySpeedChangeSound(3f);
			this.SetSpeed(2);
		};
		this.pauseButton.onClick += delegate
		{
			this.TogglePause(true);
		};
		this.speedButtonWidget_slow.GetComponent<ToolTip>().AddMultiStringTooltip(GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.SPEEDBUTTON_SLOW, global::Action.CycleSpeed), this.TooltipTextStyle);
		this.speedButtonWidget_medium.GetComponent<ToolTip>().AddMultiStringTooltip(GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.SPEEDBUTTON_MEDIUM, global::Action.CycleSpeed), this.TooltipTextStyle);
		this.speedButtonWidget_fast.GetComponent<ToolTip>().AddMultiStringTooltip(GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.SPEEDBUTTON_FAST, global::Action.CycleSpeed), this.TooltipTextStyle);
		this.playButtonWidget.GetComponent<KButton>().onClick += delegate
		{
			this.TogglePause(true);
		};
		KInputManager.InputChange.AddListener(new UnityAction(this.ResetToolTip));
	}

	// Token: 0x060074CA RID: 29898 RVA: 0x002C9859 File Offset: 0x002C7A59
	protected override void OnSpawn()
	{
		if (SaveGame.Instance != null)
		{
			this.speed = SaveGame.Instance.GetSpeed();
			this.SetSpeed(this.speed);
		}
		base.OnSpawn();
		this.OnChanged();
	}

	// Token: 0x060074CB RID: 29899 RVA: 0x002C9890 File Offset: 0x002C7A90
	protected override void OnForcedCleanUp()
	{
		KInputManager.InputChange.RemoveListener(new UnityAction(this.ResetToolTip));
		base.OnForcedCleanUp();
	}

	// Token: 0x060074CC RID: 29900 RVA: 0x002C98AE File Offset: 0x002C7AAE
	public int GetSpeed()
	{
		return this.speed;
	}

	// Token: 0x060074CD RID: 29901 RVA: 0x002C98B8 File Offset: 0x002C7AB8
	public void SetSpeed(int Speed)
	{
		this.speed = Speed % 3;
		switch (this.speed)
		{
		case 0:
			this.slowButton.Select();
			this.slowButton.isOn = true;
			this.mediumButton.isOn = false;
			this.fastButton.isOn = false;
			break;
		case 1:
			this.mediumButton.Select();
			this.slowButton.isOn = false;
			this.mediumButton.isOn = true;
			this.fastButton.isOn = false;
			break;
		case 2:
			this.fastButton.Select();
			this.slowButton.isOn = false;
			this.mediumButton.isOn = false;
			this.fastButton.isOn = true;
			break;
		}
		this.OnSpeedChange();
	}

	// Token: 0x060074CE RID: 29902 RVA: 0x002C9983 File Offset: 0x002C7B83
	public void ToggleRidiculousSpeed()
	{
		if (this.ultraSpeed == 3f)
		{
			this.ultraSpeed = 10f;
		}
		else
		{
			this.ultraSpeed = 3f;
		}
		this.speed = 2;
		this.OnChanged();
	}

	// Token: 0x060074CF RID: 29903 RVA: 0x002C99B7 File Offset: 0x002C7BB7
	public void TogglePause(bool playsound = true)
	{
		if (this.IsPaused)
		{
			this.Unpause(playsound);
			return;
		}
		this.Pause(playsound, false);
	}

	// Token: 0x060074D0 RID: 29904 RVA: 0x002C99D4 File Offset: 0x002C7BD4
	public void ResetToolTip()
	{
		this.speedButtonWidget_slow.GetComponent<ToolTip>().ClearMultiStringTooltip();
		this.speedButtonWidget_medium.GetComponent<ToolTip>().ClearMultiStringTooltip();
		this.speedButtonWidget_fast.GetComponent<ToolTip>().ClearMultiStringTooltip();
		this.speedButtonWidget_slow.GetComponent<ToolTip>().AddMultiStringTooltip(GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.SPEEDBUTTON_SLOW, global::Action.CycleSpeed), this.TooltipTextStyle);
		this.speedButtonWidget_medium.GetComponent<ToolTip>().AddMultiStringTooltip(GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.SPEEDBUTTON_MEDIUM, global::Action.CycleSpeed), this.TooltipTextStyle);
		this.speedButtonWidget_fast.GetComponent<ToolTip>().AddMultiStringTooltip(GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.SPEEDBUTTON_FAST, global::Action.CycleSpeed), this.TooltipTextStyle);
		if (this.pauseButton.isOn)
		{
			this.pauseButtonWidget.GetComponent<ToolTip>().ClearMultiStringTooltip();
			this.pauseButtonWidget.GetComponent<ToolTip>().AddMultiStringTooltip(GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.UNPAUSE, global::Action.TogglePause), this.TooltipTextStyle);
			return;
		}
		this.pauseButtonWidget.GetComponent<ToolTip>().ClearMultiStringTooltip();
		this.pauseButtonWidget.GetComponent<ToolTip>().AddMultiStringTooltip(GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.PAUSE, global::Action.TogglePause), this.TooltipTextStyle);
	}

	// Token: 0x060074D1 RID: 29905 RVA: 0x002C9B04 File Offset: 0x002C7D04
	public void Pause(bool playSound = true, bool isCrashed = false)
	{
		this.pauseCount++;
		if (this.pauseCount == 1)
		{
			if (playSound)
			{
				if (isCrashed)
				{
					KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Crash_Screen", false));
				}
				else
				{
					KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Speed_Pause", false));
				}
				if (SoundListenerController.Instance != null)
				{
					SoundListenerController.Instance.SetLoopingVolume(0f);
				}
			}
			AudioMixer.instance.Start(AudioMixerSnapshots.Get().SpeedPausedMigrated);
			MusicManager.instance.SetDynamicMusicPaused();
			this.pauseButtonWidget.GetComponent<ToolTip>().ClearMultiStringTooltip();
			this.pauseButtonWidget.GetComponent<ToolTip>().AddMultiStringTooltip(GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.UNPAUSE, global::Action.TogglePause), this.TooltipTextStyle);
			this.pauseButton.isOn = true;
			this.OnPause();
		}
	}

	// Token: 0x060074D2 RID: 29906 RVA: 0x002C9BD8 File Offset: 0x002C7DD8
	public void Unpause(bool playSound = true)
	{
		this.pauseCount = Mathf.Max(0, this.pauseCount - 1);
		if (this.pauseCount == 0)
		{
			if (playSound)
			{
				KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Speed_Unpause", false));
				if (SoundListenerController.Instance != null)
				{
					SoundListenerController.Instance.SetLoopingVolume(1f);
				}
			}
			AudioMixer.instance.Stop(AudioMixerSnapshots.Get().SpeedPausedMigrated, STOP_MODE.ALLOWFADEOUT);
			MusicManager.instance.SetDynamicMusicUnpaused();
			this.pauseButtonWidget.GetComponent<ToolTip>().ClearMultiStringTooltip();
			this.pauseButtonWidget.GetComponent<ToolTip>().AddMultiStringTooltip(GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.PAUSE, global::Action.TogglePause), this.TooltipTextStyle);
			this.pauseButton.isOn = false;
			this.SetSpeed(this.speed);
			this.OnPlay();
		}
	}

	// Token: 0x060074D3 RID: 29907 RVA: 0x002C9CA8 File Offset: 0x002C7EA8
	private void OnPause()
	{
		this.OnChanged();
	}

	// Token: 0x060074D4 RID: 29908 RVA: 0x002C9CB0 File Offset: 0x002C7EB0
	private void OnPlay()
	{
		this.OnChanged();
	}

	// Token: 0x060074D5 RID: 29909 RVA: 0x002C9CB8 File Offset: 0x002C7EB8
	public void OnSpeedChange()
	{
		if (Game.IsQuitting())
		{
			return;
		}
		this.OnChanged();
	}

	// Token: 0x060074D6 RID: 29910 RVA: 0x002C9CC8 File Offset: 0x002C7EC8
	private void OnChanged()
	{
		if (this.IsPaused)
		{
			Time.timeScale = 0f;
			return;
		}
		if (this.speed == 0)
		{
			Time.timeScale = this.normalSpeed;
			return;
		}
		if (this.speed == 1)
		{
			Time.timeScale = this.fastSpeed;
			return;
		}
		if (this.speed == 2)
		{
			Time.timeScale = this.ultraSpeed;
		}
	}

	// Token: 0x060074D7 RID: 29911 RVA: 0x002C9D28 File Offset: 0x002C7F28
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.TogglePause))
		{
			this.TogglePause(true);
			return;
		}
		if (e.TryConsume(global::Action.CycleSpeed))
		{
			this.PlaySpeedChangeSound((float)((this.speed + 1) % 3 + 1));
			this.SetSpeed(this.speed + 1);
			this.OnSpeedChange();
			return;
		}
		if (e.TryConsume(global::Action.SpeedUp))
		{
			this.speed++;
			this.speed = Math.Min(this.speed, 2);
			this.SetSpeed(this.speed);
			return;
		}
		if (e.TryConsume(global::Action.SlowDown))
		{
			this.speed--;
			this.speed = Math.Max(this.speed, 0);
			this.SetSpeed(this.speed);
		}
	}

	// Token: 0x060074D8 RID: 29912 RVA: 0x002C9DE8 File Offset: 0x002C7FE8
	private void PlaySpeedChangeSound(float speed)
	{
		string sound = GlobalAssets.GetSound("Speed_Change", false);
		if (sound != null)
		{
			EventInstance eventInstance = SoundEvent.BeginOneShot(sound, Vector3.zero, 1f, false);
			eventInstance.setParameterByName("Speed", speed, false);
			SoundEvent.EndOneShot(eventInstance);
		}
	}

	// Token: 0x060074D9 RID: 29913 RVA: 0x002C9E30 File Offset: 0x002C8030
	public void DebugStepFrame()
	{
		DebugUtil.LogArgs(new object[] { string.Format("Stepping one frame {0} ({1})", GameClock.Instance.GetTime(), GameClock.Instance.GetTime() / 600f) });
		this.stepTime = Time.time;
		this.Unpause(false);
		base.StartCoroutine(this.DebugStepFrameDelay());
	}

	// Token: 0x060074DA RID: 29914 RVA: 0x002C9E98 File Offset: 0x002C8098
	private IEnumerator DebugStepFrameDelay()
	{
		yield return null;
		DebugUtil.LogArgs(new object[]
		{
			"Stepped one frame",
			Time.time - this.stepTime,
			"seconds"
		});
		this.Pause(false, false);
		yield break;
	}

	// Token: 0x040050C4 RID: 20676
	public GameObject playButtonWidget;

	// Token: 0x040050C5 RID: 20677
	public GameObject pauseButtonWidget;

	// Token: 0x040050C6 RID: 20678
	public Image playIcon;

	// Token: 0x040050C7 RID: 20679
	public Image pauseIcon;

	// Token: 0x040050C8 RID: 20680
	[SerializeField]
	private TextStyleSetting TooltipTextStyle;

	// Token: 0x040050C9 RID: 20681
	public GameObject speedButtonWidget_slow;

	// Token: 0x040050CA RID: 20682
	public GameObject speedButtonWidget_medium;

	// Token: 0x040050CB RID: 20683
	public GameObject speedButtonWidget_fast;

	// Token: 0x040050CC RID: 20684
	public GameObject mainMenuWidget;

	// Token: 0x040050CD RID: 20685
	public float normalSpeed;

	// Token: 0x040050CE RID: 20686
	public float fastSpeed;

	// Token: 0x040050CF RID: 20687
	public float ultraSpeed;

	// Token: 0x040050D0 RID: 20688
	private KToggle pauseButton;

	// Token: 0x040050D1 RID: 20689
	private KToggle slowButton;

	// Token: 0x040050D2 RID: 20690
	private KToggle mediumButton;

	// Token: 0x040050D3 RID: 20691
	private KToggle fastButton;

	// Token: 0x040050D4 RID: 20692
	private int speed;

	// Token: 0x040050D5 RID: 20693
	private int pauseCount;

	// Token: 0x040050D7 RID: 20695
	private float stepTime;
}
