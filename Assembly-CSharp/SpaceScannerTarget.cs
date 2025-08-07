using System;

// Token: 0x02000B97 RID: 2967
public readonly struct SpaceScannerTarget
{
	// Token: 0x0600589C RID: 22684 RVA: 0x002006C3 File Offset: 0x001FE8C3
	private SpaceScannerTarget(string id)
	{
		this.id = id;
	}

	// Token: 0x0600589D RID: 22685 RVA: 0x002006CC File Offset: 0x001FE8CC
	public static SpaceScannerTarget MeteorShower()
	{
		return new SpaceScannerTarget("meteor_shower");
	}

	// Token: 0x0600589E RID: 22686 RVA: 0x002006D8 File Offset: 0x001FE8D8
	public static SpaceScannerTarget BallisticObject()
	{
		return new SpaceScannerTarget("ballistic_object");
	}

	// Token: 0x0600589F RID: 22687 RVA: 0x002006E4 File Offset: 0x001FE8E4
	public static SpaceScannerTarget RocketBaseGame(LaunchConditionManager rocket)
	{
		return new SpaceScannerTarget(string.Format("rocket_base_game::{0}", rocket.GetComponent<KPrefabID>().InstanceID));
	}

	// Token: 0x060058A0 RID: 22688 RVA: 0x00200705 File Offset: 0x001FE905
	public static SpaceScannerTarget RocketDlc1(Clustercraft rocket)
	{
		return new SpaceScannerTarget(string.Format("rocket_dlc1::{0}", rocket.GetComponent<KPrefabID>().InstanceID));
	}

	// Token: 0x04003AE0 RID: 15072
	public readonly string id;
}
