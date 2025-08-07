using System;
using Klei;
using Klei.AI;
using Klei.AI.DiseaseGrowthRules;

// Token: 0x0200083A RID: 2106
public class ConduitDiseaseManager : KCompactedVector<ConduitDiseaseManager.Data>
{
	// Token: 0x060039B6 RID: 14774 RVA: 0x00140960 File Offset: 0x0013EB60
	private static ElemGrowthInfo GetGrowthInfo(byte disease_idx, ushort elem_idx)
	{
		ElemGrowthInfo elemGrowthInfo;
		if (disease_idx != 255)
		{
			elemGrowthInfo = Db.Get().Diseases[(int)disease_idx].elemGrowthInfo[(int)elem_idx];
		}
		else
		{
			elemGrowthInfo = Disease.DEFAULT_GROWTH_INFO;
		}
		return elemGrowthInfo;
	}

	// Token: 0x060039B7 RID: 14775 RVA: 0x0014099A File Offset: 0x0013EB9A
	public ConduitDiseaseManager(ConduitTemperatureManager temperature_manager)
		: base(0)
	{
		this.temperatureManager = temperature_manager;
	}

	// Token: 0x060039B8 RID: 14776 RVA: 0x001409AC File Offset: 0x0013EBAC
	public HandleVector<int>.Handle Allocate(HandleVector<int>.Handle temperature_handle, ref ConduitFlow.ConduitContents contents)
	{
		ushort elementIndex = ElementLoader.GetElementIndex(contents.element);
		ConduitDiseaseManager.Data data = new ConduitDiseaseManager.Data(temperature_handle, elementIndex, contents.mass, contents.diseaseIdx, contents.diseaseCount);
		return base.Allocate(data);
	}

	// Token: 0x060039B9 RID: 14777 RVA: 0x001409E8 File Offset: 0x0013EBE8
	public void SetData(HandleVector<int>.Handle handle, ref ConduitFlow.ConduitContents contents)
	{
		ConduitDiseaseManager.Data data = base.GetData(handle);
		data.diseaseCount = contents.diseaseCount;
		if (contents.diseaseIdx != data.diseaseIdx)
		{
			data.diseaseIdx = contents.diseaseIdx;
			ushort elementIndex = ElementLoader.GetElementIndex(contents.element);
			data.growthInfo = ConduitDiseaseManager.GetGrowthInfo(contents.diseaseIdx, elementIndex);
		}
		base.SetData(handle, data);
	}

	// Token: 0x060039BA RID: 14778 RVA: 0x00140A4C File Offset: 0x0013EC4C
	public void Sim200ms(float dt)
	{
		using (new KProfiler.Region("ConduitDiseaseManager.SimUpdate", null))
		{
			for (int i = 0; i < this.data.Count; i++)
			{
				ConduitDiseaseManager.Data data = this.data[i];
				if (data.diseaseIdx != 255)
				{
					float num = data.accumulatedError;
					num += data.growthInfo.CalculateDiseaseCountDelta(data.diseaseCount, data.mass, dt);
					Disease disease = Db.Get().Diseases[(int)data.diseaseIdx];
					float num2 = Disease.HalfLifeToGrowthRate(Disease.CalculateRangeHalfLife(this.temperatureManager.GetTemperature(data.temperatureHandle), ref disease.temperatureRange, ref disease.temperatureHalfLives), dt);
					num += (float)data.diseaseCount * num2 - (float)data.diseaseCount;
					int num3 = (int)num;
					data.accumulatedError = num - (float)num3;
					data.diseaseCount += num3;
					if (data.diseaseCount <= 0)
					{
						data.diseaseCount = 0;
						data.diseaseIdx = byte.MaxValue;
						data.accumulatedError = 0f;
					}
					this.data[i] = data;
				}
			}
		}
	}

	// Token: 0x060039BB RID: 14779 RVA: 0x00140B9C File Offset: 0x0013ED9C
	public void ModifyDiseaseCount(HandleVector<int>.Handle h, int disease_count_delta)
	{
		ConduitDiseaseManager.Data data = base.GetData(h);
		data.diseaseCount = Math.Max(0, data.diseaseCount + disease_count_delta);
		if (data.diseaseCount == 0)
		{
			data.diseaseIdx = byte.MaxValue;
		}
		base.SetData(h, data);
	}

	// Token: 0x060039BC RID: 14780 RVA: 0x00140BE4 File Offset: 0x0013EDE4
	public void AddDisease(HandleVector<int>.Handle h, byte disease_idx, int disease_count)
	{
		ConduitDiseaseManager.Data data = base.GetData(h);
		SimUtil.DiseaseInfo diseaseInfo = SimUtil.CalculateFinalDiseaseInfo(disease_idx, disease_count, data.diseaseIdx, data.diseaseCount);
		data.diseaseIdx = diseaseInfo.idx;
		data.diseaseCount = diseaseInfo.count;
		base.SetData(h, data);
	}

	// Token: 0x0400237E RID: 9086
	private ConduitTemperatureManager temperatureManager;

	// Token: 0x020017AB RID: 6059
	public struct Data
	{
		// Token: 0x06009A09 RID: 39433 RVA: 0x00388C75 File Offset: 0x00386E75
		public Data(HandleVector<int>.Handle temperature_handle, ushort elem_idx, float mass, byte disease_idx, int disease_count)
		{
			this.diseaseIdx = disease_idx;
			this.elemIdx = elem_idx;
			this.mass = mass;
			this.diseaseCount = disease_count;
			this.accumulatedError = 0f;
			this.temperatureHandle = temperature_handle;
			this.growthInfo = ConduitDiseaseManager.GetGrowthInfo(disease_idx, elem_idx);
		}

		// Token: 0x04007679 RID: 30329
		public byte diseaseIdx;

		// Token: 0x0400767A RID: 30330
		public ushort elemIdx;

		// Token: 0x0400767B RID: 30331
		public int diseaseCount;

		// Token: 0x0400767C RID: 30332
		public float accumulatedError;

		// Token: 0x0400767D RID: 30333
		public float mass;

		// Token: 0x0400767E RID: 30334
		public HandleVector<int>.Handle temperatureHandle;

		// Token: 0x0400767F RID: 30335
		public ElemGrowthInfo growthInfo;
	}
}
