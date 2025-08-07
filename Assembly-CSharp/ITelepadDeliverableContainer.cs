using System;
using UnityEngine;

// Token: 0x0200095E RID: 2398
public interface ITelepadDeliverableContainer
{
	// Token: 0x060044D3 RID: 17619
	void SelectDeliverable();

	// Token: 0x060044D4 RID: 17620
	void DeselectDeliverable();

	// Token: 0x060044D5 RID: 17621
	GameObject GetGameObject();
}
