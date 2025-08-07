using System;
using KSerialization;
using UnityEngine;

// Token: 0x020006D5 RID: 1749
[SerializationConfig(MemberSerialization.OptIn)]
public class AlgaeDistillery : StateMachineComponent<AlgaeDistillery.StatesInstance>
{
	// Token: 0x06002B2A RID: 11050 RVA: 0x000F989A File Offset: 0x000F7A9A
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x0400197C RID: 6524
	[SerializeField]
	public Tag emitTag;

	// Token: 0x0400197D RID: 6525
	[SerializeField]
	public float emitMass;

	// Token: 0x0400197E RID: 6526
	[SerializeField]
	public Vector3 emitOffset;

	// Token: 0x0400197F RID: 6527
	[MyCmpAdd]
	private Storage storage;

	// Token: 0x04001980 RID: 6528
	[MyCmpGet]
	private ElementConverter emitter;

	// Token: 0x04001981 RID: 6529
	[MyCmpReq]
	private Operational operational;

	// Token: 0x02001558 RID: 5464
	public class StatesInstance : GameStateMachine<AlgaeDistillery.States, AlgaeDistillery.StatesInstance, AlgaeDistillery, object>.GameInstance
	{
		// Token: 0x060090EB RID: 37099 RVA: 0x00361D84 File Offset: 0x0035FF84
		public StatesInstance(AlgaeDistillery smi)
			: base(smi)
		{
		}

		// Token: 0x060090EC RID: 37100 RVA: 0x00361D90 File Offset: 0x0035FF90
		public void TryEmit()
		{
			Storage storage = base.smi.master.storage;
			GameObject gameObject = storage.FindFirst(base.smi.master.emitTag);
			if (gameObject != null && gameObject.GetComponent<PrimaryElement>().Mass >= base.master.emitMass)
			{
				storage.Drop(gameObject, true).transform.SetPosition(base.transform.GetPosition() + base.master.emitOffset);
			}
		}
	}

	// Token: 0x02001559 RID: 5465
	public class States : GameStateMachine<AlgaeDistillery.States, AlgaeDistillery.StatesInstance, AlgaeDistillery>
	{
		// Token: 0x060090ED RID: 37101 RVA: 0x00361E14 File Offset: 0x00360014
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.disabled;
			this.root.EventTransition(GameHashes.OperationalChanged, this.disabled, (AlgaeDistillery.StatesInstance smi) => !smi.master.operational.IsOperational);
			this.disabled.EventTransition(GameHashes.OperationalChanged, this.waiting, (AlgaeDistillery.StatesInstance smi) => smi.master.operational.IsOperational);
			this.waiting.Enter("Waiting", delegate(AlgaeDistillery.StatesInstance smi)
			{
				smi.master.operational.SetActive(false, false);
			}).EventTransition(GameHashes.OnStorageChange, this.converting, (AlgaeDistillery.StatesInstance smi) => smi.master.GetComponent<ElementConverter>().HasEnoughMassToStartConverting(false));
			this.converting.Enter("Ready", delegate(AlgaeDistillery.StatesInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			}).Transition(this.waiting, (AlgaeDistillery.StatesInstance smi) => !smi.master.GetComponent<ElementConverter>().CanConvertAtAll(), UpdateRate.SIM_200ms).EventHandler(GameHashes.OnStorageChange, delegate(AlgaeDistillery.StatesInstance smi)
			{
				smi.TryEmit();
			});
		}

		// Token: 0x04006F69 RID: 28521
		public GameStateMachine<AlgaeDistillery.States, AlgaeDistillery.StatesInstance, AlgaeDistillery, object>.State disabled;

		// Token: 0x04006F6A RID: 28522
		public GameStateMachine<AlgaeDistillery.States, AlgaeDistillery.StatesInstance, AlgaeDistillery, object>.State waiting;

		// Token: 0x04006F6B RID: 28523
		public GameStateMachine<AlgaeDistillery.States, AlgaeDistillery.StatesInstance, AlgaeDistillery, object>.State converting;

		// Token: 0x04006F6C RID: 28524
		public GameStateMachine<AlgaeDistillery.States, AlgaeDistillery.StatesInstance, AlgaeDistillery, object>.State overpressure;
	}
}
