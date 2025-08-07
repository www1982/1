using System;

// Token: 0x02000910 RID: 2320
public class Expectation
{
	// Token: 0x1700049B RID: 1179
	// (get) Token: 0x06004094 RID: 16532 RVA: 0x0016A405 File Offset: 0x00168605
	// (set) Token: 0x06004095 RID: 16533 RVA: 0x0016A40D File Offset: 0x0016860D
	public string id { get; protected set; }

	// Token: 0x1700049C RID: 1180
	// (get) Token: 0x06004096 RID: 16534 RVA: 0x0016A416 File Offset: 0x00168616
	// (set) Token: 0x06004097 RID: 16535 RVA: 0x0016A41E File Offset: 0x0016861E
	public string name { get; protected set; }

	// Token: 0x1700049D RID: 1181
	// (get) Token: 0x06004098 RID: 16536 RVA: 0x0016A427 File Offset: 0x00168627
	// (set) Token: 0x06004099 RID: 16537 RVA: 0x0016A42F File Offset: 0x0016862F
	public string description { get; protected set; }

	// Token: 0x1700049E RID: 1182
	// (get) Token: 0x0600409A RID: 16538 RVA: 0x0016A438 File Offset: 0x00168638
	// (set) Token: 0x0600409B RID: 16539 RVA: 0x0016A440 File Offset: 0x00168640
	public Action<MinionResume> OnApply { get; protected set; }

	// Token: 0x1700049F RID: 1183
	// (get) Token: 0x0600409C RID: 16540 RVA: 0x0016A449 File Offset: 0x00168649
	// (set) Token: 0x0600409D RID: 16541 RVA: 0x0016A451 File Offset: 0x00168651
	public Action<MinionResume> OnRemove { get; protected set; }

	// Token: 0x0600409E RID: 16542 RVA: 0x0016A45A File Offset: 0x0016865A
	public Expectation(string id, string name, string description, Action<MinionResume> OnApply, Action<MinionResume> OnRemove)
	{
		this.id = id;
		this.name = name;
		this.description = description;
		this.OnApply = OnApply;
		this.OnRemove = OnRemove;
	}
}
