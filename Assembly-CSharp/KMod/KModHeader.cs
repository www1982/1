using System;

namespace KMod
{
	// Token: 0x02000F6C RID: 3948
	public class KModHeader
	{
		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06007B71 RID: 31601 RVA: 0x0031132C File Offset: 0x0030F52C
		// (set) Token: 0x06007B72 RID: 31602 RVA: 0x00311334 File Offset: 0x0030F534
		public string staticID { get; set; }

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06007B73 RID: 31603 RVA: 0x0031133D File Offset: 0x0030F53D
		// (set) Token: 0x06007B74 RID: 31604 RVA: 0x00311345 File Offset: 0x0030F545
		public string title { get; set; }

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06007B75 RID: 31605 RVA: 0x0031134E File Offset: 0x0030F54E
		// (set) Token: 0x06007B76 RID: 31606 RVA: 0x00311356 File Offset: 0x0030F556
		public string description { get; set; }
	}
}
