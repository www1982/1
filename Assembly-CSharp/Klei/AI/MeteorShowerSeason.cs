using System;
using System.Diagnostics;
using Klei.CustomSettings;

namespace Klei.AI
{
	// Token: 0x02000FF9 RID: 4089
	[DebuggerDisplay("{base.Id}")]
	public class MeteorShowerSeason : GameplaySeason
	{
		// Token: 0x06007E38 RID: 32312 RVA: 0x003285E4 File Offset: 0x003267E4
		public MeteorShowerSeason(string id, GameplaySeason.Type type, float period, bool synchronizedToPeriod, float randomizedEventStartTime = -1f, bool startActive = false, int finishAfterNumEvents = -1, float minCycle = 0f, float maxCycle = float.PositiveInfinity, int numEventsToStartEachPeriod = 1, bool affectedByDifficultySettings = true, float clusterTravelDuration = -1f, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
			: base(id, type, period, synchronizedToPeriod, randomizedEventStartTime, startActive, finishAfterNumEvents, minCycle, maxCycle, numEventsToStartEachPeriod, requiredDlcIds, forbiddenDlcIds)
		{
			this.affectedByDifficultySettings = affectedByDifficultySettings;
			this.clusterTravelDuration = clusterTravelDuration;
		}

		// Token: 0x06007E39 RID: 32313 RVA: 0x00328630 File Offset: 0x00326830
		[Obsolete]
		public MeteorShowerSeason(string id, GameplaySeason.Type type, string dlcId, float period, bool synchronizedToPeriod, float randomizedEventStartTime = -1f, bool startActive = false, int finishAfterNumEvents = -1, float minCycle = 0f, float maxCycle = float.PositiveInfinity, int numEventsToStartEachPeriod = 1, bool affectedByDifficultySettings = true, float clusterTravelDuration = -1f)
			: base(id, type, period, synchronizedToPeriod, randomizedEventStartTime, startActive, finishAfterNumEvents, minCycle, maxCycle, numEventsToStartEachPeriod, new string[] { dlcId }, null)
		{
		}

		// Token: 0x06007E3A RID: 32314 RVA: 0x00328672 File Offset: 0x00326872
		public override void AdditionalEventInstanceSetup(StateMachine.Instance generic_smi)
		{
			(generic_smi as MeteorShowerEvent.StatesInstance).clusterTravelDuration = this.clusterTravelDuration;
		}

		// Token: 0x06007E3B RID: 32315 RVA: 0x00328688 File Offset: 0x00326888
		public override float GetSeasonPeriod()
		{
			SettingLevel currentQualitySetting = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.MeteorShowers);
			float num = base.GetSeasonPeriod();
			if (this.affectedByDifficultySettings && currentQualitySetting != null)
			{
				string id = currentQualitySetting.id;
				if (!(id == "Infrequent"))
				{
					if (!(id == "Intense"))
					{
						if (id == "Doomed")
						{
							num *= 1f;
						}
					}
					else
					{
						num *= 1f;
					}
				}
				else
				{
					num *= 2f;
				}
			}
			return num;
		}

		// Token: 0x04005F42 RID: 24386
		public bool affectedByDifficultySettings = true;

		// Token: 0x04005F43 RID: 24387
		public float clusterTravelDuration = -1f;
	}
}
