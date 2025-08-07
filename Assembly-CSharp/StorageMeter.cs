using System;

// Token: 0x020007D2 RID: 2002
public class StorageMeter : KMonoBehaviour
{
	// Token: 0x060035CC RID: 13772 RVA: 0x0012C766 File Offset: 0x0012A966
	public void SetInterpolateFunction(Func<float, int, float> func)
	{
		this.interpolateFunction = func;
		if (this.meter != null)
		{
			this.meter.interpolateFunction = this.interpolateFunction;
		}
	}

	// Token: 0x060035CD RID: 13773 RVA: 0x0012C788 File Offset: 0x0012A988
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x060035CE RID: 13774 RVA: 0x0012C790 File Offset: 0x0012A990
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_frame", "meter_level" });
		this.meter.interpolateFunction = this.interpolateFunction;
		this.UpdateMeter(null);
		base.Subscribe(-1697596308, new Action<object>(this.UpdateMeter));
	}

	// Token: 0x060035CF RID: 13775 RVA: 0x0012C80F File Offset: 0x0012AA0F
	private void UpdateMeter(object data)
	{
		this.meter.SetPositionPercent(this.storage.MassStored() / this.storage.Capacity());
	}

	// Token: 0x0400208C RID: 8332
	[MyCmpGet]
	private Storage storage;

	// Token: 0x0400208D RID: 8333
	private MeterController meter;

	// Token: 0x0400208E RID: 8334
	private Func<float, int, float> interpolateFunction = new Func<float, int, float>(MeterController.MinMaxStepLerp);
}
