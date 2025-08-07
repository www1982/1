using System;
using System.Collections.Generic;
using Klei.AI;

namespace Database
{
	// Token: 0x02000ECD RID: 3789
	public class FertilityModifiers : ResourceSet<FertilityModifier>
	{
		// Token: 0x06007916 RID: 30998 RVA: 0x002EC234 File Offset: 0x002EA434
		public List<FertilityModifier> GetForTag(Tag searchTag)
		{
			List<FertilityModifier> list = new List<FertilityModifier>();
			foreach (FertilityModifier fertilityModifier in this.resources)
			{
				if (fertilityModifier.TargetTag == searchTag)
				{
					list.Add(fertilityModifier);
				}
			}
			return list;
		}
	}
}
