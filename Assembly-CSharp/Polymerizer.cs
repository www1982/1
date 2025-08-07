using System;
using KSerialization;
using UnityEngine;

// Token: 0x020007AA RID: 1962
[SerializationConfig(MemberSerialization.OptIn)]
public class Polymerizer : StateMachineComponent<Polymerizer.StatesInstance>
{
	// Token: 0x060033FA RID: 13306 RVA: 0x00123C34 File Offset: 0x00121E34
	protected override void OnSpawn()
	{
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		this.plasticMeter = new MeterController(component, "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new Vector3(0f, 0f, 0f), null);
		this.oilMeter = new MeterController(component, "meter2_target", "meter2", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new Vector3(0f, 0f, 0f), null);
		component.SetSymbolVisiblity("meter_target", true);
		this.UpdateOilMeter();
		base.smi.StartSM();
		base.Subscribe<Polymerizer>(-1697596308, Polymerizer.OnStorageChangedDelegate);
	}

	// Token: 0x060033FB RID: 13307 RVA: 0x00123CD8 File Offset: 0x00121ED8
	private void TryEmit()
	{
		GameObject gameObject = this.storage.FindFirst(this.emitTag);
		if (gameObject != null)
		{
			PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
			this.UpdatePercentDone(component);
			this.TryEmit(component);
		}
	}

	// Token: 0x060033FC RID: 13308 RVA: 0x00123D18 File Offset: 0x00121F18
	private void TryEmit(PrimaryElement primary_elem)
	{
		if (primary_elem.Mass >= this.emitMass)
		{
			this.plasticMeter.SetPositionPercent(0f);
			GameObject gameObject = this.storage.Drop(primary_elem.gameObject, true);
			Rotatable component = base.GetComponent<Rotatable>();
			Vector3 vector = component.transform.GetPosition() + component.GetRotatedOffset(this.emitOffset);
			int num = Grid.PosToCell(vector);
			if (Grid.Solid[num])
			{
				vector += component.GetRotatedOffset(Vector3.left);
			}
			gameObject.transform.SetPosition(vector);
			PrimaryElement primaryElement = this.storage.FindPrimaryElement(this.exhaustElement);
			if (primaryElement != null)
			{
				SimMessages.AddRemoveSubstance(Grid.PosToCell(vector), primaryElement.ElementID, null, primaryElement.Mass, primaryElement.Temperature, primaryElement.DiseaseIdx, primaryElement.DiseaseCount, true, -1);
				primaryElement.Mass = 0f;
				primaryElement.ModifyDiseaseCount(int.MinValue, "Polymerizer.Exhaust");
			}
		}
	}

	// Token: 0x060033FD RID: 13309 RVA: 0x00123E10 File Offset: 0x00122010
	private void UpdatePercentDone(PrimaryElement primary_elem)
	{
		float num = Mathf.Clamp01(primary_elem.Mass / this.emitMass);
		this.plasticMeter.SetPositionPercent(num);
	}

	// Token: 0x060033FE RID: 13310 RVA: 0x00123E3C File Offset: 0x0012203C
	private void OnStorageChanged(object data)
	{
		GameObject gameObject = (GameObject)data;
		if (gameObject == null)
		{
			return;
		}
		if (gameObject.HasTag(PolymerizerConfig.INPUT_ELEMENT_TAG))
		{
			this.UpdateOilMeter();
		}
	}

	// Token: 0x060033FF RID: 13311 RVA: 0x00123E70 File Offset: 0x00122070
	private void UpdateOilMeter()
	{
		float num = 0f;
		foreach (GameObject gameObject in this.storage.items)
		{
			if (gameObject.HasTag(PolymerizerConfig.INPUT_ELEMENT_TAG))
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				num += component.Mass;
			}
		}
		float num2 = Mathf.Clamp01(num / this.consumer.capacityKG);
		this.oilMeter.SetPositionPercent(num2);
	}

	// Token: 0x04001F48 RID: 8008
	[SerializeField]
	public float maxMass = 2.5f;

	// Token: 0x04001F49 RID: 8009
	[SerializeField]
	public float emitMass = 1f;

	// Token: 0x04001F4A RID: 8010
	[SerializeField]
	public Tag emitTag;

	// Token: 0x04001F4B RID: 8011
	[SerializeField]
	public Vector3 emitOffset = Vector3.zero;

	// Token: 0x04001F4C RID: 8012
	[SerializeField]
	public SimHashes exhaustElement = SimHashes.Vacuum;

	// Token: 0x04001F4D RID: 8013
	[MyCmpAdd]
	private Storage storage;

	// Token: 0x04001F4E RID: 8014
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001F4F RID: 8015
	[MyCmpGet]
	private ConduitConsumer consumer;

	// Token: 0x04001F50 RID: 8016
	[MyCmpGet]
	private ElementConverter converter;

	// Token: 0x04001F51 RID: 8017
	private MeterController plasticMeter;

	// Token: 0x04001F52 RID: 8018
	private MeterController oilMeter;

	// Token: 0x04001F53 RID: 8019
	private static readonly EventSystem.IntraObjectHandler<Polymerizer> OnStorageChangedDelegate = new EventSystem.IntraObjectHandler<Polymerizer>(delegate(Polymerizer component, object data)
	{
		component.OnStorageChanged(data);
	});

	// Token: 0x020016BF RID: 5823
	public class StatesInstance : GameStateMachine<Polymerizer.States, Polymerizer.StatesInstance, Polymerizer, object>.GameInstance
	{
		// Token: 0x06009666 RID: 38502 RVA: 0x00377EC5 File Offset: 0x003760C5
		public StatesInstance(Polymerizer smi)
			: base(smi)
		{
		}
	}

	// Token: 0x020016C0 RID: 5824
	public class States : GameStateMachine<Polymerizer.States, Polymerizer.StatesInstance, Polymerizer>
	{
		// Token: 0x06009667 RID: 38503 RVA: 0x00377ED0 File Offset: 0x003760D0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.off;
			this.root.EventTransition(GameHashes.OperationalChanged, this.off, (Polymerizer.StatesInstance smi) => !smi.master.operational.IsOperational);
			this.off.EventTransition(GameHashes.OperationalChanged, this.on, (Polymerizer.StatesInstance smi) => smi.master.operational.IsOperational);
			this.on.EventTransition(GameHashes.OnStorageChange, this.converting, (Polymerizer.StatesInstance smi) => smi.master.converter.CanConvertAtAll());
			this.converting.Enter("Ready", delegate(Polymerizer.StatesInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			}).EventHandler(GameHashes.OnStorageChange, delegate(Polymerizer.StatesInstance smi)
			{
				smi.master.TryEmit();
			}).EventTransition(GameHashes.OnStorageChange, this.on, (Polymerizer.StatesInstance smi) => !smi.master.converter.CanConvertAtAll())
				.Exit("Ready", delegate(Polymerizer.StatesInstance smi)
				{
					smi.master.operational.SetActive(false, false);
				});
		}

		// Token: 0x040073A6 RID: 29606
		public GameStateMachine<Polymerizer.States, Polymerizer.StatesInstance, Polymerizer, object>.State off;

		// Token: 0x040073A7 RID: 29607
		public GameStateMachine<Polymerizer.States, Polymerizer.StatesInstance, Polymerizer, object>.State on;

		// Token: 0x040073A8 RID: 29608
		public GameStateMachine<Polymerizer.States, Polymerizer.StatesInstance, Polymerizer, object>.State converting;
	}
}
