using System;
using UnityEngine;

// Token: 0x02000CCD RID: 3277
public class FoldOutPanel : KMonoBehaviour
{
	// Token: 0x06006500 RID: 25856 RVA: 0x0025FD68 File Offset: 0x0025DF68
	protected override void OnSpawn()
	{
		MultiToggle componentInChildren = base.GetComponentInChildren<MultiToggle>();
		componentInChildren.onClick = (global::System.Action)Delegate.Combine(componentInChildren.onClick, new global::System.Action(this.OnClick));
		this.ToggleOpen(this.startOpen);
	}

	// Token: 0x06006501 RID: 25857 RVA: 0x0025FD9D File Offset: 0x0025DF9D
	private void OnClick()
	{
		this.ToggleOpen(!this.panelOpen);
	}

	// Token: 0x06006502 RID: 25858 RVA: 0x0025FDAE File Offset: 0x0025DFAE
	private void ToggleOpen(bool open)
	{
		this.panelOpen = open;
		this.container.SetActive(this.panelOpen);
		base.GetComponentInChildren<MultiToggle>().ChangeState(this.panelOpen ? 1 : 0);
	}

	// Token: 0x04004506 RID: 17670
	private bool panelOpen = true;

	// Token: 0x04004507 RID: 17671
	public GameObject container;

	// Token: 0x04004508 RID: 17672
	public bool startOpen = true;
}
