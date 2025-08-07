using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006F4 RID: 1780
public class Compost : StateMachineComponent<Compost.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x06002C8D RID: 11405 RVA: 0x00101341 File Offset: 0x000FF541
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<Compost>(-1697596308, Compost.OnStorageChangedDelegate);
	}

	// Token: 0x06002C8E RID: 11406 RVA: 0x0010135C File Offset: 0x000FF55C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.GetComponent<ManualDeliveryKG>().ShowStatusItem = false;
		this.temperatureAdjuster = new SimulatedTemperatureAdjuster(this.simulatedInternalTemperature, this.simulatedInternalHeatCapacity, this.simulatedThermalConductivity, base.GetComponent<Storage>());
		base.smi.StartSM();
	}

	// Token: 0x06002C8F RID: 11407 RVA: 0x001013A9 File Offset: 0x000FF5A9
	protected override void OnCleanUp()
	{
		this.temperatureAdjuster.CleanUp();
	}

	// Token: 0x06002C90 RID: 11408 RVA: 0x001013B6 File Offset: 0x000FF5B6
	private void OnStorageChanged(object data)
	{
		(GameObject)data == null;
	}

	// Token: 0x06002C91 RID: 11409 RVA: 0x001013C5 File Offset: 0x000FF5C5
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return SimulatedTemperatureAdjuster.GetDescriptors(this.simulatedInternalTemperature);
	}

	// Token: 0x04001A51 RID: 6737
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04001A52 RID: 6738
	[MyCmpGet]
	private Storage storage;

	// Token: 0x04001A53 RID: 6739
	[MyCmpAdd]
	private ManuallySetRemoteWorkTargetComponent remoteChore;

	// Token: 0x04001A54 RID: 6740
	[SerializeField]
	public float flipInterval = 600f;

	// Token: 0x04001A55 RID: 6741
	[SerializeField]
	public float simulatedInternalTemperature = 323.15f;

	// Token: 0x04001A56 RID: 6742
	[SerializeField]
	public float simulatedInternalHeatCapacity = 400f;

	// Token: 0x04001A57 RID: 6743
	[SerializeField]
	public float simulatedThermalConductivity = 1000f;

	// Token: 0x04001A58 RID: 6744
	private SimulatedTemperatureAdjuster temperatureAdjuster;

	// Token: 0x04001A59 RID: 6745
	private static readonly EventSystem.IntraObjectHandler<Compost> OnStorageChangedDelegate = new EventSystem.IntraObjectHandler<Compost>(delegate(Compost component, object data)
	{
		component.OnStorageChanged(data);
	});

	// Token: 0x02001592 RID: 5522
	public class StatesInstance : GameStateMachine<Compost.States, Compost.StatesInstance, Compost, object>.GameInstance
	{
		// Token: 0x060091F6 RID: 37366 RVA: 0x003651CE File Offset: 0x003633CE
		public StatesInstance(Compost master)
			: base(master)
		{
		}

		// Token: 0x060091F7 RID: 37367 RVA: 0x003651D7 File Offset: 0x003633D7
		public bool CanStartConverting()
		{
			return base.master.GetComponent<ElementConverter>().HasEnoughMassToStartConverting(false);
		}

		// Token: 0x060091F8 RID: 37368 RVA: 0x003651EA File Offset: 0x003633EA
		public bool CanContinueConverting()
		{
			return base.master.GetComponent<ElementConverter>().CanConvertAtAll();
		}

		// Token: 0x060091F9 RID: 37369 RVA: 0x003651FC File Offset: 0x003633FC
		public bool IsEmpty()
		{
			return base.master.storage.IsEmpty();
		}

		// Token: 0x060091FA RID: 37370 RVA: 0x0036520E File Offset: 0x0036340E
		public void ResetWorkable()
		{
			CompostWorkable component = base.master.GetComponent<CompostWorkable>();
			component.ShowProgressBar(false);
			component.WorkTimeRemaining = component.GetWorkTime();
		}
	}

	// Token: 0x02001593 RID: 5523
	public class States : GameStateMachine<Compost.States, Compost.StatesInstance, Compost>
	{
		// Token: 0x060091FB RID: 37371 RVA: 0x00365230 File Offset: 0x00363430
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.empty;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.empty.Enter("empty", delegate(Compost.StatesInstance smi)
			{
				smi.ResetWorkable();
			}).EventTransition(GameHashes.OnStorageChange, this.insufficientMass, (Compost.StatesInstance smi) => !smi.IsEmpty()).EventTransition(GameHashes.OperationalChanged, this.disabledEmpty, (Compost.StatesInstance smi) => !smi.GetComponent<Operational>().IsOperational)
				.ToggleStatusItem(Db.Get().BuildingStatusItems.AwaitingWaste, null)
				.PlayAnim("off");
			this.insufficientMass.Enter("empty", delegate(Compost.StatesInstance smi)
			{
				smi.ResetWorkable();
			}).EventTransition(GameHashes.OnStorageChange, this.empty, (Compost.StatesInstance smi) => smi.IsEmpty()).EventTransition(GameHashes.OnStorageChange, this.inert, (Compost.StatesInstance smi) => smi.CanStartConverting())
				.ToggleStatusItem(Db.Get().BuildingStatusItems.AwaitingWaste, null)
				.PlayAnim("idle_half");
			this.inert.EventTransition(GameHashes.OperationalChanged, this.disabled, (Compost.StatesInstance smi) => !smi.GetComponent<Operational>().IsOperational).PlayAnim("on").ToggleStatusItem(Db.Get().BuildingStatusItems.AwaitingCompostFlip, null)
				.ToggleChore(new Func<Compost.StatesInstance, Chore>(Compost.States.CreateFlipChore), new Action<Compost.StatesInstance, Chore>(Compost.States.SetRemoteChore), this.composting);
			this.composting.Enter("Composting", delegate(Compost.StatesInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			}).EventTransition(GameHashes.OnStorageChange, this.empty, (Compost.StatesInstance smi) => !smi.CanContinueConverting()).EventTransition(GameHashes.OperationalChanged, this.disabled, (Compost.StatesInstance smi) => !smi.GetComponent<Operational>().IsOperational)
				.ScheduleGoTo((Compost.StatesInstance smi) => smi.master.flipInterval, this.inert)
				.Exit(delegate(Compost.StatesInstance smi)
				{
					smi.master.operational.SetActive(false, false);
				});
			this.disabled.Enter("disabledEmpty", delegate(Compost.StatesInstance smi)
			{
				smi.ResetWorkable();
			}).PlayAnim("on").EventTransition(GameHashes.OperationalChanged, this.inert, (Compost.StatesInstance smi) => smi.GetComponent<Operational>().IsOperational);
			this.disabledEmpty.Enter("disabledEmpty", delegate(Compost.StatesInstance smi)
			{
				smi.ResetWorkable();
			}).PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.empty, (Compost.StatesInstance smi) => smi.GetComponent<Operational>().IsOperational);
		}

		// Token: 0x060091FC RID: 37372 RVA: 0x003655CC File Offset: 0x003637CC
		private static void SetRemoteChore(Compost.StatesInstance smi, Chore chore)
		{
			smi.master.remoteChore.SetChore(chore);
		}

		// Token: 0x060091FD RID: 37373 RVA: 0x003655E0 File Offset: 0x003637E0
		private static Chore CreateFlipChore(Compost.StatesInstance smi)
		{
			return new WorkChore<CompostWorkable>(Db.Get().ChoreTypes.FlipCompost, smi.master, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		}

		// Token: 0x04007047 RID: 28743
		public GameStateMachine<Compost.States, Compost.StatesInstance, Compost, object>.State empty;

		// Token: 0x04007048 RID: 28744
		public GameStateMachine<Compost.States, Compost.StatesInstance, Compost, object>.State insufficientMass;

		// Token: 0x04007049 RID: 28745
		public GameStateMachine<Compost.States, Compost.StatesInstance, Compost, object>.State disabled;

		// Token: 0x0400704A RID: 28746
		public GameStateMachine<Compost.States, Compost.StatesInstance, Compost, object>.State disabledEmpty;

		// Token: 0x0400704B RID: 28747
		public GameStateMachine<Compost.States, Compost.StatesInstance, Compost, object>.State inert;

		// Token: 0x0400704C RID: 28748
		public GameStateMachine<Compost.States, Compost.StatesInstance, Compost, object>.State composting;
	}
}
