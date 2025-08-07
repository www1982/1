using System;
using System.Collections.Generic;

// Token: 0x02000C48 RID: 3144
public class UIStringFormatter
{
	// Token: 0x0400411C RID: 16668
	private List<UIStringFormatter.Entry> entries = new List<UIStringFormatter.Entry>();

	// Token: 0x02001E11 RID: 7697
	private struct Entry
	{
		// Token: 0x04008C79 RID: 35961
		public string format;

		// Token: 0x04008C7A RID: 35962
		public string key;

		// Token: 0x04008C7B RID: 35963
		public string value;

		// Token: 0x04008C7C RID: 35964
		public string result;
	}
}
