using System;
using KSerialization;
using TUNING;
using UnityEngine;

// Token: 0x0200079B RID: 1947
[SerializationConfig(MemberSerialization.OptIn)]
public class OilRefinery : StateMachineComponent<OilRefinery.StatesInstance>
{
	// Token: 0x0600337B RID: 13179 RVA: 0x00121B30 File Offset: 0x0011FD30
	protected override void OnSpawn()
	{
		base.Subscribe<OilRefinery>(-1697596308, OilRefinery.OnStorageChangedDelegate);
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		this.meter = new MeterController(component, "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Vector3.zero, null);
		base.smi.StartSM();
		this.maxSrcMass = base.GetComponent<ConduitConsumer>().capacityKG;
	}

	// Token: 0x0600337C RID: 13180 RVA: 0x00121B90 File Offset: 0x0011FD90
	private void OnStorageChanged(object data)
	{
		float num = Mathf.Clamp01(this.storage.GetMassAvailable(SimHashes.CrudeOil) / this.maxSrcMass);
		this.meter.SetPositionPercent(num);
	}

	// Token: 0x0600337D RID: 13181 RVA: 0x00121BC8 File Offset: 0x0011FDC8
	private static bool UpdateStateCb(int cell, object data)
	{
		OilRefinery oilRefinery = data as OilRefinery;
		if (Grid.Element[cell].IsGas)
		{
			oilRefinery.cellCount += 1f;
			oilRefinery.envPressure += Grid.Mass[cell];
		}
		return true;
	}

	// Token: 0x0600337E RID: 13182 RVA: 0x00121C18 File Offset: 0x0011FE18
	private void TestAreaPressure()
	{
		this.envPressure = 0f;
		this.cellCount = 0f;
		if (this.occupyArea != null && base.gameObject != null)
		{
			this.occupyArea.TestArea(Grid.PosToCell(base.gameObject), this, new Func<int, object, bool>(OilRefinery.UpdateStateCb));
			this.envPressure /= this.cellCount;
		}
	}

	// Token: 0x0600337F RID: 13183 RVA: 0x00121C8E File Offset: 0x0011FE8E
	private bool IsOverPressure()
	{
		return this.envPressure >= this.overpressureMass;
	}

	// Token: 0x06003380 RID: 13184 RVA: 0x00121CA1 File Offset: 0x0011FEA1
	private bool IsOverWarningPressure()
	{
		return this.envPressure >= this.overpressureWarningMass;
	}

	// Token: 0x04001EEE RID: 7918
	private bool wasOverPressure;

	// Token: 0x04001EEF RID: 7919
	[SerializeField]
	public float overpressureWarningMass = 4.5f;

	// Token: 0x04001EF0 RID: 7920
	[SerializeField]
	public float overpressureMass = 5f;

	// Token: 0x04001EF1 RID: 7921
	private float maxSrcMass;

	// Token: 0x04001EF2 RID: 7922
	private float envPressure;

	// Token: 0x04001EF3 RID: 7923
	private float cellCount;

	// Token: 0x04001EF4 RID: 7924
	[MyCmpGet]
	private Storage storage;

	// Token: 0x04001EF5 RID: 7925
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001EF6 RID: 7926
	[MyCmpAdd]
	private OilRefinery.WorkableTarget workable;

	// Token: 0x04001EF7 RID: 7927
	[MyCmpReq]
	private OccupyArea occupyArea;

	// Token: 0x04001EF8 RID: 7928
	[MyCmpAdd]
	private ManuallySetRemoteWorkTargetComponent remoteChore;

	// Token: 0x04001EF9 RID: 7929
	private const bool hasMeter = true;

	// Token: 0x04001EFA RID: 7930
	private MeterController meter;

	// Token: 0x04001EFB RID: 7931
	private static readonly EventSystem.IntraObjectHandler<OilRefinery> OnStorageChangedDelegate = new EventSystem.IntraObjectHandler<OilRefinery>(delegate(OilRefinery component, object data)
	{
		component.OnStorageChanged(data);
	});

	// Token: 0x020016A5 RID: 5797
	public class StatesInstance : GameStateMachine<OilRefinery.States, OilRefinery.StatesInstance, OilRefinery, object>.GameInstance
	{
		// Token: 0x0600961B RID: 38427 RVA: 0x00376DE7 File Offset: 0x00374FE7
		public StatesInstance(OilRefinery smi)
			: base(smi)
		{
		}

		// Token: 0x0600961C RID: 38428 RVA: 0x00376DF0 File Offset: 0x00374FF0
		public void TestAreaPressure()
		{
			base.smi.master.TestAreaPressure();
			bool flag = base.smi.master.IsOverPressure();
			bool flag2 = base.smi.master.IsOverWarningPressure();
			if (flag)
			{
				base.smi.master.wasOverPressure = true;
				base.sm.isOverPressure.Set(true, this, false);
				return;
			}
			if (base.smi.master.wasOverPressure && !flag2)
			{
				base.sm.isOverPressure.Set(false, this, false);
			}
		}
	}

	// Token: 0x020016A6 RID: 5798
	public class States : GameStateMachine<OilRefinery.States, OilRefinery.StatesInstance, OilRefinery>
	{
		// Token: 0x0600961D RID: 38429 RVA: 0x00376E80 File Offset: 0x00375080
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.disabled;
			this.root.EventTransition(GameHashes.OperationalChanged, this.disabled, (OilRefinery.StatesInstance smi) => !smi.master.operational.IsOperational);
			this.disabled.EventTransition(GameHashes.OperationalChanged, this.needResources, (OilRefinery.StatesInstance smi) => smi.master.operational.IsOperational);
			this.needResources.EventTransition(GameHashes.OnStorageChange, this.ready, (OilRefinery.StatesInstance smi) => smi.master.GetComponent<ElementConverter>().HasEnoughMassToStartConverting(false));
			this.ready.Update("Test Pressure Update", delegate(OilRefinery.StatesInstance smi, float dt)
			{
				smi.TestAreaPressure();
			}, UpdateRate.SIM_1000ms, false).ParamTransition<bool>(this.isOverPressure, this.overpressure, GameStateMachine<OilRefinery.States, OilRefinery.StatesInstance, OilRefinery, object>.IsTrue).Transition(this.needResources, (OilRefinery.StatesInstance smi) => !smi.master.GetComponent<ElementConverter>().HasEnoughMassToStartConverting(false), UpdateRate.SIM_200ms)
				.ToggleChore((OilRefinery.StatesInstance smi) => new WorkChore<OilRefinery.WorkableTarget>(Db.Get().ChoreTypes.Fabricate, smi.master.workable, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true), new Action<OilRefinery.StatesInstance, Chore>(OilRefinery.States.SetRemoteChore), this.needResources);
			this.overpressure.Update("Test Pressure Update", delegate(OilRefinery.StatesInstance smi, float dt)
			{
				smi.TestAreaPressure();
			}, UpdateRate.SIM_1000ms, false).ParamTransition<bool>(this.isOverPressure, this.ready, GameStateMachine<OilRefinery.States, OilRefinery.StatesInstance, OilRefinery, object>.IsFalse).ToggleStatusItem(Db.Get().BuildingStatusItems.PressureOk, null);
		}

		// Token: 0x0600961E RID: 38430 RVA: 0x0037703D File Offset: 0x0037523D
		private static void SetRemoteChore(OilRefinery.StatesInstance smi, Chore chore)
		{
			smi.master.remoteChore.SetChore(chore);
		}

		// Token: 0x04007380 RID: 29568
		public StateMachine<OilRefinery.States, OilRefinery.StatesInstance, OilRefinery, object>.BoolParameter isOverPressure;

		// Token: 0x04007381 RID: 29569
		public StateMachine<OilRefinery.States, OilRefinery.StatesInstance, OilRefinery, object>.BoolParameter isOverPressureWarning;

		// Token: 0x04007382 RID: 29570
		public GameStateMachine<OilRefinery.States, OilRefinery.StatesInstance, OilRefinery, object>.State disabled;

		// Token: 0x04007383 RID: 29571
		public GameStateMachine<OilRefinery.States, OilRefinery.StatesInstance, OilRefinery, object>.State overpressure;

		// Token: 0x04007384 RID: 29572
		public GameStateMachine<OilRefinery.States, OilRefinery.StatesInstance, OilRefinery, object>.State needResources;

		// Token: 0x04007385 RID: 29573
		public GameStateMachine<OilRefinery.States, OilRefinery.StatesInstance, OilRefinery, object>.State ready;
	}

	// Token: 0x020016A7 RID: 5799
	[AddComponentMenu("KMonoBehaviour/Workable/WorkableTarget")]
	public class WorkableTarget : Workable
	{
		// Token: 0x06009620 RID: 38432 RVA: 0x00377058 File Offset: 0x00375258
		protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
			this.showProgressBar = false;
			this.workerStatusItem = null;
			this.skillExperienceSkillGroup = Db.Get().SkillGroups.Technicals.Id;
			this.skillExperienceMultiplier = SKILLS.MOST_DAY_EXPERIENCE;
			this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_oilrefinery_kanim") };
		}

		// Token: 0x06009621 RID: 38433 RVA: 0x003770BC File Offset: 0x003752BC
		protected override void OnSpawn()
		{
			base.OnSpawn();
			base.SetWorkTime(float.PositiveInfinity);
		}

		// Token: 0x06009622 RID: 38434 RVA: 0x003770CF File Offset: 0x003752CF
		protected override void OnStartWork(WorkerBase worker)
		{
			this.operational.SetActive(true, false);
		}

		// Token: 0x06009623 RID: 38435 RVA: 0x003770DE File Offset: 0x003752DE
		protected override void OnStopWork(WorkerBase worker)
		{
			this.operational.SetActive(false, false);
		}

		// Token: 0x06009624 RID: 38436 RVA: 0x003770ED File Offset: 0x003752ED
		protected override void OnCompleteWork(WorkerBase worker)
		{
			this.operational.SetActive(false, false);
		}

		// Token: 0x06009625 RID: 38437 RVA: 0x003770FC File Offset: 0x003752FC
		public override bool InstantlyFinish(WorkerBase worker)
		{
			return false;
		}

		// Token: 0x04007386 RID: 29574
		[MyCmpGet]
		public Operational operational;
	}
}
