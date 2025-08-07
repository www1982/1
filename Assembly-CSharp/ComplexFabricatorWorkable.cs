using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x02000584 RID: 1412
[AddComponentMenu("KMonoBehaviour/Workable/ComplexFabricatorWorkable")]
public class ComplexFabricatorWorkable : Workable
{
	// Token: 0x17000137 RID: 311
	// (get) Token: 0x06002017 RID: 8215 RVA: 0x000B8A09 File Offset: 0x000B6C09
	// (set) Token: 0x06002018 RID: 8216 RVA: 0x000B8A11 File Offset: 0x000B6C11
	public StatusItem WorkerStatusItem
	{
		get
		{
			return this.workerStatusItem;
		}
		set
		{
			this.workerStatusItem = value;
		}
	}

	// Token: 0x17000138 RID: 312
	// (get) Token: 0x06002019 RID: 8217 RVA: 0x000B8A1A File Offset: 0x000B6C1A
	// (set) Token: 0x0600201A RID: 8218 RVA: 0x000B8A22 File Offset: 0x000B6C22
	public AttributeConverter AttributeConverter
	{
		get
		{
			return this.attributeConverter;
		}
		set
		{
			this.attributeConverter = value;
		}
	}

	// Token: 0x17000139 RID: 313
	// (get) Token: 0x0600201B RID: 8219 RVA: 0x000B8A2B File Offset: 0x000B6C2B
	// (set) Token: 0x0600201C RID: 8220 RVA: 0x000B8A33 File Offset: 0x000B6C33
	public float AttributeExperienceMultiplier
	{
		get
		{
			return this.attributeExperienceMultiplier;
		}
		set
		{
			this.attributeExperienceMultiplier = value;
		}
	}

	// Token: 0x1700013A RID: 314
	// (set) Token: 0x0600201D RID: 8221 RVA: 0x000B8A3C File Offset: 0x000B6C3C
	public string SkillExperienceSkillGroup
	{
		set
		{
			this.skillExperienceSkillGroup = value;
		}
	}

	// Token: 0x1700013B RID: 315
	// (set) Token: 0x0600201E RID: 8222 RVA: 0x000B8A45 File Offset: 0x000B6C45
	public float SkillExperienceMultiplier
	{
		set
		{
			this.skillExperienceMultiplier = value;
		}
	}

	// Token: 0x1700013C RID: 316
	// (get) Token: 0x0600201F RID: 8223 RVA: 0x000B8A4E File Offset: 0x000B6C4E
	public ComplexRecipe CurrentWorkingOrder
	{
		get
		{
			if (!(this.fabricator != null))
			{
				return null;
			}
			return this.fabricator.CurrentWorkingOrder;
		}
	}

	// Token: 0x06002020 RID: 8224 RVA: 0x000B8A6C File Offset: 0x000B6C6C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Fabricating;
		this.attributeConverter = Db.Get().AttributeConverters.MachinerySpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Technicals.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
	}

	// Token: 0x06002021 RID: 8225 RVA: 0x000B8ADC File Offset: 0x000B6CDC
	public override string GetConversationTopic()
	{
		string conversationTopic = this.fabricator.GetConversationTopic();
		if (conversationTopic == null)
		{
			return base.GetConversationTopic();
		}
		return conversationTopic;
	}

	// Token: 0x06002022 RID: 8226 RVA: 0x000B8B00 File Offset: 0x000B6D00
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		if (!this.operational.IsOperational)
		{
			return;
		}
		if (this.fabricator.CurrentWorkingOrder != null)
		{
			this.InstantiateVisualizer(this.fabricator.CurrentWorkingOrder);
			this.QueueWorkingAnimations();
			return;
		}
		DebugUtil.DevAssertArgs(false, new object[] { "ComplexFabricatorWorkable.OnStartWork called but CurrentMachineOrder is null", base.gameObject });
	}

	// Token: 0x06002023 RID: 8227 RVA: 0x000B8B64 File Offset: 0x000B6D64
	protected override bool OnWorkTick(WorkerBase worker, float dt)
	{
		if (this.OnWorkTickActions != null)
		{
			this.OnWorkTickActions(worker, dt);
		}
		this.UpdateOrderProgress(worker, dt);
		return base.OnWorkTick(worker, dt);
	}

	// Token: 0x06002024 RID: 8228 RVA: 0x000B8B8B File Offset: 0x000B6D8B
	protected override void OnStopWork(WorkerBase worker)
	{
		base.OnStopWork(worker);
		if (worker != null && this.GetDupeInteract != null)
		{
			worker.GetAnimController().onAnimComplete -= this.PlayNextWorkingAnim;
		}
	}

	// Token: 0x06002025 RID: 8229 RVA: 0x000B8BBC File Offset: 0x000B6DBC
	public override float GetWorkTime()
	{
		ComplexRecipe currentWorkingOrder = this.fabricator.CurrentWorkingOrder;
		if (currentWorkingOrder != null)
		{
			this.workTime = currentWorkingOrder.time;
			return this.workTime;
		}
		return -1f;
	}

	// Token: 0x06002026 RID: 8230 RVA: 0x000B8BF0 File Offset: 0x000B6DF0
	public Chore CreateWorkChore(ChoreType choreType, float order_progress)
	{
		Chore chore = new WorkChore<ComplexFabricatorWorkable>(choreType, this, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		this.workTimeRemaining = this.GetWorkTime() * (1f - order_progress);
		return chore;
	}

	// Token: 0x06002027 RID: 8231 RVA: 0x000B8C29 File Offset: 0x000B6E29
	protected override void OnCompleteWork(WorkerBase worker)
	{
		base.OnCompleteWork(worker);
		this.fabricator.CompleteWorkingOrder();
		this.DestroyVisualizer();
		base.OnStopWork(worker);
	}

	// Token: 0x06002028 RID: 8232 RVA: 0x000B8C4C File Offset: 0x000B6E4C
	private void InstantiateVisualizer(ComplexRecipe recipe)
	{
		if (this.visualizer != null)
		{
			this.DestroyVisualizer();
		}
		if (this.visualizerLink != null)
		{
			this.visualizerLink.Unregister();
			this.visualizerLink = null;
		}
		if (recipe.FabricationVisualizer == null)
		{
			return;
		}
		this.visualizer = Util.KInstantiate(recipe.FabricationVisualizer, null, null);
		this.visualizer.transform.parent = this.meter.meterController.transform;
		this.visualizer.transform.SetLocalPosition(new Vector3(0f, 0f, 1f));
		this.visualizer.SetActive(true);
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		KBatchedAnimController component2 = this.visualizer.GetComponent<KBatchedAnimController>();
		this.visualizerLink = new KAnimLink(component, component2);
	}

	// Token: 0x06002029 RID: 8233 RVA: 0x000B8D1C File Offset: 0x000B6F1C
	private void UpdateOrderProgress(WorkerBase worker, float dt)
	{
		float workTime = this.GetWorkTime();
		float num = Mathf.Clamp01((workTime - base.WorkTimeRemaining) / workTime);
		if (this.fabricator)
		{
			this.fabricator.OrderProgress = num;
		}
		if (this.meter != null)
		{
			this.meter.SetPositionPercent(num);
		}
	}

	// Token: 0x0600202A RID: 8234 RVA: 0x000B8D6D File Offset: 0x000B6F6D
	private void DestroyVisualizer()
	{
		if (this.visualizer != null)
		{
			if (this.visualizerLink != null)
			{
				this.visualizerLink.Unregister();
				this.visualizerLink = null;
			}
			Util.KDestroyGameObject(this.visualizer);
			this.visualizer = null;
		}
	}

	// Token: 0x0600202B RID: 8235 RVA: 0x000B8DAC File Offset: 0x000B6FAC
	public void QueueWorkingAnimations()
	{
		KBatchedAnimController animController = base.worker.GetAnimController();
		if (this.GetDupeInteract != null)
		{
			animController.Queue("working_loop", KAnim.PlayMode.Once, 1f, 0f);
			animController.onAnimComplete += this.PlayNextWorkingAnim;
		}
	}

	// Token: 0x0600202C RID: 8236 RVA: 0x000B8DFC File Offset: 0x000B6FFC
	private void PlayNextWorkingAnim(HashedString anim)
	{
		if (base.worker == null)
		{
			return;
		}
		if (this.GetDupeInteract != null)
		{
			KBatchedAnimController animController = base.worker.GetAnimController();
			if (base.worker.GetState() == WorkerBase.State.Working)
			{
				animController.Play(this.GetDupeInteract(), KAnim.PlayMode.Once);
				return;
			}
			animController.onAnimComplete -= this.PlayNextWorkingAnim;
		}
	}

	// Token: 0x040012A4 RID: 4772
	[MyCmpReq]
	private Operational operational;

	// Token: 0x040012A5 RID: 4773
	[MyCmpReq]
	private ComplexFabricator fabricator;

	// Token: 0x040012A6 RID: 4774
	public Action<WorkerBase, float> OnWorkTickActions;

	// Token: 0x040012A7 RID: 4775
	public MeterController meter;

	// Token: 0x040012A8 RID: 4776
	protected GameObject visualizer;

	// Token: 0x040012A9 RID: 4777
	protected KAnimLink visualizerLink;

	// Token: 0x040012AA RID: 4778
	public Func<HashedString[]> GetDupeInteract;
}
