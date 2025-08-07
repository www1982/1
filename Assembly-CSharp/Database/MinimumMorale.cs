using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

namespace Database
{
	// Token: 0x02000F24 RID: 3876
	public class MinimumMorale : VictoryColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007A4E RID: 31310 RVA: 0x003080B7 File Offset: 0x003062B7
		public override string Name()
		{
			return string.Format(COLONY_ACHIEVEMENTS.THRIVING.REQUIREMENTS.MINIMUM_MORALE, this.minimumMorale);
		}

		// Token: 0x06007A4F RID: 31311 RVA: 0x003080D3 File Offset: 0x003062D3
		public override string Description()
		{
			return string.Format(COLONY_ACHIEVEMENTS.THRIVING.REQUIREMENTS.MINIMUM_MORALE_DESCRIPTION, this.minimumMorale);
		}

		// Token: 0x06007A50 RID: 31312 RVA: 0x003080EF File Offset: 0x003062EF
		public MinimumMorale(int minimumMorale = 16)
		{
			this.minimumMorale = minimumMorale;
		}

		// Token: 0x06007A51 RID: 31313 RVA: 0x00308100 File Offset: 0x00306300
		public override bool Success()
		{
			bool flag = true;
			foreach (object obj in Components.MinionAssignablesProxy)
			{
				GameObject targetGameObject = ((MinionAssignablesProxy)obj).GetTargetGameObject();
				if (targetGameObject != null && !targetGameObject.HasTag(GameTags.Dead))
				{
					AttributeInstance attributeInstance = Db.Get().Attributes.QualityOfLife.Lookup(targetGameObject.GetComponent<MinionModifiers>());
					flag = attributeInstance != null && attributeInstance.GetTotalValue() >= (float)this.minimumMorale && flag;
				}
			}
			return flag;
		}

		// Token: 0x06007A52 RID: 31314 RVA: 0x003081A8 File Offset: 0x003063A8
		public void Deserialize(IReader reader)
		{
			this.minimumMorale = reader.ReadInt32();
		}

		// Token: 0x06007A53 RID: 31315 RVA: 0x003081B6 File Offset: 0x003063B6
		public override string GetProgress(bool complete)
		{
			return this.Description();
		}

		// Token: 0x040059A9 RID: 22953
		public int minimumMorale;
	}
}
