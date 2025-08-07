using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AE8 RID: 2792
public class RunningWeightedAverage
{
	// Token: 0x06005169 RID: 20841 RVA: 0x001D97F8 File Offset: 0x001D79F8
	public RunningWeightedAverage(float minValue = -3.4028235E+38f, float maxValue = 3.4028235E+38f, int sampleCount = 20, bool allowZero = true)
	{
		this.min = minValue;
		this.max = maxValue;
		this.ignoreZero = !allowZero;
		this.samples = new List<global::Tuple<float, float>>();
	}

	// Token: 0x170005B9 RID: 1465
	// (get) Token: 0x0600516A RID: 20842 RVA: 0x001D9837 File Offset: 0x001D7A37
	public float GetUnweightedAverage
	{
		get
		{
			return this.GetAverageOfLastSeconds(4f);
		}
	}

	// Token: 0x170005BA RID: 1466
	// (get) Token: 0x0600516B RID: 20843 RVA: 0x001D9844 File Offset: 0x001D7A44
	public bool HasEverHadValidValues
	{
		get
		{
			return this.validSampleCount >= this.maxSamples;
		}
	}

	// Token: 0x0600516C RID: 20844 RVA: 0x001D9858 File Offset: 0x001D7A58
	public void AddSample(float value, float timeOfRecord)
	{
		if (this.ignoreZero && value == 0f)
		{
			return;
		}
		if (value > this.max)
		{
			value = this.max;
		}
		if (value < this.min)
		{
			value = this.min;
		}
		if (this.validSampleCount <= this.maxSamples)
		{
			this.validSampleCount++;
		}
		this.samples.Add(new global::Tuple<float, float>(value, timeOfRecord));
		if (this.samples.Count > this.maxSamples)
		{
			this.samples.RemoveAt(0);
		}
	}

	// Token: 0x0600516D RID: 20845 RVA: 0x001D98E8 File Offset: 0x001D7AE8
	public int ValidRecordsInLastSeconds(float seconds)
	{
		int num = 0;
		int num2 = this.samples.Count - 1;
		while (num2 >= 0 && Time.time - this.samples[num2].second <= seconds)
		{
			num++;
			num2--;
		}
		return num;
	}

	// Token: 0x0600516E RID: 20846 RVA: 0x001D9930 File Offset: 0x001D7B30
	private float GetAverageOfLastSeconds(float seconds)
	{
		float num = 0f;
		int num2 = 0;
		int num3 = this.samples.Count - 1;
		while (num3 >= 0 && Time.time - this.samples[num3].second <= seconds)
		{
			num += this.samples[num3].first;
			num2++;
			num3--;
		}
		if (num2 == 0)
		{
			return 0f;
		}
		return num / (float)num2;
	}

	// Token: 0x040036DA RID: 14042
	private List<global::Tuple<float, float>> samples = new List<global::Tuple<float, float>>();

	// Token: 0x040036DB RID: 14043
	private float min;

	// Token: 0x040036DC RID: 14044
	private float max;

	// Token: 0x040036DD RID: 14045
	private bool ignoreZero;

	// Token: 0x040036DE RID: 14046
	private int validSampleCount;

	// Token: 0x040036DF RID: 14047
	private int maxSamples = 20;
}
