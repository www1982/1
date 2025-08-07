using System;

// Token: 0x02000C95 RID: 3221
public class ElementUsage
{
	// Token: 0x0600630B RID: 25355 RVA: 0x00253448 File Offset: 0x00251648
	public ElementUsage(Tag tag, float amount, bool continuous)
		: this(tag, amount, continuous, null)
	{
	}

	// Token: 0x0600630C RID: 25356 RVA: 0x00253454 File Offset: 0x00251654
	public ElementUsage(Tag tag, float amount, bool continuous, Func<Tag, float, bool, string> customFormating)
	{
		this.tag = tag;
		this.amount = amount;
		this.continuous = continuous;
		this.customFormating = customFormating;
	}

	// Token: 0x040042F5 RID: 17141
	public Tag tag;

	// Token: 0x040042F6 RID: 17142
	public float amount;

	// Token: 0x040042F7 RID: 17143
	public bool continuous;

	// Token: 0x040042F8 RID: 17144
	public Func<Tag, float, bool, string> customFormating;
}
