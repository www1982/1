using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000BC1 RID: 3009
[SerializationConfig(MemberSerialization.OptOut)]
[AddComponentMenu("KMonoBehaviour/scripts/UnstableGround")]
public class UnstableGround : KMonoBehaviour
{
	// Token: 0x04003BD0 RID: 15312
	public SimHashes element;

	// Token: 0x04003BD1 RID: 15313
	public float mass;

	// Token: 0x04003BD2 RID: 15314
	public float temperature;

	// Token: 0x04003BD3 RID: 15315
	public byte diseaseIdx;

	// Token: 0x04003BD4 RID: 15316
	public int diseaseCount;
}
