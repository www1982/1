using System;

// Token: 0x020007F8 RID: 2040
public interface IDisconnectable
{
	// Token: 0x06003780 RID: 14208
	bool Connect();

	// Token: 0x06003781 RID: 14209
	void Disconnect();

	// Token: 0x06003782 RID: 14210
	bool IsDisconnected();
}
