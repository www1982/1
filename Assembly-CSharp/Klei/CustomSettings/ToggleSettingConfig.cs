using System;
using System.Collections.Generic;

namespace Klei.CustomSettings
{
	// Token: 0x02000FC8 RID: 4040
	public class ToggleSettingConfig : SettingConfig
	{
		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06007CE5 RID: 31973 RVA: 0x00320C50 File Offset: 0x0031EE50
		// (set) Token: 0x06007CE6 RID: 31974 RVA: 0x00320C58 File Offset: 0x0031EE58
		public SettingLevel on_level { get; private set; }

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06007CE7 RID: 31975 RVA: 0x00320C61 File Offset: 0x0031EE61
		// (set) Token: 0x06007CE8 RID: 31976 RVA: 0x00320C69 File Offset: 0x0031EE69
		public SettingLevel off_level { get; private set; }

		// Token: 0x06007CE9 RID: 31977 RVA: 0x00320C74 File Offset: 0x0031EE74
		public ToggleSettingConfig(string id, string label, string tooltip, SettingLevel off_level, SettingLevel on_level, string default_level_id, string nosweat_default_level_id, long coordinate_range = -1L, bool debug_only = false, bool triggers_custom_game = true, string[] required_content = null, string missing_content_default = "")
			: base(id, label, tooltip, default_level_id, nosweat_default_level_id, coordinate_range, debug_only, triggers_custom_game, required_content, missing_content_default, false)
		{
			this.off_level = off_level;
			this.on_level = on_level;
		}

		// Token: 0x06007CEA RID: 31978 RVA: 0x00320CA9 File Offset: 0x0031EEA9
		public void StompLevels(SettingLevel off_level, SettingLevel on_level, string default_level_id, string nosweat_default_level_id)
		{
			this.off_level = off_level;
			this.on_level = on_level;
			this.default_level_id = default_level_id;
			this.nosweat_default_level_id = nosweat_default_level_id;
		}

		// Token: 0x06007CEB RID: 31979 RVA: 0x00320CC8 File Offset: 0x0031EEC8
		public override SettingLevel GetLevel(string level_id)
		{
			if (this.on_level.id == level_id)
			{
				return this.on_level;
			}
			if (this.off_level.id == level_id)
			{
				return this.off_level;
			}
			if (this.default_level_id == this.on_level.id)
			{
				Debug.LogWarning(string.Concat(new string[] { "Unable to find level for setting:", base.id, "(", level_id, ") Using default level." }));
				return this.on_level;
			}
			if (this.default_level_id == this.off_level.id)
			{
				Debug.LogWarning(string.Concat(new string[] { "Unable to find level for setting:", base.id, "(", level_id, ") Using default level." }));
				return this.off_level;
			}
			Debug.LogError("Unable to find setting level for setting:" + base.id + " level: " + level_id);
			return null;
		}

		// Token: 0x06007CEC RID: 31980 RVA: 0x00320DCD File Offset: 0x0031EFCD
		public override List<SettingLevel> GetLevels()
		{
			return new List<SettingLevel> { this.off_level, this.on_level };
		}

		// Token: 0x06007CED RID: 31981 RVA: 0x00320DEC File Offset: 0x0031EFEC
		public string ToggleSettingLevelID(string current_id)
		{
			if (this.on_level.id == current_id)
			{
				return this.off_level.id;
			}
			return this.on_level.id;
		}

		// Token: 0x06007CEE RID: 31982 RVA: 0x00320E18 File Offset: 0x0031F018
		public bool IsOnLevel(string level_id)
		{
			return level_id == this.on_level.id;
		}
	}
}
