using System;
using KSerialization;
using UnityEngine;

// Token: 0x0200018F RID: 399
public class ForestTreeSeedMonitor : KMonoBehaviour
{
	// Token: 0x17000012 RID: 18
	// (get) Token: 0x060007A8 RID: 1960 RVA: 0x00034717 File Offset: 0x00032917
	public bool ExtraSeedAvailable
	{
		get
		{
			return this.hasExtraSeedAvailable;
		}
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x00034720 File Offset: 0x00032920
	public void ExtractExtraSeed()
	{
		if (!this.hasExtraSeedAvailable)
		{
			return;
		}
		this.hasExtraSeedAvailable = false;
		Vector3 position = base.transform.position;
		position.z = Grid.GetLayerZ(Grid.SceneLayer.Ore);
		Util.KInstantiate(Assets.GetPrefab("ForestTreeSeed"), position).SetActive(true);
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x00034772 File Offset: 0x00032972
	public void TryRollNewSeed()
	{
		if (!this.hasExtraSeedAvailable && global::UnityEngine.Random.Range(0, 100) < 5)
		{
			this.hasExtraSeedAvailable = true;
		}
	}

	// Token: 0x040005B6 RID: 1462
	[Serialize]
	private bool hasExtraSeedAvailable;
}
