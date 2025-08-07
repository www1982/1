using System;
using UnityEngine;

// Token: 0x020000CE RID: 206
public interface IApproachableBehaviour
{
	// Token: 0x06000392 RID: 914
	bool IsValidTarget();

	// Token: 0x06000393 RID: 915
	GameObject GetTarget();

	// Token: 0x06000394 RID: 916
	StatusItem GetApproachStatusItem();

	// Token: 0x06000395 RID: 917
	StatusItem GetBehaviourStatusItem();

	// Token: 0x06000396 RID: 918 RVA: 0x0001EA9A File Offset: 0x0001CC9A
	CellOffset[] GetApproachOffsets()
	{
		return OffsetGroups.Use;
	}

	// Token: 0x06000397 RID: 919 RVA: 0x0001EAA1 File Offset: 0x0001CCA1
	void OnArrive()
	{
	}

	// Token: 0x06000398 RID: 920 RVA: 0x0001EAA3 File Offset: 0x0001CCA3
	void OnSuccess()
	{
	}

	// Token: 0x06000399 RID: 921 RVA: 0x0001EAA5 File Offset: 0x0001CCA5
	void OnFailure()
	{
	}
}
