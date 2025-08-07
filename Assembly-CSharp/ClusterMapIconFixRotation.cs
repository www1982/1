using System;

// Token: 0x02000B77 RID: 2935
public class ClusterMapIconFixRotation : KMonoBehaviour
{
	// Token: 0x060057CA RID: 22474 RVA: 0x001FCA9C File Offset: 0x001FAC9C
	private void Update()
	{
		if (base.transform.parent != null)
		{
			float z = base.transform.parent.rotation.eulerAngles.z;
			this.rotation = -z;
			this.animController.Rotation = this.rotation;
		}
	}

	// Token: 0x04003A8F RID: 14991
	[MyCmpGet]
	private KBatchedAnimController animController;

	// Token: 0x04003A90 RID: 14992
	private float rotation;
}
