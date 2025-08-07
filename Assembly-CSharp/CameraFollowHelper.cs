using System;
using UnityEngine;

// Token: 0x0200057B RID: 1403
[AddComponentMenu("KMonoBehaviour/scripts/CameraFollowHelper")]
public class CameraFollowHelper : KMonoBehaviour
{
	// Token: 0x06001FC5 RID: 8133 RVA: 0x000B6AAF File Offset: 0x000B4CAF
	private void LateUpdate()
	{
		if (CameraController.Instance != null)
		{
			CameraController.Instance.UpdateFollowTarget();
		}
	}
}
