using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020004BA RID: 1210
[DebuggerDisplay("{base.Id}")]
public abstract class GameplayEvent : Resource, IComparable<GameplayEvent>, IHasDlcRestrictions
{
	// Token: 0x1700009E RID: 158
	// (get) Token: 0x060019CE RID: 6606 RVA: 0x0008E1DC File Offset: 0x0008C3DC
	// (set) Token: 0x060019CF RID: 6607 RVA: 0x0008E1E4 File Offset: 0x0008C3E4
	public int importance { get; private set; }

	// Token: 0x060019D0 RID: 6608 RVA: 0x0008E1ED File Offset: 0x0008C3ED
	public string[] GetRequiredDlcIds()
	{
		return this.requiredDlcIds;
	}

	// Token: 0x060019D1 RID: 6609 RVA: 0x0008E1F5 File Offset: 0x0008C3F5
	public string[] GetForbiddenDlcIds()
	{
		return this.forbiddenDlcIds;
	}

	// Token: 0x060019D2 RID: 6610 RVA: 0x0008E200 File Offset: 0x0008C400
	public virtual bool IsAllowed()
	{
		if (this.WillNeverRunAgain())
		{
			return false;
		}
		if (!this.allowMultipleEventInstances && GameplayEventManager.Instance.IsGameplayEventActive(this))
		{
			return false;
		}
		foreach (GameplayEventPrecondition gameplayEventPrecondition in this.preconditions)
		{
			if (gameplayEventPrecondition.required && !gameplayEventPrecondition.condition())
			{
				return false;
			}
		}
		float sleepTimer = GameplayEventManager.Instance.GetSleepTimer(this);
		return GameUtil.GetCurrentTimeInCycles() >= sleepTimer;
	}

	// Token: 0x060019D3 RID: 6611 RVA: 0x0008E2A0 File Offset: 0x0008C4A0
	public void SetSleepTimer(float timeToSleepUntil)
	{
		GameplayEventManager.Instance.SetSleepTimerForEvent(this, timeToSleepUntil);
	}

	// Token: 0x060019D4 RID: 6612 RVA: 0x0008E2AE File Offset: 0x0008C4AE
	public virtual bool WillNeverRunAgain()
	{
		return !Game.IsCorrectDlcActiveForCurrentSave(this) || (this.numTimesAllowed != -1 && GameplayEventManager.Instance.NumberOfPastEvents(this.Id) >= this.numTimesAllowed);
	}

	// Token: 0x060019D5 RID: 6613 RVA: 0x0008E2E5 File Offset: 0x0008C4E5
	public int GetCashedPriority()
	{
		return this.calculatedPriority;
	}

	// Token: 0x060019D6 RID: 6614 RVA: 0x0008E2ED File Offset: 0x0008C4ED
	public virtual int CalculatePriority()
	{
		this.calculatedPriority = this.basePriority + this.CalculateBoost();
		return this.calculatedPriority;
	}

	// Token: 0x060019D7 RID: 6615 RVA: 0x0008E308 File Offset: 0x0008C508
	public int CalculateBoost()
	{
		int num = 0;
		foreach (GameplayEventPrecondition gameplayEventPrecondition in this.preconditions)
		{
			if (!gameplayEventPrecondition.required && gameplayEventPrecondition.condition())
			{
				num += gameplayEventPrecondition.priorityModifier;
			}
		}
		return num;
	}

	// Token: 0x060019D8 RID: 6616 RVA: 0x0008E378 File Offset: 0x0008C578
	public GameplayEvent AddPrecondition(GameplayEventPrecondition precondition)
	{
		precondition.required = true;
		this.preconditions.Add(precondition);
		return this;
	}

	// Token: 0x060019D9 RID: 6617 RVA: 0x0008E38E File Offset: 0x0008C58E
	public GameplayEvent AddPriorityBoost(GameplayEventPrecondition precondition, int priorityBoost)
	{
		precondition.required = false;
		precondition.priorityModifier = priorityBoost;
		this.preconditions.Add(precondition);
		return this;
	}

	// Token: 0x060019DA RID: 6618 RVA: 0x0008E3AB File Offset: 0x0008C5AB
	public GameplayEvent AddMinionFilter(GameplayEventMinionFilter filter)
	{
		this.minionFilters.Add(filter);
		return this;
	}

	// Token: 0x060019DB RID: 6619 RVA: 0x0008E3BA File Offset: 0x0008C5BA
	public GameplayEvent TrySpawnEventOnSuccess(HashedString evt)
	{
		this.successEvents.Add(evt);
		return this;
	}

	// Token: 0x060019DC RID: 6620 RVA: 0x0008E3C9 File Offset: 0x0008C5C9
	public GameplayEvent TrySpawnEventOnFailure(HashedString evt)
	{
		this.failureEvents.Add(evt);
		return this;
	}

	// Token: 0x060019DD RID: 6621 RVA: 0x0008E3D8 File Offset: 0x0008C5D8
	public GameplayEvent SetVisuals(HashedString animFileName)
	{
		this.animFileName = animFileName;
		return this;
	}

	// Token: 0x060019DE RID: 6622 RVA: 0x0008E3E2 File Offset: 0x0008C5E2
	public virtual Sprite GetDisplaySprite()
	{
		return null;
	}

	// Token: 0x060019DF RID: 6623 RVA: 0x0008E3E5 File Offset: 0x0008C5E5
	public virtual string GetDisplayString()
	{
		return null;
	}

	// Token: 0x060019E0 RID: 6624 RVA: 0x0008E3E8 File Offset: 0x0008C5E8
	public MinionIdentity GetRandomFilteredMinion()
	{
		List<MinionIdentity> list = new List<MinionIdentity>(Components.LiveMinionIdentities.Items);
		using (List<GameplayEventMinionFilter>.Enumerator enumerator = this.minionFilters.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				GameplayEventMinionFilter filter = enumerator.Current;
				list.RemoveAll((MinionIdentity x) => !filter.filter(x));
			}
		}
		if (list.Count != 0)
		{
			return list[global::UnityEngine.Random.Range(0, list.Count)];
		}
		return null;
	}

	// Token: 0x060019E1 RID: 6625 RVA: 0x0008E480 File Offset: 0x0008C680
	public MinionIdentity GetRandomMinionPrioritizeFiltered()
	{
		MinionIdentity randomFilteredMinion = this.GetRandomFilteredMinion();
		if (!(randomFilteredMinion == null))
		{
			return randomFilteredMinion;
		}
		return Components.LiveMinionIdentities.Items[global::UnityEngine.Random.Range(0, Components.LiveMinionIdentities.Items.Count)];
	}

	// Token: 0x060019E2 RID: 6626 RVA: 0x0008E4C4 File Offset: 0x0008C6C4
	public int CompareTo(GameplayEvent other)
	{
		return -this.GetCashedPriority().CompareTo(other.GetCashedPriority());
	}

	// Token: 0x060019E3 RID: 6627 RVA: 0x0008E4E8 File Offset: 0x0008C6E8
	public GameplayEvent(string id, int priority, int importance)
		: base(id, null, null)
	{
		this.tags = new List<Tag>();
		this.basePriority = priority;
		this.preconditions = new List<GameplayEventPrecondition>();
		this.minionFilters = new List<GameplayEventMinionFilter>();
		this.successEvents = new List<HashedString>();
		this.failureEvents = new List<HashedString>();
		this.importance = importance;
		this.animFileName = id;
	}

	// Token: 0x060019E4 RID: 6628 RVA: 0x0008E556 File Offset: 0x0008C756
	public GameplayEvent(string id, int priority, int importance, string[] requiredDlcIds, string[] forbiddenDlcIds = null)
		: this(id, priority, importance)
	{
		this.requiredDlcIds = requiredDlcIds;
		this.forbiddenDlcIds = forbiddenDlcIds;
	}

	// Token: 0x060019E5 RID: 6629
	public abstract StateMachine.Instance GetSMI(GameplayEventManager manager, GameplayEventInstance eventInstance);

	// Token: 0x060019E6 RID: 6630 RVA: 0x0008E574 File Offset: 0x0008C774
	public GameplayEventInstance CreateInstance(int worldId)
	{
		GameplayEventInstance gameplayEventInstance = new GameplayEventInstance(this, worldId);
		if (this.tags != null)
		{
			gameplayEventInstance.tags.AddRange(this.tags);
		}
		return gameplayEventInstance;
	}

	// Token: 0x04000ED5 RID: 3797
	public const int INFINITE = -1;

	// Token: 0x04000ED6 RID: 3798
	public int numTimesAllowed = -1;

	// Token: 0x04000ED7 RID: 3799
	public bool allowMultipleEventInstances;

	// Token: 0x04000ED8 RID: 3800
	protected int basePriority;

	// Token: 0x04000ED9 RID: 3801
	protected int calculatedPriority;

	// Token: 0x04000EDB RID: 3803
	public List<GameplayEventPrecondition> preconditions;

	// Token: 0x04000EDC RID: 3804
	public List<GameplayEventMinionFilter> minionFilters;

	// Token: 0x04000EDD RID: 3805
	public List<HashedString> successEvents;

	// Token: 0x04000EDE RID: 3806
	public List<HashedString> failureEvents;

	// Token: 0x04000EDF RID: 3807
	public string title;

	// Token: 0x04000EE0 RID: 3808
	public string description;

	// Token: 0x04000EE1 RID: 3809
	public HashedString animFileName;

	// Token: 0x04000EE2 RID: 3810
	public List<Tag> tags;

	// Token: 0x04000EE3 RID: 3811
	private string[] requiredDlcIds;

	// Token: 0x04000EE4 RID: 3812
	private string[] forbiddenDlcIds;
}
