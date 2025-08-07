using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000716 RID: 1814
public class DirectVolumeHeater : KMonoBehaviour, ISim33ms, ISim200ms, ISim1000ms, ISim4000ms, IGameObjectEffectDescriptor
{
	// Token: 0x06002D8A RID: 11658 RVA: 0x00104F88 File Offset: 0x00103188
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.primaryElement = base.GetComponent<PrimaryElement>();
		this.structureTemperature = GameComps.StructureTemperatures.GetHandle(base.gameObject);
	}

	// Token: 0x06002D8B RID: 11659 RVA: 0x00104FB4 File Offset: 0x001031B4
	public void Sim33ms(float dt)
	{
		if (this.impulseFrequency == DirectVolumeHeater.TimeMode.ms33)
		{
			float num = 0f;
			num += this.AddHeatToVolume(dt);
			num += this.AddSelfHeat(dt);
			this.heatEffect.SetHeatBeingProducedValue(num);
		}
	}

	// Token: 0x06002D8C RID: 11660 RVA: 0x00104FF0 File Offset: 0x001031F0
	public void Sim200ms(float dt)
	{
		if (this.impulseFrequency == DirectVolumeHeater.TimeMode.ms200)
		{
			float num = 0f;
			num += this.AddHeatToVolume(dt);
			num += this.AddSelfHeat(dt);
			this.heatEffect.SetHeatBeingProducedValue(num);
		}
	}

	// Token: 0x06002D8D RID: 11661 RVA: 0x0010502C File Offset: 0x0010322C
	public void Sim1000ms(float dt)
	{
		if (this.impulseFrequency == DirectVolumeHeater.TimeMode.ms1000)
		{
			float num = 0f;
			num += this.AddHeatToVolume(dt);
			num += this.AddSelfHeat(dt);
			this.heatEffect.SetHeatBeingProducedValue(num);
		}
	}

	// Token: 0x06002D8E RID: 11662 RVA: 0x00105068 File Offset: 0x00103268
	public void Sim4000ms(float dt)
	{
		if (this.impulseFrequency == DirectVolumeHeater.TimeMode.ms4000)
		{
			float num = 0f;
			num += this.AddHeatToVolume(dt);
			num += this.AddSelfHeat(dt);
			this.heatEffect.SetHeatBeingProducedValue(num);
		}
	}

	// Token: 0x06002D8F RID: 11663 RVA: 0x001050A4 File Offset: 0x001032A4
	private float CalculateCellWeight(int dx, int dy, int maxDistance)
	{
		return 1f + (float)(maxDistance - Math.Abs(dx) - Math.Abs(dy));
	}

	// Token: 0x06002D90 RID: 11664 RVA: 0x001050BC File Offset: 0x001032BC
	private bool TestLineOfSight(int offsetCell)
	{
		int num = Grid.PosToCell(base.gameObject);
		int num2;
		int num3;
		Grid.CellToXY(offsetCell, out num2, out num3);
		int num4;
		int num5;
		Grid.CellToXY(num, out num4, out num5);
		return Grid.FastTestLineOfSightSolid(num4, num5, num2, num3);
	}

	// Token: 0x06002D91 RID: 11665 RVA: 0x001050F0 File Offset: 0x001032F0
	private float AddSelfHeat(float dt)
	{
		if (!this.EnableEmission)
		{
			return 0f;
		}
		if (this.primaryElement.Temperature > this.maximumInternalTemperature)
		{
			return 0f;
		}
		float num = 8f;
		GameComps.StructureTemperatures.ProduceEnergy(this.structureTemperature, 8f * dt, BUILDINGS.PREFABS.STEAMTURBINE2.HEAT_SOURCE, dt);
		return num;
	}

	// Token: 0x06002D92 RID: 11666 RVA: 0x0010514C File Offset: 0x0010334C
	private float AddHeatToVolume(float dt)
	{
		if (!this.EnableEmission)
		{
			return 0f;
		}
		int num = Grid.PosToCell(base.gameObject);
		int num2 = this.width / 2;
		int num3 = this.width % 2;
		int num4 = num2 + this.height;
		float num5 = 0f;
		float num6 = this.DTUs * dt / 1000f;
		for (int i = -num2; i < num2 + num3; i++)
		{
			for (int j = 0; j < this.height; j++)
			{
				if (Grid.IsCellOffsetValid(num, i, j))
				{
					int num7 = Grid.OffsetCell(num, i, j);
					if (!Grid.Solid[num7] && Grid.Mass[num7] != 0f && Grid.WorldIdx[num7] == Grid.WorldIdx[num] && this.TestLineOfSight(num7) && Grid.Temperature[num7] < this.maximumExternalTemperature)
					{
						num5 += this.CalculateCellWeight(i, j, num4);
					}
				}
			}
		}
		float num8 = num6;
		if (num5 > 0f)
		{
			num8 /= num5;
		}
		float num9 = 0f;
		for (int k = -num2; k < num2 + num3; k++)
		{
			for (int l = 0; l < this.height; l++)
			{
				if (Grid.IsCellOffsetValid(num, k, l))
				{
					int num10 = Grid.OffsetCell(num, k, l);
					if (!Grid.Solid[num10] && Grid.Mass[num10] != 0f && Grid.WorldIdx[num10] == Grid.WorldIdx[num] && this.TestLineOfSight(num10) && Grid.Temperature[num10] < this.maximumExternalTemperature)
					{
						float num11 = num8 * this.CalculateCellWeight(k, l, num4);
						num9 += num11;
						SimMessages.ModifyEnergy(num10, num11, 10000f, SimMessages.EnergySourceID.HeatBulb);
					}
				}
			}
		}
		return num9;
	}

	// Token: 0x06002D93 RID: 11667 RVA: 0x00105334 File Offset: 0x00103534
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		string formattedHeatEnergy = GameUtil.GetFormattedHeatEnergy(this.DTUs, GameUtil.HeatEnergyFormatterUnit.Automatic);
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.HEATGENERATED, formattedHeatEnergy), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.HEATGENERATED, formattedHeatEnergy), Descriptor.DescriptorType.Effect);
		list.Add(descriptor);
		return list;
	}

	// Token: 0x04001AC4 RID: 6852
	[SerializeField]
	public int width = 12;

	// Token: 0x04001AC5 RID: 6853
	[SerializeField]
	public int height = 4;

	// Token: 0x04001AC6 RID: 6854
	[SerializeField]
	public float DTUs = 100000f;

	// Token: 0x04001AC7 RID: 6855
	[SerializeField]
	public float maximumInternalTemperature = 773.15f;

	// Token: 0x04001AC8 RID: 6856
	[SerializeField]
	public float maximumExternalTemperature = 340f;

	// Token: 0x04001AC9 RID: 6857
	[SerializeField]
	public Operational operational;

	// Token: 0x04001ACA RID: 6858
	[MyCmpAdd]
	private KBatchedAnimHeatPostProcessingEffect heatEffect;

	// Token: 0x04001ACB RID: 6859
	public bool EnableEmission;

	// Token: 0x04001ACC RID: 6860
	private HandleVector<int>.Handle structureTemperature;

	// Token: 0x04001ACD RID: 6861
	private PrimaryElement primaryElement;

	// Token: 0x04001ACE RID: 6862
	[SerializeField]
	private DirectVolumeHeater.TimeMode impulseFrequency = DirectVolumeHeater.TimeMode.ms1000;

	// Token: 0x020015B4 RID: 5556
	private enum TimeMode
	{
		// Token: 0x04007095 RID: 28821
		ms33,
		// Token: 0x04007096 RID: 28822
		ms200,
		// Token: 0x04007097 RID: 28823
		ms1000,
		// Token: 0x04007098 RID: 28824
		ms4000
	}
}
