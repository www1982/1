using System;
using Klei.AI;
using STRINGS;
using TUNING;

namespace Database
{
	// Token: 0x02000EED RID: 3821
	public class DuplicantStatusItems : StatusItems
	{
		// Token: 0x0600797A RID: 31098 RVA: 0x002F9AAB File Offset: 0x002F7CAB
		public DuplicantStatusItems(ResourceSet parent)
			: base("DuplicantStatusItems", parent)
		{
			this.CreateStatusItems();
		}

		// Token: 0x0600797B RID: 31099 RVA: 0x002F9AC0 File Offset: 0x002F7CC0
		private StatusItem CreateStatusItem(string id, string prefix, string icon, StatusItem.IconType icon_type, NotificationType notification_type, bool allow_multiples, HashedString render_overlay, bool showWorldIcon = true, int status_overlays = 2)
		{
			return base.Add(new StatusItem(id, prefix, icon, icon_type, notification_type, allow_multiples, render_overlay, showWorldIcon, status_overlays, null));
		}

		// Token: 0x0600797C RID: 31100 RVA: 0x002F9AE8 File Offset: 0x002F7CE8
		private StatusItem CreateStatusItem(string id, string name, string tooltip, string icon, StatusItem.IconType icon_type, NotificationType notification_type, bool allow_multiples, HashedString render_overlay, int status_overlays = 2)
		{
			return base.Add(new StatusItem(id, name, tooltip, icon, icon_type, notification_type, allow_multiples, render_overlay, status_overlays, true, null));
		}

		// Token: 0x0600797D RID: 31101 RVA: 0x002F9B14 File Offset: 0x002F7D14
		private void CreateStatusItems()
		{
			Func<string, object, string> func = delegate(string str, object data)
			{
				Workable workable = (Workable)data;
				if (workable != null && workable.GetComponent<KSelectable>() != null)
				{
					str = str.Replace("{Target}", workable.GetComponent<KSelectable>().GetName());
				}
				return str;
			};
			Func<string, object, string> func2 = delegate(string str, object data)
			{
				Workable workable2 = (Workable)data;
				if (workable2 != null)
				{
					str = str.Replace("{Target}", workable2.GetComponent<KSelectable>().GetName());
					ComplexFabricatorWorkable complexFabricatorWorkable = workable2 as ComplexFabricatorWorkable;
					if (complexFabricatorWorkable != null)
					{
						ComplexRecipe currentWorkingOrder = complexFabricatorWorkable.CurrentWorkingOrder;
						if (currentWorkingOrder != null)
						{
							str = str.Replace("{Item}", currentWorkingOrder.FirstResult.ProperName());
						}
					}
				}
				return str;
			};
			this.BedUnreachable = this.CreateStatusItem("BedUnreachable", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 2);
			this.BedUnreachable.AddNotification(null, null, null);
			this.DailyRationLimitReached = this.CreateStatusItem("DailyRationLimitReached", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 2);
			this.DailyRationLimitReached.AddNotification(null, null, null);
			this.HoldingBreath = this.CreateStatusItem("HoldingBreath", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 2);
			this.Hungry = this.CreateStatusItem("Hungry", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 2);
			this.Slippering = this.CreateStatusItem("Slippering", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 2);
			this.Unhappy = this.CreateStatusItem("Unhappy", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 2);
			this.Unhappy.AddNotification(null, null, null);
			this.NervousBreakdown = this.CreateStatusItem("NervousBreakdown", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.Bad, false, OverlayModes.None.ID, true, 2);
			this.NervousBreakdown.AddNotification(null, null, null);
			this.NoRationsAvailable = this.CreateStatusItem("NoRationsAvailable", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.Bad, false, OverlayModes.None.ID, true, 2);
			this.PendingPacification = this.CreateStatusItem("PendingPacification", "DUPLICANTS", "status_item_pending_pacification", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.QuarantineAreaUnassigned = this.CreateStatusItem("QuarantineAreaUnassigned", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 2);
			this.QuarantineAreaUnassigned.AddNotification(null, null, null);
			this.QuarantineAreaUnreachable = this.CreateStatusItem("QuarantineAreaUnreachable", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 2);
			this.QuarantineAreaUnreachable.AddNotification(null, null, null);
			this.Quarantined = this.CreateStatusItem("Quarantined", "DUPLICANTS", "status_item_quarantined", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.RationsUnreachable = this.CreateStatusItem("RationsUnreachable", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 2);
			this.RationsUnreachable.AddNotification(null, null, null);
			this.RationsNotPermitted = this.CreateStatusItem("RationsNotPermitted", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 2);
			this.RationsNotPermitted.AddNotification(null, null, null);
			this.Rotten = this.CreateStatusItem("Rotten", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 2);
			this.Starving = this.CreateStatusItem("Starving", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.Bad, false, OverlayModes.None.ID, true, 2);
			this.Starving.AddNotification(null, null, null);
			this.Suffocating = this.CreateStatusItem("Suffocating", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.DuplicantThreatening, false, OverlayModes.None.ID, true, 2);
			this.Suffocating.AddNotification(null, null, null);
			this.Tired = this.CreateStatusItem("Tired", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 2);
			this.Idle = this.CreateStatusItem("Idle", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 2);
			this.IdleInRockets = this.CreateStatusItem("IdleInRockets", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Pacified = this.CreateStatusItem("Pacified", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Dead = this.CreateStatusItem("Dead", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 2);
			this.Dead.resolveStringCallback = delegate(string str, object data)
			{
				Death death = (Death)data;
				return str.Replace("{Death}", death.Name);
			};
			this.MoveToSuitNotRequired = this.CreateStatusItem("MoveToSuitNotRequired", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.DroppingUnusedInventory = this.CreateStatusItem("DroppingUnusedInventory", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.MovingToSafeArea = this.CreateStatusItem("MovingToSafeArea", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.ToiletUnreachable = this.CreateStatusItem("ToiletUnreachable", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 2);
			this.ToiletUnreachable.AddNotification(null, null, null);
			this.NoUsableToilets = this.CreateStatusItem("NoUsableToilets", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 2);
			this.NoUsableToilets.AddNotification(null, null, null);
			this.NoToilets = this.CreateStatusItem("NoToilets", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 2);
			this.NoToilets.AddNotification(null, null, null);
			this.BreathingO2 = this.CreateStatusItem("BreathingO2", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 130);
			this.BreathingO2.resolveStringCallback = delegate(string str, object data)
			{
				OxygenBreather oxygenBreather = (OxygenBreather)data;
				float num = ((oxygenBreather.O2Accumulator == HandleVector<int>.InvalidHandle) ? 0f : Game.Instance.accumulators.GetAverageRate(oxygenBreather.O2Accumulator));
				return str.Replace("{ConsumptionRate}", GameUtil.GetFormattedMass(-num, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
			};
			this.BreathingO2Bionic = this.CreateStatusItem("BreathingO2Bionic", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 130);
			this.BreathingO2Bionic.resolveStringCallback = delegate(string str, object data)
			{
				OxygenBreather oxygenBreather2 = (OxygenBreather)data;
				float num2 = ((oxygenBreather2.O2Accumulator == HandleVector<int>.InvalidHandle) ? 0f : Game.Instance.accumulators.GetAverageRate(oxygenBreather2.O2Accumulator));
				return str.Replace("{ConsumptionRate}", GameUtil.GetFormattedMass(-num2, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
			};
			this.EmittingCO2 = this.CreateStatusItem("EmittingCO2", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 130);
			this.EmittingCO2.resolveStringCallback = delegate(string str, object data)
			{
				OxygenBreather oxygenBreather3 = (OxygenBreather)data;
				return str.Replace("{EmittingRate}", GameUtil.GetFormattedMass(oxygenBreather3.CO2EmitRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
			};
			this.Vomiting = this.CreateStatusItem("Vomiting", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 2);
			this.Coughing = this.CreateStatusItem("Coughing", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 2);
			this.LowOxygen = this.CreateStatusItem("LowOxygen", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 2);
			this.LowOxygen.AddNotification(null, null, null);
			this.RedAlert = this.CreateStatusItem("RedAlert", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Dreaming = this.CreateStatusItem("Dreaming", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Sleeping = this.CreateStatusItem("Sleeping", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Sleeping.resolveTooltipCallback = delegate(string str, object data)
			{
				if (data is SleepChore.StatesInstance)
				{
					string stateChangeNoiseSource = ((SleepChore.StatesInstance)data).stateChangeNoiseSource;
					if (!string.IsNullOrEmpty(stateChangeNoiseSource))
					{
						string text = DUPLICANTS.STATUSITEMS.SLEEPING.TOOLTIP;
						text = text.Replace("{Disturber}", stateChangeNoiseSource);
						str += text;
					}
				}
				return str;
			};
			this.SleepingExhausted = this.CreateStatusItem("SleepingExhausted", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.Bad, false, OverlayModes.None.ID, true, 2);
			this.SleepingInterruptedByNoise = this.CreateStatusItem("SleepingInterruptedByNoise", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.SleepingInterruptedByLight = this.CreateStatusItem("SleepingInterruptedByLight", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.SleepingInterruptedByFearOfDark = this.CreateStatusItem("SleepingInterruptedByFearOfDark", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.SleepingInterruptedByMovement = this.CreateStatusItem("SleepingInterruptedByMovement", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.SleepingInterruptedByCold = this.CreateStatusItem("SleepingInterruptedByCold", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Eating = this.CreateStatusItem("Eating", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Eating.resolveStringCallback = func;
			this.Digging = this.CreateStatusItem("Digging", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Cleaning = this.CreateStatusItem("Cleaning", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Cleaning.resolveStringCallback = func;
			this.PickingUp = this.CreateStatusItem("PickingUp", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.PickingUp.resolveStringCallback = func;
			this.Mopping = this.CreateStatusItem("Mopping", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Cooking = this.CreateStatusItem("Cooking", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Cooking.resolveStringCallback = func2;
			this.Mushing = this.CreateStatusItem("Mushing", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Mushing.resolveStringCallback = func2;
			this.Researching = this.CreateStatusItem("Researching", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Researching.resolveStringCallback = delegate(string str, object data)
			{
				TechInstance activeResearch = Research.Instance.GetActiveResearch();
				if (activeResearch != null)
				{
					return str.Replace("{Tech}", activeResearch.tech.Name);
				}
				return str;
			};
			this.ResearchingFromPOI = this.CreateStatusItem("ResearchingFromPOI", DUPLICANTS.STATUSITEMS.RESEARCHING_FROM_POI.NAME, DUPLICANTS.STATUSITEMS.RESEARCHING_FROM_POI.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, 2);
			this.MissionControlling = this.CreateStatusItem("MissionControlling", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Tinkering = this.CreateStatusItem("Tinkering", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Tinkering.resolveStringCallback = delegate(string str, object data)
			{
				Tinkerable tinkerable = (Tinkerable)data;
				if (tinkerable != null)
				{
					return string.Format(str, tinkerable.tinkerMaterialTag.ProperName());
				}
				return str;
			};
			this.Storing = this.CreateStatusItem("Storing", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Storing.resolveStringCallback = delegate(string str, object data)
			{
				Workable workable3 = (Workable)data;
				if (workable3 != null && workable3.worker as StandardWorker != null)
				{
					KSelectable component = workable3.GetComponent<KSelectable>();
					if (component)
					{
						str = str.Replace("{Target}", component.GetName());
					}
					Pickupable pickupable = (workable3.worker as StandardWorker).workCompleteData as Pickupable;
					if (workable3.worker != null && pickupable)
					{
						KSelectable component2 = pickupable.GetComponent<KSelectable>();
						if (component2)
						{
							str = str.Replace("{Item}", component2.GetName());
						}
					}
				}
				return str;
			};
			this.Building = this.CreateStatusItem("Building", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Building.resolveStringCallback = func;
			this.Equipping = this.CreateStatusItem("Equipping", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Equipping.resolveStringCallback = func;
			this.WarmingUp = this.CreateStatusItem("WarmingUp", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.WarmingUp.resolveStringCallback = func;
			this.GeneratingPower = this.CreateStatusItem("GeneratingPower", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.GeneratingPower.resolveStringCallback = func;
			this.Harvesting = this.CreateStatusItem("Harvesting", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Ranching = this.CreateStatusItem("Ranching", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Harvesting.resolveStringCallback = func;
			this.Uprooting = this.CreateStatusItem("Uprooting", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Uprooting.resolveStringCallback = func;
			this.Emptying = this.CreateStatusItem("Emptying", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Emptying.resolveStringCallback = func;
			this.Toggling = this.CreateStatusItem("Toggling", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Toggling.resolveStringCallback = func;
			this.Deconstructing = this.CreateStatusItem("Deconstructing", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Deconstructing.resolveStringCallback = func;
			this.Disinfecting = this.CreateStatusItem("Disinfecting", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Disinfecting.resolveStringCallback = func;
			this.Upgrading = this.CreateStatusItem("Upgrading", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Upgrading.resolveStringCallback = func;
			this.Fabricating = this.CreateStatusItem("Fabricating", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Fabricating.resolveStringCallback = func2;
			this.Processing = this.CreateStatusItem("Processing", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Processing.resolveStringCallback = func2;
			this.Spicing = this.CreateStatusItem("Spicing", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Clearing = this.CreateStatusItem("Clearing", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Clearing.resolveStringCallback = func;
			this.GeneratingPower = this.CreateStatusItem("GeneratingPower", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.GeneratingPower.resolveStringCallback = func;
			this.CloggingToilet = this.CreateStatusItem("CLOGGINGTOILET", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Cold = this.CreateStatusItem("Cold", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 2);
			this.Cold.resolveTooltipCallback = delegate(string str, object data)
			{
				ExternalTemperatureMonitor.Instance smi = ((ColdImmunityMonitor.Instance)data).GetSMI<ExternalTemperatureMonitor.Instance>();
				str = str.Replace("{StressModification}", GameUtil.GetFormattedPercent(Db.Get().effects.Get("ColdAir").SelfModifiers[0].Value, GameUtil.TimeSlice.PerCycle));
				str = str.Replace("{StaminaModification}", GameUtil.GetFormattedPercent(Db.Get().effects.Get("ColdAir").SelfModifiers[1].Value, GameUtil.TimeSlice.PerCycle));
				str = str.Replace("{AthleticsModification}", Db.Get().effects.Get("ColdAir").SelfModifiers[2].Value.ToString());
				float num3 = smi.temperatureTransferer.average_kilowatts_exchanged.GetUnweightedAverage * 1000f;
				str = str.Replace("{currentTransferWattage}", GameUtil.GetFormattedHeatEnergyRate(num3, GameUtil.HeatEnergyFormatterUnit.Automatic));
				AttributeInstance attributeInstance = smi.attributes.Get("ThermalConductivityBarrier");
				string text2 = "<b>" + attributeInstance.GetFormattedValue() + "</b>";
				for (int num4 = 0; num4 != attributeInstance.Modifiers.Count; num4++)
				{
					AttributeModifier attributeModifier = attributeInstance.Modifiers[num4];
					text2 += "\n";
					text2 = string.Concat(new string[]
					{
						text2,
						"    • ",
						attributeModifier.GetDescription(),
						" <b>",
						attributeModifier.GetFormattedString(),
						"</b>"
					});
				}
				str = str.Replace("{conductivityBarrier}", text2);
				return str;
			};
			this.ExitingCold = this.CreateStatusItem("ExitingCold", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.ExitingCold.resolveTooltipCallback = delegate(string str, object data)
			{
				ColdImmunityMonitor.Instance instance = (ColdImmunityMonitor.Instance)data;
				str = str.Replace("{0}", GameUtil.GetFormattedTime(instance.ColdCountdown, "F0"));
				str = str.Replace("{StressModification}", GameUtil.GetFormattedPercent(Db.Get().effects.Get("ColdAir").SelfModifiers[0].Value, GameUtil.TimeSlice.PerCycle));
				str = str.Replace("{StaminaModification}", GameUtil.GetFormattedPercent(Db.Get().effects.Get("ColdAir").SelfModifiers[1].Value, GameUtil.TimeSlice.PerCycle));
				str = str.Replace("{AthleticsModification}", Db.Get().effects.Get("ColdAir").SelfModifiers[2].Value.ToString());
				return str;
			};
			this.Hot = this.CreateStatusItem("Hot", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 2);
			this.Hot.resolveTooltipCallback = delegate(string str, object data)
			{
				ExternalTemperatureMonitor.Instance smi2 = ((HeatImmunityMonitor.Instance)data).GetSMI<ExternalTemperatureMonitor.Instance>();
				str = str.Replace("{StressModification}", GameUtil.GetFormattedPercent(Db.Get().effects.Get("WarmAir").SelfModifiers[0].Value, GameUtil.TimeSlice.PerCycle));
				str = str.Replace("{StaminaModification}", GameUtil.GetFormattedPercent(Db.Get().effects.Get("WarmAir").SelfModifiers[1].Value, GameUtil.TimeSlice.PerCycle));
				str = str.Replace("{AthleticsModification}", Db.Get().effects.Get("WarmAir").SelfModifiers[2].Value.ToString());
				float num5 = smi2.temperatureTransferer.average_kilowatts_exchanged.GetUnweightedAverage * 1000f;
				str = str.Replace("{currentTransferWattage}", GameUtil.GetFormattedHeatEnergyRate(num5, GameUtil.HeatEnergyFormatterUnit.Automatic));
				AttributeInstance attributeInstance2 = smi2.attributes.Get("ThermalConductivityBarrier");
				string text3 = "<b>" + attributeInstance2.GetFormattedValue() + "</b>";
				for (int num6 = 0; num6 != attributeInstance2.Modifiers.Count; num6++)
				{
					AttributeModifier attributeModifier2 = attributeInstance2.Modifiers[num6];
					text3 += "\n";
					text3 = string.Concat(new string[]
					{
						text3,
						"    • ",
						attributeModifier2.GetDescription(),
						" <b>",
						attributeModifier2.GetFormattedString(),
						"</b>"
					});
				}
				str = str.Replace("{conductivityBarrier}", text3);
				return str;
			};
			this.ExitingHot = this.CreateStatusItem("ExitingHot", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.ExitingHot.resolveTooltipCallback = delegate(string str, object data)
			{
				HeatImmunityMonitor.Instance instance2 = (HeatImmunityMonitor.Instance)data;
				str = str.Replace("{0}", GameUtil.GetFormattedTime(instance2.HeatCountdown, "F0"));
				str = str.Replace("{StressModification}", GameUtil.GetFormattedPercent(Db.Get().effects.Get("WarmAir").SelfModifiers[0].Value, GameUtil.TimeSlice.PerCycle));
				str = str.Replace("{StaminaModification}", GameUtil.GetFormattedPercent(Db.Get().effects.Get("WarmAir").SelfModifiers[1].Value, GameUtil.TimeSlice.PerCycle));
				str = str.Replace("{AthleticsModification}", Db.Get().effects.Get("WarmAir").SelfModifiers[2].Value.ToString());
				return str;
			};
			this.BodyRegulatingHeating = this.CreateStatusItem("BodyRegulatingHeating", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.BodyRegulatingHeating.resolveStringCallback = delegate(string str, object data)
			{
				WarmBlooded.StatesInstance statesInstance = (WarmBlooded.StatesInstance)data;
				return str.Replace("{TempDelta}", GameUtil.GetFormattedTemperature(statesInstance.TemperatureDelta, GameUtil.TimeSlice.PerSecond, GameUtil.TemperatureInterpretation.Relative, true, false));
			};
			this.BodyRegulatingCooling = this.CreateStatusItem("BodyRegulatingCooling", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.BodyRegulatingCooling.resolveStringCallback = this.BodyRegulatingHeating.resolveStringCallback;
			this.EntombedChore = this.CreateStatusItem("EntombedChore", "DUPLICANTS", "status_item_entombed", StatusItem.IconType.Custom, NotificationType.DuplicantThreatening, false, OverlayModes.None.ID, true, 2);
			this.EntombedChore.AddNotification(null, null, null);
			this.EarlyMorning = this.CreateStatusItem("EarlyMorning", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.NightTime = this.CreateStatusItem("NightTime", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.PoorDecor = this.CreateStatusItem("PoorDecor", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.PoorQualityOfLife = this.CreateStatusItem("PoorQualityOfLife", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.PoorFoodQuality = this.CreateStatusItem("PoorFoodQuality", DUPLICANTS.STATUSITEMS.POOR_FOOD_QUALITY.NAME, DUPLICANTS.STATUSITEMS.POOR_FOOD_QUALITY.TOOLTIP, "", StatusItem.IconType.Exclamation, NotificationType.Neutral, false, OverlayModes.None.ID, 2);
			this.GoodFoodQuality = this.CreateStatusItem("GoodFoodQuality", DUPLICANTS.STATUSITEMS.GOOD_FOOD_QUALITY.NAME, DUPLICANTS.STATUSITEMS.GOOD_FOOD_QUALITY.TOOLTIP, "", StatusItem.IconType.Exclamation, NotificationType.Neutral, false, OverlayModes.None.ID, 2);
			this.Arting = this.CreateStatusItem("Arting", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Arting.resolveStringCallback = func;
			this.SevereWounds = this.CreateStatusItem("SevereWounds", "DUPLICANTS", "status_item_broken", StatusItem.IconType.Custom, NotificationType.Bad, false, OverlayModes.None.ID, true, 2);
			this.SevereWounds.AddNotification(null, null, null);
			this.BionicOfflineIncapacitated = this.CreateStatusItem("BionicOfflineIncapacitated", "DUPLICANTS", "status_electrobank", StatusItem.IconType.Custom, NotificationType.DuplicantThreatening, false, OverlayModes.None.ID, true, 2);
			this.BionicOfflineIncapacitated.AddNotification(null, null, null);
			this.BionicMicrochipGeneration = this.CreateStatusItem("BionicMicrochipGeneration", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.BionicMicrochipGeneration.resolveStringCallback = delegate(string str, object data)
			{
				float num7 = ((BionicMicrochipMonitor.Instance)data).Progress * 100f;
				str = string.Format(str, GameUtil.GetFormattedPercent(num7, GameUtil.TimeSlice.None));
				return str;
			};
			this.BionicMicrochipGeneration.resolveTooltipCallback = delegate(string str, object data)
			{
				BionicMicrochipMonitor.Instance instance3 = (BionicMicrochipMonitor.Instance)data;
				float num8 = 150f;
				str = string.Format(str, GameUtil.GetFormattedTime(num8, "F0"));
				return str;
			};
			this.BionicWantsOilChange = this.CreateStatusItem("BionicWantsOilChange", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 2);
			this.BionicWaitingForReboot = this.CreateStatusItem("BionicWaitingForReboot", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.BionicBeingRebooted = this.CreateStatusItem("BionicBeingRebooted", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.BionicRequiresSkillPerk = this.CreateStatusItem("BionicRequiresSkillPerk", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.BionicRequiresSkillPerk.resolveStringCallback = delegate(string str, object data)
			{
				str = str.Replace("{Skills}", GameUtil.NamesOfSkillsWithSkillPerk((string)data));
				str = str.Replace("{Boosters}", GameUtil.NamesOfBoostersWithSkillPerk((string)data));
				return str;
			};
			this.Incapacitated = this.CreateStatusItem("Incapacitated", "DUPLICANTS", "status_item_broken", StatusItem.IconType.Custom, NotificationType.DuplicantThreatening, false, OverlayModes.None.ID, true, 2);
			this.Incapacitated.AddNotification(null, null, null);
			this.Incapacitated.resolveStringCallback = delegate(string str, object data)
			{
				IncapacitationMonitor.Instance instance4 = (IncapacitationMonitor.Instance)data;
				float bleedLifeTime = instance4.GetBleedLifeTime(instance4);
				str = str.Replace("{CauseOfIncapacitation}", instance4.GetCauseOfIncapacitation().Name);
				return str.Replace("{TimeUntilDeath}", GameUtil.GetFormattedTime(bleedLifeTime, "F0"));
			};
			this.Relocating = this.CreateStatusItem("Relocating", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Relocating.resolveStringCallback = func;
			this.Fighting = this.CreateStatusItem("Fighting", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.Bad, false, OverlayModes.None.ID, true, 2);
			this.Fighting.AddNotification(null, null, null);
			this.Fleeing = this.CreateStatusItem("Fleeing", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.Bad, false, OverlayModes.None.ID, true, 2);
			this.Fleeing.AddNotification(null, null, null);
			this.Stressed = this.CreateStatusItem("Stressed", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Stressed.AddNotification(null, null, null);
			this.LashingOut = this.CreateStatusItem("LashingOut", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.Bad, false, OverlayModes.None.ID, true, 2);
			this.LashingOut.AddNotification(null, null, null);
			this.LowImmunity = this.CreateStatusItem("LowImmunity", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 2);
			this.LowImmunity.AddNotification(null, null, null);
			this.Studying = this.CreateStatusItem("Studying", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.InstallingElectrobank = this.CreateStatusItem("InstallingElectrobank", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Socializing = this.CreateStatusItem("Socializing", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, true, 2);
			this.Mingling = this.CreateStatusItem("Mingling", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, true, 2);
			this.BionicExplorerBooster = this.CreateStatusItem("BionicExplorerBooster", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, true, OverlayModes.None.ID, true, 2);
			this.BionicExplorerBooster.resolveStringCallback = delegate(string str, object data)
			{
				BionicUpgrade_ExplorerBoosterMonitor.Instance instance5 = (BionicUpgrade_ExplorerBoosterMonitor.Instance)data;
				str = string.Format(str, GameUtil.GetFormattedPercent(instance5.CurrentProgress * 100f, GameUtil.TimeSlice.None));
				return str;
			};
			this.ContactWithGerms = this.CreateStatusItem("ContactWithGerms", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, true, OverlayModes.Disease.ID, true, 2);
			this.ContactWithGerms.resolveStringCallback = delegate(string str, object data)
			{
				GermExposureMonitor.ExposureStatusData exposureStatusData = (GermExposureMonitor.ExposureStatusData)data;
				string name = Db.Get().Sicknesses.Get(exposureStatusData.exposure_type.sickness_id).Name;
				str = str.Replace("{Sickness}", name);
				return str;
			};
			this.ContactWithGerms.statusItemClickCallback = delegate(object data)
			{
				GermExposureMonitor.ExposureStatusData exposureStatusData2 = (GermExposureMonitor.ExposureStatusData)data;
				GameUtil.FocusCamera(exposureStatusData2.owner.GetLastExposurePosition(exposureStatusData2.exposure_type.germ_id), 2f, true, true);
				if (OverlayScreen.Instance.mode == OverlayModes.None.ID)
				{
					OverlayScreen.Instance.ToggleOverlay(OverlayModes.Disease.ID, true);
				}
			};
			this.ExposedToGerms = this.CreateStatusItem("ExposedToGerms", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, true, OverlayModes.Disease.ID, true, 2);
			this.ExposedToGerms.resolveStringCallback = delegate(string str, object data)
			{
				GermExposureMonitor.ExposureStatusData exposureStatusData3 = (GermExposureMonitor.ExposureStatusData)data;
				string name2 = Db.Get().Sicknesses.Get(exposureStatusData3.exposure_type.sickness_id).Name;
				AttributeInstance attributeInstance3 = Db.Get().Attributes.GermResistance.Lookup(exposureStatusData3.owner.gameObject);
				string lastDiseaseSource = exposureStatusData3.owner.GetLastDiseaseSource(exposureStatusData3.exposure_type.germ_id);
				GermExposureMonitor.Instance smi3 = exposureStatusData3.owner.GetSMI<GermExposureMonitor.Instance>();
				float num9 = (float)exposureStatusData3.exposure_type.base_resistance + GERM_EXPOSURE.EXPOSURE_TIER_RESISTANCE_BONUSES[0];
				float totalValue = attributeInstance3.GetTotalValue();
				float resistanceToExposureType = smi3.GetResistanceToExposureType(exposureStatusData3.exposure_type, -1f);
				float contractionChance = GermExposureMonitor.GetContractionChance(resistanceToExposureType);
				float exposureTier = smi3.GetExposureTier(exposureStatusData3.exposure_type.germ_id);
				float num10 = GERM_EXPOSURE.EXPOSURE_TIER_RESISTANCE_BONUSES[(int)exposureTier - 1] - GERM_EXPOSURE.EXPOSURE_TIER_RESISTANCE_BONUSES[0];
				str = str.Replace("{Severity}", DUPLICANTS.STATUSITEMS.EXPOSEDTOGERMS.EXPOSURE_TIERS[(int)exposureTier - 1].ToString());
				str = str.Replace("{Sickness}", name2);
				str = str.Replace("{Source}", lastDiseaseSource);
				str = str.Replace("{Base}", GameUtil.GetFormattedSimple(num9, GameUtil.TimeSlice.None, null));
				str = str.Replace("{Dupe}", GameUtil.GetFormattedSimple(totalValue, GameUtil.TimeSlice.None, null));
				str = str.Replace("{Total}", GameUtil.GetFormattedSimple(resistanceToExposureType, GameUtil.TimeSlice.None, null));
				str = str.Replace("{ExposureLevelBonus}", GameUtil.GetFormattedSimple(num10, GameUtil.TimeSlice.None, null));
				str = str.Replace("{Chance}", GameUtil.GetFormattedPercent(contractionChance * 100f, GameUtil.TimeSlice.None));
				return str;
			};
			this.ExposedToGerms.statusItemClickCallback = delegate(object data)
			{
				GermExposureMonitor.ExposureStatusData exposureStatusData4 = (GermExposureMonitor.ExposureStatusData)data;
				GameUtil.FocusCamera(exposureStatusData4.owner.GetLastExposurePosition(exposureStatusData4.exposure_type.germ_id), 2f, true, true);
				if (OverlayScreen.Instance.mode == OverlayModes.None.ID)
				{
					OverlayScreen.Instance.ToggleOverlay(OverlayModes.Disease.ID, true);
				}
			};
			this.LightWorkEfficiencyBonus = this.CreateStatusItem("LightWorkEfficiencyBonus", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, true, 2);
			this.LightWorkEfficiencyBonus.resolveStringCallback = delegate(string str, object data)
			{
				string text4 = string.Format(DUPLICANTS.STATUSITEMS.LIGHTWORKEFFICIENCYBONUS.NO_BUILDING_WORK_ATTRIBUTE, GameUtil.AddPositiveSign(GameUtil.GetFormattedPercent(DUPLICANTSTATS.STANDARD.Light.LIGHT_WORK_EFFICIENCY_BONUS * 100f, GameUtil.TimeSlice.None), true));
				return string.Format(str, text4);
			};
			this.LaboratoryWorkEfficiencyBonus = this.CreateStatusItem("LaboratoryWorkEfficiencyBonus", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, true, 2);
			this.LaboratoryWorkEfficiencyBonus.resolveStringCallback = delegate(string str, object data)
			{
				string text5 = string.Format(DUPLICANTS.STATUSITEMS.LABORATORYWORKEFFICIENCYBONUS.NO_BUILDING_WORK_ATTRIBUTE, GameUtil.AddPositiveSign(GameUtil.GetFormattedPercent(10f, GameUtil.TimeSlice.None), true));
				return string.Format(str, text5);
			};
			this.BeingProductive = this.CreateStatusItem("BeingProductive", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.BalloonArtistPlanning = this.CreateStatusItem("BalloonArtistPlanning", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.BalloonArtistHandingOut = this.CreateStatusItem("BalloonArtistHandingOut", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.Partying = this.CreateStatusItem("Partying", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, true, 2);
			this.DataRainerPlanning = this.CreateStatusItem("DataRainerPlanning", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.DataRainerRaining = this.CreateStatusItem("DataRainerRaining", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.RoboDancerPlanning = this.CreateStatusItem("RoboDancerPlanning", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.RoboDancerDancing = this.CreateStatusItem("RoboDancerDancing", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.WatchRoboDancerWorkable = this.CreateStatusItem("WatchRoboDancerWorkable", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.GasLiquidIrritation = this.CreateStatusItem("GasLiquidIrritated", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 2);
			this.GasLiquidIrritation.resolveStringCallback = (string str, object data) => ((GasLiquidExposureMonitor.Instance)data).IsMajorIrritation() ? DUPLICANTS.STATUSITEMS.GASLIQUIDEXPOSURE.NAME_MAJOR : DUPLICANTS.STATUSITEMS.GASLIQUIDEXPOSURE.NAME_MINOR;
			this.GasLiquidIrritation.resolveTooltipCallback = delegate(string str, object data)
			{
				GasLiquidExposureMonitor.Instance instance6 = (GasLiquidExposureMonitor.Instance)data;
				string text6 = DUPLICANTS.STATUSITEMS.GASLIQUIDEXPOSURE.TOOLTIP;
				string text7 = "";
				Effect appliedEffect = instance6.sm.GetAppliedEffect(instance6);
				if (appliedEffect != null)
				{
					text7 = Effect.CreateTooltip(appliedEffect, false, "\n    • ", true);
				}
				string text8 = DUPLICANTS.STATUSITEMS.GASLIQUIDEXPOSURE.TOOLTIP_EXPOSED.Replace("{element}", instance6.CurrentlyExposedToElement().name);
				float currentExposure = instance6.sm.GetCurrentExposure(instance6);
				if (currentExposure < 0f)
				{
					text8 = text8.Replace("{rate}", DUPLICANTS.STATUSITEMS.GASLIQUIDEXPOSURE.TOOLTIP_RATE_DECREASE);
				}
				else if (currentExposure > 0f)
				{
					text8 = text8.Replace("{rate}", DUPLICANTS.STATUSITEMS.GASLIQUIDEXPOSURE.TOOLTIP_RATE_INCREASE);
				}
				else
				{
					text8 = text8.Replace("{rate}", DUPLICANTS.STATUSITEMS.GASLIQUIDEXPOSURE.TOOLTIP_RATE_STAYS);
				}
				float num11 = (instance6.exposure - instance6.minorIrritationThreshold) / Math.Abs(instance6.exposureRate);
				string text9 = DUPLICANTS.STATUSITEMS.GASLIQUIDEXPOSURE.TOOLTIP_EXPOSURE_LEVEL.Replace("{time}", GameUtil.GetFormattedTime(num11, "F0"));
				return string.Concat(new string[] { text6, "\n\n", text7, "\n\n", text8, "\n\n", text9 });
			};
			this.ExpellingRads = this.CreateStatusItem("ExpellingRads", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.AnalyzingGenes = this.CreateStatusItem("AnalyzingGenes", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, true, 2);
			this.AnalyzingArtifact = this.CreateStatusItem("AnalyzingArtifact", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, true, 2);
			this.MegaBrainTank_Pajamas_Wearing = this.CreateStatusItem("MegaBrainTank_Pajamas_Wearing", DUPLICANTS.STATUSITEMS.WEARING_PAJAMAS.NAME, DUPLICANTS.STATUSITEMS.WEARING_PAJAMAS.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, 2);
			this.MegaBrainTank_Pajamas_Wearing.resolveTooltipCallback_shouldStillCallIfDataIsNull = true;
			this.MegaBrainTank_Pajamas_Wearing.resolveTooltipCallback = delegate(string str, object data)
			{
				string text10 = DUPLICANTS.STATUSITEMS.WEARING_PAJAMAS.TOOLTIP;
				Effect effect = Db.Get().effects.Get("SleepClinic");
				string text11;
				if (effect != null)
				{
					text11 = Effect.CreateTooltip(effect, false, "\n    • ", true);
				}
				else
				{
					text11 = "";
				}
				return text10 + "\n\n" + text11;
			};
			this.MegaBrainTank_Pajamas_Sleeping = this.CreateStatusItem("MegaBrainTank_Pajamas_Sleeping", DUPLICANTS.STATUSITEMS.DREAMING.NAME, DUPLICANTS.STATUSITEMS.DREAMING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, 2);
			this.MegaBrainTank_Pajamas_Sleeping.resolveTooltipCallback = delegate(string str, object data)
			{
				ClinicDreamable clinicDreamable = (ClinicDreamable)data;
				return str.Replace("{time}", GameUtil.GetFormattedTime(clinicDreamable.WorkTimeRemaining, "F0"));
			};
			this.FossilHunt_WorkerExcavating = this.CreateStatusItem("FossilHunt_WorkerExcavating", DUPLICANTS.STATUSITEMS.FOSSILHUNT.WORKEREXCAVATING.NAME, DUPLICANTS.STATUSITEMS.FOSSILHUNT.WORKEREXCAVATING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, 2);
			this.MorbRoverMakerWorkingOnRevealing = this.CreateStatusItem("MorbRoverMakerWorkingOnRevealing", CODEX.STORY_TRAITS.MORB_ROVER_MAKER.STATUSITEMS.BUILDING_REVEALING.NAME, CODEX.STORY_TRAITS.MORB_ROVER_MAKER.STATUSITEMS.BUILDING_REVEALING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, 2);
			this.MorbRoverMakerDoctorWorking = this.CreateStatusItem("MorbRoverMakerDoctorWorking", CODEX.STORY_TRAITS.MORB_ROVER_MAKER.STATUSITEMS.DOCTOR_WORKING_BUILDING.NAME, CODEX.STORY_TRAITS.MORB_ROVER_MAKER.STATUSITEMS.DOCTOR_WORKING_BUILDING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, 2);
			this.ArmingTrap = this.CreateStatusItem("ArmingTrap", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.WaxedForTransitTube = this.CreateStatusItem("WaxedForTransitTube", "DUPLICANTS", "action_speed_up", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.WaxedForTransitTube.resolveTooltipCallback = delegate(string str, object data)
			{
				float num12 = (float)data * 100f;
				return str.Replace("{0}", GameUtil.GetFormattedPercent(num12, GameUtil.TimeSlice.None));
			};
			this.JoyResponse_HasBalloon = this.CreateStatusItem("JoyResponse_HasBalloon", DUPLICANTS.MODIFIERS.HASBALLOON.NAME, DUPLICANTS.MODIFIERS.HASBALLOON.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, 2);
			this.JoyResponse_HasBalloon.resolveTooltipCallback = delegate(string str, object data)
			{
				EquippableBalloon.StatesInstance statesInstance2 = (EquippableBalloon.StatesInstance)data;
				return str + "\n\n" + DUPLICANTS.MODIFIERS.TIME_REMAINING.Replace("{0}", GameUtil.GetFormattedCycles(statesInstance2.transitionTime - GameClock.Instance.GetTime(), "F1", false));
			};
			this.JoyResponse_HeardJoySinger = this.CreateStatusItem("JoyResponse_HeardJoySinger", DUPLICANTS.MODIFIERS.HEARDJOYSINGER.NAME, DUPLICANTS.MODIFIERS.HEARDJOYSINGER.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, 2);
			this.JoyResponse_HeardJoySinger.resolveTooltipCallback = delegate(string str, object data)
			{
				InspirationEffectMonitor.Instance instance7 = (InspirationEffectMonitor.Instance)data;
				return str + "\n\n" + DUPLICANTS.MODIFIERS.TIME_REMAINING.Replace("{0}", GameUtil.GetFormattedCycles(instance7.sm.inspirationTimeRemaining.Get(instance7), "F1", false));
			};
			this.JoyResponse_StickerBombing = this.CreateStatusItem("JoyResponse_StickerBombing", DUPLICANTS.MODIFIERS.ISSTICKERBOMBING.NAME, DUPLICANTS.MODIFIERS.ISSTICKERBOMBING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, 2);
			this.Meteorphile = this.CreateStatusItem("Meteorphile", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 2);
			this.EnteringDock = this.CreateStatusItem("EnteringDock", DUPLICANTS.STATUSITEMS.REMOTEWORKER.ENTERINGDOCK.NAME, DUPLICANTS.STATUSITEMS.REMOTEWORKER.ENTERINGDOCK.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, 2);
			this.UnreachableDock = this.CreateStatusItem("UnreachableDock", DUPLICANTS.STATUSITEMS.REMOTEWORKER.UNREACHABLEDOCK.NAME, DUPLICANTS.STATUSITEMS.REMOTEWORKER.UNREACHABLEDOCK.TOOLTIP, "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, 2);
			this.NoHomeDock = this.CreateStatusItem("UnreachableDock", DUPLICANTS.STATUSITEMS.REMOTEWORKER.NOHOMEDOCK.NAME, DUPLICANTS.STATUSITEMS.REMOTEWORKER.NOHOMEDOCK.TOOLTIP, "", StatusItem.IconType.Exclamation, NotificationType.Bad, false, OverlayModes.None.ID, 2);
			this.RemoteWorkerCapacitorStatus = this.CreateStatusItem("RemoteWorkerCapacitorStatus", DUPLICANTS.STATUSITEMS.REMOTEWORKER.POWERSTATUS.NAME, DUPLICANTS.STATUSITEMS.REMOTEWORKER.POWERSTATUS.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, 2);
			this.RemoteWorkerCapacitorStatus.resolveStringCallback = delegate(string str, object obj)
			{
				RemoteWorkerCapacitor remoteWorkerCapacitor = obj as RemoteWorkerCapacitor;
				float num13 = 0f;
				float num14 = 0f;
				if (remoteWorkerCapacitor != null)
				{
					num13 = remoteWorkerCapacitor.Charge;
					num14 = remoteWorkerCapacitor.ChargeRatio * 100f;
				}
				return str.Replace("{CHARGE}", GameUtil.GetFormattedJoules(num13, "F1", GameUtil.TimeSlice.None)).Replace("{RATIO}", GameUtil.GetFormattedPercent(num14, GameUtil.TimeSlice.None));
			};
			this.RemoteWorkerLowPower = this.CreateStatusItem("RemoteWorkerLowPower", DUPLICANTS.STATUSITEMS.REMOTEWORKER.LOWPOWER.NAME, DUPLICANTS.STATUSITEMS.REMOTEWORKER.LOWPOWER.TOOLTIP, "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, 2);
			this.RemoteWorkerOutOfPower = this.CreateStatusItem("RemoteWorkerOutOfPower", DUPLICANTS.STATUSITEMS.REMOTEWORKER.OUTOFPOWER.NAME, DUPLICANTS.STATUSITEMS.REMOTEWORKER.OUTOFPOWER.TOOLTIP, "", StatusItem.IconType.Exclamation, NotificationType.Bad, false, OverlayModes.None.ID, 2);
			this.RemoteWorkerHighGunkLevel = this.CreateStatusItem("RemoteWorkerHighGunkLevel", DUPLICANTS.STATUSITEMS.REMOTEWORKER.HIGHGUNK.NAME, DUPLICANTS.STATUSITEMS.REMOTEWORKER.HIGHGUNK.TOOLTIP, "", StatusItem.IconType.Exclamation, NotificationType.Bad, false, OverlayModes.None.ID, 2);
			this.RemoteWorkerFullGunkLevel = this.CreateStatusItem("RemoteWorkerFullGunkLevel", DUPLICANTS.STATUSITEMS.REMOTEWORKER.FULLGUNK.NAME, DUPLICANTS.STATUSITEMS.REMOTEWORKER.FULLGUNK.TOOLTIP, "", StatusItem.IconType.Exclamation, NotificationType.Bad, false, OverlayModes.None.ID, 2);
			this.RemoteWorkerLowOil = this.CreateStatusItem("RemoteWorkerLowOil", DUPLICANTS.STATUSITEMS.REMOTEWORKER.LOWOIL.NAME, DUPLICANTS.STATUSITEMS.REMOTEWORKER.LOWOIL.TOOLTIP, "", StatusItem.IconType.Exclamation, NotificationType.Bad, false, OverlayModes.None.ID, 2);
			this.RemoteWorkerOutOfOil = this.CreateStatusItem("RemoteWorkerOutOfOil", DUPLICANTS.STATUSITEMS.REMOTEWORKER.OUTOFOIL.NAME, DUPLICANTS.STATUSITEMS.REMOTEWORKER.OUTOFOIL.TOOLTIP, "", StatusItem.IconType.Exclamation, NotificationType.Bad, false, OverlayModes.None.ID, 2);
			this.RemoteWorkerRecharging = this.CreateStatusItem("RemoteWorkerRecharging", DUPLICANTS.STATUSITEMS.REMOTEWORKER.RECHARGING.NAME, DUPLICANTS.STATUSITEMS.REMOTEWORKER.RECHARGING.TOOLTIP, "", StatusItem.IconType.Exclamation, NotificationType.Neutral, false, OverlayModes.None.ID, 2);
			this.RemoteWorkerOiling = this.CreateStatusItem("RemoteWorkerOiling", DUPLICANTS.STATUSITEMS.REMOTEWORKER.OILING.NAME, DUPLICANTS.STATUSITEMS.REMOTEWORKER.OILING.TOOLTIP, "", StatusItem.IconType.Exclamation, NotificationType.Neutral, false, OverlayModes.None.ID, 2);
			this.RemoteWorkerDraining = this.CreateStatusItem("RemoteWorkerDraining", DUPLICANTS.STATUSITEMS.REMOTEWORKER.DRAINING.NAME, DUPLICANTS.STATUSITEMS.REMOTEWORKER.DRAINING.TOOLTIP, "", StatusItem.IconType.Exclamation, NotificationType.Neutral, false, OverlayModes.None.ID, 2);
			this.BionicCriticalBattery = this.CreateStatusItem("BionicCriticalBattery", "DUPLICANTS", "", StatusItem.IconType.Exclamation, NotificationType.Bad, false, OverlayModes.None.ID, true, 2);
			this.BionicCriticalBattery.AddNotification(null, null, null);
		}

		// Token: 0x040056E9 RID: 22249
		public StatusItem Idle;

		// Token: 0x040056EA RID: 22250
		public StatusItem IdleInRockets;

		// Token: 0x040056EB RID: 22251
		public StatusItem Pacified;

		// Token: 0x040056EC RID: 22252
		public StatusItem PendingPacification;

		// Token: 0x040056ED RID: 22253
		public StatusItem Dead;

		// Token: 0x040056EE RID: 22254
		public StatusItem CloggingToilet;

		// Token: 0x040056EF RID: 22255
		public StatusItem MoveToSuitNotRequired;

		// Token: 0x040056F0 RID: 22256
		public StatusItem DroppingUnusedInventory;

		// Token: 0x040056F1 RID: 22257
		public StatusItem MovingToSafeArea;

		// Token: 0x040056F2 RID: 22258
		public StatusItem BedUnreachable;

		// Token: 0x040056F3 RID: 22259
		public StatusItem Hungry;

		// Token: 0x040056F4 RID: 22260
		public StatusItem Starving;

		// Token: 0x040056F5 RID: 22261
		public StatusItem Rotten;

		// Token: 0x040056F6 RID: 22262
		public StatusItem Quarantined;

		// Token: 0x040056F7 RID: 22263
		public StatusItem NoRationsAvailable;

		// Token: 0x040056F8 RID: 22264
		public StatusItem RationsUnreachable;

		// Token: 0x040056F9 RID: 22265
		public StatusItem RationsNotPermitted;

		// Token: 0x040056FA RID: 22266
		public StatusItem DailyRationLimitReached;

		// Token: 0x040056FB RID: 22267
		public StatusItem Scalding;

		// Token: 0x040056FC RID: 22268
		public StatusItem Hot;

		// Token: 0x040056FD RID: 22269
		public StatusItem Cold;

		// Token: 0x040056FE RID: 22270
		public StatusItem ExitingCold;

		// Token: 0x040056FF RID: 22271
		public StatusItem ExitingHot;

		// Token: 0x04005700 RID: 22272
		public StatusItem QuarantineAreaUnassigned;

		// Token: 0x04005701 RID: 22273
		public StatusItem QuarantineAreaUnreachable;

		// Token: 0x04005702 RID: 22274
		public StatusItem Tired;

		// Token: 0x04005703 RID: 22275
		public StatusItem NervousBreakdown;

		// Token: 0x04005704 RID: 22276
		public StatusItem Unhappy;

		// Token: 0x04005705 RID: 22277
		public StatusItem Suffocating;

		// Token: 0x04005706 RID: 22278
		public StatusItem HoldingBreath;

		// Token: 0x04005707 RID: 22279
		public StatusItem ToiletUnreachable;

		// Token: 0x04005708 RID: 22280
		public StatusItem NoUsableToilets;

		// Token: 0x04005709 RID: 22281
		public StatusItem NoToilets;

		// Token: 0x0400570A RID: 22282
		public StatusItem Vomiting;

		// Token: 0x0400570B RID: 22283
		public StatusItem Coughing;

		// Token: 0x0400570C RID: 22284
		public StatusItem Slippering;

		// Token: 0x0400570D RID: 22285
		public StatusItem BreathingO2;

		// Token: 0x0400570E RID: 22286
		public StatusItem BreathingO2Bionic;

		// Token: 0x0400570F RID: 22287
		public StatusItem EmittingCO2;

		// Token: 0x04005710 RID: 22288
		public StatusItem LowOxygen;

		// Token: 0x04005711 RID: 22289
		public StatusItem RedAlert;

		// Token: 0x04005712 RID: 22290
		public StatusItem Digging;

		// Token: 0x04005713 RID: 22291
		public StatusItem Eating;

		// Token: 0x04005714 RID: 22292
		public StatusItem Dreaming;

		// Token: 0x04005715 RID: 22293
		public StatusItem Sleeping;

		// Token: 0x04005716 RID: 22294
		public StatusItem SleepingExhausted;

		// Token: 0x04005717 RID: 22295
		public StatusItem SleepingInterruptedByLight;

		// Token: 0x04005718 RID: 22296
		public StatusItem SleepingInterruptedByNoise;

		// Token: 0x04005719 RID: 22297
		public StatusItem SleepingInterruptedByFearOfDark;

		// Token: 0x0400571A RID: 22298
		public StatusItem SleepingInterruptedByMovement;

		// Token: 0x0400571B RID: 22299
		public StatusItem SleepingInterruptedByCold;

		// Token: 0x0400571C RID: 22300
		public StatusItem SleepingPeacefully;

		// Token: 0x0400571D RID: 22301
		public StatusItem SleepingBadly;

		// Token: 0x0400571E RID: 22302
		public StatusItem SleepingTerribly;

		// Token: 0x0400571F RID: 22303
		public StatusItem Cleaning;

		// Token: 0x04005720 RID: 22304
		public StatusItem PickingUp;

		// Token: 0x04005721 RID: 22305
		public StatusItem Mopping;

		// Token: 0x04005722 RID: 22306
		public StatusItem Cooking;

		// Token: 0x04005723 RID: 22307
		public StatusItem Arting;

		// Token: 0x04005724 RID: 22308
		public StatusItem Mushing;

		// Token: 0x04005725 RID: 22309
		public StatusItem Researching;

		// Token: 0x04005726 RID: 22310
		public StatusItem ResearchingFromPOI;

		// Token: 0x04005727 RID: 22311
		public StatusItem MissionControlling;

		// Token: 0x04005728 RID: 22312
		public StatusItem Tinkering;

		// Token: 0x04005729 RID: 22313
		public StatusItem Storing;

		// Token: 0x0400572A RID: 22314
		public StatusItem Building;

		// Token: 0x0400572B RID: 22315
		public StatusItem Equipping;

		// Token: 0x0400572C RID: 22316
		public StatusItem WarmingUp;

		// Token: 0x0400572D RID: 22317
		public StatusItem GeneratingPower;

		// Token: 0x0400572E RID: 22318
		public StatusItem Ranching;

		// Token: 0x0400572F RID: 22319
		public StatusItem Harvesting;

		// Token: 0x04005730 RID: 22320
		public StatusItem Uprooting;

		// Token: 0x04005731 RID: 22321
		public StatusItem Emptying;

		// Token: 0x04005732 RID: 22322
		public StatusItem Toggling;

		// Token: 0x04005733 RID: 22323
		public StatusItem Deconstructing;

		// Token: 0x04005734 RID: 22324
		public StatusItem Disinfecting;

		// Token: 0x04005735 RID: 22325
		public StatusItem Relocating;

		// Token: 0x04005736 RID: 22326
		public StatusItem Upgrading;

		// Token: 0x04005737 RID: 22327
		public StatusItem Fabricating;

		// Token: 0x04005738 RID: 22328
		public StatusItem Processing;

		// Token: 0x04005739 RID: 22329
		public StatusItem Spicing;

		// Token: 0x0400573A RID: 22330
		public StatusItem Clearing;

		// Token: 0x0400573B RID: 22331
		public StatusItem BodyRegulatingHeating;

		// Token: 0x0400573C RID: 22332
		public StatusItem BodyRegulatingCooling;

		// Token: 0x0400573D RID: 22333
		public StatusItem EntombedChore;

		// Token: 0x0400573E RID: 22334
		public StatusItem EarlyMorning;

		// Token: 0x0400573F RID: 22335
		public StatusItem NightTime;

		// Token: 0x04005740 RID: 22336
		public StatusItem PoorDecor;

		// Token: 0x04005741 RID: 22337
		public StatusItem PoorQualityOfLife;

		// Token: 0x04005742 RID: 22338
		public StatusItem PoorFoodQuality;

		// Token: 0x04005743 RID: 22339
		public StatusItem GoodFoodQuality;

		// Token: 0x04005744 RID: 22340
		public StatusItem SevereWounds;

		// Token: 0x04005745 RID: 22341
		public StatusItem Incapacitated;

		// Token: 0x04005746 RID: 22342
		public StatusItem BionicOfflineIncapacitated;

		// Token: 0x04005747 RID: 22343
		public StatusItem BionicWaitingForReboot;

		// Token: 0x04005748 RID: 22344
		public StatusItem BionicBeingRebooted;

		// Token: 0x04005749 RID: 22345
		public StatusItem BionicRequiresSkillPerk;

		// Token: 0x0400574A RID: 22346
		public StatusItem BionicWantsOilChange;

		// Token: 0x0400574B RID: 22347
		public StatusItem BionicMicrochipGeneration;

		// Token: 0x0400574C RID: 22348
		public StatusItem InstallingElectrobank;

		// Token: 0x0400574D RID: 22349
		public StatusItem Fighting;

		// Token: 0x0400574E RID: 22350
		public StatusItem Fleeing;

		// Token: 0x0400574F RID: 22351
		public StatusItem Stressed;

		// Token: 0x04005750 RID: 22352
		public StatusItem LashingOut;

		// Token: 0x04005751 RID: 22353
		public StatusItem LowImmunity;

		// Token: 0x04005752 RID: 22354
		public StatusItem Studying;

		// Token: 0x04005753 RID: 22355
		public StatusItem Socializing;

		// Token: 0x04005754 RID: 22356
		public StatusItem Mingling;

		// Token: 0x04005755 RID: 22357
		public StatusItem ContactWithGerms;

		// Token: 0x04005756 RID: 22358
		public StatusItem ExposedToGerms;

		// Token: 0x04005757 RID: 22359
		public StatusItem LightWorkEfficiencyBonus;

		// Token: 0x04005758 RID: 22360
		public StatusItem LaboratoryWorkEfficiencyBonus;

		// Token: 0x04005759 RID: 22361
		public StatusItem BeingProductive;

		// Token: 0x0400575A RID: 22362
		public StatusItem BalloonArtistPlanning;

		// Token: 0x0400575B RID: 22363
		public StatusItem BalloonArtistHandingOut;

		// Token: 0x0400575C RID: 22364
		public StatusItem Partying;

		// Token: 0x0400575D RID: 22365
		public StatusItem GasLiquidIrritation;

		// Token: 0x0400575E RID: 22366
		public StatusItem ExpellingRads;

		// Token: 0x0400575F RID: 22367
		public StatusItem AnalyzingGenes;

		// Token: 0x04005760 RID: 22368
		public StatusItem AnalyzingArtifact;

		// Token: 0x04005761 RID: 22369
		public StatusItem MegaBrainTank_Pajamas_Wearing;

		// Token: 0x04005762 RID: 22370
		public StatusItem MegaBrainTank_Pajamas_Sleeping;

		// Token: 0x04005763 RID: 22371
		public StatusItem JoyResponse_HasBalloon;

		// Token: 0x04005764 RID: 22372
		public StatusItem JoyResponse_HeardJoySinger;

		// Token: 0x04005765 RID: 22373
		public StatusItem JoyResponse_StickerBombing;

		// Token: 0x04005766 RID: 22374
		public StatusItem Meteorphile;

		// Token: 0x04005767 RID: 22375
		public StatusItem FossilHunt_WorkerExcavating;

		// Token: 0x04005768 RID: 22376
		public StatusItem MorbRoverMakerDoctorWorking;

		// Token: 0x04005769 RID: 22377
		public StatusItem MorbRoverMakerWorkingOnRevealing;

		// Token: 0x0400576A RID: 22378
		public StatusItem ArmingTrap;

		// Token: 0x0400576B RID: 22379
		public StatusItem WaxedForTransitTube;

		// Token: 0x0400576C RID: 22380
		public StatusItem DataRainerPlanning;

		// Token: 0x0400576D RID: 22381
		public StatusItem DataRainerRaining;

		// Token: 0x0400576E RID: 22382
		public StatusItem RoboDancerPlanning;

		// Token: 0x0400576F RID: 22383
		public StatusItem RoboDancerDancing;

		// Token: 0x04005770 RID: 22384
		public StatusItem WatchRoboDancerWorkable;

		// Token: 0x04005771 RID: 22385
		public StatusItem BionicExplorerBooster;

		// Token: 0x04005772 RID: 22386
		public StatusItem EnteringDock;

		// Token: 0x04005773 RID: 22387
		public StatusItem UnreachableDock;

		// Token: 0x04005774 RID: 22388
		public StatusItem NoHomeDock;

		// Token: 0x04005775 RID: 22389
		public StatusItem RemoteWorkerCapacitorStatus;

		// Token: 0x04005776 RID: 22390
		public StatusItem RemoteWorkerLowPower;

		// Token: 0x04005777 RID: 22391
		public StatusItem RemoteWorkerOutOfPower;

		// Token: 0x04005778 RID: 22392
		public StatusItem RemoteWorkerHighGunkLevel;

		// Token: 0x04005779 RID: 22393
		public StatusItem RemoteWorkerFullGunkLevel;

		// Token: 0x0400577A RID: 22394
		public StatusItem RemoteWorkerLowOil;

		// Token: 0x0400577B RID: 22395
		public StatusItem RemoteWorkerOutOfOil;

		// Token: 0x0400577C RID: 22396
		public StatusItem RemoteWorkerRecharging;

		// Token: 0x0400577D RID: 22397
		public StatusItem RemoteWorkerOiling;

		// Token: 0x0400577E RID: 22398
		public StatusItem RemoteWorkerDraining;

		// Token: 0x0400577F RID: 22399
		public StatusItem BionicCriticalBattery;

		// Token: 0x04005780 RID: 22400
		private const int NONE_OVERLAY = 0;
	}
}
