using System;

// Token: 0x02000AE7 RID: 2791
public class RunningAverage
{
	// Token: 0x06005165 RID: 20837 RVA: 0x001D96EE File Offset: 0x001D78EE
	public RunningAverage(float minValue = -3.4028235E+38f, float maxValue = 3.4028235E+38f, int sampleCount = 15, bool allowZero = true)
	{
		this.min = minValue;
		this.max = maxValue;
		this.ignoreZero = !allowZero;
		this.samples = new float[sampleCount];
	}

	// Token: 0x170005B8 RID: 1464
	// (get) Token: 0x06005166 RID: 20838 RVA: 0x001D971B File Offset: 0x001D791B
	public float AverageValue
	{
		get
		{
			return this.GetAverage();
		}
	}

	// Token: 0x06005167 RID: 20839 RVA: 0x001D9724 File Offset: 0x001D7924
	public void AddSample(float value)
	{
		if (value < this.min || value > this.max || (this.ignoreZero && value == 0f))
		{
			return;
		}
		if (this.validValues < this.samples.Length)
		{
			this.validValues++;
		}
		for (int i = 0; i < this.samples.Length - 1; i++)
		{
			this.samples[i] = this.samples[i + 1];
		}
		this.samples[this.samples.Length - 1] = value;
	}

	// Token: 0x06005168 RID: 20840 RVA: 0x001D97AC File Offset: 0x001D79AC
	private float GetAverage()
	{
		float num = 0f;
		for (int i = this.samples.Length - 1; i > this.samples.Length - 1 - this.validValues; i--)
		{
			num += this.samples[i];
		}
		return num / (float)this.validValues;
	}

	// Token: 0x040036D5 RID: 14037
	private float[] samples;

	// Token: 0x040036D6 RID: 14038
	private float min;

	// Token: 0x040036D7 RID: 14039
	private float max;

	// Token: 0x040036D8 RID: 14040
	private bool ignoreZero;

	// Token: 0x040036D9 RID: 14041
	private int validValues;
}
