using System;
using System.Collections.Generic;
using Klei;
using UnityEngine;

// Token: 0x02000939 RID: 2361
[AddComponentMenu("KMonoBehaviour/scripts/GeyserConfigurator")]
public class GeyserConfigurator : KMonoBehaviour
{
	// Token: 0x0600432D RID: 17197 RVA: 0x00181A24 File Offset: 0x0017FC24
	public static GeyserConfigurator.GeyserType FindType(HashedString typeId)
	{
		GeyserConfigurator.GeyserType geyserType = null;
		if (typeId != HashedString.Invalid)
		{
			geyserType = GeyserConfigurator.geyserTypes.Find((GeyserConfigurator.GeyserType t) => t.id == typeId);
		}
		if (geyserType == null)
		{
			global::Debug.LogError(string.Format("Tried finding a geyser with id {0} but it doesn't exist!", typeId.ToString()));
		}
		return geyserType;
	}

	// Token: 0x0600432E RID: 17198 RVA: 0x00181A8D File Offset: 0x0017FC8D
	public GeyserConfigurator.GeyserInstanceConfiguration MakeConfiguration()
	{
		return this.CreateRandomInstance(this.presetType, this.presetMin, this.presetMax);
	}

	// Token: 0x0600432F RID: 17199 RVA: 0x00181AA8 File Offset: 0x0017FCA8
	private GeyserConfigurator.GeyserInstanceConfiguration CreateRandomInstance(HashedString typeId, float min, float max)
	{
		KRandom krandom = new KRandom(SaveLoader.Instance.clusterDetailSave.globalWorldSeed + (int)base.transform.GetPosition().x + (int)base.transform.GetPosition().y);
		return new GeyserConfigurator.GeyserInstanceConfiguration
		{
			typeId = typeId,
			rateRoll = this.Roll(krandom, min, max),
			iterationLengthRoll = this.Roll(krandom, 0f, 1f),
			iterationPercentRoll = this.Roll(krandom, min, max),
			yearLengthRoll = this.Roll(krandom, 0f, 1f),
			yearPercentRoll = this.Roll(krandom, min, max)
		};
	}

	// Token: 0x06004330 RID: 17200 RVA: 0x00181B55 File Offset: 0x0017FD55
	private float Roll(KRandom randomSource, float min, float max)
	{
		return (float)(randomSource.NextDouble() * (double)(max - min)) + min;
	}

	// Token: 0x04002CCF RID: 11471
	private static List<GeyserConfigurator.GeyserType> geyserTypes;

	// Token: 0x04002CD0 RID: 11472
	public HashedString presetType;

	// Token: 0x04002CD1 RID: 11473
	public float presetMin;

	// Token: 0x04002CD2 RID: 11474
	public float presetMax = 1f;

	// Token: 0x0200191C RID: 6428
	public enum GeyserShape
	{
		// Token: 0x04007B3C RID: 31548
		Gas,
		// Token: 0x04007B3D RID: 31549
		Liquid,
		// Token: 0x04007B3E RID: 31550
		Molten
	}

	// Token: 0x0200191D RID: 6429
	public class GeyserType : IHasDlcRestrictions
	{
		// Token: 0x06009E51 RID: 40529 RVA: 0x003967FC File Offset: 0x003949FC
		public string[] GetRequiredDlcIds()
		{
			return this.requiredDlcIds;
		}

		// Token: 0x06009E52 RID: 40530 RVA: 0x00396804 File Offset: 0x00394A04
		public string[] GetForbiddenDlcIds()
		{
			return this.forbiddenDlcIds;
		}

		// Token: 0x06009E53 RID: 40531 RVA: 0x0039680C File Offset: 0x00394A0C
		public GeyserType(string id, SimHashes element, GeyserConfigurator.GeyserShape shape, float temperature, float minRatePerCycle, float maxRatePerCycle, float maxPressure, string[] requiredDlcIds, string[] forbiddenDlcIds = null, float minIterationLength = 60f, float maxIterationLength = 1140f, float minIterationPercent = 0.1f, float maxIterationPercent = 0.9f, float minYearLength = 15000f, float maxYearLength = 135000f, float minYearPercent = 0.4f, float maxYearPercent = 0.8f, float geyserTemperature = 372.15f)
		{
			this.id = id;
			this.idHash = id;
			this.element = element;
			this.shape = shape;
			this.temperature = temperature;
			this.minRatePerCycle = minRatePerCycle;
			this.maxRatePerCycle = maxRatePerCycle;
			this.maxPressure = maxPressure;
			this.minIterationLength = minIterationLength;
			this.maxIterationLength = maxIterationLength;
			this.minIterationPercent = minIterationPercent;
			this.maxIterationPercent = maxIterationPercent;
			this.minYearLength = minYearLength;
			this.maxYearLength = maxYearLength;
			this.minYearPercent = minYearPercent;
			this.maxYearPercent = maxYearPercent;
			this.requiredDlcIds = requiredDlcIds;
			this.forbiddenDlcIds = forbiddenDlcIds;
			this.geyserTemperature = geyserTemperature;
			if (GeyserConfigurator.geyserTypes == null)
			{
				GeyserConfigurator.geyserTypes = new List<GeyserConfigurator.GeyserType>();
			}
			GeyserConfigurator.geyserTypes.Add(this);
		}

		// Token: 0x06009E54 RID: 40532 RVA: 0x003968E0 File Offset: 0x00394AE0
		[Obsolete]
		public GeyserType(string id, SimHashes element, GeyserConfigurator.GeyserShape shape, float temperature, float minRatePerCycle, float maxRatePerCycle, float maxPressure, float minIterationLength = 60f, float maxIterationLength = 1140f, float minIterationPercent = 0.1f, float maxIterationPercent = 0.9f, float minYearLength = 15000f, float maxYearLength = 135000f, float minYearPercent = 0.4f, float maxYearPercent = 0.8f, float geyserTemperature = 372.15f, string DlcID = "")
		{
			this.id = id;
			this.idHash = id;
			this.element = element;
			this.shape = shape;
			this.temperature = temperature;
			this.minRatePerCycle = minRatePerCycle;
			this.maxRatePerCycle = maxRatePerCycle;
			this.maxPressure = maxPressure;
			this.minIterationLength = minIterationLength;
			this.maxIterationLength = maxIterationLength;
			this.minIterationPercent = minIterationPercent;
			this.maxIterationPercent = maxIterationPercent;
			this.minYearLength = minYearLength;
			this.maxYearLength = maxYearLength;
			this.minYearPercent = minYearPercent;
			this.maxYearPercent = maxYearPercent;
			this.requiredDlcIds = new string[] { DlcID };
			this.geyserTemperature = geyserTemperature;
			if (GeyserConfigurator.geyserTypes == null)
			{
				GeyserConfigurator.geyserTypes = new List<GeyserConfigurator.GeyserType>();
			}
			GeyserConfigurator.geyserTypes.Add(this);
		}

		// Token: 0x06009E55 RID: 40533 RVA: 0x003969B4 File Offset: 0x00394BB4
		public GeyserConfigurator.GeyserType AddDisease(SimUtil.DiseaseInfo diseaseInfo)
		{
			this.diseaseInfo = diseaseInfo;
			return this;
		}

		// Token: 0x06009E56 RID: 40534 RVA: 0x003969C0 File Offset: 0x00394BC0
		public GeyserType()
		{
			this.id = "Blank";
			this.element = SimHashes.Void;
			this.temperature = 0f;
			this.minRatePerCycle = 0f;
			this.maxRatePerCycle = 0f;
			this.maxPressure = 0f;
			this.minIterationLength = 0f;
			this.maxIterationLength = 0f;
			this.minIterationPercent = 0f;
			this.maxIterationPercent = 0f;
			this.minYearLength = 0f;
			this.maxYearLength = 0f;
			this.minYearPercent = 0f;
			this.maxYearPercent = 0f;
			this.geyserTemperature = 0f;
		}

		// Token: 0x04007B3F RID: 31551
		public string id;

		// Token: 0x04007B40 RID: 31552
		public HashedString idHash;

		// Token: 0x04007B41 RID: 31553
		public SimHashes element;

		// Token: 0x04007B42 RID: 31554
		public GeyserConfigurator.GeyserShape shape;

		// Token: 0x04007B43 RID: 31555
		public float temperature;

		// Token: 0x04007B44 RID: 31556
		public float minRatePerCycle;

		// Token: 0x04007B45 RID: 31557
		public float maxRatePerCycle;

		// Token: 0x04007B46 RID: 31558
		public float maxPressure;

		// Token: 0x04007B47 RID: 31559
		public SimUtil.DiseaseInfo diseaseInfo = SimUtil.DiseaseInfo.Invalid;

		// Token: 0x04007B48 RID: 31560
		public float minIterationLength;

		// Token: 0x04007B49 RID: 31561
		public float maxIterationLength;

		// Token: 0x04007B4A RID: 31562
		public float minIterationPercent;

		// Token: 0x04007B4B RID: 31563
		public float maxIterationPercent;

		// Token: 0x04007B4C RID: 31564
		public float minYearLength;

		// Token: 0x04007B4D RID: 31565
		public float maxYearLength;

		// Token: 0x04007B4E RID: 31566
		public float minYearPercent;

		// Token: 0x04007B4F RID: 31567
		public float maxYearPercent;

		// Token: 0x04007B50 RID: 31568
		public float geyserTemperature;

		// Token: 0x04007B51 RID: 31569
		[Obsolete]
		public string DlcID;

		// Token: 0x04007B52 RID: 31570
		public string[] requiredDlcIds;

		// Token: 0x04007B53 RID: 31571
		public string[] forbiddenDlcIds;

		// Token: 0x04007B54 RID: 31572
		public const string BLANK_ID = "Blank";

		// Token: 0x04007B55 RID: 31573
		public const SimHashes BLANK_ELEMENT = SimHashes.Void;
	}

	// Token: 0x0200191E RID: 6430
	[Serializable]
	public class GeyserInstanceConfiguration
	{
		// Token: 0x06009E57 RID: 40535 RVA: 0x00396A83 File Offset: 0x00394C83
		public Geyser.GeyserModification GetModifier()
		{
			return this.modifier;
		}

		// Token: 0x06009E58 RID: 40536 RVA: 0x00396A8C File Offset: 0x00394C8C
		public void Init(bool reinit = false)
		{
			if (this.didInit && !reinit)
			{
				return;
			}
			this.didInit = true;
			this.scaledRate = this.Resample(this.rateRoll, this.geyserType.minRatePerCycle, this.geyserType.maxRatePerCycle);
			this.scaledIterationLength = this.Resample(this.iterationLengthRoll, this.geyserType.minIterationLength, this.geyserType.maxIterationLength);
			this.scaledIterationPercent = this.Resample(this.iterationPercentRoll, this.geyserType.minIterationPercent, this.geyserType.maxIterationPercent);
			this.scaledYearLength = this.Resample(this.yearLengthRoll, this.geyserType.minYearLength, this.geyserType.maxYearLength);
			this.scaledYearPercent = this.Resample(this.yearPercentRoll, this.geyserType.minYearPercent, this.geyserType.maxYearPercent);
		}

		// Token: 0x06009E59 RID: 40537 RVA: 0x00396B74 File Offset: 0x00394D74
		public void SetModifier(Geyser.GeyserModification modifier)
		{
			this.modifier = modifier;
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06009E5A RID: 40538 RVA: 0x00396B7D File Offset: 0x00394D7D
		public GeyserConfigurator.GeyserType geyserType
		{
			get
			{
				return GeyserConfigurator.FindType(this.typeId);
			}
		}

		// Token: 0x06009E5B RID: 40539 RVA: 0x00396B8C File Offset: 0x00394D8C
		private float GetModifiedValue(float geyserVariable, float modifier, Geyser.ModificationMethod method)
		{
			float num = geyserVariable;
			if (method != Geyser.ModificationMethod.Values)
			{
				if (method == Geyser.ModificationMethod.Percentages)
				{
					num += geyserVariable * modifier;
				}
			}
			else
			{
				num += modifier;
			}
			return num;
		}

		// Token: 0x06009E5C RID: 40540 RVA: 0x00396BAF File Offset: 0x00394DAF
		public float GetMaxPressure()
		{
			return this.GetModifiedValue(this.geyserType.maxPressure, this.modifier.maxPressureModifier, Geyser.maxPressureModificationMethod);
		}

		// Token: 0x06009E5D RID: 40541 RVA: 0x00396BD2 File Offset: 0x00394DD2
		public float GetIterationLength()
		{
			this.Init(false);
			return this.GetModifiedValue(this.scaledIterationLength, this.modifier.iterationDurationModifier, Geyser.IterationDurationModificationMethod);
		}

		// Token: 0x06009E5E RID: 40542 RVA: 0x00396BF7 File Offset: 0x00394DF7
		public float GetIterationPercent()
		{
			this.Init(false);
			return Mathf.Clamp(this.GetModifiedValue(this.scaledIterationPercent, this.modifier.iterationPercentageModifier, Geyser.IterationPercentageModificationMethod), 0f, 1f);
		}

		// Token: 0x06009E5F RID: 40543 RVA: 0x00396C2B File Offset: 0x00394E2B
		public float GetOnDuration()
		{
			return this.GetIterationLength() * this.GetIterationPercent();
		}

		// Token: 0x06009E60 RID: 40544 RVA: 0x00396C3A File Offset: 0x00394E3A
		public float GetOffDuration()
		{
			return this.GetIterationLength() * (1f - this.GetIterationPercent());
		}

		// Token: 0x06009E61 RID: 40545 RVA: 0x00396C4F File Offset: 0x00394E4F
		public float GetMassPerCycle()
		{
			this.Init(false);
			return this.GetModifiedValue(this.scaledRate, this.modifier.massPerCycleModifier, Geyser.massModificationMethod);
		}

		// Token: 0x06009E62 RID: 40546 RVA: 0x00396C74 File Offset: 0x00394E74
		public float GetEmitRate()
		{
			float num = 600f / this.GetIterationLength();
			return this.GetMassPerCycle() / num / this.GetOnDuration();
		}

		// Token: 0x06009E63 RID: 40547 RVA: 0x00396C9D File Offset: 0x00394E9D
		public float GetYearLength()
		{
			this.Init(false);
			return this.GetModifiedValue(this.scaledYearLength, this.modifier.yearDurationModifier, Geyser.yearDurationModificationMethod);
		}

		// Token: 0x06009E64 RID: 40548 RVA: 0x00396CC2 File Offset: 0x00394EC2
		public float GetYearPercent()
		{
			this.Init(false);
			return Mathf.Clamp(this.GetModifiedValue(this.scaledYearPercent, this.modifier.yearPercentageModifier, Geyser.yearPercentageModificationMethod), 0f, 1f);
		}

		// Token: 0x06009E65 RID: 40549 RVA: 0x00396CF6 File Offset: 0x00394EF6
		public float GetYearOnDuration()
		{
			return this.GetYearLength() * this.GetYearPercent();
		}

		// Token: 0x06009E66 RID: 40550 RVA: 0x00396D05 File Offset: 0x00394F05
		public float GetYearOffDuration()
		{
			return this.GetYearLength() * (1f - this.GetYearPercent());
		}

		// Token: 0x06009E67 RID: 40551 RVA: 0x00396D1A File Offset: 0x00394F1A
		public SimHashes GetElement()
		{
			if (!this.modifier.modifyElement || this.modifier.newElement == (SimHashes)0)
			{
				return this.geyserType.element;
			}
			return this.modifier.newElement;
		}

		// Token: 0x06009E68 RID: 40552 RVA: 0x00396D4D File Offset: 0x00394F4D
		public float GetTemperature()
		{
			return this.GetModifiedValue(this.geyserType.temperature, this.modifier.temperatureModifier, Geyser.temperatureModificationMethod);
		}

		// Token: 0x06009E69 RID: 40553 RVA: 0x00396D70 File Offset: 0x00394F70
		public byte GetDiseaseIdx()
		{
			return this.geyserType.diseaseInfo.idx;
		}

		// Token: 0x06009E6A RID: 40554 RVA: 0x00396D82 File Offset: 0x00394F82
		public int GetDiseaseCount()
		{
			return this.geyserType.diseaseInfo.count;
		}

		// Token: 0x06009E6B RID: 40555 RVA: 0x00396D94 File Offset: 0x00394F94
		public float GetAverageEmission()
		{
			float num = this.GetEmitRate() * this.GetOnDuration();
			return this.GetYearOnDuration() / this.GetIterationLength() * num / this.GetYearLength();
		}

		// Token: 0x06009E6C RID: 40556 RVA: 0x00396DC8 File Offset: 0x00394FC8
		private float Resample(float t, float min, float max)
		{
			float num = 6f;
			float num2 = 0.002472623f;
			float num3 = t * (1f - num2 * 2f) + num2;
			return (-Mathf.Log(1f / num3 - 1f) + num) / (num * 2f) * (max - min) + min;
		}

		// Token: 0x04007B56 RID: 31574
		public HashedString typeId;

		// Token: 0x04007B57 RID: 31575
		public float rateRoll;

		// Token: 0x04007B58 RID: 31576
		public float iterationLengthRoll;

		// Token: 0x04007B59 RID: 31577
		public float iterationPercentRoll;

		// Token: 0x04007B5A RID: 31578
		public float yearLengthRoll;

		// Token: 0x04007B5B RID: 31579
		public float yearPercentRoll;

		// Token: 0x04007B5C RID: 31580
		public float scaledRate;

		// Token: 0x04007B5D RID: 31581
		public float scaledIterationLength;

		// Token: 0x04007B5E RID: 31582
		public float scaledIterationPercent;

		// Token: 0x04007B5F RID: 31583
		public float scaledYearLength;

		// Token: 0x04007B60 RID: 31584
		public float scaledYearPercent;

		// Token: 0x04007B61 RID: 31585
		private bool didInit;

		// Token: 0x04007B62 RID: 31586
		private Geyser.GeyserModification modifier;
	}
}
