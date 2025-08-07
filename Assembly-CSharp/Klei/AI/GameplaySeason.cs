using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Klei.AI
{
	// Token: 0x02000FF6 RID: 4086
	[DebuggerDisplay("{base.Id}")]
	public class GameplaySeason : Resource, IHasDlcRestrictions
	{
		// Token: 0x06007E23 RID: 32291 RVA: 0x00328081 File Offset: 0x00326281
		public string[] GetRequiredDlcIds()
		{
			return this.requiredDlcIds;
		}

		// Token: 0x06007E24 RID: 32292 RVA: 0x00328089 File Offset: 0x00326289
		public string[] GetForbiddenDlcIds()
		{
			return this.forbiddenDlcIds;
		}

		// Token: 0x06007E25 RID: 32293 RVA: 0x00328094 File Offset: 0x00326294
		public GameplaySeason(string id, GameplaySeason.Type type, float period, bool synchronizedToPeriod, float randomizedEventStartTime = -1f, bool startActive = false, int finishAfterNumEvents = -1, float minCycle = 0f, float maxCycle = float.PositiveInfinity, int numEventsToStartEachPeriod = 1, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
			: base(id, null, null)
		{
			this.type = type;
			this.requiredDlcIds = requiredDlcIds;
			this.forbiddenDlcIds = forbiddenDlcIds;
			this.period = period;
			this.synchronizedToPeriod = synchronizedToPeriod;
			global::Debug.Assert(period > 0f, "Season " + id + "'s Period cannot be 0 or negative");
			if (randomizedEventStartTime == -1f)
			{
				this.randomizedEventStartTime = new MathUtil.MinMax(-0f * period, 0f * period);
			}
			else
			{
				this.randomizedEventStartTime = new MathUtil.MinMax(-randomizedEventStartTime, randomizedEventStartTime);
				DebugUtil.DevAssert((this.randomizedEventStartTime.max - this.randomizedEventStartTime.min) * 0.4f < period, string.Format("Season {0} randomizedEventStartTime is greater than {1}% of its period.", id, 0.4f), null);
			}
			this.startActive = startActive;
			this.finishAfterNumEvents = finishAfterNumEvents;
			this.minCycle = minCycle;
			this.maxCycle = maxCycle;
			this.events = new List<GameplayEvent>();
			this.numEventsToStartEachPeriod = numEventsToStartEachPeriod;
		}

		// Token: 0x06007E26 RID: 32294 RVA: 0x00328198 File Offset: 0x00326398
		[Obsolete]
		public GameplaySeason(string id, GameplaySeason.Type type, string dlcId, float period, bool synchronizedToPeriod, float randomizedEventStartTime = -1f, bool startActive = false, int finishAfterNumEvents = -1, float minCycle = 0f, float maxCycle = float.PositiveInfinity, int numEventsToStartEachPeriod = 1)
			: this(id, type, period, synchronizedToPeriod, randomizedEventStartTime, startActive, finishAfterNumEvents, minCycle, maxCycle, numEventsToStartEachPeriod, new string[] { dlcId }, null)
		{
		}

		// Token: 0x06007E27 RID: 32295 RVA: 0x003281C8 File Offset: 0x003263C8
		public virtual void AdditionalEventInstanceSetup(StateMachine.Instance generic_smi)
		{
		}

		// Token: 0x06007E28 RID: 32296 RVA: 0x003281CA File Offset: 0x003263CA
		public virtual float GetSeasonPeriod()
		{
			return this.period;
		}

		// Token: 0x06007E29 RID: 32297 RVA: 0x003281D2 File Offset: 0x003263D2
		public GameplaySeason AddEvent(GameplayEvent evt)
		{
			this.events.Add(evt);
			return this;
		}

		// Token: 0x06007E2A RID: 32298 RVA: 0x003281E1 File Offset: 0x003263E1
		public virtual GameplaySeasonInstance Instantiate(int worldId)
		{
			return new GameplaySeasonInstance(this, worldId);
		}

		// Token: 0x04005F22 RID: 24354
		public const float DEFAULT_PERCENTAGE_RANDOMIZED_EVENT_START = 0f;

		// Token: 0x04005F23 RID: 24355
		public const float PERCENTAGE_WARNING = 0.4f;

		// Token: 0x04005F24 RID: 24356
		public const float USE_DEFAULT = -1f;

		// Token: 0x04005F25 RID: 24357
		public const int INFINITE = -1;

		// Token: 0x04005F26 RID: 24358
		public float period;

		// Token: 0x04005F27 RID: 24359
		public bool synchronizedToPeriod;

		// Token: 0x04005F28 RID: 24360
		public MathUtil.MinMax randomizedEventStartTime;

		// Token: 0x04005F29 RID: 24361
		public int finishAfterNumEvents = -1;

		// Token: 0x04005F2A RID: 24362
		public bool startActive;

		// Token: 0x04005F2B RID: 24363
		public int numEventsToStartEachPeriod;

		// Token: 0x04005F2C RID: 24364
		public float minCycle;

		// Token: 0x04005F2D RID: 24365
		public float maxCycle;

		// Token: 0x04005F2E RID: 24366
		public List<GameplayEvent> events;

		// Token: 0x04005F2F RID: 24367
		private string[] requiredDlcIds;

		// Token: 0x04005F30 RID: 24368
		private string[] forbiddenDlcIds;

		// Token: 0x04005F31 RID: 24369
		public GameplaySeason.Type type;

		// Token: 0x04005F32 RID: 24370
		[Obsolete]
		public string dlcId;

		// Token: 0x020025DA RID: 9690
		public enum Type
		{
			// Token: 0x0400A8F8 RID: 43256
			World,
			// Token: 0x0400A8F9 RID: 43257
			Cluster
		}
	}
}
