using System;
using KSerialization;

namespace ProcGenGame
{
	// Token: 0x02000E98 RID: 3736
	[SerializationConfig(MemberSerialization.OptOut)]
	public struct Neighbors
	{
		// Token: 0x0600771B RID: 30491 RVA: 0x002DD006 File Offset: 0x002DB206
		public Neighbors(TerrainCell a, TerrainCell b)
		{
			Debug.Assert(a != null && b != null, "NULL Neighbor");
			this.n0 = a;
			this.n1 = b;
		}

		// Token: 0x040052D9 RID: 21209
		public TerrainCell n0;

		// Token: 0x040052DA RID: 21210
		public TerrainCell n1;
	}
}
