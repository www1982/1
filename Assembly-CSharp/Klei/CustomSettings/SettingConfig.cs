using System;
using System.Collections.Generic;

namespace Klei.CustomSettings
{
	// Token: 0x02000FC6 RID: 4038
	public abstract class SettingConfig : IHasDlcRestrictions
	{
		// Token: 0x06007CC1 RID: 31937 RVA: 0x003208A4 File Offset: 0x0031EAA4
		public SettingConfig(string id, string label, string tooltip, string default_level_id, string nosweat_default_level_id, long coordinate_range = -1L, bool debug_only = false, bool triggers_custom_game = true, string[] required_content = null, string missing_content_default = "", bool hide_in_ui = false)
		{
			this.id = id;
			this.label = label;
			this.tooltip = tooltip;
			this.default_level_id = default_level_id;
			this.nosweat_default_level_id = nosweat_default_level_id;
			this.coordinate_range = coordinate_range;
			this.debug_only = debug_only;
			this.triggers_custom_game = triggers_custom_game;
			this.required_content = required_content;
			this.missing_content_default = missing_content_default;
			this.hide_in_ui = hide_in_ui;
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06007CC2 RID: 31938 RVA: 0x0032090C File Offset: 0x0031EB0C
		// (set) Token: 0x06007CC3 RID: 31939 RVA: 0x00320914 File Offset: 0x0031EB14
		public string id { get; private set; }

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06007CC4 RID: 31940 RVA: 0x0032091D File Offset: 0x0031EB1D
		// (set) Token: 0x06007CC5 RID: 31941 RVA: 0x00320925 File Offset: 0x0031EB25
		public virtual string label { get; private set; }

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06007CC6 RID: 31942 RVA: 0x0032092E File Offset: 0x0031EB2E
		// (set) Token: 0x06007CC7 RID: 31943 RVA: 0x00320936 File Offset: 0x0031EB36
		public virtual string tooltip { get; private set; }

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06007CC8 RID: 31944 RVA: 0x0032093F File Offset: 0x0031EB3F
		// (set) Token: 0x06007CC9 RID: 31945 RVA: 0x00320947 File Offset: 0x0031EB47
		public long coordinate_range { get; protected set; }

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06007CCA RID: 31946 RVA: 0x00320950 File Offset: 0x0031EB50
		// (set) Token: 0x06007CCB RID: 31947 RVA: 0x00320958 File Offset: 0x0031EB58
		public string[] required_content { get; private set; }

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06007CCC RID: 31948 RVA: 0x00320961 File Offset: 0x0031EB61
		// (set) Token: 0x06007CCD RID: 31949 RVA: 0x00320969 File Offset: 0x0031EB69
		public string missing_content_default { get; private set; }

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06007CCE RID: 31950 RVA: 0x00320972 File Offset: 0x0031EB72
		// (set) Token: 0x06007CCF RID: 31951 RVA: 0x0032097A File Offset: 0x0031EB7A
		public bool triggers_custom_game { get; protected set; }

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06007CD0 RID: 31952 RVA: 0x00320983 File Offset: 0x0031EB83
		// (set) Token: 0x06007CD1 RID: 31953 RVA: 0x0032098B File Offset: 0x0031EB8B
		public bool debug_only { get; protected set; }

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06007CD2 RID: 31954 RVA: 0x00320994 File Offset: 0x0031EB94
		// (set) Token: 0x06007CD3 RID: 31955 RVA: 0x0032099C File Offset: 0x0031EB9C
		public bool hide_in_ui { get; protected set; }

		// Token: 0x06007CD4 RID: 31956
		public abstract SettingLevel GetLevel(string level_id);

		// Token: 0x06007CD5 RID: 31957
		public abstract List<SettingLevel> GetLevels();

		// Token: 0x06007CD6 RID: 31958 RVA: 0x003209A5 File Offset: 0x0031EBA5
		public bool IsDefaultLevel(string level_id)
		{
			return level_id == this.default_level_id;
		}

		// Token: 0x06007CD7 RID: 31959 RVA: 0x003209B3 File Offset: 0x0031EBB3
		public bool ShowInUI()
		{
			return !this.deprecated && !this.hide_in_ui && (!this.debug_only || DebugHandler.enabled) && DlcManager.IsAllContentSubscribed(this.required_content);
		}

		// Token: 0x06007CD8 RID: 31960 RVA: 0x003209E6 File Offset: 0x0031EBE6
		public string GetDefaultLevelId()
		{
			if (!DlcManager.IsAllContentSubscribed(this.required_content) && !string.IsNullOrEmpty(this.missing_content_default))
			{
				return this.missing_content_default;
			}
			return this.default_level_id;
		}

		// Token: 0x06007CD9 RID: 31961 RVA: 0x00320A0F File Offset: 0x0031EC0F
		public string GetNoSweatDefaultLevelId()
		{
			if (!DlcManager.IsAllContentSubscribed(this.required_content) && !string.IsNullOrEmpty(this.missing_content_default))
			{
				return this.missing_content_default;
			}
			return this.nosweat_default_level_id;
		}

		// Token: 0x06007CDA RID: 31962 RVA: 0x00320A38 File Offset: 0x0031EC38
		public string[] GetRequiredDlcIds()
		{
			return this.required_content;
		}

		// Token: 0x06007CDB RID: 31963 RVA: 0x00320A40 File Offset: 0x0031EC40
		public string[] GetForbiddenDlcIds()
		{
			return null;
		}

		// Token: 0x04005E47 RID: 24135
		protected string default_level_id;

		// Token: 0x04005E48 RID: 24136
		protected string nosweat_default_level_id;

		// Token: 0x04005E4F RID: 24143
		public bool deprecated;
	}
}
