using System;
using System.Collections.Generic;

// Token: 0x020004B8 RID: 1208
public class VoidChoreProvider : ChoreProvider
{
	// Token: 0x060019C0 RID: 6592 RVA: 0x0008DE10 File Offset: 0x0008C010
	public static void DestroyInstance()
	{
		VoidChoreProvider.Instance = null;
	}

	// Token: 0x060019C1 RID: 6593 RVA: 0x0008DE18 File Offset: 0x0008C018
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		VoidChoreProvider.Instance = this;
	}

	// Token: 0x060019C2 RID: 6594 RVA: 0x0008DE26 File Offset: 0x0008C026
	public override void AddChore(Chore chore)
	{
	}

	// Token: 0x060019C3 RID: 6595 RVA: 0x0008DE28 File Offset: 0x0008C028
	public override void RemoveChore(Chore chore)
	{
	}

	// Token: 0x060019C4 RID: 6596 RVA: 0x0008DE2A File Offset: 0x0008C02A
	public override void CollectChores(ChoreConsumerState consumer_state, List<Chore.Precondition.Context> succeeded, List<Chore.Precondition.Context> failed_contexts)
	{
	}

	// Token: 0x04000EC7 RID: 3783
	public static VoidChoreProvider Instance;
}
