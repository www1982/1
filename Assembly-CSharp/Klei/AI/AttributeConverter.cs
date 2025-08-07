using System;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000FD3 RID: 4051
	public class AttributeConverter : Resource
	{
		// Token: 0x06007D34 RID: 32052 RVA: 0x003219FC File Offset: 0x0031FBFC
		public AttributeConverter(string id, string name, string description, float multiplier, float base_value, Attribute attribute, IAttributeFormatter formatter = null)
			: base(id, name)
		{
			this.description = description;
			this.multiplier = multiplier;
			this.baseValue = base_value;
			this.attribute = attribute;
			this.formatter = formatter;
		}

		// Token: 0x06007D35 RID: 32053 RVA: 0x00321A2D File Offset: 0x0031FC2D
		public AttributeConverterInstance Lookup(Component cmp)
		{
			return this.Lookup(cmp.gameObject);
		}

		// Token: 0x06007D36 RID: 32054 RVA: 0x00321A3C File Offset: 0x0031FC3C
		public AttributeConverterInstance Lookup(GameObject go)
		{
			AttributeConverters component = go.GetComponent<AttributeConverters>();
			if (component != null)
			{
				return component.Get(this);
			}
			return null;
		}

		// Token: 0x06007D37 RID: 32055 RVA: 0x00321A64 File Offset: 0x0031FC64
		public string DescriptionFromAttribute(float value, GameObject go)
		{
			string text;
			if (this.formatter != null)
			{
				text = this.formatter.GetFormattedValue(value, this.formatter.DeltaTimeSlice);
			}
			else if (this.attribute.formatter != null)
			{
				text = this.attribute.formatter.GetFormattedValue(value, this.attribute.formatter.DeltaTimeSlice);
			}
			else
			{
				text = GameUtil.GetFormattedSimple(value, GameUtil.TimeSlice.None, null);
			}
			if (text != null)
			{
				text = GameUtil.AddPositiveSign(text, value > 0f);
				return string.Format(this.description, text);
			}
			return null;
		}

		// Token: 0x04005E91 RID: 24209
		public string description;

		// Token: 0x04005E92 RID: 24210
		public float multiplier;

		// Token: 0x04005E93 RID: 24211
		public float baseValue;

		// Token: 0x04005E94 RID: 24212
		public Attribute attribute;

		// Token: 0x04005E95 RID: 24213
		public IAttributeFormatter formatter;
	}
}
