using System;
using System.Diagnostics;

// Token: 0x02000A4C RID: 2636
[DebuggerDisplay("{slot.Id}")]
public class OwnableSlotInstance : AssignableSlotInstance
{
	// Token: 0x06004C70 RID: 19568 RVA: 0x001BBA65 File Offset: 0x001B9C65
	public OwnableSlotInstance(Assignables assignables, OwnableSlot slot)
		: base(assignables, slot)
	{
	}
}
