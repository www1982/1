using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000A5D RID: 2653
[AddComponentMenu("KMonoBehaviour/scripts/BuddingTrunk")]
public class BuddingTrunk : KMonoBehaviour
{
	// Token: 0x06004CD7 RID: 19671 RVA: 0x001BDA30 File Offset: 0x001BBC30
	protected override void OnSpawn()
	{
		base.OnSpawn();
		PlantBranchGrower.Instance smi = base.gameObject.GetSMI<PlantBranchGrower.Instance>();
		if (smi != null && !smi.IsRunning())
		{
			smi.StartSM();
		}
	}

	// Token: 0x06004CD8 RID: 19672 RVA: 0x001BDA60 File Offset: 0x001BBC60
	public KPrefabID[] GetAndForgetOldSerializedBranches()
	{
		KPrefabID[] array = null;
		if (this.buds != null)
		{
			array = new KPrefabID[this.buds.Length];
			for (int i = 0; i < this.buds.Length; i++)
			{
				HarvestDesignatable harvestDesignatable = ((this.buds[i] == null) ? null : this.buds[i].Get());
				array[i] = ((harvestDesignatable == null) ? null : harvestDesignatable.GetComponent<KPrefabID>());
			}
		}
		this.buds = null;
		return array;
	}

	// Token: 0x040032FD RID: 13053
	[Serialize]
	private Ref<HarvestDesignatable>[] buds;
}
