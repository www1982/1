using System;

// Token: 0x02000A58 RID: 2648
public struct PlantElementAbsorber
{
	// Token: 0x06004CC3 RID: 19651 RVA: 0x001BD551 File Offset: 0x001BB751
	public void Clear()
	{
		this.storage = null;
		this.consumedElements = null;
	}

	// Token: 0x040032EB RID: 13035
	public Storage storage;

	// Token: 0x040032EC RID: 13036
	public PlantElementAbsorber.LocalInfo localInfo;

	// Token: 0x040032ED RID: 13037
	public HandleVector<int>.Handle[] accumulators;

	// Token: 0x040032EE RID: 13038
	public PlantElementAbsorber.ConsumeInfo[] consumedElements;

	// Token: 0x02001B12 RID: 6930
	public struct ConsumeInfo
	{
		// Token: 0x0600A605 RID: 42501 RVA: 0x003AAB6B File Offset: 0x003A8D6B
		public ConsumeInfo(Tag tag, float mass_consumption_rate)
		{
			this.tag = tag;
			this.massConsumptionRate = mass_consumption_rate;
		}

		// Token: 0x04008199 RID: 33177
		public Tag tag;

		// Token: 0x0400819A RID: 33178
		public float massConsumptionRate;
	}

	// Token: 0x02001B13 RID: 6931
	public struct LocalInfo
	{
		// Token: 0x0400819B RID: 33179
		public Tag tag;

		// Token: 0x0400819C RID: 33180
		public float massConsumptionRate;
	}
}
