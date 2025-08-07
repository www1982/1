using System;
using UnityEngine;

// Token: 0x0200069A RID: 1690
[AddComponentMenu("KMonoBehaviour/scripts/Achievements")]
public class Achievements : KMonoBehaviour
{
	// Token: 0x06002938 RID: 10552 RVA: 0x000EFBBD File Offset: 0x000EDDBD
	public void Unlock(string id)
	{
		if (SteamAchievementService.Instance)
		{
			SteamAchievementService.Instance.Unlock(id);
		}
	}
}
