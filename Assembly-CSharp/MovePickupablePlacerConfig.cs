using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000372 RID: 882
public class MovePickupablePlacerConfig : CommonPlacerConfig, IEntityConfig
{
	// Token: 0x06001214 RID: 4628 RVA: 0x000697B4 File Offset: 0x000679B4
	public GameObject CreatePrefab()
	{
		GameObject gameObject = base.CreatePrefab(MovePickupablePlacerConfig.ID, MISC.PLACERS.MOVEPICKUPABLEPLACER.NAME, Assets.instance.movePickupToPlacerAssets.material);
		gameObject.AddOrGet<CancellableMove>();
		Storage storage = gameObject.AddOrGet<Storage>();
		storage.showInUI = false;
		storage.showUnreachableStatus = true;
		gameObject.AddOrGet<Approachable>();
		gameObject.AddOrGet<Prioritizable>();
		gameObject.AddTag(GameTags.NotConversationTopic);
		return gameObject;
	}

	// Token: 0x06001215 RID: 4629 RVA: 0x00069818 File Offset: 0x00067A18
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06001216 RID: 4630 RVA: 0x0006981A File Offset: 0x00067A1A
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000B77 RID: 2935
	public static string ID = "MovePickupablePlacer";

	// Token: 0x020011F5 RID: 4597
	[Serializable]
	public class MovePickupablePlacerAssets
	{
		// Token: 0x040064C0 RID: 25792
		public Material material;
	}
}
