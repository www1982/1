using System;
using System.Collections.Generic;
using KMod;

namespace Klei
{
	// Token: 0x02000FBC RID: 4028
	internal class SaveFileRoot
	{
		// Token: 0x06007C9D RID: 31901 RVA: 0x0031F6B5 File Offset: 0x0031D8B5
		public SaveFileRoot()
		{
			this.streamed = new Dictionary<string, byte[]>();
		}

		// Token: 0x04005E00 RID: 24064
		public int WidthInCells;

		// Token: 0x04005E01 RID: 24065
		public int HeightInCells;

		// Token: 0x04005E02 RID: 24066
		public Dictionary<string, byte[]> streamed;

		// Token: 0x04005E03 RID: 24067
		public string clusterID;

		// Token: 0x04005E04 RID: 24068
		public List<ModInfo> requiredMods;

		// Token: 0x04005E05 RID: 24069
		public List<Label> active_mods;
	}
}
