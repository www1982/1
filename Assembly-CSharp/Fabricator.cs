using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000724 RID: 1828
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/Fabricator")]
public class Fabricator : KMonoBehaviour
{
	// Token: 0x06002E13 RID: 11795 RVA: 0x00108511 File Offset: 0x00106711
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}
}
