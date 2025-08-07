using System;
using KSerialization;

// Token: 0x02000A4D RID: 2637
[SerializationConfig(MemberSerialization.OptIn)]
public class Ownables : Assignables
{
	// Token: 0x06004C71 RID: 19569 RVA: 0x001BBA6F File Offset: 0x001B9C6F
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x06004C72 RID: 19570 RVA: 0x001BBA78 File Offset: 0x001B9C78
	public void UnassignAll()
	{
		foreach (AssignableSlotInstance assignableSlotInstance in this.slots)
		{
			if (assignableSlotInstance.assignable != null)
			{
				assignableSlotInstance.assignable.Unassign();
			}
		}
	}
}
