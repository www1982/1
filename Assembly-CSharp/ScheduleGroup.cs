using System;
using System.Collections.Generic;
using System.Diagnostics;
using STRINGS;
using UnityEngine;

// Token: 0x0200065A RID: 1626
[DebuggerDisplay("{Id}")]
public class ScheduleGroup : Resource
{
	// Token: 0x170001E5 RID: 485
	// (get) Token: 0x060027CF RID: 10191 RVA: 0x000E20B4 File Offset: 0x000E02B4
	// (set) Token: 0x060027D0 RID: 10192 RVA: 0x000E20BC File Offset: 0x000E02BC
	public int defaultSegments { get; private set; }

	// Token: 0x170001E6 RID: 486
	// (get) Token: 0x060027D1 RID: 10193 RVA: 0x000E20C5 File Offset: 0x000E02C5
	// (set) Token: 0x060027D2 RID: 10194 RVA: 0x000E20CD File Offset: 0x000E02CD
	public string description { get; private set; }

	// Token: 0x170001E7 RID: 487
	// (get) Token: 0x060027D3 RID: 10195 RVA: 0x000E20D6 File Offset: 0x000E02D6
	// (set) Token: 0x060027D4 RID: 10196 RVA: 0x000E20DE File Offset: 0x000E02DE
	public string notificationTooltip { get; private set; }

	// Token: 0x170001E8 RID: 488
	// (get) Token: 0x060027D5 RID: 10197 RVA: 0x000E20E7 File Offset: 0x000E02E7
	// (set) Token: 0x060027D6 RID: 10198 RVA: 0x000E20EF File Offset: 0x000E02EF
	public List<ScheduleBlockType> allowedTypes { get; private set; }

	// Token: 0x170001E9 RID: 489
	// (get) Token: 0x060027D7 RID: 10199 RVA: 0x000E20F8 File Offset: 0x000E02F8
	// (set) Token: 0x060027D8 RID: 10200 RVA: 0x000E2100 File Offset: 0x000E0300
	public bool alarm { get; private set; }

	// Token: 0x170001EA RID: 490
	// (get) Token: 0x060027D9 RID: 10201 RVA: 0x000E2109 File Offset: 0x000E0309
	// (set) Token: 0x060027DA RID: 10202 RVA: 0x000E2111 File Offset: 0x000E0311
	public Color uiColor { get; private set; }

	// Token: 0x060027DB RID: 10203 RVA: 0x000E211A File Offset: 0x000E031A
	public ScheduleGroup(string id, ResourceSet parent, int defaultSegments, string name, string description, Color uiColor, string notificationTooltip, List<ScheduleBlockType> allowedTypes, bool alarm = false)
		: base(id, parent, name)
	{
		this.defaultSegments = defaultSegments;
		this.description = description;
		this.notificationTooltip = notificationTooltip;
		this.allowedTypes = allowedTypes;
		this.alarm = alarm;
		this.uiColor = uiColor;
	}

	// Token: 0x060027DC RID: 10204 RVA: 0x000E2155 File Offset: 0x000E0355
	public bool Allowed(ScheduleBlockType type)
	{
		return this.allowedTypes.Contains(type);
	}

	// Token: 0x060027DD RID: 10205 RVA: 0x000E2163 File Offset: 0x000E0363
	public string GetTooltip()
	{
		return string.Format(UI.SCHEDULEGROUPS.TOOLTIP_FORMAT, this.Name, this.description);
	}
}
