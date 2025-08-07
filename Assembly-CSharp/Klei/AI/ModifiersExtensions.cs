using System;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02001005 RID: 4101
	public static class ModifiersExtensions
	{
		// Token: 0x06007E79 RID: 32377 RVA: 0x00329302 File Offset: 0x00327502
		public static Attributes GetAttributes(this KMonoBehaviour cmp)
		{
			return cmp.gameObject.GetAttributes();
		}

		// Token: 0x06007E7A RID: 32378 RVA: 0x00329310 File Offset: 0x00327510
		public static Attributes GetAttributes(this GameObject go)
		{
			Modifiers component = go.GetComponent<Modifiers>();
			if (component != null)
			{
				return component.attributes;
			}
			return null;
		}

		// Token: 0x06007E7B RID: 32379 RVA: 0x00329335 File Offset: 0x00327535
		public static Amounts GetAmounts(this KMonoBehaviour cmp)
		{
			if (cmp is Modifiers)
			{
				return ((Modifiers)cmp).amounts;
			}
			return cmp.gameObject.GetAmounts();
		}

		// Token: 0x06007E7C RID: 32380 RVA: 0x00329358 File Offset: 0x00327558
		public static Amounts GetAmounts(this GameObject go)
		{
			Modifiers component = go.GetComponent<Modifiers>();
			if (component != null)
			{
				return component.amounts;
			}
			return null;
		}

		// Token: 0x06007E7D RID: 32381 RVA: 0x0032937D File Offset: 0x0032757D
		public static Sicknesses GetSicknesses(this KMonoBehaviour cmp)
		{
			return cmp.gameObject.GetSicknesses();
		}

		// Token: 0x06007E7E RID: 32382 RVA: 0x0032938C File Offset: 0x0032758C
		public static Sicknesses GetSicknesses(this GameObject go)
		{
			Modifiers component = go.GetComponent<Modifiers>();
			if (component != null)
			{
				return component.sicknesses;
			}
			return null;
		}
	}
}
