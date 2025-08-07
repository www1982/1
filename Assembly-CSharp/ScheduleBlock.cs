using System;
using System.Collections.Generic;
using KSerialization;

// Token: 0x02000AF7 RID: 2807
[Serializable]
public class ScheduleBlock
{
	// Token: 0x170005CA RID: 1482
	// (get) Token: 0x06005278 RID: 21112 RVA: 0x001E0D9F File Offset: 0x001DEF9F
	public List<ScheduleBlockType> allowed_types
	{
		get
		{
			Debug.Assert(!string.IsNullOrEmpty(this._groupId));
			return Db.Get().ScheduleGroups.Get(this._groupId).allowedTypes;
		}
	}

	// Token: 0x170005CB RID: 1483
	// (get) Token: 0x0600527A RID: 21114 RVA: 0x001E0DD7 File Offset: 0x001DEFD7
	// (set) Token: 0x06005279 RID: 21113 RVA: 0x001E0DCE File Offset: 0x001DEFCE
	public string GroupId
	{
		get
		{
			return this._groupId;
		}
		set
		{
			this._groupId = value;
		}
	}

	// Token: 0x0600527B RID: 21115 RVA: 0x001E0DDF File Offset: 0x001DEFDF
	public ScheduleBlock(string name, string groupId)
	{
		this.name = name;
		this._groupId = groupId;
	}

	// Token: 0x0600527C RID: 21116 RVA: 0x001E0DF8 File Offset: 0x001DEFF8
	public bool IsAllowed(ScheduleBlockType type)
	{
		if (this.allowed_types != null)
		{
			foreach (ScheduleBlockType scheduleBlockType in this.allowed_types)
			{
				if (type.IdHash == scheduleBlockType.IdHash)
				{
					return true;
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x04003779 RID: 14201
	[Serialize]
	public string name;

	// Token: 0x0400377A RID: 14202
	[Serialize]
	private string _groupId;
}
