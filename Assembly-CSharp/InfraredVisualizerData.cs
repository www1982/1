using System;
using Klei.AI;
using UnityEngine;

// Token: 0x020005C5 RID: 1477
public struct InfraredVisualizerData
{
	// Token: 0x06002208 RID: 8712 RVA: 0x000C4190 File Offset: 0x000C2390
	public void Update()
	{
		float num = 0f;
		if (this.temperatureAmount != null)
		{
			num = this.temperatureAmount.value;
		}
		else if (this.structureTemperature.IsValid())
		{
			num = GameComps.StructureTemperatures.GetPayload(this.structureTemperature).Temperature;
		}
		else if (this.primaryElement != null)
		{
			num = this.primaryElement.Temperature;
		}
		else if (this.temperatureVulnerable != null)
		{
			num = this.temperatureVulnerable.InternalTemperature;
		}
		else if (this.critterTemperatureMonitorInstance != null)
		{
			num = this.critterTemperatureMonitorInstance.GetTemperatureInternal();
		}
		if (num < 0f)
		{
			return;
		}
		Color32 color = SimDebugView.Instance.NormalizedTemperature(num);
		this.controller.OverlayColour = color;
	}

	// Token: 0x06002209 RID: 8713 RVA: 0x000C4258 File Offset: 0x000C2458
	public InfraredVisualizerData(GameObject go)
	{
		this.controller = go.GetComponent<KBatchedAnimController>();
		if (this.controller != null)
		{
			this.temperatureAmount = Db.Get().Amounts.Temperature.Lookup(go);
			this.structureTemperature = GameComps.StructureTemperatures.GetHandle(go);
			this.primaryElement = go.GetComponent<PrimaryElement>();
			this.temperatureVulnerable = go.GetComponent<TemperatureVulnerable>();
			this.critterTemperatureMonitorInstance = go.GetSMI<CritterTemperatureMonitor.Instance>();
			return;
		}
		this.temperatureAmount = null;
		this.structureTemperature = HandleVector<int>.InvalidHandle;
		this.primaryElement = null;
		this.temperatureVulnerable = null;
		this.critterTemperatureMonitorInstance = null;
	}

	// Token: 0x040013DB RID: 5083
	public KAnimControllerBase controller;

	// Token: 0x040013DC RID: 5084
	public AmountInstance temperatureAmount;

	// Token: 0x040013DD RID: 5085
	public HandleVector<int>.Handle structureTemperature;

	// Token: 0x040013DE RID: 5086
	public PrimaryElement primaryElement;

	// Token: 0x040013DF RID: 5087
	public TemperatureVulnerable temperatureVulnerable;

	// Token: 0x040013E0 RID: 5088
	public CritterTemperatureMonitor.Instance critterTemperatureMonitorInstance;
}
