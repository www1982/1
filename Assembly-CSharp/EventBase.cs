using System;

// Token: 0x0200090C RID: 2316
public class EventBase : Resource
{
	// Token: 0x0600407E RID: 16510 RVA: 0x00169D73 File Offset: 0x00167F73
	public EventBase(string id)
		: base(id, id)
	{
		this.hash = Hash.SDBMLower(id);
	}

	// Token: 0x0600407F RID: 16511 RVA: 0x00169D89 File Offset: 0x00167F89
	public virtual string GetDescription(EventInstanceBase ev)
	{
		return "";
	}

	// Token: 0x04002839 RID: 10297
	public int hash;
}
