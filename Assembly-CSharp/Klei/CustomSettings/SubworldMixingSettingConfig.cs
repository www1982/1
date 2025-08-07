using System;
using System.Collections.Generic;
using ProcGen;
using STRINGS;
using UnityEngine;

namespace Klei.CustomSettings
{
	// Token: 0x02000FCD RID: 4045
	public class SubworldMixingSettingConfig : MixingSettingConfig
	{
		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06007D03 RID: 32003 RVA: 0x00321128 File Offset: 0x0031F328
		public override string label
		{
			get
			{
				SubworldMixingSettings cachedSubworldMixingSetting = SettingsCache.GetCachedSubworldMixingSetting(base.worldgenPath);
				StringEntry stringEntry;
				if (!Strings.TryGet(cachedSubworldMixingSetting.name, out stringEntry))
				{
					return cachedSubworldMixingSetting.name;
				}
				return stringEntry;
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06007D04 RID: 32004 RVA: 0x00321160 File Offset: 0x0031F360
		public override string tooltip
		{
			get
			{
				SubworldMixingSettings cachedSubworldMixingSetting = SettingsCache.GetCachedSubworldMixingSetting(base.worldgenPath);
				StringEntry stringEntry;
				if (!Strings.TryGet(cachedSubworldMixingSetting.description, out stringEntry))
				{
					return cachedSubworldMixingSetting.description;
				}
				return stringEntry;
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06007D05 RID: 32005 RVA: 0x00321198 File Offset: 0x0031F398
		public override Sprite icon
		{
			get
			{
				SubworldMixingSettings cachedSubworldMixingSetting = SettingsCache.GetCachedSubworldMixingSetting(base.worldgenPath);
				Sprite sprite = ((cachedSubworldMixingSetting.icon != null) ? Assets.GetSprite(cachedSubworldMixingSetting.icon) : null);
				if (sprite == null)
				{
					sprite = Assets.GetSprite("unknown");
				}
				return sprite;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06007D06 RID: 32006 RVA: 0x003211E7 File Offset: 0x0031F3E7
		public override List<string> forbiddenClusterTags
		{
			get
			{
				return SettingsCache.GetCachedSubworldMixingSetting(base.worldgenPath).forbiddenClusterTags;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06007D07 RID: 32007 RVA: 0x003211F9 File Offset: 0x0031F3F9
		public override bool isModded
		{
			get
			{
				return SettingsCache.GetCachedSubworldMixingSetting(base.worldgenPath).isModded;
			}
		}

		// Token: 0x06007D08 RID: 32008 RVA: 0x0032120C File Offset: 0x0031F40C
		public SubworldMixingSettingConfig(string id, string worldgenPath, string[] required_content = null, string dlcIdFrom = null, bool triggers_custom_game = true, long coordinate_range = 5L)
			: base(id, null, null, null, worldgenPath, coordinate_range, false, triggers_custom_game, required_content, "", false)
		{
			this.dlcIdFrom = dlcIdFrom;
			List<SettingLevel> list = new List<SettingLevel>
			{
				new SettingLevel("Disabled", UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.SUBWORLD_MIXING.LEVELS.DISABLED.NAME, DlcManager.FeatureClusterSpaceEnabled() ? UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.SUBWORLD_MIXING.LEVELS.DISABLED.TOOLTIP : UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.SUBWORLD_MIXING.LEVELS.DISABLED.TOOLTIP_BASEGAME, 0L, null),
				new SettingLevel("TryMixing", UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.SUBWORLD_MIXING.LEVELS.TRY_MIXING.NAME, DlcManager.FeatureClusterSpaceEnabled() ? UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.SUBWORLD_MIXING.LEVELS.TRY_MIXING.TOOLTIP : UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.SUBWORLD_MIXING.LEVELS.TRY_MIXING.TOOLTIP_BASEGAME, 1L, null),
				new SettingLevel("GuranteeMixing", UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.SUBWORLD_MIXING.LEVELS.GUARANTEE_MIXING.NAME, DlcManager.FeatureClusterSpaceEnabled() ? UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.SUBWORLD_MIXING.LEVELS.GUARANTEE_MIXING.TOOLTIP : UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.SUBWORLD_MIXING.LEVELS.GUARANTEE_MIXING.TOOLTIP_BASEGAME, 2L, null)
			};
			base.StompLevels(list, "Disabled", "Disabled");
		}

		// Token: 0x04005E60 RID: 24160
		private const int COORDINATE_RANGE = 5;

		// Token: 0x04005E61 RID: 24161
		public const string DisabledLevelId = "Disabled";

		// Token: 0x04005E62 RID: 24162
		public const string TryMixingLevelId = "TryMixing";

		// Token: 0x04005E63 RID: 24163
		public const string GuaranteeMixingLevelId = "GuranteeMixing";
	}
}
