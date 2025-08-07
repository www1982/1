using System;

// Token: 0x02000718 RID: 1816
public interface INavDoor
{
	// Token: 0x17000273 RID: 627
	// (get) Token: 0x06002D9D RID: 11677
	bool isSpawned { get; }

	// Token: 0x06002D9E RID: 11678
	bool IsOpen();

	// Token: 0x06002D9F RID: 11679
	void Open();

	// Token: 0x06002DA0 RID: 11680
	void Close();
}
