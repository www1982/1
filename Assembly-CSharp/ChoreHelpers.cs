using System;
using UnityEngine;

// Token: 0x02000478 RID: 1144
public static class ChoreHelpers
{
	// Token: 0x06001815 RID: 6165 RVA: 0x00086274 File Offset: 0x00084474
	public static GameObject CreateLocator(string name, Vector3 pos)
	{
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(ApproachableLocator.ID), null, null);
		gameObject.name = name;
		gameObject.transform.SetPosition(pos);
		gameObject.gameObject.SetActive(true);
		return gameObject;
	}

	// Token: 0x06001816 RID: 6166 RVA: 0x000862AC File Offset: 0x000844AC
	public static GameObject CreateSleepLocator(Vector3 pos)
	{
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(SleepLocator.ID), null, null);
		gameObject.name = "SLeepLocator";
		gameObject.transform.SetPosition(pos);
		gameObject.gameObject.SetActive(true);
		return gameObject;
	}

	// Token: 0x06001817 RID: 6167 RVA: 0x000862E8 File Offset: 0x000844E8
	public static void DestroyLocator(GameObject locator)
	{
		if (locator != null)
		{
			locator.gameObject.DeleteObject();
		}
	}
}
