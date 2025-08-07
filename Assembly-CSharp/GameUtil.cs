using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Database;
using Klei;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000930 RID: 2352
public static class GameUtil
{
	// Token: 0x060041DD RID: 16861 RVA: 0x001798B0 File Offset: 0x00177AB0
	public static CellOffset[] Expand(this CellOffset[] original)
	{
		List<CellOffset> list = new List<CellOffset>(original);
		Vector4 vector = new Vector2(float.MaxValue, float.MinValue);
		Vector4 vector2 = new Vector2(float.MaxValue, float.MinValue);
		foreach (CellOffset cellOffset in original)
		{
			if ((float)cellOffset.x < vector.x)
			{
				vector.x = (float)cellOffset.x;
			}
			if ((float)cellOffset.x > vector.y)
			{
				vector.y = (float)cellOffset.x;
			}
			if ((float)cellOffset.y < vector2.x)
			{
				vector2.x = (float)cellOffset.y;
			}
			if ((float)cellOffset.y > vector2.y)
			{
				vector2.y = (float)cellOffset.y;
			}
		}
		foreach (CellOffset cellOffset2 in original)
		{
			Vector2Int zero = Vector2Int.zero;
			if ((float)cellOffset2.x == vector.x)
			{
				list.Add(new CellOffset(cellOffset2.x - 1, cellOffset2.y));
				zero.x = -1;
			}
			if ((float)cellOffset2.x == vector.y)
			{
				list.Add(new CellOffset(cellOffset2.x + 1, cellOffset2.y));
				zero.x = 1;
			}
			if ((float)cellOffset2.y == vector2.x)
			{
				list.Add(new CellOffset(cellOffset2.x, cellOffset2.y - 1));
				zero.y = -1;
			}
			if ((float)cellOffset2.y == vector2.y)
			{
				list.Add(new CellOffset(cellOffset2.x, cellOffset2.y + 1));
				zero.y = 1;
			}
			if (zero.x != 0 && zero.y != 0)
			{
				list.Add(new CellOffset((int)((zero.x < 0) ? vector.x : vector.y) + zero.x, (int)((zero.y < 0) ? vector2.x : vector2.y) + zero.y));
			}
		}
		return list.ToArray();
	}

	// Token: 0x060041DE RID: 16862 RVA: 0x00179AE4 File Offset: 0x00177CE4
	public static string GetTemperatureUnitSuffix()
	{
		GameUtil.TemperatureUnit temperatureUnit = GameUtil.temperatureUnit;
		string text;
		if (temperatureUnit != GameUtil.TemperatureUnit.Celsius)
		{
			if (temperatureUnit != GameUtil.TemperatureUnit.Fahrenheit)
			{
				text = UI.UNITSUFFIXES.TEMPERATURE.KELVIN;
			}
			else
			{
				text = UI.UNITSUFFIXES.TEMPERATURE.FAHRENHEIT;
			}
		}
		else
		{
			text = UI.UNITSUFFIXES.TEMPERATURE.CELSIUS;
		}
		return text;
	}

	// Token: 0x060041DF RID: 16863 RVA: 0x00179B26 File Offset: 0x00177D26
	private static string AddTemperatureUnitSuffix(string text)
	{
		return text + GameUtil.GetTemperatureUnitSuffix();
	}

	// Token: 0x060041E0 RID: 16864 RVA: 0x00179B33 File Offset: 0x00177D33
	public static float GetTemperatureConvertedFromKelvin(float temperature, GameUtil.TemperatureUnit targetUnit)
	{
		if (targetUnit == GameUtil.TemperatureUnit.Celsius)
		{
			return temperature - 273.15f;
		}
		if (targetUnit != GameUtil.TemperatureUnit.Fahrenheit)
		{
			return temperature;
		}
		return temperature * 1.8f - 459.67f;
	}

	// Token: 0x060041E1 RID: 16865 RVA: 0x00179B58 File Offset: 0x00177D58
	public static float GetConvertedTemperature(float temperature, bool roundOutput = false)
	{
		GameUtil.TemperatureUnit temperatureUnit = GameUtil.temperatureUnit;
		if (temperatureUnit != GameUtil.TemperatureUnit.Celsius)
		{
			if (temperatureUnit != GameUtil.TemperatureUnit.Fahrenheit)
			{
				if (!roundOutput)
				{
					return temperature;
				}
				return Mathf.Round(temperature);
			}
			else
			{
				float num = temperature * 1.8f - 459.67f;
				if (!roundOutput)
				{
					return num;
				}
				return Mathf.Round(num);
			}
		}
		else
		{
			float num = temperature - 273.15f;
			if (!roundOutput)
			{
				return num;
			}
			return Mathf.Round(num);
		}
	}

	// Token: 0x060041E2 RID: 16866 RVA: 0x00179BB3 File Offset: 0x00177DB3
	public static float GetTemperatureConvertedToKelvin(float temperature, GameUtil.TemperatureUnit fromUnit)
	{
		if (fromUnit == GameUtil.TemperatureUnit.Celsius)
		{
			return temperature + 273.15f;
		}
		if (fromUnit != GameUtil.TemperatureUnit.Fahrenheit)
		{
			return temperature;
		}
		return (temperature + 459.67f) * 5f / 9f;
	}

	// Token: 0x060041E3 RID: 16867 RVA: 0x00179BDC File Offset: 0x00177DDC
	public static float GetTemperatureConvertedToKelvin(float temperature)
	{
		GameUtil.TemperatureUnit temperatureUnit = GameUtil.temperatureUnit;
		if (temperatureUnit == GameUtil.TemperatureUnit.Celsius)
		{
			return temperature + 273.15f;
		}
		if (temperatureUnit != GameUtil.TemperatureUnit.Fahrenheit)
		{
			return temperature;
		}
		return (temperature + 459.67f) * 5f / 9f;
	}

	// Token: 0x060041E4 RID: 16868 RVA: 0x00179C18 File Offset: 0x00177E18
	private static float GetConvertedTemperatureDelta(float kelvin_delta)
	{
		switch (GameUtil.temperatureUnit)
		{
		case GameUtil.TemperatureUnit.Celsius:
			return kelvin_delta;
		case GameUtil.TemperatureUnit.Fahrenheit:
			return kelvin_delta * 1.8f;
		case GameUtil.TemperatureUnit.Kelvin:
			return kelvin_delta;
		default:
			return kelvin_delta;
		}
	}

	// Token: 0x060041E5 RID: 16869 RVA: 0x00179C4C File Offset: 0x00177E4C
	public static float ApplyTimeSlice(float val, GameUtil.TimeSlice timeSlice)
	{
		if (timeSlice == GameUtil.TimeSlice.PerCycle)
		{
			return val * 600f;
		}
		return val;
	}

	// Token: 0x060041E6 RID: 16870 RVA: 0x00179C5B File Offset: 0x00177E5B
	public static float ApplyTimeSlice(int val, GameUtil.TimeSlice timeSlice)
	{
		if (timeSlice == GameUtil.TimeSlice.PerCycle)
		{
			return (float)val * 600f;
		}
		return (float)val;
	}

	// Token: 0x060041E7 RID: 16871 RVA: 0x00179C6C File Offset: 0x00177E6C
	public static string AddTimeSliceText(string text, GameUtil.TimeSlice timeSlice)
	{
		switch (timeSlice)
		{
		case GameUtil.TimeSlice.PerSecond:
			return text + UI.UNITSUFFIXES.PERSECOND;
		case GameUtil.TimeSlice.PerCycle:
			return text + UI.UNITSUFFIXES.PERCYCLE;
		}
		return text;
	}

	// Token: 0x060041E8 RID: 16872 RVA: 0x00179CA9 File Offset: 0x00177EA9
	public static void AddTimeSliceText(StringBuilder builder, GameUtil.TimeSlice timeSlice)
	{
		switch (timeSlice)
		{
		case GameUtil.TimeSlice.None:
		case GameUtil.TimeSlice.ModifyOnly:
			break;
		case GameUtil.TimeSlice.PerSecond:
			builder.Append(UI.UNITSUFFIXES.PERSECOND);
			return;
		case GameUtil.TimeSlice.PerCycle:
			builder.Append(UI.UNITSUFFIXES.PERCYCLE);
			break;
		default:
			return;
		}
	}

	// Token: 0x060041E9 RID: 16873 RVA: 0x00179CE5 File Offset: 0x00177EE5
	public static string AddPositiveSign(string text, bool positive)
	{
		if (positive)
		{
			return string.Format(UI.POSITIVE_FORMAT, text);
		}
		return text;
	}

	// Token: 0x060041EA RID: 16874 RVA: 0x00179CFC File Offset: 0x00177EFC
	public static float AttributeSkillToAlpha(AttributeInstance attributeInstance)
	{
		return Mathf.Min(attributeInstance.GetTotalValue() / 10f, 1f);
	}

	// Token: 0x060041EB RID: 16875 RVA: 0x00179D14 File Offset: 0x00177F14
	public static float AttributeSkillToAlpha(float attributeSkill)
	{
		return Mathf.Min(attributeSkill / 10f, 1f);
	}

	// Token: 0x060041EC RID: 16876 RVA: 0x00179D27 File Offset: 0x00177F27
	public static float AptitudeToAlpha(float aptitude)
	{
		return Mathf.Min(aptitude / 10f, 1f);
	}

	// Token: 0x060041ED RID: 16877 RVA: 0x00179D3A File Offset: 0x00177F3A
	public static float GetThermalEnergy(PrimaryElement pe)
	{
		return pe.Temperature * pe.Mass * pe.Element.specificHeatCapacity;
	}

	// Token: 0x060041EE RID: 16878 RVA: 0x00179D55 File Offset: 0x00177F55
	public static float CalculateTemperatureChange(float shc, float mass, float kilowatts)
	{
		return kilowatts / (shc * mass);
	}

	// Token: 0x060041EF RID: 16879 RVA: 0x00179D5C File Offset: 0x00177F5C
	public static void DeltaThermalEnergy(PrimaryElement pe, float kilowatts, float targetTemperature)
	{
		float num = GameUtil.CalculateTemperatureChange(pe.Element.specificHeatCapacity, pe.Mass, kilowatts);
		float num2 = pe.Temperature + num;
		if (targetTemperature > pe.Temperature)
		{
			num2 = Mathf.Clamp(num2, pe.Temperature, targetTemperature);
		}
		else
		{
			num2 = Mathf.Clamp(num2, targetTemperature, pe.Temperature);
		}
		pe.Temperature = num2;
	}

	// Token: 0x060041F0 RID: 16880 RVA: 0x00179DB8 File Offset: 0x00177FB8
	public static BindingEntry ActionToBinding(global::Action action)
	{
		foreach (BindingEntry bindingEntry in GameInputMapping.KeyBindings)
		{
			if (bindingEntry.mAction == action)
			{
				return bindingEntry;
			}
		}
		throw new ArgumentException(action.ToString() + " is not bound in GameInputBindings");
	}

	// Token: 0x060041F1 RID: 16881 RVA: 0x00179E08 File Offset: 0x00178008
	public static string GetIdentityDescriptor(GameObject go, GameUtil.IdentityDescriptorTense tense = GameUtil.IdentityDescriptorTense.Normal)
	{
		if (go.GetComponent<MinionIdentity>())
		{
			switch (tense)
			{
			case GameUtil.IdentityDescriptorTense.Normal:
				return DUPLICANTS.STATS.SUBJECTS.DUPLICANT;
			case GameUtil.IdentityDescriptorTense.Possessive:
				return DUPLICANTS.STATS.SUBJECTS.DUPLICANT_POSSESSIVE;
			case GameUtil.IdentityDescriptorTense.Plural:
				return DUPLICANTS.STATS.SUBJECTS.DUPLICANT_PLURAL;
			}
		}
		else if (go.GetComponent<CreatureBrain>())
		{
			switch (tense)
			{
			case GameUtil.IdentityDescriptorTense.Normal:
				return DUPLICANTS.STATS.SUBJECTS.CREATURE;
			case GameUtil.IdentityDescriptorTense.Possessive:
				return DUPLICANTS.STATS.SUBJECTS.CREATURE_POSSESSIVE;
			case GameUtil.IdentityDescriptorTense.Plural:
				return DUPLICANTS.STATS.SUBJECTS.CREATURE_PLURAL;
			}
		}
		else
		{
			switch (tense)
			{
			case GameUtil.IdentityDescriptorTense.Normal:
				return DUPLICANTS.STATS.SUBJECTS.PLANT;
			case GameUtil.IdentityDescriptorTense.Possessive:
				return DUPLICANTS.STATS.SUBJECTS.PLANT_POSESSIVE;
			case GameUtil.IdentityDescriptorTense.Plural:
				return DUPLICANTS.STATS.SUBJECTS.PLANT_PLURAL;
			}
		}
		return "";
	}

	// Token: 0x060041F2 RID: 16882 RVA: 0x00179ED6 File Offset: 0x001780D6
	public static float GetEnergyInPrimaryElement(PrimaryElement element)
	{
		return 0.001f * (element.Temperature * (element.Mass * 1000f * element.Element.specificHeatCapacity));
	}

	// Token: 0x060041F3 RID: 16883 RVA: 0x00179F00 File Offset: 0x00178100
	public static float EnergyToTemperatureDelta(float kilojoules, PrimaryElement element)
	{
		global::Debug.Assert(element.Mass > 0f);
		float num = Mathf.Max(GameUtil.GetEnergyInPrimaryElement(element) - kilojoules, 1f);
		float temperature = element.Temperature;
		return num / (0.001f * (element.Mass * (element.Element.specificHeatCapacity * 1000f))) - temperature;
	}

	// Token: 0x060041F4 RID: 16884 RVA: 0x00179F59 File Offset: 0x00178159
	public static float CalculateEnergyDeltaForElement(PrimaryElement element, float startTemp, float endTemp)
	{
		return GameUtil.CalculateEnergyDeltaForElementChange(element.Mass, element.Element.specificHeatCapacity, startTemp, endTemp);
	}

	// Token: 0x060041F5 RID: 16885 RVA: 0x00179F73 File Offset: 0x00178173
	public static float CalculateEnergyDeltaForElementChange(float mass, float shc, float startTemp, float endTemp)
	{
		return (endTemp - startTemp) * mass * shc;
	}

	// Token: 0x060041F6 RID: 16886 RVA: 0x00179F7C File Offset: 0x0017817C
	public static float GetFinalTemperature(float t1, float m1, float t2, float m2)
	{
		float num = m1 + m2;
		float num2 = (t1 * m1 + t2 * m2) / num;
		float num3 = Mathf.Min(t1, t2);
		float num4 = Mathf.Max(t1, t2);
		num2 = Mathf.Clamp(num2, num3, num4);
		if (float.IsNaN(num2) || float.IsInfinity(num2))
		{
			global::Debug.LogError(string.Format("Calculated an invalid temperature: t1={0}, m1={1}, t2={2}, m2={3}, min_temp={4}, max_temp={5}", new object[] { t1, m1, t2, m2, num3, num4 }));
		}
		return num2;
	}

	// Token: 0x060041F7 RID: 16887 RVA: 0x0017A00C File Offset: 0x0017820C
	public static void ForceConduction(PrimaryElement a, PrimaryElement b, float dt)
	{
		float num = a.Temperature * a.Element.specificHeatCapacity * a.Mass;
		float num2 = b.Temperature * b.Element.specificHeatCapacity * b.Mass;
		float num3 = Math.Min(a.Element.thermalConductivity, b.Element.thermalConductivity);
		float num4 = Math.Min(a.Mass, b.Mass);
		float num5 = (b.Temperature - a.Temperature) * (num3 * num4) * dt;
		float num6 = (num + num2) / (a.Element.specificHeatCapacity * a.Mass + b.Element.specificHeatCapacity * b.Mass);
		float num7 = Math.Abs((num6 - a.Temperature) * a.Element.specificHeatCapacity * a.Mass);
		float num8 = Math.Abs((num6 - b.Temperature) * b.Element.specificHeatCapacity * b.Mass);
		float num9 = Math.Min(num7, num8);
		num5 = Math.Min(num5, num9);
		num5 = Math.Max(num5, -num9);
		a.Temperature = (num + num5) / a.Element.specificHeatCapacity / a.Mass;
		b.Temperature = (num2 - num5) / b.Element.specificHeatCapacity / b.Mass;
	}

	// Token: 0x060041F8 RID: 16888 RVA: 0x0017A158 File Offset: 0x00178358
	public static string FloatToString(float f, string format = null)
	{
		if (float.IsPositiveInfinity(f))
		{
			return UI.POS_INFINITY;
		}
		if (float.IsNegativeInfinity(f))
		{
			return UI.NEG_INFINITY;
		}
		return f.ToString(format);
	}

	// Token: 0x060041F9 RID: 16889 RVA: 0x0017A188 File Offset: 0x00178388
	public unsafe static void AppendFloatToString(StringBuilder builder, float f, string format = null)
	{
		if (float.IsPositiveInfinity(f))
		{
			builder.Append(UI.POS_INFINITY);
			return;
		}
		if (float.IsNegativeInfinity(f))
		{
			builder.Append(UI.NEG_INFINITY);
			return;
		}
		if (format != null)
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)128], 64);
			int num;
			f.TryFormat(span, out num, format, null);
			builder.Append(span.Slice(0, num));
			return;
		}
		builder.Append(f);
	}

	// Token: 0x060041FA RID: 16890 RVA: 0x0017A20C File Offset: 0x0017840C
	public static string GetFloatWithDecimalPoint(float f)
	{
		string text;
		if (f == 0f)
		{
			text = "0";
		}
		else if (Mathf.Abs(f) < 1f)
		{
			text = "#,##0.#";
		}
		else
		{
			text = "#,###.#";
		}
		return GameUtil.FloatToString(f, text);
	}

	// Token: 0x060041FB RID: 16891 RVA: 0x0017A254 File Offset: 0x00178454
	public static void AppendFloatWithDecimalPoint(StringBuilder builder, float f)
	{
		if (f == 0f)
		{
			builder.AppendFormat("{0:0}", f);
			return;
		}
		if (Mathf.Abs(f) < 1f)
		{
			builder.AppendFormat("{0:#,##0.#}", f);
			return;
		}
		builder.AppendFormat("{0:#,###.#}", f);
	}

	// Token: 0x060041FC RID: 16892 RVA: 0x0017A2B0 File Offset: 0x001784B0
	public static string GetStandardFloat(float f)
	{
		string text;
		if (f == 0f)
		{
			text = "0";
		}
		else if (Mathf.Abs(f) < 1f)
		{
			text = "#,##0.#";
		}
		else if (Mathf.Abs(f) < 10f)
		{
			text = "#,###.#";
		}
		else
		{
			text = "#,###";
		}
		return GameUtil.FloatToString(f, text);
	}

	// Token: 0x060041FD RID: 16893 RVA: 0x0017A30C File Offset: 0x0017850C
	public static void AppendStandardFloat(StringBuilder builder, float f)
	{
		if (float.IsPositiveInfinity(f))
		{
			builder.Append(UI.POS_INFINITY);
			return;
		}
		if (float.IsNegativeInfinity(f))
		{
			builder.Append(UI.NEG_INFINITY);
			return;
		}
		if (f == 0f)
		{
			builder.AppendFormat("{0:0}", f);
			return;
		}
		if (Math.Abs(f) < 1f)
		{
			builder.AppendFormat("{0:#,##0.##}", f);
			return;
		}
		if (Math.Abs(f) < 10f)
		{
			builder.AppendFormat("{0:#,##0.##}", f);
			return;
		}
		builder.AppendFormat("{0:#,###}", f);
	}

	// Token: 0x060041FE RID: 16894 RVA: 0x0017A3BC File Offset: 0x001785BC
	public static string GetStandardPercentageFloat(float f, bool allowHundredths = false)
	{
		string text;
		if (Mathf.Abs(f) == 0f)
		{
			text = "0";
		}
		else if (Mathf.Abs(f) < 0.1f && allowHundredths)
		{
			text = "##0.##";
		}
		else if (Mathf.Abs(f) < 1f)
		{
			text = "##0.#";
		}
		else
		{
			text = "##0";
		}
		return GameUtil.FloatToString(f, text);
	}

	// Token: 0x060041FF RID: 16895 RVA: 0x0017A420 File Offset: 0x00178620
	public static void AppendStandardPercentageFloat(StringBuilder builder, float f, bool allowHundredths = false)
	{
		if (Mathf.Abs(f) == 0f)
		{
			builder.AppendFormat("{0:0}", f);
			return;
		}
		if (Mathf.Abs(f) < 0.1f && allowHundredths)
		{
			builder.AppendFormat("{0:##0.##}", f);
			return;
		}
		if (Mathf.Abs(f) < 1f)
		{
			builder.AppendFormat("{0:##0.#}", f);
			return;
		}
		builder.AppendFormat("{0:##0}", f);
	}

	// Token: 0x06004200 RID: 16896 RVA: 0x0017A4A4 File Offset: 0x001786A4
	public static string GetUnitFormattedName(GameObject go, bool upperName = false)
	{
		KPrefabID component = go.GetComponent<KPrefabID>();
		if (component != null && Assets.IsTagCountable(component.PrefabTag))
		{
			PrimaryElement component2 = go.GetComponent<PrimaryElement>();
			return GameUtil.GetUnitFormattedName(go.GetProperName(), component2.Units, upperName);
		}
		if (!upperName)
		{
			return go.GetProperName();
		}
		return StringFormatter.ToUpper(go.GetProperName());
	}

	// Token: 0x06004201 RID: 16897 RVA: 0x0017A4FD File Offset: 0x001786FD
	public static string GetUnitFormattedName(string name, float count, bool upperName = false)
	{
		if (upperName)
		{
			name = name.ToUpper();
		}
		return StringFormatter.Replace(UI.NAME_WITH_UNITS, "{0}", name).Replace("{1}", string.Format("{0:0.##}", count));
	}

	// Token: 0x06004202 RID: 16898 RVA: 0x0017A53C File Offset: 0x0017873C
	public static void AppendFormattedUnits(StringBuilder builder, float units, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None, bool displaySuffix = true, string floatFormatOverride = "")
	{
		units = GameUtil.ApplyTimeSlice(units, timeSlice);
		if (!floatFormatOverride.IsNullOrWhiteSpace())
		{
			builder.AppendFormat(floatFormatOverride, units);
		}
		else
		{
			GameUtil.AppendStandardFloat(builder, units);
		}
		if (displaySuffix)
		{
			builder.Append((units == 1f) ? UI.UNITSUFFIXES.UNIT : UI.UNITSUFFIXES.UNITS);
		}
		GameUtil.AddTimeSliceText(builder, timeSlice);
	}

	// Token: 0x06004203 RID: 16899 RVA: 0x0017A59D File Offset: 0x0017879D
	public static string GetFormattedUnits(float units, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None, bool displaySuffix = true, string floatFormatOverride = "")
	{
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		GameUtil.AppendFormattedUnits(stringBuilder, units, timeSlice, displaySuffix, floatFormatOverride);
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}

	// Token: 0x06004204 RID: 16900 RVA: 0x0017A5B3 File Offset: 0x001787B3
	public static void AppendFormattedRocketRangePerCycle(StringBuilder builder, float range, bool displaySuffix = true)
	{
		if (displaySuffix)
		{
			builder.AppendFormat("{0:N1} {1}", range, UI.CLUSTERMAP.TILES_PER_CYCLE);
			return;
		}
		builder.AppendFormat("{0:N1}", range);
	}

	// Token: 0x06004205 RID: 16901 RVA: 0x0017A5E2 File Offset: 0x001787E2
	public static string GetFormattedRocketRangePerCycle(float range, bool displaySuffix = true)
	{
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		GameUtil.AppendFormattedRocketRangePerCycle(stringBuilder, range, displaySuffix);
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}

	// Token: 0x06004206 RID: 16902 RVA: 0x0017A5F6 File Offset: 0x001787F6
	public static void AppendFormattedRocketRange(StringBuilder builder, int rangeInTiles, bool displaySuffix = true)
	{
		builder.Append(rangeInTiles);
		if (displaySuffix)
		{
			builder.Append(" ");
			builder.Append(UI.CLUSTERMAP.TILES);
		}
	}

	// Token: 0x06004207 RID: 16903 RVA: 0x0017A620 File Offset: 0x00178820
	public static string GetFormattedRocketRange(int rangeInTiles, bool displaySuffix = true)
	{
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		GameUtil.AppendFormattedRocketRange(stringBuilder, rangeInTiles, displaySuffix);
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}

	// Token: 0x06004208 RID: 16904 RVA: 0x0017A634 File Offset: 0x00178834
	public static string ApplyBoldString(string source)
	{
		return "<b>" + source + "</b>";
	}

	// Token: 0x06004209 RID: 16905 RVA: 0x0017A646 File Offset: 0x00178846
	public static void AppendBoldString(StringBuilder builder, string source)
	{
		builder.AppendFormat("<b>{0}</b>", source);
	}

	// Token: 0x0600420A RID: 16906 RVA: 0x0017A658 File Offset: 0x00178858
	public static float GetRoundedTemperatureInKelvin(float kelvin)
	{
		float num = 0f;
		switch (GameUtil.temperatureUnit)
		{
		case GameUtil.TemperatureUnit.Celsius:
			num = GameUtil.GetTemperatureConvertedToKelvin(Mathf.Round(GameUtil.GetConvertedTemperature(Mathf.Round(kelvin), true)));
			break;
		case GameUtil.TemperatureUnit.Fahrenheit:
			num = GameUtil.GetTemperatureConvertedToKelvin((float)Mathf.RoundToInt(GameUtil.GetTemperatureConvertedFromKelvin(kelvin, GameUtil.TemperatureUnit.Fahrenheit)), GameUtil.TemperatureUnit.Fahrenheit);
			break;
		case GameUtil.TemperatureUnit.Kelvin:
			num = (float)Mathf.RoundToInt(kelvin);
			break;
		}
		return num;
	}

	// Token: 0x0600420B RID: 16907 RVA: 0x0017A6C0 File Offset: 0x001788C0
	public static void AppendFormattedTemperature(StringBuilder builder, float temp, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation interpretation = GameUtil.TemperatureInterpretation.Absolute, bool displayUnits = true, bool roundInDestinationFormat = false)
	{
		if (interpretation != GameUtil.TemperatureInterpretation.Absolute)
		{
			if (interpretation != GameUtil.TemperatureInterpretation.Relative)
			{
			}
			temp = GameUtil.GetConvertedTemperatureDelta(temp);
		}
		else
		{
			temp = GameUtil.GetConvertedTemperature(temp, roundInDestinationFormat);
		}
		temp = GameUtil.ApplyTimeSlice(temp, timeSlice);
		if (Mathf.Abs(temp) < 0.1f)
		{
			builder.AppendFormat("{0:##0.####}", temp);
		}
		else
		{
			builder.AppendFormat("{0:##0.#}", temp);
		}
		if (displayUnits)
		{
			builder.Append(GameUtil.GetTemperatureUnitSuffix());
		}
		GameUtil.AddTimeSliceText(builder, timeSlice);
	}

	// Token: 0x0600420C RID: 16908 RVA: 0x0017A73D File Offset: 0x0017893D
	public static string GetFormattedTemperature(float temp, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation interpretation = GameUtil.TemperatureInterpretation.Absolute, bool displayUnits = true, bool roundInDestinationFormat = false)
	{
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		GameUtil.AppendFormattedTemperature(stringBuilder, temp, timeSlice, interpretation, displayUnits, roundInDestinationFormat);
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}

	// Token: 0x0600420D RID: 16909 RVA: 0x0017A758 File Offset: 0x00178958
	public static void AppendFormattedCaloriesForItem(StringBuilder builder, Tag tag, float amount, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None, bool forceKcal = true)
	{
		EdiblesManager.FoodInfo foodInfo = EdiblesManager.GetFoodInfo(tag.Name);
		GameUtil.AppendFormattedCalories(builder, (foodInfo != null) ? (foodInfo.CaloriesPerUnit * amount) : (-1f), timeSlice, forceKcal);
	}

	// Token: 0x0600420E RID: 16910 RVA: 0x0017A78D File Offset: 0x0017898D
	public static string GetFormattedCaloriesForItem(Tag tag, float amount, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None, bool forceKcal = true)
	{
		return GameUtil.GetFormattedCaloriesForItem(tag, amount, true, timeSlice, forceKcal);
	}

	// Token: 0x0600420F RID: 16911 RVA: 0x0017A79C File Offset: 0x0017899C
	public static string GetFormattedCaloriesForItem(Tag tag, float amount, bool showSuffix, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None, bool forceKcal = true)
	{
		EdiblesManager.FoodInfo foodInfo = EdiblesManager.GetFoodInfo(tag.Name);
		return GameUtil.GetFormattedCalories((foodInfo != null) ? (foodInfo.CaloriesPerUnit * amount) : (-1f), showSuffix, timeSlice, forceKcal);
	}

	// Token: 0x06004210 RID: 16912 RVA: 0x0017A7D1 File Offset: 0x001789D1
	public static void AppendFormattedCalories(StringBuilder builder, float calories, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None, bool forceKcal = true)
	{
		GameUtil.AppendFormattedCalories(builder, calories, true, timeSlice, forceKcal);
	}

	// Token: 0x06004211 RID: 16913 RVA: 0x0017A7E0 File Offset: 0x001789E0
	public static void AppendFormattedCalories(StringBuilder builder, float calories, bool showSuffix, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None, bool forceKcal = true)
	{
		string text = UI.UNITSUFFIXES.CALORIES.CALORIE;
		if (Mathf.Abs(calories) >= 1000f || forceKcal)
		{
			calories /= 1000f;
			text = UI.UNITSUFFIXES.CALORIES.KILOCALORIE;
		}
		calories = GameUtil.ApplyTimeSlice(calories, timeSlice);
		GameUtil.AppendStandardFloat(builder, calories);
		if (showSuffix)
		{
			builder.Append(text);
			GameUtil.AddTimeSliceText(builder, timeSlice);
		}
	}

	// Token: 0x06004212 RID: 16914 RVA: 0x0017A843 File Offset: 0x00178A43
	public static string GetFormattedCalories(float calories, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None, bool forceKcal = true)
	{
		return GameUtil.GetFormattedCalories(calories, true, timeSlice, forceKcal);
	}

	// Token: 0x06004213 RID: 16915 RVA: 0x0017A84E File Offset: 0x00178A4E
	public static string GetFormattedCalories(float calories, bool showSuffix, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None, bool forceKcal = true)
	{
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		GameUtil.AppendFormattedCalories(stringBuilder, calories, showSuffix, timeSlice, forceKcal);
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}

	// Token: 0x06004214 RID: 16916 RVA: 0x0017A864 File Offset: 0x00178A64
	public static string GetFormattedPreyConsumptionValuePerCycle(Tag preyTag, float crittersPerSecond, bool perCycle = true)
	{
		Assets.GetPrefab(preyTag).GetComponent<PrimaryElement>();
		return GameUtil.GetFormattedUnits(crittersPerSecond, GameUtil.TimeSlice.PerCycle, true, "");
	}

	// Token: 0x06004215 RID: 16917 RVA: 0x0017A880 File Offset: 0x00178A80
	public static string GetFormattedDirectPlantConsumptionValuePerCycle(Tag plantTag, float consumer_caloriesLossPerCaloriesPerKG, bool perCycle = true)
	{
		IPlantConsumptionInstructions[] plantConsumptionInstructions = GameUtil.GetPlantConsumptionInstructions(Assets.GetPrefab(plantTag));
		if (plantConsumptionInstructions == null || plantConsumptionInstructions.Length == 0)
		{
			return "Error";
		}
		foreach (IPlantConsumptionInstructions plantConsumptionInstructions2 in plantConsumptionInstructions)
		{
			if (plantConsumptionInstructions2.GetDietFoodType() == Diet.Info.FoodType.EatPlantDirectly)
			{
				return plantConsumptionInstructions2.GetFormattedConsumptionPerCycle(consumer_caloriesLossPerCaloriesPerKG);
			}
		}
		return "Error";
	}

	// Token: 0x06004216 RID: 16918 RVA: 0x0017A8D0 File Offset: 0x00178AD0
	public static string GetFormattedBranchGrowerPlantProductionValuePerCycle(Tag productTag, float outputAmountPerBranch, int branchCount, bool perCycle = true)
	{
		return GameUtil.SafeStringFormat(UI.BUILDINGEFFECTS.TOOLTIPS.BRANCH_GROWER_PLANT_POTENTIAL_OUTPUT, new object[]
		{
			GameUtil.GetFormattedByTag(productTag, outputAmountPerBranch, false, GameUtil.TimeSlice.PerCycle),
			GameUtil.GetFormattedByTag(productTag, outputAmountPerBranch * (float)branchCount, GameUtil.TimeSlice.PerCycle)
		});
	}

	// Token: 0x06004217 RID: 16919 RVA: 0x0017A904 File Offset: 0x00178B04
	public static string GetFormattedPlantStorageConsumptionValuePerCycle(Tag plantTag, float consumer_caloriesLossPerCaloriesPerKG, bool perCycle = true)
	{
		IPlantConsumptionInstructions[] plantConsumptionInstructions = GameUtil.GetPlantConsumptionInstructions(Assets.GetPrefab(plantTag));
		if (plantConsumptionInstructions == null || plantConsumptionInstructions.Length == 0)
		{
			return "Error";
		}
		foreach (IPlantConsumptionInstructions plantConsumptionInstructions2 in plantConsumptionInstructions)
		{
			if (plantConsumptionInstructions2.GetDietFoodType() == Diet.Info.FoodType.EatPlantStorage)
			{
				return plantConsumptionInstructions2.GetFormattedConsumptionPerCycle(consumer_caloriesLossPerCaloriesPerKG);
			}
		}
		return "Error";
	}

	// Token: 0x06004218 RID: 16920 RVA: 0x0017A954 File Offset: 0x00178B54
	public static IPlantConsumptionInstructions[] GetPlantConsumptionInstructions(GameObject prefab)
	{
		IPlantConsumptionInstructions[] components = prefab.GetComponents<IPlantConsumptionInstructions>();
		List<IPlantConsumptionInstructions> allSMI = prefab.GetAllSMI<IPlantConsumptionInstructions>();
		List<IPlantConsumptionInstructions> list = new List<IPlantConsumptionInstructions>();
		if (components != null)
		{
			list.AddRange(components);
		}
		if (allSMI != null)
		{
			list.AddRange(allSMI);
		}
		return list.ToArray();
	}

	// Token: 0x06004219 RID: 16921 RVA: 0x0017A990 File Offset: 0x00178B90
	public static void AppendFormattedPlantGrowth(StringBuilder builder, float percent, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None)
	{
		percent = GameUtil.ApplyTimeSlice(percent, timeSlice);
		GameUtil.AppendStandardPercentageFloat(builder, percent, true);
		builder.Append(UI.UNITSUFFIXES.PERCENT);
		builder.Append(" ");
		builder.Append(UI.UNITSUFFIXES.GROWTH);
		GameUtil.AddTimeSliceText(builder, timeSlice);
	}

	// Token: 0x0600421A RID: 16922 RVA: 0x0017A9E3 File Offset: 0x00178BE3
	public static string GetFormattedPlantGrowth(float percent, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None)
	{
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		GameUtil.AppendFormattedPlantGrowth(stringBuilder, percent, timeSlice);
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}

	// Token: 0x0600421B RID: 16923 RVA: 0x0017A9F7 File Offset: 0x00178BF7
	public static void AppendFormattedPercent(StringBuilder builder, float percent, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None)
	{
		GameUtil.AppendStandardPercentageFloat(builder, GameUtil.ApplyTimeSlice(percent, timeSlice), false);
		builder.Append(UI.UNITSUFFIXES.PERCENT);
		GameUtil.AddTimeSliceText(builder, timeSlice);
	}

	// Token: 0x0600421C RID: 16924 RVA: 0x0017AA1F File Offset: 0x00178C1F
	public static string GetFormattedPercent(float percent, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None)
	{
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		GameUtil.AppendFormattedPercent(stringBuilder, percent, timeSlice);
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}

	// Token: 0x0600421D RID: 16925 RVA: 0x0017AA34 File Offset: 0x00178C34
	public static void AppendFormattedRoundedJoules(StringBuilder builder, float joules)
	{
		if (Mathf.Abs(joules) > 1000f)
		{
			builder.AppendFormat("{0:F1}", joules / 1000f);
			builder.Append(UI.UNITSUFFIXES.ELECTRICAL.KILOJOULE);
			return;
		}
		builder.AppendFormat("{0:F1}", joules);
		builder.Append(UI.UNITSUFFIXES.ELECTRICAL.JOULE);
	}

	// Token: 0x0600421E RID: 16926 RVA: 0x0017AA9B File Offset: 0x00178C9B
	public static string GetFormattedRoundedJoules(float joules)
	{
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		GameUtil.AppendFormattedRoundedJoules(stringBuilder, joules);
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}

	// Token: 0x0600421F RID: 16927 RVA: 0x0017AAB0 File Offset: 0x00178CB0
	public static string GetFormattedJoules(float joules, string floatFormat = "F1", GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None)
	{
		if (timeSlice == GameUtil.TimeSlice.PerSecond)
		{
			return GameUtil.GetFormattedWattage(joules, GameUtil.WattageFormatterUnit.Automatic, true);
		}
		joules = GameUtil.ApplyTimeSlice(joules, timeSlice);
		string text;
		if (Math.Abs(joules) > 1000000f)
		{
			text = GameUtil.FloatToString(joules / 1000000f, floatFormat) + UI.UNITSUFFIXES.ELECTRICAL.MEGAJOULE;
		}
		else if (Mathf.Abs(joules) > 1000f)
		{
			text = GameUtil.FloatToString(joules / 1000f, floatFormat) + UI.UNITSUFFIXES.ELECTRICAL.KILOJOULE;
		}
		else
		{
			text = GameUtil.FloatToString(joules, floatFormat) + UI.UNITSUFFIXES.ELECTRICAL.JOULE;
		}
		return GameUtil.AddTimeSliceText(text, timeSlice);
	}

	// Token: 0x06004220 RID: 16928 RVA: 0x0017AB49 File Offset: 0x00178D49
	public static void AppendFormattedRads(StringBuilder builder, float rads, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None)
	{
		rads = GameUtil.ApplyTimeSlice(rads, timeSlice);
		GameUtil.AppendStandardFloat(builder, rads);
		builder.Append(UI.UNITSUFFIXES.RADIATION.RADS);
		GameUtil.AddTimeSliceText(builder, timeSlice);
	}

	// Token: 0x06004221 RID: 16929 RVA: 0x0017AB73 File Offset: 0x00178D73
	public static string GetFormattedRads(float rads, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None)
	{
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		GameUtil.AppendFormattedRads(stringBuilder, rads, timeSlice);
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}

	// Token: 0x06004222 RID: 16930 RVA: 0x0017AB87 File Offset: 0x00178D87
	public static void AppendFormattedHighEnergyParticles(StringBuilder builder, float units, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None, bool displayUnits = true)
	{
		GameUtil.AppendFloatWithDecimalPoint(builder, units);
		if (displayUnits)
		{
			builder.Append((units == 1f) ? UI.UNITSUFFIXES.HIGHENERGYPARTICLES.PARTRICLE : UI.UNITSUFFIXES.HIGHENERGYPARTICLES.PARTRICLES);
		}
		GameUtil.AddTimeSliceText(builder, timeSlice);
	}

	// Token: 0x06004223 RID: 16931 RVA: 0x0017ABBA File Offset: 0x00178DBA
	public static string GetFormattedHighEnergyParticles(float units, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None, bool displayUnits = true)
	{
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		GameUtil.AppendFormattedHighEnergyParticles(stringBuilder, units, timeSlice, displayUnits);
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}

	// Token: 0x06004224 RID: 16932 RVA: 0x0017ABD0 File Offset: 0x00178DD0
	public static void AppendFormattedWattage(StringBuilder builder, float watts, GameUtil.WattageFormatterUnit unit = GameUtil.WattageFormatterUnit.Automatic, bool displayUnits = true)
	{
		string text = null;
		switch (unit)
		{
		case GameUtil.WattageFormatterUnit.Watts:
			text = UI.UNITSUFFIXES.ELECTRICAL.WATT;
			break;
		case GameUtil.WattageFormatterUnit.Kilowatts:
			watts /= 1000f;
			text = UI.UNITSUFFIXES.ELECTRICAL.KILOWATT;
			break;
		case GameUtil.WattageFormatterUnit.Automatic:
			if (Mathf.Abs(watts) > 1000f)
			{
				watts /= 1000f;
				text = UI.UNITSUFFIXES.ELECTRICAL.KILOWATT;
			}
			else
			{
				text = UI.UNITSUFFIXES.ELECTRICAL.WATT;
			}
			break;
		}
		GameUtil.AppendFloatToString(builder, watts, "###0.##");
		if (displayUnits && text != null)
		{
			builder.Append(text);
		}
	}

	// Token: 0x06004225 RID: 16933 RVA: 0x0017AC5E File Offset: 0x00178E5E
	public static string GetFormattedWattage(float watts, GameUtil.WattageFormatterUnit unit = GameUtil.WattageFormatterUnit.Automatic, bool displayUnits = true)
	{
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		GameUtil.AppendFormattedWattage(stringBuilder, watts, unit, displayUnits);
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}

	// Token: 0x06004226 RID: 16934 RVA: 0x0017AC74 File Offset: 0x00178E74
	public static void AppendFormattedHeatEnergy(StringBuilder builder, float dtu, GameUtil.HeatEnergyFormatterUnit unit = GameUtil.HeatEnergyFormatterUnit.Automatic)
	{
		string text;
		string text2;
		switch (unit)
		{
		case GameUtil.HeatEnergyFormatterUnit.DTU_S:
			text = UI.UNITSUFFIXES.HEAT.DTU;
			text2 = "###0.";
			break;
		case GameUtil.HeatEnergyFormatterUnit.KDTU_S:
			dtu /= 1000f;
			text = UI.UNITSUFFIXES.HEAT.KDTU;
			text2 = "###0.##";
			break;
		default:
			if (Mathf.Abs(dtu) > 1000f)
			{
				dtu /= 1000f;
				text = UI.UNITSUFFIXES.HEAT.KDTU;
				text2 = "###0.##";
			}
			else
			{
				text = UI.UNITSUFFIXES.HEAT.DTU;
				text2 = "###0.";
			}
			break;
		}
		GameUtil.AppendFloatToString(builder, dtu, text2);
		builder.Append(text);
	}

	// Token: 0x06004227 RID: 16935 RVA: 0x0017AD0E File Offset: 0x00178F0E
	public static string GetFormattedHeatEnergy(float dtu, GameUtil.HeatEnergyFormatterUnit unit = GameUtil.HeatEnergyFormatterUnit.Automatic)
	{
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		GameUtil.AppendFormattedHeatEnergy(stringBuilder, dtu, unit);
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}

	// Token: 0x06004228 RID: 16936 RVA: 0x0017AD24 File Offset: 0x00178F24
	public static void AppendFormattedHeatEnergyRate(StringBuilder builder, float dtu_s, GameUtil.HeatEnergyFormatterUnit unit = GameUtil.HeatEnergyFormatterUnit.Automatic)
	{
		string text = null;
		switch (unit)
		{
		case GameUtil.HeatEnergyFormatterUnit.DTU_S:
			text = UI.UNITSUFFIXES.HEAT.DTU_S;
			break;
		case GameUtil.HeatEnergyFormatterUnit.KDTU_S:
			dtu_s /= 1000f;
			text = UI.UNITSUFFIXES.HEAT.KDTU_S;
			break;
		case GameUtil.HeatEnergyFormatterUnit.Automatic:
			if (Mathf.Abs(dtu_s) > 1000f)
			{
				dtu_s /= 1000f;
				text = UI.UNITSUFFIXES.HEAT.KDTU_S;
			}
			else
			{
				text = UI.UNITSUFFIXES.HEAT.DTU_S;
			}
			break;
		}
		GameUtil.AppendFloatToString(builder, dtu_s, null);
		if (text != null)
		{
			builder.Append(text);
		}
	}

	// Token: 0x06004229 RID: 16937 RVA: 0x0017ADAB File Offset: 0x00178FAB
	public static string GetFormattedHeatEnergyRate(float dtu_s, GameUtil.HeatEnergyFormatterUnit unit = GameUtil.HeatEnergyFormatterUnit.Automatic)
	{
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		GameUtil.AppendFormattedHeatEnergyRate(stringBuilder, dtu_s, unit);
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}

	// Token: 0x0600422A RID: 16938 RVA: 0x0017ADBF File Offset: 0x00178FBF
	public static string GetFormattedInt(float num, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None)
	{
		num = GameUtil.ApplyTimeSlice(num, timeSlice);
		return GameUtil.AddTimeSliceText(GameUtil.FloatToString(num, "F0"), timeSlice);
	}

	// Token: 0x0600422B RID: 16939 RVA: 0x0017ADDC File Offset: 0x00178FDC
	public static string GetSpeciesNameFromGameObject(GameObject critterGameObject)
	{
		CreatureBrain component = critterGameObject.GetComponent<CreatureBrain>();
		if (component != null)
		{
			return GameUtil.GetNameForSpecies(component.species);
		}
		return "UNKNOWN SPECIES";
	}

	// Token: 0x0600422C RID: 16940 RVA: 0x0017AE0C File Offset: 0x0017900C
	public static string GetNameForSpecies(Tag species)
	{
		Option<string> option = Option.None;
		if (species == GameTags.Creatures.Species.HatchSpecies)
		{
			option = Option.Some<string>(global::STRINGS.CREATURES.FAMILY_PLURAL.HATCHSPECIES);
		}
		else if (species == GameTags.Creatures.Species.LightBugSpecies)
		{
			option = Option.Some<string>(global::STRINGS.CREATURES.FAMILY_PLURAL.LIGHTBUGSPECIES);
		}
		else if (species == GameTags.Creatures.Species.OilFloaterSpecies)
		{
			option = Option.Some<string>(global::STRINGS.CREATURES.FAMILY_PLURAL.OILFLOATERSPECIES);
		}
		else if (species == GameTags.Creatures.Species.DreckoSpecies)
		{
			option = Option.Some<string>(global::STRINGS.CREATURES.FAMILY_PLURAL.DRECKOSPECIES);
		}
		else if (species == GameTags.Creatures.Species.GlomSpecies)
		{
			option = Option.Some<string>(global::STRINGS.CREATURES.FAMILY_PLURAL.GLOMSPECIES);
		}
		else if (species == GameTags.Creatures.Species.PuftSpecies)
		{
			option = Option.Some<string>(global::STRINGS.CREATURES.FAMILY_PLURAL.PUFTSPECIES);
		}
		else if (species == GameTags.Creatures.Species.PacuSpecies)
		{
			option = Option.Some<string>(global::STRINGS.CREATURES.FAMILY_PLURAL.PACUSPECIES);
		}
		else if (species == GameTags.Creatures.Species.MooSpecies)
		{
			option = Option.Some<string>(global::STRINGS.CREATURES.FAMILY_PLURAL.MOOSPECIES);
		}
		else if (species == GameTags.Creatures.Species.MoleSpecies)
		{
			option = Option.Some<string>(global::STRINGS.CREATURES.FAMILY_PLURAL.MOLESPECIES);
		}
		else if (species == GameTags.Creatures.Species.SquirrelSpecies)
		{
			option = Option.Some<string>(global::STRINGS.CREATURES.FAMILY_PLURAL.SQUIRRELSPECIES);
		}
		else if (species == GameTags.Creatures.Species.CrabSpecies)
		{
			option = Option.Some<string>(global::STRINGS.CREATURES.FAMILY_PLURAL.CRABSPECIES);
		}
		else if (species == GameTags.Creatures.Species.DivergentSpecies)
		{
			option = Option.Some<string>(global::STRINGS.CREATURES.FAMILY_PLURAL.DIVERGENTSPECIES);
		}
		else if (species == GameTags.Creatures.Species.StaterpillarSpecies)
		{
			option = Option.Some<string>(global::STRINGS.CREATURES.FAMILY_PLURAL.STATERPILLARSPECIES);
		}
		else if (species == GameTags.Creatures.Species.BeetaSpecies)
		{
			option = Option.Some<string>(global::STRINGS.CREATURES.FAMILY_PLURAL.BEETASPECIES);
		}
		else if (species == GameTags.Creatures.Species.BellySpecies)
		{
			option = Option.Some<string>(global::STRINGS.CREATURES.FAMILY_PLURAL.BELLYSPECIES);
		}
		else if (species == GameTags.Creatures.Species.SealSpecies)
		{
			option = Option.Some<string>(global::STRINGS.CREATURES.FAMILY_PLURAL.SEALSPECIES);
		}
		else if (species == GameTags.Creatures.Species.DeerSpecies)
		{
			option = Option.Some<string>(global::STRINGS.CREATURES.FAMILY_PLURAL.DEERSPECIES);
		}
		else
		{
			option = Option.None;
		}
		return option.Value;
	}

	// Token: 0x0600422D RID: 16941 RVA: 0x0017B06C File Offset: 0x0017926C
	public static void AppendFormattedSimple(StringBuilder builder, float num, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None, string formatString = null)
	{
		num = GameUtil.ApplyTimeSlice(num, timeSlice);
		if (formatString != null)
		{
			GameUtil.AppendFloatToString(builder, num, formatString);
		}
		else if (num == 0f)
		{
			builder.Append("0");
		}
		else if (Mathf.Abs(num) < 1f)
		{
			GameUtil.AppendFloatToString(builder, num, "#,##0.##");
		}
		else if (Mathf.Abs(num) < 10f)
		{
			GameUtil.AppendFloatToString(builder, num, "#,###.##");
		}
		else
		{
			GameUtil.AppendFloatToString(builder, num, "#,###.##");
		}
		GameUtil.AddTimeSliceText(builder, timeSlice);
	}

	// Token: 0x0600422E RID: 16942 RVA: 0x0017B0EE File Offset: 0x001792EE
	public static string GetFormattedSimple(float num, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None, string formatString = null)
	{
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		GameUtil.AppendFormattedSimple(stringBuilder, num, timeSlice, formatString);
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}

	// Token: 0x0600422F RID: 16943 RVA: 0x0017B103 File Offset: 0x00179303
	public static void AppendFormattedLux(StringBuilder builder, int lux)
	{
		builder.Append(lux);
		builder.Append(UI.UNITSUFFIXES.LIGHT.LUX);
	}

	// Token: 0x06004230 RID: 16944 RVA: 0x0017B11E File Offset: 0x0017931E
	public static string GetFormattedLux(int lux)
	{
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		GameUtil.AppendFormattedLux(stringBuilder, lux);
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}

	// Token: 0x06004231 RID: 16945 RVA: 0x0017B134 File Offset: 0x00179334
	public static string GetLightDescription(int lux)
	{
		if (lux == 0)
		{
			return UI.OVERLAYS.LIGHTING.RANGES.NO_LIGHT;
		}
		if (lux < DUPLICANTSTATS.STANDARD.Light.LOW_LIGHT)
		{
			return UI.OVERLAYS.LIGHTING.RANGES.VERY_LOW_LIGHT;
		}
		if (lux < DUPLICANTSTATS.STANDARD.Light.MEDIUM_LIGHT)
		{
			return UI.OVERLAYS.LIGHTING.RANGES.LOW_LIGHT;
		}
		if (lux < DUPLICANTSTATS.STANDARD.Light.HIGH_LIGHT)
		{
			return UI.OVERLAYS.LIGHTING.RANGES.MEDIUM_LIGHT;
		}
		if (lux < DUPLICANTSTATS.STANDARD.Light.VERY_HIGH_LIGHT)
		{
			return UI.OVERLAYS.LIGHTING.RANGES.HIGH_LIGHT;
		}
		if (lux < DUPLICANTSTATS.STANDARD.Light.MAX_LIGHT)
		{
			return UI.OVERLAYS.LIGHTING.RANGES.VERY_HIGH_LIGHT;
		}
		return UI.OVERLAYS.LIGHTING.RANGES.MAX_LIGHT;
	}

	// Token: 0x06004232 RID: 16946 RVA: 0x0017B1EC File Offset: 0x001793EC
	public static string GetRadiationDescription(float radsPerCycle)
	{
		if (radsPerCycle == 0f)
		{
			return UI.OVERLAYS.RADIATION.RANGES.NONE;
		}
		if (radsPerCycle < 100f)
		{
			return UI.OVERLAYS.RADIATION.RANGES.VERY_LOW;
		}
		if (radsPerCycle < 200f)
		{
			return UI.OVERLAYS.RADIATION.RANGES.LOW;
		}
		if (radsPerCycle < 400f)
		{
			return UI.OVERLAYS.RADIATION.RANGES.MEDIUM;
		}
		if (radsPerCycle < 2000f)
		{
			return UI.OVERLAYS.RADIATION.RANGES.HIGH;
		}
		if (radsPerCycle < 4000f)
		{
			return UI.OVERLAYS.RADIATION.RANGES.VERY_HIGH;
		}
		return UI.OVERLAYS.RADIATION.RANGES.MAX;
	}

	// Token: 0x06004233 RID: 16947 RVA: 0x0017B278 File Offset: 0x00179478
	public static void AppendFormattedByTag(StringBuilder builder, Tag tag, float amount, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None)
	{
		if (GameTags.DisplayAsCalories.Contains(tag))
		{
			GameUtil.AppendFormattedCaloriesForItem(builder, tag, amount, timeSlice, true);
			return;
		}
		if (GameTags.DisplayAsUnits.Contains(tag))
		{
			GameUtil.AppendFormattedUnits(builder, amount, timeSlice, true, "");
			return;
		}
		GameUtil.AppendFormattedMass(builder, amount, timeSlice, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}");
	}

	// Token: 0x06004234 RID: 16948 RVA: 0x0017B2C8 File Offset: 0x001794C8
	public static string GetFormattedByTag(Tag tag, float amount, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None)
	{
		return GameUtil.GetFormattedByTag(tag, amount, true, timeSlice);
	}

	// Token: 0x06004235 RID: 16949 RVA: 0x0017B2D4 File Offset: 0x001794D4
	public static string GetFormattedByTag(Tag tag, float amount, bool showSuffix, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None)
	{
		if (GameTags.DisplayAsCalories.Contains(tag))
		{
			return GameUtil.GetFormattedCaloriesForItem(tag, amount, showSuffix, timeSlice, true);
		}
		if (GameTags.DisplayAsUnits.Contains(tag))
		{
			return GameUtil.GetFormattedUnits(amount, timeSlice, showSuffix, "");
		}
		return GameUtil.GetFormattedMass(amount, timeSlice, GameUtil.MetricMassFormat.UseThreshold, showSuffix, "{0:0.#}");
	}

	// Token: 0x06004236 RID: 16950 RVA: 0x0017B324 File Offset: 0x00179524
	public static string GetFormattedFoodQuality(int quality)
	{
		if (GameUtil.adjectives == null)
		{
			GameUtil.adjectives = LocString.GetStrings(typeof(DUPLICANTS.NEEDS.FOOD_QUALITY.ADJECTIVES));
		}
		LocString locString = ((quality >= 0) ? DUPLICANTS.NEEDS.FOOD_QUALITY.ADJECTIVE_FORMAT_POSITIVE : DUPLICANTS.NEEDS.FOOD_QUALITY.ADJECTIVE_FORMAT_NEGATIVE);
		int num = quality - DUPLICANTS.NEEDS.FOOD_QUALITY.ADJECTIVE_INDEX_OFFSET;
		num = Mathf.Clamp(num, 0, GameUtil.adjectives.Length);
		return string.Format(locString, GameUtil.adjectives[num], GameUtil.AddPositiveSign(quality.ToString(), quality > 0));
	}

	// Token: 0x06004237 RID: 16951 RVA: 0x0017B394 File Offset: 0x00179594
	public static string GetFormattedBytes(ulong amount)
	{
		string[] array = new string[]
		{
			UI.UNITSUFFIXES.INFORMATION.BYTE,
			UI.UNITSUFFIXES.INFORMATION.KILOBYTE,
			UI.UNITSUFFIXES.INFORMATION.MEGABYTE,
			UI.UNITSUFFIXES.INFORMATION.GIGABYTE,
			UI.UNITSUFFIXES.INFORMATION.TERABYTE
		};
		int num = ((amount == 0UL) ? 0 : ((int)Math.Floor(Math.Floor(Math.Log(amount)) / Math.Log(1024.0))));
		double num2 = amount / Math.Pow(1024.0, (double)num);
		global::Debug.Assert(num >= 0 && num < array.Length);
		return string.Format("{0:F} {1}", num2, array[num]);
	}

	// Token: 0x06004238 RID: 16952 RVA: 0x0017B44C File Offset: 0x0017964C
	public static string GetFormattedInfomation(float amount, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None)
	{
		amount = GameUtil.ApplyTimeSlice(amount, timeSlice);
		string text = "";
		if (amount < 1024f)
		{
			text = UI.UNITSUFFIXES.INFORMATION.KILOBYTE;
		}
		else if (amount < 1048576f)
		{
			amount /= 1000f;
			text = UI.UNITSUFFIXES.INFORMATION.MEGABYTE;
		}
		else if (amount < 1.0737418E+09f)
		{
			amount /= 1048576f;
			text = UI.UNITSUFFIXES.INFORMATION.GIGABYTE;
		}
		return GameUtil.AddTimeSliceText(amount.ToString() + text, timeSlice);
	}

	// Token: 0x06004239 RID: 16953 RVA: 0x0017B4CC File Offset: 0x001796CC
	public static LocString GetCurrentMassUnit(bool useSmallUnit = false)
	{
		LocString locString = null;
		GameUtil.MassUnit massUnit = GameUtil.massUnit;
		if (massUnit != GameUtil.MassUnit.Kilograms)
		{
			if (massUnit == GameUtil.MassUnit.Pounds)
			{
				locString = UI.UNITSUFFIXES.MASS.POUND;
			}
		}
		else if (useSmallUnit)
		{
			locString = UI.UNITSUFFIXES.MASS.GRAM;
		}
		else
		{
			locString = UI.UNITSUFFIXES.MASS.KILOGRAM;
		}
		return locString;
	}

	// Token: 0x0600423A RID: 16954 RVA: 0x0017B504 File Offset: 0x00179704
	public static void AppendFormattedMass(StringBuilder builder, float mass, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None, GameUtil.MetricMassFormat massFormat = GameUtil.MetricMassFormat.UseThreshold, bool includeSuffix = true, string floatFormat = "{0:0.#}")
	{
		if (mass == -3.4028235E+38f)
		{
			builder.Append(UI.CALCULATING);
			return;
		}
		if (float.IsPositiveInfinity(mass))
		{
			builder.Append(UI.POS_INFINITY);
			builder.Append(UI.UNITSUFFIXES.MASS.TONNE);
			return;
		}
		if (float.IsNegativeInfinity(mass))
		{
			builder.Append(UI.NEG_INFINITY);
			builder.Append(UI.UNITSUFFIXES.MASS.TONNE);
			return;
		}
		mass = GameUtil.ApplyTimeSlice(mass, timeSlice);
		string text;
		if (GameUtil.massUnit == GameUtil.MassUnit.Kilograms)
		{
			text = UI.UNITSUFFIXES.MASS.TONNE;
			if (massFormat == GameUtil.MetricMassFormat.UseThreshold)
			{
				float num = Mathf.Abs(mass);
				if (0f < num)
				{
					if (num < 5E-06f)
					{
						text = UI.UNITSUFFIXES.MASS.MICROGRAM;
						mass = Mathf.Floor(mass * 1E+09f);
					}
					else if (num < 0.005f)
					{
						mass *= 1000000f;
						text = UI.UNITSUFFIXES.MASS.MILLIGRAM;
					}
					else if (Mathf.Abs(mass) < 5f)
					{
						mass *= 1000f;
						text = UI.UNITSUFFIXES.MASS.GRAM;
					}
					else if (Mathf.Abs(mass) < 5000f)
					{
						text = UI.UNITSUFFIXES.MASS.KILOGRAM;
					}
					else
					{
						mass /= 1000f;
						text = UI.UNITSUFFIXES.MASS.TONNE;
					}
				}
				else
				{
					text = UI.UNITSUFFIXES.MASS.KILOGRAM;
				}
			}
			else if (massFormat == GameUtil.MetricMassFormat.Kilogram)
			{
				text = UI.UNITSUFFIXES.MASS.KILOGRAM;
			}
			else if (massFormat == GameUtil.MetricMassFormat.Gram)
			{
				mass *= 1000f;
				text = UI.UNITSUFFIXES.MASS.GRAM;
			}
			else if (massFormat == GameUtil.MetricMassFormat.Tonne)
			{
				mass /= 1000f;
				text = UI.UNITSUFFIXES.MASS.TONNE;
			}
		}
		else
		{
			mass /= 2.2f;
			text = UI.UNITSUFFIXES.MASS.POUND;
			if (massFormat == GameUtil.MetricMassFormat.UseThreshold)
			{
				float num2 = Mathf.Abs(mass);
				if (num2 < 5f && num2 > 0.001f)
				{
					mass *= 256f;
					text = UI.UNITSUFFIXES.MASS.DRACHMA;
				}
				else
				{
					mass *= 7000f;
					text = UI.UNITSUFFIXES.MASS.GRAIN;
				}
			}
		}
		builder.AppendFormat(floatFormat, mass);
		if (includeSuffix)
		{
			builder.Append(text);
			GameUtil.AddTimeSliceText(builder, timeSlice);
		}
	}

	// Token: 0x0600423B RID: 16955 RVA: 0x0017B72C File Offset: 0x0017992C
	public static string GetFormattedMass(float mass, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None, GameUtil.MetricMassFormat massFormat = GameUtil.MetricMassFormat.UseThreshold, bool includeSuffix = true, string floatFormat = "{0:0.#}")
	{
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		GameUtil.AppendFormattedMass(stringBuilder, mass, timeSlice, massFormat, includeSuffix, floatFormat);
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}

	// Token: 0x0600423C RID: 16956 RVA: 0x0017B744 File Offset: 0x00179944
	public static void AppendFormattedTime(StringBuilder builder, float seconds)
	{
		builder.AppendFormat(UI.FORMATSECONDS, (int)seconds);
	}

	// Token: 0x0600423D RID: 16957 RVA: 0x0017B75E File Offset: 0x0017995E
	public static string GetFormattedTime(float seconds, string floatFormat = "F0")
	{
		return string.Format(UI.FORMATSECONDS, seconds.ToString(floatFormat));
	}

	// Token: 0x0600423E RID: 16958 RVA: 0x0017B777 File Offset: 0x00179977
	public static void AppendFormattedEngineEfficiency(StringBuilder builder, float amount)
	{
		builder.Append(amount);
		builder.Append(" km /");
		builder.Append(UI.UNITSUFFIXES.MASS.KILOGRAM);
	}

	// Token: 0x0600423F RID: 16959 RVA: 0x0017B79E File Offset: 0x0017999E
	public static string GetFormattedEngineEfficiency(float amount)
	{
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		GameUtil.AppendFormattedEngineEfficiency(stringBuilder, amount);
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}

	// Token: 0x06004240 RID: 16960 RVA: 0x0017B7B4 File Offset: 0x001799B4
	public static void AppendFormattedDistance(StringBuilder builder, float meters)
	{
		if (Mathf.Abs(meters) < 1f)
		{
			builder.AppendFormat("{0:0.0} cm", Math.Abs(meters * 100f));
			return;
		}
		if (meters < 1000f)
		{
			builder.Append(meters);
			builder.Append(" m");
			return;
		}
		builder.AppendFormat("{0:0.0} km", meters / 1000f);
	}

	// Token: 0x06004241 RID: 16961 RVA: 0x0017B821 File Offset: 0x00179A21
	public static string GetFormattedDistance(float meters)
	{
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		GameUtil.AppendFormattedDistance(stringBuilder, meters);
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}

	// Token: 0x06004242 RID: 16962 RVA: 0x0017B834 File Offset: 0x00179A34
	public static void AppendFormattedCycles(StringBuilder builder, float seconds, bool forceCycles = false)
	{
		if (forceCycles || Math.Abs(seconds) > 100f)
		{
			builder.AppendFormat(UI.FORMATDAY, seconds / 600f);
			return;
		}
		GameUtil.AppendFormattedTime(builder, seconds);
	}

	// Token: 0x06004243 RID: 16963 RVA: 0x0017B86B File Offset: 0x00179A6B
	public static string GetFormattedCycles(float seconds, string formatString = "F1", bool forceCycles = false)
	{
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		GameUtil.AppendFormattedCycles(stringBuilder, seconds, forceCycles);
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}

	// Token: 0x06004244 RID: 16964 RVA: 0x0017B87F File Offset: 0x00179A7F
	public static float GetDisplaySHC(float shc)
	{
		if (GameUtil.temperatureUnit == GameUtil.TemperatureUnit.Fahrenheit)
		{
			shc /= 1.8f;
		}
		return shc;
	}

	// Token: 0x06004245 RID: 16965 RVA: 0x0017B893 File Offset: 0x00179A93
	public static string GetSHCSuffix()
	{
		return string.Format("(DTU/g)/{0}", GameUtil.GetTemperatureUnitSuffix());
	}

	// Token: 0x06004246 RID: 16966 RVA: 0x0017B8A4 File Offset: 0x00179AA4
	public static string GetFormattedSHC(float shc)
	{
		shc = GameUtil.GetDisplaySHC(shc);
		return string.Format("{0} (DTU/g)/{1}", shc.ToString("0.000"), GameUtil.GetTemperatureUnitSuffix());
	}

	// Token: 0x06004247 RID: 16967 RVA: 0x0017B8C9 File Offset: 0x00179AC9
	public static float GetDisplayThermalConductivity(float tc)
	{
		if (GameUtil.temperatureUnit == GameUtil.TemperatureUnit.Fahrenheit)
		{
			tc /= 1.8f;
		}
		return tc;
	}

	// Token: 0x06004248 RID: 16968 RVA: 0x0017B8DD File Offset: 0x00179ADD
	public static string GetThermalConductivitySuffix()
	{
		return string.Format("(DTU/(m*s))/{0}", GameUtil.GetTemperatureUnitSuffix());
	}

	// Token: 0x06004249 RID: 16969 RVA: 0x0017B8EE File Offset: 0x00179AEE
	public static string GetFormattedThermalConductivity(float tc)
	{
		tc = GameUtil.GetDisplayThermalConductivity(tc);
		return string.Format("{0} (DTU/(m*s))/{1}", tc.ToString("0.000"), GameUtil.GetTemperatureUnitSuffix());
	}

	// Token: 0x0600424A RID: 16970 RVA: 0x0017B913 File Offset: 0x00179B13
	public static string GetElementNameByElementHash(SimHashes elementHash)
	{
		return ElementLoader.FindElementByHash(elementHash).tag.ProperName();
	}

	// Token: 0x0600424B RID: 16971 RVA: 0x0017B928 File Offset: 0x00179B28
	public static string SafeStringFormat(string source, params object[] args)
	{
		for (int i = 0; i < args.Length; i++)
		{
			string text = "{" + i.ToString() + "}";
			if (!source.Contains(text))
			{
				KCrashReporter.ReportDevNotification(string.Format("Format error in string: \"{0}\". Source is missing the {{{1}}} format marker for argument \"{2}\" insertion.", source, i, args[i]), Environment.StackTrace, "", false, null);
			}
			else
			{
				source = source.Replace(text, args[i].ToString());
			}
		}
		return source;
	}

	// Token: 0x0600424C RID: 16972 RVA: 0x0017B99C File Offset: 0x00179B9C
	public static bool HasTrait(GameObject go, string traitName)
	{
		Traits component = go.GetComponent<Traits>();
		return !(component == null) && component.HasTrait(traitName);
	}

	// Token: 0x0600424D RID: 16973 RVA: 0x0017B9C4 File Offset: 0x00179BC4
	public static HashSet<int> GetFloodFillCavity(int startCell, bool allowLiquid)
	{
		HashSet<int> hashSet = new HashSet<int>();
		if (allowLiquid)
		{
			hashSet = GameUtil.FloodCollectCells(startCell, (int cell) => !Grid.Solid[cell], 300, null, true);
		}
		else
		{
			hashSet = GameUtil.FloodCollectCells(startCell, (int cell) => Grid.Element[cell].IsVacuum || Grid.Element[cell].IsGas, 300, null, true);
		}
		return hashSet;
	}

	// Token: 0x0600424E RID: 16974 RVA: 0x0017BA38 File Offset: 0x00179C38
	public static float GetRadiationAbsorptionPercentage(int cell)
	{
		if (Grid.IsValidCell(cell))
		{
			return GameUtil.GetRadiationAbsorptionPercentage(Grid.Element[cell], Grid.Mass[cell], Grid.IsSolidCell(cell) && (Grid.Properties[cell] & 128) == 128);
		}
		return 0f;
	}

	// Token: 0x0600424F RID: 16975 RVA: 0x0017BA90 File Offset: 0x00179C90
	public static float GetRadiationAbsorptionPercentage(Element elem, float mass, bool isConstructed)
	{
		float num = 2000f;
		float num2 = 0.3f;
		float num3 = 0.7f;
		float num4 = 0.8f;
		float num5;
		if (isConstructed)
		{
			num5 = elem.radiationAbsorptionFactor * num4;
		}
		else
		{
			num5 = elem.radiationAbsorptionFactor * num2 + mass / num * elem.radiationAbsorptionFactor * num3;
		}
		return Mathf.Clamp(num5, 0f, 1f);
	}

	// Token: 0x06004250 RID: 16976 RVA: 0x0017BAF4 File Offset: 0x00179CF4
	public static HashSet<int> CollectCellsBreadthFirst(int start_cell, Func<int, bool> test_func, int max_depth = 10)
	{
		HashSet<int> hashSet = new HashSet<int>();
		HashSet<int> hashSet2 = new HashSet<int>();
		HashSet<int> hashSet3 = new HashSet<int>();
		hashSet3.Add(start_cell);
		Vector2Int[] array = new Vector2Int[]
		{
			new Vector2Int(1, 0),
			new Vector2Int(-1, 0),
			new Vector2Int(0, 1),
			new Vector2Int(0, -1)
		};
		for (int i = 0; i < max_depth; i++)
		{
			List<int> list = new List<int>();
			foreach (int num in hashSet3)
			{
				foreach (Vector2Int vector2Int in array)
				{
					int num2 = Grid.OffsetCell(num, vector2Int.x, vector2Int.y);
					if (!hashSet2.Contains(num2) && !hashSet.Contains(num2))
					{
						if (Grid.IsValidCell(num2) && test_func(num2))
						{
							hashSet.Add(num2);
							list.Add(num2);
						}
						else
						{
							hashSet2.Add(num2);
						}
					}
				}
			}
			hashSet3.Clear();
			foreach (int num3 in list)
			{
				hashSet3.Add(num3);
			}
			list.Clear();
			if (hashSet3.Count == 0)
			{
				break;
			}
		}
		return hashSet;
	}

	// Token: 0x06004251 RID: 16977 RVA: 0x0017BC90 File Offset: 0x00179E90
	public static HashSet<int> FloodCollectCells(int start_cell, Func<int, bool> is_valid, int maxSize = 300, HashSet<int> AddInvalidCellsToSet = null, bool clearOversizedResults = true)
	{
		HashSet<int> hashSet = new HashSet<int>();
		HashSet<int> hashSet2 = new HashSet<int>();
		GameUtil.probeFromCell(start_cell, is_valid, hashSet, hashSet2, maxSize);
		if (AddInvalidCellsToSet != null)
		{
			AddInvalidCellsToSet.UnionWith(hashSet2);
			if (hashSet.Count > maxSize)
			{
				AddInvalidCellsToSet.UnionWith(hashSet);
			}
		}
		if (hashSet.Count > maxSize && clearOversizedResults)
		{
			hashSet.Clear();
		}
		return hashSet;
	}

	// Token: 0x06004252 RID: 16978 RVA: 0x0017BCE4 File Offset: 0x00179EE4
	public static HashSet<int> FloodCollectCells(HashSet<int> results, int start_cell, Func<int, bool> is_valid, int maxSize = 300, HashSet<int> AddInvalidCellsToSet = null, bool clearOversizedResults = true)
	{
		HashSet<int> hashSet = new HashSet<int>();
		GameUtil.probeFromCell(start_cell, is_valid, results, hashSet, maxSize);
		if (AddInvalidCellsToSet != null)
		{
			AddInvalidCellsToSet.UnionWith(hashSet);
			if (results.Count > maxSize)
			{
				AddInvalidCellsToSet.UnionWith(results);
			}
		}
		if (results.Count > maxSize && clearOversizedResults)
		{
			results.Clear();
		}
		return results;
	}

	// Token: 0x06004253 RID: 16979 RVA: 0x0017BD34 File Offset: 0x00179F34
	private static void probeFromCell(int start_cell, Func<int, bool> is_valid, HashSet<int> cells, HashSet<int> invalidCells, int maxSize = 300)
	{
		if (cells.Count > maxSize || !Grid.IsValidCell(start_cell) || invalidCells.Contains(start_cell) || cells.Contains(start_cell) || !is_valid(start_cell))
		{
			invalidCells.Add(start_cell);
			return;
		}
		cells.Add(start_cell);
		GameUtil.probeFromCell(Grid.CellLeft(start_cell), is_valid, cells, invalidCells, maxSize);
		GameUtil.probeFromCell(Grid.CellRight(start_cell), is_valid, cells, invalidCells, maxSize);
		GameUtil.probeFromCell(Grid.CellAbove(start_cell), is_valid, cells, invalidCells, maxSize);
		GameUtil.probeFromCell(Grid.CellBelow(start_cell), is_valid, cells, invalidCells, maxSize);
	}

	// Token: 0x06004254 RID: 16980 RVA: 0x0017BDBF File Offset: 0x00179FBF
	public static bool FloodFillCheck<ArgType>(Func<int, ArgType, bool> fn, ArgType arg, int start_cell, int max_depth, bool stop_at_solid, bool stop_at_liquid)
	{
		return GameUtil.FloodFillFind<ArgType>(fn, arg, start_cell, max_depth, stop_at_solid, stop_at_liquid) != -1;
	}

	// Token: 0x06004255 RID: 16981 RVA: 0x0017BDD4 File Offset: 0x00179FD4
	private static void FillThreadLocalNeighbors(int cell)
	{
		GameUtil.FloodFillNeighbors.Value[0] = Grid.CellLeft(cell);
		GameUtil.FloodFillNeighbors.Value[1] = Grid.CellAbove(cell);
		GameUtil.FloodFillNeighbors.Value[2] = Grid.CellRight(cell);
		GameUtil.FloodFillNeighbors.Value[3] = Grid.CellBelow(cell);
	}

	// Token: 0x06004256 RID: 16982 RVA: 0x0017BE3C File Offset: 0x0017A03C
	private static bool CellCheck(int cell, bool stop_at_solid, bool stop_at_liquid)
	{
		if (!Grid.IsValidCell(cell))
		{
			return false;
		}
		Element element = Grid.Element[cell];
		return (!stop_at_solid || !element.IsSolid) && (!stop_at_liquid || !element.IsLiquid) && !GameUtil.FloodFillVisited.Value.Contains(cell);
	}

	// Token: 0x06004257 RID: 16983 RVA: 0x0017BE8C File Offset: 0x0017A08C
	public static int FloodFillFind<ArgType>(Func<int, ArgType, bool> fn, ArgType arg, int start_cell, int max_depth, bool stop_at_solid, bool stop_at_liquid)
	{
		if (GameUtil.CellCheck(start_cell, stop_at_solid, stop_at_liquid))
		{
			GameUtil.FloodFillNext.Value.Enqueue(new GameUtil.FloodFillInfo
			{
				cell = start_cell,
				depth = 0
			});
		}
		int num = -1;
		while (GameUtil.FloodFillNext.Value.Count > 0)
		{
			GameUtil.FloodFillInfo floodFillInfo = GameUtil.FloodFillNext.Value.Dequeue();
			if (!GameUtil.FloodFillVisited.Value.Contains(floodFillInfo.cell))
			{
				GameUtil.FloodFillVisited.Value.Add(floodFillInfo.cell);
				if (fn(floodFillInfo.cell, arg))
				{
					num = floodFillInfo.cell;
					break;
				}
				if (floodFillInfo.depth < max_depth)
				{
					GameUtil.FillThreadLocalNeighbors(floodFillInfo.cell);
					foreach (int num2 in GameUtil.FloodFillNeighbors.Value)
					{
						if (GameUtil.CellCheck(num2, stop_at_solid, stop_at_liquid))
						{
							GameUtil.FloodFillNext.Value.Enqueue(new GameUtil.FloodFillInfo
							{
								cell = num2,
								depth = floodFillInfo.depth + 1
							});
						}
					}
				}
			}
		}
		GameUtil.FloodFillVisited.Value.Clear();
		GameUtil.FloodFillNext.Value.Clear();
		return num;
	}

	// Token: 0x06004258 RID: 16984 RVA: 0x0017BFF8 File Offset: 0x0017A1F8
	public static int FloodFillFindBest<ArgType>(Func<int, ArgType, float> rateCell, ArgType arg, Func<int, ArgType, bool> validCheck, int startCell, int maxCellEvaluations = -1)
	{
		if (!validCheck(startCell, arg))
		{
			return Grid.InvalidCell;
		}
		float num = rateCell(startCell, arg);
		int num2 = startCell;
		if (validCheck(startCell, arg))
		{
			GameUtil.FloodFillNext.Value.Enqueue(new GameUtil.FloodFillInfo
			{
				cell = startCell,
				depth = 0
			});
		}
		GameUtil.FloodFillVisited.Value.Add(Grid.InvalidCell);
		GameUtil.FloodFillVisited.Value.Add(startCell);
		while (GameUtil.FloodFillNext.Value.Count > 0 && maxCellEvaluations != 0)
		{
			GameUtil.FloodFillInfo floodFillInfo = GameUtil.FloodFillNext.Value.Dequeue();
			float num3 = rateCell(floodFillInfo.cell, arg);
			if (num3 > num)
			{
				num = num3;
				num2 = floodFillInfo.cell;
			}
			GameUtil.FillThreadLocalNeighbors(floodFillInfo.cell);
			foreach (int num4 in GameUtil.FloodFillNeighbors.Value)
			{
				if (!GameUtil.FloodFillVisited.Value.Contains(num4) && validCheck(num4, arg))
				{
					GameUtil.FloodFillNext.Value.Enqueue(new GameUtil.FloodFillInfo
					{
						cell = num4,
						depth = floodFillInfo.depth + 1
					});
					GameUtil.FloodFillVisited.Value.Add(num4);
				}
			}
			if (maxCellEvaluations > 0)
			{
				maxCellEvaluations--;
			}
		}
		GameUtil.FloodFillNext.Value.Clear();
		GameUtil.FloodFillVisited.Value.Clear();
		return num2;
	}

	// Token: 0x06004259 RID: 16985 RVA: 0x0017C1A4 File Offset: 0x0017A3A4
	public static void FloodFillConditional(int start_cell, Func<int, bool> condition, ICollection<int> visited_cells, ICollection<int> valid_cells = null)
	{
		GameUtil.FloodFillNext.Value.Enqueue(new GameUtil.FloodFillInfo
		{
			cell = start_cell,
			depth = 0
		});
		GameUtil.FloodFillConditional(GameUtil.FloodFillNext.Value, condition, visited_cells, valid_cells, 10000);
	}

	// Token: 0x0600425A RID: 16986 RVA: 0x0017C1F0 File Offset: 0x0017A3F0
	public static void FloodFillConditional(Queue<GameUtil.FloodFillInfo> queue, Func<int, bool> condition, ICollection<int> visited_cells, ICollection<int> valid_cells = null, int max_depth = 10000)
	{
		while (queue.Count > 0)
		{
			GameUtil.FloodFillInfo floodFillInfo = queue.Dequeue();
			if (floodFillInfo.depth < max_depth && Grid.IsValidCell(floodFillInfo.cell) && !visited_cells.Contains(floodFillInfo.cell))
			{
				visited_cells.Add(floodFillInfo.cell);
				if (condition(floodFillInfo.cell))
				{
					if (valid_cells != null)
					{
						valid_cells.Add(floodFillInfo.cell);
					}
					int num = floodFillInfo.depth + 1;
					queue.Enqueue(new GameUtil.FloodFillInfo
					{
						cell = Grid.CellLeft(floodFillInfo.cell),
						depth = num
					});
					queue.Enqueue(new GameUtil.FloodFillInfo
					{
						cell = Grid.CellRight(floodFillInfo.cell),
						depth = num
					});
					queue.Enqueue(new GameUtil.FloodFillInfo
					{
						cell = Grid.CellAbove(floodFillInfo.cell),
						depth = num
					});
					queue.Enqueue(new GameUtil.FloodFillInfo
					{
						cell = Grid.CellBelow(floodFillInfo.cell),
						depth = num
					});
				}
			}
		}
		queue.Clear();
	}

	// Token: 0x0600425B RID: 16987 RVA: 0x0017C324 File Offset: 0x0017A524
	public static void AppendHardnessString(StringBuilder builder, Element element, bool addColor = true)
	{
		if (!element.IsSolid)
		{
			builder.Append(ELEMENTS.HARDNESS.NA);
			return;
		}
		Color color = GameUtil.Hardness.firmColor;
		string text;
		if (element.hardness >= 255)
		{
			color = GameUtil.Hardness.ImpenetrableColor;
			text = ELEMENTS.HARDNESS.IMPENETRABLE;
		}
		else if (element.hardness >= 150)
		{
			color = GameUtil.Hardness.nearlyImpenetrableColor;
			text = ELEMENTS.HARDNESS.NEARLYIMPENETRABLE;
		}
		else if (element.hardness >= 50)
		{
			color = GameUtil.Hardness.veryFirmColor;
			text = ELEMENTS.HARDNESS.VERYFIRM;
		}
		else if (element.hardness >= 25)
		{
			color = GameUtil.Hardness.firmColor;
			text = ELEMENTS.HARDNESS.FIRM;
		}
		else if (element.hardness >= 10)
		{
			color = GameUtil.Hardness.softColor;
			text = ELEMENTS.HARDNESS.SOFT;
		}
		else
		{
			color = GameUtil.Hardness.verySoftColor;
			text = ELEMENTS.HARDNESS.VERYSOFT;
		}
		if (addColor)
		{
			builder.AppendFormat("<color=#{0}>", color.ToHexString());
		}
		builder.AppendFormat(text, element.hardness);
		if (addColor)
		{
			builder.Append("</color>");
		}
	}

	// Token: 0x0600425C RID: 16988 RVA: 0x0017C435 File Offset: 0x0017A635
	public static string GetHardnessString(Element element, bool addColor = true)
	{
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		GameUtil.AppendHardnessString(stringBuilder, element, addColor);
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}

	// Token: 0x0600425D RID: 16989 RVA: 0x0017C44C File Offset: 0x0017A64C
	public static string GetGermResistanceModifierString(float modifier, bool addColor = true)
	{
		Color color = Color.black;
		string text = "";
		if (modifier > 0f)
		{
			if (modifier >= 5f)
			{
				color = GameUtil.GermResistanceValues.PositiveLargeColor;
				text = string.Format(DUPLICANTS.ATTRIBUTES.GERMRESISTANCE.MODIFIER_DESCRIPTORS.POSITIVE_LARGE, modifier);
			}
			else if (modifier >= 2f)
			{
				color = GameUtil.GermResistanceValues.PositiveMediumColor;
				text = string.Format(DUPLICANTS.ATTRIBUTES.GERMRESISTANCE.MODIFIER_DESCRIPTORS.POSITIVE_MEDIUM, modifier);
			}
			else if (modifier > 0f)
			{
				color = GameUtil.GermResistanceValues.PositiveSmallColor;
				text = string.Format(DUPLICANTS.ATTRIBUTES.GERMRESISTANCE.MODIFIER_DESCRIPTORS.POSITIVE_SMALL, modifier);
			}
		}
		else if (modifier < 0f)
		{
			if (modifier <= -5f)
			{
				color = GameUtil.GermResistanceValues.NegativeLargeColor;
				text = string.Format(DUPLICANTS.ATTRIBUTES.GERMRESISTANCE.MODIFIER_DESCRIPTORS.NEGATIVE_LARGE, modifier);
			}
			else if (modifier <= -2f)
			{
				color = GameUtil.GermResistanceValues.NegativeMediumColor;
				text = string.Format(DUPLICANTS.ATTRIBUTES.GERMRESISTANCE.MODIFIER_DESCRIPTORS.NEGATIVE_MEDIUM, modifier);
			}
			else if (modifier < 0f)
			{
				color = GameUtil.GermResistanceValues.NegativeSmallColor;
				text = string.Format(DUPLICANTS.ATTRIBUTES.GERMRESISTANCE.MODIFIER_DESCRIPTORS.NEGATIVE_SMALL, modifier);
			}
		}
		else
		{
			addColor = false;
			text = string.Format(DUPLICANTS.ATTRIBUTES.GERMRESISTANCE.MODIFIER_DESCRIPTORS.NONE, modifier);
		}
		if (addColor)
		{
			text = string.Format("<color=#{0}>{1}</color>", color.ToHexString(), text);
		}
		return text;
	}

	// Token: 0x0600425E RID: 16990 RVA: 0x0017C594 File Offset: 0x0017A794
	public static string GetThermalConductivityString(Element element, bool addColor = true, bool addValue = true)
	{
		Color color = GameUtil.ThermalConductivityValues.mediumConductivityColor;
		string text;
		if (element.thermalConductivity >= 50f)
		{
			color = GameUtil.ThermalConductivityValues.veryHighConductivityColor;
			text = UI.ELEMENTAL.THERMALCONDUCTIVITY.ADJECTIVES.VERY_HIGH_CONDUCTIVITY;
		}
		else if (element.thermalConductivity >= 10f)
		{
			color = GameUtil.ThermalConductivityValues.highConductivityColor;
			text = UI.ELEMENTAL.THERMALCONDUCTIVITY.ADJECTIVES.HIGH_CONDUCTIVITY;
		}
		else if (element.thermalConductivity >= 2f)
		{
			color = GameUtil.ThermalConductivityValues.mediumConductivityColor;
			text = UI.ELEMENTAL.THERMALCONDUCTIVITY.ADJECTIVES.MEDIUM_CONDUCTIVITY;
		}
		else if (element.thermalConductivity >= 1f)
		{
			color = GameUtil.ThermalConductivityValues.lowConductivityColor;
			text = UI.ELEMENTAL.THERMALCONDUCTIVITY.ADJECTIVES.LOW_CONDUCTIVITY;
		}
		else
		{
			color = GameUtil.ThermalConductivityValues.veryLowConductivityColor;
			text = UI.ELEMENTAL.THERMALCONDUCTIVITY.ADJECTIVES.VERY_LOW_CONDUCTIVITY;
		}
		if (addColor)
		{
			text = string.Format("<color=#{0}>{1}</color>", color.ToHexString(), text);
		}
		if (addValue)
		{
			text = string.Format(UI.ELEMENTAL.THERMALCONDUCTIVITY.ADJECTIVES.VALUE_WITH_ADJECTIVE, element.thermalConductivity.ToString(), text);
		}
		return text;
	}

	// Token: 0x0600425F RID: 16991 RVA: 0x0017C674 File Offset: 0x0017A874
	public static string GetBreathableString(Element element, float Mass)
	{
		if (!element.IsGas && !element.IsVacuum)
		{
			return "";
		}
		Color color = GameUtil.BreathableValues.positiveColor;
		SimHashes id = element.id;
		LocString locString;
		if (id != SimHashes.Oxygen)
		{
			if (id != SimHashes.ContaminatedOxygen)
			{
				color = GameUtil.BreathableValues.negativeColor;
				locString = UI.OVERLAYS.OXYGEN.LEGEND4;
			}
			else if (Mass >= SimDebugView.optimallyBreathable)
			{
				color = GameUtil.BreathableValues.positiveColor;
				locString = UI.OVERLAYS.OXYGEN.LEGEND1;
			}
			else if (Mass >= SimDebugView.minimumBreathable + (SimDebugView.optimallyBreathable - SimDebugView.minimumBreathable) / 2f)
			{
				color = GameUtil.BreathableValues.positiveColor;
				locString = UI.OVERLAYS.OXYGEN.LEGEND2;
			}
			else if (Mass >= SimDebugView.minimumBreathable)
			{
				color = GameUtil.BreathableValues.warningColor;
				locString = UI.OVERLAYS.OXYGEN.LEGEND3;
			}
			else
			{
				color = GameUtil.BreathableValues.negativeColor;
				locString = UI.OVERLAYS.OXYGEN.LEGEND4;
			}
		}
		else if (Mass >= SimDebugView.optimallyBreathable)
		{
			color = GameUtil.BreathableValues.positiveColor;
			locString = UI.OVERLAYS.OXYGEN.LEGEND1;
		}
		else if (Mass >= SimDebugView.minimumBreathable + (SimDebugView.optimallyBreathable - SimDebugView.minimumBreathable) / 2f)
		{
			color = GameUtil.BreathableValues.positiveColor;
			locString = UI.OVERLAYS.OXYGEN.LEGEND2;
		}
		else if (Mass >= SimDebugView.minimumBreathable)
		{
			color = GameUtil.BreathableValues.warningColor;
			locString = UI.OVERLAYS.OXYGEN.LEGEND3;
		}
		else
		{
			color = GameUtil.BreathableValues.negativeColor;
			locString = UI.OVERLAYS.OXYGEN.LEGEND4;
		}
		return string.Format(ELEMENTS.BREATHABLEDESC, color.ToHexString(), locString);
	}

	// Token: 0x06004260 RID: 16992 RVA: 0x0017C7A8 File Offset: 0x0017A9A8
	public static string GetWireLoadColor(float load, float maxLoad, float potentialLoad)
	{
		Color color;
		if (load > maxLoad + POWER.FLOAT_FUDGE_FACTOR)
		{
			color = GameUtil.WireLoadValues.negativeColor;
		}
		else if (potentialLoad > maxLoad && load / maxLoad >= 0.75f)
		{
			color = GameUtil.WireLoadValues.warningColor;
		}
		else
		{
			color = Color.white;
		}
		return color.ToHexString();
	}

	// Token: 0x06004261 RID: 16993 RVA: 0x0017C7E9 File Offset: 0x0017A9E9
	public static string GetHotkeyString(global::Action action)
	{
		if (KInputManager.currentControllerIsGamepad)
		{
			return UI.FormatAsHotkey(GameUtil.GetActionString(action));
		}
		return UI.FormatAsHotkey("[" + GameUtil.GetActionString(action) + "]");
	}

	// Token: 0x06004262 RID: 16994 RVA: 0x0017C818 File Offset: 0x0017AA18
	public static string ReplaceHotkeyString(string template, global::Action action)
	{
		return template.Replace("{Hotkey}", GameUtil.GetHotkeyString(action));
	}

	// Token: 0x06004263 RID: 16995 RVA: 0x0017C82B File Offset: 0x0017AA2B
	public static string ReplaceHotkeyString(string template, global::Action action1, global::Action action2)
	{
		return template.Replace("{Hotkey}", GameUtil.GetHotkeyString(action1) + GameUtil.GetHotkeyString(action2));
	}

	// Token: 0x06004264 RID: 16996 RVA: 0x0017C84C File Offset: 0x0017AA4C
	public static string GetKeycodeLocalized(KKeyCode key_code)
	{
		string text = key_code.ToString();
		if (key_code <= KKeyCode.Slash)
		{
			if (key_code <= KKeyCode.Tab)
			{
				if (key_code == KKeyCode.None)
				{
					return text;
				}
				if (key_code == KKeyCode.Backspace)
				{
					return INPUT.BACKSPACE;
				}
				if (key_code == KKeyCode.Tab)
				{
					return INPUT.TAB;
				}
			}
			else if (key_code <= KKeyCode.Escape)
			{
				if (key_code == KKeyCode.Return)
				{
					return INPUT.ENTER;
				}
				if (key_code == KKeyCode.Escape)
				{
					return INPUT.ESCAPE;
				}
			}
			else
			{
				if (key_code == KKeyCode.Space)
				{
					return INPUT.SPACE;
				}
				switch (key_code)
				{
				case KKeyCode.Plus:
					return "+";
				case KKeyCode.Comma:
					return ",";
				case KKeyCode.Minus:
					return "-";
				case KKeyCode.Period:
					return INPUT.PERIOD;
				case KKeyCode.Slash:
					return "/";
				}
			}
		}
		else if (key_code <= KKeyCode.Insert)
		{
			switch (key_code)
			{
			case KKeyCode.Colon:
				return ":";
			case KKeyCode.Semicolon:
				return ";";
			case KKeyCode.Less:
				break;
			case KKeyCode.Equals:
				return "=";
			default:
				switch (key_code)
				{
				case KKeyCode.LeftBracket:
					return "[";
				case KKeyCode.Backslash:
					return "\\";
				case KKeyCode.RightBracket:
					return "]";
				case KKeyCode.Caret:
				case KKeyCode.Underscore:
					break;
				case KKeyCode.BackQuote:
					return INPUT.BACKQUOTE;
				default:
					switch (key_code)
					{
					case KKeyCode.Keypad0:
						return INPUT.NUM + " 0";
					case KKeyCode.Keypad1:
						return INPUT.NUM + " 1";
					case KKeyCode.Keypad2:
						return INPUT.NUM + " 2";
					case KKeyCode.Keypad3:
						return INPUT.NUM + " 3";
					case KKeyCode.Keypad4:
						return INPUT.NUM + " 4";
					case KKeyCode.Keypad5:
						return INPUT.NUM + " 5";
					case KKeyCode.Keypad6:
						return INPUT.NUM + " 6";
					case KKeyCode.Keypad7:
						return INPUT.NUM + " 7";
					case KKeyCode.Keypad8:
						return INPUT.NUM + " 8";
					case KKeyCode.Keypad9:
						return INPUT.NUM + " 9";
					case KKeyCode.KeypadPeriod:
						return INPUT.NUM + " " + INPUT.PERIOD;
					case KKeyCode.KeypadDivide:
						return INPUT.NUM + " /";
					case KKeyCode.KeypadMultiply:
						return INPUT.NUM + " *";
					case KKeyCode.KeypadMinus:
						return INPUT.NUM + " -";
					case KKeyCode.KeypadPlus:
						return INPUT.NUM + " +";
					case KKeyCode.KeypadEnter:
						return INPUT.NUM + " " + INPUT.ENTER;
					case KKeyCode.Insert:
						return INPUT.INSERT;
					}
					break;
				}
				break;
			}
		}
		else if (key_code <= KKeyCode.Mouse6)
		{
			switch (key_code)
			{
			case KKeyCode.RightShift:
				return INPUT.RIGHT_SHIFT;
			case KKeyCode.LeftShift:
				return INPUT.LEFT_SHIFT;
			case KKeyCode.RightControl:
				return INPUT.RIGHT_CTRL;
			case KKeyCode.LeftControl:
				return INPUT.LEFT_CTRL;
			case KKeyCode.RightAlt:
				return INPUT.RIGHT_ALT;
			case KKeyCode.LeftAlt:
				return INPUT.LEFT_ALT;
			default:
				switch (key_code)
				{
				case KKeyCode.Mouse0:
					return INPUT.MOUSE + " 0";
				case KKeyCode.Mouse1:
					return INPUT.MOUSE + " 1";
				case KKeyCode.Mouse2:
					return INPUT.MOUSE + " 2";
				case KKeyCode.Mouse3:
					return INPUT.MOUSE + " 3";
				case KKeyCode.Mouse4:
					return INPUT.MOUSE + " 4";
				case KKeyCode.Mouse5:
					return INPUT.MOUSE + " 5";
				case KKeyCode.Mouse6:
					return INPUT.MOUSE + " 6";
				}
				break;
			}
		}
		else
		{
			if (key_code == KKeyCode.MouseScrollDown)
			{
				return INPUT.MOUSE_SCROLL_DOWN;
			}
			if (key_code == KKeyCode.MouseScrollUp)
			{
				return INPUT.MOUSE_SCROLL_UP;
			}
		}
		if (KKeyCode.A <= key_code && key_code <= KKeyCode.Z)
		{
			text = ((char)(65 + (key_code - KKeyCode.A))).ToString();
		}
		else if (KKeyCode.Alpha0 <= key_code && key_code <= KKeyCode.Alpha9)
		{
			text = ((char)(48 + (key_code - KKeyCode.Alpha0))).ToString();
		}
		else if (KKeyCode.F1 <= key_code && key_code <= KKeyCode.F12)
		{
			text = "F" + (key_code - KKeyCode.F1 + 1).ToString();
		}
		else
		{
			global::Debug.LogWarning("Unable to find proper string for KKeyCode: " + key_code.ToString() + " using key_code.ToString()");
		}
		return text;
	}

	// Token: 0x06004265 RID: 16997 RVA: 0x0017CE54 File Offset: 0x0017B054
	public static string GetActionString(global::Action action)
	{
		string text = "";
		if (action == global::Action.NumActions)
		{
			return text;
		}
		BindingEntry bindingEntry = GameUtil.ActionToBinding(action);
		KKeyCode mKeyCode = bindingEntry.mKeyCode;
		if (KInputManager.currentControllerIsGamepad)
		{
			return KInputManager.steamInputInterpreter.GetActionGlyph(action);
		}
		if (bindingEntry.mModifier == global::Modifier.None)
		{
			return GameUtil.GetKeycodeLocalized(mKeyCode).ToUpper();
		}
		string text2 = "";
		global::Modifier mModifier = bindingEntry.mModifier;
		switch (mModifier)
		{
		case global::Modifier.Alt:
			text2 = GameUtil.GetKeycodeLocalized(KKeyCode.LeftAlt).ToUpper();
			break;
		case global::Modifier.Ctrl:
			text2 = GameUtil.GetKeycodeLocalized(KKeyCode.LeftControl).ToUpper();
			break;
		case (global::Modifier)3:
			break;
		case global::Modifier.Shift:
			text2 = GameUtil.GetKeycodeLocalized(KKeyCode.LeftShift).ToUpper();
			break;
		default:
			if (mModifier != global::Modifier.CapsLock)
			{
				if (mModifier == global::Modifier.Backtick)
				{
					text2 = GameUtil.GetKeycodeLocalized(KKeyCode.BackQuote).ToUpper();
				}
			}
			else
			{
				text2 = GameUtil.GetKeycodeLocalized(KKeyCode.CapsLock).ToUpper();
			}
			break;
		}
		return text2 + " + " + GameUtil.GetKeycodeLocalized(mKeyCode).ToUpper();
	}

	// Token: 0x06004266 RID: 16998 RVA: 0x0017CF48 File Offset: 0x0017B148
	public static void CreateExplosion(Vector3 explosion_pos)
	{
		Vector2 vector = new Vector2(explosion_pos.x, explosion_pos.y);
		float num = 5f;
		float num2 = num * num;
		foreach (Health health in Components.Health.Items)
		{
			Vector3 position = health.transform.GetPosition();
			float sqrMagnitude = (new Vector2(position.x, position.y) - vector).sqrMagnitude;
			if (num2 >= sqrMagnitude && health != null)
			{
				health.Damage(health.maxHitPoints);
			}
		}
	}

	// Token: 0x06004267 RID: 16999 RVA: 0x0017D000 File Offset: 0x0017B200
	private static void GetNonSolidCells(int x, int y, List<int> cells, int min_x, int min_y, int max_x, int max_y)
	{
		int num = Grid.XYToCell(x, y);
		if (Grid.IsValidCell(num) && !Grid.Solid[num] && !Grid.DupePassable[num] && x >= min_x && x <= max_x && y >= min_y && y <= max_y && !cells.Contains(num))
		{
			cells.Add(num);
			GameUtil.GetNonSolidCells(x + 1, y, cells, min_x, min_y, max_x, max_y);
			GameUtil.GetNonSolidCells(x - 1, y, cells, min_x, min_y, max_x, max_y);
			GameUtil.GetNonSolidCells(x, y + 1, cells, min_x, min_y, max_x, max_y);
			GameUtil.GetNonSolidCells(x, y - 1, cells, min_x, min_y, max_x, max_y);
		}
	}

	// Token: 0x06004268 RID: 17000 RVA: 0x0017D0A4 File Offset: 0x0017B2A4
	public static void GetNonSolidCells(int cell, int radius, List<int> cells)
	{
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(cell, out num, out num2);
		GameUtil.GetNonSolidCells(num, num2, cells, num - radius, num2 - radius, num + radius, num2 + radius);
	}

	// Token: 0x06004269 RID: 17001 RVA: 0x0017D0D4 File Offset: 0x0017B2D4
	public static float GetMaxStressInActiveWorld()
	{
		if (Components.LiveMinionIdentities.Count <= 0)
		{
			return 0f;
		}
		float num = 0f;
		foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.Items)
		{
			if (!minionIdentity.IsNullOrDestroyed() && minionIdentity.GetMyWorldId() == ClusterManager.Instance.activeWorldId)
			{
				AmountInstance amountInstance = Db.Get().Amounts.Stress.Lookup(minionIdentity);
				if (amountInstance != null)
				{
					num = Mathf.Max(num, amountInstance.value);
				}
			}
		}
		return num;
	}

	// Token: 0x0600426A RID: 17002 RVA: 0x0017D180 File Offset: 0x0017B380
	public static float GetAverageStressInActiveWorld()
	{
		if (Components.LiveMinionIdentities.Count <= 0)
		{
			return 0f;
		}
		float num = 0f;
		int num2 = 0;
		foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.Items)
		{
			if (!minionIdentity.IsNullOrDestroyed() && minionIdentity.GetMyWorldId() == ClusterManager.Instance.activeWorldId)
			{
				num += Db.Get().Amounts.Stress.Lookup(minionIdentity).value;
				num2++;
			}
		}
		return num / (float)num2;
	}

	// Token: 0x0600426B RID: 17003 RVA: 0x0017D22C File Offset: 0x0017B42C
	public static string MigrateFMOD(FMODAsset asset)
	{
		if (asset == null)
		{
			return null;
		}
		if (asset.path == null)
		{
			return asset.name;
		}
		return asset.path;
	}

	// Token: 0x0600426C RID: 17004 RVA: 0x0017D24E File Offset: 0x0017B44E
	private static void SortGameObjectDescriptors(List<IGameObjectEffectDescriptor> descriptorList)
	{
		descriptorList.Sort(delegate(IGameObjectEffectDescriptor e1, IGameObjectEffectDescriptor e2)
		{
			int num = global::TUNING.BUILDINGS.COMPONENT_DESCRIPTION_ORDER.IndexOf(e1.GetType());
			int num2 = global::TUNING.BUILDINGS.COMPONENT_DESCRIPTION_ORDER.IndexOf(e2.GetType());
			return num.CompareTo(num2);
		});
	}

	// Token: 0x0600426D RID: 17005 RVA: 0x0017D278 File Offset: 0x0017B478
	public static void IndentListOfDescriptors(List<Descriptor> list, int indentCount = 1)
	{
		for (int i = 0; i < list.Count; i++)
		{
			Descriptor descriptor = list[i];
			for (int j = 0; j < indentCount; j++)
			{
				descriptor.IncreaseIndent();
			}
			list[i] = descriptor;
		}
	}

	// Token: 0x0600426E RID: 17006 RVA: 0x0017D2BC File Offset: 0x0017B4BC
	public static List<Descriptor> GetAllDescriptors(GameObject go, bool simpleInfoScreen = false)
	{
		List<Descriptor> list = new List<Descriptor>();
		List<IGameObjectEffectDescriptor> list2 = new List<IGameObjectEffectDescriptor>(go.GetComponents<IGameObjectEffectDescriptor>());
		StateMachineController component = go.GetComponent<StateMachineController>();
		if (component != null)
		{
			list2.AddRange(component.GetDescriptors());
		}
		GameUtil.SortGameObjectDescriptors(list2);
		foreach (IGameObjectEffectDescriptor gameObjectEffectDescriptor in list2)
		{
			List<Descriptor> descriptors = gameObjectEffectDescriptor.GetDescriptors(go);
			if (descriptors != null)
			{
				foreach (Descriptor descriptor in descriptors)
				{
					if (!descriptor.onlyForSimpleInfoScreen || simpleInfoScreen)
					{
						list.Add(descriptor);
					}
				}
			}
		}
		KPrefabID component2 = go.GetComponent<KPrefabID>();
		if (component2 != null && component2.AdditionalRequirements != null)
		{
			foreach (Descriptor descriptor2 in component2.AdditionalRequirements)
			{
				if (!descriptor2.onlyForSimpleInfoScreen || simpleInfoScreen)
				{
					list.Add(descriptor2);
				}
			}
		}
		if (component2 != null && component2.AdditionalEffects != null)
		{
			foreach (Descriptor descriptor3 in component2.AdditionalEffects)
			{
				if (!descriptor3.onlyForSimpleInfoScreen || simpleInfoScreen)
				{
					list.Add(descriptor3);
				}
			}
		}
		return list;
	}

	// Token: 0x0600426F RID: 17007 RVA: 0x0017D45C File Offset: 0x0017B65C
	public static List<Descriptor> GetDetailDescriptors(List<Descriptor> descriptors)
	{
		List<Descriptor> list = new List<Descriptor>();
		foreach (Descriptor descriptor in descriptors)
		{
			if (descriptor.type == Descriptor.DescriptorType.Detail)
			{
				list.Add(descriptor);
			}
		}
		GameUtil.IndentListOfDescriptors(list, 1);
		return list;
	}

	// Token: 0x06004270 RID: 17008 RVA: 0x0017D4C4 File Offset: 0x0017B6C4
	public static List<Descriptor> GetRequirementDescriptors(List<Descriptor> descriptors)
	{
		return GameUtil.GetRequirementDescriptors(descriptors, true);
	}

	// Token: 0x06004271 RID: 17009 RVA: 0x0017D4D0 File Offset: 0x0017B6D0
	public static List<Descriptor> GetRequirementDescriptors(List<Descriptor> descriptors, bool indent)
	{
		List<Descriptor> list = new List<Descriptor>();
		foreach (Descriptor descriptor in descriptors)
		{
			if (descriptor.type == Descriptor.DescriptorType.Requirement)
			{
				list.Add(descriptor);
			}
		}
		if (indent)
		{
			GameUtil.IndentListOfDescriptors(list, 1);
		}
		return list;
	}

	// Token: 0x06004272 RID: 17010 RVA: 0x0017D538 File Offset: 0x0017B738
	public static List<Descriptor> GetEffectDescriptors(List<Descriptor> descriptors)
	{
		List<Descriptor> list = new List<Descriptor>();
		foreach (Descriptor descriptor in descriptors)
		{
			if (descriptor.type == Descriptor.DescriptorType.Effect || descriptor.type == Descriptor.DescriptorType.DiseaseSource)
			{
				list.Add(descriptor);
			}
		}
		GameUtil.IndentListOfDescriptors(list, 1);
		return list;
	}

	// Token: 0x06004273 RID: 17011 RVA: 0x0017D5A8 File Offset: 0x0017B7A8
	public static List<Descriptor> GetInformationDescriptors(List<Descriptor> descriptors)
	{
		List<Descriptor> list = new List<Descriptor>();
		foreach (Descriptor descriptor in descriptors)
		{
			if (descriptor.type == Descriptor.DescriptorType.Lifecycle)
			{
				list.Add(descriptor);
			}
		}
		GameUtil.IndentListOfDescriptors(list, 1);
		return list;
	}

	// Token: 0x06004274 RID: 17012 RVA: 0x0017D610 File Offset: 0x0017B810
	public static List<Descriptor> GetCropOptimumConditionDescriptors(List<Descriptor> descriptors)
	{
		List<Descriptor> list = new List<Descriptor>();
		foreach (Descriptor descriptor in descriptors)
		{
			if (descriptor.type == Descriptor.DescriptorType.Lifecycle)
			{
				Descriptor descriptor2 = descriptor;
				descriptor2.text = "• " + descriptor2.text;
				list.Add(descriptor2);
			}
		}
		GameUtil.IndentListOfDescriptors(list, 1);
		return list;
	}

	// Token: 0x06004275 RID: 17013 RVA: 0x0017D690 File Offset: 0x0017B890
	public static List<Descriptor> GetGameObjectRequirements(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		List<IGameObjectEffectDescriptor> list2 = new List<IGameObjectEffectDescriptor>(go.GetComponents<IGameObjectEffectDescriptor>());
		StateMachineController component = go.GetComponent<StateMachineController>();
		if (component != null)
		{
			list2.AddRange(component.GetDescriptors());
		}
		GameUtil.SortGameObjectDescriptors(list2);
		foreach (IGameObjectEffectDescriptor gameObjectEffectDescriptor in list2)
		{
			List<Descriptor> descriptors = gameObjectEffectDescriptor.GetDescriptors(go);
			if (descriptors != null)
			{
				foreach (Descriptor descriptor in descriptors)
				{
					if (descriptor.type == Descriptor.DescriptorType.Requirement)
					{
						list.Add(descriptor);
					}
				}
			}
		}
		KPrefabID component2 = go.GetComponent<KPrefabID>();
		if (component2.AdditionalRequirements != null)
		{
			list.AddRange(component2.AdditionalRequirements);
		}
		return list;
	}

	// Token: 0x06004276 RID: 17014 RVA: 0x0017D780 File Offset: 0x0017B980
	public static List<Descriptor> GetGameObjectEffects(GameObject go, bool simpleInfoScreen = false)
	{
		List<Descriptor> list = new List<Descriptor>();
		List<IGameObjectEffectDescriptor> list2 = new List<IGameObjectEffectDescriptor>(go.GetComponents<IGameObjectEffectDescriptor>());
		StateMachineController component = go.GetComponent<StateMachineController>();
		if (component != null)
		{
			list2.AddRange(component.GetDescriptors());
		}
		GameUtil.SortGameObjectDescriptors(list2);
		foreach (IGameObjectEffectDescriptor gameObjectEffectDescriptor in list2)
		{
			List<Descriptor> descriptors = gameObjectEffectDescriptor.GetDescriptors(go);
			if (descriptors != null)
			{
				foreach (Descriptor descriptor in descriptors)
				{
					if ((!descriptor.onlyForSimpleInfoScreen || simpleInfoScreen) && (descriptor.type == Descriptor.DescriptorType.Effect || descriptor.type == Descriptor.DescriptorType.DiseaseSource))
					{
						list.Add(descriptor);
					}
				}
			}
		}
		KPrefabID component2 = go.GetComponent<KPrefabID>();
		if (component2 != null && component2.AdditionalEffects != null)
		{
			foreach (Descriptor descriptor2 in component2.AdditionalEffects)
			{
				if (!descriptor2.onlyForSimpleInfoScreen || simpleInfoScreen)
				{
					list.Add(descriptor2);
				}
			}
		}
		return list;
	}

	// Token: 0x06004277 RID: 17015 RVA: 0x0017D8D4 File Offset: 0x0017BAD4
	public static List<Descriptor> GetPlantRequirementDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		List<Descriptor> requirementDescriptors = GameUtil.GetRequirementDescriptors(GameUtil.GetAllDescriptors(go, false));
		if (requirementDescriptors.Count > 0)
		{
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(UI.UISIDESCREENS.PLANTERSIDESCREEN.PLANTREQUIREMENTS, UI.UISIDESCREENS.PLANTERSIDESCREEN.TOOLTIPS.PLANTREQUIREMENTS, Descriptor.DescriptorType.Requirement);
			list.Add(descriptor);
			list.AddRange(requirementDescriptors);
		}
		return list;
	}

	// Token: 0x06004278 RID: 17016 RVA: 0x0017D930 File Offset: 0x0017BB30
	public static List<Descriptor> GetPlantLifeCycleDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		List<Descriptor> informationDescriptors = GameUtil.GetInformationDescriptors(GameUtil.GetAllDescriptors(go, false));
		if (informationDescriptors.Count > 0)
		{
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(UI.UISIDESCREENS.PLANTERSIDESCREEN.LIFECYCLE, UI.UISIDESCREENS.PLANTERSIDESCREEN.TOOLTIPS.PLANTLIFECYCLE, Descriptor.DescriptorType.Lifecycle);
			list.Add(descriptor);
			list.AddRange(informationDescriptors);
		}
		return list;
	}

	// Token: 0x06004279 RID: 17017 RVA: 0x0017D98C File Offset: 0x0017BB8C
	public static List<Descriptor> GetPlantEffectDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (go.GetComponent<Growing>() == null)
		{
			return list;
		}
		List<Descriptor> allDescriptors = GameUtil.GetAllDescriptors(go, false);
		List<Descriptor> list2 = new List<Descriptor>();
		list2.AddRange(GameUtil.GetEffectDescriptors(allDescriptors));
		if (list2.Count > 0)
		{
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(UI.UISIDESCREENS.PLANTERSIDESCREEN.PLANTEFFECTS, UI.UISIDESCREENS.PLANTERSIDESCREEN.TOOLTIPS.PLANTEFFECTS, Descriptor.DescriptorType.Effect);
			list.Add(descriptor);
			list.AddRange(list2);
		}
		return list;
	}

	// Token: 0x0600427A RID: 17018 RVA: 0x0017DA08 File Offset: 0x0017BC08
	public static string GetGameObjectEffectsTooltipString(GameObject go)
	{
		string text = "";
		List<Descriptor> gameObjectEffects = GameUtil.GetGameObjectEffects(go, false);
		if (gameObjectEffects.Count > 0)
		{
			text = text + UI.BUILDINGEFFECTS.OPERATIONEFFECTS + "\n";
		}
		foreach (Descriptor descriptor in gameObjectEffects)
		{
			text = text + descriptor.IndentedText() + "\n";
		}
		return text;
	}

	// Token: 0x0600427B RID: 17019 RVA: 0x0017DA90 File Offset: 0x0017BC90
	public static List<Descriptor> GetEquipmentEffects(EquipmentDef def)
	{
		global::Debug.Assert(def != null);
		List<Descriptor> list = new List<Descriptor>();
		List<AttributeModifier> attributeModifiers = def.AttributeModifiers;
		if (attributeModifiers != null)
		{
			foreach (AttributeModifier attributeModifier in attributeModifiers)
			{
				string name = Db.Get().Attributes.Get(attributeModifier.AttributeId).Name;
				string formattedString = attributeModifier.GetFormattedString();
				string text = ((attributeModifier.Value >= 0f) ? "produced" : "consumed");
				string text2 = UI.GAMEOBJECTEFFECTS.EQUIPMENT_MODS.text.Replace("{Attribute}", name).Replace("{Style}", text).Replace("{Value}", formattedString);
				list.Add(new Descriptor(text2, text2, Descriptor.DescriptorType.Effect, false));
			}
		}
		return list;
	}

	// Token: 0x0600427C RID: 17020 RVA: 0x0017DB80 File Offset: 0x0017BD80
	public static string GetRecipeDescription(Recipe recipe)
	{
		string text = null;
		if (recipe != null)
		{
			text = recipe.recipeDescription;
		}
		if (text == null)
		{
			text = RESEARCH.TYPES.MISSINGRECIPEDESC;
			global::Debug.LogWarning("Missing recipeDescription");
		}
		return text;
	}

	// Token: 0x0600427D RID: 17021 RVA: 0x0017DBB2 File Offset: 0x0017BDB2
	public static int GetCurrentCycle()
	{
		return GameClock.Instance.GetCycle() + 1;
	}

	// Token: 0x0600427E RID: 17022 RVA: 0x0017DBC0 File Offset: 0x0017BDC0
	public static float GetCurrentTimeInCycles()
	{
		return GameClock.Instance.GetTimeInCycles() + 1f;
	}

	// Token: 0x0600427F RID: 17023 RVA: 0x0017DBD4 File Offset: 0x0017BDD4
	public static GameObject GetActiveTelepad()
	{
		GameObject gameObject = GameUtil.GetTelepad(ClusterManager.Instance.activeWorldId);
		if (gameObject == null)
		{
			gameObject = GameUtil.GetTelepad(ClusterManager.Instance.GetStartWorld().id);
		}
		return gameObject;
	}

	// Token: 0x06004280 RID: 17024 RVA: 0x0017DC10 File Offset: 0x0017BE10
	public static GameObject GetTelepad(int worldId)
	{
		if (Components.Telepads.Count > 0)
		{
			for (int i = 0; i < Components.Telepads.Count; i++)
			{
				if (Components.Telepads[i].GetMyWorldId() == worldId)
				{
					return Components.Telepads[i].gameObject;
				}
			}
		}
		return null;
	}

	// Token: 0x06004281 RID: 17025 RVA: 0x0017DC64 File Offset: 0x0017BE64
	public static GameObject KInstantiate(GameObject original, Vector3 position, Grid.SceneLayer sceneLayer, string name = null, int gameLayer = 0)
	{
		return GameUtil.KInstantiate(original, position, sceneLayer, null, name, gameLayer);
	}

	// Token: 0x06004282 RID: 17026 RVA: 0x0017DC72 File Offset: 0x0017BE72
	public static GameObject KInstantiate(GameObject original, Vector3 position, Grid.SceneLayer sceneLayer, GameObject parent, string name = null, int gameLayer = 0)
	{
		position.z = Grid.GetLayerZ(sceneLayer);
		return Util.KInstantiate(original, position, Quaternion.identity, parent, name, true, gameLayer);
	}

	// Token: 0x06004283 RID: 17027 RVA: 0x0017DC93 File Offset: 0x0017BE93
	public static GameObject KInstantiate(GameObject original, Grid.SceneLayer sceneLayer, string name = null, int gameLayer = 0)
	{
		return GameUtil.KInstantiate(original, Vector3.zero, sceneLayer, name, gameLayer);
	}

	// Token: 0x06004284 RID: 17028 RVA: 0x0017DCA3 File Offset: 0x0017BEA3
	public static GameObject KInstantiate(Component original, Grid.SceneLayer sceneLayer, string name = null, int gameLayer = 0)
	{
		return GameUtil.KInstantiate(original.gameObject, Vector3.zero, sceneLayer, name, gameLayer);
	}

	// Token: 0x06004285 RID: 17029 RVA: 0x0017DCB8 File Offset: 0x0017BEB8
	public unsafe static void IsEmissionBlocked(int cell, out bool all_not_gaseous, out bool all_over_pressure)
	{
		int* ptr = stackalloc int[(UIntPtr)16];
		*ptr = Grid.CellBelow(cell);
		ptr[1] = Grid.CellLeft(cell);
		ptr[2] = Grid.CellRight(cell);
		ptr[3] = Grid.CellAbove(cell);
		all_not_gaseous = true;
		all_over_pressure = true;
		for (int i = 0; i < 4; i++)
		{
			int num = ptr[i];
			if (Grid.IsValidCell(num))
			{
				Element element = Grid.Element[num];
				all_not_gaseous = all_not_gaseous && !element.IsGas && !element.IsVacuum;
				all_over_pressure = all_over_pressure && ((!element.IsGas && !element.IsVacuum) || Grid.Mass[num] >= 1.8f);
			}
		}
	}

	// Token: 0x06004286 RID: 17030 RVA: 0x0017DD70 File Offset: 0x0017BF70
	public static float GetDecorAtCell(int cell)
	{
		float num = 0f;
		if (!Grid.Solid[cell])
		{
			num = Grid.Decor[cell];
			num += (float)DecorProvider.GetLightDecorBonus(cell);
		}
		return num;
	}

	// Token: 0x06004287 RID: 17031 RVA: 0x0017DDA4 File Offset: 0x0017BFA4
	public static string GetUnitTypeMassOrUnit(GameObject go)
	{
		string text = UI.UNITSUFFIXES.UNITS;
		KPrefabID component = go.GetComponent<KPrefabID>();
		if (component != null)
		{
			text = (component.Tags.Contains(GameTags.Seed) ? UI.UNITSUFFIXES.UNITS : UI.UNITSUFFIXES.MASS.KILOGRAM);
		}
		return text;
	}

	// Token: 0x06004288 RID: 17032 RVA: 0x0017DDF4 File Offset: 0x0017BFF4
	public static string GetKeywordStyle(Tag tag)
	{
		Element element = ElementLoader.GetElement(tag);
		string text;
		if (element != null)
		{
			text = GameUtil.GetKeywordStyle(element);
		}
		else if (GameUtil.foodTags.Contains(tag))
		{
			text = "food";
		}
		else if (GameUtil.solidTags.Contains(tag))
		{
			text = "solid";
		}
		else
		{
			text = null;
		}
		return text;
	}

	// Token: 0x06004289 RID: 17033 RVA: 0x0017DE44 File Offset: 0x0017C044
	public static string GetKeywordStyle(SimHashes hash)
	{
		Element element = ElementLoader.FindElementByHash(hash);
		if (element != null)
		{
			return GameUtil.GetKeywordStyle(element);
		}
		return null;
	}

	// Token: 0x0600428A RID: 17034 RVA: 0x0017DE64 File Offset: 0x0017C064
	public static string GetKeywordStyle(Element element)
	{
		if (element.id == SimHashes.Oxygen)
		{
			return "oxygen";
		}
		if (element.IsSolid)
		{
			return "solid";
		}
		if (element.IsLiquid)
		{
			return "liquid";
		}
		if (element.IsGas)
		{
			return "gas";
		}
		if (element.IsVacuum)
		{
			return "vacuum";
		}
		return null;
	}

	// Token: 0x0600428B RID: 17035 RVA: 0x0017DEC0 File Offset: 0x0017C0C0
	public static string GetKeywordStyle(GameObject go)
	{
		string text = "";
		global::UnityEngine.Object component = go.GetComponent<Edible>();
		Equippable component2 = go.GetComponent<Equippable>();
		MedicinalPill component3 = go.GetComponent<MedicinalPill>();
		ResearchPointObject component4 = go.GetComponent<ResearchPointObject>();
		if (component != null)
		{
			text = "food";
		}
		else if (component2 != null)
		{
			text = "equipment";
		}
		else if (component3 != null)
		{
			text = "medicine";
		}
		else if (component4 != null)
		{
			text = "research";
		}
		return text;
	}

	// Token: 0x0600428C RID: 17036 RVA: 0x0017DF30 File Offset: 0x0017C130
	public static Sprite GetBiomeSprite(string id)
	{
		string text = "biomeIcon" + char.ToUpper(id[0]).ToString() + id.Substring(1).ToLower();
		Sprite sprite = Assets.GetSprite(text);
		if (sprite != null)
		{
			return new global::Tuple<Sprite, Color>(sprite, Color.white).first;
		}
		global::Debug.LogWarning("Missing codex biome icon: " + text);
		return null;
	}

	// Token: 0x0600428D RID: 17037 RVA: 0x0017DFA0 File Offset: 0x0017C1A0
	public static string GenerateRandomDuplicantName()
	{
		string text = "";
		string text2 = "";
		bool flag = global::UnityEngine.Random.Range(0f, 1f) >= 0.5f;
		List<string> list = new List<string>(LocString.GetStrings(typeof(NAMEGEN.DUPLICANT.NAME.NB)));
		list.AddRange(flag ? LocString.GetStrings(typeof(NAMEGEN.DUPLICANT.NAME.MALE)) : LocString.GetStrings(typeof(NAMEGEN.DUPLICANT.NAME.FEMALE)));
		string random = list.GetRandom<string>();
		if (global::UnityEngine.Random.Range(0f, 1f) > 0.7f)
		{
			List<string> list2 = new List<string>(LocString.GetStrings(typeof(NAMEGEN.DUPLICANT.PREFIX.NB)));
			list2.AddRange(flag ? LocString.GetStrings(typeof(NAMEGEN.DUPLICANT.PREFIX.MALE)) : LocString.GetStrings(typeof(NAMEGEN.DUPLICANT.PREFIX.FEMALE)));
			text = list2.GetRandom<string>();
		}
		if (!string.IsNullOrEmpty(text))
		{
			text += " ";
		}
		if (global::UnityEngine.Random.Range(0f, 1f) >= 0.9f)
		{
			List<string> list3 = new List<string>(LocString.GetStrings(typeof(NAMEGEN.DUPLICANT.SUFFIX.NB)));
			list3.AddRange(flag ? LocString.GetStrings(typeof(NAMEGEN.DUPLICANT.SUFFIX.MALE)) : LocString.GetStrings(typeof(NAMEGEN.DUPLICANT.SUFFIX.FEMALE)));
			text2 = list3.GetRandom<string>();
		}
		if (!string.IsNullOrEmpty(text2))
		{
			text2 = " " + text2;
		}
		return text + random + text2;
	}

	// Token: 0x0600428E RID: 17038 RVA: 0x0017E108 File Offset: 0x0017C308
	public static string GenerateRandomLaunchPadName()
	{
		return NAMEGEN.LAUNCHPAD.FORMAT.Replace("{Name}", global::UnityEngine.Random.Range(1, 1000).ToString());
	}

	// Token: 0x0600428F RID: 17039 RVA: 0x0017E138 File Offset: 0x0017C338
	public static string GenerateRandomRocketName()
	{
		string text = "";
		string text2 = "";
		string text3 = "";
		int num = 1;
		int num2 = 2;
		int num3 = 4;
		string random = new List<string>(LocString.GetStrings(typeof(NAMEGEN.ROCKET.NOUN))).GetRandom<string>();
		int num4 = 0;
		if (global::UnityEngine.Random.value > 0.7f)
		{
			text = new List<string>(LocString.GetStrings(typeof(NAMEGEN.ROCKET.PREFIX))).GetRandom<string>();
			num4 |= num;
		}
		if (global::UnityEngine.Random.value > 0.5f)
		{
			text2 = new List<string>(LocString.GetStrings(typeof(NAMEGEN.ROCKET.ADJECTIVE))).GetRandom<string>();
			num4 |= num2;
		}
		if (global::UnityEngine.Random.value > 0.1f)
		{
			text3 = new List<string>(LocString.GetStrings(typeof(NAMEGEN.ROCKET.SUFFIX))).GetRandom<string>();
			num4 |= num3;
		}
		string text4;
		if (num4 == (num | num2 | num3))
		{
			text4 = NAMEGEN.ROCKET.FMT_PREFIX_ADJECTIVE_NOUN_SUFFIX;
		}
		else if (num4 == (num2 | num3))
		{
			text4 = NAMEGEN.ROCKET.FMT_ADJECTIVE_NOUN_SUFFIX;
		}
		else if (num4 == (num | num3))
		{
			text4 = NAMEGEN.ROCKET.FMT_PREFIX_NOUN_SUFFIX;
		}
		else if (num4 == num3)
		{
			text4 = NAMEGEN.ROCKET.FMT_NOUN_SUFFIX;
		}
		else if (num4 == (num | num2))
		{
			text4 = NAMEGEN.ROCKET.FMT_PREFIX_ADJECTIVE_NOUN;
		}
		else if (num4 == num)
		{
			text4 = NAMEGEN.ROCKET.FMT_PREFIX_NOUN;
		}
		else if (num4 == num2)
		{
			text4 = NAMEGEN.ROCKET.FMT_ADJECTIVE_NOUN;
		}
		else
		{
			text4 = NAMEGEN.ROCKET.FMT_NOUN;
		}
		DebugUtil.LogArgs(new object[]
		{
			"Rocket name bits:",
			Convert.ToString(num4, 2)
		});
		return text4.Replace("{Prefix}", text).Replace("{Adjective}", text2).Replace("{Noun}", random)
			.Replace("{Suffix}", text3);
	}

	// Token: 0x06004290 RID: 17040 RVA: 0x0017E300 File Offset: 0x0017C500
	public static string GenerateRandomWorldName(string[] nameTables)
	{
		if (nameTables == null)
		{
			global::Debug.LogWarning("No name tables provided to generate world name. Using GENERIC");
			nameTables = new string[] { "GENERIC" };
		}
		string text = "";
		foreach (string text2 in nameTables)
		{
			text += Strings.Get("STRINGS.NAMEGEN.WORLD.ROOTS." + text2.ToUpper());
		}
		string text3 = GameUtil.RandomValueFromSeparatedString(text, "\n");
		if (string.IsNullOrEmpty(text3))
		{
			text3 = GameUtil.RandomValueFromSeparatedString(Strings.Get(NAMEGEN.WORLD.ROOTS.GENERIC), "\n");
		}
		string text4 = GameUtil.RandomValueFromSeparatedString(NAMEGEN.WORLD.SUFFIXES.GENERICLIST, "\n");
		return text3 + text4;
	}

	// Token: 0x06004291 RID: 17041 RVA: 0x0017E3BC File Offset: 0x0017C5BC
	public static float GetThermalComfort(Tag duplicantType, int cell, float tolerance)
	{
		DUPLICANTSTATS statsFor = DUPLICANTSTATS.GetStatsFor(duplicantType);
		float num = 0f;
		Element element = ElementLoader.FindElementByHash(SimHashes.Creature);
		if (Grid.Element[cell].thermalConductivity != 0f)
		{
			num = SimUtil.CalculateEnergyFlowCreatures(cell, statsFor.Temperature.Internal.IDEAL, element.specificHeatCapacity, element.thermalConductivity, statsFor.Temperature.SURFACE_AREA, statsFor.Temperature.SKIN_THICKNESS + 0.0025f);
		}
		num -= tolerance;
		return num * 1000f;
	}

	// Token: 0x06004292 RID: 17042 RVA: 0x0017E444 File Offset: 0x0017C644
	public static void FocusCamera(Transform target, bool select = true, bool show_back_button = true)
	{
		GameUtil.FocusCamera(target.GetPosition(), 2f, true, show_back_button);
		if (select)
		{
			KSelectable component = target.GetComponent<KSelectable>();
			SelectTool.Instance.Select(component, false);
		}
	}

	// Token: 0x06004293 RID: 17043 RVA: 0x0017E479 File Offset: 0x0017C679
	public static void FocusCameraOnWorld(int worldID, Vector3 pos, float forceOrthgraphicSize = 10f, global::System.Action callback = null, bool show_back_button = true)
	{
		CameraController.Instance.ActiveWorldStarWipe(worldID, pos, forceOrthgraphicSize, callback);
		if (show_back_button && NotificationScreen_TemporaryActions.Instance != null)
		{
			NotificationScreen_TemporaryActions.Instance.CreateCameraReturnActionButton(CameraController.Instance.transform.position);
		}
	}

	// Token: 0x06004294 RID: 17044 RVA: 0x0017E4B3 File Offset: 0x0017C6B3
	public static void FocusCamera(int cell, bool show_back_button = true)
	{
		GameUtil.FocusCamera(Grid.CellToPos(cell), 2f, true, show_back_button);
	}

	// Token: 0x06004295 RID: 17045 RVA: 0x0017E4C7 File Offset: 0x0017C6C7
	public static void FocusCamera(Vector3 position, float speed = 2f, bool playSound = true, bool show_back_button = true)
	{
		CameraController.Instance.CameraGoTo(position, speed, playSound);
		if (show_back_button && NotificationScreen_TemporaryActions.Instance != null)
		{
			NotificationScreen_TemporaryActions.Instance.CreateCameraReturnActionButton(CameraController.Instance.transform.position);
		}
	}

	// Token: 0x06004296 RID: 17046 RVA: 0x0017E500 File Offset: 0x0017C700
	public static string RandomValueFromSeparatedString(string source, string separator = "\n")
	{
		int num = 0;
		int num2 = 0;
		for (;;)
		{
			num = source.IndexOf(separator, num);
			if (num == -1)
			{
				break;
			}
			num += separator.Length;
			num2++;
		}
		if (num2 == 0)
		{
			return "";
		}
		int num3 = global::UnityEngine.Random.Range(0, num2);
		num = 0;
		for (int i = 0; i < num3; i++)
		{
			num = source.IndexOf(separator, num) + separator.Length;
		}
		int num4 = source.IndexOf(separator, num);
		return source.Substring(num, (num4 == -1) ? (source.Length - num) : (num4 - num));
	}

	// Token: 0x06004297 RID: 17047 RVA: 0x0017E584 File Offset: 0x0017C784
	public static string GetFormattedDiseaseName(byte idx, bool color = false)
	{
		Disease disease = Db.Get().Diseases[(int)idx];
		if (color)
		{
			return string.Format(UI.OVERLAYS.DISEASE.DISEASE_NAME_FORMAT, disease.Name, GameUtil.ColourToHex(GlobalAssets.Instance.colorSet.GetColorByName(disease.overlayColourName)));
		}
		return string.Format(UI.OVERLAYS.DISEASE.DISEASE_NAME_FORMAT_NO_COLOR, disease.Name);
	}

	// Token: 0x06004298 RID: 17048 RVA: 0x0017E5EC File Offset: 0x0017C7EC
	public static string GetFormattedDisease(byte idx, int units, bool color = false)
	{
		if (idx == 255 || units <= 0)
		{
			return UI.OVERLAYS.DISEASE.NO_DISEASE;
		}
		Disease disease = Db.Get().Diseases[(int)idx];
		if (color)
		{
			return string.Format(UI.OVERLAYS.DISEASE.DISEASE_FORMAT, disease.Name, GameUtil.GetFormattedDiseaseAmount(units, GameUtil.TimeSlice.None), GameUtil.ColourToHex(GlobalAssets.Instance.colorSet.GetColorByName(disease.overlayColourName)));
		}
		return string.Format(UI.OVERLAYS.DISEASE.DISEASE_FORMAT_NO_COLOR, disease.Name, GameUtil.GetFormattedDiseaseAmount(units, GameUtil.TimeSlice.None));
	}

	// Token: 0x06004299 RID: 17049 RVA: 0x0017E677 File Offset: 0x0017C877
	public static string GetFormattedDiseaseAmount(int units, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None)
	{
		GameUtil.ApplyTimeSlice(units, timeSlice);
		return GameUtil.AddTimeSliceText(units.ToString("#,##0") + UI.UNITSUFFIXES.DISEASE.UNITS, timeSlice);
	}

	// Token: 0x0600429A RID: 17050 RVA: 0x0017E6A2 File Offset: 0x0017C8A2
	public static string GetFormattedDiseaseAmount(long units, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None)
	{
		GameUtil.ApplyTimeSlice((float)units, timeSlice);
		return GameUtil.AddTimeSliceText(units.ToString("#,##0") + UI.UNITSUFFIXES.DISEASE.UNITS, timeSlice);
	}

	// Token: 0x0600429B RID: 17051 RVA: 0x0017E6CE File Offset: 0x0017C8CE
	public static string ColourizeString(Color32 colour, string str)
	{
		return string.Format("<color=#{0}>{1}</color>", GameUtil.ColourToHex(colour), str);
	}

	// Token: 0x0600429C RID: 17052 RVA: 0x0017E6E4 File Offset: 0x0017C8E4
	public static string ColourToHex(Color32 colour)
	{
		return string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", new object[] { colour.r, colour.g, colour.b, colour.a });
	}

	// Token: 0x0600429D RID: 17053 RVA: 0x0017E73C File Offset: 0x0017C93C
	public static string GetFormattedDecor(float value, bool enforce_max = false)
	{
		string text = "";
		LocString locString = ((value > DecorMonitor.MAXIMUM_DECOR_VALUE && enforce_max) ? UI.OVERLAYS.DECOR.MAXIMUM_DECOR : UI.OVERLAYS.DECOR.VALUE);
		if (enforce_max)
		{
			value = Math.Min(value, DecorMonitor.MAXIMUM_DECOR_VALUE);
		}
		if (value > 0f)
		{
			text = "+";
		}
		else if (value >= 0f)
		{
			locString = UI.OVERLAYS.DECOR.VALUE_ZERO;
		}
		return string.Format(locString, text, value);
	}

	// Token: 0x0600429E RID: 17054 RVA: 0x0017E7A8 File Offset: 0x0017C9A8
	public static Color GetDecorColourFromValue(int decor)
	{
		Color color = Color.black;
		float num = (float)decor / 100f;
		if (num > 0f)
		{
			color = Color.Lerp(new Color(0.15f, 0f, 0f), new Color(0f, 1f, 0f), Mathf.Abs(num));
		}
		else
		{
			color = Color.Lerp(new Color(0.15f, 0f, 0f), new Color(1f, 0f, 0f), Mathf.Abs(num));
		}
		return color;
	}

	// Token: 0x0600429F RID: 17055 RVA: 0x0017E838 File Offset: 0x0017CA38
	public static List<Descriptor> GetMaterialDescriptors(Element element)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (element.attributeModifiers.Count > 0)
		{
			foreach (AttributeModifier attributeModifier in element.attributeModifiers)
			{
				string text = string.Format(Strings.Get(new StringKey("STRINGS.ELEMENTS.MATERIAL_MODIFIERS." + attributeModifier.AttributeId.ToUpper())), attributeModifier.GetFormattedString());
				string text2 = string.Format(Strings.Get(new StringKey("STRINGS.ELEMENTS.MATERIAL_MODIFIERS.TOOLTIP." + attributeModifier.AttributeId.ToUpper())), attributeModifier.GetFormattedString());
				Descriptor descriptor = default(Descriptor);
				descriptor.SetupDescriptor(text, text2, Descriptor.DescriptorType.Effect);
				descriptor.IncreaseIndent();
				list.Add(descriptor);
			}
		}
		list.AddRange(GameUtil.GetSignificantMaterialPropertyDescriptors(element));
		return list;
	}

	// Token: 0x060042A0 RID: 17056 RVA: 0x0017E934 File Offset: 0x0017CB34
	public static string GetMaterialTooltips(Element element)
	{
		string text = element.tag.ProperName();
		foreach (AttributeModifier attributeModifier in element.attributeModifiers)
		{
			string name = Db.Get().BuildingAttributes.Get(attributeModifier.AttributeId).Name;
			string formattedString = attributeModifier.GetFormattedString();
			text = text + "\n    • " + string.Format(DUPLICANTS.MODIFIERS.MODIFIER_FORMAT, name, formattedString);
		}
		text += GameUtil.GetSignificantMaterialPropertyTooltips(element);
		return text;
	}

	// Token: 0x060042A1 RID: 17057 RVA: 0x0017E9DC File Offset: 0x0017CBDC
	public static string GetSignificantMaterialPropertyTooltips(Element element)
	{
		string text = "";
		List<Descriptor> significantMaterialPropertyDescriptors = GameUtil.GetSignificantMaterialPropertyDescriptors(element);
		if (significantMaterialPropertyDescriptors.Count > 0)
		{
			text += "\n";
			for (int i = 0; i < significantMaterialPropertyDescriptors.Count; i++)
			{
				text = text + "    • " + Util.StripTextFormatting(significantMaterialPropertyDescriptors[i].text) + "\n";
			}
		}
		return text;
	}

	// Token: 0x060042A2 RID: 17058 RVA: 0x0017EA40 File Offset: 0x0017CC40
	public static List<Descriptor> GetSignificantMaterialPropertyDescriptors(Element element)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (element.thermalConductivity > 10f)
		{
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(string.Format(ELEMENTS.MATERIAL_MODIFIERS.HIGH_THERMAL_CONDUCTIVITY, GameUtil.GetThermalConductivityString(element, false, false)), string.Format(ELEMENTS.MATERIAL_MODIFIERS.TOOLTIP.HIGH_THERMAL_CONDUCTIVITY, element.name, element.thermalConductivity.ToString("0.#####")), Descriptor.DescriptorType.Effect);
			descriptor.IncreaseIndent();
			list.Add(descriptor);
		}
		if (element.thermalConductivity < 1f)
		{
			Descriptor descriptor2 = default(Descriptor);
			descriptor2.SetupDescriptor(string.Format(ELEMENTS.MATERIAL_MODIFIERS.LOW_THERMAL_CONDUCTIVITY, GameUtil.GetThermalConductivityString(element, false, false)), string.Format(ELEMENTS.MATERIAL_MODIFIERS.TOOLTIP.LOW_THERMAL_CONDUCTIVITY, element.name, element.thermalConductivity.ToString("0.#####")), Descriptor.DescriptorType.Effect);
			descriptor2.IncreaseIndent();
			list.Add(descriptor2);
		}
		if (element.specificHeatCapacity <= 0.2f)
		{
			Descriptor descriptor3 = default(Descriptor);
			descriptor3.SetupDescriptor(ELEMENTS.MATERIAL_MODIFIERS.LOW_SPECIFIC_HEAT_CAPACITY, string.Format(ELEMENTS.MATERIAL_MODIFIERS.TOOLTIP.LOW_SPECIFIC_HEAT_CAPACITY, element.name, element.specificHeatCapacity * 1f), Descriptor.DescriptorType.Effect);
			descriptor3.IncreaseIndent();
			list.Add(descriptor3);
		}
		if (element.specificHeatCapacity >= 1f)
		{
			Descriptor descriptor4 = default(Descriptor);
			descriptor4.SetupDescriptor(ELEMENTS.MATERIAL_MODIFIERS.HIGH_SPECIFIC_HEAT_CAPACITY, string.Format(ELEMENTS.MATERIAL_MODIFIERS.TOOLTIP.HIGH_SPECIFIC_HEAT_CAPACITY, element.name, element.specificHeatCapacity * 1f), Descriptor.DescriptorType.Effect);
			descriptor4.IncreaseIndent();
			list.Add(descriptor4);
		}
		if (Sim.IsRadiationEnabled() && element.radiationAbsorptionFactor >= 0.8f)
		{
			Descriptor descriptor5 = default(Descriptor);
			descriptor5.SetupDescriptor(ELEMENTS.MATERIAL_MODIFIERS.EXCELLENT_RADIATION_SHIELD, string.Format(ELEMENTS.MATERIAL_MODIFIERS.TOOLTIP.EXCELLENT_RADIATION_SHIELD, element.name, element.radiationAbsorptionFactor), Descriptor.DescriptorType.Effect);
			descriptor5.IncreaseIndent();
			list.Add(descriptor5);
		}
		return list;
	}

	// Token: 0x060042A3 RID: 17059 RVA: 0x0017EC3B File Offset: 0x0017CE3B
	public static int NaturalBuildingCell(this KMonoBehaviour cmp)
	{
		return Grid.PosToCell(cmp.transform.GetPosition());
	}

	// Token: 0x060042A4 RID: 17060 RVA: 0x0017EC50 File Offset: 0x0017CE50
	public static List<Descriptor> GetMaterialDescriptors(Tag tag)
	{
		List<Descriptor> list = new List<Descriptor>();
		Element element = ElementLoader.GetElement(tag);
		if (element != null)
		{
			if (element.attributeModifiers.Count > 0)
			{
				foreach (AttributeModifier attributeModifier in element.attributeModifiers)
				{
					string text = string.Format(Strings.Get(new StringKey("STRINGS.ELEMENTS.MATERIAL_MODIFIERS." + attributeModifier.AttributeId.ToUpper())), attributeModifier.GetFormattedString());
					string text2 = string.Format(Strings.Get(new StringKey("STRINGS.ELEMENTS.MATERIAL_MODIFIERS.TOOLTIP." + attributeModifier.AttributeId.ToUpper())), attributeModifier.GetFormattedString());
					Descriptor descriptor = default(Descriptor);
					descriptor.SetupDescriptor(text, text2, Descriptor.DescriptorType.Effect);
					descriptor.IncreaseIndent();
					list.Add(descriptor);
				}
			}
			list.AddRange(GameUtil.GetSignificantMaterialPropertyDescriptors(element));
		}
		else
		{
			GameObject gameObject = Assets.TryGetPrefab(tag);
			if (gameObject != null)
			{
				PrefabAttributeModifiers component = gameObject.GetComponent<PrefabAttributeModifiers>();
				if (component != null)
				{
					foreach (AttributeModifier attributeModifier2 in component.descriptors)
					{
						string text3 = string.Format(Strings.Get(new StringKey("STRINGS.ELEMENTS.MATERIAL_MODIFIERS." + attributeModifier2.AttributeId.ToUpper())), attributeModifier2.GetFormattedString());
						string text4 = string.Format(Strings.Get(new StringKey("STRINGS.ELEMENTS.MATERIAL_MODIFIERS.TOOLTIP." + attributeModifier2.AttributeId.ToUpper())), attributeModifier2.GetFormattedString());
						Descriptor descriptor2 = default(Descriptor);
						descriptor2.SetupDescriptor(text3, text4, Descriptor.DescriptorType.Effect);
						descriptor2.IncreaseIndent();
						list.Add(descriptor2);
					}
				}
			}
		}
		return list;
	}

	// Token: 0x060042A5 RID: 17061 RVA: 0x0017EE58 File Offset: 0x0017D058
	public static string GetMaterialTooltips(Tag tag)
	{
		string text = tag.ProperName();
		Element element = ElementLoader.GetElement(tag);
		if (element != null)
		{
			foreach (AttributeModifier attributeModifier in element.attributeModifiers)
			{
				string name = Db.Get().BuildingAttributes.Get(attributeModifier.AttributeId).Name;
				string formattedString = attributeModifier.GetFormattedString();
				text = text + "\n    • " + string.Format(DUPLICANTS.MODIFIERS.MODIFIER_FORMAT, name, formattedString);
			}
			text += GameUtil.GetSignificantMaterialPropertyTooltips(element);
		}
		else
		{
			GameObject gameObject = Assets.TryGetPrefab(tag);
			if (gameObject != null)
			{
				PrefabAttributeModifiers component = gameObject.GetComponent<PrefabAttributeModifiers>();
				if (component != null)
				{
					foreach (AttributeModifier attributeModifier2 in component.descriptors)
					{
						string name2 = Db.Get().BuildingAttributes.Get(attributeModifier2.AttributeId).Name;
						string formattedString2 = attributeModifier2.GetFormattedString();
						text = text + "\n    • " + string.Format(DUPLICANTS.MODIFIERS.MODIFIER_FORMAT, name2, formattedString2);
					}
				}
			}
		}
		return text;
	}

	// Token: 0x060042A6 RID: 17062 RVA: 0x0017EFB8 File Offset: 0x0017D1B8
	public static bool AreChoresUIMergeable(Chore.Precondition.Context choreA, Chore.Precondition.Context choreB)
	{
		if (choreA.chore.target.isNull || choreB.chore.target.isNull)
		{
			return false;
		}
		ChoreType choreType = choreB.chore.choreType;
		ChoreType choreType2 = choreA.chore.choreType;
		return (choreA.chore.choreType == choreB.chore.choreType && choreA.chore.target.GetComponent<KPrefabID>().PrefabTag == choreB.chore.target.GetComponent<KPrefabID>().PrefabTag) || (choreA.chore.choreType == Db.Get().ChoreTypes.Dig && choreB.chore.choreType == Db.Get().ChoreTypes.Dig) || (choreA.chore.choreType == Db.Get().ChoreTypes.Relax && choreB.chore.choreType == Db.Get().ChoreTypes.Relax) || ((choreType2 == Db.Get().ChoreTypes.ReturnSuitIdle || choreType2 == Db.Get().ChoreTypes.ReturnSuitUrgent) && (choreType == Db.Get().ChoreTypes.ReturnSuitIdle || choreType == Db.Get().ChoreTypes.ReturnSuitUrgent)) || (choreA.chore.target.gameObject == choreB.chore.target.gameObject && choreA.chore.choreType == choreB.chore.choreType);
	}

	// Token: 0x060042A7 RID: 17063 RVA: 0x0017F150 File Offset: 0x0017D350
	public static string GetChoreName(Chore chore, object choreData)
	{
		string text = "";
		if (chore.choreType == Db.Get().ChoreTypes.Fetch || chore.choreType == Db.Get().ChoreTypes.MachineFetch || chore.choreType == Db.Get().ChoreTypes.FabricateFetch || chore.choreType == Db.Get().ChoreTypes.FetchCritical || chore.choreType == Db.Get().ChoreTypes.PowerFetch)
		{
			text = chore.GetReportName(chore.gameObject.GetProperName());
		}
		else if (chore.choreType == Db.Get().ChoreTypes.StorageFetch || chore.choreType == Db.Get().ChoreTypes.FoodFetch)
		{
			FetchChore fetchChore = chore as FetchChore;
			FetchAreaChore fetchAreaChore = chore as FetchAreaChore;
			if (fetchAreaChore != null)
			{
				GameObject getFetchTarget = fetchAreaChore.GetFetchTarget;
				KMonoBehaviour kmonoBehaviour = choreData as KMonoBehaviour;
				if (getFetchTarget != null)
				{
					text = chore.GetReportName(getFetchTarget.GetProperName());
				}
				else if (kmonoBehaviour != null)
				{
					text = chore.GetReportName(kmonoBehaviour.GetProperName());
				}
				else
				{
					text = chore.GetReportName(null);
				}
			}
			else if (fetchChore != null)
			{
				Pickupable fetchTarget = fetchChore.fetchTarget;
				KMonoBehaviour kmonoBehaviour2 = choreData as KMonoBehaviour;
				if (fetchTarget != null)
				{
					text = chore.GetReportName(fetchTarget.GetProperName());
				}
				else if (kmonoBehaviour2 != null)
				{
					text = chore.GetReportName(kmonoBehaviour2.GetProperName());
				}
				else
				{
					text = chore.GetReportName(null);
				}
			}
		}
		else
		{
			text = chore.GetReportName(null);
		}
		return text;
	}

	// Token: 0x060042A8 RID: 17064 RVA: 0x0017F2D4 File Offset: 0x0017D4D4
	public static string ChoreGroupsForChoreType(ChoreType choreType)
	{
		if (choreType.groups == null || choreType.groups.Length == 0)
		{
			return null;
		}
		string text = "";
		for (int i = 0; i < choreType.groups.Length; i++)
		{
			if (i != 0)
			{
				text += UI.UISIDESCREENS.MINIONTODOSIDESCREEN.CHORE_GROUP_SEPARATOR;
			}
			text += choreType.groups[i].Name;
		}
		return text;
	}

	// Token: 0x060042A9 RID: 17065 RVA: 0x0017F338 File Offset: 0x0017D538
	public static List<BuildingDef> GetBuildingsRequiringSkillPerk(string perkID)
	{
		return Assets.BuildingDefs.Where((BuildingDef building) => building.RequiredSkillPerkID == perkID).ToList<BuildingDef>();
	}

	// Token: 0x060042AA RID: 17066 RVA: 0x0017F370 File Offset: 0x0017D570
	public static string NamesOfBuildingsRequiringSkillPerk(string perkID)
	{
		List<string> list = (from building in GameUtil.GetBuildingsRequiringSkillPerk(perkID)
			select GameUtil.SafeStringFormat(UI.ROLES_SCREEN.PERKS.CAN_USE_BUILDING.DESCRIPTION, new object[] { building.Name })).ToList<string>();
		if (list == null || list.Count == 0)
		{
			return null;
		}
		return string.Join("\n", list);
	}

	// Token: 0x060042AB RID: 17067 RVA: 0x0017F3C8 File Offset: 0x0017D5C8
	public static string NamesOfBoostersWithSkillPerk(string perkID)
	{
		List<string> list = (from tag in BionicUpgradeComponentConfig.GetBoostersWithSkillPerk(perkID)
			select Strings.Get(string.Format("STRINGS.ITEMS.BIONIC_BOOSTERS.{0}.NAME", tag.ToString().ToUpper())).String).ToList<string>();
		return string.Join("\n", list);
	}

	// Token: 0x060042AC RID: 17068 RVA: 0x0017F410 File Offset: 0x0017D610
	public static string NamesOfSkillsWithSkillPerk(string perkID)
	{
		List<string> list = (from match in Db.Get().Skills.resources
			where !match.deprecated && match.GivesPerk(perkID)
			select match.Name).ToList<string>();
		return string.Join("\n", list.ToArray());
	}

	// Token: 0x060042AD RID: 17069 RVA: 0x0017F484 File Offset: 0x0017D684
	public static bool IsCapturingTimeLapse()
	{
		return Game.Instance != null && Game.Instance.timelapser != null && Game.Instance.timelapser.CapturingTimelapseScreenshot;
	}

	// Token: 0x060042AE RID: 17070 RVA: 0x0017F4B8 File Offset: 0x0017D6B8
	public static ExposureType GetExposureTypeForDisease(Disease disease)
	{
		for (int i = 0; i < GERM_EXPOSURE.TYPES.Length; i++)
		{
			if (disease.id == GERM_EXPOSURE.TYPES[i].germ_id)
			{
				return GERM_EXPOSURE.TYPES[i];
			}
		}
		return null;
	}

	// Token: 0x060042AF RID: 17071 RVA: 0x0017F500 File Offset: 0x0017D700
	public static Sickness GetSicknessForDisease(Disease disease)
	{
		int i = 0;
		while (i < GERM_EXPOSURE.TYPES.Length)
		{
			if (disease.id == GERM_EXPOSURE.TYPES[i].germ_id)
			{
				if (GERM_EXPOSURE.TYPES[i].sickness_id == null)
				{
					return null;
				}
				return Db.Get().Sicknesses.Get(GERM_EXPOSURE.TYPES[i].sickness_id);
			}
			else
			{
				i++;
			}
		}
		return null;
	}

	// Token: 0x060042B0 RID: 17072 RVA: 0x0017F56A File Offset: 0x0017D76A
	public static void SubscribeToTags<T>(T target, EventSystem.IntraObjectHandler<T> handler, bool triggerImmediately) where T : KMonoBehaviour
	{
		if (triggerImmediately)
		{
			handler.Trigger(target.gameObject, new TagChangedEventData(Tag.Invalid, false));
		}
		target.Subscribe<T>(-1582839653, handler);
	}

	// Token: 0x060042B1 RID: 17073 RVA: 0x0017F5A2 File Offset: 0x0017D7A2
	public static void UnsubscribeToTags<T>(T target, EventSystem.IntraObjectHandler<T> handler) where T : KMonoBehaviour
	{
		target.Unsubscribe<T>(-1582839653, handler, false);
	}

	// Token: 0x060042B2 RID: 17074 RVA: 0x0017F5B6 File Offset: 0x0017D7B6
	public static EventSystem.IntraObjectHandler<T> CreateHasTagHandler<T>(Tag tag, Action<T, object> callback) where T : KMonoBehaviour
	{
		return new EventSystem.IntraObjectHandler<T>(delegate(T component, object data)
		{
			TagChangedEventData tagChangedEventData = (TagChangedEventData)data;
			if (tagChangedEventData.tag == Tag.Invalid)
			{
				KPrefabID component2 = component.GetComponent<KPrefabID>();
				tagChangedEventData = new TagChangedEventData(tag, component2.HasTag(tag));
			}
			if (tagChangedEventData.tag == tag && tagChangedEventData.added)
			{
				callback(component, data);
			}
		});
	}

	// Token: 0x04002C90 RID: 11408
	public static GameUtil.TemperatureUnit temperatureUnit;

	// Token: 0x04002C91 RID: 11409
	public static GameUtil.MassUnit massUnit;

	// Token: 0x04002C92 RID: 11410
	private static string[] adjectives;

	// Token: 0x04002C93 RID: 11411
	public static ThreadLocal<Queue<GameUtil.FloodFillInfo>> FloodFillNext = new ThreadLocal<Queue<GameUtil.FloodFillInfo>>(() => new Queue<GameUtil.FloodFillInfo>());

	// Token: 0x04002C94 RID: 11412
	public static ThreadLocal<HashSet<int>> FloodFillVisited = new ThreadLocal<HashSet<int>>(() => new HashSet<int>());

	// Token: 0x04002C95 RID: 11413
	public static ThreadLocal<List<int>> FloodFillNeighbors = new ThreadLocal<List<int>>(() => new List<int>(4) { -1, -1, -1, -1 });

	// Token: 0x04002C96 RID: 11414
	public static TagSet foodTags = new TagSet(new string[]
	{
		"BasicPlantFood",
		"MushBar",
		"ColdWheatSeed",
		"ColdWheatSeed",
		"SpiceNut",
		"PrickleFruit",
		"Meat",
		"Mushroom",
		"ColdWheat",
		GameTags.Compostable.Name
	});

	// Token: 0x04002C97 RID: 11415
	public static TagSet solidTags = new TagSet(new string[] { "Filter", "Coal", "BasicFabric", "SwampLilyFlower", "RefinedMetal" });

	// Token: 0x020018F4 RID: 6388
	public enum UnitClass
	{
		// Token: 0x04007AA3 RID: 31395
		SimpleFloat,
		// Token: 0x04007AA4 RID: 31396
		SimpleInteger,
		// Token: 0x04007AA5 RID: 31397
		Temperature,
		// Token: 0x04007AA6 RID: 31398
		Mass,
		// Token: 0x04007AA7 RID: 31399
		Calories,
		// Token: 0x04007AA8 RID: 31400
		Percent,
		// Token: 0x04007AA9 RID: 31401
		Distance,
		// Token: 0x04007AAA RID: 31402
		Disease,
		// Token: 0x04007AAB RID: 31403
		Radiation,
		// Token: 0x04007AAC RID: 31404
		Energy,
		// Token: 0x04007AAD RID: 31405
		Power,
		// Token: 0x04007AAE RID: 31406
		Lux,
		// Token: 0x04007AAF RID: 31407
		Time,
		// Token: 0x04007AB0 RID: 31408
		Seconds,
		// Token: 0x04007AB1 RID: 31409
		Cycles
	}

	// Token: 0x020018F5 RID: 6389
	public enum TemperatureUnit
	{
		// Token: 0x04007AB3 RID: 31411
		Celsius,
		// Token: 0x04007AB4 RID: 31412
		Fahrenheit,
		// Token: 0x04007AB5 RID: 31413
		Kelvin
	}

	// Token: 0x020018F6 RID: 6390
	public enum MassUnit
	{
		// Token: 0x04007AB7 RID: 31415
		Kilograms,
		// Token: 0x04007AB8 RID: 31416
		Pounds
	}

	// Token: 0x020018F7 RID: 6391
	public enum MetricMassFormat
	{
		// Token: 0x04007ABA RID: 31418
		UseThreshold,
		// Token: 0x04007ABB RID: 31419
		Kilogram,
		// Token: 0x04007ABC RID: 31420
		Gram,
		// Token: 0x04007ABD RID: 31421
		Tonne
	}

	// Token: 0x020018F8 RID: 6392
	public enum TemperatureInterpretation
	{
		// Token: 0x04007ABF RID: 31423
		Absolute,
		// Token: 0x04007AC0 RID: 31424
		Relative
	}

	// Token: 0x020018F9 RID: 6393
	public enum TimeSlice
	{
		// Token: 0x04007AC2 RID: 31426
		None,
		// Token: 0x04007AC3 RID: 31427
		ModifyOnly,
		// Token: 0x04007AC4 RID: 31428
		PerSecond,
		// Token: 0x04007AC5 RID: 31429
		PerCycle
	}

	// Token: 0x020018FA RID: 6394
	public enum MeasureUnit
	{
		// Token: 0x04007AC7 RID: 31431
		mass,
		// Token: 0x04007AC8 RID: 31432
		kcal,
		// Token: 0x04007AC9 RID: 31433
		quantity
	}

	// Token: 0x020018FB RID: 6395
	public enum IdentityDescriptorTense
	{
		// Token: 0x04007ACB RID: 31435
		Normal,
		// Token: 0x04007ACC RID: 31436
		Possessive,
		// Token: 0x04007ACD RID: 31437
		Plural
	}

	// Token: 0x020018FC RID: 6396
	public enum WattageFormatterUnit
	{
		// Token: 0x04007ACF RID: 31439
		Watts,
		// Token: 0x04007AD0 RID: 31440
		Kilowatts,
		// Token: 0x04007AD1 RID: 31441
		Automatic
	}

	// Token: 0x020018FD RID: 6397
	public enum HeatEnergyFormatterUnit
	{
		// Token: 0x04007AD3 RID: 31443
		DTU_S,
		// Token: 0x04007AD4 RID: 31444
		KDTU_S,
		// Token: 0x04007AD5 RID: 31445
		Automatic
	}

	// Token: 0x020018FE RID: 6398
	public struct FloodFillInfo
	{
		// Token: 0x04007AD6 RID: 31446
		public int cell;

		// Token: 0x04007AD7 RID: 31447
		public int depth;
	}

	// Token: 0x020018FF RID: 6399
	public static class Hardness
	{
		// Token: 0x04007AD8 RID: 31448
		public const int VERY_SOFT = 0;

		// Token: 0x04007AD9 RID: 31449
		public const int SOFT = 10;

		// Token: 0x04007ADA RID: 31450
		public const int FIRM = 25;

		// Token: 0x04007ADB RID: 31451
		public const int VERY_FIRM = 50;

		// Token: 0x04007ADC RID: 31452
		public const int NEARLY_IMPENETRABLE = 150;

		// Token: 0x04007ADD RID: 31453
		public const int SUPER_DUPER_HARD = 200;

		// Token: 0x04007ADE RID: 31454
		public const int RADIOACTIVE_MATERIALS = 251;

		// Token: 0x04007ADF RID: 31455
		public const int IMPENETRABLE = 255;

		// Token: 0x04007AE0 RID: 31456
		public static Color ImpenetrableColor = new Color(0.83137256f, 0.28627452f, 0.28235295f);

		// Token: 0x04007AE1 RID: 31457
		public static Color nearlyImpenetrableColor = new Color(0.7411765f, 0.34901962f, 0.49803922f);

		// Token: 0x04007AE2 RID: 31458
		public static Color veryFirmColor = new Color(0.6392157f, 0.39215687f, 0.6039216f);

		// Token: 0x04007AE3 RID: 31459
		public static Color firmColor = new Color(0.5254902f, 0.41960785f, 0.64705884f);

		// Token: 0x04007AE4 RID: 31460
		public static Color softColor = new Color(0.42745098f, 0.48235294f, 0.75686276f);

		// Token: 0x04007AE5 RID: 31461
		public static Color verySoftColor = new Color(0.44313726f, 0.67058825f, 0.8117647f);
	}

	// Token: 0x02001900 RID: 6400
	public static class GermResistanceValues
	{
		// Token: 0x04007AE6 RID: 31462
		public const float MEDIUM = 2f;

		// Token: 0x04007AE7 RID: 31463
		public const float LARGE = 5f;

		// Token: 0x04007AE8 RID: 31464
		public static Color NegativeLargeColor = new Color(0.83137256f, 0.28627452f, 0.28235295f);

		// Token: 0x04007AE9 RID: 31465
		public static Color NegativeMediumColor = new Color(0.7411765f, 0.34901962f, 0.49803922f);

		// Token: 0x04007AEA RID: 31466
		public static Color NegativeSmallColor = new Color(0.6392157f, 0.39215687f, 0.6039216f);

		// Token: 0x04007AEB RID: 31467
		public static Color PositiveSmallColor = new Color(0.5254902f, 0.41960785f, 0.64705884f);

		// Token: 0x04007AEC RID: 31468
		public static Color PositiveMediumColor = new Color(0.42745098f, 0.48235294f, 0.75686276f);

		// Token: 0x04007AED RID: 31469
		public static Color PositiveLargeColor = new Color(0.44313726f, 0.67058825f, 0.8117647f);
	}

	// Token: 0x02001901 RID: 6401
	public static class ThermalConductivityValues
	{
		// Token: 0x04007AEE RID: 31470
		public const float VERY_HIGH = 50f;

		// Token: 0x04007AEF RID: 31471
		public const float HIGH = 10f;

		// Token: 0x04007AF0 RID: 31472
		public const float MEDIUM = 2f;

		// Token: 0x04007AF1 RID: 31473
		public const float LOW = 1f;

		// Token: 0x04007AF2 RID: 31474
		public static Color veryLowConductivityColor = new Color(0.83137256f, 0.28627452f, 0.28235295f);

		// Token: 0x04007AF3 RID: 31475
		public static Color lowConductivityColor = new Color(0.7411765f, 0.34901962f, 0.49803922f);

		// Token: 0x04007AF4 RID: 31476
		public static Color mediumConductivityColor = new Color(0.6392157f, 0.39215687f, 0.6039216f);

		// Token: 0x04007AF5 RID: 31477
		public static Color highConductivityColor = new Color(0.5254902f, 0.41960785f, 0.64705884f);

		// Token: 0x04007AF6 RID: 31478
		public static Color veryHighConductivityColor = new Color(0.42745098f, 0.48235294f, 0.75686276f);
	}

	// Token: 0x02001902 RID: 6402
	public static class BreathableValues
	{
		// Token: 0x04007AF7 RID: 31479
		public static Color positiveColor = new Color(0.44313726f, 0.67058825f, 0.8117647f);

		// Token: 0x04007AF8 RID: 31480
		public static Color warningColor = new Color(0.6392157f, 0.39215687f, 0.6039216f);

		// Token: 0x04007AF9 RID: 31481
		public static Color negativeColor = new Color(0.83137256f, 0.28627452f, 0.28235295f);
	}

	// Token: 0x02001903 RID: 6403
	public static class WireLoadValues
	{
		// Token: 0x04007AFA RID: 31482
		public static Color warningColor = new Color(0.9843137f, 0.6901961f, 0.23137255f);

		// Token: 0x04007AFB RID: 31483
		public static Color negativeColor = new Color(1f, 0.19215687f, 0.19215687f);
	}
}
