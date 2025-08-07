using System;

// Token: 0x02000935 RID: 2357
public interface IEnergyProducer
{
	// Token: 0x170004BB RID: 1211
	// (get) Token: 0x060042DD RID: 17117
	float JoulesAvailable { get; }

	// Token: 0x060042DE RID: 17118
	void ConsumeEnergy(float joules);
}
