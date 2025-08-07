using System;
using UnityEngine;

// Token: 0x020006AD RID: 1709
public abstract class AssignableSlotInstance
{
	// Token: 0x170001F8 RID: 504
	// (get) Token: 0x060029D6 RID: 10710 RVA: 0x000F2E82 File Offset: 0x000F1082
	// (set) Token: 0x060029D7 RID: 10711 RVA: 0x000F2E8A File Offset: 0x000F108A
	public Assignables assignables { get; private set; }

	// Token: 0x170001F9 RID: 505
	// (get) Token: 0x060029D8 RID: 10712 RVA: 0x000F2E93 File Offset: 0x000F1093
	public GameObject gameObject
	{
		get
		{
			return this.assignables.gameObject;
		}
	}

	// Token: 0x060029D9 RID: 10713 RVA: 0x000F2EA0 File Offset: 0x000F10A0
	public AssignableSlotInstance(Assignables assignables, AssignableSlot slot)
		: this(slot.Id, assignables, slot)
	{
	}

	// Token: 0x060029DA RID: 10714 RVA: 0x000F2EB0 File Offset: 0x000F10B0
	public AssignableSlotInstance(string id, Assignables assignables, AssignableSlot slot)
	{
		this.ID = id;
		this.slot = slot;
		this.assignables = assignables;
	}

	// Token: 0x060029DB RID: 10715 RVA: 0x000F2ECD File Offset: 0x000F10CD
	public void Assign(Assignable assignable)
	{
		if (this.assignable == assignable)
		{
			return;
		}
		this.Unassign(false);
		this.assignable = assignable;
		this.assignables.Trigger(-1585839766, this);
	}

	// Token: 0x060029DC RID: 10716 RVA: 0x000F2F00 File Offset: 0x000F1100
	public virtual void Unassign(bool trigger_event = true)
	{
		if (this.unassigning)
		{
			return;
		}
		if (this.IsAssigned())
		{
			this.unassigning = true;
			this.assignable.Unassign();
			if (trigger_event)
			{
				this.assignables.Trigger(-1585839766, this);
			}
			this.assignable = null;
			this.unassigning = false;
		}
	}

	// Token: 0x060029DD RID: 10717 RVA: 0x000F2F52 File Offset: 0x000F1152
	public bool IsAssigned()
	{
		return this.assignable != null;
	}

	// Token: 0x060029DE RID: 10718 RVA: 0x000F2F60 File Offset: 0x000F1160
	public bool IsUnassigning()
	{
		return this.unassigning;
	}

	// Token: 0x040018DF RID: 6367
	public string ID;

	// Token: 0x040018E0 RID: 6368
	public AssignableSlot slot;

	// Token: 0x040018E1 RID: 6369
	public Assignable assignable;

	// Token: 0x040018E3 RID: 6371
	private bool unassigning;
}
