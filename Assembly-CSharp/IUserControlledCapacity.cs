using System;

// Token: 0x02000DDD RID: 3549
public interface IUserControlledCapacity
{
	// Token: 0x170007B9 RID: 1977
	// (get) Token: 0x06007008 RID: 28680
	// (set) Token: 0x06007009 RID: 28681
	float UserMaxCapacity { get; set; }

	// Token: 0x170007BA RID: 1978
	// (get) Token: 0x0600700A RID: 28682
	float AmountStored { get; }

	// Token: 0x170007BB RID: 1979
	// (get) Token: 0x0600700B RID: 28683
	float MinCapacity { get; }

	// Token: 0x170007BC RID: 1980
	// (get) Token: 0x0600700C RID: 28684
	float MaxCapacity { get; }

	// Token: 0x170007BD RID: 1981
	// (get) Token: 0x0600700D RID: 28685
	bool WholeValues { get; }

	// Token: 0x170007BE RID: 1982
	// (get) Token: 0x0600700E RID: 28686
	LocString CapacityUnits { get; }
}
