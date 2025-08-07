using System;
using System.Collections.Generic;
using FMODUnity;
using ProcGen;

namespace Database
{
	// Token: 0x02000F5E RID: 3934
	public class ColonyAchievement : Resource, IHasDlcRestrictions
	{
		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06007B3C RID: 31548 RVA: 0x0030CF7A File Offset: 0x0030B17A
		// (set) Token: 0x06007B3D RID: 31549 RVA: 0x0030CF82 File Offset: 0x0030B182
		public EventReference victoryNISSnapshot { get; private set; }

		// Token: 0x06007B3E RID: 31550 RVA: 0x0030CF8B File Offset: 0x0030B18B
		public string[] GetRequiredDlcIds()
		{
			return this.requiredDlcIds;
		}

		// Token: 0x06007B3F RID: 31551 RVA: 0x0030CF93 File Offset: 0x0030B193
		public string[] GetForbiddenDlcIds()
		{
			return this.forbiddenDlcIds;
		}

		// Token: 0x06007B40 RID: 31552 RVA: 0x0030CF9C File Offset: 0x0030B19C
		public ColonyAchievement()
		{
			this.Id = "Disabled";
			this.platformAchievementId = "Disabled";
			this.Name = "Disabled";
			this.description = "Disabled";
			this.isVictoryCondition = false;
			this.requirementChecklist = new List<ColonyAchievementRequirement>();
			this.messageTitle = string.Empty;
			this.messageBody = string.Empty;
			this.shortVideoName = string.Empty;
			this.loopVideoName = string.Empty;
			this.platformAchievementId = string.Empty;
			this.icon = string.Empty;
			this.clusterTag = string.Empty;
			this.Disabled = true;
		}

		// Token: 0x06007B41 RID: 31553 RVA: 0x0030D04C File Offset: 0x0030B24C
		public ColonyAchievement(string Id, string platformAchievementId, string Name, string description, bool isVictoryCondition, List<ColonyAchievementRequirement> requirementChecklist, string messageTitle = "", string messageBody = "", string videoDataName = "", string victoryLoopVideo = "", Action<KMonoBehaviour> VictorySequence = null, EventReference victorySnapshot = default(EventReference), string icon = "", string[] requiredDlcIds = null, string[] forbiddenDlcIds = null, string dlcIdFrom = null, string clusterTag = null)
			: base(Id, Name)
		{
			this.Id = Id;
			this.platformAchievementId = platformAchievementId;
			this.Name = Name;
			this.description = description;
			this.isVictoryCondition = isVictoryCondition;
			this.requirementChecklist = requirementChecklist;
			this.messageTitle = messageTitle;
			this.messageBody = messageBody;
			this.shortVideoName = videoDataName;
			this.loopVideoName = victoryLoopVideo;
			this.victorySequence = VictorySequence;
			this.victoryNISSnapshot = (victorySnapshot.IsNull ? AudioMixerSnapshots.Get().VictoryNISGenericSnapshot : victorySnapshot);
			this.icon = icon;
			this.clusterTag = clusterTag;
			this.requiredDlcIds = requiredDlcIds;
			this.forbiddenDlcIds = forbiddenDlcIds;
			this.dlcIdFrom = dlcIdFrom;
		}

		// Token: 0x06007B42 RID: 31554 RVA: 0x0030D108 File Offset: 0x0030B308
		public bool IsValidForSave()
		{
			if (this.clusterTag.IsNullOrWhiteSpace())
			{
				return true;
			}
			DebugUtil.Assert(CustomGameSettings.Instance != null, "IsValidForSave called when CustomGamesSettings is not initialized.");
			ClusterLayout currentClusterLayout = CustomGameSettings.Instance.GetCurrentClusterLayout();
			return currentClusterLayout != null && currentClusterLayout.clusterTags.Contains(this.clusterTag);
		}

		// Token: 0x04005A10 RID: 23056
		public string description;

		// Token: 0x04005A11 RID: 23057
		public bool isVictoryCondition;

		// Token: 0x04005A12 RID: 23058
		public string messageTitle;

		// Token: 0x04005A13 RID: 23059
		public string messageBody;

		// Token: 0x04005A14 RID: 23060
		public string shortVideoName;

		// Token: 0x04005A15 RID: 23061
		public string loopVideoName;

		// Token: 0x04005A16 RID: 23062
		public string platformAchievementId;

		// Token: 0x04005A17 RID: 23063
		public string icon;

		// Token: 0x04005A18 RID: 23064
		public string clusterTag;

		// Token: 0x04005A19 RID: 23065
		public List<ColonyAchievementRequirement> requirementChecklist = new List<ColonyAchievementRequirement>();

		// Token: 0x04005A1A RID: 23066
		public Action<KMonoBehaviour> victorySequence;

		// Token: 0x04005A1C RID: 23068
		public string[] requiredDlcIds;

		// Token: 0x04005A1D RID: 23069
		public string[] forbiddenDlcIds;

		// Token: 0x04005A1E RID: 23070
		public string dlcIdFrom;
	}
}
