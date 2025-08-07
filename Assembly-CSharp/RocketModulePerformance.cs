using System;

// Token: 0x02000B67 RID: 2919
[Serializable]
public class RocketModulePerformance
{
	// Token: 0x06005705 RID: 22277 RVA: 0x001F86FB File Offset: 0x001F68FB
	public RocketModulePerformance(float burden, float fuelKilogramPerDistance, float enginePower)
	{
		this.burden = burden;
		this.fuelKilogramPerDistance = fuelKilogramPerDistance;
		this.enginePower = enginePower;
	}

	// Token: 0x1700065A RID: 1626
	// (get) Token: 0x06005706 RID: 22278 RVA: 0x001F8718 File Offset: 0x001F6918
	public float Burden
	{
		get
		{
			return this.burden;
		}
	}

	// Token: 0x1700065B RID: 1627
	// (get) Token: 0x06005707 RID: 22279 RVA: 0x001F8720 File Offset: 0x001F6920
	public float FuelKilogramPerDistance
	{
		get
		{
			return this.fuelKilogramPerDistance;
		}
	}

	// Token: 0x1700065C RID: 1628
	// (get) Token: 0x06005708 RID: 22280 RVA: 0x001F8728 File Offset: 0x001F6928
	public float EnginePower
	{
		get
		{
			return this.enginePower;
		}
	}

	// Token: 0x04003A37 RID: 14903
	public float burden;

	// Token: 0x04003A38 RID: 14904
	public float fuelKilogramPerDistance;

	// Token: 0x04003A39 RID: 14905
	public float enginePower;
}
