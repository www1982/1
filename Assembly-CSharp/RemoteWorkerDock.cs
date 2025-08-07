using System;
using System.Collections.Generic;
using Klei;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000A9C RID: 2716
[AddComponentMenu("KMonoBehaviour/Workable/RemoteWorkDock")]
public class RemoteWorkerDock : KMonoBehaviour
{
	// Token: 0x17000574 RID: 1396
	// (get) Token: 0x06004ECF RID: 20175 RVA: 0x001C76B5 File Offset: 0x001C58B5
	// (set) Token: 0x06004ED0 RID: 20176 RVA: 0x001C76BD File Offset: 0x001C58BD
	public RemoteWorkerSM RemoteWorker
	{
		get
		{
			return this.remoteWorker;
		}
		private set
		{
			this.remoteWorker = value;
			this.worker = ((value != null) ? new Ref<KSelectable>(value.GetComponent<KSelectable>()) : null);
		}
	}

	// Token: 0x06004ED1 RID: 20177 RVA: 0x001C76E3 File Offset: 0x001C58E3
	public WorkerBase GetActiveTerminalWorker()
	{
		if (this.terminal == null)
		{
			return null;
		}
		return this.terminal.worker;
	}

	// Token: 0x17000575 RID: 1397
	// (get) Token: 0x06004ED2 RID: 20178 RVA: 0x001C7700 File Offset: 0x001C5900
	public bool IsOperational
	{
		get
		{
			return this.operational.IsOperational;
		}
	}

	// Token: 0x06004ED3 RID: 20179 RVA: 0x001C7710 File Offset: 0x001C5910
	private bool canWork(IRemoteDockWorkTarget provider)
	{
		int num;
		int num2;
		Grid.CellToXY(Grid.PosToCell(this), out num, out num2);
		int num3;
		int num4;
		Grid.CellToXY(provider.Approachable.GetCell(), out num3, out num4);
		return num2 == num4 && Math.Abs(num - num3) <= 12;
	}

	// Token: 0x06004ED4 RID: 20180 RVA: 0x001C7755 File Offset: 0x001C5955
	private void considerProvider(IRemoteDockWorkTarget provider)
	{
		if (this.canWork(provider))
		{
			this.providers.Add(provider);
		}
	}

	// Token: 0x06004ED5 RID: 20181 RVA: 0x001C776C File Offset: 0x001C596C
	private void forgetProvider(IRemoteDockWorkTarget provider)
	{
		this.providers.Remove(provider);
	}

	// Token: 0x06004ED6 RID: 20182 RVA: 0x001C777C File Offset: 0x001C597C
	private static string GenerateName()
	{
		string text = "";
		for (int i = 0; i < 3; i++)
		{
			text += "011223345789"[global::UnityEngine.Random.Range(0, "011223345789".Length)].ToString();
		}
		return BUILDINGS.PREFABS.REMOTEWORKERDOCK.NAME_FMT.Replace("{ID}", text);
	}

	// Token: 0x06004ED7 RID: 20183 RVA: 0x001C77D4 File Offset: 0x001C59D4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		UserNameable component = base.GetComponent<UserNameable>();
		if (component.savedName == "" || component.savedName == BUILDINGS.PREFABS.REMOTEWORKERDOCK.NAME)
		{
			component.SetName(RemoteWorkerDock.GenerateName());
		}
		base.Subscribe(-1697596308, new Action<object>(this.OnStorageChanged));
		Components.RemoteWorkerDocks.Add(this.GetMyWorldId(), this);
		this.add_provider_binding = new Action<IRemoteDockWorkTarget>(this.considerProvider);
		this.remove_provider_binding = new Action<IRemoteDockWorkTarget>(this.forgetProvider);
		Components.RemoteDockWorkTargets.Register(this.GetMyWorldId(), this.add_provider_binding, this.remove_provider_binding);
		Ref<KSelectable> @ref = this.worker;
		RemoteWorkerSM remoteWorkerSM;
		if (@ref == null)
		{
			remoteWorkerSM = null;
		}
		else
		{
			KSelectable kselectable = @ref.Get();
			remoteWorkerSM = ((kselectable != null) ? kselectable.GetComponent<RemoteWorkerSM>() : null);
		}
		this.remoteWorker = remoteWorkerSM;
		if (this.remoteWorker == null)
		{
			this.RequestNewWorker(null);
			return;
		}
		this.remoteWorkerDestroyedEventId = this.remoteWorker.Subscribe(1969584890, new Action<object>(this.RequestNewWorker));
	}

	// Token: 0x06004ED8 RID: 20184 RVA: 0x001C78E8 File Offset: 0x001C5AE8
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.RemoteWorkerDocks.Remove(this.GetMyWorldId(), this);
		Components.RemoteDockWorkTargets.Unregister(this.GetMyWorldId(), this.add_provider_binding, this.remove_provider_binding);
		if (this.remoteWorker != null)
		{
			this.remoteWorker.Unsubscribe(this.remoteWorkerDestroyedEventId);
		}
		if (this.newRemoteWorkerHandle.IsValid)
		{
			this.newRemoteWorkerHandle.ClearScheduler();
		}
	}

	// Token: 0x06004ED9 RID: 20185 RVA: 0x001C7960 File Offset: 0x001C5B60
	public void CollectChores(ChoreConsumerState duplicant_state, List<Chore.Precondition.Context> succeeded_contexts, List<Chore.Precondition.Context> incomplete_contexts, List<Chore.Precondition.Context> failed_contexts, bool is_attempting_override)
	{
		if (this.remoteWorker == null)
		{
			return;
		}
		ChoreConsumerState consumerState = this.remoteWorker.ConsumerState;
		consumerState.resume = duplicant_state.resume;
		foreach (IRemoteDockWorkTarget remoteDockWorkTarget in this.providers)
		{
			Chore remoteDockChore = remoteDockWorkTarget.RemoteDockChore;
			if (remoteDockChore != null)
			{
				remoteDockChore.CollectChores(consumerState, succeeded_contexts, incomplete_contexts, failed_contexts, false);
			}
		}
	}

	// Token: 0x06004EDA RID: 20186 RVA: 0x001C79E8 File Offset: 0x001C5BE8
	public bool AvailableForWorkBy(RemoteWorkTerminal terminal)
	{
		return this.terminal == null || this.terminal == terminal;
	}

	// Token: 0x06004EDB RID: 20187 RVA: 0x001C7A06 File Offset: 0x001C5C06
	public bool HasWorker()
	{
		return this.remoteWorker != null;
	}

	// Token: 0x06004EDC RID: 20188 RVA: 0x001C7A14 File Offset: 0x001C5C14
	public void SetNextChore(RemoteWorkTerminal terminal, Chore.Precondition.Context chore_context)
	{
		global::Debug.Assert(this.worker != null);
		global::Debug.Assert(this.terminal == null || this.terminal == terminal);
		this.terminal = terminal;
		if (this.remoteWorker != null)
		{
			this.remoteWorker.SetNextChore(chore_context);
		}
	}

	// Token: 0x06004EDD RID: 20189 RVA: 0x001C7A74 File Offset: 0x001C5C74
	public bool StartWorking(RemoteWorkTerminal terminal)
	{
		if (this.terminal == null)
		{
			this.terminal = terminal;
		}
		if (this.terminal == terminal && this.remoteWorker != null)
		{
			this.remoteWorker.ActivelyControlled = true;
			return true;
		}
		return false;
	}

	// Token: 0x06004EDE RID: 20190 RVA: 0x001C7AC1 File Offset: 0x001C5CC1
	public void StopWorking(RemoteWorkTerminal terminal)
	{
		if (terminal == this.terminal)
		{
			this.terminal = null;
			if (this.remoteWorker != null)
			{
				this.remoteWorker.ActivelyControlled = false;
			}
		}
	}

	// Token: 0x06004EDF RID: 20191 RVA: 0x001C7AF2 File Offset: 0x001C5CF2
	public bool OnRemoteWorkTick(float dt)
	{
		return this.remoteWorker == null || (!this.remoteWorker.ActivelyWorking && !this.remoteWorker.HasChoreQueued());
	}

	// Token: 0x06004EE0 RID: 20192 RVA: 0x001C7B21 File Offset: 0x001C5D21
	private void OnStorageChanged(object _)
	{
		if (this.remoteWorker == null || this.worker.Get() == null)
		{
			this.RequestNewWorker(null);
		}
	}

	// Token: 0x06004EE1 RID: 20193 RVA: 0x001C7B4C File Offset: 0x001C5D4C
	private void RequestNewWorker(object _ = null)
	{
		if (this.newRemoteWorkerHandle.IsValid)
		{
			return;
		}
		Tag build_MATERIAL_TAG = RemoteWorkerConfig.BUILD_MATERIAL_TAG;
		if (this.storage.FindFirstWithMass(build_MATERIAL_TAG, 200f) == null)
		{
			if (!this.activeFetch)
			{
				this.activeFetch = true;
				FetchList2 fetchList = new FetchList2(this.storage, Db.Get().ChoreTypes.Fetch);
				fetchList.Add(build_MATERIAL_TAG, null, 200f, Operational.State.None);
				fetchList.Submit(delegate
				{
					this.activeFetch = false;
					this.RequestNewWorker(null);
				}, true);
				return;
			}
		}
		else
		{
			this.MakeNewWorker(null);
		}
	}

	// Token: 0x06004EE2 RID: 20194 RVA: 0x001C7BD8 File Offset: 0x001C5DD8
	private void MakeNewWorker(object _ = null)
	{
		if (this.newRemoteWorkerHandle.IsValid)
		{
			return;
		}
		if (this.storage.GetAmountAvailable(RemoteWorkerConfig.BUILD_MATERIAL_TAG) < 200f)
		{
			return;
		}
		PrimaryElement elem = this.storage.FindFirstWithMass(RemoteWorkerConfig.BUILD_MATERIAL_TAG, 200f);
		if (elem == null)
		{
			return;
		}
		float temperature;
		SimUtil.DiseaseInfo disease;
		float num;
		this.storage.ConsumeAndGetDisease(elem.ElementID.CreateTag(), 200f, out num, out disease, out temperature);
		this.status_item_handle = base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.RemoteWorkDockMakingWorker, null);
		this.newRemoteWorkerHandle = GameScheduler.Instance.Schedule("MakeRemoteWorker", 2f, delegate(object _)
		{
			GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab(RemoteWorkerConfig.ID), this.transform.position, Grid.SceneLayer.Creatures, null, 0);
			if (this.remoteWorkerDestroyedEventId != -1 && this.remoteWorker != null)
			{
				this.remoteWorker.Unsubscribe(this.remoteWorkerDestroyedEventId);
			}
			this.RemoteWorker = gameObject.GetComponent<RemoteWorkerSM>();
			this.remoteWorker.HomeDepot = this;
			this.remoteWorker.playNewWorker = true;
			gameObject.SetActive(true);
			PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
			component.ElementID = elem.ElementID;
			component.Temperature = temperature;
			if (disease.idx != 255)
			{
				component.AddDisease(disease.idx, disease.count, "Inherited from construction material");
			}
			this.remoteWorkerDestroyedEventId = gameObject.Subscribe(1969584890, new Action<object>(this.RequestNewWorker));
			this.newRemoteWorkerHandle.ClearScheduler();
			this.GetComponent<KSelectable>().RemoveStatusItem(this.status_item_handle, false);
		}, null, null);
	}

	// Token: 0x04003451 RID: 13393
	[Serialize]
	protected Ref<KSelectable> worker;

	// Token: 0x04003452 RID: 13394
	protected RemoteWorkerSM remoteWorker;

	// Token: 0x04003453 RID: 13395
	private int remoteWorkerDestroyedEventId = -1;

	// Token: 0x04003454 RID: 13396
	protected RemoteWorkTerminal terminal;

	// Token: 0x04003455 RID: 13397
	[MyCmpGet]
	private Storage storage;

	// Token: 0x04003456 RID: 13398
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04003457 RID: 13399
	[MyCmpAdd]
	private UserNameable nameable;

	// Token: 0x04003458 RID: 13400
	[MyCmpAdd]
	private RemoteWorkerDock.NewWorker new_worker_;

	// Token: 0x04003459 RID: 13401
	[MyCmpAdd]
	private RemoteWorkerDock.EnterableDock enter_;

	// Token: 0x0400345A RID: 13402
	[MyCmpAdd]
	private RemoteWorkerDock.ExitableDock exit_;

	// Token: 0x0400345B RID: 13403
	[MyCmpAdd]
	private RemoteWorkerDock.WorkerRecharger recharger_;

	// Token: 0x0400345C RID: 13404
	[MyCmpAdd]
	private RemoteWorkerDock.WorkerGunkRemover gunk_remover_;

	// Token: 0x0400345D RID: 13405
	[MyCmpAdd]
	private RemoteWorkerDock.WorkerOilRefiller oil_refiller_;

	// Token: 0x0400345E RID: 13406
	private Guid status_item_handle;

	// Token: 0x0400345F RID: 13407
	private SchedulerHandle newRemoteWorkerHandle;

	// Token: 0x04003460 RID: 13408
	private List<IRemoteDockWorkTarget> providers = new List<IRemoteDockWorkTarget>();

	// Token: 0x04003461 RID: 13409
	private Action<IRemoteDockWorkTarget> add_provider_binding;

	// Token: 0x04003462 RID: 13410
	private Action<IRemoteDockWorkTarget> remove_provider_binding;

	// Token: 0x04003463 RID: 13411
	private bool activeFetch;

	// Token: 0x02001B7E RID: 7038
	public class NewWorker : Workable
	{
		// Token: 0x0600A7C7 RID: 42951 RVA: 0x003B2780 File Offset: 0x003B0980
		protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
			this.workAnims = RemoteWorkerDock.NewWorker.WORK_ANIMS;
			this.workingPstComplete = null;
			this.workingPstFailed = null;
			this.workAnimPlayMode = KAnim.PlayMode.Once;
			this.synchronizeAnims = true;
			this.triggerWorkReactions = false;
			this.workLayer = Grid.SceneLayer.BuildingUse;
			this.resetProgressOnStop = true;
			KAnim.Anim anim = Assets.GetAnim(RemoteWorkerConfig.DOCK_ANIM_OVERRIDES).GetData().GetAnim("new_worker");
			base.SetWorkTime((float)anim.numFrames / anim.frameRate);
		}

		// Token: 0x0600A7C8 RID: 42952 RVA: 0x003B2803 File Offset: 0x003B0A03
		protected override void OnStartWork(WorkerBase worker)
		{
			base.OnStartWork(worker);
		}

		// Token: 0x0600A7C9 RID: 42953 RVA: 0x003B280C File Offset: 0x003B0A0C
		protected override void OnCompleteWork(WorkerBase worker)
		{
			base.OnCompleteWork(worker);
			worker.GetComponent<RemoteWorkerSM>().Docked = true;
		}

		// Token: 0x04008304 RID: 33540
		private static readonly HashedString[] WORK_ANIMS = new HashedString[] { "new_worker" };
	}

	// Token: 0x02001B7F RID: 7039
	public class EnterableDock : Workable
	{
		// Token: 0x0600A7CC RID: 42956 RVA: 0x003B2848 File Offset: 0x003B0A48
		protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
			this.workerStatusItem = Db.Get().DuplicantStatusItems.EnteringDock;
			this.workAnims = RemoteWorkerDock.EnterableDock.WORK_ANIMS;
			this.workingPstComplete = null;
			this.workingPstFailed = null;
			this.workAnimPlayMode = KAnim.PlayMode.Once;
			this.synchronizeAnims = true;
			this.triggerWorkReactions = false;
			this.workLayer = Grid.SceneLayer.BuildingUse;
			this.resetProgressOnStop = true;
			KAnim.Anim anim = Assets.GetAnim(RemoteWorkerConfig.DOCK_ANIM_OVERRIDES).GetData().GetAnim("enter_dock");
			base.SetWorkTime((float)anim.numFrames / anim.frameRate);
		}

		// Token: 0x0600A7CD RID: 42957 RVA: 0x003B28E0 File Offset: 0x003B0AE0
		protected override void OnCompleteWork(WorkerBase worker)
		{
			worker.GetComponent<RemoteWorkerSM>().Docked = true;
			base.OnCompleteWork(worker);
		}

		// Token: 0x04008305 RID: 33541
		private static readonly HashedString[] WORK_ANIMS = new HashedString[] { "enter_dock" };
	}

	// Token: 0x02001B80 RID: 7040
	public class ExitableDock : Workable
	{
		// Token: 0x0600A7D0 RID: 42960 RVA: 0x003B291C File Offset: 0x003B0B1C
		protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
			this.workAnims = RemoteWorkerDock.ExitableDock.WORK_ANIMS;
			this.workingPstComplete = null;
			this.workingPstFailed = null;
			this.workAnimPlayMode = KAnim.PlayMode.Once;
			this.synchronizeAnims = true;
			this.triggerWorkReactions = false;
			this.workLayer = Grid.SceneLayer.BuildingUse;
			this.resetProgressOnStop = true;
			KAnim.Anim anim = Assets.GetAnim(RemoteWorkerConfig.DOCK_ANIM_OVERRIDES).GetData().GetAnim("exit_dock");
			base.SetWorkTime((float)anim.numFrames / anim.frameRate);
		}

		// Token: 0x0600A7D1 RID: 42961 RVA: 0x003B299F File Offset: 0x003B0B9F
		protected override void OnCompleteWork(WorkerBase worker)
		{
			base.OnCompleteWork(worker);
			worker.GetComponent<RemoteWorkerSM>().Docked = false;
		}

		// Token: 0x04008306 RID: 33542
		private static readonly HashedString[] WORK_ANIMS = new HashedString[] { "exit_dock" };
	}

	// Token: 0x02001B81 RID: 7041
	public class WorkerRecharger : Workable
	{
		// Token: 0x0600A7D4 RID: 42964 RVA: 0x003B29DC File Offset: 0x003B0BDC
		protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
			this.workAnims = RemoteWorkerDock.WorkerRecharger.WORK_ANIMS;
			this.workingPstComplete = RemoteWorkerDock.WorkerRecharger.WORK_PST_ANIM;
			this.workingPstFailed = RemoteWorkerDock.WorkerRecharger.WORK_PST_ANIM;
			this.synchronizeAnims = true;
			this.triggerWorkReactions = false;
			this.workLayer = Grid.SceneLayer.BuildingUse;
			this.workerStatusItem = Db.Get().DuplicantStatusItems.RemoteWorkerRecharging;
			base.SetWorkTime(float.PositiveInfinity);
		}

		// Token: 0x0600A7D5 RID: 42965 RVA: 0x003B2A48 File Offset: 0x003B0C48
		protected override void OnStartWork(WorkerBase worker)
		{
			base.OnStartWork(worker);
			RemoteWorkerCapacitor component = worker.GetComponent<RemoteWorkerCapacitor>();
			this.progress = ((component != null) ? component.ChargeRatio : 0f);
			if (this.progressBar != null)
			{
				this.progressBar.SetUpdateFunc(() => this.progress);
			}
		}

		// Token: 0x0600A7D6 RID: 42966 RVA: 0x003B2AA4 File Offset: 0x003B0CA4
		protected override bool OnWorkTick(WorkerBase worker, float dt)
		{
			base.OnWorkTick(worker, dt);
			RemoteWorkerCapacitor component = worker.GetComponent<RemoteWorkerCapacitor>();
			if (component != null)
			{
				this.progress = component.ChargeRatio;
				return component.ApplyDeltaEnergy(7.5f * dt) == 0f;
			}
			return true;
		}

		// Token: 0x04008307 RID: 33543
		private static readonly HashedString[] WORK_ANIMS = new HashedString[] { "recharge_pre", "recharge_loop" };

		// Token: 0x04008308 RID: 33544
		private static readonly HashedString[] WORK_PST_ANIM = new HashedString[] { "recharge_pst" };

		// Token: 0x04008309 RID: 33545
		private float progress;
	}

	// Token: 0x02001B82 RID: 7042
	public class WorkerGunkRemover : Workable
	{
		// Token: 0x0600A7DA RID: 42970 RVA: 0x003B2B54 File Offset: 0x003B0D54
		protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
			this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_remote_work_dock_kanim") };
			this.workAnims = RemoteWorkerDock.WorkerGunkRemover.WORK_ANIMS;
			this.workingPstComplete = RemoteWorkerDock.WorkerGunkRemover.WORK_PST_ANIM;
			this.workingPstFailed = RemoteWorkerDock.WorkerGunkRemover.WORK_PST_ANIM;
			this.synchronizeAnims = true;
			this.triggerWorkReactions = false;
			this.workLayer = Grid.SceneLayer.BuildingUse;
			this.workerStatusItem = Db.Get().DuplicantStatusItems.RemoteWorkerDraining;
			base.SetWorkTime(float.PositiveInfinity);
		}

		// Token: 0x0600A7DB RID: 42971 RVA: 0x003B2BDC File Offset: 0x003B0DDC
		protected override void OnStartWork(WorkerBase worker)
		{
			base.OnStartWork(worker);
			Storage component = worker.GetComponent<Storage>();
			if (component != null)
			{
				this.progress = 1f - component.GetMassAvailable(SimHashes.LiquidGunk) / 20.000002f;
			}
			if (this.progressBar != null)
			{
				this.progressBar.SetUpdateFunc(() => this.progress);
			}
		}

		// Token: 0x0600A7DC RID: 42972 RVA: 0x003B2C44 File Offset: 0x003B0E44
		protected override bool OnWorkTick(WorkerBase worker, float dt)
		{
			base.OnWorkTick(worker, dt);
			Storage component = worker.GetComponent<Storage>();
			if (component != null)
			{
				float massAvailable = component.GetMassAvailable(SimHashes.LiquidGunk);
				float num = Math.Min(massAvailable, 3.3333337f * dt);
				this.progress = 1f - massAvailable / 20.000002f;
				if (num > 0f)
				{
					component.TransferMass(this.storage, SimHashes.LiquidGunk.CreateTag(), num, false, false, true);
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400830A RID: 33546
		private static readonly HashedString[] WORK_ANIMS = new HashedString[] { "drain_gunk_pre", "drain_gunk_loop" };

		// Token: 0x0400830B RID: 33547
		private static readonly HashedString[] WORK_PST_ANIM = new HashedString[] { "drain_gunk_pst" };

		// Token: 0x0400830C RID: 33548
		[MyCmpGet]
		private Storage storage;

		// Token: 0x0400830D RID: 33549
		private float progress;
	}

	// Token: 0x02001B83 RID: 7043
	public class WorkerOilRefiller : Workable
	{
		// Token: 0x0600A7E0 RID: 42976 RVA: 0x003B2D28 File Offset: 0x003B0F28
		protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
			this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_remote_work_dock_kanim") };
			this.workAnims = RemoteWorkerDock.WorkerOilRefiller.WORK_ANIMS;
			this.workingPstComplete = RemoteWorkerDock.WorkerOilRefiller.WORK_PST_ANIM;
			this.workingPstFailed = RemoteWorkerDock.WorkerOilRefiller.WORK_PST_ANIM;
			this.synchronizeAnims = true;
			this.triggerWorkReactions = false;
			this.workLayer = Grid.SceneLayer.BuildingUse;
			this.workerStatusItem = Db.Get().DuplicantStatusItems.RemoteWorkerOiling;
			base.SetWorkTime(float.PositiveInfinity);
		}

		// Token: 0x0600A7E1 RID: 42977 RVA: 0x003B2DB0 File Offset: 0x003B0FB0
		protected override void OnStartWork(WorkerBase worker)
		{
			base.OnStartWork(worker);
			Storage component = worker.GetComponent<Storage>();
			if (component != null)
			{
				float massAvailable = component.GetMassAvailable(GameTags.LubricatingOil);
				this.progress = massAvailable / 20.000002f;
			}
			if (this.progressBar != null)
			{
				this.progressBar.SetUpdateFunc(() => this.progress);
			}
		}

		// Token: 0x0600A7E2 RID: 42978 RVA: 0x003B2E14 File Offset: 0x003B1014
		protected override bool OnWorkTick(WorkerBase worker, float dt)
		{
			base.OnWorkTick(worker, dt);
			Storage component = worker.GetComponent<Storage>();
			if (component != null)
			{
				float massAvailable = component.GetMassAvailable(GameTags.LubricatingOil);
				float num = Math.Min(20.000002f - massAvailable, 2.5000002f * dt);
				this.progress = massAvailable / 20.000002f;
				if (num > 0f)
				{
					this.storage.TransferMass(component, GameTags.LubricatingOil, num, false, false, true);
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400830E RID: 33550
		private static readonly HashedString[] WORK_ANIMS = new HashedString[] { "oil_pre", "oil_loop" };

		// Token: 0x0400830F RID: 33551
		private static readonly HashedString[] WORK_PST_ANIM = new HashedString[] { "oil_pst" };

		// Token: 0x04008310 RID: 33552
		[MyCmpGet]
		private Storage storage;

		// Token: 0x04008311 RID: 33553
		private float progress;
	}
}
