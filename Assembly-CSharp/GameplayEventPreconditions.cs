using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using Klei.AI;
using Klei.CustomSettings;
using ProcGen;

// Token: 0x020004C0 RID: 1216
public class GameplayEventPreconditions
{
	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x06001A01 RID: 6657 RVA: 0x0008EAC7 File Offset: 0x0008CCC7
	public static GameplayEventPreconditions Instance
	{
		get
		{
			if (GameplayEventPreconditions._instance == null)
			{
				GameplayEventPreconditions._instance = new GameplayEventPreconditions();
			}
			return GameplayEventPreconditions._instance;
		}
	}

	// Token: 0x06001A02 RID: 6658 RVA: 0x0008EAE0 File Offset: 0x0008CCE0
	public GameplayEventPrecondition LiveMinions(int count = 1)
	{
		return new GameplayEventPrecondition
		{
			condition = () => Components.LiveMinionIdentities.Count >= count,
			description = string.Format("At least {0} dupes alive", count)
		};
	}

	// Token: 0x06001A03 RID: 6659 RVA: 0x0008EB2C File Offset: 0x0008CD2C
	public GameplayEventPrecondition BuildingExists(string buildingId, int count = 1)
	{
		return new GameplayEventPrecondition
		{
			condition = () => BuildingInventory.Instance.BuildingCount(new Tag(buildingId)) >= count,
			description = string.Format("{0} {1} has been built", count, buildingId)
		};
	}

	// Token: 0x06001A04 RID: 6660 RVA: 0x0008EB88 File Offset: 0x0008CD88
	public GameplayEventPrecondition ResearchCompleted(string techName)
	{
		return new GameplayEventPrecondition
		{
			condition = () => Research.Instance.Get(Db.Get().Techs.Get(techName)).IsComplete(),
			description = "Has researched " + techName + "."
		};
	}

	// Token: 0x06001A05 RID: 6661 RVA: 0x0008EBD4 File Offset: 0x0008CDD4
	public GameplayEventPrecondition AchievementUnlocked(ColonyAchievement achievement)
	{
		return new GameplayEventPrecondition
		{
			condition = () => SaveGame.Instance.ColonyAchievementTracker.IsAchievementUnlocked(achievement),
			description = "Unlocked the " + achievement.Id + " achievement"
		};
	}

	// Token: 0x06001A06 RID: 6662 RVA: 0x0008EC28 File Offset: 0x0008CE28
	public GameplayEventPrecondition RoomBuilt(RoomType roomType)
	{
		Predicate<global::Room> <>9__1;
		return new GameplayEventPrecondition
		{
			condition = delegate
			{
				List<global::Room> rooms = Game.Instance.roomProber.rooms;
				Predicate<global::Room> predicate;
				if ((predicate = <>9__1) == null)
				{
					predicate = (<>9__1 = (global::Room match) => match.roomType == roomType);
				}
				return rooms.Exists(predicate);
			},
			description = "Built a " + roomType.Id + " room"
		};
	}

	// Token: 0x06001A07 RID: 6663 RVA: 0x0008EC7C File Offset: 0x0008CE7C
	public GameplayEventPrecondition CycleRestriction(float min = 0f, float max = float.PositiveInfinity)
	{
		return new GameplayEventPrecondition
		{
			condition = () => GameUtil.GetCurrentTimeInCycles() >= min && GameUtil.GetCurrentTimeInCycles() <= max,
			description = string.Format("After cycle {0} and before cycle {1}", min, max)
		};
	}

	// Token: 0x06001A08 RID: 6664 RVA: 0x0008ECDC File Offset: 0x0008CEDC
	public GameplayEventPrecondition MinionsWithEffect(string effectId, int count = 1)
	{
		Func<MinionIdentity, bool> <>9__1;
		return new GameplayEventPrecondition
		{
			condition = delegate
			{
				IEnumerable<MinionIdentity> items = Components.LiveMinionIdentities.Items;
				Func<MinionIdentity, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (MinionIdentity minion) => minion.GetComponent<Effects>().Get(effectId) != null);
				}
				return items.Count(func) >= count;
			},
			description = string.Format("At least {0} dupes have the {1} effect applied", count, effectId)
		};
	}

	// Token: 0x06001A09 RID: 6665 RVA: 0x0008ED38 File Offset: 0x0008CF38
	public GameplayEventPrecondition MinionsWithStatusItem(StatusItem statusItem, int count = 1)
	{
		Func<MinionIdentity, bool> <>9__1;
		return new GameplayEventPrecondition
		{
			condition = delegate
			{
				IEnumerable<MinionIdentity> items = Components.LiveMinionIdentities.Items;
				Func<MinionIdentity, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (MinionIdentity minion) => minion.GetComponent<KSelectable>().HasStatusItem(statusItem));
				}
				return items.Count(func) >= count;
			},
			description = string.Format("At least {0} dupes have the {1} status item", count, statusItem)
		};
	}

	// Token: 0x06001A0A RID: 6666 RVA: 0x0008ED94 File Offset: 0x0008CF94
	public GameplayEventPrecondition MinionsWithChoreGroupPriorityOrGreater(ChoreGroup choreGroup, int count, int priority)
	{
		Func<MinionIdentity, bool> <>9__1;
		return new GameplayEventPrecondition
		{
			condition = delegate
			{
				IEnumerable<MinionIdentity> items = Components.LiveMinionIdentities.Items;
				Func<MinionIdentity, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = delegate(MinionIdentity minion)
					{
						ChoreConsumer component = minion.GetComponent<ChoreConsumer>();
						return !component.IsChoreGroupDisabled(choreGroup) && component.GetPersonalPriority(choreGroup) >= priority;
					});
				}
				return items.Count(func) >= count;
			},
			description = string.Format("At least {0} dupes have their {1} set to {2} or higher.", count, choreGroup.Name, priority)
		};
	}

	// Token: 0x06001A0B RID: 6667 RVA: 0x0008EE04 File Offset: 0x0008D004
	public GameplayEventPrecondition PastEventCount(string evtId, int count = 1)
	{
		return new GameplayEventPrecondition
		{
			condition = () => GameplayEventManager.Instance.NumberOfPastEvents(evtId) >= count,
			description = string.Format("The {0} event has triggered {1} times.", evtId, count)
		};
	}

	// Token: 0x06001A0C RID: 6668 RVA: 0x0008EE60 File Offset: 0x0008D060
	public GameplayEventPrecondition DifficultySetting(SettingConfig config, string levelId)
	{
		return new GameplayEventPrecondition
		{
			condition = () => CustomGameSettings.Instance.GetCurrentQualitySetting(config).id == levelId,
			description = string.Concat(new string[] { "The config ", config.id, " is level ", levelId, "." })
		};
	}

	// Token: 0x06001A0D RID: 6669 RVA: 0x0008EEDC File Offset: 0x0008D0DC
	public GameplayEventPrecondition ClusterHasTag(string tag)
	{
		return new GameplayEventPrecondition
		{
			condition = delegate
			{
				ClusterLayout currentClusterLayout = CustomGameSettings.Instance.GetCurrentClusterLayout();
				return currentClusterLayout != null && currentClusterLayout.clusterTags.Contains(tag);
			},
			description = "The cluster is tagged with " + tag + "."
		};
	}

	// Token: 0x06001A0E RID: 6670 RVA: 0x0008EF28 File Offset: 0x0008D128
	public GameplayEventPrecondition PastEventCountAndNotActive(GameplayEvent evt, int count = 1)
	{
		return new GameplayEventPrecondition
		{
			condition = () => GameplayEventManager.Instance.NumberOfPastEvents(evt.IdHash) >= count && !GameplayEventManager.Instance.IsGameplayEventActive(evt),
			description = string.Format("The {0} event has triggered {1} times and is not active.", evt.Id, count)
		};
	}

	// Token: 0x06001A0F RID: 6671 RVA: 0x0008EF88 File Offset: 0x0008D188
	public GameplayEventPrecondition Not(GameplayEventPrecondition precondition)
	{
		return new GameplayEventPrecondition
		{
			condition = () => !precondition.condition(),
			description = "Not[" + precondition.description + "]"
		};
	}

	// Token: 0x06001A10 RID: 6672 RVA: 0x0008EFDC File Offset: 0x0008D1DC
	public GameplayEventPrecondition Or(GameplayEventPrecondition precondition1, GameplayEventPrecondition precondition2)
	{
		return new GameplayEventPrecondition
		{
			condition = () => precondition1.condition() || precondition2.condition(),
			description = string.Concat(new string[] { "[", precondition1.description, "]-OR-[", precondition2.description, "]" })
		};
	}

	// Token: 0x04000EF5 RID: 3829
	private static GameplayEventPreconditions _instance;
}
