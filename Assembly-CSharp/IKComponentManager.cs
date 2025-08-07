using System;
using UnityEngine;

// Token: 0x020006E3 RID: 1763
public interface IKComponentManager
{
	// Token: 0x06002BC6 RID: 11206
	HandleVector<int>.Handle Add(GameObject go);

	// Token: 0x06002BC7 RID: 11207
	void Remove(GameObject go);
}
