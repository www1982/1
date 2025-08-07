using System;

// Token: 0x0200095B RID: 2395
public interface IFuelTank
{
	// Token: 0x170004EB RID: 1259
	// (get) Token: 0x060044C4 RID: 17604
	IStorage Storage { get; }

	// Token: 0x170004EC RID: 1260
	// (get) Token: 0x060044C5 RID: 17605
	bool ConsumeFuelOnLand { get; }

	// Token: 0x060044C6 RID: 17606
	void DEBUG_FillTank();
}
