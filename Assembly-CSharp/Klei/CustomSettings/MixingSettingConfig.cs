using System;
using System.Collections.Generic;
using UnityEngine;

namespace Klei.CustomSettings
{
	// Token: 0x02000FCB RID: 4043
	public class MixingSettingConfig : ListSettingConfig
	{
		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06007CF5 RID: 31989 RVA: 0x00320F05 File Offset: 0x0031F105
		// (set) Token: 0x06007CF6 RID: 31990 RVA: 0x00320F0D File Offset: 0x0031F10D
		public string worldgenPath { get; private set; }

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06007CF7 RID: 31991 RVA: 0x00320F16 File Offset: 0x0031F116
		public virtual Sprite icon { get; }

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06007CF8 RID: 31992 RVA: 0x00320F1E File Offset: 0x0031F11E
		public virtual List<string> forbiddenClusterTags { get; }

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06007CF9 RID: 31993 RVA: 0x00320F26 File Offset: 0x0031F126
		// (set) Token: 0x06007CFA RID: 31994 RVA: 0x00320F2E File Offset: 0x0031F12E
		public virtual string dlcIdFrom { get; protected set; }

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06007CFB RID: 31995 RVA: 0x00320F37 File Offset: 0x0031F137
		public virtual bool isModded { get; }

		// Token: 0x06007CFC RID: 31996 RVA: 0x00320F40 File Offset: 0x0031F140
		protected MixingSettingConfig(string id, List<SettingLevel> levels, string default_level_id, string nosweat_default_level_id, string worldgenPath, long coordinate_range = -1L, bool debug_only = false, bool triggers_custom_game = false, string[] required_content = null, string missing_content_default = "", bool hide_in_ui = false)
			: base(id, "", "", levels, default_level_id, nosweat_default_level_id, coordinate_range, debug_only, triggers_custom_game, required_content, missing_content_default, hide_in_ui)
		{
			this.worldgenPath = worldgenPath;
		}
	}
}
