using System;

// Token: 0x020008E4 RID: 2276
public interface IEnergyConsumer : ICircuitConnected
{
	// Token: 0x1700046D RID: 1133
	// (get) Token: 0x06003F64 RID: 16228
	float WattsUsed { get; }

	// Token: 0x1700046E RID: 1134
	// (get) Token: 0x06003F65 RID: 16229
	float WattsNeededWhenActive { get; }

	// Token: 0x1700046F RID: 1135
	// (get) Token: 0x06003F66 RID: 16230
	int PowerSortOrder { get; }

	// Token: 0x06003F67 RID: 16231
	void SetConnectionStatus(CircuitManager.ConnectionStatus status);

	// Token: 0x17000470 RID: 1136
	// (get) Token: 0x06003F68 RID: 16232
	string Name { get; }

	// Token: 0x17000471 RID: 1137
	// (get) Token: 0x06003F69 RID: 16233
	bool IsConnected { get; }

	// Token: 0x17000472 RID: 1138
	// (get) Token: 0x06003F6A RID: 16234
	bool IsPowered { get; }
}
