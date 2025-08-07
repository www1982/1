using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000891 RID: 2193
[AddComponentMenu("KMonoBehaviour/Workable/ResearchCenter")]
public class DataMiner : ComplexFabricator
{
	// Token: 0x17000432 RID: 1074
	// (get) Token: 0x06003CA9 RID: 15529 RVA: 0x00150F4B File Offset: 0x0014F14B
	public float OperatingTemp
	{
		get
		{
			return this.pe.Temperature;
		}
	}

	// Token: 0x17000433 RID: 1075
	// (get) Token: 0x06003CAA RID: 15530 RVA: 0x00150F58 File Offset: 0x0014F158
	public float TemperatureScaleFactor
	{
		get
		{
			return 1f - DataMinerConfig.TEMPERATURE_SCALING_RANGE.LerpFactorClamped(this.OperatingTemp);
		}
	}

	// Token: 0x17000434 RID: 1076
	// (get) Token: 0x06003CAB RID: 15531 RVA: 0x00150F70 File Offset: 0x0014F170
	public float EfficiencyRate
	{
		get
		{
			return DataMinerConfig.PRODUCTION_RATE_SCALE.Lerp(this.TemperatureScaleFactor);
		}
	}

	// Token: 0x06003CAC RID: 15532 RVA: 0x00150F84 File Offset: 0x0014F184
	protected override float ComputeWorkProgress(float dt, ComplexRecipe recipe)
	{
		float efficiencyRate = this.EfficiencyRate;
		this.minEfficiency = Mathf.Min(this.minEfficiency, efficiencyRate);
		return base.ComputeWorkProgress(dt, recipe) * efficiencyRate;
	}

	// Token: 0x06003CAD RID: 15533 RVA: 0x00150FB4 File Offset: 0x0014F1B4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.meter = new MeterController(this, Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Array.Empty<string>());
		base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.DataMinerEfficiency, this);
	}

	// Token: 0x06003CAE RID: 15534 RVA: 0x00150FEC File Offset: 0x0014F1EC
	public override void CompleteWorkingOrder()
	{
		if (this.minEfficiency == DataMinerConfig.PRODUCTION_RATE_SCALE.max)
		{
			SaveGame.Instance.ColonyAchievementTracker.efficientlyGatheredData = true;
		}
		this.minEfficiency = DataMinerConfig.PRODUCTION_RATE_SCALE.max;
		base.CompleteWorkingOrder();
	}

	// Token: 0x06003CAF RID: 15535 RVA: 0x00151026 File Offset: 0x0014F226
	public override void Sim1000ms(float dt)
	{
		base.Sim1000ms(dt);
		this.meter.SetPositionPercent(this.TemperatureScaleFactor);
	}

	// Token: 0x04002527 RID: 9511
	[MyCmpReq]
	private PrimaryElement pe;

	// Token: 0x04002528 RID: 9512
	[Serialize]
	private float minEfficiency = DataMinerConfig.PRODUCTION_RATE_SCALE.max;

	// Token: 0x04002529 RID: 9513
	private MeterController meter;
}
