using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000721 RID: 1825
[SerializationConfig(MemberSerialization.OptIn)]
public class Electrolyzer : StateMachineComponent<Electrolyzer.StatesInstance>
{
	// Token: 0x06002DFB RID: 11771 RVA: 0x00107BC8 File Offset: 0x00105DC8
	protected override void OnSpawn()
	{
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		if (this.hasMeter)
		{
			this.meter = new MeterController(component, "U2H_meter_target", "meter", Meter.Offset.Behind, Grid.SceneLayer.NoLayer, new Vector3(-0.4f, 0.5f, -0.1f), new string[] { "U2H_meter_target", "U2H_meter_tank", "U2H_meter_waterbody", "U2H_meter_level" });
		}
		base.smi.StartSM();
		this.UpdateMeter();
		Tutorial.Instance.oxygenGenerators.Add(base.gameObject);
	}

	// Token: 0x06002DFC RID: 11772 RVA: 0x00107C5D File Offset: 0x00105E5D
	protected override void OnCleanUp()
	{
		Tutorial.Instance.oxygenGenerators.Remove(base.gameObject);
		base.OnCleanUp();
	}

	// Token: 0x06002DFD RID: 11773 RVA: 0x00107C7C File Offset: 0x00105E7C
	public void UpdateMeter()
	{
		if (this.hasMeter)
		{
			float num = Mathf.Clamp01(this.storage.MassStored() / this.storage.capacityKg);
			this.meter.SetPositionPercent(num);
		}
	}

	// Token: 0x1700027A RID: 634
	// (get) Token: 0x06002DFE RID: 11774 RVA: 0x00107CBC File Offset: 0x00105EBC
	private bool RoomForPressure
	{
		get
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			num = Grid.OffsetCell(num, this.emissionOffset);
			return !GameUtil.FloodFillCheck<Electrolyzer>(new Func<int, Electrolyzer, bool>(Electrolyzer.OverPressure), this, num, 3, true, true);
		}
	}

	// Token: 0x06002DFF RID: 11775 RVA: 0x00107D00 File Offset: 0x00105F00
	private static bool OverPressure(int cell, Electrolyzer electrolyzer)
	{
		return Grid.Mass[cell] > electrolyzer.maxMass;
	}

	// Token: 0x04001B1B RID: 6939
	[SerializeField]
	public float maxMass = 2.5f;

	// Token: 0x04001B1C RID: 6940
	[SerializeField]
	public bool hasMeter = true;

	// Token: 0x04001B1D RID: 6941
	[SerializeField]
	public CellOffset emissionOffset = CellOffset.none;

	// Token: 0x04001B1E RID: 6942
	[MyCmpAdd]
	private Storage storage;

	// Token: 0x04001B1F RID: 6943
	[MyCmpGet]
	private ElementConverter emitter;

	// Token: 0x04001B20 RID: 6944
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001B21 RID: 6945
	private MeterController meter;

	// Token: 0x020015C6 RID: 5574
	public class StatesInstance : GameStateMachine<Electrolyzer.States, Electrolyzer.StatesInstance, Electrolyzer, object>.GameInstance
	{
		// Token: 0x060092AB RID: 37547 RVA: 0x00367732 File Offset: 0x00365932
		public StatesInstance(Electrolyzer smi)
			: base(smi)
		{
		}
	}

	// Token: 0x020015C7 RID: 5575
	public class States : GameStateMachine<Electrolyzer.States, Electrolyzer.StatesInstance, Electrolyzer>
	{
		// Token: 0x060092AC RID: 37548 RVA: 0x0036773C File Offset: 0x0036593C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.disabled;
			this.root.EventTransition(GameHashes.OperationalChanged, this.disabled, (Electrolyzer.StatesInstance smi) => !smi.master.operational.IsOperational).EventHandler(GameHashes.OnStorageChange, delegate(Electrolyzer.StatesInstance smi)
			{
				smi.master.UpdateMeter();
			});
			this.disabled.EventTransition(GameHashes.OperationalChanged, this.waiting, (Electrolyzer.StatesInstance smi) => smi.master.operational.IsOperational);
			this.waiting.Enter("Waiting", delegate(Electrolyzer.StatesInstance smi)
			{
				smi.master.operational.SetActive(false, false);
			}).EventTransition(GameHashes.OnStorageChange, this.converting, (Electrolyzer.StatesInstance smi) => smi.master.GetComponent<ElementConverter>().HasEnoughMassToStartConverting(false));
			this.converting.Enter("Ready", delegate(Electrolyzer.StatesInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			}).Transition(this.waiting, (Electrolyzer.StatesInstance smi) => !smi.master.GetComponent<ElementConverter>().CanConvertAtAll(), UpdateRate.SIM_200ms).Transition(this.overpressure, (Electrolyzer.StatesInstance smi) => !smi.master.RoomForPressure, UpdateRate.SIM_200ms);
			this.overpressure.Enter("OverPressure", delegate(Electrolyzer.StatesInstance smi)
			{
				smi.master.operational.SetActive(false, false);
			}).ToggleStatusItem(Db.Get().BuildingStatusItems.PressureOk, null).Transition(this.converting, (Electrolyzer.StatesInstance smi) => smi.master.RoomForPressure, UpdateRate.SIM_200ms);
		}

		// Token: 0x040070D4 RID: 28884
		public GameStateMachine<Electrolyzer.States, Electrolyzer.StatesInstance, Electrolyzer, object>.State disabled;

		// Token: 0x040070D5 RID: 28885
		public GameStateMachine<Electrolyzer.States, Electrolyzer.StatesInstance, Electrolyzer, object>.State waiting;

		// Token: 0x040070D6 RID: 28886
		public GameStateMachine<Electrolyzer.States, Electrolyzer.StatesInstance, Electrolyzer, object>.State converting;

		// Token: 0x040070D7 RID: 28887
		public GameStateMachine<Electrolyzer.States, Electrolyzer.StatesInstance, Electrolyzer, object>.State overpressure;
	}
}
