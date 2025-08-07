using System;

// Token: 0x02000E4E RID: 3662
public class HatListable : IListableOption
{
	// Token: 0x06007479 RID: 29817 RVA: 0x002C5BB8 File Offset: 0x002C3DB8
	public HatListable(string name, string hat)
	{
		this.name = name;
		this.hat = hat;
	}

	// Token: 0x17000806 RID: 2054
	// (get) Token: 0x0600747A RID: 29818 RVA: 0x002C5BCE File Offset: 0x002C3DCE
	// (set) Token: 0x0600747B RID: 29819 RVA: 0x002C5BD6 File Offset: 0x002C3DD6
	public string name { get; private set; }

	// Token: 0x17000807 RID: 2055
	// (get) Token: 0x0600747C RID: 29820 RVA: 0x002C5BDF File Offset: 0x002C3DDF
	// (set) Token: 0x0600747D RID: 29821 RVA: 0x002C5BE7 File Offset: 0x002C3DE7
	public string hat { get; private set; }

	// Token: 0x0600747E RID: 29822 RVA: 0x002C5BF0 File Offset: 0x002C3DF0
	public string GetProperName()
	{
		return this.name;
	}
}
