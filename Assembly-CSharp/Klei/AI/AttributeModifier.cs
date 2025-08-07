using System;
using System.Diagnostics;

namespace Klei.AI
{
	// Token: 0x02000FD9 RID: 4057
	[DebuggerDisplay("{AttributeId}")]
	public class AttributeModifier
	{
		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06007D68 RID: 32104 RVA: 0x003226AA File Offset: 0x003208AA
		// (set) Token: 0x06007D69 RID: 32105 RVA: 0x003226B2 File Offset: 0x003208B2
		public string AttributeId { get; private set; }

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06007D6A RID: 32106 RVA: 0x003226BB File Offset: 0x003208BB
		// (set) Token: 0x06007D6B RID: 32107 RVA: 0x003226C3 File Offset: 0x003208C3
		public float Value { get; private set; }

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06007D6C RID: 32108 RVA: 0x003226CC File Offset: 0x003208CC
		// (set) Token: 0x06007D6D RID: 32109 RVA: 0x003226D4 File Offset: 0x003208D4
		public bool IsMultiplier { get; private set; }

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06007D6E RID: 32110 RVA: 0x003226DD File Offset: 0x003208DD
		// (set) Token: 0x06007D6F RID: 32111 RVA: 0x003226E5 File Offset: 0x003208E5
		public GameUtil.TimeSlice? OverrideTimeSlice { get; set; }

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06007D70 RID: 32112 RVA: 0x003226EE File Offset: 0x003208EE
		// (set) Token: 0x06007D71 RID: 32113 RVA: 0x003226F6 File Offset: 0x003208F6
		public bool UIOnly { get; private set; }

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06007D72 RID: 32114 RVA: 0x003226FF File Offset: 0x003208FF
		// (set) Token: 0x06007D73 RID: 32115 RVA: 0x00322707 File Offset: 0x00320907
		public bool IsReadonly { get; private set; }

		// Token: 0x06007D74 RID: 32116 RVA: 0x00322710 File Offset: 0x00320910
		public AttributeModifier(string attribute_id, float value, string description = null, bool is_multiplier = false, bool uiOnly = false, bool is_readonly = true)
		{
			this.AttributeId = attribute_id;
			this.Value = value;
			this.Description = ((description == null) ? attribute_id : description);
			this.DescriptionCB = null;
			this.IsMultiplier = is_multiplier;
			this.UIOnly = uiOnly;
			this.IsReadonly = is_readonly;
			this.OverrideTimeSlice = null;
		}

		// Token: 0x06007D75 RID: 32117 RVA: 0x0032276C File Offset: 0x0032096C
		public AttributeModifier(string attribute_id, float value, Func<string> description_cb, bool is_multiplier = false, bool uiOnly = false)
		{
			this.AttributeId = attribute_id;
			this.Value = value;
			this.DescriptionCB = description_cb;
			this.Description = null;
			this.IsMultiplier = is_multiplier;
			this.UIOnly = uiOnly;
			this.OverrideTimeSlice = null;
			if (description_cb == null)
			{
				global::Debug.LogWarning("AttributeModifier being constructed without a description callback: " + attribute_id);
			}
		}

		// Token: 0x06007D76 RID: 32118 RVA: 0x003227CD File Offset: 0x003209CD
		public void SetValue(float value)
		{
			this.Value = value;
		}

		// Token: 0x06007D77 RID: 32119 RVA: 0x003227D8 File Offset: 0x003209D8
		public string GetName()
		{
			Attribute attribute = Db.Get().Attributes.TryGet(this.AttributeId);
			if (attribute != null && attribute.ShowInUI != Attribute.Display.Never)
			{
				return attribute.Name;
			}
			return "";
		}

		// Token: 0x06007D78 RID: 32120 RVA: 0x00322813 File Offset: 0x00320A13
		public string GetDescription()
		{
			if (this.DescriptionCB == null)
			{
				return this.Description;
			}
			return this.DescriptionCB();
		}

		// Token: 0x06007D79 RID: 32121 RVA: 0x00322830 File Offset: 0x00320A30
		public string GetFormattedString()
		{
			IAttributeFormatter attributeFormatter = null;
			Attribute attribute = Db.Get().Attributes.TryGet(this.AttributeId);
			if (!this.IsMultiplier)
			{
				if (attribute != null)
				{
					attributeFormatter = attribute.formatter;
				}
				else
				{
					attribute = Db.Get().BuildingAttributes.TryGet(this.AttributeId);
					if (attribute != null)
					{
						attributeFormatter = attribute.formatter;
					}
					else
					{
						attribute = Db.Get().PlantAttributes.TryGet(this.AttributeId);
						if (attribute != null)
						{
							attributeFormatter = attribute.formatter;
						}
					}
				}
			}
			string text = "";
			if (attributeFormatter != null)
			{
				text = attributeFormatter.GetFormattedModifier(this);
			}
			else if (this.IsMultiplier)
			{
				text += GameUtil.GetFormattedPercent(this.Value * 100f, GameUtil.TimeSlice.None);
			}
			else
			{
				text += GameUtil.GetFormattedSimple(this.Value, GameUtil.TimeSlice.None, null);
			}
			if (text != null && text.Length > 0 && text[0] != '-')
			{
				GameUtil.TimeSlice? overrideTimeSlice = this.OverrideTimeSlice;
				GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None;
				if (!((overrideTimeSlice.GetValueOrDefault() == timeSlice) & (overrideTimeSlice != null)))
				{
					text = GameUtil.AddPositiveSign(text, this.Value > 0f);
				}
			}
			return text;
		}

		// Token: 0x06007D7A RID: 32122 RVA: 0x00322940 File Offset: 0x00320B40
		public AttributeModifier Clone()
		{
			return new AttributeModifier(this.AttributeId, this.Value, this.Description, false, false, true);
		}

		// Token: 0x04005EAC RID: 24236
		public string Description;

		// Token: 0x04005EAD RID: 24237
		public Func<string> DescriptionCB;
	}
}
