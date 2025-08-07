using System;

// Token: 0x020004FF RID: 1279
public class PathProberSensor : Sensor
{
	// Token: 0x06001B5C RID: 7004 RVA: 0x00096598 File Offset: 0x00094798
	public PathProberSensor(Sensors sensors)
		: base(sensors)
	{
		this.navigator = sensors.GetComponent<Navigator>();
	}

	// Token: 0x06001B5D RID: 7005 RVA: 0x000965AD File Offset: 0x000947AD
	public override void Update()
	{
		this.navigator.UpdateProbe(false);
	}

	// Token: 0x04001025 RID: 4133
	private Navigator navigator;
}
