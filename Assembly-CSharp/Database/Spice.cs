using System;
using Klei.AI;
using UnityEngine;

namespace Database
{
	// Token: 0x02000F0E RID: 3854
	public class Spice : Resource, IHasDlcRestrictions
	{
		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x060079EB RID: 31211 RVA: 0x00303873 File Offset: 0x00301A73
		// (set) Token: 0x060079EC RID: 31212 RVA: 0x0030387B File Offset: 0x00301A7B
		public AttributeModifier StatBonus { get; private set; }

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x060079ED RID: 31213 RVA: 0x00303884 File Offset: 0x00301A84
		// (set) Token: 0x060079EE RID: 31214 RVA: 0x0030388C File Offset: 0x00301A8C
		public AttributeModifier FoodModifier { get; private set; }

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x060079EF RID: 31215 RVA: 0x00303895 File Offset: 0x00301A95
		// (set) Token: 0x060079F0 RID: 31216 RVA: 0x0030389D File Offset: 0x00301A9D
		public AttributeModifier CalorieModifier { get; private set; }

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x060079F1 RID: 31217 RVA: 0x003038A6 File Offset: 0x00301AA6
		// (set) Token: 0x060079F2 RID: 31218 RVA: 0x003038AE File Offset: 0x00301AAE
		public Color PrimaryColor { get; private set; }

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x060079F3 RID: 31219 RVA: 0x003038B7 File Offset: 0x00301AB7
		// (set) Token: 0x060079F4 RID: 31220 RVA: 0x003038BF File Offset: 0x00301ABF
		public Color SecondaryColor { get; private set; }

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x060079F6 RID: 31222 RVA: 0x003038D1 File Offset: 0x00301AD1
		// (set) Token: 0x060079F5 RID: 31221 RVA: 0x003038C8 File Offset: 0x00301AC8
		public string Image { get; private set; }

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x060079F8 RID: 31224 RVA: 0x003038E2 File Offset: 0x00301AE2
		// (set) Token: 0x060079F7 RID: 31223 RVA: 0x003038D9 File Offset: 0x00301AD9
		public string[] requiredDlcIds { get; private set; }

		// Token: 0x060079F9 RID: 31225 RVA: 0x003038EC File Offset: 0x00301AEC
		public Spice(ResourceSet parent, string id, Spice.Ingredient[] ingredients, Color primaryColor, Color secondaryColor, AttributeModifier foodMod = null, AttributeModifier statBonus = null, string imageName = "unknown", string[] dlcID = null)
			: base(id, parent, null)
		{
			this.requiredDlcIds = this.requiredDlcIds;
			this.StatBonus = statBonus;
			this.FoodModifier = foodMod;
			this.Ingredients = ingredients;
			this.Image = imageName;
			this.PrimaryColor = primaryColor;
			this.SecondaryColor = secondaryColor;
			for (int i = 0; i < this.Ingredients.Length; i++)
			{
				this.TotalKG += this.Ingredients[i].AmountKG;
			}
		}

		// Token: 0x060079FA RID: 31226 RVA: 0x0030396A File Offset: 0x00301B6A
		public string[] GetRequiredDlcIds()
		{
			return this.requiredDlcIds;
		}

		// Token: 0x060079FB RID: 31227 RVA: 0x00303972 File Offset: 0x00301B72
		public string[] GetForbiddenDlcIds()
		{
			return null;
		}

		// Token: 0x04005918 RID: 22808
		public readonly Spice.Ingredient[] Ingredients;

		// Token: 0x04005919 RID: 22809
		public readonly float TotalKG;

		// Token: 0x020020F4 RID: 8436
		public class Ingredient : IConfigurableConsumerIngredient
		{
			// Token: 0x0600B8AC RID: 47276 RVA: 0x003EA6CB File Offset: 0x003E88CB
			public float GetAmount()
			{
				return this.AmountKG;
			}

			// Token: 0x0600B8AD RID: 47277 RVA: 0x003EA6D3 File Offset: 0x003E88D3
			public Tag[] GetIDSets()
			{
				return this.IngredientSet;
			}

			// Token: 0x040096E7 RID: 38631
			public Tag[] IngredientSet;

			// Token: 0x040096E8 RID: 38632
			public float AmountKG;
		}
	}
}
