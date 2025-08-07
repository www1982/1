using System;
using System.Collections.Generic;
using System.Diagnostics;
using Klei.AI;

// Token: 0x02000654 RID: 1620
[DebuggerDisplay("{IdHash}")]
public class ChoreGroup : Resource
{
	// Token: 0x170001CE RID: 462
	// (get) Token: 0x06002796 RID: 10134 RVA: 0x000E1A28 File Offset: 0x000DFC28
	public int DefaultPersonalPriority
	{
		get
		{
			return this.defaultPersonalPriority;
		}
	}

	// Token: 0x06002797 RID: 10135 RVA: 0x000E1A30 File Offset: 0x000DFC30
	public ChoreGroup(string id, string name, Klei.AI.Attribute attribute, string sprite, int default_personal_priority, bool user_prioritizable = true)
		: base(id, name)
	{
		this.attribute = attribute;
		this.description = Strings.Get("STRINGS.DUPLICANTS.CHOREGROUPS." + id.ToUpper() + ".DESC").String;
		this.sprite = sprite;
		this.defaultPersonalPriority = default_personal_priority;
		this.userPrioritizable = user_prioritizable;
	}

	// Token: 0x0400173A RID: 5946
	public List<ChoreType> choreTypes = new List<ChoreType>();

	// Token: 0x0400173B RID: 5947
	public Klei.AI.Attribute attribute;

	// Token: 0x0400173C RID: 5948
	public string description;

	// Token: 0x0400173D RID: 5949
	public string sprite;

	// Token: 0x0400173E RID: 5950
	private int defaultPersonalPriority;

	// Token: 0x0400173F RID: 5951
	public bool userPrioritizable;
}
