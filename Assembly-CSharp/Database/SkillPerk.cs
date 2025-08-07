using System;

namespace Database
{
	// Token: 0x02000F5F RID: 3935
	public class SkillPerk : Resource, IHasDlcRestrictions
	{
		// Token: 0x06007B43 RID: 31555 RVA: 0x0030D15A File Offset: 0x0030B35A
		public string[] GetRequiredDlcIds()
		{
			return this.requiredDlcIds;
		}

		// Token: 0x06007B44 RID: 31556 RVA: 0x0030D162 File Offset: 0x0030B362
		public string[] GetForbiddenDlcIds()
		{
			return null;
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06007B45 RID: 31557 RVA: 0x0030D165 File Offset: 0x0030B365
		// (set) Token: 0x06007B46 RID: 31558 RVA: 0x0030D16D File Offset: 0x0030B36D
		public Action<MinionResume> OnApply { get; protected set; }

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06007B47 RID: 31559 RVA: 0x0030D176 File Offset: 0x0030B376
		// (set) Token: 0x06007B48 RID: 31560 RVA: 0x0030D17E File Offset: 0x0030B37E
		public Action<MinionResume> OnRemove { get; protected set; }

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06007B49 RID: 31561 RVA: 0x0030D187 File Offset: 0x0030B387
		// (set) Token: 0x06007B4A RID: 31562 RVA: 0x0030D18F File Offset: 0x0030B38F
		public Action<MinionResume> OnMinionsChanged { get; protected set; }

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06007B4B RID: 31563 RVA: 0x0030D198 File Offset: 0x0030B398
		// (set) Token: 0x06007B4C RID: 31564 RVA: 0x0030D1A0 File Offset: 0x0030B3A0
		public bool affectAll { get; protected set; }

		// Token: 0x06007B4D RID: 31565 RVA: 0x0030D1AC File Offset: 0x0030B3AC
		public static string GetDescription(string perkID)
		{
			string text = GameUtil.NamesOfBuildingsRequiringSkillPerk(perkID);
			if (text == null)
			{
				return Db.Get().SkillPerks.Get(perkID).Name;
			}
			return text;
		}

		// Token: 0x06007B4E RID: 31566 RVA: 0x0030D1DA File Offset: 0x0030B3DA
		public SkillPerk(string id_str, string description, Action<MinionResume> OnApply, Action<MinionResume> OnRemove, Action<MinionResume> OnMinionsChanged, bool affectAll = false)
			: base(id_str, description)
		{
			this.OnApply = OnApply;
			this.OnRemove = OnRemove;
			this.OnMinionsChanged = OnMinionsChanged;
			this.affectAll = affectAll;
		}

		// Token: 0x06007B4F RID: 31567 RVA: 0x0030D203 File Offset: 0x0030B403
		public SkillPerk(string id_str, string description, Action<MinionResume> OnApply, Action<MinionResume> OnRemove, Action<MinionResume> OnMinionsChanged, string[] requiredDlcIds = null, bool affectAll = false)
			: base(id_str, description)
		{
			this.OnApply = OnApply;
			this.OnRemove = OnRemove;
			this.OnMinionsChanged = OnMinionsChanged;
			this.affectAll = affectAll;
			this.requiredDlcIds = requiredDlcIds;
		}

		// Token: 0x04005A1F RID: 23071
		public string[] requiredDlcIds;
	}
}
