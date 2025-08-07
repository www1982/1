using System;
using System.Collections.Generic;
using STRINGS;

namespace Database
{
	// Token: 0x02000F35 RID: 3893
	public class SkillBranchComplete : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007AA5 RID: 31397 RVA: 0x00308CB0 File Offset: 0x00306EB0
		public SkillBranchComplete(List<Skill> skillsToMaster)
		{
			this.skillsToMaster = skillsToMaster;
		}

		// Token: 0x06007AA6 RID: 31398 RVA: 0x00308CC0 File Offset: 0x00306EC0
		public override bool Success()
		{
			foreach (MinionResume minionResume in Components.MinionResumes.Items)
			{
				foreach (Skill skill in this.skillsToMaster)
				{
					if (minionResume.HasMasteredSkill(skill.Id))
					{
						if (!minionResume.HasBeenGrantedSkill(skill))
						{
							return true;
						}
						List<Skill> allPriorSkills = Db.Get().Skills.GetAllPriorSkills(skill);
						bool flag = true;
						foreach (Skill skill2 in allPriorSkills)
						{
							flag = flag && minionResume.HasMasteredSkill(skill2.Id);
						}
						if (flag)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06007AA7 RID: 31399 RVA: 0x00308DE0 File Offset: 0x00306FE0
		public void Deserialize(IReader reader)
		{
			this.skillsToMaster = new List<Skill>();
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text = reader.ReadKleiString();
				this.skillsToMaster.Add(Db.Get().Skills.Get(text));
			}
		}

		// Token: 0x06007AA8 RID: 31400 RVA: 0x00308E2D File Offset: 0x0030702D
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.SKILL_BRANCH;
		}

		// Token: 0x040059B3 RID: 22963
		private List<Skill> skillsToMaster;
	}
}
