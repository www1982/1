using System;

namespace Database
{
	// Token: 0x02000F0A RID: 3850
	public class Shirts : ResourceSet<Shirt>
	{
		// Token: 0x060079DF RID: 31199 RVA: 0x003023A4 File Offset: 0x003005A4
		public Shirts()
		{
			this.Hot00 = base.Add(new Shirt("body_shirt_hot_shearling"));
			this.Decor00 = base.Add(new Shirt("body_shirt_decor01"));
		}

		// Token: 0x040058E2 RID: 22754
		public Shirt Hot00;

		// Token: 0x040058E3 RID: 22755
		public Shirt Decor00;
	}
}
