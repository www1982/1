using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000371 RID: 881
public class MopPlacerConfig : CommonPlacerConfig, IEntityConfig
{
	// Token: 0x0600120F RID: 4623 RVA: 0x00069740 File Offset: 0x00067940
	public GameObject CreatePrefab()
	{
		GameObject gameObject = base.CreatePrefab(MopPlacerConfig.ID, MISC.PLACERS.MOPPLACER.NAME, Assets.instance.mopPlacerAssets.material);
		gameObject.AddTag(GameTags.NotConversationTopic);
		Moppable moppable = gameObject.AddOrGet<Moppable>();
		moppable.synchronizeAnims = false;
		moppable.amountMoppedPerTick = 20f;
		gameObject.AddOrGet<Cancellable>();
		return gameObject;
	}

	// Token: 0x06001210 RID: 4624 RVA: 0x0006979A File Offset: 0x0006799A
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06001211 RID: 4625 RVA: 0x0006979C File Offset: 0x0006799C
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000B76 RID: 2934
	public static string ID = "MopPlacer";

	// Token: 0x020011F4 RID: 4596
	[Serializable]
	public class MopPlacerAssets
	{
		// Token: 0x040064BF RID: 25791
		public Material material;
	}
}
