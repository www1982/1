using System;
using UnityEngine;

// Token: 0x02000CB1 RID: 3249
public class DebugElementMenu : KButtonMenu
{
	// Token: 0x060063F8 RID: 25592 RVA: 0x00258B1B File Offset: 0x00256D1B
	protected override void OnPrefabInit()
	{
		DebugElementMenu.Instance = this;
		base.OnPrefabInit();
		base.ConsumeMouseScroll = true;
	}

	// Token: 0x060063F9 RID: 25593 RVA: 0x00258B30 File Offset: 0x00256D30
	protected override void OnForcedCleanUp()
	{
		DebugElementMenu.Instance = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x060063FA RID: 25594 RVA: 0x00258B3E File Offset: 0x00256D3E
	public void Turnoff()
	{
		this.root.gameObject.SetActive(false);
	}

	// Token: 0x0400440D RID: 17421
	public static DebugElementMenu Instance;

	// Token: 0x0400440E RID: 17422
	public GameObject root;
}
