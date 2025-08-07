using System;
using System.Collections.Generic;
using Klei;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200085A RID: 2138
public class CreatureSimTemperatureTransfer : SimTemperatureTransfer, ISim200ms
{
	// Token: 0x06003AB5 RID: 15029 RVA: 0x001465C4 File Offset: 0x001447C4
	protected override void OnPrefabInit()
	{
		this.primaryElement = base.GetComponent<PrimaryElement>();
		this.average_kilowatts_exchanged = new RunningWeightedAverage(-10f, 10f, 20, true);
		this.averageTemperatureTransferPerSecond = new AttributeModifier(this.temperatureAttributeName + "Delta", 0f, DUPLICANTS.MODIFIERS.TEMPEXCHANGE.NAME, false, true, false);
		this.GetAttributes().Add(this.averageTemperatureTransferPerSecond);
		base.OnPrefabInit();
	}

	// Token: 0x06003AB6 RID: 15030 RVA: 0x0014663C File Offset: 0x0014483C
	protected override void OnSpawn()
	{
		AttributeInstance attributeInstance = base.gameObject.GetAttributes().Add(Db.Get().Attributes.ThermalConductivityBarrier);
		AttributeModifier attributeModifier = new AttributeModifier(Db.Get().Attributes.ThermalConductivityBarrier.Id, this.skinThickness, this.skinThicknessAttributeModifierName, false, false, true);
		attributeInstance.Add(attributeModifier);
		base.OnSpawn();
	}

	// Token: 0x17000403 RID: 1027
	// (get) Token: 0x06003AB7 RID: 15031 RVA: 0x0014669D File Offset: 0x0014489D
	public bool LastTemperatureRecordIsReliable
	{
		get
		{
			return Time.time - this.lastTemperatureRecordTime < 2f && this.average_kilowatts_exchanged.HasEverHadValidValues && this.average_kilowatts_exchanged.ValidRecordsInLastSeconds(4f) > 5;
		}
	}

	// Token: 0x06003AB8 RID: 15032 RVA: 0x001466D4 File Offset: 0x001448D4
	protected unsafe void unsafeUpdateAverageKiloWattsExchanged(float dt)
	{
		if (Time.time < this.lastTemperatureRecordTime + 0.2f)
		{
			return;
		}
		if (Sim.IsValidHandle(this.simHandle))
		{
			int handleIndex = Sim.GetHandleIndex(this.simHandle);
			if (Game.Instance.simData.elementChunks[handleIndex].deltaKJ == 0f)
			{
				return;
			}
			this.average_kilowatts_exchanged.AddSample(Game.Instance.simData.elementChunks[handleIndex].deltaKJ, Time.time);
			this.lastTemperatureRecordTime = Time.time;
		}
	}

	// Token: 0x06003AB9 RID: 15033 RVA: 0x0014676D File Offset: 0x0014496D
	private void Update()
	{
		this.unsafeUpdateAverageKiloWattsExchanged(Time.deltaTime);
	}

	// Token: 0x06003ABA RID: 15034 RVA: 0x0014677C File Offset: 0x0014497C
	public void Sim200ms(float dt)
	{
		this.averageTemperatureTransferPerSecond.SetValue(SimUtil.EnergyFlowToTemperatureDelta(this.average_kilowatts_exchanged.GetUnweightedAverage, this.primaryElement.Element.specificHeatCapacity, this.primaryElement.Mass));
		float num = 0f;
		foreach (AttributeModifier attributeModifier in this.NonSimTemperatureModifiers)
		{
			num += attributeModifier.Value;
		}
		if (Sim.IsValidHandle(this.simHandle))
		{
			float num2 = num * (this.primaryElement.Mass * 1000f) * this.primaryElement.Element.specificHeatCapacity * 0.001f;
			float num3 = num2 * dt;
			SimMessages.ModifyElementChunkEnergy(this.simHandle, num3);
			this.heatEffect.SetHeatBeingProducedValue(num2);
			return;
		}
		this.heatEffect.SetHeatBeingProducedValue(0f);
	}

	// Token: 0x06003ABB RID: 15035 RVA: 0x00146874 File Offset: 0x00144A74
	public void RefreshRegistration()
	{
		base.SimUnregister();
		AttributeInstance attributeInstance = base.gameObject.GetAttributes().Get(Db.Get().Attributes.ThermalConductivityBarrier);
		this.thickness = attributeInstance.GetTotalValue();
		this.simHandle = -1;
		base.SimRegister();
	}

	// Token: 0x06003ABC RID: 15036 RVA: 0x001468C0 File Offset: 0x00144AC0
	public static float PotentialEnergyFlowToCreature(int cell, PrimaryElement transfererPrimaryElement, SimTemperatureTransfer temperatureTransferer, float deltaTime = 1f)
	{
		return SimUtil.CalculateEnergyFlowCreatures(cell, transfererPrimaryElement.Temperature, transfererPrimaryElement.Element.specificHeatCapacity, transfererPrimaryElement.Element.thermalConductivity, temperatureTransferer.SurfaceArea, temperatureTransferer.Thickness);
	}

	// Token: 0x040023FB RID: 9211
	public string temperatureAttributeName = "Temperature";

	// Token: 0x040023FC RID: 9212
	public float skinThickness = DUPLICANTSTATS.STANDARD.Temperature.SKIN_THICKNESS;

	// Token: 0x040023FD RID: 9213
	public string skinThicknessAttributeModifierName = DUPLICANTS.MODEL.STANDARD.NAME;

	// Token: 0x040023FE RID: 9214
	public AttributeModifier averageTemperatureTransferPerSecond;

	// Token: 0x040023FF RID: 9215
	[MyCmpAdd]
	private KBatchedAnimHeatPostProcessingEffect heatEffect;

	// Token: 0x04002400 RID: 9216
	private PrimaryElement primaryElement;

	// Token: 0x04002401 RID: 9217
	public RunningWeightedAverage average_kilowatts_exchanged;

	// Token: 0x04002402 RID: 9218
	public List<AttributeModifier> NonSimTemperatureModifiers = new List<AttributeModifier>();

	// Token: 0x04002403 RID: 9219
	private float lastTemperatureRecordTime;
}
