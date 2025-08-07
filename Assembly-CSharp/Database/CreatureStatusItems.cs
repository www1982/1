using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Klei.AI;
using STRINGS;
using UnityEngine;

namespace Database
{
	// Token: 0x02000EE8 RID: 3816
	public class CreatureStatusItems : StatusItems
	{
		// Token: 0x06007971 RID: 31089 RVA: 0x002F7D78 File Offset: 0x002F5F78
		public CreatureStatusItems(ResourceSet parent)
			: base("CreatureStatusItems", parent)
		{
			this.CreateStatusItems();
		}

		// Token: 0x06007972 RID: 31090 RVA: 0x002F7D8C File Offset: 0x002F5F8C
		private void CreateStatusItems()
		{
			this.Dead = new StatusItem("Dead", "CREATURES", "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022, null);
			this.Hot = new StatusItem("Hot", "CREATURES", "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022, null);
			this.Hot.resolveStringCallback = delegate(string str, object data)
			{
				TemperatureVulnerable temperatureVulnerable = (TemperatureVulnerable)data;
				return string.Format(str, GameUtil.GetFormattedTemperature(temperatureVulnerable.TemperatureWarningLow, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false), GameUtil.GetFormattedTemperature(temperatureVulnerable.TemperatureWarningHigh, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
			};
			this.Hot_Crop = new StatusItem("Hot_Crop", "CREATURES", "status_item_plant_temperature", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022, null);
			this.Hot_Crop.resolveStringCallback = delegate(string str, object data)
			{
				TemperatureVulnerable temperatureVulnerable2 = (TemperatureVulnerable)data;
				str = str.Replace("{low_temperature}", GameUtil.GetFormattedTemperature(temperatureVulnerable2.TemperatureWarningLow, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
				str = str.Replace("{high_temperature}", GameUtil.GetFormattedTemperature(temperatureVulnerable2.TemperatureWarningHigh, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
				return str;
			};
			this.Scalding = new StatusItem("Scalding", "CREATURES", "", StatusItem.IconType.Exclamation, NotificationType.DuplicantThreatening, true, OverlayModes.None.ID, true, 129022, null);
			this.Scalding.resolveTooltipCallback = delegate(string str, object data)
			{
				float averageExternalTemperature = ((ScaldingMonitor.Instance)data).AverageExternalTemperature;
				float scaldingThreshold = ((ScaldingMonitor.Instance)data).GetScaldingThreshold();
				str = str.Replace("{ExternalTemperature}", GameUtil.GetFormattedTemperature(averageExternalTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
				str = str.Replace("{TargetTemperature}", GameUtil.GetFormattedTemperature(scaldingThreshold, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
				return str;
			};
			this.Scalding.AddNotification(null, null, null);
			this.Scolding = new StatusItem("Scolding", "CREATURES", "", StatusItem.IconType.Exclamation, NotificationType.DuplicantThreatening, true, OverlayModes.None.ID, true, 129022, null);
			this.Scolding.resolveTooltipCallback = delegate(string str, object data)
			{
				float averageExternalTemperature2 = ((ScaldingMonitor.Instance)data).AverageExternalTemperature;
				float scoldingThreshold = ((ScaldingMonitor.Instance)data).GetScoldingThreshold();
				str = str.Replace("{ExternalTemperature}", GameUtil.GetFormattedTemperature(averageExternalTemperature2, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
				str = str.Replace("{TargetTemperature}", GameUtil.GetFormattedTemperature(scoldingThreshold, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
				return str;
			};
			this.Scolding.AddNotification(null, null, null);
			this.Cold = new StatusItem("Cold", "CREATURES", "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022, null);
			this.Cold.resolveStringCallback = delegate(string str, object data)
			{
				TemperatureVulnerable temperatureVulnerable3 = (TemperatureVulnerable)data;
				return string.Format(str, GameUtil.GetFormattedTemperature(temperatureVulnerable3.TemperatureWarningLow, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false), GameUtil.GetFormattedTemperature(temperatureVulnerable3.TemperatureWarningHigh, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
			};
			this.Cold_Crop = new StatusItem("Cold_Crop", "CREATURES", "status_item_plant_temperature", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022, null);
			this.Cold_Crop.resolveStringCallback = delegate(string str, object data)
			{
				TemperatureVulnerable temperatureVulnerable4 = (TemperatureVulnerable)data;
				str = str.Replace("low_temperature", GameUtil.GetFormattedTemperature(temperatureVulnerable4.TemperatureWarningLow, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
				str = str.Replace("high_temperature", GameUtil.GetFormattedTemperature(temperatureVulnerable4.TemperatureWarningHigh, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
				return str;
			};
			this.Crop_Too_Dark = new StatusItem("Crop_Too_Dark", "CREATURES", "status_item_plant_light", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022, null);
			this.Crop_Too_Bright = new StatusItem("Crop_Too_Bright", "CREATURES", "status_item_plant_light", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022, null);
			this.Crop_Blighted = new StatusItem("Crop_Blighted", "CREATURES", "status_item_plant_blighted", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022, null);
			this.Hyperthermia = new StatusItem("Hyperthermia", "CREATURES", "", StatusItem.IconType.Exclamation, NotificationType.Bad, false, OverlayModes.None.ID, true, 129022, null);
			this.Hyperthermia.resolveTooltipCallback = delegate(string str, object data)
			{
				float value = ((TemperatureMonitor.Instance)data).temperature.value;
				float hyperthermiaThreshold = ((TemperatureMonitor.Instance)data).HyperthermiaThreshold;
				str = str.Replace("{InternalTemperature}", GameUtil.GetFormattedTemperature(value, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
				str = str.Replace("{TargetTemperature}", GameUtil.GetFormattedTemperature(hyperthermiaThreshold, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
				return str;
			};
			this.Hypothermia = new StatusItem("Hypothermia", "CREATURES", "", StatusItem.IconType.Exclamation, NotificationType.Bad, false, OverlayModes.None.ID, true, 129022, null);
			this.Hypothermia.resolveTooltipCallback = delegate(string str, object data)
			{
				float value2 = ((TemperatureMonitor.Instance)data).temperature.value;
				float hypothermiaThreshold = ((TemperatureMonitor.Instance)data).HypothermiaThreshold;
				str = str.Replace("{InternalTemperature}", GameUtil.GetFormattedTemperature(value2, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
				str = str.Replace("{TargetTemperature}", GameUtil.GetFormattedTemperature(hypothermiaThreshold, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
				return str;
			};
			this.Suffocating = new StatusItem("Suffocating", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.Hatching = new StatusItem("Hatching", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.Incubating = new StatusItem("Incubating", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.Drowning = new StatusItem("Drowning", "CREATURES", "status_item_flooded", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			this.Drowning.resolveStringCallback = (string str, object data) => str;
			this.AquaticCreatureSuffocating = new StatusItem("AquaticCreatureSuffocating", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Bad, false, OverlayModes.None.ID, true, 129022, null);
			this.AquaticCreatureSuffocating.resolveTooltipCallback = delegate(string str, object data)
			{
				AquaticCreatureSuffocationMonitor.Instance instance = (AquaticCreatureSuffocationMonitor.Instance)data;
				str = GameUtil.SafeStringFormat(str, new object[] { GameUtil.GetFormattedCycles(instance.TimeUntilDeath, "F1", false) });
				return str;
			};
			this.ProducingSugarWater = new StatusItem("ProducingSugarWater", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, true, 129022, null);
			this.ProducingSugarWater.resolveStringCallback = delegate(string str, object data)
			{
				SpaceTreePlant.Instance instance2 = (SpaceTreePlant.Instance)data;
				str = str.Replace("{0}", GameUtil.GetFormattedPercent(instance2.CurrentProductionProgress * 100f, GameUtil.TimeSlice.None));
				return str;
			};
			this.ProducingSugarWater.resolveTooltipCallback = delegate(string str, object data)
			{
				SpaceTreePlant.Instance instance3 = (SpaceTreePlant.Instance)data;
				PlantBranchGrower.Instance smi = instance3.GetSMI<PlantBranchGrower.Instance>();
				for (int i = 0; i < instance3.def.OptimalAmountOfBranches; i++)
				{
					string text = CREATURES.STATUSITEMS.PRODUCINGSUGARWATER.BRANCH_LINE_MISSING;
					string text2 = SpaceTreeBranchConfig.BRANCH_NAMES[i];
					GameObject branch = smi.GetBranch(i);
					if (branch != null)
					{
						SpaceTreeBranch.Instance smi2 = branch.GetSMI<SpaceTreeBranch.Instance>();
						if (smi2 != null && !smi2.isMasterNull)
						{
							if (smi2.IsBranchFullyGrown)
							{
								string formattedPercent = GameUtil.GetFormattedPercent(smi2.Productivity * 100f, GameUtil.TimeSlice.None);
								text = CREATURES.STATUSITEMS.PRODUCINGSUGARWATER.BRANCH_LINE;
								text = text.Replace("{1}", formattedPercent);
							}
							else
							{
								string formattedPercent2 = GameUtil.GetFormattedPercent(smi2.GetcurrentGrowthPercentage() * 100f, GameUtil.TimeSlice.None);
								text = CREATURES.STATUSITEMS.PRODUCINGSUGARWATER.BRANCH_LINE_GROWING;
								text = text.Replace("{1}", formattedPercent2);
							}
						}
					}
					text = text.Replace("{0}", text2);
					string text3 = "{BRANCH_" + i.ToString() + "}";
					str = str.Replace(text3, text);
				}
				str = str.Replace("{0}", GameUtil.GetFormattedMass(instance3.GetProductionSpeed() * 20f / instance3.OptimalProductionDuration, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				str = str.Replace("{1}", instance3.def.OptimalAmountOfBranches.ToString());
				str = str.Replace("{2}", GameUtil.GetFormattedLux(10000));
				return str;
			};
			this.SugarWaterProductionPaused = new StatusItem("SugarWaterProductionPaused", "CREATURES", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			this.SugarWaterProductionPaused.resolveStringCallback = delegate(string str, object data)
			{
				SpaceTreePlant.Instance instance4 = (SpaceTreePlant.Instance)data;
				str = str.Replace("{0}", GameUtil.GetFormattedPercent(instance4.CurrentProductionProgress * 100f, GameUtil.TimeSlice.None));
				return str;
			};
			this.SugarWaterProductionPaused.resolveTooltipCallback = delegate(string str, object data)
			{
				SpaceTreePlant.Instance instance5 = (SpaceTreePlant.Instance)data;
				PlantBranchGrower.Instance smi3 = instance5.GetSMI<PlantBranchGrower.Instance>();
				for (int j = 0; j < instance5.def.OptimalAmountOfBranches; j++)
				{
					string text4 = CREATURES.STATUSITEMS.SUGARWATERPRODUCTIONPAUSED.BRANCH_LINE_MISSING;
					string text5 = SpaceTreeBranchConfig.BRANCH_NAMES[j];
					GameObject branch2 = smi3.GetBranch(j);
					if (branch2 != null)
					{
						SpaceTreeBranch.Instance smi4 = branch2.GetSMI<SpaceTreeBranch.Instance>();
						if (smi4 != null && !smi4.isMasterNull)
						{
							if (smi4.IsBranchFullyGrown)
							{
								string formattedPercent3 = GameUtil.GetFormattedPercent(smi4.Productivity * 100f, GameUtil.TimeSlice.None);
								text4 = CREATURES.STATUSITEMS.SUGARWATERPRODUCTIONPAUSED.BRANCH_LINE;
								text4 = text4.Replace("{1}", formattedPercent3);
							}
							else
							{
								string formattedPercent4 = GameUtil.GetFormattedPercent(smi4.GetcurrentGrowthPercentage() * 100f, GameUtil.TimeSlice.None);
								text4 = CREATURES.STATUSITEMS.SUGARWATERPRODUCTIONPAUSED.BRANCH_LINE_GROWING;
								text4 = text4.Replace("{1}", formattedPercent4);
							}
						}
					}
					text4 = text4.Replace("{0}", text5);
					string text6 = "{BRANCH_" + j.ToString() + "}";
					str = str.Replace(text6, text4);
				}
				str = str.Replace("{0}", instance5.def.OptimalAmountOfBranches.ToString());
				str = str.Replace("{1}", GameUtil.GetFormattedLux(10000));
				return str;
			};
			this.SugarWaterProductionWilted = new StatusItem("SugarWaterProductionWilted", "CREATURES", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			this.SugarWaterProductionWilted.resolveStringCallback = delegate(string str, object data)
			{
				SpaceTreePlant.Instance instance6 = (SpaceTreePlant.Instance)data;
				str = str.Replace("{0}", GameUtil.GetFormattedPercent(instance6.CurrentProductionProgress * 100f, GameUtil.TimeSlice.None));
				return str;
			};
			this.SpaceTreeBranchLightStatus = new StatusItem("SpaceTreeBranchLightStatus", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, true, 129022, null);
			this.SpaceTreeBranchLightStatus.resolveStringCallback = delegate(string str, object data)
			{
				SpaceTreeBranch.Instance instance7 = (SpaceTreeBranch.Instance)data;
				str = str.Replace("{0}", GameUtil.GetFormattedPercent(instance7.Productivity * 100f, GameUtil.TimeSlice.None));
				return str;
			};
			this.SpaceTreeBranchLightStatus.resolveTooltipCallback = delegate(string str, object data)
			{
				SpaceTreeBranch.Instance instance8 = (SpaceTreeBranch.Instance)data;
				str = str.Replace("{0}", GameUtil.GetFormattedLux(instance8.def.OPTIMAL_LUX_LEVELS));
				str = str.Replace("{1}", GameUtil.GetFormattedLux(instance8.CurrentAmountOfLux));
				return str;
			};
			this.Saturated = new StatusItem("Saturated", "CREATURES", "status_item_flooded", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			this.Saturated.resolveStringCallback = (string str, object data) => str;
			this.DryingOut = new StatusItem("DryingOut", "CREATURES", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 1026, null);
			this.DryingOut.resolveStringCallback = (string str, object data) => str;
			this.ReadyForHarvest = new StatusItem("ReadyForHarvest", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 1026, null);
			this.ReadyForHarvest_Branch = new StatusItem("ReadyForHarvest_Branch", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 1026, null);
			this.Growing = new StatusItem("Growing", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 1026, null);
			this.Growing.resolveStringCallback = delegate(string str, object data)
			{
				IManageGrowingStates manageGrowingStates = (IManageGrowingStates)data;
				if (manageGrowingStates.GetCropComponent() != null)
				{
					float num = manageGrowingStates.TimeUntilNextHarvest();
					str = str.Replace("{TimeUntilNextHarvest}", GameUtil.GetFormattedCycles(num, "F1", false));
				}
				float num2 = 100f * manageGrowingStates.PercentGrown();
				str = str.Replace("{PercentGrow}", Math.Floor((double)Math.Max(num2, 0f)).ToString("F0"));
				return str;
			};
			this.GrowingFruit = new StatusItem("GrowingFruit", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 1026, null);
			this.GrowingFruit.resolveStringCallback = delegate(string str, object data)
			{
				IManageGrowingStates manageGrowingStates2 = (IManageGrowingStates)data;
				if (manageGrowingStates2.GetCropComponent() != null)
				{
					float num3 = manageGrowingStates2.TimeUntilNextHarvest();
					str = str.Replace("{TimeUntilNextHarvest}", GameUtil.GetFormattedCycles(num3, "F1", false));
				}
				float num4 = 100f * manageGrowingStates2.PercentGrown();
				str = str.Replace("{PercentGrow}", Math.Floor((double)Math.Max(num4, 0f)).ToString("F0"));
				return str;
			};
			this.CarnivorousPlantAwaitingVictim = new StatusItem("CarnivorousPlantAwaitingVictim", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 1026, null);
			this.CarnivorousPlantAwaitingVictim.resolveTooltipCallback = delegate(string str, object data)
			{
				string[] formattedPossiblePreyList = ((IPlantConsumeEntities)data).GetFormattedPossiblePreyList();
				string text7 = "";
				for (int k = 0; k < formattedPossiblePreyList.Length; k++)
				{
					text7 = text7 + "\n" + GameUtil.SafeStringFormat(CREATURES.STATUSITEMS.CARNIVOROUSPLANTAWAITINGVICTIM.TOOLTIP_ITEM, new object[] { formattedPossiblePreyList[k] });
				}
				str += text7;
				return str;
			};
			this.EnvironmentTooWarm = new StatusItem("EnvironmentTooWarm", "CREATURES", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			this.EnvironmentTooWarm.resolveStringCallback = delegate(string str, object data)
			{
				float num5 = Grid.Temperature[Grid.PosToCell(((TemperatureVulnerable)data).gameObject)];
				float num6 = ((TemperatureVulnerable)data).TemperatureLethalHigh - 1f;
				str = str.Replace("{ExternalTemperature}", GameUtil.GetFormattedTemperature(num5, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
				str = str.Replace("{TargetTemperature}", GameUtil.GetFormattedTemperature(num6, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
				return str;
			};
			this.EnvironmentTooCold = new StatusItem("EnvironmentTooCold", "CREATURES", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			this.EnvironmentTooCold.resolveStringCallback = delegate(string str, object data)
			{
				float num7 = Grid.Temperature[Grid.PosToCell(((TemperatureVulnerable)data).gameObject)];
				float num8 = ((TemperatureVulnerable)data).TemperatureLethalLow + 1f;
				str = str.Replace("{ExternalTemperature}", GameUtil.GetFormattedTemperature(num7, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
				str = str.Replace("{TargetTemperature}", GameUtil.GetFormattedTemperature(num8, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
				return str;
			};
			this.Entombed = new StatusItem("Entombed", "CREATURES", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			this.Entombed.resolveStringCallback = (string str, object go) => str;
			this.Entombed.resolveTooltipCallback = delegate(string str, object go)
			{
				GameObject gameObject = go as GameObject;
				return string.Format(str, GameUtil.GetIdentityDescriptor(gameObject, GameUtil.IdentityDescriptorTense.Normal));
			};
			this.Wilting = new StatusItem("Wilting", "CREATURES", "status_item_need_plant", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 1026, null);
			this.Wilting.resolveStringCallback = delegate(string str, object data)
			{
				Growing growing = data as Growing;
				if (growing != null && data != null)
				{
					AmountInstance amountInstance = growing.gameObject.GetAmounts().Get(Db.Get().Amounts.Maturity);
					str = str.Replace("{TimeUntilNextHarvest}", GameUtil.GetFormattedCycles(Mathf.Min(amountInstance.GetMax(), growing.TimeUntilNextHarvest()), "F1", false));
				}
				str = str.Replace("{Reasons}", (data as KMonoBehaviour).GetComponent<WiltCondition>().WiltCausesString());
				return str;
			};
			this.WiltingDomestic = new StatusItem("WiltingDomestic", "CREATURES", "status_item_need_plant", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 1026, null);
			this.WiltingDomestic.resolveStringCallback = delegate(string str, object data)
			{
				Growing growing2 = data as Growing;
				if (growing2 != null && data != null)
				{
					AmountInstance amountInstance2 = growing2.gameObject.GetAmounts().Get(Db.Get().Amounts.Maturity);
					str = str.Replace("{TimeUntilNextHarvest}", GameUtil.GetFormattedCycles(Mathf.Min(amountInstance2.GetMax(), growing2.TimeUntilNextHarvest()), "F1", false));
				}
				str = str.Replace("{Reasons}", (data as KMonoBehaviour).GetComponent<WiltCondition>().WiltCausesString());
				return str;
			};
			this.WiltingNonGrowing = new StatusItem("WiltingNonGrowing", "CREATURES", "status_item_need_plant", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 1026, null);
			this.WiltingNonGrowing.resolveStringCallback = delegate(string str, object data)
			{
				str = CREATURES.STATUSITEMS.WILTING_NON_GROWING_PLANT.NAME;
				str = str.Replace("{Reasons}", (data as WiltCondition).WiltCausesString());
				return str;
			};
			this.WiltingNonGrowingDomestic = new StatusItem("WiltingNonGrowing", "CREATURES", "status_item_need_plant", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 1026, null);
			this.WiltingNonGrowingDomestic.resolveStringCallback = delegate(string str, object data)
			{
				str = CREATURES.STATUSITEMS.WILTING_NON_GROWING_PLANT.NAME;
				str = str.Replace("{Reasons}", (data as WiltCondition).WiltCausesString());
				return str;
			};
			this.WrongAtmosphere = new StatusItem("WrongAtmosphere", "CREATURES", "status_item_plant_atmosphere", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022, null);
			this.WrongAtmosphere.resolveStringCallback = delegate(string str, object data)
			{
				string text8 = "";
				foreach (Element element in (data as PressureVulnerable).safe_atmospheres)
				{
					text8 = text8 + "\n    •  " + element.name;
				}
				str = str.Replace("{elements}", text8);
				return str;
			};
			this.AtmosphericPressureTooLow = new StatusItem("AtmosphericPressureTooLow", "CREATURES", "status_item_plant_atmosphere", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022, null);
			this.AtmosphericPressureTooLow.resolveStringCallback = delegate(string str, object data)
			{
				PressureVulnerable pressureVulnerable = (PressureVulnerable)data;
				str = str.Replace("{low_mass}", GameUtil.GetFormattedMass(pressureVulnerable.pressureWarning_Low, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				str = str.Replace("{high_mass}", GameUtil.GetFormattedMass(pressureVulnerable.pressureWarning_High, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				return str;
			};
			this.AtmosphericPressureTooHigh = new StatusItem("AtmosphericPressureTooHigh", "CREATURES", "status_item_plant_atmosphere", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022, null);
			this.AtmosphericPressureTooHigh.resolveStringCallback = delegate(string str, object data)
			{
				PressureVulnerable pressureVulnerable2 = (PressureVulnerable)data;
				str = str.Replace("{low_mass}", GameUtil.GetFormattedMass(pressureVulnerable2.pressureWarning_Low, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				str = str.Replace("{high_mass}", GameUtil.GetFormattedMass(pressureVulnerable2.pressureWarning_High, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				return str;
			};
			this.HealthStatus = new StatusItem("HealthStatus", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.HealthStatus.resolveStringCallback = delegate(string str, object data)
			{
				string text9 = "";
				switch ((Health.HealthState)data)
				{
				case Health.HealthState.Perfect:
					text9 = MISC.STATUSITEMS.HEALTHSTATUS.PERFECT.NAME;
					break;
				case Health.HealthState.Scuffed:
					text9 = MISC.STATUSITEMS.HEALTHSTATUS.SCUFFED.NAME;
					break;
				case Health.HealthState.Injured:
					text9 = MISC.STATUSITEMS.HEALTHSTATUS.INJURED.NAME;
					break;
				case Health.HealthState.Critical:
					text9 = MISC.STATUSITEMS.HEALTHSTATUS.CRITICAL.NAME;
					break;
				case Health.HealthState.Incapacitated:
					text9 = MISC.STATUSITEMS.HEALTHSTATUS.INCAPACITATED.NAME;
					break;
				case Health.HealthState.Dead:
					text9 = MISC.STATUSITEMS.HEALTHSTATUS.DEAD.NAME;
					break;
				}
				str = str.Replace("{healthState}", text9);
				return str;
			};
			this.HealthStatus.resolveTooltipCallback = delegate(string str, object data)
			{
				string text10 = "";
				switch ((Health.HealthState)data)
				{
				case Health.HealthState.Perfect:
					text10 = MISC.STATUSITEMS.HEALTHSTATUS.PERFECT.TOOLTIP;
					break;
				case Health.HealthState.Scuffed:
					text10 = MISC.STATUSITEMS.HEALTHSTATUS.SCUFFED.TOOLTIP;
					break;
				case Health.HealthState.Injured:
					text10 = MISC.STATUSITEMS.HEALTHSTATUS.INJURED.TOOLTIP;
					break;
				case Health.HealthState.Critical:
					text10 = MISC.STATUSITEMS.HEALTHSTATUS.CRITICAL.TOOLTIP;
					break;
				case Health.HealthState.Incapacitated:
					text10 = MISC.STATUSITEMS.HEALTHSTATUS.INCAPACITATED.TOOLTIP;
					break;
				case Health.HealthState.Dead:
					text10 = MISC.STATUSITEMS.HEALTHSTATUS.DEAD.TOOLTIP;
					break;
				}
				str = str.Replace("{healthState}", text10);
				return str;
			};
			this.Barren = new StatusItem("Barren", "CREATURES", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			this.NeedsFertilizer = new StatusItem("NeedsFertilizer", "CREATURES", "status_item_plant_solid", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022, null);
			Func<string, object, string> func = (string str, object data) => str;
			this.NeedsFertilizer.resolveStringCallback = func;
			this.NeedsIrrigation = new StatusItem("NeedsIrrigation", "CREATURES", "status_item_plant_liquid", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022, null);
			Func<string, object, string> func2 = (string str, object data) => str;
			this.NeedsIrrigation.resolveStringCallback = func2;
			this.WrongFertilizer = new StatusItem("WrongFertilizer", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			Func<string, object, string> func3 = (string str, object data) => str;
			this.WrongFertilizer.resolveStringCallback = func3;
			this.WrongFertilizerMajor = new StatusItem("WrongFertilizerMajor", "CREATURES", "status_item_fabricator_empty", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			this.WrongFertilizerMajor.resolveStringCallback = func3;
			this.WrongIrrigation = new StatusItem("WrongIrrigation", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			Func<string, object, string> func4 = (string str, object data) => str;
			this.WrongIrrigation.resolveStringCallback = func4;
			this.WrongIrrigationMajor = new StatusItem("WrongIrrigationMajor", "CREATURES", "status_item_fabricator_empty", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			this.WrongIrrigationMajor.resolveStringCallback = func4;
			this.CantAcceptFertilizer = new StatusItem("CantAcceptFertilizer", "CREATURES", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			this.Rotting = new StatusItem("Rotting", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.Rotting.resolveStringCallback = (string str, object data) => str.Replace("{RotTemperature}", GameUtil.GetFormattedTemperature(277.15f, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
			this.Fresh = new StatusItem("Fresh", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.Fresh.resolveStringCallback = delegate(string str, object data)
			{
				Rottable.Instance instance9 = (Rottable.Instance)data;
				return str.Replace("{RotPercentage}", "(" + Util.FormatWholeNumber(instance9.RotConstitutionPercentage * 100f) + "%)");
			};
			this.Fresh.resolveTooltipCallback = delegate(string str, object data)
			{
				Rottable.Instance instance10 = (Rottable.Instance)data;
				return str.Replace("{RotTooltip}", instance10.GetToolTip());
			};
			this.Stale = new StatusItem("Stale", "CREATURES", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			this.Stale.resolveStringCallback = delegate(string str, object data)
			{
				Rottable.Instance instance11 = (Rottable.Instance)data;
				return str.Replace("{RotPercentage}", "(" + Util.FormatWholeNumber(instance11.RotConstitutionPercentage * 100f) + "%)");
			};
			this.Stale.resolveTooltipCallback = delegate(string str, object data)
			{
				Rottable.Instance instance12 = (Rottable.Instance)data;
				return str.Replace("{RotTooltip}", instance12.GetToolTip());
			};
			this.Spoiled = new StatusItem("Spoiled", "CREATURES", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			Func<string, object, string> func5 = delegate(string str, object data)
			{
				IRottable rottable = (IRottable)data;
				return str.Replace("{RotTemperature}", GameUtil.GetFormattedTemperature(rottable.RotTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)).Replace("{PreserveTemperature}", GameUtil.GetFormattedTemperature(rottable.PreserveTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
			};
			this.Refrigerated = new StatusItem("Refrigerated", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.Refrigerated.resolveStringCallback = func5;
			this.RefrigeratedFrozen = new StatusItem("RefrigeratedFrozen", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.RefrigeratedFrozen.resolveStringCallback = func5;
			this.Unrefrigerated = new StatusItem("Unrefrigerated", "CREATURES", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			this.Unrefrigerated.resolveStringCallback = func5;
			this.SterilizingAtmosphere = new StatusItem("SterilizingAtmosphere", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.ContaminatedAtmosphere = new StatusItem("ContaminatedAtmosphere", "CREATURES", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			this.Old = new StatusItem("Old", "CREATURES", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			this.Old.resolveTooltipCallback = delegate(string str, object data)
			{
				AgeMonitor.Instance instance13 = (AgeMonitor.Instance)data;
				return str.Replace("{TimeUntilDeath}", GameUtil.GetFormattedCycles(instance13.CyclesUntilDeath * 600f, "F1", false));
			};
			this.ExchangingElementConsume = new StatusItem("ExchangingElementConsume", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.ExchangingElementConsume.resolveStringCallback = delegate(string str, object data)
			{
				EntityElementExchanger.StatesInstance statesInstance = (EntityElementExchanger.StatesInstance)data;
				str = str.Replace("{ConsumeElement}", ElementLoader.FindElementByHash(statesInstance.master.consumedElement).tag.ProperName());
				str = str.Replace("{ConsumeRate}", GameUtil.GetFormattedMass(statesInstance.master.consumeRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				return str;
			};
			this.ExchangingElementConsume.resolveTooltipCallback = delegate(string str, object data)
			{
				EntityElementExchanger.StatesInstance statesInstance2 = (EntityElementExchanger.StatesInstance)data;
				str = str.Replace("{ConsumeElement}", ElementLoader.FindElementByHash(statesInstance2.master.consumedElement).tag.ProperName());
				str = str.Replace("{ConsumeRate}", GameUtil.GetFormattedMass(statesInstance2.master.consumeRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				return str;
			};
			this.ExchangingElementOutput = new StatusItem("ExchangingElementOutput", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.ExchangingElementOutput.resolveStringCallback = delegate(string str, object data)
			{
				EntityElementExchanger.StatesInstance statesInstance3 = (EntityElementExchanger.StatesInstance)data;
				str = str.Replace("{OutputElement}", ElementLoader.FindElementByHash(statesInstance3.master.emittedElement).tag.ProperName());
				str = str.Replace("{OutputRate}", GameUtil.GetFormattedMass(statesInstance3.master.consumeRate * statesInstance3.master.exchangeRatio, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				return str;
			};
			this.ExchangingElementOutput.resolveTooltipCallback = delegate(string str, object data)
			{
				EntityElementExchanger.StatesInstance statesInstance4 = (EntityElementExchanger.StatesInstance)data;
				str = str.Replace("{OutputElement}", ElementLoader.FindElementByHash(statesInstance4.master.emittedElement).tag.ProperName());
				str = str.Replace("{OutputRate}", GameUtil.GetFormattedMass(statesInstance4.master.consumeRate * statesInstance4.master.exchangeRatio, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				return str;
			};
			this.Hungry = new StatusItem("Hungry", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.Hungry.resolveTooltipCallback = delegate(string str, object data)
			{
				Diet diet = ((CreatureCalorieMonitor.Instance)data).stomach.diet;
				if (diet.consumedTags.Count > 0)
				{
					string[] array = diet.consumedTags.Select((KeyValuePair<Tag, float> t) => t.Key.ProperName()).ToArray<string>();
					if (array.Length > 3)
					{
						array = new string[]
						{
							array[0],
							array[1],
							array[2],
							"..."
						};
					}
					string text11 = string.Join(", ", array);
					return str + "\n" + UI.BUILDINGEFFECTS.DIET_CONSUMED.text.Replace("{Foodlist}", text11);
				}
				return str;
			};
			this.HiveHungry = new StatusItem("HiveHungry", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.HiveHungry.resolveTooltipCallback = delegate(string str, object data)
			{
				Diet diet2 = ((BeehiveCalorieMonitor.Instance)data).stomach.diet;
				if (diet2.consumedTags.Count > 0)
				{
					string[] array2 = diet2.consumedTags.Select((KeyValuePair<Tag, float> t) => t.Key.ProperName()).ToArray<string>();
					if (array2.Length > 3)
					{
						array2 = new string[]
						{
							array2[0],
							array2[1],
							array2[2],
							"..."
						};
					}
					string text12 = string.Join(", ", array2);
					return str + "\n" + UI.BUILDINGEFFECTS.DIET_STORED.text.Replace("{Foodlist}", text12);
				}
				return str;
			};
			this.NoSleepSpot = new StatusItem("NoSleepSpot", "CREATURES", "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			this.OriginalPlantMutation = new StatusItem("OriginalPlantMutation", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.UnknownMutation = new StatusItem("UnknownMutation", "CREATURES", "status_item_unknown_mutation", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			this.SpecificPlantMutation = new StatusItem("SpecificPlantMutation", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.SpecificPlantMutation.resolveStringCallback = delegate(string str, object data)
			{
				PlantMutation plantMutation = (PlantMutation)data;
				return str.Replace("{MutationName}", plantMutation.Name);
			};
			this.SpecificPlantMutation.resolveTooltipCallback = delegate(string str, object data)
			{
				PlantMutation plantMutation2 = (PlantMutation)data;
				str = str.Replace("{MutationName}", plantMutation2.Name);
				return str + "\n" + plantMutation2.GetTooltip();
			};
			this.Crop_Too_NonRadiated = new StatusItem("Crop_Too_NonRadiated", "CREATURES", "status_item_plant_light", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022, null);
			this.Crop_Too_Radiated = new StatusItem("Crop_Too_Radiated", "CREATURES", "status_item_plant_light", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022, null);
			this.ElementGrowthGrowing = new StatusItem("Element_Growth_Growing", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.ElementGrowthGrowing.resolveTooltipCallback = delegate(string str, object data)
			{
				ElementGrowthMonitor.Instance instance14 = (ElementGrowthMonitor.Instance)data;
				StringBuilder stringBuilder = new StringBuilder(str, str.Length * 2);
				stringBuilder.Replace("{templo}", GameUtil.GetFormattedTemperature(instance14.def.minTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
				stringBuilder.Replace("{temphi}", GameUtil.GetFormattedTemperature(instance14.def.maxTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
				if (instance14.lastConsumedTemperature > 0f)
				{
					stringBuilder.Append("\n\n");
					stringBuilder.Append(CREATURES.STATUSITEMS.ELEMENT_GROWTH_GROWING.PREFERRED_TEMP);
					stringBuilder.Replace("{element}", ElementLoader.FindElementByHash(instance14.lastConsumedElement).name);
					stringBuilder.Replace("{temperature}", GameUtil.GetFormattedTemperature(instance14.lastConsumedTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
				}
				return stringBuilder.ToString();
			};
			this.ElementGrowthStunted = new StatusItem("Element_Growth_Stunted", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Bad, false, OverlayModes.None.ID, true, 129022, null);
			this.ElementGrowthStunted.resolveTooltipCallback = this.ElementGrowthGrowing.resolveTooltipCallback;
			this.ElementGrowthStunted.resolveStringCallback = delegate(string str, object data)
			{
				ElementGrowthMonitor.Instance instance15 = (ElementGrowthMonitor.Instance)data;
				string text13 = ((instance15.lastConsumedTemperature < instance15.def.minTemperature) ? CREATURES.STATUSITEMS.ELEMENT_GROWTH_STUNTED.TOO_COLD : CREATURES.STATUSITEMS.ELEMENT_GROWTH_STUNTED.TOO_HOT);
				str = str.Replace("{reason}", text13);
				return str;
			};
			this.ElementGrowthHalted = new StatusItem("Element_Growth_Halted", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Bad, false, OverlayModes.None.ID, true, 129022, null);
			this.ElementGrowthHalted.resolveTooltipCallback = this.ElementGrowthGrowing.resolveTooltipCallback;
			this.ElementGrowthComplete = new StatusItem("Element_Growth_Complete", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Bad, false, OverlayModes.None.ID, true, 129022, null);
			this.ElementGrowthComplete.resolveTooltipCallback = this.ElementGrowthGrowing.resolveTooltipCallback;
			this.LookingForFood = new StatusItem("Hungry", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.LookingForGas = new StatusItem("LookingForGas", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.LookingForLiquid = new StatusItem("LookingForLiquid", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.Beckoning = new StatusItem("Beckoning", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.BeckoningBlocked = new StatusItem("BeckoningBlocked", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Bad, false, OverlayModes.None.ID, true, 129022, null);
			this.MilkProducer = new StatusItem("MilkProducer", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.MilkProducer.resolveStringCallback = delegate(string str, object data)
			{
				MilkProductionMonitor.Instance instance16 = (MilkProductionMonitor.Instance)data;
				str = str.Replace("{amount}", GameUtil.GetFormattedMass(instance16.MilkPercentage, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				return str;
			};
			this.GettingRanched = new StatusItem("Getting_Ranched", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.GettingMilked = new StatusItem("Getting_Milked", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.MilkFull = new StatusItem("MilkFull", "CREATURES", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			this.TemperatureHotUncomfortable = new StatusItem("TemperatureHotUncomfortable", CREATURES.STATUSITEMS.TEMPERATURE_HOT_UNCOMFORTABLE.NAME, CREATURES.STATUSITEMS.TEMPERATURE_HOT_UNCOMFORTABLE.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, 129022, true, null);
			this.TemperatureHotDeadly = new StatusItem("TemperatureHotDeadly", CREATURES.STATUSITEMS.TEMPERATURE_HOT_DEADLY.NAME, CREATURES.STATUSITEMS.TEMPERATURE_HOT_DEADLY.TOOLTIP, "", StatusItem.IconType.Exclamation, NotificationType.Bad, false, OverlayModes.None.ID, 129022, true, null);
			this.TemperatureColdUncomfortable = new StatusItem("TemperatureColdUncomfortable", CREATURES.STATUSITEMS.TEMPERATURE_COLD_UNCOMFORTABLE.NAME, CREATURES.STATUSITEMS.TEMPERATURE_COLD_UNCOMFORTABLE.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, 129022, true, null);
			this.TemperatureColdDeadly = new StatusItem("TemperatureColdDeadly", CREATURES.STATUSITEMS.TEMPERATURE_COLD_DEADLY.NAME, CREATURES.STATUSITEMS.TEMPERATURE_COLD_DEADLY.TOOLTIP, "", StatusItem.IconType.Exclamation, NotificationType.Bad, false, OverlayModes.None.ID, 129022, true, null);
			this.TemperatureHotUncomfortable.resolveStringCallback = delegate(string str, object obj)
			{
				CritterTemperatureMonitor.Instance instance17 = (CritterTemperatureMonitor.Instance)obj;
				return string.Format(str, new object[]
				{
					GameUtil.GetFormattedTemperature(instance17.GetTemperatureInternal(), GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false),
					GameUtil.GetFormattedTemperature(instance17.def.temperatureColdUncomfortable, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false),
					GameUtil.GetFormattedTemperature(instance17.def.temperatureHotUncomfortable, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false),
					Effect.CreateTooltip(instance17.sm.uncomfortableEffect, false, "\n    • ", true)
				});
			};
			this.TemperatureHotDeadly.resolveStringCallback = delegate(string str, object obj)
			{
				CritterTemperatureMonitor.Instance instance18 = (CritterTemperatureMonitor.Instance)obj;
				return string.Format(str, new object[]
				{
					GameUtil.GetFormattedTemperature(instance18.GetTemperatureExternal(), GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false),
					GameUtil.GetFormattedTemperature(instance18.def.temperatureColdDeadly, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false),
					GameUtil.GetFormattedTemperature(instance18.def.temperatureHotDeadly, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false),
					Effect.CreateTooltip(instance18.sm.deadlyEffect, false, "\n    • ", true)
				});
			};
			this.TemperatureColdUncomfortable.resolveStringCallback = this.TemperatureHotUncomfortable.resolveStringCallback;
			this.TemperatureColdDeadly.resolveStringCallback = this.TemperatureHotDeadly.resolveStringCallback;
			this.TravelingToPollinate = new StatusItem("POLLINATING.MOVINGTO", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.Pollinating = new StatusItem("POLLINATING.INTERACTING", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.NotPollinated = new StatusItem("NOT_POLLINATED", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
		}

		// Token: 0x0400567B RID: 22139
		public StatusItem Dead;

		// Token: 0x0400567C RID: 22140
		public StatusItem HealthStatus;

		// Token: 0x0400567D RID: 22141
		public StatusItem Hot;

		// Token: 0x0400567E RID: 22142
		public StatusItem Hot_Crop;

		// Token: 0x0400567F RID: 22143
		public StatusItem Scalding;

		// Token: 0x04005680 RID: 22144
		public StatusItem Scolding;

		// Token: 0x04005681 RID: 22145
		public StatusItem Cold;

		// Token: 0x04005682 RID: 22146
		public StatusItem Cold_Crop;

		// Token: 0x04005683 RID: 22147
		public StatusItem Crop_Too_Dark;

		// Token: 0x04005684 RID: 22148
		public StatusItem Crop_Too_Bright;

		// Token: 0x04005685 RID: 22149
		public StatusItem Crop_Blighted;

		// Token: 0x04005686 RID: 22150
		public StatusItem Hypothermia;

		// Token: 0x04005687 RID: 22151
		public StatusItem Hyperthermia;

		// Token: 0x04005688 RID: 22152
		public StatusItem Suffocating;

		// Token: 0x04005689 RID: 22153
		public StatusItem AquaticCreatureSuffocating;

		// Token: 0x0400568A RID: 22154
		public StatusItem Hatching;

		// Token: 0x0400568B RID: 22155
		public StatusItem Incubating;

		// Token: 0x0400568C RID: 22156
		public StatusItem Drowning;

		// Token: 0x0400568D RID: 22157
		public StatusItem Saturated;

		// Token: 0x0400568E RID: 22158
		public StatusItem DryingOut;

		// Token: 0x0400568F RID: 22159
		public StatusItem Growing;

		// Token: 0x04005690 RID: 22160
		public StatusItem GrowingFruit;

		// Token: 0x04005691 RID: 22161
		public StatusItem CarnivorousPlantAwaitingVictim;

		// Token: 0x04005692 RID: 22162
		public StatusItem ReadyForHarvest;

		// Token: 0x04005693 RID: 22163
		public StatusItem ReadyForHarvest_Branch;

		// Token: 0x04005694 RID: 22164
		public StatusItem EnvironmentTooWarm;

		// Token: 0x04005695 RID: 22165
		public StatusItem EnvironmentTooCold;

		// Token: 0x04005696 RID: 22166
		public StatusItem Entombed;

		// Token: 0x04005697 RID: 22167
		public StatusItem Wilting;

		// Token: 0x04005698 RID: 22168
		public StatusItem WiltingDomestic;

		// Token: 0x04005699 RID: 22169
		public StatusItem WiltingNonGrowing;

		// Token: 0x0400569A RID: 22170
		public StatusItem WiltingNonGrowingDomestic;

		// Token: 0x0400569B RID: 22171
		public StatusItem WrongAtmosphere;

		// Token: 0x0400569C RID: 22172
		public StatusItem AtmosphericPressureTooLow;

		// Token: 0x0400569D RID: 22173
		public StatusItem AtmosphericPressureTooHigh;

		// Token: 0x0400569E RID: 22174
		public StatusItem Barren;

		// Token: 0x0400569F RID: 22175
		public StatusItem NeedsFertilizer;

		// Token: 0x040056A0 RID: 22176
		public StatusItem NeedsIrrigation;

		// Token: 0x040056A1 RID: 22177
		public StatusItem WrongTemperature;

		// Token: 0x040056A2 RID: 22178
		public StatusItem WrongFertilizer;

		// Token: 0x040056A3 RID: 22179
		public StatusItem WrongIrrigation;

		// Token: 0x040056A4 RID: 22180
		public StatusItem WrongFertilizerMajor;

		// Token: 0x040056A5 RID: 22181
		public StatusItem WrongIrrigationMajor;

		// Token: 0x040056A6 RID: 22182
		public StatusItem CantAcceptFertilizer;

		// Token: 0x040056A7 RID: 22183
		public StatusItem CantAcceptIrrigation;

		// Token: 0x040056A8 RID: 22184
		public StatusItem Rotting;

		// Token: 0x040056A9 RID: 22185
		public StatusItem Fresh;

		// Token: 0x040056AA RID: 22186
		public StatusItem Stale;

		// Token: 0x040056AB RID: 22187
		public StatusItem Spoiled;

		// Token: 0x040056AC RID: 22188
		public StatusItem Refrigerated;

		// Token: 0x040056AD RID: 22189
		public StatusItem RefrigeratedFrozen;

		// Token: 0x040056AE RID: 22190
		public StatusItem Unrefrigerated;

		// Token: 0x040056AF RID: 22191
		public StatusItem SterilizingAtmosphere;

		// Token: 0x040056B0 RID: 22192
		public StatusItem ContaminatedAtmosphere;

		// Token: 0x040056B1 RID: 22193
		public StatusItem Old;

		// Token: 0x040056B2 RID: 22194
		public StatusItem ExchangingElementOutput;

		// Token: 0x040056B3 RID: 22195
		public StatusItem ExchangingElementConsume;

		// Token: 0x040056B4 RID: 22196
		public StatusItem Hungry;

		// Token: 0x040056B5 RID: 22197
		public StatusItem HiveHungry;

		// Token: 0x040056B6 RID: 22198
		public StatusItem NoSleepSpot;

		// Token: 0x040056B7 RID: 22199
		public StatusItem ProducingSugarWater;

		// Token: 0x040056B8 RID: 22200
		public StatusItem SugarWaterProductionPaused;

		// Token: 0x040056B9 RID: 22201
		public StatusItem SugarWaterProductionWilted;

		// Token: 0x040056BA RID: 22202
		public StatusItem SpaceTreeBranchLightStatus;

		// Token: 0x040056BB RID: 22203
		public StatusItem OriginalPlantMutation;

		// Token: 0x040056BC RID: 22204
		public StatusItem UnknownMutation;

		// Token: 0x040056BD RID: 22205
		public StatusItem SpecificPlantMutation;

		// Token: 0x040056BE RID: 22206
		public StatusItem Crop_Too_NonRadiated;

		// Token: 0x040056BF RID: 22207
		public StatusItem Crop_Too_Radiated;

		// Token: 0x040056C0 RID: 22208
		public StatusItem ElementGrowthGrowing;

		// Token: 0x040056C1 RID: 22209
		public StatusItem ElementGrowthStunted;

		// Token: 0x040056C2 RID: 22210
		public StatusItem ElementGrowthHalted;

		// Token: 0x040056C3 RID: 22211
		public StatusItem ElementGrowthComplete;

		// Token: 0x040056C4 RID: 22212
		public StatusItem LookingForFood;

		// Token: 0x040056C5 RID: 22213
		public StatusItem LookingForGas;

		// Token: 0x040056C6 RID: 22214
		public StatusItem LookingForLiquid;

		// Token: 0x040056C7 RID: 22215
		public StatusItem Beckoning;

		// Token: 0x040056C8 RID: 22216
		public StatusItem BeckoningBlocked;

		// Token: 0x040056C9 RID: 22217
		public StatusItem MilkProducer;

		// Token: 0x040056CA RID: 22218
		public StatusItem MilkFull;

		// Token: 0x040056CB RID: 22219
		public StatusItem GettingRanched;

		// Token: 0x040056CC RID: 22220
		public StatusItem GettingMilked;

		// Token: 0x040056CD RID: 22221
		public StatusItem TemperatureHotUncomfortable;

		// Token: 0x040056CE RID: 22222
		public StatusItem TemperatureHotDeadly;

		// Token: 0x040056CF RID: 22223
		public StatusItem TemperatureColdUncomfortable;

		// Token: 0x040056D0 RID: 22224
		public StatusItem TemperatureColdDeadly;

		// Token: 0x040056D1 RID: 22225
		public StatusItem TravelingToPollinate;

		// Token: 0x040056D2 RID: 22226
		public StatusItem Pollinating;

		// Token: 0x040056D3 RID: 22227
		public StatusItem NotPollinated;
	}
}
