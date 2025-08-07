using System;

// Token: 0x020004FA RID: 1274
public class ClosestLubricantSensor : ClosestPickupableSensor<Pickupable>
{
	// Token: 0x06001B4B RID: 6987 RVA: 0x00096037 File Offset: 0x00094237
	public ClosestLubricantSensor(Sensors sensors, bool shouldStartActive)
		: base(sensors, GameTags.SolidLubricant, shouldStartActive)
	{
	}
}
