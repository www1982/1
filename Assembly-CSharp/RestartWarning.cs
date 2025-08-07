using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000ACE RID: 2766
public class RestartWarning : MonoBehaviour
{
	// Token: 0x06005061 RID: 20577 RVA: 0x001D12C6 File Offset: 0x001CF4C6
	private void Update()
	{
		if (RestartWarning.ShouldWarn)
		{
			this.text.enabled = true;
			this.image.enabled = true;
		}
	}

	// Token: 0x04003615 RID: 13845
	public static bool ShouldWarn;

	// Token: 0x04003616 RID: 13846
	public LocText text;

	// Token: 0x04003617 RID: 13847
	public Image image;
}
