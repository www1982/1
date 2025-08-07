using System;
using STRINGS;

namespace Klei.CustomSettings
{
	// Token: 0x02000FCE RID: 4046
	public static class CustomMixingSettingsConfigs
	{
		// Token: 0x04005E64 RID: 24164
		public static SettingConfig DLC2Mixing = new DlcMixingSettingConfig("DLC2_ID", UI.DLC2.NAME, UI.DLC2.MIXING_TOOLTIP, 5L, false, DlcManager.DLC2, "DLC2_ID", "");

		// Token: 0x04005E65 RID: 24165
		public static SettingConfig DLC3Mixing = new DlcMixingSettingConfig("DLC3_ID", UI.DLC3.NAME, UI.DLC3.MIXING_TOOLTIP, 5L, false, DlcManager.DLC3, "DLC3_ID", "");

		// Token: 0x04005E66 RID: 24166
		public static SettingConfig DLC4Mixing = new DlcMixingSettingConfig("DLC4_ID", UI.DLC4.NAME, UI.DLC4.MIXING_TOOLTIP, 5L, false, DlcManager.DLC4, "DLC4_ID", "");

		// Token: 0x04005E67 RID: 24167
		public static SettingConfig CeresAsteroidMixing = new WorldMixingSettingConfig("CeresAsteroidMixing", "dlc2::worldMixing/CeresMixingSettings", DlcManager.DLC2, "DLC2_ID", true, 5L);

		// Token: 0x04005E68 RID: 24168
		public static SettingConfig PrehistoricAsteroidMixing = new WorldMixingSettingConfig("PrehistoricAsteroidMixing", "dlc4::worldMixing/PrehistoricMixingSettings", DlcManager.DLC4, "DLC4_ID", true, 5L);

		// Token: 0x04005E69 RID: 24169
		public static SettingConfig IceCavesMixing = new SubworldMixingSettingConfig("IceCavesMixing", "dlc2::subworldMixing/IceCavesMixingSettings", DlcManager.DLC2, "DLC2_ID", true, 5L);

		// Token: 0x04005E6A RID: 24170
		public static SettingConfig CarrotQuarryMixing = new SubworldMixingSettingConfig("CarrotQuarryMixing", "dlc2::subworldMixing/CarrotQuarryMixingSettings", DlcManager.DLC2, "DLC2_ID", true, 5L);

		// Token: 0x04005E6B RID: 24171
		public static SettingConfig SugarWoodsMixing = new SubworldMixingSettingConfig("SugarWoodsMixing", "dlc2::subworldMixing/SugarWoodsMixingSettings", DlcManager.DLC2, "DLC2_ID", true, 5L);

		// Token: 0x04005E6C RID: 24172
		public static SettingConfig GardenMixing = new SubworldMixingSettingConfig("GardenMixing", "dlc4::subworldMixing/GardenMixingSettings", DlcManager.DLC4, "DLC4_ID", true, 5L);

		// Token: 0x04005E6D RID: 24173
		public static SettingConfig RaptorMixing = new SubworldMixingSettingConfig("RaptorMixing", "dlc4::subworldMixing/RaptorMixingSettings", DlcManager.DLC4, "DLC4_ID", true, 5L);

		// Token: 0x04005E6E RID: 24174
		public static SettingConfig WetlandsMixing = new SubworldMixingSettingConfig("WetlandsMixing", "dlc4::subworldMixing/WetlandsMixingSettings", DlcManager.DLC4, "DLC4_ID", true, 5L);
	}
}
