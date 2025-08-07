using System;
using UnityEngine;

// Token: 0x02000321 RID: 801
public class MouthAnimation : IEntityConfig
{
	// Token: 0x06001084 RID: 4228 RVA: 0x000627F8 File Offset: 0x000609F8
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(MouthAnimation.ID, MouthAnimation.ID, false);
		gameObject.AddOrGet<KBatchedAnimController>().AnimFiles = new KAnimFile[] { Assets.GetAnim("anim_mouth_flap_kanim") };
		return gameObject;
	}

	// Token: 0x06001085 RID: 4229 RVA: 0x0006283A File Offset: 0x00060A3A
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06001086 RID: 4230 RVA: 0x0006283C File Offset: 0x00060A3C
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000A82 RID: 2690
	public static string ID = "MouthAnimation";
}
