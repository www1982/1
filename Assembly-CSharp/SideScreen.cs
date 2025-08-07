using System;
using UnityEngine;

// Token: 0x02000E2F RID: 3631
public class SideScreen : KScreen
{
	// Token: 0x06007309 RID: 29449 RVA: 0x002BC852 File Offset: 0x002BAA52
	public void SetContent(SideScreenContent sideScreenContent, GameObject target)
	{
		if (sideScreenContent.transform.parent != this.contentBody.transform)
		{
			sideScreenContent.transform.SetParent(this.contentBody.transform);
		}
		sideScreenContent.SetTarget(target);
	}

	// Token: 0x04004F43 RID: 20291
	[SerializeField]
	private GameObject contentBody;
}
