using System;
using UnityEngine;

// Token: 0x020005AA RID: 1450
[AddComponentMenu("KMonoBehaviour/scripts/DestroyAfter")]
public class DestroyAfter : KMonoBehaviour
{
	// Token: 0x06002137 RID: 8503 RVA: 0x000BFCA3 File Offset: 0x000BDEA3
	protected override void OnSpawn()
	{
		this.particleSystems = base.gameObject.GetComponentsInChildren<ParticleSystem>(true);
	}

	// Token: 0x06002138 RID: 8504 RVA: 0x000BFCB8 File Offset: 0x000BDEB8
	private bool IsAlive()
	{
		for (int i = 0; i < this.particleSystems.Length; i++)
		{
			if (this.particleSystems[i].IsAlive(false))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002139 RID: 8505 RVA: 0x000BFCEB File Offset: 0x000BDEEB
	private void Update()
	{
		if (this.particleSystems != null && !this.IsAlive())
		{
			this.DeleteObject();
		}
	}

	// Token: 0x0400135A RID: 4954
	private ParticleSystem[] particleSystems;
}
