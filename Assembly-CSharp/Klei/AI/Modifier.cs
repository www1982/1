using System;
using System.Collections.Generic;

namespace Klei.AI
{
	// Token: 0x02001001 RID: 4097
	public class Modifier : Resource
	{
		// Token: 0x06007E58 RID: 32344 RVA: 0x00328CD0 File Offset: 0x00326ED0
		public Modifier(string id, string name, string description)
			: base(id, name)
		{
			this.description = description;
		}

		// Token: 0x06007E59 RID: 32345 RVA: 0x00328CEC File Offset: 0x00326EEC
		public void Add(AttributeModifier modifier)
		{
			if (modifier.AttributeId != "")
			{
				this.SelfModifiers.Add(modifier);
			}
		}

		// Token: 0x06007E5A RID: 32346 RVA: 0x00328D0C File Offset: 0x00326F0C
		public virtual void AddTo(Attributes attributes)
		{
			foreach (AttributeModifier attributeModifier in this.SelfModifiers)
			{
				attributes.Add(attributeModifier);
			}
		}

		// Token: 0x06007E5B RID: 32347 RVA: 0x00328D60 File Offset: 0x00326F60
		public virtual void RemoveFrom(Attributes attributes)
		{
			foreach (AttributeModifier attributeModifier in this.SelfModifiers)
			{
				attributes.Remove(attributeModifier);
			}
		}

		// Token: 0x04005F56 RID: 24406
		public string description;

		// Token: 0x04005F57 RID: 24407
		public List<AttributeModifier> SelfModifiers = new List<AttributeModifier>();
	}
}
