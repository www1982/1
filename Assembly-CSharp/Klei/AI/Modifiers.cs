using System;
using System.Collections.Generic;
using System.IO;
using KSerialization;
using STRINGS;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02001004 RID: 4100
	[SerializationConfig(MemberSerialization.OptIn)]
	[AddComponentMenu("KMonoBehaviour/scripts/Modifiers")]
	public class Modifiers : KMonoBehaviour, ISaveLoadableDetails
	{
		// Token: 0x06007E6D RID: 32365 RVA: 0x00328EB4 File Offset: 0x003270B4
		protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
			this.amounts = new Amounts(base.gameObject);
			this.sicknesses = new Sicknesses(base.gameObject);
			this.attributes = new Attributes(base.gameObject);
			foreach (string text in this.initialAmounts)
			{
				this.amounts.Add(new AmountInstance(Db.Get().Amounts.Get(text), base.gameObject));
			}
			foreach (string text2 in this.initialAttributes)
			{
				Attribute attribute = Db.Get().CritterAttributes.TryGet(text2);
				if (attribute == null)
				{
					attribute = Db.Get().PlantAttributes.TryGet(text2);
				}
				if (attribute == null)
				{
					attribute = Db.Get().Attributes.TryGet(text2);
				}
				DebugUtil.Assert(attribute != null, "Couldn't find an attribute for id", text2);
				this.attributes.Add(attribute);
			}
			Traits component = base.GetComponent<Traits>();
			if (this.initialTraits != null)
			{
				foreach (string text3 in this.initialTraits)
				{
					Trait trait = Db.Get().traits.Get(text3);
					component.Add(trait);
				}
			}
		}

		// Token: 0x06007E6E RID: 32366 RVA: 0x00329060 File Offset: 0x00327260
		public float GetPreModifiedAttributeValue(Attribute attribute)
		{
			return AttributeInstance.GetTotalValue(attribute, this.GetPreModifiers(attribute));
		}

		// Token: 0x06007E6F RID: 32367 RVA: 0x00329070 File Offset: 0x00327270
		public string GetPreModifiedAttributeFormattedValue(Attribute attribute)
		{
			float totalValue = AttributeInstance.GetTotalValue(attribute, this.GetPreModifiers(attribute));
			return attribute.formatter.GetFormattedValue(totalValue, attribute.formatter.DeltaTimeSlice);
		}

		// Token: 0x06007E70 RID: 32368 RVA: 0x003290A4 File Offset: 0x003272A4
		public string GetPreModifiedAttributeDescription(Attribute attribute)
		{
			float totalValue = AttributeInstance.GetTotalValue(attribute, this.GetPreModifiers(attribute));
			return string.Format(DUPLICANTS.ATTRIBUTES.VALUE, attribute.Name, attribute.formatter.GetFormattedValue(totalValue, GameUtil.TimeSlice.None));
		}

		// Token: 0x06007E71 RID: 32369 RVA: 0x003290E1 File Offset: 0x003272E1
		public string GetPreModifiedAttributeToolTip(Attribute attribute)
		{
			return attribute.formatter.GetTooltip(attribute, this.GetPreModifiers(attribute), null);
		}

		// Token: 0x06007E72 RID: 32370 RVA: 0x003290F8 File Offset: 0x003272F8
		public List<AttributeModifier> GetPreModifiers(Attribute attribute)
		{
			List<AttributeModifier> list = new List<AttributeModifier>();
			foreach (string text in this.initialTraits)
			{
				foreach (AttributeModifier attributeModifier in Db.Get().traits.Get(text).SelfModifiers)
				{
					if (attributeModifier.AttributeId == attribute.Id)
					{
						list.Add(attributeModifier);
					}
				}
			}
			MutantPlant component = base.GetComponent<MutantPlant>();
			if (component != null && component.MutationIDs != null)
			{
				foreach (string text2 in component.MutationIDs)
				{
					foreach (AttributeModifier attributeModifier2 in Db.Get().PlantMutations.Get(text2).SelfModifiers)
					{
						if (attributeModifier2.AttributeId == attribute.Id)
						{
							list.Add(attributeModifier2);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06007E73 RID: 32371 RVA: 0x00329278 File Offset: 0x00327478
		public void Serialize(BinaryWriter writer)
		{
			this.OnSerialize(writer);
		}

		// Token: 0x06007E74 RID: 32372 RVA: 0x00329281 File Offset: 0x00327481
		public void Deserialize(IReader reader)
		{
			this.OnDeserialize(reader);
		}

		// Token: 0x06007E75 RID: 32373 RVA: 0x0032928A File Offset: 0x0032748A
		public virtual void OnSerialize(BinaryWriter writer)
		{
			this.amounts.Serialize(writer);
			this.sicknesses.Serialize(writer);
		}

		// Token: 0x06007E76 RID: 32374 RVA: 0x003292A4 File Offset: 0x003274A4
		public virtual void OnDeserialize(IReader reader)
		{
			this.amounts.Deserialize(reader);
			this.sicknesses.Deserialize(reader);
		}

		// Token: 0x06007E77 RID: 32375 RVA: 0x003292BE File Offset: 0x003274BE
		protected override void OnCleanUp()
		{
			base.OnCleanUp();
			if (this.amounts != null)
			{
				this.amounts.Cleanup();
			}
		}

		// Token: 0x04005F5B RID: 24411
		public Amounts amounts;

		// Token: 0x04005F5C RID: 24412
		public Attributes attributes;

		// Token: 0x04005F5D RID: 24413
		public Sicknesses sicknesses;

		// Token: 0x04005F5E RID: 24414
		public List<string> initialTraits = new List<string>();

		// Token: 0x04005F5F RID: 24415
		public List<string> initialAmounts = new List<string>();

		// Token: 0x04005F60 RID: 24416
		public List<string> initialAttributes = new List<string>();
	}
}
