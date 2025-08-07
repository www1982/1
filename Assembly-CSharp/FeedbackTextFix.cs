using System;
using Steamworks;
using UnityEngine;

// Token: 0x02000CCC RID: 3276
public class FeedbackTextFix : MonoBehaviour
{
	// Token: 0x060064FE RID: 25854 RVA: 0x0025FD38 File Offset: 0x0025DF38
	private void Awake()
	{
		if (!DistributionPlatform.Initialized || !SteamUtils.IsSteamRunningOnSteamDeck())
		{
			global::UnityEngine.Object.DestroyImmediate(this);
			return;
		}
		this.locText.key = this.newKey;
	}

	// Token: 0x04004504 RID: 17668
	public string newKey;

	// Token: 0x04004505 RID: 17669
	public LocText locText;
}
