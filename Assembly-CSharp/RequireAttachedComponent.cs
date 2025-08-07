using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000B93 RID: 2963
[SerializationConfig(MemberSerialization.OptIn)]
public class RequireAttachedComponent : ProcessCondition
{
	// Token: 0x17000677 RID: 1655
	// (get) Token: 0x0600587E RID: 22654 RVA: 0x00200178 File Offset: 0x001FE378
	// (set) Token: 0x0600587F RID: 22655 RVA: 0x00200180 File Offset: 0x001FE380
	public Type RequiredType
	{
		get
		{
			return this.requiredType;
		}
		set
		{
			this.requiredType = value;
			this.typeNameString = this.requiredType.Name;
		}
	}

	// Token: 0x06005880 RID: 22656 RVA: 0x0020019A File Offset: 0x001FE39A
	public RequireAttachedComponent(AttachableBuilding myAttachable, Type required_type, string type_name_string)
	{
		this.myAttachable = myAttachable;
		this.requiredType = required_type;
		this.typeNameString = type_name_string;
	}

	// Token: 0x06005881 RID: 22657 RVA: 0x002001B8 File Offset: 0x001FE3B8
	public override ProcessCondition.Status EvaluateCondition()
	{
		if (this.myAttachable != null)
		{
			using (List<GameObject>.Enumerator enumerator = AttachableBuilding.GetAttachedNetwork(this.myAttachable).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.GetComponent(this.requiredType))
					{
						return ProcessCondition.Status.Ready;
					}
				}
			}
			return ProcessCondition.Status.Failure;
		}
		return ProcessCondition.Status.Failure;
	}

	// Token: 0x06005882 RID: 22658 RVA: 0x00200230 File Offset: 0x001FE430
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		return this.typeNameString;
	}

	// Token: 0x06005883 RID: 22659 RVA: 0x0020023C File Offset: 0x001FE43C
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return string.Format(UI.STARMAP.LAUNCHCHECKLIST.INSTALLED_TOOLTIP, this.typeNameString.ToLower());
		}
		return string.Format(UI.STARMAP.LAUNCHCHECKLIST.MISSING_TOOLTIP, this.typeNameString.ToLower());
	}

	// Token: 0x06005884 RID: 22660 RVA: 0x00200277 File Offset: 0x001FE477
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04003AD2 RID: 15058
	private string typeNameString;

	// Token: 0x04003AD3 RID: 15059
	private Type requiredType;

	// Token: 0x04003AD4 RID: 15060
	private AttachableBuilding myAttachable;
}
