using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020007E5 RID: 2021
[AddComponentMenu("KMonoBehaviour/Workable/ToiletWorkableUse")]
public class ToiletWorkableUse : Workable, IGameObjectEffectDescriptor
{
	// Token: 0x060036BC RID: 14012 RVA: 0x001305E7 File Offset: 0x0012E7E7
	private ToiletWorkableUse()
	{
		base.SetReportType(ReportManager.ReportType.PersonalTime);
	}

	// Token: 0x060036BD RID: 14013 RVA: 0x00130618 File Offset: 0x0012E818
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.showProgressBar = true;
		this.resetProgressOnStop = true;
		this.attributeConverter = Db.Get().AttributeConverters.ToiletSpeed;
		base.SetWorkTime(8.5f);
	}

	// Token: 0x060036BE RID: 14014 RVA: 0x00130650 File Offset: 0x0012E850
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		if (Sim.IsRadiationEnabled() && worker.GetAmounts().Get(Db.Get().Amounts.RadiationBalance).value > 0f)
		{
			worker.gameObject.GetComponent<KSelectable>().AddStatusItem(Db.Get().DuplicantStatusItems.ExpellingRads, null);
		}
		Room roomOfGameObject = Game.Instance.roomProber.GetRoomOfGameObject(base.gameObject);
		if (roomOfGameObject != null)
		{
			roomOfGameObject.roomType.TriggerRoomEffects(base.GetComponent<KPrefabID>(), worker.GetComponent<Effects>());
		}
		if (worker != null)
		{
			this.last_user_id = worker.gameObject.PrefabID();
		}
	}

	// Token: 0x060036BF RID: 14015 RVA: 0x001306FC File Offset: 0x0012E8FC
	public override HashedString[] GetWorkPstAnims(WorkerBase worker, bool successfully_completed)
	{
		HashedString[] array = null;
		if (this.workerTypePstAnims.TryGetValue(worker.PrefabID(), out array))
		{
			this.workingPstComplete = array;
			this.workingPstFailed = array;
		}
		return base.GetWorkPstAnims(worker, successfully_completed);
	}

	// Token: 0x060036C0 RID: 14016 RVA: 0x00130738 File Offset: 0x0012E938
	public override Workable.AnimInfo GetAnim(WorkerBase worker)
	{
		KAnimFile[] array = null;
		if (this.workerTypeOverrideAnims.TryGetValue(worker.PrefabID(), out array))
		{
			this.overrideAnims = array;
		}
		return base.GetAnim(worker);
	}

	// Token: 0x060036C1 RID: 14017 RVA: 0x0013076A File Offset: 0x0012E96A
	protected override void OnStopWork(WorkerBase worker)
	{
		if (Sim.IsRadiationEnabled())
		{
			worker.gameObject.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().DuplicantStatusItems.ExpellingRads, false);
		}
		base.OnStopWork(worker);
	}

	// Token: 0x060036C2 RID: 14018 RVA: 0x0013079B File Offset: 0x0012E99B
	protected override void OnAbortWork(WorkerBase worker)
	{
		if (Sim.IsRadiationEnabled())
		{
			worker.gameObject.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().DuplicantStatusItems.ExpellingRads, false);
		}
		base.OnAbortWork(worker);
	}

	// Token: 0x060036C3 RID: 14019 RVA: 0x001307CC File Offset: 0x0012E9CC
	protected override void OnCompleteWork(WorkerBase worker)
	{
		AmountInstance amountInstance = Db.Get().Amounts.Bladder.Lookup(worker);
		if (amountInstance != null)
		{
			this.lastAmountOfWasteMassRemovedFromDupe = DUPLICANTSTATS.STANDARD.Secretions.PEE_PER_TOILET_PEE;
			this.lastElementRemovedFromDupe = SimHashes.DirtyWater;
			amountInstance.SetValue(0f);
		}
		else
		{
			GunkMonitor.Instance smi = worker.GetSMI<GunkMonitor.Instance>();
			if (smi != null)
			{
				this.lastAmountOfWasteMassRemovedFromDupe = smi.CurrentGunkMass;
				this.lastElementRemovedFromDupe = GunkMonitor.GunkElement;
				smi.SetGunkMassValue(0f);
				Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_GunkedToilet, true);
			}
		}
		if (Sim.IsRadiationEnabled())
		{
			worker.gameObject.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().DuplicantStatusItems.ExpellingRads, false);
			AmountInstance amountInstance2 = Db.Get().Amounts.RadiationBalance.Lookup(worker);
			RadiationMonitor.Instance smi2 = worker.GetSMI<RadiationMonitor.Instance>();
			float num = Math.Min(amountInstance2.value, 100f * smi2.difficultySettingMod);
			if (num >= 1f)
			{
				PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, Math.Floor((double)num).ToString() + UI.UNITSUFFIXES.RADIATION.RADS, worker.transform, Vector3.up * 2f, 1.5f, false, false);
			}
			amountInstance2.ApplyDelta(-num);
		}
		this.timesUsed++;
		if (amountInstance != null)
		{
			base.Trigger(-350347868, worker);
		}
		else
		{
			base.Trigger(1234642927, worker);
		}
		base.OnCompleteWork(worker);
	}

	// Token: 0x060036C4 RID: 14020 RVA: 0x0013094B File Offset: 0x0012EB4B
	public override StatusItem GetWorkerStatusItem()
	{
		if (base.worker != null && base.worker.gameObject.HasTag(GameTags.Minions.Models.Bionic))
		{
			return Db.Get().DuplicantStatusItems.CloggingToilet;
		}
		return base.GetWorkerStatusItem();
	}

	// Token: 0x0400210C RID: 8460
	public Dictionary<Tag, KAnimFile[]> workerTypeOverrideAnims = new Dictionary<Tag, KAnimFile[]>();

	// Token: 0x0400210D RID: 8461
	public Dictionary<Tag, HashedString[]> workerTypePstAnims = new Dictionary<Tag, HashedString[]>();

	// Token: 0x0400210E RID: 8462
	[Serialize]
	public int timesUsed;

	// Token: 0x0400210F RID: 8463
	[Serialize]
	public Tag last_user_id;

	// Token: 0x04002110 RID: 8464
	[Serialize]
	public SimHashes lastElementRemovedFromDupe = SimHashes.DirtyWater;

	// Token: 0x04002111 RID: 8465
	[Serialize]
	public float lastAmountOfWasteMassRemovedFromDupe;
}
