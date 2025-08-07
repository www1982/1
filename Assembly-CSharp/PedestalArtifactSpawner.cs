using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000A55 RID: 2645
public class PedestalArtifactSpawner : KMonoBehaviour
{
	// Token: 0x06004CB0 RID: 19632 RVA: 0x001BCF04 File Offset: 0x001BB104
	protected override void OnSpawn()
	{
		base.OnSpawn();
		foreach (GameObject gameObject in this.storage.items)
		{
			if (ArtifactSelector.Instance.GetArtifactType(gameObject.name) == ArtifactType.Terrestrial)
			{
				gameObject.GetComponent<KPrefabID>().AddTag(GameTags.TerrestrialArtifact, true);
			}
		}
		if (this.artifactSpawned)
		{
			return;
		}
		GameObject gameObject2 = Util.KInstantiate(Assets.GetPrefab(ArtifactSelector.Instance.GetUniqueArtifactID(ArtifactType.Terrestrial)), base.transform.position);
		gameObject2.SetActive(true);
		gameObject2.GetComponent<KPrefabID>().AddTag(GameTags.TerrestrialArtifact, true);
		this.storage.Store(gameObject2, false, false, true, false);
		this.receptacle.ForceDeposit(gameObject2);
		this.artifactSpawned = true;
	}

	// Token: 0x040032DC RID: 13020
	[MyCmpReq]
	private Storage storage;

	// Token: 0x040032DD RID: 13021
	[MyCmpReq]
	private SingleEntityReceptacle receptacle;

	// Token: 0x040032DE RID: 13022
	[Serialize]
	private bool artifactSpawned;
}
