using System;
using UnityEngine;

// Token: 0x02000D7C RID: 3452
public class NonLinearSlider : KSlider
{
	// Token: 0x06006B5D RID: 27485 RVA: 0x00288BA1 File Offset: 0x00286DA1
	public static NonLinearSlider.Range[] GetDefaultRange(float maxValue)
	{
		return new NonLinearSlider.Range[]
		{
			new NonLinearSlider.Range(100f, maxValue)
		};
	}

	// Token: 0x06006B5E RID: 27486 RVA: 0x00288BBB File Offset: 0x00286DBB
	protected override void Start()
	{
		base.Start();
		base.minValue = 0f;
		base.maxValue = 100f;
	}

	// Token: 0x06006B5F RID: 27487 RVA: 0x00288BD9 File Offset: 0x00286DD9
	public void SetRanges(NonLinearSlider.Range[] ranges)
	{
		this.ranges = ranges;
	}

	// Token: 0x06006B60 RID: 27488 RVA: 0x00288BE4 File Offset: 0x00286DE4
	public float GetPercentageFromValue(float value)
	{
		float num = 0f;
		float num2 = 0f;
		for (int i = 0; i < this.ranges.Length; i++)
		{
			if (value >= num2 && value <= this.ranges[i].peakValue)
			{
				float num3 = (value - num2) / (this.ranges[i].peakValue - num2);
				return Mathf.Lerp(num, num + this.ranges[i].width, num3);
			}
			num += this.ranges[i].width;
			num2 = this.ranges[i].peakValue;
		}
		return 100f;
	}

	// Token: 0x06006B61 RID: 27489 RVA: 0x00288C88 File Offset: 0x00286E88
	public float GetValueForPercentage(float percentage)
	{
		float num = 0f;
		float num2 = 0f;
		for (int i = 0; i < this.ranges.Length; i++)
		{
			if (percentage >= num && num + this.ranges[i].width >= percentage)
			{
				float num3 = (percentage - num) / this.ranges[i].width;
				return Mathf.Lerp(num2, this.ranges[i].peakValue, num3);
			}
			num += this.ranges[i].width;
			num2 = this.ranges[i].peakValue;
		}
		return num2;
	}

	// Token: 0x06006B62 RID: 27490 RVA: 0x00288D24 File Offset: 0x00286F24
	protected override void Set(float input, bool sendCallback)
	{
		base.Set(input, sendCallback);
	}

	// Token: 0x0400491B RID: 18715
	public NonLinearSlider.Range[] ranges;

	// Token: 0x02001F65 RID: 8037
	[Serializable]
	public struct Range
	{
		// Token: 0x0600B327 RID: 45863 RVA: 0x003D90D2 File Offset: 0x003D72D2
		public Range(float width, float peakValue)
		{
			this.width = width;
			this.peakValue = peakValue;
		}

		// Token: 0x040090AF RID: 37039
		public float width;

		// Token: 0x040090B0 RID: 37040
		public float peakValue;
	}
}
