using System;
using UnityEngine;

// Token: 0x020000AC RID: 172
[AddComponentMenu("KMonoBehaviour/scripts/LightSymbolTracker")]
public class LightSymbolTracker : KMonoBehaviour, IRenderEveryTick
{
	// Token: 0x06000327 RID: 807 RVA: 0x000193C9 File Offset: 0x000175C9
	protected override void OnSpawn()
	{
		this.animController = base.GetComponent<KBatchedAnimController>();
		this.light2D = base.GetComponent<Light2D>();
		this.pickupable = base.GetComponent<Pickupable>();
	}

	// Token: 0x06000328 RID: 808 RVA: 0x000193F0 File Offset: 0x000175F0
	public bool IsEnableAndVisible()
	{
		return CameraController.Instance.VisibleArea.CurrentAreaExtended.Contains(this.pickupable.cachedCell) && base.enabled;
	}

	// Token: 0x06000329 RID: 809 RVA: 0x0001942C File Offset: 0x0001762C
	public void RenderEveryTick(float dt)
	{
		if (!this.IsEnableAndVisible())
		{
			return;
		}
		Vector3 vector = Vector3.zero;
		bool flag;
		vector = (this.animController.GetTransformMatrix() * this.animController.GetSymbolLocalTransform(this.targetSymbol, out flag)).MultiplyPoint(Vector3.zero) - base.transform.position;
		this.light2D.Offset = vector;
	}

	// Token: 0x04000224 RID: 548
	public HashedString targetSymbol;

	// Token: 0x04000225 RID: 549
	private KBatchedAnimController animController;

	// Token: 0x04000226 RID: 550
	private Light2D light2D;

	// Token: 0x04000227 RID: 551
	private Pickupable pickupable;
}
