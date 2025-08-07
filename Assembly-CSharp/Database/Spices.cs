using System;
using Klei.AI;
using UnityEngine;

namespace Database
{
	// Token: 0x02000F0F RID: 3855
	public class Spices : ResourceSet<Spice>
	{
		// Token: 0x060079FC RID: 31228 RVA: 0x00303978 File Offset: 0x00301B78
		public Spices(ResourceSet parent)
			: base("Spices", parent)
		{
			this.PreservingSpice = new Spice(this, "PRESERVING_SPICE", new Spice.Ingredient[]
			{
				new Spice.Ingredient
				{
					IngredientSet = new Tag[] { "BasicSingleHarvestPlantSeed" },
					AmountKG = 0.1f
				},
				new Spice.Ingredient
				{
					IngredientSet = new Tag[] { SimHashes.Salt.CreateTag() },
					AmountKG = 3f
				}
			}, new Color(0.961f, 0.827f, 0.29f), Color.white, new AttributeModifier("RotDelta", 0.5f, "Spices", false, false, true), null, "spice_recipe1", null);
			this.PilotingSpice = new Spice(this, "PILOTING_SPICE", new Spice.Ingredient[]
			{
				new Spice.Ingredient
				{
					IngredientSet = new Tag[] { "MushroomSeed" },
					AmountKG = 0.1f
				},
				new Spice.Ingredient
				{
					IngredientSet = new Tag[] { SimHashes.Sucrose.CreateTag() },
					AmountKG = 3f
				}
			}, new Color(0.039f, 0.725f, 0.831f), Color.white, null, new AttributeModifier("SpaceNavigation", 3f, "Spices", false, false, true), "spice_recipe2", DlcManager.EXPANSION1);
			this.StrengthSpice = new Spice(this, "STRENGTH_SPICE", new Spice.Ingredient[]
			{
				new Spice.Ingredient
				{
					IngredientSet = new Tag[] { "SeaLettuceSeed" },
					AmountKG = 0.1f
				},
				new Spice.Ingredient
				{
					IngredientSet = new Tag[] { SimHashes.Iron.CreateTag() },
					AmountKG = 3f
				}
			}, new Color(0.588f, 0.278f, 0.788f), Color.white, null, new AttributeModifier("Strength", 3f, "Spices", false, false, true), "spice_recipe3", null);
			this.MachinerySpice = new Spice(this, "MACHINERY_SPICE", new Spice.Ingredient[]
			{
				new Spice.Ingredient
				{
					IngredientSet = new Tag[] { "PrickleFlowerSeed" },
					AmountKG = 0.1f
				},
				new Spice.Ingredient
				{
					IngredientSet = new Tag[] { SimHashes.SlimeMold.CreateTag() },
					AmountKG = 3f
				}
			}, new Color(0.788f, 0.443f, 0.792f), Color.white, null, new AttributeModifier("Machinery", 3f, "Spices", false, false, true), "spice_recipe4", null);
		}

		// Token: 0x0400591C RID: 22812
		public Spice PreservingSpice;

		// Token: 0x0400591D RID: 22813
		public Spice PilotingSpice;

		// Token: 0x0400591E RID: 22814
		public Spice StrengthSpice;

		// Token: 0x0400591F RID: 22815
		public Spice MachinerySpice;
	}
}
