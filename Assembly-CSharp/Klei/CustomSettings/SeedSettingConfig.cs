using System;
using System.Collections.Generic;

namespace Klei.CustomSettings
{
	// Token: 0x02000FC9 RID: 4041
	public class SeedSettingConfig : SettingConfig
	{
		// Token: 0x06007CEF RID: 31983 RVA: 0x00320E2C File Offset: 0x0031F02C
		public SeedSettingConfig(string id, string label, string tooltip, bool debug_only = false, bool triggers_custom_game = true)
			: base(id, label, tooltip, "", "", -1L, debug_only, triggers_custom_game, null, "", false)
		{
		}

		// Token: 0x06007CF0 RID: 31984 RVA: 0x00320E59 File Offset: 0x0031F059
		public override SettingLevel GetLevel(string level_id)
		{
			return new SettingLevel(level_id, level_id, level_id, 0L, null);
		}

		// Token: 0x06007CF1 RID: 31985 RVA: 0x00320E66 File Offset: 0x0031F066
		public override List<SettingLevel> GetLevels()
		{
			return new List<SettingLevel>();
		}
	}
}
