using System;

// Token: 0x02000853 RID: 2131
[SkipSaveFileSerialization]
public class BlightVulnerable : StateMachineComponent<BlightVulnerable.StatesInstance>
{
	// Token: 0x06003A86 RID: 14982 RVA: 0x001459BD File Offset: 0x00143BBD
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06003A87 RID: 14983 RVA: 0x001459C5 File Offset: 0x00143BC5
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06003A88 RID: 14984 RVA: 0x001459D8 File Offset: 0x00143BD8
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x06003A89 RID: 14985 RVA: 0x001459E0 File Offset: 0x00143BE0
	public void MakeBlighted()
	{
		Debug.Log("Blighting plant", this);
		base.smi.sm.isBlighted.Set(true, base.smi, false);
	}

	// Token: 0x040023E2 RID: 9186
	private SchedulerHandle handle;

	// Token: 0x040023E3 RID: 9187
	public bool prefersDarkness;

	// Token: 0x020017D7 RID: 6103
	public class StatesInstance : GameStateMachine<BlightVulnerable.States, BlightVulnerable.StatesInstance, BlightVulnerable, object>.GameInstance
	{
		// Token: 0x06009A92 RID: 39570 RVA: 0x0038AD70 File Offset: 0x00388F70
		public StatesInstance(BlightVulnerable master)
			: base(master)
		{
		}
	}

	// Token: 0x020017D8 RID: 6104
	public class States : GameStateMachine<BlightVulnerable.States, BlightVulnerable.StatesInstance, BlightVulnerable>
	{
		// Token: 0x06009A93 RID: 39571 RVA: 0x0038AD7C File Offset: 0x00388F7C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.comfortable;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.comfortable.ParamTransition<bool>(this.isBlighted, this.blighted, GameStateMachine<BlightVulnerable.States, BlightVulnerable.StatesInstance, BlightVulnerable, object>.IsTrue);
			this.blighted.TriggerOnEnter(GameHashes.BlightChanged, (BlightVulnerable.StatesInstance smi) => true).Enter(delegate(BlightVulnerable.StatesInstance smi)
			{
				smi.GetComponent<SeedProducer>().seedInfo.seedId = RotPileConfig.ID;
			}).ToggleTag(GameTags.Blighted)
				.Exit(delegate(BlightVulnerable.StatesInstance smi)
				{
					GameplayEventManager.Instance.Trigger(-1425542080, smi.gameObject);
				});
		}

		// Token: 0x04007720 RID: 30496
		public StateMachine<BlightVulnerable.States, BlightVulnerable.StatesInstance, BlightVulnerable, object>.BoolParameter isBlighted;

		// Token: 0x04007721 RID: 30497
		public GameStateMachine<BlightVulnerable.States, BlightVulnerable.StatesInstance, BlightVulnerable, object>.State comfortable;

		// Token: 0x04007722 RID: 30498
		public GameStateMachine<BlightVulnerable.States, BlightVulnerable.StatesInstance, BlightVulnerable, object>.State blighted;
	}
}
