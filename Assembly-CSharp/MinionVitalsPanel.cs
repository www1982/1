using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Klei.AI;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D6E RID: 3438
[AddComponentMenu("KMonoBehaviour/scripts/MinionVitalsPanel")]
public class MinionVitalsPanel : CollapsibleDetailContentPanel
{
	// Token: 0x06006A9F RID: 27295 RVA: 0x00283944 File Offset: 0x00281B44
	public void Init()
	{
		this.AddAmountLine(Db.Get().Amounts.HitPoints, null);
		this.AddAmountLine(Db.Get().Amounts.BionicInternalBattery, null);
		this.AddAmountLine(Db.Get().Amounts.BionicOil, null);
		this.AddAmountLine(Db.Get().Amounts.BionicGunk, null);
		this.AddAttributeLine(Db.Get().CritterAttributes.Happiness, null);
		this.AddAmountLine(Db.Get().Amounts.Wildness, null);
		this.AddAmountLine(Db.Get().Amounts.Incubation, null);
		this.AddAmountLine(Db.Get().Amounts.Viability, null);
		this.AddAmountLine(Db.Get().Amounts.PowerCharge, null);
		this.AddAmountLine(Db.Get().Amounts.Fertility, null);
		this.AddAmountLine(Db.Get().Amounts.Beckoning, null);
		this.AddAmountLine(Db.Get().Amounts.Age, null);
		this.AddAmountLine(Db.Get().Amounts.Stress, null);
		this.AddAttributeLine(Db.Get().Attributes.QualityOfLife, null);
		this.AddAmountLine(Db.Get().Amounts.Bladder, null);
		this.AddAmountLine(Db.Get().Amounts.Breath, null);
		this.AddAmountLine(Db.Get().Amounts.BionicOxygenTank, null);
		this.AddAmountLine(Db.Get().Amounts.Stamina, null);
		this.AddAttributeLine(Db.Get().CritterAttributes.Metabolism, null);
		this.AddAmountLine(Db.Get().Amounts.Calories, null);
		this.AddAmountLine(Db.Get().Amounts.ScaleGrowth, null);
		this.AddAmountLine(Db.Get().Amounts.MilkProduction, null);
		this.AddAmountLine(Db.Get().Amounts.ElementGrowth, null);
		this.AddAmountLine(Db.Get().Amounts.Temperature, null);
		this.AddAmountLine(Db.Get().Amounts.CritterTemperature, null);
		this.AddAmountLine(Db.Get().Amounts.Decor, null);
		this.AddAmountLine(Db.Get().Amounts.InternalBattery, null);
		this.AddAmountLine(Db.Get().Amounts.InternalChemicalBattery, null);
		this.AddAmountLine(Db.Get().Amounts.InternalBioBattery, null);
		this.AddAmountLine(Db.Get().Amounts.InternalElectroBank, null);
		if (DlcManager.FeatureRadiationEnabled())
		{
			this.AddAmountLine(Db.Get().Amounts.RadiationBalance, null);
		}
		this.AddCheckboxLine(Db.Get().Amounts.AirPressure, this.conditionsContainerNormal, (GameObject go) => this.GetAirPressureLabel(go), delegate(GameObject go)
		{
			if (go.GetComponent<PressureVulnerable>() != null && go.GetComponent<PressureVulnerable>().pressure_sensitive)
			{
				return MinionVitalsPanel.CheckboxLineDisplayType.Normal;
			}
			return MinionVitalsPanel.CheckboxLineDisplayType.Hidden;
		}, (GameObject go) => this.check_pressure(go), (GameObject go) => this.GetAirPressureTooltip(go));
		this.AddCheckboxLine(null, this.conditionsContainerNormal, (GameObject go) => this.GetAtmosphereLabel(go), delegate(GameObject go)
		{
			if (go.GetComponent<PressureVulnerable>() != null && go.GetComponent<PressureVulnerable>().safe_atmospheres.Count > 0)
			{
				return MinionVitalsPanel.CheckboxLineDisplayType.Normal;
			}
			return MinionVitalsPanel.CheckboxLineDisplayType.Hidden;
		}, (GameObject go) => this.check_atmosphere(go), (GameObject go) => this.GetAtmosphereTooltip(go));
		this.AddCheckboxLine(Db.Get().Amounts.Temperature, this.conditionsContainerNormal, (GameObject go) => this.GetInternalTemperatureLabel(go), delegate(GameObject go)
		{
			if (go.GetComponent<TemperatureVulnerable>() != null)
			{
				return MinionVitalsPanel.CheckboxLineDisplayType.Normal;
			}
			return MinionVitalsPanel.CheckboxLineDisplayType.Hidden;
		}, (GameObject go) => this.check_temperature(go), (GameObject go) => this.GetInternalTemperatureTooltip(go));
		this.AddCheckboxLine(Db.Get().Amounts.Fertilization, this.conditionsContainerAdditional, (GameObject go) => this.GetFertilizationLabel(go), delegate(GameObject go)
		{
			if (go.GetComponent<ReceptacleMonitor>() == null)
			{
				return MinionVitalsPanel.CheckboxLineDisplayType.Hidden;
			}
			if (go.GetComponent<ReceptacleMonitor>().Replanted)
			{
				return MinionVitalsPanel.CheckboxLineDisplayType.Normal;
			}
			return MinionVitalsPanel.CheckboxLineDisplayType.Diminished;
		}, (GameObject go) => this.check_fertilizer(go), (GameObject go) => this.GetFertilizationTooltip(go));
		this.AddCheckboxLine(Db.Get().Amounts.Irrigation, this.conditionsContainerAdditional, (GameObject go) => this.GetIrrigationLabel(go), delegate(GameObject go)
		{
			ReceptacleMonitor component = go.GetComponent<ReceptacleMonitor>();
			if (!(component != null) || !component.Replanted)
			{
				return MinionVitalsPanel.CheckboxLineDisplayType.Diminished;
			}
			return MinionVitalsPanel.CheckboxLineDisplayType.Normal;
		}, (GameObject go) => this.check_irrigation(go), (GameObject go) => this.GetIrrigationTooltip(go));
		this.AddCheckboxLine(Db.Get().Amounts.Illumination, this.conditionsContainerNormal, (GameObject go) => this.GetIlluminationLabel(go), (GameObject go) => MinionVitalsPanel.CheckboxLineDisplayType.Normal, (GameObject go) => this.check_illumination(go), (GameObject go) => this.GetIlluminationTooltip(go));
		this.AddCheckboxLine(null, this.conditionsContainerNormal, (GameObject go) => this.GetRadiationLabel(go), delegate(GameObject go)
		{
			AttributeInstance attributeInstance = go.GetAttributes().Get(Db.Get().PlantAttributes.MaxRadiationThreshold);
			if (attributeInstance != null && attributeInstance.GetTotalValue() > 0f)
			{
				return MinionVitalsPanel.CheckboxLineDisplayType.Normal;
			}
			return MinionVitalsPanel.CheckboxLineDisplayType.Hidden;
		}, (GameObject go) => this.check_radiation(go), (GameObject go) => this.GetRadiationTooltip(go));
		this.AddCheckboxLine(null, this.conditionsContainerNormal, (GameObject go) => this.GetEntityConsumptionLabel(go), delegate(GameObject go)
		{
			if (go.GetComponent<IPlantConsumeEntities>() != null)
			{
				return MinionVitalsPanel.CheckboxLineDisplayType.Normal;
			}
			return MinionVitalsPanel.CheckboxLineDisplayType.Hidden;
		}, (GameObject go) => this.check_entity_consumed(go), (GameObject go) => this.GetEntityConsumedTooltip(go));
		this.AddCheckboxLine(null, this.conditionsContainerNormal, (GameObject go) => this.GetPollinationLabel(go), delegate(GameObject go)
		{
			if (go.GetSMI<PollinationMonitor.StatesInstance>() == null)
			{
				return MinionVitalsPanel.CheckboxLineDisplayType.Hidden;
			}
			return MinionVitalsPanel.CheckboxLineDisplayType.Normal;
		}, (GameObject go) => go.GetComponent<WiltCondition>().IsConditionSatisifed(WiltCondition.Condition.Pollination), (GameObject go) => this.GetPollinationTooltip(go));
	}

	// Token: 0x17000789 RID: 1929
	// (get) Token: 0x06006AA0 RID: 27296 RVA: 0x00283F2C File Offset: 0x0028212C
	public string UnpollinatedTooltip
	{
		get
		{
			if (string.IsNullOrEmpty(this.unpollinatedTooltip))
			{
				StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
				foreach (GameObject gameObject in Assets.GetPrefabsWithTag(GameTags.Creatures.Pollinator))
				{
					KPrefabID component = gameObject.GetComponent<KPrefabID>();
					if (!(component == null) && Game.IsCorrectDlcActiveForCurrentSave(component))
					{
						stringBuilder.AppendFormat("\n{0}{1}", "    • ", gameObject.GetProperName());
					}
				}
				this.unpollinatedTooltip = string.Format(UI.TOOLTIPS.VITALS_CHECKBOX_UNPOLLINATED, GlobalStringBuilderPool.ReturnAndFree(stringBuilder));
			}
			return this.unpollinatedTooltip;
		}
	}

	// Token: 0x06006AA1 RID: 27297 RVA: 0x00283FE4 File Offset: 0x002821E4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.Init();
	}

	// Token: 0x06006AA2 RID: 27298 RVA: 0x00283FF2 File Offset: 0x002821F2
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		SimAndRenderScheduler.instance.Add(this, false);
	}

	// Token: 0x06006AA3 RID: 27299 RVA: 0x00284006 File Offset: 0x00282206
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		SimAndRenderScheduler.instance.Remove(this);
	}

	// Token: 0x06006AA4 RID: 27300 RVA: 0x0028401C File Offset: 0x0028221C
	private void AddAmountLine(Amount amount, Func<AmountInstance, string> tooltip_func = null)
	{
		GameObject gameObject = Util.KInstantiateUI(this.LineItemPrefab, this.Content.gameObject, false);
		gameObject.GetComponentInChildren<Image>().sprite = Assets.GetSprite(amount.uiSprite);
		gameObject.GetComponent<ToolTip>().refreshWhileHovering = true;
		gameObject.SetActive(true);
		MinionVitalsPanel.AmountLine amountLine = default(MinionVitalsPanel.AmountLine);
		amountLine.amount = amount;
		amountLine.go = gameObject;
		amountLine.locText = gameObject.GetComponentInChildren<LocText>();
		amountLine.toolTip = gameObject.GetComponentInChildren<ToolTip>();
		amountLine.imageToggle = gameObject.GetComponentInChildren<ValueTrendImageToggle>();
		amountLine.toolTipFunc = ((tooltip_func != null) ? tooltip_func : new Func<AmountInstance, string>(amount.GetTooltip));
		this.amountsLines.Add(amountLine);
	}

	// Token: 0x06006AA5 RID: 27301 RVA: 0x002840D4 File Offset: 0x002822D4
	private void AddAttributeLine(Klei.AI.Attribute attribute, Func<AttributeInstance, string> tooltip_func = null)
	{
		GameObject gameObject = Util.KInstantiateUI(this.LineItemPrefab, this.Content.gameObject, false);
		gameObject.GetComponentInChildren<Image>().sprite = Assets.GetSprite(attribute.uiSprite);
		gameObject.GetComponent<ToolTip>().refreshWhileHovering = true;
		gameObject.SetActive(true);
		MinionVitalsPanel.AttributeLine attributeLine = default(MinionVitalsPanel.AttributeLine);
		attributeLine.attribute = attribute;
		attributeLine.go = gameObject;
		attributeLine.locText = gameObject.GetComponentInChildren<LocText>();
		attributeLine.toolTip = gameObject.GetComponentInChildren<ToolTip>();
		gameObject.GetComponentInChildren<ValueTrendImageToggle>().gameObject.SetActive(false);
		attributeLine.toolTipFunc = ((tooltip_func != null) ? tooltip_func : new Func<AttributeInstance, string>(attribute.GetTooltip));
		this.attributesLines.Add(attributeLine);
	}

	// Token: 0x06006AA6 RID: 27302 RVA: 0x00284190 File Offset: 0x00282390
	private void AddCheckboxLine(Amount amount, Transform parentContainer, Func<GameObject, string> label_text_func, Func<GameObject, MinionVitalsPanel.CheckboxLineDisplayType> display_condition, Func<GameObject, bool> checkbox_value_func, Func<GameObject, string> tooltip_func = null)
	{
		GameObject gameObject = Util.KInstantiateUI(this.CheckboxLinePrefab, this.Content.gameObject, false);
		HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
		gameObject.GetComponent<ToolTip>().refreshWhileHovering = true;
		gameObject.SetActive(true);
		MinionVitalsPanel.CheckboxLine checkboxLine = default(MinionVitalsPanel.CheckboxLine);
		checkboxLine.go = gameObject;
		checkboxLine.parentContainer = parentContainer;
		checkboxLine.amount = amount;
		checkboxLine.locText = component.GetReference("Label") as LocText;
		checkboxLine.get_value = checkbox_value_func;
		checkboxLine.display_condition = display_condition;
		checkboxLine.label_text_func = label_text_func;
		checkboxLine.go.name = "Checkbox_";
		if (amount != null)
		{
			GameObject go = checkboxLine.go;
			go.name += amount.Name;
		}
		else
		{
			GameObject go2 = checkboxLine.go;
			go2.name += "Unnamed";
		}
		if (tooltip_func != null)
		{
			checkboxLine.tooltip = tooltip_func;
			ToolTip tt = checkboxLine.go.GetComponent<ToolTip>();
			tt.refreshWhileHovering = true;
			tt.OnToolTip = delegate
			{
				tt.ClearMultiStringTooltip();
				tt.AddMultiStringTooltip(tooltip_func(this.lastSelectedEntity), null);
				return "";
			};
		}
		this.checkboxLines.Add(checkboxLine);
	}

	// Token: 0x06006AA7 RID: 27303 RVA: 0x002842D6 File Offset: 0x002824D6
	private void ShouldShowVitalsPanel(GameObject selectedEntity)
	{
	}

	// Token: 0x06006AA8 RID: 27304 RVA: 0x002842D8 File Offset: 0x002824D8
	public void Refresh(GameObject selectedEntity)
	{
		if (selectedEntity == null)
		{
			return;
		}
		if (selectedEntity.gameObject == null)
		{
			return;
		}
		this.lastSelectedEntity = selectedEntity;
		WiltCondition component = selectedEntity.GetComponent<WiltCondition>();
		MinionIdentity component2 = selectedEntity.GetComponent<MinionIdentity>();
		CreatureBrain component3 = selectedEntity.GetComponent<CreatureBrain>();
		IncubationMonitor.Instance smi = selectedEntity.GetSMI<IncubationMonitor.Instance>();
		object[] array = new object[] { component, component2, component3, smi };
		bool flag = false;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != null)
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			base.SetActive(false);
			return;
		}
		base.SetActive(true);
		base.SetTitle((component == null) ? UI.DETAILTABS.SIMPLEINFO.GROUPNAME_CONDITION : UI.DETAILTABS.SIMPLEINFO.GROUPNAME_REQUIREMENTS);
		Amounts amounts = selectedEntity.GetAmounts();
		Attributes attributes = selectedEntity.GetAttributes();
		if (amounts == null || attributes == null)
		{
			return;
		}
		if (component == null)
		{
			this.conditionsContainerNormal.gameObject.SetActive(false);
			this.conditionsContainerAdditional.gameObject.SetActive(false);
			foreach (MinionVitalsPanel.AmountLine amountLine in this.amountsLines)
			{
				bool flag2 = amountLine.TryUpdate(amounts);
				if (amountLine.go.activeSelf != flag2)
				{
					amountLine.go.SetActive(flag2);
				}
			}
			foreach (MinionVitalsPanel.AttributeLine attributeLine in this.attributesLines)
			{
				bool flag3 = attributeLine.TryUpdate(attributes);
				if (attributeLine.go.activeSelf != flag3)
				{
					attributeLine.go.SetActive(flag3);
				}
			}
		}
		bool flag4 = false;
		for (int j = 0; j < this.checkboxLines.Count; j++)
		{
			MinionVitalsPanel.CheckboxLine checkboxLine = this.checkboxLines[j];
			MinionVitalsPanel.CheckboxLineDisplayType checkboxLineDisplayType = MinionVitalsPanel.CheckboxLineDisplayType.Hidden;
			if (this.checkboxLines[j].amount != null)
			{
				for (int k = 0; k < amounts.Count; k++)
				{
					AmountInstance amountInstance = amounts[k];
					if (checkboxLine.amount == amountInstance.amount)
					{
						checkboxLineDisplayType = checkboxLine.display_condition(selectedEntity.gameObject);
						break;
					}
				}
			}
			else
			{
				checkboxLineDisplayType = checkboxLine.display_condition(selectedEntity.gameObject);
			}
			if (checkboxLineDisplayType != MinionVitalsPanel.CheckboxLineDisplayType.Hidden)
			{
				checkboxLine.locText.SetText(checkboxLine.label_text_func(selectedEntity.gameObject));
				if (!checkboxLine.go.activeSelf)
				{
					checkboxLine.go.SetActive(true);
				}
				GameObject gameObject = checkboxLine.go.GetComponent<HierarchyReferences>().GetReference("Check").gameObject;
				gameObject.SetActive(checkboxLine.get_value(selectedEntity.gameObject));
				if (checkboxLine.go.transform.parent != checkboxLine.parentContainer)
				{
					checkboxLine.go.transform.SetParent(checkboxLine.parentContainer);
					checkboxLine.go.transform.localScale = Vector3.one;
				}
				if (checkboxLine.parentContainer == this.conditionsContainerAdditional)
				{
					flag4 = true;
				}
				if (checkboxLineDisplayType == MinionVitalsPanel.CheckboxLineDisplayType.Normal)
				{
					if (checkboxLine.get_value(selectedEntity.gameObject))
					{
						checkboxLine.locText.color = Color.black;
						gameObject.transform.parent.GetComponent<Image>().color = Color.black;
					}
					else
					{
						Color color = new Color(0.99215686f, 0f, 0.101960786f);
						checkboxLine.locText.color = color;
						gameObject.transform.parent.GetComponent<Image>().color = color;
					}
				}
				else
				{
					checkboxLine.locText.color = Color.grey;
					gameObject.transform.parent.GetComponent<Image>().color = Color.grey;
				}
			}
			else if (checkboxLine.go.activeSelf)
			{
				checkboxLine.go.SetActive(false);
			}
		}
		if (component != null)
		{
			IManageGrowingStates manageGrowingStates = component.GetComponent<IManageGrowingStates>();
			manageGrowingStates = ((manageGrowingStates != null) ? manageGrowingStates : component.GetSMI<IManageGrowingStates>());
			bool flag5 = component.HasTag(GameTags.Decoration);
			this.conditionsContainerNormal.gameObject.SetActive(true);
			this.conditionsContainerAdditional.gameObject.SetActive(!flag5);
			if (manageGrowingStates == null)
			{
				float num = 1f;
				LocText reference = this.conditionsContainerNormal.GetComponent<HierarchyReferences>().GetReference<LocText>("Label");
				reference.text = "";
				reference.text = (flag5 ? string.Format(UI.VITALSSCREEN.CONDITIONS_GROWING.WILD_DECOR.BASE, Array.Empty<object>()) : string.Format(UI.VITALSSCREEN.CONDITIONS_GROWING.WILD_INSTANT.BASE, Util.FormatTwoDecimalPlace(num * 0.25f * 100f)));
				reference.GetComponent<ToolTip>().SetSimpleTooltip(string.Format(UI.VITALSSCREEN.CONDITIONS_GROWING.WILD_INSTANT.TOOLTIP, Array.Empty<object>()));
				LocText reference2 = this.conditionsContainerAdditional.GetComponent<HierarchyReferences>().GetReference<LocText>("Label");
				ReceptacleMonitor component4 = selectedEntity.GetComponent<ReceptacleMonitor>();
				reference2.color = ((component4 == null || component4.Replanted) ? Color.black : Color.grey);
				reference2.text = string.Format(UI.VITALSSCREEN.CONDITIONS_GROWING.ADDITIONAL_DOMESTIC_INSTANT.BASE, Util.FormatTwoDecimalPlace(num * 100f));
				reference2.GetComponent<ToolTip>().SetSimpleTooltip(string.Format(UI.VITALSSCREEN.CONDITIONS_GROWING.ADDITIONAL_DOMESTIC_INSTANT.TOOLTIP, Array.Empty<object>()));
			}
			else
			{
				LocText reference3 = this.conditionsContainerNormal.GetComponent<HierarchyReferences>().GetReference<LocText>("Label");
				reference3.text = "";
				reference3.text = string.Format(UI.VITALSSCREEN.CONDITIONS_GROWING.WILD.BASE, GameUtil.GetFormattedCycles(manageGrowingStates.WildGrowthTime(), "F1", false));
				reference3.GetComponent<ToolTip>().SetSimpleTooltip(string.Format(UI.VITALSSCREEN.CONDITIONS_GROWING.WILD.TOOLTIP, GameUtil.GetFormattedCycles(manageGrowingStates.WildGrowthTime(), "F1", false)));
				LocText reference4 = this.conditionsContainerAdditional.GetComponent<HierarchyReferences>().GetReference<LocText>("Label");
				reference4.color = (manageGrowingStates.IsWildPlanted() ? Color.grey : Color.black);
				reference4.text = "";
				reference4.text = (flag4 ? string.Format(UI.VITALSSCREEN.CONDITIONS_GROWING.ADDITIONAL_DOMESTIC.BASE, GameUtil.GetFormattedCycles(manageGrowingStates.DomesticGrowthTime(), "F1", false)) : string.Format(UI.VITALSSCREEN.CONDITIONS_GROWING.DOMESTIC.BASE, GameUtil.GetFormattedCycles(manageGrowingStates.DomesticGrowthTime(), "F1", false)));
				reference4.GetComponent<ToolTip>().SetSimpleTooltip(string.Format(UI.VITALSSCREEN.CONDITIONS_GROWING.ADDITIONAL_DOMESTIC.TOOLTIP, GameUtil.GetFormattedCycles(manageGrowingStates.DomesticGrowthTime(), "F1", false)));
			}
			foreach (MinionVitalsPanel.AmountLine amountLine2 in this.amountsLines)
			{
				amountLine2.go.SetActive(false);
			}
			foreach (MinionVitalsPanel.AttributeLine attributeLine2 in this.attributesLines)
			{
				attributeLine2.go.SetActive(false);
			}
		}
	}

	// Token: 0x06006AA9 RID: 27305 RVA: 0x00284A18 File Offset: 0x00282C18
	private string GetAirPressureTooltip(GameObject go)
	{
		PressureVulnerable component = go.GetComponent<PressureVulnerable>();
		if (component == null)
		{
			return "";
		}
		return UI.TOOLTIPS.VITALS_CHECKBOX_PRESSURE.text.Replace("{pressure}", GameUtil.GetFormattedMass(component.GetExternalPressure(), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
	}

	// Token: 0x06006AAA RID: 27306 RVA: 0x00284A64 File Offset: 0x00282C64
	private string GetInternalTemperatureTooltip(GameObject go)
	{
		TemperatureVulnerable component = go.GetComponent<TemperatureVulnerable>();
		if (component == null)
		{
			return "";
		}
		return UI.TOOLTIPS.VITALS_CHECKBOX_TEMPERATURE.text.Replace("{temperature}", GameUtil.GetFormattedTemperature(component.InternalTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
	}

	// Token: 0x06006AAB RID: 27307 RVA: 0x00284AAC File Offset: 0x00282CAC
	private string GetFertilizationTooltip(GameObject go)
	{
		FertilizationMonitor.Instance smi = go.GetSMI<FertilizationMonitor.Instance>();
		if (smi == null)
		{
			return "";
		}
		return UI.TOOLTIPS.VITALS_CHECKBOX_FERTILIZER.text.Replace("{mass}", GameUtil.GetFormattedMass(smi.total_fertilizer_available, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
	}

	// Token: 0x06006AAC RID: 27308 RVA: 0x00284AF0 File Offset: 0x00282CF0
	private string GetIrrigationTooltip(GameObject go)
	{
		IrrigationMonitor.Instance smi = go.GetSMI<IrrigationMonitor.Instance>();
		if (smi == null)
		{
			return "";
		}
		return UI.TOOLTIPS.VITALS_CHECKBOX_IRRIGATION.text.Replace("{mass}", GameUtil.GetFormattedMass(smi.total_fertilizer_available, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
	}

	// Token: 0x06006AAD RID: 27309 RVA: 0x00284B34 File Offset: 0x00282D34
	private string GetIlluminationTooltip(GameObject go)
	{
		IIlluminationTracker illuminationTracker = go.GetComponent<IIlluminationTracker>();
		if (illuminationTracker == null)
		{
			illuminationTracker = go.GetSMI<IIlluminationTracker>();
		}
		if (illuminationTracker == null)
		{
			return "";
		}
		return illuminationTracker.GetIlluminationUITooltip();
	}

	// Token: 0x06006AAE RID: 27310 RVA: 0x00284B64 File Offset: 0x00282D64
	private string GetRadiationTooltip(GameObject go)
	{
		int num = Grid.PosToCell(go);
		float num2 = (Grid.IsValidCell(num) ? Grid.Radiation[num] : 0f);
		AttributeInstance attributeInstance = go.GetAttributes().Get(Db.Get().PlantAttributes.MinRadiationThreshold);
		AttributeInstance attributeInstance2 = go.GetAttributes().Get(Db.Get().PlantAttributes.MaxRadiationThreshold);
		MutantPlant component = go.GetComponent<MutantPlant>();
		bool flag = component != null && component.IsOriginal;
		string text;
		if (attributeInstance.GetTotalValue() == 0f)
		{
			text = UI.TOOLTIPS.VITALS_CHECKBOX_RADIATION_NO_MIN.Replace("{rads}", GameUtil.GetFormattedRads(num2, GameUtil.TimeSlice.None)).Replace("{maxRads}", attributeInstance2.GetFormattedValue());
		}
		else
		{
			text = UI.TOOLTIPS.VITALS_CHECKBOX_RADIATION.Replace("{rads}", GameUtil.GetFormattedRads(num2, GameUtil.TimeSlice.None)).Replace("{minRads}", attributeInstance.GetFormattedValue()).Replace("{maxRads}", attributeInstance2.GetFormattedValue());
		}
		if (flag)
		{
			text += UI.GAMEOBJECTEFFECTS.TOOLTIPS.MUTANT_SEED_TOOLTIP;
		}
		return text;
	}

	// Token: 0x06006AAF RID: 27311 RVA: 0x00284C6C File Offset: 0x00282E6C
	private string GetReceptacleTooltip(GameObject go)
	{
		ReceptacleMonitor component = go.GetComponent<ReceptacleMonitor>();
		if (component == null)
		{
			return "";
		}
		if (component.HasOperationalReceptacle())
		{
			return UI.TOOLTIPS.VITALS_CHECKBOX_RECEPTACLE_OPERATIONAL;
		}
		return UI.TOOLTIPS.VITALS_CHECKBOX_RECEPTACLE_INOPERATIONAL;
	}

	// Token: 0x06006AB0 RID: 27312 RVA: 0x00284CAC File Offset: 0x00282EAC
	private string GetEntityConsumedTooltip(GameObject go)
	{
		IPlantConsumeEntities component = go.GetComponent<IPlantConsumeEntities>();
		component.AreEntitiesConsumptionRequirementsSatisfied();
		return GameUtil.SafeStringFormat(UI.TOOLTIPS.VITALS_CHECKBOX_ENTITY_CONSUMER_REQUIREMENTS, new object[] { component.GetConsumableEntitiesCategoryName() });
	}

	// Token: 0x06006AB1 RID: 27313 RVA: 0x00284CE5 File Offset: 0x00282EE5
	private string GetPollinationTooltip(GameObject go)
	{
		if (!go.GetComponent<WiltCondition>().IsConditionSatisifed(WiltCondition.Condition.Pollination))
		{
			return this.UnpollinatedTooltip;
		}
		return UI.TOOLTIPS.VITALS_CHECKBOX_POLLINATED;
	}

	// Token: 0x06006AB2 RID: 27314 RVA: 0x00284D08 File Offset: 0x00282F08
	private string GetAtmosphereTooltip(GameObject go)
	{
		PressureVulnerable component = go.GetComponent<PressureVulnerable>();
		if (component != null && component.currentAtmoElement != null)
		{
			return UI.TOOLTIPS.VITALS_CHECKBOX_ATMOSPHERE.text.Replace("{element}", component.currentAtmoElement.name);
		}
		return UI.TOOLTIPS.VITALS_CHECKBOX_ATMOSPHERE;
	}

	// Token: 0x06006AB3 RID: 27315 RVA: 0x00284D58 File Offset: 0x00282F58
	private string GetAirPressureLabel(GameObject go)
	{
		PressureVulnerable component = go.GetComponent<PressureVulnerable>();
		return string.Concat(new string[]
		{
			Db.Get().Amounts.AirPressure.Name,
			"\n    • ",
			GameUtil.GetFormattedMass(component.pressureWarning_Low, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Gram, false, "{0:0.#}"),
			" - ",
			GameUtil.GetFormattedMass(component.pressureWarning_High, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Gram, true, "{0:0.#}")
		});
	}

	// Token: 0x06006AB4 RID: 27316 RVA: 0x00284DCC File Offset: 0x00282FCC
	private string GetInternalTemperatureLabel(GameObject go)
	{
		TemperatureVulnerable component = go.GetComponent<TemperatureVulnerable>();
		return string.Concat(new string[]
		{
			Db.Get().Amounts.Temperature.Name,
			"\n    • ",
			GameUtil.GetFormattedTemperature(component.TemperatureWarningLow, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, false, false),
			" - ",
			GameUtil.GetFormattedTemperature(component.TemperatureWarningHigh, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)
		});
	}

	// Token: 0x06006AB5 RID: 27317 RVA: 0x00284E38 File Offset: 0x00283038
	private string GetFertilizationLabel(GameObject go)
	{
		StateMachine<FertilizationMonitor, FertilizationMonitor.Instance, IStateMachineTarget, FertilizationMonitor.Def>.GenericInstance smi = go.GetSMI<FertilizationMonitor.Instance>();
		string text = Db.Get().Amounts.Fertilization.Name;
		float totalValue = go.GetAttributes().Get(Db.Get().PlantAttributes.FertilizerUsageMod).GetTotalValue();
		foreach (PlantElementAbsorber.ConsumeInfo consumeInfo in smi.def.consumedElements)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n    • ",
				ElementLoader.GetElement(consumeInfo.tag).name,
				" ",
				GameUtil.GetFormattedMass(consumeInfo.massConsumptionRate * totalValue, GameUtil.TimeSlice.PerCycle, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")
			});
		}
		return text;
	}

	// Token: 0x06006AB6 RID: 27318 RVA: 0x00284EF0 File Offset: 0x002830F0
	private string GetIrrigationLabel(GameObject go)
	{
		StateMachine<IrrigationMonitor, IrrigationMonitor.Instance, IStateMachineTarget, IrrigationMonitor.Def>.GenericInstance smi = go.GetSMI<IrrigationMonitor.Instance>();
		string text = Db.Get().Amounts.Irrigation.Name;
		float totalValue = go.GetAttributes().Get(Db.Get().PlantAttributes.FertilizerUsageMod).GetTotalValue();
		foreach (PlantElementAbsorber.ConsumeInfo consumeInfo in smi.def.consumedElements)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n    • ",
				ElementLoader.GetElement(consumeInfo.tag).name,
				": ",
				GameUtil.GetFormattedMass(consumeInfo.massConsumptionRate * totalValue, GameUtil.TimeSlice.PerCycle, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")
			});
		}
		return text;
	}

	// Token: 0x06006AB7 RID: 27319 RVA: 0x00284FA8 File Offset: 0x002831A8
	private string GetIlluminationLabel(GameObject go)
	{
		IIlluminationTracker illuminationTracker = go.GetComponent<IIlluminationTracker>();
		if (illuminationTracker == null)
		{
			illuminationTracker = go.GetSMI<IIlluminationTracker>();
		}
		return illuminationTracker.GetIlluminationUILabel();
	}

	// Token: 0x06006AB8 RID: 27320 RVA: 0x00284FCC File Offset: 0x002831CC
	private string GetEntityConsumptionLabel(GameObject go)
	{
		IPlantConsumeEntities component = go.GetComponent<IPlantConsumeEntities>();
		return component.GetRequirementText() + "\n    • " + (this.check_entity_consumed(go) ? GameUtil.SafeStringFormat(UI.TOOLTIPS.VITALS_CHECKBOX_ENTITY_CONSUMER_SATISFIED, new object[] { component.GetConsumedEntityName() }) : UI.TOOLTIPS.VITALS_CHECKBOX_ENTITY_CONSUMER_UNSATISFIED);
	}

	// Token: 0x06006AB9 RID: 27321 RVA: 0x00285023 File Offset: 0x00283223
	private string GetPollinationLabel(GameObject go)
	{
		return UI.VITALSSCREEN.POLLINATION;
	}

	// Token: 0x06006ABA RID: 27322 RVA: 0x00285030 File Offset: 0x00283230
	private string GetAtmosphereLabel(GameObject go)
	{
		PressureVulnerable component = go.GetComponent<PressureVulnerable>();
		string text = UI.VITALSSCREEN.ATMOSPHERE_CONDITION;
		foreach (Element element in component.safe_atmospheres)
		{
			text = text + "\n    • " + element.name;
		}
		return text;
	}

	// Token: 0x06006ABB RID: 27323 RVA: 0x002850A0 File Offset: 0x002832A0
	private string GetRadiationLabel(GameObject go)
	{
		AttributeInstance attributeInstance = go.GetAttributes().Get(Db.Get().PlantAttributes.MinRadiationThreshold);
		AttributeInstance attributeInstance2 = go.GetAttributes().Get(Db.Get().PlantAttributes.MaxRadiationThreshold);
		if (attributeInstance.GetTotalValue() == 0f)
		{
			return UI.GAMEOBJECTEFFECTS.AMBIENT_RADIATION + "\n    • " + UI.GAMEOBJECTEFFECTS.AMBIENT_NO_MIN_RADIATION_FMT.Replace("{maxRads}", attributeInstance2.GetFormattedValue());
		}
		return UI.GAMEOBJECTEFFECTS.AMBIENT_RADIATION + "\n    • " + UI.GAMEOBJECTEFFECTS.AMBIENT_RADIATION_FMT.Replace("{minRads}", attributeInstance.GetFormattedValue()).Replace("{maxRads}", attributeInstance2.GetFormattedValue());
	}

	// Token: 0x06006ABC RID: 27324 RVA: 0x00285154 File Offset: 0x00283354
	private bool check_pressure(GameObject go)
	{
		PressureVulnerable component = go.GetComponent<PressureVulnerable>();
		return !(component != null) || component.ExternalPressureState == PressureVulnerable.PressureState.Normal;
	}

	// Token: 0x06006ABD RID: 27325 RVA: 0x0028517C File Offset: 0x0028337C
	private bool check_temperature(GameObject go)
	{
		TemperatureVulnerable component = go.GetComponent<TemperatureVulnerable>();
		return !(component != null) || component.GetInternalTemperatureState == TemperatureVulnerable.TemperatureState.Normal;
	}

	// Token: 0x06006ABE RID: 27326 RVA: 0x002851A4 File Offset: 0x002833A4
	private bool check_irrigation(GameObject go)
	{
		IrrigationMonitor.Instance smi = go.GetSMI<IrrigationMonitor.Instance>();
		return smi == null || (!smi.IsInsideState(smi.sm.replanted.starved) && !smi.IsInsideState(smi.sm.wild));
	}

	// Token: 0x06006ABF RID: 27327 RVA: 0x002851EC File Offset: 0x002833EC
	private bool check_illumination(GameObject go)
	{
		IIlluminationTracker illuminationTracker = go.GetComponent<IIlluminationTracker>();
		if (illuminationTracker == null)
		{
			illuminationTracker = go.GetSMI<IIlluminationTracker>();
		}
		return illuminationTracker == null || illuminationTracker.ShouldIlluminationUICheckboxBeChecked();
	}

	// Token: 0x06006AC0 RID: 27328 RVA: 0x00285218 File Offset: 0x00283418
	private bool check_radiation(GameObject go)
	{
		AttributeInstance attributeInstance = go.GetAttributes().Get(Db.Get().PlantAttributes.MinRadiationThreshold);
		if (attributeInstance != null && attributeInstance.GetTotalValue() != 0f)
		{
			int num = Grid.PosToCell(go);
			return (Grid.IsValidCell(num) ? Grid.Radiation[num] : 0f) >= attributeInstance.GetTotalValue();
		}
		return true;
	}

	// Token: 0x06006AC1 RID: 27329 RVA: 0x00285280 File Offset: 0x00283480
	private bool check_receptacle(GameObject go)
	{
		ReceptacleMonitor component = go.GetComponent<ReceptacleMonitor>();
		return !(component == null) && component.HasOperationalReceptacle();
	}

	// Token: 0x06006AC2 RID: 27330 RVA: 0x002852A8 File Offset: 0x002834A8
	private bool check_fertilizer(GameObject go)
	{
		FertilizationMonitor.Instance smi = go.GetSMI<FertilizationMonitor.Instance>();
		return smi == null || smi.sm.hasCorrectFertilizer.Get(smi);
	}

	// Token: 0x06006AC3 RID: 27331 RVA: 0x002852D4 File Offset: 0x002834D4
	private bool check_atmosphere(GameObject go)
	{
		PressureVulnerable component = go.GetComponent<PressureVulnerable>();
		return !(component != null) || component.testAreaElementSafe;
	}

	// Token: 0x06006AC4 RID: 27332 RVA: 0x002852F9 File Offset: 0x002834F9
	private bool check_entity_consumed(GameObject go)
	{
		return go.GetComponent<IPlantConsumeEntities>().AreEntitiesConsumptionRequirementsSatisfied();
	}

	// Token: 0x040048BE RID: 18622
	public GameObject LineItemPrefab;

	// Token: 0x040048BF RID: 18623
	public GameObject CheckboxLinePrefab;

	// Token: 0x040048C0 RID: 18624
	private GameObject lastSelectedEntity;

	// Token: 0x040048C1 RID: 18625
	public List<MinionVitalsPanel.AmountLine> amountsLines = new List<MinionVitalsPanel.AmountLine>();

	// Token: 0x040048C2 RID: 18626
	public List<MinionVitalsPanel.AttributeLine> attributesLines = new List<MinionVitalsPanel.AttributeLine>();

	// Token: 0x040048C3 RID: 18627
	public List<MinionVitalsPanel.CheckboxLine> checkboxLines = new List<MinionVitalsPanel.CheckboxLine>();

	// Token: 0x040048C4 RID: 18628
	public Transform conditionsContainerNormal;

	// Token: 0x040048C5 RID: 18629
	public Transform conditionsContainerAdditional;

	// Token: 0x040048C6 RID: 18630
	private string unpollinatedTooltip;

	// Token: 0x02001F4E RID: 8014
	[DebuggerDisplay("{amount.Name}")]
	public struct AmountLine
	{
		// Token: 0x0600B2E9 RID: 45801 RVA: 0x003D8314 File Offset: 0x003D6514
		public bool TryUpdate(Amounts amounts)
		{
			foreach (AmountInstance amountInstance in amounts)
			{
				if (this.amount == amountInstance.amount && !amountInstance.hide)
				{
					this.locText.SetText(this.amount.GetDescription(amountInstance));
					this.toolTip.toolTip = this.toolTipFunc(amountInstance);
					this.imageToggle.SetValue(amountInstance);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400903F RID: 36927
		public Amount amount;

		// Token: 0x04009040 RID: 36928
		public GameObject go;

		// Token: 0x04009041 RID: 36929
		public ValueTrendImageToggle imageToggle;

		// Token: 0x04009042 RID: 36930
		public LocText locText;

		// Token: 0x04009043 RID: 36931
		public ToolTip toolTip;

		// Token: 0x04009044 RID: 36932
		public Func<AmountInstance, string> toolTipFunc;
	}

	// Token: 0x02001F4F RID: 8015
	[DebuggerDisplay("{attribute.Name}")]
	public struct AttributeLine
	{
		// Token: 0x0600B2EA RID: 45802 RVA: 0x003D83AC File Offset: 0x003D65AC
		public bool TryUpdate(Attributes attributes)
		{
			foreach (AttributeInstance attributeInstance in attributes)
			{
				if (this.attribute == attributeInstance.modifier && !attributeInstance.hide)
				{
					this.locText.SetText(this.attribute.GetDescription(attributeInstance));
					this.toolTip.toolTip = this.toolTipFunc(attributeInstance);
					return true;
				}
			}
			return false;
		}

		// Token: 0x04009045 RID: 36933
		public Klei.AI.Attribute attribute;

		// Token: 0x04009046 RID: 36934
		public GameObject go;

		// Token: 0x04009047 RID: 36935
		public LocText locText;

		// Token: 0x04009048 RID: 36936
		public ToolTip toolTip;

		// Token: 0x04009049 RID: 36937
		public Func<AttributeInstance, string> toolTipFunc;
	}

	// Token: 0x02001F50 RID: 8016
	public struct CheckboxLine
	{
		// Token: 0x0400904A RID: 36938
		public Amount amount;

		// Token: 0x0400904B RID: 36939
		public GameObject go;

		// Token: 0x0400904C RID: 36940
		public LocText locText;

		// Token: 0x0400904D RID: 36941
		public Func<GameObject, string> tooltip;

		// Token: 0x0400904E RID: 36942
		public Func<GameObject, bool> get_value;

		// Token: 0x0400904F RID: 36943
		public Func<GameObject, MinionVitalsPanel.CheckboxLineDisplayType> display_condition;

		// Token: 0x04009050 RID: 36944
		public Func<GameObject, string> label_text_func;

		// Token: 0x04009051 RID: 36945
		public Transform parentContainer;
	}

	// Token: 0x02001F51 RID: 8017
	public enum CheckboxLineDisplayType
	{
		// Token: 0x04009053 RID: 36947
		Normal,
		// Token: 0x04009054 RID: 36948
		Diminished,
		// Token: 0x04009055 RID: 36949
		Hidden
	}
}
