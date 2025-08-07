using System;
using Klei.AI;
using UnityEngine;

// Token: 0x02000581 RID: 1409
[AddComponentMenu("KMonoBehaviour/Workable/Clinic Dreamable")]
public class ClinicDreamable : Workable
{
	// Token: 0x17000136 RID: 310
	// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x000B75AB File Offset: 0x000B57AB
	// (set) Token: 0x06001FF1 RID: 8177 RVA: 0x000B75B3 File Offset: 0x000B57B3
	public bool DreamIsDisturbed { get; private set; }

	// Token: 0x06001FF2 RID: 8178 RVA: 0x000B75BC File Offset: 0x000B57BC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.resetProgressOnStop = false;
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Dreaming;
		this.workingStatusItem = null;
	}

	// Token: 0x06001FF3 RID: 8179 RVA: 0x000B75E8 File Offset: 0x000B57E8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (ClinicDreamable.dreamJournalPrefab == null)
		{
			ClinicDreamable.dreamJournalPrefab = Assets.GetPrefab(DreamJournalConfig.ID);
			ClinicDreamable.sleepClinic = Db.Get().effects.Get("SleepClinic");
		}
		this.equippable = base.GetComponent<Equippable>();
		global::Debug.Assert(this.equippable != null);
		EquipmentDef def = this.equippable.def;
		def.OnEquipCallBack = (Action<Equippable>)Delegate.Combine(def.OnEquipCallBack, new Action<Equippable>(this.OnEquipPajamas));
		EquipmentDef def2 = this.equippable.def;
		def2.OnUnequipCallBack = (Action<Equippable>)Delegate.Combine(def2.OnUnequipCallBack, new Action<Equippable>(this.OnUnequipPajamas));
		this.OnEquipPajamas(this.equippable);
	}

	// Token: 0x06001FF4 RID: 8180 RVA: 0x000B76B4 File Offset: 0x000B58B4
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.equippable == null)
		{
			return;
		}
		EquipmentDef def = this.equippable.def;
		def.OnEquipCallBack = (Action<Equippable>)Delegate.Remove(def.OnEquipCallBack, new Action<Equippable>(this.OnEquipPajamas));
		EquipmentDef def2 = this.equippable.def;
		def2.OnUnequipCallBack = (Action<Equippable>)Delegate.Remove(def2.OnUnequipCallBack, new Action<Equippable>(this.OnUnequipPajamas));
	}

	// Token: 0x06001FF5 RID: 8181 RVA: 0x000B7730 File Offset: 0x000B5930
	protected override bool OnWorkTick(WorkerBase worker, float dt)
	{
		if (this.GetPercentComplete() >= 1f)
		{
			Vector3 position = this.dreamer.transform.position;
			position.y += 1f;
			position.z = Grid.GetLayerZ(Grid.SceneLayer.Ore);
			Util.KInstantiate(ClinicDreamable.dreamJournalPrefab, position, Quaternion.identity, null, null, true, 0).SetActive(true);
			this.workTimeRemaining = this.GetWorkTime();
		}
		return false;
	}

	// Token: 0x06001FF6 RID: 8182 RVA: 0x000B77A8 File Offset: 0x000B59A8
	public void OnEquipPajamas(Equippable eq)
	{
		if (this.equippable == null || this.equippable != eq)
		{
			return;
		}
		MinionAssignablesProxy minionAssignablesProxy = this.equippable.assignee as MinionAssignablesProxy;
		if (minionAssignablesProxy == null)
		{
			return;
		}
		if (minionAssignablesProxy.target is StoredMinionIdentity)
		{
			return;
		}
		GameObject targetGameObject = minionAssignablesProxy.GetTargetGameObject();
		this.effects = targetGameObject.GetComponent<Effects>();
		this.dreamer = targetGameObject.GetComponent<ChoreDriver>();
		this.selectable = targetGameObject.GetComponent<KSelectable>();
		this.dreamer.Subscribe(-1283701846, new Action<object>(this.WorkerStartedSleeping));
		this.dreamer.Subscribe(-2090444759, new Action<object>(this.WorkerStoppedSleeping));
		this.effects.Add(ClinicDreamable.sleepClinic, true);
		this.selectable.AddStatusItem(Db.Get().DuplicantStatusItems.MegaBrainTank_Pajamas_Wearing, null);
	}

	// Token: 0x06001FF7 RID: 8183 RVA: 0x000B7890 File Offset: 0x000B5A90
	public void OnUnequipPajamas(Equippable eq)
	{
		if (this.dreamer == null)
		{
			return;
		}
		if (this.equippable == null || this.equippable != eq)
		{
			return;
		}
		this.dreamer.Unsubscribe(-1283701846, new Action<object>(this.WorkerStartedSleeping));
		this.dreamer.Unsubscribe(-2090444759, new Action<object>(this.WorkerStoppedSleeping));
		this.selectable.RemoveStatusItem(Db.Get().DuplicantStatusItems.MegaBrainTank_Pajamas_Wearing, false);
		this.selectable.RemoveStatusItem(Db.Get().DuplicantStatusItems.MegaBrainTank_Pajamas_Sleeping, false);
		this.effects.Remove(ClinicDreamable.sleepClinic.Id);
		this.StopDreamingThought();
		this.dreamer = null;
		this.selectable = null;
		this.effects = null;
	}

	// Token: 0x06001FF8 RID: 8184 RVA: 0x000B796C File Offset: 0x000B5B6C
	public void WorkerStartedSleeping(object data)
	{
		SleepChore sleepChore = this.dreamer.GetCurrentChore() as SleepChore;
		StateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.Parameter<bool>.Context context = sleepChore.smi.sm.isDisturbedByLight.GetContext(sleepChore.smi);
		context.onDirty = (Action<SleepChore.StatesInstance>)Delegate.Combine(context.onDirty, new Action<SleepChore.StatesInstance>(this.OnSleepDisturbed));
		StateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.Parameter<bool>.Context context2 = sleepChore.smi.sm.isDisturbedByMovement.GetContext(sleepChore.smi);
		context2.onDirty = (Action<SleepChore.StatesInstance>)Delegate.Combine(context2.onDirty, new Action<SleepChore.StatesInstance>(this.OnSleepDisturbed));
		StateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.Parameter<bool>.Context context3 = sleepChore.smi.sm.isDisturbedByNoise.GetContext(sleepChore.smi);
		context3.onDirty = (Action<SleepChore.StatesInstance>)Delegate.Combine(context3.onDirty, new Action<SleepChore.StatesInstance>(this.OnSleepDisturbed));
		StateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.Parameter<bool>.Context context4 = sleepChore.smi.sm.isScaredOfDark.GetContext(sleepChore.smi);
		context4.onDirty = (Action<SleepChore.StatesInstance>)Delegate.Combine(context4.onDirty, new Action<SleepChore.StatesInstance>(this.OnSleepDisturbed));
		this.sleepable = data as Sleepable;
		this.sleepable.Dreamable = this;
		base.StartWork(this.sleepable.worker);
		this.progressBar.Retarget(this.sleepable.gameObject);
		this.selectable.AddStatusItem(Db.Get().DuplicantStatusItems.MegaBrainTank_Pajamas_Sleeping, this);
		this.StartDreamingThought();
	}

	// Token: 0x06001FF9 RID: 8185 RVA: 0x000B7ADC File Offset: 0x000B5CDC
	public void WorkerStoppedSleeping(object data)
	{
		this.selectable.RemoveStatusItem(Db.Get().DuplicantStatusItems.MegaBrainTank_Pajamas_Sleeping, false);
		SleepChore sleepChore = this.dreamer.GetCurrentChore() as SleepChore;
		if (!sleepChore.IsNullOrDestroyed() && !sleepChore.smi.IsNullOrDestroyed() && !sleepChore.smi.sm.IsNullOrDestroyed())
		{
			StateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.Parameter<bool>.Context context = sleepChore.smi.sm.isDisturbedByLight.GetContext(sleepChore.smi);
			context.onDirty = (Action<SleepChore.StatesInstance>)Delegate.Remove(context.onDirty, new Action<SleepChore.StatesInstance>(this.OnSleepDisturbed));
			StateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.Parameter<bool>.Context context2 = sleepChore.smi.sm.isDisturbedByMovement.GetContext(sleepChore.smi);
			context2.onDirty = (Action<SleepChore.StatesInstance>)Delegate.Remove(context2.onDirty, new Action<SleepChore.StatesInstance>(this.OnSleepDisturbed));
			StateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.Parameter<bool>.Context context3 = sleepChore.smi.sm.isDisturbedByNoise.GetContext(sleepChore.smi);
			context3.onDirty = (Action<SleepChore.StatesInstance>)Delegate.Remove(context3.onDirty, new Action<SleepChore.StatesInstance>(this.OnSleepDisturbed));
			StateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.Parameter<bool>.Context context4 = sleepChore.smi.sm.isScaredOfDark.GetContext(sleepChore.smi);
			context4.onDirty = (Action<SleepChore.StatesInstance>)Delegate.Remove(context4.onDirty, new Action<SleepChore.StatesInstance>(this.OnSleepDisturbed));
		}
		this.StopDreamingThought();
		this.DreamIsDisturbed = false;
		if (base.worker != null)
		{
			base.StopWork(base.worker, false);
		}
		if (this.sleepable != null)
		{
			this.sleepable.Dreamable = null;
			this.sleepable = null;
		}
	}

	// Token: 0x06001FFA RID: 8186 RVA: 0x000B7C80 File Offset: 0x000B5E80
	private void OnSleepDisturbed(SleepChore.StatesInstance smi)
	{
		SleepChore sleepChore = this.dreamer.GetCurrentChore() as SleepChore;
		bool flag = sleepChore.smi.sm.isDisturbedByLight.Get(sleepChore.smi);
		flag |= sleepChore.smi.sm.isDisturbedByMovement.Get(sleepChore.smi);
		flag |= sleepChore.smi.sm.isDisturbedByNoise.Get(sleepChore.smi);
		flag |= sleepChore.smi.sm.isScaredOfDark.Get(sleepChore.smi);
		this.DreamIsDisturbed = flag;
		if (flag)
		{
			this.StopDreamingThought();
		}
	}

	// Token: 0x06001FFB RID: 8187 RVA: 0x000B7D24 File Offset: 0x000B5F24
	private void StartDreamingThought()
	{
		if (this.dreamer != null && !this.HasStartedThoughts_Dreaming)
		{
			this.dreamer.GetSMI<Dreamer.Instance>().SetDream(Db.Get().Dreams.CommonDream);
			this.dreamer.GetSMI<Dreamer.Instance>().StartDreaming();
			this.HasStartedThoughts_Dreaming = true;
		}
	}

	// Token: 0x06001FFC RID: 8188 RVA: 0x000B7D7D File Offset: 0x000B5F7D
	private void StopDreamingThought()
	{
		if (this.dreamer != null && this.HasStartedThoughts_Dreaming)
		{
			this.dreamer.GetSMI<Dreamer.Instance>().StopDreaming();
			this.HasStartedThoughts_Dreaming = false;
		}
	}

	// Token: 0x04001292 RID: 4754
	private static GameObject dreamJournalPrefab;

	// Token: 0x04001293 RID: 4755
	private static Effect sleepClinic;

	// Token: 0x04001294 RID: 4756
	public bool HasStartedThoughts_Dreaming;

	// Token: 0x04001296 RID: 4758
	private ChoreDriver dreamer;

	// Token: 0x04001297 RID: 4759
	private Equippable equippable;

	// Token: 0x04001298 RID: 4760
	private Effects effects;

	// Token: 0x04001299 RID: 4761
	private Sleepable sleepable;

	// Token: 0x0400129A RID: 4762
	private KSelectable selectable;

	// Token: 0x0400129B RID: 4763
	private HashedString dreamAnimName = "portal rocket comp";
}
