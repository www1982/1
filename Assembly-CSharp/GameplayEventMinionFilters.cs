using System;
using Database;

// Token: 0x020004BE RID: 1214
public class GameplayEventMinionFilters
{
	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x060019F7 RID: 6647 RVA: 0x0008E892 File Offset: 0x0008CA92
	public static GameplayEventMinionFilters Instance
	{
		get
		{
			if (GameplayEventMinionFilters._instance == null)
			{
				GameplayEventMinionFilters._instance = new GameplayEventMinionFilters();
			}
			return GameplayEventMinionFilters._instance;
		}
	}

	// Token: 0x060019F8 RID: 6648 RVA: 0x0008E8AC File Offset: 0x0008CAAC
	public GameplayEventMinionFilter HasMasteredSkill(Skill skill)
	{
		return new GameplayEventMinionFilter
		{
			filter = (MinionIdentity minion) => minion.GetComponent<MinionResume>().HasMasteredSkill(skill.Id),
			id = "HasMasteredSkill"
		};
	}

	// Token: 0x060019F9 RID: 6649 RVA: 0x0008E8E8 File Offset: 0x0008CAE8
	public GameplayEventMinionFilter HasSkillAptitude(Skill skill)
	{
		return new GameplayEventMinionFilter
		{
			filter = (MinionIdentity minion) => minion.GetComponent<MinionResume>().HasSkillAptitude(skill),
			id = "HasSkillAptitude"
		};
	}

	// Token: 0x060019FA RID: 6650 RVA: 0x0008E924 File Offset: 0x0008CB24
	public GameplayEventMinionFilter HasChoreGroupPriorityOrHigher(ChoreGroup choreGroup, int priority)
	{
		return new GameplayEventMinionFilter
		{
			filter = delegate(MinionIdentity minion)
			{
				ChoreConsumer component = minion.GetComponent<ChoreConsumer>();
				return !component.IsChoreGroupDisabled(choreGroup) && component.GetPersonalPriority(choreGroup) >= priority;
			},
			id = "HasChoreGroupPriorityOrHigher"
		};
	}

	// Token: 0x060019FB RID: 6651 RVA: 0x0008E968 File Offset: 0x0008CB68
	public GameplayEventMinionFilter AgeRange(float min = 0f, float max = float.PositiveInfinity)
	{
		return new GameplayEventMinionFilter
		{
			filter = (MinionIdentity minion) => minion.arrivalTime >= min && minion.arrivalTime <= max,
			id = "AgeRange"
		};
	}

	// Token: 0x060019FC RID: 6652 RVA: 0x0008E9AB File Offset: 0x0008CBAB
	public GameplayEventMinionFilter PriorityIn()
	{
		GameplayEventMinionFilter gameplayEventMinionFilter = new GameplayEventMinionFilter();
		gameplayEventMinionFilter.filter = (MinionIdentity minion) => true;
		gameplayEventMinionFilter.id = "PriorityIn";
		return gameplayEventMinionFilter;
	}

	// Token: 0x060019FD RID: 6653 RVA: 0x0008E9E4 File Offset: 0x0008CBE4
	public GameplayEventMinionFilter Not(GameplayEventMinionFilter filter)
	{
		return new GameplayEventMinionFilter
		{
			filter = (MinionIdentity minion) => !filter.filter(minion),
			id = "Not[" + filter.id + "]"
		};
	}

	// Token: 0x060019FE RID: 6654 RVA: 0x0008EA38 File Offset: 0x0008CC38
	public GameplayEventMinionFilter Or(GameplayEventMinionFilter precondition1, GameplayEventMinionFilter precondition2)
	{
		return new GameplayEventMinionFilter
		{
			filter = (MinionIdentity minion) => precondition1.filter(minion) || precondition2.filter(minion),
			id = string.Concat(new string[] { "[", precondition1.id, "]-OR-[", precondition2.id, "]" })
		};
	}

	// Token: 0x04000EF0 RID: 3824
	private static GameplayEventMinionFilters _instance;
}
