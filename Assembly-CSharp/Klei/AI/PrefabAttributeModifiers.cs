using System;
using System.Collections.Generic;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02001006 RID: 4102
	[AddComponentMenu("KMonoBehaviour/scripts/PrefabAttributeModifiers")]
	public class PrefabAttributeModifiers : KMonoBehaviour
	{
		// Token: 0x06007E7F RID: 32383 RVA: 0x003293B1 File Offset: 0x003275B1
		protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
		}

		// Token: 0x06007E80 RID: 32384 RVA: 0x003293B9 File Offset: 0x003275B9
		public void AddAttributeDescriptor(AttributeModifier modifier)
		{
			this.descriptors.Add(modifier);
		}

		// Token: 0x06007E81 RID: 32385 RVA: 0x003293C7 File Offset: 0x003275C7
		public void RemovePrefabAttribute(AttributeModifier modifier)
		{
			this.descriptors.Remove(modifier);
		}

		// Token: 0x04005F61 RID: 24417
		public List<AttributeModifier> descriptors = new List<AttributeModifier>();
	}
}
