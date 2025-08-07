using System;

// Token: 0x020006E8 RID: 1768
public class Campfire : GameStateMachine<Campfire, Campfire.Instance, IStateMachineTarget, Campfire.Def>
{
	// Token: 0x06002BF1 RID: 11249 RVA: 0x000FD040 File Offset: 0x000FB240
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.noOperational;
		this.noOperational.Enter(new StateMachine<Campfire, Campfire.Instance, IStateMachineTarget, Campfire.Def>.State.Callback(Campfire.DisableHeatEmission)).TagTransition(GameTags.Operational, this.operational, false).PlayAnim("off", KAnim.PlayMode.Once);
		this.operational.TagTransition(GameTags.Operational, this.noOperational, true).DefaultState(this.operational.needsFuel);
		this.operational.needsFuel.Enter(new StateMachine<Campfire, Campfire.Instance, IStateMachineTarget, Campfire.Def>.State.Callback(Campfire.DisableHeatEmission)).EventTransition(GameHashes.OnStorageChange, this.operational.working, new StateMachine<Campfire, Campfire.Instance, IStateMachineTarget, Campfire.Def>.Transition.ConditionCallback(Campfire.HasFuel)).PlayAnim("off", KAnim.PlayMode.Once);
		this.operational.working.Enter(new StateMachine<Campfire, Campfire.Instance, IStateMachineTarget, Campfire.Def>.State.Callback(Campfire.EnableHeatEmission)).EventTransition(GameHashes.OnStorageChange, this.operational.needsFuel, GameStateMachine<Campfire, Campfire.Instance, IStateMachineTarget, Campfire.Def>.Not(new StateMachine<Campfire, Campfire.Instance, IStateMachineTarget, Campfire.Def>.Transition.ConditionCallback(Campfire.HasFuel))).PlayAnim("on", KAnim.PlayMode.Loop)
			.Exit(new StateMachine<Campfire, Campfire.Instance, IStateMachineTarget, Campfire.Def>.State.Callback(Campfire.DisableHeatEmission));
	}

	// Token: 0x06002BF2 RID: 11250 RVA: 0x000FD160 File Offset: 0x000FB360
	public static bool HasFuel(Campfire.Instance smi)
	{
		return smi.HasFuel;
	}

	// Token: 0x06002BF3 RID: 11251 RVA: 0x000FD168 File Offset: 0x000FB368
	public static void EnableHeatEmission(Campfire.Instance smi)
	{
		smi.EnableHeatEmission();
	}

	// Token: 0x06002BF4 RID: 11252 RVA: 0x000FD170 File Offset: 0x000FB370
	public static void DisableHeatEmission(Campfire.Instance smi)
	{
		smi.DisableHeatEmission();
	}

	// Token: 0x040019F3 RID: 6643
	public const string LIT_ANIM_NAME = "on";

	// Token: 0x040019F4 RID: 6644
	public const string UNLIT_ANIM_NAME = "off";

	// Token: 0x040019F5 RID: 6645
	public GameStateMachine<Campfire, Campfire.Instance, IStateMachineTarget, Campfire.Def>.State noOperational;

	// Token: 0x040019F6 RID: 6646
	public Campfire.OperationalStates operational;

	// Token: 0x040019F7 RID: 6647
	public StateMachine<Campfire, Campfire.Instance, IStateMachineTarget, Campfire.Def>.BoolParameter WarmAuraEnabled;

	// Token: 0x0200156E RID: 5486
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006FA5 RID: 28581
		public Tag fuelTag;

		// Token: 0x04006FA6 RID: 28582
		public float initialFuelMass;
	}

	// Token: 0x0200156F RID: 5487
	public class OperationalStates : GameStateMachine<Campfire, Campfire.Instance, IStateMachineTarget, Campfire.Def>.State
	{
		// Token: 0x04006FA7 RID: 28583
		public GameStateMachine<Campfire, Campfire.Instance, IStateMachineTarget, Campfire.Def>.State needsFuel;

		// Token: 0x04006FA8 RID: 28584
		public GameStateMachine<Campfire, Campfire.Instance, IStateMachineTarget, Campfire.Def>.State working;
	}

	// Token: 0x02001570 RID: 5488
	public new class Instance : GameStateMachine<Campfire, Campfire.Instance, IStateMachineTarget, Campfire.Def>.GameInstance
	{
		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x0600913A RID: 37178 RVA: 0x00362E2F File Offset: 0x0036102F
		public bool HasFuel
		{
			get
			{
				return this.storage.MassStored() > 0f;
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x0600913B RID: 37179 RVA: 0x00362E43 File Offset: 0x00361043
		public bool IsAuraEnabled
		{
			get
			{
				return base.sm.WarmAuraEnabled.Get(this);
			}
		}

		// Token: 0x0600913C RID: 37180 RVA: 0x00362E56 File Offset: 0x00361056
		public Instance(IStateMachineTarget master, Campfire.Def def)
			: base(master, def)
		{
		}

		// Token: 0x0600913D RID: 37181 RVA: 0x00362E60 File Offset: 0x00361060
		public void EnableHeatEmission()
		{
			this.operational.SetActive(true, false);
			this.light.enabled = true;
			this.heater.EnableEmission = true;
			this.decorProvider.SetValues(CampfireConfig.DECOR_ON);
			this.decorProvider.Refresh();
		}

		// Token: 0x0600913E RID: 37182 RVA: 0x00362EB0 File Offset: 0x003610B0
		public void DisableHeatEmission()
		{
			this.operational.SetActive(false, false);
			this.light.enabled = false;
			this.heater.EnableEmission = false;
			this.decorProvider.SetValues(CampfireConfig.DECOR_OFF);
			this.decorProvider.Refresh();
		}

		// Token: 0x04006FA9 RID: 28585
		[MyCmpGet]
		public Operational operational;

		// Token: 0x04006FAA RID: 28586
		[MyCmpGet]
		public Storage storage;

		// Token: 0x04006FAB RID: 28587
		[MyCmpGet]
		public RangeVisualizer rangeVisualizer;

		// Token: 0x04006FAC RID: 28588
		[MyCmpGet]
		public Light2D light;

		// Token: 0x04006FAD RID: 28589
		[MyCmpGet]
		public DirectVolumeHeater heater;

		// Token: 0x04006FAE RID: 28590
		[MyCmpGet]
		public DecorProvider decorProvider;
	}
}
