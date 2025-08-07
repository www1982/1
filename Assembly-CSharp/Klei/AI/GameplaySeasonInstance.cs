using System;
using System.Collections.Generic;
using System.Linq;
using KSerialization;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000FF7 RID: 4087
	[SerializationConfig(MemberSerialization.OptIn)]
	public class GameplaySeasonInstance : ISaveLoadable
	{
		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06007E2B RID: 32299 RVA: 0x003281EA File Offset: 0x003263EA
		public float NextEventTime
		{
			get
			{
				return this.nextPeriodTime + this.randomizedNextTime;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06007E2C RID: 32300 RVA: 0x003281F9 File Offset: 0x003263F9
		public GameplaySeason Season
		{
			get
			{
				if (this._season == null)
				{
					this._season = Db.Get().GameplaySeasons.TryGet(this.seasonId);
				}
				return this._season;
			}
		}

		// Token: 0x06007E2D RID: 32301 RVA: 0x00328224 File Offset: 0x00326424
		public GameplaySeasonInstance(GameplaySeason season, int worldId)
		{
			this.seasonId = season.Id;
			this.worldId = worldId;
			float currentTimeInCycles = GameUtil.GetCurrentTimeInCycles();
			if (season.synchronizedToPeriod)
			{
				float seasonPeriod = this.Season.GetSeasonPeriod();
				this.nextPeriodTime = (Mathf.Floor(currentTimeInCycles / seasonPeriod) + 1f) * seasonPeriod;
			}
			else
			{
				this.nextPeriodTime = currentTimeInCycles;
			}
			this.CalculateNextEventTime();
		}

		// Token: 0x06007E2E RID: 32302 RVA: 0x0032828C File Offset: 0x0032648C
		private void CalculateNextEventTime()
		{
			float seasonPeriod = this.Season.GetSeasonPeriod();
			this.randomizedNextTime = global::UnityEngine.Random.Range(this.Season.randomizedEventStartTime.min, this.Season.randomizedEventStartTime.max);
			float currentTimeInCycles = GameUtil.GetCurrentTimeInCycles();
			float num = this.nextPeriodTime + this.randomizedNextTime;
			while (num < currentTimeInCycles || num < this.Season.minCycle)
			{
				this.nextPeriodTime += seasonPeriod;
				num = this.nextPeriodTime + this.randomizedNextTime;
			}
		}

		// Token: 0x06007E2F RID: 32303 RVA: 0x00328314 File Offset: 0x00326514
		public bool StartEvent(bool ignorePreconditions = false)
		{
			bool flag = false;
			this.CalculateNextEventTime();
			this.numStartEvents++;
			List<GameplayEvent> list;
			if (!ignorePreconditions)
			{
				list = this.Season.events.Where((GameplayEvent x) => x.IsAllowed()).ToList<GameplayEvent>();
			}
			else
			{
				list = this.Season.events;
			}
			List<GameplayEvent> list2 = list;
			if (list2.Count > 0)
			{
				list2.ForEach(delegate(GameplayEvent x)
				{
					x.CalculatePriority();
				});
				list2.Sort();
				int num = Mathf.Min(list2.Count, 5);
				GameplayEvent gameplayEvent = list2[global::UnityEngine.Random.Range(0, num)];
				GameplayEventManager.Instance.StartNewEvent(gameplayEvent, this.worldId, new Action<StateMachine.Instance>(this.Season.AdditionalEventInstanceSetup));
				flag = true;
			}
			this.allEventWillNotRunAgain = true;
			using (List<GameplayEvent>.Enumerator enumerator = this.Season.events.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.WillNeverRunAgain())
					{
						this.allEventWillNotRunAgain = false;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06007E30 RID: 32304 RVA: 0x00328450 File Offset: 0x00326650
		public bool ShouldGenerateEvents()
		{
			WorldContainer world = ClusterManager.Instance.GetWorld(this.worldId);
			if (!world.IsDupeVisited && !world.IsRoverVisted)
			{
				return false;
			}
			if ((this.Season.finishAfterNumEvents != -1 && this.numStartEvents >= this.Season.finishAfterNumEvents) || this.allEventWillNotRunAgain)
			{
				return false;
			}
			float currentTimeInCycles = GameUtil.GetCurrentTimeInCycles();
			return currentTimeInCycles > this.Season.minCycle && currentTimeInCycles < this.Season.maxCycle;
		}

		// Token: 0x04005F33 RID: 24371
		public const int LIMIT_SELECTION = 5;

		// Token: 0x04005F34 RID: 24372
		[Serialize]
		public int numStartEvents;

		// Token: 0x04005F35 RID: 24373
		[Serialize]
		public int worldId;

		// Token: 0x04005F36 RID: 24374
		[Serialize]
		private readonly string seasonId;

		// Token: 0x04005F37 RID: 24375
		[Serialize]
		private float nextPeriodTime;

		// Token: 0x04005F38 RID: 24376
		[Serialize]
		private float randomizedNextTime;

		// Token: 0x04005F39 RID: 24377
		private bool allEventWillNotRunAgain;

		// Token: 0x04005F3A RID: 24378
		private GameplaySeason _season;
	}
}
