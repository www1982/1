using System;
using UnityEngine;

// Token: 0x02000A33 RID: 2611
public interface IPolluter
{
	// Token: 0x06004BB1 RID: 19377
	int GetRadius();

	// Token: 0x06004BB2 RID: 19378
	int GetNoise();

	// Token: 0x06004BB3 RID: 19379
	GameObject GetGameObject();

	// Token: 0x06004BB4 RID: 19380
	void SetAttributes(Vector2 pos, int dB, GameObject go, string name = null);

	// Token: 0x06004BB5 RID: 19381
	string GetName();

	// Token: 0x06004BB6 RID: 19382
	Vector2 GetPosition();

	// Token: 0x06004BB7 RID: 19383
	void Clear();

	// Token: 0x06004BB8 RID: 19384
	void SetSplat(NoiseSplat splat);
}
