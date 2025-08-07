using System;
using UnityEngine;

// Token: 0x02000DEB RID: 3563
public interface IConfigurableConsumerOption
{
	// Token: 0x06007092 RID: 28818
	Tag GetID();

	// Token: 0x06007093 RID: 28819
	string GetName();

	// Token: 0x06007094 RID: 28820
	string GetDetailedDescription();

	// Token: 0x06007095 RID: 28821
	string GetDescription();

	// Token: 0x06007096 RID: 28822
	Sprite GetIcon();

	// Token: 0x06007097 RID: 28823
	IConfigurableConsumerIngredient[] GetIngredients();
}
