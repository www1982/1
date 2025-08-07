using System;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000FD4 RID: 4052
	public class AttributeConverterInstance : ModifierInstance<AttributeConverter>
	{
		// Token: 0x06007D38 RID: 32056 RVA: 0x00321AED File Offset: 0x0031FCED
		public AttributeConverterInstance(GameObject game_object, AttributeConverter converter, AttributeInstance attribute_instance)
			: base(game_object, converter)
		{
			this.converter = converter;
			this.attributeInstance = attribute_instance;
		}

		// Token: 0x06007D39 RID: 32057 RVA: 0x00321B05 File Offset: 0x0031FD05
		public float Evaluate()
		{
			return this.converter.multiplier * this.attributeInstance.GetTotalValue() + this.converter.baseValue;
		}

		// Token: 0x06007D3A RID: 32058 RVA: 0x00321B2A File Offset: 0x0031FD2A
		public string DescriptionFromAttribute(float value, GameObject go)
		{
			return this.converter.DescriptionFromAttribute(this.Evaluate(), go);
		}

		// Token: 0x04005E96 RID: 24214
		public AttributeConverter converter;

		// Token: 0x04005E97 RID: 24215
		public AttributeInstance attributeInstance;
	}
}
