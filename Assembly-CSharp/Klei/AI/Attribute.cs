using System;
using System.Collections.Generic;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000FD2 RID: 4050
	public class Attribute : Resource, IHasDlcRestrictions
	{
		// Token: 0x06007D2A RID: 32042 RVA: 0x00321840 File Offset: 0x0031FA40
		public Attribute(string id, bool is_trainable, Attribute.Display show_in_ui, bool is_profession, float base_value = 0f, string uiSprite = null, string thoughtSprite = null, string uiFullColourSprite = null, string[] overrideDLCIDs = null)
			: base(id, null, null)
		{
			string text = "STRINGS.DUPLICANTS.ATTRIBUTES." + id.ToUpper();
			this.Name = Strings.Get(new StringKey(text + ".NAME"));
			this.ProfessionName = Strings.Get(new StringKey(text + ".NAME"));
			this.Description = Strings.Get(new StringKey(text + ".DESC"));
			this.IsTrainable = is_trainable;
			this.IsProfession = is_profession;
			this.ShowInUI = show_in_ui;
			this.BaseValue = base_value;
			this.formatter = Attribute.defaultFormatter;
			this.uiSprite = uiSprite;
			this.thoughtSprite = thoughtSprite;
			this.uiFullColourSprite = uiFullColourSprite;
			this.requiredDlcIds = overrideDLCIDs;
		}

		// Token: 0x06007D2B RID: 32043 RVA: 0x0032191C File Offset: 0x0031FB1C
		public Attribute(string id, string name, string profession_name, string attribute_description, float base_value, Attribute.Display show_in_ui, bool is_trainable, string uiSprite = null, string thoughtSprite = null, string uiFullColourSprite = null)
			: base(id, name)
		{
			this.Description = attribute_description;
			this.ProfessionName = profession_name;
			this.BaseValue = base_value;
			this.ShowInUI = show_in_ui;
			this.IsTrainable = is_trainable;
			this.uiSprite = uiSprite;
			this.thoughtSprite = thoughtSprite;
			this.uiFullColourSprite = uiFullColourSprite;
			if (this.ProfessionName == "")
			{
				this.ProfessionName = null;
			}
		}

		// Token: 0x06007D2C RID: 32044 RVA: 0x00321994 File Offset: 0x0031FB94
		public void SetFormatter(IAttributeFormatter formatter)
		{
			this.formatter = formatter;
		}

		// Token: 0x06007D2D RID: 32045 RVA: 0x0032199D File Offset: 0x0031FB9D
		public AttributeInstance Lookup(Component cmp)
		{
			return this.Lookup(cmp.gameObject);
		}

		// Token: 0x06007D2E RID: 32046 RVA: 0x003219AC File Offset: 0x0031FBAC
		public AttributeInstance Lookup(GameObject go)
		{
			Attributes attributes = go.GetAttributes();
			if (attributes != null)
			{
				return attributes.Get(this);
			}
			return null;
		}

		// Token: 0x06007D2F RID: 32047 RVA: 0x003219CC File Offset: 0x0031FBCC
		public string GetDescription(AttributeInstance instance)
		{
			return instance.GetDescription();
		}

		// Token: 0x06007D30 RID: 32048 RVA: 0x003219D4 File Offset: 0x0031FBD4
		public string GetTooltip(AttributeInstance instance)
		{
			return this.formatter.GetTooltip(this, instance);
		}

		// Token: 0x06007D31 RID: 32049 RVA: 0x003219E3 File Offset: 0x0031FBE3
		public string[] GetRequiredDlcIds()
		{
			return this.requiredDlcIds;
		}

		// Token: 0x06007D32 RID: 32050 RVA: 0x003219EB File Offset: 0x0031FBEB
		public string[] GetForbiddenDlcIds()
		{
			return null;
		}

		// Token: 0x04005E83 RID: 24195
		private static readonly StandardAttributeFormatter defaultFormatter = new StandardAttributeFormatter(GameUtil.UnitClass.SimpleFloat, GameUtil.TimeSlice.None);

		// Token: 0x04005E84 RID: 24196
		public string Description;

		// Token: 0x04005E85 RID: 24197
		public float BaseValue;

		// Token: 0x04005E86 RID: 24198
		public Attribute.Display ShowInUI;

		// Token: 0x04005E87 RID: 24199
		public bool IsTrainable;

		// Token: 0x04005E88 RID: 24200
		public bool IsProfession;

		// Token: 0x04005E89 RID: 24201
		public string ProfessionName;

		// Token: 0x04005E8A RID: 24202
		public List<AttributeConverter> converters = new List<AttributeConverter>();

		// Token: 0x04005E8B RID: 24203
		public string uiSprite;

		// Token: 0x04005E8C RID: 24204
		public string thoughtSprite;

		// Token: 0x04005E8D RID: 24205
		public string uiFullColourSprite;

		// Token: 0x04005E8E RID: 24206
		public string[] requiredDlcIds;

		// Token: 0x04005E8F RID: 24207
		public string[] forbiddenDlcIds;

		// Token: 0x04005E90 RID: 24208
		public IAttributeFormatter formatter;

		// Token: 0x020025BE RID: 9662
		public enum Display
		{
			// Token: 0x0400A8A4 RID: 43172
			Normal,
			// Token: 0x0400A8A5 RID: 43173
			Skill,
			// Token: 0x0400A8A6 RID: 43174
			Expectation,
			// Token: 0x0400A8A7 RID: 43175
			General,
			// Token: 0x0400A8A8 RID: 43176
			Details,
			// Token: 0x0400A8A9 RID: 43177
			Never
		}
	}
}
