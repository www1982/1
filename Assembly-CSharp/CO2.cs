using System;
using KSerialization;
using UnityEngine;

// Token: 0x020007FE RID: 2046
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/CO2")]
public class CO2 : KMonoBehaviour
{
	// Token: 0x060037B3 RID: 14259 RVA: 0x00134F4A File Offset: 0x0013314A
	public void StartLoop()
	{
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		component.Play("exhale_pre", KAnim.PlayMode.Once, 1f, 0f);
		component.Play("exhale_loop", KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x060037B4 RID: 14260 RVA: 0x00134F87 File Offset: 0x00133187
	public void TriggerDestroy()
	{
		base.GetComponent<KBatchedAnimController>().Play("exhale_pst", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x040021C6 RID: 8646
	[Serialize]
	[NonSerialized]
	public Vector3 velocity = Vector3.zero;

	// Token: 0x040021C7 RID: 8647
	[Serialize]
	[NonSerialized]
	public float mass;

	// Token: 0x040021C8 RID: 8648
	[Serialize]
	[NonSerialized]
	public float temperature;

	// Token: 0x040021C9 RID: 8649
	[Serialize]
	[NonSerialized]
	public float lifetimeRemaining;
}
