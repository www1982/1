using System;
using System.Collections.Generic;
using Klei;
using Klei.AI;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200064E RID: 1614
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/Workable")]
public class Workable : KMonoBehaviour, ISaveLoadable, IApproachable
{
	// Token: 0x170001C2 RID: 450
	// (get) Token: 0x0600271C RID: 10012 RVA: 0x000E00A1 File Offset: 0x000DE2A1
	// (set) Token: 0x0600271D RID: 10013 RVA: 0x000E00A9 File Offset: 0x000DE2A9
	public WorkerBase worker { get; protected set; }

	// Token: 0x170001C3 RID: 451
	// (get) Token: 0x0600271E RID: 10014 RVA: 0x000E00B2 File Offset: 0x000DE2B2
	// (set) Token: 0x0600271F RID: 10015 RVA: 0x000E00BA File Offset: 0x000DE2BA
	public float WorkTimeRemaining
	{
		get
		{
			return this.workTimeRemaining;
		}
		set
		{
			this.workTimeRemaining = value;
		}
	}

	// Token: 0x170001C4 RID: 452
	// (get) Token: 0x06002720 RID: 10016 RVA: 0x000E00C3 File Offset: 0x000DE2C3
	// (set) Token: 0x06002721 RID: 10017 RVA: 0x000E00CB File Offset: 0x000DE2CB
	public bool preferUnreservedCell { get; set; }

	// Token: 0x06002722 RID: 10018 RVA: 0x000E00D4 File Offset: 0x000DE2D4
	public virtual float GetWorkTime()
	{
		return this.workTime;
	}

	// Token: 0x06002723 RID: 10019 RVA: 0x000E00DC File Offset: 0x000DE2DC
	public WorkerBase GetWorker()
	{
		return this.worker;
	}

	// Token: 0x06002724 RID: 10020 RVA: 0x000E00E4 File Offset: 0x000DE2E4
	public virtual float GetPercentComplete()
	{
		if (this.workTimeRemaining > this.workTime)
		{
			return -1f;
		}
		return 1f - this.workTimeRemaining / this.workTime;
	}

	// Token: 0x06002725 RID: 10021 RVA: 0x000E010D File Offset: 0x000DE30D
	public void ConfigureMultitoolContext(HashedString context, Tag hitEffectTag)
	{
		this.multitoolContext = context;
		this.multitoolHitEffectTag = hitEffectTag;
	}

	// Token: 0x06002726 RID: 10022 RVA: 0x000E0120 File Offset: 0x000DE320
	public virtual Workable.AnimInfo GetAnim(WorkerBase worker)
	{
		Workable.AnimInfo animInfo = default(Workable.AnimInfo);
		if (this.overrideAnims != null && this.overrideAnims.Length != 0)
		{
			BuildingFacade buildingFacade = this.GetBuildingFacade();
			bool flag = false;
			if (buildingFacade != null && !buildingFacade.IsOriginal)
			{
				flag = buildingFacade.interactAnims.TryGetValue(base.name, out animInfo.overrideAnims);
			}
			if (!flag)
			{
				animInfo.overrideAnims = this.overrideAnims;
			}
		}
		if (this.multitoolContext.IsValid && this.multitoolHitEffectTag.IsValid)
		{
			animInfo.smi = new MultitoolController.Instance(this, worker, this.multitoolContext, Assets.GetPrefab(this.multitoolHitEffectTag));
		}
		return animInfo;
	}

	// Token: 0x06002727 RID: 10023 RVA: 0x000E01C3 File Offset: 0x000DE3C3
	public virtual HashedString[] GetWorkAnims(WorkerBase worker)
	{
		return this.workAnims;
	}

	// Token: 0x06002728 RID: 10024 RVA: 0x000E01CB File Offset: 0x000DE3CB
	public virtual KAnim.PlayMode GetWorkAnimPlayMode()
	{
		return this.workAnimPlayMode;
	}

	// Token: 0x06002729 RID: 10025 RVA: 0x000E01D3 File Offset: 0x000DE3D3
	public virtual HashedString[] GetWorkPstAnims(WorkerBase worker, bool successfully_completed)
	{
		if (successfully_completed)
		{
			return this.workingPstComplete;
		}
		return this.workingPstFailed;
	}

	// Token: 0x0600272A RID: 10026 RVA: 0x000E01E5 File Offset: 0x000DE3E5
	public virtual Vector3 GetWorkOffset()
	{
		return Vector3.zero;
	}

	// Token: 0x0600272B RID: 10027 RVA: 0x000E01EC File Offset: 0x000DE3EC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().MiscStatusItems.Using;
		this.workingStatusItem = Db.Get().MiscStatusItems.Operating;
		this.readyForSkillWorkStatusItem = Db.Get().BuildingStatusItems.RequiresSkillPerk;
		this.workTime = this.GetWorkTime();
		this.workTimeRemaining = Mathf.Min(this.workTimeRemaining, this.workTime);
	}

	// Token: 0x0600272C RID: 10028 RVA: 0x000E0264 File Offset: 0x000DE464
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.shouldShowSkillPerkStatusItem && !string.IsNullOrEmpty(this.requiredSkillPerk))
		{
			if (this.skillsUpdateHandle != -1)
			{
				Game.Instance.Unsubscribe(this.skillsUpdateHandle);
			}
			this.skillsUpdateHandle = Game.Instance.Subscribe(-1523247426, new Action<object>(this.UpdateStatusItem));
		}
		if (this.requireMinionToWork && this.minionUpdateHandle != -1)
		{
			Game.Instance.Unsubscribe(this.minionUpdateHandle);
		}
		this.minionUpdateHandle = Game.Instance.Subscribe(586301400, new Action<object>(this.UpdateStatusItem));
		base.GetComponent<KPrefabID>().AddTag(GameTags.HasChores, false);
		if (base.gameObject.HasTag(this.laboratoryEfficiencyBonusTagRequired))
		{
			this.useLaboratoryEfficiencyBonus = true;
			base.Subscribe<Workable>(144050788, Workable.OnUpdateRoomDelegate);
		}
		this.ShowProgressBar(this.alwaysShowProgressBar && this.workTimeRemaining < this.GetWorkTime());
		this.UpdateStatusItem(null);
	}

	// Token: 0x0600272D RID: 10029 RVA: 0x000E036C File Offset: 0x000DE56C
	private void RefreshRoom()
	{
		CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(Grid.PosToCell(base.gameObject));
		if (cavityForCell != null && cavityForCell.room != null)
		{
			this.OnUpdateRoom(cavityForCell.room);
			return;
		}
		this.OnUpdateRoom(null);
	}

	// Token: 0x0600272E RID: 10030 RVA: 0x000E03B4 File Offset: 0x000DE5B4
	private void OnUpdateRoom(object data)
	{
		if (this.worker == null)
		{
			return;
		}
		Room room = (Room)data;
		if (room != null && room.roomType == Db.Get().RoomTypes.Laboratory)
		{
			this.currentlyInLaboratory = true;
			if (this.laboratoryEfficiencyBonusStatusItemHandle == Guid.Empty)
			{
				this.laboratoryEfficiencyBonusStatusItemHandle = this.worker.OfferStatusItem(Db.Get().DuplicantStatusItems.LaboratoryWorkEfficiencyBonus, this);
				return;
			}
		}
		else
		{
			this.currentlyInLaboratory = false;
			if (this.laboratoryEfficiencyBonusStatusItemHandle != Guid.Empty)
			{
				this.worker.RevokeStatusItem(this.laboratoryEfficiencyBonusStatusItemHandle);
				this.laboratoryEfficiencyBonusStatusItemHandle = Guid.Empty;
			}
		}
	}

	// Token: 0x0600272F RID: 10031 RVA: 0x000E0464 File Offset: 0x000DE664
	protected virtual void UpdateStatusItem(object data = null)
	{
		KSelectable component = base.GetComponent<KSelectable>();
		if (component == null)
		{
			return;
		}
		component.RemoveStatusItem(this.workStatusItemHandle, false);
		if (this.worker == null)
		{
			if (this.requireMinionToWork && Components.LiveMinionIdentities.GetWorldItems(this.GetMyWorldId(), false).Count == 0)
			{
				this.workStatusItemHandle = component.AddStatusItem(Db.Get().BuildingStatusItems.WorkRequiresMinion, null);
				return;
			}
			if (this.shouldShowSkillPerkStatusItem && !string.IsNullOrEmpty(this.requiredSkillPerk))
			{
				if (!MinionResume.AnyMinionHasPerk(this.requiredSkillPerk, this.GetMyWorldId()))
				{
					StatusItem statusItem = (DlcManager.FeatureClusterSpaceEnabled() ? Db.Get().BuildingStatusItems.ClusterColonyLacksRequiredSkillPerk : Db.Get().BuildingStatusItems.ColonyLacksRequiredSkillPerk);
					this.workStatusItemHandle = component.AddStatusItem(statusItem, this.requiredSkillPerk);
					return;
				}
				this.workStatusItemHandle = component.AddStatusItem(this.readyForSkillWorkStatusItem, this.requiredSkillPerk);
				return;
			}
		}
		else if (this.workingStatusItem != null)
		{
			this.workStatusItemHandle = component.AddStatusItem(this.workingStatusItem, this);
		}
	}

	// Token: 0x06002730 RID: 10032 RVA: 0x000E057C File Offset: 0x000DE77C
	protected override void OnLoadLevel()
	{
		this.overrideAnims = null;
		base.OnLoadLevel();
	}

	// Token: 0x06002731 RID: 10033 RVA: 0x000E058B File Offset: 0x000DE78B
	public virtual int GetCell()
	{
		return Grid.PosToCell(this);
	}

	// Token: 0x06002732 RID: 10034 RVA: 0x000E0594 File Offset: 0x000DE794
	public void StartWork(WorkerBase worker_to_start)
	{
		global::Debug.Assert(worker_to_start != null, "How did we get a null worker?");
		this.worker = worker_to_start;
		this.UpdateStatusItem(null);
		if (this.showProgressBar)
		{
			this.ShowProgressBar(true);
		}
		if (this.useLaboratoryEfficiencyBonus)
		{
			this.RefreshRoom();
		}
		this.OnStartWork(this.worker);
		if (this.worker != null)
		{
			string conversationTopic = this.GetConversationTopic();
			if (conversationTopic != null)
			{
				this.worker.Trigger(937885943, conversationTopic);
			}
		}
		if (this.OnWorkableEventCB != null)
		{
			this.OnWorkableEventCB(this, Workable.WorkableEvent.WorkStarted);
		}
		this.numberOfUses++;
		if (this.worker != null)
		{
			if (base.gameObject.GetComponent<KSelectable>() != null && base.gameObject.GetComponent<KSelectable>().IsSelected && this.worker.gameObject.GetComponent<LoopingSounds>() != null)
			{
				this.worker.gameObject.GetComponent<LoopingSounds>().UpdateObjectSelection(true);
			}
			else if (this.worker.gameObject.GetComponent<KSelectable>() != null && this.worker.gameObject.GetComponent<KSelectable>().IsSelected && base.gameObject.GetComponent<LoopingSounds>() != null)
			{
				base.gameObject.GetComponent<LoopingSounds>().UpdateObjectSelection(true);
			}
		}
		base.gameObject.Trigger(853695848, this);
	}

	// Token: 0x06002733 RID: 10035 RVA: 0x000E0700 File Offset: 0x000DE900
	public bool WorkTick(WorkerBase worker, float dt)
	{
		bool flag = false;
		if (dt > 0f)
		{
			this.workTimeRemaining -= dt;
			flag = this.OnWorkTick(worker, dt);
		}
		return flag || this.workTimeRemaining < 0f;
	}

	// Token: 0x06002734 RID: 10036 RVA: 0x000E0740 File Offset: 0x000DE940
	public virtual float GetEfficiencyMultiplier(WorkerBase worker)
	{
		float num = 1f;
		if (this.attributeConverter != null)
		{
			AttributeConverterInstance attributeConverterInstance = worker.GetAttributeConverter(this.attributeConverter.Id);
			if (attributeConverterInstance != null)
			{
				num += attributeConverterInstance.Evaluate();
			}
		}
		if (this.lightEfficiencyBonus)
		{
			int num2 = Grid.PosToCell(worker.gameObject);
			if (Grid.IsValidCell(num2))
			{
				if (Grid.LightIntensity[num2] > DUPLICANTSTATS.STANDARD.Light.NO_LIGHT)
				{
					this.currentlyLit = true;
					num += DUPLICANTSTATS.STANDARD.Light.LIGHT_WORK_EFFICIENCY_BONUS;
					if (this.lightEfficiencyBonusStatusItemHandle == Guid.Empty)
					{
						this.lightEfficiencyBonusStatusItemHandle = worker.OfferStatusItem(Db.Get().DuplicantStatusItems.LightWorkEfficiencyBonus, this);
					}
				}
				else
				{
					this.currentlyLit = false;
					if (this.lightEfficiencyBonusStatusItemHandle != Guid.Empty)
					{
						worker.RevokeStatusItem(this.lightEfficiencyBonusStatusItemHandle);
					}
				}
			}
		}
		if (this.useLaboratoryEfficiencyBonus && this.currentlyInLaboratory)
		{
			num += 0.1f;
		}
		return Mathf.Max(num, this.minimumAttributeMultiplier);
	}

	// Token: 0x06002735 RID: 10037 RVA: 0x000E0849 File Offset: 0x000DEA49
	public virtual global::Klei.AI.Attribute GetWorkAttribute()
	{
		if (this.attributeConverter != null)
		{
			return this.attributeConverter.attribute;
		}
		return null;
	}

	// Token: 0x06002736 RID: 10038 RVA: 0x000E0860 File Offset: 0x000DEA60
	public virtual string GetConversationTopic()
	{
		KPrefabID component = base.GetComponent<KPrefabID>();
		if (!component.HasTag(GameTags.NotConversationTopic))
		{
			return component.PrefabTag.Name;
		}
		return null;
	}

	// Token: 0x06002737 RID: 10039 RVA: 0x000E088E File Offset: 0x000DEA8E
	public float GetAttributeExperienceMultiplier()
	{
		return this.attributeExperienceMultiplier;
	}

	// Token: 0x06002738 RID: 10040 RVA: 0x000E0896 File Offset: 0x000DEA96
	public string GetSkillExperienceSkillGroup()
	{
		return this.skillExperienceSkillGroup;
	}

	// Token: 0x06002739 RID: 10041 RVA: 0x000E089E File Offset: 0x000DEA9E
	public float GetSkillExperienceMultiplier()
	{
		return this.skillExperienceMultiplier;
	}

	// Token: 0x0600273A RID: 10042 RVA: 0x000E08A6 File Offset: 0x000DEAA6
	protected virtual bool OnWorkTick(WorkerBase worker, float dt)
	{
		return false;
	}

	// Token: 0x0600273B RID: 10043 RVA: 0x000E08AC File Offset: 0x000DEAAC
	public void StopWork(WorkerBase workerToStop, bool aborted)
	{
		if (this.worker == workerToStop && aborted)
		{
			this.OnAbortWork(workerToStop);
		}
		if (this.shouldTransferDiseaseWithWorker)
		{
			this.TransferDiseaseWithWorker(workerToStop);
		}
		if (this.OnWorkableEventCB != null)
		{
			this.OnWorkableEventCB(this, Workable.WorkableEvent.WorkStopped);
		}
		this.OnStopWork(workerToStop);
		if (this.resetProgressOnStop)
		{
			this.workTimeRemaining = this.GetWorkTime();
		}
		this.ShowProgressBar(this.alwaysShowProgressBar && this.workTimeRemaining < this.GetWorkTime());
		if (this.lightEfficiencyBonusStatusItemHandle != Guid.Empty)
		{
			workerToStop.RevokeStatusItem(this.lightEfficiencyBonusStatusItemHandle);
			this.lightEfficiencyBonusStatusItemHandle = Guid.Empty;
		}
		if (this.laboratoryEfficiencyBonusStatusItemHandle != Guid.Empty)
		{
			this.worker.RevokeStatusItem(this.laboratoryEfficiencyBonusStatusItemHandle);
			this.laboratoryEfficiencyBonusStatusItemHandle = Guid.Empty;
		}
		if (base.gameObject.GetComponent<KSelectable>() != null && !base.gameObject.GetComponent<KSelectable>().IsSelected && base.gameObject.GetComponent<LoopingSounds>() != null)
		{
			base.gameObject.GetComponent<LoopingSounds>().UpdateObjectSelection(false);
		}
		else if (workerToStop.gameObject.GetComponent<KSelectable>() != null && !workerToStop.gameObject.GetComponent<KSelectable>().IsSelected && workerToStop.gameObject.GetComponent<LoopingSounds>() != null)
		{
			workerToStop.gameObject.GetComponent<LoopingSounds>().UpdateObjectSelection(false);
		}
		this.worker = null;
		base.gameObject.Trigger(679550494, this);
		this.UpdateStatusItem(null);
	}

	// Token: 0x0600273C RID: 10044 RVA: 0x000E0A38 File Offset: 0x000DEC38
	public virtual StatusItem GetWorkerStatusItem()
	{
		return this.workerStatusItem;
	}

	// Token: 0x0600273D RID: 10045 RVA: 0x000E0A40 File Offset: 0x000DEC40
	public void SetWorkerStatusItem(StatusItem item)
	{
		this.workerStatusItem = item;
	}

	// Token: 0x0600273E RID: 10046 RVA: 0x000E0A4C File Offset: 0x000DEC4C
	public void CompleteWork(WorkerBase worker)
	{
		if (this.shouldTransferDiseaseWithWorker)
		{
			this.TransferDiseaseWithWorker(worker);
		}
		this.OnCompleteWork(worker);
		if (this.OnWorkableEventCB != null)
		{
			this.OnWorkableEventCB(this, Workable.WorkableEvent.WorkCompleted);
		}
		this.workTimeRemaining = this.GetWorkTime();
		this.ShowProgressBar(false);
		base.gameObject.Trigger(-2011693419, this);
	}

	// Token: 0x0600273F RID: 10047 RVA: 0x000E0AA8 File Offset: 0x000DECA8
	public void SetReportType(ReportManager.ReportType report_type)
	{
		this.reportType = report_type;
	}

	// Token: 0x06002740 RID: 10048 RVA: 0x000E0AB1 File Offset: 0x000DECB1
	public ReportManager.ReportType GetReportType()
	{
		return this.reportType;
	}

	// Token: 0x06002741 RID: 10049 RVA: 0x000E0AB9 File Offset: 0x000DECB9
	protected virtual void OnStartWork(WorkerBase worker)
	{
	}

	// Token: 0x06002742 RID: 10050 RVA: 0x000E0ABB File Offset: 0x000DECBB
	protected virtual void OnStopWork(WorkerBase worker)
	{
	}

	// Token: 0x06002743 RID: 10051 RVA: 0x000E0ABD File Offset: 0x000DECBD
	protected virtual void OnCompleteWork(WorkerBase worker)
	{
	}

	// Token: 0x06002744 RID: 10052 RVA: 0x000E0ABF File Offset: 0x000DECBF
	protected virtual void OnAbortWork(WorkerBase worker)
	{
	}

	// Token: 0x06002745 RID: 10053 RVA: 0x000E0AC1 File Offset: 0x000DECC1
	public virtual void OnPendingCompleteWork(WorkerBase worker)
	{
	}

	// Token: 0x06002746 RID: 10054 RVA: 0x000E0AC3 File Offset: 0x000DECC3
	public void SetOffsets(CellOffset[] offsets)
	{
		if (this.offsetTracker != null)
		{
			this.offsetTracker.Clear();
		}
		this.offsetTracker = new StandardOffsetTracker(offsets);
	}

	// Token: 0x06002747 RID: 10055 RVA: 0x000E0AE4 File Offset: 0x000DECE4
	public void SetOffsetTable(CellOffset[][] offset_table)
	{
		if (this.offsetTracker != null)
		{
			this.offsetTracker.Clear();
		}
		this.offsetTracker = new OffsetTableTracker(offset_table, this);
	}

	// Token: 0x06002748 RID: 10056 RVA: 0x000E0B06 File Offset: 0x000DED06
	public virtual CellOffset[] GetOffsets(int cell)
	{
		if (this.offsetTracker == null)
		{
			this.offsetTracker = new StandardOffsetTracker(new CellOffset[1]);
		}
		return this.offsetTracker.GetOffsets(cell);
	}

	// Token: 0x06002749 RID: 10057 RVA: 0x000E0B2D File Offset: 0x000DED2D
	public virtual bool ValidateOffsets(int cell)
	{
		if (this.offsetTracker == null)
		{
			this.offsetTracker = new StandardOffsetTracker(new CellOffset[1]);
		}
		return this.offsetTracker.ValidateOffsets(cell);
	}

	// Token: 0x0600274A RID: 10058 RVA: 0x000E0B54 File Offset: 0x000DED54
	public CellOffset[] GetOffsets()
	{
		return this.GetOffsets(Grid.PosToCell(this));
	}

	// Token: 0x0600274B RID: 10059 RVA: 0x000E0B62 File Offset: 0x000DED62
	public void SetWorkTime(float work_time)
	{
		this.workTime = work_time;
		this.workTimeRemaining = work_time;
	}

	// Token: 0x0600274C RID: 10060 RVA: 0x000E0B72 File Offset: 0x000DED72
	public bool ShouldFaceTargetWhenWorking()
	{
		return this.faceTargetWhenWorking;
	}

	// Token: 0x0600274D RID: 10061 RVA: 0x000E0B7A File Offset: 0x000DED7A
	public virtual Vector3 GetFacingTarget()
	{
		return base.transform.GetPosition();
	}

	// Token: 0x0600274E RID: 10062 RVA: 0x000E0B88 File Offset: 0x000DED88
	public void ShowProgressBar(bool show)
	{
		if (show)
		{
			if (this.progressBar == null)
			{
				this.progressBar = ProgressBar.CreateProgressBar(base.gameObject, new Func<float>(this.GetPercentComplete));
			}
			this.progressBar.SetVisibility(true);
			return;
		}
		if (this.progressBar != null)
		{
			this.progressBar.gameObject.DeleteObject();
			this.progressBar = null;
		}
	}

	// Token: 0x0600274F RID: 10063 RVA: 0x000E0BF8 File Offset: 0x000DEDF8
	protected override void OnCleanUp()
	{
		this.ShowProgressBar(false);
		if (this.offsetTracker != null)
		{
			this.offsetTracker.Clear();
		}
		if (this.skillsUpdateHandle != -1)
		{
			Game.Instance.Unsubscribe(this.skillsUpdateHandle);
		}
		if (this.minionUpdateHandle != -1)
		{
			Game.Instance.Unsubscribe(this.minionUpdateHandle);
		}
		base.OnCleanUp();
		this.OnWorkableEventCB = null;
	}

	// Token: 0x06002750 RID: 10064 RVA: 0x000E0C60 File Offset: 0x000DEE60
	public virtual Vector3 GetTargetPoint()
	{
		Vector3 vector = base.transform.GetPosition();
		float num = vector.y + 0.65f;
		KBoxCollider2D component = base.GetComponent<KBoxCollider2D>();
		if (component != null)
		{
			vector = component.bounds.center;
		}
		vector.y = num;
		vector.z = 0f;
		return vector;
	}

	// Token: 0x06002751 RID: 10065 RVA: 0x000E0CBA File Offset: 0x000DEEBA
	public int GetNavigationCost(Navigator navigator, int cell)
	{
		return navigator.GetNavigationCost(cell, this.GetOffsets(cell));
	}

	// Token: 0x06002752 RID: 10066 RVA: 0x000E0CCA File Offset: 0x000DEECA
	public int GetNavigationCost(Navigator navigator)
	{
		return this.GetNavigationCost(navigator, Grid.PosToCell(this));
	}

	// Token: 0x06002753 RID: 10067 RVA: 0x000E0CD9 File Offset: 0x000DEED9
	private void TransferDiseaseWithWorker(WorkerBase worker)
	{
		if (this == null || worker == null)
		{
			return;
		}
		Workable.TransferDiseaseWithWorker(base.gameObject, worker.gameObject);
	}

	// Token: 0x06002754 RID: 10068 RVA: 0x000E0D00 File Offset: 0x000DEF00
	public static void TransferDiseaseWithWorker(GameObject workable, GameObject worker)
	{
		if (workable == null || worker == null)
		{
			return;
		}
		PrimaryElement component = workable.GetComponent<PrimaryElement>();
		if (component == null)
		{
			return;
		}
		PrimaryElement component2 = worker.GetComponent<PrimaryElement>();
		if (component2 == null)
		{
			return;
		}
		SimUtil.DiseaseInfo invalid = SimUtil.DiseaseInfo.Invalid;
		invalid.idx = component2.DiseaseIdx;
		invalid.count = (int)((float)component2.DiseaseCount * 0.33f);
		SimUtil.DiseaseInfo invalid2 = SimUtil.DiseaseInfo.Invalid;
		invalid2.idx = component.DiseaseIdx;
		invalid2.count = (int)((float)component.DiseaseCount * 0.33f);
		component2.ModifyDiseaseCount(-invalid.count, "Workable.TransferDiseaseWithWorker");
		component.ModifyDiseaseCount(-invalid2.count, "Workable.TransferDiseaseWithWorker");
		if (invalid.count > 0)
		{
			component.AddDisease(invalid.idx, invalid.count, "Workable.TransferDiseaseWithWorker");
		}
		if (invalid2.count > 0)
		{
			component2.AddDisease(invalid2.idx, invalid2.count, "Workable.TransferDiseaseWithWorker");
		}
	}

	// Token: 0x06002755 RID: 10069 RVA: 0x000E0DF8 File Offset: 0x000DEFF8
	public void SetShouldShowSkillPerkStatusItem(bool shouldItBeShown)
	{
		this.shouldShowSkillPerkStatusItem = shouldItBeShown;
		if (this.skillsUpdateHandle != -1)
		{
			Game.Instance.Unsubscribe(this.skillsUpdateHandle);
			this.skillsUpdateHandle = -1;
		}
		if (this.shouldShowSkillPerkStatusItem && !string.IsNullOrEmpty(this.requiredSkillPerk))
		{
			this.skillsUpdateHandle = Game.Instance.Subscribe(-1523247426, new Action<object>(this.UpdateStatusItem));
		}
		this.UpdateStatusItem(null);
	}

	// Token: 0x06002756 RID: 10070 RVA: 0x000E0E6C File Offset: 0x000DF06C
	public virtual bool InstantlyFinish(WorkerBase worker)
	{
		float num = worker.GetWorkable().WorkTimeRemaining;
		if (!float.IsInfinity(num))
		{
			worker.Work(num);
			return true;
		}
		DebugUtil.DevAssert(false, this.ToString() + " was asked to instantly finish but it has infinite work time! Override InstantlyFinish in your workable!", null);
		return false;
	}

	// Token: 0x06002757 RID: 10071 RVA: 0x000E0EB0 File Offset: 0x000DF0B0
	public virtual List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.trackUses)
		{
			Descriptor descriptor = new Descriptor(string.Format(BUILDING.DETAILS.USE_COUNT, this.numberOfUses), string.Format(BUILDING.DETAILS.USE_COUNT_TOOLTIP, this.numberOfUses), Descriptor.DescriptorType.Detail, false);
			list.Add(descriptor);
		}
		return list;
	}

	// Token: 0x06002758 RID: 10072 RVA: 0x000E0F10 File Offset: 0x000DF110
	public virtual BuildingFacade GetBuildingFacade()
	{
		return base.GetComponent<BuildingFacade>();
	}

	// Token: 0x06002759 RID: 10073 RVA: 0x000E0F18 File Offset: 0x000DF118
	public virtual KAnimControllerBase GetAnimController()
	{
		return base.GetComponent<KAnimControllerBase>();
	}

	// Token: 0x0600275A RID: 10074 RVA: 0x000E0F20 File Offset: 0x000DF120
	[ContextMenu("Refresh Reachability")]
	public void RefreshReachability()
	{
		if (this.offsetTracker != null)
		{
			this.offsetTracker.ForceRefresh();
		}
	}

	// Token: 0x040016D3 RID: 5843
	public float workTime;

	// Token: 0x040016D4 RID: 5844
	protected bool showProgressBar = true;

	// Token: 0x040016D5 RID: 5845
	public bool alwaysShowProgressBar;

	// Token: 0x040016D6 RID: 5846
	public bool surpressWorkerForceSync;

	// Token: 0x040016D7 RID: 5847
	protected bool lightEfficiencyBonus = true;

	// Token: 0x040016D8 RID: 5848
	protected Guid lightEfficiencyBonusStatusItemHandle;

	// Token: 0x040016D9 RID: 5849
	public bool currentlyLit;

	// Token: 0x040016DA RID: 5850
	public Tag laboratoryEfficiencyBonusTagRequired = RoomConstraints.ConstraintTags.ScienceBuilding;

	// Token: 0x040016DB RID: 5851
	private bool useLaboratoryEfficiencyBonus;

	// Token: 0x040016DC RID: 5852
	protected Guid laboratoryEfficiencyBonusStatusItemHandle;

	// Token: 0x040016DD RID: 5853
	private bool currentlyInLaboratory;

	// Token: 0x040016DE RID: 5854
	protected StatusItem workerStatusItem;

	// Token: 0x040016DF RID: 5855
	protected StatusItem workingStatusItem;

	// Token: 0x040016E0 RID: 5856
	protected Guid workStatusItemHandle;

	// Token: 0x040016E1 RID: 5857
	protected OffsetTracker offsetTracker;

	// Token: 0x040016E2 RID: 5858
	[SerializeField]
	protected string attributeConverterId;

	// Token: 0x040016E3 RID: 5859
	protected AttributeConverter attributeConverter;

	// Token: 0x040016E4 RID: 5860
	protected float minimumAttributeMultiplier = 0.5f;

	// Token: 0x040016E5 RID: 5861
	public bool resetProgressOnStop;

	// Token: 0x040016E6 RID: 5862
	protected bool shouldTransferDiseaseWithWorker = true;

	// Token: 0x040016E7 RID: 5863
	[SerializeField]
	protected float attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;

	// Token: 0x040016E8 RID: 5864
	[SerializeField]
	protected string skillExperienceSkillGroup;

	// Token: 0x040016E9 RID: 5865
	[SerializeField]
	protected float skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;

	// Token: 0x040016EA RID: 5866
	public bool triggerWorkReactions = true;

	// Token: 0x040016EB RID: 5867
	public ReportManager.ReportType reportType = ReportManager.ReportType.WorkTime;

	// Token: 0x040016EC RID: 5868
	[SerializeField]
	[Tooltip("What layer does the dupe switch to when interacting with the building")]
	public Grid.SceneLayer workLayer = Grid.SceneLayer.Move;

	// Token: 0x040016ED RID: 5869
	[SerializeField]
	[Serialize]
	protected float workTimeRemaining = float.PositiveInfinity;

	// Token: 0x040016EE RID: 5870
	[SerializeField]
	public KAnimFile[] overrideAnims;

	// Token: 0x040016EF RID: 5871
	[SerializeField]
	protected HashedString multitoolContext;

	// Token: 0x040016F0 RID: 5872
	[SerializeField]
	protected Tag multitoolHitEffectTag;

	// Token: 0x040016F1 RID: 5873
	[SerializeField]
	[Tooltip("Whether to user the KAnimSynchronizer or not")]
	public bool synchronizeAnims = true;

	// Token: 0x040016F2 RID: 5874
	[SerializeField]
	[Tooltip("Whether to display number of uses in the details panel")]
	public bool trackUses;

	// Token: 0x040016F3 RID: 5875
	[Serialize]
	protected int numberOfUses;

	// Token: 0x040016F4 RID: 5876
	public Action<Workable, Workable.WorkableEvent> OnWorkableEventCB;

	// Token: 0x040016F5 RID: 5877
	protected int skillsUpdateHandle = -1;

	// Token: 0x040016F6 RID: 5878
	private int minionUpdateHandle = -1;

	// Token: 0x040016F7 RID: 5879
	public string requiredSkillPerk;

	// Token: 0x040016F8 RID: 5880
	[SerializeField]
	protected bool shouldShowSkillPerkStatusItem = true;

	// Token: 0x040016F9 RID: 5881
	[SerializeField]
	public bool requireMinionToWork;

	// Token: 0x040016FA RID: 5882
	protected StatusItem readyForSkillWorkStatusItem;

	// Token: 0x040016FB RID: 5883
	public HashedString[] workAnims = new HashedString[] { "working_pre", "working_loop" };

	// Token: 0x040016FC RID: 5884
	public HashedString[] workingPstComplete = new HashedString[] { "working_pst" };

	// Token: 0x040016FD RID: 5885
	public HashedString[] workingPstFailed = new HashedString[] { "working_pst" };

	// Token: 0x040016FE RID: 5886
	public KAnim.PlayMode workAnimPlayMode;

	// Token: 0x040016FF RID: 5887
	public bool faceTargetWhenWorking;

	// Token: 0x04001700 RID: 5888
	private static readonly EventSystem.IntraObjectHandler<Workable> OnUpdateRoomDelegate = new EventSystem.IntraObjectHandler<Workable>(delegate(Workable component, object data)
	{
		component.OnUpdateRoom(data);
	});

	// Token: 0x04001701 RID: 5889
	protected ProgressBar progressBar;

	// Token: 0x020014DD RID: 5341
	public enum WorkableEvent
	{
		// Token: 0x04006E2A RID: 28202
		WorkStarted,
		// Token: 0x04006E2B RID: 28203
		WorkCompleted,
		// Token: 0x04006E2C RID: 28204
		WorkStopped
	}

	// Token: 0x020014DE RID: 5342
	public struct AnimInfo
	{
		// Token: 0x04006E2D RID: 28205
		public KAnimFile[] overrideAnims;

		// Token: 0x04006E2E RID: 28206
		public StateMachine.Instance smi;
	}
}
