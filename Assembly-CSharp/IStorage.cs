using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200095C RID: 2396
public interface IStorage
{
	// Token: 0x060044C7 RID: 17607
	bool ShouldShowInUI();

	// Token: 0x170004ED RID: 1261
	// (get) Token: 0x060044C8 RID: 17608
	// (set) Token: 0x060044C9 RID: 17609
	bool allowUIItemRemoval { get; set; }

	// Token: 0x060044CA RID: 17610
	GameObject Drop(GameObject go, bool do_disease_transfer = true);

	// Token: 0x060044CB RID: 17611
	List<GameObject> GetItems();

	// Token: 0x060044CC RID: 17612
	bool IsFull();

	// Token: 0x060044CD RID: 17613
	bool IsEmpty();

	// Token: 0x060044CE RID: 17614
	float Capacity();

	// Token: 0x060044CF RID: 17615
	float RemainingCapacity();

	// Token: 0x060044D0 RID: 17616
	float GetAmountAvailable(Tag tag);

	// Token: 0x060044D1 RID: 17617
	void ConsumeIgnoringDisease(Tag tag, float amount);
}
