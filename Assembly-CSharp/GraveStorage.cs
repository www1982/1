using System;
using System.Collections.Generic;

// Token: 0x020005BF RID: 1471
public class GraveStorage : Storage
{
	// Token: 0x060021E8 RID: 8680 RVA: 0x000C3D80 File Offset: 0x000C1F80
	public override Workable.AnimInfo GetAnim(WorkerBase worker)
	{
		KAnimFile[] array = null;
		if (this.workerTypeOverrideAnims.TryGetValue(worker.PrefabID(), out array))
		{
			this.overrideAnims = array;
		}
		return base.GetAnim(worker);
	}

	// Token: 0x040013D1 RID: 5073
	public Dictionary<Tag, KAnimFile[]> workerTypeOverrideAnims = new Dictionary<Tag, KAnimFile[]>();
}
