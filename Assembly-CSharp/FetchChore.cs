using System;
using System.Collections.Generic;
using System.Linq;
using STRINGS;
using UnityEngine;

// Token: 0x02000482 RID: 1154
public class FetchChore : Chore<FetchChore.StatesInstance>
{
	// Token: 0x17000076 RID: 118
	// (get) Token: 0x06001839 RID: 6201 RVA: 0x00086EDD File Offset: 0x000850DD
	public float originalAmount
	{
		get
		{
			return base.smi.sm.requestedamount.Get(base.smi);
		}
	}

	// Token: 0x17000077 RID: 119
	// (get) Token: 0x0600183A RID: 6202 RVA: 0x00086EFA File Offset: 0x000850FA
	// (set) Token: 0x0600183B RID: 6203 RVA: 0x00086F17 File Offset: 0x00085117
	public float amount
	{
		get
		{
			return base.smi.sm.actualamount.Get(base.smi);
		}
		set
		{
			base.smi.sm.actualamount.Set(value, base.smi, false);
		}
	}

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x0600183C RID: 6204 RVA: 0x00086F37 File Offset: 0x00085137
	// (set) Token: 0x0600183D RID: 6205 RVA: 0x00086F54 File Offset: 0x00085154
	public Pickupable fetchTarget
	{
		get
		{
			return base.smi.sm.chunk.Get<Pickupable>(base.smi);
		}
		set
		{
			base.smi.sm.chunk.Set(value, base.smi);
		}
	}

	// Token: 0x17000079 RID: 121
	// (get) Token: 0x0600183E RID: 6206 RVA: 0x00086F72 File Offset: 0x00085172
	// (set) Token: 0x0600183F RID: 6207 RVA: 0x00086F8F File Offset: 0x0008518F
	public GameObject fetcher
	{
		get
		{
			return base.smi.sm.fetcher.Get(base.smi);
		}
		set
		{
			base.smi.sm.fetcher.Set(value, base.smi, false);
		}
	}

	// Token: 0x1700007A RID: 122
	// (get) Token: 0x06001840 RID: 6208 RVA: 0x00086FAF File Offset: 0x000851AF
	// (set) Token: 0x06001841 RID: 6209 RVA: 0x00086FB7 File Offset: 0x000851B7
	public Storage destination { get; private set; }

	// Token: 0x06001842 RID: 6210 RVA: 0x00086FC0 File Offset: 0x000851C0
	public void FetchAreaBegin(Chore.Precondition.Context context, float amount_to_be_fetched)
	{
		this.amount = amount_to_be_fetched;
		base.smi.sm.fetcher.Set(context.consumerState.gameObject, base.smi, false);
		ReportManager.Instance.ReportValue(ReportManager.ReportType.ChoreStatus, 1f, context.chore.choreType.Name, GameUtil.GetChoreName(this, context.data));
		base.Begin(context);
	}

	// Token: 0x06001843 RID: 6211 RVA: 0x00087030 File Offset: 0x00085230
	public void FetchAreaEnd(ChoreDriver driver, Pickupable pickupable, bool is_success)
	{
		if (is_success)
		{
			ReportManager.Instance.ReportValue(ReportManager.ReportType.ChoreStatus, -1f, this.choreType.Name, GameUtil.GetChoreName(this, pickupable));
			this.fetchTarget = pickupable;
			this.driver = driver;
			this.fetcher = driver.gameObject;
			base.Succeed("FetchAreaEnd");
			SaveGame.Instance.ColonyAchievementTracker.LogFetchChore(this.fetcher, this.choreType);
			return;
		}
		base.SetOverrideTarget(null);
		this.Fail("FetchAreaFail");
	}

	// Token: 0x06001844 RID: 6212 RVA: 0x000870B8 File Offset: 0x000852B8
	public Pickupable FindFetchTarget(ChoreConsumerState consumer_state)
	{
		if (!(this.destination != null))
		{
			return null;
		}
		if (consumer_state.hasSolidTransferArm)
		{
			return consumer_state.solidTransferArm.FindFetchTarget(this.destination, this);
		}
		return Game.Instance.fetchManager.FindFetchTarget(this.destination, this);
	}

	// Token: 0x06001845 RID: 6213 RVA: 0x00087108 File Offset: 0x00085308
	public override void Begin(Chore.Precondition.Context context)
	{
		Pickupable pickupable = (Pickupable)context.data;
		if (pickupable == null)
		{
			pickupable = this.FindFetchTarget(context.consumerState);
		}
		base.smi.sm.source.Set(pickupable.gameObject, base.smi, false);
		pickupable.Subscribe(-1582839653, new Action<object>(this.OnTagsChanged));
		base.Begin(context);
	}

	// Token: 0x06001846 RID: 6214 RVA: 0x0008717C File Offset: 0x0008537C
	protected override void End(string reason)
	{
		Pickupable pickupable = base.smi.sm.source.Get<Pickupable>(base.smi);
		if (pickupable != null)
		{
			pickupable.Unsubscribe(-1582839653, new Action<object>(this.OnTagsChanged));
		}
		base.End(reason);
	}

	// Token: 0x06001847 RID: 6215 RVA: 0x000871CC File Offset: 0x000853CC
	private void OnTagsChanged(object data)
	{
		if (base.smi.sm.chunk.Get(base.smi) != null)
		{
			this.Fail("Tags changed");
		}
	}

	// Token: 0x06001848 RID: 6216 RVA: 0x000871FC File Offset: 0x000853FC
	public override void PrepareChore(ref Chore.Precondition.Context context)
	{
		context.chore = new FetchAreaChore(context);
	}

	// Token: 0x06001849 RID: 6217 RVA: 0x0008720F File Offset: 0x0008540F
	public float AmountWaitingToFetch()
	{
		if (this.fetcher == null)
		{
			return this.originalAmount;
		}
		return this.amount;
	}

	// Token: 0x0600184A RID: 6218 RVA: 0x0008722C File Offset: 0x0008542C
	public static float GetMinimumFetchAmount(HashSet<Tag> match_tags)
	{
		float num = 1f;
		foreach (Tag tag in match_tags)
		{
			GameObject prefab = Assets.GetPrefab(tag);
			if (prefab != null)
			{
				PrimaryElement component = prefab.GetComponent<PrimaryElement>();
				if (component != null && component.MassPerUnit > 1f)
				{
					num = Mathf.Max(num, component.MassPerUnit);
				}
			}
			else
			{
				foreach (GameObject gameObject in Assets.GetPrefabsWithTag(tag))
				{
					PrimaryElement component2 = gameObject.GetComponent<PrimaryElement>();
					if (component2 != null && component2.MassPerUnit > 1f)
					{
						num = Mathf.Max(num, component2.MassPerUnit);
					}
				}
			}
		}
		return num;
	}

	// Token: 0x0600184B RID: 6219 RVA: 0x00087328 File Offset: 0x00085528
	public static float GetMinimumFetchAmount(Tag requested_tag, float requested_amount)
	{
		float num = requested_amount;
		GameObject prefab = Assets.GetPrefab(requested_tag);
		if (prefab != null)
		{
			PrimaryElement component = prefab.GetComponent<PrimaryElement>();
			if (component != null && component.MassPerUnit > 1f)
			{
				return Mathf.Max(num, component.MassPerUnit);
			}
		}
		foreach (GameObject gameObject in Assets.GetPrefabsWithTag(requested_tag))
		{
			PrimaryElement component2 = gameObject.GetComponent<PrimaryElement>();
			if (component2 != null && component2.MassPerUnit > 1f)
			{
				num = Mathf.Max(num, component2.MassPerUnit);
			}
		}
		return num;
	}

	// Token: 0x0600184C RID: 6220 RVA: 0x000873E0 File Offset: 0x000855E0
	public FetchChore(ChoreType choreType, Storage destination, float amount, HashSet<Tag> tags, FetchChore.MatchCriteria criteria, Tag required_tag, Tag[] forbidden_tags = null, ChoreProvider chore_provider = null, bool run_until_complete = true, Action<Chore> on_complete = null, Action<Chore> on_begin = null, Action<Chore> on_end = null, Operational.State operational_requirement = Operational.State.Operational, int priority_mod = 0)
		: base(choreType, destination, chore_provider, run_until_complete, on_complete, on_begin, on_end, PriorityScreen.PriorityClass.basic, 5, false, true, priority_mod, false, ReportManager.ReportType.WorkTime)
	{
		if (choreType == null)
		{
			global::Debug.LogError("You must specify a chore type for fetching!");
		}
		this.tagsFirst = ((tags.Count > 0) ? tags.First<Tag>() : Tag.Invalid);
		if (amount <= PICKUPABLETUNING.MINIMUM_PICKABLE_AMOUNT)
		{
			DebugUtil.LogWarningArgs(new object[] { string.Format("Chore {0} is requesting {1} {2} to {3}", new object[]
			{
				choreType.Id,
				this.tagsFirst,
				amount,
				(destination != null) ? destination.name : "to nowhere"
			}) });
		}
		base.SetPrioritizable((destination.prioritizable != null) ? destination.prioritizable : destination.GetComponent<Prioritizable>());
		base.smi = new FetchChore.StatesInstance(this);
		base.smi.sm.requestedamount.Set(amount, base.smi, false);
		this.destination = destination;
		DebugUtil.DevAssert(criteria != FetchChore.MatchCriteria.MatchTags || tags.Count <= 1, "For performance reasons fetch chores are limited to one tag when matching tags!", null);
		this.tags = tags;
		this.criteria = criteria;
		this.tagsHash = FetchChore.ComputeHashCodeForTags(tags);
		this.requiredTag = required_tag;
		this.forbiddenTags = ((forbidden_tags != null) ? forbidden_tags : new Tag[0]);
		this.forbidHash = FetchChore.ComputeHashCodeForTags(this.forbiddenTags);
		DebugUtil.DevAssert(!tags.Contains(GameTags.Preserved), "Fetch chore fetching invalid tags.", null);
		if (destination.GetOnlyFetchMarkedItems())
		{
			DebugUtil.DevAssert(!this.requiredTag.IsValid, "Only one requiredTag is supported at a time, this will stomp!", null);
			this.requiredTag = GameTags.Garbage;
		}
		this.AddPrecondition(ChorePreconditions.instance.IsScheduledTime, Db.Get().ScheduleBlockTypes.Work);
		this.AddPrecondition(ChorePreconditions.instance.CanMoveTo, destination);
		this.AddPrecondition(FetchChore.IsFetchTargetAvailable, null);
		this.AddPrecondition(FetchChore.CanFetchDroneComplete, destination.gameObject);
		Deconstructable component = this.target.GetComponent<Deconstructable>();
		if (component != null)
		{
			this.AddPrecondition(ChorePreconditions.instance.IsNotMarkedForDeconstruction, component);
		}
		BuildingEnabledButton component2 = this.target.GetComponent<BuildingEnabledButton>();
		if (component2 != null)
		{
			this.AddPrecondition(ChorePreconditions.instance.IsNotMarkedForDisable, component2);
		}
		if (operational_requirement != Operational.State.None)
		{
			Operational component3 = destination.GetComponent<Operational>();
			if (component3 != null)
			{
				Chore.Precondition precondition = ChorePreconditions.instance.IsOperational;
				if (operational_requirement == Operational.State.Functional)
				{
					precondition = ChorePreconditions.instance.IsFunctional;
				}
				this.AddPrecondition(precondition, component3);
			}
		}
		this.partitionerEntry = GameScenePartitioner.Instance.Add(destination.name, this, Grid.PosToCell(destination), GameScenePartitioner.Instance.fetchChoreLayer, null);
		destination.Subscribe(644822890, new Action<object>(this.OnOnlyFetchMarkedItemsSettingChanged));
		this.automatable = destination.GetComponent<Automatable>();
		if (this.automatable)
		{
			this.AddPrecondition(ChorePreconditions.instance.IsAllowedByAutomation, this.automatable);
		}
	}

	// Token: 0x0600184D RID: 6221 RVA: 0x000876DC File Offset: 0x000858DC
	private void OnOnlyFetchMarkedItemsSettingChanged(object data)
	{
		if (this.destination != null)
		{
			if (this.destination.GetOnlyFetchMarkedItems())
			{
				DebugUtil.DevAssert(!this.requiredTag.IsValid, "Only one requiredTag is supported at a time, this will stomp!", null);
				this.requiredTag = GameTags.Garbage;
				return;
			}
			this.requiredTag = Tag.Invalid;
		}
	}

	// Token: 0x0600184E RID: 6222 RVA: 0x00087734 File Offset: 0x00085934
	private void OnMasterPriorityChanged(PriorityScreen.PriorityClass priorityClass, int priority_value)
	{
		this.masterPriority.priority_class = priorityClass;
		this.masterPriority.priority_value = priority_value;
	}

	// Token: 0x0600184F RID: 6223 RVA: 0x0008774E File Offset: 0x0008594E
	public override void CollectChores(ChoreConsumerState consumer_state, List<Chore.Precondition.Context> succeeded_contexts, List<Chore.Precondition.Context> incomplete_contexts, List<Chore.Precondition.Context> failed_contexts, bool is_attempting_override)
	{
	}

	// Token: 0x06001850 RID: 6224 RVA: 0x00087750 File Offset: 0x00085950
	public void CollectChoresFromGlobalChoreProvider(ChoreConsumerState consumer_state, List<Chore.Precondition.Context> succeeded_contexts, List<Chore.Precondition.Context> failed_contexts, bool is_attempting_override)
	{
		this.CollectChoresFromGlobalChoreProvider(consumer_state, succeeded_contexts, null, failed_contexts, is_attempting_override);
	}

	// Token: 0x06001851 RID: 6225 RVA: 0x0008775E File Offset: 0x0008595E
	public void CollectChoresFromGlobalChoreProvider(ChoreConsumerState consumer_state, List<Chore.Precondition.Context> succeeded_contexts, List<Chore.Precondition.Context> incomplete_contexts, List<Chore.Precondition.Context> failed_contexts, bool is_attempting_override)
	{
		base.CollectChores(consumer_state, succeeded_contexts, incomplete_contexts, failed_contexts, is_attempting_override);
	}

	// Token: 0x06001852 RID: 6226 RVA: 0x00087770 File Offset: 0x00085970
	public override void Cleanup()
	{
		base.Cleanup();
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		if (this.destination != null)
		{
			this.destination.Unsubscribe(644822890, new Action<object>(this.OnOnlyFetchMarkedItemsSettingChanged));
		}
	}

	// Token: 0x06001853 RID: 6227 RVA: 0x000877C0 File Offset: 0x000859C0
	public static int ComputeHashCodeForTags(IEnumerable<Tag> tags)
	{
		int num = 0;
		foreach (Tag tag in tags)
		{
			num ^= tag.GetHash();
		}
		return num;
	}

	// Token: 0x04000E1E RID: 3614
	public HashSet<Tag> tags;

	// Token: 0x04000E1F RID: 3615
	public Tag tagsFirst;

	// Token: 0x04000E20 RID: 3616
	public FetchChore.MatchCriteria criteria;

	// Token: 0x04000E21 RID: 3617
	public int tagsHash;

	// Token: 0x04000E22 RID: 3618
	public bool validateRequiredTagOnTagChange;

	// Token: 0x04000E23 RID: 3619
	public Tag requiredTag;

	// Token: 0x04000E24 RID: 3620
	public Tag[] forbiddenTags;

	// Token: 0x04000E25 RID: 3621
	public int forbidHash;

	// Token: 0x04000E26 RID: 3622
	public Automatable automatable;

	// Token: 0x04000E27 RID: 3623
	public bool allowMultifetch = true;

	// Token: 0x04000E28 RID: 3624
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04000E29 RID: 3625
	public static readonly Chore.Precondition IsFetchTargetAvailable = new Chore.Precondition
	{
		id = "IsFetchTargetAvailable",
		description = DUPLICANTS.CHORES.PRECONDITIONS.IS_FETCH_TARGET_AVAILABLE,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			FetchChore fetchChore = (FetchChore)context.chore;
			Pickupable pickupable = (Pickupable)context.data;
			bool flag;
			if (pickupable == null)
			{
				pickupable = fetchChore.FindFetchTarget(context.consumerState);
				flag = pickupable != null;
			}
			else
			{
				flag = FetchManager.IsFetchablePickup(pickupable, fetchChore, context.consumerState.storage);
			}
			if (flag)
			{
				if (pickupable == null)
				{
					global::Debug.Log(string.Format("Failed to find fetch target for {0}", fetchChore.destination));
					return false;
				}
				context.data = pickupable;
				int num;
				if (context.consumerState.worker.IsFetchDrone())
				{
					if ((pickupable.targetWorkable == null || pickupable.targetWorkable.GetComponent<Pickupable>() != null) && context.consumerState.consumer.GetNavigationCost(pickupable, out num))
					{
						context.cost += num;
						return true;
					}
				}
				else if (context.consumerState.consumer.GetNavigationCost(pickupable, out num))
				{
					context.cost += num;
					return true;
				}
			}
			return false;
		}
	};

	// Token: 0x04000E2A RID: 3626
	public static readonly Chore.Precondition CanFetchDroneComplete = new Chore.Precondition
	{
		id = "CanFetchDroneComplete",
		description = DUPLICANTS.CHORES.PRECONDITIONS.CAN_FETCH_DRONE_COMPLETE_FETCH,
		canExecuteOnAnyThread = true,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			if (!context.consumerState.worker.IsFetchDrone())
			{
				return true;
			}
			FetchChore fetchChore2 = (FetchChore)context.chore;
			Pickupable pickupable2 = (Pickupable)context.data;
			bool flag2;
			if (pickupable2 == null)
			{
				pickupable2 = fetchChore2.FindFetchTarget(context.consumerState);
				flag2 = pickupable2 != null;
			}
			else
			{
				flag2 = FetchManager.IsFetchablePickup(pickupable2, fetchChore2, context.consumerState.storage);
			}
			return flag2 && !((GameObject)data == context.consumerState.gameObject) && ((pickupable2.targetWorkable == null || pickupable2.targetWorkable as Pickupable != null) && context.consumerState.consumer.navigator.CanReach(pickupable2.cachedCell));
		}
	};

	// Token: 0x02001280 RID: 4736
	public enum MatchCriteria
	{
		// Token: 0x04006689 RID: 26249
		MatchID,
		// Token: 0x0400668A RID: 26250
		MatchTags
	}

	// Token: 0x02001281 RID: 4737
	public class StatesInstance : GameStateMachine<FetchChore.States, FetchChore.StatesInstance, FetchChore, object>.GameInstance
	{
		// Token: 0x060086BB RID: 34491 RVA: 0x0033FD60 File Offset: 0x0033DF60
		public StatesInstance(FetchChore master)
			: base(master)
		{
		}
	}

	// Token: 0x02001282 RID: 4738
	public class States : GameStateMachine<FetchChore.States, FetchChore.StatesInstance, FetchChore>
	{
		// Token: 0x060086BC RID: 34492 RVA: 0x0033FD69 File Offset: 0x0033DF69
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
		}

		// Token: 0x0400668B RID: 26251
		public StateMachine<FetchChore.States, FetchChore.StatesInstance, FetchChore, object>.TargetParameter fetcher;

		// Token: 0x0400668C RID: 26252
		public StateMachine<FetchChore.States, FetchChore.StatesInstance, FetchChore, object>.TargetParameter source;

		// Token: 0x0400668D RID: 26253
		public StateMachine<FetchChore.States, FetchChore.StatesInstance, FetchChore, object>.TargetParameter chunk;

		// Token: 0x0400668E RID: 26254
		public StateMachine<FetchChore.States, FetchChore.StatesInstance, FetchChore, object>.FloatParameter requestedamount;

		// Token: 0x0400668F RID: 26255
		public StateMachine<FetchChore.States, FetchChore.StatesInstance, FetchChore, object>.FloatParameter actualamount;
	}
}
