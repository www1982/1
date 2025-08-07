using System;
using KSerialization;

// Token: 0x0200065B RID: 1627
[SerializationConfig(MemberSerialization.OptIn)]
public class ScheduleGroupInstance
{
	// Token: 0x170001EB RID: 491
	// (get) Token: 0x060027DE RID: 10206 RVA: 0x000E2180 File Offset: 0x000E0380
	// (set) Token: 0x060027DF RID: 10207 RVA: 0x000E2197 File Offset: 0x000E0397
	public ScheduleGroup scheduleGroup
	{
		get
		{
			return Db.Get().ScheduleGroups.Get(this.scheduleGroupID);
		}
		set
		{
			this.scheduleGroupID = value.Id;
		}
	}

	// Token: 0x060027E0 RID: 10208 RVA: 0x000E21A5 File Offset: 0x000E03A5
	public ScheduleGroupInstance(ScheduleGroup scheduleGroup)
	{
		this.scheduleGroup = scheduleGroup;
		this.segments = scheduleGroup.defaultSegments;
	}

	// Token: 0x0400176E RID: 5998
	[Serialize]
	private string scheduleGroupID;

	// Token: 0x0400176F RID: 5999
	[Serialize]
	public int segments;
}
