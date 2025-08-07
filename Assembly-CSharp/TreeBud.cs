using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000A70 RID: 2672
[AddComponentMenu("KMonoBehaviour/scripts/TreeBud")]
public class TreeBud : KMonoBehaviour
{
	// Token: 0x06004D49 RID: 19785 RVA: 0x001BF910 File Offset: 0x001BDB10
	protected override void OnSpawn()
	{
		base.OnSpawn();
		PlantBranch.Instance smi = base.gameObject.GetSMI<PlantBranch.Instance>();
		if (smi != null && !smi.IsRunning())
		{
			smi.StartSM();
		}
	}

	// Token: 0x06004D4A RID: 19786 RVA: 0x001BF940 File Offset: 0x001BDB40
	public BuddingTrunk GetAndForgetOldTrunk()
	{
		BuddingTrunk buddingTrunk = ((this.buddingTrunk == null) ? null : this.buddingTrunk.Get());
		this.buddingTrunk = null;
		return buddingTrunk;
	}

	// Token: 0x04003366 RID: 13158
	[Serialize]
	public Ref<BuddingTrunk> buddingTrunk;
}
