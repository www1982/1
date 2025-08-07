using System;
using UnityEngine;

// Token: 0x02000A87 RID: 2695
public class RadiationLight : StateMachineComponent<RadiationLight.StatesInstance>
{
	// Token: 0x06004E3A RID: 20026 RVA: 0x001C5218 File Offset: 0x001C3418
	public void UpdateMeter()
	{
		this.meter.SetPositionPercent(Mathf.Clamp01(this.storage.MassStored() / this.storage.capacityKg));
	}

	// Token: 0x06004E3B RID: 20027 RVA: 0x001C5241 File Offset: 0x001C3441
	public bool HasEnoughFuel()
	{
		return this.elementConverter.HasEnoughMassToStartConverting(false);
	}

	// Token: 0x06004E3C RID: 20028 RVA: 0x001C524F File Offset: 0x001C344F
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		this.UpdateMeter();
	}

	// Token: 0x040033F4 RID: 13300
	[MyCmpGet]
	private Operational operational;

	// Token: 0x040033F5 RID: 13301
	[MyCmpGet]
	private Storage storage;

	// Token: 0x040033F6 RID: 13302
	[MyCmpGet]
	private RadiationEmitter emitter;

	// Token: 0x040033F7 RID: 13303
	[MyCmpGet]
	private ElementConverter elementConverter;

	// Token: 0x040033F8 RID: 13304
	private MeterController meter;

	// Token: 0x040033F9 RID: 13305
	public Tag elementToConsume;

	// Token: 0x040033FA RID: 13306
	public float consumptionRate;

	// Token: 0x02001B6D RID: 7021
	public class StatesInstance : GameStateMachine<RadiationLight.States, RadiationLight.StatesInstance, RadiationLight, object>.GameInstance
	{
		// Token: 0x0600A787 RID: 42887 RVA: 0x003B1324 File Offset: 0x003AF524
		public StatesInstance(RadiationLight smi)
			: base(smi)
		{
			if (base.GetComponent<Rotatable>().IsRotated)
			{
				RadiationEmitter component = base.GetComponent<RadiationEmitter>();
				component.emitDirection = 180f;
				component.emissionOffset = Vector3.left;
			}
			this.ToggleEmitter(false);
			smi.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_target" });
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Radiation, true);
		}

		// Token: 0x0600A788 RID: 42888 RVA: 0x003B13A1 File Offset: 0x003AF5A1
		public void ToggleEmitter(bool on)
		{
			base.smi.master.operational.SetActive(on, false);
			base.smi.master.emitter.SetEmitting(on);
		}
	}

	// Token: 0x02001B6E RID: 7022
	public class States : GameStateMachine<RadiationLight.States, RadiationLight.StatesInstance, RadiationLight>
	{
		// Token: 0x0600A789 RID: 42889 RVA: 0x003B13D0 File Offset: 0x003AF5D0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.ready.idle;
			this.root.EventHandler(GameHashes.OnStorageChange, delegate(RadiationLight.StatesInstance smi)
			{
				smi.master.UpdateMeter();
			});
			this.waiting.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.ready.idle, (RadiationLight.StatesInstance smi) => smi.master.operational.IsOperational);
			this.ready.EventTransition(GameHashes.OperationalChanged, this.waiting, (RadiationLight.StatesInstance smi) => !smi.master.operational.IsOperational).DefaultState(this.ready.idle);
			this.ready.idle.PlayAnim("off").EventTransition(GameHashes.OnStorageChange, this.ready.on, (RadiationLight.StatesInstance smi) => smi.master.HasEnoughFuel());
			this.ready.on.PlayAnim("on").Enter(delegate(RadiationLight.StatesInstance smi)
			{
				smi.ToggleEmitter(true);
			}).EventTransition(GameHashes.OnStorageChange, this.ready.idle, (RadiationLight.StatesInstance smi) => !smi.master.HasEnoughFuel())
				.Exit(delegate(RadiationLight.StatesInstance smi)
				{
					smi.ToggleEmitter(false);
				});
		}

		// Token: 0x040082E8 RID: 33512
		public GameStateMachine<RadiationLight.States, RadiationLight.StatesInstance, RadiationLight, object>.State waiting;

		// Token: 0x040082E9 RID: 33513
		public RadiationLight.States.ReadyStates ready;

		// Token: 0x02002896 RID: 10390
		public class ReadyStates : GameStateMachine<RadiationLight.States, RadiationLight.StatesInstance, RadiationLight, object>.State
		{
			// Token: 0x0400B3FF RID: 46079
			public GameStateMachine<RadiationLight.States, RadiationLight.StatesInstance, RadiationLight, object>.State idle;

			// Token: 0x0400B400 RID: 46080
			public GameStateMachine<RadiationLight.States, RadiationLight.StatesInstance, RadiationLight, object>.State on;
		}
	}
}
