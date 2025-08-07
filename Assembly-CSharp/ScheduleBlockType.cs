using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000659 RID: 1625
[DebuggerDisplay("{Id}")]
public class ScheduleBlockType : Resource
{
	// Token: 0x170001E3 RID: 483
	// (get) Token: 0x060027CA RID: 10186 RVA: 0x000E2077 File Offset: 0x000E0277
	// (set) Token: 0x060027CB RID: 10187 RVA: 0x000E207F File Offset: 0x000E027F
	public Color color { get; private set; }

	// Token: 0x170001E4 RID: 484
	// (get) Token: 0x060027CC RID: 10188 RVA: 0x000E2088 File Offset: 0x000E0288
	// (set) Token: 0x060027CD RID: 10189 RVA: 0x000E2090 File Offset: 0x000E0290
	public string description { get; private set; }

	// Token: 0x060027CE RID: 10190 RVA: 0x000E2099 File Offset: 0x000E0299
	public ScheduleBlockType(string id, ResourceSet parent, string name, string description, Color color)
		: base(id, parent, name)
	{
		this.color = color;
		this.description = description;
	}
}
