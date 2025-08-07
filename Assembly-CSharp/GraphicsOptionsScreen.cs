using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using STRINGS;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000CD5 RID: 3285
internal class GraphicsOptionsScreen : KModalScreen
{
	// Token: 0x0600653C RID: 25916 RVA: 0x00260EE8 File Offset: 0x0025F0E8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.title.SetText(UI.FRONTEND.GRAPHICS_OPTIONS_SCREEN.TITLE);
		this.originalSettings = this.CaptureSettings();
		this.applyButton.isInteractable = false;
		this.applyButton.onClick += this.OnApply;
		this.applyButton.GetComponentInChildren<LocText>().SetText(UI.FRONTEND.GRAPHICS_OPTIONS_SCREEN.APPLYBUTTON);
		this.doneButton.onClick += this.OnDone;
		this.closeButton.onClick += this.OnDone;
		this.doneButton.GetComponentInChildren<LocText>().SetText(UI.FRONTEND.GRAPHICS_OPTIONS_SCREEN.DONE_BUTTON);
		bool flag = QualitySettings.GetQualityLevel() == 1;
		this.lowResToggle.ChangeState(flag ? 1 : 0);
		MultiToggle multiToggle = this.lowResToggle;
		multiToggle.onClick = (global::System.Action)Delegate.Combine(multiToggle.onClick, new global::System.Action(this.OnLowResToggle));
		this.lowResToggle.GetComponentInChildren<LocText>().SetText(UI.FRONTEND.GRAPHICS_OPTIONS_SCREEN.LOWRES);
		this.resolutionDropdown.ClearOptions();
		this.BuildOptions();
		ScreenResize instance = ScreenResize.Instance;
		instance.OnResize = (global::System.Action)Delegate.Combine(instance.OnResize, new global::System.Action(delegate
		{
			this.BuildOptions();
			this.resolutionDropdown.options = this.options;
		}));
		this.resolutionDropdown.options = this.options;
		this.resolutionDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnResolutionChanged));
		this.fullscreenToggle.ChangeState(Screen.fullScreen ? 1 : 0);
		MultiToggle multiToggle2 = this.fullscreenToggle;
		multiToggle2.onClick = (global::System.Action)Delegate.Combine(multiToggle2.onClick, new global::System.Action(this.OnFullscreenToggle));
		this.fullscreenToggle.GetComponentInChildren<LocText>().SetText(UI.FRONTEND.GRAPHICS_OPTIONS_SCREEN.FULLSCREEN);
		this.resolutionDropdown.transform.parent.GetComponentInChildren<LocText>().SetText(UI.FRONTEND.GRAPHICS_OPTIONS_SCREEN.RESOLUTION);
		if (this.fullscreenToggle.CurrentState == 1)
		{
			int resolutionIndex = this.GetResolutionIndex(this.originalSettings.resolution);
			if (resolutionIndex != -1)
			{
				this.resolutionDropdown.value = resolutionIndex;
			}
		}
		this.CanvasScalers = global::UnityEngine.Object.FindObjectsOfType<KCanvasScaler>(true);
		this.UpdateSliderLabel();
		this.uiScaleSlider.onValueChanged.AddListener(delegate(float data)
		{
			this.sliderLabel.text = this.uiScaleSlider.value.ToString() + "%";
		});
		this.uiScaleSlider.onReleaseHandle += delegate
		{
			this.UpdateUIScale(this.uiScaleSlider.value);
		};
		this.BuildColorModeOptions();
		this.colorModeDropdown.options = this.colorModeOptions;
		this.colorModeDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnColorModeChanged));
		int num = 0;
		if (KPlayerPrefs.HasKey(GraphicsOptionsScreen.ColorModeKey))
		{
			num = KPlayerPrefs.GetInt(GraphicsOptionsScreen.ColorModeKey);
		}
		this.colorModeDropdown.value = num;
		this.RefreshColorExamples(this.originalSettings.colorSetId);
	}

	// Token: 0x0600653D RID: 25917 RVA: 0x002611BA File Offset: 0x0025F3BA
	public static void SetSettingsFromPrefs()
	{
		GraphicsOptionsScreen.SetResolutionFromPrefs();
		GraphicsOptionsScreen.SetLowResFromPrefs();
	}

	// Token: 0x0600653E RID: 25918 RVA: 0x002611C8 File Offset: 0x0025F3C8
	public static void SetLowResFromPrefs()
	{
		int num = 0;
		if (KPlayerPrefs.HasKey(GraphicsOptionsScreen.LowResKey))
		{
			num = KPlayerPrefs.GetInt(GraphicsOptionsScreen.LowResKey);
			QualitySettings.SetQualityLevel(num, true);
		}
		else
		{
			QualitySettings.SetQualityLevel(num, true);
		}
		DebugUtil.LogArgs(new object[] { string.Format("Low Res Textures? {0}", (num == 1) ? "Yes" : "No") });
	}

	// Token: 0x0600653F RID: 25919 RVA: 0x00261228 File Offset: 0x0025F428
	public static void SetResolutionFromPrefs()
	{
		int num = Screen.currentResolution.width;
		int num2 = Screen.currentResolution.height;
		RefreshRate refreshRate = Screen.currentResolution.refreshRateRatio;
		FullScreenMode fullScreenMode = Screen.fullScreenMode;
		if (KPlayerPrefs.HasKey(GraphicsOptionsScreen.ResolutionWidthKey) && KPlayerPrefs.HasKey(GraphicsOptionsScreen.ResolutionHeightKey))
		{
			int @int = KPlayerPrefs.GetInt(GraphicsOptionsScreen.ResolutionWidthKey);
			int int2 = KPlayerPrefs.GetInt(GraphicsOptionsScreen.ResolutionHeightKey);
			uint int3 = (uint)KPlayerPrefs.GetInt(GraphicsOptionsScreen.RefreshRateKeyNumerator, (int)Screen.currentResolution.refreshRateRatio.numerator);
			uint int4 = (uint)KPlayerPrefs.GetInt(GraphicsOptionsScreen.RefreshRateKeyDenominator, (int)Screen.currentResolution.refreshRateRatio.denominator);
			FullScreenMode fullScreenMode2 = ((KPlayerPrefs.GetInt(GraphicsOptionsScreen.FullScreenKey, Screen.fullScreen ? 1 : 0) == 1) ? FullScreenMode.MaximizedWindow : FullScreenMode.Windowed);
			if (int2 <= 1 || @int <= 1)
			{
				DebugUtil.LogArgs(new object[] { "Saved resolution was invalid, ignoring..." });
			}
			else
			{
				num = @int;
				num2 = int2;
				refreshRate.numerator = int3;
				refreshRate.denominator = int4;
				fullScreenMode = fullScreenMode2;
			}
		}
		if (num <= 1 || num2 <= 1)
		{
			DebugUtil.LogWarningArgs(new object[] { "Detected a degenerate resolution, attempting to fix..." });
			foreach (Resolution resolution in Screen.resolutions)
			{
				if (resolution.width == 1920)
				{
					num = resolution.width;
					num2 = resolution.height;
					refreshRate = default(RefreshRate);
				}
			}
			if (num <= 1 || num2 <= 1)
			{
				foreach (Resolution resolution2 in Screen.resolutions)
				{
					if (resolution2.width == 1280)
					{
						num = resolution2.width;
						num2 = resolution2.height;
						refreshRate = default(RefreshRate);
					}
				}
			}
			if (num <= 1 || num2 <= 1)
			{
				foreach (Resolution resolution3 in Screen.resolutions)
				{
					if (resolution3.width > 1 && resolution3.height > 1 && resolution3.refreshRateRatio.value > 0.0)
					{
						num = resolution3.width;
						num2 = resolution3.height;
						refreshRate = default(RefreshRate);
					}
				}
			}
			if (num <= 1 || num2 <= 1)
			{
				string text = "Could not find a suitable resolution for this screen! Reported available resolutions are:";
				foreach (Resolution resolution4 in Screen.resolutions)
				{
					text += string.Format("\n{0}x{1} @ {2}hz", resolution4.width, resolution4.height, resolution4.refreshRateRatio.value);
				}
				global::Debug.LogError(text);
				num = 1280;
				num2 = 720;
				fullScreenMode = FullScreenMode.Windowed;
				refreshRate = default(RefreshRate);
			}
		}
		DebugUtil.LogArgs(new object[] { string.Format("Applying resolution {0}x{1} @{2}hz (fullscreen: {3})", new object[] { num, num2, refreshRate, fullScreenMode }) });
		Screen.SetResolution(num, num2, fullScreenMode, refreshRate);
	}

	// Token: 0x06006540 RID: 25920 RVA: 0x00261544 File Offset: 0x0025F744
	public static void SetColorModeFromPrefs()
	{
		int num = 0;
		if (KPlayerPrefs.HasKey(GraphicsOptionsScreen.ColorModeKey))
		{
			num = KPlayerPrefs.GetInt(GraphicsOptionsScreen.ColorModeKey);
		}
		GlobalAssets.Instance.colorSet = GlobalAssets.Instance.colorSetOptions[num];
	}

	// Token: 0x06006541 RID: 25921 RVA: 0x00261580 File Offset: 0x0025F780
	public static void OnResize()
	{
		GraphicsOptionsScreen.Settings settings = default(GraphicsOptionsScreen.Settings);
		settings.resolution = Screen.currentResolution;
		settings.resolution.width = Screen.width;
		settings.resolution.height = Screen.height;
		settings.fullscreen = Screen.fullScreenMode;
		settings.lowRes = QualitySettings.GetQualityLevel();
		settings.colorSetId = Array.IndexOf<ColorSet>(GlobalAssets.Instance.colorSetOptions, GlobalAssets.Instance.colorSet);
		GraphicsOptionsScreen.SaveSettingsToPrefs(settings);
	}

	// Token: 0x06006542 RID: 25922 RVA: 0x00261604 File Offset: 0x0025F804
	private static void SaveSettingsToPrefs(GraphicsOptionsScreen.Settings settings)
	{
		KPlayerPrefs.SetInt(GraphicsOptionsScreen.LowResKey, settings.lowRes);
		global::Debug.LogFormat("Screen resolution updated, saving values to prefs: {0}x{1} @ {2}, fullscreen: {3}", new object[]
		{
			settings.resolution.width,
			settings.resolution.height,
			settings.resolution.refreshRateRatio,
			settings.fullscreen
		});
		KPlayerPrefs.SetInt(GraphicsOptionsScreen.ResolutionWidthKey, settings.resolution.width);
		KPlayerPrefs.SetInt(GraphicsOptionsScreen.ResolutionHeightKey, settings.resolution.height);
		KPlayerPrefs.SetInt(GraphicsOptionsScreen.RefreshRateKeyNumerator, (int)settings.resolution.refreshRateRatio.numerator);
		KPlayerPrefs.SetInt(GraphicsOptionsScreen.RefreshRateKeyDenominator, (int)settings.resolution.refreshRateRatio.denominator);
		KPlayerPrefs.SetInt(GraphicsOptionsScreen.FullScreenKey, (settings.fullscreen == FullScreenMode.Windowed) ? 0 : 1);
		KPlayerPrefs.SetInt(GraphicsOptionsScreen.ColorModeKey, settings.colorSetId);
	}

	// Token: 0x06006543 RID: 25923 RVA: 0x00261704 File Offset: 0x0025F904
	private void UpdateUIScale(float value)
	{
		this.CanvasScalers = global::UnityEngine.Object.FindObjectsOfType<KCanvasScaler>(true);
		foreach (KCanvasScaler kcanvasScaler in this.CanvasScalers)
		{
			float num = value / 100f;
			kcanvasScaler.SetUserScale(num);
			KPlayerPrefs.SetFloat(KCanvasScaler.UIScalePrefKey, value);
		}
		ScreenResize.Instance.TriggerResize();
		this.UpdateSliderLabel();
	}

	// Token: 0x06006544 RID: 25924 RVA: 0x00261760 File Offset: 0x0025F960
	private void UpdateSliderLabel()
	{
		if (this.CanvasScalers != null && this.CanvasScalers.Length != 0 && this.CanvasScalers[0] != null)
		{
			this.uiScaleSlider.value = this.CanvasScalers[0].GetUserScale() * 100f;
			this.sliderLabel.text = this.uiScaleSlider.value.ToString() + "%";
		}
	}

	// Token: 0x06006545 RID: 25925 RVA: 0x002617D4 File Offset: 0x0025F9D4
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape) || e.TryConsume(global::Action.MouseRight))
		{
			this.resolutionDropdown.Hide();
			this.Deactivate();
			return;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x06006546 RID: 25926 RVA: 0x00261804 File Offset: 0x0025FA04
	private void BuildOptions()
	{
		this.options.Clear();
		this.resolutions.Clear();
		Resolution resolution = default(Resolution);
		resolution.width = Screen.width;
		resolution.height = Screen.height;
		resolution.refreshRateRatio = Screen.currentResolution.refreshRateRatio;
		this.options.Add(new Dropdown.OptionData(this.ResolutionDisplayString(resolution)));
		this.resolutions.Add(resolution);
		foreach (Resolution resolution2 in Screen.resolutions)
		{
			if (resolution2.height >= 720)
			{
				this.options.Add(new Dropdown.OptionData(this.ResolutionDisplayString(resolution2)));
				this.resolutions.Add(resolution2);
			}
		}
	}

	// Token: 0x06006547 RID: 25927 RVA: 0x002618D0 File Offset: 0x0025FAD0
	private string ResolutionDisplayString(Resolution resolution)
	{
		return string.Format("{0} x {1} @ {2}Hz", resolution.width, resolution.height, Mathf.Floor((float)resolution.refreshRateRatio.value));
	}

	// Token: 0x06006548 RID: 25928 RVA: 0x0026191C File Offset: 0x0025FB1C
	private void BuildColorModeOptions()
	{
		this.colorModeOptions.Clear();
		for (int i = 0; i < GlobalAssets.Instance.colorSetOptions.Length; i++)
		{
			this.colorModeOptions.Add(new Dropdown.OptionData(Strings.Get(GlobalAssets.Instance.colorSetOptions[i].settingName)));
		}
	}

	// Token: 0x06006549 RID: 25929 RVA: 0x00261978 File Offset: 0x0025FB78
	private void RefreshColorExamples(int idx)
	{
		Color32 logicOn = GlobalAssets.Instance.colorSetOptions[idx].logicOn;
		Color32 logicOff = GlobalAssets.Instance.colorSetOptions[idx].logicOff;
		Color32 cropHalted = GlobalAssets.Instance.colorSetOptions[idx].cropHalted;
		Color32 cropGrowing = GlobalAssets.Instance.colorSetOptions[idx].cropGrowing;
		Color32 cropGrown = GlobalAssets.Instance.colorSetOptions[idx].cropGrown;
		logicOn.a = byte.MaxValue;
		logicOff.a = byte.MaxValue;
		cropHalted.a = byte.MaxValue;
		cropGrowing.a = byte.MaxValue;
		cropGrown.a = byte.MaxValue;
		this.colorExampleLogicOn.color = logicOn;
		this.colorExampleLogicOff.color = logicOff;
		this.colorExampleCropHalted.color = cropHalted;
		this.colorExampleCropGrowing.color = cropGrowing;
		this.colorExampleCropGrown.color = cropGrown;
	}

	// Token: 0x0600654A RID: 25930 RVA: 0x00261A74 File Offset: 0x0025FC74
	private int GetResolutionIndex(Resolution resolution)
	{
		int num = -1;
		int num2 = -1;
		for (int i = 0; i < this.resolutions.Count; i++)
		{
			Resolution resolution2 = this.resolutions[i];
			if (resolution2.width == resolution.width && resolution2.height == resolution.height && resolution2.refreshRateRatio.value == 0.0)
			{
				num2 = i;
			}
			if (resolution2.width == resolution.width && resolution2.height == resolution.height && Math.Abs(resolution2.refreshRateRatio.value - resolution.refreshRateRatio.value) <= 1.0)
			{
				num = i;
				break;
			}
		}
		if (num != -1)
		{
			return num;
		}
		return num2;
	}

	// Token: 0x0600654B RID: 25931 RVA: 0x00261B48 File Offset: 0x0025FD48
	private GraphicsOptionsScreen.Settings CaptureSettings()
	{
		return new GraphicsOptionsScreen.Settings
		{
			fullscreen = Screen.fullScreenMode,
			resolution = new Resolution
			{
				width = Screen.width,
				height = Screen.height,
				refreshRateRatio = Screen.currentResolution.refreshRateRatio
			},
			lowRes = QualitySettings.GetQualityLevel(),
			colorSetId = Array.IndexOf<ColorSet>(GlobalAssets.Instance.colorSetOptions, GlobalAssets.Instance.colorSet)
		};
	}

	// Token: 0x0600654C RID: 25932 RVA: 0x00261BD4 File Offset: 0x0025FDD4
	private void OnApply()
	{
		try
		{
			GraphicsOptionsScreen.Settings new_settings = default(GraphicsOptionsScreen.Settings);
			new_settings.resolution = this.resolutions[this.resolutionDropdown.value];
			new_settings.fullscreen = ((this.fullscreenToggle.CurrentState == 0) ? FullScreenMode.Windowed : FullScreenMode.MaximizedWindow);
			new_settings.lowRes = this.lowResToggle.CurrentState;
			new_settings.colorSetId = this.colorModeId;
			if (GlobalAssets.Instance.colorSetOptions[this.colorModeId] != GlobalAssets.Instance.colorSet)
			{
				this.colorModeChanged = true;
			}
			this.ApplyConfirmSettings(new_settings, delegate
			{
				this.applyButton.isInteractable = false;
				if (this.colorModeChanged)
				{
					this.feedbackDialog = Util.KInstantiateUI(this.confirmPrefab.gameObject, this.transform.parent.gameObject, false).GetComponent<ConfirmDialogScreen>();
					this.feedbackDialog.PopupConfirmDialog(UI.FRONTEND.GRAPHICS_OPTIONS_SCREEN.COLORBLIND_FEEDBACK.text, null, null, UI.FRONTEND.GRAPHICS_OPTIONS_SCREEN.COLORBLIND_FEEDBACK_BUTTON.text, delegate
					{
						App.OpenWebURL("https://forums.kleientertainment.com/forums/topic/117325-color-blindness-feedback/");
					}, null, null, null, null);
					this.feedbackDialog.gameObject.SetActive(true);
				}
				this.colorModeChanged = false;
				GraphicsOptionsScreen.SaveSettingsToPrefs(new_settings);
			});
		}
		catch (Exception ex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Failed to apply graphics options!\nResolutions:");
			foreach (Resolution resolution in this.resolutions)
			{
				stringBuilder.Append("\t" + resolution.ToString() + "\n");
			}
			stringBuilder.Append("Selected Resolution Idx: " + this.resolutionDropdown.value.ToString());
			stringBuilder.Append("FullScreen: " + this.fullscreenToggle.CurrentState.ToString());
			global::Debug.LogError(stringBuilder.ToString());
			throw ex;
		}
	}

	// Token: 0x0600654D RID: 25933 RVA: 0x00261D80 File Offset: 0x0025FF80
	public void OnDone()
	{
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600654E RID: 25934 RVA: 0x00261D90 File Offset: 0x0025FF90
	private void RefreshApplyButton()
	{
		GraphicsOptionsScreen.Settings settings = this.CaptureSettings();
		if (settings.fullscreen != FullScreenMode.Windowed && this.fullscreenToggle.CurrentState == 0)
		{
			this.applyButton.isInteractable = true;
			return;
		}
		if (settings.fullscreen == FullScreenMode.Windowed && this.fullscreenToggle.CurrentState == 1)
		{
			this.applyButton.isInteractable = true;
			return;
		}
		if (settings.lowRes != this.lowResToggle.CurrentState)
		{
			this.applyButton.isInteractable = true;
			return;
		}
		if (settings.colorSetId != this.colorModeId)
		{
			this.applyButton.isInteractable = true;
			return;
		}
		int resolutionIndex = this.GetResolutionIndex(settings.resolution);
		this.applyButton.isInteractable = this.resolutionDropdown.value != resolutionIndex;
	}

	// Token: 0x0600654F RID: 25935 RVA: 0x00261E4F File Offset: 0x0026004F
	private void OnFullscreenToggle()
	{
		this.fullscreenToggle.ChangeState((this.fullscreenToggle.CurrentState == 0) ? 1 : 0);
		this.RefreshApplyButton();
	}

	// Token: 0x06006550 RID: 25936 RVA: 0x00261E73 File Offset: 0x00260073
	private void OnResolutionChanged(int idx)
	{
		this.RefreshApplyButton();
	}

	// Token: 0x06006551 RID: 25937 RVA: 0x00261E7B File Offset: 0x0026007B
	private void OnColorModeChanged(int idx)
	{
		this.colorModeId = idx;
		this.RefreshApplyButton();
		this.RefreshColorExamples(this.colorModeId);
	}

	// Token: 0x06006552 RID: 25938 RVA: 0x00261E96 File Offset: 0x00260096
	private void OnLowResToggle()
	{
		this.lowResToggle.ChangeState((this.lowResToggle.CurrentState == 0) ? 1 : 0);
		this.RefreshApplyButton();
	}

	// Token: 0x06006553 RID: 25939 RVA: 0x00261EBC File Offset: 0x002600BC
	private void ApplyConfirmSettings(GraphicsOptionsScreen.Settings new_settings, global::System.Action on_confirm)
	{
		GraphicsOptionsScreen.Settings current_settings = this.CaptureSettings();
		this.ApplySettings(new_settings);
		this.confirmDialog = Util.KInstantiateUI(this.confirmPrefab.gameObject, base.transform.parent.gameObject, false).GetComponent<ConfirmDialogScreen>();
		global::System.Action action = delegate
		{
			this.ApplySettings(current_settings);
		};
		Coroutine timer = base.StartCoroutine(this.Timer(15f, action));
		this.confirmDialog.onDeactivateCB = delegate
		{
			if (timer != null)
			{
				this.StopCoroutine(timer);
			}
		};
		this.confirmDialog.PopupConfirmDialog(this.colorModeChanged ? UI.FRONTEND.GRAPHICS_OPTIONS_SCREEN.ACCEPT_CHANGES_STRING_COLOR.text : UI.FRONTEND.GRAPHICS_OPTIONS_SCREEN.ACCEPT_CHANGES.text, on_confirm, action, null, null, null, null, null, null);
		this.confirmDialog.gameObject.SetActive(true);
	}

	// Token: 0x06006554 RID: 25940 RVA: 0x00261F94 File Offset: 0x00260194
	private void ApplySettings(GraphicsOptionsScreen.Settings new_settings)
	{
		Resolution resolution = new_settings.resolution;
		Screen.SetResolution(resolution.width, resolution.height, new_settings.fullscreen, resolution.refreshRateRatio);
		Screen.fullScreenMode = new_settings.fullscreen;
		int resolutionIndex = this.GetResolutionIndex(new_settings.resolution);
		if (resolutionIndex != -1)
		{
			this.resolutionDropdown.value = resolutionIndex;
		}
		GlobalAssets.Instance.colorSet = GlobalAssets.Instance.colorSetOptions[new_settings.colorSetId];
		global::Debug.Log("Applying low res settings " + new_settings.lowRes.ToString() + " / existing is " + QualitySettings.GetQualityLevel().ToString());
		if (QualitySettings.GetQualityLevel() != new_settings.lowRes)
		{
			QualitySettings.SetQualityLevel(new_settings.lowRes, true);
		}
	}

	// Token: 0x06006555 RID: 25941 RVA: 0x00262051 File Offset: 0x00260251
	private IEnumerator Timer(float time, global::System.Action revert)
	{
		yield return new WaitForSecondsRealtime(time);
		if (this.confirmDialog != null)
		{
			this.confirmDialog.Deactivate();
			revert();
		}
		yield break;
	}

	// Token: 0x06006556 RID: 25942 RVA: 0x0026206E File Offset: 0x0026026E
	private void Update()
	{
		global::Debug.developerConsoleVisible = false;
	}

	// Token: 0x0400452C RID: 17708
	[SerializeField]
	private Dropdown resolutionDropdown;

	// Token: 0x0400452D RID: 17709
	[SerializeField]
	private MultiToggle lowResToggle;

	// Token: 0x0400452E RID: 17710
	[SerializeField]
	private MultiToggle fullscreenToggle;

	// Token: 0x0400452F RID: 17711
	[SerializeField]
	private KButton applyButton;

	// Token: 0x04004530 RID: 17712
	[SerializeField]
	private KButton doneButton;

	// Token: 0x04004531 RID: 17713
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04004532 RID: 17714
	[SerializeField]
	private ConfirmDialogScreen confirmPrefab;

	// Token: 0x04004533 RID: 17715
	[SerializeField]
	private ConfirmDialogScreen feedbackPrefab;

	// Token: 0x04004534 RID: 17716
	[SerializeField]
	private KSlider uiScaleSlider;

	// Token: 0x04004535 RID: 17717
	[SerializeField]
	private LocText sliderLabel;

	// Token: 0x04004536 RID: 17718
	[SerializeField]
	private LocText title;

	// Token: 0x04004537 RID: 17719
	[SerializeField]
	private Dropdown colorModeDropdown;

	// Token: 0x04004538 RID: 17720
	[SerializeField]
	private KImage colorExampleLogicOn;

	// Token: 0x04004539 RID: 17721
	[SerializeField]
	private KImage colorExampleLogicOff;

	// Token: 0x0400453A RID: 17722
	[SerializeField]
	private KImage colorExampleCropHalted;

	// Token: 0x0400453B RID: 17723
	[SerializeField]
	private KImage colorExampleCropGrowing;

	// Token: 0x0400453C RID: 17724
	[SerializeField]
	private KImage colorExampleCropGrown;

	// Token: 0x0400453D RID: 17725
	public static readonly string ResolutionWidthKey = "ResolutionWidth";

	// Token: 0x0400453E RID: 17726
	public static readonly string ResolutionHeightKey = "ResolutionHeight";

	// Token: 0x0400453F RID: 17727
	public static readonly string RefreshRateKeyNumerator = "RefreshRateNumerator";

	// Token: 0x04004540 RID: 17728
	public static readonly string RefreshRateKeyDenominator = "RefreshRateNumerator";

	// Token: 0x04004541 RID: 17729
	public static readonly string FullScreenKey = "FullScreen";

	// Token: 0x04004542 RID: 17730
	public static readonly string LowResKey = "LowResTextures";

	// Token: 0x04004543 RID: 17731
	public static readonly string ColorModeKey = "ColorModeID";

	// Token: 0x04004544 RID: 17732
	private const FullScreenMode FULLSCREEN = FullScreenMode.MaximizedWindow;

	// Token: 0x04004545 RID: 17733
	private const FullScreenMode WINDOWED = FullScreenMode.Windowed;

	// Token: 0x04004546 RID: 17734
	private KCanvasScaler[] CanvasScalers;

	// Token: 0x04004547 RID: 17735
	private ConfirmDialogScreen confirmDialog;

	// Token: 0x04004548 RID: 17736
	private ConfirmDialogScreen feedbackDialog;

	// Token: 0x04004549 RID: 17737
	private List<Resolution> resolutions = new List<Resolution>();

	// Token: 0x0400454A RID: 17738
	private List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

	// Token: 0x0400454B RID: 17739
	private List<Dropdown.OptionData> colorModeOptions = new List<Dropdown.OptionData>();

	// Token: 0x0400454C RID: 17740
	private int colorModeId;

	// Token: 0x0400454D RID: 17741
	private bool colorModeChanged;

	// Token: 0x0400454E RID: 17742
	private GraphicsOptionsScreen.Settings originalSettings;

	// Token: 0x02001EA2 RID: 7842
	private struct Settings
	{
		// Token: 0x04008E1F RID: 36383
		public FullScreenMode fullscreen;

		// Token: 0x04008E20 RID: 36384
		public Resolution resolution;

		// Token: 0x04008E21 RID: 36385
		public int lowRes;

		// Token: 0x04008E22 RID: 36386
		public int colorSetId;
	}
}
