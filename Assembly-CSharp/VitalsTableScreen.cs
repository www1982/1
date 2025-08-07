using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000D3F RID: 3391
public class VitalsTableScreen : TableScreen
{
	// Token: 0x060068D3 RID: 26835 RVA: 0x0027A1D8 File Offset: 0x002783D8
	protected override void OnActivate()
	{
		this.has_default_duplicant_row = false;
		this.title = UI.VITALS;
		base.OnActivate();
		base.AddPortraitColumn("Portrait", new Action<IAssignableIdentity, GameObject>(base.on_load_portrait), null, true);
		base.AddButtonLabelColumn("Names", new Action<IAssignableIdentity, GameObject>(base.on_load_name_label), new Func<IAssignableIdentity, GameObject, string>(base.get_value_name_label), delegate(GameObject widget_go)
		{
			base.GetWidgetRow(widget_go).SelectMinion();
		}, delegate(GameObject widget_go)
		{
			base.GetWidgetRow(widget_go).SelectAndFocusMinion();
		}, new Comparison<IAssignableIdentity>(base.compare_rows_alphabetical), new Action<IAssignableIdentity, GameObject, ToolTip>(this.on_tooltip_name), new Action<IAssignableIdentity, GameObject, ToolTip>(base.on_tooltip_sort_alphabetically), false);
		base.AddLabelColumn("Stress", new Action<IAssignableIdentity, GameObject>(this.on_load_stress), new Func<IAssignableIdentity, GameObject, string>(this.get_value_stress_label), new Comparison<IAssignableIdentity>(this.compare_rows_stress), new Action<IAssignableIdentity, GameObject, ToolTip>(this.on_tooltip_stress), new Action<IAssignableIdentity, GameObject, ToolTip>(this.on_tooltip_sort_stress), 64, true);
		base.AddLabelColumn("QOLExpectations", new Action<IAssignableIdentity, GameObject>(this.on_load_qualityoflife_expectations), new Func<IAssignableIdentity, GameObject, string>(this.get_value_qualityoflife_expectations_label), new Comparison<IAssignableIdentity>(this.compare_rows_qualityoflife_expectations), new Action<IAssignableIdentity, GameObject, ToolTip>(this.on_tooltip_qualityoflife_expectations), new Action<IAssignableIdentity, GameObject, ToolTip>(this.on_tooltip_sort_qualityoflife_expectations), 64, true);
		if (Game.IsDlcActiveForCurrentSave("DLC3_ID"))
		{
			base.AddLabelColumn("PowerBanks", new Action<IAssignableIdentity, GameObject>(this.on_load_power_banks), new Func<IAssignableIdentity, GameObject, string>(this.get_value_power_banks_label), new Comparison<IAssignableIdentity>(this.compare_rows_power_banks), new Action<IAssignableIdentity, GameObject, ToolTip>(this.on_tooltip_power_banks), new Action<IAssignableIdentity, GameObject, ToolTip>(this.on_tooltip_sort_power_banks), 64, true);
		}
		base.AddLabelColumn("Fullness", new Action<IAssignableIdentity, GameObject>(this.on_load_fullness), new Func<IAssignableIdentity, GameObject, string>(this.get_value_fullness_label), new Comparison<IAssignableIdentity>(this.compare_rows_fullness), new Action<IAssignableIdentity, GameObject, ToolTip>(this.on_tooltip_fullness), new Action<IAssignableIdentity, GameObject, ToolTip>(this.on_tooltip_sort_fullness), 96, true);
		base.AddLabelColumn("Health", new Action<IAssignableIdentity, GameObject>(this.on_load_health), new Func<IAssignableIdentity, GameObject, string>(this.get_value_health_label), new Comparison<IAssignableIdentity>(this.compare_rows_health), new Action<IAssignableIdentity, GameObject, ToolTip>(this.on_tooltip_health), new Action<IAssignableIdentity, GameObject, ToolTip>(this.on_tooltip_sort_health), 64, true);
		base.AddLabelColumn("Immunity", new Action<IAssignableIdentity, GameObject>(this.on_load_sickness), new Func<IAssignableIdentity, GameObject, string>(this.get_value_sickness_label), new Comparison<IAssignableIdentity>(this.compare_rows_sicknesses), new Action<IAssignableIdentity, GameObject, ToolTip>(this.on_tooltip_sicknesses), new Action<IAssignableIdentity, GameObject, ToolTip>(this.on_tooltip_sort_sicknesses), 192, true);
	}

	// Token: 0x060068D4 RID: 26836 RVA: 0x0027A450 File Offset: 0x00278650
	private void on_load_stress(IAssignableIdentity minion, GameObject widget_go)
	{
		TableRow widgetRow = base.GetWidgetRow(widget_go);
		LocText componentInChildren = widget_go.GetComponentInChildren<LocText>(true);
		if (minion != null)
		{
			componentInChildren.text = (base.GetWidgetColumn(widget_go) as LabelTableColumn).get_value_action(minion, widget_go);
			return;
		}
		componentInChildren.text = (widgetRow.isDefault ? "" : UI.VITALSSCREEN.STRESS.ToString());
	}

	// Token: 0x060068D5 RID: 26837 RVA: 0x0027A4B0 File Offset: 0x002786B0
	private string get_value_stress_label(IAssignableIdentity identity, GameObject widget_go)
	{
		TableRow widgetRow = base.GetWidgetRow(widget_go);
		if (widgetRow.rowType == TableRow.RowType.Minion)
		{
			MinionIdentity minionIdentity = identity as MinionIdentity;
			if (minionIdentity != null)
			{
				return Db.Get().Amounts.Stress.Lookup(minionIdentity).GetValueString();
			}
		}
		else if (widgetRow.rowType == TableRow.RowType.StoredMinon)
		{
			return UI.TABLESCREENS.NA;
		}
		return "";
	}

	// Token: 0x060068D6 RID: 26838 RVA: 0x0027A514 File Offset: 0x00278714
	private int compare_rows_stress(IAssignableIdentity a, IAssignableIdentity b)
	{
		MinionIdentity minionIdentity = a as MinionIdentity;
		MinionIdentity minionIdentity2 = b as MinionIdentity;
		if (minionIdentity == null && minionIdentity2 == null)
		{
			return 0;
		}
		if (minionIdentity == null)
		{
			return -1;
		}
		if (minionIdentity2 == null)
		{
			return 1;
		}
		float value = Db.Get().Amounts.Stress.Lookup(minionIdentity).value;
		float value2 = Db.Get().Amounts.Stress.Lookup(minionIdentity2).value;
		return value2.CompareTo(value);
	}

	// Token: 0x060068D7 RID: 26839 RVA: 0x0027A598 File Offset: 0x00278798
	protected void on_tooltip_stress(IAssignableIdentity minion, GameObject widget_go, ToolTip tooltip)
	{
		tooltip.ClearMultiStringTooltip();
		switch (base.GetWidgetRow(widget_go).rowType)
		{
		case TableRow.RowType.Header:
		case TableRow.RowType.Default:
			break;
		case TableRow.RowType.Minion:
		{
			MinionIdentity minionIdentity = minion as MinionIdentity;
			if (minionIdentity != null)
			{
				tooltip.AddMultiStringTooltip(Db.Get().Amounts.Stress.Lookup(minionIdentity).GetTooltip(), null);
				return;
			}
			break;
		}
		case TableRow.RowType.StoredMinon:
			this.StoredMinionTooltip(minion, tooltip);
			break;
		default:
			return;
		}
	}

	// Token: 0x060068D8 RID: 26840 RVA: 0x0027A60C File Offset: 0x0027880C
	protected void on_tooltip_sort_stress(IAssignableIdentity minion, GameObject widget_go, ToolTip tooltip)
	{
		tooltip.ClearMultiStringTooltip();
		switch (base.GetWidgetRow(widget_go).rowType)
		{
		case TableRow.RowType.Header:
			tooltip.AddMultiStringTooltip(UI.TABLESCREENS.COLUMN_SORT_BY_STRESS, null);
			break;
		case TableRow.RowType.Default:
		case TableRow.RowType.Minion:
		case TableRow.RowType.StoredMinon:
			break;
		default:
			return;
		}
	}

	// Token: 0x060068D9 RID: 26841 RVA: 0x0027A654 File Offset: 0x00278854
	private void on_load_qualityoflife_expectations(IAssignableIdentity minion, GameObject widget_go)
	{
		TableRow widgetRow = base.GetWidgetRow(widget_go);
		LocText componentInChildren = widget_go.GetComponentInChildren<LocText>(true);
		if (minion != null)
		{
			componentInChildren.text = (base.GetWidgetColumn(widget_go) as LabelTableColumn).get_value_action(minion, widget_go);
			return;
		}
		componentInChildren.text = (widgetRow.isDefault ? "" : UI.VITALSSCREEN.QUALITYOFLIFE_EXPECTATIONS.ToString());
	}

	// Token: 0x060068DA RID: 26842 RVA: 0x0027A6B4 File Offset: 0x002788B4
	private string get_value_qualityoflife_expectations_label(IAssignableIdentity identity, GameObject widget_go)
	{
		TableRow widgetRow = base.GetWidgetRow(widget_go);
		if (widgetRow.rowType == TableRow.RowType.Minion)
		{
			MinionIdentity minionIdentity = identity as MinionIdentity;
			if (minionIdentity != null)
			{
				return Db.Get().Attributes.QualityOfLife.Lookup(minionIdentity).GetFormattedValue();
			}
		}
		else if (widgetRow.rowType == TableRow.RowType.StoredMinon)
		{
			return UI.TABLESCREENS.NA;
		}
		return "";
	}

	// Token: 0x060068DB RID: 26843 RVA: 0x0027A718 File Offset: 0x00278918
	private int compare_rows_qualityoflife_expectations(IAssignableIdentity a, IAssignableIdentity b)
	{
		MinionIdentity minionIdentity = a as MinionIdentity;
		MinionIdentity minionIdentity2 = b as MinionIdentity;
		if (minionIdentity == null && minionIdentity2 == null)
		{
			return 0;
		}
		if (minionIdentity == null)
		{
			return -1;
		}
		if (minionIdentity2 == null)
		{
			return 1;
		}
		float totalValue = Db.Get().Attributes.QualityOfLifeExpectation.Lookup(minionIdentity).GetTotalValue();
		float totalValue2 = Db.Get().Attributes.QualityOfLifeExpectation.Lookup(minionIdentity2).GetTotalValue();
		return totalValue.CompareTo(totalValue2);
	}

	// Token: 0x060068DC RID: 26844 RVA: 0x0027A79C File Offset: 0x0027899C
	protected void on_tooltip_qualityoflife_expectations(IAssignableIdentity identity, GameObject widget_go, ToolTip tooltip)
	{
		tooltip.ClearMultiStringTooltip();
		switch (base.GetWidgetRow(widget_go).rowType)
		{
		case TableRow.RowType.Header:
		case TableRow.RowType.Default:
			break;
		case TableRow.RowType.Minion:
		{
			MinionIdentity minionIdentity = identity as MinionIdentity;
			if (minionIdentity != null)
			{
				tooltip.AddMultiStringTooltip(Db.Get().Attributes.QualityOfLife.Lookup(minionIdentity).GetAttributeValueTooltip(), null);
				return;
			}
			break;
		}
		case TableRow.RowType.StoredMinon:
			this.StoredMinionTooltip(identity, tooltip);
			break;
		default:
			return;
		}
	}

	// Token: 0x060068DD RID: 26845 RVA: 0x0027A810 File Offset: 0x00278A10
	protected void on_tooltip_sort_qualityoflife_expectations(IAssignableIdentity minion, GameObject widget_go, ToolTip tooltip)
	{
		tooltip.ClearMultiStringTooltip();
		switch (base.GetWidgetRow(widget_go).rowType)
		{
		case TableRow.RowType.Header:
			tooltip.AddMultiStringTooltip(UI.TABLESCREENS.COLUMN_SORT_BY_EXPECTATIONS, null);
			break;
		case TableRow.RowType.Default:
		case TableRow.RowType.Minion:
		case TableRow.RowType.StoredMinon:
			break;
		default:
			return;
		}
	}

	// Token: 0x060068DE RID: 26846 RVA: 0x0027A858 File Offset: 0x00278A58
	private void on_load_health(IAssignableIdentity minion, GameObject widget_go)
	{
		TableRow widgetRow = base.GetWidgetRow(widget_go);
		LocText componentInChildren = widget_go.GetComponentInChildren<LocText>(true);
		if (minion != null)
		{
			componentInChildren.text = (base.GetWidgetColumn(widget_go) as LabelTableColumn).get_value_action(minion, widget_go);
			return;
		}
		componentInChildren.text = (widgetRow.isDefault ? "" : (componentInChildren.text = UI.VITALSSCREEN_HEALTH.ToString()));
	}

	// Token: 0x060068DF RID: 26847 RVA: 0x0027A8C0 File Offset: 0x00278AC0
	private string get_value_health_label(IAssignableIdentity minion, GameObject widget_go)
	{
		if (minion != null)
		{
			TableRow widgetRow = base.GetWidgetRow(widget_go);
			if (widgetRow.rowType == TableRow.RowType.Minion && minion as MinionIdentity != null)
			{
				return Db.Get().Amounts.HitPoints.Lookup(minion as MinionIdentity).GetValueString();
			}
			if (widgetRow.rowType == TableRow.RowType.StoredMinon)
			{
				return UI.TABLESCREENS.NA;
			}
		}
		return "";
	}

	// Token: 0x060068E0 RID: 26848 RVA: 0x0027A928 File Offset: 0x00278B28
	private int compare_rows_health(IAssignableIdentity a, IAssignableIdentity b)
	{
		MinionIdentity minionIdentity = a as MinionIdentity;
		MinionIdentity minionIdentity2 = b as MinionIdentity;
		if (minionIdentity == null && minionIdentity2 == null)
		{
			return 0;
		}
		if (minionIdentity == null)
		{
			return -1;
		}
		if (minionIdentity2 == null)
		{
			return 1;
		}
		float value = Db.Get().Amounts.HitPoints.Lookup(minionIdentity).value;
		float value2 = Db.Get().Amounts.HitPoints.Lookup(minionIdentity2).value;
		return value2.CompareTo(value);
	}

	// Token: 0x060068E1 RID: 26849 RVA: 0x0027A9AC File Offset: 0x00278BAC
	protected void on_tooltip_health(IAssignableIdentity identity, GameObject widget_go, ToolTip tooltip)
	{
		tooltip.ClearMultiStringTooltip();
		switch (base.GetWidgetRow(widget_go).rowType)
		{
		case TableRow.RowType.Header:
		case TableRow.RowType.Default:
			break;
		case TableRow.RowType.Minion:
		{
			MinionIdentity minionIdentity = identity as MinionIdentity;
			if (minionIdentity != null)
			{
				tooltip.AddMultiStringTooltip(Db.Get().Amounts.HitPoints.Lookup(minionIdentity).GetTooltip(), null);
				return;
			}
			break;
		}
		case TableRow.RowType.StoredMinon:
			this.StoredMinionTooltip(identity, tooltip);
			break;
		default:
			return;
		}
	}

	// Token: 0x060068E2 RID: 26850 RVA: 0x0027AA20 File Offset: 0x00278C20
	protected void on_tooltip_sort_health(IAssignableIdentity minion, GameObject widget_go, ToolTip tooltip)
	{
		tooltip.ClearMultiStringTooltip();
		switch (base.GetWidgetRow(widget_go).rowType)
		{
		case TableRow.RowType.Header:
			tooltip.AddMultiStringTooltip(UI.TABLESCREENS.COLUMN_SORT_BY_HITPOINTS, null);
			break;
		case TableRow.RowType.Default:
		case TableRow.RowType.Minion:
		case TableRow.RowType.StoredMinon:
			break;
		default:
			return;
		}
	}

	// Token: 0x060068E3 RID: 26851 RVA: 0x0027AA68 File Offset: 0x00278C68
	private void on_load_sickness(IAssignableIdentity minion, GameObject widget_go)
	{
		TableRow widgetRow = base.GetWidgetRow(widget_go);
		LocText componentInChildren = widget_go.GetComponentInChildren<LocText>(true);
		if (minion != null)
		{
			componentInChildren.text = (base.GetWidgetColumn(widget_go) as LabelTableColumn).get_value_action(minion, widget_go);
			return;
		}
		componentInChildren.text = (widgetRow.isDefault ? "" : UI.VITALSSCREEN_SICKNESS.ToString());
	}

	// Token: 0x060068E4 RID: 26852 RVA: 0x0027AAC8 File Offset: 0x00278CC8
	private string get_value_sickness_label(IAssignableIdentity minion, GameObject widget_go)
	{
		TableRow widgetRow = base.GetWidgetRow(widget_go);
		if (widgetRow.rowType == TableRow.RowType.Minion)
		{
			MinionIdentity minionIdentity = minion as MinionIdentity;
			if (minionIdentity != null)
			{
				List<KeyValuePair<string, float>> list = new List<KeyValuePair<string, float>>();
				foreach (SicknessInstance sicknessInstance in minionIdentity.GetComponent<MinionModifiers>().sicknesses)
				{
					list.Add(new KeyValuePair<string, float>(sicknessInstance.modifier.Name, sicknessInstance.GetInfectedTimeRemaining()));
				}
				if (DlcManager.FeatureRadiationEnabled())
				{
					RadiationMonitor.Instance smi = minionIdentity.GetSMI<RadiationMonitor.Instance>();
					if (smi != null && smi.sm.isSick.Get(smi))
					{
						Effects component = minionIdentity.GetComponent<Effects>();
						string text;
						if (component.HasEffect(RadiationMonitor.minorSicknessEffect) || component.HasEffect(RadiationMonitor.bionic_minorSicknessEffect))
						{
							text = Db.Get().effects.Get(RadiationMonitor.minorSicknessEffect).Name;
						}
						else if (component.HasEffect(RadiationMonitor.majorSicknessEffect) || component.HasEffect(RadiationMonitor.bionic_majorSicknessEffect))
						{
							text = Db.Get().effects.Get(RadiationMonitor.majorSicknessEffect).Name;
						}
						else if (component.HasEffect(RadiationMonitor.extremeSicknessEffect) || component.HasEffect(RadiationMonitor.bionic_extremeSicknessEffect))
						{
							text = Db.Get().effects.Get(RadiationMonitor.extremeSicknessEffect).Name;
						}
						else
						{
							text = DUPLICANTS.MODIFIERS.RADIATIONEXPOSUREDEADLY.NAME;
						}
						list.Add(new KeyValuePair<string, float>(text, smi.SicknessSecondsRemaining()));
					}
				}
				if (list.Count > 0)
				{
					string text2 = "";
					if (list.Count > 1)
					{
						float num = 0f;
						foreach (KeyValuePair<string, float> keyValuePair in list)
						{
							num = Mathf.Min(new float[] { keyValuePair.Value });
						}
						text2 += string.Format(UI.VITALSSCREEN.MULTIPLE_SICKNESSES, GameUtil.GetFormattedCycles(num, "F1", false));
					}
					else
					{
						foreach (KeyValuePair<string, float> keyValuePair2 in list)
						{
							if (!string.IsNullOrEmpty(text2))
							{
								text2 += "\n";
							}
							text2 += string.Format(UI.VITALSSCREEN.SICKNESS_REMAINING, keyValuePair2.Key, GameUtil.GetFormattedCycles(keyValuePair2.Value, "F1", false));
						}
					}
					return text2;
				}
				return UI.VITALSSCREEN.NO_SICKNESSES;
			}
		}
		else if (widgetRow.rowType == TableRow.RowType.StoredMinon)
		{
			return UI.TABLESCREENS.NA;
		}
		return "";
	}

	// Token: 0x060068E5 RID: 26853 RVA: 0x0027ADAC File Offset: 0x00278FAC
	private int compare_rows_sicknesses(IAssignableIdentity a, IAssignableIdentity b)
	{
		float num = 0f;
		return 0f.CompareTo(num);
	}

	// Token: 0x060068E6 RID: 26854 RVA: 0x0027ADD0 File Offset: 0x00278FD0
	protected void on_tooltip_sicknesses(IAssignableIdentity minion, GameObject widget_go, ToolTip tooltip)
	{
		tooltip.ClearMultiStringTooltip();
		switch (base.GetWidgetRow(widget_go).rowType)
		{
		case TableRow.RowType.Header:
		case TableRow.RowType.Default:
			break;
		case TableRow.RowType.Minion:
		{
			MinionIdentity minionIdentity = minion as MinionIdentity;
			if (minionIdentity != null)
			{
				bool flag = false;
				new List<KeyValuePair<string, float>>();
				if (DlcManager.FeatureRadiationEnabled())
				{
					RadiationMonitor.Instance smi = minionIdentity.GetSMI<RadiationMonitor.Instance>();
					if (smi != null && smi.sm.isSick.Get(smi))
					{
						tooltip.AddMultiStringTooltip(smi.GetEffectStatusTooltip(), null);
						flag = true;
					}
				}
				Sicknesses sicknesses = minionIdentity.GetComponent<MinionModifiers>().sicknesses;
				if (sicknesses.IsInfected())
				{
					flag = true;
					foreach (SicknessInstance sicknessInstance in sicknesses)
					{
						tooltip.AddMultiStringTooltip(UI.HORIZONTAL_RULE, null);
						tooltip.AddMultiStringTooltip(sicknessInstance.modifier.Name, null);
						StatusItem statusItem = sicknessInstance.GetStatusItem();
						tooltip.AddMultiStringTooltip(statusItem.GetTooltip(sicknessInstance.ExposureInfo), null);
					}
				}
				if (!flag)
				{
					tooltip.AddMultiStringTooltip(UI.VITALSSCREEN.NO_SICKNESSES, null);
					return;
				}
			}
			break;
		}
		case TableRow.RowType.StoredMinon:
			this.StoredMinionTooltip(minion, tooltip);
			break;
		default:
			return;
		}
	}

	// Token: 0x060068E7 RID: 26855 RVA: 0x0027AF0C File Offset: 0x0027910C
	protected void on_tooltip_sort_sicknesses(IAssignableIdentity minion, GameObject widget_go, ToolTip tooltip)
	{
		tooltip.ClearMultiStringTooltip();
		switch (base.GetWidgetRow(widget_go).rowType)
		{
		case TableRow.RowType.Header:
			tooltip.AddMultiStringTooltip(UI.TABLESCREENS.COLUMN_SORT_BY_SICKNESSES, null);
			break;
		case TableRow.RowType.Default:
		case TableRow.RowType.Minion:
		case TableRow.RowType.StoredMinon:
			break;
		default:
			return;
		}
	}

	// Token: 0x060068E8 RID: 26856 RVA: 0x0027AF54 File Offset: 0x00279154
	private void on_load_fullness(IAssignableIdentity minion, GameObject widget_go)
	{
		TableRow widgetRow = base.GetWidgetRow(widget_go);
		LocText componentInChildren = widget_go.GetComponentInChildren<LocText>(true);
		if (minion != null)
		{
			componentInChildren.text = (base.GetWidgetColumn(widget_go) as LabelTableColumn).get_value_action(minion, widget_go);
			return;
		}
		componentInChildren.text = (widgetRow.isDefault ? "" : UI.VITALSSCREEN_CALORIES.ToString());
	}

	// Token: 0x060068E9 RID: 26857 RVA: 0x0027AFB4 File Offset: 0x002791B4
	private string get_value_fullness_label(IAssignableIdentity minion, GameObject widget_go)
	{
		TableRow widgetRow = base.GetWidgetRow(widget_go);
		if (widgetRow.rowType == TableRow.RowType.Minion && minion as MinionIdentity != null)
		{
			AmountInstance amountInstance = Db.Get().Amounts.Calories.Lookup(minion as MinionIdentity);
			if (amountInstance != null)
			{
				return amountInstance.GetValueString();
			}
			return UI.TABLESCREENS.NA;
		}
		else
		{
			if (widgetRow.rowType == TableRow.RowType.StoredMinon)
			{
				return UI.TABLESCREENS.NA;
			}
			return "";
		}
	}

	// Token: 0x060068EA RID: 26858 RVA: 0x0027B02C File Offset: 0x0027922C
	private int compare_rows_fullness(IAssignableIdentity a, IAssignableIdentity b)
	{
		MinionIdentity minionIdentity = a as MinionIdentity;
		MinionIdentity minionIdentity2 = b as MinionIdentity;
		if (minionIdentity == null && minionIdentity2 == null)
		{
			return 0;
		}
		if (minionIdentity == null)
		{
			return -1;
		}
		if (minionIdentity2 == null)
		{
			return 1;
		}
		AmountInstance amountInstance = Db.Get().Amounts.Calories.Lookup(minionIdentity);
		AmountInstance amountInstance2 = Db.Get().Amounts.Calories.Lookup(minionIdentity2);
		if (amountInstance == null && amountInstance2 == null)
		{
			return 0;
		}
		if (amountInstance == null)
		{
			return -1;
		}
		if (amountInstance2 == null)
		{
			return 1;
		}
		float value = amountInstance.value;
		float value2 = amountInstance2.value;
		return value2.CompareTo(value);
	}

	// Token: 0x060068EB RID: 26859 RVA: 0x0027B0C8 File Offset: 0x002792C8
	protected void on_tooltip_fullness(IAssignableIdentity identity, GameObject widget_go, ToolTip tooltip)
	{
		tooltip.ClearMultiStringTooltip();
		switch (base.GetWidgetRow(widget_go).rowType)
		{
		case TableRow.RowType.Header:
		case TableRow.RowType.Default:
			break;
		case TableRow.RowType.Minion:
		{
			MinionIdentity minionIdentity = identity as MinionIdentity;
			if (minionIdentity != null)
			{
				AmountInstance amountInstance = Db.Get().Amounts.Calories.Lookup(minionIdentity);
				if (amountInstance != null)
				{
					tooltip.AddMultiStringTooltip(amountInstance.GetTooltip(), null);
					tooltip.AddMultiStringTooltip("\n" + string.Format(UI.VITALSSCREEN.EATEN_TODAY_TOOLTIP, GameUtil.GetFormattedCalories(VitalsTableScreen.RationsEatenToday(minionIdentity), GameUtil.TimeSlice.None, true)), null);
					return;
				}
			}
			break;
		}
		case TableRow.RowType.StoredMinon:
			this.StoredMinionTooltip(identity, tooltip);
			break;
		default:
			return;
		}
	}

	// Token: 0x060068EC RID: 26860 RVA: 0x0027B16C File Offset: 0x0027936C
	protected void on_tooltip_sort_fullness(IAssignableIdentity minion, GameObject widget_go, ToolTip tooltip)
	{
		tooltip.ClearMultiStringTooltip();
		switch (base.GetWidgetRow(widget_go).rowType)
		{
		case TableRow.RowType.Header:
			tooltip.AddMultiStringTooltip(UI.TABLESCREENS.COLUMN_SORT_BY_FULLNESS, null);
			break;
		case TableRow.RowType.Default:
		case TableRow.RowType.Minion:
		case TableRow.RowType.StoredMinon:
			break;
		default:
			return;
		}
	}

	// Token: 0x060068ED RID: 26861 RVA: 0x0027B1B4 File Offset: 0x002793B4
	protected void on_tooltip_name(IAssignableIdentity minion, GameObject widget_go, ToolTip tooltip)
	{
		tooltip.ClearMultiStringTooltip();
		switch (base.GetWidgetRow(widget_go).rowType)
		{
		case TableRow.RowType.Header:
		case TableRow.RowType.Default:
		case TableRow.RowType.StoredMinon:
			break;
		case TableRow.RowType.Minion:
			if (minion != null)
			{
				tooltip.AddMultiStringTooltip(string.Format(UI.TABLESCREENS.GOTO_DUPLICANT_BUTTON, minion.GetProperName()), null);
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x060068EE RID: 26862 RVA: 0x0027B20C File Offset: 0x0027940C
	private void on_load_eaten_today(IAssignableIdentity minion, GameObject widget_go)
	{
		TableRow widgetRow = base.GetWidgetRow(widget_go);
		LocText componentInChildren = widget_go.GetComponentInChildren<LocText>(true);
		if (minion != null)
		{
			componentInChildren.text = (base.GetWidgetColumn(widget_go) as LabelTableColumn).get_value_action(minion, widget_go);
			return;
		}
		componentInChildren.text = (widgetRow.isDefault ? "" : UI.VITALSSCREEN_EATENTODAY.ToString());
	}

	// Token: 0x060068EF RID: 26863 RVA: 0x0027B26C File Offset: 0x0027946C
	private static float RationsEatenToday(MinionIdentity minion)
	{
		float num = 0f;
		if (minion != null)
		{
			RationMonitor.Instance smi = minion.GetSMI<RationMonitor.Instance>();
			if (smi != null)
			{
				num = smi.GetRationsAteToday();
			}
		}
		return num;
	}

	// Token: 0x060068F0 RID: 26864 RVA: 0x0027B29C File Offset: 0x0027949C
	private string get_value_eaten_today_label(IAssignableIdentity minion, GameObject widget_go)
	{
		TableRow widgetRow = base.GetWidgetRow(widget_go);
		if (widgetRow.rowType == TableRow.RowType.Minion)
		{
			return GameUtil.GetFormattedCalories(VitalsTableScreen.RationsEatenToday(minion as MinionIdentity), GameUtil.TimeSlice.None, true);
		}
		if (widgetRow.rowType == TableRow.RowType.StoredMinon)
		{
			return UI.TABLESCREENS.NA;
		}
		return "";
	}

	// Token: 0x060068F1 RID: 26865 RVA: 0x0027B2E8 File Offset: 0x002794E8
	private int compare_rows_eaten_today(IAssignableIdentity a, IAssignableIdentity b)
	{
		MinionIdentity minionIdentity = a as MinionIdentity;
		MinionIdentity minionIdentity2 = b as MinionIdentity;
		if (minionIdentity == null && minionIdentity2 == null)
		{
			return 0;
		}
		if (minionIdentity == null)
		{
			return -1;
		}
		if (minionIdentity2 == null)
		{
			return 1;
		}
		float num = VitalsTableScreen.RationsEatenToday(minionIdentity);
		return VitalsTableScreen.RationsEatenToday(minionIdentity2).CompareTo(num);
	}

	// Token: 0x060068F2 RID: 26866 RVA: 0x0027B344 File Offset: 0x00279544
	protected void on_tooltip_eaten_today(IAssignableIdentity minion, GameObject widget_go, ToolTip tooltip)
	{
		tooltip.ClearMultiStringTooltip();
		switch (base.GetWidgetRow(widget_go).rowType)
		{
		case TableRow.RowType.Header:
		case TableRow.RowType.Default:
			break;
		case TableRow.RowType.Minion:
			if (minion != null)
			{
				float num = VitalsTableScreen.RationsEatenToday(minion as MinionIdentity);
				tooltip.AddMultiStringTooltip(string.Format(UI.VITALSSCREEN.EATEN_TODAY_TOOLTIP, GameUtil.GetFormattedCalories(num, GameUtil.TimeSlice.None, true)), null);
				return;
			}
			break;
		case TableRow.RowType.StoredMinon:
			this.StoredMinionTooltip(minion, tooltip);
			break;
		default:
			return;
		}
	}

	// Token: 0x060068F3 RID: 26867 RVA: 0x0027B3B4 File Offset: 0x002795B4
	protected void on_tooltip_sort_eaten_today(IAssignableIdentity minion, GameObject widget_go, ToolTip tooltip)
	{
		tooltip.ClearMultiStringTooltip();
		switch (base.GetWidgetRow(widget_go).rowType)
		{
		case TableRow.RowType.Header:
			tooltip.AddMultiStringTooltip(UI.TABLESCREENS.COLUMN_SORT_BY_EATEN_TODAY, null);
			break;
		case TableRow.RowType.Default:
		case TableRow.RowType.Minion:
		case TableRow.RowType.StoredMinon:
			break;
		default:
			return;
		}
	}

	// Token: 0x060068F4 RID: 26868 RVA: 0x0027B3FC File Offset: 0x002795FC
	private void on_load_power_banks(IAssignableIdentity minion, GameObject widget_go)
	{
		TableRow widgetRow = base.GetWidgetRow(widget_go);
		LocText componentInChildren = widget_go.GetComponentInChildren<LocText>(true);
		if (minion != null)
		{
			componentInChildren.text = (base.GetWidgetColumn(widget_go) as LabelTableColumn).get_value_action(minion, widget_go);
			return;
		}
		componentInChildren.text = (widgetRow.isDefault ? "" : UI.VITALSSCREEN_POWERBANKS.ToString());
	}

	// Token: 0x060068F5 RID: 26869 RVA: 0x0027B45C File Offset: 0x0027965C
	private string get_value_power_banks_label(IAssignableIdentity minion, GameObject widget_go)
	{
		TableRow widgetRow = base.GetWidgetRow(widget_go);
		if (widgetRow.rowType == TableRow.RowType.Minion)
		{
			MinionIdentity minionIdentity = minion as MinionIdentity;
			if (minionIdentity != null && minionIdentity.HasTag(GameTags.Minions.Models.Bionic))
			{
				return GameUtil.GetFormattedJoules(minionIdentity.GetAmounts().Get(Db.Get().Amounts.BionicInternalBattery).value, "F1", GameUtil.TimeSlice.None);
			}
			return UI.TABLESCREENS.NA;
		}
		else
		{
			if (widgetRow.rowType == TableRow.RowType.StoredMinon)
			{
				return UI.TABLESCREENS.NA;
			}
			return "";
		}
	}

	// Token: 0x060068F6 RID: 26870 RVA: 0x0027B4E8 File Offset: 0x002796E8
	private int compare_rows_power_banks(IAssignableIdentity a, IAssignableIdentity b)
	{
		MinionIdentity minionIdentity = a as MinionIdentity;
		MinionIdentity minionIdentity2 = b as MinionIdentity;
		float num;
		if (minionIdentity != null && minionIdentity.HasTag(GameTags.Minions.Models.Bionic))
		{
			num = minionIdentity.GetAmounts().Get(Db.Get().Amounts.BionicInternalBattery).value;
		}
		else
		{
			num = -1f;
		}
		float num2;
		if (minionIdentity2 != null && minionIdentity2.HasTag(GameTags.Minions.Models.Bionic))
		{
			num2 = minionIdentity2.GetAmounts().Get(Db.Get().Amounts.BionicInternalBattery).value;
		}
		else
		{
			num2 = -1f;
		}
		return num2.CompareTo(num);
	}

	// Token: 0x060068F7 RID: 26871 RVA: 0x0027B594 File Offset: 0x00279794
	protected void on_tooltip_power_banks(IAssignableIdentity minion, GameObject widget_go, ToolTip tooltip)
	{
		tooltip.ClearMultiStringTooltip();
		switch (base.GetWidgetRow(widget_go).rowType)
		{
		case TableRow.RowType.Header:
		case TableRow.RowType.Default:
			break;
		case TableRow.RowType.Minion:
		{
			MinionIdentity minionIdentity = minion as MinionIdentity;
			if (minionIdentity != null && minionIdentity != null && minionIdentity.HasTag(GameTags.Minions.Models.Bionic))
			{
				tooltip.SetSimpleTooltip(minionIdentity.GetAmounts().Get(Db.Get().Amounts.BionicInternalBattery).GetDescription());
				return;
			}
			break;
		}
		case TableRow.RowType.StoredMinon:
			this.StoredMinionTooltip(minion, tooltip);
			break;
		default:
			return;
		}
	}

	// Token: 0x060068F8 RID: 26872 RVA: 0x0027B620 File Offset: 0x00279820
	protected void on_tooltip_sort_power_banks(IAssignableIdentity minion, GameObject widget_go, ToolTip tooltip)
	{
		tooltip.ClearMultiStringTooltip();
		switch (base.GetWidgetRow(widget_go).rowType)
		{
		case TableRow.RowType.Header:
			tooltip.AddMultiStringTooltip(UI.TABLESCREENS.COLUMN_SORT_BY_POWERBANKS, null);
			break;
		case TableRow.RowType.Default:
		case TableRow.RowType.Minion:
		case TableRow.RowType.StoredMinon:
			break;
		default:
			return;
		}
	}

	// Token: 0x060068F9 RID: 26873 RVA: 0x0027B668 File Offset: 0x00279868
	private void StoredMinionTooltip(IAssignableIdentity minion, ToolTip tooltip)
	{
		if (minion != null && minion as StoredMinionIdentity != null)
		{
			tooltip.AddMultiStringTooltip(string.Format(UI.TABLESCREENS.INFORMATION_NOT_AVAILABLE_TOOLTIP, (minion as StoredMinionIdentity).GetStorageReason(), minion.GetProperName()), null);
		}
	}
}
