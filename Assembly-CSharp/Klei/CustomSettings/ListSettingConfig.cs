using System;
using System.Collections.Generic;
using UnityEngine;

namespace Klei.CustomSettings
{
	// Token: 0x02000FC7 RID: 4039
	public class ListSettingConfig : SettingConfig
	{
		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06007CDC RID: 31964 RVA: 0x00320A43 File Offset: 0x0031EC43
		// (set) Token: 0x06007CDD RID: 31965 RVA: 0x00320A4B File Offset: 0x0031EC4B
		public List<SettingLevel> levels { get; private set; }

		// Token: 0x06007CDE RID: 31966 RVA: 0x00320A54 File Offset: 0x0031EC54
		public ListSettingConfig(string id, string label, string tooltip, List<SettingLevel> levels, string default_level_id, string nosweat_default_level_id, long coordinate_range = -1L, bool debug_only = false, bool triggers_custom_game = true, string[] required_content = null, string missing_content_default = "", bool hide_in_ui = false)
			: base(id, label, tooltip, default_level_id, nosweat_default_level_id, coordinate_range, debug_only, triggers_custom_game, required_content, missing_content_default, hide_in_ui)
		{
			this.levels = levels;
		}

		// Token: 0x06007CDF RID: 31967 RVA: 0x00320A82 File Offset: 0x0031EC82
		public void StompLevels(List<SettingLevel> levels, string default_level_id, string nosweat_default_level_id)
		{
			this.levels = levels;
			this.default_level_id = default_level_id;
			this.nosweat_default_level_id = nosweat_default_level_id;
		}

		// Token: 0x06007CE0 RID: 31968 RVA: 0x00320A9C File Offset: 0x0031EC9C
		public override SettingLevel GetLevel(string level_id)
		{
			for (int i = 0; i < this.levels.Count; i++)
			{
				if (this.levels[i].id == level_id)
				{
					return this.levels[i];
				}
			}
			for (int j = 0; j < this.levels.Count; j++)
			{
				if (this.levels[j].id == this.default_level_id)
				{
					return this.levels[j];
				}
			}
			global::Debug.LogError("Unable to find setting level for setting:" + base.id + " level: " + level_id);
			return null;
		}

		// Token: 0x06007CE1 RID: 31969 RVA: 0x00320B42 File Offset: 0x0031ED42
		public override List<SettingLevel> GetLevels()
		{
			return this.levels;
		}

		// Token: 0x06007CE2 RID: 31970 RVA: 0x00320B4C File Offset: 0x0031ED4C
		public string CycleSettingLevelID(string current_id, int direction)
		{
			string text = "";
			if (current_id == "")
			{
				current_id = this.levels[0].id;
			}
			for (int i = 0; i < this.levels.Count; i++)
			{
				if (this.levels[i].id == current_id)
				{
					int num = Mathf.Clamp(i + direction, 0, this.levels.Count - 1);
					text = this.levels[num].id;
					break;
				}
			}
			return text;
		}

		// Token: 0x06007CE3 RID: 31971 RVA: 0x00320BDC File Offset: 0x0031EDDC
		public bool IsFirstLevel(string level_id)
		{
			return this.levels.FindIndex((SettingLevel l) => l.id == level_id) == 0;
		}

		// Token: 0x06007CE4 RID: 31972 RVA: 0x00320C10 File Offset: 0x0031EE10
		public bool IsLastLevel(string level_id)
		{
			return this.levels.FindIndex((SettingLevel l) => l.id == level_id) == this.levels.Count - 1;
		}
	}
}
