using System;
using System.Collections.Generic;

// Token: 0x020005C2 RID: 1474
public class AssignmentGroup : IAssignableIdentity
{
	// Token: 0x17000157 RID: 343
	// (get) Token: 0x060021F1 RID: 8689 RVA: 0x000C3DCD File Offset: 0x000C1FCD
	// (set) Token: 0x060021F2 RID: 8690 RVA: 0x000C3DD5 File Offset: 0x000C1FD5
	public string id { get; private set; }

	// Token: 0x17000158 RID: 344
	// (get) Token: 0x060021F3 RID: 8691 RVA: 0x000C3DDE File Offset: 0x000C1FDE
	// (set) Token: 0x060021F4 RID: 8692 RVA: 0x000C3DE6 File Offset: 0x000C1FE6
	public string name { get; private set; }

	// Token: 0x060021F5 RID: 8693 RVA: 0x000C3DF0 File Offset: 0x000C1FF0
	public AssignmentGroup(string id, IAssignableIdentity[] members, string name)
	{
		this.id = id;
		this.name = name;
		foreach (IAssignableIdentity assignableIdentity in members)
		{
			this.members.Add(assignableIdentity);
		}
		if (Game.Instance != null)
		{
			Game.Instance.assignmentManager.assignment_groups.Add(id, this);
			Game.Instance.Trigger(-1123234494, this);
		}
	}

	// Token: 0x060021F6 RID: 8694 RVA: 0x000C3E7A File Offset: 0x000C207A
	public void AddMember(IAssignableIdentity member)
	{
		if (!this.members.Contains(member))
		{
			this.members.Add(member);
		}
		Game.Instance.Trigger(-1123234494, this);
	}

	// Token: 0x060021F7 RID: 8695 RVA: 0x000C3EA6 File Offset: 0x000C20A6
	public void RemoveMember(IAssignableIdentity member)
	{
		this.members.Remove(member);
		Game.Instance.Trigger(-1123234494, this);
	}

	// Token: 0x060021F8 RID: 8696 RVA: 0x000C3EC5 File Offset: 0x000C20C5
	public string GetProperName()
	{
		return this.name;
	}

	// Token: 0x060021F9 RID: 8697 RVA: 0x000C3ECD File Offset: 0x000C20CD
	public bool HasMember(IAssignableIdentity member)
	{
		return this.members.Contains(member);
	}

	// Token: 0x060021FA RID: 8698 RVA: 0x000C3EDB File Offset: 0x000C20DB
	public bool IsNull()
	{
		return false;
	}

	// Token: 0x060021FB RID: 8699 RVA: 0x000C3EDE File Offset: 0x000C20DE
	public List<IAssignableIdentity> GetMembers()
	{
		return this.members;
	}

	// Token: 0x060021FC RID: 8700 RVA: 0x000C3EE8 File Offset: 0x000C20E8
	public List<Ownables> GetOwners()
	{
		this.current_owners.Clear();
		foreach (IAssignableIdentity assignableIdentity in this.members)
		{
			this.current_owners.AddRange(assignableIdentity.GetOwners());
		}
		return this.current_owners;
	}

	// Token: 0x060021FD RID: 8701 RVA: 0x000C3F58 File Offset: 0x000C2158
	public Ownables GetSoleOwner()
	{
		if (this.members.Count == 1)
		{
			return this.members[0] as Ownables;
		}
		Debug.LogWarningFormat("GetSoleOwner called on AssignmentGroup with {0} members", new object[] { this.members.Count });
		return null;
	}

	// Token: 0x060021FE RID: 8702 RVA: 0x000C3FAC File Offset: 0x000C21AC
	public bool HasOwner(Assignables owner)
	{
		using (List<IAssignableIdentity>.Enumerator enumerator = this.members.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.HasOwner(owner))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060021FF RID: 8703 RVA: 0x000C4008 File Offset: 0x000C2208
	public int NumOwners()
	{
		int num = 0;
		foreach (IAssignableIdentity assignableIdentity in this.members)
		{
			num += assignableIdentity.NumOwners();
		}
		return num;
	}

	// Token: 0x040013D5 RID: 5077
	private List<IAssignableIdentity> members = new List<IAssignableIdentity>();

	// Token: 0x040013D6 RID: 5078
	public List<Ownables> current_owners = new List<Ownables>();
}
