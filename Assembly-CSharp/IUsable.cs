using System;
using UnityEngine;

// Token: 0x020007E2 RID: 2018
public interface IUsable
{
	// Token: 0x060036A4 RID: 13988
	bool IsUsable();

	// Token: 0x170003A7 RID: 935
	// (get) Token: 0x060036A5 RID: 13989
	Transform transform { get; }
}
