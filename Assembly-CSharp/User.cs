using System;
using UnityEngine;

// Token: 0x020004B7 RID: 1207
[AddComponentMenu("KMonoBehaviour/scripts/User")]
public class User : KMonoBehaviour
{
	// Token: 0x060019BE RID: 6590 RVA: 0x0008DDE7 File Offset: 0x0008BFE7
	public void OnStateMachineStop(string reason, StateMachine.Status status)
	{
		if (status == StateMachine.Status.Success)
		{
			base.Trigger(58624316, null);
			return;
		}
		base.Trigger(1572098533, null);
	}
}
