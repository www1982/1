using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000B9B RID: 2971
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/Spawner")]
public class Spawner : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x060058BB RID: 22715 RVA: 0x002016C3 File Offset: 0x001FF8C3
	protected override void OnSpawn()
	{
		base.OnSpawn();
		SaveGame.Instance.worldGenSpawner.AddLegacySpawner(this.prefabTag, Grid.PosToCell(this));
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x04003AE9 RID: 15081
	[Serialize]
	public Tag prefabTag;

	// Token: 0x04003AEA RID: 15082
	[Serialize]
	public int units = 1;
}
