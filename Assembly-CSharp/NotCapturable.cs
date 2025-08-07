using System;
using UnityEngine;

// Token: 0x02000879 RID: 2169
[AddComponentMenu("KMonoBehaviour/scripts/NotCapturable")]
public class NotCapturable : KMonoBehaviour
{
	// Token: 0x06003BA8 RID: 15272 RVA: 0x0014AD9C File Offset: 0x00148F9C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (base.GetComponent<Capturable>() != null)
		{
			DebugUtil.LogErrorArgs(this, new object[] { "Entity has both Capturable and NotCapturable!" });
		}
		Components.NotCapturables.Add(this);
	}

	// Token: 0x06003BA9 RID: 15273 RVA: 0x0014ADD1 File Offset: 0x00148FD1
	protected override void OnCleanUp()
	{
		Components.NotCapturables.Remove(this);
		base.OnCleanUp();
	}
}
