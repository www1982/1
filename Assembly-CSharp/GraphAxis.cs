using System;

// Token: 0x02000CD7 RID: 3287
[Serializable]
public struct GraphAxis
{
	// Token: 0x1700075C RID: 1884
	// (get) Token: 0x06006565 RID: 25957 RVA: 0x00262654 File Offset: 0x00260854
	public float range
	{
		get
		{
			return this.max_value - this.min_value;
		}
	}

	// Token: 0x0400455E RID: 17758
	public string name;

	// Token: 0x0400455F RID: 17759
	public float min_value;

	// Token: 0x04004560 RID: 17760
	public float max_value;

	// Token: 0x04004561 RID: 17761
	public float guide_frequency;
}
