using System;
using UnityEngine;

// Token: 0x020005D9 RID: 1497
public class MainCamera : MonoBehaviour
{
	// Token: 0x060022BF RID: 8895 RVA: 0x000C7A1B File Offset: 0x000C5C1B
	private void Awake()
	{
		if (Camera.main != null)
		{
			global::UnityEngine.Object.Destroy(Camera.main.gameObject);
		}
		base.gameObject.tag = "MainCamera";
	}
}
