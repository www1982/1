using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;

namespace KMod
{
	// Token: 0x02000F68 RID: 3944
	public class UserMod2
	{
		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06007B64 RID: 31588 RVA: 0x00310E8C File Offset: 0x0030F08C
		// (set) Token: 0x06007B65 RID: 31589 RVA: 0x00310E94 File Offset: 0x0030F094
		public Assembly assembly { get; set; }

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06007B66 RID: 31590 RVA: 0x00310E9D File Offset: 0x0030F09D
		// (set) Token: 0x06007B67 RID: 31591 RVA: 0x00310EA5 File Offset: 0x0030F0A5
		public string path { get; set; }

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06007B68 RID: 31592 RVA: 0x00310EAE File Offset: 0x0030F0AE
		// (set) Token: 0x06007B69 RID: 31593 RVA: 0x00310EB6 File Offset: 0x0030F0B6
		public Mod mod { get; set; }

		// Token: 0x06007B6A RID: 31594 RVA: 0x00310EBF File Offset: 0x0030F0BF
		public virtual void OnLoad(Harmony harmony)
		{
			harmony.PatchAll(this.assembly);
		}

		// Token: 0x06007B6B RID: 31595 RVA: 0x00310ECD File Offset: 0x0030F0CD
		public virtual void OnAllModsLoaded(Harmony harmony, IReadOnlyList<Mod> mods)
		{
		}
	}
}
