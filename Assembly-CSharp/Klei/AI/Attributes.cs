using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000FDA RID: 4058
	public class Attributes
	{
		// Token: 0x06007D7B RID: 32123 RVA: 0x0032295C File Offset: 0x00320B5C
		public IEnumerator<AttributeInstance> GetEnumerator()
		{
			return this.AttributeTable.GetEnumerator();
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06007D7C RID: 32124 RVA: 0x0032296E File Offset: 0x00320B6E
		public int Count
		{
			get
			{
				return this.AttributeTable.Count;
			}
		}

		// Token: 0x06007D7D RID: 32125 RVA: 0x0032297B File Offset: 0x00320B7B
		public Attributes(GameObject game_object)
		{
			this.gameObject = game_object;
		}

		// Token: 0x06007D7E RID: 32126 RVA: 0x00322998 File Offset: 0x00320B98
		public AttributeInstance Add(Attribute attribute)
		{
			AttributeInstance attributeInstance = this.Get(attribute.Id);
			if (attributeInstance == null)
			{
				attributeInstance = new AttributeInstance(this.gameObject, attribute);
				this.AttributeTable.Add(attributeInstance);
			}
			return attributeInstance;
		}

		// Token: 0x06007D7F RID: 32127 RVA: 0x003229D0 File Offset: 0x00320BD0
		public void Add(AttributeModifier modifier)
		{
			AttributeInstance attributeInstance = this.Get(modifier.AttributeId);
			if (attributeInstance != null)
			{
				attributeInstance.Add(modifier);
			}
		}

		// Token: 0x06007D80 RID: 32128 RVA: 0x003229F4 File Offset: 0x00320BF4
		public void Remove(AttributeModifier modifier)
		{
			if (modifier == null)
			{
				return;
			}
			AttributeInstance attributeInstance = this.Get(modifier.AttributeId);
			if (attributeInstance != null)
			{
				attributeInstance.Remove(modifier);
			}
		}

		// Token: 0x06007D81 RID: 32129 RVA: 0x00322A1C File Offset: 0x00320C1C
		public float GetValuePercent(string attribute_id)
		{
			float num = 1f;
			AttributeInstance attributeInstance = this.Get(attribute_id);
			if (attributeInstance != null)
			{
				num = attributeInstance.GetTotalValue() / attributeInstance.GetBaseValue();
			}
			else
			{
				global::Debug.LogError("Could not find attribute " + attribute_id);
			}
			return num;
		}

		// Token: 0x06007D82 RID: 32130 RVA: 0x00322A5C File Offset: 0x00320C5C
		public AttributeInstance Get(string attribute_id)
		{
			for (int i = 0; i < this.AttributeTable.Count; i++)
			{
				if (this.AttributeTable[i].Id == attribute_id)
				{
					return this.AttributeTable[i];
				}
			}
			return null;
		}

		// Token: 0x06007D83 RID: 32131 RVA: 0x00322AA6 File Offset: 0x00320CA6
		public AttributeInstance Get(Attribute attribute)
		{
			return this.Get(attribute.Id);
		}

		// Token: 0x06007D84 RID: 32132 RVA: 0x00322AB4 File Offset: 0x00320CB4
		public float GetValue(string id)
		{
			float num = 0f;
			AttributeInstance attributeInstance = this.Get(id);
			if (attributeInstance != null)
			{
				num = attributeInstance.GetTotalValue();
			}
			else
			{
				global::Debug.LogError("Could not find attribute " + id);
			}
			return num;
		}

		// Token: 0x06007D85 RID: 32133 RVA: 0x00322AEC File Offset: 0x00320CEC
		public AttributeInstance GetProfession()
		{
			AttributeInstance attributeInstance = null;
			foreach (AttributeInstance attributeInstance2 in this)
			{
				if (attributeInstance2.modifier.IsProfession)
				{
					if (attributeInstance == null)
					{
						attributeInstance = attributeInstance2;
					}
					else if (attributeInstance.GetTotalValue() < attributeInstance2.GetTotalValue())
					{
						attributeInstance = attributeInstance2;
					}
				}
			}
			return attributeInstance;
		}

		// Token: 0x06007D86 RID: 32134 RVA: 0x00322B54 File Offset: 0x00320D54
		public string GetProfessionString(bool longform = true)
		{
			AttributeInstance profession = this.GetProfession();
			if ((int)profession.GetTotalValue() == 0)
			{
				return string.Format(longform ? UI.ATTRIBUTELEVEL : UI.ATTRIBUTELEVEL_SHORT, 0, DUPLICANTS.ATTRIBUTES.UNPROFESSIONAL_NAME);
			}
			return string.Format(longform ? UI.ATTRIBUTELEVEL : UI.ATTRIBUTELEVEL_SHORT, (int)profession.GetTotalValue(), profession.modifier.ProfessionName);
		}

		// Token: 0x06007D87 RID: 32135 RVA: 0x00322BC8 File Offset: 0x00320DC8
		public string GetProfessionDescriptionString()
		{
			AttributeInstance profession = this.GetProfession();
			if ((int)profession.GetTotalValue() == 0)
			{
				return DUPLICANTS.ATTRIBUTES.UNPROFESSIONAL_DESC;
			}
			return string.Format(DUPLICANTS.ATTRIBUTES.PROFESSION_DESC, profession.modifier.Name);
		}

		// Token: 0x04005EAE RID: 24238
		public List<AttributeInstance> AttributeTable = new List<AttributeInstance>();

		// Token: 0x04005EAF RID: 24239
		public GameObject gameObject;
	}
}
