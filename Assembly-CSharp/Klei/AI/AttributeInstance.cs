using System;
using System.Collections.Generic;
using System.Diagnostics;
using STRINGS;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000FD6 RID: 4054
	[DebuggerDisplay("{Attribute.Id}")]
	public class AttributeInstance : ModifierInstance<Attribute>
	{
		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06007D40 RID: 32064 RVA: 0x00321CC7 File Offset: 0x0031FEC7
		public string Id
		{
			get
			{
				return this.Attribute.Id;
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06007D41 RID: 32065 RVA: 0x00321CD4 File Offset: 0x0031FED4
		public string Name
		{
			get
			{
				return this.Attribute.Name;
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06007D42 RID: 32066 RVA: 0x00321CE1 File Offset: 0x0031FEE1
		public string Description
		{
			get
			{
				return this.Attribute.Description;
			}
		}

		// Token: 0x06007D43 RID: 32067 RVA: 0x00321CEE File Offset: 0x0031FEEE
		public float GetBaseValue()
		{
			return this.Attribute.BaseValue;
		}

		// Token: 0x06007D44 RID: 32068 RVA: 0x00321CFC File Offset: 0x0031FEFC
		public float GetTotalDisplayValue()
		{
			float num = this.Attribute.BaseValue;
			float num2 = 0f;
			for (int num3 = 0; num3 != this.Modifiers.Count; num3++)
			{
				AttributeModifier attributeModifier = this.Modifiers[num3];
				if (!attributeModifier.IsMultiplier)
				{
					num += attributeModifier.Value;
				}
				else
				{
					num2 += attributeModifier.Value;
				}
			}
			if (num2 != 0f)
			{
				num += Mathf.Abs(num) * num2;
			}
			return num;
		}

		// Token: 0x06007D45 RID: 32069 RVA: 0x00321D70 File Offset: 0x0031FF70
		public float GetTotalValue()
		{
			float num = this.Attribute.BaseValue;
			float num2 = 0f;
			for (int num3 = 0; num3 != this.Modifiers.Count; num3++)
			{
				AttributeModifier attributeModifier = this.Modifiers[num3];
				if (!attributeModifier.UIOnly)
				{
					if (!attributeModifier.IsMultiplier)
					{
						num += attributeModifier.Value;
					}
					else
					{
						num2 += attributeModifier.Value;
					}
				}
			}
			if (num2 != 0f)
			{
				num += Mathf.Abs(num) * num2;
			}
			return num;
		}

		// Token: 0x06007D46 RID: 32070 RVA: 0x00321DEC File Offset: 0x0031FFEC
		public static float GetTotalDisplayValue(Attribute attribute, List<AttributeModifier> modifiers)
		{
			float num = attribute.BaseValue;
			float num2 = 0f;
			for (int num3 = 0; num3 != modifiers.Count; num3++)
			{
				AttributeModifier attributeModifier = modifiers[num3];
				if (!attributeModifier.IsMultiplier)
				{
					num += attributeModifier.Value;
				}
				else
				{
					num2 += attributeModifier.Value;
				}
			}
			if (num2 != 0f)
			{
				num += Mathf.Abs(num) * num2;
			}
			return num;
		}

		// Token: 0x06007D47 RID: 32071 RVA: 0x00321E50 File Offset: 0x00320050
		public static float GetTotalValue(Attribute attribute, List<AttributeModifier> modifiers)
		{
			float num = attribute.BaseValue;
			float num2 = 0f;
			for (int num3 = 0; num3 != modifiers.Count; num3++)
			{
				AttributeModifier attributeModifier = modifiers[num3];
				if (!attributeModifier.UIOnly)
				{
					if (!attributeModifier.IsMultiplier)
					{
						num += attributeModifier.Value;
					}
					else
					{
						num2 += attributeModifier.Value;
					}
				}
			}
			if (num2 != 0f)
			{
				num += Mathf.Abs(num) * num2;
			}
			return num;
		}

		// Token: 0x06007D48 RID: 32072 RVA: 0x00321EBC File Offset: 0x003200BC
		public float GetModifierContribution(AttributeModifier testModifier)
		{
			if (!testModifier.IsMultiplier)
			{
				return testModifier.Value;
			}
			float num = this.Attribute.BaseValue;
			for (int num2 = 0; num2 != this.Modifiers.Count; num2++)
			{
				AttributeModifier attributeModifier = this.Modifiers[num2];
				if (!attributeModifier.IsMultiplier)
				{
					num += attributeModifier.Value;
				}
			}
			return num * testModifier.Value;
		}

		// Token: 0x06007D49 RID: 32073 RVA: 0x00321F20 File Offset: 0x00320120
		public AttributeInstance(GameObject game_object, Attribute attribute)
			: base(game_object, attribute)
		{
			DebugUtil.Assert(attribute != null);
			this.Attribute = attribute;
		}

		// Token: 0x06007D4A RID: 32074 RVA: 0x00321F3A File Offset: 0x0032013A
		public void Add(AttributeModifier modifier)
		{
			this.Modifiers.Add(modifier);
			if (this.OnDirty != null)
			{
				this.OnDirty();
			}
		}

		// Token: 0x06007D4B RID: 32075 RVA: 0x00321F5C File Offset: 0x0032015C
		public void Remove(AttributeModifier modifier)
		{
			int i = 0;
			while (i < this.Modifiers.Count)
			{
				if (this.Modifiers[i] == modifier)
				{
					this.Modifiers.RemoveAt(i);
					if (this.OnDirty != null)
					{
						this.OnDirty();
						return;
					}
					break;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06007D4C RID: 32076 RVA: 0x00321FAE File Offset: 0x003201AE
		public void ClearModifiers()
		{
			if (this.Modifiers.Count > 0)
			{
				this.Modifiers.Clear();
				if (this.OnDirty != null)
				{
					this.OnDirty();
				}
			}
		}

		// Token: 0x06007D4D RID: 32077 RVA: 0x00321FDC File Offset: 0x003201DC
		public string GetDescription()
		{
			return string.Format(DUPLICANTS.ATTRIBUTES.VALUE, this.Name, this.GetFormattedValue());
		}

		// Token: 0x06007D4E RID: 32078 RVA: 0x00321FF9 File Offset: 0x003201F9
		public string GetFormattedValue()
		{
			return this.Attribute.formatter.GetFormattedAttribute(this);
		}

		// Token: 0x06007D4F RID: 32079 RVA: 0x0032200C File Offset: 0x0032020C
		public string GetAttributeValueTooltip()
		{
			return this.Attribute.GetTooltip(this);
		}

		// Token: 0x04005E99 RID: 24217
		public Attribute Attribute;

		// Token: 0x04005E9A RID: 24218
		public global::System.Action OnDirty;

		// Token: 0x04005E9B RID: 24219
		public ArrayRef<AttributeModifier> Modifiers;

		// Token: 0x04005E9C RID: 24220
		public bool hide;
	}
}
