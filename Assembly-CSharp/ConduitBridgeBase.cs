using System;

// Token: 0x020006FE RID: 1790
public class ConduitBridgeBase : KMonoBehaviour
{
	// Token: 0x06002CCB RID: 11467 RVA: 0x00101D6E File Offset: 0x000FFF6E
	protected void SendEmptyOnMassTransfer()
	{
		if (this.OnMassTransfer != null)
		{
			this.OnMassTransfer(SimHashes.Void, 0f, 0f, 0, 0, null);
		}
	}

	// Token: 0x04001A6C RID: 6764
	public ConduitBridgeBase.DesiredMassTransfer desiredMassTransfer;

	// Token: 0x04001A6D RID: 6765
	public ConduitBridgeBase.ConduitBridgeEvent OnMassTransfer;

	// Token: 0x02001597 RID: 5527
	// (Invoke) Token: 0x06009211 RID: 37393
	public delegate float DesiredMassTransfer(float dt, SimHashes element, float mass, float temperature, byte disease_idx, int disease_count, Pickupable pickupable);

	// Token: 0x02001598 RID: 5528
	// (Invoke) Token: 0x06009215 RID: 37397
	public delegate void ConduitBridgeEvent(SimHashes element, float mass, float temperature, byte disease_idx, int disease_count, Pickupable pickupable);
}
