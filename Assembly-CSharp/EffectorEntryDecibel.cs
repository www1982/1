using System;

// Token: 0x020008D4 RID: 2260
internal struct EffectorEntryDecibel
{
	// Token: 0x06003EB0 RID: 16048 RVA: 0x00161561 File Offset: 0x0015F761
	public EffectorEntryDecibel(string name, float value)
	{
		this.name = name;
		this.value = value;
		this.count = 1;
	}

	// Token: 0x040026AD RID: 9901
	public string name;

	// Token: 0x040026AE RID: 9902
	public int count;

	// Token: 0x040026AF RID: 9903
	public float value;
}
