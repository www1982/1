using System;
using System.Collections.Generic;
using ProcGen;
using STRINGS;
using UnityEngine;

namespace Klei.CustomSettings
{
	// Token: 0x02000FCC RID: 4044
	public class WorldMixingSettingConfig : MixingSettingConfig
	{
		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06007CFD RID: 31997 RVA: 0x00320F78 File Offset: 0x0031F178
		public override string label
		{
			get
			{
				WorldMixingSettings cachedWorldMixingSetting = SettingsCache.GetCachedWorldMixingSetting(base.worldgenPath);
				StringEntry stringEntry;
				if (!Strings.TryGet(cachedWorldMixingSetting.name, out stringEntry))
				{
					return cachedWorldMixingSetting.name;
				}
				return stringEntry;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06007CFE RID: 31998 RVA: 0x00320FB0 File Offset: 0x0031F1B0
		public override string tooltip
		{
			get
			{
				WorldMixingSettings cachedWorldMixingSetting = SettingsCache.GetCachedWorldMixingSetting(base.worldgenPath);
				StringEntry stringEntry;
				if (!Strings.TryGet(cachedWorldMixingSetting.description, out stringEntry))
				{
					return cachedWorldMixingSetting.description;
				}
				return stringEntry;
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06007CFF RID: 31999 RVA: 0x00320FE8 File Offset: 0x0031F1E8
		public override Sprite icon
		{
			get
			{
				WorldMixingSettings cachedWorldMixingSetting = SettingsCache.GetCachedWorldMixingSetting(base.worldgenPath);
				Sprite sprite = ((cachedWorldMixingSetting.icon != null) ? ColonyDestinationAsteroidBeltData.GetUISprite(cachedWorldMixingSetting.icon) : null);
				if (sprite == null)
				{
					sprite = Assets.GetSprite(cachedWorldMixingSetting.icon);
				}
				if (sprite == null)
				{
					sprite = Assets.GetSprite("unknown");
				}
				return sprite;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06007D00 RID: 32000 RVA: 0x0032104C File Offset: 0x0031F24C
		public override List<string> forbiddenClusterTags
		{
			get
			{
				return SettingsCache.GetCachedWorldMixingSetting(base.worldgenPath).forbiddenClusterTags;
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06007D01 RID: 32001 RVA: 0x0032105E File Offset: 0x0031F25E
		public override bool isModded
		{
			get
			{
				return SettingsCache.GetCachedWorldMixingSetting(base.worldgenPath).isModded;
			}
		}

		// Token: 0x06007D02 RID: 32002 RVA: 0x00321070 File Offset: 0x0031F270
		public WorldMixingSettingConfig(string id, string worldgenPath, string[] required_content = null, string dlcIdFrom = null, bool triggers_custom_game = true, long coordinate_range = 5L)
			: base(id, null, null, null, worldgenPath, coordinate_range, false, triggers_custom_game, required_content, "", false)
		{
			this.dlcIdFrom = dlcIdFrom;
			List<SettingLevel> list = new List<SettingLevel>
			{
				new SettingLevel("Disabled", UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.WORLD_MIXING.LEVELS.DISABLED.NAME, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.WORLD_MIXING.LEVELS.DISABLED.TOOLTIP, 0L, null),
				new SettingLevel("TryMixing", UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.WORLD_MIXING.LEVELS.TRY_MIXING.NAME, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.WORLD_MIXING.LEVELS.TRY_MIXING.TOOLTIP, 1L, null),
				new SettingLevel("GuranteeMixing", UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.WORLD_MIXING.LEVELS.GUARANTEE_MIXING.NAME, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.WORLD_MIXING.LEVELS.GUARANTEE_MIXING.TOOLTIP, 2L, null)
			};
			base.StompLevels(list, "Disabled", "Disabled");
		}

		// Token: 0x04005E5C RID: 24156
		private const int COORDINATE_RANGE = 5;

		// Token: 0x04005E5D RID: 24157
		public const string DisabledLevelId = "Disabled";

		// Token: 0x04005E5E RID: 24158
		public const string TryMixingLevelId = "TryMixing";

		// Token: 0x04005E5F RID: 24159
		public const string GuaranteeMixingLevelId = "GuranteeMixing";
	}
}
