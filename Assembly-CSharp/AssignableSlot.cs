using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020006AC RID: 1708
[DebuggerDisplay("{Id}")]
[Serializable]
public class AssignableSlot : Resource
{
	// Token: 0x060029D4 RID: 10708 RVA: 0x000F2E41 File Offset: 0x000F1041
	public AssignableSlot(string id, string name, bool showInUI = true)
		: base(id, name)
	{
		this.showInUI = showInUI;
	}

	// Token: 0x060029D5 RID: 10709 RVA: 0x000F2E5C File Offset: 0x000F105C
	public AssignableSlotInstance Lookup(GameObject go)
	{
		Assignables component = go.GetComponent<Assignables>();
		if (component != null)
		{
			return component.GetSlot(this);
		}
		return null;
	}

	// Token: 0x040018DE RID: 6366
	public bool showInUI = true;
}
