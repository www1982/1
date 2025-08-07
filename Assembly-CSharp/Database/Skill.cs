using System;
using System.Collections.Generic;
using TUNING;

namespace Database
{
	// Token: 0x02000F65 RID: 3941
	public class Skill : Resource, IHasDlcRestrictions
	{
		// Token: 0x06007B57 RID: 31575 RVA: 0x0030ECC8 File Offset: 0x0030CEC8
		public Skill(string id, string name, string description, int tier, string hat, string badge, string skillGroup, List<SkillPerk> perks = null, List<string> priorSkills = null, string requiredDuplicantModel = "Minion", string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
			: base(id, name)
		{
			this.description = description;
			this.requiredDlcIds = requiredDlcIds;
			this.forbiddenDlcIds = forbiddenDlcIds;
			this.tier = tier;
			this.hat = hat;
			this.badge = badge;
			this.skillGroup = skillGroup;
			this.perks = perks;
			if (this.perks == null)
			{
				this.perks = new List<SkillPerk>();
			}
			this.priorSkills = priorSkills;
			if (this.priorSkills == null)
			{
				this.priorSkills = new List<string>();
			}
			this.requiredDuplicantModel = requiredDuplicantModel;
		}

		// Token: 0x06007B58 RID: 31576 RVA: 0x0030ED54 File Offset: 0x0030CF54
		[Obsolete]
		public Skill(string id, string name, string description, string dlcId, int tier, string hat, string badge, string skillGroup, List<SkillPerk> perks = null, List<string> priorSkills = null, string requiredDuplicantModel = "Minion")
			: this(id, name, description, tier, hat, badge, skillGroup, perks, priorSkills, requiredDuplicantModel, null, null)
		{
		}

		// Token: 0x06007B59 RID: 31577 RVA: 0x0030ED7A File Offset: 0x0030CF7A
		public int GetMoraleExpectation()
		{
			return SKILLS.SKILL_TIER_MORALE_COST[this.tier];
		}

		// Token: 0x06007B5A RID: 31578 RVA: 0x0030ED88 File Offset: 0x0030CF88
		public bool GivesPerk(SkillPerk perk)
		{
			return this.perks.Contains(perk);
		}

		// Token: 0x06007B5B RID: 31579 RVA: 0x0030ED98 File Offset: 0x0030CF98
		public bool GivesPerk(HashedString perkId)
		{
			using (List<SkillPerk>.Enumerator enumerator = this.perks.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IdHash == perkId)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06007B5C RID: 31580 RVA: 0x0030EDF8 File Offset: 0x0030CFF8
		public string[] GetRequiredDlcIds()
		{
			return this.requiredDlcIds;
		}

		// Token: 0x06007B5D RID: 31581 RVA: 0x0030EE00 File Offset: 0x0030D000
		public string[] GetForbiddenDlcIds()
		{
			return this.forbiddenDlcIds;
		}

		// Token: 0x04005A9D RID: 23197
		public string description;

		// Token: 0x04005A9E RID: 23198
		public string[] requiredDlcIds;

		// Token: 0x04005A9F RID: 23199
		public string[] forbiddenDlcIds;

		// Token: 0x04005AA0 RID: 23200
		public string skillGroup;

		// Token: 0x04005AA1 RID: 23201
		public string hat;

		// Token: 0x04005AA2 RID: 23202
		public string badge;

		// Token: 0x04005AA3 RID: 23203
		public int tier;

		// Token: 0x04005AA4 RID: 23204
		public bool deprecated;

		// Token: 0x04005AA5 RID: 23205
		public List<SkillPerk> perks;

		// Token: 0x04005AA6 RID: 23206
		public List<string> priorSkills;

		// Token: 0x04005AA7 RID: 23207
		public string requiredDuplicantModel;
	}
}
