using System;

// Token: 0x020005E3 RID: 1507
public static class MinionStorageDataHolder_StaticHelpers
{
	// Token: 0x060022FF RID: 8959 RVA: 0x000C9099 File Offset: 0x000C7299
	public static void UpdateData<T>(this MinionStorageDataHolder dataHolderComponent, MinionStorageDataHolder.DataPackData data)
	{
		dataHolderComponent.Internal_UpdateData(typeof(T).ToString(), data);
	}

	// Token: 0x06002300 RID: 8960 RVA: 0x000C90B1 File Offset: 0x000C72B1
	public static MinionStorageDataHolder.DataPack GetDataPack<T>(this MinionStorageDataHolder dataHolderComponent)
	{
		return dataHolderComponent.Internal_GetDataPack(typeof(T).ToString());
	}
}
