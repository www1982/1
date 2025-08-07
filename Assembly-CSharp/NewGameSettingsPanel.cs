using System;
using System.Collections.Generic;
using Klei.CustomSettings;
using UnityEngine;

// Token: 0x02000D79 RID: 3449
[AddComponentMenu("KMonoBehaviour/scripts/NewGameSettingsPanel")]
public class NewGameSettingsPanel : CustomGameSettingsPanelBase
{
	// Token: 0x06006B49 RID: 27465 RVA: 0x0028871D File Offset: 0x0028691D
	public void SetCloseAction(global::System.Action onClose)
	{
		if (this.closeButton != null)
		{
			this.closeButton.onClick += onClose;
		}
		if (this.background != null)
		{
			this.background.onClick += onClose;
		}
	}

	// Token: 0x06006B4A RID: 27466 RVA: 0x00288754 File Offset: 0x00286954
	public override void Init()
	{
		CustomGameSettings.Instance.LoadClusters();
		Global.Instance.modManager.Report(base.gameObject);
		this.settings = CustomGameSettings.Instance;
		this.widgets = new List<CustomGameSettingWidget>();
		foreach (KeyValuePair<string, SettingConfig> keyValuePair in this.settings.QualitySettings)
		{
			if (keyValuePair.Value.ShowInUI())
			{
				ListSettingConfig listSettingConfig = keyValuePair.Value as ListSettingConfig;
				if (listSettingConfig != null)
				{
					CustomGameSettingListWidget customGameSettingListWidget = Util.KInstantiateUI<CustomGameSettingListWidget>(this.prefab_cycle_setting, this.content.gameObject, false);
					customGameSettingListWidget.Initialize(listSettingConfig, new Func<SettingConfig, SettingLevel>(CustomGameSettings.Instance.GetCurrentQualitySetting), new Func<ListSettingConfig, int, SettingLevel>(CustomGameSettings.Instance.CycleQualitySettingLevel));
					customGameSettingListWidget.gameObject.SetActive(true);
					base.AddWidget(customGameSettingListWidget);
				}
				else
				{
					ToggleSettingConfig toggleSettingConfig = keyValuePair.Value as ToggleSettingConfig;
					if (toggleSettingConfig != null)
					{
						CustomGameSettingToggleWidget customGameSettingToggleWidget = Util.KInstantiateUI<CustomGameSettingToggleWidget>(this.prefab_checkbox_setting, this.content.gameObject, false);
						customGameSettingToggleWidget.Initialize(toggleSettingConfig, new Func<SettingConfig, SettingLevel>(CustomGameSettings.Instance.GetCurrentQualitySetting), new Func<ToggleSettingConfig, SettingLevel>(CustomGameSettings.Instance.ToggleQualitySettingLevel));
						customGameSettingToggleWidget.gameObject.SetActive(true);
						base.AddWidget(customGameSettingToggleWidget);
					}
					else
					{
						SeedSettingConfig seedSettingConfig = keyValuePair.Value as SeedSettingConfig;
						if (seedSettingConfig != null)
						{
							CustomGameSettingSeed customGameSettingSeed = Util.KInstantiateUI<CustomGameSettingSeed>(this.prefab_seed_input_setting, this.content.gameObject, false);
							customGameSettingSeed.Initialize(seedSettingConfig);
							customGameSettingSeed.gameObject.SetActive(true);
							base.AddWidget(customGameSettingSeed);
						}
					}
				}
			}
		}
		this.Refresh();
	}

	// Token: 0x06006B4B RID: 27467 RVA: 0x00288920 File Offset: 0x00286B20
	public void ConsumeSettingsCode(string code)
	{
		this.settings.ParseAndApplySettingsCode(code);
	}

	// Token: 0x06006B4C RID: 27468 RVA: 0x0028892E File Offset: 0x00286B2E
	public void ConsumeStoryTraitsCode(string code)
	{
		this.settings.ParseAndApplyStoryTraitSettingsCode(code);
	}

	// Token: 0x06006B4D RID: 27469 RVA: 0x0028893C File Offset: 0x00286B3C
	public void ConsumeMixingSettingsCode(string code)
	{
		this.settings.ParseAndApplyMixingSettingsCode(code);
	}

	// Token: 0x06006B4E RID: 27470 RVA: 0x0028894A File Offset: 0x00286B4A
	public void SetSetting(SettingConfig setting, string level, bool notify = true)
	{
		this.settings.SetQualitySetting(setting, level, notify);
	}

	// Token: 0x06006B4F RID: 27471 RVA: 0x0028895A File Offset: 0x00286B5A
	public string GetSetting(SettingConfig setting)
	{
		return this.settings.GetCurrentQualitySetting(setting).id;
	}

	// Token: 0x06006B50 RID: 27472 RVA: 0x0028896D File Offset: 0x00286B6D
	public string GetSetting(string setting)
	{
		return this.settings.GetCurrentQualitySetting(setting).id;
	}

	// Token: 0x06006B51 RID: 27473 RVA: 0x00288980 File Offset: 0x00286B80
	public void Cancel()
	{
	}

	// Token: 0x0400490C RID: 18700
	[SerializeField]
	private Transform content;

	// Token: 0x0400490D RID: 18701
	[SerializeField]
	private KButton closeButton;

	// Token: 0x0400490E RID: 18702
	[SerializeField]
	private KButton background;

	// Token: 0x0400490F RID: 18703
	[Header("Prefab UI Refs")]
	[SerializeField]
	private GameObject prefab_cycle_setting;

	// Token: 0x04004910 RID: 18704
	[SerializeField]
	private GameObject prefab_slider_setting;

	// Token: 0x04004911 RID: 18705
	[SerializeField]
	private GameObject prefab_checkbox_setting;

	// Token: 0x04004912 RID: 18706
	[SerializeField]
	private GameObject prefab_seed_input_setting;

	// Token: 0x04004913 RID: 18707
	private CustomGameSettings settings;
}
