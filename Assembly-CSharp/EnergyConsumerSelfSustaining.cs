using System;
using System.Diagnostics;
using KSerialization;

// Token: 0x020008E6 RID: 2278
[SerializationConfig(MemberSerialization.OptIn)]
[DebuggerDisplay("{name} {WattsUsed}W")]
public class EnergyConsumerSelfSustaining : EnergyConsumer
{
	// Token: 0x1400001B RID: 27
	// (add) Token: 0x06003F86 RID: 16262 RVA: 0x00164FCC File Offset: 0x001631CC
	// (remove) Token: 0x06003F87 RID: 16263 RVA: 0x00165004 File Offset: 0x00163204
	public event global::System.Action OnConnectionChanged;

	// Token: 0x1700047F RID: 1151
	// (get) Token: 0x06003F88 RID: 16264 RVA: 0x00165039 File Offset: 0x00163239
	public override bool IsPowered
	{
		get
		{
			return this.isSustained || this.connectionStatus == CircuitManager.ConnectionStatus.Powered;
		}
	}

	// Token: 0x17000480 RID: 1152
	// (get) Token: 0x06003F89 RID: 16265 RVA: 0x0016504E File Offset: 0x0016324E
	public bool IsExternallyPowered
	{
		get
		{
			return this.connectionStatus == CircuitManager.ConnectionStatus.Powered;
		}
	}

	// Token: 0x06003F8A RID: 16266 RVA: 0x00165059 File Offset: 0x00163259
	public void SetSustained(bool isSustained)
	{
		this.isSustained = isSustained;
	}

	// Token: 0x06003F8B RID: 16267 RVA: 0x00165064 File Offset: 0x00163264
	public override void SetConnectionStatus(CircuitManager.ConnectionStatus connection_status)
	{
		CircuitManager.ConnectionStatus connectionStatus = this.connectionStatus;
		switch (connection_status)
		{
		case CircuitManager.ConnectionStatus.NotConnected:
			this.connectionStatus = CircuitManager.ConnectionStatus.NotConnected;
			break;
		case CircuitManager.ConnectionStatus.Unpowered:
			if (this.connectionStatus == CircuitManager.ConnectionStatus.Powered && base.GetComponent<Battery>() == null)
			{
				this.connectionStatus = CircuitManager.ConnectionStatus.Unpowered;
			}
			break;
		case CircuitManager.ConnectionStatus.Powered:
			if (this.connectionStatus != CircuitManager.ConnectionStatus.Powered)
			{
				this.connectionStatus = CircuitManager.ConnectionStatus.Powered;
			}
			break;
		}
		this.UpdatePoweredStatus();
		if (connectionStatus != this.connectionStatus && this.OnConnectionChanged != null)
		{
			this.OnConnectionChanged();
		}
	}

	// Token: 0x06003F8C RID: 16268 RVA: 0x001650E7 File Offset: 0x001632E7
	public void UpdatePoweredStatus()
	{
		this.operational.SetFlag(EnergyConsumer.PoweredFlag, this.IsPowered);
	}

	// Token: 0x04002775 RID: 10101
	private bool isSustained;

	// Token: 0x04002776 RID: 10102
	private CircuitManager.ConnectionStatus connectionStatus;
}
