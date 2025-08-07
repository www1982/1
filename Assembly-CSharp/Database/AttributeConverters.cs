using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;

namespace Database
{
	// Token: 0x02000ED7 RID: 3799
	public class AttributeConverters : ResourceSet<AttributeConverter>
	{
		// Token: 0x06007931 RID: 31025 RVA: 0x002EDB84 File Offset: 0x002EBD84
		public AttributeConverter Create(string id, string name, string description, Klei.AI.Attribute attribute, float multiplier, float base_value, IAttributeFormatter formatter, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
		{
			AttributeConverter attributeConverter = new AttributeConverter(id, name, description, multiplier, base_value, attribute, formatter);
			if (DlcManager.IsCorrectDlcSubscribed(requiredDlcIds, forbiddenDlcIds))
			{
				base.Add(attributeConverter);
				attribute.converters.Add(attributeConverter);
			}
			return attributeConverter;
		}

		// Token: 0x06007932 RID: 31026 RVA: 0x002EDBC4 File Offset: 0x002EBDC4
		public AttributeConverters()
		{
			ToPercentAttributeFormatter toPercentAttributeFormatter = new ToPercentAttributeFormatter(1f, GameUtil.TimeSlice.None);
			StandardAttributeFormatter standardAttributeFormatter = new StandardAttributeFormatter(GameUtil.UnitClass.Mass, GameUtil.TimeSlice.None);
			this.MovementSpeed = this.Create("MovementSpeed", "Movement Speed", DUPLICANTS.ATTRIBUTES.ATHLETICS.SPEEDMODIFIER, Db.Get().Attributes.Athletics, 0.1f, 0f, toPercentAttributeFormatter, null, null);
			this.ConstructionSpeed = this.Create("ConstructionSpeed", "Construction Speed", DUPLICANTS.ATTRIBUTES.CONSTRUCTION.SPEEDMODIFIER, Db.Get().Attributes.Construction, 0.25f, 0f, toPercentAttributeFormatter, null, null);
			this.DiggingSpeed = this.Create("DiggingSpeed", "Digging Speed", DUPLICANTS.ATTRIBUTES.DIGGING.SPEEDMODIFIER, Db.Get().Attributes.Digging, 0.25f, 0f, toPercentAttributeFormatter, null, null);
			this.MachinerySpeed = this.Create("MachinerySpeed", "Machinery Speed", DUPLICANTS.ATTRIBUTES.MACHINERY.SPEEDMODIFIER, Db.Get().Attributes.Machinery, 0.1f, 0f, toPercentAttributeFormatter, null, null);
			this.HarvestSpeed = this.Create("HarvestSpeed", "Harvest Speed", DUPLICANTS.ATTRIBUTES.BOTANIST.HARVEST_SPEED_MODIFIER, Db.Get().Attributes.Botanist, 0.05f, 0f, toPercentAttributeFormatter, null, null);
			this.PlantTendSpeed = this.Create("PlantTendSpeed", "Plant Tend Speed", DUPLICANTS.ATTRIBUTES.BOTANIST.TINKER_MODIFIER, Db.Get().Attributes.Botanist, 0.025f, 0f, toPercentAttributeFormatter, null, null);
			this.CompoundingSpeed = this.Create("CompoundingSpeed", "Compounding Speed", DUPLICANTS.ATTRIBUTES.CARING.FABRICATE_SPEEDMODIFIER, Db.Get().Attributes.Caring, 0.1f, 0f, toPercentAttributeFormatter, null, null);
			this.ResearchSpeed = this.Create("ResearchSpeed", "Research Speed", DUPLICANTS.ATTRIBUTES.LEARNING.RESEARCHSPEED, Db.Get().Attributes.Learning, 0.4f, 0f, toPercentAttributeFormatter, null, null);
			this.TrainingSpeed = this.Create("TrainingSpeed", "Training Speed", DUPLICANTS.ATTRIBUTES.LEARNING.SPEEDMODIFIER, Db.Get().Attributes.Learning, 0.1f, 0f, toPercentAttributeFormatter, null, null);
			this.CookingSpeed = this.Create("CookingSpeed", "Cooking Speed", DUPLICANTS.ATTRIBUTES.COOKING.SPEEDMODIFIER, Db.Get().Attributes.Cooking, 0.05f, 0f, toPercentAttributeFormatter, null, null);
			this.ArtSpeed = this.Create("ArtSpeed", "Art Speed", DUPLICANTS.ATTRIBUTES.ART.SPEEDMODIFIER, Db.Get().Attributes.Art, 0.1f, 0f, toPercentAttributeFormatter, null, null);
			this.DoctorSpeed = this.Create("DoctorSpeed", "Doctor Speed", DUPLICANTS.ATTRIBUTES.CARING.SPEEDMODIFIER, Db.Get().Attributes.Caring, 0.2f, 0f, toPercentAttributeFormatter, null, null);
			this.TidyingSpeed = this.Create("TidyingSpeed", "Tidying Speed", DUPLICANTS.ATTRIBUTES.STRENGTH.SPEEDMODIFIER, Db.Get().Attributes.Strength, 0.25f, 0f, toPercentAttributeFormatter, null, null);
			this.AttackDamage = this.Create("AttackDamage", "Attack Damage", DUPLICANTS.ATTRIBUTES.DIGGING.ATTACK_MODIFIER, Db.Get().Attributes.Digging, 0.05f, 0f, toPercentAttributeFormatter, null, null);
			this.PilotingSpeed = this.Create("PilotingSpeed", "Piloting Speed", DUPLICANTS.ATTRIBUTES.SPACENAVIGATION.SPEED_MODIFIER, Db.Get().Attributes.SpaceNavigation, 0.025f, 0f, toPercentAttributeFormatter, DlcManager.EXPANSION1, null);
			this.ImmuneLevelBoost = this.Create("ImmuneLevelBoost", "Immune Level Boost", DUPLICANTS.ATTRIBUTES.IMMUNITY.BOOST_MODIFIER, Db.Get().Attributes.Immunity, 0.0016666667f, 0f, new ToPercentAttributeFormatter(100f, GameUtil.TimeSlice.PerCycle), null, null);
			this.ToiletSpeed = this.Create("ToiletSpeed", "Toilet Speed", "", Db.Get().Attributes.ToiletEfficiency, 1f, -1f, toPercentAttributeFormatter, null, null);
			this.CarryAmountFromStrength = this.Create("CarryAmountFromStrength", "Carry Amount", DUPLICANTS.ATTRIBUTES.STRENGTH.CARRYMODIFIER, Db.Get().Attributes.Strength, 40f, 0f, standardAttributeFormatter, null, null);
			this.TemperatureInsulation = this.Create("TemperatureInsulation", "Temperature Insulation", DUPLICANTS.ATTRIBUTES.INSULATION.SPEEDMODIFIER, Db.Get().Attributes.Insulation, 0.1f, 0f, toPercentAttributeFormatter, null, null);
			this.SeedHarvestChance = this.Create("SeedHarvestChance", "Seed Harvest Chance", DUPLICANTS.ATTRIBUTES.BOTANIST.BONUS_SEEDS, Db.Get().Attributes.Botanist, 0.033f, 0f, toPercentAttributeFormatter, null, null);
			this.CapturableSpeed = this.Create("CapturableSpeed", "Capturable Speed", DUPLICANTS.ATTRIBUTES.RANCHING.CAPTURABLESPEED, Db.Get().Attributes.Ranching, 0.05f, 0f, toPercentAttributeFormatter, null, null);
			this.GeotuningSpeed = this.Create("GeotuningSpeed", "Geotuning Speed", DUPLICANTS.ATTRIBUTES.LEARNING.GEOTUNER_SPEED_MODIFIER, Db.Get().Attributes.Learning, 0.05f, 0f, toPercentAttributeFormatter, null, null);
			this.RanchingEffectDuration = this.Create("RanchingEffectDuration", "Ranching Effect Duration", DUPLICANTS.ATTRIBUTES.RANCHING.EFFECTMODIFIER, Db.Get().Attributes.Ranching, 0.1f, 0f, toPercentAttributeFormatter, null, null);
			this.FarmedEffectDuration = this.Create("FarmedEffectDuration", "Farmer's Touch Duration", DUPLICANTS.ATTRIBUTES.BOTANIST.TINKER_EFFECT_MODIFIER, Db.Get().Attributes.Botanist, 0.1f, 0f, toPercentAttributeFormatter, null, null);
			this.PowerTinkerEffectDuration = this.Create("PowerTinkerEffectDuration", "Engie's Tune-Up Effect Duration", DUPLICANTS.ATTRIBUTES.MACHINERY.TINKER_EFFECT_MODIFIER, Db.Get().Attributes.Machinery, 0.025f, 0f, toPercentAttributeFormatter, null, null);
		}

		// Token: 0x06007933 RID: 31027 RVA: 0x002EE1D0 File Offset: 0x002EC3D0
		public List<AttributeConverter> GetConvertersForAttribute(Klei.AI.Attribute attrib)
		{
			List<AttributeConverter> list = new List<AttributeConverter>();
			foreach (AttributeConverter attributeConverter in this.resources)
			{
				if (attributeConverter.attribute == attrib)
				{
					list.Add(attributeConverter);
				}
			}
			return list;
		}

		// Token: 0x04005433 RID: 21555
		public AttributeConverter MovementSpeed;

		// Token: 0x04005434 RID: 21556
		public AttributeConverter ConstructionSpeed;

		// Token: 0x04005435 RID: 21557
		public AttributeConverter DiggingSpeed;

		// Token: 0x04005436 RID: 21558
		public AttributeConverter MachinerySpeed;

		// Token: 0x04005437 RID: 21559
		public AttributeConverter HarvestSpeed;

		// Token: 0x04005438 RID: 21560
		public AttributeConverter PlantTendSpeed;

		// Token: 0x04005439 RID: 21561
		public AttributeConverter CompoundingSpeed;

		// Token: 0x0400543A RID: 21562
		public AttributeConverter ResearchSpeed;

		// Token: 0x0400543B RID: 21563
		public AttributeConverter TrainingSpeed;

		// Token: 0x0400543C RID: 21564
		public AttributeConverter CookingSpeed;

		// Token: 0x0400543D RID: 21565
		public AttributeConverter ArtSpeed;

		// Token: 0x0400543E RID: 21566
		public AttributeConverter DoctorSpeed;

		// Token: 0x0400543F RID: 21567
		public AttributeConverter TidyingSpeed;

		// Token: 0x04005440 RID: 21568
		public AttributeConverter AttackDamage;

		// Token: 0x04005441 RID: 21569
		public AttributeConverter PilotingSpeed;

		// Token: 0x04005442 RID: 21570
		public AttributeConverter ImmuneLevelBoost;

		// Token: 0x04005443 RID: 21571
		public AttributeConverter ToiletSpeed;

		// Token: 0x04005444 RID: 21572
		public AttributeConverter CarryAmountFromStrength;

		// Token: 0x04005445 RID: 21573
		public AttributeConverter TemperatureInsulation;

		// Token: 0x04005446 RID: 21574
		public AttributeConverter SeedHarvestChance;

		// Token: 0x04005447 RID: 21575
		public AttributeConverter RanchingEffectDuration;

		// Token: 0x04005448 RID: 21576
		public AttributeConverter FarmedEffectDuration;

		// Token: 0x04005449 RID: 21577
		public AttributeConverter PowerTinkerEffectDuration;

		// Token: 0x0400544A RID: 21578
		public AttributeConverter CapturableSpeed;

		// Token: 0x0400544B RID: 21579
		public AttributeConverter GeotuningSpeed;
	}
}
