using System;

// Token: 0x02000D39 RID: 3385
public interface IConsumableUIItem
{
	// Token: 0x17000770 RID: 1904
	// (get) Token: 0x0600687D RID: 26749
	string ConsumableId { get; }

	// Token: 0x17000771 RID: 1905
	// (get) Token: 0x0600687E RID: 26750
	string ConsumableName { get; }

	// Token: 0x17000772 RID: 1906
	// (get) Token: 0x0600687F RID: 26751
	int MajorOrder { get; }

	// Token: 0x17000773 RID: 1907
	// (get) Token: 0x06006880 RID: 26752
	int MinorOrder { get; }

	// Token: 0x17000774 RID: 1908
	// (get) Token: 0x06006881 RID: 26753
	bool Display { get; }

	// Token: 0x06006882 RID: 26754 RVA: 0x00276E18 File Offset: 0x00275018
	string OverrideSpriteName()
	{
		return null;
	}

	// Token: 0x06006883 RID: 26755 RVA: 0x00276E1B File Offset: 0x0027501B
	bool RevealTest()
	{
		return ConsumerManager.instance.isDiscovered(this.ConsumableId.ToTag());
	}
}
