using System;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000FD1 RID: 4049
	public class Amounts : Modifications<Amount, AmountInstance>
	{
		// Token: 0x06007D24 RID: 32036 RVA: 0x003217CE File Offset: 0x0031F9CE
		public Amounts(GameObject go)
			: base(go, null)
		{
		}

		// Token: 0x06007D25 RID: 32037 RVA: 0x003217D8 File Offset: 0x0031F9D8
		public float GetValue(string amount_id)
		{
			return base.Get(amount_id).value;
		}

		// Token: 0x06007D26 RID: 32038 RVA: 0x003217E6 File Offset: 0x0031F9E6
		public void SetValue(string amount_id, float value)
		{
			base.Get(amount_id).value = value;
		}

		// Token: 0x06007D27 RID: 32039 RVA: 0x003217F5 File Offset: 0x0031F9F5
		public override AmountInstance Add(AmountInstance instance)
		{
			instance.Activate();
			return base.Add(instance);
		}

		// Token: 0x06007D28 RID: 32040 RVA: 0x00321804 File Offset: 0x0031FA04
		public override void Remove(AmountInstance instance)
		{
			instance.Deactivate();
			base.Remove(instance);
		}

		// Token: 0x06007D29 RID: 32041 RVA: 0x00321814 File Offset: 0x0031FA14
		public void Cleanup()
		{
			for (int i = 0; i < base.Count; i++)
			{
				base[i].Deactivate();
			}
		}
	}
}
