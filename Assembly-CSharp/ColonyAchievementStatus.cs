using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Database;

// Token: 0x0200081F RID: 2079
public class ColonyAchievementStatus
{
	// Token: 0x170003D8 RID: 984
	// (get) Token: 0x06003902 RID: 14594 RVA: 0x0013C212 File Offset: 0x0013A412
	public List<ColonyAchievementRequirement> Requirements
	{
		get
		{
			return this.m_achievement.requirementChecklist;
		}
	}

	// Token: 0x06003903 RID: 14595 RVA: 0x0013C21F File Offset: 0x0013A41F
	public ColonyAchievementStatus(string achievementId)
	{
		this.m_achievement = Db.Get().ColonyAchievements.TryGet(achievementId);
		if (this.m_achievement == null)
		{
			this.m_achievement = new ColonyAchievement();
		}
	}

	// Token: 0x06003904 RID: 14596 RVA: 0x0013C250 File Offset: 0x0013A450
	public void UpdateAchievement()
	{
		if (this.Requirements.Count <= 0)
		{
			return;
		}
		if (this.m_achievement.Disabled)
		{
			return;
		}
		this.success = true;
		foreach (ColonyAchievementRequirement colonyAchievementRequirement in this.Requirements)
		{
			this.success &= colonyAchievementRequirement.Success();
			this.failed |= colonyAchievementRequirement.Fail();
		}
	}

	// Token: 0x06003905 RID: 14597 RVA: 0x0013C2E8 File Offset: 0x0013A4E8
	public static ColonyAchievementStatus Deserialize(IReader reader, string achievementId)
	{
		bool flag = reader.ReadByte() > 0;
		bool flag2 = reader.ReadByte() > 0;
		if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 22))
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				Type type = Type.GetType(reader.ReadKleiString());
				if (type != null)
				{
					AchievementRequirementSerialization_Deprecated achievementRequirementSerialization_Deprecated = FormatterServices.GetUninitializedObject(type) as AchievementRequirementSerialization_Deprecated;
					Debug.Assert(achievementRequirementSerialization_Deprecated != null, string.Format("Cannot deserialize old data for type {0}", type));
					achievementRequirementSerialization_Deprecated.Deserialize(reader);
				}
			}
		}
		return new ColonyAchievementStatus(achievementId)
		{
			success = flag,
			failed = flag2
		};
	}

	// Token: 0x06003906 RID: 14598 RVA: 0x0013C389 File Offset: 0x0013A589
	public void Serialize(BinaryWriter writer)
	{
		writer.Write(this.success ? 1 : 0);
		writer.Write(this.failed ? 1 : 0);
	}

	// Token: 0x04002265 RID: 8805
	public bool success;

	// Token: 0x04002266 RID: 8806
	public bool failed;

	// Token: 0x04002267 RID: 8807
	private ColonyAchievement m_achievement;
}
