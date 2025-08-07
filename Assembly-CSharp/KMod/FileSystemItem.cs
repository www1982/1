using System;

namespace KMod
{
	// Token: 0x02000F70 RID: 3952
	public struct FileSystemItem
	{
		// Token: 0x04005AEB RID: 23275
		public string name;

		// Token: 0x04005AEC RID: 23276
		public FileSystemItem.ItemType type;

		// Token: 0x02002108 RID: 8456
		public enum ItemType
		{
			// Token: 0x0400971C RID: 38684
			Directory,
			// Token: 0x0400971D RID: 38685
			File
		}
	}
}
