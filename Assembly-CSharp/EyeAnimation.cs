using System;
using UnityEngine;

// Token: 0x02000306 RID: 774
public class EyeAnimation : IEntityConfig
{
	// Token: 0x06000FEE RID: 4078 RVA: 0x0005F690 File Offset: 0x0005D890
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(EyeAnimation.ID, EyeAnimation.ID, false);
		gameObject.AddOrGet<KBatchedAnimController>().AnimFiles = new KAnimFile[] { Assets.GetAnim("anim_blinks_kanim") };
		return gameObject;
	}

	// Token: 0x06000FEF RID: 4079 RVA: 0x0005F6D2 File Offset: 0x0005D8D2
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000FF0 RID: 4080 RVA: 0x0005F6D4 File Offset: 0x0005D8D4
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000A20 RID: 2592
	public static string ID = "EyeAnimation";
}
