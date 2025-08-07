using System;
using Klei;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x020009C1 RID: 2497
[AddComponentMenu("KMonoBehaviour/Workable/MechanicalSurfboardWorkable")]
public class MechanicalSurfboardWorkable : Workable, IWorkerPrioritizable
{
	// Token: 0x060048E6 RID: 18662 RVA: 0x001A4581 File Offset: 0x001A2781
	private MechanicalSurfboardWorkable()
	{
		base.SetReportType(ReportManager.ReportType.PersonalTime);
	}

	// Token: 0x060048E7 RID: 18663 RVA: 0x001A4591 File Offset: 0x001A2791
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.showProgressBar = true;
		this.resetProgressOnStop = true;
		this.synchronizeAnims = true;
		base.SetWorkTime(30f);
		this.surfboard = base.GetComponent<MechanicalSurfboard>();
	}

	// Token: 0x060048E8 RID: 18664 RVA: 0x001A45C5 File Offset: 0x001A27C5
	protected override void OnStartWork(WorkerBase worker)
	{
		this.operational.SetActive(true, false);
		worker.GetComponent<Effects>().Add("MechanicalSurfing", false);
	}

	// Token: 0x060048E9 RID: 18665 RVA: 0x001A45E8 File Offset: 0x001A27E8
	public override Workable.AnimInfo GetAnim(WorkerBase worker)
	{
		Workable.AnimInfo animInfo = default(Workable.AnimInfo);
		AttributeInstance attributeInstance = worker.GetAttributes().Get(Db.Get().Attributes.Athletics);
		if (attributeInstance.GetTotalValue() <= 7f)
		{
			animInfo.overrideAnims = new KAnimFile[] { Assets.GetAnim(this.surfboard.interactAnims[0]) };
		}
		else if (attributeInstance.GetTotalValue() <= 15f)
		{
			animInfo.overrideAnims = new KAnimFile[] { Assets.GetAnim(this.surfboard.interactAnims[1]) };
		}
		else
		{
			animInfo.overrideAnims = new KAnimFile[] { Assets.GetAnim(this.surfboard.interactAnims[2]) };
		}
		return animInfo;
	}

	// Token: 0x060048EA RID: 18666 RVA: 0x001A46AC File Offset: 0x001A28AC
	protected override bool OnWorkTick(WorkerBase worker, float dt)
	{
		Building component = base.GetComponent<Building>();
		MechanicalSurfboard component2 = base.GetComponent<MechanicalSurfboard>();
		int widthInCells = component.Def.WidthInCells;
		int num = -(widthInCells - 1) / 2;
		int num2 = widthInCells / 2;
		int num3 = global::UnityEngine.Random.Range(num, num2);
		float num4 = component2.waterSpillRateKG * dt;
		float num5;
		SimUtil.DiseaseInfo diseaseInfo;
		float num6;
		base.GetComponent<Storage>().ConsumeAndGetDisease(SimHashes.Water.CreateTag(), num4, out num5, out diseaseInfo, out num6);
		int num7 = Grid.OffsetCell(Grid.PosToCell(base.gameObject), new CellOffset(num3, 0));
		ushort elementIndex = ElementLoader.GetElementIndex(SimHashes.Water);
		FallingWater.instance.AddParticle(num7, elementIndex, num5, num6, diseaseInfo.idx, diseaseInfo.count, true, false, false, false);
		return false;
	}

	// Token: 0x060048EB RID: 18667 RVA: 0x001A4754 File Offset: 0x001A2954
	protected override void OnCompleteWork(WorkerBase worker)
	{
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(this.surfboard.specificEffect))
		{
			component.Add(this.surfboard.specificEffect, true);
		}
		if (!string.IsNullOrEmpty(this.surfboard.trackingEffect))
		{
			component.Add(this.surfboard.trackingEffect, true);
		}
	}

	// Token: 0x060048EC RID: 18668 RVA: 0x001A47B2 File Offset: 0x001A29B2
	protected override void OnStopWork(WorkerBase worker)
	{
		this.operational.SetActive(false, false);
		worker.GetComponent<Effects>().Remove("MechanicalSurfing");
	}

	// Token: 0x060048ED RID: 18669 RVA: 0x001A47D4 File Offset: 0x001A29D4
	public bool GetWorkerPriority(WorkerBase worker, out int priority)
	{
		priority = this.basePriority;
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(this.surfboard.trackingEffect) && component.HasEffect(this.surfboard.trackingEffect))
		{
			priority = 0;
			return false;
		}
		if (!string.IsNullOrEmpty(this.surfboard.specificEffect) && component.HasEffect(this.surfboard.specificEffect))
		{
			priority = RELAXATION.PRIORITY.RECENTLY_USED;
		}
		return true;
	}

	// Token: 0x04003007 RID: 12295
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04003008 RID: 12296
	public int basePriority;

	// Token: 0x04003009 RID: 12297
	private MechanicalSurfboard surfboard;
}
