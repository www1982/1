using System;
using KSerialization;

// Token: 0x020008DF RID: 2271
[SerializationConfig(MemberSerialization.OptIn)]
public class ElementConverterOperationalRequirement : KMonoBehaviour
{
	// Token: 0x06003F2D RID: 16173 RVA: 0x00163E4B File Offset: 0x0016204B
	private void onStorageChanged(object _)
	{
		this.operational.SetFlag(this.sufficientResources, this.converter.HasEnoughMassToStartConverting(false));
	}

	// Token: 0x06003F2E RID: 16174 RVA: 0x00163E6A File Offset: 0x0016206A
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.sufficientResources = new Operational.Flag("sufficientResources", this.operationalReq);
		base.Subscribe(-1697596308, new Action<object>(this.onStorageChanged));
		this.onStorageChanged(null);
	}

	// Token: 0x04002748 RID: 10056
	[MyCmpReq]
	private ElementConverter converter;

	// Token: 0x04002749 RID: 10057
	[MyCmpReq]
	private Operational operational;

	// Token: 0x0400274A RID: 10058
	private Operational.Flag.Type operationalReq;

	// Token: 0x0400274B RID: 10059
	private Operational.Flag sufficientResources;
}
