using System;

// Token: 0x02000BC9 RID: 3017
public class UtilityNetwork
{
	// Token: 0x06005A5B RID: 23131 RVA: 0x0020ABB8 File Offset: 0x00208DB8
	public virtual void AddItem(object item)
	{
	}

	// Token: 0x06005A5C RID: 23132 RVA: 0x0020ABBA File Offset: 0x00208DBA
	public virtual void RemoveItem(object item)
	{
	}

	// Token: 0x06005A5D RID: 23133 RVA: 0x0020ABBC File Offset: 0x00208DBC
	public virtual void ConnectItem(object item)
	{
	}

	// Token: 0x06005A5E RID: 23134 RVA: 0x0020ABBE File Offset: 0x00208DBE
	public virtual void DisconnectItem(object item)
	{
	}

	// Token: 0x06005A5F RID: 23135 RVA: 0x0020ABC0 File Offset: 0x00208DC0
	public virtual void Reset(UtilityNetworkGridNode[] grid)
	{
	}

	// Token: 0x04003BF8 RID: 15352
	public int id;

	// Token: 0x04003BF9 RID: 15353
	public ConduitType conduitType;
}
