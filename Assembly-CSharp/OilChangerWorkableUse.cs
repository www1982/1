using System;
using Klei;
using Klei.AI;
using UnityEngine;

// Token: 0x0200079A RID: 1946
public class OilChangerWorkableUse : Workable, IGameObjectEffectDescriptor
{
	// Token: 0x06003376 RID: 13174 RVA: 0x00121900 File Offset: 0x0011FB00
	private OilChangerWorkableUse()
	{
		base.SetReportType(ReportManager.ReportType.PersonalTime);
	}

	// Token: 0x06003377 RID: 13175 RVA: 0x00121910 File Offset: 0x0011FB10
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.operational = base.GetComponent<Operational>();
		this.showProgressBar = true;
		this.resetProgressOnStop = true;
		this.attributeConverter = Db.Get().AttributeConverters.ToiletSpeed;
		base.SetWorkTime(8.5f);
	}

	// Token: 0x06003378 RID: 13176 RVA: 0x00121960 File Offset: 0x0011FB60
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		if (worker != null)
		{
			Vector3 position = worker.transform.GetPosition();
			position.z = Grid.GetLayerZ(Grid.SceneLayer.BuildingUse);
			worker.transform.SetPosition(position);
		}
		Room roomOfGameObject = Game.Instance.roomProber.GetRoomOfGameObject(base.gameObject);
		if (roomOfGameObject != null)
		{
			roomOfGameObject.roomType.TriggerRoomEffects(base.GetComponent<KPrefabID>(), worker.GetComponent<Effects>());
		}
		this.operational.SetActive(true, false);
	}

	// Token: 0x06003379 RID: 13177 RVA: 0x001219E4 File Offset: 0x0011FBE4
	protected override void OnStopWork(WorkerBase worker)
	{
		if (worker != null)
		{
			Vector3 position = worker.transform.GetPosition();
			position.z = Grid.GetLayerZ(Grid.SceneLayer.Move);
			worker.transform.SetPosition(position);
		}
		this.operational.SetActive(false, false);
		base.OnStopWork(worker);
	}

	// Token: 0x0600337A RID: 13178 RVA: 0x00121A38 File Offset: 0x0011FC38
	protected override void OnCompleteWork(WorkerBase worker)
	{
		Storage component = base.GetComponent<Storage>();
		BionicOilMonitor.Instance smi = worker.GetSMI<BionicOilMonitor.Instance>();
		if (smi != null)
		{
			float num = 200f - smi.CurrentOilMass;
			float num2 = Mathf.Min(component.GetMassAvailable(GameTags.LubricatingOil), num);
			float num3 = num2;
			float num4 = 0f;
			Storage component2 = base.GetComponent<Storage>();
			SimHashes simHashes = SimHashes.CrudeOil;
			foreach (SimHashes simHashes2 in BionicOilMonitor.LUBRICANT_TYPE_EFFECT.Keys)
			{
				float num5;
				SimUtil.DiseaseInfo diseaseInfo;
				float num6;
				component2.ConsumeAndGetDisease(simHashes2.CreateTag(), num3, out num5, out diseaseInfo, out num6);
				if (num5 > num4)
				{
					simHashes = simHashes2;
					num4 = num5;
				}
				num3 -= num5;
			}
			base.GetComponent<Storage>().ConsumeIgnoringDisease(GameTags.LubricatingOil, num3);
			smi.RefillOil(num2);
			BionicOilMonitor.ApplyLubricationEffects(worker.GetComponent<Effects>(), simHashes);
		}
		base.OnCompleteWork(worker);
	}

	// Token: 0x04001EED RID: 7917
	private Operational operational;
}
