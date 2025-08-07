using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x0200069C RID: 1692
[SerializationConfig(MemberSerialization.OptIn)]
public class AirFilter : StateMachineComponent<AirFilter.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x06002941 RID: 10561 RVA: 0x000EFEBF File Offset: 0x000EE0BF
	public bool HasFilter()
	{
		return this.elementConverter.HasEnoughMass(this.filterTag, false);
	}

	// Token: 0x06002942 RID: 10562 RVA: 0x000EFED3 File Offset: 0x000EE0D3
	public bool IsConvertable()
	{
		return this.elementConverter.HasEnoughMassToStartConverting(false);
	}

	// Token: 0x06002943 RID: 10563 RVA: 0x000EFEE1 File Offset: 0x000EE0E1
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06002944 RID: 10564 RVA: 0x000EFEF4 File Offset: 0x000EE0F4
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return null;
	}

	// Token: 0x04001861 RID: 6241
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04001862 RID: 6242
	[MyCmpGet]
	private Storage storage;

	// Token: 0x04001863 RID: 6243
	[MyCmpGet]
	private ElementConverter elementConverter;

	// Token: 0x04001864 RID: 6244
	[MyCmpGet]
	private ElementConsumer elementConsumer;

	// Token: 0x04001865 RID: 6245
	public Tag filterTag;

	// Token: 0x0200151E RID: 5406
	public class StatesInstance : GameStateMachine<AirFilter.States, AirFilter.StatesInstance, AirFilter, object>.GameInstance
	{
		// Token: 0x0600900C RID: 36876 RVA: 0x0035EF5A File Offset: 0x0035D15A
		public StatesInstance(AirFilter smi)
			: base(smi)
		{
		}
	}

	// Token: 0x0200151F RID: 5407
	public class States : GameStateMachine<AirFilter.States, AirFilter.StatesInstance, AirFilter>
	{
		// Token: 0x0600900D RID: 36877 RVA: 0x0035EF64 File Offset: 0x0035D164
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.waiting;
			this.waiting.EventTransition(GameHashes.OnStorageChange, this.hasFilter, (AirFilter.StatesInstance smi) => smi.master.HasFilter() && smi.master.operational.IsOperational).EventTransition(GameHashes.OperationalChanged, this.hasFilter, (AirFilter.StatesInstance smi) => smi.master.HasFilter() && smi.master.operational.IsOperational);
			this.hasFilter.EventTransition(GameHashes.OperationalChanged, this.waiting, (AirFilter.StatesInstance smi) => !smi.master.operational.IsOperational).Enter("EnableConsumption", delegate(AirFilter.StatesInstance smi)
			{
				smi.master.elementConsumer.EnableConsumption(true);
			}).Exit("DisableConsumption", delegate(AirFilter.StatesInstance smi)
			{
				smi.master.elementConsumer.EnableConsumption(false);
			})
				.DefaultState(this.hasFilter.idle);
			this.hasFilter.idle.EventTransition(GameHashes.OnStorageChange, this.hasFilter.converting, (AirFilter.StatesInstance smi) => smi.master.IsConvertable());
			this.hasFilter.converting.Enter("SetActive(true)", delegate(AirFilter.StatesInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			}).Exit("SetActive(false)", delegate(AirFilter.StatesInstance smi)
			{
				smi.master.operational.SetActive(false, false);
			}).EventTransition(GameHashes.OnStorageChange, this.hasFilter.idle, (AirFilter.StatesInstance smi) => !smi.master.IsConvertable());
		}

		// Token: 0x04006ECB RID: 28363
		public AirFilter.States.ReadyStates hasFilter;

		// Token: 0x04006ECC RID: 28364
		public GameStateMachine<AirFilter.States, AirFilter.StatesInstance, AirFilter, object>.State waiting;

		// Token: 0x02002752 RID: 10066
		public class ReadyStates : GameStateMachine<AirFilter.States, AirFilter.StatesInstance, AirFilter, object>.State
		{
			// Token: 0x0400AD7C RID: 44412
			public GameStateMachine<AirFilter.States, AirFilter.StatesInstance, AirFilter, object>.State idle;

			// Token: 0x0400AD7D RID: 44413
			public GameStateMachine<AirFilter.States, AirFilter.StatesInstance, AirFilter, object>.State converting;
		}
	}
}
