using System;

// Token: 0x020008EA RID: 2282
public class EntityConfigOrder : Attribute
{
	// Token: 0x06003FC8 RID: 16328 RVA: 0x001663E2 File Offset: 0x001645E2
	public EntityConfigOrder(int sort_order)
	{
		this.sortOrder = sort_order;
	}

	// Token: 0x0400279E RID: 10142
	public int sortOrder;
}
