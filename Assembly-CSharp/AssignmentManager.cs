using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020006AF RID: 1711
[AddComponentMenu("KMonoBehaviour/scripts/AssignmentManager")]
public class AssignmentManager : KMonoBehaviour
{
	// Token: 0x060029EB RID: 10731 RVA: 0x000F3298 File Offset: 0x000F1498
	public IEnumerator<Assignable> GetEnumerator()
	{
		return this.assignables.GetEnumerator();
	}

	// Token: 0x060029EC RID: 10732 RVA: 0x000F32AA File Offset: 0x000F14AA
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Game.Instance.Subscribe<AssignmentManager>(586301400, AssignmentManager.MinionMigrationDelegate);
	}

	// Token: 0x060029ED RID: 10733 RVA: 0x000F32C8 File Offset: 0x000F14C8
	protected void MinionMigration(object data)
	{
		MinionMigrationEventArgs minionMigrationEventArgs = data as MinionMigrationEventArgs;
		foreach (Assignable assignable in this.assignables)
		{
			if (assignable.assignee != null)
			{
				Ownables soleOwner = assignable.assignee.GetSoleOwner();
				if (soleOwner != null && soleOwner.GetComponent<MinionAssignablesProxy>() != null && assignable.assignee.GetSoleOwner().GetComponent<MinionAssignablesProxy>().GetTargetGameObject() == minionMigrationEventArgs.minionId.gameObject)
				{
					assignable.Unassign();
				}
			}
		}
	}

	// Token: 0x060029EE RID: 10734 RVA: 0x000F3374 File Offset: 0x000F1574
	public void Add(Assignable assignable)
	{
		this.assignables.Add(assignable);
	}

	// Token: 0x060029EF RID: 10735 RVA: 0x000F3382 File Offset: 0x000F1582
	public void Remove(Assignable assignable)
	{
		this.assignables.Remove(assignable);
	}

	// Token: 0x060029F0 RID: 10736 RVA: 0x000F3391 File Offset: 0x000F1591
	public AssignmentGroup TryCreateAssignmentGroup(string id, IAssignableIdentity[] members, string name)
	{
		if (this.assignment_groups.ContainsKey(id))
		{
			return this.assignment_groups[id];
		}
		return new AssignmentGroup(id, members, name);
	}

	// Token: 0x060029F1 RID: 10737 RVA: 0x000F33B6 File Offset: 0x000F15B6
	public void RemoveAssignmentGroup(string id)
	{
		if (!this.assignment_groups.ContainsKey(id))
		{
			global::Debug.LogError("Assignment group with id " + id + " doesn't exists");
			return;
		}
		this.assignment_groups.Remove(id);
	}

	// Token: 0x060029F2 RID: 10738 RVA: 0x000F33E9 File Offset: 0x000F15E9
	public void AddToAssignmentGroup(string group_id, IAssignableIdentity member)
	{
		global::Debug.Assert(this.assignment_groups.ContainsKey(group_id));
		this.assignment_groups[group_id].AddMember(member);
	}

	// Token: 0x060029F3 RID: 10739 RVA: 0x000F340E File Offset: 0x000F160E
	public void RemoveFromAssignmentGroup(string group_id, IAssignableIdentity member)
	{
		global::Debug.Assert(this.assignment_groups.ContainsKey(group_id));
		this.assignment_groups[group_id].RemoveMember(member);
	}

	// Token: 0x060029F4 RID: 10740 RVA: 0x000F3434 File Offset: 0x000F1634
	public void RemoveFromAllGroups(IAssignableIdentity member)
	{
		foreach (Assignable assignable in this.assignables)
		{
			if (assignable.assignee == member)
			{
				assignable.Unassign();
			}
		}
		foreach (KeyValuePair<string, AssignmentGroup> keyValuePair in this.assignment_groups)
		{
			if (keyValuePair.Value.HasMember(member))
			{
				keyValuePair.Value.RemoveMember(member);
			}
		}
	}

	// Token: 0x060029F5 RID: 10741 RVA: 0x000F34E8 File Offset: 0x000F16E8
	public void RemoveFromWorld(IAssignableIdentity minionIdentity, int world_id)
	{
		foreach (Assignable assignable in this.assignables)
		{
			if (assignable.assignee != null && assignable.assignee.GetOwners().Count == 1)
			{
				Ownables soleOwner = assignable.assignee.GetSoleOwner();
				if (soleOwner != null && soleOwner.GetComponent<MinionAssignablesProxy>() != null && assignable.assignee == minionIdentity && assignable.GetMyWorldId() == world_id)
				{
					assignable.Unassign();
				}
			}
		}
	}

	// Token: 0x060029F6 RID: 10742 RVA: 0x000F358C File Offset: 0x000F178C
	public List<Assignable> GetPreferredAssignables(Assignables owner, AssignableSlot slot)
	{
		List<Assignable> preferredAssignableResults = this.PreferredAssignableResults;
		List<Assignable> preferredAssignableResults2;
		lock (preferredAssignableResults)
		{
			this.PreferredAssignableResults.Clear();
			int num = int.MaxValue;
			foreach (Assignable assignable in this.assignables)
			{
				if (assignable.slot == slot && assignable.assignee != null && assignable.assignee.HasOwner(owner))
				{
					Room room = assignable.assignee as Room;
					if (room != null && room.roomType.priority_building_use)
					{
						this.PreferredAssignableResults.Clear();
						this.PreferredAssignableResults.Add(assignable);
						return this.PreferredAssignableResults;
					}
					int num2 = assignable.assignee.NumOwners();
					if (num2 == num)
					{
						this.PreferredAssignableResults.Add(assignable);
					}
					else if (num2 < num)
					{
						num = num2;
						this.PreferredAssignableResults.Clear();
						this.PreferredAssignableResults.Add(assignable);
					}
				}
			}
			preferredAssignableResults2 = this.PreferredAssignableResults;
		}
		return preferredAssignableResults2;
	}

	// Token: 0x060029F7 RID: 10743 RVA: 0x000F36EC File Offset: 0x000F18EC
	public bool IsPreferredAssignable(Assignables owner, Assignable candidate)
	{
		IAssignableIdentity assignee = candidate.assignee;
		if (assignee == null || !assignee.HasOwner(owner))
		{
			return false;
		}
		int num = assignee.NumOwners();
		Room room = assignee as Room;
		if (room != null && room.roomType.priority_building_use)
		{
			return true;
		}
		foreach (Assignable assignable in this.assignables)
		{
			if (assignable.slot == candidate.slot && assignable.assignee != assignee)
			{
				Room room2 = assignable.assignee as Room;
				if (room2 != null && room2.roomType.priority_building_use && assignable.assignee.HasOwner(owner))
				{
					return false;
				}
				if (assignable.assignee.NumOwners() < num && assignable.assignee.HasOwner(owner))
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x040018E6 RID: 6374
	private List<Assignable> assignables = new List<Assignable>();

	// Token: 0x040018E7 RID: 6375
	public const string PUBLIC_GROUP_ID = "public";

	// Token: 0x040018E8 RID: 6376
	public Dictionary<string, AssignmentGroup> assignment_groups = new Dictionary<string, AssignmentGroup> { 
	{
		"public",
		new AssignmentGroup("public", new IAssignableIdentity[0], UI.UISIDESCREENS.ASSIGNABLESIDESCREEN.PUBLIC)
	} };

	// Token: 0x040018E9 RID: 6377
	private static readonly EventSystem.IntraObjectHandler<AssignmentManager> MinionMigrationDelegate = new EventSystem.IntraObjectHandler<AssignmentManager>(delegate(AssignmentManager component, object data)
	{
		component.MinionMigration(data);
	});

	// Token: 0x040018EA RID: 6378
	private List<Assignable> PreferredAssignableResults = new List<Assignable>();
}
