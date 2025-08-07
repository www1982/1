using System;

namespace STRINGS
{
	// Token: 0x02000FAF RID: 4015
	public class ELEMENTS
	{
		// Token: 0x04005DB4 RID: 23988
		public static LocString ELEMENTDESCSOLID = "Resource Type: {0}\nMelting point: {1}\nHardness: {2}";

		// Token: 0x04005DB5 RID: 23989
		public static LocString ELEMENTDESCLIQUID = "Resource Type: {0}\nFreezing point: {1}\nEvaporation point: {2}";

		// Token: 0x04005DB6 RID: 23990
		public static LocString ELEMENTDESCGAS = "Resource Type: {0}\nCondensation point: {1}";

		// Token: 0x04005DB7 RID: 23991
		public static LocString ELEMENTDESCVACUUM = "Resource Type: {0}";

		// Token: 0x04005DB8 RID: 23992
		public static LocString BREATHABLEDESC = "<color=#{0}>({1})</color>";

		// Token: 0x04005DB9 RID: 23993
		public static LocString THERMALPROPERTIES = "\nSpecific Heat Capacity: {SPECIFIC_HEAT_CAPACITY}\nThermal Conductivity: {THERMAL_CONDUCTIVITY}";

		// Token: 0x04005DBA RID: 23994
		public static LocString RADIATIONPROPERTIES = "Radiation Absorption Factor: {0}\nRadiation Emission/1000kg: {1}";

		// Token: 0x04005DBB RID: 23995
		public static LocString ELEMENTPROPERTIES = "Properties: {0}";

		// Token: 0x0200243D RID: 9277
		public class STATE
		{
			// Token: 0x0400A426 RID: 42022
			public static LocString SOLID = "Solid";

			// Token: 0x0400A427 RID: 42023
			public static LocString LIQUID = "Liquid";

			// Token: 0x0400A428 RID: 42024
			public static LocString GAS = "Gas";

			// Token: 0x0400A429 RID: 42025
			public static LocString VACUUM = "None";
		}

		// Token: 0x0200243E RID: 9278
		public class MATERIAL_MODIFIERS
		{
			// Token: 0x0400A42A RID: 42026
			public static LocString EFFECTS_HEADER = "<b>Resource Effects:</b>";

			// Token: 0x0400A42B RID: 42027
			public static LocString DECOR = UI.FormatAsLink("Decor", "DECOR") + ": {0}";

			// Token: 0x0400A42C RID: 42028
			public static LocString OVERHEATTEMPERATURE = UI.FormatAsLink("Overheat Temperature", "HEAT") + ": {0}";

			// Token: 0x0400A42D RID: 42029
			public static LocString HIGH_THERMAL_CONDUCTIVITY = UI.FormatAsLink("High Thermal Conductivity", "HEAT");

			// Token: 0x0400A42E RID: 42030
			public static LocString LOW_THERMAL_CONDUCTIVITY = UI.FormatAsLink("Insulator", "HEAT");

			// Token: 0x0400A42F RID: 42031
			public static LocString LOW_SPECIFIC_HEAT_CAPACITY = UI.FormatAsLink("Thermally Reactive", "HEAT");

			// Token: 0x0400A430 RID: 42032
			public static LocString HIGH_SPECIFIC_HEAT_CAPACITY = UI.FormatAsLink("Slow Heating", "HEAT");

			// Token: 0x0400A431 RID: 42033
			public static LocString EXCELLENT_RADIATION_SHIELD = UI.FormatAsLink("Excellent Radiation Shield", "RADIATION");

			// Token: 0x02003792 RID: 14226
			public class TOOLTIP
			{
				// Token: 0x0400E14D RID: 57677
				public static LocString EFFECTS_HEADER = "Buildings constructed from this material will have these properties";

				// Token: 0x0400E14E RID: 57678
				public static LocString DECOR = "This material will add <b>{0}</b> to the finished building's " + UI.PRE_KEYWORD + "Decor" + UI.PST_KEYWORD;

				// Token: 0x0400E14F RID: 57679
				public static LocString OVERHEATTEMPERATURE = "This material will add <b>{0}</b> to the finished building's " + UI.PRE_KEYWORD + "Overheat Temperature" + UI.PST_KEYWORD;

				// Token: 0x0400E150 RID: 57680
				public static LocString HIGH_THERMAL_CONDUCTIVITY = string.Concat(new string[]
				{
					"This material disperses ",
					UI.PRE_KEYWORD,
					"Heat",
					UI.PST_KEYWORD,
					" because energy transfers quickly through materials with high ",
					UI.PRE_KEYWORD,
					"Thermal Conductivity",
					UI.PST_KEYWORD,
					"\n\nBetween two objects, the rate of ",
					UI.PRE_KEYWORD,
					"Heat",
					UI.PST_KEYWORD,
					" transfer will be determined by the object with the <i>lowest</i> ",
					UI.PRE_KEYWORD,
					"Thermal Conductivity",
					UI.PST_KEYWORD,
					"\n\nThermal Conductivity: {1} W per degree K difference (Oxygen: 0.024 W)"
				});

				// Token: 0x0400E151 RID: 57681
				public static LocString LOW_THERMAL_CONDUCTIVITY = string.Concat(new string[]
				{
					"This material retains ",
					UI.PRE_KEYWORD,
					"Heat",
					UI.PST_KEYWORD,
					" because energy transfers slowly through materials with low ",
					UI.PRE_KEYWORD,
					"Thermal Conductivity",
					UI.PST_KEYWORD,
					"\n\nBetween two objects, the rate of ",
					UI.PRE_KEYWORD,
					"Heat",
					UI.PST_KEYWORD,
					" transfer will be determined by the object with the <i>lowest</i> ",
					UI.PRE_KEYWORD,
					"Thermal Conductivity",
					UI.PST_KEYWORD,
					"\n\nThermal Conductivity: {1} W per degree K difference (Oxygen: 0.024 W)"
				});

				// Token: 0x0400E152 RID: 57682
				public static LocString LOW_SPECIFIC_HEAT_CAPACITY = string.Concat(new string[]
				{
					UI.PRE_KEYWORD,
					"Thermally Reactive",
					UI.PST_KEYWORD,
					" materials require little energy to raise in ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					", and therefore heat and cool quickly\n\nSpecific Heat Capacity: {1} DTU to raise 1g by 1K"
				});

				// Token: 0x0400E153 RID: 57683
				public static LocString HIGH_SPECIFIC_HEAT_CAPACITY = string.Concat(new string[]
				{
					UI.PRE_KEYWORD,
					"Slow Heating",
					UI.PST_KEYWORD,
					" materials require a large amount of energy to raise in ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					", and therefore heat and cool slowly\n\nSpecific Heat Capacity: {1} DTU to raise 1g by 1K"
				});

				// Token: 0x0400E154 RID: 57684
				public static LocString EXCELLENT_RADIATION_SHIELD = string.Concat(new string[]
				{
					UI.PRE_KEYWORD,
					"Excellent Radiation Shield",
					UI.PST_KEYWORD,
					" radiation has a hard time passing through materials with a high ",
					UI.PRE_KEYWORD,
					"Radiation Absorption Factor",
					UI.PST_KEYWORD,
					" value. \n\nRadiation Absorption Factor: {1}"
				});
			}
		}

		// Token: 0x0200243F RID: 9279
		public class HARDNESS
		{
			// Token: 0x0400A432 RID: 42034
			public static LocString NA = "N/A";

			// Token: 0x0400A433 RID: 42035
			public static LocString SOFT = "{0} (" + ELEMENTS.HARDNESS.HARDNESS_DESCRIPTOR.SOFT + ")";

			// Token: 0x0400A434 RID: 42036
			public static LocString VERYSOFT = "{0} (" + ELEMENTS.HARDNESS.HARDNESS_DESCRIPTOR.VERYSOFT + ")";

			// Token: 0x0400A435 RID: 42037
			public static LocString FIRM = "{0} (" + ELEMENTS.HARDNESS.HARDNESS_DESCRIPTOR.FIRM + ")";

			// Token: 0x0400A436 RID: 42038
			public static LocString VERYFIRM = "{0} (" + ELEMENTS.HARDNESS.HARDNESS_DESCRIPTOR.VERYFIRM + ")";

			// Token: 0x0400A437 RID: 42039
			public static LocString NEARLYIMPENETRABLE = "{0} (" + ELEMENTS.HARDNESS.HARDNESS_DESCRIPTOR.NEARLYIMPENETRABLE + ")";

			// Token: 0x0400A438 RID: 42040
			public static LocString IMPENETRABLE = "{0} (" + ELEMENTS.HARDNESS.HARDNESS_DESCRIPTOR.IMPENETRABLE + ")";

			// Token: 0x02003793 RID: 14227
			public class HARDNESS_DESCRIPTOR
			{
				// Token: 0x0400E155 RID: 57685
				public static LocString SOFT = "Soft";

				// Token: 0x0400E156 RID: 57686
				public static LocString VERYSOFT = "Very Soft";

				// Token: 0x0400E157 RID: 57687
				public static LocString FIRM = "Firm";

				// Token: 0x0400E158 RID: 57688
				public static LocString VERYFIRM = "Very Firm";

				// Token: 0x0400E159 RID: 57689
				public static LocString NEARLYIMPENETRABLE = "Nearly Impenetrable";

				// Token: 0x0400E15A RID: 57690
				public static LocString IMPENETRABLE = "Impenetrable";
			}
		}

		// Token: 0x02002440 RID: 9280
		public class AEROGEL
		{
			// Token: 0x0400A439 RID: 42041
			public static LocString NAME = UI.FormatAsLink("Aerogel", "AEROGEL");

			// Token: 0x0400A43A RID: 42042
			public static LocString DESC = "";
		}

		// Token: 0x02002441 RID: 9281
		public class ALGAE
		{
			// Token: 0x0400A43B RID: 42043
			public static LocString NAME = UI.FormatAsLink("Algae", "ALGAE");

			// Token: 0x0400A43C RID: 42044
			public static LocString DESC = string.Concat(new string[]
			{
				"Algae is a cluster of non-motile, single-celled lifeforms.\n\nIt can be used to produce ",
				ELEMENTS.OXYGEN.NAME,
				" when used in an ",
				BUILDINGS.PREFABS.MINERALDEOXIDIZER.NAME,
				"."
			});
		}

		// Token: 0x02002442 RID: 9282
		public class ALUMINUMORE
		{
			// Token: 0x0400A43D RID: 42045
			public static LocString NAME = UI.FormatAsLink("Aluminum Ore", "ALUMINUMORE");

			// Token: 0x0400A43E RID: 42046
			public static LocString DESC = "Aluminum ore, also known as Bauxite, is a sedimentary rock high in metal content.\n\nIt can be refined into " + UI.FormatAsLink("Aluminum", "ALUMINUM") + ".";
		}

		// Token: 0x02002443 RID: 9283
		public class ALUMINUM
		{
			// Token: 0x0400A43F RID: 42047
			public static LocString NAME = UI.FormatAsLink("Aluminum", "ALUMINUM");

			// Token: 0x0400A440 RID: 42048
			public static LocString DESC = string.Concat(new string[]
			{
				"(Al) Aluminum is a low density ",
				UI.FormatAsLink("Metal", "REFINEDMETAL"),
				".\n\nIt has high Thermal Conductivity and is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x02002444 RID: 9284
		public class MOLTENALUMINUM
		{
			// Token: 0x0400A441 RID: 42049
			public static LocString NAME = UI.FormatAsLink("Molten Aluminum", "MOLTENALUMINUM");

			// Token: 0x0400A442 RID: 42050
			public static LocString DESC = string.Concat(new string[]
			{
				"(Al) Molten Aluminum is a low density ",
				UI.FormatAsLink("Metal", "REFINEDMETAL"),
				" heated into a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x02002445 RID: 9285
		public class ALUMINUMGAS
		{
			// Token: 0x0400A443 RID: 42051
			public static LocString NAME = UI.FormatAsLink("Aluminum Gas", "ALUMINUMGAS");

			// Token: 0x0400A444 RID: 42052
			public static LocString DESC = string.Concat(new string[]
			{
				"(Al) Aluminum Gas is a low density ",
				UI.FormatAsLink("Metal", "REFINEDMETAL"),
				" heated into a ",
				UI.FormatAsLink("Gaseous", "ELEMENTS_GAS"),
				" state."
			});
		}

		// Token: 0x02002446 RID: 9286
		public class BLEACHSTONE
		{
			// Token: 0x0400A445 RID: 42053
			public static LocString NAME = UI.FormatAsLink("Bleach Stone", "BLEACHSTONE");

			// Token: 0x0400A446 RID: 42054
			public static LocString DESC = string.Concat(new string[]
			{
				"Bleach stone is an unstable compound that emits unbreathable ",
				UI.FormatAsLink("Chlorine Gas", "CHLORINEGAS"),
				".\n\nIt is often used in ",
				UI.FormatAsLink("Hygienic", "HANDSANITIZER"),
				" processes."
			});
		}

		// Token: 0x02002447 RID: 9287
		public class BITUMEN
		{
			// Token: 0x0400A447 RID: 42055
			public static LocString NAME = UI.FormatAsLink("Bitumen", "BITUMEN");

			// Token: 0x0400A448 RID: 42056
			public static LocString DESC = "Bitumen is a sticky viscous residue left behind from " + ELEMENTS.PETROLEUM.NAME + " production.";
		}

		// Token: 0x02002448 RID: 9288
		public class BOTTLEDWATER
		{
			// Token: 0x0400A449 RID: 42057
			public static LocString NAME = UI.FormatAsLink("Water", "BOTTLEDWATER");

			// Token: 0x0400A44A RID: 42058
			public static LocString DESC = "(H<sub>2</sub>O) Clean " + ELEMENTS.WATER.NAME + ", prepped for transport.";
		}

		// Token: 0x02002449 RID: 9289
		public class BRINEICE
		{
			// Token: 0x0400A44B RID: 42059
			public static LocString NAME = UI.FormatAsLink("Brine Ice", "BRINEICE");

			// Token: 0x0400A44C RID: 42060
			public static LocString DESC = string.Concat(new string[]
			{
				"Brine Ice is a natural, highly concentrated solution of ",
				UI.FormatAsLink("Salt", "SALT"),
				" dissolved in ",
				UI.FormatAsLink("Water", "WATER"),
				" and frozen into a ",
				UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
				" state.\n\nIt can be used in desalination processes, separating out usable salt."
			});
		}

		// Token: 0x0200244A RID: 9290
		public class MILKICE
		{
			// Token: 0x0400A44D RID: 42061
			public static LocString NAME = UI.FormatAsLink("Frozen Brackene", "MILKICE");

			// Token: 0x0400A44E RID: 42062
			public static LocString DESC = string.Concat(new string[]
			{
				"Frozen Brackene is ",
				UI.FormatAsLink("Brackene", "MILK"),
				" frozen into a ",
				UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
				" state."
			});
		}

		// Token: 0x0200244B RID: 9291
		public class BRINE
		{
			// Token: 0x0400A44F RID: 42063
			public static LocString NAME = UI.FormatAsLink("Brine", "BRINE");

			// Token: 0x0400A450 RID: 42064
			public static LocString DESC = string.Concat(new string[]
			{
				"Brine is a natural, highly concentrated solution of ",
				UI.FormatAsLink("Salt", "SALT"),
				" dissolved in ",
				UI.FormatAsLink("Water", "WATER"),
				".\n\nIt can be used in desalination processes, separating out usable salt."
			});
		}

		// Token: 0x0200244C RID: 9292
		public class CARBON
		{
			// Token: 0x0400A451 RID: 42065
			public static LocString NAME = UI.FormatAsLink("Coal", "CARBON");

			// Token: 0x0400A452 RID: 42066
			public static LocString DESC = "(C) Coal is a combustible fossil fuel composed of carbon.\n\nIt is useful in " + UI.FormatAsLink("Power", "POWER") + " production.";
		}

		// Token: 0x0200244D RID: 9293
		public class REFINEDCARBON
		{
			// Token: 0x0400A453 RID: 42067
			public static LocString NAME = UI.FormatAsLink("Refined Carbon", "REFINEDCARBON");

			// Token: 0x0400A454 RID: 42068
			public static LocString DESC = "(C) Refined carbon is solid element purified from raw " + ELEMENTS.CARBON.NAME + ".";
		}

		// Token: 0x0200244E RID: 9294
		public class PEAT
		{
			// Token: 0x0400A455 RID: 42069
			public static LocString NAME = UI.FormatAsLink("Peat", "PEAT");

			// Token: 0x0400A456 RID: 42070
			public static LocString DESC = "Peat is a densely packed material made up of partially decomposed organic matter.\n\nIt is a combustible fuel, useful in " + UI.FormatAsLink("Power", "POWER") + " production.";
		}

		// Token: 0x0200244F RID: 9295
		public class ETHANOLGAS
		{
			// Token: 0x0400A457 RID: 42071
			public static LocString NAME = UI.FormatAsLink("Ethanol Gas", "ETHANOLGAS");

			// Token: 0x0400A458 RID: 42072
			public static LocString DESC = "(C<sub>2</sub>H<sub>6</sub>O) Ethanol Gas is an advanced chemical compound heated into a " + UI.FormatAsLink("Gaseous", "ELEMENTS_GAS") + " state.";
		}

		// Token: 0x02002450 RID: 9296
		public class ETHANOL
		{
			// Token: 0x0400A459 RID: 42073
			public static LocString NAME = UI.FormatAsLink("Ethanol", "ETHANOL");

			// Token: 0x0400A45A RID: 42074
			public static LocString DESC = "(C<sub>2</sub>H<sub>6</sub>O) Ethanol is an advanced chemical compound in a " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " state.\n\nIt can be used as a highly effective fuel source when burned.";
		}

		// Token: 0x02002451 RID: 9297
		public class SOLIDETHANOL
		{
			// Token: 0x0400A45B RID: 42075
			public static LocString NAME = UI.FormatAsLink("Solid Ethanol", "SOLIDETHANOL");

			// Token: 0x0400A45C RID: 42076
			public static LocString DESC = "(C<sub>2</sub>H<sub>6</sub>O) Solid Ethanol is an advanced chemical compound.\n\nIt can be used as a highly effective fuel source when burned.";
		}

		// Token: 0x02002452 RID: 9298
		public class CARBONDIOXIDE
		{
			// Token: 0x0400A45D RID: 42077
			public static LocString NAME = UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE");

			// Token: 0x0400A45E RID: 42078
			public static LocString DESC = "(CO<sub>2</sub>) Carbon Dioxide is an atomically heavy chemical compound in a " + UI.FormatAsLink("Gaseous", "ELEMENTS_GAS") + " state.\n\nIt tends to sink below other gases.";
		}

		// Token: 0x02002453 RID: 9299
		public class CARBONFIBRE
		{
			// Token: 0x0400A45F RID: 42079
			public static LocString NAME = UI.FormatAsLink("Carbon Fiber", "CARBONFIBRE");

			// Token: 0x0400A460 RID: 42080
			public static LocString DESC = "Carbon Fiber is a " + UI.FormatAsLink("Manufactured Material", "REFINEDMINERAL") + " with high tensile strength.";
		}

		// Token: 0x02002454 RID: 9300
		public class CARBONGAS
		{
			// Token: 0x0400A461 RID: 42081
			public static LocString NAME = UI.FormatAsLink("Carbon Gas", "CARBONGAS");

			// Token: 0x0400A462 RID: 42082
			public static LocString DESC = "(C) Carbon is an abundant, versatile element heated into a " + UI.FormatAsLink("Gaseous", "ELEMENTS_GAS") + " state.";
		}

		// Token: 0x02002455 RID: 9301
		public class CHLORINE
		{
			// Token: 0x0400A463 RID: 42083
			public static LocString NAME = UI.FormatAsLink("Liquid Chlorine", "CHLORINE");

			// Token: 0x0400A464 RID: 42084
			public static LocString DESC = string.Concat(new string[]
			{
				"(Cl) Chlorine is a natural ",
				UI.FormatAsLink("Germ", "DISEASE"),
				"-killing element in a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x02002456 RID: 9302
		public class CHLORINEGAS
		{
			// Token: 0x0400A465 RID: 42085
			public static LocString NAME = UI.FormatAsLink("Chlorine Gas", "CHLORINEGAS");

			// Token: 0x0400A466 RID: 42086
			public static LocString DESC = string.Concat(new string[]
			{
				"(Cl) Chlorine is a natural ",
				UI.FormatAsLink("Germ", "DISEASE"),
				"-killing element in a ",
				UI.FormatAsLink("Gaseous", "ELEMENTS_GAS"),
				" state."
			});
		}

		// Token: 0x02002457 RID: 9303
		public class CLAY
		{
			// Token: 0x0400A467 RID: 42087
			public static LocString NAME = UI.FormatAsLink("Clay", "CLAY");

			// Token: 0x0400A468 RID: 42088
			public static LocString DESC = "Clay is a soft, naturally occurring composite of stone and soil that hardens at high " + UI.FormatAsLink("Temperatures", "HEAT") + ".\n\nIt is a reliable <b>Construction Material</b>.";
		}

		// Token: 0x02002458 RID: 9304
		public class BRICK
		{
			// Token: 0x0400A469 RID: 42089
			public static LocString NAME = UI.FormatAsLink("Brick", "BRICK");

			// Token: 0x0400A46A RID: 42090
			public static LocString DESC = "Brick is a hard, brittle material formed from heated " + ELEMENTS.CLAY.NAME + ".\n\nIt is a reliable <b>Construction Material</b>.";
		}

		// Token: 0x02002459 RID: 9305
		public class CERAMIC
		{
			// Token: 0x0400A46B RID: 42091
			public static LocString NAME = UI.FormatAsLink("Ceramic", "CERAMIC");

			// Token: 0x0400A46C RID: 42092
			public static LocString DESC = "Ceramic is a hard, brittle material formed from heated " + ELEMENTS.CLAY.NAME + ".\n\nIt is a reliable <b>Construction Material</b>.";
		}

		// Token: 0x0200245A RID: 9306
		public class CEMENT
		{
			// Token: 0x0400A46D RID: 42093
			public static LocString NAME = UI.FormatAsLink("Cement", "CEMENT");

			// Token: 0x0400A46E RID: 42094
			public static LocString DESC = "Cement is a refined building material used for assembling advanced buildings.";
		}

		// Token: 0x0200245B RID: 9307
		public class CEMENTMIX
		{
			// Token: 0x0400A46F RID: 42095
			public static LocString NAME = UI.FormatAsLink("Cement Mix", "CEMENTMIX");

			// Token: 0x0400A470 RID: 42096
			public static LocString DESC = "Cement Mix can be used to create " + ELEMENTS.CEMENT.NAME + " for advanced building assembly.";
		}

		// Token: 0x0200245C RID: 9308
		public class CONTAMINATEDOXYGEN
		{
			// Token: 0x0400A471 RID: 42097
			public static LocString NAME = UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN");

			// Token: 0x0400A472 RID: 42098
			public static LocString DESC = "(O<sub>2</sub>) Polluted Oxygen is dirty, unfiltered air.\n\nIt is breathable.";
		}

		// Token: 0x0200245D RID: 9309
		public class COPPER
		{
			// Token: 0x0400A473 RID: 42099
			public static LocString NAME = UI.FormatAsLink("Copper", "COPPER");

			// Token: 0x0400A474 RID: 42100
			public static LocString DESC = string.Concat(new string[]
			{
				"(Cu) Copper is a conductive ",
				UI.FormatAsLink("Metal", "METAL"),
				".\n\nIt is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x0200245E RID: 9310
		public class COPPERGAS
		{
			// Token: 0x0400A475 RID: 42101
			public static LocString NAME = UI.FormatAsLink("Copper Gas", "COPPERGAS");

			// Token: 0x0400A476 RID: 42102
			public static LocString DESC = string.Concat(new string[]
			{
				"(Cu) Copper Gas is a conductive ",
				UI.FormatAsLink("Metal", "METAL"),
				" heated into a ",
				UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
				" state."
			});
		}

		// Token: 0x0200245F RID: 9311
		public class NICKELORE
		{
			// Token: 0x0400A477 RID: 42103
			public static LocString NAME = UI.FormatAsLink("Nickel Ore", "NICKELORE");

			// Token: 0x0400A478 RID: 42104
			public static LocString DESC = string.Concat(new string[]
			{
				"(Ni) Nickel Ore is a conductive ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				".\n\nIt can be refined into ",
				UI.FormatAsLink("Nickel", "NICKEL"),
				" and is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x02002460 RID: 9312
		public class NICKEL
		{
			// Token: 0x0400A479 RID: 42105
			public static LocString NAME = UI.FormatAsLink("Nickel", "NICKEL");

			// Token: 0x0400A47A RID: 42106
			public static LocString DESC = string.Concat(new string[]
			{
				"(Ni) Nickel is a conductive ",
				UI.FormatAsLink("Metal", "METAL"),
				".\n\nIt is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x02002461 RID: 9313
		public class MOLTENNICKEL
		{
			// Token: 0x0400A47B RID: 42107
			public static LocString NAME = UI.FormatAsLink("Molten Nickel", "MOLTENNICKEL");

			// Token: 0x0400A47C RID: 42108
			public static LocString DESC = string.Concat(new string[]
			{
				"(Ni) Molten Nickel is a conductive ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				" heated into a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x02002462 RID: 9314
		public class NICKELGAS
		{
			// Token: 0x0400A47D RID: 42109
			public static LocString NAME = UI.FormatAsLink("Nickel Gas", "NICKELGAS");

			// Token: 0x0400A47E RID: 42110
			public static LocString DESC = string.Concat(new string[]
			{
				"(Ni) Nickel Gas is a conductive ",
				UI.FormatAsLink("Metal", "METAL"),
				" heated into a ",
				UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
				" state."
			});
		}

		// Token: 0x02002463 RID: 9315
		public class CREATURE
		{
			// Token: 0x0400A47F RID: 42111
			public static LocString NAME = UI.FormatAsLink("Genetic Ooze", "CREATURE");

			// Token: 0x0400A480 RID: 42112
			public static LocString DESC = "(DuPe) Ooze is a slurry of water, carbon, and dozens and dozens of trace elements.\n\nDuplicants are printed from pure Ooze.";
		}

		// Token: 0x02002464 RID: 9316
		public class PHYTOOIL
		{
			// Token: 0x0400A481 RID: 42113
			public static LocString NAME = UI.FormatAsLink("Phyto Oil", "PHYTOOIL");

			// Token: 0x0400A482 RID: 42114
			public static LocString DESC = string.Concat(new string[]
			{
				"Phyto Oil is a thick, slippery ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" extracted from pureed ",
				UI.FormatAsLink("Slime", "SLIME"),
				"."
			});
		}

		// Token: 0x02002465 RID: 9317
		public class REFINEDLIPID
		{
			// Token: 0x0400A483 RID: 42115
			public static LocString NAME = UI.FormatAsLink("Biodiesel", "REFINEDLIPID");

			// Token: 0x0400A484 RID: 42116
			public static LocString DESC = "Biodiesel is a a " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " composed of highly processed fatty acids derived from purified natural oils.";
		}

		// Token: 0x02002466 RID: 9318
		public class FROZENPHYTOOIL
		{
			// Token: 0x0400A485 RID: 42117
			public static LocString NAME = UI.FormatAsLink("Frozen Phyto Oil", "FROZENPHYTOOIL");

			// Token: 0x0400A486 RID: 42118
			public static LocString DESC = string.Concat(new string[]
			{
				"Frozen Phyto Oil is thick, slippery ",
				UI.FormatAsLink("Slime", "SLIME"),
				" extract, frozen into a ",
				UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
				" state."
			});
		}

		// Token: 0x02002467 RID: 9319
		public class CRUDEOIL
		{
			// Token: 0x0400A487 RID: 42119
			public static LocString NAME = UI.FormatAsLink("Crude Oil", "CRUDEOIL");

			// Token: 0x0400A488 RID: 42120
			public static LocString DESC = "Crude Oil is a raw potential " + UI.FormatAsLink("Power", "POWER") + " source composed of billions of dead, primordial organisms.\n\nIt is also a useful lubricant for certain types of machinery.";
		}

		// Token: 0x02002468 RID: 9320
		public class PETROLEUM
		{
			// Token: 0x0400A489 RID: 42121
			public static LocString NAME = UI.FormatAsLink("Petroleum", "PETROLEUM");

			// Token: 0x0400A48A RID: 42122
			public static LocString NAME_TWO = UI.FormatAsLink("Petroleum", "PETROLEUM");

			// Token: 0x0400A48B RID: 42123
			public static LocString DESC = string.Concat(new string[]
			{
				"Petroleum is a ",
				UI.FormatAsLink("Power", "POWER"),
				" source refined from ",
				UI.FormatAsLink("Crude Oil", "CRUDEOIL"),
				".\n\nIt is also an essential ingredient in the production of ",
				UI.FormatAsLink("Plastic", "POLYPROPYLENE"),
				"."
			});
		}

		// Token: 0x02002469 RID: 9321
		public class SOURGAS
		{
			// Token: 0x0400A48C RID: 42124
			public static LocString NAME = UI.FormatAsLink("Sour Gas", "SOURGAS");

			// Token: 0x0400A48D RID: 42125
			public static LocString NAME_TWO = UI.FormatAsLink("Sour Gas", "SOURGAS");

			// Token: 0x0400A48E RID: 42126
			public static LocString DESC = string.Concat(new string[]
			{
				"Sour Gas is a hydrocarbon ",
				UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
				" containing high concentrations of hydrogen sulfide.\n\nIt is a byproduct of highly heated ",
				UI.FormatAsLink("Petroleum", "PETROLEUM"),
				"."
			});
		}

		// Token: 0x0200246A RID: 9322
		public class CRUSHEDICE
		{
			// Token: 0x0400A48F RID: 42127
			public static LocString NAME = UI.FormatAsLink("Crushed Ice", "CRUSHEDICE");

			// Token: 0x0400A490 RID: 42128
			public static LocString DESC = "(H<sub>2</sub>O) A slush of crushed, semi-solid ice.";
		}

		// Token: 0x0200246B RID: 9323
		public class CRUSHEDROCK
		{
			// Token: 0x0400A491 RID: 42129
			public static LocString NAME = UI.FormatAsLink("Crushed Rock", "CRUSHEDROCK");

			// Token: 0x0400A492 RID: 42130
			public static LocString DESC = "Crushed Rock is " + UI.FormatAsLink("Igneous Rock", "IGNEOUSROCK") + " crushed into a mechanical mixture.";
		}

		// Token: 0x0200246C RID: 9324
		public class CUPRITE
		{
			// Token: 0x0400A493 RID: 42131
			public static LocString NAME = UI.FormatAsLink("Copper Ore", "CUPRITE");

			// Token: 0x0400A494 RID: 42132
			public static LocString DESC = string.Concat(new string[]
			{
				"(Cu<sub>2</sub>O) Copper Ore is a conductive ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				".\n\nIt is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x0200246D RID: 9325
		public class DEPLETEDURANIUM
		{
			// Token: 0x0400A495 RID: 42133
			public static LocString NAME = UI.FormatAsLink("Depleted Uranium", "DEPLETEDURANIUM");

			// Token: 0x0400A496 RID: 42134
			public static LocString DESC = string.Concat(new string[]
			{
				"(U) Depleted Uranium is ",
				UI.FormatAsLink("Uranium", "URANIUMORE"),
				" with a low U-235 content.\n\nIt is created as a byproduct of ",
				UI.FormatAsLink("Enriched Uranium", "ENRICHEDURANIUM"),
				" and is no longer suitable as fuel."
			});
		}

		// Token: 0x0200246E RID: 9326
		public class DIAMOND
		{
			// Token: 0x0400A497 RID: 42135
			public static LocString NAME = UI.FormatAsLink("Diamond", "DIAMOND");

			// Token: 0x0400A498 RID: 42136
			public static LocString DESC = "(C) Diamond is industrial-grade, high density carbon.\n\nIt is very difficult to excavate.";
		}

		// Token: 0x0200246F RID: 9327
		public class DIRT
		{
			// Token: 0x0400A499 RID: 42137
			public static LocString NAME = UI.FormatAsLink("Dirt", "DIRT");

			// Token: 0x0400A49A RID: 42138
			public static LocString DESC = "Dirt is a soft, nutrient-rich substance capable of supporting life.\n\nIt is necessary in some forms of " + UI.FormatAsLink("Food", "FOOD") + " production.";
		}

		// Token: 0x02002470 RID: 9328
		public class DIRTYICE
		{
			// Token: 0x0400A49B RID: 42139
			public static LocString NAME = UI.FormatAsLink("Polluted Ice", "DIRTYICE");

			// Token: 0x0400A49C RID: 42140
			public static LocString DESC = "Polluted Ice is dirty, unfiltered water frozen into a " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " state.";
		}

		// Token: 0x02002471 RID: 9329
		public class DIRTYWATER
		{
			// Token: 0x0400A49D RID: 42141
			public static LocString NAME = UI.FormatAsLink("Polluted Water", "DIRTYWATER");

			// Token: 0x0400A49E RID: 42142
			public static LocString DESC = "Polluted Water is dirty, unfiltered " + UI.FormatAsLink("Water", "WATER") + ".\n\nIt is not fit for consumption.";
		}

		// Token: 0x02002472 RID: 9330
		public class ELECTRUM
		{
			// Token: 0x0400A49F RID: 42143
			public static LocString NAME = UI.FormatAsLink("Electrum", "ELECTRUM");

			// Token: 0x0400A4A0 RID: 42144
			public static LocString DESC = string.Concat(new string[]
			{
				"Electrum is a conductive ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				" alloy composed of gold and silver.\n\nIt is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x02002473 RID: 9331
		public class ENRICHEDURANIUM
		{
			// Token: 0x0400A4A1 RID: 42145
			public static LocString NAME = UI.FormatAsLink("Enriched Uranium", "ENRICHEDURANIUM");

			// Token: 0x0400A4A2 RID: 42146
			public static LocString DESC = string.Concat(new string[]
			{
				"(U) Enriched Uranium is a refined substance primarily used to ",
				UI.FormatAsLink("Power", "POWER"),
				" potent research reactors.\n\nIt becomes highly ",
				UI.FormatAsLink("Radioactive", "RADIATION"),
				" when consumed."
			});
		}

		// Token: 0x02002474 RID: 9332
		public class FERTILIZER
		{
			// Token: 0x0400A4A3 RID: 42147
			public static LocString NAME = UI.FormatAsLink("Fertilizer", "FERTILIZER");

			// Token: 0x0400A4A4 RID: 42148
			public static LocString DESC = "Fertilizer is a processed mixture of biological nutrients.\n\nIt aids in the growth of certain " + UI.FormatAsLink("Plants", "PLANTS") + ".";
		}

		// Token: 0x02002475 RID: 9333
		public class PONDSCUM
		{
			// Token: 0x0400A4A5 RID: 42149
			public static LocString NAME = UI.FormatAsLink("Pondscum", "PONDSCUM");

			// Token: 0x0400A4A6 RID: 42150
			public static LocString DESC = string.Concat(new string[]
			{
				"Pondscum is a soft, naturally occurring composite of biological nutrients.\n\nIt may be processed into ",
				UI.FormatAsLink("Fertilizer", "FERTILIZER"),
				" and aids in the growth of certain ",
				UI.FormatAsLink("Plants", "PLANTS"),
				"."
			});
		}

		// Token: 0x02002476 RID: 9334
		public class FALLOUT
		{
			// Token: 0x0400A4A7 RID: 42151
			public static LocString NAME = UI.FormatAsLink("Nuclear Fallout", "FALLOUT");

			// Token: 0x0400A4A8 RID: 42152
			public static LocString DESC = string.Concat(new string[]
			{
				"Nuclear Fallout is a highly toxic gas full of ",
				UI.FormatAsLink("Radioactive Contaminants", "RADIATION"),
				". Condenses into ",
				UI.FormatAsLink("Liquid Nuclear Waste", "NUCLEARWASTE"),
				"."
			});
		}

		// Token: 0x02002477 RID: 9335
		public class FOOLSGOLD
		{
			// Token: 0x0400A4A9 RID: 42153
			public static LocString NAME = UI.FormatAsLink("Pyrite", "FOOLSGOLD");

			// Token: 0x0400A4AA RID: 42154
			public static LocString DESC = string.Concat(new string[]
			{
				"(FeS<sub>2</sub>) Pyrite is a conductive ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				".\n\nAlso known as \"Fool's Gold\", is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x02002478 RID: 9336
		public class FULLERENE
		{
			// Token: 0x0400A4AB RID: 42155
			public static LocString NAME = UI.FormatAsLink("Fullerene", "FULLERENE");

			// Token: 0x0400A4AC RID: 42156
			public static LocString DESC = "(C<sub>60</sub>) Fullerene is a form of " + UI.FormatAsLink("Coal", "CARBON") + " consisting of spherical molecules.";
		}

		// Token: 0x02002479 RID: 9337
		public class GLASS
		{
			// Token: 0x0400A4AD RID: 42157
			public static LocString NAME = UI.FormatAsLink("Glass", "GLASS");

			// Token: 0x0400A4AE RID: 42158
			public static LocString DESC = "Glass is a brittle, transparent substance formed from " + UI.FormatAsLink("Sand", "SAND") + " fired at high temperatures.";
		}

		// Token: 0x0200247A RID: 9338
		public class GOLD
		{
			// Token: 0x0400A4AF RID: 42159
			public static LocString NAME = UI.FormatAsLink("Gold", "GOLD");

			// Token: 0x0400A4B0 RID: 42160
			public static LocString DESC = string.Concat(new string[]
			{
				"(Au) Gold is a conductive precious ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				".\n\nIt is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x0200247B RID: 9339
		public class GOLDAMALGAM
		{
			// Token: 0x0400A4B1 RID: 42161
			public static LocString NAME = UI.FormatAsLink("Gold Amalgam", "GOLDAMALGAM");

			// Token: 0x0400A4B2 RID: 42162
			public static LocString DESC = "Gold Amalgam is a conductive amalgam of gold and mercury.\n\nIt is suitable for building " + UI.FormatAsLink("Power", "POWER") + " systems.";
		}

		// Token: 0x0200247C RID: 9340
		public class GOLDGAS
		{
			// Token: 0x0400A4B3 RID: 42163
			public static LocString NAME = UI.FormatAsLink("Gold Gas", "GOLDGAS");

			// Token: 0x0400A4B4 RID: 42164
			public static LocString DESC = string.Concat(new string[]
			{
				"(Au) Gold Gas is a conductive precious ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				", heated into a ",
				UI.FormatAsLink("Gaseous", "ELEMENTS_GAS"),
				" state."
			});
		}

		// Token: 0x0200247D RID: 9341
		public class GRANITE
		{
			// Token: 0x0400A4B5 RID: 42165
			public static LocString NAME = UI.FormatAsLink("Granite", "GRANITE");

			// Token: 0x0400A4B6 RID: 42166
			public static LocString DESC = "Granite is a dense composite of " + UI.FormatAsLink("Igneous Rock", "IGNEOUSROCK") + ".\n\nIt is useful as a <b>Construction Material</b>.";
		}

		// Token: 0x0200247E RID: 9342
		public class GRAPHITE
		{
			// Token: 0x0400A4B7 RID: 42167
			public static LocString NAME = UI.FormatAsLink("Graphite", "GRAPHITE");

			// Token: 0x0400A4B8 RID: 42168
			public static LocString DESC = "(C) Graphite is the most stable form of carbon.\n\nIt has high thermal conductivity and is useful as a <b>Construction Material</b>.";
		}

		// Token: 0x0200247F RID: 9343
		public class LIQUIDGUNK
		{
			// Token: 0x0400A4B9 RID: 42169
			public static LocString NAME = UI.FormatAsLink("Gunk", "LIQUIDGUNK");

			// Token: 0x0400A4BA RID: 42170
			public static LocString DESC = "Gunk is the built-up grime and grit produced by Duplicants' bionic mechanisms.\n\nIt is unpleasantly viscous.";
		}

		// Token: 0x02002480 RID: 9344
		public class GUNK
		{
			// Token: 0x0400A4BB RID: 42171
			public static LocString NAME = UI.FormatAsLink("Solid Gunk", "GUNK");

			// Token: 0x0400A4BC RID: 42172
			public static LocString DESC = "Solid Gunk is the built-up grime and grit produced by Duplicants' bionic mechanisms, which has been frozen into a " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " state.";
		}

		// Token: 0x02002481 RID: 9345
		public class SOLIDNUCLEARWASTE
		{
			// Token: 0x0400A4BD RID: 42173
			public static LocString NAME = UI.FormatAsLink("Solid Nuclear Waste", "SOLIDNUCLEARWASTE");

			// Token: 0x0400A4BE RID: 42174
			public static LocString DESC = "Highly toxic solid full of " + UI.FormatAsLink("Radioactive Contaminants", "RADIATION") + ".";
		}

		// Token: 0x02002482 RID: 9346
		public class HELIUM
		{
			// Token: 0x0400A4BF RID: 42175
			public static LocString NAME = UI.FormatAsLink("Helium", "HELIUM");

			// Token: 0x0400A4C0 RID: 42176
			public static LocString DESC = "(He) Helium is an atomically lightweight, chemical " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + ".";
		}

		// Token: 0x02002483 RID: 9347
		public class HYDROGEN
		{
			// Token: 0x0400A4C1 RID: 42177
			public static LocString NAME = UI.FormatAsLink("Hydrogen Gas", "HYDROGEN");

			// Token: 0x0400A4C2 RID: 42178
			public static LocString DESC = "(H) Hydrogen Gas is the universe's most common and atomically light element in a " + UI.FormatAsLink("Gaseous", "ELEMENTS_GAS") + " state.";
		}

		// Token: 0x02002484 RID: 9348
		public class ICE
		{
			// Token: 0x0400A4C3 RID: 42179
			public static LocString NAME = UI.FormatAsLink("Ice", "ICE");

			// Token: 0x0400A4C4 RID: 42180
			public static LocString DESC = "(H<sub>2</sub>O) Ice is clean water frozen into a " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " state.";
		}

		// Token: 0x02002485 RID: 9349
		public class IGNEOUSROCK
		{
			// Token: 0x0400A4C5 RID: 42181
			public static LocString NAME = UI.FormatAsLink("Igneous Rock", "IGNEOUSROCK");

			// Token: 0x0400A4C6 RID: 42182
			public static LocString DESC = "Igneous Rock is a composite of solidified volcanic rock.\n\nIt is useful as a <b>Construction Material</b>.";
		}

		// Token: 0x02002486 RID: 9350
		public class IRIDIUM
		{
			// Token: 0x0400A4C7 RID: 42183
			public static LocString NAME = UI.FormatAsLink("Iridium", "IRIDIUM");

			// Token: 0x0400A4C8 RID: 42184
			public static LocString DESC = string.Concat(new string[]
			{
				"(Ir) Iridium is a firm and highly conductive ",
				UI.FormatAsLink("Metal", "METAL"),
				" that can withstand extreme  ",
				UI.FormatAsLink("Heat", "HEAT"),
				"."
			});
		}

		// Token: 0x02002487 RID: 9351
		public class MOLTENIRIDIUM
		{
			// Token: 0x0400A4C9 RID: 42185
			public static LocString NAME = UI.FormatAsLink("Molten Iridium", "MOLTENIRIDIUM");

			// Token: 0x0400A4CA RID: 42186
			public static LocString DESC = string.Concat(new string[]
			{
				"(Ir) Molten Iridium is a highly conductive ",
				UI.FormatAsLink("Metal", "METAL"),
				" heated to a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				"  state."
			});
		}

		// Token: 0x02002488 RID: 9352
		public class IRIDIUMGAS
		{
			// Token: 0x0400A4CB RID: 42187
			public static LocString NAME = UI.FormatAsLink("Iridium Gas", "IRIDIUMGAS");

			// Token: 0x0400A4CC RID: 42188
			public static LocString DESC = string.Concat(new string[]
			{
				"(Ir) Iridium Gas is a highly conductive ",
				UI.FormatAsLink("Metal", "METAL"),
				" heated into a  ",
				UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
				" state."
			});
		}

		// Token: 0x02002489 RID: 9353
		public class AMBER
		{
			// Token: 0x0400A4CD RID: 42189
			public static LocString NAME = UI.FormatAsLink("Amber", "AMBER");

			// Token: 0x0400A4CE RID: 42190
			public static LocString DESC = string.Concat(new string[]
			{
				"Amber is a ",
				UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
				" organic composite of ",
				UI.FormatAsLink("Resin", "NATURALRESIN"),
				" and ",
				UI.FormatAsLink("Fossil", "FOSSIL"),
				"."
			});
		}

		// Token: 0x0200248A RID: 9354
		public class NATURALRESIN
		{
			// Token: 0x0400A4CF RID: 42191
			public static LocString NAME = UI.FormatAsLink("Resin", "NATURALRESIN");

			// Token: 0x0400A4D0 RID: 42192
			public static LocString DESC = string.Concat(new string[]
			{
				"Resin is a viscous organic ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				".\n\nIt can be treated to become ",
				UI.FormatAsLink("Plastic", "POLYPROPYLENE"),
				" in the ",
				UI.FormatAsLink("Polymer Press", "POLYMERIZER"),
				"."
			});
		}

		// Token: 0x0200248B RID: 9355
		public class NATURALSOLIDRESIN
		{
			// Token: 0x0400A4D1 RID: 42193
			public static LocString NAME = UI.FormatAsLink("Solid Resin", "NATURALSOLIDRESIN");

			// Token: 0x0400A4D2 RID: 42194
			public static LocString DESC = string.Concat(new string[]
			{
				"Resin that has been cooled to a ",
				UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
				" state.\n\nIt can be treated to become ",
				UI.FormatAsLink("Plastic", "POLYPROPYLENE"),
				" in the ",
				UI.FormatAsLink("Polymer Press", "POLYMERIZER"),
				"."
			});
		}

		// Token: 0x0200248C RID: 9356
		public class ISORESIN
		{
			// Token: 0x0400A4D3 RID: 42195
			public static LocString NAME = UI.FormatAsLink("Isosap", "ISORESIN");

			// Token: 0x0400A4D4 RID: 42196
			public static LocString DESC = "Isosap is a crystallized sap composed of long-chain polymers.\n\nIt is used in the production of rare, high grade materials.";
		}

		// Token: 0x0200248D RID: 9357
		public class RESIN
		{
			// Token: 0x0400A4D5 RID: 42197
			public static LocString NAME = UI.FormatAsLink("Sap", "RESIN");

			// Token: 0x0400A4D6 RID: 42198
			public static LocString DESC = "Sticky goo harvested from a grumpy tree.\n\nIt can be polymerized into " + UI.FormatAsLink("Isosap", "ISORESIN") + " by boiling away its excess moisture.";
		}

		// Token: 0x0200248E RID: 9358
		public class SOLIDRESIN
		{
			// Token: 0x0400A4D7 RID: 42199
			public static LocString NAME = UI.FormatAsLink("Solid Sap", "SOLIDRESIN");

			// Token: 0x0400A4D8 RID: 42200
			public static LocString DESC = "Solidified goo harvested from a grumpy tree.\n\nIt is used in the production of " + UI.FormatAsLink("Isosap", "ISORESIN") + ".";
		}

		// Token: 0x0200248F RID: 9359
		public class IRON
		{
			// Token: 0x0400A4D9 RID: 42201
			public static LocString NAME = UI.FormatAsLink("Iron", "IRON");

			// Token: 0x0400A4DA RID: 42202
			public static LocString DESC = "(Fe) Iron is a common industrial " + UI.FormatAsLink("Metal", "RAWMETAL") + ".";
		}

		// Token: 0x02002490 RID: 9360
		public class IRONGAS
		{
			// Token: 0x0400A4DB RID: 42203
			public static LocString NAME = UI.FormatAsLink("Iron Gas", "IRONGAS");

			// Token: 0x0400A4DC RID: 42204
			public static LocString DESC = string.Concat(new string[]
			{
				"(Fe) Iron Gas is a common industrial ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				", heated into a ",
				UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
				"."
			});
		}

		// Token: 0x02002491 RID: 9361
		public class IRONORE
		{
			// Token: 0x0400A4DD RID: 42205
			public static LocString NAME = UI.FormatAsLink("Iron Ore", "IRONORE");

			// Token: 0x0400A4DE RID: 42206
			public static LocString DESC = string.Concat(new string[]
			{
				"(Fe) Iron Ore is a soft ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				".\n\nIt is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x02002492 RID: 9362
		public class COBALTGAS
		{
			// Token: 0x0400A4DF RID: 42207
			public static LocString NAME = UI.FormatAsLink("Cobalt Gas", "COBALTGAS");

			// Token: 0x0400A4E0 RID: 42208
			public static LocString DESC = string.Concat(new string[]
			{
				"(Co) Cobalt is a ",
				UI.FormatAsLink("Refined Metal", "REFINEDMETAL"),
				", heated into a ",
				UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
				"."
			});
		}

		// Token: 0x02002493 RID: 9363
		public class COBALT
		{
			// Token: 0x0400A4E1 RID: 42209
			public static LocString NAME = UI.FormatAsLink("Cobalt", "COBALT");

			// Token: 0x0400A4E2 RID: 42210
			public static LocString DESC = string.Concat(new string[]
			{
				"(Co) Cobalt is a ",
				UI.FormatAsLink("Refined Metal", "REFINEDMETAL"),
				" made from ",
				UI.FormatAsLink("Cobalt Ore", "COBALTITE"),
				"."
			});
		}

		// Token: 0x02002494 RID: 9364
		public class COBALTITE
		{
			// Token: 0x0400A4E3 RID: 42211
			public static LocString NAME = UI.FormatAsLink("Cobalt Ore", "COBALTITE");

			// Token: 0x0400A4E4 RID: 42212
			public static LocString DESC = string.Concat(new string[]
			{
				"(Co) Cobalt Ore is a blue-hued ",
				UI.FormatAsLink("Metal", "BUILDINGMATERIALCLASSES"),
				".\n\nIt is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x02002495 RID: 9365
		public class KATAIRITE
		{
			// Token: 0x0400A4E5 RID: 42213
			public static LocString NAME = UI.FormatAsLink("Abyssalite", "KATAIRITE");

			// Token: 0x0400A4E6 RID: 42214
			public static LocString DESC = "(Ab) Abyssalite is a resilient, crystalline element.";
		}

		// Token: 0x02002496 RID: 9366
		public class LIME
		{
			// Token: 0x0400A4E7 RID: 42215
			public static LocString NAME = UI.FormatAsLink("Lime", "LIME");

			// Token: 0x0400A4E8 RID: 42216
			public static LocString DESC = "(CaCO<sub>3</sub>) Lime is a mineral commonly found in " + UI.FormatAsLink("Critter", "CREATURES") + " egg shells.\n\nIt is useful as a <b>Construction Material</b>.";
		}

		// Token: 0x02002497 RID: 9367
		public class FOSSIL
		{
			// Token: 0x0400A4E9 RID: 42217
			public static LocString NAME = UI.FormatAsLink("Fossil", "FOSSIL");

			// Token: 0x0400A4EA RID: 42218
			public static LocString DESC = "Fossil is organic matter, highly compressed and hardened into a mineral state.\n\nIt is useful as a <b>Construction Material</b>.";
		}

		// Token: 0x02002498 RID: 9368
		public class LEADGAS
		{
			// Token: 0x0400A4EB RID: 42219
			public static LocString NAME = UI.FormatAsLink("Lead Gas", "LEADGAS");

			// Token: 0x0400A4EC RID: 42220
			public static LocString DESC = string.Concat(new string[]
			{
				"(Pb) Lead Gas is a soft yet extremely dense ",
				UI.FormatAsLink("Refined Metal", "REFINEDMETAL"),
				" heated into a ",
				UI.FormatAsLink("Gaseous", "ELEMENTS_GAS"),
				"."
			});
		}

		// Token: 0x02002499 RID: 9369
		public class LEAD
		{
			// Token: 0x0400A4ED RID: 42221
			public static LocString NAME = UI.FormatAsLink("Lead", "LEAD");

			// Token: 0x0400A4EE RID: 42222
			public static LocString DESC = string.Concat(new string[]
			{
				"(Pb) Lead is a soft yet extremely dense ",
				UI.FormatAsLink("Refined Metal", "REFINEDMETAL"),
				".\n\nIt has a low Overheat Temperature and is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x0200249A RID: 9370
		public class LIQUIDCARBONDIOXIDE
		{
			// Token: 0x0400A4EF RID: 42223
			public static LocString NAME = UI.FormatAsLink("Liquid Carbon Dioxide", "LIQUIDCARBONDIOXIDE");

			// Token: 0x0400A4F0 RID: 42224
			public static LocString DESC = "(CO<sub>2</sub>) Carbon Dioxide is an unbreathable chemical compound.\n\nThis selection is currently in a " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " state.";
		}

		// Token: 0x0200249B RID: 9371
		public class LIQUIDHELIUM
		{
			// Token: 0x0400A4F1 RID: 42225
			public static LocString NAME = UI.FormatAsLink("Helium", "LIQUIDHELIUM");

			// Token: 0x0400A4F2 RID: 42226
			public static LocString DESC = "(He) Helium is an atomically lightweight chemical element cooled into a " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " state.";
		}

		// Token: 0x0200249C RID: 9372
		public class LIQUIDHYDROGEN
		{
			// Token: 0x0400A4F3 RID: 42227
			public static LocString NAME = UI.FormatAsLink("Liquid Hydrogen", "LIQUIDHYDROGEN");

			// Token: 0x0400A4F4 RID: 42228
			public static LocString DESC = "(H) Hydrogen in its " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " state.\n\nIt freezes most substances that come into contact with it.";
		}

		// Token: 0x0200249D RID: 9373
		public class LIQUIDOXYGEN
		{
			// Token: 0x0400A4F5 RID: 42229
			public static LocString NAME = UI.FormatAsLink("Liquid Oxygen", "LIQUIDOXYGEN");

			// Token: 0x0400A4F6 RID: 42230
			public static LocString DESC = "(O<sub>2</sub>) Oxygen is a breathable chemical.\n\nThis selection is in a " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " state.";
		}

		// Token: 0x0200249E RID: 9374
		public class LIQUIDMETHANE
		{
			// Token: 0x0400A4F7 RID: 42231
			public static LocString NAME = UI.FormatAsLink("Liquid Methane", "LIQUIDMETHANE");

			// Token: 0x0400A4F8 RID: 42232
			public static LocString DESC = "(CH<sub>4</sub>) Methane is an alkane.\n\nThis selection is in a " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " state.";
		}

		// Token: 0x0200249F RID: 9375
		public class LIQUIDPHOSPHORUS
		{
			// Token: 0x0400A4F9 RID: 42233
			public static LocString NAME = UI.FormatAsLink("Liquid Phosphorus", "LIQUIDPHOSPHORUS");

			// Token: 0x0400A4FA RID: 42234
			public static LocString DESC = "(P) Phosphorus is a chemical element.\n\nThis selection is in a " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " state.";
		}

		// Token: 0x020024A0 RID: 9376
		public class LIQUIDPROPANE
		{
			// Token: 0x0400A4FB RID: 42235
			public static LocString NAME = UI.FormatAsLink("Liquid Propane", "LIQUIDPROPANE");

			// Token: 0x0400A4FC RID: 42236
			public static LocString DESC = string.Concat(new string[]
			{
				"(C<sub>3</sub>H<sub>8</sub>) Propane is an alkane.\n\nThis selection is in a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state.\n\nIt is useful in ",
				UI.FormatAsLink("Power", "POWER"),
				" production."
			});
		}

		// Token: 0x020024A1 RID: 9377
		public class LIQUIDSULFUR
		{
			// Token: 0x0400A4FD RID: 42237
			public static LocString NAME = UI.FormatAsLink("Liquid Sulfur", "LIQUIDSULFUR");

			// Token: 0x0400A4FE RID: 42238
			public static LocString DESC = string.Concat(new string[]
			{
				"(S) Sulfur is a common chemical element and byproduct of ",
				UI.FormatAsLink("Natural Gas", "METHANE"),
				" production.\n\nThis selection is in a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x020024A2 RID: 9378
		public class MAFICROCK
		{
			// Token: 0x0400A4FF RID: 42239
			public static LocString NAME = UI.FormatAsLink("Mafic Rock", "MAFICROCK");

			// Token: 0x0400A500 RID: 42240
			public static LocString DESC = string.Concat(new string[]
			{
				"Mafic Rock is a variation of ",
				UI.FormatAsLink("Igneous Rock", "IGNEOUSROCK"),
				" that is rich in ",
				UI.FormatAsLink("Iron", "IRON"),
				".\n\nIt is useful as a <b>Construction Material</b>."
			});
		}

		// Token: 0x020024A3 RID: 9379
		public class MAGMA
		{
			// Token: 0x0400A501 RID: 42241
			public static LocString NAME = UI.FormatAsLink("Magma", "MAGMA");

			// Token: 0x0400A502 RID: 42242
			public static LocString DESC = string.Concat(new string[]
			{
				"Magma is a composite of ",
				UI.FormatAsLink("Igneous Rock", "IGNEOUSROCK"),
				" heated into a molten, ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x020024A4 RID: 9380
		public class WOODLOG
		{
			// Token: 0x0400A503 RID: 42243
			public static LocString NAME = UI.FormatAsLink("Wood", "WOOD");

			// Token: 0x0400A504 RID: 42244
			public static LocString DESC = string.Concat(new string[]
			{
				"Wood is a good source of ",
				UI.FormatAsLink("Heat", "HEAT"),
				" and ",
				UI.FormatAsLink("Power", "POWER"),
				".\n\nIts insulation properties and positive ",
				UI.FormatAsLink("Decor", "DECOR"),
				" also make it a useful <b>Construction Material</b>."
			});
		}

		// Token: 0x020024A5 RID: 9381
		public class CINNABAR
		{
			// Token: 0x0400A505 RID: 42245
			public static LocString NAME = UI.FormatAsLink("Cinnabar Ore", "CINNABAR");

			// Token: 0x0400A506 RID: 42246
			public static LocString DESC = string.Concat(new string[]
			{
				"(HgS) Cinnabar Ore, also known as mercury sulfide, is a conductive ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				" that can be refined into ",
				UI.FormatAsLink("Mercury", "MERCURY"),
				".\n\nIt is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x020024A6 RID: 9382
		public class TALLOW
		{
			// Token: 0x0400A507 RID: 42247
			public static LocString NAME = UI.FormatAsLink("Tallow", "TALLOW");

			// Token: 0x0400A508 RID: 42248
			public static LocString DESC = "A chunk of raw grease that can be used in " + UI.FormatAsLink("Food", "FOOD") + " production or industrial processes.";
		}

		// Token: 0x020024A7 RID: 9383
		public class MERCURY
		{
			// Token: 0x0400A509 RID: 42249
			public static LocString NAME = UI.FormatAsLink("Mercury", "MERCURY");

			// Token: 0x0400A50A RID: 42250
			public static LocString DESC = "(Hg) Mercury is a metallic " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + ".";
		}

		// Token: 0x020024A8 RID: 9384
		public class MERCURYGAS
		{
			// Token: 0x0400A50B RID: 42251
			public static LocString NAME = UI.FormatAsLink("Mercury Gas", "MERCURYGAS");

			// Token: 0x0400A50C RID: 42252
			public static LocString DESC = string.Concat(new string[]
			{
				"(Hg) Mercury Gas is a ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				" heated into a ",
				UI.FormatAsLink("Gaseous", "ELEMENTS_GAS"),
				" state."
			});
		}

		// Token: 0x020024A9 RID: 9385
		public class METHANE
		{
			// Token: 0x0400A50D RID: 42253
			public static LocString NAME = UI.FormatAsLink("Natural Gas", "METHANE");

			// Token: 0x0400A50E RID: 42254
			public static LocString DESC = string.Concat(new string[]
			{
				"Natural Gas is a mixture of various alkanes in a ",
				UI.FormatAsLink("Gaseous", "ELEMENTS_GAS"),
				" state.\n\nIt is useful in ",
				UI.FormatAsLink("Power", "POWER"),
				" production."
			});
		}

		// Token: 0x020024AA RID: 9386
		public class MILK
		{
			// Token: 0x0400A50F RID: 42255
			public static LocString NAME = UI.FormatAsLink("Brackene", "MILK");

			// Token: 0x0400A510 RID: 42256
			public static LocString DESC = string.Concat(new string[]
			{
				"Brackene is a sodium-rich ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				".\n\nIt is useful in ",
				UI.FormatAsLink("Ranching", "RANCHING"),
				"."
			});
		}

		// Token: 0x020024AB RID: 9387
		public class MILKFAT
		{
			// Token: 0x0400A511 RID: 42257
			public static LocString NAME = UI.FormatAsLink("Brackwax", "MILKFAT");

			// Token: 0x0400A512 RID: 42258
			public static LocString DESC = string.Concat(new string[]
			{
				"Brackwax is a ",
				UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
				" byproduct of ",
				UI.FormatAsLink("Brackene", "MILK"),
				"."
			});
		}

		// Token: 0x020024AC RID: 9388
		public class MOLTENCARBON
		{
			// Token: 0x0400A513 RID: 42259
			public static LocString NAME = UI.FormatAsLink("Liquid Carbon", "MOLTENCARBON");

			// Token: 0x0400A514 RID: 42260
			public static LocString DESC = "(C) Liquid Carbon is an abundant, versatile element heated into a " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " state.";
		}

		// Token: 0x020024AD RID: 9389
		public class MOLTENCOPPER
		{
			// Token: 0x0400A515 RID: 42261
			public static LocString NAME = UI.FormatAsLink("Molten Copper", "MOLTENCOPPER");

			// Token: 0x0400A516 RID: 42262
			public static LocString DESC = string.Concat(new string[]
			{
				"(Cu) Molten Copper is a conductive ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				" heated into a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x020024AE RID: 9390
		public class MOLTENGLASS
		{
			// Token: 0x0400A517 RID: 42263
			public static LocString NAME = UI.FormatAsLink("Molten Glass", "MOLTENGLASS");

			// Token: 0x0400A518 RID: 42264
			public static LocString DESC = "Molten Glass is a composite of granular rock, heated into a " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " state.";
		}

		// Token: 0x020024AF RID: 9391
		public class MOLTENGOLD
		{
			// Token: 0x0400A519 RID: 42265
			public static LocString NAME = UI.FormatAsLink("Molten Gold", "MOLTENGOLD");

			// Token: 0x0400A51A RID: 42266
			public static LocString DESC = string.Concat(new string[]
			{
				"(Au) Gold, a conductive precious ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				", heated into a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x020024B0 RID: 9392
		public class MOLTENIRON
		{
			// Token: 0x0400A51B RID: 42267
			public static LocString NAME = UI.FormatAsLink("Molten Iron", "MOLTENIRON");

			// Token: 0x0400A51C RID: 42268
			public static LocString DESC = string.Concat(new string[]
			{
				"(Fe) Molten Iron is a common industrial ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				" heated into a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x020024B1 RID: 9393
		public class MOLTENCOBALT
		{
			// Token: 0x0400A51D RID: 42269
			public static LocString NAME = UI.FormatAsLink("Molten Cobalt", "MOLTENCOBALT");

			// Token: 0x0400A51E RID: 42270
			public static LocString DESC = string.Concat(new string[]
			{
				"(Co) Molten Cobalt is a ",
				UI.FormatAsLink("Refined Metal", "REFINEDMETAL"),
				" heated into a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x020024B2 RID: 9394
		public class MOLTENLEAD
		{
			// Token: 0x0400A51F RID: 42271
			public static LocString NAME = UI.FormatAsLink("Molten Lead", "MOLTENLEAD");

			// Token: 0x0400A520 RID: 42272
			public static LocString DESC = string.Concat(new string[]
			{
				"(Pb) Lead is an extremely dense ",
				UI.FormatAsLink("Refined Metal", "REFINEDMETAL"),
				" heated into a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x020024B3 RID: 9395
		public class MOLTENNIOBIUM
		{
			// Token: 0x0400A521 RID: 42273
			public static LocString NAME = UI.FormatAsLink("Molten Niobium", "MOLTENNIOBIUM");

			// Token: 0x0400A522 RID: 42274
			public static LocString DESC = "(Nb) Molten Niobium is a rare metal heated into a " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " state.";
		}

		// Token: 0x020024B4 RID: 9396
		public class MOLTENTUNGSTEN
		{
			// Token: 0x0400A523 RID: 42275
			public static LocString NAME = UI.FormatAsLink("Molten Tungsten", "MOLTENTUNGSTEN");

			// Token: 0x0400A524 RID: 42276
			public static LocString DESC = string.Concat(new string[]
			{
				"(W) Molten Tungsten is a crystalline ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				" heated into a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x020024B5 RID: 9397
		public class MOLTENTUNGSTENDISELENIDE
		{
			// Token: 0x0400A525 RID: 42277
			public static LocString NAME = UI.FormatAsLink("Tungsten Diselenide", "MOLTENTUNGSTENDISELENIDE");

			// Token: 0x0400A526 RID: 42278
			public static LocString DESC = string.Concat(new string[]
			{
				"(WSe<sub>2</sub>) Tungsten Diselenide is an inorganic ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				" compound heated into a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x020024B6 RID: 9398
		public class MOLTENSTEEL
		{
			// Token: 0x0400A527 RID: 42279
			public static LocString NAME = UI.FormatAsLink("Molten Steel", "MOLTENSTEEL");

			// Token: 0x0400A528 RID: 42280
			public static LocString DESC = string.Concat(new string[]
			{
				"Molten Steel is a ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				" alloy of iron and carbon, heated into a hazardous ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x020024B7 RID: 9399
		public class MOLTENURANIUM
		{
			// Token: 0x0400A529 RID: 42281
			public static LocString NAME = UI.FormatAsLink("Liquid Uranium", "MOLTENURANIUM");

			// Token: 0x0400A52A RID: 42282
			public static LocString DESC = string.Concat(new string[]
			{
				"(U) Liquid Uranium is a highly ",
				UI.FormatAsLink("Radioactive", "RADIATION"),
				" substance, heated into a hazardous ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state.\n\nIt is a byproduct of ",
				UI.FormatAsLink("Enriched Uranium", "ENRICHEDURANIUM"),
				"."
			});
		}

		// Token: 0x020024B8 RID: 9400
		public class NIOBIUM
		{
			// Token: 0x0400A52B RID: 42283
			public static LocString NAME = UI.FormatAsLink("Niobium", "NIOBIUM");

			// Token: 0x0400A52C RID: 42284
			public static LocString DESC = "(Nb) Niobium is a rare metal with many practical applications in metallurgy and superconductor " + UI.FormatAsLink("Research", "RESEARCH") + ".";
		}

		// Token: 0x020024B9 RID: 9401
		public class NIOBIUMGAS
		{
			// Token: 0x0400A52D RID: 42285
			public static LocString NAME = UI.FormatAsLink("Niobium Gas", "NIOBIUMGAS");

			// Token: 0x0400A52E RID: 42286
			public static LocString DESC = "(Nb) Niobium Gas is a rare metal.\n\nThis selection is in a " + UI.FormatAsLink("Gaseous", "ELEMENTS_GAS") + " state.";
		}

		// Token: 0x020024BA RID: 9402
		public class NUCLEARWASTE
		{
			// Token: 0x0400A52F RID: 42287
			public static LocString NAME = UI.FormatAsLink("Liquid Nuclear Waste", "NUCLEARWASTE");

			// Token: 0x0400A530 RID: 42288
			public static LocString DESC = string.Concat(new string[]
			{
				"Highly toxic liquid full of ",
				UI.FormatAsLink("Radioactive Contaminants", "RADIATION"),
				" which emit ",
				UI.FormatAsLink("Radiation", "RADIATION"),
				" that can be absorbed by ",
				UI.FormatAsLink("Radbolt Generators", "HIGHENERGYPARTICLESPAWNER"),
				"."
			});
		}

		// Token: 0x020024BB RID: 9403
		public class OBSIDIAN
		{
			// Token: 0x0400A531 RID: 42289
			public static LocString NAME = UI.FormatAsLink("Obsidian", "OBSIDIAN");

			// Token: 0x0400A532 RID: 42290
			public static LocString DESC = "Obsidian is a brittle composite of volcanic " + UI.FormatAsLink("Glass", "GLASS") + ".";
		}

		// Token: 0x020024BC RID: 9404
		public class OXYGEN
		{
			// Token: 0x0400A533 RID: 42291
			public static LocString NAME = UI.FormatAsLink("Oxygen", "OXYGEN");

			// Token: 0x0400A534 RID: 42292
			public static LocString DESC = "(O<sub>2</sub>) Oxygen is an atomically lightweight and breathable " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + ", necessary for sustaining life.\n\nIt tends to rise above other gases.";
		}

		// Token: 0x020024BD RID: 9405
		public class OXYROCK
		{
			// Token: 0x0400A535 RID: 42293
			public static LocString NAME = UI.FormatAsLink("Oxylite", "OXYROCK");

			// Token: 0x0400A536 RID: 42294
			public static LocString DESC = string.Concat(new string[]
			{
				"(Ir<sub>3</sub>O<sub>2</sub>) Oxylite is a chemical compound that slowly emits breathable ",
				UI.FormatAsLink("Oxygen", "OXYGEN"),
				".\n\nExcavating ",
				ELEMENTS.OXYROCK.NAME,
				" increases its emission rate, but depletes the ore more rapidly."
			});
		}

		// Token: 0x020024BE RID: 9406
		public class PHOSPHATENODULES
		{
			// Token: 0x0400A537 RID: 42295
			public static LocString NAME = UI.FormatAsLink("Phosphate Nodules", "PHOSPHATENODULES");

			// Token: 0x0400A538 RID: 42296
			public static LocString DESC = "(PO<sup>3-</sup><sub>4</sub>) Nodules of sedimentary rock containing high concentrations of phosphate.";
		}

		// Token: 0x020024BF RID: 9407
		public class PHOSPHORITE
		{
			// Token: 0x0400A539 RID: 42297
			public static LocString NAME = UI.FormatAsLink("Phosphorite", "PHOSPHORITE");

			// Token: 0x0400A53A RID: 42298
			public static LocString DESC = "Phosphorite is a composite of sedimentary rock, saturated with phosphate.";
		}

		// Token: 0x020024C0 RID: 9408
		public class PHOSPHORUS
		{
			// Token: 0x0400A53B RID: 42299
			public static LocString NAME = UI.FormatAsLink("Refined Phosphorus", "PHOSPHORUS");

			// Token: 0x0400A53C RID: 42300
			public static LocString DESC = "(P) Refined Phosphorus is a chemical element in its " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " state.";
		}

		// Token: 0x020024C1 RID: 9409
		public class PHOSPHORUSGAS
		{
			// Token: 0x0400A53D RID: 42301
			public static LocString NAME = UI.FormatAsLink("Phosphorus Gas", "PHOSPHORUSGAS");

			// Token: 0x0400A53E RID: 42302
			public static LocString DESC = string.Concat(new string[]
			{
				"(P) Phosphorus Gas is the ",
				UI.FormatAsLink("Gaseous", "ELEMENTS_GAS"),
				" state of ",
				UI.FormatAsLink("Refined Phosphorus", "PHOSPHORUS"),
				"."
			});
		}

		// Token: 0x020024C2 RID: 9410
		public class PROPANE
		{
			// Token: 0x0400A53F RID: 42303
			public static LocString NAME = UI.FormatAsLink("Propane Gas", "PROPANE");

			// Token: 0x0400A540 RID: 42304
			public static LocString DESC = string.Concat(new string[]
			{
				"(C<sub>3</sub>H<sub>8</sub>) Propane Gas is a natural alkane.\n\nThis selection is in a ",
				UI.FormatAsLink("Gaseous", "ELEMENTS_GAS"),
				" state.\n\nIt is useful in ",
				UI.FormatAsLink("Power", "POWER"),
				" production."
			});
		}

		// Token: 0x020024C3 RID: 9411
		public class RADIUM
		{
			// Token: 0x0400A541 RID: 42305
			public static LocString NAME = UI.FormatAsLink("Radium", "RADIUM");

			// Token: 0x0400A542 RID: 42306
			public static LocString DESC = string.Concat(new string[]
			{
				"(Ra) Radium is a ",
				UI.FormatAsLink("Light", "LIGHT"),
				" emitting radioactive substance.\n\nIt is useful as a ",
				UI.FormatAsLink("Power", "POWER"),
				" source."
			});
		}

		// Token: 0x020024C4 RID: 9412
		public class YELLOWCAKE
		{
			// Token: 0x0400A543 RID: 42307
			public static LocString NAME = UI.FormatAsLink("Yellowcake", "YELLOWCAKE");

			// Token: 0x0400A544 RID: 42308
			public static LocString DESC = string.Concat(new string[]
			{
				"(U<sub>3</sub>O<sub>8</sub>) Yellowcake is a byproduct of ",
				UI.FormatAsLink("Uranium", "URANIUM"),
				" mining.\n\nIt is useful in preparing fuel for ",
				UI.FormatAsLink("Research Reactors", "NUCLEARREACTOR"),
				".\n\nNote: Do not eat."
			});
		}

		// Token: 0x020024C5 RID: 9413
		public class ROCKGAS
		{
			// Token: 0x0400A545 RID: 42309
			public static LocString NAME = UI.FormatAsLink("Rock Gas", "ROCKGAS");

			// Token: 0x0400A546 RID: 42310
			public static LocString DESC = "Rock Gas is rock that has been superheated into a " + UI.FormatAsLink("Gaseous", "ELEMENTS_GAS") + " state.";
		}

		// Token: 0x020024C6 RID: 9414
		public class RUST
		{
			// Token: 0x0400A547 RID: 42311
			public static LocString NAME = UI.FormatAsLink("Rust", "RUST");

			// Token: 0x0400A548 RID: 42312
			public static LocString DESC = string.Concat(new string[]
			{
				"Rust is an iron oxide that forms from the breakdown of ",
				UI.FormatAsLink("Iron", "IRON"),
				".\n\nIt is useful in some ",
				UI.FormatAsLink("Oxygen", "OXYGEN"),
				" production processes."
			});
		}

		// Token: 0x020024C7 RID: 9415
		public class REGOLITH
		{
			// Token: 0x0400A549 RID: 42313
			public static LocString NAME = UI.FormatAsLink("Regolith", "REGOLITH");

			// Token: 0x0400A54A RID: 42314
			public static LocString DESC = "Regolith is a sandy substance composed of the various particles that collect atop terrestrial objects.\n\nIt is useful as a " + UI.FormatAsLink("Filtration Medium", "FILTER") + ".";
		}

		// Token: 0x020024C8 RID: 9416
		public class SALTGAS
		{
			// Token: 0x0400A54B RID: 42315
			public static LocString NAME = UI.FormatAsLink("Salt Gas", "SALTGAS");

			// Token: 0x0400A54C RID: 42316
			public static LocString DESC = "(NaCl) Salt Gas is an edible chemical compound that has been superheated into a " + UI.FormatAsLink("Gaseous", "ELEMENTS_GAS") + " state.";
		}

		// Token: 0x020024C9 RID: 9417
		public class MOLTENSALT
		{
			// Token: 0x0400A54D RID: 42317
			public static LocString NAME = UI.FormatAsLink("Molten Salt", "MOLTENSALT");

			// Token: 0x0400A54E RID: 42318
			public static LocString DESC = "(NaCl) Molten Salt is an edible chemical compound that has been heated into a " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " state.";
		}

		// Token: 0x020024CA RID: 9418
		public class SALT
		{
			// Token: 0x0400A54F RID: 42319
			public static LocString NAME = UI.FormatAsLink("Salt", "SALT");

			// Token: 0x0400A550 RID: 42320
			public static LocString DESC = "(NaCl) Salt, also known as sodium chloride, is an edible chemical compound.\n\nWhen refined, it can be eaten with meals to increase Duplicant " + UI.FormatAsLink("Morale", "MORALE") + ".";
		}

		// Token: 0x020024CB RID: 9419
		public class SALTWATER
		{
			// Token: 0x0400A551 RID: 42321
			public static LocString NAME = UI.FormatAsLink("Salt Water", "SALTWATER");

			// Token: 0x0400A552 RID: 42322
			public static LocString DESC = string.Concat(new string[]
			{
				"Salt Water is a natural, lightly concentrated solution of ",
				UI.FormatAsLink("Salt", "SALT"),
				" dissolved in ",
				UI.FormatAsLink("Water", "WATER"),
				".\n\nIt can be used in desalination processes, separating out usable salt."
			});
		}

		// Token: 0x020024CC RID: 9420
		public class SAND
		{
			// Token: 0x0400A553 RID: 42323
			public static LocString NAME = UI.FormatAsLink("Sand", "SAND");

			// Token: 0x0400A554 RID: 42324
			public static LocString DESC = "Sand is a composite of granular rock.\n\nIt is useful as a " + UI.FormatAsLink("Filtration Medium", "FILTER") + ".";
		}

		// Token: 0x020024CD RID: 9421
		public class SANDCEMENT
		{
			// Token: 0x0400A555 RID: 42325
			public static LocString NAME = UI.FormatAsLink("Sand Cement", "SANDCEMENT");

			// Token: 0x0400A556 RID: 42326
			public static LocString DESC = "";
		}

		// Token: 0x020024CE RID: 9422
		public class SANDSTONE
		{
			// Token: 0x0400A557 RID: 42327
			public static LocString NAME = UI.FormatAsLink("Sandstone", "SANDSTONE");

			// Token: 0x0400A558 RID: 42328
			public static LocString DESC = "Sandstone is a composite of relatively soft sedimentary rock.\n\nIt is useful as a <b>Construction Material</b>.";
		}

		// Token: 0x020024CF RID: 9423
		public class SEDIMENTARYROCK
		{
			// Token: 0x0400A559 RID: 42329
			public static LocString NAME = UI.FormatAsLink("Sedimentary Rock", "SEDIMENTARYROCK");

			// Token: 0x0400A55A RID: 42330
			public static LocString DESC = "Sedimentary Rock is a hardened composite of sediment layers.\n\nIt is useful as a <b>Construction Material</b>.";
		}

		// Token: 0x020024D0 RID: 9424
		public class SHALE
		{
			// Token: 0x0400A55B RID: 42331
			public static LocString NAME = UI.FormatAsLink("Shale", "SHALE");

			// Token: 0x0400A55C RID: 42332
			public static LocString DESC = "Shale is a brittle composite of sediment layers.\n\nIt is useful as a <b>Construction Material</b>.";
		}

		// Token: 0x020024D1 RID: 9425
		public class SLIMEMOLD
		{
			// Token: 0x0400A55D RID: 42333
			public static LocString NAME = UI.FormatAsLink("Slime", "SLIMEMOLD");

			// Token: 0x0400A55E RID: 42334
			public static LocString DESC = string.Concat(new string[]
			{
				"Slime is a thick biomixture of algae, fungi, and mucopolysaccharides.\n\nIt can be distilled into ",
				UI.FormatAsLink("Algae", "ALGAE"),
				" and emits ",
				ELEMENTS.CONTAMINATEDOXYGEN.NAME,
				" once dug up."
			});
		}

		// Token: 0x020024D2 RID: 9426
		public class SNOW
		{
			// Token: 0x0400A55F RID: 42335
			public static LocString NAME = UI.FormatAsLink("Snow", "SNOW");

			// Token: 0x0400A560 RID: 42336
			public static LocString DESC = "(H<sub>2</sub>O) Snow is a mass of loose, crystalline ice particles.\n\nIt becomes " + UI.FormatAsLink("Water", "WATER") + " when melted.";
		}

		// Token: 0x020024D3 RID: 9427
		public class STABLESNOW
		{
			// Token: 0x0400A561 RID: 42337
			public static LocString NAME = "Packed " + ELEMENTS.SNOW.NAME;

			// Token: 0x0400A562 RID: 42338
			public static LocString DESC = ELEMENTS.SNOW.DESC;
		}

		// Token: 0x020024D4 RID: 9428
		public class SOLIDCARBONDIOXIDE
		{
			// Token: 0x0400A563 RID: 42339
			public static LocString NAME = UI.FormatAsLink("Solid Carbon Dioxide", "SOLIDCARBONDIOXIDE");

			// Token: 0x0400A564 RID: 42340
			public static LocString DESC = "(CO<sub>2</sub>) Carbon Dioxide is an unbreathable compound in a " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " state.";
		}

		// Token: 0x020024D5 RID: 9429
		public class SOLIDCHLORINE
		{
			// Token: 0x0400A565 RID: 42341
			public static LocString NAME = UI.FormatAsLink("Solid Chlorine", "SOLIDCHLORINE");

			// Token: 0x0400A566 RID: 42342
			public static LocString DESC = string.Concat(new string[]
			{
				"(Cl) Chlorine is a natural ",
				UI.FormatAsLink("Germ", "DISEASE"),
				"-killing element in a ",
				UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
				" state."
			});
		}

		// Token: 0x020024D6 RID: 9430
		public class SOLIDCRUDEOIL
		{
			// Token: 0x0400A567 RID: 42343
			public static LocString NAME = UI.FormatAsLink("Solid Crude Oil", "SOLIDCRUDEOIL");

			// Token: 0x0400A568 RID: 42344
			public static LocString DESC = "";
		}

		// Token: 0x020024D7 RID: 9431
		public class SOLIDHYDROGEN
		{
			// Token: 0x0400A569 RID: 42345
			public static LocString NAME = UI.FormatAsLink("Solid Hydrogen", "SOLIDHYDROGEN");

			// Token: 0x0400A56A RID: 42346
			public static LocString DESC = "(H) Solid Hydrogen is the universe's most common element in a " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " state.";
		}

		// Token: 0x020024D8 RID: 9432
		public class SOLIDMERCURY
		{
			// Token: 0x0400A56B RID: 42347
			public static LocString NAME = UI.FormatAsLink("Mercury", "SOLIDMERCURY");

			// Token: 0x0400A56C RID: 42348
			public static LocString DESC = string.Concat(new string[]
			{
				"(Hg) Mercury is a rare ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				" in a ",
				UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
				" state."
			});
		}

		// Token: 0x020024D9 RID: 9433
		public class SOLIDOXYGEN
		{
			// Token: 0x0400A56D RID: 42349
			public static LocString NAME = UI.FormatAsLink("Solid Oxygen", "SOLIDOXYGEN");

			// Token: 0x0400A56E RID: 42350
			public static LocString DESC = "(O<sub>2</sub>) Solid Oxygen is a breathable element in a " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " state.";
		}

		// Token: 0x020024DA RID: 9434
		public class SOLIDMETHANE
		{
			// Token: 0x0400A56F RID: 42351
			public static LocString NAME = UI.FormatAsLink("Solid Methane", "SOLIDMETHANE");

			// Token: 0x0400A570 RID: 42352
			public static LocString DESC = "(CH<sub>4</sub>) Methane is an alkane in a " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " state.";
		}

		// Token: 0x020024DB RID: 9435
		public class SOLIDNAPHTHA
		{
			// Token: 0x0400A571 RID: 42353
			public static LocString NAME = UI.FormatAsLink("Solid Naphtha", "SOLIDNAPHTHA");

			// Token: 0x0400A572 RID: 42354
			public static LocString DESC = "Naphtha is a distilled hydrocarbon mixture in a " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " state.";
		}

		// Token: 0x020024DC RID: 9436
		public class CORIUM
		{
			// Token: 0x0400A573 RID: 42355
			public static LocString NAME = UI.FormatAsLink("Corium", "CORIUM");

			// Token: 0x0400A574 RID: 42356
			public static LocString DESC = "A radioactive mixture of nuclear waste and melted reactor materials.\n\nReleases " + UI.FormatAsLink("Nuclear Fallout", "FALLOUT") + " gas.";
		}

		// Token: 0x020024DD RID: 9437
		public class SOLIDPETROLEUM
		{
			// Token: 0x0400A575 RID: 42357
			public static LocString NAME = UI.FormatAsLink("Solid Petroleum", "SOLIDPETROLEUM");

			// Token: 0x0400A576 RID: 42358
			public static LocString DESC = string.Concat(new string[]
			{
				"Petroleum is a ",
				UI.FormatAsLink("Power", "POWER"),
				" source.\n\nThis selection is in a ",
				UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
				" state."
			});
		}

		// Token: 0x020024DE RID: 9438
		public class SOLIDPROPANE
		{
			// Token: 0x0400A577 RID: 42359
			public static LocString NAME = UI.FormatAsLink("Solid Propane", "SOLIDPROPANE");

			// Token: 0x0400A578 RID: 42360
			public static LocString DESC = "(C<sub>3</sub>H<sub>8</sub>) Solid Propane is a natural gas in a " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " state.";
		}

		// Token: 0x020024DF RID: 9439
		public class SOLIDSUPERCOOLANT
		{
			// Token: 0x0400A579 RID: 42361
			public static LocString NAME = UI.FormatAsLink("Solid Super Coolant", "SOLIDSUPERCOOLANT");

			// Token: 0x0400A57A RID: 42362
			public static LocString DESC = string.Concat(new string[]
			{
				"Super Coolant is an industrial-grade ",
				UI.FormatAsLink("Fullerene", "FULLERENE"),
				" coolant.\n\nThis selection is in a ",
				UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
				" state."
			});
		}

		// Token: 0x020024E0 RID: 9440
		public class SOLIDVISCOGEL
		{
			// Token: 0x0400A57B RID: 42363
			public static LocString NAME = UI.FormatAsLink("Solid Visco-Gel", "SOLIDVISCOGEL");

			// Token: 0x0400A57C RID: 42364
			public static LocString DESC = string.Concat(new string[]
			{
				"Visco-Gel is a polymer that has high surface tension when in ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" form.\n\nThis selection is in a ",
				UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
				" state."
			});
		}

		// Token: 0x020024E1 RID: 9441
		public class SYNGAS
		{
			// Token: 0x0400A57D RID: 42365
			public static LocString NAME = UI.FormatAsLink("Synthesis Gas", "SYNGAS");

			// Token: 0x0400A57E RID: 42366
			public static LocString DESC = "Synthesis Gas is an artificial, unbreathable " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + ".\n\nIt can be converted into an efficient fuel.";
		}

		// Token: 0x020024E2 RID: 9442
		public class MOLTENSYNGAS
		{
			// Token: 0x0400A57F RID: 42367
			public static LocString NAME = UI.FormatAsLink("Molten Synthesis Gas", "SYNGAS");

			// Token: 0x0400A580 RID: 42368
			public static LocString DESC = "Molten Synthesis Gas is an artificial, unbreathable " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + ".\n\nIt can be converted into an efficient fuel.";
		}

		// Token: 0x020024E3 RID: 9443
		public class SOLIDSYNGAS
		{
			// Token: 0x0400A581 RID: 42369
			public static LocString NAME = UI.FormatAsLink("Solid Synthesis Gas", "SYNGAS");

			// Token: 0x0400A582 RID: 42370
			public static LocString DESC = "Solid Synthesis Gas is an artificial, unbreathable " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + ".\n\nIt can be converted into an efficient fuel.";
		}

		// Token: 0x020024E4 RID: 9444
		public class STEAM
		{
			// Token: 0x0400A583 RID: 42371
			public static LocString NAME = UI.FormatAsLink("Steam", "STEAM");

			// Token: 0x0400A584 RID: 42372
			public static LocString DESC = string.Concat(new string[]
			{
				"(H<sub>2</sub>O) Steam is ",
				ELEMENTS.WATER.NAME,
				" that has been heated into a scalding ",
				UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
				"."
			});
		}

		// Token: 0x020024E5 RID: 9445
		public class STEEL
		{
			// Token: 0x0400A585 RID: 42373
			public static LocString NAME = UI.FormatAsLink("Steel", "STEEL");

			// Token: 0x0400A586 RID: 42374
			public static LocString DESC = "Steel is a " + UI.FormatAsLink("Metal Alloy", "REFINEDMETAL") + " composed of iron and carbon.";
		}

		// Token: 0x020024E6 RID: 9446
		public class STEELGAS
		{
			// Token: 0x0400A587 RID: 42375
			public static LocString NAME = UI.FormatAsLink("Steel Gas", "STEELGAS");

			// Token: 0x0400A588 RID: 42376
			public static LocString DESC = string.Concat(new string[]
			{
				"Steel Gas is a superheated ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				" ",
				UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
				" composed of iron and carbon."
			});
		}

		// Token: 0x020024E7 RID: 9447
		public class SUGARWATER
		{
			// Token: 0x0400A589 RID: 42377
			public static LocString NAME = UI.FormatAsLink("Nectar", "SUGARWATER");

			// Token: 0x0400A58A RID: 42378
			public static LocString DESC = string.Concat(new string[]
			{
				"Nectar is a natural, lightly concentrated solution of ",
				UI.FormatAsLink("Sucrose", "SUCROSE"),
				" dissolved in ",
				UI.FormatAsLink("Water", "WATER"),
				"."
			});
		}

		// Token: 0x020024E8 RID: 9448
		public class SULFUR
		{
			// Token: 0x0400A58B RID: 42379
			public static LocString NAME = UI.FormatAsLink("Sulfur", "SULFUR");

			// Token: 0x0400A58C RID: 42380
			public static LocString DESC = string.Concat(new string[]
			{
				"(S) Sulfur is a common chemical element and byproduct of ",
				UI.FormatAsLink("Natural Gas", "METHANE"),
				" production.\n\nThis selection is in a ",
				UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
				" state."
			});
		}

		// Token: 0x020024E9 RID: 9449
		public class SULFURGAS
		{
			// Token: 0x0400A58D RID: 42381
			public static LocString NAME = UI.FormatAsLink("Sulfur Gas", "SULFURGAS");

			// Token: 0x0400A58E RID: 42382
			public static LocString DESC = string.Concat(new string[]
			{
				"(S) Sulfur is a common chemical element and byproduct of ",
				UI.FormatAsLink("Natural Gas", "METHANE"),
				" production.\n\nThis selection is in a ",
				UI.FormatAsLink("Gaseous", "ELEMENTS_GAS"),
				" state."
			});
		}

		// Token: 0x020024EA RID: 9450
		public class SUPERCOOLANT
		{
			// Token: 0x0400A58F RID: 42383
			public static LocString NAME = UI.FormatAsLink("Super Coolant", "SUPERCOOLANT");

			// Token: 0x0400A590 RID: 42384
			public static LocString DESC = string.Concat(new string[]
			{
				"Super Coolant is an industrial-grade coolant that utilizes the unusual energy states of ",
				UI.FormatAsLink("Fullerene", "FULLERENE"),
				".\n\nThis selection is in a ",
				UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
				" state."
			});
		}

		// Token: 0x020024EB RID: 9451
		public class SUPERCOOLANTGAS
		{
			// Token: 0x0400A591 RID: 42385
			public static LocString NAME = UI.FormatAsLink("Super Coolant Gas", "SUPERCOOLANTGAS");

			// Token: 0x0400A592 RID: 42386
			public static LocString DESC = string.Concat(new string[]
			{
				"Super Coolant is an industrial-grade ",
				UI.FormatAsLink("Fullerene", "FULLERENE"),
				" coolant.\n\nThis selection is in a ",
				UI.FormatAsLink("Gaseous", "ELEMENTS_GAS"),
				" state."
			});
		}

		// Token: 0x020024EC RID: 9452
		public class SUPERINSULATOR
		{
			// Token: 0x0400A593 RID: 42387
			public static LocString NAME = UI.FormatAsLink("Insulite", "SUPERINSULATOR");

			// Token: 0x0400A594 RID: 42388
			public static LocString DESC = string.Concat(new string[]
			{
				"Insulite reduces ",
				UI.FormatAsLink("Heat Transfer", "HEAT"),
				" and is composed of recrystallized ",
				UI.FormatAsLink("Abyssalite", "KATAIRITE"),
				"."
			});
		}

		// Token: 0x020024ED RID: 9453
		public class TEMPCONDUCTORSOLID
		{
			// Token: 0x0400A595 RID: 42389
			public static LocString NAME = UI.FormatAsLink("Thermium", "TEMPCONDUCTORSOLID");

			// Token: 0x0400A596 RID: 42390
			public static LocString DESC = "Thermium is an industrial metal alloy formulated to maximize " + UI.FormatAsLink("Heat Transfer", "HEAT") + " and thermal dispersion.";
		}

		// Token: 0x020024EE RID: 9454
		public class TUNGSTEN
		{
			// Token: 0x0400A597 RID: 42391
			public static LocString NAME = UI.FormatAsLink("Tungsten", "TUNGSTEN");

			// Token: 0x0400A598 RID: 42392
			public static LocString DESC = string.Concat(new string[]
			{
				"(W) Tungsten is an extremely tough crystalline ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				".\n\nIt is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x020024EF RID: 9455
		public class TUNGSTENGAS
		{
			// Token: 0x0400A599 RID: 42393
			public static LocString NAME = UI.FormatAsLink("Tungsten Gas", "TUNGSTENGAS");

			// Token: 0x0400A59A RID: 42394
			public static LocString DESC = string.Concat(new string[]
			{
				"(W) Tungsten is a superheated crystalline ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				".\n\nThis selection is in a ",
				UI.FormatAsLink("Gaseous", "ELEMENTS_GAS"),
				" state."
			});
		}

		// Token: 0x020024F0 RID: 9456
		public class TUNGSTENDISELENIDE
		{
			// Token: 0x0400A59B RID: 42395
			public static LocString NAME = UI.FormatAsLink("Tungsten Diselenide", "TUNGSTENDISELENIDE");

			// Token: 0x0400A59C RID: 42396
			public static LocString DESC = string.Concat(new string[]
			{
				"(WSe<sub>2</sub>) Tungsten Diselenide is an inorganic ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				" compound with a crystalline structure.\n\nIt is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x020024F1 RID: 9457
		public class TUNGSTENDISELENIDEGAS
		{
			// Token: 0x0400A59D RID: 42397
			public static LocString NAME = UI.FormatAsLink("Tungsten Diselenide Gas", "TUNGSTENDISELENIDEGAS");

			// Token: 0x0400A59E RID: 42398
			public static LocString DESC = string.Concat(new string[]
			{
				"(WSe<sub>2</sub>) Tungsten Diselenide Gasis a superheated ",
				UI.FormatAsLink("Metal", "RAWMETAL"),
				" compound in a ",
				UI.FormatAsLink("Gaseous", "ELEMENTS_GAS"),
				" state."
			});
		}

		// Token: 0x020024F2 RID: 9458
		public class TOXICSAND
		{
			// Token: 0x0400A59F RID: 42399
			public static LocString NAME = UI.FormatAsLink("Polluted Dirt", "TOXICSAND");

			// Token: 0x0400A5A0 RID: 42400
			public static LocString DESC = "Polluted Dirt is unprocessed biological waste.\n\nIt emits " + UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN") + " over time.";
		}

		// Token: 0x020024F3 RID: 9459
		public class UNOBTANIUM
		{
			// Token: 0x0400A5A1 RID: 42401
			public static LocString NAME = UI.FormatAsLink("Neutronium", "UNOBTANIUM");

			// Token: 0x0400A5A2 RID: 42402
			public static LocString DESC = "(Nt) Neutronium is a mysterious and extremely resilient element.\n\nIt cannot be excavated by any Duplicant mining tool.";
		}

		// Token: 0x020024F4 RID: 9460
		public class URANIUMORE
		{
			// Token: 0x0400A5A3 RID: 42403
			public static LocString NAME = UI.FormatAsLink("Uranium Ore", "URANIUMORE");

			// Token: 0x0400A5A4 RID: 42404
			public static LocString DESC = "(U) Uranium Ore is a highly " + UI.FormatAsLink("Radioactive", "RADIATION") + " substance.\n\nIt can be refined into fuel for research reactors.";
		}

		// Token: 0x020024F5 RID: 9461
		public class VACUUM
		{
			// Token: 0x0400A5A5 RID: 42405
			public static LocString NAME = UI.FormatAsLink("Vacuum", "VACUUM");

			// Token: 0x0400A5A6 RID: 42406
			public static LocString DESC = "A vacuum is a space devoid of all matter.";
		}

		// Token: 0x020024F6 RID: 9462
		public class VISCOGEL
		{
			// Token: 0x0400A5A7 RID: 42407
			public static LocString NAME = UI.FormatAsLink("Visco-Gel Fluid", "VISCOGEL");

			// Token: 0x0400A5A8 RID: 42408
			public static LocString DESC = "Visco-Gel Fluid is a " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " polymer with high surface tension, preventing typical liquid flow and allowing for unusual configurations.";
		}

		// Token: 0x020024F7 RID: 9463
		public class VOID
		{
			// Token: 0x0400A5A9 RID: 42409
			public static LocString NAME = UI.FormatAsLink("Void", "VOID");

			// Token: 0x0400A5AA RID: 42410
			public static LocString DESC = "Cold, infinite nothingness.";
		}

		// Token: 0x020024F8 RID: 9464
		public class COMPOSITION
		{
			// Token: 0x0400A5AB RID: 42411
			public static LocString NAME = UI.FormatAsLink("Composition", "COMPOSITION");

			// Token: 0x0400A5AC RID: 42412
			public static LocString DESC = "A mixture of two or more elements.";
		}

		// Token: 0x020024F9 RID: 9465
		public class WATER
		{
			// Token: 0x0400A5AD RID: 42413
			public static LocString NAME = UI.FormatAsLink("Water", "WATER");

			// Token: 0x0400A5AE RID: 42414
			public static LocString DESC = "(H<sub>2</sub>O) Clean " + UI.FormatAsLink("Water", "WATER") + ", suitable for consumption.";
		}

		// Token: 0x020024FA RID: 9466
		public class WOLFRAMITE
		{
			// Token: 0x0400A5AF RID: 42415
			public static LocString NAME = UI.FormatAsLink("Wolframite", "WOLFRAMITE");

			// Token: 0x0400A5B0 RID: 42416
			public static LocString DESC = string.Concat(new string[]
			{
				"((Fe,Mn)WO<sub>4</sub>) Wolframite is a dense Metallic element in a ",
				UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
				" state.\n\nIt is a source of ",
				UI.FormatAsLink("Tungsten", "TUNGSTEN"),
				" and is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x020024FB RID: 9467
		public class TESTELEMENT
		{
			// Token: 0x0400A5B1 RID: 42417
			public static LocString NAME = UI.FormatAsLink("Test Element", "TESTELEMENT");

			// Token: 0x0400A5B2 RID: 42418
			public static LocString DESC = string.Concat(new string[]
			{
				"((Fe,Mn)WO<sub>4</sub>) Wolframite is a dense Metallic element in a ",
				UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
				" state.\n\nIt is a source of ",
				UI.FormatAsLink("Tungsten", "TUNGSTEN"),
				" and is suitable for building ",
				UI.FormatAsLink("Power", "POWER"),
				" systems."
			});
		}

		// Token: 0x020024FC RID: 9468
		public class POLYPROPYLENE
		{
			// Token: 0x0400A5B3 RID: 42419
			public static LocString NAME = UI.FormatAsLink("Plastic", "POLYPROPYLENE");

			// Token: 0x0400A5B4 RID: 42420
			public static LocString DESC = "(C<sub>3</sub>H<sub>6</sub>)<sub>n</sub> " + ELEMENTS.POLYPROPYLENE.NAME + " is a thermoplastic polymer.\n\nIt is useful for constructing a variety of advanced buildings and equipment.";

			// Token: 0x0400A5B5 RID: 42421
			public static LocString BUILD_DESC = "Buildings made of this " + ELEMENTS.POLYPROPYLENE.NAME + " have antiseptic properties";
		}

		// Token: 0x020024FD RID: 9469
		public class HARDPOLYPROPYLENE
		{
			// Token: 0x0400A5B6 RID: 42422
			public static LocString NAME = UI.FormatAsLink("Plastium", "HARDPOLYPROPYLENE");

			// Token: 0x0400A5B7 RID: 42423
			public static LocString DESC = string.Concat(new string[]
			{
				ELEMENTS.HARDPOLYPROPYLENE.NAME,
				" is an advanced thermoplastic polymer made from ",
				UI.FormatAsLink("Thermium", "TEMPCONDUCTORSOLID"),
				", ",
				UI.FormatAsLink("Plastic", "POLYPROPYLENE"),
				" and ",
				UI.FormatAsLink("Brackwax", "MILKFAT"),
				".\n\nIt is highly heat-resistant and suitable for use in space buildings."
			});
		}

		// Token: 0x020024FE RID: 9470
		public class NAPHTHA
		{
			// Token: 0x0400A5B8 RID: 42424
			public static LocString NAME = UI.FormatAsLink("Liquid Naphtha", "NAPHTHA");

			// Token: 0x0400A5B9 RID: 42425
			public static LocString DESC = "Naphtha a distilled hydrocarbon mixture produced from the burning of " + UI.FormatAsLink("Plastic", "POLYPROPYLENE") + ".";
		}

		// Token: 0x020024FF RID: 9471
		public class SLABS
		{
			// Token: 0x0400A5BA RID: 42426
			public static LocString NAME = UI.FormatAsLink("Building Slab", "SLABS");

			// Token: 0x0400A5BB RID: 42427
			public static LocString DESC = "Slabs are a refined mineral building block used for assembling advanced buildings.";
		}

		// Token: 0x02002500 RID: 9472
		public class TOXICMUD
		{
			// Token: 0x0400A5BC RID: 42428
			public static LocString NAME = UI.FormatAsLink("Polluted Mud", "TOXICMUD");

			// Token: 0x0400A5BD RID: 42429
			public static LocString DESC = string.Concat(new string[]
			{
				"A mixture of ",
				UI.FormatAsLink("Polluted Dirt", "TOXICSAND"),
				" and ",
				UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
				".\n\nCan be separated into its base elements using a ",
				UI.FormatAsLink("Sludge Press", "SLUDGEPRESS"),
				"."
			});
		}

		// Token: 0x02002501 RID: 9473
		public class MUD
		{
			// Token: 0x0400A5BE RID: 42430
			public static LocString NAME = UI.FormatAsLink("Mud", "MUD");

			// Token: 0x0400A5BF RID: 42431
			public static LocString DESC = string.Concat(new string[]
			{
				"A mixture of ",
				UI.FormatAsLink("Dirt", "DIRT"),
				" and ",
				UI.FormatAsLink("Water", "WATER"),
				".\n\nCan be separated into its base elements using a ",
				UI.FormatAsLink("Sludge Press", "SLUDGEPRESS"),
				"."
			});
		}

		// Token: 0x02002502 RID: 9474
		public class SUCROSE
		{
			// Token: 0x0400A5C0 RID: 42432
			public static LocString NAME = UI.FormatAsLink("Sucrose", "SUCROSE");

			// Token: 0x0400A5C1 RID: 42433
			public static LocString DESC = "(C<sub>12</sub>H<sub>22</sub>O<sub>11</sub>) Sucrose is the raw form of sugar.\n\nIt can be used for cooking higher-quality " + UI.FormatAsLink("Food", "FOOD") + ".";
		}

		// Token: 0x02002503 RID: 9475
		public class MOLTENSUCROSE
		{
			// Token: 0x0400A5C2 RID: 42434
			public static LocString NAME = UI.FormatAsLink("Liquid Sucrose", "MOLTENSUCROSE");

			// Token: 0x0400A5C3 RID: 42435
			public static LocString DESC = "(C<sub>12</sub>H<sub>22</sub>O<sub>11</sub>) Liquid Sucrose is the raw form of sugar, heated into a " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " state.";
		}
	}
}
