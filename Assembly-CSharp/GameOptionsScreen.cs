using System;
using System.IO;
using Steamworks;
using STRINGS;
using UnityEngine;

// Token: 0x02000CD1 RID: 3281
public class GameOptionsScreen : KModalButtonMenu
{
	// Token: 0x0600651E RID: 25886 RVA: 0x002607C7 File Offset: 0x0025E9C7
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x0600651F RID: 25887 RVA: 0x002607D0 File Offset: 0x0025E9D0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.unitConfiguration.Init();
		if (SaveGame.Instance != null)
		{
			this.saveConfiguration.ToggleDisabledContent(true);
			this.saveConfiguration.Init();
			this.SetSandboxModeActive(SaveGame.Instance.sandboxEnabled);
		}
		else
		{
			this.saveConfiguration.ToggleDisabledContent(false);
		}
		this.resetTutorialButton.onClick += this.OnTutorialReset;
		if (DistributionPlatform.Initialized && SteamUtils.IsSteamRunningOnSteamDeck())
		{
			this.controlsButton.gameObject.SetActive(false);
		}
		else
		{
			this.controlsButton.onClick += this.OnKeyBindings;
		}
		this.sandboxButton.onClick += this.OnUnlockSandboxMode;
		this.doneButton.onClick += this.Deactivate;
		this.closeButton.onClick += this.Deactivate;
		if (this.defaultToCloudSaveToggle != null)
		{
			this.RefreshCloudSaveToggle();
			this.defaultToCloudSaveToggle.GetComponentInChildren<KButton>().onClick += this.OnDefaultToCloudSaveToggle;
		}
		if (this.cloudSavesPanel != null)
		{
			this.cloudSavesPanel.SetActive(SaveLoader.GetCloudSavesAvailable());
		}
		this.cameraSpeedSlider.minValue = 1f;
		this.cameraSpeedSlider.maxValue = 20f;
		this.cameraSpeedSlider.onValueChanged.AddListener(delegate(float val)
		{
			this.OnCameraSpeedValueChanged(Mathf.FloorToInt(val));
		});
		this.cameraSpeedSlider.value = this.CameraSpeedToSlider(KPlayerPrefs.GetFloat("CameraSpeed"));
		this.RefreshCameraSliderLabel();
	}

	// Token: 0x06006520 RID: 25888 RVA: 0x00260974 File Offset: 0x0025EB74
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (SaveGame.Instance != null)
		{
			this.savePanel.SetActive(true);
			this.saveConfiguration.Show(show);
			this.SetSandboxModeActive(SaveGame.Instance.sandboxEnabled);
		}
		else
		{
			this.savePanel.SetActive(false);
		}
		if (!KPlayerPrefs.HasKey("CameraSpeed"))
		{
			CameraController.SetDefaultCameraSpeed();
		}
	}

	// Token: 0x06006521 RID: 25889 RVA: 0x002609DC File Offset: 0x0025EBDC
	private float CameraSpeedToSlider(float prefsValue)
	{
		return prefsValue * 10f;
	}

	// Token: 0x06006522 RID: 25890 RVA: 0x002609E5 File Offset: 0x0025EBE5
	private void OnCameraSpeedValueChanged(int sliderValue)
	{
		KPlayerPrefs.SetFloat("CameraSpeed", (float)sliderValue / 10f);
		this.RefreshCameraSliderLabel();
		if (Game.Instance != null)
		{
			Game.Instance.Trigger(75424175, null);
		}
	}

	// Token: 0x06006523 RID: 25891 RVA: 0x00260A1C File Offset: 0x0025EC1C
	private void RefreshCameraSliderLabel()
	{
		this.cameraSpeedSliderLabel.text = string.Format(UI.FRONTEND.GAME_OPTIONS_SCREEN.CAMERA_SPEED_LABEL, (KPlayerPrefs.GetFloat("CameraSpeed") * 10f * 10f).ToString());
	}

	// Token: 0x06006524 RID: 25892 RVA: 0x00260A61 File Offset: 0x0025EC61
	private void OnDefaultToCloudSaveToggle()
	{
		SaveLoader.SetCloudSavesDefault(!SaveLoader.GetCloudSavesDefault());
		this.RefreshCloudSaveToggle();
	}

	// Token: 0x06006525 RID: 25893 RVA: 0x00260A78 File Offset: 0x0025EC78
	private void RefreshCloudSaveToggle()
	{
		bool cloudSavesDefault = SaveLoader.GetCloudSavesDefault();
		this.defaultToCloudSaveToggle.GetComponent<HierarchyReferences>().GetReference("Checkmark").gameObject.SetActive(cloudSavesDefault);
	}

	// Token: 0x06006526 RID: 25894 RVA: 0x00260AAB File Offset: 0x0025ECAB
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape) || e.TryConsume(global::Action.MouseRight))
		{
			this.Deactivate();
			return;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x06006527 RID: 25895 RVA: 0x00260AD0 File Offset: 0x0025ECD0
	private void OnTutorialReset()
	{
		ConfirmDialogScreen component = base.ActivateChildScreen(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject).GetComponent<ConfirmDialogScreen>();
		component.PopupConfirmDialog(UI.FRONTEND.OPTIONS_SCREEN.RESET_TUTORIAL_WARNING, delegate
		{
			Tutorial.ResetHiddenTutorialMessages();
		}, delegate
		{
		}, null, null, null, null, null, null);
		component.Activate();
	}

	// Token: 0x06006528 RID: 25896 RVA: 0x00260B50 File Offset: 0x0025ED50
	private void OnUnlockSandboxMode()
	{
		ConfirmDialogScreen component = base.ActivateChildScreen(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject).GetComponent<ConfirmDialogScreen>();
		string text = UI.FRONTEND.OPTIONS_SCREEN.TOGGLE_SANDBOX_SCREEN.UNLOCK_SANDBOX_WARNING;
		global::System.Action action = delegate
		{
			SaveGame.Instance.sandboxEnabled = true;
			this.SetSandboxModeActive(SaveGame.Instance.sandboxEnabled);
			TopLeftControlScreen.Instance.UpdateSandboxToggleState();
			this.Deactivate();
		};
		global::System.Action action2 = delegate
		{
			string savePrefixAndCreateFolder = SaveLoader.GetSavePrefixAndCreateFolder();
			string text4 = SaveGame.Instance.BaseName + UI.FRONTEND.OPTIONS_SCREEN.TOGGLE_SANDBOX_SCREEN.BACKUP_SAVE_GAME_APPEND + ".sav";
			SaveLoader.Instance.Save(Path.Combine(savePrefixAndCreateFolder, text4), false, false);
			this.SetSandboxModeActive(SaveGame.Instance.sandboxEnabled);
			TopLeftControlScreen.Instance.UpdateSandboxToggleState();
			this.Deactivate();
		};
		string text2 = UI.FRONTEND.OPTIONS_SCREEN.TOGGLE_SANDBOX_SCREEN.CONFIRM;
		string text3 = UI.FRONTEND.OPTIONS_SCREEN.TOGGLE_SANDBOX_SCREEN.CONFIRM_SAVE_BACKUP;
		component.PopupConfirmDialog(text, action, action2, UI.FRONTEND.OPTIONS_SCREEN.TOGGLE_SANDBOX_SCREEN.CANCEL, delegate
		{
		}, null, text2, text3, null);
		component.Activate();
	}

	// Token: 0x06006529 RID: 25897 RVA: 0x00260BE7 File Offset: 0x0025EDE7
	private void OnKeyBindings()
	{
		base.ActivateChildScreen(this.inputBindingsScreenPrefab.gameObject);
	}

	// Token: 0x0600652A RID: 25898 RVA: 0x00260BFC File Offset: 0x0025EDFC
	private void SetSandboxModeActive(bool active)
	{
		this.sandboxButton.GetComponent<HierarchyReferences>().GetReference("Checkmark").gameObject.SetActive(active);
		this.sandboxButton.isInteractable = !active;
		this.sandboxButton.gameObject.GetComponentInParent<CanvasGroup>().alpha = (active ? 0.5f : 1f);
	}

	// Token: 0x04004511 RID: 17681
	[SerializeField]
	private SaveConfigurationScreen saveConfiguration;

	// Token: 0x04004512 RID: 17682
	[SerializeField]
	private UnitConfigurationScreen unitConfiguration;

	// Token: 0x04004513 RID: 17683
	[SerializeField]
	private KButton resetTutorialButton;

	// Token: 0x04004514 RID: 17684
	[SerializeField]
	private KButton controlsButton;

	// Token: 0x04004515 RID: 17685
	[SerializeField]
	private KButton sandboxButton;

	// Token: 0x04004516 RID: 17686
	[SerializeField]
	private ConfirmDialogScreen confirmPrefab;

	// Token: 0x04004517 RID: 17687
	[SerializeField]
	private KButton doneButton;

	// Token: 0x04004518 RID: 17688
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04004519 RID: 17689
	[SerializeField]
	private GameObject cloudSavesPanel;

	// Token: 0x0400451A RID: 17690
	[SerializeField]
	private GameObject defaultToCloudSaveToggle;

	// Token: 0x0400451B RID: 17691
	[SerializeField]
	private GameObject savePanel;

	// Token: 0x0400451C RID: 17692
	[SerializeField]
	private InputBindingsScreen inputBindingsScreenPrefab;

	// Token: 0x0400451D RID: 17693
	[SerializeField]
	private KSlider cameraSpeedSlider;

	// Token: 0x0400451E RID: 17694
	[SerializeField]
	private LocText cameraSpeedSliderLabel;

	// Token: 0x0400451F RID: 17695
	private const int cameraSliderNotchScale = 10;

	// Token: 0x04004520 RID: 17696
	public const string PREFS_KEY_CAMERA_SPEED = "CameraSpeed";
}
