using System;
using STRINGS;

namespace Klei.CustomSettings
{
	// Token: 0x02000FCA RID: 4042
	public class DlcMixingSettingConfig : ToggleSettingConfig
	{
		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06007CF2 RID: 31986 RVA: 0x00320E6D File Offset: 0x0031F06D
		// (set) Token: 0x06007CF3 RID: 31987 RVA: 0x00320E75 File Offset: 0x0031F075
		public virtual string dlcIdFrom { get; protected set; }

		// Token: 0x06007CF4 RID: 31988 RVA: 0x00320E80 File Offset: 0x0031F080
		public DlcMixingSettingConfig(string id, string label, string tooltip, long coordinate_range = 5L, bool triggers_custom_game = false, string[] required_content = null, string dlcIdFrom = null, string missing_content_default = "")
			: base(id, label, tooltip, null, null, null, "Disabled", coordinate_range, false, triggers_custom_game, required_content, missing_content_default)
		{
			this.dlcIdFrom = dlcIdFrom;
			SettingLevel settingLevel = new SettingLevel("Disabled", UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.DLC_MIXING.LEVELS.DISABLED.NAME, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.DLC_MIXING.LEVELS.DISABLED.TOOLTIP, 0L, null);
			SettingLevel settingLevel2 = new SettingLevel("Enabled", UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.DLC_MIXING.LEVELS.ENABLED.NAME, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.DLC_MIXING.LEVELS.ENABLED.TOOLTIP, 1L, null);
			base.StompLevels(settingLevel, settingLevel2, "Disabled", "Disabled");
		}

		// Token: 0x04005E53 RID: 24147
		private const int COORDINATE_RANGE = 5;

		// Token: 0x04005E54 RID: 24148
		public const string DisabledLevelId = "Disabled";

		// Token: 0x04005E55 RID: 24149
		public const string EnabledLevelId = "Enabled";
	}
}
