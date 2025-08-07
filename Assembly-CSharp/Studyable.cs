using System;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000620 RID: 1568
[AddComponentMenu("KMonoBehaviour/Workable/Studyable")]
public class Studyable : Workable, ISidescreenButtonControl
{
	// Token: 0x170001B3 RID: 435
	// (get) Token: 0x060025FD RID: 9725 RVA: 0x000D93DD File Offset: 0x000D75DD
	public bool Studied
	{
		get
		{
			return this.studied;
		}
	}

	// Token: 0x170001B4 RID: 436
	// (get) Token: 0x060025FE RID: 9726 RVA: 0x000D93E5 File Offset: 0x000D75E5
	public bool Studying
	{
		get
		{
			return this.chore != null && this.chore.InProgress();
		}
	}

	// Token: 0x170001B5 RID: 437
	// (get) Token: 0x060025FF RID: 9727 RVA: 0x000D93FC File Offset: 0x000D75FC
	public string SidescreenTitleKey
	{
		get
		{
			return "STRINGS.UI.UISIDESCREENS.STUDYABLE_SIDE_SCREEN.TITLE";
		}
	}

	// Token: 0x170001B6 RID: 438
	// (get) Token: 0x06002600 RID: 9728 RVA: 0x000D9403 File Offset: 0x000D7603
	public string SidescreenStatusMessage
	{
		get
		{
			if (this.studied)
			{
				return UI.UISIDESCREENS.STUDYABLE_SIDE_SCREEN.STUDIED_STATUS;
			}
			if (this.markedForStudy)
			{
				return UI.UISIDESCREENS.STUDYABLE_SIDE_SCREEN.PENDING_STATUS;
			}
			return UI.UISIDESCREENS.STUDYABLE_SIDE_SCREEN.SEND_STATUS;
		}
	}

	// Token: 0x170001B7 RID: 439
	// (get) Token: 0x06002601 RID: 9729 RVA: 0x000D9435 File Offset: 0x000D7635
	public string SidescreenButtonText
	{
		get
		{
			if (this.studied)
			{
				return UI.UISIDESCREENS.STUDYABLE_SIDE_SCREEN.STUDIED_BUTTON;
			}
			if (this.markedForStudy)
			{
				return UI.UISIDESCREENS.STUDYABLE_SIDE_SCREEN.PENDING_BUTTON;
			}
			return UI.UISIDESCREENS.STUDYABLE_SIDE_SCREEN.SEND_BUTTON;
		}
	}

	// Token: 0x170001B8 RID: 440
	// (get) Token: 0x06002602 RID: 9730 RVA: 0x000D9467 File Offset: 0x000D7667
	public string SidescreenButtonTooltip
	{
		get
		{
			if (this.studied)
			{
				return UI.UISIDESCREENS.STUDYABLE_SIDE_SCREEN.STUDIED_STATUS;
			}
			if (this.markedForStudy)
			{
				return UI.UISIDESCREENS.STUDYABLE_SIDE_SCREEN.PENDING_STATUS;
			}
			return UI.UISIDESCREENS.STUDYABLE_SIDE_SCREEN.SEND_STATUS;
		}
	}

	// Token: 0x06002603 RID: 9731 RVA: 0x000D9499 File Offset: 0x000D7699
	public int HorizontalGroupID()
	{
		return -1;
	}

	// Token: 0x06002604 RID: 9732 RVA: 0x000D949C File Offset: 0x000D769C
	public void SetButtonTextOverride(ButtonMenuTextOverride text)
	{
		throw new NotImplementedException();
	}

	// Token: 0x06002605 RID: 9733 RVA: 0x000D94A3 File Offset: 0x000D76A3
	public bool SidescreenEnabled()
	{
		return true;
	}

	// Token: 0x06002606 RID: 9734 RVA: 0x000D94A6 File Offset: 0x000D76A6
	public bool SidescreenButtonInteractable()
	{
		return !this.studied;
	}

	// Token: 0x06002607 RID: 9735 RVA: 0x000D94B4 File Offset: 0x000D76B4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_use_machine_kanim") };
		this.faceTargetWhenWorking = true;
		this.synchronizeAnims = false;
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Studying;
		this.resetProgressOnStop = false;
		this.requiredSkillPerk = Db.Get().SkillPerks.CanStudyWorldObjects.Id;
		this.attributeConverter = Db.Get().AttributeConverters.ResearchSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.MOST_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Research.Id;
		this.skillExperienceMultiplier = SKILLS.MOST_DAY_EXPERIENCE;
		base.SetWorkTime(3600f);
	}

	// Token: 0x06002608 RID: 9736 RVA: 0x000D957C File Offset: 0x000D777C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.studiedIndicator = new MeterController(base.GetComponent<KBatchedAnimController>(), this.meterTrackerSymbol, this.meterAnim, Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { this.meterTrackerSymbol });
		this.studiedIndicator.meterController.gameObject.AddComponent<LoopingSounds>();
		this.Refresh();
	}

	// Token: 0x06002609 RID: 9737 RVA: 0x000D95DA File Offset: 0x000D77DA
	public void CancelChore()
	{
		if (this.chore != null)
		{
			this.chore.Cancel("Studyable.CancelChore");
			this.chore = null;
			base.Trigger(1488501379, null);
		}
	}

	// Token: 0x0600260A RID: 9738 RVA: 0x000D9608 File Offset: 0x000D7808
	public void Refresh()
	{
		if (KMonoBehaviour.isLoadingScene)
		{
			return;
		}
		KSelectable component = base.GetComponent<KSelectable>();
		if (this.studied)
		{
			this.statusItemGuid = component.ReplaceStatusItem(this.statusItemGuid, Db.Get().MiscStatusItems.Studied, null);
			this.studiedIndicator.gameObject.SetActive(true);
			this.studiedIndicator.meterController.Play(this.meterAnim, KAnim.PlayMode.Loop, 1f, 0f);
			this.requiredSkillPerk = null;
			this.UpdateStatusItem(null);
			return;
		}
		if (this.markedForStudy)
		{
			if (this.chore == null)
			{
				this.chore = new WorkChore<Studyable>(Db.Get().ChoreTypes.Research, this, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
			}
			this.statusItemGuid = component.ReplaceStatusItem(this.statusItemGuid, Db.Get().MiscStatusItems.AwaitingStudy, null);
		}
		else
		{
			this.CancelChore();
			this.statusItemGuid = component.RemoveStatusItem(this.statusItemGuid, false);
		}
		this.studiedIndicator.gameObject.SetActive(false);
	}

	// Token: 0x0600260B RID: 9739 RVA: 0x000D9720 File Offset: 0x000D7920
	private void ToggleStudyChore()
	{
		if (DebugHandler.InstantBuildMode)
		{
			this.studied = true;
			if (this.chore != null)
			{
				this.chore.Cancel("debug");
				this.chore = null;
			}
			base.Trigger(-1436775550, null);
		}
		else
		{
			this.markedForStudy = !this.markedForStudy;
		}
		this.Refresh();
	}

	// Token: 0x0600260C RID: 9740 RVA: 0x000D977D File Offset: 0x000D797D
	protected override void OnCompleteWork(WorkerBase worker)
	{
		base.OnCompleteWork(worker);
		this.studied = true;
		this.chore = null;
		this.Refresh();
		base.Trigger(-1436775550, null);
		if (DlcManager.IsExpansion1Active())
		{
			this.DropDatabanks();
		}
	}

	// Token: 0x0600260D RID: 9741 RVA: 0x000D97B4 File Offset: 0x000D79B4
	private void DropDatabanks()
	{
		int num = global::UnityEngine.Random.Range(7, 13);
		for (int i = 0; i <= num; i++)
		{
			GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab("OrbitalResearchDatabank"), base.transform.position + new Vector3(0f, 1f, 0f), Grid.SceneLayer.Ore, null, 0);
			gameObject.GetComponent<PrimaryElement>().Temperature = 298.15f;
			gameObject.SetActive(true);
		}
	}

	// Token: 0x0600260E RID: 9742 RVA: 0x000D9828 File Offset: 0x000D7A28
	public void OnSidescreenButtonPressed()
	{
		this.ToggleStudyChore();
	}

	// Token: 0x0600260F RID: 9743 RVA: 0x000D9830 File Offset: 0x000D7A30
	public int ButtonSideScreenSortOrder()
	{
		return 20;
	}

	// Token: 0x04001658 RID: 5720
	public string meterTrackerSymbol;

	// Token: 0x04001659 RID: 5721
	public string meterAnim;

	// Token: 0x0400165A RID: 5722
	private Chore chore;

	// Token: 0x0400165B RID: 5723
	private const float STUDY_WORK_TIME = 3600f;

	// Token: 0x0400165C RID: 5724
	[Serialize]
	private bool studied;

	// Token: 0x0400165D RID: 5725
	[Serialize]
	private bool markedForStudy;

	// Token: 0x0400165E RID: 5726
	private Guid statusItemGuid;

	// Token: 0x0400165F RID: 5727
	private Guid additionalStatusItemGuid;

	// Token: 0x04001660 RID: 5728
	public MeterController studiedIndicator;
}
