using System;
using System.Collections.Generic;
using Database;
using Klei;
using Klei.AI;
using Klei.AI.DiseaseGrowthRules;
using UnityEngine;

// Token: 0x020008C2 RID: 2242
public class DiseaseContainers : KGameObjectSplitComponentManager<DiseaseHeader, DiseaseContainer>
{
	// Token: 0x06003E14 RID: 15892 RVA: 0x0015B478 File Offset: 0x00159678
	public HandleVector<int>.Handle Add(GameObject go, byte disease_idx, int disease_count)
	{
		DiseaseHeader diseaseHeader = new DiseaseHeader
		{
			diseaseIdx = disease_idx,
			diseaseCount = disease_count,
			primaryElement = go.GetComponent<PrimaryElement>()
		};
		DiseaseContainer diseaseContainer = new DiseaseContainer(go, diseaseHeader.primaryElement.Element.idx);
		if (disease_idx != 255)
		{
			this.EvaluateGrowthConstants(diseaseHeader, ref diseaseContainer);
		}
		return base.Add(go, diseaseHeader, ref diseaseContainer);
	}

	// Token: 0x06003E15 RID: 15893 RVA: 0x0015B4E0 File Offset: 0x001596E0
	protected override void OnCleanUp(HandleVector<int>.Handle h)
	{
		AutoDisinfectable autoDisinfectable = base.GetPayload(h).autoDisinfectable;
		if (autoDisinfectable != null)
		{
			AutoDisinfectableManager.Instance.RemoveAutoDisinfectable(autoDisinfectable);
		}
		base.OnCleanUp(h);
	}

	// Token: 0x06003E16 RID: 15894 RVA: 0x0015B518 File Offset: 0x00159718
	public override void Sim200ms(float dt)
	{
		ListPool<int, DiseaseContainers>.PooledList pooledList = ListPool<int, DiseaseContainers>.Allocate();
		pooledList.Capacity = Math.Max(pooledList.Capacity, this.headers.Count);
		for (int i = 0; i < this.headers.Count; i++)
		{
			DiseaseHeader diseaseHeader = this.headers[i];
			if (diseaseHeader.diseaseIdx != 255 && diseaseHeader.primaryElement != null)
			{
				pooledList.Add(i);
			}
		}
		bool flag = Sim.IsRadiationEnabled();
		foreach (int num in pooledList)
		{
			DiseaseContainer diseaseContainer = this.payloads[num];
			DiseaseHeader diseaseHeader2 = this.headers[num];
			Disease disease = Db.Get().Diseases[(int)diseaseHeader2.diseaseIdx];
			float num2 = DiseaseContainers.CalculateDelta(diseaseHeader2, ref diseaseContainer, disease, dt, flag);
			num2 += diseaseContainer.accumulatedError;
			int num3 = (int)num2;
			diseaseContainer.accumulatedError = num2 - (float)num3;
			bool flag2 = diseaseHeader2.diseaseCount > diseaseContainer.overpopulationCount;
			bool flag3 = diseaseHeader2.diseaseCount + num3 > diseaseContainer.overpopulationCount;
			if (flag2 != flag3)
			{
				this.EvaluateGrowthConstants(diseaseHeader2, ref diseaseContainer);
			}
			diseaseHeader2.diseaseCount += num3;
			if (diseaseHeader2.diseaseCount <= 0)
			{
				diseaseContainer.accumulatedError = 0f;
				diseaseHeader2.diseaseCount = 0;
				diseaseHeader2.diseaseIdx = byte.MaxValue;
			}
			this.headers[num] = diseaseHeader2;
			this.payloads[num] = diseaseContainer;
		}
		pooledList.Recycle();
	}

	// Token: 0x06003E17 RID: 15895 RVA: 0x0015B6D8 File Offset: 0x001598D8
	private static float CalculateDelta(DiseaseHeader header, ref DiseaseContainer container, Disease disease, float dt, bool radiation_enabled)
	{
		return DiseaseContainers.CalculateDelta(header.diseaseCount, container.elemIdx, header.primaryElement.Mass, Grid.PosToCell(header.primaryElement.transform.GetPosition()), header.primaryElement.Temperature, container.instanceGrowthRate, disease, dt, radiation_enabled);
	}

	// Token: 0x06003E18 RID: 15896 RVA: 0x0015B72C File Offset: 0x0015992C
	public static float CalculateDelta(int disease_count, ushort element_idx, float mass, int environment_cell, float temperature, float tags_multiplier_base, Disease disease, float dt, bool radiation_enabled)
	{
		float num = 0f;
		ElemGrowthInfo elemGrowthInfo = disease.elemGrowthInfo[(int)element_idx];
		num += elemGrowthInfo.CalculateDiseaseCountDelta(disease_count, mass, dt);
		float num2 = Disease.HalfLifeToGrowthRate(Disease.CalculateRangeHalfLife(temperature, ref disease.temperatureRange, ref disease.temperatureHalfLives), dt);
		num += (float)disease_count * num2 - (float)disease_count;
		float num3 = Mathf.Pow(tags_multiplier_base, dt);
		num += (float)disease_count * num3 - (float)disease_count;
		if (Grid.IsValidCell(environment_cell))
		{
			ushort num4 = Grid.ElementIdx[environment_cell];
			ElemExposureInfo elemExposureInfo = disease.elemExposureInfo[(int)num4];
			num += elemExposureInfo.CalculateExposureDiseaseCountDelta(disease_count, dt);
			if (radiation_enabled)
			{
				float num5 = Grid.Radiation[environment_cell];
				if (num5 > 0f)
				{
					num -= num5 * disease.radiationKillRate;
				}
			}
		}
		return num;
	}

	// Token: 0x06003E19 RID: 15897 RVA: 0x0015B7F0 File Offset: 0x001599F0
	public int ModifyDiseaseCount(HandleVector<int>.Handle h, int disease_count_delta)
	{
		DiseaseHeader header = base.GetHeader(h);
		header.diseaseCount = Math.Max(0, header.diseaseCount + disease_count_delta);
		if (header.diseaseCount == 0)
		{
			header.diseaseIdx = byte.MaxValue;
			DiseaseContainer payload = base.GetPayload(h);
			payload.accumulatedError = 0f;
			base.SetPayload(h, ref payload);
		}
		base.SetHeader(h, header);
		return header.diseaseCount;
	}

	// Token: 0x06003E1A RID: 15898 RVA: 0x0015B85C File Offset: 0x00159A5C
	public int AddDisease(HandleVector<int>.Handle h, byte disease_idx, int disease_count)
	{
		DiseaseHeader diseaseHeader;
		DiseaseContainer diseaseContainer;
		base.GetData(h, out diseaseHeader, out diseaseContainer);
		SimUtil.DiseaseInfo diseaseInfo = SimUtil.CalculateFinalDiseaseInfo(disease_idx, disease_count, diseaseHeader.diseaseIdx, diseaseHeader.diseaseCount);
		bool flag = diseaseHeader.diseaseIdx != diseaseInfo.idx;
		diseaseHeader.diseaseIdx = diseaseInfo.idx;
		diseaseHeader.diseaseCount = diseaseInfo.count;
		if (flag && diseaseInfo.idx != 255)
		{
			this.EvaluateGrowthConstants(diseaseHeader, ref diseaseContainer);
			base.SetData(h, diseaseHeader, ref diseaseContainer);
		}
		else
		{
			base.SetHeader(h, diseaseHeader);
		}
		if (flag)
		{
			diseaseHeader.primaryElement.Trigger(-283306403, null);
		}
		return diseaseHeader.diseaseCount;
	}

	// Token: 0x06003E1B RID: 15899 RVA: 0x0015B8FC File Offset: 0x00159AFC
	private void GetVisualDiseaseIdxAndCount(DiseaseHeader header, ref DiseaseContainer payload, out int disease_idx, out int disease_count)
	{
		if (payload.visualDiseaseProvider == null)
		{
			disease_idx = (int)header.diseaseIdx;
			disease_count = header.diseaseCount;
			return;
		}
		disease_idx = 255;
		disease_count = 0;
		HandleVector<int>.Handle handle = GameComps.DiseaseContainers.GetHandle(payload.visualDiseaseProvider);
		if (handle != HandleVector<int>.InvalidHandle)
		{
			DiseaseHeader header2 = GameComps.DiseaseContainers.GetHeader(handle);
			disease_idx = (int)header2.diseaseIdx;
			disease_count = header2.diseaseCount;
		}
	}

	// Token: 0x06003E1C RID: 15900 RVA: 0x0015B970 File Offset: 0x00159B70
	public void UpdateOverlayColours()
	{
		GridArea visibleArea = GridVisibleArea.GetVisibleArea();
		Diseases diseases = Db.Get().Diseases;
		Color32 color = new Color32(0, 0, 0, byte.MaxValue);
		for (int i = 0; i < this.headers.Count; i++)
		{
			DiseaseContainer diseaseContainer = this.payloads[i];
			DiseaseHeader diseaseHeader = this.headers[i];
			KBatchedAnimController controller = diseaseContainer.controller;
			if (controller != null)
			{
				Color32 color2 = color;
				Vector3 position = controller.transform.GetPosition();
				if (visibleArea.Min <= position && position <= visibleArea.Max)
				{
					int num = 0;
					int num2 = 255;
					int num3 = 0;
					this.GetVisualDiseaseIdxAndCount(diseaseHeader, ref diseaseContainer, out num2, out num3);
					if (num2 != 255)
					{
						color2 = GlobalAssets.Instance.colorSet.GetColorByName(diseases[num2].overlayColourName);
						num = num3;
					}
					if (diseaseContainer.isContainer)
					{
						List<GameObject> items = diseaseHeader.primaryElement.GetComponent<Storage>().items;
						for (int j = 0; j < items.Count; j++)
						{
							GameObject gameObject = items[j];
							if (gameObject != null)
							{
								HandleVector<int>.Handle handle = base.GetHandle(gameObject);
								if (handle.IsValid())
								{
									DiseaseHeader header = base.GetHeader(handle);
									if (header.diseaseCount > num && header.diseaseIdx != 255)
									{
										num = header.diseaseCount;
										color2 = GlobalAssets.Instance.colorSet.GetColorByName(diseases[(int)header.diseaseIdx].overlayColourName);
									}
								}
							}
						}
					}
					color2.a = SimUtil.DiseaseCountToAlpha254(num);
					if (diseaseContainer.conduitType != ConduitType.None)
					{
						ConduitFlow flowManager = Conduit.GetFlowManager(diseaseContainer.conduitType);
						int num4 = Grid.PosToCell(position);
						ConduitFlow.ConduitContents contents = flowManager.GetContents(num4);
						if (contents.diseaseIdx != 255 && contents.diseaseCount > num)
						{
							num = contents.diseaseCount;
							color2 = GlobalAssets.Instance.colorSet.GetColorByName(diseases[(int)contents.diseaseIdx].overlayColourName);
							color2.a = byte.MaxValue;
						}
					}
				}
				controller.OverlayColour = color2;
			}
		}
	}

	// Token: 0x06003E1D RID: 15901 RVA: 0x0015BBB8 File Offset: 0x00159DB8
	private void EvaluateGrowthConstants(DiseaseHeader header, ref DiseaseContainer container)
	{
		Disease disease = Db.Get().Diseases[(int)header.diseaseIdx];
		KPrefabID component = header.primaryElement.GetComponent<KPrefabID>();
		ElemGrowthInfo elemGrowthInfo = disease.elemGrowthInfo[(int)header.diseaseIdx];
		container.overpopulationCount = (int)(elemGrowthInfo.maxCountPerKG * header.primaryElement.Mass);
		container.instanceGrowthRate = disease.GetGrowthRateForTags(component.Tags, header.diseaseCount > container.overpopulationCount);
	}

	// Token: 0x06003E1E RID: 15902 RVA: 0x0015BC34 File Offset: 0x00159E34
	public override void Clear()
	{
		base.Clear();
		for (int i = 0; i < this.payloads.Count; i++)
		{
			this.payloads[i].Clear();
		}
		this.headers.Clear();
		this.payloads.Clear();
		this.handles.Clear();
	}
}
