using System;

// Token: 0x02000A36 RID: 2614
public class NonEssentialEnergyConsumer : EnergyConsumer
{
	// Token: 0x17000531 RID: 1329
	// (get) Token: 0x06004BE0 RID: 19424 RVA: 0x001B78E5 File Offset: 0x001B5AE5
	// (set) Token: 0x06004BE1 RID: 19425 RVA: 0x001B78ED File Offset: 0x001B5AED
	public override bool IsPowered
	{
		get
		{
			return this.isPowered;
		}
		protected set
		{
			if (value == this.isPowered)
			{
				return;
			}
			this.isPowered = value;
			Action<bool> poweredStateChanged = this.PoweredStateChanged;
			if (poweredStateChanged == null)
			{
				return;
			}
			poweredStateChanged(this.isPowered);
		}
	}

	// Token: 0x04003249 RID: 12873
	public Action<bool> PoweredStateChanged;

	// Token: 0x0400324A RID: 12874
	private bool isPowered;
}
