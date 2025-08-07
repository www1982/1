using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;

namespace KMod
{
	// Token: 0x02000F69 RID: 3945
	public class LoadedModData
	{
		// Token: 0x04005AE1 RID: 23265
		public Harmony harmony;

		// Token: 0x04005AE2 RID: 23266
		public Dictionary<Assembly, UserMod2> userMod2Instances;

		// Token: 0x04005AE3 RID: 23267
		public ICollection<Assembly> dlls;

		// Token: 0x04005AE4 RID: 23268
		public ICollection<MethodBase> patched_methods;
	}
}
