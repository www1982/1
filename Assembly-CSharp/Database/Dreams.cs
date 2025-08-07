using System;

namespace Database
{
	// Token: 0x02000EEC RID: 3820
	public class Dreams : ResourceSet<Dream>
	{
		// Token: 0x06007979 RID: 31097 RVA: 0x002F9A79 File Offset: 0x002F7C79
		public Dreams(ResourceSet parent)
			: base("Dreams", parent)
		{
			this.CommonDream = new Dream("CommonDream", this, "dream_tear_swirly_kanim", new string[] { "dreamIcon_journal" });
		}

		// Token: 0x040056E8 RID: 22248
		public Dream CommonDream;
	}
}
