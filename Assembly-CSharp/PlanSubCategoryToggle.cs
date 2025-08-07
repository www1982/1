using System;
using UnityEngine;

// Token: 0x02000D9A RID: 3482
public class PlanSubCategoryToggle : KMonoBehaviour
{
	// Token: 0x06006D07 RID: 27911 RVA: 0x0029461F File Offset: 0x0029281F
	protected override void OnSpawn()
	{
		base.OnSpawn();
		MultiToggle multiToggle = this.toggle;
		multiToggle.onClick = (global::System.Action)Delegate.Combine(multiToggle.onClick, new global::System.Action(delegate
		{
			this.open = !this.open;
			this.gridContainer.SetActive(this.open);
			this.toggle.ChangeState(this.open ? 0 : 1);
		}));
	}

	// Token: 0x04004A5D RID: 19037
	[SerializeField]
	private MultiToggle toggle;

	// Token: 0x04004A5E RID: 19038
	[SerializeField]
	private GameObject gridContainer;

	// Token: 0x04004A5F RID: 19039
	private bool open = true;
}
