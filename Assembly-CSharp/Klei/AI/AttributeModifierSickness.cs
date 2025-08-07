using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000FEB RID: 4075
	public class AttributeModifierSickness : Sickness.SicknessComponent
	{
		// Token: 0x06007DD1 RID: 32209 RVA: 0x003260A8 File Offset: 0x003242A8
		public AttributeModifierSickness(Tag minionModel, AttributeModifier[] attribute_modifiers)
		{
			this.GetAttributeModifierForMinionModel[minionModel] = attribute_modifiers;
			this.attributeModifiers = new AttributeModifier[0];
		}

		// Token: 0x06007DD2 RID: 32210 RVA: 0x003260D4 File Offset: 0x003242D4
		public AttributeModifierSickness(AttributeModifier[] attribute_modifiers)
		{
			this.attributeModifiers = attribute_modifiers;
		}

		// Token: 0x06007DD3 RID: 32211 RVA: 0x003260F0 File Offset: 0x003242F0
		public override object OnInfect(GameObject go, SicknessInstance diseaseInstance)
		{
			Attributes attributes = go.GetAttributes();
			Tag tag = go.PrefabID();
			if (this.GetAttributeModifierForMinionModel.ContainsKey(tag))
			{
				for (int i = 0; i < this.GetAttributeModifierForMinionModel[tag].Length; i++)
				{
					AttributeModifier attributeModifier = this.GetAttributeModifierForMinionModel[tag][i];
					attributes.Add(attributeModifier);
				}
			}
			for (int j = 0; j < this.attributeModifiers.Length; j++)
			{
				AttributeModifier attributeModifier2 = this.attributeModifiers[j];
				attributes.Add(attributeModifier2);
			}
			return null;
		}

		// Token: 0x06007DD4 RID: 32212 RVA: 0x00326174 File Offset: 0x00324374
		public override void OnCure(GameObject go, object instance_data)
		{
			Attributes attributes = go.GetAttributes();
			Tag tag = go.PrefabID();
			if (this.GetAttributeModifierForMinionModel.ContainsKey(tag))
			{
				for (int i = 0; i < this.GetAttributeModifierForMinionModel[tag].Length; i++)
				{
					AttributeModifier attributeModifier = this.GetAttributeModifierForMinionModel[tag][i];
					attributes.Remove(attributeModifier);
				}
			}
			for (int j = 0; j < this.attributeModifiers.Length; j++)
			{
				AttributeModifier attributeModifier2 = this.attributeModifiers[j];
				attributes.Remove(attributeModifier2);
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06007DD5 RID: 32213 RVA: 0x003261F6 File Offset: 0x003243F6
		public AttributeModifier[] Modifers
		{
			get
			{
				return this.attributeModifiers;
			}
		}

		// Token: 0x06007DD6 RID: 32214 RVA: 0x00326200 File Offset: 0x00324400
		public override List<Descriptor> GetSymptoms(GameObject victim)
		{
			if (victim == null)
			{
				return this.GetSymptoms();
			}
			List<Descriptor> list = new List<Descriptor>();
			Tag tag = victim.PrefabID();
			if (this.GetAttributeModifierForMinionModel.ContainsKey(tag))
			{
				foreach (AttributeModifier attributeModifier in this.GetAttributeModifierForMinionModel[tag])
				{
					Attribute attribute = Db.Get().Attributes.Get(attributeModifier.AttributeId);
					list.Add(new Descriptor(string.Format(DUPLICANTS.DISEASES.ATTRIBUTE_MODIFIER_SYMPTOMS, attribute.Name, attributeModifier.GetFormattedString()), string.Format(DUPLICANTS.DISEASES.ATTRIBUTE_MODIFIER_SYMPTOMS_TOOLTIP, attribute.Name, attributeModifier.GetFormattedString()), Descriptor.DescriptorType.Symptom, false));
				}
			}
			foreach (AttributeModifier attributeModifier2 in this.attributeModifiers)
			{
				Attribute attribute2 = Db.Get().Attributes.Get(attributeModifier2.AttributeId);
				list.Add(new Descriptor(string.Format(DUPLICANTS.DISEASES.ATTRIBUTE_MODIFIER_SYMPTOMS, attribute2.Name, attributeModifier2.GetFormattedString()), string.Format(DUPLICANTS.DISEASES.ATTRIBUTE_MODIFIER_SYMPTOMS_TOOLTIP, attribute2.Name, attributeModifier2.GetFormattedString()), Descriptor.DescriptorType.Symptom, false));
			}
			return list;
		}

		// Token: 0x06007DD7 RID: 32215 RVA: 0x00326334 File Offset: 0x00324534
		public override List<Descriptor> GetSymptoms()
		{
			List<Descriptor> list = new List<Descriptor>();
			foreach (Tag tag in this.GetAttributeModifierForMinionModel.Keys)
			{
				string properName = Assets.GetPrefab(tag).GetProperName();
				foreach (AttributeModifier attributeModifier in this.GetAttributeModifierForMinionModel[tag])
				{
					Attribute attribute = Db.Get().Attributes.Get(attributeModifier.AttributeId);
					list.Add(new Descriptor(string.Format(DUPLICANTS.DISEASES.ATTRIBUTE_BY_MODEL_MODIFIER_SYMPTOMS, properName, attribute.Name, attributeModifier.GetFormattedString()), string.Format(DUPLICANTS.DISEASES.ATTRIBUTE_MODIFIER_SYMPTOMS_TOOLTIP, attribute.Name, attributeModifier.GetFormattedString()), Descriptor.DescriptorType.Symptom, false));
				}
			}
			foreach (AttributeModifier attributeModifier2 in this.attributeModifiers)
			{
				Attribute attribute2 = Db.Get().Attributes.Get(attributeModifier2.AttributeId);
				list.Add(new Descriptor(string.Format(DUPLICANTS.DISEASES.ATTRIBUTE_MODIFIER_SYMPTOMS, attribute2.Name, attributeModifier2.GetFormattedString()), string.Format(DUPLICANTS.DISEASES.ATTRIBUTE_MODIFIER_SYMPTOMS_TOOLTIP, attribute2.Name, attributeModifier2.GetFormattedString()), Descriptor.DescriptorType.Symptom, false));
			}
			return list;
		}

		// Token: 0x04005EEF RID: 24303
		private Dictionary<Tag, AttributeModifier[]> GetAttributeModifierForMinionModel = new Dictionary<Tag, AttributeModifier[]>();

		// Token: 0x04005EF0 RID: 24304
		private AttributeModifier[] attributeModifiers;
	}
}
