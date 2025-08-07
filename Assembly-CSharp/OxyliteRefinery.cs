using System;
using KSerialization;
using UnityEngine;

// Token: 0x020007A1 RID: 1953
[SerializationConfig(MemberSerialization.OptIn)]
public class OxyliteRefinery : StateMachineComponent<OxyliteRefinery.StatesInstance>
{
	// Token: 0x060033BC RID: 13244 RVA: 0x00122815 File Offset: 0x00120A15
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x04001F1A RID: 7962
	[MyCmpAdd]
	private Storage storage;

	// Token: 0x04001F1B RID: 7963
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001F1C RID: 7964
	public Tag emitTag;

	// Token: 0x04001F1D RID: 7965
	public float emitMass;

	// Token: 0x04001F1E RID: 7966
	public Vector3 dropOffset;

	// Token: 0x020016B3 RID: 5811
	public class StatesInstance : GameStateMachine<OxyliteRefinery.States, OxyliteRefinery.StatesInstance, OxyliteRefinery, object>.GameInstance
	{
		// Token: 0x0600964C RID: 38476 RVA: 0x00377B44 File Offset: 0x00375D44
		public StatesInstance(OxyliteRefinery smi)
			: base(smi)
		{
		}

		// Token: 0x0600964D RID: 38477 RVA: 0x00377B50 File Offset: 0x00375D50
		public void TryEmit()
		{
			Storage storage = base.smi.master.storage;
			GameObject gameObject = storage.FindFirst(base.smi.master.emitTag);
			if (gameObject != null && gameObject.GetComponent<PrimaryElement>().Mass >= base.master.emitMass)
			{
				Vector3 vector = base.transform.GetPosition() + base.master.dropOffset;
				vector.z = Grid.GetLayerZ(Grid.SceneLayer.Ore);
				gameObject.transform.SetPosition(vector);
				storage.Drop(gameObject, true);
			}
		}
	}

	// Token: 0x020016B4 RID: 5812
	public class States : GameStateMachine<OxyliteRefinery.States, OxyliteRefinery.StatesInstance, OxyliteRefinery>
	{
		// Token: 0x0600964E RID: 38478 RVA: 0x00377BE8 File Offset: 0x00375DE8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.disabled;
			this.root.EventTransition(GameHashes.OperationalChanged, this.disabled, (OxyliteRefinery.StatesInstance smi) => !smi.master.operational.IsOperational);
			this.disabled.EventTransition(GameHashes.OperationalChanged, this.waiting, (OxyliteRefinery.StatesInstance smi) => smi.master.operational.IsOperational);
			this.waiting.EventTransition(GameHashes.OnStorageChange, this.converting, (OxyliteRefinery.StatesInstance smi) => smi.master.GetComponent<ElementConverter>().HasEnoughMassToStartConverting(false));
			this.converting.Enter(delegate(OxyliteRefinery.StatesInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			}).Exit(delegate(OxyliteRefinery.StatesInstance smi)
			{
				smi.master.operational.SetActive(false, false);
			}).Transition(this.waiting, (OxyliteRefinery.StatesInstance smi) => !smi.master.GetComponent<ElementConverter>().CanConvertAtAll(), UpdateRate.SIM_200ms)
				.EventHandler(GameHashes.OnStorageChange, delegate(OxyliteRefinery.StatesInstance smi)
				{
					smi.TryEmit();
				});
		}

		// Token: 0x04007396 RID: 29590
		public GameStateMachine<OxyliteRefinery.States, OxyliteRefinery.StatesInstance, OxyliteRefinery, object>.State disabled;

		// Token: 0x04007397 RID: 29591
		public GameStateMachine<OxyliteRefinery.States, OxyliteRefinery.StatesInstance, OxyliteRefinery, object>.State waiting;

		// Token: 0x04007398 RID: 29592
		public GameStateMachine<OxyliteRefinery.States, OxyliteRefinery.StatesInstance, OxyliteRefinery, object>.State converting;
	}
}
