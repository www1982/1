using System;
using System.Collections.Generic;

namespace Klei
{
	// Token: 0x02000FBE RID: 4030
	public class WorldGenSave
	{
		// Token: 0x06007C9F RID: 31903 RVA: 0x0031F733 File Offset: 0x0031D933
		public WorldGenSave()
		{
			this.data = new Data();
		}

		// Token: 0x04005E13 RID: 24083
		public Vector2I version;

		// Token: 0x04005E14 RID: 24084
		public Data data;

		// Token: 0x04005E15 RID: 24085
		public string worldID;

		// Token: 0x04005E16 RID: 24086
		public List<string> traitIDs;

		// Token: 0x04005E17 RID: 24087
		public List<string> storyTraitIDs;
	}
}
