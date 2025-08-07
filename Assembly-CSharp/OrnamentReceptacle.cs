using System;
using UnityEngine;

// Token: 0x020007A0 RID: 1952
public class OrnamentReceptacle : SingleEntityReceptacle
{
	// Token: 0x060033B7 RID: 13239 RVA: 0x00122718 File Offset: 0x00120918
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x060033B8 RID: 13240 RVA: 0x00122720 File Offset: 0x00120920
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.GetComponent<KBatchedAnimController>().SetSymbolVisiblity("snapTo_ornament", false);
	}

	// Token: 0x060033B9 RID: 13241 RVA: 0x00122740 File Offset: 0x00120940
	protected override void PositionOccupyingObject()
	{
		KBatchedAnimController component = base.occupyingObject.GetComponent<KBatchedAnimController>();
		component.transform.SetLocalPosition(new Vector3(0f, 0f, -0.1f));
		this.occupyingTracker = base.occupyingObject.AddComponent<KBatchedAnimTracker>();
		this.occupyingTracker.symbol = new HashedString("snapTo_ornament");
		this.occupyingTracker.forceAlwaysVisible = true;
		this.animLink = new KAnimLink(base.GetComponent<KBatchedAnimController>(), component);
	}

	// Token: 0x060033BA RID: 13242 RVA: 0x001227C0 File Offset: 0x001209C0
	protected override void ClearOccupant()
	{
		if (this.occupyingTracker != null)
		{
			global::UnityEngine.Object.Destroy(this.occupyingTracker);
			this.occupyingTracker = null;
		}
		if (this.animLink != null)
		{
			this.animLink.Unregister();
			this.animLink = null;
		}
		base.ClearOccupant();
	}

	// Token: 0x04001F17 RID: 7959
	[MyCmpReq]
	private SnapOn snapOn;

	// Token: 0x04001F18 RID: 7960
	private KBatchedAnimTracker occupyingTracker;

	// Token: 0x04001F19 RID: 7961
	private KAnimLink animLink;
}
