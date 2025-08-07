using System;

// Token: 0x02000285 RID: 645
public class LimitValveTuning
{
	// Token: 0x06000D13 RID: 3347 RVA: 0x0004DF9E File Offset: 0x0004C19E
	public static NonLinearSlider.Range[] GetDefaultSlider()
	{
		return new NonLinearSlider.Range[]
		{
			new NonLinearSlider.Range(70f, 100f),
			new NonLinearSlider.Range(30f, 500f)
		};
	}

	// Token: 0x040008C7 RID: 2247
	public const float MAX_LIMIT = 500f;

	// Token: 0x040008C8 RID: 2248
	public const float DEFAULT_LIMIT = 100f;
}
