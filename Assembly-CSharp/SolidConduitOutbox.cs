using System;
using KSerialization;
using UnityEngine;

// Token: 0x020007C8 RID: 1992
[SerializationConfig(MemberSerialization.OptIn)]
public class SolidConduitOutbox : StateMachineComponent<SolidConduitOutbox.SMInstance>
{
	// Token: 0x06003545 RID: 13637 RVA: 0x0012A40F File Offset: 0x0012860F
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06003546 RID: 13638 RVA: 0x0012A417 File Offset: 0x00128617
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.meter = new MeterController(this, Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Array.Empty<string>());
		base.Subscribe<SolidConduitOutbox>(-1697596308, SolidConduitOutbox.OnStorageChangedDelegate);
		this.UpdateMeter();
		base.smi.StartSM();
	}

	// Token: 0x06003547 RID: 13639 RVA: 0x0012A455 File Offset: 0x00128655
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x06003548 RID: 13640 RVA: 0x0012A45D File Offset: 0x0012865D
	private void OnStorageChanged(object data)
	{
		this.UpdateMeter();
	}

	// Token: 0x06003549 RID: 13641 RVA: 0x0012A468 File Offset: 0x00128668
	private void UpdateMeter()
	{
		float num = Mathf.Clamp01(this.storage.MassStored() / this.storage.capacityKg);
		this.meter.SetPositionPercent(num);
	}

	// Token: 0x0600354A RID: 13642 RVA: 0x0012A49E File Offset: 0x0012869E
	private void UpdateConsuming()
	{
		base.smi.sm.consuming.Set(this.consumer.IsConsuming, base.smi, false);
	}

	// Token: 0x04002023 RID: 8227
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04002024 RID: 8228
	[MyCmpReq]
	private SolidConduitConsumer consumer;

	// Token: 0x04002025 RID: 8229
	[MyCmpAdd]
	private Storage storage;

	// Token: 0x04002026 RID: 8230
	private MeterController meter;

	// Token: 0x04002027 RID: 8231
	private static readonly EventSystem.IntraObjectHandler<SolidConduitOutbox> OnStorageChangedDelegate = new EventSystem.IntraObjectHandler<SolidConduitOutbox>(delegate(SolidConduitOutbox component, object data)
	{
		component.OnStorageChanged(data);
	});

	// Token: 0x020016FC RID: 5884
	public class SMInstance : GameStateMachine<SolidConduitOutbox.States, SolidConduitOutbox.SMInstance, SolidConduitOutbox, object>.GameInstance
	{
		// Token: 0x06009752 RID: 38738 RVA: 0x0037BB2C File Offset: 0x00379D2C
		public SMInstance(SolidConduitOutbox master)
			: base(master)
		{
		}
	}

	// Token: 0x020016FD RID: 5885
	public class States : GameStateMachine<SolidConduitOutbox.States, SolidConduitOutbox.SMInstance, SolidConduitOutbox>
	{
		// Token: 0x06009753 RID: 38739 RVA: 0x0037BB38 File Offset: 0x00379D38
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.root.Update("RefreshConsuming", delegate(SolidConduitOutbox.SMInstance smi, float dt)
			{
				smi.master.UpdateConsuming();
			}, UpdateRate.SIM_1000ms, false);
			this.idle.PlayAnim("on").ParamTransition<bool>(this.consuming, this.working, GameStateMachine<SolidConduitOutbox.States, SolidConduitOutbox.SMInstance, SolidConduitOutbox, object>.IsTrue);
			this.working.PlayAnim("working_pre").QueueAnim("working_loop", true, null).ParamTransition<bool>(this.consuming, this.post, GameStateMachine<SolidConduitOutbox.States, SolidConduitOutbox.SMInstance, SolidConduitOutbox, object>.IsFalse);
			this.post.PlayAnim("working_pst").OnAnimQueueComplete(this.idle);
		}

		// Token: 0x04007454 RID: 29780
		public StateMachine<SolidConduitOutbox.States, SolidConduitOutbox.SMInstance, SolidConduitOutbox, object>.BoolParameter consuming;

		// Token: 0x04007455 RID: 29781
		public GameStateMachine<SolidConduitOutbox.States, SolidConduitOutbox.SMInstance, SolidConduitOutbox, object>.State idle;

		// Token: 0x04007456 RID: 29782
		public GameStateMachine<SolidConduitOutbox.States, SolidConduitOutbox.SMInstance, SolidConduitOutbox, object>.State working;

		// Token: 0x04007457 RID: 29783
		public GameStateMachine<SolidConduitOutbox.States, SolidConduitOutbox.SMInstance, SolidConduitOutbox, object>.State post;
	}
}
