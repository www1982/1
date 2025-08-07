using System;
using System.Collections.Generic;

// Token: 0x02000AE0 RID: 2784
public class RoleSlotUnlock
{
	// Token: 0x170005B1 RID: 1457
	// (get) Token: 0x06005126 RID: 20774 RVA: 0x001D750F File Offset: 0x001D570F
	// (set) Token: 0x06005127 RID: 20775 RVA: 0x001D7517 File Offset: 0x001D5717
	public string id { get; protected set; }

	// Token: 0x170005B2 RID: 1458
	// (get) Token: 0x06005128 RID: 20776 RVA: 0x001D7520 File Offset: 0x001D5720
	// (set) Token: 0x06005129 RID: 20777 RVA: 0x001D7528 File Offset: 0x001D5728
	public string name { get; protected set; }

	// Token: 0x170005B3 RID: 1459
	// (get) Token: 0x0600512A RID: 20778 RVA: 0x001D7531 File Offset: 0x001D5731
	// (set) Token: 0x0600512B RID: 20779 RVA: 0x001D7539 File Offset: 0x001D5739
	public string description { get; protected set; }

	// Token: 0x170005B4 RID: 1460
	// (get) Token: 0x0600512C RID: 20780 RVA: 0x001D7542 File Offset: 0x001D5742
	// (set) Token: 0x0600512D RID: 20781 RVA: 0x001D754A File Offset: 0x001D574A
	public List<global::Tuple<string, int>> slots { get; protected set; }

	// Token: 0x170005B5 RID: 1461
	// (get) Token: 0x0600512E RID: 20782 RVA: 0x001D7553 File Offset: 0x001D5753
	// (set) Token: 0x0600512F RID: 20783 RVA: 0x001D755B File Offset: 0x001D575B
	public Func<bool> isSatisfied { get; protected set; }

	// Token: 0x06005130 RID: 20784 RVA: 0x001D7564 File Offset: 0x001D5764
	public RoleSlotUnlock(string id, string name, string description, List<global::Tuple<string, int>> slots, Func<bool> isSatisfied)
	{
		this.id = id;
		this.name = name;
		this.description = description;
		this.slots = slots;
		this.isSatisfied = isSatisfied;
	}
}
