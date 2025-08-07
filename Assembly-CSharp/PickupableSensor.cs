using System;

// Token: 0x02000500 RID: 1280
public class PickupableSensor : Sensor
{
	// Token: 0x06001B5E RID: 7006 RVA: 0x000965BB File Offset: 0x000947BB
	public PickupableSensor(Sensors sensors)
		: base(sensors)
	{
		this.worker = base.GetComponent<WorkerBase>();
		this.pathProber = base.GetComponent<PathProber>();
	}

	// Token: 0x06001B5F RID: 7007 RVA: 0x000965DC File Offset: 0x000947DC
	public override void Update()
	{
		GlobalChoreProvider.Instance.UpdateFetches(this.pathProber);
		Game.Instance.fetchManager.UpdatePickups(this.pathProber, this.worker);
	}

	// Token: 0x04001026 RID: 4134
	private PathProber pathProber;

	// Token: 0x04001027 RID: 4135
	private WorkerBase worker;
}
