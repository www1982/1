using System;
using System.Collections.Generic;
using System.Linq;

namespace Database
{
	// Token: 0x02000F4E RID: 3918
	public class DupesCompleteChoreInExoSuitForCycles : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007B0B RID: 31499 RVA: 0x0030A967 File Offset: 0x00308B67
		public DupesCompleteChoreInExoSuitForCycles(int numCycles)
		{
			this.numCycles = numCycles;
		}

		// Token: 0x06007B0C RID: 31500 RVA: 0x0030A978 File Offset: 0x00308B78
		public override bool Success()
		{
			Dictionary<int, List<int>> dupesCompleteChoresInSuits = SaveGame.Instance.ColonyAchievementTracker.dupesCompleteChoresInSuits;
			Dictionary<int, float> dictionary = new Dictionary<int, float>();
			foreach (MinionIdentity minionIdentity in Components.MinionIdentities.Items)
			{
				KPrefabID component = minionIdentity.GetComponent<KPrefabID>();
				if (!component.HasTag(GameTags.Dead))
				{
					dictionary.Add(component.InstanceID, minionIdentity.arrivalTime);
				}
			}
			int num = 0;
			int num2 = Math.Min(dupesCompleteChoresInSuits.Count, this.numCycles);
			for (int i = GameClock.Instance.GetCycle() - num2; i <= GameClock.Instance.GetCycle(); i++)
			{
				if (dupesCompleteChoresInSuits.ContainsKey(i))
				{
					List<int> list = dictionary.Keys.Except(dupesCompleteChoresInSuits[i]).ToList<int>();
					bool flag = true;
					foreach (int num3 in list)
					{
						if (dictionary[num3] < (float)i)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						num++;
					}
					else if (i != GameClock.Instance.GetCycle())
					{
						num = 0;
					}
					this.currentCycleStreak = num;
					if (num >= this.numCycles)
					{
						this.currentCycleStreak = this.numCycles;
						return true;
					}
				}
				else
				{
					this.currentCycleStreak = Math.Max(this.currentCycleStreak, num);
					num = 0;
				}
			}
			return false;
		}

		// Token: 0x06007B0D RID: 31501 RVA: 0x0030AB08 File Offset: 0x00308D08
		public void Deserialize(IReader reader)
		{
			this.numCycles = reader.ReadInt32();
		}

		// Token: 0x06007B0E RID: 31502 RVA: 0x0030AB18 File Offset: 0x00308D18
		public int GetNumberOfDupesForCycle(int cycle)
		{
			int num = 0;
			Dictionary<int, List<int>> dupesCompleteChoresInSuits = SaveGame.Instance.ColonyAchievementTracker.dupesCompleteChoresInSuits;
			if (dupesCompleteChoresInSuits.ContainsKey(GameClock.Instance.GetCycle()))
			{
				num = dupesCompleteChoresInSuits[GameClock.Instance.GetCycle()].Count;
			}
			return num;
		}

		// Token: 0x040059D6 RID: 22998
		public int currentCycleStreak;

		// Token: 0x040059D7 RID: 22999
		public int numCycles;
	}
}
