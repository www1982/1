using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020008DB RID: 2267
[DebuggerDisplay("{name}")]
[Serializable]
public class Element : IComparable<Element>
{
	// Token: 0x06003EDD RID: 16093 RVA: 0x00161F50 File Offset: 0x00160150
	public float GetRelativeHeatLevel(float currentTemperature)
	{
		float num = this.lowTemp - 3f;
		float num2 = this.highTemp + 3f;
		return Mathf.Clamp01((currentTemperature - num) / (num2 - num));
	}

	// Token: 0x06003EDE RID: 16094 RVA: 0x00161F83 File Offset: 0x00160183
	public float PressureToMass(float pressure)
	{
		return pressure / this.defaultValues.pressure;
	}

	// Token: 0x1700045D RID: 1117
	// (get) Token: 0x06003EDF RID: 16095 RVA: 0x00161F92 File Offset: 0x00160192
	public bool IsSlippery
	{
		get
		{
			return this.HasTag(GameTags.Slippery);
		}
	}

	// Token: 0x1700045E RID: 1118
	// (get) Token: 0x06003EE0 RID: 16096 RVA: 0x00161F9F File Offset: 0x0016019F
	public bool IsUnstable
	{
		get
		{
			return this.HasTag(GameTags.Unstable);
		}
	}

	// Token: 0x1700045F RID: 1119
	// (get) Token: 0x06003EE1 RID: 16097 RVA: 0x00161FAC File Offset: 0x001601AC
	public bool IsLiquid
	{
		get
		{
			return (this.state & Element.State.Solid) == Element.State.Liquid;
		}
	}

	// Token: 0x17000460 RID: 1120
	// (get) Token: 0x06003EE2 RID: 16098 RVA: 0x00161FB9 File Offset: 0x001601B9
	public bool IsGas
	{
		get
		{
			return (this.state & Element.State.Solid) == Element.State.Gas;
		}
	}

	// Token: 0x17000461 RID: 1121
	// (get) Token: 0x06003EE3 RID: 16099 RVA: 0x00161FC6 File Offset: 0x001601C6
	public bool IsSolid
	{
		get
		{
			return (this.state & Element.State.Solid) == Element.State.Solid;
		}
	}

	// Token: 0x17000462 RID: 1122
	// (get) Token: 0x06003EE4 RID: 16100 RVA: 0x00161FD3 File Offset: 0x001601D3
	public bool IsVacuum
	{
		get
		{
			return (this.state & Element.State.Solid) == Element.State.Vacuum;
		}
	}

	// Token: 0x17000463 RID: 1123
	// (get) Token: 0x06003EE5 RID: 16101 RVA: 0x00161FE0 File Offset: 0x001601E0
	public bool IsTemperatureInsulated
	{
		get
		{
			return (this.state & Element.State.TemperatureInsulated) > Element.State.Vacuum;
		}
	}

	// Token: 0x06003EE6 RID: 16102 RVA: 0x00161FEE File Offset: 0x001601EE
	public bool IsState(Element.State expected_state)
	{
		return (this.state & Element.State.Solid) == expected_state;
	}

	// Token: 0x17000464 RID: 1124
	// (get) Token: 0x06003EE7 RID: 16103 RVA: 0x00161FFB File Offset: 0x001601FB
	public bool HasTransitionUp
	{
		get
		{
			return this.highTempTransitionTarget != (SimHashes)0 && this.highTempTransitionTarget != SimHashes.Unobtanium && this.highTempTransition != null && this.highTempTransition != this;
		}
	}

	// Token: 0x17000465 RID: 1125
	// (get) Token: 0x06003EE8 RID: 16104 RVA: 0x00162028 File Offset: 0x00160228
	// (set) Token: 0x06003EE9 RID: 16105 RVA: 0x00162030 File Offset: 0x00160230
	public string name { get; set; }

	// Token: 0x17000466 RID: 1126
	// (get) Token: 0x06003EEA RID: 16106 RVA: 0x00162039 File Offset: 0x00160239
	// (set) Token: 0x06003EEB RID: 16107 RVA: 0x00162041 File Offset: 0x00160241
	public string nameUpperCase { get; set; }

	// Token: 0x17000467 RID: 1127
	// (get) Token: 0x06003EEC RID: 16108 RVA: 0x0016204A File Offset: 0x0016024A
	// (set) Token: 0x06003EED RID: 16109 RVA: 0x00162052 File Offset: 0x00160252
	public string description { get; set; }

	// Token: 0x06003EEE RID: 16110 RVA: 0x0016205B File Offset: 0x0016025B
	public string GetStateString()
	{
		return Element.GetStateString(this.state);
	}

	// Token: 0x06003EEF RID: 16111 RVA: 0x00162068 File Offset: 0x00160268
	public static string GetStateString(Element.State state)
	{
		if ((state & Element.State.Solid) == Element.State.Solid)
		{
			return ELEMENTS.STATE.SOLID;
		}
		if ((state & Element.State.Solid) == Element.State.Liquid)
		{
			return ELEMENTS.STATE.LIQUID;
		}
		if ((state & Element.State.Solid) == Element.State.Gas)
		{
			return ELEMENTS.STATE.GAS;
		}
		return ELEMENTS.STATE.VACUUM;
	}

	// Token: 0x06003EF0 RID: 16112 RVA: 0x001620A8 File Offset: 0x001602A8
	public string FullDescription(bool addHardnessColor = true)
	{
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		stringBuilder.Clear();
		stringBuilder.Append(this.Description());
		if (this.IsSolid)
		{
			stringBuilder.Append("\n\n");
			stringBuilder.AppendFormat(ELEMENTS.ELEMENTDESCSOLID, this.GetMaterialCategoryTag().ProperName(), GameUtil.GetFormattedTemperature(this.highTemp, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false), GameUtil.GetHardnessString(this, addHardnessColor));
		}
		else if (this.IsLiquid)
		{
			stringBuilder.Append("\n\n");
			stringBuilder.AppendFormat(ELEMENTS.ELEMENTDESCLIQUID, this.GetMaterialCategoryTag().ProperName(), GameUtil.GetFormattedTemperature(this.lowTemp, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false), GameUtil.GetFormattedTemperature(this.highTemp, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
		}
		else if (!this.IsVacuum)
		{
			stringBuilder.Append("\n\n");
			stringBuilder.AppendFormat(ELEMENTS.ELEMENTDESCGAS, this.GetMaterialCategoryTag().ProperName(), GameUtil.GetFormattedTemperature(this.lowTemp, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
		}
		StringBuilder stringBuilder2 = GlobalStringBuilderPool.Alloc();
		stringBuilder2.Append(ELEMENTS.THERMALPROPERTIES);
		stringBuilder2.Replace("{SPECIFIC_HEAT_CAPACITY}", GameUtil.GetFormattedSHC(this.specificHeatCapacity));
		stringBuilder2.Replace("{THERMAL_CONDUCTIVITY}", GameUtil.GetFormattedThermalConductivity(this.thermalConductivity));
		stringBuilder.Append("\n");
		stringBuilder.Append(stringBuilder2.ToString());
		GlobalStringBuilderPool.Free(stringBuilder2);
		if (DlcManager.FeatureRadiationEnabled())
		{
			stringBuilder.Append("\n");
			stringBuilder.AppendFormat(ELEMENTS.RADIATIONPROPERTIES, this.radiationAbsorptionFactor, GameUtil.GetFormattedRads(this.radiationPer1000Mass * 1.1f / 600f, GameUtil.TimeSlice.PerCycle));
		}
		if (this.oreTags.Length != 0 && !this.IsVacuum)
		{
			stringBuilder.Append("\n\n");
			StringBuilder stringBuilder3 = GlobalStringBuilderPool.Alloc();
			for (int i = 0; i < this.oreTags.Length; i++)
			{
				Tag tag = new Tag(this.oreTags[i]);
				if (!GameTags.HiddenElementTags.Contains(tag))
				{
					stringBuilder3.Append(tag.ProperName());
					if (i < this.oreTags.Length - 1)
					{
						stringBuilder3.Append(", ");
					}
				}
			}
			stringBuilder.AppendFormat(ELEMENTS.ELEMENTPROPERTIES, GlobalStringBuilderPool.ReturnAndFree(stringBuilder3));
		}
		if (this.attributeModifiers.Count > 0)
		{
			foreach (AttributeModifier attributeModifier in this.attributeModifiers)
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendFormat(DUPLICANTS.MODIFIERS.MODIFIER_FORMAT, Db.Get().BuildingAttributes.Get(attributeModifier.AttributeId).Name, attributeModifier.GetFormattedString());
			}
		}
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}

	// Token: 0x06003EF1 RID: 16113 RVA: 0x00162380 File Offset: 0x00160580
	public string Description()
	{
		return this.description;
	}

	// Token: 0x06003EF2 RID: 16114 RVA: 0x00162388 File Offset: 0x00160588
	public bool HasTag(Tag search_tag)
	{
		return this.tag == search_tag || Array.IndexOf<Tag>(this.oreTags, search_tag) != -1;
	}

	// Token: 0x06003EF3 RID: 16115 RVA: 0x001623AC File Offset: 0x001605AC
	public Tag GetMaterialCategoryTag()
	{
		return this.materialCategory;
	}

	// Token: 0x06003EF4 RID: 16116 RVA: 0x001623B4 File Offset: 0x001605B4
	public int CompareTo(Element other)
	{
		return this.id - other.id;
	}

	// Token: 0x040026E6 RID: 9958
	public const int INVALID_ID = 0;

	// Token: 0x040026E7 RID: 9959
	public SimHashes id;

	// Token: 0x040026E8 RID: 9960
	public Tag tag;

	// Token: 0x040026E9 RID: 9961
	public ushort idx;

	// Token: 0x040026EA RID: 9962
	public float specificHeatCapacity;

	// Token: 0x040026EB RID: 9963
	public float thermalConductivity = 1f;

	// Token: 0x040026EC RID: 9964
	public float molarMass = 1f;

	// Token: 0x040026ED RID: 9965
	public float strength;

	// Token: 0x040026EE RID: 9966
	public float flow;

	// Token: 0x040026EF RID: 9967
	public float maxCompression;

	// Token: 0x040026F0 RID: 9968
	public float viscosity;

	// Token: 0x040026F1 RID: 9969
	public float minHorizontalFlow = float.PositiveInfinity;

	// Token: 0x040026F2 RID: 9970
	public float minVerticalFlow = float.PositiveInfinity;

	// Token: 0x040026F3 RID: 9971
	public float maxMass = 10000f;

	// Token: 0x040026F4 RID: 9972
	public float solidSurfaceAreaMultiplier;

	// Token: 0x040026F5 RID: 9973
	public float liquidSurfaceAreaMultiplier;

	// Token: 0x040026F6 RID: 9974
	public float gasSurfaceAreaMultiplier;

	// Token: 0x040026F7 RID: 9975
	public Element.State state;

	// Token: 0x040026F8 RID: 9976
	public byte hardness;

	// Token: 0x040026F9 RID: 9977
	public float lowTemp;

	// Token: 0x040026FA RID: 9978
	public SimHashes lowTempTransitionTarget;

	// Token: 0x040026FB RID: 9979
	public Element lowTempTransition;

	// Token: 0x040026FC RID: 9980
	public float highTemp;

	// Token: 0x040026FD RID: 9981
	public SimHashes highTempTransitionTarget;

	// Token: 0x040026FE RID: 9982
	public Element highTempTransition;

	// Token: 0x040026FF RID: 9983
	public SimHashes highTempTransitionOreID = SimHashes.Vacuum;

	// Token: 0x04002700 RID: 9984
	public float highTempTransitionOreMassConversion;

	// Token: 0x04002701 RID: 9985
	public SimHashes lowTempTransitionOreID = SimHashes.Vacuum;

	// Token: 0x04002702 RID: 9986
	public float lowTempTransitionOreMassConversion;

	// Token: 0x04002703 RID: 9987
	public SimHashes sublimateId;

	// Token: 0x04002704 RID: 9988
	public SimHashes convertId;

	// Token: 0x04002705 RID: 9989
	public SpawnFXHashes sublimateFX;

	// Token: 0x04002706 RID: 9990
	public float sublimateRate;

	// Token: 0x04002707 RID: 9991
	public float sublimateEfficiency;

	// Token: 0x04002708 RID: 9992
	public float sublimateProbability;

	// Token: 0x04002709 RID: 9993
	public float offGasPercentage;

	// Token: 0x0400270A RID: 9994
	public float lightAbsorptionFactor;

	// Token: 0x0400270B RID: 9995
	public float radiationAbsorptionFactor;

	// Token: 0x0400270C RID: 9996
	public float radiationPer1000Mass;

	// Token: 0x0400270D RID: 9997
	public Sim.PhysicsData defaultValues;

	// Token: 0x0400270E RID: 9998
	public SimHashes refinedMetalTarget;

	// Token: 0x0400270F RID: 9999
	public float toxicity;

	// Token: 0x04002710 RID: 10000
	public Substance substance;

	// Token: 0x04002711 RID: 10001
	public Tag materialCategory;

	// Token: 0x04002712 RID: 10002
	public int buildMenuSort;

	// Token: 0x04002713 RID: 10003
	public ElementLoader.ElementComposition[] elementComposition;

	// Token: 0x04002714 RID: 10004
	public Tag[] oreTags = new Tag[0];

	// Token: 0x04002715 RID: 10005
	public List<AttributeModifier> attributeModifiers = new List<AttributeModifier>();

	// Token: 0x04002716 RID: 10006
	public bool disabled;

	// Token: 0x04002717 RID: 10007
	public string dlcId;

	// Token: 0x04002718 RID: 10008
	public const byte StateMask = 3;

	// Token: 0x02001886 RID: 6278
	[Serializable]
	public enum State : byte
	{
		// Token: 0x0400792A RID: 31018
		Vacuum,
		// Token: 0x0400792B RID: 31019
		Gas,
		// Token: 0x0400792C RID: 31020
		Liquid,
		// Token: 0x0400792D RID: 31021
		Solid,
		// Token: 0x0400792E RID: 31022
		Unbreakable,
		// Token: 0x0400792F RID: 31023
		Unstable = 8,
		// Token: 0x04007930 RID: 31024
		TemperatureInsulated = 16
	}
}
