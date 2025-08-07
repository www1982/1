using System;

namespace KMod
{
	// Token: 0x02000F75 RID: 3957
	[Flags]
	public enum Content : byte
	{
		// Token: 0x04005AF7 RID: 23287
		LayerableFiles = 1,
		// Token: 0x04005AF8 RID: 23288
		Strings = 2,
		// Token: 0x04005AF9 RID: 23289
		DLL = 4,
		// Token: 0x04005AFA RID: 23290
		Translation = 8,
		// Token: 0x04005AFB RID: 23291
		Animation = 16
	}
}
