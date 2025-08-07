using System;
using System.Collections.Generic;
using Klei.AI;

namespace Database
{
	// Token: 0x02000F0B RID: 3851
	public class Sicknesses : ResourceSet<Sickness>
	{
		// Token: 0x060079E0 RID: 31200 RVA: 0x003023D8 File Offset: 0x003005D8
		public Sicknesses(ResourceSet parent)
			: base("Sicknesses", parent)
		{
			this.FoodSickness = base.Add(new FoodSickness());
			this.SlimeSickness = base.Add(new SlimeSickness());
			this.ZombieSickness = base.Add(new ZombieSickness());
			if (DlcManager.FeatureRadiationEnabled())
			{
				this.RadiationSickness = base.Add(new RadiationSickness());
			}
			this.Allergies = base.Add(new Allergies());
			this.Sunburn = base.Add(new Sunburn());
		}

		// Token: 0x060079E1 RID: 31201 RVA: 0x00302460 File Offset: 0x00300660
		public static bool IsValidID(string id)
		{
			bool flag = false;
			using (List<Sickness>.Enumerator enumerator = Db.Get().Sicknesses.resources.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Id == id)
					{
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x040058E4 RID: 22756
		public Sickness FoodSickness;

		// Token: 0x040058E5 RID: 22757
		public Sickness SlimeSickness;

		// Token: 0x040058E6 RID: 22758
		public Sickness ZombieSickness;

		// Token: 0x040058E7 RID: 22759
		public Sickness Allergies;

		// Token: 0x040058E8 RID: 22760
		public Sickness RadiationSickness;

		// Token: 0x040058E9 RID: 22761
		public Sickness Sunburn;
	}
}
