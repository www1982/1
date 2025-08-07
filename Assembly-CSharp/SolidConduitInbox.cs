using System;
using KSerialization;

// Token: 0x020007C7 RID: 1991
[SerializationConfig(MemberSerialization.OptIn)]
public class SolidConduitInbox : StateMachineComponent<SolidConduitInbox.SMInstance>, ISim1000ms
{
	// Token: 0x06003540 RID: 13632 RVA: 0x0012A384 File Offset: 0x00128584
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.filteredStorage = new FilteredStorage(this, null, null, false, Db.Get().ChoreTypes.StorageFetch);
	}

	// Token: 0x06003541 RID: 13633 RVA: 0x0012A3AA File Offset: 0x001285AA
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.filteredStorage.FilterChanged();
		base.smi.StartSM();
	}

	// Token: 0x06003542 RID: 13634 RVA: 0x0012A3C8 File Offset: 0x001285C8
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x06003543 RID: 13635 RVA: 0x0012A3D0 File Offset: 0x001285D0
	public void Sim1000ms(float dt)
	{
		if (this.operational.IsOperational && this.dispenser.IsDispensing)
		{
			this.operational.SetActive(true, false);
			return;
		}
		this.operational.SetActive(false, false);
	}

	// Token: 0x0400201F RID: 8223
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04002020 RID: 8224
	[MyCmpReq]
	private SolidConduitDispenser dispenser;

	// Token: 0x04002021 RID: 8225
	[MyCmpAdd]
	private Storage storage;

	// Token: 0x04002022 RID: 8226
	private FilteredStorage filteredStorage;

	// Token: 0x020016FA RID: 5882
	public class SMInstance : GameStateMachine<SolidConduitInbox.States, SolidConduitInbox.SMInstance, SolidConduitInbox, object>.GameInstance
	{
		// Token: 0x0600974F RID: 38735 RVA: 0x0037B9B0 File Offset: 0x00379BB0
		public SMInstance(SolidConduitInbox master)
			: base(master)
		{
		}
	}

	// Token: 0x020016FB RID: 5883
	public class States : GameStateMachine<SolidConduitInbox.States, SolidConduitInbox.SMInstance, SolidConduitInbox>
	{
		// Token: 0x06009750 RID: 38736 RVA: 0x0037B9BC File Offset: 0x00379BBC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.off;
			this.root.DoNothing();
			this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.on, (SolidConduitInbox.SMInstance smi) => smi.GetComponent<Operational>().IsOperational);
			this.on.DefaultState(this.on.idle).EventTransition(GameHashes.OperationalChanged, this.off, (SolidConduitInbox.SMInstance smi) => !smi.GetComponent<Operational>().IsOperational);
			this.on.idle.PlayAnim("on").EventTransition(GameHashes.ActiveChanged, this.on.working, (SolidConduitInbox.SMInstance smi) => smi.GetComponent<Operational>().IsActive);
			this.on.working.PlayAnim("working_pre").QueueAnim("working_loop", true, null).EventTransition(GameHashes.ActiveChanged, this.on.post, (SolidConduitInbox.SMInstance smi) => !smi.GetComponent<Operational>().IsActive);
			this.on.post.PlayAnim("working_pst").OnAnimQueueComplete(this.on);
		}

		// Token: 0x04007452 RID: 29778
		public GameStateMachine<SolidConduitInbox.States, SolidConduitInbox.SMInstance, SolidConduitInbox, object>.State off;

		// Token: 0x04007453 RID: 29779
		public SolidConduitInbox.States.ReadyStates on;

		// Token: 0x020027D7 RID: 10199
		public class ReadyStates : GameStateMachine<SolidConduitInbox.States, SolidConduitInbox.SMInstance, SolidConduitInbox, object>.State
		{
			// Token: 0x0400B0A6 RID: 45222
			public GameStateMachine<SolidConduitInbox.States, SolidConduitInbox.SMInstance, SolidConduitInbox, object>.State idle;

			// Token: 0x0400B0A7 RID: 45223
			public GameStateMachine<SolidConduitInbox.States, SolidConduitInbox.SMInstance, SolidConduitInbox, object>.State working;

			// Token: 0x0400B0A8 RID: 45224
			public GameStateMachine<SolidConduitInbox.States, SolidConduitInbox.SMInstance, SolidConduitInbox, object>.State post;
		}
	}
}
