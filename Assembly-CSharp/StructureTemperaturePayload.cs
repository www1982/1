using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000BA7 RID: 2983
public struct StructureTemperaturePayload
{
	// Token: 0x1700067E RID: 1662
	// (get) Token: 0x06005908 RID: 22792 RVA: 0x00202865 File Offset: 0x00200A65
	// (set) Token: 0x06005909 RID: 22793 RVA: 0x0020286D File Offset: 0x00200A6D
	public PrimaryElement primaryElement
	{
		get
		{
			return this.primaryElementBacking;
		}
		set
		{
			if (this.primaryElementBacking != value)
			{
				this.primaryElementBacking = value;
				this.overheatable = this.primaryElementBacking.GetComponent<Overheatable>();
			}
		}
	}

	// Token: 0x0600590A RID: 22794 RVA: 0x00202898 File Offset: 0x00200A98
	public StructureTemperaturePayload(GameObject go)
	{
		this.simHandleCopy = -1;
		this.enabled = true;
		this.bypass = false;
		this.overrideExtents = false;
		this.overriddenExtents = default(Extents);
		this.primaryElementBacking = go.GetComponent<PrimaryElement>();
		this.overheatable = ((this.primaryElementBacking != null) ? this.primaryElementBacking.GetComponent<Overheatable>() : null);
		this.building = go.GetComponent<Building>();
		this.operational = go.GetComponent<Operational>();
		this.heatEffect = go.GetComponent<KBatchedAnimHeatPostProcessingEffect>();
		this.pendingEnergyModifications = 0f;
		this.maxTemperature = 10000f;
		this.energySourcesKW = null;
		this.isActiveStatusItemSet = false;
	}

	// Token: 0x1700067F RID: 1663
	// (get) Token: 0x0600590B RID: 22795 RVA: 0x00202944 File Offset: 0x00200B44
	public float TotalEnergyProducedKW
	{
		get
		{
			if (this.energySourcesKW == null || this.energySourcesKW.Count == 0)
			{
				return 0f;
			}
			float num = 0f;
			for (int i = 0; i < this.energySourcesKW.Count; i++)
			{
				num += this.energySourcesKW[i].value;
			}
			return num;
		}
	}

	// Token: 0x0600590C RID: 22796 RVA: 0x0020299D File Offset: 0x00200B9D
	public void OverrideExtents(Extents newExtents)
	{
		this.overrideExtents = true;
		this.overriddenExtents = newExtents;
	}

	// Token: 0x0600590D RID: 22797 RVA: 0x002029AD File Offset: 0x00200BAD
	public Extents GetExtents()
	{
		if (!this.overrideExtents)
		{
			return this.building.GetExtents();
		}
		return this.overriddenExtents;
	}

	// Token: 0x17000680 RID: 1664
	// (get) Token: 0x0600590E RID: 22798 RVA: 0x002029C9 File Offset: 0x00200BC9
	public float Temperature
	{
		get
		{
			return this.primaryElement.Temperature;
		}
	}

	// Token: 0x17000681 RID: 1665
	// (get) Token: 0x0600590F RID: 22799 RVA: 0x002029D6 File Offset: 0x00200BD6
	public float ExhaustKilowatts
	{
		get
		{
			return this.building.Def.ExhaustKilowattsWhenActive;
		}
	}

	// Token: 0x17000682 RID: 1666
	// (get) Token: 0x06005910 RID: 22800 RVA: 0x002029E8 File Offset: 0x00200BE8
	public float OperatingKilowatts
	{
		get
		{
			if (!(this.operational != null) || !this.operational.IsActive)
			{
				return 0f;
			}
			return this.building.Def.SelfHeatKilowattsWhenActive;
		}
	}

	// Token: 0x04003B21 RID: 15137
	public int simHandleCopy;

	// Token: 0x04003B22 RID: 15138
	public bool enabled;

	// Token: 0x04003B23 RID: 15139
	public bool bypass;

	// Token: 0x04003B24 RID: 15140
	public bool isActiveStatusItemSet;

	// Token: 0x04003B25 RID: 15141
	public bool overrideExtents;

	// Token: 0x04003B26 RID: 15142
	private PrimaryElement primaryElementBacking;

	// Token: 0x04003B27 RID: 15143
	public Overheatable overheatable;

	// Token: 0x04003B28 RID: 15144
	public Building building;

	// Token: 0x04003B29 RID: 15145
	public Operational operational;

	// Token: 0x04003B2A RID: 15146
	public KBatchedAnimHeatPostProcessingEffect heatEffect;

	// Token: 0x04003B2B RID: 15147
	public List<StructureTemperaturePayload.EnergySource> energySourcesKW;

	// Token: 0x04003B2C RID: 15148
	public float pendingEnergyModifications;

	// Token: 0x04003B2D RID: 15149
	public float maxTemperature;

	// Token: 0x04003B2E RID: 15150
	public Extents overriddenExtents;

	// Token: 0x02001CD4 RID: 7380
	public class EnergySource
	{
		// Token: 0x0600AC52 RID: 44114 RVA: 0x003C1989 File Offset: 0x003BFB89
		public EnergySource(float kj, string source)
		{
			this.source = source;
			this.kw_accumulator = new RunningAverage(float.MinValue, float.MaxValue, Mathf.RoundToInt(186f), true);
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x0600AC53 RID: 44115 RVA: 0x003C19B8 File Offset: 0x003BFBB8
		public float value
		{
			get
			{
				return this.kw_accumulator.AverageValue;
			}
		}

		// Token: 0x0600AC54 RID: 44116 RVA: 0x003C19C5 File Offset: 0x003BFBC5
		public void Accumulate(float value)
		{
			this.kw_accumulator.AddSample(value);
		}

		// Token: 0x0400877C RID: 34684
		public string source;

		// Token: 0x0400877D RID: 34685
		public RunningAverage kw_accumulator;
	}
}
